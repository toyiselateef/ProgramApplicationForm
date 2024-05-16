using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProgramApplicationForm.Application.Dtos;

namespace ProgramApplicationForm.Controllers;

public class ApplicationController : BaseController
{
    private readonly IValidator validator;

    public ApplicationController()
    {
        this.validator = validator;
    }


    [HttpPost("submit")]
    public async Task<IActionResult> SubmitApplication([FromBody] ApplicationDataDto createApplicationDataDto, CancellationToken cancellationToken)
    {
        
         
       
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationById(string id, CancellationToken cancellationToken)
    {
        return NotFound();
    }
}
