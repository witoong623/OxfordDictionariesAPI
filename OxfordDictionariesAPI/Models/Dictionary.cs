using System;
using System.Collections.Generic;
using System.Text;

namespace OxfordDictionariesAPI.Models
{
    /// <summary>
    /// A type of dictionary
    /// </summary>
    public enum DictionaryType { Monolingual, Bilingual }

    /// <summary>
    /// A class that hold information about dictionary
    /// </summary>
    public class OxfordDictionary
    {
        /// <summary>
        /// The region e.g. gb or us, this property maybe null
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The source of this dictionary
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The source language of this dictionary in IANA language code
        /// </summary>
        public string SourceLanguage { get; set; }

        /// <summary>
        /// The translate to language of this dictionary in IANA language code, this property is null if Type is Monolingual
        /// </summary>
        public string TargetLanguage { get; set; }

        // The type of this dictionary
        public DictionaryType Type { get; set; }
        
    }
}