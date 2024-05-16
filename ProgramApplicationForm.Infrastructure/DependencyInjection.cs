
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramApplicationForm.Infrastructure.DAL;
using ProgramApplicationForm.Infrastructure.Interfaces;
using ProgramApplicationForm.Infrastructure.Repositories;

namespace ProgramApplicationForm.Infrastructure;


public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration Configuration)
        {
        services.AddSingleton<DBContext>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IApplicationsRepository, ApplicationsRepository>();
        services.AddScoped<IProgramApplicationFormRepository, ProgramApplicationFormRepository>();
        return services;
        }

    }

 
