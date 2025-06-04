using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Html_Serializer
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper instance => _instance;
        public string[] HtmlTags { get; }
        public string[] HtmlVoidTags { get; }

        private HtmlHelper()
        {
            string directoryPath = "E:\\תכנות\\שנה ב\\פרקטיקוד\\פרוייקט 2 - Html Serializer\\JSON Files\\";
            //קריאה מהקבצים
            string jsonHtmlTags = File.ReadAllText(directoryPath + "HtmlTags.json").ToString();
            string jsonHtmlVoidTags = File.ReadAllText(directoryPath + "/HtmlVoidTags.json").ToString();
            //הצבה במשתני המחלקה
            this.HtmlTags = JsonSerializer.Deserialize<string[]>(jsonHtmlTags);
            this.HtmlVoidTags = JsonSerializer.Deserialize<string[]>(jsonHtmlVoidTags);
        }
    }
}
