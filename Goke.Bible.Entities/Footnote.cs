// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace Goke.Bible.Entities;

public class Footnote
{
    public int NoteId { get; set; }
    public string? Caller { get; set; }
    public string? Text { get; set; }
    public Reference? Reference { get; set; }

    public string DisplayText => $"{Caller}{NoteId}: {Text}";


}


