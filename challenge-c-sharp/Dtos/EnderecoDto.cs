namespace challenge_c_sharp.Dtos
{
    public class EnderecoDto
    {
        public int Id { get; set; }
        public string? Logradouro { get; set; }
        public int Numero { get; set; }
        public long CEP { get; set; }
        public string? Complemento { get; set; }
        public int BairroId { get; set; } 
        public BairroDto? Bairro { get; set; } 
    }
}