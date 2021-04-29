namespace Softplan.CustomCountries.Domain.ViewModel
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class CountryViewModel
    {
        [JsonProperty("_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public string Id { get; set; }
                
        public long? Area { get; set; }
                
        public string Capital { get; set; }
                
        public string Name { get; set; }
                
        public string NativeName { get; set; }
                
        public long Population { get; set; }

        
        public decimal? PopulationDensity { get; set; }
                
        public FlagViewModel Flag { get; set; }
                
        public List<TopLevelDomainViewModel> TopLevelDomains { get; set; }
                
        public bool IsCustomInformation { get; set; }
    }

    public partial class FlagViewModel
    {
        [JsonProperty("_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }
                
        public string Emoji { get; set; }
                
        public string EmojiUnicode { get; set; }
                
        public Uri SvgFile { get; set; }
    }

    public partial class TopLevelDomainViewModel
    {
        [JsonProperty("_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }
        
        public string Name { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(long) || objectType == typeof(long?);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (Int64.TryParse(value, out long l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            serializer.Serialize(writer, value.ToString());
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
