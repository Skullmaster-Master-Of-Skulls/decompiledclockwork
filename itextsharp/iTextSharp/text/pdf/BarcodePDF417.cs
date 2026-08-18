using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.codec;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200016E RID: 366
	public class BarcodePDF417
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x0004CB4A File Offset: 0x0004BB4A
		public BarcodePDF417()
		{
			this.SetDefaultParameters();
		}

		// Token: 0x170002A5 RID: 677
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0004CB6F File Offset: 0x0004BB6F
		public int MacroSegmentId
		{
			set
			{
				this.macroSegmentId = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x0004CB78 File Offset: 0x0004BB78
		public int MacroSegmentCount
		{
			set
			{
				this.macroSegmentCount = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0004CB81 File Offset: 0x0004BB81
		public string MacroFileId
		{
			set
			{
				this.macroFileId = value;
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0004CB8A File Offset: 0x0004BB8A
		protected bool CheckSegmentType(BarcodePDF417.Segment segment, char type)
		{
			return segment != null && segment.type == type;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0004CB9A File Offset: 0x0004BB9A
		protected int GetSegmentLength(BarcodePDF417.Segment segment)
		{
			if (segment == null)
			{
				return 0;
			}
			return segment.end - segment.start;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0004CBAE File Offset: 0x0004BBAE
		public void SetDefaultParameters()
		{
			this.options = 0;
			this.outBits = null;
			this.text = new byte[0];
			this.yHeight = 3f;
			this.aspectRatio = 0.5f;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0004CBE0 File Offset: 0x0004BBE0
		protected void OutCodeword17(int codeword)
		{
			int num = this.bitPtr / 8;
			int num2 = this.bitPtr - num * 8;
			byte[] array = this.outBits;
			int num3 = num++;
			array[num3] |= (byte)(codeword >> 9 + num2);
			byte[] array2 = this.outBits;
			int num4 = num++;
			array2[num4] |= (byte)(codeword >> 1 + num2);
			codeword <<= 8;
			byte[] array3 = this.outBits;
			int num5 = num;
			array3[num5] |= (byte)(codeword >> 1 + num2);
			this.bitPtr += 17;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0004CC84 File Offset: 0x0004BC84
		protected void OutCodeword18(int codeword)
		{
			int num = this.bitPtr / 8;
			int num2 = this.bitPtr - num * 8;
			byte[] array = this.outBits;
			int num3 = num++;
			array[num3] |= (byte)(codeword >> 10 + num2);
			byte[] array2 = this.outBits;
			int num4 = num++;
			array2[num4] |= (byte)(codeword >> 2 + num2);
			codeword <<= 8;
			byte[] array3 = this.outBits;
			int num5 = num;
			array3[num5] |= (byte)(codeword >> 2 + num2);
			if (num2 == 7)
			{
				byte[] array4 = this.outBits;
				int num6 = num + 1;
				array4[num6] |= 128;
			}
			this.bitPtr += 18;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0004CD4E File Offset: 0x0004BD4E
		protected void OutCodeword(int codeword)
		{
			this.OutCodeword17(codeword);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0004CD57 File Offset: 0x0004BD57
		protected void OutStopPattern()
		{
			this.OutCodeword18(260649);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0004CD64 File Offset: 0x0004BD64
		protected void OutStartPattern()
		{
			this.OutCodeword17(130728);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0004CD74 File Offset: 0x0004BD74
		protected void OutPaintCode()
		{
			int num = 0;
			this.bitColumns = 17 * (this.codeColumns + 3) + 18;
			int num2 = ((this.bitColumns - 1) / 8 + 1) * this.codeRows;
			this.outBits = new byte[num2];
			for (int i = 0; i < this.codeRows; i++)
			{
				this.bitPtr = ((this.bitColumns - 1) / 8 + 1) * 8 * i;
				int num3 = i % 3;
				int[] array = BarcodePDF417.CLUSTERS[num3];
				this.OutStartPattern();
				int num4;
				switch (num3)
				{
				case 0:
					num4 = 30 * (i / 3) + (this.codeRows - 1) / 3;
					break;
				case 1:
					num4 = 30 * (i / 3) + this.errorLevel * 3 + (this.codeRows - 1) % 3;
					break;
				default:
					num4 = 30 * (i / 3) + this.codeColumns - 1;
					break;
				}
				this.OutCodeword(array[num4]);
				for (int j = 0; j < this.codeColumns; j++)
				{
					this.OutCodeword(array[this.codewords[num++]]);
				}
				switch (num3)
				{
				case 0:
					num4 = 30 * (i / 3) + this.codeColumns - 1;
					break;
				case 1:
					num4 = 30 * (i / 3) + (this.codeRows - 1) / 3;
					break;
				default:
					num4 = 30 * (i / 3) + this.errorLevel * 3 + (this.codeRows - 1) % 3;
					break;
				}
				this.OutCodeword(array[num4]);
				this.OutStopPattern();
			}
			if ((this.options & 128) != 0)
			{
				for (int k = 0; k < this.outBits.Length; k++)
				{
					byte[] array2 = this.outBits;
					int num5 = k;
					array2[num5] ^= byte.MaxValue;
				}
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0004CF34 File Offset: 0x0004BF34
		protected void CalculateErrorCorrection(int dest)
		{
			if (this.errorLevel < 0 || this.errorLevel > 8)
			{
				this.errorLevel = 0;
			}
			int[] array = BarcodePDF417.ERROR_LEVEL[this.errorLevel];
			int num = 2 << this.errorLevel;
			for (int i = 0; i < num; i++)
			{
				this.codewords[dest + i] = 0;
			}
			int num2 = num - 1;
			for (int j = 0; j < this.lenCodewords; j++)
			{
				int num3 = this.codewords[j] + this.codewords[dest];
				for (int k = 0; k <= num2; k++)
				{
					int num4 = num3 * array[num2 - k] % 929;
					int num5 = 929 - num4;
					this.codewords[dest + k] = (((k == num2) ? 0 : this.codewords[dest + k + 1]) + num5) % 929;
				}
			}
			for (int l = 0; l < num; l++)
			{
				this.codewords[dest + l] = (929 - this.codewords[dest + l]) % 929;
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0004D040 File Offset: 0x0004C040
		private static int GetTextTypeAndValue(byte[] input, int maxLength, int idx)
		{
			if (idx >= maxLength)
			{
				return 0;
			}
			char c = (char)(input[idx] & byte.MaxValue);
			if (c >= 'A' && c <= 'Z')
			{
				return 65536 + (int)c - 65;
			}
			if (c >= 'a' && c <= 'z')
			{
				return 131072 + (int)c - 97;
			}
			if (c == ' ')
			{
				return 458778;
			}
			int num = "0123456789&\r\t,:#-.$/+%*=^".IndexOf(c);
			int num2 = ";<>@[\\]_`~!\r\t,:\n-.$/\"|*()?{}'".IndexOf(c);
			if (num < 0 && num2 < 0)
			{
				return 1048576 + (int)c;
			}
			if (num == num2)
			{
				return 786432 + num;
			}
			if (num >= 0)
			{
				return 262144 + num;
			}
			return 524288 + num2;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0004D0DA File Offset: 0x0004C0DA
		protected int GetTextTypeAndValue(int maxLength, int idx)
		{
			return BarcodePDF417.GetTextTypeAndValue(this.text, maxLength, idx);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0004D0EC File Offset: 0x0004C0EC
		private void TextCompaction(byte[] input, int start, int length)
		{
			int[] array = new int[10840];
			int num = 65536;
			int i = 0;
			int num2 = 0;
			length += start;
			for (int j = start; j < length; j++)
			{
				int num3 = BarcodePDF417.GetTextTypeAndValue(input, length, j);
				if ((num3 & num) != 0)
				{
					array[i++] = (num3 & 255);
				}
				else if ((num3 & 1048576) != 0)
				{
					if ((i & 1) != 0)
					{
						array[i++] = 29;
						num = (((num & 524288) != 0) ? 65536 : num);
					}
					array[i++] = 913;
					array[i++] = (num3 & 255);
					num2 += 2;
				}
				else
				{
					int num4 = num;
					if (num4 <= 131072)
					{
						if (num4 != 65536)
						{
							if (num4 == 131072)
							{
								if ((num3 & 65536) != 0)
								{
									if ((this.GetTextTypeAndValue(length, j + 1) & this.GetTextTypeAndValue(length, j + 2) & 65536) != 0)
									{
										array[i++] = 28;
										array[i++] = 28;
										num = 65536;
									}
									else
									{
										array[i++] = 27;
									}
									array[i++] = (num3 & 255);
								}
								else if ((num3 & 262144) != 0)
								{
									array[i++] = 28;
									array[i++] = (num3 & 255);
									num = 262144;
								}
								else if ((BarcodePDF417.GetTextTypeAndValue(input, length, j + 1) & BarcodePDF417.GetTextTypeAndValue(input, length, j + 2) & 524288) != 0)
								{
									array[i++] = 28;
									array[i++] = 25;
									array[i++] = (num3 & 255);
									num = 524288;
								}
								else
								{
									array[i++] = 29;
									array[i++] = (num3 & 255);
								}
							}
						}
						else if ((num3 & 131072) != 0)
						{
							array[i++] = 27;
							array[i++] = (num3 & 255);
							num = 131072;
						}
						else if ((num3 & 262144) != 0)
						{
							array[i++] = 28;
							array[i++] = (num3 & 255);
							num = 262144;
						}
						else if ((BarcodePDF417.GetTextTypeAndValue(input, length, j + 1) & BarcodePDF417.GetTextTypeAndValue(input, length, j + 2) & 524288) != 0)
						{
							array[i++] = 28;
							array[i++] = 25;
							array[i++] = (num3 & 255);
							num = 524288;
						}
						else
						{
							array[i++] = 29;
							array[i++] = (num3 & 255);
						}
					}
					else if (num4 != 262144)
					{
						if (num4 == 524288)
						{
							array[i++] = 29;
							num = 65536;
							j--;
						}
					}
					else if ((num3 & 131072) != 0)
					{
						array[i++] = 27;
						array[i++] = (num3 & 255);
						num = 131072;
					}
					else if ((num3 & 65536) != 0)
					{
						array[i++] = 28;
						array[i++] = (num3 & 255);
						num = 65536;
					}
					else if ((BarcodePDF417.GetTextTypeAndValue(input, length, j + 1) & BarcodePDF417.GetTextTypeAndValue(input, length, j + 2) & 524288) != 0)
					{
						array[i++] = 25;
						array[i++] = (num3 & 255);
						num = 524288;
					}
					else
					{
						array[i++] = 29;
						array[i++] = (num3 & 255);
					}
				}
			}
			if ((i & 1) != 0)
			{
				array[i++] = 29;
			}
			int num5 = (i + num2) / 2;
			if (num5 + this.cwPtr > 926)
			{
				throw new ArgumentOutOfRangeException(MessageLocalization.GetComposedMessage("the.text.is.too.big"));
			}
			length = i;
			i = 0;
			while (i < length)
			{
				int num3 = array[i++];
				if (num3 >= 30)
				{
					this.codewords[this.cwPtr++] = num3;
					this.codewords[this.cwPtr++] = array[i++];
				}
				else
				{
					this.codewords[this.cwPtr++] = num3 * 30 + array[i++];
				}
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0004D513 File Offset: 0x0004C513
		protected void TextCompaction(int start, int length)
		{
			this.TextCompaction(this.text, start, length);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0004D523 File Offset: 0x0004C523
		protected void BasicNumberCompaction(int start, int length)
		{
			this.BasicNumberCompaction(this.text, start, length);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0004D534 File Offset: 0x0004C534
		private void BasicNumberCompaction(byte[] input, int start, int length)
		{
			int num = this.cwPtr;
			int num2 = length / 3;
			this.cwPtr += num2 + 1;
			for (int i = 0; i <= num2; i++)
			{
				this.codewords[num + i] = 0;
			}
			this.codewords[num + num2] = 1;
			length += start;
			for (int j = start; j < length; j++)
			{
				for (int i = num2; i >= 0; i--)
				{
					this.codewords[num + i] *= 10;
				}
				this.codewords[num + num2] += (int)(input[j] - 48);
				for (int i = num2; i > 0; i--)
				{
					this.codewords[num + i - 1] += this.codewords[num + i] / 900;
					this.codewords[num + i] %= 900;
				}
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0004D634 File Offset: 0x0004C634
		private void NumberCompaction(byte[] input, int start, int length)
		{
			int num = length / 44 * 15;
			int num2 = length % 44;
			if (num2 == 0)
			{
				num2 = num;
			}
			else
			{
				num2 = num + num2 / 3 + 1;
			}
			if (num2 + this.cwPtr > 926)
			{
				throw new ArgumentOutOfRangeException(MessageLocalization.GetComposedMessage("the.text.is.too.big"));
			}
			length += start;
			for (int i = start; i < length; i += 44)
			{
				num2 = ((length - i < 44) ? (length - i) : 44);
				this.BasicNumberCompaction(input, i, num2);
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0004D6A6 File Offset: 0x0004C6A6
		protected void NumberCompaction(int start, int length)
		{
			this.NumberCompaction(this.text, start, length);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0004D6B8 File Offset: 0x0004C6B8
		protected void ByteCompaction6(int start)
		{
			int num = 6;
			int num2 = this.cwPtr;
			int num3 = 4;
			this.cwPtr += num3 + 1;
			for (int i = 0; i <= num3; i++)
			{
				this.codewords[num2 + i] = 0;
			}
			num += start;
			for (int j = start; j < num; j++)
			{
				for (int i = num3; i >= 0; i--)
				{
					this.codewords[num2 + i] *= 256;
				}
				this.codewords[num2 + num3] += (int)(this.text[j] & byte.MaxValue);
				for (int i = num3; i > 0; i--)
				{
					this.codewords[num2 + i - 1] += this.codewords[num2 + i] / 900;
					this.codewords[num2 + i] %= 900;
				}
			}
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0004D7C8 File Offset: 0x0004C7C8
		internal void ByteCompaction(int start, int length)
		{
			int num = length / 6 * 5 + length % 6;
			if (num + this.cwPtr > 926)
			{
				throw new ArgumentOutOfRangeException(MessageLocalization.GetComposedMessage("the.text.is.too.big"));
			}
			length += start;
			for (int i = start; i < length; i += 6)
			{
				num = ((length - i < 44) ? (length - i) : 6);
				if (num < 6)
				{
					for (int j = 0; j < num; j++)
					{
						this.codewords[this.cwPtr++] = (int)(this.text[i + j] & byte.MaxValue);
					}
				}
				else
				{
					this.ByteCompaction6(i);
				}
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0004D860 File Offset: 0x0004C860
		internal void BreakString()
		{
			int num = this.text.Length;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if ((this.options & 32) != 0)
			{
				this.segmentList.Add('B', 0, num);
				return;
			}
			for (int i = 0; i < num; i++)
			{
				char c = (char)(this.text[i] & byte.MaxValue);
				if (c >= '0' && c <= '9')
				{
					if (num4 == 0)
					{
						num3 = i;
					}
					num4++;
				}
				else
				{
					if (num4 >= 13)
					{
						if (num2 != num3)
						{
							c = (char)(this.text[num2] & byte.MaxValue);
							bool flag = (c >= ' ' && c < '\u007f') || c == '\r' || c == '\n' || c == '\t';
							for (int j = num2; j < num3; j++)
							{
								c = (char)(this.text[j] & byte.MaxValue);
								bool flag2 = (c >= ' ' && c < '\u007f') || c == '\r' || c == '\n' || c == '\t';
								if (flag2 != flag)
								{
									this.segmentList.Add(flag ? 'T' : 'B', num2, j);
									num2 = j;
									flag = flag2;
								}
							}
							this.segmentList.Add(flag ? 'T' : 'B', num2, num3);
						}
						this.segmentList.Add('N', num3, i);
						num2 = i;
					}
					num4 = 0;
				}
			}
			if (num4 < 13)
			{
				num3 = num;
			}
			if (num2 != num3)
			{
				char c = (char)(this.text[num2] & byte.MaxValue);
				bool flag = (c >= ' ' && c < '\u007f') || c == '\r' || c == '\n' || c == '\t';
				for (int j = num2; j < num3; j++)
				{
					c = (char)(this.text[j] & byte.MaxValue);
					bool flag2 = (c >= ' ' && c < '\u007f') || c == '\r' || c == '\n' || c == '\t';
					if (flag2 != flag)
					{
						this.segmentList.Add(flag ? 'T' : 'B', num2, j);
						num2 = j;
						flag = flag2;
					}
				}
				this.segmentList.Add(flag ? 'T' : 'B', num2, num3);
			}
			if (num4 >= 13)
			{
				this.segmentList.Add('N', num3, num);
			}
			BarcodePDF417.Segment segment;
			for (int i = 0; i < this.segmentList.Size; i++)
			{
				segment = this.segmentList.Get(i);
				BarcodePDF417.Segment segment2 = this.segmentList.Get(i - 1);
				BarcodePDF417.Segment segment3 = this.segmentList.Get(i + 1);
				if (this.CheckSegmentType(segment, 'B') && this.GetSegmentLength(segment) == 1 && this.CheckSegmentType(segment2, 'T') && this.CheckSegmentType(segment3, 'T') && this.GetSegmentLength(segment2) + this.GetSegmentLength(segment3) >= 3)
				{
					segment2.end = segment3.end;
					this.segmentList.Remove(i);
					this.segmentList.Remove(i);
					i = -1;
				}
			}
			for (int i = 0; i < this.segmentList.Size; i++)
			{
				segment = this.segmentList.Get(i);
				BarcodePDF417.Segment segment2 = this.segmentList.Get(i - 1);
				BarcodePDF417.Segment segment3 = this.segmentList.Get(i + 1);
				if (this.CheckSegmentType(segment, 'T') && this.GetSegmentLength(segment) >= 5)
				{
					bool flag3 = false;
					if ((this.CheckSegmentType(segment2, 'B') && this.GetSegmentLength(segment2) == 1) || this.CheckSegmentType(segment2, 'T'))
					{
						flag3 = true;
						segment.start = segment2.start;
						this.segmentList.Remove(i - 1);
						i--;
					}
					if ((this.CheckSegmentType(segment3, 'B') && this.GetSegmentLength(segment3) == 1) || this.CheckSegmentType(segment3, 'T'))
					{
						flag3 = true;
						segment.end = segment3.end;
						this.segmentList.Remove(i + 1);
					}
					if (flag3)
					{
						i = -1;
					}
				}
			}
			for (int i = 0; i < this.segmentList.Size; i++)
			{
				segment = this.segmentList.Get(i);
				BarcodePDF417.Segment segment2 = this.segmentList.Get(i - 1);
				BarcodePDF417.Segment segment3 = this.segmentList.Get(i + 1);
				if (this.CheckSegmentType(segment, 'B'))
				{
					bool flag4 = false;
					if ((this.CheckSegmentType(segment2, 'T') && this.GetSegmentLength(segment2) < 5) || this.CheckSegmentType(segment2, 'B'))
					{
						flag4 = true;
						segment.start = segment2.start;
						this.segmentList.Remove(i - 1);
						i--;
					}
					if ((this.CheckSegmentType(segment3, 'T') && this.GetSegmentLength(segment3) < 5) || this.CheckSegmentType(segment3, 'B'))
					{
						flag4 = true;
						segment.end = segment3.end;
						this.segmentList.Remove(i + 1);
					}
					if (flag4)
					{
						i = -1;
					}
				}
			}
			if (this.segmentList.Size == 1 && (segment = this.segmentList.Get(0)).type == 'T' && this.GetSegmentLength(segment) >= 8)
			{
				int i;
				for (i = segment.start; i < segment.end; i++)
				{
					char c = (char)(this.text[i] & byte.MaxValue);
					if (c < '0' || c > '9')
					{
						break;
					}
				}
				if (i == segment.end)
				{
					segment.type = 'N';
				}
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0004DDC8 File Offset: 0x0004CDC8
		protected void Assemble()
		{
			if (this.segmentList.Size == 0)
			{
				return;
			}
			this.cwPtr = 1;
			for (int i = 0; i < this.segmentList.Size; i++)
			{
				BarcodePDF417.Segment segment = this.segmentList.Get(i);
				char type = segment.type;
				if (type != 'B')
				{
					if (type != 'N')
					{
						if (type == 'T')
						{
							if (i != 0)
							{
								this.codewords[this.cwPtr++] = 900;
							}
							this.TextCompaction(segment.start, this.GetSegmentLength(segment));
						}
					}
					else
					{
						this.codewords[this.cwPtr++] = 902;
						this.NumberCompaction(segment.start, this.GetSegmentLength(segment));
					}
				}
				else
				{
					this.codewords[this.cwPtr++] = ((this.GetSegmentLength(segment) % 6 != 0) ? 901 : 924);
					this.ByteCompaction(segment.start, this.GetSegmentLength(segment));
				}
			}
			if ((this.options & 256) != 0)
			{
				this.MacroCodes();
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0004DEF0 File Offset: 0x0004CEF0
		private void MacroCodes()
		{
			if (this.macroSegmentId < 0)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("macrosegmentid.must.be.gt.eq.0"));
			}
			if (this.macroSegmentId >= this.macroSegmentCount)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("macrosegmentid.must.be.lt.macrosemgentcount"));
			}
			if (this.macroSegmentCount < 1)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("macrosemgentcount.must.be.gt.0"));
			}
			this.macroIndex = this.cwPtr;
			this.codewords[this.cwPtr++] = 928;
			this.Append(this.macroSegmentId, 5);
			if (this.macroFileId != null)
			{
				this.Append(this.macroFileId);
			}
			if (this.macroSegmentId >= this.macroSegmentCount - 1)
			{
				this.codewords[this.cwPtr++] = 922;
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0004DFC4 File Offset: 0x0004CFC4
		private void Append(int inp, int len)
		{
			StringBuilder stringBuilder = new StringBuilder(len + 1);
			stringBuilder.Append(inp);
			for (int i = stringBuilder.Length; i < len; i++)
			{
				stringBuilder.Insert(0, "0");
			}
			byte[] array = PdfEncodings.ConvertToBytes(stringBuilder.ToString(), "cp437");
			this.NumberCompaction(array, 0, array.Length);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0004E01C File Offset: 0x0004D01C
		private void Append(string s)
		{
			byte[] array = PdfEncodings.ConvertToBytes(s, "cp437");
			this.TextCompaction(array, 0, array.Length);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0004E040 File Offset: 0x0004D040
		protected static int MaxPossibleErrorLevel(int remain)
		{
			int i = 8;
			int num = 512;
			while (i > 0)
			{
				if (remain >= num)
				{
					return i;
				}
				i--;
				num >>= 1;
			}
			return 0;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0004E06C File Offset: 0x0004D06C
		protected void DumpList()
		{
			if (this.segmentList.Size == 0)
			{
				return;
			}
			for (int i = 0; i < this.segmentList.Size; i++)
			{
				BarcodePDF417.Segment segment = this.segmentList.Get(i);
				int segmentLength = this.GetSegmentLength(segment);
				char[] array = new char[segmentLength];
				for (int j = 0; j < segmentLength; j++)
				{
					array[j] = (char)(this.text[segment.start + j] & byte.MaxValue);
					if (array[j] == '\r')
					{
						array[j] = '\n';
					}
				}
				Console.WriteLine("" + segment.type + new string(array));
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0004E115 File Offset: 0x0004D115
		protected int GetMaxSquare()
		{
			if (this.codeColumns > 21)
			{
				this.codeColumns = 29;
				this.codeRows = 32;
			}
			else
			{
				this.codeColumns = 16;
				this.codeRows = 58;
			}
			return 928;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0004E148 File Offset: 0x0004D148
		public void PaintCode()
		{
			if ((this.options & 64) != 0)
			{
				if (this.lenCodewords > 926 || this.lenCodewords < 1 || this.lenCodewords != this.codewords[0])
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.codeword.size"));
				}
			}
			else
			{
				if (this.text == null)
				{
					throw new ArgumentNullException(MessageLocalization.GetComposedMessage("text.cannot.be.null"));
				}
				if (this.text.Length > 5420)
				{
					throw new ArgumentOutOfRangeException(MessageLocalization.GetComposedMessage("the.text.is.too.big"));
				}
				this.segmentList = new BarcodePDF417.SegmentList();
				this.BreakString();
				this.Assemble();
				this.segmentList = null;
				this.codewords[0] = (this.lenCodewords = this.cwPtr);
			}
			int num = BarcodePDF417.MaxPossibleErrorLevel(928 - this.lenCodewords);
			if ((this.options & 16) == 0)
			{
				if (this.lenCodewords < 41)
				{
					this.errorLevel = 2;
				}
				else if (this.lenCodewords < 161)
				{
					this.errorLevel = 3;
				}
				else if (this.lenCodewords < 321)
				{
					this.errorLevel = 4;
				}
				else
				{
					this.errorLevel = 5;
				}
			}
			if (this.errorLevel < 0)
			{
				this.errorLevel = 0;
			}
			else if (this.errorLevel > num)
			{
				this.errorLevel = num;
			}
			if (this.codeColumns < 1)
			{
				this.codeColumns = 1;
			}
			else if (this.codeColumns > 30)
			{
				this.codeColumns = 30;
			}
			if (this.codeRows < 3)
			{
				this.codeRows = 3;
			}
			else if (this.codeRows > 90)
			{
				this.codeRows = 90;
			}
			int num2 = 2 << this.errorLevel;
			bool flag = (this.options & 4) == 0;
			bool flag2 = false;
			int num3 = this.lenCodewords + num2;
			if ((this.options & 1) != 0)
			{
				num3 = this.codeColumns * this.codeRows;
				if (num3 > 928)
				{
					num3 = this.GetMaxSquare();
				}
				if (num3 < this.lenCodewords + num2)
				{
					num3 = this.lenCodewords + num2;
				}
				else
				{
					flag2 = true;
				}
			}
			else if ((this.options & 6) == 0)
			{
				flag = true;
				if ((double)this.aspectRatio < 0.001)
				{
					this.aspectRatio = 0.001f;
				}
				else if (this.aspectRatio > 1000f)
				{
					this.aspectRatio = 1000f;
				}
				double num4 = (double)(73f * this.aspectRatio - 4f);
				double num5 = (-num4 + Math.Sqrt(num4 * num4 + (double)(68f * this.aspectRatio * (float)(this.lenCodewords + num2) * this.yHeight))) / (double)(34f * this.aspectRatio);
				this.codeColumns = (int)(num5 + 0.5);
				if (this.codeColumns < 1)
				{
					this.codeColumns = 1;
				}
				else if (this.codeColumns > 30)
				{
					this.codeColumns = 30;
				}
			}
			if (!flag2)
			{
				if (flag)
				{
					this.codeRows = (num3 - 1) / this.codeColumns + 1;
					if (this.codeRows < 3)
					{
						this.codeRows = 3;
					}
					else if (this.codeRows > 90)
					{
						this.codeRows = 90;
						this.codeColumns = (num3 - 1) / 90 + 1;
					}
				}
				else
				{
					this.codeColumns = (num3 - 1) / this.codeRows + 1;
					if (this.codeColumns > 30)
					{
						this.codeColumns = 30;
						this.codeRows = (num3 - 1) / 30 + 1;
					}
				}
				num3 = this.codeRows * this.codeColumns;
			}
			if (num3 > 928)
			{
				num3 = this.GetMaxSquare();
			}
			this.errorLevel = BarcodePDF417.MaxPossibleErrorLevel(num3 - this.lenCodewords);
			num2 = 2 << this.errorLevel;
			int num6 = num3 - num2 - this.lenCodewords;
			if ((this.options & 256) != 0)
			{
				Array.Copy(this.codewords, this.macroIndex, this.codewords, this.macroIndex + num6, num6);
				this.cwPtr = this.lenCodewords + num6;
				while (num6-- != 0)
				{
					this.codewords[this.macroIndex++] = 900;
				}
			}
			else
			{
				this.cwPtr = this.lenCodewords;
				while (num6-- != 0)
				{
					this.codewords[this.cwPtr++] = 900;
				}
			}
			this.codewords[0] = (this.lenCodewords = this.cwPtr);
			this.CalculateErrorCorrection(this.lenCodewords);
			this.lenCodewords = num3;
			this.OutPaintCode();
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0004E5A4 File Offset: 0x0004D5A4
		public Image GetImage()
		{
			this.PaintCode();
			byte[] data = CCITTG4Encoder.Compress(this.outBits, this.bitColumns, this.codeRows);
			return Image.GetInstance(this.bitColumns, this.codeRows, false, 256, ((this.options & 128) == 0) ? 0 : 1, data, null);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0004E5FC File Offset: 0x0004D5FC
		public virtual Image CreateDrawingImage(Color foreground, Color background)
		{
			this.PaintCode();
			int num = (int)this.yHeight;
			int num2 = (this.bitColumns + 7) / 8;
			Bitmap bitmap = new Bitmap(this.bitColumns, this.codeRows * num);
			int num3 = 0;
			for (int i = 0; i < this.codeRows; i++)
			{
				for (int j = 0; j < num; j++)
				{
					int num4 = i * num2;
					for (int k = 0; k < this.bitColumns; k++)
					{
						int num5 = (int)(this.outBits[num4 + k / 8] & byte.MaxValue);
						num5 <<= k % 8;
						bitmap.SetPixel(k, num3, ((num5 & 128) == 0) ? background : foreground);
					}
					num3++;
				}
			}
			return bitmap;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0004E6B6 File Offset: 0x0004D6B6
		public byte[] OutBits
		{
			get
			{
				return this.outBits;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0004E6BE File Offset: 0x0004D6BE
		public int BitColumns
		{
			get
			{
				return this.bitColumns;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0004E6CF File Offset: 0x0004D6CF
		// (set) Token: 0x06000DFE RID: 3582 RVA: 0x0004E6C6 File Offset: 0x0004D6C6
		public int CodeRows
		{
			get
			{
				return this.codeRows;
			}
			set
			{
				this.codeRows = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0004E6E0 File Offset: 0x0004D6E0
		// (set) Token: 0x06000E00 RID: 3584 RVA: 0x0004E6D7 File Offset: 0x0004D6D7
		public int CodeColumns
		{
			get
			{
				return this.codeColumns;
			}
			set
			{
				this.codeColumns = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0004E6E8 File Offset: 0x0004D6E8
		public int[] Codewords
		{
			get
			{
				return this.codewords;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0004E6F9 File Offset: 0x0004D6F9
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x0004E6F0 File Offset: 0x0004D6F0
		public int LenCodewords
		{
			get
			{
				return this.lenCodewords;
			}
			set
			{
				this.lenCodewords = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0004E70A File Offset: 0x0004D70A
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x0004E701 File Offset: 0x0004D701
		public int ErrorLevel
		{
			get
			{
				return this.errorLevel;
			}
			set
			{
				this.errorLevel = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0004E71B File Offset: 0x0004D71B
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x0004E712 File Offset: 0x0004D712
		public byte[] Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0004E723 File Offset: 0x0004D723
		public void SetText(string s)
		{
			this.text = PdfEncodings.ConvertToBytes(s, "cp437");
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0004E73F File Offset: 0x0004D73F
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x0004E736 File Offset: 0x0004D736
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

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0004E750 File Offset: 0x0004D750
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x0004E747 File Offset: 0x0004D747
		public float AspectRatio
		{
			get
			{
				return this.aspectRatio;
			}
			set
			{
				this.aspectRatio = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0004E761 File Offset: 0x0004D761
		// (set) Token: 0x06000E0E RID: 3598 RVA: 0x0004E758 File Offset: 0x0004D758
		public float YHeight
		{
			get
			{
				return this.yHeight;
			}
			set
			{
				this.yHeight = value;
			}
		}

		// Token: 0x04000A2A RID: 2602
		public const int PDF417_USE_ASPECT_RATIO = 0;

		// Token: 0x04000A2B RID: 2603
		public const int PDF417_FIXED_RECTANGLE = 1;

		// Token: 0x04000A2C RID: 2604
		public const int PDF417_FIXED_COLUMNS = 2;

		// Token: 0x04000A2D RID: 2605
		public const int PDF417_FIXED_ROWS = 4;

		// Token: 0x04000A2E RID: 2606
		public const int PDF417_AUTO_ERROR_LEVEL = 0;

		// Token: 0x04000A2F RID: 2607
		public const int PDF417_USE_ERROR_LEVEL = 16;

		// Token: 0x04000A30 RID: 2608
		public const int PDF417_FORCE_BINARY = 32;

		// Token: 0x04000A31 RID: 2609
		public const int PDF417_USE_RAW_CODEWORDS = 64;

		// Token: 0x04000A32 RID: 2610
		public const int PDF417_INVERT_BITMAP = 128;

		// Token: 0x04000A33 RID: 2611
		public const int PDF417_USE_MACRO = 256;

		// Token: 0x04000A34 RID: 2612
		protected const int START_PATTERN = 130728;

		// Token: 0x04000A35 RID: 2613
		protected const int STOP_PATTERN = 260649;

		// Token: 0x04000A36 RID: 2614
		protected const int START_CODE_SIZE = 17;

		// Token: 0x04000A37 RID: 2615
		protected const int STOP_SIZE = 18;

		// Token: 0x04000A38 RID: 2616
		protected const int MOD = 929;

		// Token: 0x04000A39 RID: 2617
		protected const int ALPHA = 65536;

		// Token: 0x04000A3A RID: 2618
		protected const int LOWER = 131072;

		// Token: 0x04000A3B RID: 2619
		protected const int MIXED = 262144;

		// Token: 0x04000A3C RID: 2620
		protected const int PUNCTUATION = 524288;

		// Token: 0x04000A3D RID: 2621
		protected const int ISBYTE = 1048576;

		// Token: 0x04000A3E RID: 2622
		protected const int BYTESHIFT = 913;

		// Token: 0x04000A3F RID: 2623
		protected const int PL = 25;

		// Token: 0x04000A40 RID: 2624
		protected const int LL = 27;

		// Token: 0x04000A41 RID: 2625
		protected const int AS = 27;

		// Token: 0x04000A42 RID: 2626
		protected const int ML = 28;

		// Token: 0x04000A43 RID: 2627
		protected const int AL = 28;

		// Token: 0x04000A44 RID: 2628
		protected const int PS = 29;

		// Token: 0x04000A45 RID: 2629
		protected const int PAL = 29;

		// Token: 0x04000A46 RID: 2630
		protected const int SPACE = 26;

		// Token: 0x04000A47 RID: 2631
		protected const int TEXT_MODE = 900;

		// Token: 0x04000A48 RID: 2632
		protected const int BYTE_MODE_6 = 924;

		// Token: 0x04000A49 RID: 2633
		protected const int BYTE_MODE = 901;

		// Token: 0x04000A4A RID: 2634
		protected const int NUMERIC_MODE = 902;

		// Token: 0x04000A4B RID: 2635
		protected const int ABSOLUTE_MAX_TEXT_SIZE = 5420;

		// Token: 0x04000A4C RID: 2636
		protected const int MAX_DATA_CODEWORDS = 926;

		// Token: 0x04000A4D RID: 2637
		protected const int MACRO_SEGMENT_ID = 928;

		// Token: 0x04000A4E RID: 2638
		protected const int MACRO_LAST_SEGMENT = 922;

		// Token: 0x04000A4F RID: 2639
		private const string MIXED_SET = "0123456789&\r\t,:#-.$/+%*=^";

		// Token: 0x04000A50 RID: 2640
		private const string PUNCTUATION_SET = ";<>@[\\]_`~!\r\t,:\n-.$/\"|*()?{}'";

		// Token: 0x04000A51 RID: 2641
		private int macroSegmentCount;

		// Token: 0x04000A52 RID: 2642
		private int macroSegmentId = -1;

		// Token: 0x04000A53 RID: 2643
		private string macroFileId;

		// Token: 0x04000A54 RID: 2644
		private int macroIndex;

		// Token: 0x04000A55 RID: 2645
		protected int bitPtr;

		// Token: 0x04000A56 RID: 2646
		protected int cwPtr;

		// Token: 0x04000A57 RID: 2647
		protected BarcodePDF417.SegmentList segmentList;

		// Token: 0x04000A58 RID: 2648
		private static readonly int[][] CLUSTERS = new int[][]
		{
			new int[]
			{
				120256,
				125680,
				128380,
				120032,
				125560,
				128318,
				108736,
				119920,
				108640,
				86080,
				108592,
				86048,
				110016,
				120560,
				125820,
				109792,
				120440,
				125758,
				88256,
				109680,
				88160,
				89536,
				110320,
				120700,
				89312,
				110200,
				120638,
				89200,
				110140,
				89840,
				110460,
				89720,
				110398,
				89980,
				128506,
				119520,
				125304,
				128190,
				107712,
				119408,
				125244,
				107616,
				119352,
				84032,
				107568,
				119324,
				84000,
				107544,
				83984,
				108256,
				119672,
				125374,
				85184,
				108144,
				119612,
				85088,
				108088,
				119582,
				85040,
				108060,
				85728,
				108408,
				119742,
				85616,
				108348,
				85560,
				108318,
				85880,
				108478,
				85820,
				85790,
				107200,
				119152,
				125116,
				107104,
				119096,
				125086,
				83008,
				107056,
				119068,
				82976,
				107032,
				82960,
				82952,
				83648,
				107376,
				119228,
				83552,
				107320,
				119198,
				83504,
				107292,
				83480,
				83468,
				83824,
				107452,
				83768,
				107422,
				83740,
				83900,
				106848,
				118968,
				125022,
				82496,
				106800,
				118940,
				82464,
				106776,
				118926,
				82448,
				106764,
				82440,
				106758,
				82784,
				106936,
				119006,
				82736,
				106908,
				82712,
				106894,
				82700,
				82694,
				106974,
				82830,
				82240,
				106672,
				118876,
				82208,
				106648,
				118862,
				82192,
				106636,
				82184,
				106630,
				82180,
				82352,
				82328,
				82316,
				82080,
				118830,
				106572,
				106566,
				82050,
				117472,
				124280,
				127678,
				103616,
				117360,
				124220,
				103520,
				117304,
				124190,
				75840,
				103472,
				75808,
				104160,
				117624,
				124350,
				76992,
				104048,
				117564,
				76896,
				103992,
				76848,
				76824,
				77536,
				104312,
				117694,
				77424,
				104252,
				77368,
				77340,
				77688,
				104382,
				77628,
				77758,
				121536,
				126320,
				128700,
				121440,
				126264,
				128670,
				111680,
				121392,
				126236,
				111648,
				121368,
				126222,
				111632,
				121356,
				103104,
				117104,
				124092,
				112320,
				103008,
				117048,
				124062,
				112224,
				121656,
				126366,
				93248,
				74784,
				102936,
				117006,
				93216,
				112152,
				93200,
				75456,
				103280,
				117180,
				93888,
				75360,
				103224,
				117150,
				93792,
				112440,
				121758,
				93744,
				75288,
				93720,
				75632,
				103356,
				94064,
				75576,
				103326,
				94008,
				112542,
				93980,
				75708,
				94140,
				75678,
				94110,
				121184,
				126136,
				128606,
				111168,
				121136,
				126108,
				111136,
				121112,
				126094,
				111120,
				121100,
				111112,
				111108,
				102752,
				116920,
				123998,
				111456,
				102704,
				116892,
				91712,
				74272,
				121244,
				116878,
				91680,
				74256,
				102668,
				91664,
				111372,
				102662,
				74244,
				74592,
				102840,
				116958,
				92000,
				74544,
				102812,
				91952,
				111516,
				102798,
				91928,
				74508,
				74502,
				74680,
				102878,
				92088,
				74652,
				92060,
				74638,
				92046,
				92126,
				110912,
				121008,
				126044,
				110880,
				120984,
				126030,
				110864,
				120972,
				110856,
				120966,
				110852,
				110850,
				74048,
				102576,
				116828,
				90944,
				74016,
				102552,
				116814,
				90912,
				111000,
				121038,
				90896,
				73992,
				102534,
				90888,
				110982,
				90884,
				74160,
				102620,
				91056,
				74136,
				102606,
				91032,
				111054,
				91020,
				74118,
				91014,
				91100,
				91086,
				110752,
				120920,
				125998,
				110736,
				120908,
				110728,
				120902,
				110724,
				110722,
				73888,
				102488,
				116782,
				90528,
				73872,
				102476,
				90512,
				110796,
				102470,
				90504,
				73860,
				90500,
				73858,
				73944,
				90584,
				90572,
				90566,
				120876,
				120870,
				110658,
				102444,
				73800,
				90312,
				90308,
				90306,
				101056,
				116080,
				123580,
				100960,
				116024,
				70720,
				100912,
				115996,
				70688,
				100888,
				70672,
				70664,
				71360,
				101232,
				116156,
				71264,
				101176,
				116126,
				71216,
				101148,
				71192,
				71180,
				71536,
				101308,
				71480,
				101278,
				71452,
				71612,
				71582,
				118112,
				124600,
				127838,
				105024,
				118064,
				124572,
				104992,
				118040,
				124558,
				104976,
				118028,
				104968,
				118022,
				100704,
				115896,
				123486,
				105312,
				100656,
				115868,
				79424,
				70176,
				118172,
				115854,
				79392,
				105240,
				100620,
				79376,
				70152,
				79368,
				70496,
				100792,
				115934,
				79712,
				70448,
				118238,
				79664,
				105372,
				100750,
				79640,
				70412,
				79628,
				70584,
				100830,
				79800,
				70556,
				79772,
				70542,
				70622,
				79838,
				122176,
				126640,
				128860,
				122144,
				126616,
				128846,
				122128,
				126604,
				122120,
				126598,
				122116,
				104768,
				117936,
				124508,
				113472,
				104736,
				126684,
				124494,
				113440,
				122264,
				126670,
				113424,
				104712,
				117894,
				113416,
				122246,
				104706,
				69952,
				100528,
				115804,
				78656,
				69920,
				100504,
				115790,
				96064,
				78624,
				104856,
				117966,
				96032,
				113560,
				122318,
				100486,
				96016,
				78600,
				104838,
				96008,
				69890,
				70064,
				100572,
				78768,
				70040,
				100558,
				96176,
				78744,
				104910,
				96152,
				113614,
				70022,
				78726,
				70108,
				78812,
				70094,
				96220,
				78798,
				122016,
				126552,
				128814,
				122000,
				126540,
				121992,
				126534,
				121988,
				121986,
				104608,
				117848,
				124462,
				113056,
				104592,
				126574,
				113040,
				122060,
				117830,
				113032,
				104580,
				113028,
				104578,
				113026,
				69792,
				100440,
				115758,
				78240,
				69776,
				100428,
				95136,
				78224,
				104652,
				100422,
				95120,
				113100,
				69764,
				95112,
				78212,
				69762,
				78210,
				69848,
				100462,
				78296,
				69836,
				95192,
				78284,
				69830,
				95180,
				78278,
				69870,
				95214,
				121936,
				126508,
				121928,
				126502,
				121924,
				121922,
				104528,
				117804,
				112848,
				104520,
				117798,
				112840,
				121958,
				112836,
				104514,
				112834,
				69712,
				100396,
				78032,
				69704,
				100390,
				94672,
				78024,
				104550,
				94664,
				112870,
				69698,
				94660,
				78018,
				94658,
				78060,
				94700,
				94694,
				126486,
				121890,
				117782,
				104484,
				104482,
				69672,
				77928,
				94440,
				69666,
				77922,
				99680,
				68160,
				99632,
				68128,
				99608,
				115342,
				68112,
				99596,
				68104,
				99590,
				68448,
				99768,
				115422,
				68400,
				99740,
				68376,
				99726,
				68364,
				68358,
				68536,
				99806,
				68508,
				68494,
				68574,
				101696,
				116400,
				123740,
				101664,
				116376,
				101648,
				116364,
				101640,
				116358,
				101636,
				67904,
				99504,
				115292,
				72512,
				67872,
				116444,
				115278,
				72480,
				101784,
				116430,
				72464,
				67848,
				99462,
				72456,
				101766,
				67842,
				68016,
				99548,
				72624,
				67992,
				99534,
				72600,
				101838,
				72588,
				67974,
				68060,
				72668,
				68046,
				72654,
				118432,
				124760,
				127918,
				118416,
				124748,
				118408,
				124742,
				118404,
				118402,
				101536,
				116312,
				105888,
				101520,
				116300,
				105872,
				118476,
				116294,
				105864,
				101508,
				105860,
				101506,
				105858,
				67744,
				99416,
				72096,
				67728,
				116334,
				80800,
				72080,
				101580,
				99398,
				80784,
				105932,
				67716,
				80776,
				72068,
				67714,
				72066,
				67800,
				99438,
				72152,
				67788,
				80856,
				72140,
				67782,
				80844,
				72134,
				67822,
				72174,
				80878,
				126800,
				128940,
				126792,
				128934,
				126788,
				126786,
				118352,
				124716,
				122576,
				126828,
				124710,
				122568,
				126822,
				122564,
				118338,
				122562,
				101456,
				116268,
				105680,
				101448,
				116262,
				114128,
				105672,
				118374,
				114120,
				122598,
				101442,
				114116,
				105666,
				114114,
				67664,
				99372,
				71888,
				67656,
				99366,
				80336,
				71880,
				101478,
				97232,
				80328,
				105702,
				67650,
				97224,
				114150,
				71874,
				97220,
				67692,
				71916,
				67686,
				80364,
				71910,
				97260,
				80358,
				97254,
				126760,
				128918,
				126756,
				126754,
				118312,
				124694,
				122472,
				126774,
				122468,
				118306,
				122466,
				101416,
				116246,
				105576,
				101412,
				113896,
				105572,
				101410,
				113892,
				105570,
				113890,
				67624,
				99350,
				71784,
				101430,
				80104,
				71780,
				67618,
				96744,
				80100,
				71778,
				96740,
				80098,
				96738,
				71798,
				96758,
				126738,
				122420,
				122418,
				105524,
				113780,
				113778,
				71732,
				79988,
				96500,
				96498,
				66880,
				66848,
				98968,
				66832,
				66824,
				66820,
				66992,
				66968,
				66956,
				66950,
				67036,
				67022,
				100000,
				99984,
				115532,
				99976,
				115526,
				99972,
				99970,
				66720,
				98904,
				69024,
				100056,
				98892,
				69008,
				100044,
				69000,
				100038,
				68996,
				66690,
				68994,
				66776,
				98926,
				69080,
				100078,
				69068,
				66758,
				69062,
				66798,
				69102,
				116560,
				116552,
				116548,
				116546,
				99920,
				102096,
				116588,
				115494,
				102088,
				116582,
				102084,
				99906,
				102082,
				66640,
				68816,
				66632,
				98854,
				73168,
				68808,
				66628,
				73160,
				68804,
				66626,
				73156,
				68802,
				66668,
				68844,
				66662,
				73196,
				68838,
				73190,
				124840,
				124836,
				124834,
				116520,
				118632,
				124854,
				118628,
				116514,
				118626,
				99880,
				115478,
				101992,
				116534,
				106216,
				101988,
				99874,
				106212,
				101986,
				106210,
				66600,
				98838,
				68712,
				99894,
				72936,
				68708,
				66594,
				81384,
				72932,
				68706,
				81380,
				72930,
				66614,
				68726,
				72950,
				81398,
				128980,
				128978,
				124820,
				126900,
				124818,
				126898,
				116500,
				118580,
				116498,
				122740,
				118578,
				122738,
				99860,
				101940,
				99858,
				106100,
				101938,
				114420
			},
			new int[]
			{
				128352,
				129720,
				125504,
				128304,
				129692,
				125472,
				128280,
				129678,
				125456,
				128268,
				125448,
				128262,
				125444,
				125792,
				128440,
				129758,
				120384,
				125744,
				128412,
				120352,
				125720,
				128398,
				120336,
				125708,
				120328,
				125702,
				120324,
				120672,
				125880,
				128478,
				110144,
				120624,
				125852,
				110112,
				120600,
				125838,
				110096,
				120588,
				110088,
				120582,
				110084,
				110432,
				120760,
				125918,
				89664,
				110384,
				120732,
				89632,
				110360,
				120718,
				89616,
				110348,
				89608,
				110342,
				89952,
				110520,
				120798,
				89904,
				110492,
				89880,
				110478,
				89868,
				90040,
				110558,
				90012,
				89998,
				125248,
				128176,
				129628,
				125216,
				128152,
				129614,
				125200,
				128140,
				125192,
				128134,
				125188,
				125186,
				119616,
				125360,
				128220,
				119584,
				125336,
				128206,
				119568,
				125324,
				119560,
				125318,
				119556,
				119554,
				108352,
				119728,
				125404,
				108320,
				119704,
				125390,
				108304,
				119692,
				108296,
				119686,
				108292,
				108290,
				85824,
				108464,
				119772,
				85792,
				108440,
				119758,
				85776,
				108428,
				85768,
				108422,
				85764,
				85936,
				108508,
				85912,
				108494,
				85900,
				85894,
				85980,
				85966,
				125088,
				128088,
				129582,
				125072,
				128076,
				125064,
				128070,
				125060,
				125058,
				119200,
				125144,
				128110,
				119184,
				125132,
				119176,
				125126,
				119172,
				119170,
				107424,
				119256,
				125166,
				107408,
				119244,
				107400,
				119238,
				107396,
				107394,
				83872,
				107480,
				119278,
				83856,
				107468,
				83848,
				107462,
				83844,
				83842,
				83928,
				107502,
				83916,
				83910,
				83950,
				125008,
				128044,
				125000,
				128038,
				124996,
				124994,
				118992,
				125036,
				118984,
				125030,
				118980,
				118978,
				106960,
				119020,
				106952,
				119014,
				106948,
				106946,
				82896,
				106988,
				82888,
				106982,
				82884,
				82882,
				82924,
				82918,
				124968,
				128022,
				124964,
				124962,
				118888,
				124982,
				118884,
				118882,
				106728,
				118902,
				106724,
				106722,
				82408,
				106742,
				82404,
				82402,
				124948,
				124946,
				118836,
				118834,
				106612,
				106610,
				124224,
				127664,
				129372,
				124192,
				127640,
				129358,
				124176,
				127628,
				124168,
				127622,
				124164,
				124162,
				117568,
				124336,
				127708,
				117536,
				124312,
				127694,
				117520,
				124300,
				117512,
				124294,
				117508,
				117506,
				104256,
				117680,
				124380,
				104224,
				117656,
				124366,
				104208,
				117644,
				104200,
				117638,
				104196,
				104194,
				77632,
				104368,
				117724,
				77600,
				104344,
				117710,
				77584,
				104332,
				77576,
				104326,
				77572,
				77744,
				104412,
				77720,
				104398,
				77708,
				77702,
				77788,
				77774,
				128672,
				129880,
				93168,
				128656,
				129868,
				92664,
				128648,
				129862,
				92412,
				128644,
				128642,
				124064,
				127576,
				129326,
				126368,
				124048,
				129902,
				126352,
				128716,
				127558,
				126344,
				124036,
				126340,
				124034,
				126338,
				117152,
				124120,
				127598,
				121760,
				117136,
				124108,
				121744,
				126412,
				124102,
				121736,
				117124,
				121732,
				117122,
				121730,
				103328,
				117208,
				124142,
				112544,
				103312,
				117196,
				112528,
				121804,
				117190,
				112520,
				103300,
				112516,
				103298,
				112514,
				75680,
				103384,
				117230,
				94112,
				75664,
				103372,
				94096,
				112588,
				103366,
				94088,
				75652,
				94084,
				75650,
				75736,
				103406,
				94168,
				75724,
				94156,
				75718,
				94150,
				75758,
				128592,
				129836,
				91640,
				128584,
				129830,
				91388,
				128580,
				91262,
				128578,
				123984,
				127532,
				126160,
				123976,
				127526,
				126152,
				128614,
				126148,
				123970,
				126146,
				116944,
				124012,
				121296,
				116936,
				124006,
				121288,
				126182,
				121284,
				116930,
				121282,
				102864,
				116972,
				111568,
				102856,
				116966,
				111560,
				121318,
				111556,
				102850,
				111554,
				74704,
				102892,
				92112,
				74696,
				102886,
				92104,
				111590,
				92100,
				74690,
				92098,
				74732,
				92140,
				74726,
				92134,
				128552,
				129814,
				90876,
				128548,
				90750,
				128546,
				123944,
				127510,
				126056,
				128566,
				126052,
				123938,
				126050,
				116840,
				123958,
				121064,
				116836,
				121060,
				116834,
				121058,
				102632,
				116854,
				111080,
				121078,
				111076,
				102626,
				111074,
				74216,
				102646,
				91112,
				74212,
				91108,
				74210,
				91106,
				74230,
				91126,
				128532,
				90494,
				128530,
				123924,
				126004,
				123922,
				126002,
				116788,
				120948,
				116786,
				120946,
				102516,
				110836,
				102514,
				110834,
				73972,
				90612,
				73970,
				90610,
				128522,
				123914,
				125978,
				116762,
				120890,
				102458,
				110714,
				123552,
				127320,
				129198,
				123536,
				127308,
				123528,
				127302,
				123524,
				123522,
				116128,
				123608,
				127342,
				116112,
				123596,
				116104,
				123590,
				116100,
				116098,
				101280,
				116184,
				123630,
				101264,
				116172,
				101256,
				116166,
				101252,
				101250,
				71584,
				101336,
				116206,
				71568,
				101324,
				71560,
				101318,
				71556,
				71554,
				71640,
				101358,
				71628,
				71622,
				71662,
				127824,
				129452,
				79352,
				127816,
				129446,
				79100,
				127812,
				78974,
				127810,
				123472,
				127276,
				124624,
				123464,
				127270,
				124616,
				127846,
				124612,
				123458,
				124610,
				115920,
				123500,
				118224,
				115912,
				123494,
				118216,
				124646,
				118212,
				115906,
				118210,
				100816,
				115948,
				105424,
				100808,
				115942,
				105416,
				118246,
				105412,
				100802,
				105410,
				70608,
				100844,
				79824,
				70600,
				100838,
				79816,
				105446,
				79812,
				70594,
				79810,
				70636,
				79852,
				70630,
				79846,
				129960,
				95728,
				113404,
				129956,
				95480,
				113278,
				129954,
				95356,
				95294,
				127784,
				129430,
				78588,
				128872,
				129974,
				95996,
				78462,
				128868,
				127778,
				95870,
				128866,
				123432,
				127254,
				124520,
				123428,
				126696,
				128886,
				123426,
				126692,
				124514,
				126690,
				115816,
				123446,
				117992,
				115812,
				122344,
				117988,
				115810,
				122340,
				117986,
				122338,
				100584,
				115830,
				104936,
				100580,
				113640,
				104932,
				100578,
				113636,
				104930,
				113634,
				70120,
				100598,
				78824,
				70116,
				96232,
				78820,
				70114,
				96228,
				78818,
				96226,
				70134,
				78838,
				129940,
				94968,
				113022,
				129938,
				94844,
				94782,
				127764,
				78206,
				128820,
				127762,
				95102,
				128818,
				123412,
				124468,
				123410,
				126580,
				124466,
				126578,
				115764,
				117876,
				115762,
				122100,
				117874,
				122098,
				100468,
				104692,
				100466,
				113140,
				104690,
				113138,
				69876,
				78324,
				69874,
				95220,
				78322,
				95218,
				129930,
				94588,
				94526,
				127754,
				128794,
				123402,
				124442,
				126522,
				115738,
				117818,
				121978,
				100410,
				104570,
				112890,
				69754,
				78074,
				94714,
				94398,
				123216,
				127148,
				123208,
				127142,
				123204,
				123202,
				115408,
				123244,
				115400,
				123238,
				115396,
				115394,
				99792,
				115436,
				99784,
				115430,
				99780,
				99778,
				68560,
				99820,
				68552,
				99814,
				68548,
				68546,
				68588,
				68582,
				127400,
				129238,
				72444,
				127396,
				72318,
				127394,
				123176,
				127126,
				123752,
				123172,
				123748,
				123170,
				123746,
				115304,
				123190,
				116456,
				115300,
				116452,
				115298,
				116450,
				99560,
				115318,
				101864,
				99556,
				101860,
				99554,
				101858,
				68072,
				99574,
				72680,
				68068,
				72676,
				68066,
				72674,
				68086,
				72694,
				129492,
				80632,
				105854,
				129490,
				80508,
				80446,
				127380,
				72062,
				127924,
				127378,
				80766,
				127922,
				123156,
				123700,
				123154,
				124788,
				123698,
				124786,
				115252,
				116340,
				115250,
				118516,
				116338,
				118514,
				99444,
				101620,
				99442,
				105972,
				101618,
				105970,
				67828,
				72180,
				67826,
				80884,
				72178,
				80882,
				97008,
				114044,
				96888,
				113982,
				96828,
				96798,
				129482,
				80252,
				130010,
				97148,
				80190,
				97086,
				127370,
				127898,
				128954,
				123146,
				123674,
				124730,
				126842,
				115226,
				116282,
				118394,
				122618,
				99386,
				101498,
				105722,
				114170,
				67706,
				71930,
				80378,
				96632,
				113854,
				96572,
				96542,
				80062,
				96702,
				96444,
				96414,
				96350,
				123048,
				123044,
				123042,
				115048,
				123062,
				115044,
				115042,
				99048,
				115062,
				99044,
				99042,
				67048,
				99062,
				67044,
				67042,
				67062,
				127188,
				68990,
				127186,
				123028,
				123316,
				123026,
				123314,
				114996,
				115572,
				114994,
				115570,
				98932,
				100084,
				98930,
				100082,
				66804,
				69108,
				66802,
				69106,
				129258,
				73084,
				73022,
				127178,
				127450,
				123018,
				123290,
				123834,
				114970,
				115514,
				116602,
				98874,
				99962,
				102138,
				66682,
				68858,
				73210,
				81272,
				106174,
				81212,
				81182,
				72894,
				81342,
				97648,
				114364,
				97592,
				114334,
				97564,
				97550,
				81084,
				97724,
				81054,
				97694,
				97464,
				114270,
				97436,
				97422,
				80990,
				97502,
				97372,
				97358,
				97326,
				114868,
				114866,
				98676,
				98674,
				66292,
				66290,
				123098,
				114842,
				115130,
				98618,
				99194,
				66170,
				67322,
				69310,
				73404,
				73374,
				81592,
				106334,
				81564,
				81550,
				73310,
				81630,
				97968,
				114524,
				97944,
				114510,
				97932,
				97926,
				81500,
				98012,
				81486,
				97998,
				97880,
				114478,
				97868,
				97862,
				81454,
				97902,
				97836,
				97830,
				69470,
				73564,
				73550,
				81752,
				106414,
				81740,
				81734,
				73518,
				81774,
				81708,
				81702
			},
			new int[]
			{
				109536,
				120312,
				86976,
				109040,
				120060,
				86496,
				108792,
				119934,
				86256,
				108668,
				86136,
				129744,
				89056,
				110072,
				129736,
				88560,
				109820,
				129732,
				88312,
				109694,
				129730,
				88188,
				128464,
				129772,
				89592,
				128456,
				129766,
				89340,
				128452,
				89214,
				128450,
				125904,
				128492,
				125896,
				128486,
				125892,
				125890,
				120784,
				125932,
				120776,
				125926,
				120772,
				120770,
				110544,
				120812,
				110536,
				120806,
				110532,
				84928,
				108016,
				119548,
				84448,
				107768,
				119422,
				84208,
				107644,
				84088,
				107582,
				84028,
				129640,
				85488,
				108284,
				129636,
				85240,
				108158,
				129634,
				85116,
				85054,
				128232,
				129654,
				85756,
				128228,
				85630,
				128226,
				125416,
				128246,
				125412,
				125410,
				119784,
				125430,
				119780,
				119778,
				108520,
				119798,
				108516,
				108514,
				83424,
				107256,
				119166,
				83184,
				107132,
				83064,
				107070,
				83004,
				82974,
				129588,
				83704,
				107390,
				129586,
				83580,
				83518,
				128116,
				83838,
				128114,
				125172,
				125170,
				119284,
				119282,
				107508,
				107506,
				82672,
				106876,
				82552,
				106814,
				82492,
				82462,
				129562,
				82812,
				82750,
				128058,
				125050,
				119034,
				82296,
				106686,
				82236,
				82206,
				82366,
				82108,
				82078,
				76736,
				103920,
				117500,
				76256,
				103672,
				117374,
				76016,
				103548,
				75896,
				103486,
				75836,
				129384,
				77296,
				104188,
				129380,
				77048,
				104062,
				129378,
				76924,
				76862,
				127720,
				129398,
				77564,
				127716,
				77438,
				127714,
				124392,
				127734,
				124388,
				124386,
				117736,
				124406,
				117732,
				117730,
				104424,
				117750,
				104420,
				104418,
				112096,
				121592,
				126334,
				92608,
				111856,
				121468,
				92384,
				111736,
				121406,
				92272,
				111676,
				92216,
				111646,
				92188,
				75232,
				103160,
				117118,
				93664,
				74992,
				103036,
				93424,
				112252,
				102974,
				93304,
				74812,
				93244,
				74782,
				93214,
				129332,
				75512,
				103294,
				129908,
				129330,
				93944,
				75388,
				129906,
				93820,
				75326,
				93758,
				127604,
				75646,
				128756,
				127602,
				94078,
				128754,
				124148,
				126452,
				124146,
				126450,
				117236,
				121844,
				117234,
				121842,
				103412,
				103410,
				91584,
				111344,
				121212,
				91360,
				111224,
				121150,
				91248,
				111164,
				91192,
				111134,
				91164,
				91150,
				74480,
				102780,
				91888,
				74360,
				102718,
				91768,
				111422,
				91708,
				74270,
				91678,
				129306,
				74620,
				129850,
				92028,
				74558,
				91966,
				127546,
				128634,
				124026,
				126202,
				116986,
				121338,
				102906,
				90848,
				110968,
				121022,
				90736,
				110908,
				90680,
				110878,
				90652,
				90638,
				74104,
				102590,
				91000,
				74044,
				90940,
				74014,
				90910,
				74174,
				91070,
				90480,
				110780,
				90424,
				110750,
				90396,
				90382,
				73916,
				90556,
				73886,
				90526,
				90296,
				110686,
				90268,
				90254,
				73822,
				90334,
				90204,
				90190,
				71136,
				101112,
				116094,
				70896,
				100988,
				70776,
				100926,
				70716,
				70686,
				129204,
				71416,
				101246,
				129202,
				71292,
				71230,
				127348,
				71550,
				127346,
				123636,
				123634,
				116212,
				116210,
				101364,
				101362,
				79296,
				105200,
				118140,
				79072,
				105080,
				118078,
				78960,
				105020,
				78904,
				104990,
				78876,
				78862,
				70384,
				100732,
				79600,
				70264,
				100670,
				79480,
				105278,
				79420,
				70174,
				79390,
				129178,
				70524,
				129466,
				79740,
				70462,
				79678,
				127290,
				127866,
				123514,
				124666,
				115962,
				118266,
				100858,
				113376,
				122232,
				126654,
				95424,
				113264,
				122172,
				95328,
				113208,
				122142,
				95280,
				113180,
				95256,
				113166,
				95244,
				78560,
				104824,
				117950,
				95968,
				78448,
				104764,
				95856,
				113468,
				104734,
				95800,
				78364,
				95772,
				78350,
				95758,
				70008,
				100542,
				78712,
				69948,
				96120,
				78652,
				69918,
				96060,
				78622,
				96030,
				70078,
				78782,
				96190,
				94912,
				113008,
				122044,
				94816,
				112952,
				122014,
				94768,
				112924,
				94744,
				112910,
				94732,
				94726,
				78192,
				104636,
				95088,
				78136,
				104606,
				95032,
				113054,
				95004,
				78094,
				94990,
				69820,
				78268,
				69790,
				95164,
				78238,
				95134,
				94560,
				112824,
				121950,
				94512,
				112796,
				94488,
				112782,
				94476,
				94470,
				78008,
				104542,
				94648,
				77980,
				94620,
				77966,
				94606,
				69726,
				78046,
				94686,
				94384,
				112732,
				94360,
				112718,
				94348,
				94342,
				77916,
				94428,
				77902,
				94414,
				94296,
				112686,
				94284,
				94278,
				77870,
				94318,
				94252,
				94246,
				68336,
				99708,
				68216,
				99646,
				68156,
				68126,
				68476,
				68414,
				127162,
				123258,
				115450,
				99834,
				72416,
				101752,
				116414,
				72304,
				101692,
				72248,
				101662,
				72220,
				72206,
				67960,
				99518,
				72568,
				67900,
				72508,
				67870,
				72478,
				68030,
				72638,
				80576,
				105840,
				118460,
				80480,
				105784,
				118430,
				80432,
				105756,
				80408,
				105742,
				80396,
				80390,
				72048,
				101564,
				80752,
				71992,
				101534,
				80696,
				71964,
				80668,
				71950,
				80654,
				67772,
				72124,
				67742,
				80828,
				72094,
				80798,
				114016,
				122552,
				126814,
				96832,
				113968,
				122524,
				96800,
				113944,
				122510,
				96784,
				113932,
				96776,
				113926,
				96772,
				80224,
				105656,
				118366,
				97120,
				80176,
				105628,
				97072,
				114076,
				105614,
				97048,
				80140,
				97036,
				80134,
				97030,
				71864,
				101470,
				80312,
				71836,
				97208,
				80284,
				71822,
				97180,
				80270,
				97166,
				67678,
				71902,
				80350,
				97246,
				96576,
				113840,
				122460,
				96544,
				113816,
				122446,
				96528,
				113804,
				96520,
				113798,
				96516,
				96514,
				80048,
				105564,
				96688,
				80024,
				105550,
				96664,
				113870,
				96652,
				80006,
				96646,
				71772,
				80092,
				71758,
				96732,
				80078,
				96718,
				96416,
				113752,
				122414,
				96400,
				113740,
				96392,
				113734,
				96388,
				96386,
				79960,
				105518,
				96472,
				79948,
				96460,
				79942,
				96454,
				71726,
				79982,
				96494,
				96336,
				113708,
				96328,
				113702,
				96324,
				96322,
				79916,
				96364,
				79910,
				96358,
				96296,
				113686,
				96292,
				96290,
				79894,
				96310,
				66936,
				99006,
				66876,
				66846,
				67006,
				68976,
				100028,
				68920,
				99998,
				68892,
				68878,
				66748,
				69052,
				66718,
				69022,
				73056,
				102072,
				116574,
				73008,
				102044,
				72984,
				102030,
				72972,
				72966,
				68792,
				99934,
				73144,
				68764,
				73116,
				68750,
				73102,
				66654,
				68830,
				73182,
				81216,
				106160,
				118620,
				81184,
				106136,
				118606,
				81168,
				106124,
				81160,
				106118,
				81156,
				81154,
				72880,
				101980,
				81328,
				72856,
				101966,
				81304,
				106190,
				81292,
				72838,
				81286,
				68700,
				72924,
				68686,
				81372,
				72910,
				81358,
				114336,
				122712,
				126894,
				114320,
				122700,
				114312,
				122694,
				114308,
				114306,
				81056,
				106072,
				118574,
				97696,
				81040,
				106060,
				97680,
				114380,
				106054,
				97672,
				81028,
				97668,
				81026,
				97666,
				72792,
				101934,
				81112,
				72780,
				97752,
				81100,
				72774,
				97740,
				81094,
				97734,
				68654,
				72814,
				81134,
				97774,
				114256,
				122668,
				114248,
				122662,
				114244,
				114242,
				80976,
				106028,
				97488,
				80968,
				106022,
				97480,
				114278,
				97476,
				80962,
				97474,
				72748,
				81004,
				72742,
				97516,
				80998,
				97510,
				114216,
				122646,
				114212,
				114210,
				80936,
				106006,
				97384,
				80932,
				97380,
				80930,
				97378,
				72726,
				80950,
				97398,
				114196,
				114194,
				80916,
				97332,
				80914,
				97330,
				66236,
				66206,
				67256,
				99166,
				67228,
				67214,
				66142,
				67294,
				69296,
				100188,
				69272,
				100174,
				69260,
				69254,
				67164,
				69340,
				67150,
				69326,
				73376,
				102232,
				116654,
				73360,
				102220,
				73352,
				102214,
				73348,
				73346,
				69208,
				100142,
				73432,
				102254,
				73420,
				69190,
				73414,
				67118,
				69230,
				73454,
				106320,
				118700,
				106312,
				118694,
				106308,
				106306,
				73296,
				102188,
				81616,
				106348,
				102182,
				81608,
				73284,
				81604,
				73282,
				81602,
				69164,
				73324,
				69158,
				81644,
				73318,
				81638,
				122792,
				126934,
				122788,
				122786,
				106280,
				118678,
				114536,
				106276,
				114532,
				106274,
				114530,
				73256,
				102166,
				81512,
				73252,
				98024,
				81508,
				73250,
				98020,
				81506,
				98018,
				69142,
				73270,
				81526,
				98038,
				122772,
				122770,
				106260,
				114484,
				106258,
				114482,
				73236,
				81460,
				73234,
				97908,
				81458,
				97906,
				122762,
				106250,
				114458,
				73226,
				81434,
				97850,
				66396,
				66382,
				67416,
				99246,
				67404,
				67398,
				66350,
				67438,
				69456,
				100268,
				69448,
				100262,
				69444,
				69442,
				67372,
				69484,
				67366,
				69478,
				102312,
				116694,
				102308,
				102306,
				69416,
				100246,
				73576,
				102326,
				73572,
				69410,
				73570,
				67350,
				69430,
				73590,
				118740,
				118738,
				102292,
				106420,
				102290,
				106418,
				69396,
				73524,
				69394,
				81780,
				73522,
				81778,
				118730,
				102282,
				106394,
				69386,
				73498,
				81722,
				66476,
				66470,
				67496,
				99286,
				67492,
				67490,
				66454,
				67510,
				100308,
				100306,
				67476,
				69556,
				67474,
				69554,
				116714
			}
		};

		// Token: 0x04000A59 RID: 2649
		private static readonly int[][] ERROR_LEVEL = new int[][]
		{
			new int[]
			{
				27,
				917
			},
			new int[]
			{
				522,
				568,
				723,
				809
			},
			new int[]
			{
				237,
				308,
				436,
				284,
				646,
				653,
				428,
				379
			},
			new int[]
			{
				274,
				562,
				232,
				755,
				599,
				524,
				801,
				132,
				295,
				116,
				442,
				428,
				295,
				42,
				176,
				65
			},
			new int[]
			{
				361,
				575,
				922,
				525,
				176,
				586,
				640,
				321,
				536,
				742,
				677,
				742,
				687,
				284,
				193,
				517,
				273,
				494,
				263,
				147,
				593,
				800,
				571,
				320,
				803,
				133,
				231,
				390,
				685,
				330,
				63,
				410
			},
			new int[]
			{
				539,
				422,
				6,
				93,
				862,
				771,
				453,
				106,
				610,
				287,
				107,
				505,
				733,
				877,
				381,
				612,
				723,
				476,
				462,
				172,
				430,
				609,
				858,
				822,
				543,
				376,
				511,
				400,
				672,
				762,
				283,
				184,
				440,
				35,
				519,
				31,
				460,
				594,
				225,
				535,
				517,
				352,
				605,
				158,
				651,
				201,
				488,
				502,
				648,
				733,
				717,
				83,
				404,
				97,
				280,
				771,
				840,
				629,
				4,
				381,
				843,
				623,
				264,
				543
			},
			new int[]
			{
				521,
				310,
				864,
				547,
				858,
				580,
				296,
				379,
				53,
				779,
				897,
				444,
				400,
				925,
				749,
				415,
				822,
				93,
				217,
				208,
				928,
				244,
				583,
				620,
				246,
				148,
				447,
				631,
				292,
				908,
				490,
				704,
				516,
				258,
				457,
				907,
				594,
				723,
				674,
				292,
				272,
				96,
				684,
				432,
				686,
				606,
				860,
				569,
				193,
				219,
				129,
				186,
				236,
				287,
				192,
				775,
				278,
				173,
				40,
				379,
				712,
				463,
				646,
				776,
				171,
				491,
				297,
				763,
				156,
				732,
				95,
				270,
				447,
				90,
				507,
				48,
				228,
				821,
				808,
				898,
				784,
				663,
				627,
				378,
				382,
				262,
				380,
				602,
				754,
				336,
				89,
				614,
				87,
				432,
				670,
				616,
				157,
				374,
				242,
				726,
				600,
				269,
				375,
				898,
				845,
				454,
				354,
				130,
				814,
				587,
				804,
				34,
				211,
				330,
				539,
				297,
				827,
				865,
				37,
				517,
				834,
				315,
				550,
				86,
				801,
				4,
				108,
				539
			},
			new int[]
			{
				524,
				894,
				75,
				766,
				882,
				857,
				74,
				204,
				82,
				586,
				708,
				250,
				905,
				786,
				138,
				720,
				858,
				194,
				311,
				913,
				275,
				190,
				375,
				850,
				438,
				733,
				194,
				280,
				201,
				280,
				828,
				757,
				710,
				814,
				919,
				89,
				68,
				569,
				11,
				204,
				796,
				605,
				540,
				913,
				801,
				700,
				799,
				137,
				439,
				418,
				592,
				668,
				353,
				859,
				370,
				694,
				325,
				240,
				216,
				257,
				284,
				549,
				209,
				884,
				315,
				70,
				329,
				793,
				490,
				274,
				877,
				162,
				749,
				812,
				684,
				461,
				334,
				376,
				849,
				521,
				307,
				291,
				803,
				712,
				19,
				358,
				399,
				908,
				103,
				511,
				51,
				8,
				517,
				225,
				289,
				470,
				637,
				731,
				66,
				255,
				917,
				269,
				463,
				830,
				730,
				433,
				848,
				585,
				136,
				538,
				906,
				90,
				2,
				290,
				743,
				199,
				655,
				903,
				329,
				49,
				802,
				580,
				355,
				588,
				188,
				462,
				10,
				134,
				628,
				320,
				479,
				130,
				739,
				71,
				263,
				318,
				374,
				601,
				192,
				605,
				142,
				673,
				687,
				234,
				722,
				384,
				177,
				752,
				607,
				640,
				455,
				193,
				689,
				707,
				805,
				641,
				48,
				60,
				732,
				621,
				895,
				544,
				261,
				852,
				655,
				309,
				697,
				755,
				756,
				60,
				231,
				773,
				434,
				421,
				726,
				528,
				503,
				118,
				49,
				795,
				32,
				144,
				500,
				238,
				836,
				394,
				280,
				566,
				319,
				9,
				647,
				550,
				73,
				914,
				342,
				126,
				32,
				681,
				331,
				792,
				620,
				60,
				609,
				441,
				180,
				791,
				893,
				754,
				605,
				383,
				228,
				749,
				760,
				213,
				54,
				297,
				134,
				54,
				834,
				299,
				922,
				191,
				910,
				532,
				609,
				829,
				189,
				20,
				167,
				29,
				872,
				449,
				83,
				402,
				41,
				656,
				505,
				579,
				481,
				173,
				404,
				251,
				688,
				95,
				497,
				555,
				642,
				543,
				307,
				159,
				924,
				558,
				648,
				55,
				497,
				10
			},
			new int[]
			{
				352,
				77,
				373,
				504,
				35,
				599,
				428,
				207,
				409,
				574,
				118,
				498,
				285,
				380,
				350,
				492,
				197,
				265,
				920,
				155,
				914,
				299,
				229,
				643,
				294,
				871,
				306,
				88,
				87,
				193,
				352,
				781,
				846,
				75,
				327,
				520,
				435,
				543,
				203,
				666,
				249,
				346,
				781,
				621,
				640,
				268,
				794,
				534,
				539,
				781,
				408,
				390,
				644,
				102,
				476,
				499,
				290,
				632,
				545,
				37,
				858,
				916,
				552,
				41,
				542,
				289,
				122,
				272,
				383,
				800,
				485,
				98,
				752,
				472,
				761,
				107,
				784,
				860,
				658,
				741,
				290,
				204,
				681,
				407,
				855,
				85,
				99,
				62,
				482,
				180,
				20,
				297,
				451,
				593,
				913,
				142,
				808,
				684,
				287,
				536,
				561,
				76,
				653,
				899,
				729,
				567,
				744,
				390,
				513,
				192,
				516,
				258,
				240,
				518,
				794,
				395,
				768,
				848,
				51,
				610,
				384,
				168,
				190,
				826,
				328,
				596,
				786,
				303,
				570,
				381,
				415,
				641,
				156,
				237,
				151,
				429,
				531,
				207,
				676,
				710,
				89,
				168,
				304,
				402,
				40,
				708,
				575,
				162,
				864,
				229,
				65,
				861,
				841,
				512,
				164,
				477,
				221,
				92,
				358,
				785,
				288,
				357,
				850,
				836,
				827,
				736,
				707,
				94,
				8,
				494,
				114,
				521,
				2,
				499,
				851,
				543,
				152,
				729,
				771,
				95,
				248,
				361,
				578,
				323,
				856,
				797,
				289,
				51,
				684,
				466,
				533,
				820,
				669,
				45,
				902,
				452,
				167,
				342,
				244,
				173,
				35,
				463,
				651,
				51,
				699,
				591,
				452,
				578,
				37,
				124,
				298,
				332,
				552,
				43,
				427,
				119,
				662,
				777,
				475,
				850,
				764,
				364,
				578,
				911,
				283,
				711,
				472,
				420,
				245,
				288,
				594,
				394,
				511,
				327,
				589,
				777,
				699,
				688,
				43,
				408,
				842,
				383,
				721,
				521,
				560,
				644,
				714,
				559,
				62,
				145,
				873,
				663,
				713,
				159,
				672,
				729,
				624,
				59,
				193,
				417,
				158,
				209,
				563,
				564,
				343,
				693,
				109,
				608,
				563,
				365,
				181,
				772,
				677,
				310,
				248,
				353,
				708,
				410,
				579,
				870,
				617,
				841,
				632,
				860,
				289,
				536,
				35,
				777,
				618,
				586,
				424,
				833,
				77,
				597,
				346,
				269,
				757,
				632,
				695,
				751,
				331,
				247,
				184,
				45,
				787,
				680,
				18,
				66,
				407,
				369,
				54,
				492,
				228,
				613,
				830,
				922,
				437,
				519,
				644,
				905,
				789,
				420,
				305,
				441,
				207,
				300,
				892,
				827,
				141,
				537,
				381,
				662,
				513,
				56,
				252,
				341,
				242,
				797,
				838,
				837,
				720,
				224,
				307,
				631,
				61,
				87,
				560,
				310,
				756,
				665,
				397,
				808,
				851,
				309,
				473,
				795,
				378,
				31,
				647,
				915,
				459,
				806,
				590,
				731,
				425,
				216,
				548,
				249,
				321,
				881,
				699,
				535,
				673,
				782,
				210,
				815,
				905,
				303,
				843,
				922,
				281,
				73,
				469,
				791,
				660,
				162,
				498,
				308,
				155,
				422,
				907,
				817,
				187,
				62,
				16,
				425,
				535,
				336,
				286,
				437,
				375,
				273,
				610,
				296,
				183,
				923,
				116,
				667,
				751,
				353,
				62,
				366,
				691,
				379,
				687,
				842,
				37,
				357,
				720,
				742,
				330,
				5,
				39,
				923,
				311,
				424,
				242,
				749,
				321,
				54,
				669,
				316,
				342,
				299,
				534,
				105,
				667,
				488,
				640,
				672,
				576,
				540,
				316,
				486,
				721,
				610,
				46,
				656,
				447,
				171,
				616,
				464,
				190,
				531,
				297,
				321,
				762,
				752,
				533,
				175,
				134,
				14,
				381,
				433,
				717,
				45,
				111,
				20,
				596,
				284,
				736,
				138,
				646,
				411,
				877,
				669,
				141,
				919,
				45,
				780,
				407,
				164,
				332,
				899,
				165,
				726,
				600,
				325,
				498,
				655,
				357,
				752,
				768,
				223,
				849,
				647,
				63,
				310,
				863,
				251,
				366,
				304,
				282,
				738,
				675,
				410,
				389,
				244,
				31,
				121,
				303,
				263
			}
		};

		// Token: 0x04000A5A RID: 2650
		private byte[] outBits;

		// Token: 0x04000A5B RID: 2651
		private int bitColumns;

		// Token: 0x04000A5C RID: 2652
		private int codeRows;

		// Token: 0x04000A5D RID: 2653
		private int codeColumns;

		// Token: 0x04000A5E RID: 2654
		private int[] codewords = new int[928];

		// Token: 0x04000A5F RID: 2655
		private int lenCodewords;

		// Token: 0x04000A60 RID: 2656
		private int errorLevel;

		// Token: 0x04000A61 RID: 2657
		private byte[] text;

		// Token: 0x04000A62 RID: 2658
		private int options;

		// Token: 0x04000A63 RID: 2659
		private float aspectRatio;

		// Token: 0x04000A64 RID: 2660
		private float yHeight;

		// Token: 0x0200016F RID: 367
		protected class Segment
		{
			// Token: 0x06000E11 RID: 3601 RVA: 0x0005242F File Offset: 0x0005142F
			public Segment(char type, int start, int end)
			{
				this.type = type;
				this.start = start;
				this.end = end;
			}

			// Token: 0x04000A65 RID: 2661
			public char type;

			// Token: 0x04000A66 RID: 2662
			public int start;

			// Token: 0x04000A67 RID: 2663
			public int end;
		}

		// Token: 0x02000170 RID: 368
		protected class SegmentList
		{
			// Token: 0x06000E12 RID: 3602 RVA: 0x0005244C File Offset: 0x0005144C
			public void Add(char type, int start, int end)
			{
				this.list.Add(new BarcodePDF417.Segment(type, start, end));
			}

			// Token: 0x06000E13 RID: 3603 RVA: 0x00052461 File Offset: 0x00051461
			public BarcodePDF417.Segment Get(int idx)
			{
				if (idx < 0 || idx >= this.list.Count)
				{
					return null;
				}
				return this.list[idx];
			}

			// Token: 0x06000E14 RID: 3604 RVA: 0x00052483 File Offset: 0x00051483
			public void Remove(int idx)
			{
				if (idx < 0 || idx >= this.list.Count)
				{
					return;
				}
				this.list.RemoveAt(idx);
			}

			// Token: 0x170002B3 RID: 691
			// (get) Token: 0x06000E15 RID: 3605 RVA: 0x000524A4 File Offset: 0x000514A4
			public int Size
			{
				get
				{
					return this.list.Count;
				}
			}

			// Token: 0x04000A68 RID: 2664
			protected List<BarcodePDF417.Segment> list = new List<BarcodePDF417.Segment>();
		}
	}
}
