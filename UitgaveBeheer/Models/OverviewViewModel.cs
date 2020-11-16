using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UitgaveBeheer.Models
{
    public class OverviewViewModel
    {

        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public string OmschrijvingHoogst { get; set; }
        public double BedragHoogst { get; set; }
        public string OmschrijvingLaagst { get; set; }
        public double BedragLaagst { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatumDag { get; set; }
        public double BedragDag { get; set; }
        public double BedragCatHoog { get; set; }
        public double BedragCatLaag { get; set; }
        public string CatHoog { get; set; }
        public string CatLaag { get; set; }
    }
}
