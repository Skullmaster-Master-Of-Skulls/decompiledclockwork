using System;
using System.Drawing;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020003BA RID: 954
	public class BarcodeInter25 : Barcode
	{
		// Token: 0x06002124 RID: 8484 RVA: 0x000C782C File Offset: 0x000C682C
		public BarcodeInter25()
		{
			this.x = 0.8f;
			this.n = 2f;
			this.font = BaseFont.CreateFont("Helvetica", "winansi", false);
			this.size = 8f;
			this.baseline = this.size;
			this.barHeight = this.size * 3f;
			this.textAlignment = 1;
			this.generateChecksum = false;
			this.checksumText = false;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000C78AC File Offset: 0x000C68AC
		public static string KeepNumbers(string text)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in text)
			{
				if (c >= '0' && c <= '9')
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000C78F0 File Offset: 0x000C68F0
		public static char GetChecksum(string text)
		{
			int num = 3;
			int num2 = 0;
			for (int i = text.Length - 1; i >= 0; i--)
			{
				int num3 = (int)(text[i] - '0');
				num2 += num * num3;
				num ^= 2;
			}
			return (char)((10 - num2 % 10) % 10 + 48);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000C7938 File Offset: 0x000C6938
		public static byte[] GetBarsInter25(string text)
		{
			text = BarcodeInter25.KeepNumbers(text);
			if ((text.Length & 1) != 0)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.text.length.must.be.even"));
			}
			byte[] array = new byte[text.Length * 5 + 7];
			int num = 0;
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = 0;
			int num2 = text.Length / 2;
			for (int i = 0; i < num2; i++)
			{
				int num3 = (int)(text[i * 2] - '0');
				int num4 = (int)(text[i * 2 + 1] - '0');
				byte[] array2 = BarcodeInter25.BARS[num3];
				byte[] array3 = BarcodeInter25.BARS[num4];
				for (int j = 0; j < 5; j++)
				{
					array[num++] = array2[j];
					array[num++] = array3[j];
				}
			}
			array[num++] = 1;
			array[num++] = 0;
			array[num++] = 0;
			return array;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x000C7A24 File Offset: 0x000C6A24
		public override Rectangle BarcodeSize
		{
			get
			{
				float val = 0f;
				float num = 0f;
				if (this.font != null)
				{
					if (this.baseline > 0f)
					{
						num = this.baseline - this.font.GetFontDescriptor(3, this.size);
					}
					else
					{
						num = -this.baseline + this.size;
					}
					string text = this.code;
					if (this.generateChecksum && this.checksumText)
					{
						text += BarcodeInter25.GetChecksum(text);
					}
					val = this.font.GetWidthPoint((this.altText != null) ? this.altText : text, this.size);
				}
				string text2 = BarcodeInter25.KeepNumbers(this.code);
				int num2 = text2.Length;
				if (this.generateChecksum)
				{
					num2++;
				}
				float num3 = (float)num2 * (3f * this.x + 2f * this.x * this.n) + (6f + this.n) * this.x;
				num3 = Math.Max(num3, val);
				float ury = this.barHeight + num;
				return new Rectangle(num3, ury);
			}
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x000C7B48 File Offset: 0x000C6B48
		public override Rectangle PlaceBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor)
		{
			string text = this.code;
			float num = 0f;
			if (this.font != null)
			{
				if (this.generateChecksum && this.checksumText)
				{
					text += BarcodeInter25.GetChecksum(text);
				}
				num = this.font.GetWidthPoint(text = ((this.altText != null) ? this.altText : text), this.size);
			}
			string text2 = BarcodeInter25.KeepNumbers(this.code);
			if (this.generateChecksum)
			{
				text2 += BarcodeInter25.GetChecksum(text2);
			}
			int length = text2.Length;
			float num2 = (float)length * (3f * this.x + 2f * this.x * this.n) + (6f + this.n) * this.x;
			float num3 = 0f;
			float x = 0f;
			switch (this.textAlignment)
			{
			case 0:
				goto IL_121;
			case 2:
				if (num > num2)
				{
					num3 = num - num2;
					goto IL_121;
				}
				x = num2 - num;
				goto IL_121;
			}
			if (num > num2)
			{
				num3 = (num - num2) / 2f;
			}
			else
			{
				x = (num2 - num) / 2f;
			}
			IL_121:
			float y = 0f;
			float num4 = 0f;
			if (this.font != null)
			{
				if (this.baseline <= 0f)
				{
					num4 = this.barHeight - this.baseline;
				}
				else
				{
					num4 = -this.font.GetFontDescriptor(3, this.size);
					y = num4 + this.baseline;
				}
			}
			byte[] barsInter = BarcodeInter25.GetBarsInter25(text2);
			bool flag = true;
			if (barColor != null)
			{
				cb.SetColorFill(barColor);
			}
			for (int i = 0; i < barsInter.Length; i++)
			{
				float num5 = (barsInter[i] == 0) ? this.x : (this.x * this.n);
				if (flag)
				{
					cb.Rectangle(num3, y, num5 - this.inkSpreading, this.barHeight);
				}
				flag = !flag;
				num3 += num5;
			}
			cb.Fill();
			if (this.font != null)
			{
				if (textColor != null)
				{
					cb.SetColorFill(textColor);
				}
				cb.BeginText();
				cb.SetFontAndSize(this.font, this.size);
				cb.SetTextMatrix(x, num4);
				cb.ShowText(text);
				cb.EndText();
			}
			return this.BarcodeSize;
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000C7D88 File Offset: 0x000C6D88
		public override Image CreateDrawingImage(Color foreground, Color background)
		{
			string text = BarcodeInter25.KeepNumbers(this.code);
			if (this.generateChecksum)
			{
				text += BarcodeInter25.GetChecksum(text);
			}
			int length = text.Length;
			int num = (int)this.n;
			int width = length * (3 + 2 * num) + (6 + num);
			byte[] barsInter = BarcodeInter25.GetBarsInter25(text);
			int num2 = (int)this.barHeight;
			Bitmap bitmap = new Bitmap(width, num2);
			for (int i = 0; i < num2; i++)
			{
				bool flag = true;
				int num3 = 0;
				for (int j = 0; j < barsInter.Length; j++)
				{
					int num4 = (barsInter[j] == 0) ? 1 : num;
					Color color = background;
					if (flag)
					{
						color = foreground;
					}
					flag = !flag;
					for (int k = 0; k < num4; k++)
					{
						bitmap.SetPixel(num3++, i, color);
					}
				}
			}
			return bitmap;
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000C7E60 File Offset: 0x000C6E60
		// Note: this type is marked as 'beforefieldinit'.
		static BarcodeInter25()
		{
			byte[][] array = new byte[10][];
			byte[][] array2 = array;
			int num = 0;
			byte[] array3 = new byte[5];
			array3[2] = 1;
			array3[3] = 1;
			array2[num] = array3;
			array[1] = new byte[]
			{
				1,
				0,
				0,
				0,
				1
			};
			array[2] = new byte[]
			{
				0,
				1,
				0,
				0,
				1
			};
			byte[][] array4 = array;
			int num2 = 3;
			byte[] array5 = new byte[5];
			array5[0] = 1;
			array5[1] = 1;
			array4[num2] = array5;
			array[4] = new byte[]
			{
				0,
				0,
				1,
				0,
				1
			};
			byte[][] array6 = array;
			int num3 = 5;
			byte[] array7 = new byte[5];
			array7[0] = 1;
			array7[2] = 1;
			array6[num3] = array7;
			byte[][] array8 = array;
			int num4 = 6;
			byte[] array9 = new byte[5];
			array9[1] = 1;
			array9[2] = 1;
			array8[num4] = array9;
			array[7] = new byte[]
			{
				0,
				0,
				0,
				1,
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
			array13[1] = 1;
			array13[3] = 1;
			array12[num6] = array13;
			BarcodeInter25.BARS = array;
		}

		// Token: 0x040016DB RID: 5851
		private static readonly byte[][] BARS;
	}
}
