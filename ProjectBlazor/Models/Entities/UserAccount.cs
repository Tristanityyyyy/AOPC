using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBlazor.Models.Entities
{
    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Column("password")]
        [MaxLength(255)]
        public string Password { get; set; }

        [Column("role")]
        [MaxLength(20)]
        public string Role { get; set; }
    }
}
