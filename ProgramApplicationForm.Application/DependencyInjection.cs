
using ApplicationForm.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ProgramApplicationForm.Api.Validators;
using ProgramApplicationForm.Application.Dtos;
using ProgramApplicationForm.Application.Interfaces;
using System.Reflection;

namespace ProgramApplicationForm.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services/*, IConfiguration Configuration*/)
    {

        services.AddScoped<IApplicationsService, ApplicationsService>();
        services.AddScoped<IQuestionService, QuestionService>();


        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }

}