using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000881 RID: 2177
	[ComVisible(true)]
	public class DSASignatureFormatter : AsymmetricSignatureFormatter
	{
		// Token: 0x06004F5B RID: 20315 RVA: 0x001142F2 File Offset: 0x001132F2
		public DSASignatureFormatter()
		{
			this._oid = CryptoConfig.MapNameToOID("SHA1");
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x0011430A File Offset: 0x0011330A
		public DSASignatureFormatter(AsymmetricAlgorithm key) : this()
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x0011432C File Offset: 0x0011332C
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x00114348 File Offset: 0x00113348
		public override void SetHashAlgorithm(string strName)
		{
			if (CryptoConfig.MapNameToOID(strName) != this._oid)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_InvalidOperation"));
			}
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x00114370 File Offset: 0x00113370
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			if (this._oid == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingOID"));
			}
			if (this._dsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			return this._dsaKey.CreateSignature(rgbHash);
		}

		// Token: 0x040028F9 RID: 10489
		private DSA _dsaKey;

		// Token: 0x040028FA RID: 10490
		private string _oid;
	}
}
