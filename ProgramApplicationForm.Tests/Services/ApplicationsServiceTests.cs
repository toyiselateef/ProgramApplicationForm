﻿using ApplicationForm.Application.Services;
using Moq;
using ProgramApplicationForm.Application.Interfaces;
using ProgramApplicationForm.Infrastructure.Interfaces;
using AutoMapper;
using Bogus;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Domain.Entities;

namespace ProgramApplicationForm.Tests.Services;

public class ApplicationsServiceTests
{
    private readonly Mock<IApplicationsRepository> applicationsRepoMock;
    private readonly Mock<IMapper> mapperMock;
    private readonly IApplicationsService applicationsService;
    private readonly Faker<ApplicationDataDto> faker;
    public ApplicationsServiceTests()
    {
        applicationsRepoMock = new Mock<IApplicationsRepository>();
        mapperMock = new Mock<IMapper>();
        applicationsService = new ApplicationsService(applicationsRepoMock.Object, mapperMock.Object);
        faker = new Faker<ApplicationDataDto>()
            .RuleFor(a => a.Id, f => f.Random.Guid().ToString())
            .RuleFor(a => a.ApplicationFormId, f => f.Random.Guid().ToString())
            .RuleFor(a => a.FullName, f => f.Name.FullName())
            .RuleFor(a => a.Email, (f, a) => f.Internet.Email(a.FullName))
            .RuleFor(a => a.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(a => a.Address, f => f.Address.FullAddress())
            .RuleFor(a => a.Answers, f => GenerateAnswers(f))
            .RuleFor(a => a.SubmittedAt, f => f.Date.Recent());
    }

    [Fact]
    public async Task AddApplicationAsync_Should_Add_Application()
    {
        // Arrange
        var newApplicationDto = faker.Generate();

        // Act
        await applicationsService.SubmitApplicationAsync(newApplicationDto, CancellationToken.None);

        // Assert
        applicationsRepoMock.Verify(s => s.SubmitApplicationAsync(It.Is<ApplicationData>(a =>
    a.Answers.Select(dtoAnswer => new Answer
    {
        QuestionId = dtoAnswer.QuestionId,
        AnswerContent = dtoAnswer.AnswerContent
    }).SequenceEqual(newApplicationDto.Answers.Select(dtoAnswer => new Answer
    {
        QuestionId = dtoAnswer.QuestionId,
        AnswerContent = dtoAnswer.AnswerContent
    }))), CancellationToken.None), Times.Once);


    }

    [Fact]
    public async Task GetApplicationByIdAsync_Should_Return_Application()
    {
        // Arrange
        var applicationId = Guid.NewGuid().ToString();
        var application = new ApplicationData { Id = applicationId, Answers = new List<Answer> { new Answer { QuestionId = "Q1", AnswerContent = "Response 1" }, new Answer { QuestionId = "Q2", AnswerContent = "Response 2" } } };

        applicationsRepoMock.Setup(s => s.GetApplicationByIdAsync(applicationId, It.IsAny<CancellationToken>())).ReturnsAsync(application);

        // Act
        var result = await applicationsService.GetApplicationByIdAsync(applicationId, CancellationToken.None);

        // Assert
        Assert.Equal(applicationId, result.Id);
        Assert.Equal(2, result.Answers.Count);
    }



    List<AnswerDto> GenerateAnswers(Faker f)
    {
        var answerFaker = new Faker<AnswerDto>()
            .RuleFor(a => a.QuestionId, f => f.Random.Guid().ToString())
            .RuleFor(a => a.AnswerContent, f => f.Lorem.Sentence());

        return answerFaker.Generate(5); // Generating 5 fake answers
    }
}

