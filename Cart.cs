using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models
{
    public class Cart
    {
        public string Title { get; set; } = "Giỏ hàng của bạn";
        public string Message { get; set; } = "Hiện chưa có sản phẩm nào trong giỏ hàng.";
    }
}
