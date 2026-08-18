using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Win32.SafeHandles;

// Token: 0x02000002 RID: 2
internal static class Interop
{
	// Token: 0x020002D3 RID: 723
	internal static class Libraries
	{
		// Token: 0x04000CD6 RID: 3286
		internal const string Crypt32 = "crypt32.dll";

		// Token: 0x04000CD7 RID: 3287
		internal const string NCrypt = "ncrypt.dll";
	}

	// Token: 0x020002D4 RID: 724
	internal static class Crypt32
	{
		// Token: 0x06001A32 RID: 6706 RVA: 0x00060680 File Offset: 0x0005E880
		[SecuritySafeCritical]
		internal static Interop.Crypt32.CRYPT_OID_INFO FindOidInfo(Interop.Crypt32.CryptOidInfoKeyType keyType, string key, OidGroup group, bool fallBackToAllGroups)
		{
			IntPtr intPtr = IntPtr.Zero;
			Interop.Crypt32.CRYPT_OID_INFO result;
			try
			{
				if (keyType == Interop.Crypt32.CryptOidInfoKeyType.CRYPT_OID_INFO_OID_KEY)
				{
					intPtr = Marshal.StringToCoTaskMemAnsi(key);
				}
				else
				{
					if (keyType != Interop.Crypt32.CryptOidInfoKeyType.CRYPT_OID_INFO_NAME_KEY)
					{
						throw new NotSupportedException();
					}
					intPtr = Marshal.StringToCoTaskMemUni(key);
				}
				if (!Interop.Crypt32.OidGroupWillNotUseActiveDirectory(group))
				{
					OidGroup group2 = group | (OidGroup)(-2147483648);
					IntPtr intPtr2 = Interop.Crypt32.CryptFindOIDInfo(keyType, intPtr, group2);
					if (intPtr2 != IntPtr.Zero)
					{
						return (Interop.Crypt32.CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr2, typeof(Interop.Crypt32.CRYPT_OID_INFO));
					}
				}
				IntPtr intPtr3 = Interop.Crypt32.CryptFindOIDInfo(keyType, intPtr, group);
				if (intPtr3 != IntPtr.Zero)
				{
					result = (Interop.Crypt32.CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr3, typeof(Interop.Crypt32.CRYPT_OID_INFO));
				}
				else
				{
					if (fallBackToAllGroups && group != OidGroup.All)
					{
						IntPtr intPtr4 = Interop.Crypt32.CryptFindOIDInfo(keyType, intPtr, OidGroup.All);
						if (intPtr4 != IntPtr.Zero)
						{
							return (Interop.Crypt32.CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr4, typeof(Interop.Crypt32.CRYPT_OID_INFO));
						}
					}
					result = new Interop.Crypt32.CRYPT_OID_INFO
					{
						AlgId = -1
					};
				}
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

		// Token: 0x06001A33 RID: 6707 RVA: 0x00060798 File Offset: 0x0005E998
		[SecuritySafeCritical]
		public static Interop.Crypt32.CRYPT_OID_INFO FindAlgIdOidInfo(int algId)
		{
			int num = algId;
			IntPtr intPtr = Interop.Crypt32.CryptFindOIDInfo(Interop.Crypt32.CryptOidInfoKeyType.CRYPT_OID_INFO_ALGID_KEY, ref num, OidGroup.HashAlgorithm);
			if (intPtr != IntPtr.Zero)
			{
				return (Interop.Crypt32.CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr, typeof(Interop.Crypt32.CRYPT_OID_INFO));
			}
			return new Interop.Crypt32.CRYPT_OID_INFO
			{
				AlgId = -1
			};
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000607E5 File Offset: 0x0005E9E5
		private static bool OidGroupWillNotUseActiveDirectory(OidGroup group)
		{
			return group == OidGroup.HashAlgorithm || group == OidGroup.EncryptionAlgorithm || group == OidGroup.PublicKeyAlgorithm || group == OidGroup.SignatureAlgorithm || group == OidGroup.Attribute || group == OidGroup.ExtensionOrAttribute || group == OidGroup.KeyDerivationFunction;
		}

		// Token: 0x06001A35 RID: 6709
		[SecurityCritical]
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr CryptFindOIDInfo(Interop.Crypt32.CryptOidInfoKeyType dwKeyType, IntPtr pvKey, OidGroup group);

		// Token: 0x06001A36 RID: 6710
		[SecurityCritical]
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr CryptFindOIDInfo(Interop.Crypt32.CryptOidInfoKeyType dwKeyType, ref int pvKey, OidGroup group);

		// Token: 0x0200046E RID: 1134
		internal struct CRYPT_OID_INFO
		{
			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x0600201D RID: 8221 RVA: 0x00070261 File Offset: 0x0006E461
			public string OID
			{
				[SecuritySafeCritical]
				get
				{
					return Marshal.PtrToStringAnsi(this.pszOID);
				}
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x0600201E RID: 8222 RVA: 0x0007026E File Offset: 0x0006E46E
			public string Name
			{
				[SecuritySafeCritical]
				get
				{
					return Marshal.PtrToStringUni(this.pwszName);
				}
			}

			// Token: 0x04001351 RID: 4945
			public int cbSize;

			// Token: 0x04001352 RID: 4946
			public IntPtr pszOID;

			// Token: 0x04001353 RID: 4947
			public IntPtr pwszName;

			// Token: 0x04001354 RID: 4948
			public OidGroup dwGroupId;

			// Token: 0x04001355 RID: 4949
			public int AlgId;

			// Token: 0x04001356 RID: 4950
			public int cbData;

			// Token: 0x04001357 RID: 4951
			public IntPtr pbData;
		}

		// Token: 0x0200046F RID: 1135
		internal enum CryptOidInfoKeyType
		{
			// Token: 0x04001359 RID: 4953
			CRYPT_OID_INFO_OID_KEY = 1,
			// Token: 0x0400135A RID: 4954
			CRYPT_OID_INFO_NAME_KEY,
			// Token: 0x0400135B RID: 4955
			CRYPT_OID_INFO_ALGID_KEY,
			// Token: 0x0400135C RID: 4956
			CRYPT_OID_INFO_SIGN_KEY,
			// Token: 0x0400135D RID: 4957
			CRYPT_OID_INFO_CNG_ALGID_KEY,
			// Token: 0x0400135E RID: 4958
			CRYPT_OID_INFO_CNG_SIGN_KEY
		}
	}

	// Token: 0x020002D5 RID: 725
	internal static class NCrypt
	{
		// Token: 0x06001A37 RID: 6711
		[SecurityCritical]
		[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern Interop.NCrypt.ErrorCode NCryptEncrypt(SafeNCryptKeyHandle hKey, byte* pbInput, int cbInput, void* pPaddingInfo, byte* pbOutput, int cbOutput, out int pcbResult, Interop.NCrypt.AsymmetricPaddingMode dwFlags);

		// Token: 0x06001A38 RID: 6712
		[SecurityCritical]
		[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern Interop.NCrypt.ErrorCode NCryptDecrypt(SafeNCryptKeyHandle hKey, byte* pbInput, int cbInput, void* pPaddingInfo, byte* pbOutput, int cbOutput, out int pcbResult, Interop.NCrypt.AsymmetricPaddingMode dwFlags);

		// Token: 0x04000CD8 RID: 3288
		internal const string NCRYPT_3DES_ALGORITHM = "3DES";

		// Token: 0x04000CD9 RID: 3289
		internal const string NCRYPT_AES_ALGORITHM = "AES";

		// Token: 0x04000CDA RID: 3290
		internal const string NCRYPT_CIPHER_KEY_BLOB = "CipherKeyBlob";

		// Token: 0x04000CDB RID: 3291
		internal const int NCRYPT_CIPHER_KEY_BLOB_MAGIC = 1380470851;

		// Token: 0x04000CDC RID: 3292
		internal const string NCRYPT_CHAINING_MODE_PROPERTY = "Chaining Mode";

		// Token: 0x04000CDD RID: 3293
		internal const string NCRYPT_INITIALIZATION_VECTOR = "IV";

		// Token: 0x02000470 RID: 1136
		internal enum ErrorCode
		{
			// Token: 0x04001360 RID: 4960
			ERROR_SUCCESS,
			// Token: 0x04001361 RID: 4961
			NTE_BAD_SIGNATURE = -2146893818,
			// Token: 0x04001362 RID: 4962
			NTE_NOT_FOUND = -2146893807,
			// Token: 0x04001363 RID: 4963
			NTE_BAD_KEYSET = -2146893802,
			// Token: 0x04001364 RID: 4964
			NTE_INVALID_PARAMETER = -2146893785,
			// Token: 0x04001365 RID: 4965
			NTE_BUFFER_TOO_SMALL,
			// Token: 0x04001366 RID: 4966
			NTE_NOT_SUPPORTED,
			// Token: 0x04001367 RID: 4967
			NTE_NO_MORE_ITEMS,
			// Token: 0x04001368 RID: 4968
			E_FAIL = -2147467259
		}

		// Token: 0x02000471 RID: 1137
		internal enum AsymmetricPaddingMode
		{
			// Token: 0x0400136A RID: 4970
			None,
			// Token: 0x0400136B RID: 4971
			NCRYPT_NO_PADDING_FLAG,
			// Token: 0x0400136C RID: 4972
			NCRYPT_PAD_PKCS1_FLAG,
			// Token: 0x0400136D RID: 4973
			NCRYPT_PAD_OAEP_FLAG = 4,
			// Token: 0x0400136E RID: 4974
			NCRYPT_PAD_PSS_FLAG = 8
		}
	}

	// Token: 0x020002D6 RID: 726
	internal class BCrypt
	{
		// Token: 0x06001A39 RID: 6713 RVA: 0x00060806 File Offset: 0x0005EA06
		internal static void Emit(byte[] blob, ref int offset, byte[] value)
		{
			Buffer.BlockCopy(value, 0, blob, offset, value.Length);
			offset += value.Length;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00060820 File Offset: 0x0005EA20
		internal static byte[] Consume(byte[] blob, ref int offset, int count)
		{
			byte[] array = new byte[count];
			Buffer.BlockCopy(blob, offset, array, 0, count);
			offset += count;
			return array;
		}

		// Token: 0x04000CDE RID: 3294
		internal const string BCRYPT_CHAIN_MODE_CBC = "ChainingModeCBC";

		// Token: 0x04000CDF RID: 3295
		internal const string BCRYPT_CHAIN_MODE_ECB = "ChainingModeECB";

		// Token: 0x04000CE0 RID: 3296
		internal const int BCRYPT_KEY_DATA_BLOB_MAGIC = 1296188491;

		// Token: 0x04000CE1 RID: 3297
		internal const int BCRYPT_KEY_DATA_BLOB_VERSION1 = 1;

		// Token: 0x04000CE2 RID: 3298
		internal const int BCRYPTBUFFER_VERSION = 0;

		// Token: 0x04000CE3 RID: 3299
		internal const int BCRYPT_ECC_PARAMETER_HEADER_V1 = 1;

		// Token: 0x02000472 RID: 1138
		internal enum KeyBlobMagicNumber
		{
			// Token: 0x04001370 RID: 4976
			BCRYPT_ECDH_PUBLIC_P256_MAGIC = 827016005,
			// Token: 0x04001371 RID: 4977
			BCRYPT_ECDH_PRIVATE_P256_MAGIC = 843793221,
			// Token: 0x04001372 RID: 4978
			BCRYPT_ECDH_PUBLIC_P384_MAGIC = 860570437,
			// Token: 0x04001373 RID: 4979
			BCRYPT_ECDH_PRIVATE_P384_MAGIC = 877347653,
			// Token: 0x04001374 RID: 4980
			BCRYPT_ECDH_PUBLIC_P521_MAGIC = 894124869,
			// Token: 0x04001375 RID: 4981
			BCRYPT_ECDH_PRIVATE_P521_MAGIC = 910902085,
			// Token: 0x04001376 RID: 4982
			BCRYPT_ECDH_PUBLIC_GENERIC_MAGIC = 1347109701,
			// Token: 0x04001377 RID: 4983
			BCRYPT_ECDH_PRIVATE_GENERIC_MAGIC = 1447772997,
			// Token: 0x04001378 RID: 4984
			BCRYPT_ECDSA_PUBLIC_P256_MAGIC = 827540293,
			// Token: 0x04001379 RID: 4985
			BCRYPT_ECDSA_PRIVATE_P256_MAGIC = 844317509,
			// Token: 0x0400137A RID: 4986
			BCRYPT_ECDSA_PUBLIC_P384_MAGIC = 861094725,
			// Token: 0x0400137B RID: 4987
			BCRYPT_ECDSA_PRIVATE_P384_MAGIC = 877871941,
			// Token: 0x0400137C RID: 4988
			BCRYPT_ECDSA_PUBLIC_P521_MAGIC = 894649157,
			// Token: 0x0400137D RID: 4989
			BCRYPT_ECDSA_PRIVATE_P521_MAGIC = 911426373,
			// Token: 0x0400137E RID: 4990
			BCRYPT_ECDSA_PUBLIC_GENERIC_MAGIC = 1346650949,
			// Token: 0x0400137F RID: 4991
			BCRYPT_ECDSA_PRIVATE_GENERIC_MAGIC = 1447314245,
			// Token: 0x04001380 RID: 4992
			BCRYPT_RSAPUBLIC_MAGIC = 826364754,
			// Token: 0x04001381 RID: 4993
			BCRYPT_RSAPRIVATE_MAGIC = 843141970,
			// Token: 0x04001382 RID: 4994
			BCRYPT_RSAFULLPRIVATE_MAGIC = 859919186,
			// Token: 0x04001383 RID: 4995
			BCRYPT_KEY_DATA_BLOB_MAGIC = 1296188491
		}

		// Token: 0x02000473 RID: 1139
		internal struct BCRYPT_ECCKEY_BLOB
		{
			// Token: 0x04001384 RID: 4996
			internal Interop.BCrypt.KeyBlobMagicNumber Magic;

			// Token: 0x04001385 RID: 4997
			internal int cbKey;
		}

		// Token: 0x02000474 RID: 1140
		internal enum ECC_CURVE_TYPE_ENUM
		{
			// Token: 0x04001387 RID: 4999
			BCRYPT_ECC_PRIME_SHORT_WEIERSTRASS_CURVE = 1,
			// Token: 0x04001388 RID: 5000
			BCRYPT_ECC_PRIME_TWISTED_EDWARDS_CURVE,
			// Token: 0x04001389 RID: 5001
			BCRYPT_ECC_PRIME_MONTGOMERY_CURVE
		}

		// Token: 0x02000475 RID: 1141
		internal enum ECC_CURVE_ALG_ID_ENUM
		{
			// Token: 0x0400138B RID: 5003
			BCRYPT_NO_CURVE_GENERATION_ALG_ID
		}

		// Token: 0x02000476 RID: 1142
		internal struct BCRYPT_ECCFULLKEY_BLOB
		{
			// Token: 0x0400138C RID: 5004
			internal Interop.BCrypt.KeyBlobMagicNumber Magic;

			// Token: 0x0400138D RID: 5005
			internal int Version;

			// Token: 0x0400138E RID: 5006
			internal Interop.BCrypt.ECC_CURVE_TYPE_ENUM CurveType;

			// Token: 0x0400138F RID: 5007
			internal Interop.BCrypt.ECC_CURVE_ALG_ID_ENUM CurveGenerationAlgId;

			// Token: 0x04001390 RID: 5008
			internal int cbFieldLength;

			// Token: 0x04001391 RID: 5009
			internal int cbSubgroupOrder;

			// Token: 0x04001392 RID: 5010
			internal int cbCofactor;

			// Token: 0x04001393 RID: 5011
			internal int cbSeed;
		}

		// Token: 0x02000477 RID: 1143
		internal enum NCryptBufferDescriptors
		{
			// Token: 0x04001395 RID: 5013
			NCRYPTBUFFER_ECC_CURVE_NAME = 60
		}

		// Token: 0x02000478 RID: 1144
		internal struct BCryptBuffer
		{
			// Token: 0x04001396 RID: 5014
			internal int cbBuffer;

			// Token: 0x04001397 RID: 5015
			internal Interop.BCrypt.NCryptBufferDescriptors BufferType;

			// Token: 0x04001398 RID: 5016
			internal IntPtr pvBuffer;
		}

		// Token: 0x02000479 RID: 1145
		internal struct BCryptBufferDesc
		{
			// Token: 0x04001399 RID: 5017
			internal int ulVersion;

			// Token: 0x0400139A RID: 5018
			internal int cBuffers;

			// Token: 0x0400139B RID: 5019
			internal IntPtr pBuffers;
		}

		// Token: 0x0200047A RID: 1146
		internal struct BCRYPT_ECC_PARAMETER_HEADER
		{
			// Token: 0x0400139C RID: 5020
			internal int Version;

			// Token: 0x0400139D RID: 5021
			internal Interop.BCrypt.ECC_CURVE_TYPE_ENUM CurveType;

			// Token: 0x0400139E RID: 5022
			internal Interop.BCrypt.ECC_CURVE_ALG_ID_ENUM CurveGenerationAlgId;

			// Token: 0x0400139F RID: 5023
			internal int cbFieldLength;

			// Token: 0x040013A0 RID: 5024
			internal int cbSubgroupOrder;

			// Token: 0x040013A1 RID: 5025
			internal int cbCofactor;

			// Token: 0x040013A2 RID: 5026
			internal int cbSeed;
		}
	}
}
