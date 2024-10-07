using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_c_sharp.Models
{
    public class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NOME_CIDADE")]
        public string Nome { get; set; }
        [Column("ID_ESTADO")]
        [ForeignKey("FK_ESTADO")]
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
