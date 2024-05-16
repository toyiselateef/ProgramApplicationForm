
using AutoMapper;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Domain.Entities;

namespace ApplicationForm.Application.Profiles;


public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<QuestionDto, Question>().IncludeAllDerived();
        CreateMap<ParagraphQuestionDto, ParagraphQuestion>().ReverseMap();
        CreateMap<NumericQuestionDto, NumericQuestion>().ReverseMap();
        CreateMap<DateQuestionDto, DateQuestion>().ReverseMap();
        CreateMap<DropdownQuestionDto, DropdownQuestion>().ReverseMap();
        CreateMap<YesNoQuestionDto, YesNoQuestion>().ReverseMap();
        CreateMap<MultipleChoiceQuestionDto, MultipleChoiceQuestion>().ReverseMap();


        CreateMap<Question, QuestionDto>().IncludeAllDerived();


        CreateMap<Answer, AnswerDto>().ReverseMap();
        CreateMap<Question, ReadQuestionDto>().IncludeAllDerived().ReverseMap();
        //CreateMap<Question, ParagraphQuestion>().ReverseMap();


        CreateMap<ApplicationData, ApplicationDataDto>().ReverseMap();





        //CreateMap<QuestionDto, Question>()
        //       .ForMember(dest => dest.Id, opt => opt.Ignore())
        //       .IncludeAllDerived();// Assuming Id is generated elsewhere
        //       //.Include<ParagraphQuestionDto, ParagraphQuestion>()
        //       //.Include<YesNoQuestionDto, YesNoQuestion>()
        //.Include<DropdownQuestionDto, DropdownQuestion>()
        //.Include<MultipleChoiceQuestionDto, MultipleChoiceQuestion>()
        //.Include<DateQuestionDto, DateQuestion>()
        //.Include<NumericQuestionDto, NumericQuestion>();

        //CreateMap<QuestionDto, ParagraphQuestion>()
        //    .IncludeMembers(s => s.Type)
        //    .IncludeMembers(s => s.QuestionText)
        //    .IncludeMembers(s => s.ApplicationFormId);
        //CreateMap<QuestionDto, YesNoQuestion>()
        //    .IncludeMembers(s => s.Type)
        //    .IncludeMembers(s => s.QuestionText)
        //    .IncludeMembers(s => s.ApplicationFormId);
        //CreateMap<QuestionDto, DropdownQuestion>()
        //    .IncludeMembers(s => s.Type)
        //    .IncludeMembers(s => s.QuestionText)
        //    .IncludeMembers(s => s.ApplicationFormId);
        //CreateMap<QuestionDto, MultipleChoiceQuestion>()
        //    .IncludeMembers(s => s.Type)
        //    .IncludeMembers(s => s.QuestionText)
        //    .IncludeMembers(s => s.ApplicationFormId);
        //CreateMap<QuestionDto, DateQuestion>()
        //    .IncludeMembers(s => s.Type)
        //    .IncludeMembers(s => s.QuestionText)
        //    .IncludeMembers(s => s.ApplicationFormId);
        //CreateMap<QuestionDto, NumericQuestion>()
        //    .IncludeMembers(s => s.Type)
        //    .IncludeMembers(s => s.QuestionText)
        //    .IncludeMembers(s => s.ApplicationFormId);

        // Mapping from specific question types to ReadQuestionDto
        //CreateMap<Question, ReadQuestionDto>()
        //    .Include<ParagraphQuestion, ParagraphQuestionDto>()
        //    .Include<YesNoQuestion, YesNoQuestionDto>()
        //    .Include<DropdownQuestion, DropdownQuestionDto>()
        //    .Include<MultipleChoiceQuestion, MultipleChoiceQuestionDto>()
        //    .Include<DateQuestion, DateQuestionDto>()
        //    .Include<NumericQuestion, NumericQuestionDto>();

        //CreateMap<ParagraphQuestion, ParagraphQuestionDto>();
        //CreateMap<YesNoQuestion, YesNoQuestionDto>();
        //CreateMap<DropdownQuestion, DropdownQuestionDto>();
        //CreateMap<MultipleChoiceQuestion, MultipleChoiceQuestionDto>();
        //CreateMap<DateQuestion, DateQuestionDto>();
        //CreateMap<NumericQuestion, NumericQuestionDto>();

        // Mapping from specific question types DTOs to Question
        //CreateMap<ParagraphQuestionDto, ParagraphQuestion>()
        //    .IncludeBase<ReadQuestionDto, Question>();
        //CreateMap<YesNoQuestionDto, YesNoQuestion>()
        //    .IncludeBase<ReadQuestionDto, Question>();
        //CreateMap<DropdownQuestionDto, DropdownQuestion>()
        //    .IncludeBase<ReadQuestionDto, Question>();
        //CreateMap<MultipleChoiceQuestionDto, MultipleChoiceQuestion>()
        //    .IncludeBase<ReadQuestionDto, Question>();
        //CreateMap<DateQuestionDto, DateQuestion>()
        //    .IncludeBase<ReadQuestionDto, Question>();
        //CreateMap<NumericQuestionDto, NumericQuestion>()
        //    .IncludeBase<ReadQuestionDto, Question>();


    }

}
