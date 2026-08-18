using System;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000024 RID: 36
	public abstract class GeneralDigest : IDigest
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00007133 File Offset: 0x00006133
		internal GeneralDigest()
		{
			this.xBuf = new byte[4];
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007148 File Offset: 0x00006148
		internal GeneralDigest(GeneralDigest t)
		{
			this.xBuf = new byte[t.xBuf.Length];
			Array.Copy(t.xBuf, 0, this.xBuf, 0, t.xBuf.Length);
			this.xBufOff = t.xBufOff;
			this.byteCount = t.byteCount;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000071A4 File Offset: 0x000061A4
		public void Update(byte input)
		{
			this.xBuf[this.xBufOff++] = input;
			if (this.xBufOff == this.xBuf.Length)
			{
				this.ProcessWord(this.xBuf, 0);
				this.xBufOff = 0;
			}
			this.byteCount += 1L;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007200 File Offset: 0x00006200
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			while (this.xBufOff != 0)
			{
				if (length <= 0)
				{
					break;
				}
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
			while (length > this.xBuf.Length)
			{
				this.ProcessWord(input, inOff);
				inOff += this.xBuf.Length;
				length -= this.xBuf.Length;
				this.byteCount += (long)this.xBuf.Length;
			}
			while (length > 0)
			{
				this.Update(input[inOff]);
				inOff++;
				length--;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000728C File Offset: 0x0000628C
		public void Finish()
		{
			long bitLength = this.byteCount << 3;
			this.Update(128);
			while (this.xBufOff != 0)
			{
				this.Update(0);
			}
			this.ProcessLength(bitLength);
			this.ProcessBlock();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000072CB File Offset: 0x000062CB
		public virtual void Reset()
		{
			this.byteCount = 0L;
			this.xBufOff = 0;
			Array.Clear(this.xBuf, 0, this.xBuf.Length);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000072F0 File Offset: 0x000062F0
		public int GetByteLength()
		{
			return 64;
		}

		// Token: 0x060000EB RID: 235
		internal abstract void ProcessWord(byte[] input, int inOff);

		// Token: 0x060000EC RID: 236
		internal abstract void ProcessLength(long bitLength);

		// Token: 0x060000ED RID: 237
		internal abstract void ProcessBlock();

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000EE RID: 238
		public abstract string AlgorithmName { get; }

		// Token: 0x060000EF RID: 239
		public abstract int GetDigestSize();

		// Token: 0x060000F0 RID: 240
		public abstract int DoFinal(byte[] output, int outOff);

		// Token: 0x04000071 RID: 113
		private const int BYTE_LENGTH = 64;

		// Token: 0x04000072 RID: 114
		private byte[] xBuf;

		// Token: 0x04000073 RID: 115
		private int xBufOff;

		// Token: 0x04000074 RID: 116
		private long byteCount;
	}
}
