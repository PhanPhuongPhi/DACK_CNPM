using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CustomerController : Controller
    {
        // 🔐 Mô phỏng khách hàng đăng nhập
        private UserAccount CurrentUser => new UserAccount
        {
            Username = "nguyenvana",
            Role = "customer"
        };

        private bool IsCustomer() => CurrentUser.Role == "customer";

        public IActionResult SearchProduct()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("🔍 Đang tìm kiếm sản phẩm.");
        }

        public IActionResult AddToCart()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("🛒 Sản phẩm đã được thêm vào giỏ.");
        }

        public IActionResult Purchase()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("💳 Mua sản phẩm thành công.");
        }

        public IActionResult ChoosePayment()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("🏦 Đã chọn phương thức thanh toán.");
        }

        public IActionResult ViewDeliveryStatus()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("🚚 Đang xem tình trạng giao hàng.");
        }

        public IActionResult EditAccount()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("✏️ Đã cập nhật thông tin tài khoản.");
        }

        public IActionResult DeleteAccount()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("❌ Tài khoản đã bị xoá.");
        }

        public IActionResult AddComment()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("💬 Đã bình luận sản phẩm.");
        }

        public IActionResult AddRating()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("⭐ Đã đánh giá sản phẩm.");
        }

        public IActionResult ReportComment()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("🚨 Đã báo cáo bình luận vi phạm.");
        }

        public IActionResult CreateSupportTicket()
        {
            if (!IsCustomer())
                return Forbid();
            return Content("📨 Đã gửi phiếu hỗ trợ khách hàng.");
        }
    }
}
