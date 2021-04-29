using Softplan.CustomCountries.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Softplan.CustomCountries.API.Tests.IntegrationTests.Builders
{
    public class CountryTestBuilder : BaseBuilder<CountryTestBuilder, Country>
    {
        public CountryTestBuilder()
        {
            Model = new Country();
        }

        public Country Default()
        {
            Model.Id = "51";
            Model.Area = 282248;
            Model.Capital = "Tiranas";
            Model.Name = "Albania";
            Model.NativeName = "Shqipëria";
            Model.Population = 2886026;
            Model.PopulationDensity = 100.39049673020732M;
            Model.Flag = new Flag()
            {
                Id = 70,
                Emoji = "🇦🇱",
                EmojiUnicode = "U+1F1E6 U+1F1F1",
                SvgFile = new Uri("https://restcountries.eu/data/alb.svg")
            };
            Model.TopLevelDomains = new List<TopLevelDomain>()
            {
                new TopLevelDomain()
                {
                    Id = 52,
                    Name = ".al"
                }
            };

            return this;
        }


        public Country Invalido()
        {
            Model.Area = 282248;
            Model.Capital = "Tiranas";
            Model.Name = "Albania";
            Model.NativeName = "Shqipëria";
            Model.Flag = new Flag()
            {
                Id = 70,
                Emoji = "🇦🇱",
                EmojiUnicode = "U+1F1E6 U+1F1F1",
                SvgFile = new Uri("https://restcountries.eu/data/alb.svg")
            };
            Model.TopLevelDomains = new List<TopLevelDomain>()
            {
                new TopLevelDomain()
                {
                    Id = 52,
                    Name = ".al"
                }
            };

            return this;
        }
    }
}
