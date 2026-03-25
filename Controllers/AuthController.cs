using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ThrendyThreads.DataLayer;
using ThrendyThreads.Model;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ThrendyThreads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly SqlServerDB _db;

        public AuthController(IConfiguration config, SqlServerDB db)
        {
            _config = config;
            _db = db;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginRequest)
        {
            try
            {
                string query = "SELECT * FROM Registration_table WHERE Email = @Email";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", loginRequest.Email)
                };

                DataTable dt = _db.GetDataTable(query, CommandType.Text, parameters);

                if (dt.Rows.Count == 0)
                    return Unauthorized(new { message = "User not found" });

                var userRow = dt.Rows[0];

                string dbPassword = userRow["Password"]?.ToString();

                if (dbPassword != loginRequest.Password)
                    return Unauthorized(new { message = "Invalid password" });

                int userId = Convert.ToInt32(userRow["UserId"]);
                string username = userRow["UserName"]?.ToString();
                string email = userRow["Email"]?.ToString();
                string role = userRow["Role"]?.ToString() ?? "User";

                string imageBase64 = null;

                if (userRow["Image"] != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])userRow["Image"];
                    imageBase64 = Convert.ToBase64String(imageBytes);
                }

                var token = GenerateJwtToken(email, role);

                return Ok(new
                {
                    token,
                    user = new
                    {
                        userId,
                        username,
                        email,
                        role,
                        image = imageBase64,
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred",
                    error = ex.Message
                });
            }
        }

        private string GenerateJwtToken(string email, string role)
        {
            var key = _config["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(key) || Encoding.UTF8.GetBytes(key).Length < 32)
                throw new SecurityTokenException("JWT key must be at least 32 characters.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}