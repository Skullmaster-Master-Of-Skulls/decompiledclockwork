using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x02000533 RID: 1331
	public class TIFFFaxDecoder
	{
		// Token: 0x06002DB6 RID: 11702 RVA: 0x00116CC8 File Offset: 0x00115CC8
		public TIFFFaxDecoder(int fillOrder, int w, int h)
		{
			this.fillOrder = fillOrder;
			this.w = w;
			this.h = h;
			this.bitPointer = 0;
			this.bytePointer = 0;
			this.prevChangingElems = new int[w];
			this.currChangingElems = new int[w];
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x00116D20 File Offset: 0x00115D20
		public static void ReverseBits(byte[] b)
		{
			for (int i = 0; i < b.Length; i++)
			{
				b[i] = TIFFFaxDecoder.flipTable[(int)(b[i] & byte.MaxValue)];
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x00116D50 File Offset: 0x00115D50
		public void Decode1D(byte[] buffer, byte[] compData, int startX, int height)
		{
			this.data = compData;
			int num = 0;
			int num2 = (this.w + 7) / 8;
			this.bitPointer = 0;
			this.bytePointer = 0;
			for (int i = 0; i < height; i++)
			{
				this.DecodeNextScanline(buffer, num, startX);
				num += num2;
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x00116D9C File Offset: 0x00115D9C
		public void DecodeNextScanline(byte[] buffer, int lineOffset, int bitOffset)
		{
			bool flag = true;
			this.changingElemSize = 0;
			while (bitOffset < this.w)
			{
				while (flag)
				{
					int num = this.NextNBits(10);
					int num2 = (int)TIFFFaxDecoder.white[num];
					int num3 = num2 & 1;
					int num4 = num2 >> 1 & 15;
					if (num4 == 12)
					{
						int num5 = this.NextLesserThan8Bits(2);
						num = ((num << 2 & 12) | num5);
						num2 = (int)TIFFFaxDecoder.additionalMakeup[num];
						num4 = (num2 >> 1 & 7);
						int num6 = num2 >> 4 & 4095;
						bitOffset += num6;
						this.UpdatePointer(4 - num4);
					}
					else
					{
						if (num4 == 0)
						{
							throw new Exception(MessageLocalization.GetComposedMessage("invalid.code.encountered"));
						}
						if (num4 == 15)
						{
							throw new Exception(MessageLocalization.GetComposedMessage("eol.code.word.encountered.in.white.run"));
						}
						int num6 = num2 >> 5 & 2047;
						bitOffset += num6;
						this.UpdatePointer(10 - num4);
						if (num3 == 0)
						{
							flag = false;
							this.currChangingElems[this.changingElemSize++] = bitOffset;
						}
					}
				}
				if (bitOffset == this.w)
				{
					if (this.compression == 2)
					{
						this.AdvancePointer();
						break;
					}
					break;
				}
				else
				{
					while (!flag)
					{
						int num = this.NextLesserThan8Bits(4);
						int num2 = (int)TIFFFaxDecoder.initBlack[num];
						int num3 = num2 & 1;
						int num4 = num2 >> 1 & 15;
						int num6 = num2 >> 5 & 2047;
						if (num6 == 100)
						{
							num = this.NextNBits(9);
							num2 = (int)TIFFFaxDecoder.black[num];
							num3 = (num2 & 1);
							num4 = (num2 >> 1 & 15);
							num6 = (num2 >> 5 & 2047);
							if (num4 == 12)
							{
								this.UpdatePointer(5);
								num = this.NextLesserThan8Bits(4);
								num2 = (int)TIFFFaxDecoder.additionalMakeup[num];
								num4 = (num2 >> 1 & 7);
								num6 = (num2 >> 4 & 4095);
								this.SetToBlack(buffer, lineOffset, bitOffset, num6);
								bitOffset += num6;
								this.UpdatePointer(4 - num4);
							}
							else
							{
								if (num4 == 15)
								{
									throw new Exception(MessageLocalization.GetComposedMessage("eol.code.word.encountered.in.black.run"));
								}
								this.SetToBlack(buffer, lineOffset, bitOffset, num6);
								bitOffset += num6;
								this.UpdatePointer(9 - num4);
								if (num3 == 0)
								{
									flag = true;
									this.currChangingElems[this.changingElemSize++] = bitOffset;
								}
							}
						}
						else if (num6 == 200)
						{
							num = this.NextLesserThan8Bits(2);
							num2 = (int)TIFFFaxDecoder.twoBitBlack[num];
							num6 = (num2 >> 5 & 2047);
							num4 = (num2 >> 1 & 15);
							this.SetToBlack(buffer, lineOffset, bitOffset, num6);
							bitOffset += num6;
							this.UpdatePointer(2 - num4);
							flag = true;
							this.currChangingElems[this.changingElemSize++] = bitOffset;
						}
						else
						{
							this.SetToBlack(buffer, lineOffset, bitOffset, num6);
							bitOffset += num6;
							this.UpdatePointer(4 - num4);
							flag = true;
							this.currChangingElems[this.changingElemSize++] = bitOffset;
						}
					}
					if (bitOffset == this.w)
					{
						if (this.compression == 2)
						{
							this.AdvancePointer();
							break;
						}
						break;
					}
				}
			}
			this.currChangingElems[this.changingElemSize++] = bitOffset;
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x00117090 File Offset: 0x00116090
		public void Decode2D(byte[] buffer, byte[] compData, int startX, int height, long tiffT4Options)
		{
			this.data = compData;
			this.compression = 3;
			this.bitPointer = 0;
			this.bytePointer = 0;
			int num = (this.w + 7) / 8;
			int[] array = new int[2];
			this.oneD = (int)(tiffT4Options & 1L);
			this.uncompressedMode = (int)((tiffT4Options & 2L) >> 1);
			this.fillBits = (int)((tiffT4Options & 4L) >> 2);
			if (this.ReadEOL(true) != 1)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("first.scanline.must.be.1d.encoded"));
			}
			int num2 = 0;
			this.DecodeNextScanline(buffer, num2, startX);
			num2 += num;
			for (int i = 1; i < height; i++)
			{
				if (this.ReadEOL(false) == 0)
				{
					int[] array2 = this.prevChangingElems;
					this.prevChangingElems = this.currChangingElems;
					this.currChangingElems = array2;
					int num3 = 0;
					int a = -1;
					bool flag = true;
					int j = startX;
					this.lastChangingElement = 0;
					while (j < this.w)
					{
						this.GetNextChangingElement(a, flag, array);
						int num4 = array[0];
						int num5 = array[1];
						int num6 = this.NextLesserThan8Bits(7);
						num6 = (int)(TIFFFaxDecoder.twoDCodes[num6] & byte.MaxValue);
						int num7 = (num6 & 120) >> 3;
						int num8 = num6 & 7;
						if (num7 == 0)
						{
							if (!flag)
							{
								this.SetToBlack(buffer, num2, j, num5 - j);
							}
							a = (j = num5);
							this.UpdatePointer(7 - num8);
						}
						else if (num7 == 1)
						{
							this.UpdatePointer(7 - num8);
							if (flag)
							{
								int num9 = this.DecodeWhiteCodeWord();
								j += num9;
								this.currChangingElems[num3++] = j;
								num9 = this.DecodeBlackCodeWord();
								this.SetToBlack(buffer, num2, j, num9);
								j += num9;
								this.currChangingElems[num3++] = j;
							}
							else
							{
								int num9 = this.DecodeBlackCodeWord();
								this.SetToBlack(buffer, num2, j, num9);
								j += num9;
								this.currChangingElems[num3++] = j;
								num9 = this.DecodeWhiteCodeWord();
								j += num9;
								this.currChangingElems[num3++] = j;
							}
							a = j;
						}
						else
						{
							if (num7 > 8)
							{
								throw new Exception(MessageLocalization.GetComposedMessage("invalid.code.encountered.while.decoding.2d.group.3.compressed.data"));
							}
							int num10 = num4 + (num7 - 5);
							this.currChangingElems[num3++] = num10;
							if (!flag)
							{
								this.SetToBlack(buffer, num2, j, num10 - j);
							}
							a = (j = num10);
							flag = !flag;
							this.UpdatePointer(7 - num8);
						}
					}
					this.currChangingElems[num3++] = j;
					this.changingElemSize = num3;
				}
				else
				{
					this.DecodeNextScanline(buffer, num2, startX);
				}
				num2 += num;
			}
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x00117328 File Offset: 0x00116328
		public void DecodeT6(byte[] buffer, byte[] compData, int startX, int height, long tiffT6Options)
		{
			this.data = compData;
			this.compression = 4;
			this.bitPointer = 0;
			this.bytePointer = 0;
			int num = (this.w + 7) / 8;
			int[] array = new int[2];
			this.uncompressedMode = (int)((tiffT6Options & 2L) >> 1);
			int[] array2 = this.currChangingElems;
			this.changingElemSize = 0;
			array2[this.changingElemSize++] = this.w;
			array2[this.changingElemSize++] = this.w;
			int num2 = 0;
			for (int i = 0; i < height; i++)
			{
				int a = -1;
				bool flag = true;
				int[] array3 = this.prevChangingElems;
				this.prevChangingElems = this.currChangingElems;
				array2 = (this.currChangingElems = array3);
				int num3 = 0;
				int j = startX;
				this.lastChangingElement = 0;
				while (j < this.w)
				{
					this.GetNextChangingElement(a, flag, array);
					int num4 = array[0];
					int num5 = array[1];
					int num6 = this.NextLesserThan8Bits(7);
					num6 = (int)(TIFFFaxDecoder.twoDCodes[num6] & byte.MaxValue);
					int num7 = (num6 & 120) >> 3;
					int num8 = num6 & 7;
					if (num7 == 0)
					{
						if (!flag)
						{
							this.SetToBlack(buffer, num2, j, num5 - j);
						}
						a = (j = num5);
						this.UpdatePointer(7 - num8);
					}
					else if (num7 == 1)
					{
						this.UpdatePointer(7 - num8);
						if (flag)
						{
							int num9 = this.DecodeWhiteCodeWord();
							j += num9;
							array2[num3++] = j;
							num9 = this.DecodeBlackCodeWord();
							this.SetToBlack(buffer, num2, j, num9);
							j += num9;
							array2[num3++] = j;
						}
						else
						{
							int num9 = this.DecodeBlackCodeWord();
							this.SetToBlack(buffer, num2, j, num9);
							j += num9;
							array2[num3++] = j;
							num9 = this.DecodeWhiteCodeWord();
							j += num9;
							array2[num3++] = j;
						}
						a = j;
					}
					else if (num7 <= 8)
					{
						int num10 = num4 + (num7 - 5);
						array2[num3++] = num10;
						if (!flag)
						{
							this.SetToBlack(buffer, num2, j, num10 - j);
						}
						a = (j = num10);
						flag = !flag;
						this.UpdatePointer(7 - num8);
					}
					else if (num7 == 11)
					{
						if (this.NextLesserThan8Bits(3) != 7)
						{
							throw new Exception(MessageLocalization.GetComposedMessage("invalid.code.encountered.while.decoding.2d.group.4.compressed.data"));
						}
						int num11 = 0;
						bool flag2 = false;
						while (!flag2)
						{
							while (this.NextLesserThan8Bits(1) != 1)
							{
								num11++;
							}
							if (num11 > 5)
							{
								num11 -= 6;
								if (!flag && num11 > 0)
								{
									array2[num3++] = j;
								}
								j += num11;
								if (num11 > 0)
								{
									flag = true;
								}
								if (this.NextLesserThan8Bits(1) == 0)
								{
									if (!flag)
									{
										array2[num3++] = j;
									}
									flag = true;
								}
								else
								{
									if (flag)
									{
										array2[num3++] = j;
									}
									flag = false;
								}
								flag2 = true;
							}
							if (num11 == 5)
							{
								if (!flag)
								{
									array2[num3++] = j;
								}
								j += num11;
								flag = true;
							}
							else
							{
								j += num11;
								array2[num3++] = j;
								this.SetToBlack(buffer, num2, j, 1);
								j++;
								flag = false;
							}
						}
					}
					else
					{
						j = this.w;
						this.UpdatePointer(7 - num8);
					}
				}
				if (num3 < array2.Length)
				{
					array2[num3++] = j;
				}
				this.changingElemSize = num3;
				num2 += num;
			}
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x00117698 File Offset: 0x00116698
		private void SetToBlack(byte[] buffer, int lineOffset, int bitOffset, int numBits)
		{
			int i = 8 * lineOffset + bitOffset;
			int num = i + numBits;
			int num2 = i >> 3;
			int num3 = i & 7;
			if (num3 > 0)
			{
				int num4 = 1 << 7 - num3;
				byte b = buffer[num2];
				while (num4 > 0 && i < num)
				{
					b |= (byte)num4;
					num4 >>= 1;
					i++;
				}
				buffer[num2] = b;
			}
			num2 = i >> 3;
			while (i < num - 7)
			{
				buffer[num2++] = byte.MaxValue;
				i += 8;
			}
			while (i < num)
			{
				num2 = i >> 3;
				int num5 = num2;
				buffer[num5] |= (byte)(1 << 7 - (i & 7));
				i++;
			}
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x00117738 File Offset: 0x00116738
		private int DecodeWhiteCodeWord()
		{
			int num = 0;
			bool flag = true;
			while (flag)
			{
				int num2 = this.NextNBits(10);
				int num3 = (int)TIFFFaxDecoder.white[num2];
				int num4 = num3 & 1;
				int num5 = num3 >> 1 & 15;
				if (num5 == 12)
				{
					int num6 = this.NextLesserThan8Bits(2);
					num2 = ((num2 << 2 & 12) | num6);
					num3 = (int)TIFFFaxDecoder.additionalMakeup[num2];
					num5 = (num3 >> 1 & 7);
					int num7 = num3 >> 4 & 4095;
					num += num7;
					this.UpdatePointer(4 - num5);
				}
				else
				{
					if (num5 == 0)
					{
						throw new Exception(MessageLocalization.GetComposedMessage("invalid.code.encountered"));
					}
					if (num5 == 15)
					{
						throw new Exception(MessageLocalization.GetComposedMessage("eol.code.word.encountered.in.white.run"));
					}
					int num7 = num3 >> 5 & 2047;
					num += num7;
					this.UpdatePointer(10 - num5);
					if (num4 == 0)
					{
						flag = false;
					}
				}
			}
			return num;
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x00117808 File Offset: 0x00116808
		private int DecodeBlackCodeWord()
		{
			int num = 0;
			bool flag = false;
			while (!flag)
			{
				int num2 = this.NextLesserThan8Bits(4);
				int num3 = (int)TIFFFaxDecoder.initBlack[num2];
				int num4 = num3 & 1;
				int num5 = num3 >> 1 & 15;
				int num6 = num3 >> 5 & 2047;
				if (num6 == 100)
				{
					num2 = this.NextNBits(9);
					num3 = (int)TIFFFaxDecoder.black[num2];
					num4 = (num3 & 1);
					num5 = (num3 >> 1 & 15);
					num6 = (num3 >> 5 & 2047);
					if (num5 == 12)
					{
						this.UpdatePointer(5);
						num2 = this.NextLesserThan8Bits(4);
						num3 = (int)TIFFFaxDecoder.additionalMakeup[num2];
						num5 = (num3 >> 1 & 7);
						num6 = (num3 >> 4 & 4095);
						num += num6;
						this.UpdatePointer(4 - num5);
					}
					else
					{
						if (num5 == 15)
						{
							throw new Exception(MessageLocalization.GetComposedMessage("eol.code.word.encountered.in.black.run"));
						}
						num += num6;
						this.UpdatePointer(9 - num5);
						if (num4 == 0)
						{
							flag = true;
						}
					}
				}
				else if (num6 == 200)
				{
					num2 = this.NextLesserThan8Bits(2);
					num3 = (int)TIFFFaxDecoder.twoBitBlack[num2];
					num6 = (num3 >> 5 & 2047);
					num += num6;
					num5 = (num3 >> 1 & 15);
					this.UpdatePointer(2 - num5);
					flag = true;
				}
				else
				{
					num += num6;
					this.UpdatePointer(4 - num5);
					flag = true;
				}
			}
			return num;
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x00117948 File Offset: 0x00116948
		private int ReadEOL(bool isFirstEOL)
		{
			if (this.fillBits == 0)
			{
				int num = this.NextNBits(12);
				if (isFirstEOL && num == 0 && this.NextNBits(4) == 1)
				{
					this.fillBits = 1;
					return 1;
				}
				if (num != 1)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("scanline.must.begin.with.eol.code.word"));
				}
			}
			else if (this.fillBits == 1)
			{
				int num2 = 8 - this.bitPointer;
				if (this.NextNBits(num2) != 0)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("all.fill.bits.preceding.eol.code.must.be.0"));
				}
				if (num2 < 4 && this.NextNBits(8) != 0)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("all.fill.bits.preceding.eol.code.must.be.0"));
				}
				int num3;
				while ((num3 = this.NextNBits(8)) != 1)
				{
					if (num3 != 0)
					{
						throw new Exception(MessageLocalization.GetComposedMessage("all.fill.bits.preceding.eol.code.must.be.0"));
					}
				}
			}
			if (this.oneD == 0)
			{
				return 1;
			}
			return this.NextLesserThan8Bits(1);
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x00117A0C File Offset: 0x00116A0C
		private void GetNextChangingElement(int a0, bool isWhite, int[] ret)
		{
			int[] array = this.prevChangingElems;
			int num = this.changingElemSize;
			int num2 = (this.lastChangingElement > 0) ? (this.lastChangingElement - 1) : 0;
			if (isWhite)
			{
				num2 &= -2;
			}
			else
			{
				num2 |= 1;
			}
			int i;
			for (i = num2; i < num; i += 2)
			{
				int num3 = array[i];
				if (num3 > a0)
				{
					this.lastChangingElement = i;
					ret[0] = num3;
					break;
				}
			}
			if (i + 1 < num)
			{
				ret[1] = array[i + 1];
			}
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x00117A7C File Offset: 0x00116A7C
		private int NextNBits(int bitsToGet)
		{
			int num = this.data.Length - 1;
			int num2 = this.bytePointer;
			byte b;
			byte b2;
			byte b3;
			if (this.fillOrder == 1)
			{
				b = this.data[num2];
				if (num2 == num)
				{
					b2 = 0;
					b3 = 0;
				}
				else if (num2 + 1 == num)
				{
					b2 = this.data[num2 + 1];
					b3 = 0;
				}
				else
				{
					b2 = this.data[num2 + 1];
					b3 = this.data[num2 + 2];
				}
			}
			else
			{
				if (this.fillOrder != 2)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("tiff.fill.order.tag.must.be.either.1.or.2"));
				}
				b = TIFFFaxDecoder.flipTable[(int)(this.data[num2] & byte.MaxValue)];
				if (num2 == num)
				{
					b2 = 0;
					b3 = 0;
				}
				else if (num2 + 1 == num)
				{
					b2 = TIFFFaxDecoder.flipTable[(int)(this.data[num2 + 1] & byte.MaxValue)];
					b3 = 0;
				}
				else
				{
					b2 = TIFFFaxDecoder.flipTable[(int)(this.data[num2 + 1] & byte.MaxValue)];
					b3 = TIFFFaxDecoder.flipTable[(int)(this.data[num2 + 2] & byte.MaxValue)];
				}
			}
			int num3 = 8 - this.bitPointer;
			int num4 = bitsToGet - num3;
			int num5 = 0;
			if (num4 > 8)
			{
				num5 = num4 - 8;
				num4 = 8;
			}
			this.bytePointer++;
			int num6 = ((int)b & TIFFFaxDecoder.table1[num3]) << bitsToGet - num3;
			int num7 = ((int)b2 & TIFFFaxDecoder.table2[num4]) >> 8 - num4;
			if (num5 != 0)
			{
				num7 <<= num5;
				int num8 = ((int)b3 & TIFFFaxDecoder.table2[num5]) >> 8 - num5;
				num7 |= num8;
				this.bytePointer++;
				this.bitPointer = num5;
			}
			else if (num4 == 8)
			{
				this.bitPointer = 0;
				this.bytePointer++;
			}
			else
			{
				this.bitPointer = num4;
			}
			return num6 | num7;
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x00117C4C File Offset: 0x00116C4C
		private int NextLesserThan8Bits(int bitsToGet)
		{
			int num = this.data.Length - 1;
			int num2 = this.bytePointer;
			byte b;
			byte b2;
			if (this.fillOrder == 1)
			{
				b = this.data[num2];
				if (num2 == num)
				{
					b2 = 0;
				}
				else
				{
					b2 = this.data[num2 + 1];
				}
			}
			else
			{
				if (this.fillOrder != 2)
				{
					throw new Exception(MessageLocalization.GetComposedMessage("tiff.fill.order.tag.must.be.either.1.or.2"));
				}
				b = TIFFFaxDecoder.flipTable[(int)(this.data[num2] & byte.MaxValue)];
				if (num2 == num)
				{
					b2 = 0;
				}
				else
				{
					b2 = TIFFFaxDecoder.flipTable[(int)(this.data[num2 + 1] & byte.MaxValue)];
				}
			}
			int num3 = 8 - this.bitPointer;
			int num4 = bitsToGet - num3;
			int num5 = num3 - bitsToGet;
			int num6;
			if (num5 >= 0)
			{
				num6 = ((int)b & TIFFFaxDecoder.table1[num3]) >> num5;
				this.bitPointer += bitsToGet;
				if (this.bitPointer == 8)
				{
					this.bitPointer = 0;
					this.bytePointer++;
				}
			}
			else
			{
				num6 = ((int)b & TIFFFaxDecoder.table1[num3]) << -num5;
				int num7 = ((int)b2 & TIFFFaxDecoder.table2[num4]) >> 8 - num4;
				num6 |= num7;
				this.bytePointer++;
				this.bitPointer = num4;
			}
			return num6;
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x00117D84 File Offset: 0x00116D84
		private void UpdatePointer(int bitsToMoveBack)
		{
			int num = this.bitPointer - bitsToMoveBack;
			if (num < 0)
			{
				this.bytePointer--;
				this.bitPointer = 8 + num;
				return;
			}
			this.bitPointer = num;
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x00117DBD File Offset: 0x00116DBD
		private bool AdvancePointer()
		{
			if (this.bitPointer != 0)
			{
				this.bytePointer++;
				this.bitPointer = 0;
			}
			return true;
		}

		// Token: 0x04001F7E RID: 8062
		private int bitPointer;

		// Token: 0x04001F7F RID: 8063
		private int bytePointer;

		// Token: 0x04001F80 RID: 8064
		private byte[] data;

		// Token: 0x04001F81 RID: 8065
		private int w;

		// Token: 0x04001F82 RID: 8066
		private int h;

		// Token: 0x04001F83 RID: 8067
		private int fillOrder;

		// Token: 0x04001F84 RID: 8068
		private int changingElemSize;

		// Token: 0x04001F85 RID: 8069
		private int[] prevChangingElems;

		// Token: 0x04001F86 RID: 8070
		private int[] currChangingElems;

		// Token: 0x04001F87 RID: 8071
		private int lastChangingElement;

		// Token: 0x04001F88 RID: 8072
		private int compression = 2;

		// Token: 0x04001F89 RID: 8073
		private int uncompressedMode;

		// Token: 0x04001F8A RID: 8074
		private int fillBits;

		// Token: 0x04001F8B RID: 8075
		private int oneD;

		// Token: 0x04001F8C RID: 8076
		private static int[] table1 = new int[]
		{
			0,
			1,
			3,
			7,
			15,
			31,
			63,
			127,
			255
		};

		// Token: 0x04001F8D RID: 8077
		private static int[] table2 = new int[]
		{
			0,
			128,
			192,
			224,
			240,
			248,
			252,
			254,
			255
		};

		// Token: 0x04001F8E RID: 8078
		internal static byte[] flipTable = new byte[]
		{
			0,
			128,
			64,
			192,
			32,
			160,
			96,
			224,
			16,
			144,
			80,
			208,
			48,
			176,
			112,
			240,
			8,
			136,
			72,
			200,
			40,
			168,
			104,
			232,
			24,
			152,
			88,
			216,
			56,
			184,
			120,
			248,
			4,
			132,
			68,
			196,
			36,
			164,
			100,
			228,
			20,
			148,
			84,
			212,
			52,
			180,
			116,
			244,
			12,
			140,
			76,
			204,
			44,
			172,
			108,
			236,
			28,
			156,
			92,
			220,
			60,
			188,
			124,
			252,
			2,
			130,
			66,
			194,
			34,
			162,
			98,
			226,
			18,
			146,
			82,
			210,
			50,
			178,
			114,
			242,
			10,
			138,
			74,
			202,
			42,
			170,
			106,
			234,
			26,
			154,
			90,
			218,
			58,
			186,
			122,
			250,
			6,
			134,
			70,
			198,
			38,
			166,
			102,
			230,
			22,
			150,
			86,
			214,
			54,
			182,
			118,
			246,
			14,
			142,
			78,
			206,
			46,
			174,
			110,
			238,
			30,
			158,
			94,
			222,
			62,
			190,
			126,
			254,
			1,
			129,
			65,
			193,
			33,
			161,
			97,
			225,
			17,
			145,
			81,
			209,
			49,
			177,
			113,
			241,
			9,
			137,
			73,
			201,
			41,
			169,
			105,
			233,
			25,
			153,
			89,
			217,
			57,
			185,
			121,
			249,
			5,
			133,
			69,
			197,
			37,
			165,
			101,
			229,
			21,
			149,
			85,
			213,
			53,
			181,
			117,
			245,
			13,
			141,
			77,
			205,
			45,
			173,
			109,
			237,
			29,
			157,
			93,
			221,
			61,
			189,
			125,
			253,
			3,
			131,
			67,
			195,
			35,
			163,
			99,
			227,
			19,
			147,
			83,
			211,
			51,
			179,
			115,
			243,
			11,
			139,
			75,
			203,
			43,
			171,
			107,
			235,
			27,
			155,
			91,
			219,
			59,
			187,
			123,
			251,
			7,
			135,
			71,
			199,
			39,
			167,
			103,
			231,
			23,
			151,
			87,
			215,
			55,
			183,
			119,
			247,
			15,
			143,
			79,
			207,
			47,
			175,
			111,
			239,
			31,
			159,
			95,
			223,
			63,
			191,
			127,
			byte.MaxValue
		};

		// Token: 0x04001F8F RID: 8079
		private static short[] white = new short[]
		{
			6430,
			6400,
			6400,
			6400,
			3225,
			3225,
			3225,
			3225,
			944,
			944,
			944,
			944,
			976,
			976,
			976,
			976,
			1456,
			1456,
			1456,
			1456,
			1488,
			1488,
			1488,
			1488,
			718,
			718,
			718,
			718,
			718,
			718,
			718,
			718,
			750,
			750,
			750,
			750,
			750,
			750,
			750,
			750,
			1520,
			1520,
			1520,
			1520,
			1552,
			1552,
			1552,
			1552,
			428,
			428,
			428,
			428,
			428,
			428,
			428,
			428,
			428,
			428,
			428,
			428,
			428,
			428,
			428,
			428,
			654,
			654,
			654,
			654,
			654,
			654,
			654,
			654,
			1072,
			1072,
			1072,
			1072,
			1104,
			1104,
			1104,
			1104,
			1136,
			1136,
			1136,
			1136,
			1168,
			1168,
			1168,
			1168,
			1200,
			1200,
			1200,
			1200,
			1232,
			1232,
			1232,
			1232,
			622,
			622,
			622,
			622,
			622,
			622,
			622,
			622,
			1008,
			1008,
			1008,
			1008,
			1040,
			1040,
			1040,
			1040,
			44,
			44,
			44,
			44,
			44,
			44,
			44,
			44,
			44,
			44,
			44,
			44,
			44,
			44,
			44,
			44,
			396,
			396,
			396,
			396,
			396,
			396,
			396,
			396,
			396,
			396,
			396,
			396,
			396,
			396,
			396,
			396,
			1712,
			1712,
			1712,
			1712,
			1744,
			1744,
			1744,
			1744,
			846,
			846,
			846,
			846,
			846,
			846,
			846,
			846,
			1264,
			1264,
			1264,
			1264,
			1296,
			1296,
			1296,
			1296,
			1328,
			1328,
			1328,
			1328,
			1360,
			1360,
			1360,
			1360,
			1392,
			1392,
			1392,
			1392,
			1424,
			1424,
			1424,
			1424,
			686,
			686,
			686,
			686,
			686,
			686,
			686,
			686,
			910,
			910,
			910,
			910,
			910,
			910,
			910,
			910,
			1968,
			1968,
			1968,
			1968,
			2000,
			2000,
			2000,
			2000,
			2032,
			2032,
			2032,
			2032,
			16,
			16,
			16,
			16,
			10257,
			10257,
			10257,
			10257,
			12305,
			12305,
			12305,
			12305,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			330,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			362,
			878,
			878,
			878,
			878,
			878,
			878,
			878,
			878,
			1904,
			1904,
			1904,
			1904,
			1936,
			1936,
			1936,
			1936,
			-18413,
			-18413,
			-16365,
			-16365,
			-14317,
			-14317,
			-10221,
			-10221,
			590,
			590,
			590,
			590,
			590,
			590,
			590,
			590,
			782,
			782,
			782,
			782,
			782,
			782,
			782,
			782,
			1584,
			1584,
			1584,
			1584,
			1616,
			1616,
			1616,
			1616,
			1648,
			1648,
			1648,
			1648,
			1680,
			1680,
			1680,
			1680,
			814,
			814,
			814,
			814,
			814,
			814,
			814,
			814,
			1776,
			1776,
			1776,
			1776,
			1808,
			1808,
			1808,
			1808,
			1840,
			1840,
			1840,
			1840,
			1872,
			1872,
			1872,
			1872,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			6157,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			-12275,
			14353,
			14353,
			14353,
			14353,
			16401,
			16401,
			16401,
			16401,
			22547,
			22547,
			24595,
			24595,
			20497,
			20497,
			20497,
			20497,
			18449,
			18449,
			18449,
			18449,
			26643,
			26643,
			28691,
			28691,
			30739,
			30739,
			-32749,
			-32749,
			-30701,
			-30701,
			-28653,
			-28653,
			-26605,
			-26605,
			-24557,
			-24557,
			-22509,
			-22509,
			-20461,
			-20461,
			8207,
			8207,
			8207,
			8207,
			8207,
			8207,
			8207,
			8207,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			72,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			104,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			4107,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			266,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			136,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			168,
			460,
			460,
			460,
			460,
			460,
			460,
			460,
			460,
			460,
			460,
			460,
			460,
			460,
			460,
			460,
			460,
			492,
			492,
			492,
			492,
			492,
			492,
			492,
			492,
			492,
			492,
			492,
			492,
			492,
			492,
			492,
			492,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			2059,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			200,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232,
			232
		};

		// Token: 0x04001F90 RID: 8080
		private static short[] additionalMakeup = new short[]
		{
			28679,
			28679,
			31752,
			-32759,
			-31735,
			-30711,
			-29687,
			-28663,
			29703,
			29703,
			30727,
			30727,
			-27639,
			-26615,
			-25591,
			-24567
		};

		// Token: 0x04001F91 RID: 8081
		private static short[] initBlack = new short[]
		{
			3226,
			6412,
			200,
			168,
			38,
			38,
			134,
			134,
			100,
			100,
			100,
			100,
			68,
			68,
			68,
			68
		};

		// Token: 0x04001F92 RID: 8082
		private static short[] twoBitBlack = new short[]
		{
			292,
			260,
			226,
			226
		};

		// Token: 0x04001F93 RID: 8083
		private static short[] black = new short[]
		{
			62,
			62,
			30,
			30,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			3225,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			1680,
			1680,
			20499,
			22547,
			24595,
			26643,
			1776,
			1776,
			1808,
			1808,
			-24557,
			-22509,
			-20461,
			-18413,
			1904,
			1904,
			1936,
			1936,
			-16365,
			-14317,
			782,
			782,
			782,
			782,
			814,
			814,
			814,
			814,
			-12269,
			-10221,
			10257,
			10257,
			12305,
			12305,
			14353,
			14353,
			16403,
			18451,
			1712,
			1712,
			1744,
			1744,
			28691,
			30739,
			-32749,
			-30701,
			-28653,
			-26605,
			2061,
			2061,
			2061,
			2061,
			2061,
			2061,
			2061,
			2061,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			424,
			750,
			750,
			750,
			750,
			1616,
			1616,
			1648,
			1648,
			1424,
			1424,
			1456,
			1456,
			1488,
			1488,
			1520,
			1520,
			1840,
			1840,
			1872,
			1872,
			1968,
			1968,
			8209,
			8209,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			524,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			556,
			1552,
			1552,
			1584,
			1584,
			2000,
			2000,
			2032,
			2032,
			976,
			976,
			1008,
			1008,
			1040,
			1040,
			1072,
			1072,
			1296,
			1296,
			1328,
			1328,
			718,
			718,
			718,
			718,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			456,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			326,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			490,
			490,
			490,
			490,
			490,
			490,
			490,
			490,
			490,
			490,
			490,
			490,
			490,
			490,
			490,
			490,
			4113,
			4113,
			6161,
			6161,
			848,
			848,
			880,
			880,
			912,
			912,
			944,
			944,
			622,
			622,
			622,
			622,
			654,
			654,
			654,
			654,
			1104,
			1104,
			1136,
			1136,
			1168,
			1168,
			1200,
			1200,
			1232,
			1232,
			1264,
			1264,
			686,
			686,
			686,
			686,
			1360,
			1360,
			1392,
			1392,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			12,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390,
			390
		};

		// Token: 0x04001F94 RID: 8084
		private static byte[] twoDCodes = new byte[]
		{
			80,
			88,
			23,
			71,
			30,
			30,
			62,
			62,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			11,
			35,
			35,
			35,
			35,
			35,
			35,
			35,
			35,
			35,
			35,
			35,
			35,
			35,
			35,
			35,
			35,
			51,
			51,
			51,
			51,
			51,
			51,
			51,
			51,
			51,
			51,
			51,
			51,
			51,
			51,
			51,
			51,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41,
			41
		};
	}
}
