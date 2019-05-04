using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxfordDictionariesAPI.Models;

namespace OxfordDictionariesAPI.Converters
{
    public class EntriesConverter : JsonConverter<SearchResult>
    {
        public override bool CanRead => true;

        public override SearchResult ReadJson(JsonReader reader, Type objectType, SearchResult existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jObject = JObject.Load(reader);
            var metadata = jObject["metadata"].ToObject<Metadata>();

            var jResults = (JArray)jObject["results"];
            var resultList = new List<Result>();

            foreach (var jResult in jResults)
            {
                var result = new Result();

                result.Id = (string)jResult["id"];
                result.Language = (string)jResult["language"];
                result.Type = (string)jResult["type"];
                result.Word = (string)jResult["word"];

                var lexicalEntries = new List<LexicalEntry>();
                var jLexicalEntries = (JArray)jResult["lexicalEntries"];

                foreach (var jLexicalEntry in jLexicalEntries)
                {
                    var lexicalEntry = new LexicalEntry();
                    lexicalEntry.Language = (string)jLexicalEntry["language"];
                    lexicalEntry.Text = (string)jLexicalEntry["text"];
                    lexicalEntry.LexicalCategory = (string)jLexicalEntry["lexicalCategory"]["text"];
                    var entries = new List<Entry>();
                    var jEntries = (JArray)jLexicalEntry["entries"];

                    foreach (var jEntry in jEntries)
                    {
                        var entry = new Entry();
                        var jEtymologies = (JArray)jEntry["etymologies"];
                        entry.Etymologies = jEtymologies is null ? new string[0] : jEtymologies.Select(jEtymology => (string)jEtymology).ToArray();

                        var jGrammarticalFeatures = (JArray)jEntry["grammaticalFeatures"];
                        if (jGrammarticalFeatures is null)
                        {
                            entry.GrammaticalFeatures = new GrammaticalFeature[0];
                        }
                        else
                        {
                            jGrammarticalFeatures
                                .Select(jGrammarticalFeature => new GrammaticalFeature { Text = (string)jGrammarticalFeature["text"], Type = (string)jGrammarticalFeature["type"] })
                                .ToArray();
                        }

                        var jSenses = (JArray)jEntry["senses"];
                        var senses = new List<Sense>();
                        foreach (var jSense in jSenses)
                        {
                            var sense = new Sense();
                            var jDefinitions = (JArray)jSense["definitions"];
                            if (jDefinitions is null)
                            {
                                sense.Definitions = new string[0];
                            }
                            else
                            {
                                sense.Definitions = jDefinitions.Select(jDefinition => (string)jDefinition).ToArray();
                            }

                            var jDomains = (JArray)jSense["domains"];
                            if (jDomains is null)
                            {
                                sense.Domains = new string[0];
                            }
                            else
                            {
                                sense.Domains = jDomains.Select(jDomain => (string)jDomain["text"]).ToArray();
                            }

                            sense.Id = (string)jSense["id"];

                            var jRegisters = (JArray)jSense["registers"];
                            if (jRegisters is null)
                            {
                                sense.Registers = new string[0];
                            }
                            else
                            {
                                sense.Registers = jRegisters.Select(jRegister => (string)jRegister["text"]).ToArray();
                            }

                            var jExamples = (JArray)jSense["examples"];
                            if (jExamples is null)
                            {
                                sense.Examples = new string[0];
                            }
                            else
                            {
                                sense.Examples = jExamples.Select(jExample => (string)jExample["text"]).ToArray();
                            }

                            var jSubsenses = (JArray)jSense["subsenses"];
                            if (jSubsenses is null)
                            {
                                sense.Subsenses = new Subsense[0];
                            }
                            else
                            {
                                var subsenses = new List<Subsense>();
                                foreach (var jSubsense in jSubsenses)
                                {
                                    var subsense = new Subsense();
                                    var jSubId = jSubsense["definition"];
                                    if (jSubId is null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        subsense.Id = (string)jSubId;
                                    }

                                    var jSubDefinitions = (JArray)jSubsense["definitions"];
                                    if (jSubDefinitions is null)
                                    {
                                        subsense.Definitions = new string[0];
                                    }
                                    else
                                    {
                                        subsense.Definitions = jSubDefinitions.Select(jSubDeifinition => (string)jSubDeifinition).ToArray();
                                    }

                                    var jSubExamples = (JArray)jSubsense["examples"];
                                    if (jSubExamples is null)
                                    {
                                        subsense.Examples = new string[0];
                                    }
                                    else
                                    {
                                        subsense.Examples = jSubExamples.Select(jSubExample => (string)jSubExample).ToArray();
                                    }

                                    var jSubDomains = (JArray)jSubsense["domains"];
                                    if (jSubDomains is null)
                                    {
                                        subsense.Domains = new string[0];
                                    }
                                    else
                                    {
                                        subsense.Domains = jSubDomains.Select(jSubDomain => (string)jSubDomain).ToArray();
                                    }

                                    var jSubRegions = (JArray)jSubsense["regions"];
                                    if (jSubRegions is null)
                                    {
                                        subsense.Regions = new string[0];
                                    }
                                    else
                                    {
                                        subsense.Regions = jSubRegions.Select(jSubRegion => (string)jSubRegion["text"]).ToArray();
                                    }

                                    var jSubRegisters = (JArray)jSubsense["registers"];
                                    if (jSubRegisters is null)
                                    {
                                        subsense.Registers = new string[0];
                                    }
                                    else
                                    {
                                        subsense.Registers = jSubRegisters.Select(jSubRegister => (string)jSubRegister["text"]).ToArray();
                                    }

                                    subsenses.Add(subsense);
                                }

                                sense.Subsenses = subsenses.ToArray();
                            }

                            senses.Add(sense);
                        }

                        entry.Senses = senses.ToArray();
                        entries.Add(entry);
                    }

                    lexicalEntry.Entries = entries.ToArray();

                    var jPronunciations = (JArray)jLexicalEntry["pronunciations"];
                    if (jPronunciations is null)
                    {
                        lexicalEntry.Pronunciations = new Pronunciation[0];
                    }
                    else
                    {
                        var pronunciations = new List<Pronunciation>();
                        foreach (var jPronunciation in jPronunciations)
                        {
                            var pronunciation = new Pronunciation();
                            pronunciation.AudioFile = (string)jPronunciation["audioFile"];
                            pronunciation.PhoneticNotation = (string)jPronunciation["phoneticNotation"];
                            pronunciation.PhoneticSpelling = (string)jPronunciation["phoneticSpelling"];

                            var jDialects = (JArray)jPronunciation["dialects"];
                            if (jDialects is null)
                            {
                                pronunciation.Dialects = new string[0];
                            }
                            else
                            {
                                pronunciation.Dialects = jDialects.Select(jDialect => (string)jDialect).ToArray();
                            }

                            pronunciations.Add(pronunciation);
                        }
                    }

                    lexicalEntries.Add(lexicalEntry);
                }

                result.LexicalEntries = lexicalEntries.ToArray();
                resultList.Add(result);
            }
            

            return new SearchResult { Metadata = metadata, Results = resultList.ToArray() };
        }

        public override void WriteJson(JsonWriter writer, SearchResult value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
