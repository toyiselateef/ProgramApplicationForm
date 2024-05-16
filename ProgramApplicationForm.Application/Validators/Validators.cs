 using FluentValidation;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Domain.Enums;

namespace ProgramApplicationForm.Api.Validators;

public class NumericQuestionValidator : AbstractValidator<NumericQuestionDto>
{
    public NumericQuestionValidator()
    {
        Include(new QuestionValidator());
        RuleFor(q => q.MinValue).LessThanOrEqualTo(q => q.MaxValue).WithMessage("Min value must be less than or equal to max value.");
        RuleFor(q => q.MaxValue).GreaterThanOrEqualTo(q => q.MinValue).WithMessage("Max value must be greater than or equal to min value.");
    }
}
public class ParagraphQuestionValidator : AbstractValidator<ParagraphQuestionDto>
{
    public ParagraphQuestionValidator()
    {
        Include(new QuestionValidator());
        RuleFor(q => q.MaxLength).GreaterThan(0).WithMessage("Max length must be greater than zero.");
    }
}

public class QuestionValidator : AbstractValidator<QuestionDto>
{
    public QuestionValidator()
    {
        RuleFor(q => q.ApplicationFormId).NotEmpty()
                                    .WithMessage("Program Application Form ID is required.");
        RuleFor(q => q.Type).NotEmpty()
                                    .WithMessage("Question type is required.");
        RuleFor(q => q.QuestionText).NotEmpty()
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
public class MultipleChoiceQuestionValidator : AbstractValidator<MultipleChoiceQuestionDto>
{
    public MultipleChoiceQuestionValidator()
    {
        Include(new QuestionValidator());
        RuleFor(q => q.Options).NotEmpty().WithMessage("Options must not be empty.")
                               .Must(options => options.Count >= 2).WithMessage("There must be at least two options.");

    }
}
public class YesNoQuestionValidator : AbstractValidator<YesNoQuestionDto>
{
    public YesNoQuestionValidator()
    {
        Include(new QuestionValidator());
    }
}
public class DropdownQuestionValidator : AbstractValidator<DropdownQuestionDto>
{
    public DropdownQuestionValidator()
    {
        Include(new QuestionValidator());
        RuleFor(q => q.Options).NotEmpty().WithMessage("Options must not be empty.")
                               .Must(options => options.Count >= 2).WithMessage("There must be at least two options.");
        RuleFor(q => q.MaxSelection).GreaterThan(0).WithMessage("Max selection must be greater than zero.")
                                 .LessThanOrEqualTo(q => q.Options.Count).WithMessage("Max selection must be less than or equal to the number of options.");
    }
}
public class DateQuestionValidator : AbstractValidator<DateQuestionDto>
{
    public DateQuestionValidator()
    {
        Include(new QuestionValidator());
        RuleFor(q => q.MinDate).LessThanOrEqualTo(q => q.MaxDate).WithMessage("Min date must be less than or equal to max date.");
        RuleFor(q => q.MaxDate).GreaterThanOrEqualTo(q => q.MinDate).WithMessage("Max date must be greater than or equal to min date.");
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

