
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
         
    }

}
