# OxfordDictionariesAPI
Unofficial Oxford Dictionaries API in .NET.
# Important! Update to version 2.0 since this version support v2 of Oxford dictionary. There are a few breaking changes as follows
- `Subsen` class was rename to `Subsense`.
- `Example` class was remove from `Sense` and `Subsense` and was replaced with string.

**You must update to 2.0 otherwise you will not be able to use it because Oxford is going to shutdown v1 API in June 2.19.**  

**Another important change in v2 API for entries endpoint is that English GB and US have separate dataset names i.e. `en-gb` and `en-us` the this library default to `en-gb`.  Continue usnig `en` still work but request will be redirected to en-gb and API usage is counted twice!**
## Requirement
- .NET Standard 1.1

## Available API
-  `/entries/{source_lang}/{word_id}`  

## Nuget
`Install-Package OxfordDictionariesAPI`
