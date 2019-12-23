using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaNetCore
{
    public class DataContext: DbContext
    {
        public DbSet<Autor> Autor { get; set; }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Libro> Libro { get; set; }

        public DataContext()
        {
        }

        //Constructor con parametros para la configuracion
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
