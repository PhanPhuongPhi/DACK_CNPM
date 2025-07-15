using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        // ✅ Chuỗi kết nối đến SQL Server
        private readonly string connectionString = "Server=MR-PHI\\SQLEXPRESS;Database=store;Trusted_Connection=True;TrustServerCertificate=True";

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Tạo id ngẫu nhiên (16 ký tự)
            string userId = Guid.NewGuid().ToString("N").Substring(0, 16);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    INSERT INTO [user] 
                    (id, username, real_name, password, phone_number, date_of_birth, role) 
                    VALUES 
                    (@Id, @Username, @RealName, @Password, @Phone, @Dob, @Role)", conn);

                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.Parameters.AddWithValue("@Username", model.Username);
                cmd.Parameters.AddWithValue("@RealName", model.RealName);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@Phone", model.PhoneNumber ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Dob", model.DateOfBirth);
                cmd.Parameters.AddWithValue("@Role", model.Role);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Login");
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT id, username, real_name, role 
                    FROM [user] 
                    WHERE username = @Username AND password = @Password", conn);

                cmd.Parameters.AddWithValue("@Username", model.Username);
                cmd.Parameters.AddWithValue("@Password", model.Password);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // ✅ Lưu thông tin vào session
                        HttpContext.Session.SetString("UserId", reader["id"].ToString());
                        HttpContext.Session.SetString("Username", reader["username"].ToString());
                        HttpContext.Session.SetString("RealName", reader["real_name"].ToString());
                        HttpContext.Session.SetString("Role", reader["role"].ToString());

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu.");
                        return View(model);
                    }
                }
            }
        }

        // ✅ GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xoá toàn bộ session
            return RedirectToAction("Login");
        }

        // ✅ GET: /Account/GetUserInfo
        public IActionResult GetUserInfo()
        {
            string username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
                return Unauthorized(); // Chưa đăng nhập

            string userId = HttpContext.Session.GetString("UserId");
            string realName = HttpContext.Session.GetString("RealName");
            string role = HttpContext.Session.GetString("Role");

            var userInfo = new
            {
                Id = userId,
                Username = username,
                RealName = realName,
                Role = role
            };

            return Json(userInfo); // trả về JSON thông tin người dùng
        }
    }
}
