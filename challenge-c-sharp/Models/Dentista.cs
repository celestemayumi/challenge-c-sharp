using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_c_sharp.Models
{
    public class Dentista
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NOME_DENTISTA")]
        public string? Nome { get; set; }
        [Column("DATA_DE_NASCIMENTO")]
        public DateTime Nascimento { get; set; }
        [Column("CRO")]
        public string? CRO { get; set; }
        [Column("EMAIL_DENTISTA")]
        public string? Email { get; set; }
        [Column("CPF_DENTISTA")]
        public long CPF { get; set; }
        [Column("TELEFONE_DENTISTA")]
        public long Telefone { get; set; }
        [Column("DENTISTA_SUSPEITO")]
        public char DentistaSuspeito { get; set; }
        [Column("ID_GENERO")]
        public int Genero { get; set; }
        [Column("ID_ENDERECO")]
        public int Endereco { get; set; }
        [Column("ID_LOGIN")]
        public int Login {  get; set; }
        // Apenas exibe no swagger o genero do dentista indicado
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
