using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000050 RID: 80
	public class Deflater
	{
		// Token: 0x06000370 RID: 880 RVA: 0x00014651 File Offset: 0x00013651
		public Deflater() : this(-1, false)
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001465B File Offset: 0x0001365B
		public Deflater(int level) : this(level, false)
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00014668 File Offset: 0x00013668
		public Deflater(int level, bool noZlibHeaderOrFooter)
		{
			if (level == -1)
			{
				level = 6;
			}
			else if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.pending = new DeflaterPending();
			this.engine = new DeflaterEngine(this.pending);
			this.noZlibHeaderOrFooter = noZlibHeaderOrFooter;
			this.SetStrategy(DeflateStrategy.Default);
			this.SetLevel(level);
			this.Reset();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000146CF File Offset: 0x000136CF
		public void Reset()
		{
			this.state = (this.noZlibHeaderOrFooter ? 16 : 0);
			this.totalOut = 0L;
			this.pending.Reset();
			this.engine.Reset();
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00014702 File Offset: 0x00013702
		public int Adler
		{
			get
			{
				return this.engine.Adler;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0001470F File Offset: 0x0001370F
		public long TotalIn
		{
			get
			{
				return this.engine.TotalIn;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0001471C File Offset: 0x0001371C
		public long TotalOut
		{
			get
			{
				return this.totalOut;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00014724 File Offset: 0x00013724
		public void Flush()
		{
			this.state |= 4;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00014734 File Offset: 0x00013734
		public void Finish()
		{
			this.state |= 12;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00014745 File Offset: 0x00013745
		public bool IsFinished
		{
			get
			{
				return this.state == 30 && this.pending.IsFlushed;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0001475E File Offset: 0x0001375E
		public bool IsNeedingInput
		{
			get
			{
				return this.engine.NeedsInput();
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001476B File Offset: 0x0001376B
		public void SetInput(byte[] input)
		{
			this.SetInput(input, 0, input.Length);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00014778 File Offset: 0x00013778
		public void SetInput(byte[] input, int offset, int count)
		{
			if ((this.state & 8) != 0)
			{
				throw new InvalidOperationException("Finish() already called");
			}
			this.engine.SetInput(input, offset, count);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001479D File Offset: 0x0001379D
		public void SetLevel(int level)
		{
			if (level == -1)
			{
				level = 6;
			}
			else if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			if (this.level != level)
			{
				this.level = level;
				this.engine.SetLevel(level);
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000147D8 File Offset: 0x000137D8
		public int GetLevel()
		{
			return this.level;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000147E0 File Offset: 0x000137E0
		public void SetStrategy(DeflateStrategy strategy)
		{
			this.engine.Strategy = strategy;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000147EE File Offset: 0x000137EE
		public int Deflate(byte[] output)
		{
			return this.Deflate(output, 0, output.Length);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000147FC File Offset: 0x000137FC
		public int Deflate(byte[] output, int offset, int length)
		{
			int num = length;
			if (this.state == 127)
			{
				throw new InvalidOperationException("Deflater closed");
			}
			if (this.state < 16)
			{
				int num2 = 30720;
				int num3 = this.level - 1 >> 1;
				if (num3 < 0 || num3 > 3)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				if ((this.state & 1) != 0)
				{
					num2 |= 32;
				}
				num2 += 31 - num2 % 31;
				this.pending.WriteShortMSB(num2);
				if ((this.state & 1) != 0)
				{
					int adler = this.engine.Adler;
					this.engine.ResetAdler();
					this.pending.WriteShortMSB(adler >> 16);
					this.pending.WriteShortMSB(adler & 65535);
				}
				this.state = (16 | (this.state & 12));
			}
			for (;;)
			{
				int num4 = this.pending.Flush(output, offset, length);
				offset += num4;
				this.totalOut += (long)num4;
				length -= num4;
				if (length == 0 || this.state == 30)
				{
					goto IL_1DE;
				}
				if (!this.engine.Deflate((this.state & 4) != 0, (this.state & 8) != 0))
				{
					if (this.state == 16)
					{
						break;
					}
					if (this.state == 20)
					{
						if (this.level != 0)
						{
							for (int i = 8 + (-this.pending.BitCount & 7); i > 0; i -= 10)
							{
								this.pending.WriteBits(2, 10);
							}
						}
						this.state = 16;
					}
					else if (this.state == 28)
					{
						this.pending.AlignToByte();
						if (!this.noZlibHeaderOrFooter)
						{
							int adler2 = this.engine.Adler;
							this.pending.WriteShortMSB(adler2 >> 16);
							this.pending.WriteShortMSB(adler2 & 65535);
						}
						this.state = 30;
					}
				}
			}
			return num - length;
			IL_1DE:
			return num - length;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000149EA File Offset: 0x000139EA
		public void SetDictionary(byte[] dictionary)
		{
			this.SetDictionary(dictionary, 0, dictionary.Length);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000149F7 File Offset: 0x000139F7
		public void SetDictionary(byte[] dictionary, int index, int count)
		{
			if (this.state != 0)
			{
				throw new InvalidOperationException();
			}
			this.state = 1;
			this.engine.SetDictionary(dictionary, index, count);
		}

		// Token: 0x04000280 RID: 640
		public const int BEST_COMPRESSION = 9;

		// Token: 0x04000281 RID: 641
		public const int BEST_SPEED = 1;

		// Token: 0x04000282 RID: 642
		public const int DEFAULT_COMPRESSION = -1;

		// Token: 0x04000283 RID: 643
		public const int NO_COMPRESSION = 0;

		// Token: 0x04000284 RID: 644
		public const int DEFLATED = 8;

		// Token: 0x04000285 RID: 645
		private const int IS_SETDICT = 1;

		// Token: 0x04000286 RID: 646
		private const int IS_FLUSHING = 4;

		// Token: 0x04000287 RID: 647
		private const int IS_FINISHING = 8;

		// Token: 0x04000288 RID: 648
		private const int INIT_STATE = 0;

		// Token: 0x04000289 RID: 649
		private const int SETDICT_STATE = 1;

		// Token: 0x0400028A RID: 650
		private const int BUSY_STATE = 16;

		// Token: 0x0400028B RID: 651
		private const int FLUSHING_STATE = 20;

		// Token: 0x0400028C RID: 652
		private const int FINISHING_STATE = 28;

		// Token: 0x0400028D RID: 653
		private const int FINISHED_STATE = 30;

		// Token: 0x0400028E RID: 654
		private const int CLOSED_STATE = 127;

		// Token: 0x0400028F RID: 655
		private int level;

		// Token: 0x04000290 RID: 656
		private bool noZlibHeaderOrFooter;

		// Token: 0x04000291 RID: 657
		private int state;

		// Token: 0x04000292 RID: 658
		private long totalOut;

		// Token: 0x04000293 RID: 659
		private DeflaterPending pending;

		// Token: 0x04000294 RID: 660
		private DeflaterEngine engine;
	}
}
