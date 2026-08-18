using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.IdentityModel
{
	// Token: 0x0200002D RID: 45
	[SuppressUnmanagedCodeSecurity]
	internal static class CAPI
	{
		// Token: 0x06000150 RID: 336
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeCertContextHandle CertCreateCertificateContext([In] uint dwCertEncodingType, [In] IntPtr pbCertEncoded, [In] uint cbCertEncoded);

		// Token: 0x06000151 RID: 337
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeCertStoreHandle CertOpenStore([In] IntPtr lpszStoreProvider, [In] uint dwMsgAndCertEncodingType, [In] IntPtr hCryptProv, [In] uint dwFlags, [In] string pvPara);

		// Token: 0x06000152 RID: 338
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		internal static extern bool CertCloseStore([In] IntPtr hCertStore, [In] uint dwFlags);

		// Token: 0x06000153 RID: 339
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		internal static extern bool CertFreeCertificateContext([In] IntPtr pCertContext);

		// Token: 0x06000154 RID: 340
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		internal static extern SafeCertContextHandle CertFindCertificateInStore([In] SafeCertStoreHandle hCertStore, [In] uint dwCertEncodingType, [In] uint dwFindFlags, [In] uint dwFindType, [In] SafeHGlobalHandle pvFindPara, [In] SafeCertContextHandle pPrevCertContext);

		// Token: 0x06000155 RID: 341
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertAddCertificateLinkToStore([In] SafeCertStoreHandle hCertStore, [In] IntPtr pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext);

		// Token: 0x06000156 RID: 342
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertGetCertificateChain([In] IntPtr hChainEngine, [In] IntPtr pCertContext, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME pTime, [In] SafeCertStoreHandle hAdditionalStore, [In] ref CAPI.CERT_CHAIN_PARA pChainPara, [In] uint dwFlags, [In] IntPtr pvReserved, out SafeCertChainHandle ppChainContext);

		// Token: 0x06000157 RID: 343
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CertVerifyCertificateChainPolicy([In] IntPtr pszPolicyOID, [In] SafeCertChainHandle pChainContext, [In] ref CAPI.CERT_CHAIN_POLICY_PARA pPolicyPara, [In] [Out] ref CAPI.CERT_CHAIN_POLICY_STATUS pPolicyStatus);

		// Token: 0x06000158 RID: 344
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		internal static extern void CertFreeCertificateChain(IntPtr handle);

		// Token: 0x06000159 RID: 345
		[DllImport("bcrypt.dll", SetLastError = true)]
		internal static extern int BCryptGetFipsAlgorithmMode([MarshalAs(UnmanagedType.U1)] out bool pfEnabled);

		// Token: 0x040000ED RID: 237
		internal const string CRYPT32 = "crypt32.dll";

		// Token: 0x040000EE RID: 238
		internal const string BCRYPT = "bcrypt.dll";

		// Token: 0x040000EF RID: 239
		internal const string SubjectKeyIdentifierOid = "2.5.29.14";

		// Token: 0x040000F0 RID: 240
		internal const int S_OK = 0;

		// Token: 0x040000F1 RID: 241
		internal const int S_FALSE = 1;

		// Token: 0x040000F2 RID: 242
		internal const string szOID_CRL_DIST_POINTS = "2.5.29.31";

		// Token: 0x040000F3 RID: 243
		internal const string szOID_AUTHORITY_INFO_ACCESS = "1.3.6.1.5.5.7.1.1";

		// Token: 0x040000F4 RID: 244
		internal const uint CERT_STORE_ENUM_ARCHIVED_FLAG = 512U;

		// Token: 0x040000F5 RID: 245
		internal const uint CERT_STORE_READONLY_FLAG = 32768U;

		// Token: 0x040000F6 RID: 246
		internal const uint CERT_STORE_OPEN_EXISTING_FLAG = 16384U;

		// Token: 0x040000F7 RID: 247
		internal const uint CERT_STORE_CREATE_NEW_FLAG = 8192U;

		// Token: 0x040000F8 RID: 248
		internal const uint CERT_STORE_MAXIMUM_ALLOWED_FLAG = 4096U;

		// Token: 0x040000F9 RID: 249
		internal const uint CERT_STORE_ADD_ALWAYS = 4U;

		// Token: 0x040000FA RID: 250
		internal const uint CERT_CHAIN_POLICY_BASE = 1U;

		// Token: 0x040000FB RID: 251
		internal const uint CERT_CHAIN_POLICY_NT_AUTH = 6U;

		// Token: 0x040000FC RID: 252
		internal const uint X509_ASN_ENCODING = 1U;

		// Token: 0x040000FD RID: 253
		internal const uint PKCS_7_ASN_ENCODING = 65536U;

		// Token: 0x040000FE RID: 254
		internal const uint CERT_STORE_PROV_MEMORY = 2U;

		// Token: 0x040000FF RID: 255
		internal const uint CERT_STORE_PROV_SYSTEM = 10U;

		// Token: 0x04000100 RID: 256
		internal const uint CERT_SYSTEM_STORE_CURRENT_USER_ID = 1U;

		// Token: 0x04000101 RID: 257
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ID = 2U;

		// Token: 0x04000102 RID: 258
		internal const uint CERT_SYSTEM_STORE_LOCATION_SHIFT = 16U;

		// Token: 0x04000103 RID: 259
		internal const uint CERT_SYSTEM_STORE_CURRENT_USER = 65536U;

		// Token: 0x04000104 RID: 260
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072U;

		// Token: 0x04000105 RID: 261
		internal const uint CERT_INFO_ISSUER_FLAG = 4U;

		// Token: 0x04000106 RID: 262
		internal const uint CERT_INFO_SUBJECT_FLAG = 7U;

		// Token: 0x04000107 RID: 263
		internal const uint CERT_COMPARE_SHIFT = 16U;

		// Token: 0x04000108 RID: 264
		internal const uint CERT_COMPARE_ANY = 0U;

		// Token: 0x04000109 RID: 265
		internal const uint CERT_COMPARE_SHA1_HASH = 1U;

		// Token: 0x0400010A RID: 266
		internal const uint CERT_COMPARE_NAME_STR_A = 7U;

		// Token: 0x0400010B RID: 267
		internal const uint CERT_COMPARE_NAME_STR_W = 8U;

		// Token: 0x0400010C RID: 268
		internal const uint CERT_FIND_ANY = 0U;

		// Token: 0x0400010D RID: 269
		internal const uint CERT_FIND_SHA1_HASH = 65536U;

		// Token: 0x0400010E RID: 270
		internal const uint CERT_FIND_HASH = 65536U;

		// Token: 0x0400010F RID: 271
		internal const uint CERT_FIND_SUBJECT_STR_A = 458759U;

		// Token: 0x04000110 RID: 272
		internal const uint CERT_FIND_SUBJECT_STR_W = 524295U;

		// Token: 0x04000111 RID: 273
		internal const uint CERT_FIND_SUBJECT_STR = 524295U;

		// Token: 0x04000112 RID: 274
		internal const uint CERT_FIND_ISSUER_STR_A = 458756U;

		// Token: 0x04000113 RID: 275
		internal const uint CERT_FIND_ISSUER_STR_W = 524292U;

		// Token: 0x04000114 RID: 276
		internal const uint CERT_FIND_ISSUER_STR = 524292U;

		// Token: 0x04000115 RID: 277
		internal const uint CERT_CHAIN_REVOCATION_CHECK_END_CERT = 268435456U;

		// Token: 0x04000116 RID: 278
		internal const uint CERT_CHAIN_REVOCATION_CHECK_CHAIN = 536870912U;

		// Token: 0x04000117 RID: 279
		internal const uint CERT_CHAIN_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT = 1073741824U;

		// Token: 0x04000118 RID: 280
		internal const uint CERT_CHAIN_REVOCATION_CHECK_CACHE_ONLY = 2147483648U;

		// Token: 0x04000119 RID: 281
		internal const uint CERT_CHAIN_REVOCATION_ACCUMULATIVE_TIMEOUT = 134217728U;

		// Token: 0x0400011A RID: 282
		internal const uint CERT_CHAIN_POLICY_IGNORE_PEER_TRUST_FLAG = 4096U;

		// Token: 0x0400011B RID: 283
		internal const uint USAGE_MATCH_TYPE_AND = 0U;

		// Token: 0x0400011C RID: 284
		internal const uint USAGE_MATCH_TYPE_OR = 1U;

		// Token: 0x0400011D RID: 285
		internal const uint HCCE_CURRENT_USER = 0U;

		// Token: 0x0400011E RID: 286
		internal const uint HCCE_LOCAL_MACHINE = 1U;

		// Token: 0x0400011F RID: 287
		internal const uint CERT_TRUST_IS_PEER_TRUSTED = 2048U;

		// Token: 0x0200022C RID: 556
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CONTEXT
		{
			// Token: 0x04000F0D RID: 3853
			internal uint dwCertEncodingType;

			// Token: 0x04000F0E RID: 3854
			internal IntPtr pbCertEncoded;

			// Token: 0x04000F0F RID: 3855
			internal uint cbCertEncoded;

			// Token: 0x04000F10 RID: 3856
			internal IntPtr pCertInfo;

			// Token: 0x04000F11 RID: 3857
			internal IntPtr hCertStore;
		}

		// Token: 0x0200022D RID: 557
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPTOAPI_BLOB
		{
			// Token: 0x04000F12 RID: 3858
			internal uint cbData;

			// Token: 0x04000F13 RID: 3859
			internal IntPtr pbData;

			// Token: 0x04000F14 RID: 3860
			internal static int Size = Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB));
		}

		// Token: 0x0200022E RID: 558
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_ENHKEY_USAGE
		{
			// Token: 0x04000F15 RID: 3861
			internal uint cUsageIdentifier;

			// Token: 0x04000F16 RID: 3862
			internal IntPtr rgpszUsageIdentifier;
		}

		// Token: 0x0200022F RID: 559
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_USAGE_MATCH
		{
			// Token: 0x04000F17 RID: 3863
			internal uint dwType;

			// Token: 0x04000F18 RID: 3864
			internal CAPI.CERT_ENHKEY_USAGE Usage;
		}

		// Token: 0x02000230 RID: 560
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_PARA
		{
			// Token: 0x04000F19 RID: 3865
			internal uint cbSize;

			// Token: 0x04000F1A RID: 3866
			internal CAPI.CERT_USAGE_MATCH RequestedUsage;

			// Token: 0x04000F1B RID: 3867
			internal CAPI.CERT_USAGE_MATCH RequestedIssuancePolicy;

			// Token: 0x04000F1C RID: 3868
			internal uint dwUrlRetrievalTimeout;

			// Token: 0x04000F1D RID: 3869
			internal bool fCheckRevocationFreshnessTime;

			// Token: 0x04000F1E RID: 3870
			internal uint dwRevocationFreshnessTime;
		}

		// Token: 0x02000231 RID: 561
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_POLICY_PARA
		{
			// Token: 0x060011F2 RID: 4594 RVA: 0x0004E660 File Offset: 0x0004C860
			internal CERT_CHAIN_POLICY_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.dwFlags = 0U;
				this.pvExtraPolicyPara = IntPtr.Zero;
			}

			// Token: 0x04000F1F RID: 3871
			internal uint cbSize;

			// Token: 0x04000F20 RID: 3872
			internal uint dwFlags;

			// Token: 0x04000F21 RID: 3873
			internal IntPtr pvExtraPolicyPara;
		}

		// Token: 0x02000232 RID: 562
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_POLICY_STATUS
		{
			// Token: 0x060011F3 RID: 4595 RVA: 0x0004E67B File Offset: 0x0004C87B
			internal CERT_CHAIN_POLICY_STATUS(int size)
			{
				this.cbSize = (uint)size;
				this.dwError = 0U;
				this.lChainIndex = IntPtr.Zero;
				this.lElementIndex = IntPtr.Zero;
				this.pvExtraPolicyStatus = IntPtr.Zero;
			}

			// Token: 0x04000F22 RID: 3874
			internal uint cbSize;

			// Token: 0x04000F23 RID: 3875
			internal uint dwError;

			// Token: 0x04000F24 RID: 3876
			internal IntPtr lChainIndex;

			// Token: 0x04000F25 RID: 3877
			internal IntPtr lElementIndex;

			// Token: 0x04000F26 RID: 3878
			internal IntPtr pvExtraPolicyStatus;
		}

		// Token: 0x02000233 RID: 563
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_CONTEXT
		{
			// Token: 0x060011F4 RID: 4596 RVA: 0x0004E6AC File Offset: 0x0004C8AC
			internal CERT_CHAIN_CONTEXT(int size)
			{
				this.cbSize = (uint)size;
				this.dwErrorStatus = 0U;
				this.dwInfoStatus = 0U;
				this.cChain = 0U;
				this.rgpChain = IntPtr.Zero;
				this.cLowerQualityChainContext = 0U;
				this.rgpLowerQualityChainContext = IntPtr.Zero;
				this.fHasRevocationFreshnessTime = 0U;
				this.dwRevocationFreshnessTime = 0U;
			}

			// Token: 0x04000F27 RID: 3879
			internal uint cbSize;

			// Token: 0x04000F28 RID: 3880
			internal uint dwErrorStatus;

			// Token: 0x04000F29 RID: 3881
			internal uint dwInfoStatus;

			// Token: 0x04000F2A RID: 3882
			internal uint cChain;

			// Token: 0x04000F2B RID: 3883
			internal IntPtr rgpChain;

			// Token: 0x04000F2C RID: 3884
			internal uint cLowerQualityChainContext;

			// Token: 0x04000F2D RID: 3885
			internal IntPtr rgpLowerQualityChainContext;

			// Token: 0x04000F2E RID: 3886
			internal uint fHasRevocationFreshnessTime;

			// Token: 0x04000F2F RID: 3887
			internal uint dwRevocationFreshnessTime;
		}
	}
}
