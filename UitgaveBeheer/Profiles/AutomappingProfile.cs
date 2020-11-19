using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UitgaveBeheer.Models;
using UitgaveBeheer.Models.DTO;

namespace UitgaveBeheer.Profiles
{
    public class AutomappingProfile : Profile
    {
        public AutomappingProfile()
        {
            CreateMap<ExpenseDto, ExpenseDetailsViewModel>().ReverseMap();
            CreateMap<OverviewDto, OverviewViewModel>().ReverseMap();
        }
    }
}
