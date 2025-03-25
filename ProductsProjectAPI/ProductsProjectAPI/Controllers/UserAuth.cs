using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySql.Data.MySqlClient;
using ProductsProjectAPI.Data;

namespace ProductsProjectAPI.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class UserAuth : ControllerBase
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("Login")]
        public IActionResult Login(string username, string password) 
        {
            using (MySqlConnection connection = Global.getInstance())
            {
                connection.Open();

                var details = connection.Query<User>($"SELECT * FROM User WHERE email = @username AND password = @password", new { username = username, password = password });

                connection.Close();

                if (details.Count() == 0)
                {
                    return NotFound();
                }
                else if (details.Count() == 1 && details != null)
                {
                    this.SignIn(new System.Security.Claims.ClaimsPrincipal 
                    {
                        
                    });
                    return Ok(details.First());
                }
                else {
                    return BadRequest();
                }
            }
        }

        [HttpPost("Register")]
        public IActionResult Register(string email, string password)
        {
            using (MySqlConnection connection = Global.getInstance())
            {
                connection.Open();

                var execute = connection.Execute($"INSERT INTO User(email,password) VALUES('{email}','{password}')", new { email, password });

                connection.Close();

                switch (execute)
                {
                    case 1: //Sign the user in once the User has been created
                        return this.Login(email, password);

                    default: //in-case the User has failed to get created or network issues;
                        return BadRequest();
                }
            }
        }
    }
}
