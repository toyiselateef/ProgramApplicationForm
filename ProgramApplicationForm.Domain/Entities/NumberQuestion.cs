namespace ProgramApplicationForm.Domain.Entities;

public class NumericQuestion : Question
{
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}
