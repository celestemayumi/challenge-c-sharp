using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_c_sharp.Models
{
    public class Login
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("SENHA")]
        public string Senha { get; set; }
    }
}
