using System;
using System.Drawing;
using System.Globalization;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000535 RID: 1333
	public class BarcodeCodabar : Barcode
	{
		// Token: 0x06002DE3 RID: 11747 RVA: 0x0011AAC4 File Offset: 0x00119AC4
		public BarcodeCodabar()
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
			this.startStopText = false;
			this.codeType = 12;
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x0011AB50 File Offset: 0x00119B50
		public static byte[] GetBarsCodabar(string text)
		{
			text = text.ToUpper(CultureInfo.InvariantCulture);
			int length = text.Length;
			if (length < 2)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("codabar.must.have.at.least.a.start.and.stop.character"));
			}
			if ("0123456789-$:/.+ABCD".IndexOf(text[0]) < 16 || "0123456789-$:/.+ABCD".IndexOf(text[length - 1]) < 16)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("codabar.must.have.one.of.abcd.as.start.stop.character"));
			}
			byte[] array = new byte[text.Length * 8 - 1];
			for (int i = 0; i < length; i++)
			{
				int num = "0123456789-$:/.+ABCD".IndexOf(text[i]);
				if (num >= 16 && i > 0 && i < length - 1)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("in.codabar.start.stop.characters.are.only.allowed.at.the.extremes"));
				}
				if (num < 0)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("the.character.1.is.illegal.in.codabar", text[i]));
				}
				Array.Copy(BarcodeCodabar.BARS[num], 0, array, i * 8, 7);
			}
			return array;
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x0011AC44 File Offset: 0x00119C44
		public static string CalculateChecksum(string code)
		{
			if (code.Length < 2)
			{
				return code;
			}
			string text = code.ToUpper(CultureInfo.InvariantCulture);
			int num = 0;
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				num += "0123456789-$:/.+ABCD".IndexOf(text[i]);
			}
			num = (num + 15) / 16 * 16 - num;
			return code.Substring(0, length - 1) + "0123456789-$:/.+ABCD"[num] + code.Substring(length - 1);
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x0011ACC8 File Offset: 0x00119CC8
		public override Rectangle BarcodeSize
		{
			get
			{
				float val = 0f;
				float num = 0f;
				string text = this.code;
				if (this.generateChecksum && this.checksumText)
				{
					text = BarcodeCodabar.CalculateChecksum(this.code);
				}
				if (!this.startStopText)
				{
					text = text.Substring(1, text.Length - 2);
				}
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
					val = this.font.GetWidthPoint((this.altText != null) ? this.altText : text, this.size);
				}
				text = this.code;
				if (this.generateChecksum)
				{
					text = BarcodeCodabar.CalculateChecksum(this.code);
				}
				byte[] barsCodabar = BarcodeCodabar.GetBarsCodabar(text);
				int num2 = 0;
				for (int i = 0; i < barsCodabar.Length; i++)
				{
					num2 += (int)barsCodabar[i];
				}
				int num3 = barsCodabar.Length - num2;
				float num4 = this.x * ((float)num3 + (float)num2 * this.n);
				num4 = Math.Max(num4, val);
				float ury = this.barHeight + num;
				return new Rectangle(num4, ury);
			}
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x0011ADFC File Offset: 0x00119DFC
		public override Rectangle PlaceBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor)
		{
			string text = this.code;
			if (this.generateChecksum && this.checksumText)
			{
				text = BarcodeCodabar.CalculateChecksum(this.code);
			}
			if (!this.startStopText)
			{
				text = text.Substring(1, text.Length - 2);
			}
			float num = 0f;
			if (this.font != null)
			{
				num = this.font.GetWidthPoint(text = ((this.altText != null) ? this.altText : text), this.size);
			}
			byte[] barsCodabar = BarcodeCodabar.GetBarsCodabar(this.generateChecksum ? BarcodeCodabar.CalculateChecksum(this.code) : this.code);
			int num2 = 0;
			for (int i = 0; i < barsCodabar.Length; i++)
			{
				num2 += (int)barsCodabar[i];
			}
			int num3 = barsCodabar.Length - num2;
			float num4 = this.x * ((float)num3 + (float)num2 * this.n);
			float num5 = 0f;
			float x = 0f;
			switch (this.textAlignment)
			{
			case 0:
				goto IL_126;
			case 2:
				if (num > num4)
				{
					num5 = num - num4;
					goto IL_126;
				}
				x = num4 - num;
				goto IL_126;
			}
			if (num > num4)
			{
				num5 = (num - num4) / 2f;
			}
			else
			{
				x = (num4 - num) / 2f;
			}
			IL_126:
			float y = 0f;
			float num6 = 0f;
			if (this.font != null)
			{
				if (this.baseline <= 0f)
				{
					num6 = this.barHeight - this.baseline;
				}
				else
				{
					num6 = -this.font.GetFontDescriptor(3, this.size);
					y = num6 + this.baseline;
				}
			}
			bool flag = true;
			if (barColor != null)
			{
				cb.SetColorFill(barColor);
			}
			for (int j = 0; j < barsCodabar.Length; j++)
			{
				float num7 = (barsCodabar[j] == 0) ? this.x : (this.x * this.n);
				if (flag)
				{
					cb.Rectangle(num5, y, num7 - this.inkSpreading, this.barHeight);
				}
				flag = !flag;
				num5 += num7;
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
				cb.SetTextMatrix(x, num6);
				cb.ShowText(text);
				cb.EndText();
			}
			return this.BarcodeSize;
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x0011B038 File Offset: 0x0011A038
		public override Image CreateDrawingImage(Color foreground, Color background)
		{
			string text = this.code;
			if (this.generateChecksum && this.checksumText)
			{
				text = BarcodeCodabar.CalculateChecksum(this.code);
			}
			if (!this.startStopText)
			{
				text = text.Substring(1, text.Length - 2);
			}
			byte[] barsCodabar = BarcodeCodabar.GetBarsCodabar(this.generateChecksum ? BarcodeCodabar.CalculateChecksum(this.code) : this.code);
			int num = 0;
			for (int i = 0; i < barsCodabar.Length; i++)
			{
				num += (int)barsCodabar[i];
			}
			int num2 = barsCodabar.Length - num;
			int width = num2 + num * (int)this.n;
			int num3 = (int)this.barHeight;
			Bitmap bitmap = new Bitmap(width, num3);
			for (int j = 0; j < num3; j++)
			{
				bool flag = true;
				int num4 = 0;
				for (int k = 0; k < barsCodabar.Length; k++)
				{
					int num5 = (barsCodabar[k] == 0) ? 1 : ((int)this.n);
					Color color = background;
					if (flag)
					{
						color = foreground;
					}
					flag = !flag;
					for (int l = 0; l < num5; l++)
					{
						bitmap.SetPixel(num4++, j, color);
					}
				}
			}
			return bitmap;
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x0011B198 File Offset: 0x0011A198
		// Note: this type is marked as 'beforefieldinit'.
		static BarcodeCodabar()
		{
			byte[][] array = new byte[20][];
			array[0] = new byte[]
			{
				0,
				0,
				0,
				0,
				0,
				1,
				1
			};
			byte[][] array2 = array;
			int num = 1;
			byte[] array3 = new byte[7];
			array3[4] = 1;
			array3[5] = 1;
			array2[num] = array3;
			array[2] = new byte[]
			{
				0,
				0,
				0,
				1,
				0,
				0,
				1
			};
			byte[][] array4 = array;
			int num2 = 3;
			byte[] array5 = new byte[7];
			array5[0] = 1;
			array5[1] = 1;
			array4[num2] = array5;
			byte[][] array6 = array;
			int num3 = 4;
			byte[] array7 = new byte[7];
			array7[2] = 1;
			array7[5] = 1;
			array6[num3] = array7;
			byte[][] array8 = array;
			int num4 = 5;
			byte[] array9 = new byte[7];
			array9[0] = 1;
			array9[5] = 1;
			array8[num4] = array9;
			array[6] = new byte[]
			{
				0,
				1,
				0,
				0,
				0,
				0,
				1
			};
			byte[][] array10 = array;
			int num5 = 7;
			byte[] array11 = new byte[7];
			array11[1] = 1;
			array11[4] = 1;
			array10[num5] = array11;
			byte[][] array12 = array;
			int num6 = 8;
			byte[] array13 = new byte[7];
			array13[1] = 1;
			array13[2] = 1;
			array12[num6] = array13;
			byte[][] array14 = array;
			int num7 = 9;
			byte[] array15 = new byte[7];
			array15[0] = 1;
			array15[3] = 1;
			array14[num7] = array15;
			byte[][] array16 = array;
			int num8 = 10;
			byte[] array17 = new byte[7];
			array17[3] = 1;
			array17[4] = 1;
			array16[num8] = array17;
			byte[][] array18 = array;
			int num9 = 11;
			byte[] array19 = new byte[7];
			array19[2] = 1;
			array19[3] = 1;
			array18[num9] = array19;
			array[12] = new byte[]
			{
				1,
				0,
				0,
				0,
				1,
				0,
				1
			};
			array[13] = new byte[]
			{
				1,
				0,
				1,
				0,
				0,
				0,
				1
			};
			array[14] = new byte[]
			{
				1,
				0,
				1,
				0,
				1,
				0,
				0
			};
			array[15] = new byte[]
			{
				0,
				0,
				1,
				0,
				1,
				0,
				1
			};
			array[16] = new byte[]
			{
				0,
				0,
				1,
				1,
				0,
				1,
				0
			};
			array[17] = new byte[]
			{
				0,
				1,
				0,
				1,
				0,
				0,
				1
			};
			array[18] = new byte[]
			{
				0,
				0,
				0,
				1,
				0,
				1,
				1
			};
			array[19] = new byte[]
			{
				0,
				0,
				0,
				1,
				1,
				1,
				0
			};
			BarcodeCodabar.BARS = array;
		}

		// Token: 0x04001FBF RID: 8127
		private const string CHARS = "0123456789-$:/.+ABCD";

		// Token: 0x04001FC0 RID: 8128
		private const int START_STOP_IDX = 16;

		// Token: 0x04001FC1 RID: 8129
		private static readonly byte[][] BARS;
	}
}
