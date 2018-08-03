using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using OxfordDictionariesAPI.Models;

namespace OxfordDictionariesAPI.Converters
{
    public class OxfordDictionaryConverter : JsonConverter<List<OxfordDictionary>>
    {
        public override bool CanRead => true;

        public override List<OxfordDictionary> ReadJson(JsonReader reader, Type objectType, List<OxfordDictionary> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            List<OxfordDictionary> dictionaries = new List<OxfordDictionary>();

            var jObject = JObject.Load(reader);
            JArray results = (JArray)jObject["results"];

            foreach (var dic in results)
            {
                var dictionary = new OxfordDictionary();

                var region = (string)dic["region"];
                var source = (string)dic["source"];
                JToken sourceLang = dic["sourceLanguage"];
                JToken targetLang = dic["targetLanguage"];
                var typeString = (string)dic["type"];

                dictionary.Region = region;
                dictionary.Source = source;
                dictionary.SourceLanguage = (string)sourceLang["id"];

                if (targetLang != null)
                {
                    dictionary.TargetLanguage = (string)targetLang["id"];
                }

                if (typeString == "monolingual")
                {
                    dictionary.Type = DictionaryType.Monolingual;
                }
                else
                {
                    dictionary.Type = DictionaryType.Bilingual;
                }

                dictionaries.Add(dictionary);
            }

            return dictionaries;
        }

        public override void WriteJson(JsonWriter writer, List<OxfordDictionary> value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
