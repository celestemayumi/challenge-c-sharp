﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_c_sharp.Models
{
    public class Unidade
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NOME_UNIDADE")]
        public string? Nome { get; set; }
        [Column("TELEFONE")]
        public long Telefone { get; set; }
        [Column("ID_ENDERECO")]
        public int Endereco { get; set; }
    }
}
