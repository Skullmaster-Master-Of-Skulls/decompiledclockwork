using System;
using System.IO;

namespace Org.BouncyCastle.Apache.Bzip2
{
	// Token: 0x020001D6 RID: 470
	public class CBZip2InputStream : Stream
	{
		// Token: 0x06001255 RID: 4693 RVA: 0x00069593 File Offset: 0x00068593
		private static void Cadvise()
		{
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00069595 File Offset: 0x00068595
		private static void BadBGLengths()
		{
			CBZip2InputStream.Cadvise();
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0006959C File Offset: 0x0006859C
		private static void BitStreamEOF()
		{
			CBZip2InputStream.Cadvise();
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x000695A3 File Offset: 0x000685A3
		private static void CompressedStreamEOF()
		{
			CBZip2InputStream.Cadvise();
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000695AC File Offset: 0x000685AC
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

		// Token: 0x0600125A RID: 4698 RVA: 0x00069608 File Offset: 0x00068608
		public CBZip2InputStream(Stream zStream)
		{
			this.ll8 = null;
			this.tt = null;
			this.BsSetStream(zStream);
			this.Initialize();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000696FC File Offset: 0x000686FC
		internal static int[][] InitIntArray(int n1, int n2)
		{
			int[][] array = new int[n1][];
			for (int i = 0; i < n1; i++)
			{
				array[i] = new int[n2];
			}
			return array;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00069728 File Offset: 0x00068728
		internal static char[][] InitCharArray(int n1, int n2)
		{
			char[][] array = new char[n1][];
			for (int i = 0; i < n1; i++)
			{
				array[i] = new char[n2];
			}
			return array;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00069754 File Offset: 0x00068754
		public override int ReadByte()
		{
			if (this.streamEnd)
			{
				return -1;
			}
			int result = this.currentChar;
			switch (this.currentState)
			{
			case 3:
				this.SetupRandPartB();
				break;
			case 4:
				this.SetupRandPartC();
				break;
			case 6:
				this.SetupNoRandPartB();
				break;
			case 7:
				this.SetupNoRandPartC();
				break;
			}
			return result;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x000697C0 File Offset: 0x000687C0
		private void Initialize()
		{
			char c = this.BsGetUChar();
			char c2 = this.BsGetUChar();
			if (c != 'B' && c2 != 'Z')
			{
				throw new IOException("Not a BZIP2 marked stream");
			}
			c = this.BsGetUChar();
			c2 = this.BsGetUChar();
			if (c != 'h' || c2 < '1' || c2 > '9')
			{
				this.BsFinishedWithStream();
				this.streamEnd = true;
				return;
			}
			this.SetDecompressStructureSizes((int)(c2 - '0'));
			this.computedCombinedCRC = 0;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0006982C File Offset: 0x0006882C
		private void InitBlock()
		{
			char c = this.BsGetUChar();
			char c2 = this.BsGetUChar();
			char c3 = this.BsGetUChar();
			char c4 = this.BsGetUChar();
			char c5 = this.BsGetUChar();
			char c6 = this.BsGetUChar();
			if (c == '\u0017' && c2 == 'r' && c3 == 'E' && c4 == '8' && c5 == 'P' && c6 == '\u0090')
			{
				this.Complete();
				return;
			}
			if (c != '1' || c2 != 'A' || c3 != 'Y' || c4 != '&' || c5 != 'S' || c6 != 'Y')
			{
				CBZip2InputStream.BadBlockHeader();
				this.streamEnd = true;
				return;
			}
			this.storedBlockCRC = this.BsGetInt32();
			if (this.BsR(1) == 1)
			{
				this.blockRandomised = true;
			}
			else
			{
				this.blockRandomised = false;
			}
			this.GetAndMoveToFrontDecode();
			this.mCrc.InitialiseCRC();
			this.currentState = 1;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000698FC File Offset: 0x000688FC
		private void EndBlock()
		{
			this.computedBlockCRC = this.mCrc.GetFinalCRC();
			if (this.storedBlockCRC != this.computedBlockCRC)
			{
				CBZip2InputStream.CrcError();
			}
			this.computedCombinedCRC = (this.computedCombinedCRC << 1 | (int)((uint)this.computedCombinedCRC >> 31));
			this.computedCombinedCRC ^= this.computedBlockCRC;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00069958 File Offset: 0x00068958
		private void Complete()
		{
			this.storedCombinedCRC = this.BsGetInt32();
			if (this.storedCombinedCRC != this.computedCombinedCRC)
			{
				CBZip2InputStream.CrcError();
			}
			this.BsFinishedWithStream();
			this.streamEnd = true;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00069986 File Offset: 0x00068986
		private static void BlockOverrun()
		{
			CBZip2InputStream.Cadvise();
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0006998D File Offset: 0x0006898D
		private static void BadBlockHeader()
		{
			CBZip2InputStream.Cadvise();
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00069994 File Offset: 0x00068994
		private static void CrcError()
		{
			CBZip2InputStream.Cadvise();
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0006999C File Offset: 0x0006899C
		private void BsFinishedWithStream()
		{
			try
			{
				if (this.bsStream != null)
				{
					this.bsStream.Close();
					this.bsStream = null;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000699D8 File Offset: 0x000689D8
		private void BsSetStream(Stream f)
		{
			this.bsStream = f;
			this.bsLive = 0;
			this.bsBuff = 0;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000699F0 File Offset: 0x000689F0
		private int BsR(int n)
		{
			while (this.bsLive < n)
			{
				char c = '\0';
				try
				{
					c = (char)this.bsStream.ReadByte();
				}
				catch (IOException)
				{
					CBZip2InputStream.CompressedStreamEOF();
				}
				if (c == '￿')
				{
					CBZip2InputStream.CompressedStreamEOF();
				}
				int num = (int)c;
				this.bsBuff = (this.bsBuff << 8 | (num & 255));
				this.bsLive += 8;
			}
			int result = this.bsBuff >> this.bsLive - n & (1 << n) - 1;
			this.bsLive -= n;
			return result;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00069A90 File Offset: 0x00068A90
		private char BsGetUChar()
		{
			return (char)this.BsR(8);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00069A9C File Offset: 0x00068A9C
		private int BsGetint()
		{
			int num = 0;
			num = (num << 8 | this.BsR(8));
			num = (num << 8 | this.BsR(8));
			num = (num << 8 | this.BsR(8));
			return num << 8 | this.BsR(8);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00069ADC File Offset: 0x00068ADC
		private int BsGetIntVS(int numBits)
		{
			return this.BsR(numBits);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00069AE5 File Offset: 0x00068AE5
		private int BsGetInt32()
		{
			return this.BsGetint();
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00069AF0 File Offset: 0x00068AF0
		private void HbCreateDecodeTables(int[] limit, int[] basev, int[] perm, char[] length, int minLen, int maxLen, int alphaSize)
		{
			int num = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if ((int)length[j] == i)
					{
						perm[num] = j;
						num++;
					}
				}
			}
			for (int i = 0; i < 23; i++)
			{
				basev[i] = 0;
			}
			for (int i = 0; i < alphaSize; i++)
			{
				basev[(int)(length[i] + '\u0001')]++;
			}
			for (int i = 1; i < 23; i++)
			{
				basev[i] += basev[i - 1];
			}
			for (int i = 0; i < 23; i++)
			{
				limit[i] = 0;
			}
			int num2 = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				num2 += basev[i + 1] - basev[i];
				limit[i] = num2 - 1;
				num2 <<= 1;
			}
			for (int i = minLen + 1; i <= maxLen; i++)
			{
				basev[i] = (limit[i - 1] + 1 << 1) - basev[i];
			}
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00069BDC File Offset: 0x00068BDC
		private void RecvDecodingTables()
		{
			char[][] array = CBZip2InputStream.InitCharArray(6, 258);
			bool[] array2 = new bool[16];
			for (int i = 0; i < 16; i++)
			{
				if (this.BsR(1) == 1)
				{
					array2[i] = true;
				}
				else
				{
					array2[i] = false;
				}
			}
			for (int i = 0; i < 256; i++)
			{
				this.inUse[i] = false;
			}
			for (int i = 0; i < 16; i++)
			{
				if (array2[i])
				{
					for (int j = 0; j < 16; j++)
					{
						if (this.BsR(1) == 1)
						{
							this.inUse[i * 16 + j] = true;
						}
					}
				}
			}
			this.MakeMaps();
			int num = this.nInUse + 2;
			int num2 = this.BsR(3);
			int num3 = this.BsR(15);
			for (int i = 0; i < num3; i++)
			{
				int j = 0;
				while (this.BsR(1) == 1)
				{
					j++;
				}
				this.selectorMtf[i] = (char)j;
			}
			char[] array3 = new char[6];
			char c = '\0';
			while ((int)c < num2)
			{
				array3[(int)c] = c;
				c += '\u0001';
			}
			for (int i = 0; i < num3; i++)
			{
				c = this.selectorMtf[i];
				char c2 = array3[(int)c];
				while (c > '\0')
				{
					array3[(int)c] = array3[(int)(c - '\u0001')];
					c -= '\u0001';
				}
				array3[0] = c2;
				this.selector[i] = c2;
			}
			for (int k = 0; k < num2; k++)
			{
				int num4 = this.BsR(5);
				for (int i = 0; i < num; i++)
				{
					while (this.BsR(1) == 1)
					{
						if (this.BsR(1) == 0)
						{
							num4++;
						}
						else
						{
							num4--;
						}
					}
					array[k][i] = (char)num4;
				}
			}
			for (int k = 0; k < num2; k++)
			{
				int num5 = 32;
				int num6 = 0;
				for (int i = 0; i < num; i++)
				{
					if ((int)array[k][i] > num6)
					{
						num6 = (int)array[k][i];
					}
					if ((int)array[k][i] < num5)
					{
						num5 = (int)array[k][i];
					}
				}
				this.HbCreateDecodeTables(this.limit[k], this.basev[k], this.perm[k], array[k], num5, num6, num);
				this.minLens[k] = num5;
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00069DE8 File Offset: 0x00068DE8
		private void GetAndMoveToFrontDecode()
		{
			char[] array = new char[256];
			int num = 100000 * this.blockSize100k;
			this.origPtr = this.BsGetIntVS(24);
			this.RecvDecodingTables();
			int num2 = this.nInUse + 1;
			int num3 = -1;
			int num4 = 0;
			for (int i = 0; i <= 255; i++)
			{
				this.unzftab[i] = 0;
			}
			for (int i = 0; i <= 255; i++)
			{
				array[i] = (char)i;
			}
			this.last = -1;
			if (num4 == 0)
			{
				num3++;
				num4 = 50;
			}
			num4--;
			int num5 = (int)this.selector[num3];
			int num6 = this.minLens[num5];
			int j;
			int num8;
			for (j = this.BsR(num6); j > this.limit[num5][num6]; j = (j << 1 | num8))
			{
				num6++;
				while (this.bsLive < 1)
				{
					char c = '\0';
					try
					{
						c = (char)this.bsStream.ReadByte();
					}
					catch (IOException)
					{
						CBZip2InputStream.CompressedStreamEOF();
					}
					if (c == '￿')
					{
						CBZip2InputStream.CompressedStreamEOF();
					}
					int num7 = (int)c;
					this.bsBuff = (this.bsBuff << 8 | (num7 & 255));
					this.bsLive += 8;
				}
				num8 = (this.bsBuff >> this.bsLive - 1 & 1);
				this.bsLive--;
			}
			int num9 = this.perm[num5][j - this.basev[num5][num6]];
			while (num9 != num2)
			{
				if (num9 == 0 || num9 == 1)
				{
					int k = -1;
					int num10 = 1;
					do
					{
						if (num9 == 0)
						{
							k += num10;
						}
						else if (num9 == 1)
						{
							k += 2 * num10;
						}
						num10 *= 2;
						if (num4 == 0)
						{
							num3++;
							num4 = 50;
						}
						num4--;
						int num11 = (int)this.selector[num3];
						int num12 = this.minLens[num11];
						int l;
						int num14;
						for (l = this.BsR(num12); l > this.limit[num11][num12]; l = (l << 1 | num14))
						{
							num12++;
							while (this.bsLive < 1)
							{
								char c2 = '\0';
								try
								{
									c2 = (char)this.bsStream.ReadByte();
								}
								catch (IOException)
								{
									CBZip2InputStream.CompressedStreamEOF();
								}
								if (c2 == '￿')
								{
									CBZip2InputStream.CompressedStreamEOF();
								}
								int num13 = (int)c2;
								this.bsBuff = (this.bsBuff << 8 | (num13 & 255));
								this.bsLive += 8;
							}
							num14 = (this.bsBuff >> this.bsLive - 1 & 1);
							this.bsLive--;
						}
						num9 = this.perm[num11][l - this.basev[num11][num12]];
					}
					while (num9 == 0 || num9 == 1);
					k++;
					char c3 = this.seqToUnseq[(int)array[0]];
					this.unzftab[(int)c3] += k;
					while (k > 0)
					{
						this.last++;
						this.ll8[this.last] = c3;
						k--;
					}
					if (this.last >= num)
					{
						CBZip2InputStream.BlockOverrun();
					}
				}
				else
				{
					this.last++;
					if (this.last >= num)
					{
						CBZip2InputStream.BlockOverrun();
					}
					char c4 = array[num9 - 1];
					this.unzftab[(int)this.seqToUnseq[(int)c4]]++;
					this.ll8[this.last] = this.seqToUnseq[(int)c4];
					int m;
					for (m = num9 - 1; m > 3; m -= 4)
					{
						array[m] = array[m - 1];
						array[m - 1] = array[m - 2];
						array[m - 2] = array[m - 3];
						array[m - 3] = array[m - 4];
					}
					while (m > 0)
					{
						array[m] = array[m - 1];
						m--;
					}
					array[0] = c4;
					if (num4 == 0)
					{
						num3++;
						num4 = 50;
					}
					num4--;
					int num15 = (int)this.selector[num3];
					int num16 = this.minLens[num15];
					int n;
					int num18;
					for (n = this.BsR(num16); n > this.limit[num15][num16]; n = (n << 1 | num18))
					{
						num16++;
						while (this.bsLive < 1)
						{
							char c5 = '\0';
							try
							{
								c5 = (char)this.bsStream.ReadByte();
							}
							catch (IOException)
							{
								CBZip2InputStream.CompressedStreamEOF();
							}
							int num17 = (int)c5;
							this.bsBuff = (this.bsBuff << 8 | (num17 & 255));
							this.bsLive += 8;
						}
						num18 = (this.bsBuff >> this.bsLive - 1 & 1);
						this.bsLive--;
					}
					num9 = this.perm[num15][n - this.basev[num15][num16]];
				}
			}
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0006A2C0 File Offset: 0x000692C0
		private void SetupBlock()
		{
			int[] array = new int[257];
			array[0] = 0;
			this.i = 1;
			while (this.i <= 256)
			{
				array[this.i] = this.unzftab[this.i - 1];
				this.i++;
			}
			this.i = 1;
			while (this.i <= 256)
			{
				array[this.i] += array[this.i - 1];
				this.i++;
			}
			this.i = 0;
			while (this.i <= this.last)
			{
				char c = this.ll8[this.i];
				this.tt[array[(int)c]] = this.i;
				array[(int)c]++;
				this.i++;
			}
			this.tPos = this.tt[this.origPtr];
			this.count = 0;
			this.i2 = 0;
			this.ch2 = 256;
			if (this.blockRandomised)
			{
				this.rNToGo = 0;
				this.rTPos = 0;
				this.SetupRandPartA();
				return;
			}
			this.SetupNoRandPartA();
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0006A408 File Offset: 0x00069408
		private void SetupRandPartA()
		{
			if (this.i2 <= this.last)
			{
				this.chPrev = this.ch2;
				this.ch2 = (int)this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				if (this.rNToGo == 0)
				{
					this.rNToGo = BZip2Constants.rNums[this.rTPos];
					this.rTPos++;
					if (this.rTPos == 512)
					{
						this.rTPos = 0;
					}
				}
				this.rNToGo--;
				this.ch2 ^= ((this.rNToGo == 1) ? 1 : 0);
				this.i2++;
				this.currentChar = this.ch2;
				this.currentState = 3;
				this.mCrc.UpdateCRC(this.ch2);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0006A504 File Offset: 0x00069504
		private void SetupNoRandPartA()
		{
			if (this.i2 <= this.last)
			{
				this.chPrev = this.ch2;
				this.ch2 = (int)this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				this.i2++;
				this.currentChar = this.ch2;
				this.currentState = 6;
				this.mCrc.UpdateCRC(this.ch2);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0006A598 File Offset: 0x00069598
		private void SetupRandPartB()
		{
			if (this.ch2 != this.chPrev)
			{
				this.currentState = 2;
				this.count = 1;
				this.SetupRandPartA();
				return;
			}
			this.count++;
			if (this.count >= 4)
			{
				this.z = this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				if (this.rNToGo == 0)
				{
					this.rNToGo = BZip2Constants.rNums[this.rTPos];
					this.rTPos++;
					if (this.rTPos == 512)
					{
						this.rTPos = 0;
					}
				}
				this.rNToGo--;
				this.z ^= ((this.rNToGo == 1) ? '\u0001' : '\0');
				this.j2 = 0;
				this.currentState = 4;
				this.SetupRandPartC();
				return;
			}
			this.currentState = 2;
			this.SetupRandPartA();
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0006A690 File Offset: 0x00069690
		private void SetupRandPartC()
		{
			if (this.j2 < (int)this.z)
			{
				this.currentChar = this.ch2;
				this.mCrc.UpdateCRC(this.ch2);
				this.j2++;
				return;
			}
			this.currentState = 2;
			this.i2++;
			this.count = 0;
			this.SetupRandPartA();
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0006A6FC File Offset: 0x000696FC
		private void SetupNoRandPartB()
		{
			if (this.ch2 != this.chPrev)
			{
				this.currentState = 5;
				this.count = 1;
				this.SetupNoRandPartA();
				return;
			}
			this.count++;
			if (this.count >= 4)
			{
				this.z = this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				this.currentState = 7;
				this.j2 = 0;
				this.SetupNoRandPartC();
				return;
			}
			this.currentState = 5;
			this.SetupNoRandPartA();
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0006A78C File Offset: 0x0006978C
		private void SetupNoRandPartC()
		{
			if (this.j2 < (int)this.z)
			{
				this.currentChar = this.ch2;
				this.mCrc.UpdateCRC(this.ch2);
				this.j2++;
				return;
			}
			this.currentState = 5;
			this.i2++;
			this.count = 0;
			this.SetupNoRandPartA();
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0006A7F8 File Offset: 0x000697F8
		private void SetDecompressStructureSizes(int newSize100k)
		{
			if (0 <= newSize100k && newSize100k <= 9 && 0 <= this.blockSize100k)
			{
				int num = this.blockSize100k;
			}
			this.blockSize100k = newSize100k;
			if (newSize100k == 0)
			{
				return;
			}
			int num2 = 100000 * newSize100k;
			this.ll8 = new char[num2];
			this.tt = new int[num2];
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0006A84C File Offset: 0x0006984C
		public override void Flush()
		{
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0006A850 File Offset: 0x00069850
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i;
			for (i = 0; i < count; i++)
			{
				int num = this.ReadByte();
				if (num == -1)
				{
					break;
				}
				buffer[i + offset] = (byte)num;
			}
			return i;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0006A87E File Offset: 0x0006987E
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0006A882 File Offset: 0x00069882
		public override void SetLength(long value)
		{
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0006A884 File Offset: 0x00069884
		public override void Write(byte[] buffer, int offset, int count)
		{
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0006A886 File Offset: 0x00069886
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0006A889 File Offset: 0x00069889
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0006A88C File Offset: 0x0006988C
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0006A88F File Offset: 0x0006988F
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0006A893 File Offset: 0x00069893
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x0006A897 File Offset: 0x00069897
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

		// Token: 0x04000D03 RID: 3331
		private const int START_BLOCK_STATE = 1;

		// Token: 0x04000D04 RID: 3332
		private const int RAND_PART_A_STATE = 2;

		// Token: 0x04000D05 RID: 3333
		private const int RAND_PART_B_STATE = 3;

		// Token: 0x04000D06 RID: 3334
		private const int RAND_PART_C_STATE = 4;

		// Token: 0x04000D07 RID: 3335
		private const int NO_RAND_PART_A_STATE = 5;

		// Token: 0x04000D08 RID: 3336
		private const int NO_RAND_PART_B_STATE = 6;

		// Token: 0x04000D09 RID: 3337
		private const int NO_RAND_PART_C_STATE = 7;

		// Token: 0x04000D0A RID: 3338
		private int last;

		// Token: 0x04000D0B RID: 3339
		private int origPtr;

		// Token: 0x04000D0C RID: 3340
		private int blockSize100k;

		// Token: 0x04000D0D RID: 3341
		private bool blockRandomised;

		// Token: 0x04000D0E RID: 3342
		private int bsBuff;

		// Token: 0x04000D0F RID: 3343
		private int bsLive;

		// Token: 0x04000D10 RID: 3344
		private CRC mCrc = new CRC();

		// Token: 0x04000D11 RID: 3345
		private bool[] inUse = new bool[256];

		// Token: 0x04000D12 RID: 3346
		private int nInUse;

		// Token: 0x04000D13 RID: 3347
		private char[] seqToUnseq = new char[256];

		// Token: 0x04000D14 RID: 3348
		private char[] unseqToSeq = new char[256];

		// Token: 0x04000D15 RID: 3349
		private char[] selector = new char[18002];

		// Token: 0x04000D16 RID: 3350
		private char[] selectorMtf = new char[18002];

		// Token: 0x04000D17 RID: 3351
		private int[] tt;

		// Token: 0x04000D18 RID: 3352
		private char[] ll8;

		// Token: 0x04000D19 RID: 3353
		private int[] unzftab = new int[256];

		// Token: 0x04000D1A RID: 3354
		private int[][] limit = CBZip2InputStream.InitIntArray(6, 258);

		// Token: 0x04000D1B RID: 3355
		private int[][] basev = CBZip2InputStream.InitIntArray(6, 258);

		// Token: 0x04000D1C RID: 3356
		private int[][] perm = CBZip2InputStream.InitIntArray(6, 258);

		// Token: 0x04000D1D RID: 3357
		private int[] minLens = new int[6];

		// Token: 0x04000D1E RID: 3358
		private Stream bsStream;

		// Token: 0x04000D1F RID: 3359
		private bool streamEnd;

		// Token: 0x04000D20 RID: 3360
		private int currentChar = -1;

		// Token: 0x04000D21 RID: 3361
		private int currentState = 1;

		// Token: 0x04000D22 RID: 3362
		private int storedBlockCRC;

		// Token: 0x04000D23 RID: 3363
		private int storedCombinedCRC;

		// Token: 0x04000D24 RID: 3364
		private int computedBlockCRC;

		// Token: 0x04000D25 RID: 3365
		private int computedCombinedCRC;

		// Token: 0x04000D26 RID: 3366
		private int i2;

		// Token: 0x04000D27 RID: 3367
		private int count;

		// Token: 0x04000D28 RID: 3368
		private int chPrev;

		// Token: 0x04000D29 RID: 3369
		private int ch2;

		// Token: 0x04000D2A RID: 3370
		private int i;

		// Token: 0x04000D2B RID: 3371
		private int tPos;

		// Token: 0x04000D2C RID: 3372
		private int rNToGo;

		// Token: 0x04000D2D RID: 3373
		private int rTPos;

		// Token: 0x04000D2E RID: 3374
		private int j2;

		// Token: 0x04000D2F RID: 3375
		private char z;
	}
}
