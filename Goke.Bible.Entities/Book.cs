// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace Goke.Bible.Entities;

public class Book
{
    public string? Id { get; set; }
    public string? TranslationId { get; set; }
    public string? Name { get; set; }
    public string? CommonName { get; set; }
    public string? Title { get; set; }
    public int Order { get; set; }
    public int NumberOfChapters { get; set; }
    public string? Sha256 { get; set; }
    public string? FirstChapterApiLink { get; set; }
    public string? LastChapterApiLink { get; set; }
    public int TotalNumberOfVerses { get; set; }
}


