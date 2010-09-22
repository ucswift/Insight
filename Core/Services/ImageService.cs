using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using WaveTech.Insight.Model.Services;

namespace WaveTech.Insight.Services
{
	public class ImageService : IImageService
	{
		public void GenerateImage(string imagePath)
		{
			Bitmap objBmpImage = new Bitmap(1, 1);

			int intWidth = 0;
			int intHeight = 0;

			// Create the Font object for the image text drawing.
			Font objFont = new Font("Arial", 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

			// Create a graphics object to measure the text's width and height.
			Graphics objGraphics = Graphics.FromImage(objBmpImage);

			// This is where the bitmap size is determined.
			intWidth = (int)objGraphics.MeasureString("TEST", objFont).Width;
			intHeight = (int)objGraphics.MeasureString("TEST", objFont).Height;

			// Create the bmpImage again with the correct size for the text and font.
			objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));

			// Add the colors to the new bitmap.
			objGraphics = Graphics.FromImage(objBmpImage);

			// Set Background color
			objGraphics.Clear(Color.White);
			objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
			objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			objGraphics.DrawString("TEST", objFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 0);
			objGraphics.Flush();

			objBmpImage.Save(imagePath, ImageFormat.Png);
		}
	}
}