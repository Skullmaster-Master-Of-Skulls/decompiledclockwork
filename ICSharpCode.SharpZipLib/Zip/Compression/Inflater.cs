using System;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x0200004F RID: 79
	public class Inflater
	{
		// Token: 0x0600035A RID: 858 RVA: 0x00013B17 File Offset: 0x00012B17
		public Inflater() : this(false)
		{
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00013B20 File Offset: 0x00012B20
		public Inflater(bool noHeader)
		{
			this.noHeader = noHeader;
			this.adler = new Adler32();
			this.input = new StreamManipulator();
			this.outputWindow = new OutputWindow();
			this.mode = (noHeader ? 2 : 0);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00013B60 File Offset: 0x00012B60
		public void Reset()
		{
			this.mode = (this.noHeader ? 2 : 0);
			this.totalIn = 0L;
			this.totalOut = 0L;
			this.input.Reset();
			this.outputWindow.Reset();
			this.dynHeader = null;
			this.litlenTree = null;
			this.distTree = null;
			this.isLastBlock = false;
			this.adler.Reset();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00013BCC File Offset: 0x00012BCC
		private bool DecodeHeader()
		{
			int num = this.input.PeekBits(16);
			if (num < 0)
			{
				return false;
			}
			this.input.DropBits(16);
			num = ((num << 8 | num >> 8) & 65535);
			if (num % 31 != 0)
			{
				throw new SharpZipBaseException("Header checksum illegal");
			}
			if ((num & 3840) != 2048)
			{
				throw new SharpZipBaseException("Compression Method unknown");
			}
			if ((num & 32) == 0)
			{
				this.mode = 2;
			}
			else
			{
				this.mode = 1;
				this.neededBits = 32;
			}
			return true;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00013C54 File Offset: 0x00012C54
		private bool DecodeDict()
		{
			while (this.neededBits > 0)
			{
				int num = this.input.PeekBits(8);
				if (num < 0)
				{
					return false;
				}
				this.input.DropBits(8);
				this.readAdler = (this.readAdler << 8 | num);
				this.neededBits -= 8;
			}
			return false;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00013CAC File Offset: 0x00012CAC
		private bool DecodeHuffman()
		{
			int i = this.outputWindow.GetFreeSpace();
			while (i >= 258)
			{
				int symbol;
				switch (this.mode)
				{
				case 7:
					while (((symbol = this.litlenTree.GetSymbol(this.input)) & -256) == 0)
					{
						this.outputWindow.Write(symbol);
						if (--i < 258)
						{
							return true;
						}
					}
					if (symbol >= 257)
					{
						try
						{
							this.repLength = Inflater.CPLENS[symbol - 257];
							this.neededBits = Inflater.CPLEXT[symbol - 257];
						}
						catch (Exception)
						{
							throw new SharpZipBaseException("Illegal rep length code");
						}
						goto IL_C5;
					}
					if (symbol < 0)
					{
						return false;
					}
					this.distTree = null;
					this.litlenTree = null;
					this.mode = 2;
					return true;
				case 8:
					goto IL_C5;
				case 9:
					goto IL_114;
				case 10:
					break;
				default:
					throw new SharpZipBaseException("Inflater unknown mode");
				}
				IL_154:
				if (this.neededBits > 0)
				{
					this.mode = 10;
					int num = this.input.PeekBits(this.neededBits);
					if (num < 0)
					{
						return false;
					}
					this.input.DropBits(this.neededBits);
					this.repDist += num;
				}
				this.outputWindow.Repeat(this.repLength, this.repDist);
				i -= this.repLength;
				this.mode = 7;
				continue;
				IL_114:
				symbol = this.distTree.GetSymbol(this.input);
				if (symbol < 0)
				{
					return false;
				}
				try
				{
					this.repDist = Inflater.CPDIST[symbol];
					this.neededBits = Inflater.CPDEXT[symbol];
				}
				catch (Exception)
				{
					throw new SharpZipBaseException("Illegal rep dist code");
				}
				goto IL_154;
				IL_C5:
				if (this.neededBits > 0)
				{
					this.mode = 8;
					int num2 = this.input.PeekBits(this.neededBits);
					if (num2 < 0)
					{
						return false;
					}
					this.input.DropBits(this.neededBits);
					this.repLength += num2;
				}
				this.mode = 9;
				goto IL_114;
			}
			return true;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00013EB4 File Offset: 0x00012EB4
		private bool DecodeChksum()
		{
			while (this.neededBits > 0)
			{
				int num = this.input.PeekBits(8);
				if (num < 0)
				{
					return false;
				}
				this.input.DropBits(8);
				this.readAdler = (this.readAdler << 8 | num);
				this.neededBits -= 8;
			}
			if ((int)this.adler.Value != this.readAdler)
			{
				throw new SharpZipBaseException(string.Concat(new object[]
				{
					"Adler chksum doesn't match: ",
					(int)this.adler.Value,
					" vs. ",
					this.readAdler
				}));
			}
			this.mode = 12;
			return false;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00013F6C File Offset: 0x00012F6C
		private bool Decode()
		{
			switch (this.mode)
			{
			case 0:
				return this.DecodeHeader();
			case 1:
				return this.DecodeDict();
			case 2:
				if (this.isLastBlock)
				{
					if (this.noHeader)
					{
						this.mode = 12;
						return false;
					}
					this.input.SkipToByteBoundary();
					this.neededBits = 32;
					this.mode = 11;
					return true;
				}
				else
				{
					int num = this.input.PeekBits(3);
					if (num < 0)
					{
						return false;
					}
					this.input.DropBits(3);
					if ((num & 1) != 0)
					{
						this.isLastBlock = true;
					}
					switch (num >> 1)
					{
					case 0:
						this.input.SkipToByteBoundary();
						this.mode = 3;
						break;
					case 1:
						this.litlenTree = InflaterHuffmanTree.defLitLenTree;
						this.distTree = InflaterHuffmanTree.defDistTree;
						this.mode = 7;
						break;
					case 2:
						this.dynHeader = new InflaterDynHeader();
						this.mode = 6;
						break;
					default:
						throw new SharpZipBaseException("Unknown block type " + num);
					}
					return true;
				}
				break;
			case 3:
				if ((this.uncomprLen = this.input.PeekBits(16)) < 0)
				{
					return false;
				}
				this.input.DropBits(16);
				this.mode = 4;
				break;
			case 4:
				break;
			case 5:
				goto IL_1A9;
			case 6:
				if (!this.dynHeader.Decode(this.input))
				{
					return false;
				}
				this.litlenTree = this.dynHeader.BuildLitLenTree();
				this.distTree = this.dynHeader.BuildDistTree();
				this.mode = 7;
				goto IL_22D;
			case 7:
			case 8:
			case 9:
			case 10:
				goto IL_22D;
			case 11:
				return this.DecodeChksum();
			case 12:
				return false;
			default:
				throw new SharpZipBaseException("Inflater.Decode unknown mode");
			}
			int num2 = this.input.PeekBits(16);
			if (num2 < 0)
			{
				return false;
			}
			this.input.DropBits(16);
			if (num2 != (this.uncomprLen ^ 65535))
			{
				throw new SharpZipBaseException("broken uncompressed block");
			}
			this.mode = 5;
			IL_1A9:
			int num3 = this.outputWindow.CopyStored(this.input, this.uncomprLen);
			this.uncomprLen -= num3;
			if (this.uncomprLen == 0)
			{
				this.mode = 2;
				return true;
			}
			return !this.input.IsNeedingInput;
			IL_22D:
			return this.DecodeHuffman();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000141B9 File Offset: 0x000131B9
		public void SetDictionary(byte[] buffer)
		{
			this.SetDictionary(buffer, 0, buffer.Length);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000141C8 File Offset: 0x000131C8
		public void SetDictionary(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.IsNeedingDictionary)
			{
				throw new InvalidOperationException("Dictionary is not needed");
			}
			this.adler.Update(buffer, index, count);
			if ((int)this.adler.Value != this.readAdler)
			{
				throw new SharpZipBaseException("Wrong adler checksum");
			}
			this.adler.Reset();
			this.outputWindow.CopyDict(buffer, index, count);
			this.mode = 2;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00014261 File Offset: 0x00013261
		public void SetInput(byte[] buffer)
		{
			this.SetInput(buffer, 0, buffer.Length);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001426E File Offset: 0x0001326E
		public void SetInput(byte[] buffer, int index, int count)
		{
			this.input.SetInput(buffer, index, count);
			this.totalIn += (long)count;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001428D File Offset: 0x0001328D
		public int Inflate(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.Inflate(buffer, 0, buffer.Length);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000142A8 File Offset: 0x000132A8
		public int Inflate(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "count cannot be negative");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset cannot be negative");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentException("count exceeds buffer bounds");
			}
			if (count == 0)
			{
				if (!this.IsFinished)
				{
					this.Decode();
				}
				return 0;
			}
			int num = 0;
			for (;;)
			{
				if (this.mode != 11)
				{
					int num2 = this.outputWindow.CopyOutput(buffer, offset, count);
					if (num2 > 0)
					{
						this.adler.Update(buffer, offset, num2);
						offset += num2;
						num += num2;
						this.totalOut += (long)num2;
						count -= num2;
						if (count == 0)
						{
							break;
						}
					}
				}
				if (!this.Decode() && (this.outputWindow.GetAvailable() <= 0 || this.mode == 11))
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00014382 File Offset: 0x00013382
		public bool IsNeedingInput
		{
			get
			{
				return this.input.IsNeedingInput;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0001438F File Offset: 0x0001338F
		public bool IsNeedingDictionary
		{
			get
			{
				return this.mode == 1 && this.neededBits == 0;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000143A5 File Offset: 0x000133A5
		public bool IsFinished
		{
			get
			{
				return this.mode == 12 && this.outputWindow.GetAvailable() == 0;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600036B RID: 875 RVA: 0x000143C1 File Offset: 0x000133C1
		public int Adler
		{
			get
			{
				if (!this.IsNeedingDictionary)
				{
					return (int)this.adler.Value;
				}
				return this.readAdler;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000143DE File Offset: 0x000133DE
		public long TotalOut
		{
			get
			{
				return this.totalOut;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600036D RID: 877 RVA: 0x000143E6 File Offset: 0x000133E6
		public long TotalIn
		{
			get
			{
				return this.totalIn - (long)this.RemainingInput;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000143F6 File Offset: 0x000133F6
		public int RemainingInput
		{
			get
			{
				return this.input.AvailableBytes;
			}
		}

		// Token: 0x0400025F RID: 607
		private const int DECODE_HEADER = 0;

		// Token: 0x04000260 RID: 608
		private const int DECODE_DICT = 1;

		// Token: 0x04000261 RID: 609
		private const int DECODE_BLOCKS = 2;

		// Token: 0x04000262 RID: 610
		private const int DECODE_STORED_LEN1 = 3;

		// Token: 0x04000263 RID: 611
		private const int DECODE_STORED_LEN2 = 4;

		// Token: 0x04000264 RID: 612
		private const int DECODE_STORED = 5;

		// Token: 0x04000265 RID: 613
		private const int DECODE_DYN_HEADER = 6;

		// Token: 0x04000266 RID: 614
		private const int DECODE_HUFFMAN = 7;

		// Token: 0x04000267 RID: 615
		private const int DECODE_HUFFMAN_LENBITS = 8;

		// Token: 0x04000268 RID: 616
		private const int DECODE_HUFFMAN_DIST = 9;

		// Token: 0x04000269 RID: 617
		private const int DECODE_HUFFMAN_DISTBITS = 10;

		// Token: 0x0400026A RID: 618
		private const int DECODE_CHKSUM = 11;

		// Token: 0x0400026B RID: 619
		private const int FINISHED = 12;

		// Token: 0x0400026C RID: 620
		private static readonly int[] CPLENS = new int[]
		{
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			13,
			15,
			17,
			19,
			23,
			27,
			31,
			35,
			43,
			51,
			59,
			67,
			83,
			99,
			115,
			131,
			163,
			195,
			227,
			258
		};

		// Token: 0x0400026D RID: 621
		private static readonly int[] CPLEXT = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			5,
			5,
			5,
			5,
			0
		};

		// Token: 0x0400026E RID: 622
		private static readonly int[] CPDIST = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			7,
			9,
			13,
			17,
			25,
			33,
			49,
			65,
			97,
			129,
			193,
			257,
			385,
			513,
			769,
			1025,
			1537,
			2049,
			3073,
			4097,
			6145,
			8193,
			12289,
			16385,
			24577
		};

		// Token: 0x0400026F RID: 623
		private static readonly int[] CPDEXT = new int[]
		{
			0,
			0,
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			10,
			10,
			11,
			11,
			12,
			12,
			13,
			13
		};

		// Token: 0x04000270 RID: 624
		private int mode;

		// Token: 0x04000271 RID: 625
		private int readAdler;

		// Token: 0x04000272 RID: 626
		private int neededBits;

		// Token: 0x04000273 RID: 627
		private int repLength;

		// Token: 0x04000274 RID: 628
		private int repDist;

		// Token: 0x04000275 RID: 629
		private int uncomprLen;

		// Token: 0x04000276 RID: 630
		private bool isLastBlock;

		// Token: 0x04000277 RID: 631
		private long totalOut;

		// Token: 0x04000278 RID: 632
		private long totalIn;

		// Token: 0x04000279 RID: 633
		private bool noHeader;

		// Token: 0x0400027A RID: 634
		private StreamManipulator input;

		// Token: 0x0400027B RID: 635
		private OutputWindow outputWindow;

		// Token: 0x0400027C RID: 636
		private InflaterDynHeader dynHeader;

		// Token: 0x0400027D RID: 637
		private InflaterHuffmanTree litlenTree;

		// Token: 0x0400027E RID: 638
		private InflaterHuffmanTree distTree;

		// Token: 0x0400027F RID: 639
		private Adler32 adler;
	}
}
