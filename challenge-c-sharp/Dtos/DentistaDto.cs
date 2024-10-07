namespace challenge_c_sharp.Dtos
{
    public class DentistaDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public string? CRO { get; set; }
        public string? Email { get; set; }
        public long CPF { get; set; }
        public long Telefone { get; set; }
        public int Endereco { get; set; }
        public int Genero { get; set; }
        public string GeneroDescricao { get; set; }
        public char DentistaSuspeito { get; set; }  
        public int Login { get; set; }  
    }
}
