
namespace ProgramApplicationForm.Application.Dtos;

public class DateQuestionDto  : ReadQuestionDto
{
    public DateTime MinDate { get; set; }
    public DateTime MaxDate { get; set; }
}

