

using ProgramApplicationForm.Domain.Entities;

namespace ProgramApplicationForm.Domain.Entities;
public class DateQuestion : Question
{
    public DateTime MinDate { get; set; }
    public DateTime MaxDate { get; set; }
}