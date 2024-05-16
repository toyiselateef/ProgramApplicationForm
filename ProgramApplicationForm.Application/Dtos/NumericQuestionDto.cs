
namespace ProgramApplicationForm.Application.Dtos;

public class NumericQuestionDto  : ReadQuestionDto
{
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}
