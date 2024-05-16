using ApplicationForm.Application.Services;
using Moq;
using ProgramApplicationForm.Application.Interfaces; 
using ProgramApplicationForm.Infrastructure.Interfaces;
using AutoMapper;
using Bogus;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Domain.Enums;
using ProgramApplicationForm.Domain.Entities; 

namespace ProgramApplicationForm.Tests.Services;

public class QuestionServiceTests
{
    private readonly Mock<IQuestionRepository> questionRepoMock;
    private readonly Mock<IProgramApplicationFormRepository> programFormRepoMock;
    private readonly Mock<IMapper> mapperMock;
    private readonly IQuestionService questionService;
    private readonly Faker<QuestionDto> faker;
    public QuestionServiceTests()
    {
        questionRepoMock = new Mock<IQuestionRepository>();
        programFormRepoMock = new Mock<IProgramApplicationFormRepository>();
        mapperMock = new Mock<IMapper>();
        questionService = new QuestionService(questionRepoMock.Object, programFormRepoMock.Object, mapperMock.Object);
        faker = new Faker<QuestionDto>()
            .RuleFor(q => q.Type, f => f.PickRandom(QuestionTypes.Paragraph, QuestionTypes.YesNo, QuestionTypes.Number, QuestionTypes.Date, QuestionTypes.MultipleChoice, QuestionTypes.DropDown))
            .RuleFor(q => q.QuestionText, f => f.Lorem.Sentence())
            .RuleFor(q => q.Options, f => f.Make(f.Random.Int(1, 5), () => f.Lorem.Word()))
            .RuleFor(q => q.MaxSelection, f => f.Random.Int(1, 5));
    }


    [Fact]
    public async Task AddQuestionAsync_Should_Add_Question()
    {
        var newQuestionDto = faker.Generate();

        await questionService.CreateQuestionAsync(newQuestionDto, CancellationToken.None);

        questionRepoMock.Verify(s => s.AddQuestionAsync(It.Is<Question>(q =>
            q.Type == newQuestionDto.Type &&
            q.QuestionText == newQuestionDto.QuestionText), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task UpdateQuestionAsync_Should_Update_Existing_Question()
    {
        var existingQuestion = new ParagraphQuestion { Id = Guid.NewGuid().ToString(), Type = QuestionTypes.Paragraph, QuestionText = "Old Text" };
        var updateQuestionDto = faker.Generate();

        questionRepoMock.Setup(s => s.GetQuestionAsync(existingQuestion.Id, It.IsAny<CancellationToken>())).ReturnsAsync(existingQuestion);

        await questionService.UpdateQuestionAsync(existingQuestion.Id, updateQuestionDto, CancellationToken.None);

        questionRepoMock.Verify(s => s.UpdateQuestionAsync(existingQuestion.Id, It.Is<Question>(q =>
            q.Type == updateQuestionDto.Type &&
            q.QuestionText == updateQuestionDto.QuestionText              
             ), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetQuestionsAsync_Should_Return_Questions()
    {
        // Arrange
        
        IAsyncEnumerable<Question> questions = CreateQuestionsAsync();

        questionRepoMock.Setup(s => s.GetQuestionsForProgramAsync(Guid.NewGuid().ToString(), It.IsAny<CancellationToken>())).Returns(questions);

        // Act 
        var result = new List<QuestionDto>();
        await foreach (var question in questionService.GetQuestionsAsync(Guid.NewGuid().ToString(), CancellationToken.None))
        {
            result.Add(question);
        }

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, q => q.QuestionText == "Question 1");
        Assert.Contains(result, q => q.QuestionText == "Question 2");
    }

    private async IAsyncEnumerable<Question> CreateQuestionsAsync()
    {
        yield return new ParagraphQuestion { Id = Guid.NewGuid().ToString(), Type = QuestionTypes.Paragraph, QuestionText = "Question 1" };
        yield return new YesNoQuestion { Id = Guid.NewGuid().ToString(), Type = QuestionTypes.YesNo, QuestionText = "Question 2" };
    }
}

 