using System;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;

namespace TestASPProject
{
    public partial class Baidge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerateBadge_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text;
            string about = txtAbout.Text;
            string url = txtLink.Text;

            // Загрузка изображения
            string fileName = "";
            if (fileUpload.HasFile)
            {
                fileName = Server.MapPath("~/Images/" + Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName));
                fileUpload.SaveAs(fileName);
            }

            // Генерация QR-кода и сохранение его в файле
            QRCodeEncoder qrEncoder = new QRCodeEncoder();
            qrEncoder.QRCodeVersion = 7;
            qrEncoder.QRCodeScale = 3;
            Bitmap qrCodeImage = qrEncoder.Encode(url);

            // Создание и сохранение бейджика с данными и изображением
            string badgeFileName = Server.MapPath("~/Badges/" + Guid.NewGuid().ToString() + ".png");
            Bitmap badge = new Bitmap(400, 300);
            Graphics graphics = Graphics.FromImage(badge);
            graphics.FillRectangle(Brushes.White, 0, 0, badge.Width, badge.Height);

            if (!string.IsNullOrEmpty(fileName))
            {
                using (System.Drawing.Image userImage = System.Drawing.Image.FromFile(fileName))
                {
                    graphics.DrawImage(userImage, new Rectangle(80, 10, 120, 140));
                }
            }

            graphics.DrawImage(qrCodeImage, 210, 10);

            // Определение шрифта и рамки для текста
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font textFont = new Font("Arial", 12);
            RectangleF titleRect = new RectangleF(10, 160, 380, 30);
            RectangleF aboutRect = new RectangleF(10, 210, 380, 100);

            // Отрисовка текста с выравниванием по левому краю
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;

            // Отрисовка заголовка
            graphics.DrawString("Представитель:", titleFont, Brushes.Black, titleRect, format);

            // Отрисовка имени пользователя
            graphics.DrawString(fullName, textFont, Brushes.Black, titleRect.X, titleRect.Y + 25, format);

            // Отрисовка обо мне
            graphics.DrawString("Обо мне:", titleFont, Brushes.Black, aboutRect, format);
            graphics.DrawString(about, textFont, Brushes.Black, aboutRect.X, aboutRect.Y + 25, format);

            badge.Save(badgeFileName);

            // Отображение сгенерированного бейджика
            imgBadge.Visible = true;
            imgBadge.ImageUrl = "~/Badges/" + Path.GetFileName(badgeFileName);
            imgBadge.Attributes.Add("onclick", "window.open(this.src,'_blank');");
        }

        protected void txtLink_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
