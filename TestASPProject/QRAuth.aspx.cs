using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace TestASPProject
{
    public partial class QRAuth : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnQR_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int i = rand.Next(100000, 999999);
            string qrimage = Convert.ToString(i);
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap qrcode = encoder.Encode(qrimage);

            using (MemoryStream ms = new MemoryStream())
            {
                qrcode.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                QRImage.ImageUrl = "data:image/png;base64," + base64String;
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string base64Image = QRImage.ImageUrl.Replace("data:image/png;base64,", "");
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            string tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, imageBytes);

            try
            {
                QRCodeDecoder decoder = new QRCodeDecoder();
                Bitmap bitmap = new Bitmap(tempFilePath);
                string decodedText = decoder.Decode(new QRCodeBitmapImage(bitmap));
                string secretCode = txtSecretCode.Text;

                if (secretCode == decodedText)
                {
                    errorTxt.ForeColor = Color.Green;
                    errorTxt.Text = "*Аутентификация по QR-коду успешна.";
                    Response.Redirect("Baidge.aspx");
                }
                else
                {
                    errorTxt.ForeColor = Color.Red;
                    errorTxt.Text = "*Вы ввели неверное число. Авторизируйтесь заново.";
                    GenerateAndDisplayQRCode();
                    txtSecretCode.Text = "";
                }
            }
            catch (Exception ex)
            {
               errorTxt.Text = "Ошибка при декодировании QR-кода: " + ex.Message;
            }
        }

        private void GenerateAndDisplayQRCode()
        {
            Random rand = new Random();
            int i = rand.Next(100000, 999999);
            string qrImage = Convert.ToString(i);
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap qrcode = encoder.Encode(qrImage);

            // Сохранение изображения в файл или другой источник

            string imagePath = Server.MapPath("~/qrcodes/qrcode.png");
            qrcode.Save(imagePath);

            // Отображение изображения
            QRImage.ImageUrl = "~/qrcodes/qrcode.png";
        }
    }
}
