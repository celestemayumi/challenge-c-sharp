using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_c_sharp.Models
{
    public class Paciente
    {
        [Key]
        [Column("ID")]
        public int Id {  get; set; }
        [Column("NOME_PACIENTE")]
        public string? Nome { get; set; }
        [Column("DATA_DE_NASCIMENTO")]
        public DateTime Nascimento { get; set; }
        [Column("EMAIL_PACIENTE")]
        public string? Email { get; set; }
        [Column("CPF_PACIENTE")]
        public long CPF { get; set; }
        [Column("TELEFONE_PACIENTE")]
        public long Telefone { get; set; }
        [Column("CLIENTE_SUSPEITO")]
        public char ClienteSuspeito { get; set; }
        [Column("ID_GENERO")]
        public int Genero { get; set; }
        [Column("ID_ENDERECO")]
        public int Endereco { get; set; }
        [Column("ID_LOGIN")]
        public int Login { get; set; }
        // Apenas exibe no swagger o genero do paciente 
        [NotMapped]
        public string GeneroDescricao
        {
            get
            {
                return Genero == 1 ? "Mulher" : Genero == 2 ? "Homem" : "Não Definido";
            }
        }
    }
}
