﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UitgaveBeheer.Domain;

namespace UitgaveBeheer.Models.DTO
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }

        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public double Bedrag { get; set; }
        public string Categorie { get; set; }
    }
}
