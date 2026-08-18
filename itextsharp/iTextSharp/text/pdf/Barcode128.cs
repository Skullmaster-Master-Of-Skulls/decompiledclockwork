using System;
using System.Drawing;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200063F RID: 1599
	public class Barcode128 : Barcode
	{
		// Token: 0x06003613 RID: 13843 RVA: 0x0014FA44 File Offset: 0x0014EA44
		public Barcode128()
		{
			this.x = 0.8f;
			this.font = BaseFont.CreateFont("Helvetica", "winansi", false);
			this.size = 8f;
			this.baseline = this.size;
			this.barHeight = this.size * 3f;
			this.textAlignment = 1;
			this.codeType = 9;
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x0014FAB0 File Offset: 0x0014EAB0
		public static string RemoveFNC1(string code)
		{
			int length = code.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = code[i];
				if (c >= ' ' && c <= '~')
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x0014FAF8 File Offset: 0x0014EAF8
		public static string GetHumanReadableUCCEAN(string code)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string value = 'Ê'.ToString();
			try
			{
				for (;;)
				{
					if (code.StartsWith(value))
					{
						code = code.Substring(1);
					}
					else
					{
						int num = 0;
						int num2 = 0;
						int num3 = 2;
						while (num3 < 5 && code.Length >= num3)
						{
							if ((num = Barcode128.ais[int.Parse(code.Substring(0, num3))]) != 0)
							{
								num2 = num3;
								break;
							}
							num3++;
						}
						if (num2 == 0)
						{
							break;
						}
						stringBuilder.Append('(').Append(code.Substring(0, num2)).Append(')');
						code = code.Substring(num2);
						if (num > 0)
						{
							num -= num2;
							if (code.Length <= num)
							{
								break;
							}
							stringBuilder.Append(Barcode128.RemoveFNC1(code.Substring(0, num)));
							code = code.Substring(num);
						}
						else
						{
							int num4 = code.IndexOf('Ê');
							if (num4 < 0)
							{
								break;
							}
							stringBuilder.Append(code.Substring(0, num4));
							code = code.Substring(num4 + 1);
						}
					}
				}
			}
			catch
			{
			}
			stringBuilder.Append(Barcode128.RemoveFNC1(code));
			return stringBuilder.ToString();
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x0014FC28 File Offset: 0x0014EC28
		internal static bool IsNextDigits(string text, int textIndex, int numDigits)
		{
			int length = text.Length;
			while (textIndex < length && numDigits > 0)
			{
				if (text[textIndex] == 'Ê')
				{
					textIndex++;
				}
				else
				{
					int num = Math.Min(2, numDigits);
					if (textIndex + num > length)
					{
						return false;
					}
					while (num-- > 0)
					{
						char c = text[textIndex++];
						if (c < '0' || c > '9')
						{
							return false;
						}
						numDigits--;
					}
				}
			}
			return numDigits == 0;
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x0014FC98 File Offset: 0x0014EC98
		internal static string GetPackedRawDigits(string text, int textIndex, int numDigits)
		{
			string text2 = "";
			int num = textIndex;
			while (numDigits > 0)
			{
				if (text[textIndex] == 'Ê')
				{
					text2 += 'f';
					textIndex++;
				}
				else
				{
					numDigits -= 2;
					int num2 = (int)(text[textIndex++] - '0');
					int num3 = (int)(text[textIndex++] - '0');
					text2 += (char)(num2 * 10 + num3);
				}
			}
			return (char)(textIndex - num) + text2;
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x0014FD20 File Offset: 0x0014ED20
		public static string GetRawText(string text, bool ucc)
		{
			string text2 = "";
			int length = text.Length;
			if (length == 0)
			{
				text2 += 'h';
				if (ucc)
				{
					text2 += 'f';
				}
				return text2;
			}
			int num;
			for (int i = 0; i < length; i++)
			{
				num = (int)text[i];
				if (num > 127 && num != 202)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("there.are.illegal.characters.for.barcode.128.in.1", text));
				}
			}
			num = (int)text[0];
			char c = 'h';
			int j = 0;
			if (Barcode128.IsNextDigits(text, j, 2))
			{
				c = 'i';
				text2 += c;
				if (ucc)
				{
					text2 += 'f';
				}
				string packedRawDigits = Barcode128.GetPackedRawDigits(text, j, 2);
				j += (int)packedRawDigits[0];
				text2 += packedRawDigits.Substring(1);
			}
			else if (num < 32)
			{
				c = 'g';
				text2 += c;
				if (ucc)
				{
					text2 += 'f';
				}
				text2 += (char)(num + 64);
				j++;
			}
			else
			{
				text2 += c;
				if (ucc)
				{
					text2 += 'f';
				}
				if (num == 202)
				{
					text2 += 'f';
				}
				else
				{
					text2 += (char)(num - 32);
				}
				j++;
			}
			while (j < length)
			{
				switch (c)
				{
				case 'g':
					if (Barcode128.IsNextDigits(text, j, 4))
					{
						c = 'i';
						text2 += 'c';
						string packedRawDigits2 = Barcode128.GetPackedRawDigits(text, j, 4);
						j += (int)packedRawDigits2[0];
						text2 += packedRawDigits2.Substring(1);
					}
					else
					{
						num = (int)text[j++];
						if (num == 202)
						{
							text2 += 'f';
						}
						else if (num > 95)
						{
							c = 'h';
							text2 += 'd';
							text2 += (char)(num - 32);
						}
						else if (num < 32)
						{
							text2 += (char)(num + 64);
						}
						else
						{
							text2 += (char)(num - 32);
						}
					}
					break;
				case 'h':
					if (Barcode128.IsNextDigits(text, j, 4))
					{
						c = 'i';
						text2 += 'c';
						string packedRawDigits3 = Barcode128.GetPackedRawDigits(text, j, 4);
						j += (int)packedRawDigits3[0];
						text2 += packedRawDigits3.Substring(1);
					}
					else
					{
						num = (int)text[j++];
						if (num == 202)
						{
							text2 += 'f';
						}
						else if (num < 32)
						{
							c = 'g';
							text2 += 'e';
							text2 += (char)(num + 64);
						}
						else
						{
							text2 += (char)(num - 32);
						}
					}
					break;
				case 'i':
					if (Barcode128.IsNextDigits(text, j, 2))
					{
						string packedRawDigits4 = Barcode128.GetPackedRawDigits(text, j, 2);
						j += (int)packedRawDigits4[0];
						text2 += packedRawDigits4.Substring(1);
					}
					else
					{
						num = (int)text[j++];
						if (num == 202)
						{
							text2 += 'f';
						}
						else if (num < 32)
						{
							c = 'g';
							text2 += 'e';
							text2 += (char)(num + 64);
						}
						else
						{
							c = 'h';
							text2 += 'd';
							text2 += (char)(num - 32);
						}
					}
					break;
				}
			}
			return text2;
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x001500E8 File Offset: 0x0014F0E8
		public static byte[] GetBarsCode128Raw(string text)
		{
			int num = text.IndexOf(char.MaxValue);
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			int num2 = (int)text[0];
			int i;
			for (i = 1; i < text.Length; i++)
			{
				num2 += i * (int)text[i];
			}
			num2 %= 103;
			text += (char)num2;
			byte[] array = new byte[(text.Length + 1) * 6 + 7];
			for (i = 0; i < text.Length; i++)
			{
				Array.Copy(Barcode128.BARS[(int)text[i]], 0, array, i * 6, 6);
			}
			Array.Copy(Barcode128.BARS_STOP, 0, array, i * 6, 7);
			return array;
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x00150194 File Offset: 0x0014F194
		public override Rectangle BarcodeSize
		{
			get
			{
				float val = 0f;
				float num = 0f;
				string text;
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
					if (this.codeType == 11)
					{
						int num2 = this.code.IndexOf(char.MaxValue);
						if (num2 < 0)
						{
							text = "";
						}
						else
						{
							text = this.code.Substring(num2 + 1);
						}
					}
					else if (this.codeType == 10)
					{
						text = Barcode128.GetHumanReadableUCCEAN(this.code);
					}
					else
					{
						text = Barcode128.RemoveFNC1(this.code);
					}
					val = this.font.GetWidthPoint((this.altText != null) ? this.altText : text, this.size);
				}
				if (this.codeType == 11)
				{
					int num3 = this.code.IndexOf(char.MaxValue);
					if (num3 >= 0)
					{
						text = this.code.Substring(0, num3);
					}
					else
					{
						text = this.code;
					}
				}
				else
				{
					text = Barcode128.GetRawText(this.code, this.codeType == 10);
				}
				int length = text.Length;
				float num4 = (float)((length + 2) * 11) * this.x + 2f * this.x;
				num4 = Math.Max(num4, val);
				float ury = this.barHeight + num;
				return new Rectangle(num4, ury);
			}
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x00150304 File Offset: 0x0014F304
		public override Rectangle PlaceBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor)
		{
			string text;
			if (this.codeType == 11)
			{
				int num = this.code.IndexOf(char.MaxValue);
				if (num < 0)
				{
					text = "";
				}
				else
				{
					text = this.code.Substring(num + 1);
				}
			}
			else if (this.codeType == 10)
			{
				text = Barcode128.GetHumanReadableUCCEAN(this.code);
			}
			else
			{
				text = Barcode128.RemoveFNC1(this.code);
			}
			float num2 = 0f;
			if (this.font != null)
			{
				num2 = this.font.GetWidthPoint(text = ((this.altText != null) ? this.altText : text), this.size);
			}
			string text2;
			if (this.codeType == 11)
			{
				int num3 = this.code.IndexOf(char.MaxValue);
				if (num3 >= 0)
				{
					text2 = this.code.Substring(0, num3);
				}
				else
				{
					text2 = this.code;
				}
			}
			else
			{
				text2 = Barcode128.GetRawText(this.code, this.codeType == 10);
			}
			int length = text2.Length;
			float num4 = (float)((length + 2) * 11) * this.x + 2f * this.x;
			float num5 = 0f;
			float x = 0f;
			switch (this.textAlignment)
			{
			case 0:
				goto IL_165;
			case 2:
				if (num2 > num4)
				{
					num5 = num2 - num4;
					goto IL_165;
				}
				x = num4 - num2;
				goto IL_165;
			}
			if (num2 > num4)
			{
				num5 = (num2 - num4) / 2f;
			}
			else
			{
				x = (num4 - num2) / 2f;
			}
			IL_165:
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
			byte[] barsCode128Raw = Barcode128.GetBarsCode128Raw(text2);
			bool flag = true;
			if (barColor != null)
			{
				cb.SetColorFill(barColor);
			}
			for (int i = 0; i < barsCode128Raw.Length; i++)
			{
				float num7 = (float)barsCode128Raw[i] * this.x;
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

		// Token: 0x0600361C RID: 13852 RVA: 0x0015057C File Offset: 0x0014F57C
		public override Image CreateDrawingImage(Color foreground, Color background)
		{
			string text;
			if (this.codeType == 11)
			{
				int num = this.code.IndexOf(char.MaxValue);
				if (num >= 0)
				{
					text = this.code.Substring(0, num);
				}
				else
				{
					text = this.code;
				}
			}
			else
			{
				text = Barcode128.GetRawText(this.code, this.codeType == 10);
			}
			int length = text.Length;
			int width = (length + 2) * 11 + 2;
			byte[] barsCode128Raw = Barcode128.GetBarsCode128Raw(text);
			int num2 = (int)this.barHeight;
			Bitmap bitmap = new Bitmap(width, num2);
			for (int i = 0; i < num2; i++)
			{
				bool flag = true;
				int num3 = 0;
				foreach (int num4 in barsCode128Raw)
				{
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

		// Token: 0x17000960 RID: 2400
		// (set) Token: 0x0600361D RID: 13853 RVA: 0x0015066C File Offset: 0x0014F66C
		public override string Code
		{
			set
			{
				if (base.CodeType == 10 && value.StartsWith("("))
				{
					int i = 0;
					string text = "";
					while (i >= 0)
					{
						int num = value.IndexOf(')', i);
						if (num < 0)
						{
							throw new ArgumentException(MessageLocalization.GetComposedMessage("badly.formed.ucc.string.1", value));
						}
						string text2 = value.Substring(i + 1, num - (i + 1));
						if (text2.Length < 2)
						{
							throw new ArgumentException(MessageLocalization.GetComposedMessage("ai.too.short.1", text2));
						}
						int key = int.Parse(text2);
						int num2 = Barcode128.ais[key];
						if (num2 == 0)
						{
							throw new ArgumentException(MessageLocalization.GetComposedMessage("ai.not.found.1", text2));
						}
						text2 = key.ToString();
						if (text2.Length == 1)
						{
							text2 = "0" + text2;
						}
						i = value.IndexOf('(', num);
						int num3 = (i < 0) ? value.Length : i;
						text = text + text2 + value.Substring(num + 1, num3 - (num + 1));
						if (num2 < 0)
						{
							if (i >= 0)
							{
								text += 'Ê';
							}
						}
						else if (num3 - num - 1 + text2.Length != num2)
						{
							throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.ai.length.1", text2));
						}
					}
					base.Code = text;
					return;
				}
				base.Code = value;
			}
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x00150B20 File Offset: 0x0014FB20
		static Barcode128()
		{
			Barcode128.ais[0] = 20;
			Barcode128.ais[1] = 16;
			Barcode128.ais[2] = 16;
			Barcode128.ais[10] = -1;
			Barcode128.ais[11] = 9;
			Barcode128.ais[12] = 8;
			Barcode128.ais[13] = 8;
			Barcode128.ais[15] = 8;
			Barcode128.ais[17] = 8;
			Barcode128.ais[20] = 4;
			Barcode128.ais[21] = -1;
			Barcode128.ais[22] = -1;
			Barcode128.ais[23] = -1;
			Barcode128.ais[240] = -1;
			Barcode128.ais[241] = -1;
			Barcode128.ais[250] = -1;
			Barcode128.ais[251] = -1;
			Barcode128.ais[252] = -1;
			Barcode128.ais[30] = -1;
			for (int i = 3100; i < 3700; i++)
			{
				Barcode128.ais[i] = 10;
			}
			Barcode128.ais[37] = -1;
			for (int j = 3900; j < 3940; j++)
			{
				Barcode128.ais[j] = -1;
			}
			Barcode128.ais[400] = -1;
			Barcode128.ais[401] = -1;
			Barcode128.ais[402] = 20;
			Barcode128.ais[403] = -1;
			for (int k = 410; k < 416; k++)
			{
				Barcode128.ais[k] = 16;
			}
			Barcode128.ais[420] = -1;
			Barcode128.ais[421] = -1;
			Barcode128.ais[422] = 6;
			Barcode128.ais[423] = -1;
			Barcode128.ais[424] = 6;
			Barcode128.ais[425] = 6;
			Barcode128.ais[426] = 6;
			Barcode128.ais[7001] = 17;
			Barcode128.ais[7002] = -1;
			for (int l = 7030; l < 7040; l++)
			{
				Barcode128.ais[l] = -1;
			}
			Barcode128.ais[8001] = 18;
			Barcode128.ais[8002] = -1;
			Barcode128.ais[8003] = -1;
			Barcode128.ais[8004] = -1;
			Barcode128.ais[8005] = 10;
			Barcode128.ais[8006] = 22;
			Barcode128.ais[8007] = -1;
			Barcode128.ais[8008] = -1;
			Barcode128.ais[8018] = 22;
			Barcode128.ais[8020] = -1;
			Barcode128.ais[8100] = 10;
			Barcode128.ais[8101] = 14;
			Barcode128.ais[8102] = 6;
			for (int m = 90; m < 100; m++)
			{
				Barcode128.ais[m] = -1;
			}
		}

		// Token: 0x04002452 RID: 9298
		public const char CODE_AB_TO_C = 'c';

		// Token: 0x04002453 RID: 9299
		public const char CODE_AC_TO_B = 'd';

		// Token: 0x04002454 RID: 9300
		public const char CODE_BC_TO_A = 'e';

		// Token: 0x04002455 RID: 9301
		public const char FNC1_INDEX = 'f';

		// Token: 0x04002456 RID: 9302
		public const char START_A = 'g';

		// Token: 0x04002457 RID: 9303
		public const char START_B = 'h';

		// Token: 0x04002458 RID: 9304
		public const char START_C = 'i';

		// Token: 0x04002459 RID: 9305
		public const char FNC1 = 'Ê';

		// Token: 0x0400245A RID: 9306
		public const char DEL = 'Ã';

		// Token: 0x0400245B RID: 9307
		public const char FNC3 = 'Ä';

		// Token: 0x0400245C RID: 9308
		public const char FNC2 = 'Å';

		// Token: 0x0400245D RID: 9309
		public const char SHIFT = 'Æ';

		// Token: 0x0400245E RID: 9310
		public const char CODE_C = 'Ç';

		// Token: 0x0400245F RID: 9311
		public const char CODE_A = 'È';

		// Token: 0x04002460 RID: 9312
		public const char FNC4 = 'È';

		// Token: 0x04002461 RID: 9313
		public const char STARTA = 'Ë';

		// Token: 0x04002462 RID: 9314
		public const char STARTB = 'Ì';

		// Token: 0x04002463 RID: 9315
		public const char STARTC = 'Í';

		// Token: 0x04002464 RID: 9316
		private static readonly byte[][] BARS = new byte[][]
		{
			new byte[]
			{
				2,
				1,
				2,
				2,
				2,
				2
			},
			new byte[]
			{
				2,
				2,
				2,
				1,
				2,
				2
			},
			new byte[]
			{
				2,
				2,
				2,
				2,
				2,
				1
			},
			new byte[]
			{
				1,
				2,
				1,
				2,
				2,
				3
			},
			new byte[]
			{
				1,
				2,
				1,
				3,
				2,
				2
			},
			new byte[]
			{
				1,
				3,
				1,
				2,
				2,
				2
			},
			new byte[]
			{
				1,
				2,
				2,
				2,
				1,
				3
			},
			new byte[]
			{
				1,
				2,
				2,
				3,
				1,
				2
			},
			new byte[]
			{
				1,
				3,
				2,
				2,
				1,
				2
			},
			new byte[]
			{
				2,
				2,
				1,
				2,
				1,
				3
			},
			new byte[]
			{
				2,
				2,
				1,
				3,
				1,
				2
			},
			new byte[]
			{
				2,
				3,
				1,
				2,
				1,
				2
			},
			new byte[]
			{
				1,
				1,
				2,
				2,
				3,
				2
			},
			new byte[]
			{
				1,
				2,
				2,
				1,
				3,
				2
			},
			new byte[]
			{
				1,
				2,
				2,
				2,
				3,
				1
			},
			new byte[]
			{
				1,
				1,
				3,
				2,
				2,
				2
			},
			new byte[]
			{
				1,
				2,
				3,
				1,
				2,
				2
			},
			new byte[]
			{
				1,
				2,
				3,
				2,
				2,
				1
			},
			new byte[]
			{
				2,
				2,
				3,
				2,
				1,
				1
			},
			new byte[]
			{
				2,
				2,
				1,
				1,
				3,
				2
			},
			new byte[]
			{
				2,
				2,
				1,
				2,
				3,
				1
			},
			new byte[]
			{
				2,
				1,
				3,
				2,
				1,
				2
			},
			new byte[]
			{
				2,
				2,
				3,
				1,
				1,
				2
			},
			new byte[]
			{
				3,
				1,
				2,
				1,
				3,
				1
			},
			new byte[]
			{
				3,
				1,
				1,
				2,
				2,
				2
			},
			new byte[]
			{
				3,
				2,
				1,
				1,
				2,
				2
			},
			new byte[]
			{
				3,
				2,
				1,
				2,
				2,
				1
			},
			new byte[]
			{
				3,
				1,
				2,
				2,
				1,
				2
			},
			new byte[]
			{
				3,
				2,
				2,
				1,
				1,
				2
			},
			new byte[]
			{
				3,
				2,
				2,
				2,
				1,
				1
			},
			new byte[]
			{
				2,
				1,
				2,
				1,
				2,
				3
			},
			new byte[]
			{
				2,
				1,
				2,
				3,
				2,
				1
			},
			new byte[]
			{
				2,
				3,
				2,
				1,
				2,
				1
			},
			new byte[]
			{
				1,
				1,
				1,
				3,
				2,
				3
			},
			new byte[]
			{
				1,
				3,
				1,
				1,
				2,
				3
			},
			new byte[]
			{
				1,
				3,
				1,
				3,
				2,
				1
			},
			new byte[]
			{
				1,
				1,
				2,
				3,
				1,
				3
			},
			new byte[]
			{
				1,
				3,
				2,
				1,
				1,
				3
			},
			new byte[]
			{
				1,
				3,
				2,
				3,
				1,
				1
			},
			new byte[]
			{
				2,
				1,
				1,
				3,
				1,
				3
			},
			new byte[]
			{
				2,
				3,
				1,
				1,
				1,
				3
			},
			new byte[]
			{
				2,
				3,
				1,
				3,
				1,
				1
			},
			new byte[]
			{
				1,
				1,
				2,
				1,
				3,
				3
			},
			new byte[]
			{
				1,
				1,
				2,
				3,
				3,
				1
			},
			new byte[]
			{
				1,
				3,
				2,
				1,
				3,
				1
			},
			new byte[]
			{
				1,
				1,
				3,
				1,
				2,
				3
			},
			new byte[]
			{
				1,
				1,
				3,
				3,
				2,
				1
			},
			new byte[]
			{
				1,
				3,
				3,
				1,
				2,
				1
			},
			new byte[]
			{
				3,
				1,
				3,
				1,
				2,
				1
			},
			new byte[]
			{
				2,
				1,
				1,
				3,
				3,
				1
			},
			new byte[]
			{
				2,
				3,
				1,
				1,
				3,
				1
			},
			new byte[]
			{
				2,
				1,
				3,
				1,
				1,
				3
			},
			new byte[]
			{
				2,
				1,
				3,
				3,
				1,
				1
			},
			new byte[]
			{
				2,
				1,
				3,
				1,
				3,
				1
			},
			new byte[]
			{
				3,
				1,
				1,
				1,
				2,
				3
			},
			new byte[]
			{
				3,
				1,
				1,
				3,
				2,
				1
			},
			new byte[]
			{
				3,
				3,
				1,
				1,
				2,
				1
			},
			new byte[]
			{
				3,
				1,
				2,
				1,
				1,
				3
			},
			new byte[]
			{
				3,
				1,
				2,
				3,
				1,
				1
			},
			new byte[]
			{
				3,
				3,
				2,
				1,
				1,
				1
			},
			new byte[]
			{
				3,
				1,
				4,
				1,
				1,
				1
			},
			new byte[]
			{
				2,
				2,
				1,
				4,
				1,
				1
			},
			new byte[]
			{
				4,
				3,
				1,
				1,
				1,
				1
			},
			new byte[]
			{
				1,
				1,
				1,
				2,
				2,
				4
			},
			new byte[]
			{
				1,
				1,
				1,
				4,
				2,
				2
			},
			new byte[]
			{
				1,
				2,
				1,
				1,
				2,
				4
			},
			new byte[]
			{
				1,
				2,
				1,
				4,
				2,
				1
			},
			new byte[]
			{
				1,
				4,
				1,
				1,
				2,
				2
			},
			new byte[]
			{
				1,
				4,
				1,
				2,
				2,
				1
			},
			new byte[]
			{
				1,
				1,
				2,
				2,
				1,
				4
			},
			new byte[]
			{
				1,
				1,
				2,
				4,
				1,
				2
			},
			new byte[]
			{
				1,
				2,
				2,
				1,
				1,
				4
			},
			new byte[]
			{
				1,
				2,
				2,
				4,
				1,
				1
			},
			new byte[]
			{
				1,
				4,
				2,
				1,
				1,
				2
			},
			new byte[]
			{
				1,
				4,
				2,
				2,
				1,
				1
			},
			new byte[]
			{
				2,
				4,
				1,
				2,
				1,
				1
			},
			new byte[]
			{
				2,
				2,
				1,
				1,
				1,
				4
			},
			new byte[]
			{
				4,
				1,
				3,
				1,
				1,
				1
			},
			new byte[]
			{
				2,
				4,
				1,
				1,
				1,
				2
			},
			new byte[]
			{
				1,
				3,
				4,
				1,
				1,
				1
			},
			new byte[]
			{
				1,
				1,
				1,
				2,
				4,
				2
			},
			new byte[]
			{
				1,
				2,
				1,
				1,
				4,
				2
			},
			new byte[]
			{
				1,
				2,
				1,
				2,
				4,
				1
			},
			new byte[]
			{
				1,
				1,
				4,
				2,
				1,
				2
			},
			new byte[]
			{
				1,
				2,
				4,
				1,
				1,
				2
			},
			new byte[]
			{
				1,
				2,
				4,
				2,
				1,
				1
			},
			new byte[]
			{
				4,
				1,
				1,
				2,
				1,
				2
			},
			new byte[]
			{
				4,
				2,
				1,
				1,
				1,
				2
			},
			new byte[]
			{
				4,
				2,
				1,
				2,
				1,
				1
			},
			new byte[]
			{
				2,
				1,
				2,
				1,
				4,
				1
			},
			new byte[]
			{
				2,
				1,
				4,
				1,
				2,
				1
			},
			new byte[]
			{
				4,
				1,
				2,
				1,
				2,
				1
			},
			new byte[]
			{
				1,
				1,
				1,
				1,
				4,
				3
			},
			new byte[]
			{
				1,
				1,
				1,
				3,
				4,
				1
			},
			new byte[]
			{
				1,
				3,
				1,
				1,
				4,
				1
			},
			new byte[]
			{
				1,
				1,
				4,
				1,
				1,
				3
			},
			new byte[]
			{
				1,
				1,
				4,
				3,
				1,
				1
			},
			new byte[]
			{
				4,
				1,
				1,
				1,
				1,
				3
			},
			new byte[]
			{
				4,
				1,
				1,
				3,
				1,
				1
			},
			new byte[]
			{
				1,
				1,
				3,
				1,
				4,
				1
			},
			new byte[]
			{
				1,
				1,
				4,
				1,
				3,
				1
			},
			new byte[]
			{
				3,
				1,
				1,
				1,
				4,
				1
			},
			new byte[]
			{
				4,
				1,
				1,
				1,
				3,
				1
			},
			new byte[]
			{
				2,
				1,
				1,
				4,
				1,
				2
			},
			new byte[]
			{
				2,
				1,
				1,
				2,
				1,
				4
			},
			new byte[]
			{
				2,
				1,
				1,
				2,
				3,
				2
			}
		};

		// Token: 0x04002465 RID: 9317
		private static readonly byte[] BARS_STOP = new byte[]
		{
			2,
			3,
			3,
			1,
			1,
			1,
			2
		};

		// Token: 0x04002466 RID: 9318
		private static IntHashtable ais = new IntHashtable();
	}
}
