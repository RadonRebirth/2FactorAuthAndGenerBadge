using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestASPProject
{
    public partial class EmailAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSendCode_Click(object sender, EventArgs e)
        {
            string emailTo = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(emailTo))
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "Пожалуйста, введите адрес электронной почты.";
                return;
            }

            string secretCode = GenerateSecretCode(); // Generate a new secret code

            try
            {
                SmtpClient mySmtpClient = new SmtpClient("smtp.mail.ru");

                // Set smtp client parameters
                mySmtpClient.UseDefaultCredentials = true;
                mySmtpClient.EnableSsl = true;

                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("jf.com@mail.ru", "pkKjpAmnzeSLfVEt0Dmk");
                mySmtpClient.Credentials = basicAuthenticationInfo;

                // Set sender and recipient
                MailAddress from = new MailAddress("jf.com@mail.ru", "3FactorAuth");
                MailAddress to = new MailAddress(emailTo, "TestToName");
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                // Set subject and encoding
                myMail.Subject = $"Привет, вот код, который ты просил: {secretCode}";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // Set body and encoding
                myMail.Body = $"Привет, вот код, который ты просил: <b>{secretCode}</b><br>using <b>HTML</b>.";
                myMail.BodyEncoding = System.Text.Encoding.UTF8;

                // Specify if the body is HTML
                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
                Session["SecretCode"] = secretCode; // Store the secret code in a session variable

                errorTxt.ForeColor = Color.Green;
                errorTxt.Text = "Код отправлен на указанный адрес электронной почты.";
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException("SmtpException has occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string enteredCode = txtSecretCode.Text;

            if (Session["SecretCode"] != null && enteredCode == Session["SecretCode"].ToString())
            {
                errorTxt.ForeColor = Color.Green;
                errorTxt.Text = "Код верен! Переход на другую форму.";
                Response.Redirect("Baidge.aspx");
            }
            else
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "Код неверен! Пожалуйста, введите правильный код.";
            }
        }

        private string GenerateSecretCode()
        {
            Random rand = new Random();
            int i = rand.Next(100000, 999999);
            return i.ToString();
        }
    }
}
