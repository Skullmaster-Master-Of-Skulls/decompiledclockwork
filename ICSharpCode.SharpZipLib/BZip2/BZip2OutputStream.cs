using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x02000036 RID: 54
	public class BZip2OutputStream : Stream
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0000B176 File Offset: 0x0000A176
		public BZip2OutputStream(Stream stream) : this(stream, 9)
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B1C0 File Offset: 0x0000A1C0
		public BZip2OutputStream(Stream stream, int blockSize)
		{
			this.BsSetStream(stream);
			this.workFactor = 50;
			if (blockSize > 9)
			{
				blockSize = 9;
			}
			if (blockSize < 1)
			{
				blockSize = 1;
			}
			this.blockSize100k = blockSize;
			this.AllocateCompressStructures();
			this.Initialize();
			this.InitBlock();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B29C File Offset: 0x0000A29C
		~BZip2OutputStream()
		{
			this.Dispose(false);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000B2CC File Offset: 0x0000A2CC
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000B2D4 File Offset: 0x0000A2D4
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000B2DD File Offset: 0x0000A2DD
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000B2E0 File Offset: 0x0000A2E0
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000B2E3 File Offset: 0x0000A2E3
		public override bool CanWrite
		{
			get
			{
				return this.baseStream.CanWrite;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000B2F0 File Offset: 0x0000A2F0
		public override long Length
		{
			get
			{
				return this.baseStream.Length;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000B2FD File Offset: 0x0000A2FD
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000B30A File Offset: 0x0000A30A
		public override long Position
		{
			get
			{
				return this.baseStream.Position;
			}
			set
			{
				throw new NotSupportedException("BZip2OutputStream position cannot be set");
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000B316 File Offset: 0x0000A316
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("BZip2OutputStream Seek not supported");
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000B322 File Offset: 0x0000A322
		public override void SetLength(long value)
		{
			throw new NotSupportedException("BZip2OutputStream SetLength not supported");
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B32E File Offset: 0x0000A32E
		public override int ReadByte()
		{
			throw new NotSupportedException("BZip2OutputStream ReadByte not supported");
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000B33A File Offset: 0x0000A33A
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("BZip2OutputStream Read not supported");
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000B348 File Offset: 0x0000A348
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset/count out of range");
			}
			for (int i = 0; i < count; i++)
			{
				this.WriteByte(buffer[offset + i]);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000B3AC File Offset: 0x0000A3AC
		public override void WriteByte(byte value)
		{
			int num = (256 + (int)value) % 256;
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

		// Token: 0x06000208 RID: 520 RVA: 0x0000B433 File Offset: 0x0000A433
		public override void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B444 File Offset: 0x0000A444
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

		// Token: 0x0600020A RID: 522 RVA: 0x0000B4A0 File Offset: 0x0000A4A0
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
				this.mCrc.Update(this.currentChar);
			}
			switch (this.runLength)
			{
			case 1:
				this.last++;
				this.block[this.last + 1] = (byte)this.currentChar;
				return;
			case 2:
				this.last++;
				this.block[this.last + 1] = (byte)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (byte)this.currentChar;
				return;
			case 3:
				this.last++;
				this.block[this.last + 1] = (byte)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (byte)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (byte)this.currentChar;
				return;
			default:
				this.inUse[this.runLength - 4] = true;
				this.last++;
				this.block[this.last + 1] = (byte)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (byte)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (byte)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (byte)this.currentChar;
				this.last++;
				this.block[this.last + 1] = (byte)(this.runLength - 4);
				return;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000B6C2 File Offset: 0x0000A6C2
		public int BytesWritten
		{
			get
			{
				return this.bytesOut;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B6CC File Offset: 0x0000A6CC
		protected override void Dispose(bool disposing)
		{
			try
			{
				base.Dispose(disposing);
				if (!this.disposed_)
				{
					this.disposed_ = true;
					if (this.runLength > 0)
					{
						this.WriteRun();
					}
					this.currentChar = -1;
					this.EndBlock();
					this.EndCompression();
					this.Flush();
				}
			}
			finally
			{
				if (disposing && this.IsStreamOwner)
				{
					this.baseStream.Close();
				}
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B740 File Offset: 0x0000A740
		public override void Flush()
		{
			this.baseStream.Flush();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B74D File Offset: 0x0000A74D
		private void Initialize()
		{
			this.bytesOut = 0;
			this.nBlocksRandomised = 0;
			this.BsPutUChar(66);
			this.BsPutUChar(90);
			this.BsPutUChar(104);
			this.BsPutUChar(48 + this.blockSize100k);
			this.combinedCRC = 0U;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B78C File Offset: 0x0000A78C
		private void InitBlock()
		{
			this.mCrc.Reset();
			this.last = -1;
			for (int i = 0; i < 256; i++)
			{
				this.inUse[i] = false;
			}
			this.allowableBlockSize = 100000 * this.blockSize100k - 20;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B7DC File Offset: 0x0000A7DC
		private void EndBlock()
		{
			if (this.last < 0)
			{
				return;
			}
			this.blockCRC = (uint)this.mCrc.Value;
			this.combinedCRC = (this.combinedCRC << 1 | this.combinedCRC >> 31);
			this.combinedCRC ^= this.blockCRC;
			this.DoReversibleTransformation();
			this.BsPutUChar(49);
			this.BsPutUChar(65);
			this.BsPutUChar(89);
			this.BsPutUChar(38);
			this.BsPutUChar(83);
			this.BsPutUChar(89);
			this.BsPutint((int)this.blockCRC);
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

		// Token: 0x06000211 RID: 529 RVA: 0x0000B8A0 File Offset: 0x0000A8A0
		private void EndCompression()
		{
			this.BsPutUChar(23);
			this.BsPutUChar(114);
			this.BsPutUChar(69);
			this.BsPutUChar(56);
			this.BsPutUChar(80);
			this.BsPutUChar(144);
			this.BsPutint((int)this.combinedCRC);
			this.BsFinishedWithStream();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000B8F2 File Offset: 0x0000A8F2
		private void BsSetStream(Stream stream)
		{
			this.baseStream = stream;
			this.bsLive = 0;
			this.bsBuff = 0;
			this.bytesOut = 0;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B910 File Offset: 0x0000A910
		private void BsFinishedWithStream()
		{
			while (this.bsLive > 0)
			{
				int num = this.bsBuff >> 24;
				this.baseStream.WriteByte((byte)num);
				this.bsBuff <<= 8;
				this.bsLive -= 8;
				this.bytesOut++;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B96C File Offset: 0x0000A96C
		private void BsW(int n, int v)
		{
			while (this.bsLive >= 8)
			{
				int num = this.bsBuff >> 24;
				this.baseStream.WriteByte((byte)num);
				this.bsBuff <<= 8;
				this.bsLive -= 8;
				this.bytesOut++;
			}
			this.bsBuff |= v << 32 - this.bsLive - n;
			this.bsLive += n;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000B9F0 File Offset: 0x0000A9F0
		private void BsPutUChar(int c)
		{
			this.BsW(8, c);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000B9FC File Offset: 0x0000A9FC
		private void BsPutint(int u)
		{
			this.BsW(8, u >> 24 & 255);
			this.BsW(8, u >> 16 & 255);
			this.BsW(8, u >> 8 & 255);
			this.BsW(8, u & 255);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000BA49 File Offset: 0x0000AA49
		private void BsPutIntVS(int numBits, int c)
		{
			this.BsW(numBits, c);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000BA54 File Offset: 0x0000AA54
		private void SendMTFValues()
		{
			char[][] array = new char[6][];
			for (int i = 0; i < 6; i++)
			{
				array[i] = new char[258];
			}
			int num = 0;
			int num2 = this.nInUse + 2;
			for (int j = 0; j < 6; j++)
			{
				for (int k = 0; k < num2; k++)
				{
					array[j][k] = '\u000f';
				}
			}
			if (this.nMTF <= 0)
			{
				BZip2OutputStream.Panic();
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
			int l = num3;
			int num4 = this.nMTF;
			int m = 0;
			while (l > 0)
			{
				int num5 = num4 / l;
				int num6 = 0;
				int num7 = m - 1;
				while (num6 < num5 && num7 < num2 - 1)
				{
					num7++;
					num6 += this.mtfFreq[num7];
				}
				if (num7 > m && l != num3 && l != 1 && (num3 - l) % 2 == 1)
				{
					num6 -= this.mtfFreq[num7];
					num7--;
				}
				for (int n = 0; n < num2; n++)
				{
					if (n >= m && n <= num7)
					{
						array[l - 1][n] = '\0';
					}
					else
					{
						array[l - 1][n] = '\u000f';
					}
				}
				l--;
				m = num7 + 1;
				num4 -= num6;
			}
			int[][] array2 = new int[6][];
			for (int num8 = 0; num8 < 6; num8++)
			{
				array2[num8] = new int[258];
			}
			int[] array3 = new int[6];
			short[] array4 = new short[6];
			for (int num9 = 0; num9 < 4; num9++)
			{
				for (int num10 = 0; num10 < num3; num10++)
				{
					array3[num10] = 0;
				}
				for (int num11 = 0; num11 < num3; num11++)
				{
					for (int num12 = 0; num12 < num2; num12++)
					{
						array2[num11][num12] = 0;
					}
				}
				num = 0;
				int num13 = 0;
				int num7;
				for (m = 0; m < this.nMTF; m = num7 + 1)
				{
					num7 = m + 50 - 1;
					if (num7 >= this.nMTF)
					{
						num7 = this.nMTF - 1;
					}
					for (int num14 = 0; num14 < num3; num14++)
					{
						array4[num14] = 0;
					}
					if (num3 == 6)
					{
						short num20;
						short num19;
						short num18;
						short num17;
						short num16;
						short num15 = num16 = (num17 = (num18 = (num19 = (num20 = 0))));
						for (int num21 = m; num21 <= num7; num21++)
						{
							short num22 = this.szptr[num21];
							num16 += (short)array[0][(int)num22];
							num15 += (short)array[1][(int)num22];
							num17 += (short)array[2][(int)num22];
							num18 += (short)array[3][(int)num22];
							num19 += (short)array[4][(int)num22];
							num20 += (short)array[5][(int)num22];
						}
						array4[0] = num16;
						array4[1] = num15;
						array4[2] = num17;
						array4[3] = num18;
						array4[4] = num19;
						array4[5] = num20;
					}
					else
					{
						for (int num23 = m; num23 <= num7; num23++)
						{
							short num24 = this.szptr[num23];
							for (int num25 = 0; num25 < num3; num25++)
							{
								short[] array5 = array4;
								int num26 = num25;
								array5[num26] += (short)array[num25][(int)num24];
							}
						}
					}
					int num27 = 999999999;
					int num28 = -1;
					for (int num29 = 0; num29 < num3; num29++)
					{
						if ((int)array4[num29] < num27)
						{
							num27 = (int)array4[num29];
							num28 = num29;
						}
					}
					num13 += num27;
					array3[num28]++;
					this.selector[num] = (char)num28;
					num++;
					for (int num30 = m; num30 <= num7; num30++)
					{
						array2[num28][(int)this.szptr[num30]]++;
					}
				}
				for (int num31 = 0; num31 < num3; num31++)
				{
					BZip2OutputStream.HbMakeCodeLengths(array[num31], array2[num31], num2, 20);
				}
			}
			if (num3 >= 8)
			{
				BZip2OutputStream.Panic();
			}
			if (num >= 32768 || num > 18002)
			{
				BZip2OutputStream.Panic();
			}
			char[] array6 = new char[6];
			for (int num32 = 0; num32 < num3; num32++)
			{
				array6[num32] = (char)num32;
			}
			for (int num33 = 0; num33 < num; num33++)
			{
				char c = this.selector[num33];
				int num34 = 0;
				char c2 = array6[num34];
				while (c != c2)
				{
					num34++;
					char c3 = c2;
					c2 = array6[num34];
					array6[num34] = c3;
				}
				array6[0] = c2;
				this.selectorMtf[num33] = (char)num34;
			}
			int[][] array7 = new int[6][];
			for (int num35 = 0; num35 < 6; num35++)
			{
				array7[num35] = new int[258];
			}
			for (int num36 = 0; num36 < num3; num36++)
			{
				int num37 = 32;
				int num38 = 0;
				for (int num39 = 0; num39 < num2; num39++)
				{
					if ((int)array[num36][num39] > num38)
					{
						num38 = (int)array[num36][num39];
					}
					if ((int)array[num36][num39] < num37)
					{
						num37 = (int)array[num36][num39];
					}
				}
				if (num38 > 20)
				{
					BZip2OutputStream.Panic();
				}
				if (num37 < 1)
				{
					BZip2OutputStream.Panic();
				}
				BZip2OutputStream.HbAssignCodes(array7[num36], array[num36], num37, num38, num2);
			}
			bool[] array8 = new bool[16];
			for (int num40 = 0; num40 < 16; num40++)
			{
				array8[num40] = false;
				for (int num41 = 0; num41 < 16; num41++)
				{
					if (this.inUse[num40 * 16 + num41])
					{
						array8[num40] = true;
					}
				}
			}
			for (int num42 = 0; num42 < 16; num42++)
			{
				if (array8[num42])
				{
					this.BsW(1, 1);
				}
				else
				{
					this.BsW(1, 0);
				}
			}
			for (int num43 = 0; num43 < 16; num43++)
			{
				if (array8[num43])
				{
					for (int num44 = 0; num44 < 16; num44++)
					{
						if (this.inUse[num43 * 16 + num44])
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
			for (int num45 = 0; num45 < num; num45++)
			{
				for (int num46 = 0; num46 < (int)this.selectorMtf[num45]; num46++)
				{
					this.BsW(1, 1);
				}
				this.BsW(1, 0);
			}
			for (int num47 = 0; num47 < num3; num47++)
			{
				int num48 = (int)array[num47][0];
				this.BsW(5, num48);
				for (int num49 = 0; num49 < num2; num49++)
				{
					while (num48 < (int)array[num47][num49])
					{
						this.BsW(2, 2);
						num48++;
					}
					while (num48 > (int)array[num47][num49])
					{
						this.BsW(2, 3);
						num48--;
					}
					this.BsW(1, 0);
				}
			}
			int num50 = 0;
			m = 0;
			while (m < this.nMTF)
			{
				int num7 = m + 50 - 1;
				if (num7 >= this.nMTF)
				{
					num7 = this.nMTF - 1;
				}
				for (int num51 = m; num51 <= num7; num51++)
				{
					this.BsW((int)array[(int)this.selector[num50]][(int)this.szptr[num51]], array7[(int)this.selector[num50]][(int)this.szptr[num51]]);
				}
				m = num7 + 1;
				num50++;
			}
			if (num50 != num)
			{
				BZip2OutputStream.Panic();
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C183 File Offset: 0x0000B183
		private void MoveToFrontCodeAndSend()
		{
			this.BsPutIntVS(24, this.origPtr);
			this.GenerateMTFValues();
			this.SendMTFValues();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C1A0 File Offset: 0x0000B1A0
		private void SimpleSort(int lo, int hi, int d)
		{
			int num = hi - lo + 1;
			if (num < 2)
			{
				return;
			}
			int i = 0;
			while (this.increments[i] < num)
			{
				i++;
			}
			for (i--; i >= 0; i--)
			{
				int num2 = this.increments[i];
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

		// Token: 0x0600021B RID: 539 RVA: 0x0000C31C File Offset: 0x0000B31C
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

		// Token: 0x0600021C RID: 540 RVA: 0x0000C364 File Offset: 0x0000B364
		private void QSort3(int loSt, int hiSt, int dSt)
		{
			BZip2OutputStream.StackElement[] array = new BZip2OutputStream.StackElement[1000];
			int i = 0;
			array[i].ll = loSt;
			array[i].hh = hiSt;
			array[i].dd = dSt;
			i++;
			while (i > 0)
			{
				if (i >= 1000)
				{
					BZip2OutputStream.Panic();
				}
				i--;
				int ll = array[i].ll;
				int hh = array[i].hh;
				int dd = array[i].dd;
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
					int num = (int)BZip2OutputStream.Med3(this.block[this.zptr[ll] + dd + 1], this.block[this.zptr[hh] + dd + 1], this.block[this.zptr[ll + hh >> 1] + dd + 1]);
					int j;
					int num2 = j = ll;
					int num4;
					int num3 = num4 = hh;
					for (;;)
					{
						if (j <= num4)
						{
							int num5 = (int)this.block[this.zptr[j] + dd + 1] - num;
							if (num5 == 0)
							{
								int num6 = this.zptr[j];
								this.zptr[j] = this.zptr[num2];
								this.zptr[num2] = num6;
								num2++;
								j++;
								continue;
							}
							if (num5 <= 0)
							{
								j++;
								continue;
							}
						}
						while (j <= num4)
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
						if (j > num4)
						{
							break;
						}
						int num8 = this.zptr[j];
						this.zptr[j] = this.zptr[num4];
						this.zptr[num4] = num8;
						j++;
						num4--;
					}
					if (num3 < num2)
					{
						array[i].ll = ll;
						array[i].hh = hh;
						array[i].dd = dd + 1;
						i++;
					}
					else
					{
						int num5 = (num2 - ll < j - num2) ? (num2 - ll) : (j - num2);
						this.Vswap(ll, j - num5, num5);
						int num9 = (hh - num3 < num3 - num4) ? (hh - num3) : (num3 - num4);
						this.Vswap(j, hh - num9 + 1, num9);
						num5 = ll + j - num2 - 1;
						num9 = hh - (num3 - num4) + 1;
						array[i].ll = ll;
						array[i].hh = num5;
						array[i].dd = dd;
						i++;
						array[i].ll = num5 + 1;
						array[i].hh = num9 - 1;
						array[i].dd = dd + 1;
						i++;
						array[i].ll = num9;
						array[i].hh = hh;
						array[i].dd = dd;
						i++;
					}
				}
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C6C0 File Offset: 0x0000B6C0
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
			j = ((int)this.block[this.last + 1] << 8) + (int)this.block[1];
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
						BZip2OutputStream.Panic();
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

		// Token: 0x0600021E RID: 542 RVA: 0x0000CBF0 File Offset: 0x0000BBF0
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
					num = BZip2Constants.RandomNumbers[num2];
					num2++;
					if (num2 == 512)
					{
						num2 = 0;
					}
				}
				num--;
				byte[] array = this.block;
				int num3 = i + 1;
				array[num3] ^= ((num == 1) ? 1 : 0);
				byte[] array2 = this.block;
				int num4 = i + 1;
				array2[num4] &= byte.MaxValue;
				this.inUse[(int)this.block[i + 1]] = true;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000CCA0 File Offset: 0x0000BCA0
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
				BZip2OutputStream.Panic();
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CD54 File Offset: 0x0000BD54
		private bool FullGtU(int i1, int i2)
		{
			byte b = this.block[i1 + 1];
			byte b2 = this.block[i2 + 1];
			if (b != b2)
			{
				return b > b2;
			}
			i1++;
			i2++;
			b = this.block[i1 + 1];
			b2 = this.block[i2 + 1];
			if (b != b2)
			{
				return b > b2;
			}
			i1++;
			i2++;
			b = this.block[i1 + 1];
			b2 = this.block[i2 + 1];
			if (b != b2)
			{
				return b > b2;
			}
			i1++;
			i2++;
			b = this.block[i1 + 1];
			b2 = this.block[i2 + 1];
			if (b != b2)
			{
				return b > b2;
			}
			i1++;
			i2++;
			b = this.block[i1 + 1];
			b2 = this.block[i2 + 1];
			if (b != b2)
			{
				return b > b2;
			}
			i1++;
			i2++;
			b = this.block[i1 + 1];
			b2 = this.block[i2 + 1];
			if (b != b2)
			{
				return b > b2;
			}
			i1++;
			i2++;
			int num = this.last + 1;
			int num2;
			int num3;
			for (;;)
			{
				b = this.block[i1 + 1];
				b2 = this.block[i2 + 1];
				if (b != b2)
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
				b = this.block[i1 + 1];
				b2 = this.block[i2 + 1];
				if (b != b2)
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
				b = this.block[i1 + 1];
				b2 = this.block[i2 + 1];
				if (b != b2)
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
				b = this.block[i1 + 1];
				b2 = this.block[i2 + 1];
				if (b != b2)
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
			return b > b2;
			Block_8:
			return num2 > num3;
			Block_9:
			return b > b2;
			Block_10:
			return num2 > num3;
			Block_11:
			return b > b2;
			Block_12:
			return num2 > num3;
			Block_13:
			return b > b2;
			Block_14:
			return num2 > num3;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CFC8 File Offset: 0x0000BFC8
		private void AllocateCompressStructures()
		{
			int num = 100000 * this.blockSize100k;
			this.block = new byte[num + 1 + 20];
			this.quadrant = new int[num + 20];
			this.zptr = new int[num];
			this.ftab = new int[65537];
			if (this.block != null && this.quadrant != null && this.zptr != null)
			{
				int[] array = this.ftab;
			}
			this.szptr = new short[2 * num];
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000D04C File Offset: 0x0000C04C
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

		// Token: 0x06000223 RID: 547 RVA: 0x0000D279 File Offset: 0x0000C279
		private static void Panic()
		{
			throw new BZip2Exception("BZip2 output stream panic");
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000D288 File Offset: 0x0000C288
		private static void HbMakeCodeLengths(char[] len, int[] freq, int alphaSize, int maxLen)
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
				for (int k = 1; k <= alphaSize; k++)
				{
					array3[k] = -1;
					j++;
					array[j] = k;
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
					BZip2OutputStream.Panic();
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
					num5 = 1;
					num6 = array[num5];
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
					num++;
					array3[num4] = (array3[num8] = num);
					array2[num] = ((int)(((long)array2[num4] & (long)((ulong)-256)) + ((long)array2[num8] & (long)((ulong)-256))) | 1 + (((array2[num4] & 255) > (array2[num8] & 255)) ? (array2[num4] & 255) : (array2[num8] & 255)));
					array3[num] = -1;
					j++;
					array[j] = num;
					num5 = j;
					num6 = array[num5];
					while (array2[num6] < array2[array[num5 >> 1]])
					{
						array[num5] = array[num5 >> 1];
						num5 >>= 1;
					}
					array[num5] = num6;
				}
				if (num >= 516)
				{
					BZip2OutputStream.Panic();
				}
				bool flag = false;
				for (int l = 1; l <= alphaSize; l++)
				{
					int num9 = 0;
					int num10 = l;
					while (array3[num10] >= 0)
					{
						num10 = array3[num10];
						num9++;
					}
					len[l - 1] = (char)num9;
					if (num9 > maxLen)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					break;
				}
				for (int m = 1; m < alphaSize; m++)
				{
					int num9 = array2[m] >> 8;
					num9 = 1 + num9 / 2;
					array2[m] = num9 << 8;
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000D568 File Offset: 0x0000C568
		private static void HbAssignCodes(int[] code, char[] length, int minLen, int maxLen, int alphaSize)
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

		// Token: 0x06000226 RID: 550 RVA: 0x0000D5A4 File Offset: 0x0000C5A4
		private static byte Med3(byte a, byte b, byte c)
		{
			if (a > b)
			{
				byte b2 = a;
				a = b;
				b = b2;
			}
			if (b > c)
			{
				byte b2 = b;
				b = c;
				c = b2;
			}
			if (a > b)
			{
				b = a;
			}
			return b;
		}

		// Token: 0x0400014A RID: 330
		private const int SETMASK = 2097152;

		// Token: 0x0400014B RID: 331
		private const int CLEARMASK = -2097153;

		// Token: 0x0400014C RID: 332
		private const int GREATER_ICOST = 15;

		// Token: 0x0400014D RID: 333
		private const int LESSER_ICOST = 0;

		// Token: 0x0400014E RID: 334
		private const int SMALL_THRESH = 20;

		// Token: 0x0400014F RID: 335
		private const int DEPTH_THRESH = 10;

		// Token: 0x04000150 RID: 336
		private const int QSORT_STACK_SIZE = 1000;

		// Token: 0x04000151 RID: 337
		private readonly int[] increments = new int[]
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

		// Token: 0x04000152 RID: 338
		private bool isStreamOwner = true;

		// Token: 0x04000153 RID: 339
		private int last;

		// Token: 0x04000154 RID: 340
		private int origPtr;

		// Token: 0x04000155 RID: 341
		private int blockSize100k;

		// Token: 0x04000156 RID: 342
		private bool blockRandomised;

		// Token: 0x04000157 RID: 343
		private int bytesOut;

		// Token: 0x04000158 RID: 344
		private int bsBuff;

		// Token: 0x04000159 RID: 345
		private int bsLive;

		// Token: 0x0400015A RID: 346
		private IChecksum mCrc = new StrangeCRC();

		// Token: 0x0400015B RID: 347
		private bool[] inUse = new bool[256];

		// Token: 0x0400015C RID: 348
		private int nInUse;

		// Token: 0x0400015D RID: 349
		private char[] seqToUnseq = new char[256];

		// Token: 0x0400015E RID: 350
		private char[] unseqToSeq = new char[256];

		// Token: 0x0400015F RID: 351
		private char[] selector = new char[18002];

		// Token: 0x04000160 RID: 352
		private char[] selectorMtf = new char[18002];

		// Token: 0x04000161 RID: 353
		private byte[] block;

		// Token: 0x04000162 RID: 354
		private int[] quadrant;

		// Token: 0x04000163 RID: 355
		private int[] zptr;

		// Token: 0x04000164 RID: 356
		private short[] szptr;

		// Token: 0x04000165 RID: 357
		private int[] ftab;

		// Token: 0x04000166 RID: 358
		private int nMTF;

		// Token: 0x04000167 RID: 359
		private int[] mtfFreq = new int[258];

		// Token: 0x04000168 RID: 360
		private int workFactor;

		// Token: 0x04000169 RID: 361
		private int workDone;

		// Token: 0x0400016A RID: 362
		private int workLimit;

		// Token: 0x0400016B RID: 363
		private bool firstAttempt;

		// Token: 0x0400016C RID: 364
		private int nBlocksRandomised;

		// Token: 0x0400016D RID: 365
		private int currentChar = -1;

		// Token: 0x0400016E RID: 366
		private int runLength;

		// Token: 0x0400016F RID: 367
		private uint blockCRC;

		// Token: 0x04000170 RID: 368
		private uint combinedCRC;

		// Token: 0x04000171 RID: 369
		private int allowableBlockSize;

		// Token: 0x04000172 RID: 370
		private Stream baseStream;

		// Token: 0x04000173 RID: 371
		private bool disposed_;

		// Token: 0x02000037 RID: 55
		private struct StackElement
		{
			// Token: 0x04000174 RID: 372
			public int ll;

			// Token: 0x04000175 RID: 373
			public int hh;

			// Token: 0x04000176 RID: 374
			public int dd;
		}
	}
}
