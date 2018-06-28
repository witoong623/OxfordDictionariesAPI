using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Xunit;
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
    }
}
