
using ProgramApplicationForm.Domain.Entities;
using ProgramApplicationForm.Infrastructure.Interfaces;

namespace ProgramApplicationForm.Infrastructure.Repositories;

public class ProgramApplicationFormRepository : IProgramApplicationFormRepository
{
    public Task AddProgramAsync(ApplicationForm program)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationForm> GetProgramAsync(string id)
    {
        throw new NotImplementedException();
    }
}
