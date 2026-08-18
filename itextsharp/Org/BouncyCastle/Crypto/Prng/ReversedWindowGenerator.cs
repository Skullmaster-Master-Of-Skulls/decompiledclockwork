using System;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000012 RID: 18
	public class ReversedWindowGenerator : IRandomGenerator
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000056A5 File Offset: 0x000046A5
		public ReversedWindowGenerator(IRandomGenerator generator, int windowSize)
		{
			if (generator == null)
			{
				throw new ArgumentNullException("generator");
			}
			if (windowSize < 2)
			{
				throw new ArgumentException("Window size must be at least 2", "windowSize");
			}
			this.generator = generator;
			this.window = new byte[windowSize];
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000056E4 File Offset: 0x000046E4
		public virtual void AddSeedMaterial(byte[] seed)
		{
			lock (this)
			{
				this.windowCount = 0;
				this.generator.AddSeedMaterial(seed);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005728 File Offset: 0x00004728
		public virtual void AddSeedMaterial(long seed)
		{
			lock (this)
			{
				this.windowCount = 0;
				this.generator.AddSeedMaterial(seed);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000576C File Offset: 0x0000476C
		public virtual void NextBytes(byte[] bytes)
		{
			this.doNextBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005779 File Offset: 0x00004779
		public virtual void NextBytes(byte[] bytes, int start, int len)
		{
			this.doNextBytes(bytes, start, len);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00005784 File Offset: 0x00004784
		private void doNextBytes(byte[] bytes, int start, int len)
		{
			lock (this)
			{
				int i = 0;
				while (i < len)
				{
					if (this.windowCount < 1)
					{
						this.generator.NextBytes(this.window, 0, this.window.Length);
						this.windowCount = this.window.Length;
					}
					bytes[start + i++] = this.window[--this.windowCount];
				}
			}
		}

		// Token: 0x04000040 RID: 64
		private readonly IRandomGenerator generator;

		// Token: 0x04000041 RID: 65
		private byte[] window;

		// Token: 0x04000042 RID: 66
		private int windowCount;
	}
}
