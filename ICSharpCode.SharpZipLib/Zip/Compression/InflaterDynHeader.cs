using System;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000047 RID: 71
	internal class InflaterDynHeader
	{
		// Token: 0x06000323 RID: 803 RVA: 0x000113FC File Offset: 0x000103FC
		public bool Decode(StreamManipulator input)
		{
			for (;;)
			{
				switch (this.mode)
				{
				case 0:
					this.lnum = input.PeekBits(5);
					if (this.lnum < 0)
					{
						return false;
					}
					this.lnum += 257;
					input.DropBits(5);
					this.mode = 1;
					goto IL_61;
				case 1:
					goto IL_61;
				case 2:
					goto IL_B9;
				case 3:
					break;
				case 4:
					goto IL_1A8;
				case 5:
					goto IL_1EE;
				default:
					continue;
				}
				IL_13B:
				while (this.ptr < this.blnum)
				{
					int num = input.PeekBits(3);
					if (num < 0)
					{
						return false;
					}
					input.DropBits(3);
					this.blLens[InflaterDynHeader.BL_ORDER[this.ptr]] = (byte)num;
					this.ptr++;
				}
				this.blTree = new InflaterHuffmanTree(this.blLens);
				this.blLens = null;
				this.ptr = 0;
				this.mode = 4;
				IL_1A8:
				int symbol;
				while (((symbol = this.blTree.GetSymbol(input)) & -16) == 0)
				{
					this.litdistLens[this.ptr++] = (this.lastLen = (byte)symbol);
					if (this.ptr == this.num)
					{
						return true;
					}
				}
				if (symbol < 0)
				{
					return false;
				}
				if (symbol >= 17)
				{
					this.lastLen = 0;
				}
				else if (this.ptr == 0)
				{
					goto Block_10;
				}
				this.repSymbol = symbol - 16;
				this.mode = 5;
				IL_1EE:
				int bitCount = InflaterDynHeader.repBits[this.repSymbol];
				int num2 = input.PeekBits(bitCount);
				if (num2 < 0)
				{
					return false;
				}
				input.DropBits(bitCount);
				num2 += InflaterDynHeader.repMin[this.repSymbol];
				if (this.ptr + num2 > this.num)
				{
					goto Block_12;
				}
				while (num2-- > 0)
				{
					this.litdistLens[this.ptr++] = this.lastLen;
				}
				if (this.ptr == this.num)
				{
					return true;
				}
				this.mode = 4;
				continue;
				IL_B9:
				this.blnum = input.PeekBits(4);
				if (this.blnum < 0)
				{
					return false;
				}
				this.blnum += 4;
				input.DropBits(4);
				this.blLens = new byte[19];
				this.ptr = 0;
				this.mode = 3;
				goto IL_13B;
				IL_61:
				this.dnum = input.PeekBits(5);
				if (this.dnum < 0)
				{
					return false;
				}
				this.dnum++;
				input.DropBits(5);
				this.num = this.lnum + this.dnum;
				this.litdistLens = new byte[this.num];
				this.mode = 2;
				goto IL_B9;
			}
			return false;
			Block_10:
			throw new SharpZipBaseException();
			Block_12:
			throw new SharpZipBaseException();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00011684 File Offset: 0x00010684
		public InflaterHuffmanTree BuildLitLenTree()
		{
			byte[] array = new byte[this.lnum];
			Array.Copy(this.litdistLens, 0, array, 0, this.lnum);
			return new InflaterHuffmanTree(array);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000116B8 File Offset: 0x000106B8
		public InflaterHuffmanTree BuildDistTree()
		{
			byte[] array = new byte[this.dnum];
			Array.Copy(this.litdistLens, this.lnum, array, 0, this.dnum);
			return new InflaterHuffmanTree(array);
		}

		// Token: 0x040001F2 RID: 498
		private const int LNUM = 0;

		// Token: 0x040001F3 RID: 499
		private const int DNUM = 1;

		// Token: 0x040001F4 RID: 500
		private const int BLNUM = 2;

		// Token: 0x040001F5 RID: 501
		private const int BLLENS = 3;

		// Token: 0x040001F6 RID: 502
		private const int LENS = 4;

		// Token: 0x040001F7 RID: 503
		private const int REPS = 5;

		// Token: 0x040001F8 RID: 504
		private static readonly int[] repMin = new int[]
		{
			3,
			3,
			11
		};

		// Token: 0x040001F9 RID: 505
		private static readonly int[] repBits = new int[]
		{
			2,
			3,
			7
		};

		// Token: 0x040001FA RID: 506
		private static readonly int[] BL_ORDER = new int[]
		{
			16,
			17,
			18,
			0,
			8,
			7,
			9,
			6,
			10,
			5,
			11,
			4,
			12,
			3,
			13,
			2,
			14,
			1,
			15
		};

		// Token: 0x040001FB RID: 507
		private byte[] blLens;

		// Token: 0x040001FC RID: 508
		private byte[] litdistLens;

		// Token: 0x040001FD RID: 509
		private InflaterHuffmanTree blTree;

		// Token: 0x040001FE RID: 510
		private int mode;

		// Token: 0x040001FF RID: 511
		private int lnum;

		// Token: 0x04000200 RID: 512
		private int dnum;

		// Token: 0x04000201 RID: 513
		private int blnum;

		// Token: 0x04000202 RID: 514
		private int num;

		// Token: 0x04000203 RID: 515
		private int repSymbol;

		// Token: 0x04000204 RID: 516
		private byte lastLen;

		// Token: 0x04000205 RID: 517
		private int ptr;
	}
}
