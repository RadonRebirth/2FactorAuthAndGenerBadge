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
    public partial class _Default : Page
    {
        private const string connectionString = "Data Source=|DataDirectory|\\database.sqlite;Version=3;";


        protected void Page_Load(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (Username VARCHAR(50) PRIMARY KEY, Password VARCHAR(50))";
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string confirmPassword = txtPasswordVF.Text;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
                {
                    errorLabel.ForeColor = Color.Red;
                    errorLabel.Text = "*Введите имя пользователя и пароль.";
                    return;
                }

                if (password != confirmPassword)
                {
                    errorLabel.ForeColor = Color.Red;
                    errorLabel.Text = "*Пароли не совпадают.";
                    return;
                }

                // Проверка наличия пользователя с таким же именем
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                using (SQLiteCommand checkUserCommand = new SQLiteCommand(checkUserQuery, connection))
                {
                    checkUserCommand.Parameters.AddWithValue("@username", username);
                    int userCount = Convert.ToInt32(checkUserCommand.ExecuteScalar());

                    if (userCount > 0)
                    {
                        errorLabel.ForeColor = Color.Red;
                        errorLabel.Text = "*Пользователь с таким именем уже существует.";
                        return;
                    }
                }

                if (Regex.IsMatch(username, @"\s+"))
                {
                    errorLabel.ForeColor = Color.Red;
                    errorLabel.Text = "*Имя пользователя не должно содержать пробелов";
                    return;
                }

                string insertUserQuery = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                using (SQLiteCommand command = new SQLiteCommand(insertUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    try
                    {
                        command.ExecuteNonQuery();
                        errorLabel.ForeColor = Color.Green;
                        errorLabel.Text = "*Регистрация прошла успешно.";
                        Response.Redirect("Auth.aspx");

                    }
                    catch (SQLiteException ex)
                    {
                        errorLabel.ForeColor = Color.Red;
                        errorLabel.Text = "Ошибка регистрации: " + ex.Message;
                    }
                }
            }
        }
    }
}