using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200011F RID: 287
	public class IesWithCipherParameters : IesParameters
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x00037F7D File Offset: 0x00036F7D
		public IesWithCipherParameters(byte[] derivation, byte[] encoding, int macKeySize, int cipherKeySize) : base(derivation, encoding, macKeySize)
		{
			this.cipherKeySize = cipherKeySize;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00037F90 File Offset: 0x00036F90
		public int CipherKeySize
		{
			get
			{
				return this.cipherKeySize;
			}
		}

		// Token: 0x0400087D RID: 2173
		private int cipherKeySize;
	}
}
