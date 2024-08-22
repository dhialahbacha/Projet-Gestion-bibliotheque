using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Emprunteur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int AnneeNaissance { get; set; }

       
        public List<Livre> ListeLivre { get; set; } 
    }
}
