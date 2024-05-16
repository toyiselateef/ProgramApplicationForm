
using ProgramApplicationForm.Application.Dtos;
using System.Runtime.CompilerServices;

namespace ProgramApplicationForm.Application.Interfaces;

public interface IQuestionService
{
    Task UpdateQuestionAsync(string id, QuestionDto questionDto, CancellationToken cancellationToken);
    Task<ReadQuestionDto> CreateQuestionAsync(QuestionDto createQuestionDto, CancellationToken cancellationToken);
    Task<IEnumerable<ReadQuestionDto>> GetQuestionsAsync(string programId, CancellationToken cancellationToken);
    Task<ReadQuestionDto> GetQuestionByIdAsync(string id, CancellationToken cancellationToken);

}
