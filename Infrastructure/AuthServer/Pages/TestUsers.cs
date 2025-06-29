using Duende.IdentityServer.Test;
using IdentityModel;
using System.Security.Claims;

namespace AuthServer.Pages;

public class TestUsers
{
    public static List<TestUser> Users => new List<TestUser>
    {
        new TestUser
        {
            SubjectId="9ad52125-592d-4887-b8c2-58107bead963",
            Username="aman",
            Password="password",
            Claims  = new List<Claim>
            {
                new Claim(type:JwtClaimTypes.GivenName,value:"aman"),
                new Claim(type:JwtClaimTypes.FamilyName,value:"kumar"),
            }
        },
        new TestUser
        {
            SubjectId = "9ad52125-a2m92-4887-b8c2-58107bead963",
            Username = "alice",
            Password = "Pass123$",
            Claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, "Alice Smith"),
                new Claim(JwtClaimTypes.GivenName, "Alice"),
                new Claim(JwtClaimTypes.FamilyName, "Smith"),
                new Claim(JwtClaimTypes.Email, "alice@example.com"),
                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                new Claim(JwtClaimTypes.WebSite, "https://alice.com"),
                new Claim(JwtClaimTypes.Address, @"{ ""street_address"": ""123 Main St"", ""locality"": ""Wonderland"", ""postal_code"": 12345, ""country"": ""Wonderland"" }", "json"),
                new Claim(JwtClaimTypes.Role, "Admin")
            }
        },
        new TestUser
        {
            SubjectId = "2ad59173-592d-4887-b3c2-86797bead963",
            Username = "bob",
            Password = "Pass123$",
            Claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, "Bob Jones"),
                new Claim(JwtClaimTypes.GivenName, "Bob"),
                new Claim(JwtClaimTypes.FamilyName, "Jones"),
                new Claim(JwtClaimTypes.Email, "bob@example.com"),
                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                new Claim(JwtClaimTypes.WebSite, "https://bob.com"),
                new Claim(JwtClaimTypes.Address, @"{ ""street_address"": ""123 Main St"", ""locality"": ""Wonderland"", ""postal_code"": 12345, ""country"": ""Wonderland"" }", "json"),
                new Claim(JwtClaimTypes.Role, "User")
            }
        }
    };
}
