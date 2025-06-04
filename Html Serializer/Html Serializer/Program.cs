using Html_Serializer;
using System.Linq;
using System.Text.RegularExpressions;

//שלו html-הפונקציה מקבלת כתובת אתר ומחזירה את ה
async Task<string> LoadHtml(string url)
{
    try
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync(url);
        var html = await response.Content.ReadAsStringAsync();
        return html;
    }
    catch
    {
        return realHtml3();
    }
}

//למעקב כשאין אינטרנט html קודים קצרים של
string realHtml1()
{
    return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head id=\"id1\">\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" " +
        "content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Simple HTML Page</title>\r\n    " +
        "<link rel=\"stylesheet\" href=\"styles.css\">\r\n</head>\r\n<body>\r\n    <header>\r\n" +
        "        <h1>Welcome to My Simple Page</h1>\r\n        <nav>\r\n            <ul id=\"ul_id\">\r\n                " +
        "<a class=\"link1 link2 link \" href=\"#home\">Home</a><li class=\"li_class\"><a class=\"link1 link2 link \" href=\"#home\">Home</a></li>\r\n" +
        "                <li class=\"li_class\"><a href=\"#about\">About</a></li>\r\n               " +
        " <li class=\"li_class\"><a href=\"#contact\">Contact</a></li>\r\n            </ul>\r\n       " +
        " </nav>\r\n    </header>\r\n    <main>\r\n        <section id=\"home\">\r\n            " +
        "<h2>Home Section</h2>\r\n            <p>This is the home section of the page.</p>\r\n        </section>\r\n       " +
        " <section id=\"about\">\r\n            <h2>About Section</h2>\r\n            " +
        "<p>Information about the website.</p>\r\n        </section>\r\n        <section id=\"contact\">\r\n" +
        "            <h2>Contact Section</h2>\r\n            <p>How to reach us.</p>\r\n        </section>\r\n    </main>\r\n    " +
        "<footer>\r\n        <p>&copy; 2023 My Simple Page</p>\r\n    </footer>\r\n</body>\r\n</html>";
}
string realHtml2()
{
    return "<div>\r\n    <div>\r\n        <p class=”class-name”></p>\r\n    </div>\r\n</div>";
}
string realHtml3()
{
    return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Document</title>\r\n    <link rel=\"stylesheet\" href=\"Project_style.css\">\r\n    <link rel=\"stylesheet\" href=\"info.css\">\r\n</head>\r\n<body>\r\n    <button class=\"mobile_button\">פתיחת תפריט</button>\r\n    <nav>   \r\n        <a href=\"home_page.html\">מרכז המבקרים של במבה</a>\r\n        <a href=\"inform.html\">מידע שימושי למבקרים</a>\r\n        <a href=\"form.html\">יצירת קשר</a>\r\n        <a href=\"book_cards1.html\">רכישת כרטיסים</a>\r\n        <div class=\"button none\">\r\n            <a href=\"book_cards1.html\">להזמנת סיור</a>\r\n        </div>\r\n    </nav>\r\n    <main>\r\n        <aside id=\"side_bar\">\r\n            <ul>\r\n                <li class=\"ask\"><a href=\"#ans1\">היכן ממוקמת ארץ במבה?</a></li>\r\n                <li class=\"ask\"><a href=\"#ans2\">האם המרכז נגיש?</a></li>\r\n                <li class=\"ask\"><a href=\"#ans3\">האם ניתן לרכוש אוכל/ שתיה במקום?</a></li>\r\n                <li class=\"ask\"><a href=\"#ans4\">האם אפשר להביא תינוקות?</a></li>\r\n                <li class=\"ask\"><a href=\"#ans5\">האם חובה להזמין כרטיסים מראש?</a></li>\r\n                <li class=\"ask\"><a href=\"#ans6\">האם הסיור בעייתי לבעלי רגישות לבוטנים?</a></li>\r\n            </ul>\r\n        </aside>\r\n        <div id=\"center\">\r\n            <p>היי חברים! זה אני, התינוק של במבה!</p>\r\n            <p>לפני שתבואו לבקר אותי בבית שלי, במרכז המבקרים של במבה,</p>\r\n            <p>קיבצתי לכם שאלות ותשובות כדי שתהיו רגועים ותוכלו לבוא בראש שקט.</p>\r\n            <p>מוכנים? קדימה...</p>\r\n            <ul id=\"answer\" class=\"margin_bottom\">\r\n                <li><div id=\"ans1\">\r\n                    <h3>היכן ממוקמת ארץ במבה?</h3>\r\n                    <p> מרכז המבקרים של במבה נמצא בקריית גת והינו חלק ממפעל הבמבה.</p>\r\n                    <p> ההגעה למרכז המבקרים מתבצעת ע\"י \"הבמבה-בוס\" הקסום, שאוסף ומחזיר את המבקרים ללא תוספת תשלום משתי תחנות האיסוף. </p>\r\n                    <p>בחירת נקודת האיסוף מתבצעת בזמן רכישת הכרטיס (מופיעה על גבי הכרטיס)  וזוהי הנקודה שעליכם להגיע אליה כדי להמתין לאיסוף.</p>\r\n                    <p>תחנה 1:</p>\r\n                    <p>למגיעים ברכב פרטי תתאפשר חניה חינם בחניון באזור התעשיה, קרית גת.</p>\r\n                    <p>תחנה 2:</p>\r\n                    <p>למגיעים באמצעות רכבת ישראל בלבד - תחנת הרכבת קרית גת.</p>\r\n                    <p>לא תתאפשר הגעה עצמאית לשטח המפעל. ההגעה למרכז המבקרים הינה באמצעות ה\"במבה -בוס\" בלבד.</p>\r\n                </div></li>\r\n                <li><div id=\"ans2\">\r\n                    <h3>האם המרכז נגיש?</h3>\r\n                    <p>בהחלט! ארץ במבה נגישה ומתאימה לאנשים עם מגבלויות.</p>\r\n                    <p>על התכנון המוקפד והתאמה לצרכיהם של מוגבלים, שקד הצוות של במבה במשך תקופה ארוכה מתוך רצון לאפשר להם להנות במתחם ככל האדם.</p>\r\n                    <p>חברת המותג שלנו דוגלת בשויון בין בני האדם בלי קשר ליכולותיהם הפיזיות ולכבוד הוא לנו לארח במרכז שלנו אנשים גיבורים אלו.</p>\r\n                    <p>אנשים עם תעודת נכה יזכו גם בהנחת כניסה משתלמת של 15% וישלמו 17 שקלים במקום 20.</p>\r\n                </div></li>\r\n                <li><div id=\"ans3\">\r\n                    <h3>האם ניתן לרכוש אוכל/ שתיה במקום?</h3>\r\n                    <p>אין להכניס בשום פנים ואופן אוכל או שתיה לשטח המתחם! (תערך בדיקה בכניסה)</p>\r\n                    <p>הכנסת מזון או משקה למפעל פוגמת בהגיינה הקפדנית של אסם ואף אסורה על פי הוראות משרד הבריאות.</p>\r\n                    <p>למען המבקרים דאגה אסם לקיוסק ופינת ישיבה נעימה ומטופחת ביציאה ממרכז המבקרים, כדי לאפשר לאורחים להנות למרות האיסור לאכול בשטח המתחם.</p>\r\n                </div></li>\r\n                <li><div id=\"ans4\">\r\n                    <h3>האם אפשר להביא תינוקות?</h3>\r\n                    <p>ובכן, לצערנו, התשובה שלילית.</p>\r\n                    <p>אין להביא למקום תינוקות וילדים מתחת לגיל ארבע!</p>\r\n                    <p>לא נאפשר בשום אופן כניסת תינוקות למתחם!</p>\r\n                    <p>במרכז המבקרים שלנו יהנו המבקרים, מבוגרים וילדים כאחד משלל חוויות מרתקות ומהנות.</p>\r\n                </div></li>\r\n                <li><div id=\"ans5\">\r\n                    <h3>האם חובה להזמין כרטיסים מראש?</h3>\r\n                    <p>בהחלט! וטוב מאד ששאלתם!</p>\r\n                    <p>לא נוכל להכניס מבקרים שלא הזמינו מקום מראש, גם ככה הלו\"ז של בובה במבה עמוס...</p>\r\n                    <p>להזמנת כרטיסים תוכלו להכנס לאתר של במבה בכתובת .......</p>\r\n                    <p>תוכלו גם לשלוח מייל לכתובת osem.nestle@IL.nestle.com</p>\r\n                    <p>או לחייג לשרות הלקוחות של במבה במספר 1700-70-76-76</p>\r\n                    <p>בכל דרך שהיא נשמח לעמוד לשרותכם, במבה והצוות של אסם.</p>\r\n                </div></li>\r\n                <li><div id=\"ans6\">\r\n                    <h3>האם הסיור בעייתי לבעלי רגישות לבוטנים?</h3>\r\n                    <p>זאת שאלה מצוינת ומעידה על רגישות ואחריות!</p>\r\n                    <p>כחלק מהתנהלותו של המרכז יבקרו האורחים בפסי היצור והאריזה ויחשפו לבמבה ובוטנים,</p>\r\n                    <p>דבר שמסוכן בהחלט לרגישים ואלרגים.</p>\r\n                    <p>לפיכך השהות במקום לרגישים לבמבה ולבוטנים היא מסוכנת ביותר!</p>\r\n                    <p> שמרו על עצמכם והמנעו מכניסה במידה ואתם אכן רגישים.</p>\r\n                </div></li> \r\n            </ul>\r\n            <div id=\"open_galery\">\r\n                <div class=\"button\">\r\n                    <li> לפתיחת גלריה</li>\r\n                </div>\r\n                <div class=\"flex_c\" id=\"galery\">\r\n                    <div class=\"flex_r\">\r\n                        <div>\r\n                            <div>\r\n                                <img src=\"P&I/galery1.png\" alt=\"bamba-galery-1\" class=\"long\">\r\n                                <img src=\"P&I/galery2.png\" alt=\"bamba-galery-2\">\r\n                            </div>\r\n                            <div class=\"flex_r\">\r\n                                <img src=\"P&I/galery6.png\" alt=\"bamba-galery-6\" class=\"tall\">\r\n                                <div class=\"flex_c\">\r\n                                    <img src=\"P&I/galery4.png\" alt=\"bamba-galery-4\" class=\"long\">\r\n                                    <div class=\"flex_r\">\r\n                                        <img src=\"P&I/galery3.png\" alt=\"bamba-galery-3\">\r\n                                        <img src=\"P&I/galery5.png\" alt=\"bamba-galery-5\">\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"flex_c\">\r\n                            <img src=\"P&I/galery1.png\" alt=\"bamba-galery-1\">\r\n                            <img src=\"P&I/galery2.png\" alt=\"bamba-galery-2\" class=\"tall\">\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"flex_r\">\r\n                        <img src=\"P&I/galery5.png\" alt=\"bamba-galery-5\" class=\"long\">\r\n                        <img src=\"P&I/galery6.png\" alt=\"bamba-galery-6\" class=\"long\">\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </main>\r\n\r\n</body>\r\n</html>";
}



// :הרצת התוכנית
//1. html-שליפת קוד ה
var html = await LoadHtml("https://learn.malkabruk.co.il");
//2. בנית עץ האלמנטים שלו
HtmlElement htmlTree=HtmlElement.buildHtmlTree(html);
//3. הסלקטור שיש לחפש בעץ
string selector = "main #open_galery .flex_c .long";
//4.1 קריאה לפונקציה שמפרקת את הסלקטור לעץ סלקטורים, ומוצאת היכן מופיע בעץ האלמנטים
//4.2   פונקציה זו קוראת לפונקציות נוספות ולא מכילה את כל הקוד בעצמה
List<HtmlElement> list= htmlTree.GetElementBySelector(selector);
//5. הדפסת רשימת התוצאות
foreach (HtmlElement element in list)
    Console.WriteLine(element.ToString());