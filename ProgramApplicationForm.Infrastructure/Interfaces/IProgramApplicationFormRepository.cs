using ProgramApplicationForm.Domain.Entities;

namespace ProgramApplicationForm.Infrastructure.Interfaces;

public interface IProgramApplicationFormRepository
{
    Task AddProgramAsync(ApplicationForm program, CancellationToken cancellationToken);
    Task<ApplicationForm> GetProgramAsync(string id, CancellationToken cancellationToken);
}
