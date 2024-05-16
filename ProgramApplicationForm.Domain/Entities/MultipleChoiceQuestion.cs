using ProgramApplicationForm.Domain.Entities;

public class MultipleChoiceQuestion : Question
{
    public List<string> Options { get; set; }
    public int MaxChoices { get; set; }
}