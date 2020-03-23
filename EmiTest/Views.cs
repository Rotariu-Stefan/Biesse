using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mshtml;

namespace IExplorerControlsTest
{
    class IndexView
    {
        public static void ClickLogin(WebUtil w)
        {
            HTMLAnchorElement loginElement =  (HTMLAnchorElement)w.GetElementByValue("a", "href","/login");
            if (loginElement != null)
            {
                w.BeginPageLoading();
                loginElement.click();
                w.WaitForComplete(); //block main thread until loaded event is fired
            }
        }
    }

    class LoginView
    {
        public static void Login(WebUtil w,string username, string password)
        {
            HTMLInputElement user_input = (HTMLInputElement)w.GetElementById("username");
            HTMLInputElement password_input = (HTMLInputElement)w.GetElementById("password");
            HTMLInputElement submit_input = (HTMLInputElement)w.GetElementByValue("input", "name", "login");
            if (user_input != null && password_input != null & submit_input != null)
            {
                user_input.value = username;
                password_input.value = password;
                w.BeginPageLoading();
                submit_input.click();
                w.WaitForComplete();
            }
        }

        public static void RefactoredLogin(WebUtil w, string username, string password)
        {
            HTMLDivElement loginDiv = w.GetElementById("loginForm") as HTMLDivElement;
            if (loginDiv != null)
            {
                IHTMLElementCollection inputs = loginDiv.getElementsByTagName("input");
                foreach (IHTMLElement element in inputs)
                {
                    if (element is HTMLInputElement)
                    {
                        HTMLInputElement input = element as HTMLInputElement;
                        switch (input.name)
                        {
                            case "username":
                                input.value = username;
                                break;
                            case "password":
                                input.value = password;
                                break;
                            case "login":
                                w.submitClick(input as IHTMLElement);
                                break;
                        }
                    }
                }
            }
        }
    }

    class LoggedInView
    {
        public static void GoToProfile(WebUtil w)
        {
            
            HTMLUListElement ullist = (HTMLUListElement)w.GetUList("ul", "class", "visible-menu");
            HTMLAnchorElement anchor =  null;
            int i = 0;
            foreach (HTMLLIElement element in ullist.getElementsByTagName("li"))
            {
                if (i == 0)
                {
                    anchor = (HTMLAnchorElement)element.firstChild;
                    i += 1;
                }
            }

            w.BeginPageLoading();
            if (anchor != null)
            {
                anchor.click();
            }
            w.WaitForComplete();
        }

        public static void Logout(WebUtil w)
        {
            HTMLLIElement logoutLI = w.GetElementById("logoutLink") as HTMLLIElement;
            if (logoutLI != null)
            {
                int i = 0;
                foreach (HTMLAnchorElement element in logoutLI.getElementsByTagName("a"))
                {

                    if (i == 0 && element.className == "profile-link")
                    {
                        element.click();
                        w.BeginPageLoading();
                        w.WaitForComplete();
                        i++;
                    }
                }
            }
        }
            
    }

    public class ProfileView
    {
        public static List<IHTMLElement> getRecentTracks(WebUtil w)
        {
            List<IHTMLElement> recentTracks = new List<IHTMLElement>();
            HTMLTable tracksTable = w.getElementByClassName("table", "tracklist withimages") as HTMLTable;
            if (tracksTable != null)
            {
                foreach (IHTMLTableRow row in tracksTable.rows)
                {
                    foreach (IHTMLElement cell in row.cells)
                    {
                        if (cell.className == "subjectCell ")
                        {
                            recentTracks.Add(cell);
                        }
                    }
                }
            }
            return recentTracks;
        }

        public static List<Melodie> getRecentTracksExtracted(List<IHTMLElement> recentTracks)
        {
            List<Melodie> melodii = new List<Melodie>();
            foreach (IHTMLElement item in recentTracks)
            {
                int i = 0;
                Melodie m = new Melodie();
                string artist = "", piesa = "";

                foreach (HTMLAnchorElement anchor in item.children as IHTMLElementCollection)
                {
                    if (i == 0)
                    {
                        artist = anchor.firstChild.nodeValue.ToString();
                        i++;
                    }
                    else
                        piesa = anchor.firstChild.nodeValue.ToString();
                }
                melodii.Add(new Melodie
                {
                    Artist = artist,
                    Piesa = piesa
                });
            }
            return melodii;
        }
    }

    public class Melodie
    {
       public Melodie() {}
       public string Artist {get;set;}
       public string Piesa {get;set;} 
    }


}
