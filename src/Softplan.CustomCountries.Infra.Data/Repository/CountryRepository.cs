using GraphQL;
using GraphQL.Client.Abstractions;
using JsonFlatFileDataStore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Softplan.CustomCountries.Domain.Entities;
using Softplan.CustomCountries.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Softplan.CustomCountries.Infra.Data.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IGraphQLClient _graphQLClient;
        private readonly IDataStore _dataStore;

        public CountryRepository(IGraphQLClient graphQLClient, IDataStore dataStore)
        {
            _graphQLClient = graphQLClient;
            _dataStore = dataStore;
        }

        public async Task<bool> AddCountry(Country country)
        {
            try
            {
                var collection = _dataStore.GetCollection<Country>();
                return await collection.InsertOneAsync(country);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateCountry(Country country)
        {
            try
            {
                var collection = _dataStore.GetCollection<Country>();
                var existsCountry = collection.AsQueryable().FirstOrDefault(c => c.Id == country.Id);
                if (existsCountry != null)
                {
                    var countryUpdated = new { area = country.Area, population = country.Population, populationDensity = country.PopulationDensity, capital = country.Capital };
                    return await collection.UpdateOneAsync(c => c.Id == country.Id, countryUpdated);
                }
                else
                    return await collection.InsertOneAsync(country);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Country> GetByIdAsync(string id)
        {
            var query = new GraphQLRequest
            {
                Query = $@"query GetCountries{{
                    Country{$@"(_id:""{id}"")"}{{
                        _id
                        area
                        capital
                        name
                        nativeName
                        population
                        populationDensity
                        flag{{
                            _id
                            emoji
                            emojiUnicode
                            svgFile
                        }}
                        topLevelDomains{{
                            _id
                            name
                        }}
                    }}
                }}"
            };

            var response = await _graphQLClient.SendQueryAsync<Countries>(query);
            return response.Data.Country.FirstOrDefault();
        }

        public async Task<List<Country>> GetCountriesAsync(string name)
        {

            var query = new GraphQLRequest
            {
                Query = $@"query GetCountries{{
                    Country{{
                        _id
                        area
                        capital
                        name
                        nativeName
                        population
                        populationDensity
                        flag{{
                            _id
                            emoji
                            emojiUnicode
                            svgFile
                        }}
                        topLevelDomains{{
                            _id
                            name
                        }}
                    }}
                }}"
            };

            var response = await _graphQLClient.SendQueryAsync<Countries>(query);

            var collection = _dataStore.GetCollection<Country>();
            response.Data.Country.ForEach(c => { c.IsCustomInformation = collection.AsQueryable().Any(x => x.Id == c.Id); });

            if (!string.IsNullOrWhiteSpace(name))
                return response.Data.Country.Where(FilterNameExpression(name)).ToList();
                       
            return response.Data.Country.ToList();
        }

        private Func<Country, bool> FilterNameExpression(string name)
        {
            var nameCapitalFirstLetter = char.ToUpper(name[0]) + name[1..];
            return country => country.Name.Contains(name.ToUpper()) || country.Name.Contains(name.ToLower()) || country.Name.Contains(nameCapitalFirstLetter);
        }

        public Country GetByCustomCountryId(long id)
        {
            var collection = _dataStore.GetCollection<Country>();
            return collection.AsQueryable().FirstOrDefault(c => c.Id == id.ToString());
        }
    }
}
