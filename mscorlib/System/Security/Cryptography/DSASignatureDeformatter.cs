using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000880 RID: 2176
	[ComVisible(true)]
	public class DSASignatureDeformatter : AsymmetricSignatureDeformatter
	{
		// Token: 0x06004F56 RID: 20310 RVA: 0x00114229 File Offset: 0x00113229
		public DSASignatureDeformatter()
		{
			this._oid = CryptoConfig.MapNameToOID("SHA1");
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x00114241 File Offset: 0x00113241
		public DSASignatureDeformatter(AsymmetricAlgorithm key) : this()
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x00114263 File Offset: 0x00113263
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x0011427F File Offset: 0x0011327F
		public override void SetHashAlgorithm(string strName)
		{
			if (CryptoConfig.MapNameToOID(strName) != this._oid)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_InvalidOperation"));
			}
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x001142A4 File Offset: 0x001132A4
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (this._dsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			return this._dsaKey.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x040028F7 RID: 10487
		private DSA _dsaKey;

		// Token: 0x040028F8 RID: 10488
		private string _oid;
	}
}
