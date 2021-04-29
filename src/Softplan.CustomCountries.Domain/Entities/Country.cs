namespace Softplan.CustomCountries.Domain.Entities
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public partial class Countries
    {
        [JsonProperty("Country")]
        public List<Country> Country { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("_id")]
        //[JsonConverter(typeof(ParseStringConverter))]
        public string Id { get; set; }

        [JsonProperty("area")]
        public long? Area { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nativeName")]
        public string NativeName { get; set; }

        [JsonProperty("population")]
        public long Population { get; set; }
        
        [JsonProperty("populationDensity")]
        public decimal? PopulationDensity { get; set; }

        [JsonProperty("flag")]
        public Flag Flag { get; set; }

        [JsonProperty("topLevelDomains")]
        public List<TopLevelDomain> TopLevelDomains { get; set; }
        
        [JsonProperty("isCustomInformation")]
        public bool IsCustomInformation { get; set; }
    }

    public partial class Flag
    {
        [JsonProperty("_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("emoji")]
        public string Emoji { get; set; }

        [JsonProperty("emojiUnicode")]
        public string EmojiUnicode { get; set; }

        [JsonProperty("svgFile")]
        public Uri SvgFile { get; set; }
    }

    public partial class TopLevelDomain
    {
        [JsonProperty("_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Country
    {
        public static Country FromJson(string json) => JsonConvert.DeserializeObject<Country>(json, Softplan.CustomCountries.Domain.Entities.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Country self) => JsonConvert.SerializeObject(self, Softplan.CustomCountries.Domain.Entities.Converter.Settings);
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
