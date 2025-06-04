using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Html_Serializer
{
    public class Selector
    {
        public string TagName { get; set; } = "";
        
        public string Id { get; set; } = "";

        public List<string> Classes { get; set; } = null!;

        public Selector Parent { get; set; } = null!;

        public Selector Child { get; set; }= null!;

        //פונקציה המקבלת מחרוזת של סלקטור שלם ומחזירה את העץ שלו
        //הפונקציה חותכת אותו לפי רווחים ושולחת כל חלק לפונקציה הבאה שבונה אותו
        public static Selector buildSelectorTree(string str)  
        {
            Selector root =new Selector();
            Selector currentSelector=root;
            //1- חיתוך המחרוזת לפי רווחים
            string[] elements = str.Split(' ');
            //2- מעבר על כל חלק במחרוזת ובניית האובייקט שלו
            foreach (string element in elements)
            {
                if (element.Length > 0)
                {
                    Selector selector = CreateSelector(element);
                    selector.Parent = currentSelector;
                    if (currentSelector != null)
                        currentSelector!.Child = selector;
                    currentSelector = selector;
                }
            }
            return root.Child;
        }

        //פונקציה שמקבלת חלק אחד מהסלקטור ומחזירה את האלמנט שלו- כולל כל הנתונים
        public static Selector CreateSelector(string selector)
        {
            Selector s = new Selector();
            var idMatch = Regex.Match(selector, @"#(\w+)");
            var classMatches = Regex.Matches(selector, @"\.(\w+)");

            s.Id = idMatch.Groups[1].Value;

            int i = selector.IndexOf(".");
            int j = selector.IndexOf("#");
            if (i == j)
                s.TagName = selector;
            else if (i == -1)
                s.TagName = selector.Substring(0, j);
            else if (j == -1)
                s.TagName = selector.Substring(0, i);
            else
                s.TagName = selector.Substring(0, Math.Min(i, j));

            List<string> classes = new List<string>();
            for (int k = 0; k < classMatches.Count; k++)
                classes.Add(classMatches[k].Groups[1].Value);
            s.Classes = classes;

            return s;
        }
    }
}
