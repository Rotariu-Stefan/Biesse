using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SHDocVw;
using System.Diagnostics;
using System.Windows.Forms;
using mshtml;

namespace IExplorerControlsTest
{
    public partial class WebUtil
    {
        #region Fields

        private bool isDocumentComplete = false;
        private InternetExplorer _IE;
        private int _timeoutSeconds = 30;
        private Process m_proc = null; //InternetExplorer process
        private ShellWindows m_IEFoundBrowsers;
        #endregion

        #region Properties

        //timeout for loading the document
        public int TimeoutSeconds
        {
            get { return _timeoutSeconds; }
        }

        /// <summary>
        /// instance of ShDocVw.InternetExplorer instanciated in ctor 
        /// </summary>
        public InternetExplorer IE
        {
            get { return _IE; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a new instance of IExplorer with Process.start()
        /// Store the handle of the new process
        /// </summary>
        public WebUtil()
        {
             m_IEFoundBrowsers = new ShellWindows(); //all shells
            int explorerInstances = m_IEFoundBrowsers.Count;
            //open a new instance of IExplore, the nomerge attribute is for generate a new HWND for the new instance,
            //other way the new instance will be hooked by the last instance
            m_proc = Process.Start("IExplore.exe", "-nomerge about:blank");
            while (explorerInstances == m_IEFoundBrowsers.Count)
            {
                ///keep program busy till the windows shell get the new instance 
            }
            SetIEHandle();
            AtachEventHls();
        }

        private void AtachEventHls()
        {
            IE.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(IE_DocumentComplete);
            IE.NavigateError += new DWebBrowserEvents2_NavigateErrorEventHandler(IE_NavigateError);
        }

        /// <summary>
        ///  Iterate through the collection of procesess handled by ShellWindows
        ///  Match the one of the newly created process
        /// </summary>
        private void SetIEHandle()
        {
            foreach (InternetExplorer Browser in m_IEFoundBrowsers)
            {
                try
                {
                    if (Browser.HWND == (int)m_proc.MainWindowHandle)
                    {
                        _IE = Browser;
                        Debug.WriteLine(_IE.HWND);
                        _IE.Left = 600; //distance from left
                        _IE.Top = 0; //distance from top
                       // _IE.FullScreen = true;
                        break;
                    }
                }
                catch
                {
                }
            }
        }
        
        private void IE_DocumentComplete(object pDisp, ref object URL)
        {
            isDocumentComplete = true;
        }

        private void IE_NavigateError(object pDisp, ref object URL, ref object Frame, ref object StatusCode, ref bool Cancel)
        {
            MessageBox.Show("An error occured . Event Navigate Error was raised");
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

        public void Navigate(string url)
        {
            isDocumentComplete = false;
            object empty = "";
            IE.Navigate(url, ref empty, ref empty, ref empty, ref empty);
            WaitForComplete();
        }

        public void BeginPageLoading()
        {
            isDocumentComplete = false;
        }

        public void ClosePage()
        {
            try
            {
                if (m_proc != null)
                {
                    //m_proc.Kill();
                    IE.Quit();
                }
            }
            catch (Exception e) { }
        }

       

    #endregion
    }
}