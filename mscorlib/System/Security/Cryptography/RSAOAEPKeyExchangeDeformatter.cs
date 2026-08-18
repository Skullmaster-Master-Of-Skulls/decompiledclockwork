using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200089D RID: 2205
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		// Token: 0x06005044 RID: 20548 RVA: 0x001197FE File Offset: 0x001187FE
		public RSAOAEPKeyExchangeDeformatter()
		{
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x00119806 File Offset: 0x00118806
		public RSAOAEPKeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06005046 RID: 20550 RVA: 0x00119828 File Offset: 0x00118828
		// (set) Token: 0x06005047 RID: 20551 RVA: 0x0011982B File Offset: 0x0011882B
		public override string Parameters
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x00119830 File Offset: 0x00118830
		public override byte[] DecryptKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			if (this._rsaKey is RSACryptoServiceProvider)
			{
				return ((RSACryptoServiceProvider)this._rsaKey).Decrypt(rgbData, true);
			}
			return Utils.RsaOaepDecrypt(this._rsaKey, SHA1.Create(), new PKCS1MaskGenerationMethod(), rgbData);
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x0011988B File Offset: 0x0011888B
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x0400294D RID: 10573
		private RSA _rsaKey;
	}
}
