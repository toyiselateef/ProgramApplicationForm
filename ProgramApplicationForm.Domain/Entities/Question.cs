 
namespace ProgramApplicationForm.Domain.Entities;
public abstract class Question
{
    public string Id { get; set; }
    public string ApplicationFormId { get; set; }
    public string Type { get; set; }
    public string QuestionText { get; set; }
    public string CreatedBy { get; set; }

}



