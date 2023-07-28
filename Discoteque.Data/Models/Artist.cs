using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discoteque.Data.Models
{
    /// <summary>
    /// El repositorio generico lo tomara como id el baseEntity
    /// </summary>
    public class Artist : BaseEntity<int>
    {
       public string Name { get; set; } = "";
        public string Label { get; set; } = "";
        public bool IsOnTour { get; set; }
    }
}
