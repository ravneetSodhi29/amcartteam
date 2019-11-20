// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer4.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{SubjectId = "818727", Username = "lalit.aggarwal@nagarro.com", Password = "admin123", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Lalit Aggarwal"),
                    new Claim(JwtClaimTypes.GivenName, "Lalit"),
                    new Claim(JwtClaimTypes.FamilyName, "Aggarwal"),
                    new Claim(JwtClaimTypes.Email, "lalit.aggarwal@nagarro.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://lalit.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Nagarro Plot 13', 'locality': 'Gurugram', 'postal_code': 122015, 'country': 'India' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                }
            },
            new TestUser{SubjectId = "88421113", Username = "sumit.maan@nagarro.com", Password = "admin123", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Sumit Maan"),
                    new Claim(JwtClaimTypes.GivenName, "Sumit"),
                    new Claim(JwtClaimTypes.FamilyName, "Maan"),
                    new Claim(JwtClaimTypes.Email, "sumit.maan@nagarro.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://sumit.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Nagarro Plot 13', 'locality': 'Gurugram', 'postal_code': 122015, 'country': 'India' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                    new Claim("location", "somewhere")
                }
            }
        };
    }
}