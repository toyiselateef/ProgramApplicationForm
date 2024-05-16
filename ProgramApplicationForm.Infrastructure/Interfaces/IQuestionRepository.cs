
using ProgramApplicationForm.Domain.Entities;
using System.Runtime.CompilerServices;

namespace ProgramApplicationForm.Infrastructure.Interfaces;

public interface IQuestionRepository
{
   
    Task<Question> AddQuestionAsync(Question question, CancellationToken cancellationToken);
    Task<Question> GetQuestionAsync(string id, CancellationToken cancellationToken);
    Task UpdateQuestionAsync(string id, Question question, CancellationToken cancellationToken);

    IAsyncEnumerable<Question> GetQuestionsForProgramAsync(string programId, [EnumeratorCancellation] CancellationToken cancellationToken);
}
