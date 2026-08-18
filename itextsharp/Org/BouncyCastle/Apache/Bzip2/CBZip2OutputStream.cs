using System;
using System.IO;

namespace Org.BouncyCastle.Apache.Bzip2
{
	// Token: 0x0200059C RID: 1436
	public class CBZip2OutputStream : Stream
	{
		// Token: 0x06003134 RID: 12596 RVA: 0x001316CD File Offset: 0x001306CD
		private static void Panic()
		{
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x001316D0 File Offset: 0x001306D0
		private void MakeMaps()
		{
			this.nInUse = 0;
			for (int i = 0; i < 256; i++)
			{
				if (this.inUse[i])
				{
					this.seqToUnseq[this.nInUse] = (char)i;
					this.unseqToSeq[i] = (char)this.nInUse;
					this.nInUse++;
				}
			}
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x0013172C File Offset: 0x0013072C
		protected static void HbMakeCodeLengths(char[] len, int[] freq, int alphaSize, int maxLen)
		{
			int[] array = new int[260];
			int[] array2 = new int[516];
			int[] array3 = new int[516];
			for (int i = 0; i < alphaSize; i++)
			{
				array2[i + 1] = ((freq[i] == 0) ? 1 : freq[i]) << 8;
			}
			for (;;)
			{
				int num = alphaSize;
				int j = 0;
				array[0] = 0;
				array2[0] = 0;
				array3[0] = -2;
				for (int i = 1; i <= alphaSize; i++)
				{
					array3[i] = -1;
					j++;
					array[j] = i;
					int num2 = j;
					int num3 = array[num2];
					while (array2[num3] < array2[array[num2 >> 1]])
					{
						array[num2] = array[num2 >> 1];
						num2 >>= 1;
					}
					array[num2] = num3;
				}
				if (j >= 260)
				{
					CBZip2OutputStream.Panic();
				}
				while (j > 1)
				{
					int num4 = array[1];
					array[1] = array[j];
					j--;
					int num5 = 1;
					int num6 = array[num5];
					for (;;)
					{
						int num7 = num5 << 1;
						if (num7 > j)
						{
							break;
						}
						if (num7 < j && array2[array[num7 + 1]] < array2[array[num7]])
						{
							num7++;
						}
						if (array2[num6] < array2[array[num7]])
						{
							break;
						}
						array[num5] = array[num7];
						num5 = num7;
					}
					array[num5] = num6;
					int num8 = array[1];
					array[1] = array[j];
					j--;
					int num9 = 1;
					int num10 = array[num9];
					for (;;)
					{
						int num11 = num9 << 1;
						if (num11 > j)
						{
							break;
						}
						if (num11 < j && array2[array[num11 + 1]] < array2[array[num11]])
						{
							num11++;
						}
						if (array2[num10] < array2[array[num11]])
						{
							break;
						}
						array[num9] = array[num11];
						num9 = num11;
					}
					array[num9] = num10;
					num++;
					array3[num4] = (array3[num8] = num);
					array2[num] = (int)((uint)(((long)array2[num4] & (long)((ulong)-256)) + ((long)array2[num8] & (long)((ulong)-256))) | (uint)(1 + (((array2[num4] & 255) > (array2[num8] & 255)) ? (array2[num4] & 255) : (array2[num8] & 255))));
					array3[num] = -1;
					j++;
					array[j] = num;
					int num12 = j;
					int num13 = array[num12];
					while (array2[num13] < array2[array[num12 >> 1]])
					{
						array[num12] = array[num12 >> 1];
						num12 >>= 1;
					}
					array[num12] = num13;
				}
				if (num >= 516)
				{
					CBZip2OutputStream.Panic();
				}
				bool flag = false;
				for (int i = 1; i <= alphaSize; i++)
				{
					int num14 = 0;
					int num15 = i;
					while (array3[num15] >= 0)
					{
						num15 = array3[num15];
						num14++;
					}
					len[i - 1] = (char)num14;
					if (num14 > maxLen)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					break;
				}
				for (int i = 1; i < alphaSize; i++)
				{
					int num14 = array2[i] >> 8;
					num14 = 1 + num14 / 2;
					array2[i] = num14 << 8;
				}
			}
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x00131A1C File Offset: 0x00130A1C
		public CBZip2OutputStream(Stream inStream) : this(inStream, 9)
		{
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x00131A60 File Offset: 0x00130A60
		public CBZip2OutputStream(Stream inStream, int inBlockSize)
		{
			this.block = null;
			this.quadrant = null;
			this.zptr = null;
			this.ftab = null;
			inStream.WriteByte(66);
			inStream.WriteByte(90);
			this.BsSetStream(inStream);
			this.workFactor = 50;
			if (inBlockSize > 9)
			{
				inBlockSize = 9;
			}
			if (inBlockSize < 1)
			{
				inBlockSize = 1;
			}
			this.blockSize100k = inBlockSize;
			this.AllocateCompressStructures();
			this.Initialize();
			this.InitBlock();
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x00131B64 File Offset: 0x00130B64
		public override void WriteByte(byte bv)
		{
			int num = (256 + (int)bv) % 256;
			if (this.currentChar != -1)
			{
				if (this.currentChar != num)
				{
					this.WriteRun();
					this.runLength = 1;
					this.currentChar = num;
					return;
				}
				this.runLength++;
				if (this.runLength > 254)
				{
					this.WriteRun();
					this.currentChar = -1;
					this.runLength = 0;
					return;
				}
			}
			else
			{
				this.currentChar = num;
				this.runLength++;
			}
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x00131BEC File Offset: 0x00130BEC
		private void WriteRun()
		{
			if (this.last >= this.allowableBlockSize)
			{
				this.EndBlock();
				this.InitBlock();
				this.WriteRun();
				return;
			}
			this.inUse[this.currentChar] = true;
			for (int i = 0; i < this.runLength; i++)
			{
				this.mCrc.UpdateCRC((int)((ushort)this.currentChar));
			}
			switch (this.runLength)
			{
			case 1:
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				return;
			case 2:
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				return;
			case 3:
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				return;
			default:
				this.inUse[this.runLength - 4] = true;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (char)(this.runLength - 4);
				return;
			}
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x00131E10 File Offset: 0x00130E10
		~CBZip2OutputStream()
		{
			this.Close();
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x00131E3C File Offset: 0x00130E3C
		public override void Close()
		{
			if (this.closed)
			{
				return;
			}
			this.Finish();
			this.closed = true;
			base.Close();
			this.bsStream.Close();
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x00131E65 File Offset: 0x00130E65
		public void Finish()
		{
			if (this.finished)
			{
				return;
			}
			if (this.runLength > 0)
			{
				this.WriteRun();
			}
			this.currentChar = -1;
			this.EndBlock();
			this.EndCompression();
			this.finished = true;
			this.Flush();
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x00131E9F File Offset: 0x00130E9F
		public override void Flush()
		{
			this.bsStream.Flush();
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x00131EAC File Offset: 0x00130EAC
		private void Initialize()
		{
			this.bytesOut = 0;
			this.nBlocksRandomised = 0;
			this.BsPutUChar(104);
			this.BsPutUChar(48 + this.blockSize100k);
			this.combinedCRC = 0;
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x00131EDC File Offset: 0x00130EDC
		private void InitBlock()
		{
			this.mCrc.InitialiseCRC();
			this.last = -1;
			for (int i = 0; i < 256; i++)
			{
				this.inUse[i] = false;
			}
			this.allowableBlockSize = 100000 * this.blockSize100k - 20;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x00131F2C File Offset: 0x00130F2C
		private void EndBlock()
		{
			this.blockCRC = this.mCrc.GetFinalCRC();
			this.combinedCRC = (this.combinedCRC << 1 | (int)((uint)this.combinedCRC >> 31));
			this.combinedCRC ^= this.blockCRC;
			this.DoReversibleTransformation();
			this.BsPutUChar(49);
			this.BsPutUChar(65);
			this.BsPutUChar(89);
			this.BsPutUChar(38);
			this.BsPutUChar(83);
			this.BsPutUChar(89);
			this.BsPutint(this.blockCRC);
			if (this.blockRandomised)
			{
				this.BsW(1, 1);
				this.nBlocksRandomised++;
			}
			else
			{
				this.BsW(1, 0);
			}
			this.MoveToFrontCodeAndSend();
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x00131FE8 File Offset: 0x00130FE8
		private void EndCompression()
		{
			this.BsPutUChar(23);
			this.BsPutUChar(114);
			this.BsPutUChar(69);
			this.BsPutUChar(56);
			this.BsPutUChar(80);
			this.BsPutUChar(144);
			this.BsPutint(this.combinedCRC);
			this.BsFinishedWithStream();
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x0013203C File Offset: 0x0013103C
		private void HbAssignCodes(int[] code, char[] length, int minLen, int maxLen, int alphaSize)
		{
			int num = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if ((int)length[j] == i)
					{
						code[j] = num;
						num++;
					}
				}
				num <<= 1;
			}
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x00132077 File Offset: 0x00131077
		private void BsSetStream(Stream f)
		{
			this.bsStream = f;
			this.bsLive = 0;
			this.bsBuff = 0;
			this.bytesOut = 0;
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x00132098 File Offset: 0x00131098
		private void BsFinishedWithStream()
		{
			while (this.bsLive > 0)
			{
				int num = this.bsBuff >> 24;
				try
				{
					this.bsStream.WriteByte((byte)num);
				}
				catch (IOException ex)
				{
					throw ex;
				}
				this.bsBuff <<= 8;
				this.bsLive -= 8;
				this.bytesOut++;
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x00132108 File Offset: 0x00131108
		private void BsW(int n, int v)
		{
			while (this.bsLive >= 8)
			{
				int num = this.bsBuff >> 24;
				try
				{
					this.bsStream.WriteByte((byte)num);
				}
				catch (IOException ex)
				{
					throw ex;
				}
				this.bsBuff <<= 8;
				this.bsLive -= 8;
				this.bytesOut++;
			}
			this.bsBuff |= v << 32 - this.bsLive - n;
			this.bsLive += n;
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x001321A4 File Offset: 0x001311A4
		private void BsPutUChar(int c)
		{
			this.BsW(8, c);
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x001321B0 File Offset: 0x001311B0
		private void BsPutint(int u)
		{
			this.BsW(8, u >> 24 & 255);
			this.BsW(8, u >> 16 & 255);
			this.BsW(8, u >> 8 & 255);
			this.BsW(8, u & 255);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x001321FD File Offset: 0x001311FD
		private void BsPutIntVS(int numBits, int c)
		{
			this.BsW(numBits, c);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x00132208 File Offset: 0x00131208
		private void SendMTFValues()
		{
			char[][] array = CBZip2InputStream.InitCharArray(6, 258);
			int num = 0;
			int num2 = this.nInUse + 2;
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					array[i][j] = '\u000f';
				}
			}
			if (this.nMTF <= 0)
			{
				CBZip2OutputStream.Panic();
			}
			int num3;
			if (this.nMTF < 200)
			{
				num3 = 2;
			}
			else if (this.nMTF < 600)
			{
				num3 = 3;
			}
			else if (this.nMTF < 1200)
			{
				num3 = 4;
			}
			else if (this.nMTF < 2400)
			{
				num3 = 5;
			}
			else
			{
				num3 = 6;
			}
			int k = num3;
			int num4 = this.nMTF;
			int l = 0;
			while (k > 0)
			{
				int num5 = num4 / k;
				int num6 = l - 1;
				int num7 = 0;
				while (num7 < num5 && num6 < num2 - 1)
				{
					num6++;
					num7 += this.mtfFreq[num6];
				}
				if (num6 > l && k != num3 && k != 1 && (num3 - k) % 2 == 1)
				{
					num7 -= this.mtfFreq[num6];
					num6--;
				}
				for (int j = 0; j < num2; j++)
				{
					if (j >= l && j <= num6)
					{
						array[k - 1][j] = '\0';
					}
					else
					{
						array[k - 1][j] = '\u000f';
					}
				}
				k--;
				l = num6 + 1;
				num4 -= num7;
			}
			int[][] array2 = CBZip2InputStream.InitIntArray(6, 258);
			int[] array3 = new int[6];
			short[] array4 = new short[6];
			for (int m = 0; m < 4; m++)
			{
				for (int i = 0; i < num3; i++)
				{
					array3[i] = 0;
				}
				for (int i = 0; i < num3; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						array2[i][j] = 0;
					}
				}
				num = 0;
				int num8 = 0;
				int num6;
				for (l = 0; l < this.nMTF; l = num6 + 1)
				{
					num6 = l + 50 - 1;
					if (num6 >= this.nMTF)
					{
						num6 = this.nMTF - 1;
					}
					for (int i = 0; i < num3; i++)
					{
						array4[i] = 0;
					}
					if (num3 == 6)
					{
						short num14;
						short num13;
						short num12;
						short num11;
						short num10;
						short num9 = num10 = (num11 = (num12 = (num13 = (num14 = 0))));
						for (int n = l; n <= num6; n++)
						{
							short num15 = this.szptr[n];
							num10 += (short)array[0][(int)num15];
							num9 += (short)array[1][(int)num15];
							num11 += (short)array[2][(int)num15];
							num12 += (short)array[3][(int)num15];
							num13 += (short)array[4][(int)num15];
							num14 += (short)array[5][(int)num15];
						}
						array4[0] = num10;
						array4[1] = num9;
						array4[2] = num11;
						array4[3] = num12;
						array4[4] = num13;
						array4[5] = num14;
					}
					else
					{
						for (int n = l; n <= num6; n++)
						{
							short num16 = this.szptr[n];
							for (int i = 0; i < num3; i++)
							{
								short[] array5 = array4;
								int num17 = i;
								array5[num17] += (short)array[i][(int)num16];
							}
						}
					}
					int num18 = 999999999;
					int num19 = -1;
					for (int i = 0; i < num3; i++)
					{
						if ((int)array4[i] < num18)
						{
							num18 = (int)array4[i];
							num19 = i;
						}
					}
					num8 += num18;
					array3[num19]++;
					this.selector[num] = (char)num19;
					num++;
					for (int n = l; n <= num6; n++)
					{
						array2[num19][(int)this.szptr[n]]++;
					}
				}
				for (int i = 0; i < num3; i++)
				{
					CBZip2OutputStream.HbMakeCodeLengths(array[i], array2[i], num2, 20);
				}
			}
			if (num3 >= 8)
			{
				CBZip2OutputStream.Panic();
			}
			if (num >= 32768 || num > 18002)
			{
				CBZip2OutputStream.Panic();
			}
			char[] array6 = new char[6];
			for (int n = 0; n < num3; n++)
			{
				array6[n] = (char)n;
			}
			for (int n = 0; n < num; n++)
			{
				char c = this.selector[n];
				int num20 = 0;
				char c2 = array6[num20];
				while (c != c2)
				{
					num20++;
					char c3 = c2;
					c2 = array6[num20];
					array6[num20] = c3;
				}
				array6[0] = c2;
				this.selectorMtf[n] = (char)num20;
			}
			int[][] array7 = CBZip2InputStream.InitIntArray(6, 258);
			for (int i = 0; i < num3; i++)
			{
				int num21 = 32;
				int num22 = 0;
				for (int n = 0; n < num2; n++)
				{
					if ((int)array[i][n] > num22)
					{
						num22 = (int)array[i][n];
					}
					if ((int)array[i][n] < num21)
					{
						num21 = (int)array[i][n];
					}
				}
				if (num22 > 20)
				{
					CBZip2OutputStream.Panic();
				}
				if (num21 < 1)
				{
					CBZip2OutputStream.Panic();
				}
				this.HbAssignCodes(array7[i], array[i], num21, num22, num2);
			}
			bool[] array8 = new bool[16];
			for (int n = 0; n < 16; n++)
			{
				array8[n] = false;
				for (int num20 = 0; num20 < 16; num20++)
				{
					if (this.inUse[n * 16 + num20])
					{
						array8[n] = true;
					}
				}
			}
			for (int n = 0; n < 16; n++)
			{
				if (array8[n])
				{
					this.BsW(1, 1);
				}
				else
				{
					this.BsW(1, 0);
				}
			}
			for (int n = 0; n < 16; n++)
			{
				if (array8[n])
				{
					for (int num20 = 0; num20 < 16; num20++)
					{
						if (this.inUse[n * 16 + num20])
						{
							this.BsW(1, 1);
						}
						else
						{
							this.BsW(1, 0);
						}
					}
				}
			}
			this.BsW(3, num3);
			this.BsW(15, num);
			for (int n = 0; n < num; n++)
			{
				for (int num20 = 0; num20 < (int)this.selectorMtf[n]; num20++)
				{
					this.BsW(1, 1);
				}
				this.BsW(1, 0);
			}
			for (int i = 0; i < num3; i++)
			{
				int num23 = (int)array[i][0];
				this.BsW(5, num23);
				for (int n = 0; n < num2; n++)
				{
					while (num23 < (int)array[i][n])
					{
						this.BsW(2, 2);
						num23++;
					}
					while (num23 > (int)array[i][n])
					{
						this.BsW(2, 3);
						num23--;
					}
					this.BsW(1, 0);
				}
			}
			int num24 = 0;
			l = 0;
			while (l < this.nMTF)
			{
				int num6 = l + 50 - 1;
				if (num6 >= this.nMTF)
				{
					num6 = this.nMTF - 1;
				}
				for (int n = l; n <= num6; n++)
				{
					this.BsW((int)array[(int)this.selector[num24]][(int)this.szptr[n]], array7[(int)this.selector[num24]][(int)this.szptr[n]]);
				}
				l = num6 + 1;
				num24++;
			}
			if (num24 != num)
			{
				CBZip2OutputStream.Panic();
			}
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x00132888 File Offset: 0x00131888
		private void MoveToFrontCodeAndSend()
		{
			this.BsPutIntVS(24, this.origPtr);
			this.GenerateMTFValues();
			this.SendMTFValues();
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x001328A4 File Offset: 0x001318A4
		private void SimpleSort(int lo, int hi, int d)
		{
			int num = hi - lo + 1;
			if (num < 2)
			{
				return;
			}
			int i = 0;
			while (this.incs[i] < num)
			{
				i++;
			}
			for (i--; i >= 0; i--)
			{
				int num2 = this.incs[i];
				int j = lo + num2;
				while (j <= hi)
				{
					int num3 = this.zptr[j];
					int num4 = j;
					while (this.FullGtU(this.zptr[num4 - num2] + d, num3 + d))
					{
						this.zptr[num4] = this.zptr[num4 - num2];
						num4 -= num2;
						if (num4 <= lo + num2 - 1)
						{
							break;
						}
					}
					this.zptr[num4] = num3;
					j++;
					if (j > hi)
					{
						break;
					}
					num3 = this.zptr[j];
					num4 = j;
					while (this.FullGtU(this.zptr[num4 - num2] + d, num3 + d))
					{
						this.zptr[num4] = this.zptr[num4 - num2];
						num4 -= num2;
						if (num4 <= lo + num2 - 1)
						{
							break;
						}
					}
					this.zptr[num4] = num3;
					j++;
					if (j > hi)
					{
						break;
					}
					num3 = this.zptr[j];
					num4 = j;
					while (this.FullGtU(this.zptr[num4 - num2] + d, num3 + d))
					{
						this.zptr[num4] = this.zptr[num4 - num2];
						num4 -= num2;
						if (num4 <= lo + num2 - 1)
						{
							break;
						}
					}
					this.zptr[num4] = num3;
					j++;
					if (this.workDone > this.workLimit && this.firstAttempt)
					{
						return;
					}
				}
			}
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x00132A20 File Offset: 0x00131A20
		private void Vswap(int p1, int p2, int n)
		{
			while (n > 0)
			{
				int num = this.zptr[p1];
				this.zptr[p1] = this.zptr[p2];
				this.zptr[p2] = num;
				p1++;
				p2++;
				n--;
			}
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x00132A68 File Offset: 0x00131A68
		private char Med3(char a, char b, char c)
		{
			if (a > b)
			{
				char c2 = a;
				a = b;
				b = c2;
			}
			if (b > c)
			{
				char c2 = b;
				b = c;
				c = c2;
			}
			if (a > b)
			{
				b = a;
			}
			return b;
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x00132A98 File Offset: 0x00131A98
		private void QSort3(int loSt, int hiSt, int dSt)
		{
			CBZip2OutputStream.StackElem[] array = new CBZip2OutputStream.StackElem[1000];
			for (int i = 0; i < 1000; i++)
			{
				array[i] = new CBZip2OutputStream.StackElem();
			}
			int j = 0;
			array[j].ll = loSt;
			array[j].hh = hiSt;
			array[j].dd = dSt;
			j++;
			while (j > 0)
			{
				if (j >= 1000)
				{
					CBZip2OutputStream.Panic();
				}
				j--;
				int ll = array[j].ll;
				int hh = array[j].hh;
				int dd = array[j].dd;
				if (hh - ll < 20 || dd > 10)
				{
					this.SimpleSort(ll, hh, dd);
					if (this.workDone > this.workLimit && this.firstAttempt)
					{
						return;
					}
				}
				else
				{
					int num = (int)this.Med3(this.block[this.zptr[ll] + dd + 1], this.block[this.zptr[hh] + dd + 1], this.block[this.zptr[ll + hh >> 1] + dd + 1]);
					int k;
					int num2 = k = ll;
					int num4;
					int num3 = num4 = hh;
					for (;;)
					{
						if (k <= num4)
						{
							int num5 = (int)this.block[this.zptr[k] + dd + 1] - num;
							if (num5 == 0)
							{
								int num6 = this.zptr[k];
								this.zptr[k] = this.zptr[num2];
								this.zptr[num2] = num6;
								num2++;
								k++;
								continue;
							}
							if (num5 <= 0)
							{
								k++;
								continue;
							}
						}
						while (k <= num4)
						{
							int num5 = (int)this.block[this.zptr[num4] + dd + 1] - num;
							if (num5 == 0)
							{
								int num7 = this.zptr[num4];
								this.zptr[num4] = this.zptr[num3];
								this.zptr[num3] = num7;
								num3--;
								num4--;
							}
							else
							{
								if (num5 < 0)
								{
									break;
								}
								num4--;
							}
						}
						if (k > num4)
						{
							break;
						}
						int num8 = this.zptr[k];
						this.zptr[k] = this.zptr[num4];
						this.zptr[num4] = num8;
						k++;
						num4--;
					}
					if (num3 < num2)
					{
						array[j].ll = ll;
						array[j].hh = hh;
						array[j].dd = dd + 1;
						j++;
					}
					else
					{
						int num5 = (num2 - ll < k - num2) ? (num2 - ll) : (k - num2);
						this.Vswap(ll, k - num5, num5);
						int num9 = (hh - num3 < num3 - num4) ? (hh - num3) : (num3 - num4);
						this.Vswap(k, hh - num9 + 1, num9);
						num5 = ll + k - num2 - 1;
						num9 = hh - (num3 - num4) + 1;
						array[j].ll = ll;
						array[j].hh = num5;
						array[j].dd = dd;
						j++;
						array[j].ll = num5 + 1;
						array[j].hh = num9 - 1;
						array[j].dd = dd + 1;
						j++;
						array[j].ll = num9;
						array[j].hh = hh;
						array[j].dd = dd;
						j++;
					}
				}
			}
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x00132DD0 File Offset: 0x00131DD0
		private void MainSort()
		{
			int[] array = new int[256];
			int[] array2 = new int[256];
			bool[] array3 = new bool[256];
			for (int i = 0; i < 20; i++)
			{
				this.block[this.last + i + 2] = this.block[i % (this.last + 1) + 1];
			}
			for (int i = 0; i <= this.last + 20; i++)
			{
				this.quadrant[i] = 0;
			}
			this.block[0] = this.block[this.last + 1];
			if (this.last < 4000)
			{
				for (int i = 0; i <= this.last; i++)
				{
					this.zptr[i] = i;
				}
				this.firstAttempt = false;
				this.workDone = (this.workLimit = 0);
				this.SimpleSort(0, this.last, 0);
				return;
			}
			int num = 0;
			for (int i = 0; i <= 255; i++)
			{
				array3[i] = false;
			}
			for (int i = 0; i <= 65536; i++)
			{
				this.ftab[i] = 0;
			}
			int num2 = (int)this.block[0];
			for (int i = 0; i <= this.last; i++)
			{
				int num3 = (int)this.block[i + 1];
				this.ftab[(num2 << 8) + num3]++;
				num2 = num3;
			}
			for (int i = 1; i <= 65536; i++)
			{
				this.ftab[i] += this.ftab[i - 1];
			}
			num2 = (int)this.block[1];
			int j;
			for (int i = 0; i < this.last; i++)
			{
				int num3 = (int)this.block[i + 2];
				j = (num2 << 8) + num3;
				num2 = num3;
				this.ftab[j]--;
				this.zptr[this.ftab[j]] = i;
			}
			j = (int)(((int)this.block[this.last + 1] << 8) + this.block[1]);
			this.ftab[j]--;
			this.zptr[this.ftab[j]] = this.last;
			for (int i = 0; i <= 255; i++)
			{
				array[i] = i;
			}
			int num4 = 1;
			do
			{
				num4 = 3 * num4 + 1;
			}
			while (num4 <= 256);
			do
			{
				num4 /= 3;
				for (int i = num4; i <= 255; i++)
				{
					int num5 = array[i];
					j = i;
					while (this.ftab[array[j - num4] + 1 << 8] - this.ftab[array[j - num4] << 8] > this.ftab[num5 + 1 << 8] - this.ftab[num5 << 8])
					{
						array[j] = array[j - num4];
						j -= num4;
						if (j <= num4 - 1)
						{
							break;
						}
					}
					array[j] = num5;
				}
			}
			while (num4 != 1);
			for (int i = 0; i <= 255; i++)
			{
				int num6 = array[i];
				for (j = 0; j <= 255; j++)
				{
					int num7 = (num6 << 8) + j;
					if ((this.ftab[num7] & 2097152) != 2097152)
					{
						int num8 = this.ftab[num7] & -2097153;
						int num9 = (this.ftab[num7 + 1] & -2097153) - 1;
						if (num9 > num8)
						{
							this.QSort3(num8, num9, 2);
							num += num9 - num8 + 1;
							if (this.workDone > this.workLimit && this.firstAttempt)
							{
								return;
							}
						}
						this.ftab[num7] |= 2097152;
					}
				}
				array3[num6] = true;
				if (i < 255)
				{
					int num10 = this.ftab[num6 << 8] & -2097153;
					int num11 = (this.ftab[num6 + 1 << 8] & -2097153) - num10;
					int num12 = 0;
					while (num11 >> num12 > 65534)
					{
						num12++;
					}
					for (j = 0; j < num11; j++)
					{
						int num13 = this.zptr[num10 + j];
						int num14 = j >> num12;
						this.quadrant[num13] = num14;
						if (num13 < 20)
						{
							this.quadrant[num13 + this.last + 1] = num14;
						}
					}
					if (num11 - 1 >> num12 > 65535)
					{
						CBZip2OutputStream.Panic();
					}
				}
				for (j = 0; j <= 255; j++)
				{
					array2[j] = (this.ftab[(j << 8) + num6] & -2097153);
				}
				for (j = (this.ftab[num6 << 8] & -2097153); j < (this.ftab[num6 + 1 << 8] & -2097153); j++)
				{
					num2 = (int)this.block[this.zptr[j]];
					if (!array3[num2])
					{
						this.zptr[array2[num2]] = ((this.zptr[j] == 0) ? this.last : (this.zptr[j] - 1));
						array2[num2]++;
					}
				}
				for (j = 0; j <= 255; j++)
				{
					this.ftab[(j << 8) + num6] |= 2097152;
				}
			}
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x00133300 File Offset: 0x00132300
		private void RandomiseBlock()
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 256; i++)
			{
				this.inUse[i] = false;
			}
			for (int i = 0; i <= this.last; i++)
			{
				if (num == 0)
				{
					num = (int)((ushort)BZip2Constants.rNums[num2]);
					num2++;
					if (num2 == 512)
					{
						num2 = 0;
					}
				}
				num--;
				char[] array = this.block;
				int num3 = i + 1;
				array[num3] ^= ((num == 1) ? '\u0001' : '\0');
				char[] array2 = this.block;
				int num4 = i + 1;
				array2[num4] &= 'ÿ';
				this.inUse[(int)this.block[i + 1]] = true;
			}
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x001333B4 File Offset: 0x001323B4
		private void DoReversibleTransformation()
		{
			this.workLimit = this.workFactor * this.last;
			this.workDone = 0;
			this.blockRandomised = false;
			this.firstAttempt = true;
			this.MainSort();
			if (this.workDone > this.workLimit && this.firstAttempt)
			{
				this.RandomiseBlock();
				this.workLimit = (this.workDone = 0);
				this.blockRandomised = true;
				this.firstAttempt = false;
				this.MainSort();
			}
			this.origPtr = -1;
			for (int i = 0; i <= this.last; i++)
			{
				if (this.zptr[i] == 0)
				{
					this.origPtr = i;
					break;
				}
			}
			if (this.origPtr == -1)
			{
				CBZip2OutputStream.Panic();
			}
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x00133468 File Offset: 0x00132468
		private bool FullGtU(int i1, int i2)
		{
			char c = this.block[i1 + 1];
			char c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			c = this.block[i1 + 1];
			c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			c = this.block[i1 + 1];
			c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			c = this.block[i1 + 1];
			c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			c = this.block[i1 + 1];
			c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			c = this.block[i1 + 1];
			c2 = this.block[i2 + 1];
			if (c != c2)
			{
				return c > c2;
			}
			i1++;
			i2++;
			int num = this.last + 1;
			int num2;
			int num3;
			for (;;)
			{
				c = this.block[i1 + 1];
				c2 = this.block[i2 + 1];
				if (c != c2)
				{
					break;
				}
				num2 = this.quadrant[i1];
				num3 = this.quadrant[i2];
				if (num2 != num3)
				{
					goto Block_8;
				}
				i1++;
				i2++;
				c = this.block[i1 + 1];
				c2 = this.block[i2 + 1];
				if (c != c2)
				{
					goto Block_9;
				}
				num2 = this.quadrant[i1];
				num3 = this.quadrant[i2];
				if (num2 != num3)
				{
					goto Block_10;
				}
				i1++;
				i2++;
				c = this.block[i1 + 1];
				c2 = this.block[i2 + 1];
				if (c != c2)
				{
					goto Block_11;
				}
				num2 = this.quadrant[i1];
				num3 = this.quadrant[i2];
				if (num2 != num3)
				{
					goto Block_12;
				}
				i1++;
				i2++;
				c = this.block[i1 + 1];
				c2 = this.block[i2 + 1];
				if (c != c2)
				{
					goto Block_13;
				}
				num2 = this.quadrant[i1];
				num3 = this.quadrant[i2];
				if (num2 != num3)
				{
					goto Block_14;
				}
				i1++;
				i2++;
				if (i1 > this.last)
				{
					i1 -= this.last;
					i1--;
				}
				if (i2 > this.last)
				{
					i2 -= this.last;
					i2--;
				}
				num -= 4;
				this.workDone++;
				if (num < 0)
				{
					return false;
				}
			}
			return c > c2;
			Block_8:
			return num2 > num3;
			Block_9:
			return c > c2;
			Block_10:
			return num2 > num3;
			Block_11:
			return c > c2;
			Block_12:
			return num2 > num3;
			Block_13:
			return c > c2;
			Block_14:
			return num2 > num3;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x001336DC File Offset: 0x001326DC
		private void AllocateCompressStructures()
		{
			int num = 100000 * this.blockSize100k;
			this.block = new char[num + 1 + 20];
			this.quadrant = new int[num + 20];
			this.zptr = new int[num];
			this.ftab = new int[65537];
			if (this.block != null && this.quadrant != null && this.zptr != null)
			{
				int[] array = this.ftab;
			}
			this.szptr = new short[2 * num];
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x00133760 File Offset: 0x00132760
		private void GenerateMTFValues()
		{
			char[] array = new char[256];
			this.MakeMaps();
			int num = this.nInUse + 1;
			for (int i = 0; i <= num; i++)
			{
				this.mtfFreq[i] = 0;
			}
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < this.nInUse; i++)
			{
				array[i] = (char)i;
			}
			for (int i = 0; i <= this.last; i++)
			{
				char c = this.unseqToSeq[(int)this.block[this.zptr[i]]];
				int num4 = 0;
				char c2 = array[num4];
				while (c != c2)
				{
					num4++;
					char c3 = c2;
					c2 = array[num4];
					array[num4] = c3;
				}
				array[0] = c2;
				if (num4 == 0)
				{
					num3++;
				}
				else
				{
					if (num3 > 0)
					{
						num3--;
						for (;;)
						{
							switch (num3 % 2)
							{
							case 0:
								this.szptr[num2] = 0;
								num2++;
								this.mtfFreq[0]++;
								break;
							case 1:
								this.szptr[num2] = 1;
								num2++;
								this.mtfFreq[1]++;
								break;
							}
							if (num3 < 2)
							{
								break;
							}
							num3 = (num3 - 2) / 2;
						}
						num3 = 0;
					}
					this.szptr[num2] = (short)(num4 + 1);
					num2++;
					this.mtfFreq[num4 + 1]++;
				}
			}
			if (num3 > 0)
			{
				num3--;
				for (;;)
				{
					switch (num3 % 2)
					{
					case 0:
						this.szptr[num2] = 0;
						num2++;
						this.mtfFreq[0]++;
						break;
					case 1:
						this.szptr[num2] = 1;
						num2++;
						this.mtfFreq[1]++;
						break;
					}
					if (num3 < 2)
					{
						break;
					}
					num3 = (num3 - 2) / 2;
				}
			}
			this.szptr[num2] = (short)num;
			num2++;
			this.mtfFreq[num]++;
			this.nMTF = num2;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x0013398D File Offset: 0x0013298D
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x00133990 File Offset: 0x00132990
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x00133994 File Offset: 0x00132994
		public override void SetLength(long value)
		{
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x00133998 File Offset: 0x00132998
		public override void Write(byte[] buffer, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.WriteByte(buffer[i + offset]);
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x001339BC File Offset: 0x001329BC
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x001339BF File Offset: 0x001329BF
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x001339C2 File Offset: 0x001329C2
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x001339C5 File Offset: 0x001329C5
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x001339C9 File Offset: 0x001329C9
		// (set) Token: 0x0600315F RID: 12639 RVA: 0x001339CD File Offset: 0x001329CD
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x040021EF RID: 8687
		protected const int SETMASK = 2097152;

		// Token: 0x040021F0 RID: 8688
		protected const int CLEARMASK = -2097153;

		// Token: 0x040021F1 RID: 8689
		protected const int GREATER_ICOST = 15;

		// Token: 0x040021F2 RID: 8690
		protected const int LESSER_ICOST = 0;

		// Token: 0x040021F3 RID: 8691
		protected const int SMALL_THRESH = 20;

		// Token: 0x040021F4 RID: 8692
		protected const int DEPTH_THRESH = 10;

		// Token: 0x040021F5 RID: 8693
		protected const int QSORT_STACK_SIZE = 1000;

		// Token: 0x040021F6 RID: 8694
		private bool finished;

		// Token: 0x040021F7 RID: 8695
		private int last;

		// Token: 0x040021F8 RID: 8696
		private int origPtr;

		// Token: 0x040021F9 RID: 8697
		private int blockSize100k;

		// Token: 0x040021FA RID: 8698
		private bool blockRandomised;

		// Token: 0x040021FB RID: 8699
		private int bytesOut;

		// Token: 0x040021FC RID: 8700
		private int bsBuff;

		// Token: 0x040021FD RID: 8701
		private int bsLive;

		// Token: 0x040021FE RID: 8702
		private CRC mCrc = new CRC();

		// Token: 0x040021FF RID: 8703
		private bool[] inUse = new bool[256];

		// Token: 0x04002200 RID: 8704
		private int nInUse;

		// Token: 0x04002201 RID: 8705
		private char[] seqToUnseq = new char[256];

		// Token: 0x04002202 RID: 8706
		private char[] unseqToSeq = new char[256];

		// Token: 0x04002203 RID: 8707
		private char[] selector = new char[18002];

		// Token: 0x04002204 RID: 8708
		private char[] selectorMtf = new char[18002];

		// Token: 0x04002205 RID: 8709
		private char[] block;

		// Token: 0x04002206 RID: 8710
		private int[] quadrant;

		// Token: 0x04002207 RID: 8711
		private int[] zptr;

		// Token: 0x04002208 RID: 8712
		private short[] szptr;

		// Token: 0x04002209 RID: 8713
		private int[] ftab;

		// Token: 0x0400220A RID: 8714
		private int nMTF;

		// Token: 0x0400220B RID: 8715
		private int[] mtfFreq = new int[258];

		// Token: 0x0400220C RID: 8716
		private int workFactor;

		// Token: 0x0400220D RID: 8717
		private int workDone;

		// Token: 0x0400220E RID: 8718
		private int workLimit;

		// Token: 0x0400220F RID: 8719
		private bool firstAttempt;

		// Token: 0x04002210 RID: 8720
		private int nBlocksRandomised;

		// Token: 0x04002211 RID: 8721
		private int currentChar = -1;

		// Token: 0x04002212 RID: 8722
		private int runLength;

		// Token: 0x04002213 RID: 8723
		private bool closed;

		// Token: 0x04002214 RID: 8724
		private int blockCRC;

		// Token: 0x04002215 RID: 8725
		private int combinedCRC;

		// Token: 0x04002216 RID: 8726
		private int allowableBlockSize;

		// Token: 0x04002217 RID: 8727
		private Stream bsStream;

		// Token: 0x04002218 RID: 8728
		private int[] incs = new int[]
		{
			1,
			4,
			13,
			40,
			121,
			364,
			1093,
			3280,
			9841,
			29524,
			88573,
			265720,
			797161,
			2391484
		};

		// Token: 0x0200059D RID: 1437
		internal class StackElem
		{
			// Token: 0x04002219 RID: 8729
			internal int ll;

			// Token: 0x0400221A RID: 8730
			internal int hh;

			// Token: 0x0400221B RID: 8731
			internal int dd;
		}
	}
}
