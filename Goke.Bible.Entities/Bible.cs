// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using System.Text.RegularExpressions;

namespace Goke.Bible.Entities;

public class Bible
{
    public Translation? Translation { get; set; }
    public List<Book>? Books { get; set; }
}


