using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000454 RID: 1108
	[SuppressUnmanagedCodeSecurity]
	internal abstract class CAPIUnsafe : CAPISafe
	{
		// Token: 0x06002933 RID: 10547
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "CryptAcquireContextA")]
		protected internal static extern bool CryptAcquireContext([In] [Out] ref SafeCryptProvHandle hCryptProv, [MarshalAs(UnmanagedType.LPStr)] [In] string pszContainer, [MarshalAs(UnmanagedType.LPStr)] [In] string pszProvider, [In] uint dwProvType, [In] uint dwFlags);

		// Token: 0x06002934 RID: 10548
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertAddCertificateContextToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext);

		// Token: 0x06002935 RID: 10549
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertAddCertificateLinkToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext);

		// Token: 0x06002936 RID: 10550
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertDeleteCertificateFromStore([In] SafeCertContextHandle pCertContext);

		// Token: 0x06002937 RID: 10551
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern IntPtr CertEnumCertificatesInStore([In] SafeCertStoreHandle hCertStore, [In] IntPtr pPrevCertContext);

		// Token: 0x06002938 RID: 10552
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern SafeCertContextHandle CertEnumCertificatesInStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pPrevCertContext);

		// Token: 0x06002939 RID: 10553
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern SafeCertContextHandle CertFindCertificateInStore([In] SafeCertStoreHandle hCertStore, [In] uint dwCertEncodingType, [In] uint dwFindFlags, [In] uint dwFindType, [In] IntPtr pvFindPara, [In] SafeCertContextHandle pPrevCertContext);

		// Token: 0x0600293A RID: 10554
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		protected internal static extern SafeCertStoreHandle CertOpenStore([In] IntPtr lpszStoreProvider, [In] uint dwMsgAndCertEncodingType, [In] IntPtr hCryptProv, [In] uint dwFlags, [In] string pvPara);

		// Token: 0x0600293B RID: 10555
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertSaveStore([In] SafeCertStoreHandle hCertStore, [In] uint dwMsgAndCertEncodingType, [In] uint dwSaveAs, [In] uint dwSaveTo, [In] [Out] IntPtr pvSaveToPara, [In] uint dwFlags);

		// Token: 0x0600293C RID: 10556
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertSetCertificateContextProperty([In] IntPtr pCertContext, [In] uint dwPropId, [In] uint dwFlags, [In] IntPtr pvData);

		// Token: 0x0600293D RID: 10557
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertSetCertificateContextProperty([In] SafeCertContextHandle pCertContext, [In] uint dwPropId, [In] uint dwFlags, [In] IntPtr pvData);

		// Token: 0x0600293E RID: 10558
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CertSetCertificateContextProperty([In] SafeCertContextHandle pCertContext, [In] uint dwPropId, [In] uint dwFlags, [In] SafeLocalAllocHandle safeLocalAllocHandle);

		// Token: 0x0600293F RID: 10559
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern SafeCertContextHandle CertCreateSelfSignCertificate([In] SafeCryptProvHandle hProv, [In] IntPtr pSubjectIssuerBlob, [In] uint dwFlags, [In] IntPtr pKeyProvInfo, [In] IntPtr pSignatureAlgorithm, [In] IntPtr pStartTime, [In] IntPtr pEndTime, [In] IntPtr pExtensions);

		// Token: 0x06002940 RID: 10560
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CryptMsgControl([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwFlags, [In] uint dwCtrlType, [In] IntPtr pvCtrlPara);

		// Token: 0x06002941 RID: 10561
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CryptMsgCountersign([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwIndex, [In] uint cCountersigners, [In] IntPtr rgCountersigners);

		// Token: 0x06002942 RID: 10562
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [In] IntPtr pszInnerContentObjID, [In] IntPtr pStreamInfo);

		// Token: 0x06002943 RID: 10563
		[DllImport("crypt32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [MarshalAs(UnmanagedType.LPStr)] [In] string pszInnerContentObjID, [In] IntPtr pStreamInfo);

		// Token: 0x06002944 RID: 10564
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CryptQueryObject([In] uint dwObjectType, [In] IntPtr pvObject, [In] uint dwExpectedContentTypeFlags, [In] uint dwExpectedFormatTypeFlags, [In] uint dwFlags, [Out] IntPtr pdwMsgAndCertEncodingType, [Out] IntPtr pdwContentType, [Out] IntPtr pdwFormatType, [In] [Out] IntPtr phCertStore, [In] [Out] IntPtr phMsg, [In] [Out] IntPtr ppvContext);

		// Token: 0x06002945 RID: 10565
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		protected internal static extern bool CryptQueryObject([In] uint dwObjectType, [In] IntPtr pvObject, [In] uint dwExpectedContentTypeFlags, [In] uint dwExpectedFormatTypeFlags, [In] uint dwFlags, [Out] IntPtr pdwMsgAndCertEncodingType, [Out] IntPtr pdwContentType, [Out] IntPtr pdwFormatType, [In] [Out] ref SafeCertStoreHandle phCertStore, [In] [Out] IntPtr phMsg, [In] [Out] IntPtr ppvContext);

		// Token: 0x06002946 RID: 10566
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptProtectData([In] IntPtr pDataIn, [In] string szDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In] [Out] IntPtr pDataBlob);

		// Token: 0x06002947 RID: 10567
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptUnprotectData([In] IntPtr pDataIn, [In] IntPtr ppszDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In] [Out] IntPtr pDataBlob);

		// Token: 0x06002948 RID: 10568
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		protected internal static extern bool PFXExportCertStore([In] SafeCertStoreHandle hStore, [In] [Out] IntPtr pPFX, [In] string szPassword, [In] uint dwFlags);

		// Token: 0x06002949 RID: 10569
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		protected internal static extern SafeCertStoreHandle PFXImportCertStore([In] IntPtr pPFX, [In] string szPassword, [In] uint dwFlags);
	}
}
