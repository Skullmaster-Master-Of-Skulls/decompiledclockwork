using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000878 RID: 2168
	[ComVisible(true)]
	public abstract class DES : SymmetricAlgorithm
	{
		// Token: 0x06004F17 RID: 20247 RVA: 0x001130DB File Offset: 0x001120DB
		protected DES()
		{
			this.KeySizeValue = 64;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = DES.s_legalBlockSizes;
			this.LegalKeySizesValue = DES.s_legalKeySizes;
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06004F18 RID: 20248 RVA: 0x00113115 File Offset: 0x00112115
		// (set) Token: 0x06004F19 RID: 20249 RVA: 0x00113150 File Offset: 0x00112150
		public override byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					do
					{
						this.GenerateKey();
					}
					while (DES.IsWeakKey(this.KeyValue) || DES.IsSemiWeakKey(this.KeyValue));
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!base.ValidKeySize(value.Length * 8))
				{
					throw new ArgumentException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
				}
				if (DES.IsWeakKey(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "DES");
				}
				if (DES.IsSemiWeakKey(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_SemiWeak"), "DES");
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x001131DE File Offset: 0x001121DE
		public new static DES Create()
		{
			return DES.Create("System.Security.Cryptography.DES");
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x001131EA File Offset: 0x001121EA
		public new static DES Create(string algName)
		{
			return (DES)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x001131F8 File Offset: 0x001121F8
		public static bool IsWeakKey(byte[] rgbKey)
		{
			if (!DES.IsLegalKeySize(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			byte[] block = Utils.FixupKeyParity(rgbKey);
			ulong num = DES.QuadWordFromBigEndian(block);
			return num == 72340172838076673UL || num == 18374403900871474942UL || num == 2242545357694045710UL || num == 16204198716015505905UL;
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x00113260 File Offset: 0x00112260
		public static bool IsSemiWeakKey(byte[] rgbKey)
		{
			if (!DES.IsLegalKeySize(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			byte[] block = Utils.FixupKeyParity(rgbKey);
			ulong num = DES.QuadWordFromBigEndian(block);
			return num == 143554428589179390UL || num == 18303189645120372225UL || num == 2296870857142767345UL || num == 16149873216566784270UL || num == 135110050437988849UL || num == 16141428838415593729UL || num == 2305315235293957886UL || num == 18311634023271562766UL || num == 80784550989267214UL || num == 2234100979542855169UL || num == 16212643094166696446UL || num == 18365959522720284401UL;
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x00113329 File Offset: 0x00112329
		private static bool IsLegalKeySize(byte[] rgbKey)
		{
			return rgbKey != null && rgbKey.Length == 8;
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x00113338 File Offset: 0x00112338
		private static ulong QuadWordFromBigEndian(byte[] block)
		{
			return (ulong)block[0] << 56 | (ulong)block[1] << 48 | (ulong)block[2] << 40 | (ulong)block[3] << 32 | (ulong)block[4] << 24 | (ulong)block[5] << 16 | (ulong)block[6] << 8 | (ulong)block[7];
		}

		// Token: 0x040028DE RID: 10462
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x040028DF RID: 10463
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};
	}
}
