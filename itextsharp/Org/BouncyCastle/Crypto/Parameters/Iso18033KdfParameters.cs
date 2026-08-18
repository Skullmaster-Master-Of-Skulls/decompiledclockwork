using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020002F0 RID: 752
	public class Iso18033KdfParameters : IDerivationParameters
	{
		// Token: 0x06001BB4 RID: 7092 RVA: 0x000A5F4A File Offset: 0x000A4F4A
		public Iso18033KdfParameters(byte[] seed)
		{
			this.seed = seed;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000A5F59 File Offset: 0x000A4F59
		public byte[] GetSeed()
		{
			return this.seed;
		}

		// Token: 0x04001302 RID: 4866
		private byte[] seed;
	}
}
