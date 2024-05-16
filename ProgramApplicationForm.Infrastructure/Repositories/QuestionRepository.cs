

using ProgramApplicationForm.Domain.Entities;
using ProgramApplicationForm.Infrastructure.Interfaces;
using System.Runtime.CompilerServices;

namespace ProgramApplicationForm.Infrastructure.Repositories;
public class QuestionRepository : IQuestionRepository
{
    public Task<Question> AddQuestionAsync(Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Question> GetQuestionAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<Question> GetQuestionsForProgramAsync(string programId, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateQuestionAsync(string id, Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}