using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Softplan.CustomCountries.Domain.Entities;
using Softplan.CustomCountries.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.API.Configuration
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Country, CountryViewModel>();
            CreateMap<Flag, FlagViewModel>();
            CreateMap<TopLevelDomain, TopLevelDomainViewModel>();
        }
    }

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CountryViewModel, Country>();
            CreateMap<FlagViewModel, Flag>();
            CreateMap<TopLevelDomainViewModel, TopLevelDomain>();
        }
    }
}
