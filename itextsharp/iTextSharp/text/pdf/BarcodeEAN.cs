using System;
using System.Drawing;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020003BB RID: 955
	public class BarcodeEAN : Barcode
	{
		// Token: 0x0600212C RID: 8492 RVA: 0x000C7F58 File Offset: 0x000C6F58
		public BarcodeEAN()
		{
			this.x = 0.8f;
			this.font = BaseFont.CreateFont("Helvetica", "winansi", false);
			this.size = 8f;
			this.baseline = this.size;
			this.barHeight = this.size * 3f;
			this.guardBars = true;
			this.codeType = 1;
			this.code = "";
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000C7FD0 File Offset: 0x000C6FD0
		public static int CalculateEANParity(string code)
		{
			int num = 3;
			int num2 = 0;
			for (int i = code.Length - 1; i >= 0; i--)
			{
				int num3 = (int)(code[i] - '0');
				num2 += num * num3;
				num ^= 2;
			}
			return (10 - num2 % 10) % 10;
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000C8014 File Offset: 0x000C7014
		public static string ConvertUPCAtoUPCE(string text)
		{
			if (text.Length != 12 || (!text.StartsWith("0") && !text.StartsWith("1")))
			{
				return null;
			}
			if (text.Substring(3, 3).Equals("000") || text.Substring(3, 3).Equals("100") || text.Substring(3, 3).Equals("200"))
			{
				if (text.Substring(6, 2).Equals("00"))
				{
					return string.Concat(new string[]
					{
						text.Substring(0, 1),
						text.Substring(1, 2),
						text.Substring(8, 3),
						text.Substring(3, 1),
						text.Substring(11)
					});
				}
			}
			else if (text.Substring(4, 2).Equals("00"))
			{
				if (text.Substring(6, 3).Equals("000"))
				{
					return string.Concat(new string[]
					{
						text.Substring(0, 1),
						text.Substring(1, 3),
						text.Substring(9, 2),
						"3",
						text.Substring(11)
					});
				}
			}
			else if (text.Substring(5, 1).Equals("0"))
			{
				if (text.Substring(6, 4).Equals("0000"))
				{
					return string.Concat(new string[]
					{
						text.Substring(0, 1),
						text.Substring(1, 4),
						text.Substring(10, 1),
						"4",
						text.Substring(11)
					});
				}
			}
			else if (text[10] >= '5' && text.Substring(6, 4).Equals("0000"))
			{
				return text.Substring(0, 1) + text.Substring(1, 5) + text.Substring(10, 1) + text.Substring(11);
			}
			return null;
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x000C8204 File Offset: 0x000C7204
		public static byte[] GetBarsEAN13(string _code)
		{
			int[] array = new int[_code.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (int)(_code[i] - '0');
			}
			byte[] array2 = new byte[59];
			int num = 0;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			byte[] array3 = BarcodeEAN.PARITY13[array[0]];
			for (int j = 0; j < array3.Length; j++)
			{
				int num2 = array[j + 1];
				byte[] array4 = BarcodeEAN.BARS[num2];
				if (array3[j] == 0)
				{
					array2[num++] = array4[0];
					array2[num++] = array4[1];
					array2[num++] = array4[2];
					array2[num++] = array4[3];
				}
				else
				{
					array2[num++] = array4[3];
					array2[num++] = array4[2];
					array2[num++] = array4[1];
					array2[num++] = array4[0];
				}
			}
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			for (int k = 7; k < 13; k++)
			{
				int num3 = array[k];
				byte[] array5 = BarcodeEAN.BARS[num3];
				array2[num++] = array5[0];
				array2[num++] = array5[1];
				array2[num++] = array5[2];
				array2[num++] = array5[3];
			}
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			return array2;
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000C837C File Offset: 0x000C737C
		public static byte[] GetBarsEAN8(string _code)
		{
			int[] array = new int[_code.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (int)(_code[i] - '0');
			}
			byte[] array2 = new byte[43];
			int num = 0;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			for (int j = 0; j < 4; j++)
			{
				int num2 = array[j];
				byte[] array3 = BarcodeEAN.BARS[num2];
				array2[num++] = array3[0];
				array2[num++] = array3[1];
				array2[num++] = array3[2];
				array2[num++] = array3[3];
			}
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			for (int k = 4; k < 8; k++)
			{
				int num3 = array[k];
				byte[] array4 = BarcodeEAN.BARS[num3];
				array2[num++] = array4[0];
				array2[num++] = array4[1];
				array2[num++] = array4[2];
				array2[num++] = array4[3];
			}
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			return array2;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000C84AC File Offset: 0x000C74AC
		public static byte[] GetBarsUPCE(string _code)
		{
			int[] array = new int[_code.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (int)(_code[i] - '0');
			}
			byte[] array2 = new byte[33];
			bool flag = array[0] != 0;
			int num = 0;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			byte[] array3 = BarcodeEAN.PARITYE[array[array.Length - 1]];
			for (int j = 1; j < array.Length - 1; j++)
			{
				int num2 = array[j];
				byte[] array4 = BarcodeEAN.BARS[num2];
				if (array3[j - 1] == (flag ? 1 : 0))
				{
					array2[num++] = array4[0];
					array2[num++] = array4[1];
					array2[num++] = array4[2];
					array2[num++] = array4[3];
				}
				else
				{
					array2[num++] = array4[3];
					array2[num++] = array4[2];
					array2[num++] = array4[1];
					array2[num++] = array4[0];
				}
			}
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 1;
			return array2;
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000C8604 File Offset: 0x000C7604
		public static byte[] GetBarsSupplemental2(string _code)
		{
			int[] array = new int[2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (int)(_code[i] - '0');
			}
			byte[] array2 = new byte[13];
			int num = 0;
			int num2 = (array[0] * 10 + array[1]) % 4;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 2;
			byte[] array3 = BarcodeEAN.PARITY2[num2];
			for (int j = 0; j < array3.Length; j++)
			{
				if (j == 1)
				{
					array2[num++] = 1;
					array2[num++] = 1;
				}
				int num3 = array[j];
				byte[] array4 = BarcodeEAN.BARS[num3];
				if (array3[j] == 0)
				{
					array2[num++] = array4[0];
					array2[num++] = array4[1];
					array2[num++] = array4[2];
					array2[num++] = array4[3];
				}
				else
				{
					array2[num++] = array4[3];
					array2[num++] = array4[2];
					array2[num++] = array4[1];
					array2[num++] = array4[0];
				}
			}
			return array2;
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000C8710 File Offset: 0x000C7710
		public static byte[] GetBarsSupplemental5(string _code)
		{
			int[] array = new int[5];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (int)(_code[i] - '0');
			}
			byte[] array2 = new byte[31];
			int num = 0;
			int num2 = ((array[0] + array[2] + array[4]) * 3 + (array[1] + array[3]) * 9) % 10;
			array2[num++] = 1;
			array2[num++] = 1;
			array2[num++] = 2;
			byte[] array3 = BarcodeEAN.PARITY5[num2];
			for (int j = 0; j < array3.Length; j++)
			{
				if (j != 0)
				{
					array2[num++] = 1;
					array2[num++] = 1;
				}
				int num3 = array[j];
				byte[] array4 = BarcodeEAN.BARS[num3];
				if (array3[j] == 0)
				{
					array2[num++] = array4[0];
					array2[num++] = array4[1];
					array2[num++] = array4[2];
					array2[num++] = array4[3];
				}
				else
				{
					array2[num++] = array4[3];
					array2[num++] = array4[2];
					array2[num++] = array4[1];
					array2[num++] = array4[0];
				}
			}
			return array2;
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x000C8828 File Offset: 0x000C7828
		public override Rectangle BarcodeSize
		{
			get
			{
				float num = this.barHeight;
				if (this.font != null)
				{
					if (this.baseline <= 0f)
					{
						num += -this.baseline + this.size;
					}
					else
					{
						num += this.baseline - this.font.GetFontDescriptor(3, this.size);
					}
				}
				float num2;
				switch (this.codeType)
				{
				case 1:
					num2 = this.x * 95f;
					if (this.font != null)
					{
						num2 += this.font.GetWidthPoint((int)this.code[0], this.size);
					}
					break;
				case 2:
					num2 = this.x * 67f;
					break;
				case 3:
					num2 = this.x * 95f;
					if (this.font != null)
					{
						num2 += this.font.GetWidthPoint((int)this.code[0], this.size) + this.font.GetWidthPoint((int)this.code[11], this.size);
					}
					break;
				case 4:
					num2 = this.x * 51f;
					if (this.font != null)
					{
						num2 += this.font.GetWidthPoint((int)this.code[0], this.size) + this.font.GetWidthPoint((int)this.code[7], this.size);
					}
					break;
				case 5:
					num2 = this.x * 20f;
					break;
				case 6:
					num2 = this.x * 47f;
					break;
				default:
					throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.code.type"));
				}
				return new Rectangle(num2, num);
			}
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000C89E8 File Offset: 0x000C79E8
		public override Rectangle PlaceBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor)
		{
			Rectangle barcodeSize = this.BarcodeSize;
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			if (this.font != null)
			{
				if (this.baseline <= 0f)
				{
					num3 = this.barHeight - this.baseline;
				}
				else
				{
					num3 = -this.font.GetFontDescriptor(3, this.size);
					num2 = num3 + this.baseline;
				}
			}
			switch (this.codeType)
			{
			case 1:
			case 3:
			case 4:
				if (this.font != null)
				{
					num += this.font.GetWidthPoint((int)this.code[0], this.size);
				}
				break;
			}
			byte[] array = null;
			int[] array2 = BarcodeEAN.GUARD_EMPTY;
			switch (this.codeType)
			{
			case 1:
				array = BarcodeEAN.GetBarsEAN13(this.code);
				array2 = BarcodeEAN.GUARD_EAN13;
				break;
			case 2:
				array = BarcodeEAN.GetBarsEAN8(this.code);
				array2 = BarcodeEAN.GUARD_EAN8;
				break;
			case 3:
				array = BarcodeEAN.GetBarsEAN13("0" + this.code);
				array2 = BarcodeEAN.GUARD_UPCA;
				break;
			case 4:
				array = BarcodeEAN.GetBarsUPCE(this.code);
				array2 = BarcodeEAN.GUARD_UPCE;
				break;
			case 5:
				array = BarcodeEAN.GetBarsSupplemental2(this.code);
				break;
			case 6:
				array = BarcodeEAN.GetBarsSupplemental5(this.code);
				break;
			}
			float num4 = num;
			bool flag = true;
			float num5 = 0f;
			if (this.font != null && this.baseline > 0f && this.guardBars)
			{
				num5 = this.baseline / 2f;
			}
			if (barColor != null)
			{
				cb.SetColorFill(barColor);
			}
			for (int i = 0; i < array.Length; i++)
			{
				float num6 = (float)array[i] * this.x;
				if (flag)
				{
					if (Array.BinarySearch<int>(array2, i) >= 0)
					{
						cb.Rectangle(num, num2 - num5, num6 - this.inkSpreading, this.barHeight + num5);
					}
					else
					{
						cb.Rectangle(num, num2, num6 - this.inkSpreading, this.barHeight);
					}
				}
				flag = !flag;
				num += num6;
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
				switch (this.codeType)
				{
				case 1:
					cb.SetTextMatrix(0f, num3);
					cb.ShowText(this.code.Substring(0, 1));
					for (int j = 1; j < 13; j++)
					{
						string text = this.code.Substring(j, 1);
						float widthPoint = this.font.GetWidthPoint(text, this.size);
						float x = num4 + BarcodeEAN.TEXTPOS_EAN13[j - 1] * this.x - widthPoint / 2f;
						cb.SetTextMatrix(x, num3);
						cb.ShowText(text);
					}
					break;
				case 2:
					for (int k = 0; k < 8; k++)
					{
						string text2 = this.code.Substring(k, 1);
						float widthPoint2 = this.font.GetWidthPoint(text2, this.size);
						float x2 = BarcodeEAN.TEXTPOS_EAN8[k] * this.x - widthPoint2 / 2f;
						cb.SetTextMatrix(x2, num3);
						cb.ShowText(text2);
					}
					break;
				case 3:
					cb.SetTextMatrix(0f, num3);
					cb.ShowText(this.code.Substring(0, 1));
					for (int l = 1; l < 11; l++)
					{
						string text3 = this.code.Substring(l, 1);
						float widthPoint3 = this.font.GetWidthPoint(text3, this.size);
						float x3 = num4 + BarcodeEAN.TEXTPOS_EAN13[l] * this.x - widthPoint3 / 2f;
						cb.SetTextMatrix(x3, num3);
						cb.ShowText(text3);
					}
					cb.SetTextMatrix(num4 + this.x * 95f, num3);
					cb.ShowText(this.code.Substring(11, 1));
					break;
				case 4:
					cb.SetTextMatrix(0f, num3);
					cb.ShowText(this.code.Substring(0, 1));
					for (int m = 1; m < 7; m++)
					{
						string text4 = this.code.Substring(m, 1);
						float widthPoint4 = this.font.GetWidthPoint(text4, this.size);
						float x4 = num4 + BarcodeEAN.TEXTPOS_EAN13[m - 1] * this.x - widthPoint4 / 2f;
						cb.SetTextMatrix(x4, num3);
						cb.ShowText(text4);
					}
					cb.SetTextMatrix(num4 + this.x * 51f, num3);
					cb.ShowText(this.code.Substring(7, 1));
					break;
				case 5:
				case 6:
					for (int n = 0; n < this.code.Length; n++)
					{
						string text5 = this.code.Substring(n, 1);
						float widthPoint5 = this.font.GetWidthPoint(text5, this.size);
						float x5 = (7.5f + (float)(9 * n)) * this.x - widthPoint5 / 2f;
						cb.SetTextMatrix(x5, num3);
						cb.ShowText(text5);
					}
					break;
				}
				cb.EndText();
			}
			return barcodeSize;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000C8F2C File Offset: 0x000C7F2C
		public override Image CreateDrawingImage(Color foreground, Color background)
		{
			byte[] array;
			int width;
			switch (this.codeType)
			{
			case 1:
				array = BarcodeEAN.GetBarsEAN13(this.code);
				width = 95;
				break;
			case 2:
				array = BarcodeEAN.GetBarsEAN8(this.code);
				width = 67;
				break;
			case 3:
				array = BarcodeEAN.GetBarsEAN13("0" + this.code);
				width = 95;
				break;
			case 4:
				array = BarcodeEAN.GetBarsUPCE(this.code);
				width = 51;
				break;
			case 5:
				array = BarcodeEAN.GetBarsSupplemental2(this.code);
				width = 20;
				break;
			case 6:
				array = BarcodeEAN.GetBarsSupplemental5(this.code);
				width = 47;
				break;
			default:
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("invalid.code.type"));
			}
			int num = (int)this.barHeight;
			Bitmap bitmap = new Bitmap(width, num);
			for (int i = 0; i < num; i++)
			{
				bool flag = true;
				int num2 = 0;
				foreach (int num3 in array)
				{
					Color color = background;
					if (flag)
					{
						color = foreground;
					}
					flag = !flag;
					for (int k = 0; k < num3; k++)
					{
						bitmap.SetPixel(num2++, i, color);
					}
				}
			}
			return bitmap;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000C9208 File Offset: 0x000C8208
		// Note: this type is marked as 'beforefieldinit'.
		static BarcodeEAN()
		{
			byte[][] array = new byte[10][];
			byte[][] array2 = array;
			int num = 0;
			byte[] array3 = new byte[6];
			array2[num] = array3;
			array[1] = new byte[]
			{
				0,
				0,
				1,
				0,
				1,
				1
			};
			array[2] = new byte[]
			{
				0,
				0,
				1,
				1,
				0,
				1
			};
			array[3] = new byte[]
			{
				0,
				0,
				1,
				1,
				1,
				0
			};
			array[4] = new byte[]
			{
				0,
				1,
				0,
				0,
				1,
				1
			};
			array[5] = new byte[]
			{
				0,
				1,
				1,
				0,
				0,
				1
			};
			array[6] = new byte[]
			{
				0,
				1,
				1,
				1,
				0,
				0
			};
			array[7] = new byte[]
			{
				0,
				1,
				0,
				1,
				0,
				1
			};
			array[8] = new byte[]
			{
				0,
				1,
				0,
				1,
				1,
				0
			};
			array[9] = new byte[]
			{
				0,
				1,
				1,
				0,
				1,
				0
			};
			BarcodeEAN.PARITY13 = array;
			byte[][] array4 = new byte[4][];
			byte[][] array5 = array4;
			int num2 = 0;
			byte[] array6 = new byte[2];
			array5[num2] = array6;
			array4[1] = new byte[]
			{
				0,
				1
			};
			byte[][] array7 = array4;
			int num3 = 2;
			byte[] array8 = new byte[2];
			array8[0] = 1;
			array7[num3] = array8;
			array4[3] = new byte[]
			{
				1,
				1
			};
			BarcodeEAN.PARITY2 = array4;
			byte[][] array9 = new byte[10][];
			byte[][] array10 = array9;
			int num4 = 0;
			byte[] array11 = new byte[5];
			array11[0] = 1;
			array11[1] = 1;
			array10[num4] = array11;
			byte[][] array12 = array9;
			int num5 = 1;
			byte[] array13 = new byte[5];
			array13[0] = 1;
			array13[2] = 1;
			array12[num5] = array13;
			byte[][] array14 = array9;
			int num6 = 2;
			byte[] array15 = new byte[5];
			array15[0] = 1;
			array15[3] = 1;
			array14[num6] = array15;
			array9[3] = new byte[]
			{
				1,
				0,
				0,
				0,
				1
			};
			byte[][] array16 = array9;
			int num7 = 4;
			byte[] array17 = new byte[5];
			array17[1] = 1;
			array17[2] = 1;
			array16[num7] = array17;
			byte[][] array18 = array9;
			int num8 = 5;
			byte[] array19 = new byte[5];
			array19[2] = 1;
			array19[3] = 1;
			array18[num8] = array19;
			array9[6] = new byte[]
			{
				0,
				0,
				0,
				1,
				1
			};
			byte[][] array20 = array9;
			int num9 = 7;
			byte[] array21 = new byte[5];
			array21[1] = 1;
			array21[3] = 1;
			array20[num9] = array21;
			array9[8] = new byte[]
			{
				0,
				1,
				0,
				0,
				1
			};
			array9[9] = new byte[]
			{
				0,
				0,
				1,
				0,
				1
			};
			BarcodeEAN.PARITY5 = array9;
			BarcodeEAN.PARITYE = new byte[][]
			{
				new byte[]
				{
					1,
					1,
					1,
					0,
					0,
					0
				},
				new byte[]
				{
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
					1,
					0,
					0,
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
					1
				},
				new byte[]
				{
					1,
					0,
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
					1,
					1,
					0
				},
				new byte[]
				{
					1,
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
					1,
					0
				},
				new byte[]
				{
					1,
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
					0,
					1,
					0,
					1
				}
			};
		}

		// Token: 0x040016DC RID: 5852
		private const int TOTALBARS_EAN13 = 59;

		// Token: 0x040016DD RID: 5853
		private const int TOTALBARS_EAN8 = 43;

		// Token: 0x040016DE RID: 5854
		private const int TOTALBARS_UPCE = 33;

		// Token: 0x040016DF RID: 5855
		private const int TOTALBARS_SUPP2 = 13;

		// Token: 0x040016E0 RID: 5856
		private const int TOTALBARS_SUPP5 = 31;

		// Token: 0x040016E1 RID: 5857
		private const byte ODD = 0;

		// Token: 0x040016E2 RID: 5858
		private const byte EVEN = 1;

		// Token: 0x040016E3 RID: 5859
		private static readonly int[] GUARD_EMPTY = new int[0];

		// Token: 0x040016E4 RID: 5860
		private static readonly int[] GUARD_UPCA = new int[]
		{
			0,
			2,
			4,
			6,
			28,
			30,
			52,
			54,
			56,
			58
		};

		// Token: 0x040016E5 RID: 5861
		private static readonly int[] GUARD_EAN13 = new int[]
		{
			0,
			2,
			28,
			30,
			56,
			58
		};

		// Token: 0x040016E6 RID: 5862
		private static readonly int[] GUARD_EAN8 = new int[]
		{
			0,
			2,
			20,
			22,
			40,
			42
		};

		// Token: 0x040016E7 RID: 5863
		private static readonly int[] GUARD_UPCE = new int[]
		{
			0,
			2,
			28,
			30,
			32
		};

		// Token: 0x040016E8 RID: 5864
		private static readonly float[] TEXTPOS_EAN13 = new float[]
		{
			6.5f,
			13.5f,
			20.5f,
			27.5f,
			34.5f,
			41.5f,
			53.5f,
			60.5f,
			67.5f,
			74.5f,
			81.5f,
			88.5f
		};

		// Token: 0x040016E9 RID: 5865
		private static readonly float[] TEXTPOS_EAN8 = new float[]
		{
			6.5f,
			13.5f,
			20.5f,
			27.5f,
			39.5f,
			46.5f,
			53.5f,
			60.5f
		};

		// Token: 0x040016EA RID: 5866
		private static readonly byte[][] BARS = new byte[][]
		{
			new byte[]
			{
				3,
				2,
				1,
				1
			},
			new byte[]
			{
				2,
				2,
				2,
				1
			},
			new byte[]
			{
				2,
				1,
				2,
				2
			},
			new byte[]
			{
				1,
				4,
				1,
				1
			},
			new byte[]
			{
				1,
				1,
				3,
				2
			},
			new byte[]
			{
				1,
				2,
				3,
				1
			},
			new byte[]
			{
				1,
				1,
				1,
				4
			},
			new byte[]
			{
				1,
				3,
				1,
				2
			},
			new byte[]
			{
				1,
				2,
				1,
				3
			},
			new byte[]
			{
				3,
				1,
				1,
				2
			}
		};

		// Token: 0x040016EB RID: 5867
		private static readonly byte[][] PARITY13;

		// Token: 0x040016EC RID: 5868
		private static readonly byte[][] PARITY2;

		// Token: 0x040016ED RID: 5869
		private static readonly byte[][] PARITY5;

		// Token: 0x040016EE RID: 5870
		private static readonly byte[][] PARITYE;
	}
}
