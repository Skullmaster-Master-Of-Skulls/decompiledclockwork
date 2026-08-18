using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using iTextSharp.text.pdf.codec;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000221 RID: 545
	public class BarcodeDatamatrix
	{
		// Token: 0x06001536 RID: 5430 RVA: 0x00076EA1 File Offset: 0x00075EA1
		private void SetBit(int x, int y, int xByte)
		{
			byte[] array = this.image;
			int num = y * xByte + x / 8;
			array[num] |= (byte)(128 >> (x & 7));
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00076ED0 File Offset: 0x00075ED0
		private void Draw(byte[] data, int dataSize, BarcodeDatamatrix.DmParams dm)
		{
			int xByte = (dm.width + this.ws * 2 + 7) / 8;
			for (int i = 0; i < this.image.Length; i++)
			{
				this.image[i] = 0;
			}
			for (int j = this.ws; j < dm.height + this.ws; j += dm.heightSection)
			{
				for (int k = this.ws; k < dm.width + this.ws; k += 2)
				{
					this.SetBit(k, j, xByte);
				}
			}
			for (int j = dm.heightSection - 1 + this.ws; j < dm.height + this.ws; j += dm.heightSection)
			{
				for (int k = this.ws; k < dm.width + this.ws; k++)
				{
					this.SetBit(k, j, xByte);
				}
			}
			for (int j = this.ws; j < dm.width + this.ws; j += dm.widthSection)
			{
				for (int k = this.ws; k < dm.height + this.ws; k++)
				{
					this.SetBit(j, k, xByte);
				}
			}
			for (int j = dm.widthSection - 1 + this.ws; j < dm.width + this.ws; j += dm.widthSection)
			{
				for (int k = 1 + this.ws; k < dm.height + this.ws; k += 2)
				{
					this.SetBit(j, k, xByte);
				}
			}
			int num = 0;
			for (int l = 0; l < dm.height; l += dm.heightSection)
			{
				for (int m = 1; m < dm.heightSection - 1; m++)
				{
					for (int n = 0; n < dm.width; n += dm.widthSection)
					{
						for (int num2 = 1; num2 < dm.widthSection - 1; num2++)
						{
							int num3 = (int)this.place[num++];
							if (num3 == 1 || (num3 > 1 && ((int)(data[num3 / 8 - 1] & 255) & 128 >> num3 % 8) != 0))
							{
								this.SetBit(num2 + n + this.ws, m + l + this.ws, xByte);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0007710C File Offset: 0x0007610C
		private static void MakePadding(byte[] data, int position, int count)
		{
			if (count <= 0)
			{
				return;
			}
			data[position++] = 129;
			while (--count > 0)
			{
				int num = 129 + (position + 1) * 149 % 253 + 1;
				if (num > 254)
				{
					num -= 254;
				}
				data[position++] = (byte)num;
			}
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00077168 File Offset: 0x00076168
		private static bool IsDigit(int c)
		{
			return c >= 48 && c <= 57;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0007717C File Offset: 0x0007617C
		private static int AsciiEncodation(byte[] text, int textOffset, int textLength, byte[] data, int dataOffset, int dataLength)
		{
			int i = textOffset;
			int num = dataOffset;
			textLength += textOffset;
			dataLength += dataOffset;
			while (i < textLength)
			{
				if (num >= dataLength)
				{
					return -1;
				}
				int num2 = (int)(text[i++] & byte.MaxValue);
				if (BarcodeDatamatrix.IsDigit(num2) && i < textLength && BarcodeDatamatrix.IsDigit((int)(text[i] & 255)))
				{
					data[num++] = (byte)((num2 - 48) * 10 + (int)(text[i++] & byte.MaxValue) - 48 + 130);
				}
				else if (num2 > 127)
				{
					if (num + 1 >= dataLength)
					{
						return -1;
					}
					data[num++] = 235;
					data[num++] = (byte)(num2 - 128 + 1);
				}
				else
				{
					data[num++] = (byte)(num2 + 1);
				}
			}
			return num - dataOffset;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0007723C File Offset: 0x0007623C
		private static int B256Encodation(byte[] text, int textOffset, int textLength, byte[] data, int dataOffset, int dataLength)
		{
			if (textLength == 0)
			{
				return 0;
			}
			if (textLength < 250 && textLength + 2 > dataLength)
			{
				return -1;
			}
			if (textLength >= 250 && textLength + 3 > dataLength)
			{
				return -1;
			}
			data[dataOffset] = 231;
			int num;
			if (textLength < 250)
			{
				data[dataOffset + 1] = (byte)textLength;
				num = 2;
			}
			else
			{
				data[dataOffset + 1] = (byte)(textLength / 250 + 249);
				data[dataOffset + 2] = (byte)(textLength % 250);
				num = 3;
			}
			Array.Copy(text, textOffset, data, num + dataOffset, textLength);
			num += textLength + dataOffset;
			for (int i = dataOffset + 1; i < num; i++)
			{
				int num2 = (int)(data[i] & byte.MaxValue);
				int num3 = 149 * (i + 1) % 255 + 1;
				int num4 = num2 + num3;
				if (num4 > 255)
				{
					num4 -= 256;
				}
				data[i] = (byte)num4;
			}
			return num - dataOffset;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00077310 File Offset: 0x00076310
		private static int X12Encodation(byte[] text, int textOffset, int textLength, byte[] data, int dataOffset, int dataLength)
		{
			if (textLength == 0)
			{
				return 0;
			}
			int i = 0;
			int num = 0;
			byte[] array = new byte[textLength];
			int num2 = 0;
			while (i < textLength)
			{
				int num3 = "\r*> 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf((char)text[i + textOffset]);
				if (num3 >= 0)
				{
					array[i] = (byte)num3;
					num2++;
				}
				else
				{
					array[i] = 100;
					if (num2 >= 6)
					{
						num2 -= num2 / 3 * 3;
					}
					for (int j = 0; j < num2; j++)
					{
						array[i - j - 1] = 100;
					}
					num2 = 0;
				}
				i++;
			}
			if (num2 >= 6)
			{
				num2 -= num2 / 3 * 3;
			}
			for (int j = 0; j < num2; j++)
			{
				array[i - j - 1] = 100;
			}
			byte b;
			for (i = 0; i < textLength; i++)
			{
				b = array[i];
				if (num >= dataLength)
				{
					break;
				}
				if (b < 40)
				{
					if (i == 0 || (i > 0 && array[i - 1] > 40))
					{
						data[dataOffset + num++] = 238;
					}
					if (num + 2 > dataLength)
					{
						break;
					}
					int num4 = 1600 * (int)array[i] + (int)(40 * array[i + 1]) + (int)array[i + 2] + 1;
					data[dataOffset + num++] = (byte)(num4 / 256);
					data[dataOffset + num++] = (byte)num4;
					i += 2;
				}
				else
				{
					if (i > 0 && array[i - 1] < 40)
					{
						data[dataOffset + num++] = 254;
					}
					int num5 = (int)(text[i + textOffset] & byte.MaxValue);
					if (num5 > 127)
					{
						data[dataOffset + num++] = 235;
						num5 -= 128;
					}
					if (num >= dataLength)
					{
						break;
					}
					data[dataOffset + num++] = (byte)(num5 + 1);
				}
			}
			b = 100;
			if (textLength > 0)
			{
				b = array[textLength - 1];
			}
			if (i != textLength || (b < 40 && num >= dataLength))
			{
				return -1;
			}
			if (b < 40)
			{
				data[dataOffset + num++] = 254;
			}
			return num;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000774D8 File Offset: 0x000764D8
		private static int EdifactEncodation(byte[] text, int textOffset, int textLength, byte[] data, int dataOffset, int dataLength)
		{
			if (textLength == 0)
			{
				return 0;
			}
			int i = 0;
			int num = 0;
			int num2 = 0;
			int num3 = 18;
			bool flag = true;
			while (i < textLength)
			{
				int num4 = (int)(text[i + textOffset] & byte.MaxValue);
				if (((num4 & 224) == 64 || (num4 & 224) == 32) && num4 != 95)
				{
					if (flag)
					{
						if (num + 1 > dataLength)
						{
							break;
						}
						data[dataOffset + num++] = 240;
						flag = false;
					}
					num4 &= 63;
					num2 |= num4 << num3;
					if (num3 == 0)
					{
						if (num + 3 > dataLength)
						{
							break;
						}
						data[dataOffset + num++] = (byte)(num2 >> 16);
						data[dataOffset + num++] = (byte)(num2 >> 8);
						data[dataOffset + num++] = (byte)num2;
						num2 = 0;
						num3 = 18;
					}
					else
					{
						num3 -= 6;
					}
				}
				else
				{
					if (!flag)
					{
						num2 |= 31 << num3;
						if (num + (3 - num3 / 8) > dataLength)
						{
							break;
						}
						data[dataOffset + num++] = (byte)(num2 >> 16);
						if (num3 <= 12)
						{
							data[dataOffset + num++] = (byte)(num2 >> 8);
						}
						if (num3 <= 6)
						{
							data[dataOffset + num++] = (byte)num2;
						}
						flag = true;
						num3 = 18;
						num2 = 0;
					}
					if (num4 > 127)
					{
						if (num >= dataLength)
						{
							break;
						}
						data[dataOffset + num++] = 235;
						num4 -= 128;
					}
					if (num >= dataLength)
					{
						break;
					}
					data[dataOffset + num++] = (byte)(num4 + 1);
				}
				i++;
			}
			if (i != textLength)
			{
				return -1;
			}
			if (!flag)
			{
				num2 |= 31 << num3;
				if (num + (3 - num3 / 8) > dataLength)
				{
					return -1;
				}
				data[dataOffset + num++] = (byte)(num2 >> 16);
				if (num3 <= 12)
				{
					data[dataOffset + num++] = (byte)(num2 >> 8);
				}
				if (num3 <= 6)
				{
					data[dataOffset + num++] = (byte)num2;
				}
			}
			return num;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00077690 File Offset: 0x00076690
		private static int C40OrTextEncodation(byte[] text, int textOffset, int textLength, byte[] data, int dataOffset, int dataLength, bool c40)
		{
			if (textLength == 0)
			{
				return 0;
			}
			int i = 0;
			int num = 0;
			if (c40)
			{
				data[dataOffset + num++] = 230;
			}
			else
			{
				data[dataOffset + num++] = 239;
			}
			string text2 = "!\"#$%&'()*+,-./:;<=>?@[\\]^_";
			string text3;
			string text4;
			if (c40)
			{
				text3 = " 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
				text4 = "`abcdefghijklmnopqrstuvwxyz{|}~\u007f";
			}
			else
			{
				text3 = " 0123456789abcdefghijklmnopqrstuvwxyz";
				text4 = "`ABCDEFGHIJKLMNOPQRSTUVWXYZ{|}~\u007f";
			}
			int[] array = new int[textLength * 4 + 10];
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			while (i < textLength)
			{
				if (num2 % 3 == 0)
				{
					num3 = i;
					num4 = num2;
				}
				int num5 = (int)(text[textOffset + i++] & byte.MaxValue);
				if (num5 > 127)
				{
					num5 -= 128;
					array[num2++] = 1;
					array[num2++] = 30;
				}
				int num6 = text3.IndexOf((char)num5);
				if (num6 >= 0)
				{
					array[num2++] = num6 + 3;
				}
				else if (num5 < 32)
				{
					array[num2++] = 0;
					array[num2++] = num5;
				}
				else if ((num6 = text2.IndexOf((char)num5)) >= 0)
				{
					array[num2++] = 1;
					array[num2++] = num6;
				}
				else if ((num6 = text4.IndexOf((char)num5)) >= 0)
				{
					array[num2++] = 2;
					array[num2++] = num6;
				}
			}
			if (num2 % 3 != 0)
			{
				i = num3;
				num2 = num4;
			}
			if (num2 / 3 * 2 > dataLength - 2)
			{
				return -1;
			}
			int j;
			for (j = 0; j < num2; j += 3)
			{
				int num7 = 1600 * array[j] + 40 * array[j + 1] + array[j + 2] + 1;
				data[dataOffset + num++] = (byte)(num7 / 256);
				data[dataOffset + num++] = (byte)num7;
			}
			data[num++] = 254;
			j = BarcodeDatamatrix.AsciiEncodation(text, i, textLength - i, data, num, dataLength - num);
			if (j < 0)
			{
				return j;
			}
			return num + j;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00077868 File Offset: 0x00076868
		private static int GetEncodation(byte[] text, int textOffset, int textSize, byte[] data, int dataOffset, int dataSize, int options, bool firstMatch)
		{
			int[] array = new int[6];
			if (dataSize < 0)
			{
				return -1;
			}
			options &= 7;
			if (options == 0)
			{
				array[0] = BarcodeDatamatrix.AsciiEncodation(text, textOffset, textSize, data, dataOffset, dataSize);
				if (firstMatch && array[0] >= 0)
				{
					return array[0];
				}
				array[1] = BarcodeDatamatrix.C40OrTextEncodation(text, textOffset, textSize, data, dataOffset, dataSize, false);
				if (firstMatch && array[1] >= 0)
				{
					return array[1];
				}
				array[2] = BarcodeDatamatrix.C40OrTextEncodation(text, textOffset, textSize, data, dataOffset, dataSize, true);
				if (firstMatch && array[2] >= 0)
				{
					return array[2];
				}
				array[3] = BarcodeDatamatrix.B256Encodation(text, textOffset, textSize, data, dataOffset, dataSize);
				if (firstMatch && array[3] >= 0)
				{
					return array[3];
				}
				array[4] = BarcodeDatamatrix.X12Encodation(text, textOffset, textSize, data, dataOffset, dataSize);
				if (firstMatch && array[4] >= 0)
				{
					return array[4];
				}
				array[5] = BarcodeDatamatrix.EdifactEncodation(text, textOffset, textSize, data, dataOffset, dataSize);
				if (firstMatch && array[5] >= 0)
				{
					return array[5];
				}
				if (array[0] < 0 && array[1] < 0 && array[2] < 0 && array[3] < 0 && array[4] < 0 && array[5] < 0)
				{
					return -1;
				}
				int num = 0;
				int num2 = 99999;
				for (int i = 0; i < 6; i++)
				{
					if (array[i] >= 0 && array[i] < num2)
					{
						num2 = array[i];
						num = i;
					}
				}
				if (num == 0)
				{
					num2 = BarcodeDatamatrix.AsciiEncodation(text, textOffset, textSize, data, dataOffset, dataSize);
				}
				else if (num == 1)
				{
					num2 = BarcodeDatamatrix.C40OrTextEncodation(text, textOffset, textSize, data, dataOffset, dataSize, false);
				}
				else if (num == 2)
				{
					num2 = BarcodeDatamatrix.C40OrTextEncodation(text, textOffset, textSize, data, dataOffset, dataSize, true);
				}
				else if (num == 3)
				{
					num2 = BarcodeDatamatrix.B256Encodation(text, textOffset, textSize, data, dataOffset, dataSize);
				}
				else if (num == 4)
				{
					num2 = BarcodeDatamatrix.X12Encodation(text, textOffset, textSize, data, dataOffset, dataSize);
				}
				return num2;
			}
			else
			{
				switch (options)
				{
				case 1:
					return BarcodeDatamatrix.AsciiEncodation(text, textOffset, textSize, data, dataOffset, dataSize);
				case 2:
					return BarcodeDatamatrix.C40OrTextEncodation(text, textOffset, textSize, data, dataOffset, dataSize, true);
				case 3:
					return BarcodeDatamatrix.C40OrTextEncodation(text, textOffset, textSize, data, dataOffset, dataSize, false);
				case 4:
					return BarcodeDatamatrix.B256Encodation(text, textOffset, textSize, data, dataOffset, dataSize);
				case 5:
					return BarcodeDatamatrix.X12Encodation(text, textOffset, textSize, data, dataOffset, dataSize);
				case 6:
					return BarcodeDatamatrix.EdifactEncodation(text, textOffset, textSize, data, dataOffset, dataSize);
				case 7:
					if (textSize > dataSize)
					{
						return -1;
					}
					Array.Copy(text, textOffset, data, dataOffset, textSize);
					return textSize;
				default:
					return -1;
				}
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00077A90 File Offset: 0x00076A90
		private static int GetNumber(byte[] text, int ptrIn, int n)
		{
			int num = 0;
			for (int i = 0; i < n; i++)
			{
				int num2 = (int)(text[ptrIn++] & byte.MaxValue);
				if (num2 < 48 || num2 > 57)
				{
					return -1;
				}
				num = num * 10 + num2 - 48;
			}
			return num;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00077AD4 File Offset: 0x00076AD4
		private int ProcessExtensions(byte[] text, int textOffset, int textSize, byte[] data)
		{
			if ((this.options & 32) == 0)
			{
				return 0;
			}
			int num = 0;
			int i = 0;
			int result = 0;
			while (i < textSize)
			{
				if (num > 20)
				{
					return -1;
				}
				int num2 = (int)(text[textOffset + i++] & byte.MaxValue);
				num++;
				int num3 = num2;
				if (num3 <= 102)
				{
					if (num3 == 46)
					{
						this.extOut = i;
						return result;
					}
					switch (num3)
					{
					case 101:
					{
						if (i + 6 > textSize)
						{
							return -1;
						}
						int number = BarcodeDatamatrix.GetNumber(text, textOffset + i, 6);
						if (number < 0)
						{
							return -1;
						}
						i += 6;
						data[result++] = 241;
						if (number < 127)
						{
							data[result++] = (byte)(number + 1);
						}
						else if (number < 16383)
						{
							data[result++] = (byte)((number - 127) / 254 + 128);
							data[result++] = (byte)((number - 127) % 254 + 1);
						}
						else
						{
							data[result++] = (byte)((number - 16383) / 64516 + 192);
							data[result++] = (byte)((number - 16383) / 254 % 254 + 1);
							data[result++] = (byte)((number - 16383) % 254 + 1);
						}
						break;
					}
					case 102:
						if (num != 1 && (num != 2 || (text[textOffset] != 115 && text[textOffset] != 109)))
						{
							return -1;
						}
						data[result++] = 232;
						break;
					}
				}
				else if (num3 != 109)
				{
					if (num3 != 112)
					{
						if (num3 == 115)
						{
							if (num != 1)
							{
								return -1;
							}
							if (i + 9 > textSize)
							{
								return -1;
							}
							int number2 = BarcodeDatamatrix.GetNumber(text, textOffset + i, 2);
							if (number2 <= 0 || number2 > 16)
							{
								return -1;
							}
							i += 2;
							int number3 = BarcodeDatamatrix.GetNumber(text, textOffset + i, 2);
							if (number3 <= 1 || number3 > 16)
							{
								return -1;
							}
							i += 2;
							int number4 = BarcodeDatamatrix.GetNumber(text, textOffset + i, 5);
							if (number4 < 0 || number2 >= 64516)
							{
								return -1;
							}
							i += 5;
							data[result++] = 233;
							data[result++] = (byte)(number2 - 1 << 4 | 17 - number3);
							data[result++] = (byte)(number4 / 254 + 1);
							data[result++] = (byte)(number4 % 254 + 1);
						}
					}
					else
					{
						if (num != 1)
						{
							return -1;
						}
						data[result++] = 234;
					}
				}
				else
				{
					if (num != 1)
					{
						return -1;
					}
					if (i + 1 > textSize)
					{
						return -1;
					}
					num2 = (int)(text[textOffset + i++] & byte.MaxValue);
					if (num2 != 53 && num2 != 53)
					{
						return -1;
					}
					data[result++] = 234;
					data[result++] = ((num2 == 53) ? 236 : 237);
				}
			}
			return -1;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00077D88 File Offset: 0x00076D88
		public int Generate(string text)
		{
			byte[] bytes = Encoding.GetEncoding(1252).GetBytes(text);
			return this.Generate(bytes, 0, bytes.Length);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00077DB4 File Offset: 0x00076DB4
		public int Generate(byte[] text, int textOffset, int textSize)
		{
			byte[] array = new byte[2500];
			this.extOut = 0;
			int num = this.ProcessExtensions(text, textOffset, textSize, array);
			if (num < 0)
			{
				return 5;
			}
			int num2;
			BarcodeDatamatrix.DmParams dmParams2;
			if (this.height == 0 || this.width == 0)
			{
				BarcodeDatamatrix.DmParams dmParams = BarcodeDatamatrix.dmSizes[BarcodeDatamatrix.dmSizes.Length - 1];
				num2 = BarcodeDatamatrix.GetEncodation(text, textOffset + this.extOut, textSize - this.extOut, array, num, dmParams.dataSize - num, this.options, false);
				if (num2 < 0)
				{
					return 1;
				}
				num2 += num;
				int num3 = 0;
				while (num3 < BarcodeDatamatrix.dmSizes.Length && BarcodeDatamatrix.dmSizes[num3].dataSize < num2)
				{
					num3++;
				}
				dmParams2 = BarcodeDatamatrix.dmSizes[num3];
				this.height = dmParams2.height;
				this.width = dmParams2.width;
			}
			else
			{
				int num3 = 0;
				while (num3 < BarcodeDatamatrix.dmSizes.Length && (this.height != BarcodeDatamatrix.dmSizes[num3].height || this.width != BarcodeDatamatrix.dmSizes[num3].width))
				{
					num3++;
				}
				if (num3 == BarcodeDatamatrix.dmSizes.Length)
				{
					return 3;
				}
				dmParams2 = BarcodeDatamatrix.dmSizes[num3];
				num2 = BarcodeDatamatrix.GetEncodation(text, textOffset + this.extOut, textSize - this.extOut, array, num, dmParams2.dataSize - num, this.options, true);
				if (num2 < 0)
				{
					return 1;
				}
				num2 += num;
			}
			if ((this.options & 64) != 0)
			{
				return 0;
			}
			this.image = new byte[(dmParams2.width + 2 * this.ws + 7) / 8 * (dmParams2.height + 2 * this.ws)];
			BarcodeDatamatrix.MakePadding(array, num2, dmParams2.dataSize - num2);
			this.place = BarcodeDatamatrix.Placement.DoPlacement(dmParams2.height - dmParams2.height / dmParams2.heightSection * 2, dmParams2.width - dmParams2.width / dmParams2.widthSection * 2);
			int dataSize = dmParams2.dataSize + (dmParams2.dataSize + 2) / dmParams2.dataBlock * dmParams2.errorBlock;
			BarcodeDatamatrix.ReedSolomon.GenerateECC(array, dmParams2.dataSize, dmParams2.dataBlock, dmParams2.errorBlock);
			this.Draw(array, dataSize, dmParams2);
			return 0;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00077FE4 File Offset: 0x00076FE4
		public Image CreateImage()
		{
			if (this.image == null)
			{
				return null;
			}
			byte[] data = CCITTG4Encoder.Compress(this.image, this.width + 2 * this.ws, this.height + 2 * this.ws);
			return Image.GetInstance(this.width + 2 * this.ws, this.height + 2 * this.ws, false, 256, 0, data, null);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00078054 File Offset: 0x00077054
		public virtual Image CreateDrawingImage(Color foreground, Color background)
		{
			if (this.image == null)
			{
				return null;
			}
			int num = this.height + 2 * this.ws;
			int num2 = this.width + 2 * this.ws;
			int num3 = (num2 + 7) / 8;
			Bitmap bitmap = new Bitmap(num2, num);
			for (int i = 0; i < num; i++)
			{
				int num4 = i * num3;
				for (int j = 0; j < num2; j++)
				{
					int num5 = (int)(this.image[num4 + j / 8] & byte.MaxValue);
					num5 <<= j % 8;
					bitmap.SetPixel(j, i, ((num5 & 128) == 0) ? background : foreground);
				}
			}
			return bitmap;
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x000780FA File Offset: 0x000770FA
		public byte[] BitImage
		{
			get
			{
				return this.image;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00078102 File Offset: 0x00077102
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x0007810A File Offset: 0x0007710A
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00078113 File Offset: 0x00077113
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x0007811B File Offset: 0x0007711B
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00078124 File Offset: 0x00077124
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x0007812C File Offset: 0x0007712C
		public int Ws
		{
			get
			{
				return this.ws;
			}
			set
			{
				this.ws = value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00078135 File Offset: 0x00077135
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x0007813D File Offset: 0x0007713D
		public int Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x04000E45 RID: 3653
		public const int DM_NO_ERROR = 0;

		// Token: 0x04000E46 RID: 3654
		public const int DM_ERROR_TEXT_TOO_BIG = 1;

		// Token: 0x04000E47 RID: 3655
		public const int DM_ERROR_INVALID_SQUARE = 3;

		// Token: 0x04000E48 RID: 3656
		public const int DM_ERROR_EXTENSION = 5;

		// Token: 0x04000E49 RID: 3657
		public const int DM_AUTO = 0;

		// Token: 0x04000E4A RID: 3658
		public const int DM_ASCII = 1;

		// Token: 0x04000E4B RID: 3659
		public const int DM_C40 = 2;

		// Token: 0x04000E4C RID: 3660
		public const int DM_TEXT = 3;

		// Token: 0x04000E4D RID: 3661
		public const int DM_B256 = 4;

		// Token: 0x04000E4E RID: 3662
		public const int DM_X21 = 5;

		// Token: 0x04000E4F RID: 3663
		public const int DM_EDIFACT = 6;

		// Token: 0x04000E50 RID: 3664
		public const int DM_RAW = 7;

		// Token: 0x04000E51 RID: 3665
		public const int DM_EXTENSION = 32;

		// Token: 0x04000E52 RID: 3666
		public const int DM_TEST = 64;

		// Token: 0x04000E53 RID: 3667
		private const string x12 = "\r*> 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		// Token: 0x04000E54 RID: 3668
		private static readonly BarcodeDatamatrix.DmParams[] dmSizes = new BarcodeDatamatrix.DmParams[]
		{
			new BarcodeDatamatrix.DmParams(10, 10, 10, 10, 3, 3, 5),
			new BarcodeDatamatrix.DmParams(12, 12, 12, 12, 5, 5, 7),
			new BarcodeDatamatrix.DmParams(8, 18, 8, 18, 5, 5, 7),
			new BarcodeDatamatrix.DmParams(14, 14, 14, 14, 8, 8, 10),
			new BarcodeDatamatrix.DmParams(8, 32, 8, 16, 10, 10, 11),
			new BarcodeDatamatrix.DmParams(16, 16, 16, 16, 12, 12, 12),
			new BarcodeDatamatrix.DmParams(12, 26, 12, 26, 16, 16, 14),
			new BarcodeDatamatrix.DmParams(18, 18, 18, 18, 18, 18, 14),
			new BarcodeDatamatrix.DmParams(20, 20, 20, 20, 22, 22, 18),
			new BarcodeDatamatrix.DmParams(12, 36, 12, 18, 22, 22, 18),
			new BarcodeDatamatrix.DmParams(22, 22, 22, 22, 30, 30, 20),
			new BarcodeDatamatrix.DmParams(16, 36, 16, 18, 32, 32, 24),
			new BarcodeDatamatrix.DmParams(24, 24, 24, 24, 36, 36, 24),
			new BarcodeDatamatrix.DmParams(26, 26, 26, 26, 44, 44, 28),
			new BarcodeDatamatrix.DmParams(16, 48, 16, 24, 49, 49, 28),
			new BarcodeDatamatrix.DmParams(32, 32, 16, 16, 62, 62, 36),
			new BarcodeDatamatrix.DmParams(36, 36, 18, 18, 86, 86, 42),
			new BarcodeDatamatrix.DmParams(40, 40, 20, 20, 114, 114, 48),
			new BarcodeDatamatrix.DmParams(44, 44, 22, 22, 144, 144, 56),
			new BarcodeDatamatrix.DmParams(48, 48, 24, 24, 174, 174, 68),
			new BarcodeDatamatrix.DmParams(52, 52, 26, 26, 204, 102, 42),
			new BarcodeDatamatrix.DmParams(64, 64, 16, 16, 280, 140, 56),
			new BarcodeDatamatrix.DmParams(72, 72, 18, 18, 368, 92, 36),
			new BarcodeDatamatrix.DmParams(80, 80, 20, 20, 456, 114, 48),
			new BarcodeDatamatrix.DmParams(88, 88, 22, 22, 576, 144, 56),
			new BarcodeDatamatrix.DmParams(96, 96, 24, 24, 696, 174, 68),
			new BarcodeDatamatrix.DmParams(104, 104, 26, 26, 816, 136, 56),
			new BarcodeDatamatrix.DmParams(120, 120, 20, 20, 1050, 175, 68),
			new BarcodeDatamatrix.DmParams(132, 132, 22, 22, 1304, 163, 62),
			new BarcodeDatamatrix.DmParams(144, 144, 24, 24, 1558, 156, 62)
		};

		// Token: 0x04000E55 RID: 3669
		private int extOut;

		// Token: 0x04000E56 RID: 3670
		private short[] place;

		// Token: 0x04000E57 RID: 3671
		private byte[] image;

		// Token: 0x04000E58 RID: 3672
		private int height;

		// Token: 0x04000E59 RID: 3673
		private int width;

		// Token: 0x04000E5A RID: 3674
		private int ws;

		// Token: 0x04000E5B RID: 3675
		private int options;

		// Token: 0x02000222 RID: 546
		private class DmParams
		{
			// Token: 0x06001550 RID: 5456 RVA: 0x00078448 File Offset: 0x00077448
			internal DmParams(int height, int width, int heightSection, int widthSection, int dataSize, int dataBlock, int errorBlock)
			{
				this.height = height;
				this.width = width;
				this.heightSection = heightSection;
				this.widthSection = widthSection;
				this.dataSize = dataSize;
				this.dataBlock = dataBlock;
				this.errorBlock = errorBlock;
			}

			// Token: 0x04000E5C RID: 3676
			internal int height;

			// Token: 0x04000E5D RID: 3677
			internal int width;

			// Token: 0x04000E5E RID: 3678
			internal int heightSection;

			// Token: 0x04000E5F RID: 3679
			internal int widthSection;

			// Token: 0x04000E60 RID: 3680
			internal int dataSize;

			// Token: 0x04000E61 RID: 3681
			internal int dataBlock;

			// Token: 0x04000E62 RID: 3682
			internal int errorBlock;
		}

		// Token: 0x02000223 RID: 547
		internal class Placement
		{
			// Token: 0x06001551 RID: 5457 RVA: 0x00078485 File Offset: 0x00077485
			private Placement()
			{
			}

			// Token: 0x06001552 RID: 5458 RVA: 0x00078490 File Offset: 0x00077490
			internal static short[] DoPlacement(int nrow, int ncol)
			{
				int key = nrow * 1000 + ncol;
				lock (BarcodeDatamatrix.Placement.cache)
				{
					short[] result;
					if (BarcodeDatamatrix.Placement.cache.TryGetValue(key, out result))
					{
						return result;
					}
				}
				BarcodeDatamatrix.Placement placement = new BarcodeDatamatrix.Placement();
				placement.nrow = nrow;
				placement.ncol = ncol;
				placement.array = new short[nrow * ncol];
				placement.Ecc200();
				lock (BarcodeDatamatrix.Placement.cache)
				{
					BarcodeDatamatrix.Placement.cache[key] = placement.array;
				}
				return placement.array;
			}

			// Token: 0x06001553 RID: 5459 RVA: 0x00078548 File Offset: 0x00077548
			private void Module(int row, int col, int chr, int bit)
			{
				if (row < 0)
				{
					row += this.nrow;
					col += 4 - (this.nrow + 4) % 8;
				}
				if (col < 0)
				{
					col += this.ncol;
					row += 4 - (this.ncol + 4) % 8;
				}
				this.array[row * this.ncol + col] = (short)(8 * chr + bit);
			}

			// Token: 0x06001554 RID: 5460 RVA: 0x000785AC File Offset: 0x000775AC
			private void Utah(int row, int col, int chr)
			{
				this.Module(row - 2, col - 2, chr, 0);
				this.Module(row - 2, col - 1, chr, 1);
				this.Module(row - 1, col - 2, chr, 2);
				this.Module(row - 1, col - 1, chr, 3);
				this.Module(row - 1, col, chr, 4);
				this.Module(row, col - 2, chr, 5);
				this.Module(row, col - 1, chr, 6);
				this.Module(row, col, chr, 7);
			}

			// Token: 0x06001555 RID: 5461 RVA: 0x00078620 File Offset: 0x00077620
			private void Corner1(int chr)
			{
				this.Module(this.nrow - 1, 0, chr, 0);
				this.Module(this.nrow - 1, 1, chr, 1);
				this.Module(this.nrow - 1, 2, chr, 2);
				this.Module(0, this.ncol - 2, chr, 3);
				this.Module(0, this.ncol - 1, chr, 4);
				this.Module(1, this.ncol - 1, chr, 5);
				this.Module(2, this.ncol - 1, chr, 6);
				this.Module(3, this.ncol - 1, chr, 7);
			}

			// Token: 0x06001556 RID: 5462 RVA: 0x000786B8 File Offset: 0x000776B8
			private void Corner2(int chr)
			{
				this.Module(this.nrow - 3, 0, chr, 0);
				this.Module(this.nrow - 2, 0, chr, 1);
				this.Module(this.nrow - 1, 0, chr, 2);
				this.Module(0, this.ncol - 4, chr, 3);
				this.Module(0, this.ncol - 3, chr, 4);
				this.Module(0, this.ncol - 2, chr, 5);
				this.Module(0, this.ncol - 1, chr, 6);
				this.Module(1, this.ncol - 1, chr, 7);
			}

			// Token: 0x06001557 RID: 5463 RVA: 0x00078750 File Offset: 0x00077750
			private void Corner3(int chr)
			{
				this.Module(this.nrow - 3, 0, chr, 0);
				this.Module(this.nrow - 2, 0, chr, 1);
				this.Module(this.nrow - 1, 0, chr, 2);
				this.Module(0, this.ncol - 2, chr, 3);
				this.Module(0, this.ncol - 1, chr, 4);
				this.Module(1, this.ncol - 1, chr, 5);
				this.Module(2, this.ncol - 1, chr, 6);
				this.Module(3, this.ncol - 1, chr, 7);
			}

			// Token: 0x06001558 RID: 5464 RVA: 0x000787E8 File Offset: 0x000777E8
			private void Corner4(int chr)
			{
				this.Module(this.nrow - 1, 0, chr, 0);
				this.Module(this.nrow - 1, this.ncol - 1, chr, 1);
				this.Module(0, this.ncol - 3, chr, 2);
				this.Module(0, this.ncol - 2, chr, 3);
				this.Module(0, this.ncol - 1, chr, 4);
				this.Module(1, this.ncol - 3, chr, 5);
				this.Module(1, this.ncol - 2, chr, 6);
				this.Module(1, this.ncol - 1, chr, 7);
			}

			// Token: 0x06001559 RID: 5465 RVA: 0x00078884 File Offset: 0x00077884
			private void Ecc200()
			{
				for (int i = 0; i < this.array.Length; i++)
				{
					this.array[i] = 0;
				}
				int num = 1;
				int num2 = 4;
				int num3 = 0;
				do
				{
					if (num2 == this.nrow && num3 == 0)
					{
						this.Corner1(num++);
					}
					if (num2 == this.nrow - 2 && num3 == 0 && this.ncol % 4 != 0)
					{
						this.Corner2(num++);
					}
					if (num2 == this.nrow - 2 && num3 == 0 && this.ncol % 8 == 4)
					{
						this.Corner3(num++);
					}
					if (num2 == this.nrow + 4 && num3 == 2 && this.ncol % 8 == 0)
					{
						this.Corner4(num++);
					}
					do
					{
						if (num2 < this.nrow && num3 >= 0 && this.array[num2 * this.ncol + num3] == 0)
						{
							this.Utah(num2, num3, num++);
						}
						num2 -= 2;
						num3 += 2;
					}
					while (num2 >= 0 && num3 < this.ncol);
					num2++;
					num3 += 3;
					do
					{
						if (num2 >= 0 && num3 < this.ncol && this.array[num2 * this.ncol + num3] == 0)
						{
							this.Utah(num2, num3, num++);
						}
						num2 += 2;
						num3 -= 2;
					}
					while (num2 < this.nrow && num3 >= 0);
					num2 += 3;
					num3++;
				}
				while (num2 < this.nrow || num3 < this.ncol);
				if (this.array[this.nrow * this.ncol - 1] == 0)
				{
					this.array[this.nrow * this.ncol - 1] = (this.array[this.nrow * this.ncol - this.ncol - 2] = 1);
				}
			}

			// Token: 0x04000E63 RID: 3683
			private int nrow;

			// Token: 0x04000E64 RID: 3684
			private int ncol;

			// Token: 0x04000E65 RID: 3685
			private short[] array;

			// Token: 0x04000E66 RID: 3686
			private static Dictionary<int, short[]> cache = new Dictionary<int, short[]>();
		}

		// Token: 0x02000224 RID: 548
		internal class ReedSolomon
		{
			// Token: 0x0600155B RID: 5467 RVA: 0x00078A40 File Offset: 0x00077A40
			private static int[] GetPoly(int nc)
			{
				if (nc <= 36)
				{
					if (nc <= 20)
					{
						switch (nc)
						{
						case 5:
							return BarcodeDatamatrix.ReedSolomon.poly5;
						case 6:
						case 8:
						case 9:
						case 13:
							break;
						case 7:
							return BarcodeDatamatrix.ReedSolomon.poly7;
						case 10:
							return BarcodeDatamatrix.ReedSolomon.poly10;
						case 11:
							return BarcodeDatamatrix.ReedSolomon.poly11;
						case 12:
							return BarcodeDatamatrix.ReedSolomon.poly12;
						case 14:
							return BarcodeDatamatrix.ReedSolomon.poly14;
						default:
							switch (nc)
							{
							case 18:
								return BarcodeDatamatrix.ReedSolomon.poly18;
							case 20:
								return BarcodeDatamatrix.ReedSolomon.poly20;
							}
							break;
						}
					}
					else
					{
						if (nc == 24)
						{
							return BarcodeDatamatrix.ReedSolomon.poly24;
						}
						if (nc == 28)
						{
							return BarcodeDatamatrix.ReedSolomon.poly28;
						}
						if (nc == 36)
						{
							return BarcodeDatamatrix.ReedSolomon.poly36;
						}
					}
				}
				else if (nc <= 48)
				{
					if (nc == 42)
					{
						return BarcodeDatamatrix.ReedSolomon.poly42;
					}
					if (nc == 48)
					{
						return BarcodeDatamatrix.ReedSolomon.poly48;
					}
				}
				else
				{
					if (nc == 56)
					{
						return BarcodeDatamatrix.ReedSolomon.poly56;
					}
					if (nc == 62)
					{
						return BarcodeDatamatrix.ReedSolomon.poly62;
					}
					if (nc == 68)
					{
						return BarcodeDatamatrix.ReedSolomon.poly68;
					}
				}
				return null;
			}

			// Token: 0x0600155C RID: 5468 RVA: 0x00078B3C File Offset: 0x00077B3C
			private static void ReedSolomonBlock(byte[] wd, int nd, byte[] ncout, int nc, int[] c)
			{
				for (int i = 0; i <= nc; i++)
				{
					ncout[i] = 0;
				}
				for (int i = 0; i < nd; i++)
				{
					int num = (int)((ncout[0] ^ wd[i]) & byte.MaxValue);
					for (int j = 0; j < nc; j++)
					{
						ncout[j] = (ncout[j + 1] ^ ((num == 0) ? 0 : ((byte)BarcodeDatamatrix.ReedSolomon.alog[(BarcodeDatamatrix.ReedSolomon.log[num] + BarcodeDatamatrix.ReedSolomon.log[c[nc - j - 1]]) % 255])));
					}
				}
			}

			// Token: 0x0600155D RID: 5469 RVA: 0x00078BB4 File Offset: 0x00077BB4
			internal static void GenerateECC(byte[] wd, int nd, int datablock, int nc)
			{
				int num = (nd + 2) / datablock;
				byte[] array = new byte[256];
				byte[] array2 = new byte[256];
				int[] poly = BarcodeDatamatrix.ReedSolomon.GetPoly(nc);
				for (int i = 0; i < num; i++)
				{
					int nd2 = 0;
					for (int j = i; j < nd; j += num)
					{
						array[nd2++] = wd[j];
					}
					BarcodeDatamatrix.ReedSolomon.ReedSolomonBlock(array, nd2, array2, nc, poly);
					nd2 = 0;
					for (int j = i; j < nc * num; j += num)
					{
						wd[nd + j] = array2[nd2++];
					}
				}
			}

			// Token: 0x04000E67 RID: 3687
			private static readonly int[] log = new int[]
			{
				0,
				255,
				1,
				240,
				2,
				225,
				241,
				53,
				3,
				38,
				226,
				133,
				242,
				43,
				54,
				210,
				4,
				195,
				39,
				114,
				227,
				106,
				134,
				28,
				243,
				140,
				44,
				23,
				55,
				118,
				211,
				234,
				5,
				219,
				196,
				96,
				40,
				222,
				115,
				103,
				228,
				78,
				107,
				125,
				135,
				8,
				29,
				162,
				244,
				186,
				141,
				180,
				45,
				99,
				24,
				49,
				56,
				13,
				119,
				153,
				212,
				199,
				235,
				91,
				6,
				76,
				220,
				217,
				197,
				11,
				97,
				184,
				41,
				36,
				223,
				253,
				116,
				138,
				104,
				193,
				229,
				86,
				79,
				171,
				108,
				165,
				126,
				145,
				136,
				34,
				9,
				74,
				30,
				32,
				163,
				84,
				245,
				173,
				187,
				204,
				142,
				81,
				181,
				190,
				46,
				88,
				100,
				159,
				25,
				231,
				50,
				207,
				57,
				147,
				14,
				67,
				120,
				128,
				154,
				248,
				213,
				167,
				200,
				63,
				236,
				110,
				92,
				176,
				7,
				161,
				77,
				124,
				221,
				102,
				218,
				95,
				198,
				90,
				12,
				152,
				98,
				48,
				185,
				179,
				42,
				209,
				37,
				132,
				224,
				52,
				254,
				239,
				117,
				233,
				139,
				22,
				105,
				27,
				194,
				113,
				230,
				206,
				87,
				158,
				80,
				189,
				172,
				203,
				109,
				175,
				166,
				62,
				127,
				247,
				146,
				66,
				137,
				192,
				35,
				252,
				10,
				183,
				75,
				216,
				31,
				83,
				33,
				73,
				164,
				144,
				85,
				170,
				246,
				65,
				174,
				61,
				188,
				202,
				205,
				157,
				143,
				169,
				82,
				72,
				182,
				215,
				191,
				251,
				47,
				178,
				89,
				151,
				101,
				94,
				160,
				123,
				26,
				112,
				232,
				21,
				51,
				238,
				208,
				131,
				58,
				69,
				148,
				18,
				15,
				16,
				68,
				17,
				121,
				149,
				129,
				19,
				155,
				59,
				249,
				70,
				214,
				250,
				168,
				71,
				201,
				156,
				64,
				60,
				237,
				130,
				111,
				20,
				93,
				122,
				177,
				150
			};

			// Token: 0x04000E68 RID: 3688
			private static readonly int[] alog = new int[]
			{
				1,
				2,
				4,
				8,
				16,
				32,
				64,
				128,
				45,
				90,
				180,
				69,
				138,
				57,
				114,
				228,
				229,
				231,
				227,
				235,
				251,
				219,
				155,
				27,
				54,
				108,
				216,
				157,
				23,
				46,
				92,
				184,
				93,
				186,
				89,
				178,
				73,
				146,
				9,
				18,
				36,
				72,
				144,
				13,
				26,
				52,
				104,
				208,
				141,
				55,
				110,
				220,
				149,
				7,
				14,
				28,
				56,
				112,
				224,
				237,
				247,
				195,
				171,
				123,
				246,
				193,
				175,
				115,
				230,
				225,
				239,
				243,
				203,
				187,
				91,
				182,
				65,
				130,
				41,
				82,
				164,
				101,
				202,
				185,
				95,
				190,
				81,
				162,
				105,
				210,
				137,
				63,
				126,
				252,
				213,
				135,
				35,
				70,
				140,
				53,
				106,
				212,
				133,
				39,
				78,
				156,
				21,
				42,
				84,
				168,
				125,
				250,
				217,
				159,
				19,
				38,
				76,
				152,
				29,
				58,
				116,
				232,
				253,
				215,
				131,
				43,
				86,
				172,
				117,
				234,
				249,
				223,
				147,
				11,
				22,
				44,
				88,
				176,
				77,
				154,
				25,
				50,
				100,
				200,
				189,
				87,
				174,
				113,
				226,
				233,
				255,
				211,
				139,
				59,
				118,
				236,
				245,
				199,
				163,
				107,
				214,
				129,
				47,
				94,
				188,
				85,
				170,
				121,
				242,
				201,
				191,
				83,
				166,
				97,
				194,
				169,
				127,
				254,
				209,
				143,
				51,
				102,
				204,
				181,
				71,
				142,
				49,
				98,
				196,
				165,
				103,
				206,
				177,
				79,
				158,
				17,
				34,
				68,
				136,
				61,
				122,
				244,
				197,
				167,
				99,
				198,
				161,
				111,
				222,
				145,
				15,
				30,
				60,
				120,
				240,
				205,
				183,
				67,
				134,
				33,
				66,
				132,
				37,
				74,
				148,
				5,
				10,
				20,
				40,
				80,
				160,
				109,
				218,
				153,
				31,
				62,
				124,
				248,
				221,
				151,
				3,
				6,
				12,
				24,
				48,
				96,
				192,
				173,
				119,
				238,
				241,
				207,
				179,
				75,
				150,
				1
			};

			// Token: 0x04000E69 RID: 3689
			private static readonly int[] poly5 = new int[]
			{
				228,
				48,
				15,
				111,
				62
			};

			// Token: 0x04000E6A RID: 3690
			private static readonly int[] poly7 = new int[]
			{
				23,
				68,
				144,
				134,
				240,
				92,
				254
			};

			// Token: 0x04000E6B RID: 3691
			private static readonly int[] poly10 = new int[]
			{
				28,
				24,
				185,
				166,
				223,
				248,
				116,
				255,
				110,
				61
			};

			// Token: 0x04000E6C RID: 3692
			private static readonly int[] poly11 = new int[]
			{
				175,
				138,
				205,
				12,
				194,
				168,
				39,
				245,
				60,
				97,
				120
			};

			// Token: 0x04000E6D RID: 3693
			private static readonly int[] poly12 = new int[]
			{
				41,
				153,
				158,
				91,
				61,
				42,
				142,
				213,
				97,
				178,
				100,
				242
			};

			// Token: 0x04000E6E RID: 3694
			private static readonly int[] poly14 = new int[]
			{
				156,
				97,
				192,
				252,
				95,
				9,
				157,
				119,
				138,
				45,
				18,
				186,
				83,
				185
			};

			// Token: 0x04000E6F RID: 3695
			private static readonly int[] poly18 = new int[]
			{
				83,
				195,
				100,
				39,
				188,
				75,
				66,
				61,
				241,
				213,
				109,
				129,
				94,
				254,
				225,
				48,
				90,
				188
			};

			// Token: 0x04000E70 RID: 3696
			private static readonly int[] poly20 = new int[]
			{
				15,
				195,
				244,
				9,
				233,
				71,
				168,
				2,
				188,
				160,
				153,
				145,
				253,
				79,
				108,
				82,
				27,
				174,
				186,
				172
			};

			// Token: 0x04000E71 RID: 3697
			private static readonly int[] poly24 = new int[]
			{
				52,
				190,
				88,
				205,
				109,
				39,
				176,
				21,
				155,
				197,
				251,
				223,
				155,
				21,
				5,
				172,
				254,
				124,
				12,
				181,
				184,
				96,
				50,
				193
			};

			// Token: 0x04000E72 RID: 3698
			private static readonly int[] poly28 = new int[]
			{
				211,
				231,
				43,
				97,
				71,
				96,
				103,
				174,
				37,
				151,
				170,
				53,
				75,
				34,
				249,
				121,
				17,
				138,
				110,
				213,
				141,
				136,
				120,
				151,
				233,
				168,
				93,
				255
			};

			// Token: 0x04000E73 RID: 3699
			private static readonly int[] poly36 = new int[]
			{
				245,
				127,
				242,
				218,
				130,
				250,
				162,
				181,
				102,
				120,
				84,
				179,
				220,
				251,
				80,
				182,
				229,
				18,
				2,
				4,
				68,
				33,
				101,
				137,
				95,
				119,
				115,
				44,
				175,
				184,
				59,
				25,
				225,
				98,
				81,
				112
			};

			// Token: 0x04000E74 RID: 3700
			private static readonly int[] poly42 = new int[]
			{
				77,
				193,
				137,
				31,
				19,
				38,
				22,
				153,
				247,
				105,
				122,
				2,
				245,
				133,
				242,
				8,
				175,
				95,
				100,
				9,
				167,
				105,
				214,
				111,
				57,
				121,
				21,
				1,
				253,
				57,
				54,
				101,
				248,
				202,
				69,
				50,
				150,
				177,
				226,
				5,
				9,
				5
			};

			// Token: 0x04000E75 RID: 3701
			private static readonly int[] poly48 = new int[]
			{
				245,
				132,
				172,
				223,
				96,
				32,
				117,
				22,
				238,
				133,
				238,
				231,
				205,
				188,
				237,
				87,
				191,
				106,
				16,
				147,
				118,
				23,
				37,
				90,
				170,
				205,
				131,
				88,
				120,
				100,
				66,
				138,
				186,
				240,
				82,
				44,
				176,
				87,
				187,
				147,
				160,
				175,
				69,
				213,
				92,
				253,
				225,
				19
			};

			// Token: 0x04000E76 RID: 3702
			private static readonly int[] poly56 = new int[]
			{
				175,
				9,
				223,
				238,
				12,
				17,
				220,
				208,
				100,
				29,
				175,
				170,
				230,
				192,
				215,
				235,
				150,
				159,
				36,
				223,
				38,
				200,
				132,
				54,
				228,
				146,
				218,
				234,
				117,
				203,
				29,
				232,
				144,
				238,
				22,
				150,
				201,
				117,
				62,
				207,
				164,
				13,
				137,
				245,
				127,
				67,
				247,
				28,
				155,
				43,
				203,
				107,
				233,
				53,
				143,
				46
			};

			// Token: 0x04000E77 RID: 3703
			private static readonly int[] poly62 = new int[]
			{
				242,
				93,
				169,
				50,
				144,
				210,
				39,
				118,
				202,
				188,
				201,
				189,
				143,
				108,
				196,
				37,
				185,
				112,
				134,
				230,
				245,
				63,
				197,
				190,
				250,
				106,
				185,
				221,
				175,
				64,
				114,
				71,
				161,
				44,
				147,
				6,
				27,
				218,
				51,
				63,
				87,
				10,
				40,
				130,
				188,
				17,
				163,
				31,
				176,
				170,
				4,
				107,
				232,
				7,
				94,
				166,
				224,
				124,
				86,
				47,
				11,
				204
			};

			// Token: 0x04000E78 RID: 3704
			private static readonly int[] poly68 = new int[]
			{
				220,
				228,
				173,
				89,
				251,
				149,
				159,
				56,
				89,
				33,
				147,
				244,
				154,
				36,
				73,
				127,
				213,
				136,
				248,
				180,
				234,
				197,
				158,
				177,
				68,
				122,
				93,
				213,
				15,
				160,
				227,
				236,
				66,
				139,
				153,
				185,
				202,
				167,
				179,
				25,
				220,
				232,
				96,
				210,
				231,
				136,
				223,
				239,
				181,
				241,
				59,
				52,
				172,
				25,
				49,
				232,
				211,
				189,
				64,
				54,
				108,
				153,
				132,
				63,
				96,
				103,
				82,
				186
			};
		}
	}
}
