using System;
using System.IO;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000F6 RID: 246
	public sealed class DSACng : DSA
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x00019879 File Offset: 0x00017A79
		public DSACng() : this(2048)
		{
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00019886 File Offset: 0x00017A86
		public DSACng(int keySize)
		{
			this.LegalKeySizesValue = DSACng.s_legalKeySizes;
			this.KeySize = keySize;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000198A0 File Offset: 0x00017AA0
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public DSACng(CngKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.AlgorithmGroup != CngAlgorithmGroup.Dsa)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgDSARequiresDSAKey"), "key");
			}
			this.LegalKeySizesValue = DSACng.s_legalKeySizes;
			CngKey key2 = CngKey.Open(key.Handle, key.IsEphemeral ? CngKeyHandleOpenOptions.EphemeralKey : CngKeyHandleOpenOptions.None);
			this.Key = key2;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00019914 File Offset: 0x00017B14
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x000199A4 File Offset: 0x00017BA4
		public CngKey Key
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			get
			{
				if (this._key != null && this._key.KeySize != this.KeySize)
				{
					this._key.Dispose();
					this._key = null;
				}
				if (this._key == null)
				{
					CngKeyCreationParameters cngKeyCreationParameters = new CngKeyCreationParameters
					{
						ExportPolicy = new CngExportPolicies?(CngExportPolicies.AllowPlaintextExport)
					};
					CngProperty item = new CngProperty("Length", BitConverter.GetBytes(this.KeySize), CngPropertyOptions.None);
					cngKeyCreationParameters.Parameters.Add(item);
					this._key = CngKey.Create(DSACng.s_cngAlgorithmDsa, null, cngKeyCreationParameters);
				}
				return this._key;
			}
			private set
			{
				if (value.AlgorithmGroup != CngAlgorithmGroup.Dsa)
				{
					throw new ArgumentException(SR.GetString("Cryptography_ArgDSARequiresDSAKey"), "value");
				}
				if (this._key != null)
				{
					this._key.Dispose();
				}
				this._key = value;
				this.KeySizeValue = value.KeySize;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x000199FE File Offset: 0x00017BFE
		private SafeNCryptKeyHandle KeyHandle
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			get
			{
				return this.Key.Handle;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00019A0B File Offset: 0x00017C0B
		public override KeySizes[] LegalKeySizes
		{
			get
			{
				return base.LegalKeySizes;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00019A13 File Offset: 0x00017C13
		public override string SignatureAlgorithm
		{
			get
			{
				return "DSA";
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00019A1A File Offset: 0x00017C1A
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00019A1D File Offset: 0x00017C1D
		[SecuritySafeCritical]
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			rgbHash = this.AdjustHashSizeIfNecessary(rgbHash);
			return NCryptNative.SignHash(this.KeyHandle, rgbHash, rgbHash.Length * 2);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00019A47 File Offset: 0x00017C47
		[SecuritySafeCritical]
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			rgbHash = this.AdjustHashSizeIfNecessary(rgbHash);
			return NCryptNative.VerifySignature(this.KeyHandle, rgbHash, rgbSignature);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00019A7C File Offset: 0x00017C7C
		protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			byte[] result;
			using (BCryptHashAlgorithm bcryptHashAlgorithm = new BCryptHashAlgorithm(new CngAlgorithm(hashAlgorithm.Name), "Microsoft Primitive Provider"))
			{
				bcryptHashAlgorithm.HashCore(data, offset, count);
				result = bcryptHashAlgorithm.HashFinal();
			}
			return result;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00019AD0 File Offset: 0x00017CD0
		protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			byte[] result;
			using (BCryptHashAlgorithm bcryptHashAlgorithm = new BCryptHashAlgorithm(new CngAlgorithm(hashAlgorithm.Name), "Microsoft Primitive Provider"))
			{
				bcryptHashAlgorithm.HashStream(data);
				result = bcryptHashAlgorithm.HashFinal();
			}
			return result;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00019B20 File Offset: 0x00017D20
		protected override void Dispose(bool disposing)
		{
			if (disposing && this._key != null)
			{
				this._key.Dispose();
				this._key = null;
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00019B40 File Offset: 0x00017D40
		private byte[] AdjustHashSizeIfNecessary(byte[] hash)
		{
			int num = this.ComputeQLength();
			if (num > hash.Length)
			{
				throw new PlatformNotSupportedException("Cryptography_DSA_HashTooShort");
			}
			Array.Resize<byte>(ref hash, num);
			return hash;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00019B70 File Offset: 0x00017D70
		[SecuritySafeCritical]
		private unsafe int ComputeQLength()
		{
			CngKey key = this.Key;
			byte[] array = key.Export(CngKeyBlobFormat.GenericPublicBlob);
			if (array.Length < sizeof(BCRYPT_DSA_KEY_BLOB_V2))
			{
				return 20;
			}
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			BCRYPT_DSA_KEY_BLOB_V2* ptr2 = (BCRYPT_DSA_KEY_BLOB_V2*)ptr;
			if (ptr2->dwMagic != BCryptNative.KeyBlobMagicNumber.DsaPublicV2 && ptr2->dwMagic != BCryptNative.KeyBlobMagicNumber.DsaPrivateV2)
			{
				return 20;
			}
			return ptr2->cbGroupSize;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00019BE0 File Offset: 0x00017DE0
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			byte[] buffer = this.Key.Export(includePrivateParameters ? CngKeyBlobFormat.GenericPrivateBlob : CngKeyBlobFormat.GenericPublicBlob);
			DSAParameters result;
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(buffer)))
			{
				try
				{
					DSAParameters dsaparameters = default(DSAParameters);
					BCryptNative.KeyBlobMagicNumber keyBlobMagicNumber = (BCryptNative.KeyBlobMagicNumber)binaryReader.ReadInt32();
					if (keyBlobMagicNumber == BCryptNative.KeyBlobMagicNumber.DsaPublic || keyBlobMagicNumber == BCryptNative.KeyBlobMagicNumber.DsaPrivate)
					{
						if (includePrivateParameters && keyBlobMagicNumber != BCryptNative.KeyBlobMagicNumber.DsaPrivate)
						{
							throw new CryptographicException("Cryptography_NotValidPublicOrPrivateKey");
						}
						int count = binaryReader.ReadInt32();
						dsaparameters.Counter = DSACng.FromBigEndian(binaryReader.ReadBytes(4));
						dsaparameters.Seed = binaryReader.ReadBytes(20);
						dsaparameters.Q = binaryReader.ReadBytes(20);
						dsaparameters.P = binaryReader.ReadBytes(count);
						dsaparameters.G = binaryReader.ReadBytes(count);
						dsaparameters.Y = binaryReader.ReadBytes(count);
						if (includePrivateParameters)
						{
							dsaparameters.X = binaryReader.ReadBytes(20);
						}
					}
					else
					{
						if (keyBlobMagicNumber != BCryptNative.KeyBlobMagicNumber.DsaPublicV2 && keyBlobMagicNumber != BCryptNative.KeyBlobMagicNumber.DsaPrivateV2)
						{
							throw new CryptographicException("Cryptography_NotValidPublicOrPrivateKey");
						}
						if (includePrivateParameters && keyBlobMagicNumber != BCryptNative.KeyBlobMagicNumber.DsaPrivateV2)
						{
							throw new CryptographicException("Cryptography_NotValidPublicOrPrivateKey");
						}
						int count2 = binaryReader.ReadInt32();
						HASHALGORITHM_ENUM hashalgorithm_ENUM = (HASHALGORITHM_ENUM)binaryReader.ReadInt32();
						DSAFIPSVERSION_ENUM dsafipsversion_ENUM = (DSAFIPSVERSION_ENUM)binaryReader.ReadInt32();
						int count3 = binaryReader.ReadInt32();
						int count4 = binaryReader.ReadInt32();
						dsaparameters.Counter = DSACng.FromBigEndian(binaryReader.ReadBytes(4));
						dsaparameters.Seed = binaryReader.ReadBytes(count3);
						dsaparameters.Q = binaryReader.ReadBytes(count4);
						dsaparameters.P = binaryReader.ReadBytes(count2);
						dsaparameters.G = binaryReader.ReadBytes(count2);
						dsaparameters.Y = binaryReader.ReadBytes(count2);
						if (includePrivateParameters)
						{
							dsaparameters.X = binaryReader.ReadBytes(count4);
						}
					}
					if (dsaparameters.Counter == -1)
					{
						dsaparameters.Counter = 0;
						dsaparameters.Seed = null;
					}
					result = dsaparameters;
				}
				catch (EndOfStreamException)
				{
					throw new CryptographicException("Cryptography_NotValidPublicOrPrivateKey");
				}
			}
			return result;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00019E04 File Offset: 0x00018004
		private static int FromBigEndian(byte[] b)
		{
			return (int)b[0] << 24 | (int)b[1] << 16 | (int)b[2] << 8 | (int)b[3];
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00019E20 File Offset: 0x00018020
		public override void ImportParameters(DSAParameters parameters)
		{
			if (parameters.P == null || parameters.Q == null || parameters.G == null || parameters.Y == null)
			{
				throw new ArgumentException("Cryptography_InvalidDsaParameters_MissingFields");
			}
			if (parameters.J != null && parameters.J.Length >= parameters.P.Length)
			{
				throw new ArgumentException("Cryptography_InvalidDsaParameters_MismatchedPJ");
			}
			bool flag = parameters.X != null;
			int num = parameters.P.Length;
			int num2 = num * 8;
			if (parameters.G.Length != num || parameters.Y.Length != num)
			{
				throw new ArgumentException("Cryptography_InvalidDsaParameters_MismatchedPGY");
			}
			if (flag && parameters.X.Length != parameters.Q.Length)
			{
				throw new ArgumentException("Cryptography_InvalidDsaParameters_MismatchedQX");
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					if (num2 <= 1024)
					{
						DSACng.GenerateV1DsaBlob(binaryWriter, parameters, num, flag);
					}
					else
					{
						DSACng.GenerateV2DsaBlob(binaryWriter, parameters, num, flag);
					}
				}
				memoryStream.Flush();
				byte[] keyBlob = memoryStream.ToArray();
				CngKey cngKey = CngKey.Import(keyBlob, flag ? CngKeyBlobFormat.GenericPrivateBlob : CngKeyBlobFormat.GenericPublicBlob);
				CngExportPolicies value = cngKey.ExportPolicy | CngExportPolicies.AllowPlaintextExport;
				cngKey.SetProperty(new CngProperty("Export Policy", BitConverter.GetBytes((int)value), CngPropertyOptions.None));
				this.Key = cngKey;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00019F8C File Offset: 0x0001818C
		private static void GenerateV1DsaBlob(BinaryWriter bw, DSAParameters parameters, int keySizeInBytes, bool hasPrivateKey)
		{
			bw.Write(hasPrivateKey ? 1448104772 : 1112560452);
			bw.Write(keySizeInBytes);
			if (parameters.Seed != null)
			{
				if (parameters.Seed.Length != 20)
				{
					throw new ArgumentException("Cryptography_InvalidDsaParameters_SeedRestriction_ShortKey");
				}
				bw.Write(DSACng.ToBigEndian(parameters.Counter));
				bw.Write(parameters.Seed);
			}
			else
			{
				bw.Write(uint.MaxValue);
				for (int i = 0; i < 20; i++)
				{
					bw.Write(byte.MaxValue);
				}
			}
			if (parameters.Q.Length != 20)
			{
				throw new ArgumentException("Cryptography_InvalidDsaParameters_QRestriction_ShortKey");
			}
			bw.Write(parameters.Q);
			bw.Write(parameters.P);
			bw.Write(parameters.G);
			bw.Write(parameters.Y);
			if (hasPrivateKey)
			{
				bw.Write(parameters.X);
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001A068 File Offset: 0x00018268
		private static void GenerateV2DsaBlob(BinaryWriter bw, DSAParameters parameters, int keySizeInBytes, bool hasPrivateKey)
		{
			bw.Write(hasPrivateKey ? 844517444 : 843206724);
			bw.Write(keySizeInBytes);
			int num = parameters.Q.Length;
			HASHALGORITHM_ENUM value;
			if (num != 20)
			{
				if (num != 32)
				{
					if (num != 64)
					{
						throw new PlatformNotSupportedException("Cryptography_InvalidDsaParameters_QRestriction_LargeKey");
					}
					value = HASHALGORITHM_ENUM.DSA_HASH_ALGORITHM_SHA512;
				}
				else
				{
					value = HASHALGORITHM_ENUM.DSA_HASH_ALGORITHM_SHA256;
				}
			}
			else
			{
				value = HASHALGORITHM_ENUM.DSA_HASH_ALGORITHM_SHA1;
			}
			bw.Write((int)value);
			bw.Write(1);
			if (parameters.Seed != null)
			{
				bw.Write(parameters.Seed.Length);
				bw.Write(parameters.Q.Length);
				bw.Write(DSACng.ToBigEndian(parameters.Counter));
				bw.Write(parameters.Seed);
			}
			else
			{
				int num2 = parameters.Q.Length;
				bw.Write(num2);
				bw.Write(parameters.Q.Length);
				bw.Write(uint.MaxValue);
				for (int i = 0; i < num2; i++)
				{
					bw.Write(byte.MaxValue);
				}
			}
			bw.Write(parameters.Q);
			bw.Write(parameters.P);
			bw.Write(parameters.G);
			bw.Write(parameters.Y);
			if (hasPrivateKey)
			{
				bw.Write(parameters.X);
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001A190 File Offset: 0x00018390
		private static byte[] ToBigEndian(int i)
		{
			return new byte[]
			{
				(byte)(i >> 24),
				(byte)(i >> 16),
				(byte)(i >> 8),
				(byte)i
			};
		}

		// Token: 0x0400064C RID: 1612
		private CngKey _key;

		// Token: 0x0400064D RID: 1613
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(512, 3072, 64)
		};

		// Token: 0x0400064E RID: 1614
		private static CngAlgorithm s_cngAlgorithmDsa = new CngAlgorithm("DSA");

		// Token: 0x0400064F RID: 1615
		private const int MaxV1KeySize = 1024;

		// Token: 0x04000650 RID: 1616
		private const int Sha1HashOutputSize = 20;

		// Token: 0x04000651 RID: 1617
		private const int Sha256HashOutputSize = 32;

		// Token: 0x04000652 RID: 1618
		private const int Sha512HashOutputSize = 64;
	}
}
