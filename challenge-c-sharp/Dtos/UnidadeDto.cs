namespace challenge_c_sharp.Dtos
{
    public class UnidadeDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public long Telefone { get; set; }
        public int EnderecoId { get; set; }
        public EnderecoDto? Endereco { get; set; }
    }

}
