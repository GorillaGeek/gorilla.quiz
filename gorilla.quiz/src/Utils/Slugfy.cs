using System.Text;
using System.Text.RegularExpressions;

namespace GorillaQuiz.Utils
{
    public class Slugfy
    {
        public static string ToSlug(string source, short? limit = null)
        {

            if (null == source)
            {
                return "";
            }

            //Remove os acentos
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(source);
            var value = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            // Converte "GooglePlus" em "-google-plus"
            value = Regex.Replace(value, @"([A-Z]+)", "-$1").ToLowerInvariant();

            // Remove caracteres inválidos
            value = Regex.Replace(value, @"[^a-z0-9]+", "-");

            // Remove hífens do início e do fim da string
            value = value.Trim('-');

            // Se houver um length e a string for maior do que ele, trunca a string
            if (limit.HasValue && value.Length > limit)
            {
                value = value.Substring(0, limit.Value).Trim('-');
            }

            return value;
        }
    }
}