
namespace ProgramApplicationForm.Application.Dtos;

public class DateQuestionDto  : QuestionDto
{
    public DateTime MinDate { get; set; }
    public DateTime MaxDate { get; set; }
}

