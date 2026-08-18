using System;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace Internal.Cryptography
{
	// Token: 0x02000011 RID: 17
	internal struct CngSymmetricAlgorithmCore
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002DD3 File Offset: 0x00000FD3
		public CngSymmetricAlgorithmCore(ICngSymmetricAlgorithm outer)
		{
			this._outer = outer;
			this._keyName = null;
			this._provider = null;
			this._optionOptions = CngKeyOpenOptions.None;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public CngSymmetricAlgorithmCore(ICngSymmetricAlgorithm outer, string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._outer = outer;
			this._keyName = keyName;
			this._provider = provider;
			this._optionOptions = openOptions;
			using (CngKey cngKey = this.ProduceCngKey())
			{
				CngAlgorithm algorithm = cngKey.Algorithm;
				string ncryptAlgorithmIdentifier = this._outer.GetNCryptAlgorithmIdentifier();
				if (ncryptAlgorithmIdentifier != algorithm.Algorithm)
				{
					throw new CryptographicException(SR.GetString("Cryptography_CngKeyWrongAlgorithm", new object[]
					{
						algorithm.Algorithm,
						ncryptAlgorithmIdentifier
					}));
				}
				this._outer.BaseKeySize = cngKey.KeySize;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002EB8 File Offset: 0x000010B8
		public byte[] GetKeyIfExportable()
		{
			if (this.KeyInPlainText)
			{
				return this._outer.BaseKey;
			}
			byte[] symmetricKeyDataIfExportable;
			using (CngKey cngKey = this.ProduceCngKey())
			{
				symmetricKeyDataIfExportable = cngKey.GetSymmetricKeyDataIfExportable(this._outer.GetNCryptAlgorithmIdentifier());
			}
			return symmetricKeyDataIfExportable;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F10 File Offset: 0x00001110
		public void SetKey(byte[] key)
		{
			this._outer.BaseKey = key;
			this._keyName = null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F25 File Offset: 0x00001125
		public void SetKeySize(int keySize, ICngSymmetricAlgorithm outer)
		{
			outer.BaseKeySize = keySize;
			this._keyName = null;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F38 File Offset: 0x00001138
		public void GenerateKey()
		{
			byte[] key = Helpers.GenerateRandom(this._outer.BaseKeySize.BitSizeToByteSize());
			this.SetKey(key);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F64 File Offset: 0x00001164
		public void GenerateIV()
		{
			byte[] iv = Helpers.GenerateRandom(this._outer.BlockSize.BitSizeToByteSize());
			this._outer.IV = iv;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F93 File Offset: 0x00001193
		public ICryptoTransform CreateEncryptor()
		{
			return this.CreateCryptoTransform(true);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F9C File Offset: 0x0000119C
		public ICryptoTransform CreateDecryptor()
		{
			return this.CreateCryptoTransform(false);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002FA5 File Offset: 0x000011A5
		public ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this.CreateCryptoTransform(rgbKey, rgbIV, true);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FB0 File Offset: 0x000011B0
		public ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this.CreateCryptoTransform(rgbKey, rgbIV, false);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002FBC File Offset: 0x000011BC
		private ICryptoTransform CreateCryptoTransform(bool encrypting)
		{
			if (this.KeyInPlainText)
			{
				return this.CreateCryptoTransform(this._outer.BaseKey, this._outer.IV, encrypting);
			}
			return this.CreatePersistedCryptoTransformCore(new Func<CngKey>(this.ProduceCngKey), this._outer.IV, encrypting);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003018 File Offset: 0x00001218
		private ICryptoTransform CreateCryptoTransform(byte[] rgbKey, byte[] rgbIV, bool encrypting)
		{
			if (rgbKey == null)
			{
				throw new ArgumentNullException("key");
			}
			byte[] key = rgbKey.CloneByteArray();
			long num = (long)key.Length * 8L;
			if (num > 2147483647L || !((int)num).IsLegalSize(this._outer.LegalKeySizes))
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidKeySize", new object[]
				{
					"key"
				}));
			}
			if (this._outer.IsWeakKey(key))
			{
				throw new CryptographicException(SR.GetString("Cryptography_WeakKey"));
			}
			if (rgbIV != null && rgbIV.Length != this._outer.BlockSize.BitSizeToByteSize())
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidIVSize", new object[]
				{
					"iv"
				}));
			}
			byte[] iv = this._outer.Mode.GetCipherIv(rgbIV).CloneByteArray();
			if (LocalAppContextSwitches.SymmetricCngAlwaysUseNCrypt)
			{
				string algorithm = this._outer.GetNCryptAlgorithmIdentifier();
				return this.CreatePersistedCryptoTransformCore(() => key.ToCngKey(algorithm), iv, encrypting);
			}
			return this.CreateEphemeralCryptoTransformCore(key, iv, encrypting);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003138 File Offset: 0x00001338
		private ICryptoTransform CreatePersistedCryptoTransformCore(Func<CngKey> cngKeyFactory, byte[] iv, bool encrypting)
		{
			int blockSizeInBytes = this._outer.BlockSize.BitSizeToByteSize();
			BasicSymmetricCipher cipher = new BasicSymmetricCipherNCrypt(cngKeyFactory, this._outer.Mode, blockSizeInBytes, iv, encrypting);
			return UniversalCryptoTransform.Create(this._outer.Padding, cipher, encrypting);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003180 File Offset: 0x00001380
		[SecuritySafeCritical]
		private ICryptoTransform CreateEphemeralCryptoTransformCore(byte[] key, byte[] iv, bool encrypting)
		{
			int blockSizeInBytes = this._outer.BlockSize.BitSizeToByteSize();
			SafeBCryptAlgorithmHandle ephemeralModeHandle = this._outer.GetEphemeralModeHandle();
			BasicSymmetricCipher cipher = new BasicSymmetricCipherBCrypt(ephemeralModeHandle, this._outer.Mode, blockSizeInBytes, key, iv, encrypting);
			return UniversalCryptoTransform.Create(this._outer.Padding, cipher, encrypting);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000031D2 File Offset: 0x000013D2
		private CngKey ProduceCngKey()
		{
			return CngKey.Open(this._keyName, this._provider, this._optionOptions);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000031EB File Offset: 0x000013EB
		private bool KeyInPlainText
		{
			get
			{
				return this._keyName == null;
			}
		}

		// Token: 0x0400006F RID: 111
		private readonly ICngSymmetricAlgorithm _outer;

		// Token: 0x04000070 RID: 112
		private string _keyName;

		// Token: 0x04000071 RID: 113
		private CngProvider _provider;

		// Token: 0x04000072 RID: 114
		private CngKeyOpenOptions _optionOptions;

		// Token: 0x04000073 RID: 115
		private const int BitsPerByte = 8;
	}
}
