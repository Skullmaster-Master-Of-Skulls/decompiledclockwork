using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Telerik.Pdf.Filter;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Image
{
	// Token: 0x020015CB RID: 5579
	internal sealed class ApocImage
	{
		// Token: 0x0600D974 RID: 55668 RVA: 0x002FB704 File Offset: 0x002F9904
		public ApocImage(string href, byte[] imageData)
		{
			this.m_href = href;
			this.m_colorSpace = new ColorSpace(2);
			this.m_bitsPerPixel = 8;
			Bitmap bitmap = new Bitmap(new MemoryStream(imageData));
			this.width = bitmap.Width;
			this.height = bitmap.Height;
			this.m_bitmaps = imageData;
			this.ExtractImage(bitmap);
		}

		// Token: 0x170042F7 RID: 17143
		// (get) Token: 0x0600D975 RID: 55669 RVA: 0x002FB763 File Offset: 0x002F9963
		public string Uri
		{
			get
			{
				return this.m_href;
			}
		}

		// Token: 0x170042F8 RID: 17144
		// (get) Token: 0x0600D976 RID: 55670 RVA: 0x002FB76B File Offset: 0x002F996B
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x170042F9 RID: 17145
		// (get) Token: 0x0600D977 RID: 55671 RVA: 0x002FB773 File Offset: 0x002F9973
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x170042FA RID: 17146
		// (get) Token: 0x0600D978 RID: 55672 RVA: 0x002FB77B File Offset: 0x002F997B
		public int BitsPerPixel
		{
			get
			{
				return this.m_bitsPerPixel;
			}
		}

		// Token: 0x170042FB RID: 17147
		// (get) Token: 0x0600D979 RID: 55673 RVA: 0x002FB783 File Offset: 0x002F9983
		public int BitmapsSize
		{
			get
			{
				if (this.m_bitmaps == null)
				{
					return 0;
				}
				return this.m_bitmaps.Length;
			}
		}

		// Token: 0x170042FC RID: 17148
		// (get) Token: 0x0600D97A RID: 55674 RVA: 0x002FB797 File Offset: 0x002F9997
		public byte[] Bitmaps
		{
			get
			{
				return this.m_bitmaps;
			}
		}

		// Token: 0x170042FD RID: 17149
		// (get) Token: 0x0600D97B RID: 55675 RVA: 0x002FB79F File Offset: 0x002F999F
		public ColorSpace ColorSpace
		{
			get
			{
				return this.m_colorSpace;
			}
		}

		// Token: 0x170042FE RID: 17150
		// (get) Token: 0x0600D97C RID: 55676 RVA: 0x002FB7A7 File Offset: 0x002F99A7
		public IFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x0600D97D RID: 55677 RVA: 0x002FB7B0 File Offset: 0x002F99B0
		private Point GetPixelSize(Bitmap bitmap)
		{
			GraphicsUnit graphicsUnit = GraphicsUnit.Pixel;
			RectangleF bounds = bitmap.GetBounds(ref graphicsUnit);
			return new Point((int)bounds.Width, (int)bounds.Height);
		}

		// Token: 0x0600D97E RID: 55678 RVA: 0x002FB7E0 File Offset: 0x002F99E0
		private void ExtractImage(Bitmap bitmap)
		{
			if (bitmap.RawFormat.Equals(ImageFormat.Jpeg))
			{
				JpegParser jpegParser = new JpegParser(this.m_bitmaps);
				JpegInfo jpegInfo = jpegParser.Parse();
				this.m_bitsPerPixel = jpegInfo.BitsPerSample;
				this.m_colorSpace = new ColorSpace(jpegInfo.ColourSpace);
				this.width = jpegInfo.Width;
				this.height = jpegInfo.Height;
				this.filter = new DctFilter();
				return;
			}
			this.ExtractOtherImageBits(bitmap);
		}

		// Token: 0x0600D97F RID: 55679 RVA: 0x002FB85C File Offset: 0x002F9A5C
		private void ExtractOtherImageBits(Bitmap bitmap)
		{
			Point pixelSize = this.GetPixelSize(bitmap);
			this.LockBitmap(bitmap);
			this.m_bitmaps = new byte[pixelSize.X * pixelSize.Y * 3];
			try
			{
				for (int i = 0; i < pixelSize.Y; i++)
				{
					for (int j = 0; j < pixelSize.X; j++)
					{
						Color pixel = bitmap.GetPixel(j, i);
						PixelData pixelData = default(PixelData);
						pixelData.blue = pixel.B;
						pixelData.red = pixel.R;
						pixelData.green = pixel.G;
						this.m_bitmaps[3 * (i * this.width + j)] = pixelData.red;
						this.m_bitmaps[3 * (i * this.width + j) + 1] = pixelData.green;
						this.m_bitmaps[3 * (i * this.width + j) + 2] = pixelData.blue;
					}
				}
			}
			catch (Exception ex)
			{
				ApocDriver.ActiveDriver.FireApocError(ex.ToString());
			}
			finally
			{
				this.UnlockBitmap(bitmap);
			}
		}

		// Token: 0x0600D980 RID: 55680 RVA: 0x002FB990 File Offset: 0x002F9B90
		private void LockBitmap(Bitmap bitmap)
		{
			GraphicsUnit graphicsUnit = GraphicsUnit.Pixel;
			RectangleF bounds = bitmap.GetBounds(ref graphicsUnit);
			new Rectangle((int)bounds.X, (int)bounds.Y, (int)bounds.Width, (int)bounds.Height);
		}

		// Token: 0x0600D981 RID: 55681 RVA: 0x002FB9CE File Offset: 0x002F9BCE
		private void UnlockBitmap(Bitmap bitmap)
		{
		}

		// Token: 0x04003C20 RID: 15392
		public const int DEFAULT_BITPLANES = 8;

		// Token: 0x04003C21 RID: 15393
		private string m_href;

		// Token: 0x04003C22 RID: 15394
		private int width;

		// Token: 0x04003C23 RID: 15395
		private int height;

		// Token: 0x04003C24 RID: 15396
		private ColorSpace m_colorSpace;

		// Token: 0x04003C25 RID: 15397
		private int m_bitsPerPixel;

		// Token: 0x04003C26 RID: 15398
		private byte[] m_bitmaps;

		// Token: 0x04003C27 RID: 15399
		private IFilter filter;
	}
}
