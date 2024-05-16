
using Microsoft.Azure.Cosmos;
using ProgramApplicationForm.Domain.Entities;
using ProgramApplicationForm.Infrastructure.Common;
using ProgramApplicationForm.Infrastructure.DAL;
using ProgramApplicationForm.Infrastructure.Interfaces;

namespace ProgramApplicationForm.Infrastructure.Repositories;

public class ApplicationsRepository : IApplicationsRepository
{ 
    private readonly Container container;

    public ApplicationsRepository(DBContext context)
    { 
        container = context.GetContainer(DataConstants.appsContainerId);
    }

    public async Task<ApplicationData> GetApplicationByIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await container.ReadItemAsync<ApplicationData>(id, new PartitionKey(id), cancellationToken: cancellationToken);
            return response.Resource;
        }
        catch (CosmosException ce) when (ce.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return default(ApplicationData);
        }
    }

    public async Task<ApplicationData> SubmitApplicationAsync(ApplicationData applicationData, CancellationToken cancellationToken)
    {
        var response = await container.CreateItemAsync(applicationData, new PartitionKey(applicationData.Id), cancellationToken: cancellationToken);
        return response.Resource;
    }
}

 