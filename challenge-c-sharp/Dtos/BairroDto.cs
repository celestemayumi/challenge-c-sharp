namespace challenge_c_sharp.Dtos
{
    public class BairroDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CidadeId { get; set; }  // Apenas a chave estrangeira, se necessário
        public CidadeDto Cidade { get; set; }  // Informação da Cidade, se necessário
    }
}
