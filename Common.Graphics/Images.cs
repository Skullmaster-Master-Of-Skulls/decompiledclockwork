using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TechnoPro.Common.Graphics
{
	// Token: 0x02000002 RID: 2
	public static class Images
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static Image ResizeImageKeepAspectRatio(this Image imgToResize, Size size)
		{
			if (imgToResize == null)
			{
				return null;
			}
			int width = imgToResize.Width;
			int height = imgToResize.Height;
			float num = (float)size.Width / (float)width;
			float num2 = (float)size.Height / (float)height;
			float num3 = (num2 < num) ? num2 : num;
			int width2 = (int)((float)width * num3);
			int height2 = (int)((float)height * num3);
			Bitmap bitmap = new Bitmap(width2, height2);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.DrawImage(imgToResize, 0, 0, width2, height2);
			}
			return bitmap;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002118 File Offset: 0x00000318
		public static void SaveAsJpeg(this Image image, Stream outputStream, int quality = 100)
		{
			ImageCodecInfo encoder = ImageCodecInfo.GetImageEncoders().First((ImageCodecInfo codecInfo) => codecInfo.MimeType == "image/jpeg");
			using (EncoderParameters encoderParameters = new EncoderParameters(1))
			{
				encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
				image.Save(outputStream, encoder, encoderParameters);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002190 File Offset: 0x00000390
		public static Size ResizeKeepAspectRatio(this Size SizeToResize, Size TargetSize)
		{
			int width = SizeToResize.Width;
			int height = SizeToResize.Height;
			float num = (float)TargetSize.Width / (float)width;
			float num2 = (float)TargetSize.Height / (float)height;
			float num3;
			if (num2 < num)
			{
				num3 = num2;
			}
			else
			{
				num3 = num;
			}
			int width2 = (int)((float)width * num3);
			int height2 = (int)((float)height * num3);
			return new Size(width2, height2);
		}
	}
}
