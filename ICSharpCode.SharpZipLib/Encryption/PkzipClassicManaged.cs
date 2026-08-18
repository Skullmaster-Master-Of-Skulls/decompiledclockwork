using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200006F RID: 111
	public sealed class PkzipClassicManaged : PkzipClassic
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00016F6E File Offset: 0x00015F6E
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00016F71 File Offset: 0x00015F71
		public override int BlockSize
		{
			get
			{
				return 8;
			}
			set
			{
				if (value != 8)
				{
					throw new CryptographicException("Block size is invalid");
				}
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00016F84 File Offset: 0x00015F84
		public override KeySizes[] LegalKeySizes
		{
			get
			{
				return new KeySizes[]
				{
					new KeySizes(96, 96, 0)
				};
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00016FA6 File Offset: 0x00015FA6
		public override void GenerateIV()
		{
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00016FA8 File Offset: 0x00015FA8
		public override KeySizes[] LegalBlockSizes
		{
			get
			{
				return new KeySizes[]
				{
					new KeySizes(8, 8, 0)
				};
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00016FC8 File Offset: 0x00015FC8
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00016FE8 File Offset: 0x00015FE8
		public override byte[] Key
		{
			get
			{
				if (this.key_ == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.key_.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 12)
				{
					throw new CryptographicException("Key size is illegal");
				}
				this.key_ = (byte[])value.Clone();
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001701C File Offset: 0x0001601C
		public override void GenerateKey()
		{
			this.key_ = new byte[12];
			Random random = new Random();
			random.NextBytes(this.key_);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00017048 File Offset: 0x00016048
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			this.key_ = rgbKey;
			return new PkzipClassicEncryptCryptoTransform(this.Key);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001705C File Offset: 0x0001605C
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			this.key_ = rgbKey;
			return new PkzipClassicDecryptCryptoTransform(this.Key);
		}

		// Token: 0x040002E3 RID: 739
		private byte[] key_;
	}
}
