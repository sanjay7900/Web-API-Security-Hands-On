using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApiOneSecurityExample.Policy
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext contxt, MinimumAgeRequirement requirement)
        {
            if (!contxt.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.FromResult(0);
            }

            var dateOfBirth = Convert.ToInt32(contxt.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);
            var qualification = contxt.User.FindFirst(c => c.Type == "Qualification").Value;

            //Console.WriteLine(dateOfBirth + " " + qualification);
            
            

            if (dateOfBirth >= requirement.MinimumAge && requirement.Qualification == qualification)
            {
                contxt.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }

    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int age, string qualification)
        {
            MinimumAge = age;
            Qualification = qualification;
        }

        public int MinimumAge { get; set; }

        public string Qualification { get; set; }

    }
}

