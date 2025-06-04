using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Html_Serializer
{
    public class HtmlElement
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public List<Match> Attributes { get; set; } = new List<Match>();
        public List<string> Classes { get; set; } = new List<string>();
        public string InnerHtml { get; set; } = "";
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();


        //ובונה ממנה עץ html הפונקציה מקבלת מחרוזת 
        public static HtmlElement buildHtmlTree(string html)
        {
            string[] htmlLins = new Regex("<(.*?)>").Split(html).Where(s => s.Length > 0).ToArray();
            HtmlElement root = new HtmlElement();
            root.Name = "root";
            HtmlElement currentElement = root;
            Console.WriteLine(htmlLins.Length);
            foreach (string htmlLine in htmlLins)
                if (new Regex("\\s").Replace(htmlLine, "") != "")
                {
                    string firstWord;
                    int spaceIndex = htmlLine.IndexOf(' ') != -1 ? htmlLine.IndexOf(' ') : htmlLine.Length;
                    firstWord = htmlLine.Substring(0, spaceIndex);
                    //אם זה תגית סוגרת
                    if (htmlLine[0] == '/' && firstWord.Length > 1 && isTag(firstWord.Substring(1)))
                    {
                        //תנאי עצירה
                        if (firstWord.Equals("html"))
                            return root;
                        //נחזור לאבא
                        currentElement = currentElement.Parent;
                    }
                    //אם זה תגית פותחת
                    else if (isTag(firstWord))
                    {
                        HtmlElement newTag = CreateElement(htmlLine, firstWord, currentElement);
                        if (isVoidTag(firstWord) == false || firstWord.Equals("!DOCTYPE") == false)
                            currentElement = newTag;
                    }
                    //אני תוכן של תגית
                    else currentElement.InnerHtml = htmlLine;
                }
            Console.WriteLine("build end!");
            return root;
        }

        //ואת האלמנט הנוכחי. היא יוצרת תגית חדשה ומעדכנת אותה בעץ html-הפונקציה מקבלת שורה ב
        public static HtmlElement CreateElement(string htmlLine, string firstWord, HtmlElement currentElement)
        {
            HtmlElement newTag = new HtmlElement();
            newTag.Name = firstWord;
            var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlLine);
            newTag.Attributes = attributes.Where(m => m.Groups[1].Value != "id" && m.Groups[1].Value != "class").ToList();
            var att = attributes.FirstOrDefault(m => m.Groups[1].Value == "class");
            if (att != null)
                newTag.Classes = att.Groups[2].Value.Split(" ").Where(c => c.Length > 0).ToList();
            att = attributes.FirstOrDefault(m => m.Groups[1].Value == "id");
            if (att != null)
                newTag.Id = att.Groups[2].Value;
            newTag.Parent = currentElement;
            currentElement.Children.Add(newTag);
            return newTag;
        }

        //הפונקציה מחזירה רשימה של כל הצאצאים של האלמנט שקרא לה
        public IEnumerable<HtmlElement> Descendants()
        {
            List<HtmlElement> list = new List<HtmlElement>();
            Queue<HtmlElement> queue = new Queue<HtmlElement>();

            queue.Enqueue(this);
            HtmlElement currentElement;

            while(queue.Count > 0)
            {
                //ניקח את הראשון בתור
                currentElement = queue.Dequeue();
                //נכניס את ילדיו לתור
                foreach(HtmlElement child in currentElement.Children)
                    queue.Enqueue(child);
                //נכניס אותו לרשימה
                if(currentElement!=this)
                    yield return currentElement;

            }
        }

        //הפונקציה מחזירה רשימה של כל האבות של האלמנט שקרא לה
        public List<HtmlElement> Ancestors()
        {
            List<HtmlElement> list=new List<HtmlElement>();
            HtmlElement temp = this.Parent;
            while (temp != null)
            {
                list.Add(temp);
                temp = temp.Parent;
            }
            return list;
        }
        
        //-פונקצית מעטפת לפונקציה הבאה
        //הפונקציה מקבלת מחרוזת סלקטור ובונה עץ סלקטורים
        //היא שולחת לפונקציה הפנימית שלה רשימה למילוי ואח"כ מחזירה אותה
        public List<HtmlElement> GetElementBySelector(string str)
        {
            Selector selector = Selector.buildSelectorTree(str);
            //List<HtmlElement> list=new List<HtmlElement>();
            HashSet<HtmlElement> set = new HashSet<HtmlElement>();
            GetElementBySelector(selector, this, set);
            return set.ToList();
        }

        //הפונקציה הפנימית של הפונקציה הקודמת
        //פונקציה רקורסיבית שמציבה ברשימה את כל האלמנטים התואמים לסלקטור
        public void GetElementBySelector(Selector selector, HtmlElement htmlElement, HashSet<HtmlElement> set)
        {
            IEnumerable<HtmlElement> Descendants = htmlElement.Descendants();
            bool isMatch = MatchesSelector(selector, htmlElement);
            if(isMatch && selector.Child==null)
                set.Add(htmlElement);
            foreach (HtmlElement des in Descendants)
                if (isMatch)
                    GetElementBySelector(selector.Child, des, set);
                else
                    GetElementBySelector(selector, des, set);
        }

        //פונקציה בוליאנית שמקבלת אלמנט וסלקטור ובודקת התאמה ביניהם
        public bool MatchesSelector(Selector selector, HtmlElement htmlElement)
        {
            if(selector==null) return false;
            if (selector.TagName != "" && !htmlElement.Name.Equals(selector.TagName))
                return false;
            if (selector.Id != "" && !htmlElement.Id.Equals(selector.Id))
                return false;
            for (int i = 0; i < selector.Classes.Count(); i++)
                if(!htmlElement.Classes.Contains(selector.Classes[i]))
                    return false;
            return true; 
        }

        //פונקציה בוליאנית שבודקת אם זה תגית
        public static bool isTag(string tagName)
        {
            HtmlHelper htmlHelper = HtmlHelper.instance;
            if (htmlHelper.HtmlTags.Contains(tagName))
                return true;
            //if (HtmlHelper.instance.HtmlTags.Contains(tagName)) return true;
            return false;
        }

        //void פונקציה בוליאנית שבודקת אם זו תגית מסוג
        public static bool isVoidTag(string tagName)
        {
            HtmlHelper htmlHelper = HtmlHelper.instance;
            if (htmlHelper.HtmlVoidTags.Contains(tagName))
                return true;
            //if (HtmlHelper.instance.HtmlVoidTags.Contains(tagName)) return true;
            return false;
        }

        //toString פונקצית
        override public string ToString()
        {
            string Classes = "";
            foreach (string c in this.Classes)
                Classes += c + " ";
            return "[ Name: " + this.Name + ", Id: " + this.Id + ", Classes: " + Classes + "]\n";
        }
    }
}
