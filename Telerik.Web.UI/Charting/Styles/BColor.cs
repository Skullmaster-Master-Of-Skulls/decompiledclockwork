using System;
using System.Drawing;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017C2 RID: 6082
	internal struct BColor
	{
		// Token: 0x0600ECB4 RID: 60596 RVA: 0x003604B7 File Offset: 0x0035E6B7
		public static BColor CreateInstance(byte r, byte g, byte b, byte a)
		{
			return new BColor(r, g, b, a);
		}

		// Token: 0x0600ECB5 RID: 60597 RVA: 0x003604C4 File Offset: 0x0035E6C4
		public static BColor CreateInstance()
		{
			return default(BColor);
		}

		// Token: 0x0600ECB6 RID: 60598 RVA: 0x003604DA File Offset: 0x0035E6DA
		internal BColor(byte r, byte g, byte b, byte a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

		// Token: 0x0600ECB7 RID: 60599 RVA: 0x003604FC File Offset: 0x0035E6FC
		public override string ToString()
		{
			return string.Format("{0},{1},{2},{3}", new object[]
			{
				this.R,
				this.G,
				this.B,
				this.A
			});
		}

		// Token: 0x0600ECB8 RID: 60600 RVA: 0x00360554 File Offset: 0x0035E754
		public static BColor[][] GetMatrix(Bitmap source, int width, int height)
		{
			BColor[][] array = new BColor[width][];
			for (int i = 0; i < width; i++)
			{
				array[i] = new BColor[height];
				for (int j = 0; j < height; j++)
				{
					Color pixel = source.GetPixel(i, j);
					array[i][j] = BColor.CreateInstance(pixel.R, pixel.G, pixel.B, pixel.A);
				}
			}
			return array;
		}

		// Token: 0x0600ECB9 RID: 60601 RVA: 0x003605C4 File Offset: 0x0035E7C4
		public static BColor[][] GetMatrix(byte[] bytes, int width, int height)
		{
			BColor[][] array = new BColor[width][];
			int num = bytes.Length;
			for (int i = 0; i < width; i++)
			{
				array[i] = new BColor[height];
			}
			BColor bcolor = BColor.CreateInstance();
			byte b = 0;
			for (int j = 0; j < num; j++)
			{
				int num2 = j / 4;
				int num3 = num2 % width;
				int num4 = (num2 - num3) / width;
				bcolor.R = ((j < num) ? bytes[j] : b);
				j++;
				bcolor.G = ((j < num) ? bytes[j] : b);
				j++;
				bcolor.B = ((j < num) ? bytes[j] : b);
				j++;
				bcolor.A = ((j < num) ? bytes[j] : b);
				array[num3][num4] = bcolor;
			}
			return array;
		}

		// Token: 0x0600ECBA RID: 60602 RVA: 0x0036069C File Offset: 0x0035E89C
		public static BColor[] GetRectAsLine(BColor[][] src, int top, int height, int left, int width)
		{
			int num = top + height;
			int num2 = left + width;
			if (width * height < 0)
			{
				return new BColor[0];
			}
			BColor[] array = new BColor[width * height];
			int num3 = 0;
			for (int i = top; i < num; i++)
			{
				for (int j = left; j < num2; j++)
				{
					array[num3++] = src[j][i];
				}
			}
			return array;
		}

		// Token: 0x0600ECBB RID: 60603 RVA: 0x00360710 File Offset: 0x0035E910
		public static byte[] GetAsLine(BColor[][] src, int top, int height, int left, int width, BColor[] dst, int srcWidth, int srcHeight)
		{
			int num = 0;
			int num2 = top + height;
			int num3 = left + width;
			for (int i = top; i < num2; i++)
			{
				for (int j = left; j < num3; j++)
				{
					src[j][i] = dst[num++];
				}
			}
			byte[] array = new byte[srcWidth * srcHeight * 4];
			num = 0;
			for (int k = 0; k < srcHeight; k++)
			{
				for (int l = 0; l < srcWidth; l++)
				{
					array[num++] = src[l][k].R;
					array[num++] = src[l][k].G;
					array[num++] = src[l][k].B;
					array[num++] = src[l][k].A;
				}
			}
			return array;
		}

		// Token: 0x04004437 RID: 17463
		internal byte R;

		// Token: 0x04004438 RID: 17464
		internal byte G;

		// Token: 0x04004439 RID: 17465
		internal byte B;

		// Token: 0x0400443A RID: 17466
		internal byte A;
	}
}
