// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

namespace Goke.Bible.Entities;

public class Reference
{
    public int Chapter { get; set; }
    public int Verse { get; set; }
}


