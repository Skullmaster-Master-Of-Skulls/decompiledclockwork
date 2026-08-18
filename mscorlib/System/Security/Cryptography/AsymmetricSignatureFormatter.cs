using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200086C RID: 2156
	[ComVisible(true)]
	public abstract class AsymmetricSignatureFormatter
	{
		// Token: 0x06004EA4 RID: 20132
		public abstract void SetKey(AsymmetricAlgorithm key);

		// Token: 0x06004EA5 RID: 20133
		public abstract void SetHashAlgorithm(string strName);

		// Token: 0x06004EA6 RID: 20134 RVA: 0x001101F2 File Offset: 0x0010F1F2
		public virtual byte[] CreateSignature(HashAlgorithm hash)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			this.SetHashAlgorithm(hash.ToString());
			return this.CreateSignature(hash.Hash);
		}

		// Token: 0x06004EA7 RID: 20135
		public abstract byte[] CreateSignature(byte[] rgbHash);
	}
}
