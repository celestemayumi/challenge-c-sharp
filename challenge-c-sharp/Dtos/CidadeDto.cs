namespace challenge_c_sharp.Dtos
{
    public class CidadeDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }  
        public EstadoDto Estado { get; set; } 
    }

}
