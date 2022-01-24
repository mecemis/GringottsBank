﻿using System.Reflection;
using BankAccount.Application.Common.Behaviors;
using BankAccount.Application.Common.Settings;
using BankAccount.Application.EventStores;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccount.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddSingleton<BankAccountStream>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

           

            return services;
        }
    }
}