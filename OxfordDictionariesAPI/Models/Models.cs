using System;
using System.Collections.Generic;
using System.Text;

namespace OxfordDictionariesAPI.Models
{
    public class Metadata
    {
        public string Provider { get; set; }
    }

    public class GrammaticalFeature
    {
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public class Example
    {
        public string Text { get; set; }
    }

    public class Subsense
    {
        public string[] Definitions { get; set; }
        public string[] Examples { get; set; }
        public string Id { get; set; }
        public string[] Domains { get; set; }
        public string[] Regions { get; set; }
        public string[] Registers { get; set; }
    }

    public class Sense
    {
        public string[] Definitions { get; set; }
        public string[] Domains { get; set; }
        public string[] Examples { get; set; }
        public string Id { get; set; }
        public Subsense[] Subsenses { get; set; }
        public string[] Registers { get; set; }
    }

    public class Entry
    {
        public GrammaticalFeature[] GrammaticalFeatures { get; set; }
        public string HomographNumber { get; set; }
        public Sense[] Senses { get; set; }
        public string[] Etymologies { get; set; }
    }

    public class Pronunciation
    {
        public string AudioFile { get; set; }
        public string[] Dialects { get; set; }
        public string PhoneticNotation { get; set; }
        public string PhoneticSpelling { get; set; }
    }

    public class LexicalEntry
    {
        public Entry[] Entries { get; set; }
        public string Language { get; set; }
        public string LexicalCategory { get; set; }
        public Pronunciation[] Pronunciations { get; set; }
        public string Text { get; set; }
    }

    public class Result
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public LexicalEntry[] LexicalEntries { get; set; }
        public string Type { get; set; }
        public string Word { get; set; }
    }

    public class SearchResult
    {
        public Metadata Metadata { get; set; }
        public Result[] Results { get; set; }
    }
}
