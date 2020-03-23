using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mshtml;
using System.Threading;
using System.Collections;
using System.Windows.Forms;

namespace IExplorerControlsTest
{
    public partial class WebUtil
    {
        public IHTMLElement GetElementById(string elementId)
        {
            HTMLDocument document = ((HTMLDocument)IE.Document); //cast the newly process dom to HtmlDoc
            
            IHTMLElement element = document.getElementById(elementId);
            int nullElementCount = 0;
            // The following loop is to account for any latency that IE
            // might experience.  Tweak the number of times to attempt
            // to continue checking the document before giving up.
            while (element == null && nullElementCount < 10)
            {
                Thread.Sleep(500);
                element = document.getElementById(elementId);
                nullElementCount++;
            }
            return element;
        }

        /// <summary>
        /// a class="primary-nav-link"
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="attName"></param>
        /// <param name="attValue"></param>
        /// <returns></returns>

        public IHTMLElement GetElementByTAV(string tagName, string attName, string attValue)
        {

            HTMLDocument document = ((HTMLDocument)IE.Document);
            IHTMLElement el = null;

            IHTMLElementCollection tags = document.getElementsByTagName(tagName);

            IEnumerator enumerator = tags.GetEnumerator();

            while (enumerator.MoveNext())
            {
                IHTMLElement element = (IHTMLElement)enumerator.Current;
                if (element.getAttribute(attName, 0).ToString().Contains(attValue))
                {
                    return element;
                }
            }
            if (el == null)
            {

                IHTMLFramesCollection2 frames = (IHTMLFramesCollection2)document.frames;
                try
                {
                    object o = null;
                    for (int i = 0; i < frames.length; i++)
                    {
                        o = i;
                        IHTMLWindow2 fr = (IHTMLWindow2)frames.item(ref o);
                        HTMLDocument doc = (HTMLDocument)fr.document;
                        tags = doc.getElementsByTagName(tagName);

                        enumerator = tags.GetEnumerator();

                        while (enumerator.MoveNext())
                        {
                            IHTMLElement element = (IHTMLElement)enumerator.Current;
                            
                            bool go = false;
                            try
                            {
                                go = element.getAttribute(attName, 0).ToString().Contains(attValue);

                            }
                            catch { }
                            if (go)
                            {
                                return element;
                            }

                            //if (element.innerText == elementValue)
                            //{
                            //    return element;
                            //}
                        }
                    }

                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                }
            }
            return null;
        }

        public IHTMLElement getElementByClassName(string tagName, string className)
        {
            HTMLDocument document = ((HTMLDocument)IE.Document);
            IHTMLElement el = null;

            IHTMLElementCollection tags = document.getElementsByTagName(tagName);

            IEnumerator enumerator = tags.GetEnumerator();

            while (enumerator.MoveNext())
            {
                IHTMLElement element = (IHTMLElement)enumerator.Current;
                if (element.className == className)
                {
                    return element;
                }
            }
            return null;    
        }
            
        public HTMLUListElement GetUList(string tagName, string attName, string attValue)
        {
            HTMLDocument document = ((HTMLDocument)IE.Document);
            IHTMLElement el = null;

            IHTMLElementCollection tags = document.getElementsByTagName(tagName);

            IEnumerator enumerator = tags.GetEnumerator();

            while (enumerator.MoveNext())
            {
                HTMLUListElement element = (HTMLUListElement)enumerator.Current;
                if (element.getAttributeNode(attName) != null ) // are class 
                {
                    if (element.getAttributeNode(attName).nodeValue != null &&
                        element.getAttributeNode(attName).nodeValue.ToString() == attValue)
                    {
                        ///MessageBox.Show("found the element");
                        return element;
                    }
                }
            }
            return null;
        }

        public IHTMLElement GetElementByValue(string tagName, string attName, string attValue)
        {
            int nullElementCount = 0;
            IHTMLElement element = GetElementByValueOnce(tagName, attName, attValue);
            while (element == null && nullElementCount < 10)
            {
                Thread.Sleep(500);
                element = GetElementByValueOnce(tagName, attName, attValue);
                nullElementCount++;
            }

            return element;
        }

        private IHTMLElement GetElementByValueOnce(string tagName, string attName, string attValue)
        {

            HTMLDocument document = ((HTMLDocument)IE.Document);
            IHTMLElement el = null;

            IHTMLElementCollection tags = document.getElementsByTagName(tagName);

            IEnumerator enumerator = tags.GetEnumerator();

            while (enumerator.MoveNext())
            {
                IHTMLElement element = (IHTMLElement)enumerator.Current;
                if (element.getAttribute(attName, 0).ToString().Contains(attValue))
                {
                    return element;
                }
            }
            if (el == null)
            {

                IHTMLFramesCollection2 frames = (IHTMLFramesCollection2)document.frames;
                try
                {
                    object o = null;
                    for (int i = 0; i < frames.length; i++)
                    {
                        o = i;
                        IHTMLWindow2 fr = (IHTMLWindow2)frames.item(ref o);
                        HTMLDocument doc = (HTMLDocument)fr.document;
                        tags = doc.getElementsByTagName(tagName);

                        enumerator = tags.GetEnumerator();

                        while (enumerator.MoveNext())
                        {
                            IHTMLElement element = (IHTMLElement)enumerator.Current;
                            bool go = false;
                            try
                            {
                                go = element.getAttribute(attName, 0).ToString().Contains(attValue);

                            }
                            catch { }
                            if (go)
                            {
                                return element;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                }
            }

            return null;
        }
        
        public void submitClick(IHTMLElement submitElement)
        {
            this.BeginPageLoading();
            submitElement.click();
            this.WaitForComplete();
        }
    
    }
}
