using Microsoft.AspNetCore.Mvc;
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

            // Chuyển sang trang đăng nhập sau khi đăng ký thành công
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
                    SELECT COUNT(*) 
                    FROM [user] 
                    WHERE username = @Username AND password = @Password", conn);

                cmd.Parameters.AddWithValue("@Username", model.Username);
                cmd.Parameters.AddWithValue("@Password", model.Password);

                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    // Đăng nhập thành công → chuyển hướng về trang chủ
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
}
