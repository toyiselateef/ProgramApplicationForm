
using AutoMapper;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Application.Interfaces;
using ProgramApplicationForm.Infrastructure.Interfaces;

namespace ApplicationForm.Application.Services;

public class ApplicationsService : IApplicationsService
{
    public ApplicationsService(IApplicationsRepository applicationsRepository, IMapper mapper)
    {
            
    }
    public Task<ApplicationDataDto> GetApplicationByIdAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationDataDto> SubmitApplicationAsync(ApplicationDataDto applicationDataDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
