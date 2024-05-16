
using ProgramApplicationForm.Application.Dtos;
using System.Runtime.CompilerServices;

namespace ProgramApplicationForm.Application.Interfaces;

public interface IQuestionService
{
    Task UpdateQuestionAsync(string id, QuestionDto questionDto, CancellationToken cancellationToken);
    Task<QuestionDto> CreateQuestionAsync(QuestionDto createQuestionDto, CancellationToken cancellationToken);
    IAsyncEnumerable<QuestionDto> GetQuestionsAsync(string programId, CancellationToken cancellationToken);
    Task<QuestionDto> GetQuestionByIdAsync(string id, CancellationToken cancellationToken);

}
