namespace challenge_c_sharp.Dtos
{
    public class ConsultaDto
    {
        public int Id { get; set; }          
        public DateTime DataConsulta { get; set; }   
        public int PacienteId { get; set; }       
        public string Paciente { get; set; }       
        public int DentistaId { get; set; }       
        public string Dentista { get; set; }        
        public int Unidade { get; set; }           
    }
}