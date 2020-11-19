using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UitgaveBeheer.Domain
{
    public class Expense
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }
        public string Categorie{ get;  set; }
        public double Bedrag { get; set; }
        public string Foto { get; set; }

        /*--------  Adding a Foreign Key  --------------
        [ForeignKey(nameof(Categorie))]
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
        */
    }
}
