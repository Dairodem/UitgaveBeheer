﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UitgaveBeheer.Models
{
    public class ExpenseListViewModel
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public double Bedrag { get; set; }
    }
}
