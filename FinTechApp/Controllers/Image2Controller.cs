using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace FinTechApp.Controllers;

public class Image2Controller : Controller
{
    public ActionResult GenerateImage()
    {
        // Create a bitmap object
        Bitmap bitmap = new (300, 300);

        // Get Graphics object from bitmap
        using Graphics graphics = Graphics.FromImage(bitmap);
       
            // Clear the bitmap with white color
        graphics.Clear(Color.White);

        // Draw a red rectangle on the bitmap
        //graphics.FillRectangle(Brushes.Red, 10, 10, 180, 80);


        // Create pen.
        Pen blackPen = new Pen(Color.Black, 1);

        // Create rectangle.
        Rectangle rect = new Rectangle(10, 10, 180, 80);

        // Draw rectangle to screen.
        graphics.DrawRectangle(blackPen, rect);



        // Draw text on the bitmap
        graphics.DrawString("NBG 2024", new Font("Arial", 12), Brushes.Red, 20, 40);
        

        // Save the bitmap to a memory stream in JPEG format
        MemoryStream ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Jpeg);

        // Set the position of the stream to the beginning
        ms.Seek(0, SeekOrigin.Begin);

        // Return the file as a FileStreamResult
        return new FileStreamResult(ms, "image/jpeg");
    }
}
