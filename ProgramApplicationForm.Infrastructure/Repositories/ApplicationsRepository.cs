
using ProgramApplicationForm.Domain.Entities;
using ProgramApplicationForm.Infrastructure.Interfaces;

namespace ProgramApplicationForm.Infrastructure.Repositories;

public class ApplicationsRepository : IApplicationsRepository
{
    public Task<ApplicationData> GetApplicationByIdAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationData> SubmitApplicationAsync(ApplicationData applicationData, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

