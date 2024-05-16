

namespace ProgramApplicationForm.Application.Dtos;
 

public class ApplicationDataDto 
{
    public string Id { get; set; }
    public string ApplicationFormId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public List<AnswerDto> Answers { get; set; }
    public DateTime SubmittedAt { get; set; }
}


public class AnswerDto
{
    public string QuestionId { get; set; }
    public object AnswerContent { get; set; }
}