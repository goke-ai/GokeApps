// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace Goke.Bible.Entities;

public class Translation
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Website { get; set; }
    public string? LicenseUrl { get; set; }
    public string? ShortName { get; set; }
    public string? EnglishName { get; set; }
    public string? Language { get; set; }
    public string? TextDirection { get; set; }
    public string? Sha256 { get; set; }
    public List<string>? AvailableFormats { get; set; }
    public string? ListOfBooksApiLink { get; set; }
    public int NumberOfBooks { get; set; }
    public int TotalNumberOfChapters { get; set; }
    public int TotalNumberOfVerses { get; set; }
    public string? LanguageName { get; set; }
    public string? LanguageEnglishName { get; set; }
}


