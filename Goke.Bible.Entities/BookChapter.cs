// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace Goke.Bible.Entities;

public class BookChapter
{
    public Translation? Translation { get; set; }
    public Book? Book { get; set; }
    public Chapter? Chapter { get; set; }
    public string? ThisChapterLink { get; set; }
    public ThisChapterAudioLinks? ThisChapterAudioLinks { get; set; }
    public string? NextChapterApiLink { get; set; }
    public NextChapterAudioLinks? NextChapterAudioLinks { get; set; }
    public string? PreviousChapterApiLink { get; set; }
    public PreviousChapterAudioLinks? PreviousChapterAudioLinks { get; set; }
    public int NumberOfVerses { get; set; }
}


