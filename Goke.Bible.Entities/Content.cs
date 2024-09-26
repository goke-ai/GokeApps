// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

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

                var w = ConvertTag(sb.ToString());

                return TrimWhitespace(w);
            }
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

    static string ConvertTag(string input)
    {
        // Use regex to capture the text and color attributes
        var match = Regex.Match(input, "<i text=\"(.*?)\" (.*?) ></i>");
        if (match.Success)
        {
            string text = match.Groups[1].Value;
            string rest = match.Groups[2].Value;
            return $"<i {rest}>{text}</i>";
        }
        return input; // Return the original input if no match is found
    }

}


