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
    public async Task<ReadQuestionDto> CreateQuestionAsync(QuestionDto createQuestionDto, CancellationToken cancellationToken)
    {
        Question question = MapQuestionDto(createQuestionDto);

        question.Id = Guid.NewGuid().ToString();

        return mapper.Map<ReadQuestionDto>(await questionRepository.AddQuestionAsync(question, cancellationToken));
    }

    public async Task<ReadQuestionDto> GetQuestionByIdAsync(string id, CancellationToken cancellationToken)
    {
        Question response = await questionRepository.GetQuestionAsync(id, cancellationToken);

        return MapBackQuestionDtos(response);
    }

    public async Task<IEnumerable<ReadQuestionDto>> GetQuestionsAsync(string programId, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var result = await questionRepository.GetQuestionsForProgramAsync(programId, cancellationToken);
        return MapBackQuestionDtoEnumerable(result);
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
            QuestionTypes.Paragraph => new ParagraphQuestion
            {
                ApplicationFormId = questionDto.ApplicationFormId,
                Type = questionDto.Type,
                QuestionText = questionDto.QuestionText,
                MaxLength = questionDto.MaxLength,
                CreatedBy = questionDto.CreatedBy
            },
            QuestionTypes.YesNo => new YesNoQuestion
            {
                ApplicationFormId = questionDto.ApplicationFormId,
                Type = questionDto.Type,
                QuestionText = questionDto.QuestionText,
                CreatedBy = questionDto.CreatedBy,
                DefaultAnswer = questionDto.DefaultAnswer
            },
            QuestionTypes.DropDown => new DropdownQuestion
            {
                ApplicationFormId = questionDto.ApplicationFormId,
                Type = questionDto.Type,
                QuestionText = questionDto.QuestionText,
                Options = questionDto.Options,
                MaxSelection = questionDto.MaxSelection,
                CreatedBy = questionDto.CreatedBy
            },
            QuestionTypes.MultipleChoice => new MultipleChoiceQuestion
            {
                ApplicationFormId = questionDto.ApplicationFormId,
                Type = questionDto.Type,
                QuestionText = questionDto.QuestionText,
                Options = questionDto.Options,
                CreatedBy = questionDto.CreatedBy
            },
            QuestionTypes.Date => new DateQuestion
            {
                ApplicationFormId = questionDto.ApplicationFormId,
                Type = questionDto.Type,
                QuestionText = questionDto.QuestionText,
                CreatedBy = questionDto.CreatedBy,
                MaxDate = questionDto.MaxDate,
                MinDate = questionDto.MinDate
            },
            QuestionTypes.Number => new NumericQuestion
            {
                ApplicationFormId = questionDto.ApplicationFormId,
                Type = questionDto.Type,
                QuestionText = questionDto.QuestionText,
                CreatedBy = questionDto.CreatedBy,
                MinValue = questionDto.MinValue,
                MaxValue = questionDto.MaxValue
            },
            _ => throw new ArgumentException("Invalid question type", nameof(questionDto.Type))
        };

    }

    
    private IEnumerable<ReadQuestionDto> MapBackQuestionDtoEnumerable(IEnumerable<Question> questions)
    {
        var dtos = new List<ReadQuestionDto>();

        foreach (var question in questions)
        {

            dtos.Add(MapBackQuestionDtos(question));
        }

        return dtos;
    }

     
    private ReadQuestionDto MapBackQuestionDto(Question question)
    {
        if (question is ParagraphQuestion pq)
        {
            return new ParagraphQuestionDto
            {
                Id = pq.Id,
                ApplicationFormId = pq.ApplicationFormId,
                Type = pq.Type,
                QuestionText = pq.QuestionText,
                MaxLength = pq.MaxLength
            };
        }
        else if (question is YesNoQuestion ynq)
        {
            return new YesNoQuestionDto
            {
                Id = ynq.Id,
                ApplicationFormId = ynq.ApplicationFormId,
                Type = ynq.Type,
                QuestionText = ynq.QuestionText,
                DefaultAnswer = ynq.DefaultAnswer
            };
        }
        else if (question is DropdownQuestion ddq)
        {
            return new DropdownQuestionDto
            {
                Id = ddq.Id,
                ApplicationFormId = ddq.ApplicationFormId,
                Type = ddq.Type,
                QuestionText = ddq.QuestionText,
                Options = ddq.Options,
                MaxSelection = ddq.MaxSelection
            };
        }
        else if (question is MultipleChoiceQuestion mcq)
        {
            return new MultipleChoiceQuestionDto
            {
                Id = mcq.Id,
                ApplicationFormId = mcq.ApplicationFormId,
                Type = mcq.Type,
                QuestionText = mcq.QuestionText,
                Options = mcq.Options
            };
        }
        else if (question is DateQuestion dq)
        {
            return new DateQuestionDto
            {
                Id = dq.Id,
                ApplicationFormId = dq.ApplicationFormId,
                Type = dq.Type,
                QuestionText = dq.QuestionText,
                MaxDate = dq.MaxDate,
                MinDate = dq.MinDate
            };
        }
        else if (question is NumericQuestion nq)
        {
            return new NumericQuestionDto
            {
                Id = nq.Id,
                ApplicationFormId = nq.ApplicationFormId,
                Type = nq.Type,
                QuestionText = nq.QuestionText,
                MinValue = nq.MinValue,
                MaxValue = nq.MaxValue
            };
        }
        else
        {
            throw new ArgumentException("Invalid question type", nameof(question.Type));
        }
    }
    ///

    private ReadQuestionDto MapBackQuestionDtos(Question question)
    {
        switch (question.Type)
        {
            case QuestionTypes.Paragraph:
                var pq = (ParagraphQuestion)question;
                return new ParagraphQuestionDto
                {
                    ApplicationFormId = pq.ApplicationFormId,
                    Type = pq.Type,
                    QuestionText = pq.QuestionText,
                    MaxLength = pq.MaxLength
                };

            case QuestionTypes.YesNo:
                var ynq = (YesNoQuestion)question;
                return new YesNoQuestionDto
                {
                    ApplicationFormId = ynq.ApplicationFormId,
                    Type = ynq.Type,
                    QuestionText = ynq.QuestionText,
                    DefaultAnswer = ynq.DefaultAnswer
                };

            case QuestionTypes.DropDown:
                var ddq = (DropdownQuestion)question;
                return new DropdownQuestionDto
                {
                    ApplicationFormId = ddq.ApplicationFormId,
                    Type = ddq.Type,
                    QuestionText = ddq.QuestionText,
                    Options = ddq.Options,
                    MaxSelection = ddq.MaxSelection
                };

            case QuestionTypes.MultipleChoice:
                var mcq = (MultipleChoiceQuestion)question;
                return new MultipleChoiceQuestionDto
                {
                    ApplicationFormId = mcq.ApplicationFormId,
                    Type = mcq.Type,
                    QuestionText = mcq.QuestionText,
                    Options = mcq.Options
                };

            case QuestionTypes.Date:
                var dq = (DateQuestion)question;
                return new DateQuestionDto
                {
                    ApplicationFormId = dq.ApplicationFormId,
                    Type = dq.Type,
                    QuestionText = dq.QuestionText,
                    MaxDate = dq.MaxDate,
                    MinDate = dq.MinDate
                };

            case QuestionTypes.Number:
                var nq = (NumericQuestion)question;
                return new NumericQuestionDto
                {
                    ApplicationFormId = nq.ApplicationFormId,
                    Type = nq.Type,
                    QuestionText = nq.QuestionText,
                    MinValue = nq.MinValue,
                    MaxValue = nq.MaxValue
                };

            default:
                throw new ArgumentException("Invalid question type", nameof(question.Type));
        }
    }


}