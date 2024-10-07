using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_c_sharp.Models
{
    public class Consulta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("DATA_CONSULTA")]
        public DateTime DataConsulta { get; set; }
        [Column("ID_PACIENTE")]
        [ForeignKey("FK_CONSULTA_PACIENTE")]
        public int PacienteId { get; set; } // Chave estrangeira
        public virtual Paciente Paciente { get; set; } // Navegação
        [Column("ID_DENTISTA")]
        [ForeignKey("FK_CONSULTA_DENTISTA")]
        public int DentistaId { get; set; } // Chave estrangeira
        public virtual Dentista Dentista { get; set; } // Navegação
        [Column("ID_UNIDADE")]
        public int Unidade { get; set; }
    }
}
