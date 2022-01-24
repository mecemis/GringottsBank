// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using GringottsBank.IdentityServer.Data;
using GringottsBank.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace GringottsBank.IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.EnsureCreated();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var dobby = userMgr.FindByNameAsync("dobby").Result;
                    if (dobby == null)
                    {
                        dobby = new ApplicationUser
                        {
                            UserName = "dobby",
                            Email = "dobby@gringottsbank.com",
                            EmailConfirmed = true,
                            FullName = "Dobby"
                        };
                        var result = userMgr.CreateAsync(dobby, "Dobby28*").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        string adminRole = "free-elf";

                        if (!roleMgr.RoleExistsAsync(adminRole).Result)
                        {
                            roleMgr.CreateAsync(new IdentityRole() { Name = adminRole }).Wait();
                        }

                        userMgr.AddToRoleAsync(dobby, adminRole).Wait();

                        result = userMgr.AddClaimsAsync(dobby, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Dobby"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("dobby created");
                    }
                    else
                    {
                        Log.Debug("dobby already exists");
                    }

                   
                }
            }
        }
    }
}
