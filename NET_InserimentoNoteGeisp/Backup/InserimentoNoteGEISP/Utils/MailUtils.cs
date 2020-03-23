using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.ComponentModel;
using System.Net.Mime;
using System.IO;
using CFG = System.Configuration;
using InserimentoNoteGEISP;
using InserimentoNoteGEISP.Utils;

namespace InserimentoNoteGEISP.Utils
{
    /// <summary>
    /// Send mails utility class
    /// </summary>
    public static class MailUtils
    {

        /// <summary>
        /// send a new mail 
        /// </summary>
        /// <param name="body"> the body of mail</param>
        public static void Send(string body, string subject, List<string> filePaths,string ccMailAdress, string mailAddress)
        {
            string mailAddreses = mailAddress;
            string[] mails = mailAddreses.Split(new char[] { ';' });
            string[] ccMails = ccMailAdress.Split(new char[] { ';' });
            if (string.IsNullOrEmpty(mailAddreses))
            {
                Logger.Log("E-mail avrebbe dovuto inviare, nessun indirizzo e-mail definito.", LogType.Error);
                return;
            }
            var message = new MailMessage()
            {
                From = new MailAddress(ConfigUtils.GetSenderAddress(), ConfigUtils.GetSenderAddress()),

                Subject = subject,
                Body = body,
                IsBodyHtml = true

            };

            foreach (string filePath in filePaths)
            {
                try
                {
                    message.Attachments.Add(new Attachment(filePath, MediaTypeNames.Application.Octet));
                }
                catch { }
            }
            for (int i = 0; i < mails.Length; i++)
            {
                if (mails[i] != string.Empty)
                    message.To.Add(new MailAddress(mails[i], mails[i]));
            }
            for (int i = 0; i < ccMails.Length; i++)
            {
                if (ccMails[i] != string.Empty)
                    message.CC.Add(new MailAddress(ccMails[i], ccMails[i]));
            }
            
            try
            {
                var client = new SmtpClient();
                client.EnableSsl = ConfigUtils.GetServerSecurity();
                client.Port = int.Parse(ConfigUtils.GetServerPort());
                client.Host = ConfigUtils.GetMailServer();
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(ConfigUtils.GetSenderAddress(), ConfigUtils.GetSenderPassword());
                client.UseDefaultCredentials = false;
                client.Credentials = SMTPUserInfo;
                client.Timeout = 3000000;
                client.SendCompleted += new
        SendCompletedEventHandler(SendCompletedCallback);
                client.Send(message);
                Logger.Log("Sent mail to " + mailAddreses +" and cc:" + ccMailAdress , LogType.Operation);
                try
                {
                    message.Dispose();
                }
                catch { }
            }
            catch (Exception ex)
            {
                Logger.Log("Error sending mail: " + ex.Message + "\n" + ex.StackTrace, LogType.Error);
            }

            try
            {
                message.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// Sendcompleted callback 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Logger.Log("Mail sending canceled", LogType.Error);
            }
            if (e.Error != null)
            {
                Logger.Log("Error while sending mail: " + token + "  " + e.Error.ToString(), LogType.Error);
            }
            else
            {
                Logger.Log("Message sent, " + token, LogType.Error);
            }
        }

    }
}
