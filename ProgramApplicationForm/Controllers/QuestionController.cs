 
using Microsoft.AspNetCore.Mvc;
using ProgramApplicationForm.Application.Dtos;

namespace ProgramApplicationForm.Controllers;

public class QuestionController : BaseController
{

    public QuestionController()
    {

    }

    [HttpPost]
    public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto questionDto, CancellationToken cancellationToken)
    {
        if(!ModelState.IsValid) {
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuestion(string id, [FromBody] CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestions(string programId, CancellationToken cancellationToken)
    {

        return Ok();

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestionById(string id, CancellationToken cancellationToken)
    {

        return NotFound();
    }
}
