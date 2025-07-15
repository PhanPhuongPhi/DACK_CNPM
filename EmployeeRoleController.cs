using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmployeeRoleController : Controller
    {
        // 🔐 Mô phỏng user đang đăng nhập (có thể thay bằng session sau)
        private UserAccount CurrentUser => new UserAccount
        {
            Username = "john.employee",
            Role = "employee",
            SubRole = "HR"
        };

        private bool HasAccess(string requiredSubRole)
        {
            return CurrentUser.Role == "employee" && CurrentUser.SubRole == requiredSubRole;
        }

        // 🧑‍💼 HR: Thêm nhân viên
        public IActionResult AddEmployee()
        {
            if (!HasAccess("HR"))
                return Forbid();

            return Content("Thêm nhân viên: OK");
        }

        // 🏬 Marketing: Tạo khuyến mãi
        public IActionResult CreatePromotion()
        {
            if (!HasAccess("Marketing"))
                return Forbid();

            return Content("Tạo khuyến mãi: OK");
        }

        // 📦 Warehouse: Nhập kho
        public IActionResult UpdateStock()
        {
            if (!HasAccess("Warehouse"))
                return Forbid();

            return Content("Cập nhật kho: OK");
        }

        // 📞 Support: Phản hồi hỗ trợ
        public IActionResult RespondCustomerTicket()
        {
            if (!HasAccess("Support"))
                return Forbid();

            return Content("Phản hồi khách hàng: OK");
        }

        // 📦 Delivery: Báo cáo giao hàng
        public IActionResult DeliveryReport()
        {
            if (!HasAccess("Delivery"))
                return Forbid();

            return Content("Báo cáo giao hàng: OK");
        }

        // 🧾 Business: Lấy báo cáo
        public IActionResult GetReport()
        {
            if (!HasAccess("Business"))
                return Forbid();

            return Content("Lấy báo cáo: OK");
        }
    }
}
