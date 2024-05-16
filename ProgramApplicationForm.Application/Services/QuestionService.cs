using AutoMapper;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Application.Interfaces;
using ProgramApplicationForm.Infrastructure.Interfaces; 

namespace ApplicationForm.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository questionRepository;
    private readonly IProgramApplicationFormRepository programFormRepository;
    private readonly IMapper mapper;

    public QuestionService(IQuestionRepository questionRepository, IProgramApplicationFormRepository programFormRepository, IMapper mapper)
    {
        this.questionRepository = questionRepository;
        this.programFormRepository = programFormRepository;
        this.mapper = mapper;
    }
    public Task<QuestionDto> CreateQuestionAsync(QuestionDto createQuestionDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<QuestionDto> GetQuestionByIdAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<QuestionDto> GetQuestionsAsync(string programId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateQuestionAsync(string id, QuestionDto questionDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
