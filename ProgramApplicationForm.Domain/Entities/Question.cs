
using Newtonsoft.Json;
using ProgramApplicationForm.Domain.Enums; 

namespace ProgramApplicationForm.Domain.Entities;
public  class Question
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ApplicationFormId { get; set; }
    public QuestionTypes Type { get; set; }
    public string QuestionText { get; set; }
    public string CreatedBy { get; set; }

}



