﻿ 
namespace ProgramApplicationForm.Domain.Entities;
public class ApplicationForm
{

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProgramTitle { get; set; }
    public string ProgramDescription { get; set; }
    public string CreatedBy { get; set; }   


}
