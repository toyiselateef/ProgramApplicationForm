
using ApplicationForm.Domain.Enums;

namespace ProgramApplicationForm.Application.Dtos;
 
public class CreateQuestionDto
{
    public string Id { get; set; }
    public string ApplicationFormId { get; set; }
    public string Type { get; set; }
    public string QuestionText { get; set; }
    public string CreatedBy { get; set; }
}




public class QuestionDto
{
    public string Id { get; set; }
    public string ApplicationFormId { get; set; }
    public QuestionTypes Type { get; set; }
    public string QuestionText { get; set; }
    public string CreatedBy { get; set; }
    public List<string> Options { get; set; } // For Dropdown and MultipleChoice
    public int MaxLength { get; set; } // For Paragraph
    public int MinValue { get; set; } // For Numeric
    public int MaxValue { get; set; } // For Numeric
    public DateTime MinDate { get; set; } // For Date
    public DateTime MaxDate { get; set; } // For Date
    public bool DefaultAnswer { get; set; } // For YesNo
    public int MaxSelection { get; set; } // For Dropdown
}
