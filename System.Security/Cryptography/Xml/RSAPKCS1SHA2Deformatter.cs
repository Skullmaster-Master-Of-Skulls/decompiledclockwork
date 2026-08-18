using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000CE RID: 206
	internal class RSAPKCS1SHA2Deformatter : AsymmetricSignatureDeformatter
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x00019A34 File Offset: 0x00018A34
		public override void SetKey(AsymmetricAlgorithm key)
		{
			this._key = (RSA)key;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00019A42 File Offset: 0x00018A42
		public override void SetHashAlgorithm(string strName)
		{
			this._hashAlgorithm = strName;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00019A4C File Offset: 0x00018A4C
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = this._key as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null && rsacryptoServiceProvider.CspKeyContainerInfo.ProviderType != 24)
			{
				RSAParameters parameters = this._key.ExportParameters(false);
				using (RSACryptoServiceProvider rsacryptoServiceProvider2 = new RSACryptoServiceProvider())
				{
					rsacryptoServiceProvider2.ImportParameters(parameters);
					return rsacryptoServiceProvider2.VerifyHash(rgbHash, this._hashAlgorithm, rgbSignature);
				}
			}
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = new RSAPKCS1SignatureDeformatter(this._key);
			asymmetricSignatureDeformatter.SetHashAlgorithm(this._hashAlgorithm);
			return asymmetricSignatureDeformatter.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x040005DB RID: 1499
		private RSA _key;

		// Token: 0x040005DC RID: 1500
		private string _hashAlgorithm;
	}
}
