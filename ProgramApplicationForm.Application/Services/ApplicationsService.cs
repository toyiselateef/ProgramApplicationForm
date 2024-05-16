
using AutoMapper;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Application.Interfaces;
using ProgramApplicationForm.Domain.Entities;
using ProgramApplicationForm.Infrastructure.Interfaces;
using ProgramApplicationForm.Infrastructure.Repositories;

namespace ApplicationForm.Application.Services;

public class ApplicationsService : IApplicationsService
{
    private readonly IApplicationsRepository applicationsRepository;
    private readonly IMapper mapper;

    public ApplicationsService(IApplicationsRepository applicationsRepository, IMapper mapper)
    {
        this.applicationsRepository = applicationsRepository;
        this.mapper = mapper;
    }
    public async Task<ApplicationDataDto> GetApplicationByIdAsync(string id, CancellationToken cancellationToken)
    {
        var response = await applicationsRepository.GetApplicationByIdAsync(id, cancellationToken);

        return mapper.Map<ApplicationDataDto>(response);
    }

    public async Task<ApplicationDataDto> SubmitApplicationAsync(ApplicationDataDto applicationDataDto, CancellationToken cancellationToken)
    {
        ApplicationData AppFormData = mapper.Map<ApplicationData>(applicationDataDto);
        ApplicationData response = await applicationsRepository.SubmitApplicationAsync(AppFormData, cancellationToken);

        return mapper.Map<ApplicationDataDto>(response);
    }
}
