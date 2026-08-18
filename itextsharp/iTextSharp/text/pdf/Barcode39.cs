using System;
using System.Drawing;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000536 RID: 1334
	public class Barcode39 : Barcode
	{
		// Token: 0x06002DEA RID: 11754 RVA: 0x0011B368 File Offset: 0x0011A368
		public Barcode39()
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
			this.startStopText = true;
			this.extended = false;
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x0011B3F4 File Offset: 0x0011A3F4
		public static byte[] GetBarsCode39(string text)
		{
			text = "*" + text + "*";
			byte[] array = new byte[text.Length * 10 - 1];
			for (int i = 0; i < text.Length; i++)
			{
				int num = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*".IndexOf(text[i]);
				if (num < 0)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("the.character.1.is.illegal.in.code.39", text[i]));
				}
				Array.Copy(Barcode39.BARS[num], 0, array, i * 10, 9);
			}
			return array;
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x0011B47C File Offset: 0x0011A47C
		public static string GetCode39Ex(string text)
		{
			string text2 = "";
			foreach (char c in text)
			{
				if (c > '\u007f')
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("the.character.1.is.illegal.in.code.39.extended", c));
				}
				char c2 = "%U$A$B$C$D$E$F$G$H$I$J$K$L$M$N$O$P$Q$R$S$T$U$V$W$X$Y$Z%A%B%C%D%E  /A/B/C/D/E/F/G/H/I/J/K/L - ./O 0 1 2 3 4 5 6 7 8 9/Z%F%G%H%I%J%V A B C D E F G H I J K L M N O P Q R S T U V W X Y Z%K%L%M%N%O%W+A+B+C+D+E+F+G+H+I+J+K+L+M+N+O+P+Q+R+S+T+U+V+W+X+Y+Z%P%Q%R%S%T"[(int)(c * '\u0002')];
				char c3 = "%U$A$B$C$D$E$F$G$H$I$J$K$L$M$N$O$P$Q$R$S$T$U$V$W$X$Y$Z%A%B%C%D%E  /A/B/C/D/E/F/G/H/I/J/K/L - ./O 0 1 2 3 4 5 6 7 8 9/Z%F%G%H%I%J%V A B C D E F G H I J K L M N O P Q R S T U V W X Y Z%K%L%M%N%O%W+A+B+C+D+E+F+G+H+I+J+K+L+M+N+O+P+Q+R+S+T+U+V+W+X+Y+Z%P%Q%R%S%T"[(int)(c * '\u0002' + '\u0001')];
				if (c2 != ' ')
				{
					text2 += c2;
				}
				text2 += c3;
			}
			return text2;
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x0011B504 File Offset: 0x0011A504
		internal static char GetChecksum(string text)
		{
			int num = 0;
			for (int i = 0; i < text.Length; i++)
			{
				int num2 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*".IndexOf(text[i]);
				if (num2 < 0)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("the.character.1.is.illegal.in.code.39", text[i]));
				}
				num += num2;
			}
			return "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*"[num % 43];
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x0011B568 File Offset: 0x0011A568
		public override Rectangle BarcodeSize
		{
			get
			{
				float val = 0f;
				float num = 0f;
				string text = this.code;
				if (this.extended)
				{
					text = Barcode39.GetCode39Ex(this.code);
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
					string text2 = this.code;
					if (this.generateChecksum && this.checksumText)
					{
						text2 += Barcode39.GetChecksum(text);
					}
					if (this.startStopText)
					{
						text2 = "*" + text2 + "*";
					}
					val = this.font.GetWidthPoint((this.altText != null) ? this.altText : text2, this.size);
				}
				int num2 = text.Length + 2;
				if (this.generateChecksum)
				{
					num2++;
				}
				float num3 = (float)num2 * (6f * this.x + 3f * this.x * this.n) + (float)(num2 - 1) * this.x;
				num3 = Math.Max(num3, val);
				float ury = this.barHeight + num;
				return new Rectangle(num3, ury);
			}
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x0011B6AC File Offset: 0x0011A6AC
		public override Rectangle PlaceBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor)
		{
			string text = this.code;
			float num = 0f;
			string text2 = this.code;
			if (this.extended)
			{
				text2 = Barcode39.GetCode39Ex(this.code);
			}
			if (this.font != null)
			{
				if (this.generateChecksum && this.checksumText)
				{
					text += Barcode39.GetChecksum(text2);
				}
				if (this.startStopText)
				{
					text = "*" + text + "*";
				}
				num = this.font.GetWidthPoint(text = ((this.altText != null) ? this.altText : text), this.size);
			}
			if (this.generateChecksum)
			{
				text2 += Barcode39.GetChecksum(text2);
			}
			int num2 = text2.Length + 2;
			float num3 = (float)num2 * (6f * this.x + 3f * this.x * this.n) + (float)(num2 - 1) * this.x;
			float num4 = 0f;
			float x = 0f;
			switch (this.textAlignment)
			{
			case 0:
				goto IL_143;
			case 2:
				if (num > num3)
				{
					num4 = num - num3;
					goto IL_143;
				}
				x = num3 - num;
				goto IL_143;
			}
			if (num > num3)
			{
				num4 = (num - num3) / 2f;
			}
			else
			{
				x = (num3 - num) / 2f;
			}
			IL_143:
			float y = 0f;
			float num5 = 0f;
			if (this.font != null)
			{
				if (this.baseline <= 0f)
				{
					num5 = this.barHeight - this.baseline;
				}
				else
				{
					num5 = -this.font.GetFontDescriptor(3, this.size);
					y = num5 + this.baseline;
				}
			}
			byte[] barsCode = Barcode39.GetBarsCode39(text2);
			bool flag = true;
			if (barColor != null)
			{
				cb.SetColorFill(barColor);
			}
			for (int i = 0; i < barsCode.Length; i++)
			{
				float num6 = (barsCode[i] == 0) ? this.x : (this.x * this.n);
				if (flag)
				{
					cb.Rectangle(num4, y, num6 - this.inkSpreading, this.barHeight);
				}
				flag = !flag;
				num4 += num6;
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
				cb.SetTextMatrix(x, num5);
				cb.ShowText(text);
				cb.EndText();
			}
			return this.BarcodeSize;
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x0011B910 File Offset: 0x0011A910
		public override Image CreateDrawingImage(Color foreground, Color background)
		{
			string text = this.code;
			if (this.extended)
			{
				text = Barcode39.GetCode39Ex(this.code);
			}
			if (this.generateChecksum)
			{
				text += Barcode39.GetChecksum(text);
			}
			int num = text.Length + 2;
			int num2 = (int)this.n;
			int width = num * (6 + 3 * num2) + (num - 1);
			int num3 = (int)this.barHeight;
			Bitmap bitmap = new Bitmap(width, num3);
			byte[] barsCode = Barcode39.GetBarsCode39(text);
			for (int i = 0; i < num3; i++)
			{
				bool flag = true;
				int num4 = 0;
				for (int j = 0; j < barsCode.Length; j++)
				{
					int num5 = (barsCode[j] == 0) ? 1 : num2;
					Color color = background;
					if (flag)
					{
						color = foreground;
					}
					flag = !flag;
					for (int k = 0; k < num5; k++)
					{
						bitmap.SetPixel(num4++, i, color);
					}
				}
			}
			return bitmap;
		}

		// Token: 0x04001FC2 RID: 8130
		private const string CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";

		// Token: 0x04001FC3 RID: 8131
		private const string EXTENDED = "%U$A$B$C$D$E$F$G$H$I$J$K$L$M$N$O$P$Q$R$S$T$U$V$W$X$Y$Z%A%B%C%D%E  /A/B/C/D/E/F/G/H/I/J/K/L - ./O 0 1 2 3 4 5 6 7 8 9/Z%F%G%H%I%J%V A B C D E F G H I J K L M N O P Q R S T U V W X Y Z%K%L%M%N%O%W+A+B+C+D+E+F+G+H+I+J+K+L+M+N+O+P+Q+R+S+T+U+V+W+X+Y+Z%P%Q%R%S%T";

		// Token: 0x04001FC4 RID: 8132
		private static readonly byte[][] BARS = new byte[][]
		{
			new byte[]
			{
				0,
				0,
				0,
				1,
				1,
				0,
				1,
				0,
				0
			},
			new byte[]
			{
				1,
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				1
			},
			new byte[]
			{
				0,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				1
			},
			new byte[]
			{
				1,
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				0
			},
			new byte[]
			{
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				0,
				1
			},
			new byte[]
			{
				1,
				0,
				0,
				1,
				1,
				0,
				0,
				0,
				0
			},
			new byte[]
			{
				0,
				0,
				1,
				1,
				1,
				0,
				0,
				0,
				0
			},
			new byte[]
			{
				0,
				0,
				0,
				1,
				0,
				0,
				1,
				0,
				1
			},
			new byte[]
			{
				1,
				0,
				0,
				1,
				0,
				0,
				1,
				0,
				0
			},
			new byte[]
			{
				0,
				0,
				1,
				1,
				0,
				0,
				1,
				0,
				0
			},
			new byte[]
			{
				1,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				1
			},
			new byte[]
			{
				0,
				0,
				1,
				0,
				0,
				1,
				0,
				0,
				1
			},
			new byte[]
			{
				1,
				0,
				1,
				0,
				0,
				1,
				0,
				0,
				0
			},
			new byte[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				1
			},
			new byte[]
			{
				1,
				0,
				0,
				0,
				1,
				1,
				0,
				0,
				0
			},
			new byte[]
			{
				0,
				0,
				1,
				0,
				1,
				1,
				0,
				0,
				0
			},
			new byte[]
			{
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				1
			},
			new byte[]
			{
				1,
				0,
				0,
				0,
				0,
				1,
				1,
				0,
				0
			},
			new byte[]
			{
				0,
				0,
				1,
				0,
				0,
				1,
				1,
				0,
				0
			},
			new byte[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				0,
				0
			},
			new byte[]
			{
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1
			},
			new byte[]
			{
				0,
				0,
				1,
				0,
				0,
				0,
				0,
				1,
				1
			},
			new byte[]
			{
				1,
				0,
				1,
				0,
				0,
				0,
				0,
				1,
				0
			},
			new byte[]
			{
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				1,
				1
			},
			new byte[]
			{
				1,
				0,
				0,
				0,
				1,
				0,
				0,
				1,
				0
			},
			new byte[]
			{
				0,
				0,
				1,
				0,
				1,
				0,
				0,
				1,
				0
			},
			new byte[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1
			},
			new byte[]
			{
				1,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				0
			},
			new byte[]
			{
				0,
				0,
				1,
				0,
				0,
				0,
				1,
				1,
				0
			},
			new byte[]
			{
				0,
				0,
				0,
				0,
				1,
				0,
				1,
				1,
				0
			},
			new byte[]
			{
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				1
			},
			new byte[]
			{
				0,
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				1
			},
			new byte[]
			{
				1,
				1,
				1,
				0,
				0,
				0,
				0,
				0,
				0
			},
			new byte[]
			{
				0,
				1,
				0,
				0,
				1,
				0,
				0,
				0,
				1
			},
			new byte[]
			{
				1,
				1,
				0,
				0,
				1,
				0,
				0,
				0,
				0
			},
			new byte[]
			{
				0,
				1,
				1,
				0,
				1,
				0,
				0,
				0,
				0
			},
			new byte[]
			{
				0,
				1,
				0,
				0,
				0,
				0,
				1,
				0,
				1
			},
			new byte[]
			{
				1,
				1,
				0,
				0,
				0,
				0,
				1,
				0,
				0
			},
			new byte[]
			{
				0,
				1,
				1,
				0,
				0,
				0,
				1,
				0,
				0
			},
			new byte[]
			{
				0,
				1,
				0,
				1,
				0,
				1,
				0,
				0,
				0
			},
			new byte[]
			{
				0,
				1,
				0,
				1,
				0,
				0,
				0,
				1,
				0
			},
			new byte[]
			{
				0,
				1,
				0,
				0,
				0,
				1,
				0,
				1,
				0
			},
			new byte[]
			{
				0,
				0,
				0,
				1,
				0,
				1,
				0,
				1,
				0
			},
			new byte[]
			{
				0,
				1,
				0,
				0,
				1,
				0,
				1,
				0,
				0
			}
		};
	}
}
