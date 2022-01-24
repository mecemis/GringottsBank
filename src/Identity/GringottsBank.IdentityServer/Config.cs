// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace GringottsBank.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_bankaccount") {Scopes = {"bankaccount_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {

                new ApiScope("bankaccount_fullpermission", "Full Access for BankAccount API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "bankaccount_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                },

                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        "bankaccount_fullpermission",
                        IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName
                    },
                    AccessTokenLifetime = 5 * 60 * 60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int) (DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                },

                new Client
                {
                    ClientName = "Token Exchange Client",
                    ClientId = "TokenExchangeClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = new[] {"urn:ietf:params:oauth:grant-type:token-exchange"},
                    AllowedScopes =
                    {
                        "bankaccount_fullpermission", "payment_fullpermission",
                        IdentityServerConstants.StandardScopes.OpenId
                    }
                },
                
            };
    }

}