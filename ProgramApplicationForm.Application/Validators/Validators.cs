using FluentValidation;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Domain.Enums;

namespace ProgramApplicationForm.Api.Validators;
 
public class QuestionValidator : AbstractValidator<QuestionDto>
{
    public QuestionValidator()
    {
        RuleFor(q => q.ApplicationFormId).NotEmpty()
                                    .WithMessage("Program Application Form ID is required.");
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required.")
            .Must(type => new[] { QuestionTypes.Paragraph, QuestionTypes.YesNo, QuestionTypes.Number, QuestionTypes.Date, QuestionTypes.MultipleChoice, QuestionTypes.DropDown }.Contains(type))
            .WithMessage("Invalid question type.");


        RuleFor(q => q.QuestionText)
            .NotEmpty()
            .WithMessage("Question text is required.");
        RuleFor(q => q.Options).NotEmpty()
            .When(q => q.Type == QuestionTypes.DropDown || q.Type == QuestionTypes.MultipleChoice)
            .WithMessage("Options are required for Dropdown and MultipleChoice questions.");
        RuleFor(q => q.MaxSelection).GreaterThan(0)
            .WithMessage("Max selection for Dropdown must be greater than zero for Dropdown questions.")
            .LessThanOrEqualTo(q => q.Options.Count).WithMessage("Max selection must be less than or equal to the number of options.")
            .When(q => q.Type == QuestionTypes.DropDown);

        RuleFor(q => q.MaxLength).GreaterThan(0).When(q => q.Type == QuestionTypes.Paragraph)
                                    .WithMessage("Max length must be greater than zero for paragraph question.");
    }
}


public class AnswerValidator : AbstractValidator<AnswerDto>
{
    public AnswerValidator()
    {
        RuleFor(a => a.QuestionId).NotEmpty().WithMessage("Question ID is required.");
        RuleFor(a => a.AnswerContent).NotNull().WithMessage("Answer content is required.");
    }
}


public class ApplicationDataValidator : AbstractValidator<ApplicationDataDto>
{
    public ApplicationDataValidator()
    {
        RuleFor(a => a.ApplicationFormId).NotEmpty().WithMessage("Application Program ID is required.");
        RuleFor(a => a.FullName).NotEmpty().WithMessage("Full name is required.");
        RuleFor(a => a.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(a => a.Phone).NotEmpty().WithMessage("Phone number is required.");
        RuleFor(a => a.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(a => a.Answers).NotEmpty().WithMessage("At least one answer is required.");
        RuleForEach(a => a.Answers).SetValidator(new AnswerValidator());
        RuleFor(a => a.SubmittedAt).NotEmpty().WithMessage("Submission date is required.");
    }
}

