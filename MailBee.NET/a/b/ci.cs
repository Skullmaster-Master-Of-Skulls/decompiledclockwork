using System;
using System.Drawing.Imaging;
using System.Globalization;

namespace a.b
{
	// Token: 0x0200034A RID: 842
	internal class ci : z
	{
		// Token: 0x06001E77 RID: 7799 RVA: 0x0008221A File Offset: 0x0008121A
		public ci() : this("{0}{1}", null)
		{
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x00082228 File Offset: 0x00081228
		public ci(string A_0) : this(A_0, null)
		{
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x00082232 File Offset: 0x00081232
		public ci(ImageFormat A_0) : this("{0}{1}", A_0)
		{
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x00082240 File Offset: 0x00081240
		public ci(string A_0, ImageFormat A_1) : this(A_0, A_1, 96.0, 96.0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("fileNamePattern");
			}
			this.b = A_0;
			this.c = A_1;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x00082278 File Offset: 0x00081278
		public ci(string A_0, ImageFormat A_1, double A_2, double A_3)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("fileNamePattern");
			}
			this.b = A_0;
			this.c = A_1;
			this.d = A_2;
			this.e = A_3;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000822AB File Offset: 0x000812AB
		public string ge()
		{
			return this.b;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000822B3 File Offset: 0x000812B3
		public ImageFormat gf()
		{
			return this.c;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x000822BB File Offset: 0x000812BB
		public double gg()
		{
			return this.d;
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x000822C3 File Offset: 0x000812C3
		public double gh()
		{
			return this.e;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x000822CC File Offset: 0x000812CC
		public ImageFormat gi(de A_0)
		{
			ImageFormat result = null;
			switch (A_0)
			{
			case de.a:
				result = ImageFormat.Emf;
				break;
			case de.b:
				result = ImageFormat.Png;
				break;
			case de.c:
				result = ImageFormat.Jpeg;
				break;
			case de.d:
				result = ImageFormat.Wmf;
				break;
			case de.e:
				result = ImageFormat.Bmp;
				break;
			}
			return result;
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00082320 File Offset: 0x00081320
		public string gj(int A_0, de A_1)
		{
			ImageFormat imageFormat = this.c;
			if (imageFormat == null)
			{
				imageFormat = this.gi(A_1);
			}
			return string.Format(CultureInfo.InvariantCulture, this.b, new object[]
			{
				A_0,
				ci.a(imageFormat)
			});
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00082368 File Offset: 0x00081368
		public int gk(de A_0, int A_1, int A_2, int A_3)
		{
			float num = (float)A_3 / 100f;
			return (int)Math.Round((double)A_2 * (double)num / 1440.0 * this.d);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0008239C File Offset: 0x0008139C
		public int gl(de A_0, int A_1, int A_2, int A_3)
		{
			float num = (float)A_3 / 100f;
			return (int)Math.Round((double)A_2 * (double)num / 1440.0 * this.e);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000823D0 File Offset: 0x000813D0
		private static string a(ImageFormat A_0)
		{
			string result = null;
			if (A_0 == ImageFormat.Bmp)
			{
				result = ".bmp";
			}
			else if (A_0 == ImageFormat.Emf)
			{
				result = ".emf";
			}
			else if (A_0 == ImageFormat.Exif)
			{
				result = ".exif";
			}
			else if (A_0 == ImageFormat.Gif)
			{
				result = ".gif";
			}
			else if (A_0 == ImageFormat.Icon)
			{
				result = ".ico";
			}
			else if (A_0 == ImageFormat.Jpeg)
			{
				result = ".jpg";
			}
			else if (A_0 == ImageFormat.Png)
			{
				result = ".png";
			}
			else if (A_0 == ImageFormat.Tiff)
			{
				result = ".tiff";
			}
			else if (A_0 == ImageFormat.Wmf)
			{
				result = ".wmf";
			}
			return result;
		}

		// Token: 0x040013DC RID: 5084
		public const double a = 96.0;

		// Token: 0x040013DD RID: 5085
		private readonly string b;

		// Token: 0x040013DE RID: 5086
		private readonly ImageFormat c;

		// Token: 0x040013DF RID: 5087
		private readonly double d;

		// Token: 0x040013E0 RID: 5088
		private readonly double e;

		// Token: 0x040013E1 RID: 5089
		private const string f = "{0}{1}";

		// Token: 0x040013E2 RID: 5090
		private const int g = 1440;
	}
}
