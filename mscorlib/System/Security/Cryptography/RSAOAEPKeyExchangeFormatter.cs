using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200089E RID: 2206
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		// Token: 0x0600504A RID: 20554 RVA: 0x001198A7 File Offset: 0x001188A7
		public RSAOAEPKeyExchangeFormatter()
		{
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x001198AF File Offset: 0x001188AF
		public RSAOAEPKeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x0600504C RID: 20556 RVA: 0x001198D1 File Offset: 0x001188D1
		// (set) Token: 0x0600504D RID: 20557 RVA: 0x001198ED File Offset: 0x001188ED
		public byte[] Parameter
		{
			get
			{
				if (this.ParameterValue != null)
				{
					return (byte[])this.ParameterValue.Clone();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.ParameterValue = (byte[])value.Clone();
					return;
				}
				this.ParameterValue = null;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x0600504E RID: 20558 RVA: 0x0011990B File Offset: 0x0011890B
		public override string Parameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x0600504F RID: 20559 RVA: 0x0011990E File Offset: 0x0011890E
		// (set) Token: 0x06005050 RID: 20560 RVA: 0x00119916 File Offset: 0x00118916
		public RandomNumberGenerator Rng
		{
			get
			{
				return this.RngValue;
			}
			set
			{
				this.RngValue = value;
			}
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x0011991F File Offset: 0x0011891F
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x0011993C File Offset: 0x0011893C
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			if (this._rsaKey is RSACryptoServiceProvider)
			{
				return ((RSACryptoServiceProvider)this._rsaKey).Encrypt(rgbData, true);
			}
			return Utils.RsaOaepEncrypt(this._rsaKey, SHA1.Create(), new PKCS1MaskGenerationMethod(), RandomNumberGenerator.Create(), rgbData);
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x0011999C File Offset: 0x0011899C
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		// Token: 0x0400294E RID: 10574
		private byte[] ParameterValue;

		// Token: 0x0400294F RID: 10575
		private RSA _rsaKey;

		// Token: 0x04002950 RID: 10576
		private RandomNumberGenerator RngValue;
	}
}
