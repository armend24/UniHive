using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using UniHive.DataAccess;
using UniHive.Models;

namespace UniHive.Controllers
{
    public class LoginController : Controller
    {
        private readonly DbHelper _db;

        public LoginController(DbHelper db)
        {
            _db = db;
        }

        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_LoginUser", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", model.Username);
                cmd.Parameters.AddWithValue("@Password", model.Password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        HttpContext.Session.SetInt32("UserID", (int)reader["UserID"]);
                        HttpContext.Session.SetString("FullName", reader["FullName"].ToString());
                        HttpContext.Session.SetString("Role", reader["Role"].ToString());

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, reader["UserID"].ToString()),
                            new Claim(ClaimTypes.Name, reader["FullName"].ToString()),
                            new Claim(ClaimTypes.Role, reader["Role"].ToString())
                        };

                        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync("MyCookieAuth", principal);

                        string role = reader["Role"].ToString();

                        return RedirectToAction("Index", role);
                    }
                    else
                    { 
                        model.ErrorMessage = "Invalid username or password.";
                        return View(model);
                    }
                }
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
