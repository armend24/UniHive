using Microsoft.AspNetCore.Mvc;
using UniHive.DataAccess;
using UniHive.Models;
using Microsoft.Data.SqlClient;

namespace UniHive.Controllers
{
    public class UserController : Controller
    {
        private readonly DbHelper _db;

        public UserController(DbHelper db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<UserViewModel> users = new List<UserViewModel>();

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserViewModel
                        {
                            UserID = (int)reader["UserID"],
                            FullName = reader["FullName"].ToString(),
                            Username = reader["Username"].ToString(),
                            Role = reader["Role"].ToString(),
                            IsActive = reader["IsActive"] != DBNull.Value ? (bool)reader["IsActive"] : false
                        });
                    }
                }
            }
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Users(Username, Password, Role, FullName, IsActive) VALUES (@Username, @Password, @Role, @FullName, @IsActive)", conn);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@IsActive", 1);
            }

            return RedirectToAction("Index");
        }
    }
}
