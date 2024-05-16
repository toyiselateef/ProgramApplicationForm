using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProgramApplicationForm.Api.Models;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Application.Interfaces;

namespace ProgramApplicationForm.Controllers;

public class ApplicationController : BaseController
{
    private readonly IApplicationsService applicationsService;

    public ApplicationController(IApplicationsService applicationsService)
    {
        this.applicationsService = applicationsService;
    }


    [HttpPost("submit")]
    public async Task<IActionResult> SubmitApplication([FromBody] ApplicationDataDto createApplicationDataDto, CancellationToken cancellationToken)
    {
        var submittedApplication = await applicationsService.SubmitApplicationAsync(createApplicationDataDto, cancellationToken);
        return CreatedAtAction(nameof(GetApplicationById), new { id = submittedApplication.Id }, ApiResponse<ApplicationDataDto>.SuccessResponse(createApplicationDataDto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationById(string id, CancellationToken cancellationToken)
    {
        var application = await applicationsService.GetApplicationByIdAsync(id, cancellationToken);

        return (application == null)
        ? NotFound(ApiResponse<ApplicationDataDto>.ErrorResponse("Application not found"))
        : Ok(ApiResponse<ApplicationDataDto>.SuccessResponse(application));
    }
}
