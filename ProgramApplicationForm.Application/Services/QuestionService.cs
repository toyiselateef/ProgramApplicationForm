using AutoMapper;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Application.Exceptions;
using ProgramApplicationForm.Application.Interfaces;
using ProgramApplicationForm.Domain.Entities;
using ProgramApplicationForm.Domain.Enums;
using ProgramApplicationForm.Infrastructure.Interfaces;
using System.Reflection;
using System.Runtime.CompilerServices;

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
    public async Task<QuestionDto> CreateQuestionAsync(QuestionDto createQuestionDto, CancellationToken cancellationToken)
    {
        Question question = MapQuestionDto(createQuestionDto);
       
       
        return mapper.Map<QuestionDto>(await questionRepository.AddQuestionAsync(question, cancellationToken));
    }

    public async Task<QuestionDto> GetQuestionByIdAsync(string id, CancellationToken cancellationToken)
    {
        Question response = await questionRepository.GetQuestionAsync(id, cancellationToken);

        return mapper.Map<QuestionDto>(response);
    }

    public async Task<IEnumerable<QuestionDto>> GetQuestionsAsync(string programId, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var result = await questionRepository.GetQuestionsForProgramAsync(programId, cancellationToken);
        return mapper.Map<IEnumerable<QuestionDto>>(result);
    }

    public async Task UpdateQuestionAsync(string id, QuestionDto questionDto, CancellationToken cancellationToken)
    {
        var existingQuestion = await questionRepository.GetQuestionAsync(id, cancellationToken);
        if (existingQuestion == null)
        {
            throw new NotFoundException("no such question");
        }

        await questionRepository.UpdateQuestionAsync(id, mapper.Map<Question>(existingQuestion), cancellationToken)//.ConfigureAwait(false)
                            ;
    }


    private Question MapQuestionDto(QuestionDto questionDto)
    {
        return questionDto.Type switch
        {
            QuestionTypes.Paragraph => new ParagraphQuestion { Id = questionDto.Id, Type = questionDto.Type, QuestionText = questionDto.QuestionText },
            QuestionTypes.YesNo => new YesNoQuestion { Id = questionDto.Id, Type = questionDto.Type, QuestionText = questionDto.QuestionText },
            QuestionTypes.DropDown => new DropdownQuestion { Id = questionDto.Id, Type = questionDto.Type, QuestionText = questionDto.QuestionText, Options = questionDto.Options },
            QuestionTypes.MultipleChoice => new MultipleChoiceQuestion { Id = questionDto.Id, Type = questionDto.Type, QuestionText = questionDto.QuestionText, Options = questionDto.Options },
            QuestionTypes.Date => new DateQuestion { Id = questionDto.Id, Type = questionDto.Type, QuestionText = questionDto.QuestionText },
            QuestionTypes.Number => new NumericQuestion { Id = questionDto.Id, Type = questionDto.Type, QuestionText = questionDto.QuestionText },
            _ => throw new ArgumentException("Invalid question type", nameof(questionDto.Type))
        };
    }
}
