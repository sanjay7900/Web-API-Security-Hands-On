using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiSecuritytokenProvider.ModelForAuth;

namespace WebApiSecuritytokenProvider.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserApiAuthnticationController : ControllerBase
    {
        private readonly ApplicationDbContextDemo _context;
        private readonly IConfiguration _configuration;


        public UserApiAuthnticationController(IConfiguration configuration, ApplicationDbContextDemo context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult GenerateTokenForFirstProjWebApiOne(UserApiAuthntication user)
        {
            //_context.Users.Add(user);
            //_context.SaveChanges(); 
            string? tokenAuth;
            var IsValidUser = _context.User!.Where(u => u.Name == user.Name && u.Password == user.Password && u.Email == user.Email).FirstOrDefault();
            if (IsValidUser != null)
            {
                var authClaims = new List<Claim>
                {

                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Name, user.Name!),
                    new Claim("Password",user.Password!),
                    new Claim(ClaimTypes.DateOfBirth,user.Age.ToString()),
                    new Claim("Qualification","B.tech"), 
                    new Claim(ClaimTypes.Role,user.RoleType.ToString()),
                    new Claim(ClaimTypes.Role,"Viewer"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                tokenAuth = CreateToken(authClaims, _configuration["JWT:AudienceWebAPI1"], _configuration["JWT:Secret1"]);



            }
            else
            {
                return BadRequest();
            }

            return Ok(tokenAuth);

        }
        [HttpPost]
        public ActionResult GenerateTokenForSecondProjWebApiTwo(UserApiAuthntication user)
        {
            //_context.Users.Add(user);
            //_context.SaveChanges(); 
            string? tokenAuth;
            var IsValidUser = _context.User!.Where(u => u.Name == user.Name && u.Password == user.Password && u.Email == user.Email).FirstOrDefault();
            if (IsValidUser != null)
            {
                var authClaims = new List<Claim>
                {

                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Name, user.Name!),
                    new Claim("Password",user.Password!),
                    new Claim(ClaimTypes.DateOfBirth,user.Age.ToString()),
                    new Claim(ClaimTypes.Role,user.RoleType.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                tokenAuth = CreateToken(authClaims, _configuration["JWT:AudienceWebAPI2"], _configuration["JWT:Secret2"]);



            }
            else
            {
                return BadRequest();
            }

            return Ok(tokenAuth);

        }
        private string CreateToken(List<Claim> authClaims, string audience, string secretKey)
        {


            var authSigninKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: audience,
                expires: DateTime.Now.AddMinutes(5),
                claims: authClaims,
                signingCredentials:new SigningCredentials(authSigninKey,SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
            //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            //var token = new JwtSecurityToken(
            //    issuer: _configuration["JWT:ValidIssuer"],
            //    audience: audience,
            //    expires: DateTime.Now.AddMinutes(5),
            //    claims: authClaims,
            //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //    );

            //return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}