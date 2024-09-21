// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using System.Text.Json.Serialization;

namespace Goke.Bible.Entities;

public class Chapter
{
    public int Number { get; set; }
    [JsonPropertyName("content")]
    public List<Content>? Verses { get; set; }
    public List<Footnote>? Footnotes { get; set; }
}


