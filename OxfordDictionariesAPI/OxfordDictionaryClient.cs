using System;
using System.Net.Http;
using System.Net.Http.Headers;
using OxfordDictionariesAPI.Models;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using OxfordDictionariesAPI.Converters;

namespace OxfordDictionariesAPI
{
    public class OxfordDictionaryClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the OxfordDictionaryClient class.  This instance should be reused.
        /// </summary>
        /// <param name="app_id">Oxford Dictionary application id</param>
        /// <param name="app_key">Oxford Dictionary application key</param>
        public OxfordDictionaryClient(string app_id, string app_key)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.BaseAddress = new Uri(@"https://od-api.oxforddictionaries.com/api/v2/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("app_id", app_id);
            _httpClient.DefaultRequestHeaders.Add("app_key", app_key);
        }

        /// <summary>
        /// Retrieve available dictionary entries for a given word and language.
        /// Return SearchResult of a given word. Return null if not found.
        /// </summary>
        /// <param name="word">A word that want to search.  It should be lowercase and should replace whitespace with underscore</param>
        /// <param name="ct">CancellationToken use for cancel</param>
        /// <param name="language">Abbreviated name of language, must be lowercase</param>
        /// <returns>SearchResult of a given word. Return null if not found</returns>
        /// <exception cref="HttpRequestException">Throw when underlying HTTPClient throw exception other than 404 HTTP error</exception>
        public async Task<SearchResult> SearchEntries(string word, CancellationToken ct, string language = "en-gb")
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word));

            word = word.Trim().Replace(" ", "_");
            var searchPath = $"entries/{language}/{word}";

            var jsonString = await SendHttpGetRequest(searchPath, ct);

            if (jsonString == null)
            {
                // null if not found
                return null;
            }

            return JsonConvert.DeserializeObject<SearchResult>(jsonString, new EntriesConverter());
        }

        /// <summary>
        /// Get available dictionaries in Oxforad Dictionary API, return null if not found
        /// </summary>
        /// <param name="sourceLanguage">IANA language code. If provided output will be filtered by sourceLanguage</param>
        /// <param name="targetLanguage">IANA language code. If provided output will be filtered by targetLanguage</param>
        /// <returns>List of OxfordDictionary</returns>
        /// <exception cref="HttpRequestException">Throw when underlying HTTPClient throw exception other than 404 HTTP error</exception>
        [Obsolete]
        public async Task<List<OxfordDictionary>> GetAvailableDictionaries(CancellationToken ct, string sourceLanguage = null, string targetLanguage = null)
        {
            var path = "languages";

            if (!string.IsNullOrWhiteSpace(sourceLanguage) && string.IsNullOrWhiteSpace(targetLanguage))
            {
                path += $"?sourceLanguage={sourceLanguage}";
            }
            else if (string.IsNullOrWhiteSpace(sourceLanguage) && !string.IsNullOrWhiteSpace(targetLanguage))
            {
                path += $"?targetLanguage={targetLanguage}";
            }
            else if (!string.IsNullOrWhiteSpace(sourceLanguage) && !string.IsNullOrWhiteSpace(targetLanguage))
            {
                path += $"?sourceLanguage={sourceLanguage}&targetLanguage={targetLanguage}";
            }

            string jsonString = await SendHttpGetRequest(path, ct);

            if (jsonString == null)
            {
                // null if not found
                return null;
            }

            return JsonConvert.DeserializeObject<List<OxfordDictionary>>(jsonString, new OxfordDictionaryConverter());
        }

        private async Task<string> SendHttpGetRequest(string path, CancellationToken ct)
        {
            HttpResponseMessage responseMsg = null;

            try
            {
                responseMsg = await _httpClient.GetAsync(path, ct);

                if (responseMsg.IsSuccessStatusCode)
                {

                    return await responseMsg.Content.ReadAsStringAsync();
                }
                else
                {
                    responseMsg.EnsureSuccessStatusCode();
                    // Will never return because EnsureSuccessStatusCode throws exception.
                    return null;
                }
            }
            catch (HttpRequestException)
            {
                if (responseMsg.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // in case not found return null
                    return null;
                }
                else
                {
                    // Otherwise re-throw exception from underlying HTTPClient
                    throw;
                }
            }
            catch (OperationCanceledException)
            {
                // Explicitly indicate that operation was canceled
                throw;
            }
            finally
            {
                if (responseMsg != null)
                    responseMsg.Dispose();
            }
        }

        public void Dispose()
        {
            if (_httpClient != null)
                _httpClient.Dispose();
        }
    }
}
