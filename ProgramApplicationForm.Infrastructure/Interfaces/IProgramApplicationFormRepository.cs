using ProgramApplicationForm.Domain.Entities;

namespace ProgramApplicationForm.Infrastructure.Interfaces;

public interface IProgramApplicationFormRepository
{
    Task AddProgramAsync(ApplicationForm program);
    Task<ApplicationForm> GetProgramAsync(string id);
}
