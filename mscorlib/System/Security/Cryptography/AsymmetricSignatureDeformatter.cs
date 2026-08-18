using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200086B RID: 2155
	[ComVisible(true)]
	public abstract class AsymmetricSignatureDeformatter
	{
		// Token: 0x06004E9F RID: 20127
		public abstract void SetKey(AsymmetricAlgorithm key);

		// Token: 0x06004EA0 RID: 20128
		public abstract void SetHashAlgorithm(string strName);

		// Token: 0x06004EA1 RID: 20129 RVA: 0x001101C1 File Offset: 0x0010F1C1
		public virtual bool VerifySignature(HashAlgorithm hash, byte[] rgbSignature)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			this.SetHashAlgorithm(hash.ToString());
			return this.VerifySignature(hash.Hash, rgbSignature);
		}

		// Token: 0x06004EA2 RID: 20130
		public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);
	}
}
