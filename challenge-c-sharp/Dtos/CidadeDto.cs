namespace challenge_c_sharp.Dtos
{
    public class CidadeDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }  // Apenas a chave estrangeira, se necessário
        public EstadoDto Estado { get; set; }  // Informação do Estado, se necessário
    }

}
