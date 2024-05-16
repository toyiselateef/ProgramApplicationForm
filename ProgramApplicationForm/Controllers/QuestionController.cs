 
using Microsoft.AspNetCore.Mvc;
using ProgramApplicationForm.Api.Models;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Application.Interfaces;

namespace ProgramApplicationForm.Controllers;

public class QuestionController : BaseController
{
    private readonly IQuestionService questionService;

    public QuestionController(IQuestionService questionService)
    { 
        this.questionService = questionService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto questionDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        QuestionDto createdQuestion = await questionService.CreateQuestionAsync(questionDto, cancellationToken);
        return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestion.Id }, ApiResponse<QuestionDto>.SuccessResponse(createdQuestion));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuestion(string id, [FromBody] QuestionDto questionDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        await questionService.UpdateQuestionAsync(id, questionDto, cancellationToken);
        return NoContent();
    }

    [HttpGet("get-Program-questions")]
    public async Task<IActionResult> GetQuestions(string programId, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        IAsyncEnumerable<QuestionDto> questions = questionService.GetQuestionsAsync(programId, cancellationToken);
        return Ok(ApiResponse<IAsyncEnumerable<QuestionDto>>.SuccessResponse(questions));

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestionById(string id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        var question = await questionService.GetQuestionByIdAsync(id, cancellationToken);
        return question == null
            ? NotFound(ApiResponse<QuestionDto>.ErrorResponse($"No such Question with id:{id} found"))
            : Ok(ApiResponse<QuestionDto>.SuccessResponse(question));
    }
}
