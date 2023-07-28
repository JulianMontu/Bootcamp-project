using Discoteque.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discoteque.Data
{
    public class DicotequeContext : DbContext
    {
        //Donde ira a buscar la data, standar procedures
        public DicotequeContext(DbContextOptions<DicotequeContext> options) : base(options) 
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }

    }
}
