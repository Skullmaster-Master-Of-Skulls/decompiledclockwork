using System;
using System.Drawing;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200016D RID: 365
	public class BarcodePostnet : Barcode
	{
		// Token: 0x06000DD3 RID: 3539 RVA: 0x0004C767 File Offset: 0x0004B767
		public BarcodePostnet()
		{
			this.n = 3.2727273f;
			this.x = 1.4399999f;
			this.barHeight = 9f;
			this.size = 3.6000001f;
			this.codeType = 7;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0004C7A4 File Offset: 0x0004B7A4
		public static byte[] GetBarsPostnet(string text)
		{
			int num = 0;
			for (int i = text.Length - 1; i >= 0; i--)
			{
				int num2 = (int)(text[i] - '0');
				num += num2;
			}
			text += (char)((10 - num % 10) % 10 + 48);
			byte[] array = new byte[text.Length * 5 + 2];
			array[0] = 1;
			array[array.Length - 1] = 1;
			for (int j = 0; j < text.Length; j++)
			{
				int num3 = (int)(text[j] - '0');
				Array.Copy(BarcodePostnet.BARS[num3], 0, array, j * 5 + 1, 5);
			}
			return array;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0004C848 File Offset: 0x0004B848
		public override Rectangle BarcodeSize
		{
			get
			{
				float urx = (float)((this.code.Length + 1) * 5 + 1) * this.n + this.x;
				return new Rectangle(urx, this.barHeight);
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0004C884 File Offset: 0x0004B884
		public override Rectangle PlaceBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor)
		{
			if (barColor != null)
			{
				cb.SetColorFill(barColor);
			}
			byte[] barsPostnet = BarcodePostnet.GetBarsPostnet(this.code);
			byte b = 1;
			if (this.codeType == 8)
			{
				b = 0;
				barsPostnet[0] = 0;
				barsPostnet[barsPostnet.Length - 1] = 0;
			}
			float num = 0f;
			for (int i = 0; i < barsPostnet.Length; i++)
			{
				cb.Rectangle(num, 0f, this.x - this.inkSpreading, (barsPostnet[i] == b) ? this.barHeight : this.size);
				num += this.n;
			}
			cb.Fill();
			return this.BarcodeSize;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0004C918 File Offset: 0x0004B918
		public override Image CreateDrawingImage(Color foreground, Color background)
		{
			int num = (int)this.x;
			if (num <= 0)
			{
				num = 1;
			}
			int num2 = (int)this.n;
			if (num2 <= num)
			{
				num2 = num + 1;
			}
			int num3 = (int)this.size;
			if (num3 <= 0)
			{
				num3 = 1;
			}
			int num4 = (int)this.barHeight;
			if (num4 <= num3)
			{
				num4 = num3 + 1;
			}
			byte[] barsPostnet = BarcodePostnet.GetBarsPostnet(this.code);
			int width = barsPostnet.Length * num2;
			byte b = 1;
			if (this.codeType == 8)
			{
				b = 0;
				barsPostnet[0] = 0;
				barsPostnet[barsPostnet.Length - 1] = 0;
			}
			Bitmap bitmap = new Bitmap(width, num4);
			int num5 = num4 - num3;
			for (int i = 0; i < num5; i++)
			{
				int num6 = 0;
				for (int j = 0; j < barsPostnet.Length; j++)
				{
					bool flag = barsPostnet[j] == b;
					for (int k = 0; k < num2; k++)
					{
						bitmap.SetPixel(num6++, i, (flag && k < num) ? foreground : background);
					}
				}
			}
			for (int l = num5; l < num4; l++)
			{
				int num7 = 0;
				for (int m = 0; m < barsPostnet.Length; m++)
				{
					for (int n = 0; n < num2; n++)
					{
						bitmap.SetPixel(num7++, l, (n < num) ? foreground : background);
					}
				}
			}
			return bitmap;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0004CA54 File Offset: 0x0004BA54
		// Note: this type is marked as 'beforefieldinit'.
		static BarcodePostnet()
		{
			byte[][] array = new byte[10][];
			byte[][] array2 = array;
			int num = 0;
			byte[] array3 = new byte[5];
			array3[0] = 1;
			array3[1] = 1;
			array2[num] = array3;
			array[1] = new byte[]
			{
				0,
				0,
				0,
				1,
				1
			};
			array[2] = new byte[]
			{
				0,
				0,
				1,
				0,
				1
			};
			byte[][] array4 = array;
			int num2 = 3;
			byte[] array5 = new byte[5];
			array5[2] = 1;
			array5[3] = 1;
			array4[num2] = array5;
			array[4] = new byte[]
			{
				0,
				1,
				0,
				0,
				1
			};
			byte[][] array6 = array;
			int num3 = 5;
			byte[] array7 = new byte[5];
			array7[1] = 1;
			array7[3] = 1;
			array6[num3] = array7;
			byte[][] array8 = array;
			int num4 = 6;
			byte[] array9 = new byte[5];
			array9[1] = 1;
			array9[2] = 1;
			array8[num4] = array9;
			array[7] = new byte[]
			{
				1,
				0,
				0,
				0,
				1
			};
			byte[][] array10 = array;
			int num5 = 8;
			byte[] array11 = new byte[5];
			array11[0] = 1;
			array11[3] = 1;
			array10[num5] = array11;
			byte[][] array12 = array;
			int num6 = 9;
			byte[] array13 = new byte[5];
			array13[0] = 1;
			array13[2] = 1;
			array12[num6] = array13;
			BarcodePostnet.BARS = array;
		}

		// Token: 0x04000A29 RID: 2601
		private static readonly byte[][] BARS;
	}
}
