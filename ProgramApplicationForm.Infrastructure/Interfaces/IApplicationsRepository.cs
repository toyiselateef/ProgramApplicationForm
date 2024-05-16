using ProgramApplicationForm.Domain.Entities;

namespace ProgramApplicationForm.Infrastructure.Interfaces;

public interface IApplicationsRepository
{
    Task<ApplicationData> GetApplicationByIdAsync(string id, CancellationToken cancellationToken);
    Task<ApplicationData> SubmitApplicationAsync(ApplicationData applicationData, CancellationToken cancellationToken);
}
