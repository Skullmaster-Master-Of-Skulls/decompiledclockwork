using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	// Token: 0x020008A1 RID: 2209
	[ComVisible(true)]
	public class RSAPKCS1SignatureDeformatter : AsymmetricSignatureDeformatter
	{
		// Token: 0x06005064 RID: 20580 RVA: 0x00119BEA File Offset: 0x00118BEA
		public RSAPKCS1SignatureDeformatter()
		{
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x00119BF2 File Offset: 0x00118BF2
		public RSAPKCS1SignatureDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x00119C14 File Offset: 0x00118C14
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x00119C30 File Offset: 0x00118C30
		public override void SetHashAlgorithm(string strName)
		{
			this._strOID = CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm);
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x00119C40 File Offset: 0x00118C40
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (this._strOID == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingOID"));
			}
			if (this._rsaKey == null)
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
			if (this._rsaKey is RSACryptoServiceProvider)
			{
				int calgHash = X509Utils.OidToAlgIdStrict(this._strOID, OidGroup.HashAlgorithm);
				return ((RSACryptoServiceProvider)this._rsaKey).VerifyHash(rgbHash, calgHash, rgbSignature);
			}
			byte[] rhs = Utils.RsaPkcs1Padding(this._rsaKey, CryptoConfig.EncodeOID(this._strOID), rgbHash);
			return Utils.CompareBigIntArrays(this._rsaKey.EncryptValue(rgbSignature), rhs);
		}

		// Token: 0x04002955 RID: 10581
		private RSA _rsaKey;

		// Token: 0x04002956 RID: 10582
		private string _strOID;
	}
}
