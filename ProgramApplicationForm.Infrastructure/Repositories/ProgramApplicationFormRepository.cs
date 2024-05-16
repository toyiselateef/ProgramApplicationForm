
using Microsoft.Azure.Cosmos;
using ProgramApplicationForm.Domain.Entities;
using ProgramApplicationForm.Infrastructure.Common;
using ProgramApplicationForm.Infrastructure.DAL;
using ProgramApplicationForm.Infrastructure.Interfaces; 

namespace ProgramApplicationForm.Infrastructure.Repositories;

public class ProgramApplicationFormRepository : IProgramApplicationFormRepository
{
    private readonly Container applicationFormContainer;

    public ProgramApplicationFormRepository(DBContext context)
    {
        applicationFormContainer = context.GetContainer(DataConstants.pafContainerId);
    }
    public async Task AddProgramAsync(ApplicationForm program, CancellationToken cancellationToken)
    {
        await applicationFormContainer.CreateItemAsync(program, new PartitionKey(program.Id), cancellationToken: cancellationToken);

    }

    public async Task<ApplicationForm> GetProgramAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            ItemResponse<ApplicationForm> response = await applicationFormContainer.ReadItemAsync<ApplicationForm>(id, new PartitionKey(id), cancellationToken: cancellationToken);
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}
