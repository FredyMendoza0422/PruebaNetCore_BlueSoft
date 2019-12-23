using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PruebaNetCore
{
    public class Libro
    {
        [Key]
        public int IdLibro { get; set; }
        [Required]
        public string Nombre { get; set; }
        [ForeignKey("IdAutor")]
        public Autor Autor { get; set; }
        public int IdAutor { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }

        public int IdCategoria { get; set; }

        [Required]
        public string ISBN { get; set; }
    }
}
