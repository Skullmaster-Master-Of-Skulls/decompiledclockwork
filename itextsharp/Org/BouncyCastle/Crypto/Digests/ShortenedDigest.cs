using System;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x02000023 RID: 35
	public class ShortenedDigest : IDigest
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00007030 File Offset: 0x00006030
		public ShortenedDigest(IDigest baseDigest, int length)
		{
			if (baseDigest == null)
			{
				throw new ArgumentNullException("baseDigest");
			}
			if (length > baseDigest.GetDigestSize())
			{
				throw new ArgumentException("baseDigest output not large enough to support length");
			}
			this.baseDigest = baseDigest;
			this.length = length;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00007068 File Offset: 0x00006068
		public string AlgorithmName
		{
			get
			{
				return string.Concat(new object[]
				{
					this.baseDigest.AlgorithmName,
					"(",
					this.length * 8,
					")"
				});
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000070B0 File Offset: 0x000060B0
		public int GetDigestSize()
		{
			return this.length;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000070B8 File Offset: 0x000060B8
		public void Update(byte input)
		{
			this.baseDigest.Update(input);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000070C6 File Offset: 0x000060C6
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.baseDigest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000070D8 File Offset: 0x000060D8
		public int DoFinal(byte[] output, int outOff)
		{
			byte[] array = new byte[this.baseDigest.GetDigestSize()];
			this.baseDigest.DoFinal(array, 0);
			Array.Copy(array, 0, output, outOff, this.length);
			return this.length;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007119 File Offset: 0x00006119
		public void Reset()
		{
			this.baseDigest.Reset();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007126 File Offset: 0x00006126
		public int GetByteLength()
		{
			return this.baseDigest.GetByteLength();
		}

		// Token: 0x0400006F RID: 111
		private IDigest baseDigest;

		// Token: 0x04000070 RID: 112
		private int length;
	}
}
