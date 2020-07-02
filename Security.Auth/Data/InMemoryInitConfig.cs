// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace OnlineAuction.Security.Auth.Data
{
    public class InMemoryInitConfig
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(
                    name: "Roles",
                    displayName: "Roles Profile",
                    claimTypes: new[] { "role" })
            };

        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
               new ApiResource("onlineauction", "Online Auction")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(List<string> sourceList)
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    // Expire the token after one hour. (This is the default value)
                    ClientId = "angular",
                    ClientName = "angular",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = GetMountURIs(sourceList,"URL", new string[]{
                        "http://URL/callback.html", "http://URL/silent-renew.html"}),
                    RequireConsent = false,
                    PostLogoutRedirectUris = GetMountURIs(sourceList,"URL", new string[]{
                        "http://URL/index.html",}),
                    AllowedCorsOrigins = GetMountURIs(sourceList,"URL", new string[]{
                        "http://URL"}),
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "onlineauction"
                    },
                }
            };
        }
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "romulo",
                    Password = "1234",
                    Claims =
                    {
                        new Claim("name", "Rômulo"),
                        new Claim("family_name", "Rocha Lemes"),
                        new Claim("given_name", "Rômulo"),
                        new Claim("email", "romulolemes01@hotmail.com"),
                    }
                }

            };
        }

        private static List<string> GetMountURIs(List<string> sourceList, string replace, string[] urls)
        {
            List<string> uris = new List<string>();

            foreach (var source in sourceList)
            {
                foreach (var url in urls)
                    uris.Add(url.Replace(replace, source));
            }
            return uris;
        }

    }
}