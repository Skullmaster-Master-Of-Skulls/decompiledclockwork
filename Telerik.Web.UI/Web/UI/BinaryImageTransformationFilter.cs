using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Telerik.Web.UI
{
	// Token: 0x020016B5 RID: 5813
	public class BinaryImageTransformationFilter : BinaryImageFilter
	{
		// Token: 0x0600E049 RID: 57417 RVA: 0x0031E228 File Offset: 0x0031C428
		public BinaryImageTransformationFilter()
		{
			this.InterpolationMode = InterpolationMode.HighQualityBicubic;
			this.Mode = BinaryImageResizeMode.Fit;
		}

		// Token: 0x170044BA RID: 17594
		// (get) Token: 0x0600E04A RID: 57418 RVA: 0x0031E23E File Offset: 0x0031C43E
		public override string Name
		{
			get
			{
				return "BinaryImageTransformationFilter";
			}
		}

		// Token: 0x170044BB RID: 17595
		// (get) Token: 0x0600E04B RID: 57419 RVA: 0x0031E245 File Offset: 0x0031C445
		// (set) Token: 0x0600E04C RID: 57420 RVA: 0x0031E24D File Offset: 0x0031C44D
		public int Width { get; set; }

		// Token: 0x170044BC RID: 17596
		// (get) Token: 0x0600E04D RID: 57421 RVA: 0x0031E256 File Offset: 0x0031C456
		// (set) Token: 0x0600E04E RID: 57422 RVA: 0x0031E25E File Offset: 0x0031C45E
		public int Height { get; set; }

		// Token: 0x170044BD RID: 17597
		// (get) Token: 0x0600E04F RID: 57423 RVA: 0x0031E267 File Offset: 0x0031C467
		// (set) Token: 0x0600E050 RID: 57424 RVA: 0x0031E26F File Offset: 0x0031C46F
		public InterpolationMode InterpolationMode { get; set; }

		// Token: 0x170044BE RID: 17598
		// (get) Token: 0x0600E051 RID: 57425 RVA: 0x0031E278 File Offset: 0x0031C478
		// (set) Token: 0x0600E052 RID: 57426 RVA: 0x0031E280 File Offset: 0x0031C480
		public BinaryImageResizeMode Mode { get; set; }

		// Token: 0x170044BF RID: 17599
		// (get) Token: 0x0600E053 RID: 57427 RVA: 0x0031E289 File Offset: 0x0031C489
		// (set) Token: 0x0600E054 RID: 57428 RVA: 0x0031E291 File Offset: 0x0031C491
		public BinaryImageCropPosition CropPosition { get; set; }

		// Token: 0x0600E055 RID: 57429 RVA: 0x0031E29A File Offset: 0x0031C49A
		public override byte[] ProcessImage(byte[] image)
		{
			if (this.Mode != BinaryImageResizeMode.None)
			{
				return this.ProcessImageInternal(image);
			}
			return image;
		}

		// Token: 0x0600E056 RID: 57430 RVA: 0x0031E2B0 File Offset: 0x0031C4B0
		protected virtual byte[] ProcessImageInternal(byte[] image)
		{
			if (image.Length <= 0)
			{
				return null;
			}
			ImageFormat imageFormat = BinaryImageFormatHelper.GetImageFormat(image);
			Image image2 = BinaryImageFormatHelper.CreateImgFromBytes(image);
			int num = (int)((float)image2.Height * ((float)this.Width / (float)image2.Width));
			int num2 = (int)((float)image2.Width * ((float)this.Height / (float)image2.Height));
			switch (this.Mode)
			{
			case BinaryImageResizeMode.Fit:
				if (image2.Height == this.Height && image2.Width == this.Width)
				{
					return image;
				}
				num = Math.Max(1, num);
				num2 = Math.Max(1, num2);
				return BinaryImageFormatHelper.CreateByteFromImage(this.FitImage(image2, num, num2), imageFormat);
			case BinaryImageResizeMode.Crop:
				if (image2.Height <= this.Height && image2.Width <= this.Width)
				{
					return image;
				}
				return BinaryImageFormatHelper.CreateByteFromImage(this.CropImage(image2, num, num2), imageFormat);
			case BinaryImageResizeMode.Fill:
				if (this.Width <= 0 || this.Height <= 0 || (image2.Width == this.Width && image2.Height == this.Height))
				{
					return image;
				}
				return BinaryImageFormatHelper.CreateByteFromImage(this.FillImage(image2, this.Height, this.Width), imageFormat);
			default:
				return null;
			}
		}

		// Token: 0x0600E057 RID: 57431 RVA: 0x0031E3E0 File Offset: 0x0031C5E0
		private Image CropImage(Image img, int scaledHeight, int scaledWidth)
		{
			int num;
			int num2;
			if ((float)this.Width / (float)img.Width > (float)this.Height / (float)img.Height)
			{
				num = this.Width;
				num2 = scaledHeight;
			}
			else
			{
				num = scaledWidth;
				num2 = this.Height;
			}
			BinaryImageTransformationFilter.CropStartPosition cropStartPosition = this.GetCropStartPosition(num, num2);
			Bitmap bitmap = new Bitmap(this.Width, this.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			this.SetupGraphics(graphics);
			graphics.DrawImage(img, cropStartPosition.X, cropStartPosition.Y, num, num2);
			return bitmap;
		}

		// Token: 0x0600E058 RID: 57432 RVA: 0x0031E46C File Offset: 0x0031C66C
		private BinaryImageTransformationFilter.CropStartPosition GetCropStartPosition(int cropWidth, int cropHeight)
		{
			int x = (this.Width - cropWidth) / 2;
			int y = (this.Height - cropHeight) / 2;
			switch (this.CropPosition)
			{
			case BinaryImageCropPosition.Top:
				y = 0;
				break;
			case BinaryImageCropPosition.Bottom:
				y = this.Height - cropHeight;
				break;
			case BinaryImageCropPosition.Left:
				x = 0;
				break;
			case BinaryImageCropPosition.Right:
				x = this.Width - cropWidth;
				break;
			}
			return new BinaryImageTransformationFilter.CropStartPosition(x, y);
		}

		// Token: 0x0600E059 RID: 57433 RVA: 0x0031E4D8 File Offset: 0x0031C6D8
		private Image FitImage(Image img, int scaledHeight, int scaledWidth)
		{
			int width;
			int height;
			if (this.Height == 0)
			{
				width = this.Width;
				height = scaledHeight;
			}
			else if (this.Width == 0)
			{
				width = scaledWidth;
				height = this.Height;
			}
			else if ((float)this.Width / (float)img.Width < (float)this.Height / (float)img.Height)
			{
				width = this.Width;
				height = scaledHeight;
			}
			else
			{
				width = scaledWidth;
				height = this.Height;
			}
			Bitmap bitmap = new Bitmap(width, height);
			Graphics graphics = Graphics.FromImage(bitmap);
			this.SetupGraphics(graphics);
			graphics.DrawImage(img, 0, 0, width, height);
			return bitmap;
		}

		// Token: 0x0600E05A RID: 57434 RVA: 0x0031E564 File Offset: 0x0031C764
		private Image FillImage(Image img, int height, int width)
		{
			Bitmap bitmap = new Bitmap(width, height);
			Graphics graphics = Graphics.FromImage(bitmap);
			this.SetupGraphics(graphics);
			graphics.DrawImage(img, 0, 0, width, height);
			return bitmap;
		}

		// Token: 0x0600E05B RID: 57435 RVA: 0x0031E593 File Offset: 0x0031C793
		protected virtual void SetupGraphics(Graphics graphics)
		{
			graphics.CompositingMode = CompositingMode.SourceCopy;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.InterpolationMode = this.InterpolationMode;
		}

		// Token: 0x020016B6 RID: 5814
		private struct CropStartPosition
		{
			// Token: 0x0600E05C RID: 57436 RVA: 0x0031E5BD File Offset: 0x0031C7BD
			public CropStartPosition(int x, int y)
			{
				this = default(BinaryImageTransformationFilter.CropStartPosition);
				this.X = x;
				this.Y = y;
			}

			// Token: 0x170044C0 RID: 17600
			// (get) Token: 0x0600E05D RID: 57437 RVA: 0x0031E5D4 File Offset: 0x0031C7D4
			// (set) Token: 0x0600E05E RID: 57438 RVA: 0x0031E5DC File Offset: 0x0031C7DC
			public int X { get; set; }

			// Token: 0x170044C1 RID: 17601
			// (get) Token: 0x0600E05F RID: 57439 RVA: 0x0031E5E5 File Offset: 0x0031C7E5
			// (set) Token: 0x0600E060 RID: 57440 RVA: 0x0031E5ED File Offset: 0x0031C7ED
			public int Y { get; set; }
		}
	}
}
