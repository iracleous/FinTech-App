using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace FinTechApp.Controllers;

public class Image2Controller : Controller
{
    public ActionResult GenerateImage()
    {
        // Create a bitmap object
        Bitmap bitmap = new Bitmap(200, 100);

        // Get Graphics object from bitmap
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            // Clear the bitmap with white color
            graphics.Clear(Color.White);

            // Draw a red rectangle on the bitmap
            graphics.FillRectangle(Brushes.Red, 10, 10, 180, 80);

            // Draw text on the bitmap
            graphics.DrawString("Dynamic Image", new Font("Arial", 12), Brushes.White, 20, 40);
        }

        // Save the bitmap to a memory stream in JPEG format
        MemoryStream ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Jpeg);

        // Set the position of the stream to the beginning
        ms.Seek(0, SeekOrigin.Begin);

        // Return the file as a FileStreamResult
        return new FileStreamResult(ms, "image/jpeg");
    }
}
