using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000E3 RID: 227
	internal static class BCryptNative
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00016958 File Offset: 0x00014B58
		internal static bool BCryptSupported
		{
			[SecuritySafeCritical]
			get
			{
				if (!BCryptNative.s_haveBcryptSupported)
				{
					using (SafeLibraryHandle safeLibraryHandle = Microsoft.Win32.UnsafeNativeMethods.LoadLibraryEx("bcrypt", IntPtr.Zero, 0))
					{
						BCryptNative.s_bcryptSupported = !safeLibraryHandle.IsInvalid;
						BCryptNative.s_haveBcryptSupported = true;
					}
				}
				return BCryptNative.s_bcryptSupported;
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000169BC File Offset: 0x00014BBC
		[SecurityCritical]
		internal static int GetInt32Property<T>(T algorithm, string property) where T : SafeHandle
		{
			return BitConverter.ToInt32(BCryptNative.GetProperty<T>(algorithm, property), 0);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000169CC File Offset: 0x00014BCC
		[SecurityCritical]
		internal static byte[] GetProperty<T>(T algorithm, string property) where T : SafeHandle
		{
			BCryptNative.BCryptPropertyGetter<T> bcryptPropertyGetter = null;
			if (typeof(T) == typeof(SafeBCryptAlgorithmHandle))
			{
				bcryptPropertyGetter = (new BCryptNative.BCryptPropertyGetter<SafeBCryptAlgorithmHandle>(BCryptNative.UnsafeNativeMethods.BCryptGetAlgorithmProperty) as BCryptNative.BCryptPropertyGetter<T>);
			}
			else if (typeof(T) == typeof(SafeBCryptHashHandle))
			{
				bcryptPropertyGetter = (new BCryptNative.BCryptPropertyGetter<SafeBCryptHashHandle>(BCryptNative.UnsafeNativeMethods.BCryptGetHashProperty) as BCryptNative.BCryptPropertyGetter<T>);
			}
			int num = 0;
			BCryptNative.ErrorCode errorCode = bcryptPropertyGetter(algorithm, property, null, 0, ref num, 0);
			if (errorCode != BCryptNative.ErrorCode.BufferToSmall && errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			byte[] array = new byte[num];
			errorCode = bcryptPropertyGetter(algorithm, property, array, array.Length, ref num, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return array;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00016A7C File Offset: 0x00014C7C
		internal static void MapAlgorithmIdToMagic(string algorithm, out BCryptNative.KeyBlobMagicNumber algorithmMagic, out int keySize)
		{
			if (algorithm == "ECDH_P256")
			{
				algorithmMagic = BCryptNative.KeyBlobMagicNumber.ECDHPublicP256;
				keySize = 256;
				return;
			}
			if (algorithm == "ECDH_P384")
			{
				algorithmMagic = BCryptNative.KeyBlobMagicNumber.ECDHPublicP384;
				keySize = 384;
				return;
			}
			if (algorithm == "ECDH_P521")
			{
				algorithmMagic = BCryptNative.KeyBlobMagicNumber.ECDHPublicP521;
				keySize = 521;
				return;
			}
			if (algorithm == "ECDSA_P256")
			{
				algorithmMagic = BCryptNative.KeyBlobMagicNumber.ECDsaPublicP256;
				keySize = 256;
				return;
			}
			if (algorithm == "ECDSA_P384")
			{
				algorithmMagic = BCryptNative.KeyBlobMagicNumber.ECDsaPublicP384;
				keySize = 384;
				return;
			}
			if (!(algorithm == "ECDSA_P521"))
			{
				throw new ArgumentException(SR.GetString("Cryptography_UnknownEllipticCurveAlgorithm"));
			}
			algorithmMagic = BCryptNative.KeyBlobMagicNumber.ECDsaPublicP521;
			keySize = 521;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00016B44 File Offset: 0x00014D44
		[SecurityCritical]
		internal static SafeBCryptAlgorithmHandle OpenAlgorithm(string algorithm, string implementation)
		{
			SafeBCryptAlgorithmHandle result = null;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptOpenAlgorithmProvider(out result, algorithm, implementation, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return result;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00016B6C File Offset: 0x00014D6C
		[SecuritySafeCritical]
		internal static SafeBCryptKeyHandle ImportAsymmetricPublicKey(X509Native.CERT_PUBLIC_KEY_INFO certPublicKeyInfo, int dwFlag)
		{
			SafeBCryptKeyHandle result = null;
			if (BCryptNative.UnsafeNativeMethods.CryptImportPublicKeyInfoEx2(1U, ref certPublicKeyInfo, dwFlag, IntPtr.Zero, out result) == 0)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00016B9C File Offset: 0x00014D9C
		[SecuritySafeCritical]
		internal static byte[] ExportBCryptKey(SafeBCryptKeyHandle hKey, string blobType)
		{
			int num;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptExportKey(hKey, IntPtr.Zero, blobType, null, 0, out num, 0);
			if (errorCode != BCryptNative.ErrorCode.BufferToSmall && errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			byte[] array = new byte[num];
			errorCode = BCryptNative.UnsafeNativeMethods.BCryptExportKey(hKey, IntPtr.Zero, blobType, array, num, out num, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00016BFC File Offset: 0x00014DFC
		[SecuritySafeCritical]
		internal unsafe static SafeBCryptKeyHandle BCryptImportKey(SafeBCryptAlgorithmHandle hAlg, byte[] key)
		{
			int num = key.Length;
			int num2 = sizeof(BCryptNative.BCRYPT_KEY_DATA_BLOB_HEADER) + num;
			byte[] array = new byte[num2];
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
			BCryptNative.BCRYPT_KEY_DATA_BLOB_HEADER* ptr2 = (BCryptNative.BCRYPT_KEY_DATA_BLOB_HEADER*)ptr;
			ptr2->dwMagic = 1296188491U;
			ptr2->dwVersion = 1U;
			ptr2->cbKeyData = (uint)num;
			array2 = null;
			Buffer.BlockCopy(key, 0, array, sizeof(BCryptNative.BCRYPT_KEY_DATA_BLOB_HEADER), num);
			SafeBCryptKeyHandle result;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptImportKey(hAlg, IntPtr.Zero, "KeyDataBlob", out result, IntPtr.Zero, 0, array, num2, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return result;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00016C98 File Offset: 0x00014E98
		[SecuritySafeCritical]
		public unsafe static int BCryptEncrypt(SafeBCryptKeyHandle hKey, byte[] input, int inputOffset, int inputCount, byte[] iv, byte[] output, int outputOffset, int outputCount)
		{
			byte* ptr;
			if (input == null || input.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &input[0];
			}
			byte* ptr2;
			if (output == null || output.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &output[0];
			}
			int result;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptEncrypt(hKey, ptr + inputOffset, inputCount, IntPtr.Zero, iv, (iv == null) ? 0 : iv.Length, ptr2 + outputOffset, outputCount, out result, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return result;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00016D0C File Offset: 0x00014F0C
		[SecuritySafeCritical]
		public unsafe static int BCryptDecrypt(SafeBCryptKeyHandle hKey, byte[] input, int inputOffset, int inputCount, byte[] iv, byte[] output, int outputOffset, int outputCount)
		{
			byte* ptr;
			if (input == null || input.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &input[0];
			}
			byte* ptr2;
			if (output == null || output.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &output[0];
			}
			int result;
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptDecrypt(hKey, ptr + inputOffset, inputCount, IntPtr.Zero, iv, (iv == null) ? 0 : iv.Length, ptr2 + outputOffset, outputCount, out result, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return result;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00016D80 File Offset: 0x00014F80
		[SecurityCritical]
		public static void SetCipherMode(SafeBCryptAlgorithmHandle hAlg, string cipherMode)
		{
			BCryptNative.ErrorCode errorCode = BCryptNative.UnsafeNativeMethods.BCryptSetProperty(hAlg, "ChainingMode", cipherMode, (cipherMode.Length + 1) * 2, 0);
			if (errorCode != BCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
		}

		// Token: 0x040005EE RID: 1518
		internal const string BCRYPT_ECCPUBLIC_BLOB = "ECCPUBLICBLOB";

		// Token: 0x040005EF RID: 1519
		internal const string BCRYPT_ECCPRIVATE_BLOB = "ECCPRIVATEBLOB";

		// Token: 0x040005F0 RID: 1520
		internal const string BCRYPT_ECC_CURVE_NISTP256 = "nistP256";

		// Token: 0x040005F1 RID: 1521
		internal const string BCRYPT_ECC_CURVE_NISTP384 = "nistP384";

		// Token: 0x040005F2 RID: 1522
		internal const string BCRYPT_ECC_CURVE_NISTP521 = "nistP521";

		// Token: 0x040005F3 RID: 1523
		private static volatile bool s_haveBcryptSupported;

		// Token: 0x040005F4 RID: 1524
		private static volatile bool s_bcryptSupported;

		// Token: 0x0200031E RID: 798
		internal static class AlgorithmName
		{
			// Token: 0x04000E63 RID: 3683
			public const string ECDH = "ECDH";

			// Token: 0x04000E64 RID: 3684
			public const string ECDHP256 = "ECDH_P256";

			// Token: 0x04000E65 RID: 3685
			public const string ECDHP384 = "ECDH_P384";

			// Token: 0x04000E66 RID: 3686
			public const string ECDHP521 = "ECDH_P521";

			// Token: 0x04000E67 RID: 3687
			public const string ECDsa = "ECDSA";

			// Token: 0x04000E68 RID: 3688
			public const string ECDsaP256 = "ECDSA_P256";

			// Token: 0x04000E69 RID: 3689
			public const string ECDsaP384 = "ECDSA_P384";

			// Token: 0x04000E6A RID: 3690
			public const string ECDsaP521 = "ECDSA_P521";

			// Token: 0x04000E6B RID: 3691
			public const string MD5 = "MD5";

			// Token: 0x04000E6C RID: 3692
			public const string Sha1 = "SHA1";

			// Token: 0x04000E6D RID: 3693
			public const string Sha256 = "SHA256";

			// Token: 0x04000E6E RID: 3694
			public const string Sha384 = "SHA384";

			// Token: 0x04000E6F RID: 3695
			public const string Sha512 = "SHA512";

			// Token: 0x04000E70 RID: 3696
			internal const string Rsa = "RSA";
		}

		// Token: 0x0200031F RID: 799
		internal static class KeyBlobType
		{
			// Token: 0x04000E71 RID: 3697
			internal const string RsaFullPrivateBlob = "RSAFULLPRIVATEBLOB";

			// Token: 0x04000E72 RID: 3698
			internal const string RsaPrivateBlob = "RSAPRIVATEBLOB";

			// Token: 0x04000E73 RID: 3699
			internal const string RsaPublicBlob = "RSAPUBLICBLOB";
		}

		// Token: 0x02000320 RID: 800
		internal struct BCRYPT_RSAKEY_BLOB
		{
			// Token: 0x04000E74 RID: 3700
			internal BCryptNative.KeyBlobMagicNumber Magic;

			// Token: 0x04000E75 RID: 3701
			internal int BitLength;

			// Token: 0x04000E76 RID: 3702
			internal int cbPublicExp;

			// Token: 0x04000E77 RID: 3703
			internal int cbModulus;

			// Token: 0x04000E78 RID: 3704
			internal int cbPrime1;

			// Token: 0x04000E79 RID: 3705
			internal int cbPrime2;
		}

		// Token: 0x02000321 RID: 801
		internal enum ErrorCode
		{
			// Token: 0x04000E7B RID: 3707
			Success,
			// Token: 0x04000E7C RID: 3708
			BufferToSmall = -1073741789,
			// Token: 0x04000E7D RID: 3709
			ObjectNameNotFound = -1073741772
		}

		// Token: 0x02000322 RID: 802
		internal static class HashPropertyName
		{
			// Token: 0x04000E7E RID: 3710
			public const string HashLength = "HashDigestLength";
		}

		// Token: 0x02000323 RID: 803
		internal enum KeyBlobMagicNumber
		{
			// Token: 0x04000E80 RID: 3712
			DsaPublic = 1112560452,
			// Token: 0x04000E81 RID: 3713
			DsaPublicV2 = 843206724,
			// Token: 0x04000E82 RID: 3714
			DsaPrivate = 1448104772,
			// Token: 0x04000E83 RID: 3715
			DsaPrivateV2 = 844517444,
			// Token: 0x04000E84 RID: 3716
			ECDHPublicP256 = 827016005,
			// Token: 0x04000E85 RID: 3717
			ECDHPublicP384 = 860570437,
			// Token: 0x04000E86 RID: 3718
			ECDHPublicP521 = 894124869,
			// Token: 0x04000E87 RID: 3719
			ECDsaPublicP256 = 827540293,
			// Token: 0x04000E88 RID: 3720
			ECDsaPublicP384 = 861094725,
			// Token: 0x04000E89 RID: 3721
			ECDsaPublicP521 = 894649157,
			// Token: 0x04000E8A RID: 3722
			RsaPublic = 826364754,
			// Token: 0x04000E8B RID: 3723
			RsaPrivate = 843141970,
			// Token: 0x04000E8C RID: 3724
			RsaFullPrivateMagic = 859919186,
			// Token: 0x04000E8D RID: 3725
			KeyDataBlob = 1296188491
		}

		// Token: 0x02000324 RID: 804
		internal struct BCRYPT_OAEP_PADDING_INFO
		{
			// Token: 0x04000E8E RID: 3726
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszAlgId;

			// Token: 0x04000E8F RID: 3727
			internal IntPtr pbLabel;

			// Token: 0x04000E90 RID: 3728
			internal int cbLabel;
		}

		// Token: 0x02000325 RID: 805
		internal struct BCRYPT_PKCS1_PADDING_INFO
		{
			// Token: 0x04000E91 RID: 3729
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszAlgId;
		}

		// Token: 0x02000326 RID: 806
		internal struct BCRYPT_PSS_PADDING_INFO
		{
			// Token: 0x04000E92 RID: 3730
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszAlgId;

			// Token: 0x04000E93 RID: 3731
			internal int cbSalt;
		}

		// Token: 0x02000327 RID: 807
		private struct BCRYPT_KEY_DATA_BLOB_HEADER
		{
			// Token: 0x04000E94 RID: 3732
			public uint dwMagic;

			// Token: 0x04000E95 RID: 3733
			public uint dwVersion;

			// Token: 0x04000E96 RID: 3734
			public uint cbKeyData;

			// Token: 0x04000E97 RID: 3735
			public const uint BCRYPT_KEY_DATA_BLOB_MAGIC = 1296188491U;

			// Token: 0x04000E98 RID: 3736
			public const uint BCRYPT_KEY_DATA_BLOB_VERSION1 = 1U;
		}

		// Token: 0x02000328 RID: 808
		internal static class KeyDerivationFunction
		{
			// Token: 0x04000E99 RID: 3737
			public const string Hash = "HASH";

			// Token: 0x04000E9A RID: 3738
			public const string Hmac = "HMAC";

			// Token: 0x04000E9B RID: 3739
			public const string Tls = "TLS_PRF";
		}

		// Token: 0x02000329 RID: 809
		internal static class ProviderName
		{
			// Token: 0x04000E9C RID: 3740
			public const string MicrosoftPrimitiveProvider = "Microsoft Primitive Provider";
		}

		// Token: 0x0200032A RID: 810
		internal static class ObjectPropertyName
		{
			// Token: 0x04000E9D RID: 3741
			public const string ObjectLength = "ObjectLength";
		}

		// Token: 0x0200032B RID: 811
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class UnsafeNativeMethods
		{
			// Token: 0x06001AFF RID: 6911
			[DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern BCryptNative.ErrorCode BCryptCreateHash(SafeBCryptAlgorithmHandle hAlgorithm, out SafeBCryptHashHandle phHash, IntPtr pbHashObject, int cbHashObject, IntPtr pbSecret, int cbSecret, int dwFlags);

			// Token: 0x06001B00 RID: 6912
			[DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern BCryptNative.ErrorCode BCryptGetProperty(SafeBCryptAlgorithmHandle hObject, string pszProperty, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbOutput, int cbOutput, [In] [Out] ref int pcbResult, int flags);

			// Token: 0x06001B01 RID: 6913
			[DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern BCryptNative.ErrorCode BCryptGetProperty(SafeBCryptKeyHandle hObject, string pszProperty, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, int flags);

			// Token: 0x06001B02 RID: 6914
			[DllImport("bcrypt.dll", CharSet = CharSet.Unicode, EntryPoint = "BCryptGetProperty")]
			internal static extern BCryptNative.ErrorCode BCryptGetAlgorithmProperty(SafeBCryptAlgorithmHandle hObject, string pszProperty, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbOutput, int cbOutput, [In] [Out] ref int pcbResult, int flags);

			// Token: 0x06001B03 RID: 6915
			[DllImport("bcrypt.dll", CharSet = CharSet.Unicode, EntryPoint = "BCryptGetProperty")]
			internal static extern BCryptNative.ErrorCode BCryptGetHashProperty(SafeBCryptHashHandle hObject, string pszProperty, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbOutput, int cbOutput, [In] [Out] ref int pcbResult, int flags);

			// Token: 0x06001B04 RID: 6916
			[DllImport("bcrypt.dll")]
			internal static extern BCryptNative.ErrorCode BCryptFinishHash(SafeBCryptHashHandle hHash, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbInput, int cbInput, int dwFlags);

			// Token: 0x06001B05 RID: 6917
			[DllImport("bcrypt.dll")]
			internal unsafe static extern BCryptNative.ErrorCode BCryptHashData(SafeBCryptHashHandle hHash, byte* pbInput, int cbInput, int dwFlags);

			// Token: 0x06001B06 RID: 6918
			[DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern BCryptNative.ErrorCode BCryptOpenAlgorithmProvider(out SafeBCryptAlgorithmHandle phAlgorithm, string pszAlgId, string pszImplementation, int dwFlags);

			// Token: 0x06001B07 RID: 6919
			[DllImport("bcrypt.dll", SetLastError = true)]
			internal static extern BCryptNative.ErrorCode BCryptExportKey([In] SafeBCryptKeyHandle hKey, [In] IntPtr hExportKey, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszBlobType, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, [In] int cbOutput, out int pcbResult, [In] int dwFlags);

			// Token: 0x06001B08 RID: 6920
			[DllImport("crypt32.dll", SetLastError = true)]
			internal static extern int CryptImportPublicKeyInfoEx2([In] uint dwCertEncodingType, [In] ref X509Native.CERT_PUBLIC_KEY_INFO pInfo, [In] int dwFlags, [In] IntPtr pvAuxInfo, out SafeBCryptKeyHandle phKey);

			// Token: 0x06001B09 RID: 6921
			[DllImport("bcrypt.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern BCryptNative.ErrorCode BCryptImportKey(SafeBCryptAlgorithmHandle hAlgorithm, IntPtr hImportKey, string pszBlobType, out SafeBCryptKeyHandle hKey, IntPtr pbKeyObject, int cbKeyObject, byte[] pbInput, int cbInput, int dwFlags);

			// Token: 0x06001B0A RID: 6922
			[DllImport("bcrypt.dll", SetLastError = true)]
			public unsafe static extern BCryptNative.ErrorCode BCryptEncrypt(SafeBCryptKeyHandle hKey, byte* pbInput, int cbInput, IntPtr paddingInfo, [In] [Out] byte[] pbIV, int cbIV, byte* pbOutput, int cbOutput, out int cbResult, int dwFlags);

			// Token: 0x06001B0B RID: 6923
			[DllImport("bcrypt.dll", SetLastError = true)]
			public unsafe static extern BCryptNative.ErrorCode BCryptDecrypt(SafeBCryptKeyHandle hKey, byte* pbInput, int cbInput, IntPtr paddingInfo, [In] [Out] byte[] pbIV, int cbIV, byte* pbOutput, int cbOutput, out int cbResult, int dwFlags);

			// Token: 0x06001B0C RID: 6924
			[DllImport("bcrypt.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern BCryptNative.ErrorCode BCryptSetProperty(SafeBCryptAlgorithmHandle hObject, string pszProperty, string pbInput, int cbInput, int dwFlags);
		}

		// Token: 0x0200032C RID: 812
		[SecuritySafeCritical]
		internal static class AesBCryptModes
		{
			// Token: 0x06001B0D RID: 6925 RVA: 0x00063077 File Offset: 0x00061277
			internal static SafeBCryptAlgorithmHandle GetSharedHandle(CipherMode cipherMode)
			{
				if (cipherMode == CipherMode.CBC)
				{
					return BCryptNative.AesBCryptModes.s_hAlgCbc;
				}
				if (cipherMode != CipherMode.ECB)
				{
					throw new NotSupportedException();
				}
				return BCryptNative.AesBCryptModes.s_hAlgEcb;
			}

			// Token: 0x06001B0E RID: 6926 RVA: 0x00063094 File Offset: 0x00061294
			private static SafeBCryptAlgorithmHandle OpenAesAlgorithm(string cipherMode)
			{
				SafeBCryptAlgorithmHandle safeBCryptAlgorithmHandle = BCryptNative.OpenAlgorithm("AES", null);
				BCryptNative.SetCipherMode(safeBCryptAlgorithmHandle, cipherMode);
				return safeBCryptAlgorithmHandle;
			}

			// Token: 0x04000E9E RID: 3742
			[SecurityCritical]
			private static readonly SafeBCryptAlgorithmHandle s_hAlgCbc = BCryptNative.AesBCryptModes.OpenAesAlgorithm("ChainingModeCBC");

			// Token: 0x04000E9F RID: 3743
			[SecurityCritical]
			private static readonly SafeBCryptAlgorithmHandle s_hAlgEcb = BCryptNative.AesBCryptModes.OpenAesAlgorithm("ChainingModeECB");
		}

		// Token: 0x0200032D RID: 813
		[SecuritySafeCritical]
		internal static class TripleDesBCryptModes
		{
			// Token: 0x06001B10 RID: 6928 RVA: 0x000630D5 File Offset: 0x000612D5
			internal static SafeBCryptAlgorithmHandle GetSharedHandle(CipherMode cipherMode)
			{
				if (cipherMode == CipherMode.CBC)
				{
					return BCryptNative.TripleDesBCryptModes.s_hAlgCbc;
				}
				if (cipherMode != CipherMode.ECB)
				{
					throw new NotSupportedException();
				}
				return BCryptNative.TripleDesBCryptModes.s_hAlgEcb;
			}

			// Token: 0x06001B11 RID: 6929 RVA: 0x000630F4 File Offset: 0x000612F4
			private static SafeBCryptAlgorithmHandle OpenAesAlgorithm(string cipherMode)
			{
				SafeBCryptAlgorithmHandle safeBCryptAlgorithmHandle = BCryptNative.OpenAlgorithm("3DES", null);
				BCryptNative.SetCipherMode(safeBCryptAlgorithmHandle, cipherMode);
				return safeBCryptAlgorithmHandle;
			}

			// Token: 0x04000EA0 RID: 3744
			[SecurityCritical]
			private static readonly SafeBCryptAlgorithmHandle s_hAlgCbc = BCryptNative.TripleDesBCryptModes.OpenAesAlgorithm("ChainingModeCBC");

			// Token: 0x04000EA1 RID: 3745
			[SecurityCritical]
			private static readonly SafeBCryptAlgorithmHandle s_hAlgEcb = BCryptNative.TripleDesBCryptModes.OpenAesAlgorithm("ChainingModeECB");
		}

		// Token: 0x0200032E RID: 814
		// (Invoke) Token: 0x06001B14 RID: 6932
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private delegate BCryptNative.ErrorCode BCryptPropertyGetter<T>(T hObject, string pszProperty, byte[] pbOutput, int cbOutput, ref int pcbResult, int dwFlags) where T : SafeHandle;
	}
}
