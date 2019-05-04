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
        public void TestExistingWordNoun()
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
        public void TestExistingWordVerb()
        {
            var searchResult = _client.SearchEntries("give", CancellationToken.None).Result;
            // result is present
            Assert.True(searchResult != null);
            // result is not empty
            Assert.True(searchResult.Results.Length > 0);
            // result match
            Result result = searchResult.Results.First();
            Assert.True(result.Id == "give" && result.Word == "give");
            // element in lexical
            Assert.True(result.LexicalEntries.Length == 2);
            Assert.True(result.LexicalEntries.First().LexicalCategory == "Verb");

            // Sense
            LexicalEntry lexicalEntry = result.LexicalEntries.First();
            Assert.True(lexicalEntry.Entries.Length == 1);
            Assert.True(lexicalEntry.Entries.First().Senses.Length == 7);
            Sense[] eachDefinition = lexicalEntry.Entries.First().Senses;
            Assert.True(eachDefinition.First().Definitions.First() == "freely transfer the possession of (something) to (someone)");
        }

        [Fact]
        public void TestExistingWordAdjective()
        {
            var searchResult = _client.SearchEntries("different", CancellationToken.None).Result;
            // result is present
            Assert.True(searchResult != null);
            // result is not empty
            Assert.True(searchResult.Results.Length > 0);
            // result match
            Result result = searchResult.Results.First();
            Assert.True(result.Id == "different" && result.Word == "different");
            // element in lexical
            Assert.True(result.LexicalEntries.Length == 1);
            Assert.True(result.LexicalEntries.First().LexicalCategory == "Adjective");

            // Sense
            LexicalEntry lexicalEntry = result.LexicalEntries.First();
            Assert.True(lexicalEntry.Entries.Length == 1);
            Assert.True(lexicalEntry.Entries.First().Senses.Length == 2);
            Sense[] eachDefinition = lexicalEntry.Entries.First().Senses;
            Assert.True(eachDefinition.First().Definitions.First() == "not the same as another or each other; unlike in nature, form, or quality");
        }

        [Fact]
        public void TestExistingWordAdverb()
        {
            var searchResult = _client.SearchEntries("almost", CancellationToken.None).Result;
            // result is present
            Assert.True(searchResult != null);
            // result is not empty
            Assert.True(searchResult.Results.Length > 0);
            // result match
            Result result = searchResult.Results.First();
            Assert.True(result.Id == "almost" && result.Word == "almost");
            // element in lexical
            Assert.True(result.LexicalEntries.Length == 1);
            Assert.True(result.LexicalEntries.First().LexicalCategory == "Adverb");

            // Sense
            LexicalEntry lexicalEntry = result.LexicalEntries.First();
            Assert.True(lexicalEntry.Entries.Length == 1);
            Assert.True(lexicalEntry.Entries.First().Senses.Length == 1);
            Sense[] eachDefinition = lexicalEntry.Entries.First().Senses;
            Assert.True(eachDefinition.First().Definitions.First() == "not quite; very nearly");
        }

        [Fact]
        public void TestExistingWordPronoun()
        {
            var searchResult = _client.SearchEntries("you", CancellationToken.None).Result;
            // result is present
            Assert.True(searchResult != null);
            // result is not empty
            Assert.True(searchResult.Results.Length > 0);
            // result match
            Result result = searchResult.Results.First();
            Assert.True(result.Id == "you" && result.Word == "you");
            // element in lexical
            Assert.True(result.LexicalEntries.Length == 1);
            Assert.True(result.LexicalEntries.First().LexicalCategory == "Pronoun");

            // Sense
            LexicalEntry lexicalEntry = result.LexicalEntries.First();
            Assert.True(lexicalEntry.Entries.Length == 1);
            Assert.True(lexicalEntry.Entries.First().Senses.Length == 2);
            Sense[] eachDefinition = lexicalEntry.Entries.First().Senses;
            Assert.True(eachDefinition.First().Definitions.First() == "used to refer to the person or people that the speaker is addressing");
        }

        [Fact]
        public void TestExistingWordPreposition()
        {
            var searchResult = _client.SearchEntries("under", CancellationToken.None).Result;
            // result is present
            Assert.True(searchResult != null);
            // result is not empty
            Assert.True(searchResult.Results.Length > 0);
            // result match
            Result result = searchResult.Results.First();
            Assert.True(result.Id == "under" && result.Word == "under");
            // element in lexical
            Assert.True(result.LexicalEntries.Length == 2);
            Assert.True(result.LexicalEntries.First().LexicalCategory == "Preposition");

            // Sense
            LexicalEntry lexicalEntry = result.LexicalEntries.First();
            Assert.True(lexicalEntry.Entries.Length == 1);
            Assert.True(lexicalEntry.Entries.First().Senses.Length == 5);
            Sense[] eachDefinition = lexicalEntry.Entries.First().Senses;
            Assert.True(eachDefinition.First().Definitions.First() == "extending or directly below");
        }

        [Fact]
        public void TestExistingWordConjunction()
        {
            var searchResult = _client.SearchEntries("unless", CancellationToken.None).Result;
            // result is present
            Assert.True(searchResult != null);
            // result is not empty
            Assert.True(searchResult.Results.Length > 0);
            // result match
            Result result = searchResult.Results.First();
            Assert.True(result.Id == "unless" && result.Word == "unless");
            // element in lexical
            Assert.True(result.LexicalEntries.Length == 1);
            Assert.True(result.LexicalEntries.First().LexicalCategory == "Conjunction");

            // Sense
            LexicalEntry lexicalEntry = result.LexicalEntries.First();
            Assert.True(lexicalEntry.Entries.Length == 1);
            Assert.True(lexicalEntry.Entries.First().Senses.Length == 1);
            Sense[] eachDefinition = lexicalEntry.Entries.First().Senses;
            Assert.True(eachDefinition.First().Definitions.First() == "except if (used to introduce the case in which a statement being made is not true or valid)");
        }

        [Fact]
        public void TestExistingWordInterjection()
        {
            var searchResult = _client.SearchEntries("oops", CancellationToken.None).Result;
            // result is present
            Assert.True(searchResult != null);
            // result is not empty
            Assert.True(searchResult.Results.Length > 0);
            // result match
            Result result = searchResult.Results.First();
            Assert.True(result.Id == "oops" && result.Word == "oops");
            // element in lexical
            Assert.True(result.LexicalEntries.Length == 1);
            Assert.True(result.LexicalEntries.First().LexicalCategory == "Interjection");

            // Sense
            LexicalEntry lexicalEntry = result.LexicalEntries.First();
            Assert.True(lexicalEntry.Entries.Length == 1);
            Assert.True(lexicalEntry.Entries.First().Senses.Length == 1);
            Sense[] eachDefinition = lexicalEntry.Entries.First().Senses;
            Assert.True(eachDefinition.First().Definitions.First() == "used to show recognition of a mistake or minor accident, often as part of an apology");
        }

        [Fact]
        public void TestExistingWordDeterminer()
        {
            var searchResult = _client.SearchEntries("his", CancellationToken.None).Result;
            // result is present
            Assert.True(searchResult != null);
            // result is not empty
            Assert.True(searchResult.Results.Length > 0);
            // result match
            Result result = searchResult.Results.First();
            Assert.True(result.Id == "his" && result.Word == "his");
            // element in lexical
            Assert.True(result.LexicalEntries.Length == 2);
            Assert.True(result.LexicalEntries.First().LexicalCategory == "Determiner");

            // Sense
            LexicalEntry lexicalEntry = result.LexicalEntries.First();
            Assert.True(lexicalEntry.Entries.Length == 1);
            Assert.True(lexicalEntry.Entries.First().Senses.Length == 2);
            Sense[] eachDefinition = lexicalEntry.Entries.First().Senses;
            Assert.True(eachDefinition.First().Definitions.First() == "belonging to or associated with a male person or animal previously mentioned or easily identified");
        }

        [Fact]
        public void TestNotFoundWord()
        {
            var searchResult = _client.SearchEntries("liewsdg", CancellationToken.None).Result;
            Assert.True(searchResult == null);
        }

        [Fact(Skip = "obsolete API")]
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
