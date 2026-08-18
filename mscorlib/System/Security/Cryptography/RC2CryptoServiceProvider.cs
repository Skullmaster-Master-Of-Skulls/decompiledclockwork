using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000895 RID: 2197
	[ComVisible(true)]
	public sealed class RC2CryptoServiceProvider : RC2
	{
		// Token: 0x06004FEF RID: 20463 RVA: 0x0011610C File Offset: 0x0011510C
		public RC2CryptoServiceProvider()
		{
			if (Utils.FipsAlgorithmPolicy == 1)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			if (!Utils.HasAlgorithm(26114, 0))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_AlgorithmNotAvailable"));
			}
			this.LegalKeySizesValue = RC2CryptoServiceProvider.s_legalKeySizes;
			this.FeedbackSizeValue = 8;
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06004FF0 RID: 20464 RVA: 0x00116166 File Offset: 0x00115166
		// (set) Token: 0x06004FF1 RID: 20465 RVA: 0x0011616E File Offset: 0x0011516E
		public override int EffectiveKeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value != this.KeySizeValue)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_RC2_EKSKS2"));
				}
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06004FF2 RID: 20466 RVA: 0x00116189 File Offset: 0x00115189
		// (set) Token: 0x06004FF3 RID: 20467 RVA: 0x00116191 File Offset: 0x00115191
		[ComVisible(false)]
		public bool UseSalt
		{
			get
			{
				return this.m_use40bitSalt;
			}
			set
			{
				this.m_use40bitSalt = value;
			}
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x0011619A File Offset: 0x0011519A
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.EffectiveKeySizeValue, this.FeedbackSizeValue, CryptoAPITransformMode.Encrypt);
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x001161B7 File Offset: 0x001151B7
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this._NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.EffectiveKeySizeValue, this.FeedbackSizeValue, CryptoAPITransformMode.Decrypt);
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x001161D4 File Offset: 0x001151D4
		public override void GenerateKey()
		{
			this.KeyValue = new byte[this.KeySizeValue / 8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x001161F9 File Offset: 0x001151F9
		public override void GenerateIV()
		{
			this.IVValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x00116218 File Offset: 0x00115218
		private ICryptoTransform _NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int effectiveKeySize, int feedbackSize, CryptoAPITransformMode encryptMode)
		{
			int num = 0;
			int[] array = new int[10];
			object[] array2 = new object[10];
			if (mode == CipherMode.OFB)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_OFBNotSupported"));
			}
			if (mode == CipherMode.CFB && feedbackSize != 8)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_CFBSizeNotSupported"));
			}
			if (rgbKey == null)
			{
				rgbKey = new byte[this.KeySizeValue / 8];
				Utils.StaticRandomNumberGenerator.GetBytes(rgbKey);
			}
			int num2 = rgbKey.Length * 8;
			if (!base.ValidKeySize(num2))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			array[num] = 19;
			if (this.EffectiveKeySizeValue == 0)
			{
				array2[num] = num2;
			}
			else
			{
				array2[num] = effectiveKeySize;
			}
			num++;
			if (mode != CipherMode.CBC)
			{
				array[num] = 4;
				array2[num] = mode;
				num++;
			}
			if (mode != CipherMode.ECB)
			{
				if (rgbIV == null)
				{
					rgbIV = new byte[8];
					Utils.StaticRandomNumberGenerator.GetBytes(rgbIV);
				}
				if (rgbIV.Length < 8)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidIVSize"));
				}
				array[num] = 1;
				array2[num] = rgbIV;
				num++;
			}
			if (mode == CipherMode.OFB || mode == CipherMode.CFB)
			{
				array[num] = 5;
				array2[num] = feedbackSize;
				num++;
			}
			if (!Utils.HasAlgorithm(26114, num2))
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_CSP_AlgKeySizeNotAvailable"), new object[]
				{
					num2
				}));
			}
			return new CryptoAPITransform(26114, num, array, array2, rgbKey, this.PaddingValue, mode, this.BlockSizeValue, feedbackSize, this.m_use40bitSalt, encryptMode);
		}

		// Token: 0x04002928 RID: 10536
		private bool m_use40bitSalt;

		// Token: 0x04002929 RID: 10537
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 128, 8)
		};
	}
}
