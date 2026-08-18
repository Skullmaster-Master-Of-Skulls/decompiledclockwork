using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200089F RID: 2207
	[ComVisible(true)]
	public class RSAPKCS1KeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		// Token: 0x06005054 RID: 20564 RVA: 0x001199A5 File Offset: 0x001189A5
		public RSAPKCS1KeyExchangeDeformatter()
		{
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x001199AD File Offset: 0x001189AD
		public RSAPKCS1KeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06005056 RID: 20566 RVA: 0x001199CF File Offset: 0x001189CF
		// (set) Token: 0x06005057 RID: 20567 RVA: 0x001199D7 File Offset: 0x001189D7
		public RandomNumberGenerator RNG
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

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06005058 RID: 20568 RVA: 0x001199E0 File Offset: 0x001189E0
		// (set) Token: 0x06005059 RID: 20569 RVA: 0x001199E3 File Offset: 0x001189E3
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

		// Token: 0x0600505A RID: 20570 RVA: 0x001199E8 File Offset: 0x001189E8
		public override byte[] DecryptKeyExchange(byte[] rgbIn)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			byte[] array;
			if (this._rsaKey is RSACryptoServiceProvider)
			{
				array = ((RSACryptoServiceProvider)this._rsaKey).Decrypt(rgbIn, false);
			}
			else
			{
				byte[] array2 = this._rsaKey.DecryptValue(rgbIn);
				int num = 2;
				while (num < array2.Length && array2[num] != 0)
				{
					num++;
				}
				if (num >= array2.Length)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_PKCS1Decoding"));
				}
				num++;
				array = new byte[array2.Length - num];
				Buffer.InternalBlockCopy(array2, num, array, 0, array.Length);
			}
			return array;
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x00119A81 File Offset: 0x00118A81
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x04002951 RID: 10577
		private RSA _rsaKey;

		// Token: 0x04002952 RID: 10578
		private RandomNumberGenerator RngValue;
	}
}
