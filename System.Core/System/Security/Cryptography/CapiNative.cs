using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000F1 RID: 241
	internal static class CapiNative
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x00018A70 File Offset: 0x00016C70
		[SecurityCritical]
		internal static SafeCspHandle AcquireCsp(string keyContainer, string providerName, CapiNative.ProviderType providerType, CapiNative.CryptAcquireContextFlags flags, bool throwPlatformException)
		{
			SafeCspHandle result = null;
			if (CapiNative.UnsafeNativeMethods.CryptAcquireContext(out result, keyContainer, providerName, providerType, flags))
			{
				return result;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (throwPlatformException && (lastWin32Error == -2146893801 || lastWin32Error == -2146893799))
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
			}
			throw new CryptographicException(lastWin32Error);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00018AC0 File Offset: 0x00016CC0
		[SecurityCritical]
		internal static byte[] ExportSymmetricKey(SafeCapiKeyHandle key)
		{
			int num = 0;
			if (!CapiNative.UnsafeNativeMethods.CryptExportKey(key, SafeCapiKeyHandle.InvalidHandle, 8, 0, null, ref num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 234)
				{
					throw new CryptographicException(lastWin32Error);
				}
			}
			byte[] array = new byte[num];
			if (!CapiNative.UnsafeNativeMethods.CryptExportKey(key, SafeCapiKeyHandle.InvalidHandle, 8, 0, array, ref num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			int srcOffset = Marshal.SizeOf(typeof(CapiNative.BLOBHEADER)) + Marshal.SizeOf(typeof(int));
			int num2 = BitConverter.ToInt32(array, Marshal.SizeOf(typeof(CapiNative.BLOBHEADER)));
			byte[] array2 = new byte[num2];
			Buffer.BlockCopy(array, srcOffset, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00018B6B File Offset: 0x00016D6B
		internal static string GetAlgorithmName(CapiNative.AlgorithmId algorithm)
		{
			return algorithm.ToString().ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00018B84 File Offset: 0x00016D84
		[SecurityCritical]
		internal static byte[] GetHashParameter(SafeCapiHashHandle hashHandle, CapiNative.HashParameter parameter)
		{
			int num = 0;
			if (!CapiNative.UnsafeNativeMethods.CryptGetHashParam(hashHandle, parameter, null, ref num, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			byte[] array = new byte[num];
			if (!CapiNative.UnsafeNativeMethods.CryptGetHashParam(hashHandle, parameter, array, ref num, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00018BE4 File Offset: 0x00016DE4
		[SecurityCritical]
		internal static T GetProviderParameterStruct<T>(SafeCspHandle provider, CapiNative.ProviderParameter parameter, CapiNative.ProviderParameterFlags flags) where T : struct
		{
			int cb = 0;
			IntPtr intPtr = IntPtr.Zero;
			if (!CapiNative.UnsafeNativeMethods.CryptGetProvParam(provider, parameter, intPtr, ref cb, flags))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 259)
				{
					return Activator.CreateInstance<T>();
				}
				if (lastWin32Error != 234)
				{
					throw new CryptographicException(lastWin32Error);
				}
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			T result;
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = Marshal.AllocCoTaskMem(cb);
				}
				if (!CapiNative.UnsafeNativeMethods.CryptGetProvParam(provider, parameter, intPtr, ref cb, flags))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				result = (T)((object)Marshal.PtrToStructure(intPtr, typeof(T)));
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return result;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00018CA0 File Offset: 0x00016EA0
		internal static int HResultForVerificationResult(SignatureVerificationResult verificationResult)
		{
			switch (verificationResult)
			{
			case SignatureVerificationResult.AssemblyIdentityMismatch:
			case SignatureVerificationResult.PublicKeyTokenMismatch:
			case SignatureVerificationResult.PublisherMismatch:
				return -2146762749;
			case SignatureVerificationResult.ContainingSignatureInvalid:
				return -2146869232;
			default:
				return (int)verificationResult;
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00018CCC File Offset: 0x00016ECC
		[SecurityCritical]
		internal unsafe static SafeCapiKeyHandle ImportSymmetricKey(SafeCspHandle provider, CapiNative.AlgorithmId algorithm, byte[] key)
		{
			int num = Marshal.SizeOf(typeof(CapiNative.BLOBHEADER)) + Marshal.SizeOf(typeof(int)) + key.Length;
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
			CapiNative.BLOBHEADER* ptr2 = (CapiNative.BLOBHEADER*)ptr;
			ptr2->bType = CapiNative.KeyBlobType.PlainText;
			ptr2->bVersion = 2;
			ptr2->reserved = 0;
			ptr2->aiKeyAlg = algorithm;
			int* ptr3 = (int*)(ptr + Marshal.SizeOf(*ptr2));
			*ptr3 = key.Length;
			array2 = null;
			Buffer.BlockCopy(key, 0, array, Marshal.SizeOf(typeof(CapiNative.BLOBHEADER)) + Marshal.SizeOf(typeof(int)), key.Length);
			SafeCapiKeyHandle safeCapiKeyHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!CapiNative.UnsafeNativeMethods.CryptImportKey(provider, array, array.Length, SafeCapiKeyHandle.InvalidHandle, CapiNative.KeyFlags.Exportable, out safeCapiKeyHandle))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				if (safeCapiKeyHandle != null && !safeCapiKeyHandle.IsInvalid)
				{
					safeCapiKeyHandle.SetParentCsp(provider);
				}
			}
			return safeCapiKeyHandle;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00018DD8 File Offset: 0x00016FD8
		[SecurityCritical]
		internal static void SetKeyParameter(SafeCapiKeyHandle key, CapiNative.KeyParameter parameter, int value)
		{
			CapiNative.SetKeyParameter(key, parameter, BitConverter.GetBytes(value));
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00018DE7 File Offset: 0x00016FE7
		[SecurityCritical]
		internal static void SetKeyParameter(SafeCapiKeyHandle key, CapiNative.KeyParameter parameter, byte[] value)
		{
			if (!CapiNative.UnsafeNativeMethods.CryptSetKeyParam(key, parameter, value, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00018E00 File Offset: 0x00017000
		[SecuritySafeCritical]
		internal static SafeLocalAllocHandle LocalAlloc(uint uFlags, IntPtr sizetdwBytes)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = CapiNative.UnsafeNativeMethods.LocalAlloc(uFlags, sizetdwBytes);
			if (safeLocalAllocHandle == null || safeLocalAllocHandle.IsInvalid)
			{
				throw new OutOfMemoryException();
			}
			return safeLocalAllocHandle;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00018E28 File Offset: 0x00017028
		[SecuritySafeCritical]
		internal unsafe static bool DecodeObject(IntPtr pszStructType, IntPtr pbEncoded, uint cbEncoded, out SafeLocalAllocHandle decodedValue, out uint cbDecodedValue)
		{
			decodedValue = SafeLocalAllocHandle.InvalidHandle;
			cbDecodedValue = 0U;
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CapiNative.UnsafeNativeMethods.CryptDecodeObject(65537U, pszStructType, pbEncoded, cbEncoded, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			safeLocalAllocHandle = CapiNative.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CapiNative.UnsafeNativeMethods.CryptDecodeObject(65537U, pszStructType, pbEncoded, cbEncoded, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			decodedValue = safeLocalAllocHandle;
			cbDecodedValue = num;
			return true;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00018E94 File Offset: 0x00017094
		[SecuritySafeCritical]
		internal unsafe static bool DecodeObject(IntPtr pszStructType, byte[] pbEncoded, out SafeLocalAllocHandle decodedValue, out uint cbDecodedValue)
		{
			decodedValue = SafeLocalAllocHandle.InvalidHandle;
			cbDecodedValue = 0U;
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CapiNative.UnsafeNativeMethods.CryptDecodeObject(65537U, pszStructType, pbEncoded, (uint)pbEncoded.Length, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			safeLocalAllocHandle = CapiNative.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CapiNative.UnsafeNativeMethods.CryptDecodeObject(65537U, pszStructType, pbEncoded, (uint)pbEncoded.Length, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			decodedValue = safeLocalAllocHandle;
			cbDecodedValue = num;
			return true;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00018F04 File Offset: 0x00017104
		[SecuritySafeCritical]
		internal static CapiNative.CRYPT_OID_INFO CryptFindOIDInfo([In] uint dwKeyType, [In] IntPtr pvKey, [In] OidGroup dwGroupId)
		{
			if (pvKey == IntPtr.Zero)
			{
				throw new ArgumentNullException("pvKey");
			}
			CapiNative.CRYPT_OID_INFO result = new CapiNative.CRYPT_OID_INFO(Marshal.SizeOf(typeof(CapiNative.CRYPT_OID_INFO)));
			IntPtr intPtr = CapiNative.UnsafeNativeMethods.CryptFindOIDInfo(dwKeyType, pvKey, dwGroupId);
			if (intPtr != IntPtr.Zero)
			{
				result = (CapiNative.CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr, typeof(CapiNative.CRYPT_OID_INFO));
			}
			return result;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00018F6C File Offset: 0x0001716C
		[SecuritySafeCritical]
		internal static CapiNative.CRYPT_OID_INFO CryptFindOIDInfo([In] uint dwKeyType, [In] SafeLocalAllocHandle pvKey, [In] OidGroup dwGroupId)
		{
			if (pvKey == null)
			{
				throw new ArgumentNullException("pvKey");
			}
			if (pvKey.IsInvalid)
			{
				throw new CryptographicException("SR.GetString(SR.Cryptography_InvalidHandle)", "pvKey");
			}
			CapiNative.CRYPT_OID_INFO result = new CapiNative.CRYPT_OID_INFO(Marshal.SizeOf(typeof(CapiNative.CRYPT_OID_INFO)));
			IntPtr intPtr = CapiNative.UnsafeNativeMethods.CryptFindOIDInfo(dwKeyType, pvKey, dwGroupId);
			if (intPtr != IntPtr.Zero)
			{
				result = (CapiNative.CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr, typeof(CapiNative.CRYPT_OID_INFO));
			}
			return result;
		}

		// Token: 0x04000632 RID: 1586
		internal const uint ALG_CLASS_SIGNATURE = 8192U;

		// Token: 0x04000633 RID: 1587
		internal const uint ALG_TYPE_RSA = 1024U;

		// Token: 0x04000634 RID: 1588
		internal const uint ALG_SID_RSA_ANY = 0U;

		// Token: 0x04000635 RID: 1589
		internal const uint ALG_SID_DSS_ANY = 0U;

		// Token: 0x04000636 RID: 1590
		internal const uint ALG_TYPE_DSS = 512U;

		// Token: 0x04000637 RID: 1591
		internal const uint ALG_CLASS_KEY_EXCHANGE = 40960U;

		// Token: 0x04000638 RID: 1592
		internal const uint CALG_RSA_SIGN = 9216U;

		// Token: 0x04000639 RID: 1593
		internal const uint CALG_DSS_SIGN = 8704U;

		// Token: 0x0400063A RID: 1594
		internal const uint CALG_RSA_KEYX = 41984U;

		// Token: 0x0400063B RID: 1595
		internal const uint CNG_RSA_PUBLIC_KEY_BLOB = 72U;

		// Token: 0x0400063C RID: 1596
		internal const uint X509_DSS_PUBLICKEY = 38U;

		// Token: 0x0400063D RID: 1597
		internal const uint X509_DSS_PARAMETERS = 39U;

		// Token: 0x0400063E RID: 1598
		internal const uint X509_ASN_ENCODING = 1U;

		// Token: 0x0400063F RID: 1599
		internal const uint PKCS_7_ASN_ENCODING = 65536U;

		// Token: 0x04000640 RID: 1600
		internal const uint CRYPT_OID_INFO_OID_KEY = 1U;

		// Token: 0x04000641 RID: 1601
		internal const uint LMEM_FIXED = 0U;

		// Token: 0x04000642 RID: 1602
		internal const uint LMEM_ZEROINIT = 64U;

		// Token: 0x0200032F RID: 815
		internal enum AlgorithmClass
		{
			// Token: 0x04000EA3 RID: 3747
			DataEncryption = 24576,
			// Token: 0x04000EA4 RID: 3748
			Hash = 32768
		}

		// Token: 0x02000330 RID: 816
		internal enum AlgorithmType
		{
			// Token: 0x04000EA6 RID: 3750
			Any,
			// Token: 0x04000EA7 RID: 3751
			Block = 1536
		}

		// Token: 0x02000331 RID: 817
		internal enum AlgorithmSubId
		{
			// Token: 0x04000EA9 RID: 3753
			MD5 = 3,
			// Token: 0x04000EAA RID: 3754
			Sha1,
			// Token: 0x04000EAB RID: 3755
			Sha256 = 12,
			// Token: 0x04000EAC RID: 3756
			Sha384,
			// Token: 0x04000EAD RID: 3757
			Sha512,
			// Token: 0x04000EAE RID: 3758
			Aes128 = 14,
			// Token: 0x04000EAF RID: 3759
			Aes192,
			// Token: 0x04000EB0 RID: 3760
			Aes256
		}

		// Token: 0x02000332 RID: 818
		internal enum AlgorithmId
		{
			// Token: 0x04000EB2 RID: 3762
			None,
			// Token: 0x04000EB3 RID: 3763
			Aes128 = 26126,
			// Token: 0x04000EB4 RID: 3764
			Aes192,
			// Token: 0x04000EB5 RID: 3765
			Aes256,
			// Token: 0x04000EB6 RID: 3766
			MD5 = 32771,
			// Token: 0x04000EB7 RID: 3767
			Sha1,
			// Token: 0x04000EB8 RID: 3768
			Sha256 = 32780,
			// Token: 0x04000EB9 RID: 3769
			Sha384,
			// Token: 0x04000EBA RID: 3770
			Sha512
		}

		// Token: 0x02000333 RID: 819
		[Flags]
		internal enum CryptAcquireContextFlags
		{
			// Token: 0x04000EBC RID: 3772
			None = 0,
			// Token: 0x04000EBD RID: 3773
			VerifyContext = -268435456
		}

		// Token: 0x02000334 RID: 820
		internal enum ErrorCode
		{
			// Token: 0x04000EBF RID: 3775
			Success,
			// Token: 0x04000EC0 RID: 3776
			MoreData = 234,
			// Token: 0x04000EC1 RID: 3777
			NoMoreItems = 259,
			// Token: 0x04000EC2 RID: 3778
			BadData = -2146893819,
			// Token: 0x04000EC3 RID: 3779
			BadAlgorithmId = -2146893816,
			// Token: 0x04000EC4 RID: 3780
			ProviderTypeNotDefined = -2146893801,
			// Token: 0x04000EC5 RID: 3781
			KeysetNotDefined = -2146893799
		}

		// Token: 0x02000335 RID: 821
		internal enum HashParameter
		{
			// Token: 0x04000EC7 RID: 3783
			None,
			// Token: 0x04000EC8 RID: 3784
			AlgorithmId,
			// Token: 0x04000EC9 RID: 3785
			HashValue,
			// Token: 0x04000ECA RID: 3786
			HashSize = 4
		}

		// Token: 0x02000336 RID: 822
		internal enum KeyBlobType : byte
		{
			// Token: 0x04000ECC RID: 3788
			PlainText = 8
		}

		// Token: 0x02000337 RID: 823
		[Flags]
		internal enum KeyFlags
		{
			// Token: 0x04000ECE RID: 3790
			None = 0,
			// Token: 0x04000ECF RID: 3791
			Exportable = 1
		}

		// Token: 0x02000338 RID: 824
		internal enum KeyParameter
		{
			// Token: 0x04000ED1 RID: 3793
			None,
			// Token: 0x04000ED2 RID: 3794
			IV,
			// Token: 0x04000ED3 RID: 3795
			Mode = 4,
			// Token: 0x04000ED4 RID: 3796
			ModeBits
		}

		// Token: 0x02000339 RID: 825
		internal static class ProviderNames
		{
			// Token: 0x04000ED5 RID: 3797
			public const string MicrosoftEnhancedRsaAes = "Microsoft Enhanced RSA and AES Cryptographic Provider";

			// Token: 0x04000ED6 RID: 3798
			public const string MicrosoftEnhancedRsaAesPrototype = "Microsoft Enhanced RSA and AES Cryptographic Provider (Prototype)";
		}

		// Token: 0x0200033A RID: 826
		internal enum ProviderParameter
		{
			// Token: 0x04000ED8 RID: 3800
			None,
			// Token: 0x04000ED9 RID: 3801
			EnumerateAlgorithms
		}

		// Token: 0x0200033B RID: 827
		[Flags]
		internal enum ProviderParameterFlags
		{
			// Token: 0x04000EDB RID: 3803
			None = 0,
			// Token: 0x04000EDC RID: 3804
			RestartEnumeration = 1
		}

		// Token: 0x0200033C RID: 828
		internal enum ProviderType
		{
			// Token: 0x04000EDE RID: 3806
			None,
			// Token: 0x04000EDF RID: 3807
			RsaAes = 24
		}

		// Token: 0x0200033D RID: 829
		internal struct BLOBHEADER
		{
			// Token: 0x04000EE0 RID: 3808
			public CapiNative.KeyBlobType bType;

			// Token: 0x04000EE1 RID: 3809
			public byte bVersion;

			// Token: 0x04000EE2 RID: 3810
			public short reserved;

			// Token: 0x04000EE3 RID: 3811
			public CapiNative.AlgorithmId aiKeyAlg;
		}

		// Token: 0x0200033E RID: 830
		internal struct CRYPTOAPI_BLOB
		{
			// Token: 0x04000EE4 RID: 3812
			public int cbData;

			// Token: 0x04000EE5 RID: 3813
			public IntPtr pbData;
		}

		// Token: 0x0200033F RID: 831
		internal struct CERT_DSS_PARAMETERS
		{
			// Token: 0x04000EE6 RID: 3814
			public CapiNative.CRYPTOAPI_BLOB p;

			// Token: 0x04000EE7 RID: 3815
			public CapiNative.CRYPTOAPI_BLOB q;

			// Token: 0x04000EE8 RID: 3816
			public CapiNative.CRYPTOAPI_BLOB g;
		}

		// Token: 0x02000340 RID: 832
		internal struct PROV_ENUMALGS
		{
			// Token: 0x04000EE9 RID: 3817
			public CapiNative.AlgorithmId aiAlgId;

			// Token: 0x04000EEA RID: 3818
			public int dwBitLen;

			// Token: 0x04000EEB RID: 3819
			public int dwNameLen;

			// Token: 0x04000EEC RID: 3820
			[FixedBuffer(typeof(byte), 20)]
			public CapiNative.PROV_ENUMALGS.<szName>e__FixedBuffer szName;

			// Token: 0x0200048E RID: 1166
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 20)]
			public struct <szName>e__FixedBuffer
			{
				// Token: 0x040013D9 RID: 5081
				public byte FixedElementField;
			}
		}

		// Token: 0x02000341 RID: 833
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_OID_INFO
		{
			// Token: 0x06001B17 RID: 6935 RVA: 0x00063135 File Offset: 0x00061335
			internal CRYPT_OID_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.pszOID = null;
				this.pwszName = null;
				this.dwGroupId = 0U;
				this.Algid = 0U;
				this.ExtraInfo = default(CapiNative.CRYPTOAPI_BLOB);
			}

			// Token: 0x04000EED RID: 3821
			internal uint cbSize;

			// Token: 0x04000EEE RID: 3822
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszOID;

			// Token: 0x04000EEF RID: 3823
			internal string pwszName;

			// Token: 0x04000EF0 RID: 3824
			internal uint dwGroupId;

			// Token: 0x04000EF1 RID: 3825
			internal uint Algid;

			// Token: 0x04000EF2 RID: 3826
			internal CapiNative.CRYPTOAPI_BLOB ExtraInfo;
		}

		// Token: 0x02000342 RID: 834
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		internal static class UnsafeNativeMethods
		{
			// Token: 0x06001B18 RID: 6936
			[DllImport("clr")]
			public static extern int _AxlPublicKeyBlobToPublicKeyToken(ref CapiNative.CRYPTOAPI_BLOB pCspPublicKeyBlob, out SafeAxlBufferHandle ppwszPublicKeyToken);

			// Token: 0x06001B19 RID: 6937
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptAcquireContext(out SafeCspHandle phProv, string pszContainer, string pszProvider, CapiNative.ProviderType dwProvType, CapiNative.CryptAcquireContextFlags dwFlags);

			// Token: 0x06001B1A RID: 6938
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptCreateHash(SafeCspHandle hProv, CapiNative.AlgorithmId Algid, SafeCapiKeyHandle hKey, int dwFlags, out SafeCapiHashHandle phHash);

			// Token: 0x06001B1B RID: 6939
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptDecrypt(SafeCapiKeyHandle hKey, SafeCapiHashHandle hHash, [MarshalAs(UnmanagedType.Bool)] bool Final, int dwFlags, IntPtr pbData, [In] [Out] ref int pdwDataLen);

			// Token: 0x06001B1C RID: 6940
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptDuplicateKey(SafeCapiKeyHandle hKey, IntPtr pdwReserved, int dwFlags, out SafeCapiKeyHandle phKey);

			// Token: 0x06001B1D RID: 6941
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptEncrypt(SafeCapiKeyHandle hKey, SafeCapiHashHandle hHash, [MarshalAs(UnmanagedType.Bool)] bool Final, int dwFlags, IntPtr pbData, [In] [Out] ref int pdwDataLen, int dwBufLen);

			// Token: 0x06001B1E RID: 6942
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptExportKey(SafeCapiKeyHandle hKey, SafeCapiKeyHandle hExpKey, int dwBlobType, int dwExportFlags, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbData, [In] [Out] ref int pdwDataLen);

			// Token: 0x06001B1F RID: 6943
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptGenKey(SafeCspHandle hProv, CapiNative.AlgorithmId Algid, CapiNative.KeyFlags dwFlags, out SafeCapiKeyHandle phKey);

			// Token: 0x06001B20 RID: 6944
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptGenRandom(SafeCspHandle hProv, int dwLen, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbBuffer);

			// Token: 0x06001B21 RID: 6945
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptGetHashParam(SafeCapiHashHandle hHash, CapiNative.HashParameter dwParam, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbData, [In] [Out] ref int pdwDataLen, int dwFlags);

			// Token: 0x06001B22 RID: 6946
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptGetProvParam(SafeCspHandle hProv, CapiNative.ProviderParameter dwParam, IntPtr pbData, [In] [Out] ref int pdwDataLen, CapiNative.ProviderParameterFlags dwFlags);

			// Token: 0x06001B23 RID: 6947
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public unsafe static extern bool CryptHashData(SafeCapiHashHandle hHash, byte* pbData, int dwDataLen, int dwFlags);

			// Token: 0x06001B24 RID: 6948
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptImportKey(SafeCspHandle hProv, [MarshalAs(UnmanagedType.LPArray)] byte[] pbData, int dwDataLen, SafeCapiKeyHandle hPubKey, CapiNative.KeyFlags dwFlags, out SafeCapiKeyHandle phKey);

			// Token: 0x06001B25 RID: 6949
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool CryptSetKeyParam(SafeCapiKeyHandle hKey, CapiNative.KeyParameter dwParam, [MarshalAs(UnmanagedType.LPArray)] byte[] pbData, int dwFlags);

			// Token: 0x06001B26 RID: 6950
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern IntPtr CryptFindOIDInfo([In] uint dwKeyType, [In] IntPtr pvKey, [In] OidGroup dwGroupId);

			// Token: 0x06001B27 RID: 6951
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern IntPtr CryptFindOIDInfo([In] uint dwKeyType, [In] SafeLocalAllocHandle pvKey, [In] OidGroup dwGroupId);

			// Token: 0x06001B28 RID: 6952
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptDecodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] IntPtr pbEncoded, [In] uint cbEncoded, [In] uint dwFlags, [In] [Out] SafeLocalAllocHandle pvStructInfo, [In] [Out] IntPtr pcbStructInfo);

			// Token: 0x06001B29 RID: 6953
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptDecodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] byte[] pbEncoded, [In] uint cbEncoded, [In] uint dwFlags, [In] [Out] SafeLocalAllocHandle pvStructInfo, [In] [Out] IntPtr pcbStructInfo);

			// Token: 0x06001B2A RID: 6954
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeLocalAllocHandle LocalAlloc([In] uint uFlags, [In] IntPtr sizetdwBytes);
		}
	}
}
