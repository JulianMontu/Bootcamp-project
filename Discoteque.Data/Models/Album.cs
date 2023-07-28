using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discoteque.Data.Models
{
    public class Album : BaseEntity<int>
    {
        public string Name { get; set; } = "";
        public int Year { get; set; }
        public Genre Genre { get; set; } = Genre.Unknown;
    }

    public enum Genre
    {
        Rock,
        Salsa,
        Metal,
        Urban,
        Folk,
        Indie,
        Techno,
        Unknown
    }
}
