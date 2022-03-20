using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            //looking for objects inherit from Profile
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // looking for objects inherit from AbstractValidator
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //looking for objects inherit from IRequestHandler and IRequest
            services.AddMediatR(Assembly.GetExecutingAssembly());


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,> ), typeof(UnhandledExceptionBehaviour<,>));
            return services;
        } 
    }
}
