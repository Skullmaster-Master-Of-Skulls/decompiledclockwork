using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020008A2 RID: 2210
	[ComVisible(true)]
	public class RSAPKCS1SignatureFormatter : AsymmetricSignatureFormatter
	{
		// Token: 0x06005069 RID: 20585 RVA: 0x00119CF1 File Offset: 0x00118CF1
		public RSAPKCS1SignatureFormatter()
		{
		}

		// Token: 0x0600506A RID: 20586 RVA: 0x00119CF9 File Offset: 0x00118CF9
		public RSAPKCS1SignatureFormatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x00119D1B File Offset: 0x00118D1B
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x0600506C RID: 20588 RVA: 0x00119D37 File Offset: 0x00118D37
		public override void SetHashAlgorithm(string strName)
		{
			this._strOID = CryptoConfig.MapNameToOID(strName);
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x00119D48 File Offset: 0x00118D48
		public override byte[] CreateSignature(byte[] rgbHash)
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
			if (this._rsaKey is RSACryptoServiceProvider)
			{
				return ((RSACryptoServiceProvider)this._rsaKey).SignHash(rgbHash, this._strOID);
			}
			byte[] rgb = Utils.RsaPkcs1Padding(this._rsaKey, CryptoConfig.EncodeOID(this._strOID), rgbHash);
			return this._rsaKey.DecryptValue(rgb);
		}

		// Token: 0x04002957 RID: 10583
		private RSA _rsaKey;

		// Token: 0x04002958 RID: 10584
		private string _strOID;
	}
}
