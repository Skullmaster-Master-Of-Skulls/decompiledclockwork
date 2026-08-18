using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x02000035 RID: 53
	public class BZip2InputStream : Stream
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00009F38 File Offset: 0x00008F38
		public BZip2InputStream(Stream stream)
		{
			for (int i = 0; i < 6; i++)
			{
				this.limit[i] = new int[258];
				this.baseArray[i] = new int[258];
				this.perm[i] = new int[258];
			}
			this.BsSetStream(stream);
			this.Initialize();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000A056 File Offset: 0x00009056
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000A05E File Offset: 0x0000905E
		public bool IsStreamOwner
		{
			get
			{
				return this.isStreamOwner;
			}
			set
			{
				this.isStreamOwner = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000A067 File Offset: 0x00009067
		public override bool CanRead
		{
			get
			{
				return this.baseStream.CanRead;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000A074 File Offset: 0x00009074
		public override bool CanSeek
		{
			get
			{
				return this.baseStream.CanSeek;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000A081 File Offset: 0x00009081
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000A084 File Offset: 0x00009084
		public override long Length
		{
			get
			{
				return this.baseStream.Length;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A091 File Offset: 0x00009091
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000A09E File Offset: 0x0000909E
		public override long Position
		{
			get
			{
				return this.baseStream.Position;
			}
			set
			{
				throw new NotSupportedException("BZip2InputStream position cannot be set");
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A0AA File Offset: 0x000090AA
		public override void Flush()
		{
			if (this.baseStream != null)
			{
				this.baseStream.Flush();
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A0BF File Offset: 0x000090BF
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("BZip2InputStream Seek not supported");
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A0CB File Offset: 0x000090CB
		public override void SetLength(long value)
		{
			throw new NotSupportedException("BZip2InputStream SetLength not supported");
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A0D7 File Offset: 0x000090D7
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("BZip2InputStream Write not supported");
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A0E3 File Offset: 0x000090E3
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("BZip2InputStream WriteByte not supported");
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000A0F0 File Offset: 0x000090F0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < count; i++)
			{
				int num = this.ReadByte();
				if (num == -1)
				{
					return i;
				}
				buffer[offset + i] = (byte)num;
			}
			return count;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000A12C File Offset: 0x0000912C
		public override void Close()
		{
			if (this.IsStreamOwner && this.baseStream != null)
			{
				this.baseStream.Close();
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A14C File Offset: 0x0000914C
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

		// Token: 0x060001DD RID: 477 RVA: 0x0000A1B8 File Offset: 0x000091B8
		private void MakeMaps()
		{
			this.nInUse = 0;
			for (int i = 0; i < 256; i++)
			{
				if (this.inUse[i])
				{
					this.seqToUnseq[this.nInUse] = (byte)i;
					this.unseqToSeq[i] = (byte)this.nInUse;
					this.nInUse++;
				}
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A214 File Offset: 0x00009214
		private void Initialize()
		{
			char c = this.BsGetUChar();
			char c2 = this.BsGetUChar();
			char c3 = this.BsGetUChar();
			char c4 = this.BsGetUChar();
			if (c != 'B' || c2 != 'Z' || c3 != 'h' || c4 < '1' || c4 > '9')
			{
				this.streamEnd = true;
				return;
			}
			this.SetDecompressStructureSizes((int)(c4 - '0'));
			this.computedCombinedCRC = 0U;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A270 File Offset: 0x00009270
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
				BZip2InputStream.BadBlockHeader();
				this.streamEnd = true;
				return;
			}
			this.storedBlockCRC = this.BsGetInt32();
			this.blockRandomised = (this.BsR(1) == 1);
			this.GetAndMoveToFrontDecode();
			this.mCrc.Reset();
			this.currentState = 1;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A334 File Offset: 0x00009334
		private void EndBlock()
		{
			this.computedBlockCRC = (int)this.mCrc.Value;
			if (this.storedBlockCRC != this.computedBlockCRC)
			{
				BZip2InputStream.CrcError();
			}
			this.computedCombinedCRC = ((this.computedCombinedCRC << 1 & uint.MaxValue) | this.computedCombinedCRC >> 31);
			this.computedCombinedCRC ^= (uint)this.computedBlockCRC;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A393 File Offset: 0x00009393
		private void Complete()
		{
			this.storedCombinedCRC = this.BsGetInt32();
			if (this.storedCombinedCRC != (int)this.computedCombinedCRC)
			{
				BZip2InputStream.CrcError();
			}
			this.streamEnd = true;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A3BB File Offset: 0x000093BB
		private void BsSetStream(Stream stream)
		{
			this.baseStream = stream;
			this.bsLive = 0;
			this.bsBuff = 0;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A3D4 File Offset: 0x000093D4
		private void FillBuffer()
		{
			int num = 0;
			try
			{
				num = this.baseStream.ReadByte();
			}
			catch (Exception)
			{
				BZip2InputStream.CompressedStreamEOF();
			}
			if (num == -1)
			{
				BZip2InputStream.CompressedStreamEOF();
			}
			this.bsBuff = (this.bsBuff << 8 | (num & 255));
			this.bsLive += 8;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A438 File Offset: 0x00009438
		private int BsR(int n)
		{
			while (this.bsLive < n)
			{
				this.FillBuffer();
			}
			int result = this.bsBuff >> this.bsLive - n & (1 << n) - 1;
			this.bsLive -= n;
			return result;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A481 File Offset: 0x00009481
		private char BsGetUChar()
		{
			return (char)this.BsR(8);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A48B File Offset: 0x0000948B
		private int BsGetIntVS(int numBits)
		{
			return this.BsR(numBits);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A494 File Offset: 0x00009494
		private int BsGetInt32()
		{
			int num = this.BsR(8);
			num = (num << 8 | this.BsR(8));
			num = (num << 8 | this.BsR(8));
			return num << 8 | this.BsR(8);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A4D0 File Offset: 0x000094D0
		private void RecvDecodingTables()
		{
			char[][] array = new char[6][];
			for (int i = 0; i < 6; i++)
			{
				array[i] = new char[258];
			}
			bool[] array2 = new bool[16];
			for (int j = 0; j < 16; j++)
			{
				array2[j] = (this.BsR(1) == 1);
			}
			for (int k = 0; k < 16; k++)
			{
				if (array2[k])
				{
					for (int l = 0; l < 16; l++)
					{
						this.inUse[k * 16 + l] = (this.BsR(1) == 1);
					}
				}
				else
				{
					for (int m = 0; m < 16; m++)
					{
						this.inUse[k * 16 + m] = false;
					}
				}
			}
			this.MakeMaps();
			int num = this.nInUse + 2;
			int num2 = this.BsR(3);
			int num3 = this.BsR(15);
			for (int n = 0; n < num3; n++)
			{
				int num4 = 0;
				while (this.BsR(1) == 1)
				{
					num4++;
				}
				this.selectorMtf[n] = (byte)num4;
			}
			byte[] array3 = new byte[6];
			for (int num5 = 0; num5 < num2; num5++)
			{
				array3[num5] = (byte)num5;
			}
			for (int num6 = 0; num6 < num3; num6++)
			{
				int num7 = (int)this.selectorMtf[num6];
				byte b = array3[num7];
				while (num7 > 0)
				{
					array3[num7] = array3[num7 - 1];
					num7--;
				}
				array3[0] = b;
				this.selector[num6] = b;
			}
			for (int num8 = 0; num8 < num2; num8++)
			{
				int num9 = this.BsR(5);
				for (int num10 = 0; num10 < num; num10++)
				{
					while (this.BsR(1) == 1)
					{
						if (this.BsR(1) == 0)
						{
							num9++;
						}
						else
						{
							num9--;
						}
					}
					array[num8][num10] = (char)num9;
				}
			}
			for (int num11 = 0; num11 < num2; num11++)
			{
				int num12 = 32;
				int num13 = 0;
				for (int num14 = 0; num14 < num; num14++)
				{
					num13 = Math.Max(num13, (int)array[num11][num14]);
					num12 = Math.Min(num12, (int)array[num11][num14]);
				}
				BZip2InputStream.HbCreateDecodeTables(this.limit[num11], this.baseArray[num11], this.perm[num11], array[num11], num12, num13, num);
				this.minLens[num11] = num12;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A71C File Offset: 0x0000971C
		private void GetAndMoveToFrontDecode()
		{
			byte[] array = new byte[256];
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
			for (int j = 0; j <= 255; j++)
			{
				array[j] = (byte)j;
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
			int k;
			int num7;
			for (k = this.BsR(num6); k > this.limit[num5][num6]; k = (k << 1 | num7))
			{
				if (num6 > 20)
				{
					throw new BZip2Exception("Bzip data error");
				}
				num6++;
				while (this.bsLive < 1)
				{
					this.FillBuffer();
				}
				num7 = (this.bsBuff >> this.bsLive - 1 & 1);
				this.bsLive--;
			}
			if (k - this.baseArray[num5][num6] < 0 || k - this.baseArray[num5][num6] >= 258)
			{
				throw new BZip2Exception("Bzip data error");
			}
			int num8 = this.perm[num5][k - this.baseArray[num5][num6]];
			while (num8 != num2)
			{
				if (num8 == 0 || num8 == 1)
				{
					int l = -1;
					int num9 = 1;
					do
					{
						if (num8 == 0)
						{
							l += num9;
						}
						else if (num8 == 1)
						{
							l += 2 * num9;
						}
						num9 <<= 1;
						if (num4 == 0)
						{
							num3++;
							num4 = 50;
						}
						num4--;
						num5 = (int)this.selector[num3];
						num6 = this.minLens[num5];
						for (k = this.BsR(num6); k > this.limit[num5][num6]; k = (k << 1 | num7))
						{
							num6++;
							while (this.bsLive < 1)
							{
								this.FillBuffer();
							}
							num7 = (this.bsBuff >> this.bsLive - 1 & 1);
							this.bsLive--;
						}
						num8 = this.perm[num5][k - this.baseArray[num5][num6]];
					}
					while (num8 == 0 || num8 == 1);
					l++;
					byte b = this.seqToUnseq[(int)array[0]];
					this.unzftab[(int)b] += l;
					while (l > 0)
					{
						this.last++;
						this.ll8[this.last] = b;
						l--;
					}
					if (this.last >= num)
					{
						BZip2InputStream.BlockOverrun();
					}
				}
				else
				{
					this.last++;
					if (this.last >= num)
					{
						BZip2InputStream.BlockOverrun();
					}
					byte b2 = array[num8 - 1];
					this.unzftab[(int)this.seqToUnseq[(int)b2]]++;
					this.ll8[this.last] = this.seqToUnseq[(int)b2];
					for (int m = num8 - 1; m > 0; m--)
					{
						array[m] = array[m - 1];
					}
					array[0] = b2;
					if (num4 == 0)
					{
						num3++;
						num4 = 50;
					}
					num4--;
					num5 = (int)this.selector[num3];
					num6 = this.minLens[num5];
					for (k = this.BsR(num6); k > this.limit[num5][num6]; k = (k << 1 | num7))
					{
						num6++;
						while (this.bsLive < 1)
						{
							this.FillBuffer();
						}
						num7 = (this.bsBuff >> this.bsLive - 1 & 1);
						this.bsLive--;
					}
					num8 = this.perm[num5][k - this.baseArray[num5][num6]];
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000AB04 File Offset: 0x00009B04
		private void SetupBlock()
		{
			int[] array = new int[257];
			array[0] = 0;
			Array.Copy(this.unzftab, 0, array, 1, 256);
			for (int i = 1; i <= 256; i++)
			{
				array[i] += array[i - 1];
			}
			for (int j = 0; j <= this.last; j++)
			{
				byte b = this.ll8[j];
				this.tt[array[(int)b]] = j;
				array[(int)b]++;
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

		// Token: 0x060001EB RID: 491 RVA: 0x0000ABE8 File Offset: 0x00009BE8
		private void SetupRandPartA()
		{
			if (this.i2 <= this.last)
			{
				this.chPrev = this.ch2;
				this.ch2 = (int)this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				if (this.rNToGo == 0)
				{
					this.rNToGo = BZip2Constants.RandomNumbers[this.rTPos];
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
				this.mCrc.Update(this.ch2);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000ACE4 File Offset: 0x00009CE4
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
				this.mCrc.Update(this.ch2);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000AD78 File Offset: 0x00009D78
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
					this.rNToGo = BZip2Constants.RandomNumbers[this.rTPos];
					this.rTPos++;
					if (this.rTPos == 512)
					{
						this.rTPos = 0;
					}
				}
				this.rNToGo--;
				this.z ^= ((this.rNToGo == 1) ? 1 : 0);
				this.j2 = 0;
				this.currentState = 4;
				this.SetupRandPartC();
				return;
			}
			this.currentState = 2;
			this.SetupRandPartA();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000AE70 File Offset: 0x00009E70
		private void SetupRandPartC()
		{
			if (this.j2 < (int)this.z)
			{
				this.currentChar = this.ch2;
				this.mCrc.Update(this.ch2);
				this.j2++;
				return;
			}
			this.currentState = 2;
			this.i2++;
			this.count = 0;
			this.SetupRandPartA();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000AEDC File Offset: 0x00009EDC
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

		// Token: 0x060001F0 RID: 496 RVA: 0x0000AF6C File Offset: 0x00009F6C
		private void SetupNoRandPartC()
		{
			if (this.j2 < (int)this.z)
			{
				this.currentChar = this.ch2;
				this.mCrc.Update(this.ch2);
				this.j2++;
				return;
			}
			this.currentState = 5;
			this.i2++;
			this.count = 0;
			this.SetupNoRandPartA();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000AFD8 File Offset: 0x00009FD8
		private void SetDecompressStructureSizes(int newSize100k)
		{
			if (0 > newSize100k || newSize100k > 9 || 0 > this.blockSize100k || this.blockSize100k > 9)
			{
				throw new BZip2Exception("Invalid block size");
			}
			this.blockSize100k = newSize100k;
			if (newSize100k == 0)
			{
				return;
			}
			int num = 100000 * newSize100k;
			this.ll8 = new byte[num];
			this.tt = new int[num];
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B037 File Offset: 0x0000A037
		private static void CompressedStreamEOF()
		{
			throw new EndOfStreamException("BZip2 input stream end of compressed stream");
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B043 File Offset: 0x0000A043
		private static void BlockOverrun()
		{
			throw new BZip2Exception("BZip2 input stream block overrun");
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B04F File Offset: 0x0000A04F
		private static void BadBlockHeader()
		{
			throw new BZip2Exception("BZip2 input stream bad block header");
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B05B File Offset: 0x0000A05B
		private static void CrcError()
		{
			throw new BZip2Exception("BZip2 input stream crc error");
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000B068 File Offset: 0x0000A068
		private static void HbCreateDecodeTables(int[] limit, int[] baseArray, int[] perm, char[] length, int minLen, int maxLen, int alphaSize)
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
			for (int k = 0; k < 23; k++)
			{
				baseArray[k] = 0;
			}
			for (int l = 0; l < alphaSize; l++)
			{
				baseArray[(int)(length[l] + '\u0001')]++;
			}
			for (int m = 1; m < 23; m++)
			{
				baseArray[m] += baseArray[m - 1];
			}
			for (int n = 0; n < 23; n++)
			{
				limit[n] = 0;
			}
			int num2 = 0;
			for (int num3 = minLen; num3 <= maxLen; num3++)
			{
				num2 += baseArray[num3 + 1] - baseArray[num3];
				limit[num3] = num2 - 1;
				num2 <<= 1;
			}
			for (int num4 = minLen + 1; num4 <= maxLen; num4++)
			{
				baseArray[num4] = (limit[num4 - 1] + 1 << 1) - baseArray[num4];
			}
		}

		// Token: 0x0400011D RID: 285
		private const int START_BLOCK_STATE = 1;

		// Token: 0x0400011E RID: 286
		private const int RAND_PART_A_STATE = 2;

		// Token: 0x0400011F RID: 287
		private const int RAND_PART_B_STATE = 3;

		// Token: 0x04000120 RID: 288
		private const int RAND_PART_C_STATE = 4;

		// Token: 0x04000121 RID: 289
		private const int NO_RAND_PART_A_STATE = 5;

		// Token: 0x04000122 RID: 290
		private const int NO_RAND_PART_B_STATE = 6;

		// Token: 0x04000123 RID: 291
		private const int NO_RAND_PART_C_STATE = 7;

		// Token: 0x04000124 RID: 292
		private int last;

		// Token: 0x04000125 RID: 293
		private int origPtr;

		// Token: 0x04000126 RID: 294
		private int blockSize100k;

		// Token: 0x04000127 RID: 295
		private bool blockRandomised;

		// Token: 0x04000128 RID: 296
		private int bsBuff;

		// Token: 0x04000129 RID: 297
		private int bsLive;

		// Token: 0x0400012A RID: 298
		private IChecksum mCrc = new StrangeCRC();

		// Token: 0x0400012B RID: 299
		private bool[] inUse = new bool[256];

		// Token: 0x0400012C RID: 300
		private int nInUse;

		// Token: 0x0400012D RID: 301
		private byte[] seqToUnseq = new byte[256];

		// Token: 0x0400012E RID: 302
		private byte[] unseqToSeq = new byte[256];

		// Token: 0x0400012F RID: 303
		private byte[] selector = new byte[18002];

		// Token: 0x04000130 RID: 304
		private byte[] selectorMtf = new byte[18002];

		// Token: 0x04000131 RID: 305
		private int[] tt;

		// Token: 0x04000132 RID: 306
		private byte[] ll8;

		// Token: 0x04000133 RID: 307
		private int[] unzftab = new int[256];

		// Token: 0x04000134 RID: 308
		private int[][] limit = new int[6][];

		// Token: 0x04000135 RID: 309
		private int[][] baseArray = new int[6][];

		// Token: 0x04000136 RID: 310
		private int[][] perm = new int[6][];

		// Token: 0x04000137 RID: 311
		private int[] minLens = new int[6];

		// Token: 0x04000138 RID: 312
		private Stream baseStream;

		// Token: 0x04000139 RID: 313
		private bool streamEnd;

		// Token: 0x0400013A RID: 314
		private int currentChar = -1;

		// Token: 0x0400013B RID: 315
		private int currentState = 1;

		// Token: 0x0400013C RID: 316
		private int storedBlockCRC;

		// Token: 0x0400013D RID: 317
		private int storedCombinedCRC;

		// Token: 0x0400013E RID: 318
		private int computedBlockCRC;

		// Token: 0x0400013F RID: 319
		private uint computedCombinedCRC;

		// Token: 0x04000140 RID: 320
		private int count;

		// Token: 0x04000141 RID: 321
		private int chPrev;

		// Token: 0x04000142 RID: 322
		private int ch2;

		// Token: 0x04000143 RID: 323
		private int tPos;

		// Token: 0x04000144 RID: 324
		private int rNToGo;

		// Token: 0x04000145 RID: 325
		private int rTPos;

		// Token: 0x04000146 RID: 326
		private int i2;

		// Token: 0x04000147 RID: 327
		private int j2;

		// Token: 0x04000148 RID: 328
		private byte z;

		// Token: 0x04000149 RID: 329
		private bool isStreamOwner = true;
	}
}
