using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
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
            ItemResponse<dynamic> response = await questionsContainer.ReadItemAsync<dynamic>(id, new PartitionKey(id), cancellationToken: cancellationToken);
            var resource = response.Resource.ToString();

            if (IsOfType<ParagraphQuestion>(resource, out ParagraphQuestion paragraphQuestion))
            {
                return paragraphQuestion;
            }
            if (IsOfType<YesNoQuestion>(resource, out YesNoQuestion yesNoQuestion))
            {
                return yesNoQuestion;
            }
            if (IsOfType<MultipleChoiceQuestion>(resource, out MultipleChoiceQuestion multipleChoiceQuestion))
            {
                return multipleChoiceQuestion;
            }
            if (IsOfType<DropdownQuestion>(resource, out DropdownQuestion dropdownQuestion))
            {
                return dropdownQuestion;
            }
            if (IsOfType<NumericQuestion>(resource, out NumericQuestion numericQuestion))
            {
                return numericQuestion;
            }
            if (IsOfType<DateQuestion>(resource, out DateQuestion dateQuestion))
            {
                return dateQuestion;
            }

            return JsonConvert.DeserializeObject<Question>(resource);
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

    private bool IsOfType<T>(string resource, out T result) where T : Question
    {
        try
        {
            result = JsonConvert.DeserializeObject<T>(resource);
            return result != null;
        }
        catch (JsonSerializationException)
        {
            result = null;
            return false;
        }
    }
}