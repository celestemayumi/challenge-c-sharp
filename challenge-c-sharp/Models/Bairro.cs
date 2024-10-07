using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_c_sharp.Models
{
    public class Bairro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NOME_BAIRRO")]
        public string Nome { get; set; }
        [Column("ID_CIDADE")]
        [ForeignKey("FK_CIDADE")]
        public int CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}
