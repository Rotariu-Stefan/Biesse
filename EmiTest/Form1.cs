using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SHDocVw;
using System.Threading;
using System.Diagnostics;
using mshtml;



/*
    <input name="someName" type="text" value="somevalue">
 *  <input name = "someName" type = "submit">
 *   2 inputuri cu acelasi nume pot fi diferentiate de form (parinte)
 *   <button type= "submit">
 *  var element = document.getElementById("cat"); //contine nested elements
      var sub = element.getElementsByName("sub");
 * 
 * <script type="text/javascript">
   function show_sub(cat) {
      cat.getElementsByTagName("ul")[0].style.display = (cat.getElementsByTagName("ul")[0].style.display == "none") ? "inline" : "none";
   }
</script>
 */
namespace IExplorerControlsTest
{
    public partial class Form1 : Form
    {
        bool isDocumentComplete;
        int TimeoutSeconds = 30;
        WebUtil w;
        string url = "http://www.youtube.com";
       
        #region credentials
        string username = "";//your account
        string password = ""; //ur account password
        #endregion

        public Form1()
        {
            InitializeComponent();
            this.sites.SelectedIndex = 0;
        }

        private void IE_DocumentComplete(object pDisp, ref object URL)
        {
            isDocumentComplete = true;

        }

        public void WaitForComplete()
        {
            int elapsedSeconds = 0;
            while (!isDocumentComplete && elapsedSeconds != TimeoutSeconds)
            {
                Thread.Sleep(1000);
                elapsedSeconds++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            w.ClosePage();
        }

        //  go to Selected Website
        private void button2_Click(object sender, EventArgs e)
        {
            w = new WebUtil();
            w.Navigate(this.sites.SelectedItem.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HTMLDocument document = ((HTMLDocument)w.IE.Document);
            //var x = document.all.tags("head").Item(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HTMLDocument document = ((HTMLDocument)w.IE.Document);

            HTMLInputElement otxtSearchBox = (HTMLInputElement)document.getElementById("masthead-search-term");
            otxtSearchBox.value = this.query.Text;

            IHTMLElement searchButton = (IHTMLElement)document.getElementById("search-btn");
            searchButton.click();
        }

        // 
        private void button5_Click(object sender, EventArgs e)
        {
            HTMLAnchorElement musicAnchor = (HTMLAnchorElement)w.GetElementByTAV("a", "href", "/music");
            w.BeginPageLoading();
            musicAnchor.click();
            w.WaitForComplete();
        }

        private void IndexLogin_Click(object sender, EventArgs e)
        {
            IndexView.ClickLogin(w);
        }

        private void back_Click(object sender, EventArgs e)
        {
            w.IE.GoBack();
        }

        private void forward_Click(object sender, EventArgs e)
        {
            w.IE.GoForward();
        }

        //GO TO INDEX login view enter credentials and login
        private void BeginAutomation_Click(object sender, EventArgs e)
        {
            w = new WebUtil();
            w.Navigate(this.sites.SelectedItem.ToString());
            IndexView.ClickLogin(w);
            LoginView.RefactoredLogin(w, username, password);
            LoggedInView.GoToProfile(w);
            foreach (var melodie in ProfileView.getRecentTracksExtracted(ProfileView.getRecentTracks(w)))
                lb.Items.Add(melodie.Artist.ToString() + " - " + melodie.Piesa.ToString());

            IOExcel.write(ProfileView.getRecentTracksExtracted(ProfileView.getRecentTracks(w)));
            
            LoggedInView.Logout(w);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            w = new WebUtil();
            w.Navigate("http://www.last.fm/user/houdinn");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            w = new WebUtil();
            w.BeginPageLoading();
            w.Navigate("https://www.last.fm/login");
            w.WaitForComplete();
            LoginView.RefactoredLogin(w, username, password);
        }
    }
}
