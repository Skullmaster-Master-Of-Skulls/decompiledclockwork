using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000347 RID: 839
	public class KdfParameters : IDerivationParameters
	{
		// Token: 0x06001E48 RID: 7752 RVA: 0x000B57A2 File Offset: 0x000B47A2
		public KdfParameters(byte[] shared, byte[] iv)
		{
			this.shared = shared;
			this.iv = iv;
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000B57B8 File Offset: 0x000B47B8
		public byte[] GetSharedSecret()
		{
			return this.shared;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000B57C0 File Offset: 0x000B47C0
		public byte[] GetIV()
		{
			return this.iv;
		}

		// Token: 0x04001503 RID: 5379
		private byte[] iv;

		// Token: 0x04001504 RID: 5380
		private byte[] shared;
	}
}
