using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        // 🔐 Mô phỏng admin đăng nhập
        private UserAccount CurrentUser => new UserAccount
        {
            Username = "admin.magnus",
            Role = "admin"
        };

        private bool IsAdmin() => CurrentUser.Role == "admin";

        public IActionResult RespondToEmployeeTicket()
        {
            if (!IsAdmin())
                return Forbid();

            return Content("✅ Admin: Đã phản hồi phiếu yêu cầu từ nhân viên.");
        }
    }
}
