using ClientesApp.Domain.Entities;
using ClientesApp.Domain.Interfaces.Services;
using ClientesApp.Domain.Services;
using ClientesApp.Domain.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Domain.Extensions
{
    public static class ClienteDomainServiceExtension
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            //Mapeamento das injeções de dependência
            services.AddTransient<IClienteDomainService, ClienteDomainService>();
            services.AddTransient<IValidator<Cliente>, ClienteValidator>();

            return services;
        }

    }
}
