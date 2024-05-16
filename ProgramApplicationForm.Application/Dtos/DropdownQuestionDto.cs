
namespace ProgramApplicationForm.Application.Dtos;

public class DropdownQuestionDto  : ReadQuestionDto
{
    public List<string> Options { get; set; }
    public int MaxSelection { get; set; }
}


