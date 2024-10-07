using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace challenge_c_sharp.Models
{
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("LOGRADOURO")]
        public string?Logradouro { get; set; }
        [Column("NUMERO")]
        public int Numero { get; set; }
        [Column("CEP")]
        public long CEP { get; set; }
        [Column("COMPLEMENTO")]
        public string?Complemento { get; set; }

        [ForeignKey("Bairro")]
        [Column("ID_BAIRRO")]
        public int BairroId { get; set; }
        public virtual Bairro Bairro { get; set; }

    }

}
