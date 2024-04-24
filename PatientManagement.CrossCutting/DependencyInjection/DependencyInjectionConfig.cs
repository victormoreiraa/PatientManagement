using Microsoft.Extensions.DependencyInjection;
using PatientManagement.Application.Interfaces;
using PatientManagement.Application.Services;
using PatientManagement.Domain.Interfaces;
using PatientManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.CrossCutting.DependencyInjection
{
    public class DependencyInjectionConfig
    {
        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();         
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPatientService, PatientService>();
        }
    }
}
