using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Models;

namespace challenge_c_sharp.Config
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet para Pacientes
        public DbSet<Paciente> Pacientes { get; set; }

        // DbSet para Dentistas
        public DbSet<Dentista> Dentistas { get; set; }

        // DbSet para Consultas
        public DbSet<Consulta> Consultas { get; set; }
        
        //DbSet para Login
        public DbSet<Login> Logins { get; set; }

        //DbSet para Estado 
        public DbSet<Estado> Estados { get; set; }

        //DbSet para Endereco
        public DbSet<Endereco> Enderecos { get; set; }
        //DbSet para Bairro
        public DbSet<Bairro> Bairros { get; set; }
        //DbSet para cidade
        public DbSet<Cidade> Cidades { get; set; }
        //Db Set de unidade
        public DbSet<Unidade> Unidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de tabelas
            modelBuilder.Entity<Paciente>().ToTable("OP_PACIENTE");
            modelBuilder.Entity<Dentista>().ToTable("OP_DENTISTA");
            modelBuilder.Entity<Consulta>().ToTable("OP_CONSULTA");
            modelBuilder.Entity<Login>().ToTable("OP_LOGIN");
            modelBuilder.Entity<Estado>().ToTable("OP_ESTADO");
            modelBuilder.Entity<Endereco>().ToTable("OP_ENDERECO");
            modelBuilder.Entity<Cidade>().ToTable("OP_CIDADE");
            modelBuilder.Entity<Bairro>().ToTable("OP_BAIRRO");
            modelBuilder.Entity<Unidade>().ToTable("OP_UNIDADE");

            // Configuração de relacionamentos

            // Relacionamento Consulta -> Paciente (ID_PACIENTE)
            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Paciente)
                .WithMany() 
                .HasForeignKey(c => c.PacienteId) 
                .HasConstraintName("FK_CONSULTA_PACIENTE"); 

            // Relacionamento Consulta -> Dentista (ID_DENTISTA)
            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Dentista)
                .WithMany() 
                .HasForeignKey(c => c.DentistaId) 
                .HasConstraintName("FK_CONSULTA_DENTISTA"); 
           

        }
    }
}
