using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_c_sharp.Models
{
    public class Estado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NOME_ESTADO")]
        public string Nome { get; set; }
        [Column("SIGLA_ESTADO")]
        public string Sigla {  get; set; }
        public virtual ICollection<Cidade> Cidades { get; set; }
    }
}
