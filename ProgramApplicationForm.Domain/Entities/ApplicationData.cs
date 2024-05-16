
namespace ProgramApplicationForm.Domain.Entities;

 


public class ApplicationData
{
    public string Id { get; set; }
    public string ApplicationFormId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public List<Answer> Answers { get; set; }
    public DateTime SubmittedAt { get; set; }
}


public class Answer
{
    public string QuestionId { get; set; }
    public object AnswerContent { get; set; }
}