
namespace ProgramApplicationForm.Application.Dtos;

public class DropdownQuestionDto  : QuestionDto
{
    public List<string> Options { get; set; }
    public int MaxSelection { get; set; }
}


