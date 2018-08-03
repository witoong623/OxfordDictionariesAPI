using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Xunit;
using FluentAssertions;
using OxfordDictionariesAPI;
using OxfordDictionariesAPI.Models;

namespace OxfordDictionariesAPITest
{
    public class UnitTest1
    {
        private OxfordDictionaryClient _client =  new OxfordDictionaryClient("e683c99e", "253d7364925b87e1a14fc0a4dcd1cbfb");
        [Fact]
        public void TestExistingWord()
        {
            var searchResult = _client.SearchEntries("study", CancellationToken.None).Result;
            // result is present
            Assert.True(searchResult != null);
            // result is not empty
            Assert.True(searchResult.Results.Length > 0);
            // result match
            Result result = searchResult.Results.First();
            Assert.True(result.Id == "study" && result.Word == "study");
            // element in lexical
            Assert.True(result.LexicalEntries.Length == 2);
            Assert.True(result.LexicalEntries.First().LexicalCategory == "Noun");
            // Sense
            LexicalEntry lexicalEntry = result.LexicalEntries.First();
            Assert.True(lexicalEntry.Entries.Length == 1);
            Assert.True(lexicalEntry.Entries.First().Senses.Length == 5);
            Sense[] eachDefinition = lexicalEntry.Entries.First().Senses;
            Assert.True(eachDefinition.First().Definitions.First() == "the devotion of time and attention to gaining knowledge of an academic subject, especially by means of books");
        }

        [Fact]
        public void TestNotFoundWord()
        {
            var searchResult = _client.SearchEntries("liewsdg", CancellationToken.None).Result;
            Assert.True(searchResult == null);
        }

        [Fact]
        public void TestAvailableDictionaries()
        {
            var dictionaries = _client.GetAvailableDictionaries(CancellationToken.None).Result;
            Assert.NotNull(dictionaries);

            // First in list, only source language
            var actual1 = dictionaries.First();

            var expected1 = new OxfordDictionary
            {
                Region = "gb",
                Source = "Oxford Dictionary of English 3e",
                SourceLanguage = "en",
                Type = DictionaryType.Monolingual
            };

            actual1.Should().BeEquivalentTo(expected1);

            // 7th in list, bilingual
            var actual2 = dictionaries[6];

            var expected2 = new OxfordDictionary
            {
                Source = "Oxford Spanish Dictionary 5e",
                SourceLanguage = "en",
                TargetLanguage = "es",
                Type = DictionaryType.Bilingual
            };

            actual2.Should().BeEquivalentTo(expected2);
        }
    }
}
