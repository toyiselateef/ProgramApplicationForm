

using Microsoft.Azure.Cosmos;
using ProgramApplicationForm.Domain.Entities;
using ProgramApplicationForm.Infrastructure.Common;
using ProgramApplicationForm.Infrastructure.DAL;
using ProgramApplicationForm.Infrastructure.Interfaces; 
using System.Runtime.CompilerServices;

namespace ProgramApplicationForm.Infrastructure.Repositories;
public class QuestionRepository : IQuestionRepository
{
    private readonly CosmosClient cosmosClient;
    private readonly Container questionsContainer;
    private readonly Container applicationFormContainer;

    public QuestionRepository(DBContext context)
    {
        questionsContainer = context.GetContainer(DataConstants.questContainerId);
        applicationFormContainer = context.GetContainer(DataConstants.appsContainerId);
    }

    public async Task<Question> AddQuestionAsync(Question question, CancellationToken cancellationToken)
    {
        var result = await questionsContainer.CreateItemAsync(question, new PartitionKey(question.Id), cancellationToken: cancellationToken);

        return result.Resource;
    }

    public async Task<Question> GetQuestionAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            ItemResponse<Question> response = await questionsContainer.ReadItemAsync<Question>(id, new PartitionKey(id), cancellationToken: cancellationToken);
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Question>> GetQuestionsForProgramAsync(string programApplicationFormId, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var query = new QueryDefinition("SELECT * FROM c WHERE c.ApplicationFormId = @ApplicationFormId")
            .WithParameter("@ApplicationFormId", programApplicationFormId);
        var resultSetIterator = questionsContainer.GetItemQueryIterator<Question>(query);
        List<Question> questions = new List<Question>();

        while (resultSetIterator.HasMoreResults)
        {
            var response = await resultSetIterator.ReadNextAsync(cancellationToken: cancellationToken);
            questions.AddRange(response.ToList());
        }

        return questions;

    }

    public async Task UpdateQuestionAsync(string id, Question question, CancellationToken cancellationToken)
    {
        await questionsContainer.UpsertItemAsync(question, new PartitionKey(id), cancellationToken: cancellationToken);
    }
}