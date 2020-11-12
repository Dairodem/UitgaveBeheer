using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UitgaveBeheer.Models
{
    public class ExpenseUpdateViewModel
    {
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }
        public double Bedrag { get; set; }
        public string Categorie { get; set; }
    }
}
