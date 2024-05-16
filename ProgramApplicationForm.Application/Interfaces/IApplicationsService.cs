using ProgramApplicationForm.Application.Dtos;

namespace ProgramApplicationForm.Application.Interfaces;

public interface IApplicationsService
{
    Task<ApplicationDataDto> GetApplicationByIdAsync(string id, CancellationToken cancellationToken);
    Task<ApplicationDataDto> SubmitApplicationAsync(ApplicationDataDto applicationDataDto, CancellationToken cancellationToken);
}
