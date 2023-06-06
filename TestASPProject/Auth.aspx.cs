using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestASPProject
{
    public partial class Auth : Page
    {
        private const string connectionString = "Data Source=|DataDirectory|\\database.sqlite;Version=3;";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "*Введите имя пользователя и пароль.";
                return;
            }

            if (Regex.IsMatch(username, @"\s+"))
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text = "*Имя пользователя не должно содержать пробелов";
                return;
            }

            if (AuthenticateUser(username, password))
            {
                errorTxt.ForeColor = Color.Green;
                errorTxt.Text = "Успешная авторизация";

                // Вставка скрипта JavaScript для отображения уведомления
                string script = @"
                function showNotification() {
                    var choice = confirm('Для продолжения выберите способ аутентификации.\nКнопка - Да, по QR коду\nКнопка - Нет, по почте');
                    if (choice) {
                        window.location.href = 'QRAuth.aspx'; // Перенаправление на Form1.aspx
                    } else {
                        window.location.href = 'EmailAuth.aspx'; // Перенаправление на VerifyForm.aspx
                    }
                }
                showNotification();";

                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "NotificationScript", script, true);
            }
            else
            {
                errorTxt.ForeColor = Color.Red;
                errorTxt.Text="*Неверное имя пользователя или пароль.";
            }

        }
        private bool AuthenticateUser(string username, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
                using (SQLiteCommand command = new SQLiteCommand(selectUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    return userCount > 0;
                }
            }
        }
    }
}