using GraphQL;
using GraphQL.Client.Abstractions;
using JsonFlatFileDataStore;
using Softplan.CustomCountries.Domain.Entities;
using Softplan.CustomCountries.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;

using System.Linq;
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
                return await collection.UpdateOneAsync(country.Id, country);
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
                    Country{(string.IsNullOrEmpty(name) ? "" : $@"(name:""{name}"")")}{{
                        _id
                        area
                        capital
                        name
                        nativeName
                        population
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
            return response.Data.Country;
        }

        public Country GetByCustomCountryId(long id)
        {
            var collection = _dataStore.GetCollection<Country>();
            return collection.AsQueryable().FirstOrDefault(c => c.Id == id);
        }
    }
}
