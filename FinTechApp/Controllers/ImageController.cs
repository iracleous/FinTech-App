
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;


namespace FinTechApp.Controllers;

public class ImageController : Controller
{
   

    // Install-Package SkiaSharp
    public IActionResult GenerateImageSkia()
    {
        // Create a bitmap object
        using SKBitmap bitmap = new(450, 100);
        // Create a canvas from the bitmap
        using SKCanvas canvas = new(bitmap);
        // Clear the canvas with white color
        canvas.Clear(SKColors.White);
        // Draw a red rectangle on the canvas
        using SKPaint paint = new();
        paint.Color = SKColors.Red;
        canvas.DrawRect(new SKRect(10, 10, 420, 90), paint);
        // Draw text on the canvas
        using SKPaint textPaint = new();
        textPaint.Color = SKColors.White;
        textPaint.TextSize = 26;
        canvas.DrawText("Universal NBG image", 20, 50, textPaint);
        // Encode the bitmap to a PNG image
        using SKImage image = SKImage.FromBitmap(bitmap);
        using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
        // Get the bytes of the encoded image
        byte[] bytes = data.ToArray();
        // Return the image bytes as a file
        return File(bytes, "image/png");
    }


}
