// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Goke.Bible.Entities;

public class Content
{
    public string? Type { get; set; }
    public int? Number { get; set; }
    [JsonPropertyName("content")]
    public List<object>? Contents { get; set; }
    //public string? Verse => Type == "verse" ? Contents?.Select(ToVerse).Aggregate((a, n) => a + " " + n) : string.Empty;
    public string? Verse => Contents?.Select(ToVerse).Aggregate((a, n) => a + " " + n);

    public static string ToVerse(object s)
    {
        if (s == null)
        {
            return string.Empty;
        }

        if (((JsonElement)s).ValueKind == JsonValueKind.Object
            //|| s.ToString()!.Contains("{") 
            )
        {
            var keyValues = JsonSerializer.Deserialize<Dictionary<string, object>>(s.ToString()!);

            if (keyValues is not null)
            {
                StringBuilder sb = new StringBuilder("<i ");

                foreach (var k in keyValues)
                {
                    sb.Append(k.Key).Append('=').Append('"').Append(k.Value).Append('"').Append(' ');
                }
                sb.Append("></i>");

                //return sb.ToString().Trim('¶', ' ');
                return TrimWhitespace(sb.ToString()!);
            }
            //if (keyValues?.TryGetValue("text", out object? v) == true)
            //{
            //    if (keyValues?.TryGetValue("wordsOfJesus", out object? b) == true)
            //    {
            //        if (b.ToString() == "True")
            //        {
            //            return $"<i>{v}</i>";
            //        }
            //        return v.ToString()!;
            //    }
            //}
            else
            {
                return string.Empty;
            }
        }
        // return s.ToString()!.Trim('¶', ' ');
        return TrimWhitespace(s.ToString()!);

    }

    static string TrimWhitespace(string s)
    {
        return s.Trim('¶', ' ');
        // Using regex to remove all whitespace characters
        //return Regex.Replace(s, @"\s+", "");
    }

}


