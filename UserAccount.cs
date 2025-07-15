using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("user")] // ánh xạ đến bảng [user] trong SQL Server
    public class UserAccount
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Required]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [Column("real_name")]
        public string RealName { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; } // Cho phép null nếu cần

        [Required]
        [Column("role")]
        public string Role { get; set; }

        [Column("sub_role")]
        public string? SubRole { get; set; }
    }
}
