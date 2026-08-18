using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000BA0 RID: 2976
	internal abstract class Quantizer
	{
		// Token: 0x06007059 RID: 28761 RVA: 0x001A3977 File Offset: 0x001A1B77
		public Quantizer(bool singlePass)
		{
			this._singlePass = singlePass;
			this._pixelSize = Marshal.SizeOf(typeof(Quantizer.Color32));
		}

		// Token: 0x0600705A RID: 28762 RVA: 0x001A399C File Offset: 0x001A1B9C
		public Bitmap Quantize(Image source)
		{
			int height = source.Height;
			int width = source.Width;
			Rectangle rectangle = new Rectangle(0, 0, width, height);
			Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			Bitmap bitmap2 = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.PageUnit = GraphicsUnit.Pixel;
				graphics.DrawImage(source, rectangle);
			}
			BitmapData bitmapData = null;
			try
			{
				bitmapData = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
				if (!this._singlePass)
				{
					this.FirstPass(bitmapData, width, height);
				}
				bitmap2.Palette = this.GetPalette(bitmap2.Palette);
				this.SecondPass(bitmapData, bitmap2, width, height, rectangle);
			}
			finally
			{
				bitmap.UnlockBits(bitmapData);
			}
			return bitmap2;
		}

		// Token: 0x0600705B RID: 28763 RVA: 0x001A3A74 File Offset: 0x001A1C74
		protected virtual void FirstPass(BitmapData sourceData, int width, int height)
		{
			IntPtr intPtr = sourceData.Scan0;
			for (int i = 0; i < height; i++)
			{
				IntPtr intPtr2 = intPtr;
				for (int j = 0; j < width; j++)
				{
					this.InitialQuantizePixel(new Quantizer.Color32(intPtr2));
					intPtr2 = (IntPtr)((long)intPtr2 + (long)this._pixelSize);
				}
				intPtr = (IntPtr)((long)intPtr + (long)sourceData.Stride);
			}
		}

		// Token: 0x0600705C RID: 28764 RVA: 0x001A3AD8 File Offset: 0x001A1CD8
		protected virtual void SecondPass(BitmapData sourceData, Bitmap output, int width, int height, Rectangle bounds)
		{
			BitmapData bitmapData = null;
			try
			{
				bitmapData = output.LockBits(bounds, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
				IntPtr intPtr = sourceData.Scan0;
				IntPtr intPtr2 = intPtr;
				IntPtr ptr = intPtr2;
				IntPtr intPtr3 = bitmapData.Scan0;
				IntPtr intPtr4 = intPtr3;
				byte val = this.QuantizePixel(new Quantizer.Color32(intPtr2));
				Marshal.WriteByte(intPtr4, val);
				for (int i = 0; i < height; i++)
				{
					intPtr2 = intPtr;
					intPtr4 = intPtr3;
					for (int j = 0; j < width; j++)
					{
						if (Marshal.ReadInt32(ptr) != Marshal.ReadInt32(intPtr2))
						{
							val = this.QuantizePixel(new Quantizer.Color32(intPtr2));
							ptr = intPtr2;
						}
						Marshal.WriteByte(intPtr4, val);
						intPtr2 = (IntPtr)((long)intPtr2 + (long)this._pixelSize);
						intPtr4 = (IntPtr)((long)intPtr4 + 1L);
					}
					intPtr = (IntPtr)((long)intPtr + (long)sourceData.Stride);
					intPtr3 = (IntPtr)((long)intPtr3 + (long)bitmapData.Stride);
				}
			}
			finally
			{
				output.UnlockBits(bitmapData);
			}
		}

		// Token: 0x0600705D RID: 28765 RVA: 0x001A3BE4 File Offset: 0x001A1DE4
		protected virtual void InitialQuantizePixel(Quantizer.Color32 pixel)
		{
		}

		// Token: 0x0600705E RID: 28766
		protected abstract byte QuantizePixel(Quantizer.Color32 pixel);

		// Token: 0x0600705F RID: 28767
		protected abstract ColorPalette GetPalette(ColorPalette original);

		// Token: 0x04001E3F RID: 7743
		private readonly bool _singlePass;

		// Token: 0x04001E40 RID: 7744
		private readonly int _pixelSize;

		// Token: 0x02000BA1 RID: 2977
		[StructLayout(LayoutKind.Explicit)]
		public struct Color32
		{
			// Token: 0x06007060 RID: 28768 RVA: 0x001A3BE6 File Offset: 0x001A1DE6
			public Color32(IntPtr pSourcePixel)
			{
				this = (Quantizer.Color32)Marshal.PtrToStructure(pSourcePixel, typeof(Quantizer.Color32));
			}

			// Token: 0x170024C0 RID: 9408
			// (get) Token: 0x06007061 RID: 28769 RVA: 0x001A3C03 File Offset: 0x001A1E03
			// (set) Token: 0x06007062 RID: 28770 RVA: 0x001A3C0B File Offset: 0x001A1E0B
			public byte Blue
			{
				get
				{
					return this._Blue;
				}
				set
				{
					this._Blue = value;
				}
			}

			// Token: 0x170024C1 RID: 9409
			// (get) Token: 0x06007063 RID: 28771 RVA: 0x001A3C14 File Offset: 0x001A1E14
			// (set) Token: 0x06007064 RID: 28772 RVA: 0x001A3C1C File Offset: 0x001A1E1C
			public byte Green
			{
				get
				{
					return this._Green;
				}
				set
				{
					this._Green = value;
				}
			}

			// Token: 0x170024C2 RID: 9410
			// (get) Token: 0x06007065 RID: 28773 RVA: 0x001A3C25 File Offset: 0x001A1E25
			// (set) Token: 0x06007066 RID: 28774 RVA: 0x001A3C2D File Offset: 0x001A1E2D
			public byte Red
			{
				get
				{
					return this._Red;
				}
				set
				{
					this._Red = value;
				}
			}

			// Token: 0x170024C3 RID: 9411
			// (get) Token: 0x06007067 RID: 28775 RVA: 0x001A3C36 File Offset: 0x001A1E36
			// (set) Token: 0x06007068 RID: 28776 RVA: 0x001A3C3E File Offset: 0x001A1E3E
			public byte Alpha
			{
				get
				{
					return this._Alpha;
				}
				set
				{
					this._Alpha = value;
				}
			}

			// Token: 0x170024C4 RID: 9412
			// (get) Token: 0x06007069 RID: 28777 RVA: 0x001A3C47 File Offset: 0x001A1E47
			// (set) Token: 0x0600706A RID: 28778 RVA: 0x001A3C4F File Offset: 0x001A1E4F
			public int ARGB
			{
				get
				{
					return this._ARGB;
				}
				set
				{
					this._ARGB = value;
				}
			}

			// Token: 0x170024C5 RID: 9413
			// (get) Token: 0x0600706B RID: 28779 RVA: 0x001A3C58 File Offset: 0x001A1E58
			public Color Color
			{
				get
				{
					return Color.FromArgb((int)this.Alpha, (int)this.Red, (int)this.Green, (int)this.Blue);
				}
			}

			// Token: 0x04001E41 RID: 7745
			[FieldOffset(0)]
			private byte _Blue;

			// Token: 0x04001E42 RID: 7746
			[FieldOffset(1)]
			private byte _Green;

			// Token: 0x04001E43 RID: 7747
			[FieldOffset(2)]
			private byte _Red;

			// Token: 0x04001E44 RID: 7748
			[FieldOffset(3)]
			private byte _Alpha;

			// Token: 0x04001E45 RID: 7749
			[FieldOffset(0)]
			private int _ARGB;
		}
	}
}
