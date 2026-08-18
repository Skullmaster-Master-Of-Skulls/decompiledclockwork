using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020002EF RID: 751
	public class MgfParameters : IDerivationParameters
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x000A5F08 File Offset: 0x000A4F08
		public MgfParameters(byte[] seed) : this(seed, 0, seed.Length)
		{
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x000A5F15 File Offset: 0x000A4F15
		public MgfParameters(byte[] seed, int off, int len)
		{
			this.seed = new byte[len];
			Array.Copy(seed, off, this.seed, 0, len);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000A5F38 File Offset: 0x000A4F38
		public byte[] GetSeed()
		{
			return (byte[])this.seed.Clone();
		}

		// Token: 0x04001301 RID: 4865
		private readonly byte[] seed;
	}
}
