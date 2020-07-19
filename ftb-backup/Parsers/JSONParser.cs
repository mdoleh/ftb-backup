using Newtonsoft.Json;
using System.IO;

namespace ftb_backup.Parsers
{
    public class JSONParser<T>: IJSONParser<T>
    {
        public T ParseFile(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }
}
