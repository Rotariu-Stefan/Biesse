using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using InserimentoNoteGEISP.Utils;
namespace InserimentoNoteGEISP
{
    public enum LogType
    {
        Error, Operation, File, Bad_Image
    }
    public static class Logger
    {
        #region Fields

        private static string _loggingDirectory = ConfigUtils.GetLogPath() + "logger";

        #endregion

        #region Methods
        /// <summary>
        /// create a log file in the Log folder
        /// if the parameter isError is true then append the message to a error file eles to a operation file
        /// the created file contains today date
        /// </summary>
        /// <param name="message">the message that will appearin the log   </param>
        /// <param name="isError">the flag depending the log will be error or operation</param>
        public static void Log(string message, LogType logtype)
        {
            try
            {
                string today = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
                if (!Directory.Exists(_loggingDirectory + today))
                    Directory.CreateDirectory(_loggingDirectory + today);
                if (!Directory.Exists(_loggingDirectory + today))
                    Directory.CreateDirectory(_loggingDirectory + today);

                switch (logtype)
                {
                    case LogType.Error:
                        string errorFile = "errors.txt";
                        StreamWriter sw = new StreamWriter(_loggingDirectory + today + "\\" + errorFile, true);
                        string ora = DateTime.Now.Hour.ToString();
                        if (ora.Length == 1)
                            ora = "0" + ora;
                        string minut = DateTime.Now.Minute.ToString();
                        if (minut.Length == 1)
                            minut = "0" + minut;
                        string secunda = DateTime.Now.Second.ToString();
                        if (secunda.Length == 1)
                            secunda = "0" + secunda;
                        string time = string.Format("[{0}:{1}:{2}] ", ora, minut, secunda);
                        sw.WriteLine(time + message);
                        sw.Close();
                        //MailUtils.Send(message);
                        break;
                    case LogType.Operation:
                        errorFile = "operations.txt";
                        sw = new StreamWriter(_loggingDirectory + today + "\\" + errorFile, true);
                        ora = DateTime.Now.Hour.ToString();
                        if (ora.Length == 1)
                            ora = "0" + ora;
                        minut = DateTime.Now.Minute.ToString();
                        if (minut.Length == 1)
                            minut = "0" + minut;
                        secunda = DateTime.Now.Second.ToString();
                        if (secunda.Length == 1)
                            secunda = "0" + secunda;
                        time = string.Format("[{0}:{1}:{2}] ", ora, minut, secunda);
                        sw.WriteLine(time + message);
                        sw.Close();
                        break;
                    case LogType.File:
                        errorFile = "files.txt";
                        sw = new StreamWriter(_loggingDirectory + today + "\\" + errorFile, true);
                        ora = DateTime.Now.Hour.ToString();
                        if (ora.Length == 1)
                            ora = "0" + ora;
                        minut = DateTime.Now.Minute.ToString();
                        if (minut.Length == 1)
                            minut = "0" + minut;
                        secunda = DateTime.Now.Second.ToString();
                        if (secunda.Length == 1)
                            secunda = "0" + secunda;
                        time = string.Format("[{0}:{1}:{2}] ", ora, minut, secunda);
                        sw.WriteLine(time + message);
                        sw.Close();
                        break;
                    case LogType.Bad_Image:
                        errorFile = "immagini_sbagliati.txt";
                        sw = new StreamWriter(_loggingDirectory + today + "\\" + errorFile, true);
                        ora = DateTime.Now.Hour.ToString();
                        if (ora.Length == 1)
                            ora = "0" + ora;
                        minut = DateTime.Now.Minute.ToString();
                        if (minut.Length == 1)
                            minut = "0" + minut;
                        secunda = DateTime.Now.Second.ToString();
                        if (secunda.Length == 1)
                            secunda = "0" + secunda;
                        time = string.Format("[{0}:{1}:{2}] ", ora, minut, secunda);
                        sw.WriteLine(time + message);
                        sw.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message + "\n" + ex.StackTrace;
            }
        }
        /// <summary>
        /// write a log about current processed  excel  file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="message"></param>
        public static void LogExcel(string path, string message)
        {
            try
            {
                string today = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
                if (!Directory.Exists(_loggingDirectory + today))
                    Directory.CreateDirectory(_loggingDirectory + today);
                string errorFile = path + "_stato.txt";
                errorFile = errorFile.Contains("\\") ? errorFile.Substring(errorFile.LastIndexOf("\\") + 1) : errorFile;
                StreamWriter sw = new StreamWriter(_loggingDirectory + today + "\\" + errorFile, true);
                string ora = DateTime.Now.Hour.ToString();
                if (ora.Length == 1)
                    ora = "0" + ora;
                string minut = DateTime.Now.Minute.ToString();
                if (minut.Length == 1)
                    minut = "0" + minut;
                string secunda = DateTime.Now.Second.ToString();
                if (secunda.Length == 1)
                    secunda = "0" + secunda;
                string time = string.Format("[{0}:{1}:{2}] ", ora, minut, secunda);
                sw.WriteLine(time + message);
                sw.Close();
            }
            catch (Exception ex)
            {
                string err = ex.Message + "\n" + ex.StackTrace;
            }
        }

        #endregion
    }
}
