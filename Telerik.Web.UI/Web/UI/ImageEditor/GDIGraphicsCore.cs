using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EA4 RID: 3748
	public class GDIGraphicsCore : IGraphicsCore
	{
		// Token: 0x06008F02 RID: 36610 RVA: 0x00203270 File Offset: 0x00201470
		public virtual Image ChangeOpacity(Image original, double opacity)
		{
			Bitmap bitmap = new Bitmap(original.Width, original.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				ColorMatrix colorMatrix = new ColorMatrix();
				colorMatrix.Matrix33 = (float)opacity;
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
				graphics.DrawImage(original, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, imageAttributes);
			}
			return bitmap;
		}

		// Token: 0x06008F03 RID: 36611 RVA: 0x002032FC File Offset: 0x002014FC
		public virtual Image Resize(Image original, Size size)
		{
			return this.Resize(original, size, InterpolationMode.HighQualityBicubic);
		}

		// Token: 0x06008F04 RID: 36612 RVA: 0x00203308 File Offset: 0x00201508
		public virtual Image Resize(Image originalImg, Size newSize, InterpolationMode intMode)
		{
			int width = newSize.Width;
			int height = newSize.Height;
			if (width > 0 && height > 0 && (originalImg.Width != width || originalImg.Height != height))
			{
				Bitmap bitmap = EditableImage.CheckPixelFormat(originalImg) ? new Bitmap(width, height, originalImg.PixelFormat) : new Bitmap(width, height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.InterpolationMode = intMode;
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					if (intMode != InterpolationMode.NearestNeighbor)
					{
						RectangleF rect = new RectangleF(-0.5f, -0.5f, (float)(newSize.Width + 1), (float)(newSize.Height + 1));
						graphics.DrawImage(originalImg, rect);
					}
					else
					{
						graphics.DrawImage(originalImg, new Rectangle(new Point(0, 0), newSize), new Rectangle(new Point(0, 0), originalImg.Size), GraphicsUnit.Pixel);
					}
					originalImg = bitmap;
				}
				return bitmap;
			}
			return new Bitmap(originalImg);
		}

		// Token: 0x06008F05 RID: 36613 RVA: 0x00203408 File Offset: 0x00201608
		public virtual Image Flip(Image original, FlipDirection direction)
		{
			return this.RotateFlip(original, (direction == FlipDirection.Horizontal) ? RotateFlipType.RotateNoneFlipX : ((direction == FlipDirection.Vertical) ? RotateFlipType.Rotate180FlipX : RotateFlipType.Rotate180FlipNone));
		}

		// Token: 0x06008F06 RID: 36614 RVA: 0x0020341F File Offset: 0x0020161F
		public virtual Image Rotate(Image original, Rotation rotate)
		{
			return this.RotateFlip(original, (rotate == Rotation.Rotate90) ? RotateFlipType.Rotate90FlipNone : ((rotate == Rotation.Rotate180) ? RotateFlipType.Rotate180FlipNone : RotateFlipType.Rotate270FlipNone));
		}

		// Token: 0x06008F07 RID: 36615 RVA: 0x00203438 File Offset: 0x00201638
		private Image RotateFlip(Image original, RotateFlipType type)
		{
			Bitmap bitmap = new Bitmap(original);
			bitmap.RotateFlip(type);
			return bitmap;
		}

		// Token: 0x06008F08 RID: 36616 RVA: 0x00203454 File Offset: 0x00201654
		public virtual Image Crop(Image original, Rectangle rectangle)
		{
			rectangle.Width = Math.Min(original.Width, rectangle.Width);
			rectangle.Height = Math.Min(original.Height, rectangle.Height);
			Bitmap bitmap = EditableImage.CheckPixelFormat(original) ? new Bitmap(rectangle.Width, rectangle.Height, original.PixelFormat) : new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppPArgb);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.DrawImage(original, new Rectangle(0, 0, rectangle.Width, rectangle.Height), rectangle, GraphicsUnit.Pixel);
			}
			return bitmap;
		}

		// Token: 0x06008F09 RID: 36617 RVA: 0x00203528 File Offset: 0x00201728
		public virtual Image AddText(Image original, Point position, ImageText text)
		{
			Bitmap bitmap = new Bitmap(original);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
				graphics.DrawString(text.Value, new Font(text.FontFamily, text.Size, GraphicsUnit.Pixel), new SolidBrush(ColorTranslator.FromHtml(text.Color)), position);
			}
			return bitmap;
		}

		// Token: 0x06008F0A RID: 36618 RVA: 0x0020359C File Offset: 0x0020179C
		public virtual Image InsertImage(Image original, Point position, Image imageToInsert)
		{
			Image result;
			using (Bitmap bitmap = new Bitmap(original))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.CompositingQuality = CompositingQuality.HighQuality;
					graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
					using (Image image = new Bitmap(imageToInsert))
					{
						graphics.DrawImage(image, new Rectangle(position.X, position.Y, image.Width, image.Height));
					}
					Image image2 = Image.FromHbitmap(bitmap.GetHbitmap());
					result = image2;
				}
			}
			return result;
		}

		// Token: 0x06008F0B RID: 36619 RVA: 0x00203664 File Offset: 0x00201864
		public virtual Image ConvertTo(Image original, EditableFormat format)
		{
			Image image = original;
			if (format == EditableFormat.Gif)
			{
				OctreeQuantizer octreeQuantizer = new OctreeQuantizer(255, 8);
				image = octreeQuantizer.Quantize(original);
			}
			MemoryStream stream = new MemoryStream();
			image.Save(stream, (format == EditableFormat.Png) ? ImageFormat.Png : ((format == EditableFormat.Jpg) ? ImageFormat.Jpeg : ((format == EditableFormat.Gif) ? ImageFormat.Gif : ((format == EditableFormat.Bmp) ? ImageFormat.Bmp : original.RawFormat))));
			return new Bitmap(stream);
		}
	}
}
