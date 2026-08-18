using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200010F RID: 271
	public sealed class RSACng : RSA
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		public RSACng() : this(2048)
		{
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001E4BD File Offset: 0x0001C6BD
		public RSACng(int keySize)
		{
			this.LegalKeySizesValue = RSACng.s_legalKeySizes;
			this.KeySize = keySize;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001E4D8 File Offset: 0x0001C6D8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public RSACng(CngKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.AlgorithmGroup != CngAlgorithmGroup.Rsa)
			{
				throw new ArgumentException(SR.GetString("Cryptography_ArgRSAaRequiresRSAKey"), "key");
			}
			this.LegalKeySizesValue = RSACng.s_legalKeySizes;
			this.Key = CngKey.Open(key.Handle, key.IsEphemeral ? CngKeyHandleOpenOptions.EphemeralKey : CngKeyHandleOpenOptions.None);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0001E548 File Offset: 0x0001C748
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0001E5D8 File Offset: 0x0001C7D8
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
					this._key = CngKey.Create(CngAlgorithm.Rsa, null, cngKeyCreationParameters);
				}
				return this._key;
			}
			private set
			{
				if (value.AlgorithmGroup != CngAlgorithmGroup.Rsa)
				{
					throw new ArgumentException(SR.GetString("Cryptography_ArgRSAaRequiresRSAKey"), "value");
				}
				if (this._key != null)
				{
					this._key.Dispose();
				}
				this._key = value;
				this.KeySizeValue = this._key.KeySize;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0001E637 File Offset: 0x0001C837
		private SafeNCryptKeyHandle KeyHandle
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			get
			{
				return this.Key.Handle;
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001E644 File Offset: 0x0001C844
		protected override void Dispose(bool disposing)
		{
			if (disposing && this._key != null)
			{
				this._key.Dispose();
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001E65C File Offset: 0x0001C85C
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

		// Token: 0x060008CD RID: 2253 RVA: 0x0001E6B0 File Offset: 0x0001C8B0
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

		// Token: 0x060008CE RID: 2254 RVA: 0x0001E700 File Offset: 0x0001C900
		private void CheckMagicValueOfKey(int magic, bool includePrivateParameters)
		{
			if (!includePrivateParameters)
			{
				if (magic != 826364754 && magic != 843141970 && magic != 859919186)
				{
					throw new CryptographicException(SR.GetString("Cryptography_NotValidPublicOrPrivateKey"));
				}
			}
			else if (magic != 843141970 && magic != 859919186)
			{
				throw new CryptographicException(SR.GetString("Cryptography_NotValidPrivateKey"));
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001E758 File Offset: 0x0001C958
		[SecuritySafeCritical]
		public unsafe override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			byte[] array = this.Key.Export(includePrivateParameters ? RSACng.s_rsaFullPrivateBlob : RSACng.s_rsaPublicBlob);
			RSAParameters rsaparameters = default(RSAParameters);
			int magic = BitConverter.ToInt32(new byte[]
			{
				array[0],
				array[1],
				array[2],
				array[3]
			}, 0);
			this.CheckMagicValueOfKey(magic, includePrivateParameters);
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
			BCryptNative.BCRYPT_RSAKEY_BLOB* ptr2 = (BCryptNative.BCRYPT_RSAKEY_BLOB*)ptr;
			int num = Marshal.SizeOf(typeof(BCryptNative.BCRYPT_RSAKEY_BLOB));
			rsaparameters.Exponent = new byte[ptr2->cbPublicExp];
			Buffer.BlockCopy(array, num, rsaparameters.Exponent, 0, rsaparameters.Exponent.Length);
			num += ptr2->cbPublicExp;
			rsaparameters.Modulus = new byte[ptr2->cbModulus];
			Buffer.BlockCopy(array, num, rsaparameters.Modulus, 0, rsaparameters.Modulus.Length);
			num += ptr2->cbModulus;
			if (includePrivateParameters)
			{
				rsaparameters.P = new byte[ptr2->cbPrime1];
				Buffer.BlockCopy(array, num, rsaparameters.P, 0, rsaparameters.P.Length);
				num += ptr2->cbPrime1;
				rsaparameters.Q = new byte[ptr2->cbPrime2];
				Buffer.BlockCopy(array, num, rsaparameters.Q, 0, rsaparameters.Q.Length);
				num += ptr2->cbPrime2;
				rsaparameters.DP = new byte[ptr2->cbPrime1];
				Buffer.BlockCopy(array, num, rsaparameters.DP, 0, rsaparameters.DP.Length);
				num += ptr2->cbPrime1;
				rsaparameters.DQ = new byte[ptr2->cbPrime2];
				Buffer.BlockCopy(array, num, rsaparameters.DQ, 0, rsaparameters.DQ.Length);
				num += ptr2->cbPrime2;
				rsaparameters.InverseQ = new byte[ptr2->cbPrime1];
				Buffer.BlockCopy(array, num, rsaparameters.InverseQ, 0, rsaparameters.InverseQ.Length);
				num += ptr2->cbPrime1;
				rsaparameters.D = new byte[ptr2->cbModulus];
				Buffer.BlockCopy(array, num, rsaparameters.D, 0, rsaparameters.D.Length);
				num += ptr2->cbModulus;
			}
			array2 = null;
			return rsaparameters;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
		[SecuritySafeCritical]
		public unsafe override void ImportParameters(RSAParameters parameters)
		{
			if (parameters.Exponent == null || parameters.Modulus == null)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidRsaParameters"));
			}
			bool flag = parameters.P == null || parameters.Q == null;
			int num = Marshal.SizeOf(typeof(BCryptNative.BCRYPT_RSAKEY_BLOB)) + parameters.Exponent.Length + parameters.Modulus.Length;
			if (!flag)
			{
				num += parameters.P.Length + parameters.Q.Length;
			}
			byte[] array = new byte[num];
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
			BCryptNative.BCRYPT_RSAKEY_BLOB* ptr2 = (BCryptNative.BCRYPT_RSAKEY_BLOB*)ptr;
			ptr2->Magic = (flag ? BCryptNative.KeyBlobMagicNumber.RsaPublic : BCryptNative.KeyBlobMagicNumber.RsaPrivate);
			ptr2->BitLength = parameters.Modulus.Length * 8;
			ptr2->cbPublicExp = parameters.Exponent.Length;
			ptr2->cbModulus = parameters.Modulus.Length;
			if (!flag)
			{
				ptr2->cbPrime1 = parameters.P.Length;
				ptr2->cbPrime2 = parameters.Q.Length;
			}
			int num2 = Marshal.SizeOf(typeof(BCryptNative.BCRYPT_RSAKEY_BLOB));
			Buffer.BlockCopy(parameters.Exponent, 0, array, num2, parameters.Exponent.Length);
			num2 += parameters.Exponent.Length;
			Buffer.BlockCopy(parameters.Modulus, 0, array, num2, parameters.Modulus.Length);
			num2 += parameters.Modulus.Length;
			if (!flag)
			{
				Buffer.BlockCopy(parameters.P, 0, array, num2, parameters.P.Length);
				num2 += parameters.P.Length;
				Buffer.BlockCopy(parameters.Q, 0, array, num2, parameters.Q.Length);
				num2 += parameters.Q.Length;
			}
			array2 = null;
			CngKey cngKey = CngKey.Import(array, flag ? RSACng.s_rsaPublicBlob : RSACng.s_rsaPrivateBlob);
			cngKey.ExportPolicy |= CngExportPolicies.AllowPlaintextExport;
			this.Key = cngKey;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001EB80 File Offset: 0x0001CD80
		[SecuritySafeCritical]
		public override byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			SafeNCryptKeyHandle keyHandle = this.KeyHandle;
			if (padding == RSAEncryptionPadding.Pkcs1)
			{
				return NCryptNative.DecryptDataPkcs1(keyHandle, data);
			}
			if (padding.Mode == RSAEncryptionPaddingMode.Oaep)
			{
				return NCryptNative.DecryptDataOaep(keyHandle, data, padding.OaepHashAlgorithm.Name);
			}
			throw new CryptographicException(SR.GetString("Cryptography_UnsupportedPaddingMode"));
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001EBFC File Offset: 0x0001CDFC
		[SecuritySafeCritical]
		public override byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding == RSAEncryptionPadding.Pkcs1)
			{
				return NCryptNative.EncryptDataPkcs1(this.KeyHandle, data);
			}
			if (padding.Mode == RSAEncryptionPaddingMode.Oaep)
			{
				return NCryptNative.EncryptDataOaep(this.KeyHandle, data, padding.OaepHashAlgorithm.Name);
			}
			throw new CryptographicException(SR.GetString("Cryptography_UnsupportedPaddingMode"));
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001EC78 File Offset: 0x0001CE78
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public override byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(SR.GetString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			CngKey key = this.Key;
			SafeNCryptKeyHandle handle = key.Handle;
			if (padding == RSASignaturePadding.Pkcs1)
			{
				return NCryptNative.SignHashPkcs1(handle, hash, hashAlgorithm.Name);
			}
			if (padding == RSASignaturePadding.Pss)
			{
				return NCryptNative.SignHashPss(handle, hash, hashAlgorithm.Name, hash.Length);
			}
			throw new CryptographicException(SR.GetString("Cryptography_UnsupportedPaddingMode"));
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001ED24 File Offset: 0x0001CF24
		[SecuritySafeCritical]
		public override bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(SR.GetString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding == RSASignaturePadding.Pkcs1)
			{
				return NCryptNative.VerifySignaturePkcs1(this.KeyHandle, hash, hashAlgorithm.Name, signature);
			}
			if (padding == RSASignaturePadding.Pss)
			{
				return NCryptNative.VerifySignaturePss(this.KeyHandle, hash, hashAlgorithm.Name, hash.Length, signature);
			}
			throw new CryptographicException(SR.GetString("Cryptography_UnsupportedPaddingMode"));
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001EDDD File Offset: 0x0001CFDD
		public override byte[] DecryptValue(byte[] rgb)
		{
			throw new NotSupportedException("NotSupported_Method");
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001EDE9 File Offset: 0x0001CFE9
		public override byte[] EncryptValue(byte[] rgb)
		{
			throw new NotSupportedException("NotSupported_Method");
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0001EDF5 File Offset: 0x0001CFF5
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0001EDFC File Offset: 0x0001CFFC
		public override string SignatureAlgorithm
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x040006B7 RID: 1719
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(512, 16384, 64)
		};

		// Token: 0x040006B8 RID: 1720
		private static CngKeyBlobFormat s_rsaFullPrivateBlob = new CngKeyBlobFormat("RSAFULLPRIVATEBLOB");

		// Token: 0x040006B9 RID: 1721
		private static CngKeyBlobFormat s_rsaPrivateBlob = new CngKeyBlobFormat("RSAPRIVATEBLOB");

		// Token: 0x040006BA RID: 1722
		private static CngKeyBlobFormat s_rsaPublicBlob = new CngKeyBlobFormat("RSAPUBLICBLOB");

		// Token: 0x040006BB RID: 1723
		private CngKey _key;
	}
}
