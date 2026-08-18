using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x0200000E RID: 14
	internal static class CAPI
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003A80 File Offset: 0x00001C80
		[SecurityCritical]
		internal static byte[] BlobToByteArray(IntPtr pBlob)
		{
			CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB = (CAPI.CRYPTOAPI_BLOB)Marshal.PtrToStructure(pBlob, typeof(CAPI.CRYPTOAPI_BLOB));
			if (cryptoapi_BLOB.cbData == 0U)
			{
				return new byte[0];
			}
			return CAPI.BlobToByteArray(cryptoapi_BLOB);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003AB8 File Offset: 0x00001CB8
		[SecurityCritical]
		internal static byte[] BlobToByteArray(CAPI.CRYPTOAPI_BLOB blob)
		{
			if (blob.cbData == 0U)
			{
				return new byte[0];
			}
			byte[] array = new byte[blob.cbData];
			Marshal.Copy(blob.pbData, array, 0, array.Length);
			return array;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003AF4 File Offset: 0x00001CF4
		[SecurityCritical]
		internal unsafe static bool DecodeObject(IntPtr pszStructType, IntPtr pbEncoded, uint cbEncoded, out SafeLocalAllocHandle decodedValue, out uint cbDecodedValue)
		{
			decodedValue = SafeLocalAllocHandle.InvalidHandle;
			cbDecodedValue = 0U;
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPI.CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, cbEncoded, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPI.CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, cbEncoded, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			decodedValue = safeLocalAllocHandle;
			cbDecodedValue = num;
			return true;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003B64 File Offset: 0x00001D64
		[SecurityCritical]
		internal unsafe static bool DecodeObject(IntPtr pszStructType, byte[] pbEncoded, out SafeLocalAllocHandle decodedValue, out uint cbDecodedValue)
		{
			decodedValue = SafeLocalAllocHandle.InvalidHandle;
			cbDecodedValue = 0U;
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPI.CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, (uint)pbEncoded.Length, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPI.CAPISafe.CryptDecodeObject(65537U, pszStructType, pbEncoded, (uint)pbEncoded.Length, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			decodedValue = safeLocalAllocHandle;
			cbDecodedValue = num;
			return true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003BD4 File Offset: 0x00001DD4
		[SecuritySafeCritical]
		internal unsafe static bool EncodeObject(IntPtr lpszStructType, IntPtr pvStructInfo, out byte[] encodedData)
		{
			encodedData = new byte[0];
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPI.CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPI.CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			encodedData = new byte[num];
			Marshal.Copy(safeLocalAllocHandle.DangerousGetHandle(), encodedData, 0, (int)num);
			safeLocalAllocHandle.Dispose();
			return true;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003C50 File Offset: 0x00001E50
		[SecurityCritical]
		internal unsafe static bool EncodeObject(string lpszStructType, IntPtr pvStructInfo, out byte[] encodedData)
		{
			encodedData = new byte[0];
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPI.CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPI.CAPISafe.CryptEncodeObject(65537U, lpszStructType, pvStructInfo, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				return false;
			}
			encodedData = new byte[num];
			Marshal.Copy(safeLocalAllocHandle.DangerousGetHandle(), encodedData, 0, (int)num);
			safeLocalAllocHandle.Dispose();
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003CCB File Offset: 0x00001ECB
		internal static bool ErrorMayBeCausedByUnloadedProfile(int errorCode)
		{
			return errorCode == -2147024894 || errorCode == 2;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003CDC File Offset: 0x00001EDC
		[SecurityCritical]
		internal static SafeLocalAllocHandle LocalAlloc(uint uFlags, IntPtr sizetdwBytes)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.CAPISafe.LocalAlloc(uFlags, sizetdwBytes);
			if (safeLocalAllocHandle == null || safeLocalAllocHandle.IsInvalid)
			{
				throw new OutOfMemoryException();
			}
			return safeLocalAllocHandle;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003D04 File Offset: 0x00001F04
		[SecurityCritical]
		internal static bool CryptAcquireContext([In] [Out] ref SafeCryptProvHandle hCryptProv, [MarshalAs(UnmanagedType.LPStr)] [In] string pwszContainer, [MarshalAs(UnmanagedType.LPStr)] [In] string pwszProvider, [In] uint dwProvType, [In] uint dwFlags)
		{
			CspParameters cspParameters = new CspParameters();
			cspParameters.ProviderName = pwszProvider;
			cspParameters.KeyContainerName = pwszContainer;
			cspParameters.ProviderType = (int)dwProvType;
			cspParameters.KeyNumber = -1;
			cspParameters.Flags = (((dwFlags & 32U) == 32U) ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
			KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
			KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(cspParameters, KeyContainerPermissionFlags.Open);
			keyContainerPermission.AccessEntries.Add(accessEntry);
			keyContainerPermission.Demand();
			bool flag = CAPI.CAPIUnsafe.CryptAcquireContext(ref hCryptProv, pwszContainer, pwszProvider, dwProvType, dwFlags);
			if (!flag && Marshal.GetLastWin32Error() == -2146893802)
			{
				flag = CAPI.CAPIUnsafe.CryptAcquireContext(ref hCryptProv, pwszContainer, pwszProvider, dwProvType, dwFlags | 8U);
			}
			return flag;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003D94 File Offset: 0x00001F94
		[SecurityCritical]
		internal static bool CryptAcquireContext(ref SafeCryptProvHandle hCryptProv, IntPtr pwszContainer, IntPtr pwszProvider, uint dwProvType, uint dwFlags)
		{
			string pwszContainer2 = null;
			if (pwszContainer != IntPtr.Zero)
			{
				pwszContainer2 = Marshal.PtrToStringUni(pwszContainer);
			}
			string pwszProvider2 = null;
			if (pwszProvider != IntPtr.Zero)
			{
				pwszProvider2 = Marshal.PtrToStringUni(pwszProvider);
			}
			return CAPI.CryptAcquireContext(ref hCryptProv, pwszContainer2, pwszProvider2, dwProvType, dwFlags);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003DD8 File Offset: 0x00001FD8
		[SecurityCritical]
		internal static CAPI.CRYPT_OID_INFO CryptFindOIDInfo([In] uint dwKeyType, [In] IntPtr pvKey, [In] uint dwGroupId)
		{
			if (pvKey == IntPtr.Zero)
			{
				throw new ArgumentNullException("pvKey");
			}
			CAPI.CRYPT_OID_INFO result = new CAPI.CRYPT_OID_INFO(Marshal.SizeOf(typeof(CAPI.CRYPT_OID_INFO)));
			IntPtr intPtr = CAPI.CAPISafe.CryptFindOIDInfo(dwKeyType, pvKey, dwGroupId);
			if (intPtr != IntPtr.Zero)
			{
				result = (CAPI.CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr, typeof(CAPI.CRYPT_OID_INFO));
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003E40 File Offset: 0x00002040
		[SecurityCritical]
		internal static CAPI.CRYPT_OID_INFO CryptFindOIDInfo([In] uint dwKeyType, [In] SafeLocalAllocHandle pvKey, [In] uint dwGroupId)
		{
			if (pvKey == null)
			{
				throw new ArgumentNullException("pvKey");
			}
			if (pvKey.IsInvalid)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "pvKey");
			}
			CAPI.CRYPT_OID_INFO result = new CAPI.CRYPT_OID_INFO(Marshal.SizeOf(typeof(CAPI.CRYPT_OID_INFO)));
			IntPtr intPtr = CAPI.CAPISafe.CryptFindOIDInfo(dwKeyType, pvKey, dwGroupId);
			if (intPtr != IntPtr.Zero)
			{
				result = (CAPI.CRYPT_OID_INFO)Marshal.PtrToStructure(intPtr, typeof(CAPI.CRYPT_OID_INFO));
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003EBB File Offset: 0x000020BB
		[SecurityCritical]
		internal static bool CryptMsgControl([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwFlags, [In] uint dwCtrlType, [In] IntPtr pvCtrlPara)
		{
			return CAPI.CAPIUnsafe.CryptMsgControl(hCryptMsg, dwFlags, dwCtrlType, pvCtrlPara);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003EC6 File Offset: 0x000020C6
		[SecurityCritical]
		internal static bool CryptMsgCountersign([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwIndex, [In] uint cCountersigners, [In] IntPtr rgCountersigners)
		{
			return CAPI.CAPIUnsafe.CryptMsgCountersign(hCryptMsg, dwIndex, cCountersigners, rgCountersigners);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003ED1 File Offset: 0x000020D1
		[SecurityCritical]
		internal static SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [In] IntPtr pszInnerContentObjID, [In] IntPtr pStreamInfo)
		{
			return CAPI.CAPIUnsafe.CryptMsgOpenToEncode(dwMsgEncodingType, dwFlags, dwMsgType, pvMsgEncodeInfo, pszInnerContentObjID, pStreamInfo);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003EE0 File Offset: 0x000020E0
		[SecurityCritical]
		internal static SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [In] string pszInnerContentObjID, [In] IntPtr pStreamInfo)
		{
			return CAPI.CAPIUnsafe.CryptMsgOpenToEncode(dwMsgEncodingType, dwFlags, dwMsgType, pvMsgEncodeInfo, pszInnerContentObjID, pStreamInfo);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003EEF File Offset: 0x000020EF
		[SecurityCritical]
		internal static SafeCertContextHandle CertDuplicateCertificateContext([In] IntPtr pCertContext)
		{
			if (pCertContext == IntPtr.Zero)
			{
				return SafeCertContextHandle.InvalidHandle;
			}
			return CAPI.CAPISafe.CertDuplicateCertificateContext(pCertContext);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003F0C File Offset: 0x0000210C
		[SecurityCritical]
		internal static IntPtr CertEnumCertificatesInStore([In] SafeCertStoreHandle hCertStore, [In] IntPtr pPrevCertContext)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "hCertStore");
			}
			if (pPrevCertContext == IntPtr.Zero)
			{
				StorePermission storePermission = new StorePermission(StorePermissionFlags.EnumerateCertificates);
				storePermission.Demand();
			}
			IntPtr intPtr = CAPI.CAPIUnsafe.CertEnumCertificatesInStore(hCertStore, pPrevCertContext);
			if (intPtr == IntPtr.Zero)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != -2146885628)
				{
					CAPI.CAPISafe.CertFreeCertificateContext(intPtr);
					throw new CryptographicException(lastWin32Error);
				}
			}
			return intPtr;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003F94 File Offset: 0x00002194
		[SecurityCritical]
		internal static bool CertAddCertificateContextToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "hCertStore");
			}
			if (pCertContext == null)
			{
				throw new ArgumentNullException("pCertContext");
			}
			if (pCertContext.IsInvalid)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "pCertContext");
			}
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AddToStore);
			storePermission.Demand();
			return CAPI.CAPIUnsafe.CertAddCertificateContextToStore(hCertStore, pCertContext, dwAddDisposition, ppStoreContext);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004010 File Offset: 0x00002210
		[SecurityCritical]
		internal static bool CertAddCertificateLinkToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "hCertStore");
			}
			if (pCertContext == null)
			{
				throw new ArgumentNullException("pCertContext");
			}
			if (pCertContext.IsInvalid)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "pCertContext");
			}
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AddToStore);
			storePermission.Demand();
			return CAPI.CAPIUnsafe.CertAddCertificateLinkToStore(hCertStore, pCertContext, dwAddDisposition, ppStoreContext);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000408C File Offset: 0x0000228C
		[SecurityCritical]
		internal static SafeCertStoreHandle CertOpenStore([In] IntPtr lpszStoreProvider, [In] uint dwMsgAndCertEncodingType, [In] IntPtr hCryptProv, [In] uint dwFlags, [In] string pvPara)
		{
			if (lpszStoreProvider != new IntPtr(2L) && lpszStoreProvider != new IntPtr(10L))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Argument_InvalidValue"), "lpszStoreProvider");
			}
			if (((dwFlags & 131072U) == 131072U || (dwFlags & 524288U) == 524288U || (dwFlags & 589824U) == 589824U) && pvPara != null && pvPara.StartsWith("\\\\", StringComparison.Ordinal))
			{
				new PermissionSet(PermissionState.Unrestricted).Demand();
			}
			if ((dwFlags & 16U) == 16U)
			{
				StorePermission storePermission = new StorePermission(StorePermissionFlags.DeleteStore);
				storePermission.Demand();
			}
			else
			{
				StorePermission storePermission2 = new StorePermission(StorePermissionFlags.OpenStore);
				storePermission2.Demand();
			}
			if ((dwFlags & 8192U) == 8192U)
			{
				StorePermission storePermission3 = new StorePermission(StorePermissionFlags.CreateStore);
				storePermission3.Demand();
			}
			if ((dwFlags & 16384U) == 0U)
			{
				StorePermission storePermission4 = new StorePermission(StorePermissionFlags.CreateStore);
				storePermission4.Demand();
			}
			return CAPI.CAPIUnsafe.CertOpenStore(lpszStoreProvider, dwMsgAndCertEncodingType, hCryptProv, dwFlags | 4U, pvPara);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004178 File Offset: 0x00002378
		[SecurityCritical]
		internal static bool CryptProtectData([In] IntPtr pDataIn, [In] string szDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In] [Out] IntPtr pDataBlob)
		{
			DataProtectionPermission dataProtectionPermission = new DataProtectionPermission(DataProtectionPermissionFlags.ProtectData);
			dataProtectionPermission.Demand();
			return CAPI.CAPIUnsafe.CryptProtectData(pDataIn, szDataDescr, pOptionalEntropy, pvReserved, pPromptStruct, dwFlags, pDataBlob);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000041A4 File Offset: 0x000023A4
		[SecurityCritical]
		internal static bool CryptUnprotectData([In] IntPtr pDataIn, [In] IntPtr ppszDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In] [Out] IntPtr pDataBlob)
		{
			DataProtectionPermission dataProtectionPermission = new DataProtectionPermission(DataProtectionPermissionFlags.UnprotectData);
			dataProtectionPermission.Demand();
			return CAPI.CAPIUnsafe.CryptUnprotectData(pDataIn, ppszDataDescr, pOptionalEntropy, pvReserved, pPromptStruct, dwFlags, pDataBlob);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000041D0 File Offset: 0x000023D0
		[SecurityCritical]
		internal static int SystemFunction040([In] [Out] byte[] pDataIn, [In] uint cbDataIn, [In] uint dwFlags)
		{
			DataProtectionPermission dataProtectionPermission = new DataProtectionPermission(DataProtectionPermissionFlags.ProtectMemory);
			dataProtectionPermission.Demand();
			return CAPI.CAPIUnsafe.SystemFunction040(pDataIn, cbDataIn, dwFlags);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000041F4 File Offset: 0x000023F4
		[SecurityCritical]
		internal static int SystemFunction041([In] [Out] byte[] pDataIn, [In] uint cbDataIn, [In] uint dwFlags)
		{
			DataProtectionPermission dataProtectionPermission = new DataProtectionPermission(DataProtectionPermissionFlags.UnprotectMemory);
			dataProtectionPermission.Demand();
			return CAPI.CAPIUnsafe.SystemFunction041(pDataIn, cbDataIn, dwFlags);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004218 File Offset: 0x00002418
		[SecurityCritical]
		internal static SafeCertContextHandle CryptUIDlgSelectCertificateW([MarshalAs(UnmanagedType.LPStruct)] [In] [Out] CAPI.CRYPTUI_SELECTCERTIFICATE_STRUCTW csc)
		{
			if (!Environment.UserInteractive)
			{
				throw new InvalidOperationException(SecurityResources.GetResourceString("Environment_NotInteractive"));
			}
			UIPermission uipermission = new UIPermission(UIPermissionWindow.SafeTopLevelWindows);
			uipermission.Demand();
			return CAPI.CAPIUnsafe.CryptUIDlgSelectCertificateW(csc);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004250 File Offset: 0x00002450
		[SecurityCritical]
		internal static bool CryptUIDlgViewCertificateW([MarshalAs(UnmanagedType.LPStruct)] [In] CAPI.CRYPTUI_VIEWCERTIFICATE_STRUCTW ViewInfo, [In] [Out] IntPtr pfPropertiesChanged)
		{
			if (!Environment.UserInteractive)
			{
				throw new InvalidOperationException(SecurityResources.GetResourceString("Environment_NotInteractive"));
			}
			UIPermission uipermission = new UIPermission(UIPermissionWindow.SafeTopLevelWindows);
			uipermission.Demand();
			return CAPI.CAPIUnsafe.CryptUIDlgViewCertificateW(ViewInfo, pfPropertiesChanged);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004288 File Offset: 0x00002488
		[SecurityCritical]
		internal static SafeCertContextHandle CertFindCertificateInStore([In] SafeCertStoreHandle hCertStore, [In] uint dwCertEncodingType, [In] uint dwFindFlags, [In] uint dwFindType, [In] IntPtr pvFindPara, [In] SafeCertContextHandle pPrevCertContext)
		{
			if (hCertStore == null)
			{
				throw new ArgumentNullException("hCertStore");
			}
			if (hCertStore.IsInvalid)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_InvalidHandle"), "hCertStore");
			}
			return CAPI.CAPIUnsafe.CertFindCertificateInStore(hCertStore, dwCertEncodingType, dwFindFlags, dwFindType, pvFindPara, pPrevCertContext);
		}

		// Token: 0x04000084 RID: 132
		internal const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04000085 RID: 133
		internal const string CRYPT32 = "crypt32.dll";

		// Token: 0x04000086 RID: 134
		internal const string CRYPTUI = "cryptui.dll";

		// Token: 0x04000087 RID: 135
		internal const string KERNEL32 = "kernel32.dll";

		// Token: 0x04000088 RID: 136
		internal const uint LMEM_FIXED = 0U;

		// Token: 0x04000089 RID: 137
		internal const uint LMEM_ZEROINIT = 64U;

		// Token: 0x0400008A RID: 138
		internal const uint LPTR = 64U;

		// Token: 0x0400008B RID: 139
		internal const int S_OK = 0;

		// Token: 0x0400008C RID: 140
		internal const int S_FALSE = 1;

		// Token: 0x0400008D RID: 141
		internal const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096U;

		// Token: 0x0400008E RID: 142
		internal const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512U;

		// Token: 0x0400008F RID: 143
		internal const uint VER_PLATFORM_WIN32s = 0U;

		// Token: 0x04000090 RID: 144
		internal const uint VER_PLATFORM_WIN32_WINDOWS = 1U;

		// Token: 0x04000091 RID: 145
		internal const uint VER_PLATFORM_WIN32_NT = 2U;

		// Token: 0x04000092 RID: 146
		internal const uint VER_PLATFORM_WINCE = 3U;

		// Token: 0x04000093 RID: 147
		internal const uint ASN_TAG_NULL = 5U;

		// Token: 0x04000094 RID: 148
		internal const uint ASN_TAG_OBJID = 6U;

		// Token: 0x04000095 RID: 149
		internal const uint CERT_QUERY_OBJECT_FILE = 1U;

		// Token: 0x04000096 RID: 150
		internal const uint CERT_QUERY_OBJECT_BLOB = 2U;

		// Token: 0x04000097 RID: 151
		internal const uint CERT_QUERY_CONTENT_CERT = 1U;

		// Token: 0x04000098 RID: 152
		internal const uint CERT_QUERY_CONTENT_CTL = 2U;

		// Token: 0x04000099 RID: 153
		internal const uint CERT_QUERY_CONTENT_CRL = 3U;

		// Token: 0x0400009A RID: 154
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_STORE = 4U;

		// Token: 0x0400009B RID: 155
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_CERT = 5U;

		// Token: 0x0400009C RID: 156
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_CTL = 6U;

		// Token: 0x0400009D RID: 157
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_CRL = 7U;

		// Token: 0x0400009E RID: 158
		internal const uint CERT_QUERY_CONTENT_PKCS7_SIGNED = 8U;

		// Token: 0x0400009F RID: 159
		internal const uint CERT_QUERY_CONTENT_PKCS7_UNSIGNED = 9U;

		// Token: 0x040000A0 RID: 160
		internal const uint CERT_QUERY_CONTENT_PKCS7_SIGNED_EMBED = 10U;

		// Token: 0x040000A1 RID: 161
		internal const uint CERT_QUERY_CONTENT_PKCS10 = 11U;

		// Token: 0x040000A2 RID: 162
		internal const uint CERT_QUERY_CONTENT_PFX = 12U;

		// Token: 0x040000A3 RID: 163
		internal const uint CERT_QUERY_CONTENT_CERT_PAIR = 13U;

		// Token: 0x040000A4 RID: 164
		internal const uint CERT_QUERY_CONTENT_FLAG_CERT = 2U;

		// Token: 0x040000A5 RID: 165
		internal const uint CERT_QUERY_CONTENT_FLAG_CTL = 4U;

		// Token: 0x040000A6 RID: 166
		internal const uint CERT_QUERY_CONTENT_FLAG_CRL = 8U;

		// Token: 0x040000A7 RID: 167
		internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_STORE = 16U;

		// Token: 0x040000A8 RID: 168
		internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_CERT = 32U;

		// Token: 0x040000A9 RID: 169
		internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_CTL = 64U;

		// Token: 0x040000AA RID: 170
		internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_CRL = 128U;

		// Token: 0x040000AB RID: 171
		internal const uint CERT_QUERY_CONTENT_FLAG_PKCS7_SIGNED = 256U;

		// Token: 0x040000AC RID: 172
		internal const uint CERT_QUERY_CONTENT_FLAG_PKCS7_UNSIGNED = 512U;

		// Token: 0x040000AD RID: 173
		internal const uint CERT_QUERY_CONTENT_FLAG_PKCS7_SIGNED_EMBED = 1024U;

		// Token: 0x040000AE RID: 174
		internal const uint CERT_QUERY_CONTENT_FLAG_PKCS10 = 2048U;

		// Token: 0x040000AF RID: 175
		internal const uint CERT_QUERY_CONTENT_FLAG_PFX = 4096U;

		// Token: 0x040000B0 RID: 176
		internal const uint CERT_QUERY_CONTENT_FLAG_CERT_PAIR = 8192U;

		// Token: 0x040000B1 RID: 177
		internal const uint CERT_QUERY_CONTENT_FLAG_ALL = 16382U;

		// Token: 0x040000B2 RID: 178
		internal const uint CERT_QUERY_FORMAT_BINARY = 1U;

		// Token: 0x040000B3 RID: 179
		internal const uint CERT_QUERY_FORMAT_BASE64_ENCODED = 2U;

		// Token: 0x040000B4 RID: 180
		internal const uint CERT_QUERY_FORMAT_ASN_ASCII_HEX_ENCODED = 3U;

		// Token: 0x040000B5 RID: 181
		internal const uint CERT_QUERY_FORMAT_FLAG_BINARY = 2U;

		// Token: 0x040000B6 RID: 182
		internal const uint CERT_QUERY_FORMAT_FLAG_BASE64_ENCODED = 4U;

		// Token: 0x040000B7 RID: 183
		internal const uint CERT_QUERY_FORMAT_FLAG_ASN_ASCII_HEX_ENCODED = 8U;

		// Token: 0x040000B8 RID: 184
		internal const uint CERT_QUERY_FORMAT_FLAG_ALL = 14U;

		// Token: 0x040000B9 RID: 185
		internal const uint CRYPTPROTECT_UI_FORBIDDEN = 1U;

		// Token: 0x040000BA RID: 186
		internal const uint CRYPTPROTECT_LOCAL_MACHINE = 4U;

		// Token: 0x040000BB RID: 187
		internal const uint CRYPTPROTECT_CRED_SYNC = 8U;

		// Token: 0x040000BC RID: 188
		internal const uint CRYPTPROTECT_AUDIT = 16U;

		// Token: 0x040000BD RID: 189
		internal const uint CRYPTPROTECT_NO_RECOVERY = 32U;

		// Token: 0x040000BE RID: 190
		internal const uint CRYPTPROTECT_VERIFY_PROTECTION = 64U;

		// Token: 0x040000BF RID: 191
		internal const uint CRYPTPROTECTMEMORY_BLOCK_SIZE = 16U;

		// Token: 0x040000C0 RID: 192
		internal const uint CRYPTPROTECTMEMORY_SAME_PROCESS = 0U;

		// Token: 0x040000C1 RID: 193
		internal const uint CRYPTPROTECTMEMORY_CROSS_PROCESS = 1U;

		// Token: 0x040000C2 RID: 194
		internal const uint CRYPTPROTECTMEMORY_SAME_LOGON = 2U;

		// Token: 0x040000C3 RID: 195
		internal const uint CRYPT_OID_INFO_OID_KEY = 1U;

		// Token: 0x040000C4 RID: 196
		internal const uint CRYPT_OID_INFO_NAME_KEY = 2U;

		// Token: 0x040000C5 RID: 197
		internal const uint CRYPT_OID_INFO_ALGID_KEY = 3U;

		// Token: 0x040000C6 RID: 198
		internal const uint CRYPT_OID_INFO_SIGN_KEY = 4U;

		// Token: 0x040000C7 RID: 199
		internal const uint CRYPT_HASH_ALG_OID_GROUP_ID = 1U;

		// Token: 0x040000C8 RID: 200
		internal const uint CRYPT_ENCRYPT_ALG_OID_GROUP_ID = 2U;

		// Token: 0x040000C9 RID: 201
		internal const uint CRYPT_PUBKEY_ALG_OID_GROUP_ID = 3U;

		// Token: 0x040000CA RID: 202
		internal const uint CRYPT_SIGN_ALG_OID_GROUP_ID = 4U;

		// Token: 0x040000CB RID: 203
		internal const uint CRYPT_RDN_ATTR_OID_GROUP_ID = 5U;

		// Token: 0x040000CC RID: 204
		internal const uint CRYPT_EXT_OR_ATTR_OID_GROUP_ID = 6U;

		// Token: 0x040000CD RID: 205
		internal const uint CRYPT_ENHKEY_USAGE_OID_GROUP_ID = 7U;

		// Token: 0x040000CE RID: 206
		internal const uint CRYPT_POLICY_OID_GROUP_ID = 8U;

		// Token: 0x040000CF RID: 207
		internal const uint CRYPT_TEMPLATE_OID_GROUP_ID = 9U;

		// Token: 0x040000D0 RID: 208
		internal const uint CRYPT_LAST_OID_GROUP_ID = 9U;

		// Token: 0x040000D1 RID: 209
		internal const uint CRYPT_FIRST_ALG_OID_GROUP_ID = 1U;

		// Token: 0x040000D2 RID: 210
		internal const uint CRYPT_LAST_ALG_OID_GROUP_ID = 4U;

		// Token: 0x040000D3 RID: 211
		internal const uint CRYPT_ASN_ENCODING = 1U;

		// Token: 0x040000D4 RID: 212
		internal const uint CRYPT_NDR_ENCODING = 2U;

		// Token: 0x040000D5 RID: 213
		internal const uint X509_ASN_ENCODING = 1U;

		// Token: 0x040000D6 RID: 214
		internal const uint X509_NDR_ENCODING = 2U;

		// Token: 0x040000D7 RID: 215
		internal const uint PKCS_7_ASN_ENCODING = 65536U;

		// Token: 0x040000D8 RID: 216
		internal const uint PKCS_7_NDR_ENCODING = 131072U;

		// Token: 0x040000D9 RID: 217
		internal const uint PKCS_7_OR_X509_ASN_ENCODING = 65537U;

		// Token: 0x040000DA RID: 218
		internal const uint CERT_STORE_PROV_MSG = 1U;

		// Token: 0x040000DB RID: 219
		internal const uint CERT_STORE_PROV_MEMORY = 2U;

		// Token: 0x040000DC RID: 220
		internal const uint CERT_STORE_PROV_FILE = 3U;

		// Token: 0x040000DD RID: 221
		internal const uint CERT_STORE_PROV_REG = 4U;

		// Token: 0x040000DE RID: 222
		internal const uint CERT_STORE_PROV_PKCS7 = 5U;

		// Token: 0x040000DF RID: 223
		internal const uint CERT_STORE_PROV_SERIALIZED = 6U;

		// Token: 0x040000E0 RID: 224
		internal const uint CERT_STORE_PROV_FILENAME_A = 7U;

		// Token: 0x040000E1 RID: 225
		internal const uint CERT_STORE_PROV_FILENAME_W = 8U;

		// Token: 0x040000E2 RID: 226
		internal const uint CERT_STORE_PROV_FILENAME = 8U;

		// Token: 0x040000E3 RID: 227
		internal const uint CERT_STORE_PROV_SYSTEM_A = 9U;

		// Token: 0x040000E4 RID: 228
		internal const uint CERT_STORE_PROV_SYSTEM_W = 10U;

		// Token: 0x040000E5 RID: 229
		internal const uint CERT_STORE_PROV_SYSTEM = 10U;

		// Token: 0x040000E6 RID: 230
		internal const uint CERT_STORE_PROV_COLLECTION = 11U;

		// Token: 0x040000E7 RID: 231
		internal const uint CERT_STORE_PROV_SYSTEM_REGISTRY_A = 12U;

		// Token: 0x040000E8 RID: 232
		internal const uint CERT_STORE_PROV_SYSTEM_REGISTRY_W = 13U;

		// Token: 0x040000E9 RID: 233
		internal const uint CERT_STORE_PROV_SYSTEM_REGISTRY = 13U;

		// Token: 0x040000EA RID: 234
		internal const uint CERT_STORE_PROV_PHYSICAL_W = 14U;

		// Token: 0x040000EB RID: 235
		internal const uint CERT_STORE_PROV_PHYSICAL = 14U;

		// Token: 0x040000EC RID: 236
		internal const uint CERT_STORE_PROV_SMART_CARD_W = 15U;

		// Token: 0x040000ED RID: 237
		internal const uint CERT_STORE_PROV_SMART_CARD = 15U;

		// Token: 0x040000EE RID: 238
		internal const uint CERT_STORE_PROV_LDAP_W = 16U;

		// Token: 0x040000EF RID: 239
		internal const uint CERT_STORE_PROV_LDAP = 16U;

		// Token: 0x040000F0 RID: 240
		internal const uint CERT_STORE_NO_CRYPT_RELEASE_FLAG = 1U;

		// Token: 0x040000F1 RID: 241
		internal const uint CERT_STORE_SET_LOCALIZED_NAME_FLAG = 2U;

		// Token: 0x040000F2 RID: 242
		internal const uint CERT_STORE_DEFER_CLOSE_UNTIL_LAST_FREE_FLAG = 4U;

		// Token: 0x040000F3 RID: 243
		internal const uint CERT_STORE_DELETE_FLAG = 16U;

		// Token: 0x040000F4 RID: 244
		internal const uint CERT_STORE_SHARE_STORE_FLAG = 64U;

		// Token: 0x040000F5 RID: 245
		internal const uint CERT_STORE_SHARE_CONTEXT_FLAG = 128U;

		// Token: 0x040000F6 RID: 246
		internal const uint CERT_STORE_MANIFOLD_FLAG = 256U;

		// Token: 0x040000F7 RID: 247
		internal const uint CERT_STORE_ENUM_ARCHIVED_FLAG = 512U;

		// Token: 0x040000F8 RID: 248
		internal const uint CERT_STORE_UPDATE_KEYID_FLAG = 1024U;

		// Token: 0x040000F9 RID: 249
		internal const uint CERT_STORE_BACKUP_RESTORE_FLAG = 2048U;

		// Token: 0x040000FA RID: 250
		internal const uint CERT_STORE_READONLY_FLAG = 32768U;

		// Token: 0x040000FB RID: 251
		internal const uint CERT_STORE_OPEN_EXISTING_FLAG = 16384U;

		// Token: 0x040000FC RID: 252
		internal const uint CERT_STORE_CREATE_NEW_FLAG = 8192U;

		// Token: 0x040000FD RID: 253
		internal const uint CERT_STORE_MAXIMUM_ALLOWED_FLAG = 4096U;

		// Token: 0x040000FE RID: 254
		internal const uint CERT_SYSTEM_STORE_UNPROTECTED_FLAG = 1073741824U;

		// Token: 0x040000FF RID: 255
		internal const uint CERT_SYSTEM_STORE_LOCATION_MASK = 16711680U;

		// Token: 0x04000100 RID: 256
		internal const uint CERT_SYSTEM_STORE_LOCATION_SHIFT = 16U;

		// Token: 0x04000101 RID: 257
		internal const uint CERT_SYSTEM_STORE_CURRENT_USER_ID = 1U;

		// Token: 0x04000102 RID: 258
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ID = 2U;

		// Token: 0x04000103 RID: 259
		internal const uint CERT_SYSTEM_STORE_CURRENT_SERVICE_ID = 4U;

		// Token: 0x04000104 RID: 260
		internal const uint CERT_SYSTEM_STORE_SERVICES_ID = 5U;

		// Token: 0x04000105 RID: 261
		internal const uint CERT_SYSTEM_STORE_USERS_ID = 6U;

		// Token: 0x04000106 RID: 262
		internal const uint CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY_ID = 7U;

		// Token: 0x04000107 RID: 263
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY_ID = 8U;

		// Token: 0x04000108 RID: 264
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE_ID = 9U;

		// Token: 0x04000109 RID: 265
		internal const uint CERT_SYSTEM_STORE_CURRENT_USER = 65536U;

		// Token: 0x0400010A RID: 266
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072U;

		// Token: 0x0400010B RID: 267
		internal const uint CERT_SYSTEM_STORE_CURRENT_SERVICE = 262144U;

		// Token: 0x0400010C RID: 268
		internal const uint CERT_SYSTEM_STORE_SERVICES = 327680U;

		// Token: 0x0400010D RID: 269
		internal const uint CERT_SYSTEM_STORE_USERS = 393216U;

		// Token: 0x0400010E RID: 270
		internal const uint CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY = 458752U;

		// Token: 0x0400010F RID: 271
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY = 524288U;

		// Token: 0x04000110 RID: 272
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE = 589824U;

		// Token: 0x04000111 RID: 273
		internal const uint CERT_NAME_EMAIL_TYPE = 1U;

		// Token: 0x04000112 RID: 274
		internal const uint CERT_NAME_RDN_TYPE = 2U;

		// Token: 0x04000113 RID: 275
		internal const uint CERT_NAME_ATTR_TYPE = 3U;

		// Token: 0x04000114 RID: 276
		internal const uint CERT_NAME_SIMPLE_DISPLAY_TYPE = 4U;

		// Token: 0x04000115 RID: 277
		internal const uint CERT_NAME_FRIENDLY_DISPLAY_TYPE = 5U;

		// Token: 0x04000116 RID: 278
		internal const uint CERT_NAME_DNS_TYPE = 6U;

		// Token: 0x04000117 RID: 279
		internal const uint CERT_NAME_URL_TYPE = 7U;

		// Token: 0x04000118 RID: 280
		internal const uint CERT_NAME_UPN_TYPE = 8U;

		// Token: 0x04000119 RID: 281
		internal const uint CERT_SIMPLE_NAME_STR = 1U;

		// Token: 0x0400011A RID: 282
		internal const uint CERT_OID_NAME_STR = 2U;

		// Token: 0x0400011B RID: 283
		internal const uint CERT_X500_NAME_STR = 3U;

		// Token: 0x0400011C RID: 284
		internal const uint CERT_NAME_STR_SEMICOLON_FLAG = 1073741824U;

		// Token: 0x0400011D RID: 285
		internal const uint CERT_NAME_STR_NO_PLUS_FLAG = 536870912U;

		// Token: 0x0400011E RID: 286
		internal const uint CERT_NAME_STR_NO_QUOTING_FLAG = 268435456U;

		// Token: 0x0400011F RID: 287
		internal const uint CERT_NAME_STR_CRLF_FLAG = 134217728U;

		// Token: 0x04000120 RID: 288
		internal const uint CERT_NAME_STR_COMMA_FLAG = 67108864U;

		// Token: 0x04000121 RID: 289
		internal const uint CERT_NAME_STR_REVERSE_FLAG = 33554432U;

		// Token: 0x04000122 RID: 290
		internal const uint CERT_NAME_ISSUER_FLAG = 1U;

		// Token: 0x04000123 RID: 291
		internal const uint CERT_NAME_STR_DISABLE_IE4_UTF8_FLAG = 65536U;

		// Token: 0x04000124 RID: 292
		internal const uint CERT_NAME_STR_ENABLE_T61_UNICODE_FLAG = 131072U;

		// Token: 0x04000125 RID: 293
		internal const uint CERT_NAME_STR_ENABLE_UTF8_UNICODE_FLAG = 262144U;

		// Token: 0x04000126 RID: 294
		internal const uint CERT_NAME_STR_FORCE_UTF8_DIR_STR_FLAG = 524288U;

		// Token: 0x04000127 RID: 295
		internal const uint CERT_KEY_PROV_HANDLE_PROP_ID = 1U;

		// Token: 0x04000128 RID: 296
		internal const uint CERT_KEY_PROV_INFO_PROP_ID = 2U;

		// Token: 0x04000129 RID: 297
		internal const uint CERT_SHA1_HASH_PROP_ID = 3U;

		// Token: 0x0400012A RID: 298
		internal const uint CERT_MD5_HASH_PROP_ID = 4U;

		// Token: 0x0400012B RID: 299
		internal const uint CERT_HASH_PROP_ID = 3U;

		// Token: 0x0400012C RID: 300
		internal const uint CERT_KEY_CONTEXT_PROP_ID = 5U;

		// Token: 0x0400012D RID: 301
		internal const uint CERT_KEY_SPEC_PROP_ID = 6U;

		// Token: 0x0400012E RID: 302
		internal const uint CERT_IE30_RESERVED_PROP_ID = 7U;

		// Token: 0x0400012F RID: 303
		internal const uint CERT_PUBKEY_HASH_RESERVED_PROP_ID = 8U;

		// Token: 0x04000130 RID: 304
		internal const uint CERT_ENHKEY_USAGE_PROP_ID = 9U;

		// Token: 0x04000131 RID: 305
		internal const uint CERT_CTL_USAGE_PROP_ID = 9U;

		// Token: 0x04000132 RID: 306
		internal const uint CERT_NEXT_UPDATE_LOCATION_PROP_ID = 10U;

		// Token: 0x04000133 RID: 307
		internal const uint CERT_FRIENDLY_NAME_PROP_ID = 11U;

		// Token: 0x04000134 RID: 308
		internal const uint CERT_PVK_FILE_PROP_ID = 12U;

		// Token: 0x04000135 RID: 309
		internal const uint CERT_DESCRIPTION_PROP_ID = 13U;

		// Token: 0x04000136 RID: 310
		internal const uint CERT_ACCESS_STATE_PROP_ID = 14U;

		// Token: 0x04000137 RID: 311
		internal const uint CERT_SIGNATURE_HASH_PROP_ID = 15U;

		// Token: 0x04000138 RID: 312
		internal const uint CERT_SMART_CARD_DATA_PROP_ID = 16U;

		// Token: 0x04000139 RID: 313
		internal const uint CERT_EFS_PROP_ID = 17U;

		// Token: 0x0400013A RID: 314
		internal const uint CERT_FORTEZZA_DATA_PROP_ID = 18U;

		// Token: 0x0400013B RID: 315
		internal const uint CERT_ARCHIVED_PROP_ID = 19U;

		// Token: 0x0400013C RID: 316
		internal const uint CERT_KEY_IDENTIFIER_PROP_ID = 20U;

		// Token: 0x0400013D RID: 317
		internal const uint CERT_AUTO_ENROLL_PROP_ID = 21U;

		// Token: 0x0400013E RID: 318
		internal const uint CERT_PUBKEY_ALG_PARA_PROP_ID = 22U;

		// Token: 0x0400013F RID: 319
		internal const uint CERT_CROSS_CERT_DIST_POINTS_PROP_ID = 23U;

		// Token: 0x04000140 RID: 320
		internal const uint CERT_ISSUER_PUBLIC_KEY_MD5_HASH_PROP_ID = 24U;

		// Token: 0x04000141 RID: 321
		internal const uint CERT_SUBJECT_PUBLIC_KEY_MD5_HASH_PROP_ID = 25U;

		// Token: 0x04000142 RID: 322
		internal const uint CERT_ENROLLMENT_PROP_ID = 26U;

		// Token: 0x04000143 RID: 323
		internal const uint CERT_DATE_STAMP_PROP_ID = 27U;

		// Token: 0x04000144 RID: 324
		internal const uint CERT_ISSUER_SERIAL_NUMBER_MD5_HASH_PROP_ID = 28U;

		// Token: 0x04000145 RID: 325
		internal const uint CERT_SUBJECT_NAME_MD5_HASH_PROP_ID = 29U;

		// Token: 0x04000146 RID: 326
		internal const uint CERT_EXTENDED_ERROR_INFO_PROP_ID = 30U;

		// Token: 0x04000147 RID: 327
		internal const uint CERT_RENEWAL_PROP_ID = 64U;

		// Token: 0x04000148 RID: 328
		internal const uint CERT_ARCHIVED_KEY_HASH_PROP_ID = 65U;

		// Token: 0x04000149 RID: 329
		internal const uint CERT_FIRST_RESERVED_PROP_ID = 66U;

		// Token: 0x0400014A RID: 330
		internal const uint CERT_NCRYPT_KEY_HANDLE_PROP_ID = 78U;

		// Token: 0x0400014B RID: 331
		internal const uint CERT_DELETE_KEYSET_PROP_ID = 125U;

		// Token: 0x0400014C RID: 332
		internal const uint CERT_INFO_VERSION_FLAG = 1U;

		// Token: 0x0400014D RID: 333
		internal const uint CERT_INFO_SERIAL_NUMBER_FLAG = 2U;

		// Token: 0x0400014E RID: 334
		internal const uint CERT_INFO_SIGNATURE_ALGORITHM_FLAG = 3U;

		// Token: 0x0400014F RID: 335
		internal const uint CERT_INFO_ISSUER_FLAG = 4U;

		// Token: 0x04000150 RID: 336
		internal const uint CERT_INFO_NOT_BEFORE_FLAG = 5U;

		// Token: 0x04000151 RID: 337
		internal const uint CERT_INFO_NOT_AFTER_FLAG = 6U;

		// Token: 0x04000152 RID: 338
		internal const uint CERT_INFO_SUBJECT_FLAG = 7U;

		// Token: 0x04000153 RID: 339
		internal const uint CERT_INFO_SUBJECT_PUBLIC_KEY_INFO_FLAG = 8U;

		// Token: 0x04000154 RID: 340
		internal const uint CERT_INFO_ISSUER_UNIQUE_ID_FLAG = 9U;

		// Token: 0x04000155 RID: 341
		internal const uint CERT_INFO_SUBJECT_UNIQUE_ID_FLAG = 10U;

		// Token: 0x04000156 RID: 342
		internal const uint CERT_INFO_EXTENSION_FLAG = 11U;

		// Token: 0x04000157 RID: 343
		internal const uint CERT_COMPARE_MASK = 65535U;

		// Token: 0x04000158 RID: 344
		internal const uint CERT_COMPARE_SHIFT = 16U;

		// Token: 0x04000159 RID: 345
		internal const uint CERT_COMPARE_ANY = 0U;

		// Token: 0x0400015A RID: 346
		internal const uint CERT_COMPARE_SHA1_HASH = 1U;

		// Token: 0x0400015B RID: 347
		internal const uint CERT_COMPARE_NAME = 2U;

		// Token: 0x0400015C RID: 348
		internal const uint CERT_COMPARE_ATTR = 3U;

		// Token: 0x0400015D RID: 349
		internal const uint CERT_COMPARE_MD5_HASH = 4U;

		// Token: 0x0400015E RID: 350
		internal const uint CERT_COMPARE_PROPERTY = 5U;

		// Token: 0x0400015F RID: 351
		internal const uint CERT_COMPARE_PUBLIC_KEY = 6U;

		// Token: 0x04000160 RID: 352
		internal const uint CERT_COMPARE_HASH = 1U;

		// Token: 0x04000161 RID: 353
		internal const uint CERT_COMPARE_NAME_STR_A = 7U;

		// Token: 0x04000162 RID: 354
		internal const uint CERT_COMPARE_NAME_STR_W = 8U;

		// Token: 0x04000163 RID: 355
		internal const uint CERT_COMPARE_KEY_SPEC = 9U;

		// Token: 0x04000164 RID: 356
		internal const uint CERT_COMPARE_ENHKEY_USAGE = 10U;

		// Token: 0x04000165 RID: 357
		internal const uint CERT_COMPARE_CTL_USAGE = 10U;

		// Token: 0x04000166 RID: 358
		internal const uint CERT_COMPARE_SUBJECT_CERT = 11U;

		// Token: 0x04000167 RID: 359
		internal const uint CERT_COMPARE_ISSUER_OF = 12U;

		// Token: 0x04000168 RID: 360
		internal const uint CERT_COMPARE_EXISTING = 13U;

		// Token: 0x04000169 RID: 361
		internal const uint CERT_COMPARE_SIGNATURE_HASH = 14U;

		// Token: 0x0400016A RID: 362
		internal const uint CERT_COMPARE_KEY_IDENTIFIER = 15U;

		// Token: 0x0400016B RID: 363
		internal const uint CERT_COMPARE_CERT_ID = 16U;

		// Token: 0x0400016C RID: 364
		internal const uint CERT_COMPARE_CROSS_CERT_DIST_POINTS = 17U;

		// Token: 0x0400016D RID: 365
		internal const uint CERT_COMPARE_PUBKEY_MD5_HASH = 18U;

		// Token: 0x0400016E RID: 366
		internal const uint CERT_FIND_ANY = 0U;

		// Token: 0x0400016F RID: 367
		internal const uint CERT_FIND_SHA1_HASH = 65536U;

		// Token: 0x04000170 RID: 368
		internal const uint CERT_FIND_MD5_HASH = 262144U;

		// Token: 0x04000171 RID: 369
		internal const uint CERT_FIND_SIGNATURE_HASH = 917504U;

		// Token: 0x04000172 RID: 370
		internal const uint CERT_FIND_KEY_IDENTIFIER = 983040U;

		// Token: 0x04000173 RID: 371
		internal const uint CERT_FIND_HASH = 65536U;

		// Token: 0x04000174 RID: 372
		internal const uint CERT_FIND_PROPERTY = 327680U;

		// Token: 0x04000175 RID: 373
		internal const uint CERT_FIND_PUBLIC_KEY = 393216U;

		// Token: 0x04000176 RID: 374
		internal const uint CERT_FIND_SUBJECT_NAME = 131079U;

		// Token: 0x04000177 RID: 375
		internal const uint CERT_FIND_SUBJECT_ATTR = 196615U;

		// Token: 0x04000178 RID: 376
		internal const uint CERT_FIND_ISSUER_NAME = 131076U;

		// Token: 0x04000179 RID: 377
		internal const uint CERT_FIND_ISSUER_ATTR = 196612U;

		// Token: 0x0400017A RID: 378
		internal const uint CERT_FIND_SUBJECT_STR_A = 458759U;

		// Token: 0x0400017B RID: 379
		internal const uint CERT_FIND_SUBJECT_STR_W = 524295U;

		// Token: 0x0400017C RID: 380
		internal const uint CERT_FIND_SUBJECT_STR = 524295U;

		// Token: 0x0400017D RID: 381
		internal const uint CERT_FIND_ISSUER_STR_A = 458756U;

		// Token: 0x0400017E RID: 382
		internal const uint CERT_FIND_ISSUER_STR_W = 524292U;

		// Token: 0x0400017F RID: 383
		internal const uint CERT_FIND_ISSUER_STR = 524292U;

		// Token: 0x04000180 RID: 384
		internal const uint CERT_FIND_KEY_SPEC = 589824U;

		// Token: 0x04000181 RID: 385
		internal const uint CERT_FIND_ENHKEY_USAGE = 655360U;

		// Token: 0x04000182 RID: 386
		internal const uint CERT_FIND_CTL_USAGE = 655360U;

		// Token: 0x04000183 RID: 387
		internal const uint CERT_FIND_SUBJECT_CERT = 720896U;

		// Token: 0x04000184 RID: 388
		internal const uint CERT_FIND_ISSUER_OF = 786432U;

		// Token: 0x04000185 RID: 389
		internal const uint CERT_FIND_EXISTING = 851968U;

		// Token: 0x04000186 RID: 390
		internal const uint CERT_FIND_CERT_ID = 1048576U;

		// Token: 0x04000187 RID: 391
		internal const uint CERT_FIND_CROSS_CERT_DIST_POINTS = 1114112U;

		// Token: 0x04000188 RID: 392
		internal const uint CERT_FIND_PUBKEY_MD5_HASH = 1179648U;

		// Token: 0x04000189 RID: 393
		internal const uint CERT_ENCIPHER_ONLY_KEY_USAGE = 1U;

		// Token: 0x0400018A RID: 394
		internal const uint CERT_CRL_SIGN_KEY_USAGE = 2U;

		// Token: 0x0400018B RID: 395
		internal const uint CERT_KEY_CERT_SIGN_KEY_USAGE = 4U;

		// Token: 0x0400018C RID: 396
		internal const uint CERT_KEY_AGREEMENT_KEY_USAGE = 8U;

		// Token: 0x0400018D RID: 397
		internal const uint CERT_DATA_ENCIPHERMENT_KEY_USAGE = 16U;

		// Token: 0x0400018E RID: 398
		internal const uint CERT_KEY_ENCIPHERMENT_KEY_USAGE = 32U;

		// Token: 0x0400018F RID: 399
		internal const uint CERT_NON_REPUDIATION_KEY_USAGE = 64U;

		// Token: 0x04000190 RID: 400
		internal const uint CERT_DIGITAL_SIGNATURE_KEY_USAGE = 128U;

		// Token: 0x04000191 RID: 401
		internal const uint CERT_DECIPHER_ONLY_KEY_USAGE = 32768U;

		// Token: 0x04000192 RID: 402
		internal const uint CERT_STORE_ADD_NEW = 1U;

		// Token: 0x04000193 RID: 403
		internal const uint CERT_STORE_ADD_USE_EXISTING = 2U;

		// Token: 0x04000194 RID: 404
		internal const uint CERT_STORE_ADD_REPLACE_EXISTING = 3U;

		// Token: 0x04000195 RID: 405
		internal const uint CERT_STORE_ADD_ALWAYS = 4U;

		// Token: 0x04000196 RID: 406
		internal const uint CERT_STORE_ADD_REPLACE_EXISTING_INHERIT_PROPERTIES = 5U;

		// Token: 0x04000197 RID: 407
		internal const uint CERT_STORE_ADD_NEWER = 6U;

		// Token: 0x04000198 RID: 408
		internal const uint CERT_STORE_ADD_NEWER_INHERIT_PROPERTIES = 7U;

		// Token: 0x04000199 RID: 409
		internal const uint CERT_STORE_SAVE_AS_STORE = 1U;

		// Token: 0x0400019A RID: 410
		internal const uint CERT_STORE_SAVE_AS_PKCS7 = 2U;

		// Token: 0x0400019B RID: 411
		internal const uint CERT_STORE_SAVE_TO_FILE = 1U;

		// Token: 0x0400019C RID: 412
		internal const uint CERT_STORE_SAVE_TO_MEMORY = 2U;

		// Token: 0x0400019D RID: 413
		internal const uint CERT_STORE_SAVE_TO_FILENAME_A = 3U;

		// Token: 0x0400019E RID: 414
		internal const uint CERT_STORE_SAVE_TO_FILENAME_W = 4U;

		// Token: 0x0400019F RID: 415
		internal const uint CERT_STORE_SAVE_TO_FILENAME = 4U;

		// Token: 0x040001A0 RID: 416
		internal const uint CERT_CA_SUBJECT_FLAG = 128U;

		// Token: 0x040001A1 RID: 417
		internal const uint CERT_END_ENTITY_SUBJECT_FLAG = 64U;

		// Token: 0x040001A2 RID: 418
		internal const uint RSA_CSP_PUBLICKEYBLOB = 19U;

		// Token: 0x040001A3 RID: 419
		internal const uint X509_MULTI_BYTE_UINT = 38U;

		// Token: 0x040001A4 RID: 420
		internal const uint X509_DSS_PUBLICKEY = 38U;

		// Token: 0x040001A5 RID: 421
		internal const uint X509_DSS_PARAMETERS = 39U;

		// Token: 0x040001A6 RID: 422
		internal const uint X509_DSS_SIGNATURE = 40U;

		// Token: 0x040001A7 RID: 423
		internal const uint X509_EXTENSIONS = 5U;

		// Token: 0x040001A8 RID: 424
		internal const uint X509_NAME_VALUE = 6U;

		// Token: 0x040001A9 RID: 425
		internal const uint X509_NAME = 7U;

		// Token: 0x040001AA RID: 426
		internal const uint X509_AUTHORITY_KEY_ID = 9U;

		// Token: 0x040001AB RID: 427
		internal const uint X509_KEY_USAGE_RESTRICTION = 11U;

		// Token: 0x040001AC RID: 428
		internal const uint X509_BASIC_CONSTRAINTS = 13U;

		// Token: 0x040001AD RID: 429
		internal const uint X509_KEY_USAGE = 14U;

		// Token: 0x040001AE RID: 430
		internal const uint X509_BASIC_CONSTRAINTS2 = 15U;

		// Token: 0x040001AF RID: 431
		internal const uint X509_CERT_POLICIES = 16U;

		// Token: 0x040001B0 RID: 432
		internal const uint PKCS_UTC_TIME = 17U;

		// Token: 0x040001B1 RID: 433
		internal const uint PKCS_ATTRIBUTE = 22U;

		// Token: 0x040001B2 RID: 434
		internal const uint X509_UNICODE_NAME_VALUE = 24U;

		// Token: 0x040001B3 RID: 435
		internal const uint X509_OCTET_STRING = 25U;

		// Token: 0x040001B4 RID: 436
		internal const uint X509_BITS = 26U;

		// Token: 0x040001B5 RID: 437
		internal const uint X509_ANY_STRING = 6U;

		// Token: 0x040001B6 RID: 438
		internal const uint X509_UNICODE_ANY_STRING = 24U;

		// Token: 0x040001B7 RID: 439
		internal const uint X509_ENHANCED_KEY_USAGE = 36U;

		// Token: 0x040001B8 RID: 440
		internal const uint PKCS_RC2_CBC_PARAMETERS = 41U;

		// Token: 0x040001B9 RID: 441
		internal const uint X509_CERTIFICATE_TEMPLATE = 64U;

		// Token: 0x040001BA RID: 442
		internal const uint PKCS7_SIGNER_INFO = 500U;

		// Token: 0x040001BB RID: 443
		internal const uint CMS_SIGNER_INFO = 501U;

		// Token: 0x040001BC RID: 444
		internal const string szOID_AUTHORITY_KEY_IDENTIFIER = "2.5.29.1";

		// Token: 0x040001BD RID: 445
		internal const string szOID_KEY_USAGE_RESTRICTION = "2.5.29.4";

		// Token: 0x040001BE RID: 446
		internal const string szOID_KEY_USAGE = "2.5.29.15";

		// Token: 0x040001BF RID: 447
		internal const string szOID_KEYID_RDN = "1.3.6.1.4.1.311.10.7.1";

		// Token: 0x040001C0 RID: 448
		internal const string szOID_RDN_DUMMY_SIGNER = "1.3.6.1.4.1.311.21.9";

		// Token: 0x040001C1 RID: 449
		internal const uint CERT_CHAIN_POLICY_BASE = 1U;

		// Token: 0x040001C2 RID: 450
		internal const uint CERT_CHAIN_POLICY_AUTHENTICODE = 2U;

		// Token: 0x040001C3 RID: 451
		internal const uint CERT_CHAIN_POLICY_AUTHENTICODE_TS = 3U;

		// Token: 0x040001C4 RID: 452
		internal const uint CERT_CHAIN_POLICY_SSL = 4U;

		// Token: 0x040001C5 RID: 453
		internal const uint CERT_CHAIN_POLICY_BASIC_CONSTRAINTS = 5U;

		// Token: 0x040001C6 RID: 454
		internal const uint CERT_CHAIN_POLICY_NT_AUTH = 6U;

		// Token: 0x040001C7 RID: 455
		internal const uint CERT_CHAIN_POLICY_MICROSOFT_ROOT = 7U;

		// Token: 0x040001C8 RID: 456
		internal const uint USAGE_MATCH_TYPE_AND = 0U;

		// Token: 0x040001C9 RID: 457
		internal const uint USAGE_MATCH_TYPE_OR = 1U;

		// Token: 0x040001CA RID: 458
		internal const uint CERT_CHAIN_REVOCATION_CHECK_END_CERT = 268435456U;

		// Token: 0x040001CB RID: 459
		internal const uint CERT_CHAIN_REVOCATION_CHECK_CHAIN = 536870912U;

		// Token: 0x040001CC RID: 460
		internal const uint CERT_CHAIN_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT = 1073741824U;

		// Token: 0x040001CD RID: 461
		internal const uint CERT_CHAIN_REVOCATION_CHECK_CACHE_ONLY = 2147483648U;

		// Token: 0x040001CE RID: 462
		internal const uint CERT_CHAIN_REVOCATION_ACCUMULATIVE_TIMEOUT = 134217728U;

		// Token: 0x040001CF RID: 463
		internal const uint CERT_TRUST_NO_ERROR = 0U;

		// Token: 0x040001D0 RID: 464
		internal const uint CERT_TRUST_IS_NOT_TIME_VALID = 1U;

		// Token: 0x040001D1 RID: 465
		internal const uint CERT_TRUST_IS_NOT_TIME_NESTED = 2U;

		// Token: 0x040001D2 RID: 466
		internal const uint CERT_TRUST_IS_REVOKED = 4U;

		// Token: 0x040001D3 RID: 467
		internal const uint CERT_TRUST_IS_NOT_SIGNATURE_VALID = 8U;

		// Token: 0x040001D4 RID: 468
		internal const uint CERT_TRUST_IS_NOT_VALID_FOR_USAGE = 16U;

		// Token: 0x040001D5 RID: 469
		internal const uint CERT_TRUST_IS_UNTRUSTED_ROOT = 32U;

		// Token: 0x040001D6 RID: 470
		internal const uint CERT_TRUST_REVOCATION_STATUS_UNKNOWN = 64U;

		// Token: 0x040001D7 RID: 471
		internal const uint CERT_TRUST_IS_CYCLIC = 128U;

		// Token: 0x040001D8 RID: 472
		internal const uint CERT_TRUST_INVALID_EXTENSION = 256U;

		// Token: 0x040001D9 RID: 473
		internal const uint CERT_TRUST_INVALID_POLICY_CONSTRAINTS = 512U;

		// Token: 0x040001DA RID: 474
		internal const uint CERT_TRUST_INVALID_BASIC_CONSTRAINTS = 1024U;

		// Token: 0x040001DB RID: 475
		internal const uint CERT_TRUST_INVALID_NAME_CONSTRAINTS = 2048U;

		// Token: 0x040001DC RID: 476
		internal const uint CERT_TRUST_HAS_NOT_SUPPORTED_NAME_CONSTRAINT = 4096U;

		// Token: 0x040001DD RID: 477
		internal const uint CERT_TRUST_HAS_NOT_DEFINED_NAME_CONSTRAINT = 8192U;

		// Token: 0x040001DE RID: 478
		internal const uint CERT_TRUST_HAS_NOT_PERMITTED_NAME_CONSTRAINT = 16384U;

		// Token: 0x040001DF RID: 479
		internal const uint CERT_TRUST_HAS_EXCLUDED_NAME_CONSTRAINT = 32768U;

		// Token: 0x040001E0 RID: 480
		internal const uint CERT_TRUST_IS_OFFLINE_REVOCATION = 16777216U;

		// Token: 0x040001E1 RID: 481
		internal const uint CERT_TRUST_NO_ISSUANCE_CHAIN_POLICY = 33554432U;

		// Token: 0x040001E2 RID: 482
		internal const uint CERT_TRUST_IS_PARTIAL_CHAIN = 65536U;

		// Token: 0x040001E3 RID: 483
		internal const uint CERT_TRUST_CTL_IS_NOT_TIME_VALID = 131072U;

		// Token: 0x040001E4 RID: 484
		internal const uint CERT_TRUST_CTL_IS_NOT_SIGNATURE_VALID = 262144U;

		// Token: 0x040001E5 RID: 485
		internal const uint CERT_TRUST_CTL_IS_NOT_VALID_FOR_USAGE = 524288U;

		// Token: 0x040001E6 RID: 486
		internal const uint CERT_CHAIN_POLICY_IGNORE_NOT_TIME_VALID_FLAG = 1U;

		// Token: 0x040001E7 RID: 487
		internal const uint CERT_CHAIN_POLICY_IGNORE_CTL_NOT_TIME_VALID_FLAG = 2U;

		// Token: 0x040001E8 RID: 488
		internal const uint CERT_CHAIN_POLICY_IGNORE_NOT_TIME_NESTED_FLAG = 4U;

		// Token: 0x040001E9 RID: 489
		internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_BASIC_CONSTRAINTS_FLAG = 8U;

		// Token: 0x040001EA RID: 490
		internal const uint CERT_CHAIN_POLICY_ALLOW_UNKNOWN_CA_FLAG = 16U;

		// Token: 0x040001EB RID: 491
		internal const uint CERT_CHAIN_POLICY_IGNORE_WRONG_USAGE_FLAG = 32U;

		// Token: 0x040001EC RID: 492
		internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_NAME_FLAG = 64U;

		// Token: 0x040001ED RID: 493
		internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_POLICY_FLAG = 128U;

		// Token: 0x040001EE RID: 494
		internal const uint CERT_CHAIN_POLICY_IGNORE_END_REV_UNKNOWN_FLAG = 256U;

		// Token: 0x040001EF RID: 495
		internal const uint CERT_CHAIN_POLICY_IGNORE_CTL_SIGNER_REV_UNKNOWN_FLAG = 512U;

		// Token: 0x040001F0 RID: 496
		internal const uint CERT_CHAIN_POLICY_IGNORE_CA_REV_UNKNOWN_FLAG = 1024U;

		// Token: 0x040001F1 RID: 497
		internal const uint CERT_CHAIN_POLICY_IGNORE_ROOT_REV_UNKNOWN_FLAG = 2048U;

		// Token: 0x040001F2 RID: 498
		internal const uint CERT_CHAIN_POLICY_IGNORE_ALL_REV_UNKNOWN_FLAGS = 3840U;

		// Token: 0x040001F3 RID: 499
		internal const uint CERT_TRUST_HAS_EXACT_MATCH_ISSUER = 1U;

		// Token: 0x040001F4 RID: 500
		internal const uint CERT_TRUST_HAS_KEY_MATCH_ISSUER = 2U;

		// Token: 0x040001F5 RID: 501
		internal const uint CERT_TRUST_HAS_NAME_MATCH_ISSUER = 4U;

		// Token: 0x040001F6 RID: 502
		internal const uint CERT_TRUST_IS_SELF_SIGNED = 8U;

		// Token: 0x040001F7 RID: 503
		internal const uint CERT_TRUST_HAS_PREFERRED_ISSUER = 256U;

		// Token: 0x040001F8 RID: 504
		internal const uint CERT_TRUST_HAS_ISSUANCE_CHAIN_POLICY = 512U;

		// Token: 0x040001F9 RID: 505
		internal const uint CERT_TRUST_HAS_VALID_NAME_CONSTRAINTS = 1024U;

		// Token: 0x040001FA RID: 506
		internal const uint CERT_TRUST_IS_COMPLEX_CHAIN = 65536U;

		// Token: 0x040001FB RID: 507
		internal const string szOID_PKIX_NO_SIGNATURE = "1.3.6.1.5.5.7.6.2";

		// Token: 0x040001FC RID: 508
		internal const string szOID_PKIX_KP_SERVER_AUTH = "1.3.6.1.5.5.7.3.1";

		// Token: 0x040001FD RID: 509
		internal const string szOID_PKIX_KP_CLIENT_AUTH = "1.3.6.1.5.5.7.3.2";

		// Token: 0x040001FE RID: 510
		internal const string szOID_PKIX_KP_CODE_SIGNING = "1.3.6.1.5.5.7.3.3";

		// Token: 0x040001FF RID: 511
		internal const string szOID_PKIX_KP_EMAIL_PROTECTION = "1.3.6.1.5.5.7.3.4";

		// Token: 0x04000200 RID: 512
		internal const string SPC_INDIVIDUAL_SP_KEY_PURPOSE_OBJID = "1.3.6.1.4.1.311.2.1.21";

		// Token: 0x04000201 RID: 513
		internal const string SPC_COMMERCIAL_SP_KEY_PURPOSE_OBJID = "1.3.6.1.4.1.311.2.1.22";

		// Token: 0x04000202 RID: 514
		internal const uint HCCE_CURRENT_USER = 0U;

		// Token: 0x04000203 RID: 515
		internal const uint HCCE_LOCAL_MACHINE = 1U;

		// Token: 0x04000204 RID: 516
		internal const string szOID_PKCS_1 = "1.2.840.113549.1.1";

		// Token: 0x04000205 RID: 517
		internal const string szOID_PKCS_2 = "1.2.840.113549.1.2";

		// Token: 0x04000206 RID: 518
		internal const string szOID_PKCS_3 = "1.2.840.113549.1.3";

		// Token: 0x04000207 RID: 519
		internal const string szOID_PKCS_4 = "1.2.840.113549.1.4";

		// Token: 0x04000208 RID: 520
		internal const string szOID_PKCS_5 = "1.2.840.113549.1.5";

		// Token: 0x04000209 RID: 521
		internal const string szOID_PKCS_6 = "1.2.840.113549.1.6";

		// Token: 0x0400020A RID: 522
		internal const string szOID_PKCS_7 = "1.2.840.113549.1.7";

		// Token: 0x0400020B RID: 523
		internal const string szOID_PKCS_8 = "1.2.840.113549.1.8";

		// Token: 0x0400020C RID: 524
		internal const string szOID_PKCS_9 = "1.2.840.113549.1.9";

		// Token: 0x0400020D RID: 525
		internal const string szOID_PKCS_10 = "1.2.840.113549.1.10";

		// Token: 0x0400020E RID: 526
		internal const string szOID_PKCS_12 = "1.2.840.113549.1.12";

		// Token: 0x0400020F RID: 527
		internal const string szOID_RSA_data = "1.2.840.113549.1.7.1";

		// Token: 0x04000210 RID: 528
		internal const string szOID_RSA_signedData = "1.2.840.113549.1.7.2";

		// Token: 0x04000211 RID: 529
		internal const string szOID_RSA_envelopedData = "1.2.840.113549.1.7.3";

		// Token: 0x04000212 RID: 530
		internal const string szOID_RSA_signEnvData = "1.2.840.113549.1.7.4";

		// Token: 0x04000213 RID: 531
		internal const string szOID_RSA_digestedData = "1.2.840.113549.1.7.5";

		// Token: 0x04000214 RID: 532
		internal const string szOID_RSA_hashedData = "1.2.840.113549.1.7.5";

		// Token: 0x04000215 RID: 533
		internal const string szOID_RSA_encryptedData = "1.2.840.113549.1.7.6";

		// Token: 0x04000216 RID: 534
		internal const string szOID_RSA_emailAddr = "1.2.840.113549.1.9.1";

		// Token: 0x04000217 RID: 535
		internal const string szOID_RSA_unstructName = "1.2.840.113549.1.9.2";

		// Token: 0x04000218 RID: 536
		internal const string szOID_RSA_contentType = "1.2.840.113549.1.9.3";

		// Token: 0x04000219 RID: 537
		internal const string szOID_RSA_messageDigest = "1.2.840.113549.1.9.4";

		// Token: 0x0400021A RID: 538
		internal const string szOID_RSA_signingTime = "1.2.840.113549.1.9.5";

		// Token: 0x0400021B RID: 539
		internal const string szOID_RSA_counterSign = "1.2.840.113549.1.9.6";

		// Token: 0x0400021C RID: 540
		internal const string szOID_RSA_challengePwd = "1.2.840.113549.1.9.7";

		// Token: 0x0400021D RID: 541
		internal const string szOID_RSA_unstructAddr = "1.2.840.113549.1.9.8";

		// Token: 0x0400021E RID: 542
		internal const string szOID_RSA_extCertAttrs = "1.2.840.113549.1.9.9";

		// Token: 0x0400021F RID: 543
		internal const string szOID_RSA_SMIMECapabilities = "1.2.840.113549.1.9.15";

		// Token: 0x04000220 RID: 544
		internal const string szOID_CAPICOM = "1.3.6.1.4.1.311.88";

		// Token: 0x04000221 RID: 545
		internal const string szOID_CAPICOM_version = "1.3.6.1.4.1.311.88.1";

		// Token: 0x04000222 RID: 546
		internal const string szOID_CAPICOM_attribute = "1.3.6.1.4.1.311.88.2";

		// Token: 0x04000223 RID: 547
		internal const string szOID_CAPICOM_documentName = "1.3.6.1.4.1.311.88.2.1";

		// Token: 0x04000224 RID: 548
		internal const string szOID_CAPICOM_documentDescription = "1.3.6.1.4.1.311.88.2.2";

		// Token: 0x04000225 RID: 549
		internal const string szOID_CAPICOM_encryptedData = "1.3.6.1.4.1.311.88.3";

		// Token: 0x04000226 RID: 550
		internal const string szOID_CAPICOM_encryptedContent = "1.3.6.1.4.1.311.88.3.1";

		// Token: 0x04000227 RID: 551
		internal const string szOID_OIWSEC_sha1 = "1.3.14.3.2.26";

		// Token: 0x04000228 RID: 552
		internal const string szOID_RSA_MD5 = "1.2.840.113549.2.5";

		// Token: 0x04000229 RID: 553
		internal const string szOID_OIWSEC_SHA256 = "2.16.840.1.101.3.4.1";

		// Token: 0x0400022A RID: 554
		internal const string szOID_OIWSEC_SHA384 = "2.16.840.1.101.3.4.2";

		// Token: 0x0400022B RID: 555
		internal const string szOID_OIWSEC_SHA512 = "2.16.840.1.101.3.4.3";

		// Token: 0x0400022C RID: 556
		internal const string szOID_RSA_RC2CBC = "1.2.840.113549.3.2";

		// Token: 0x0400022D RID: 557
		internal const string szOID_RSA_RC4 = "1.2.840.113549.3.4";

		// Token: 0x0400022E RID: 558
		internal const string szOID_RSA_DES_EDE3_CBC = "1.2.840.113549.3.7";

		// Token: 0x0400022F RID: 559
		internal const string szOID_OIWSEC_desCBC = "1.3.14.3.2.7";

		// Token: 0x04000230 RID: 560
		internal const string szOID_NIST_AES128_CBC = "2.16.840.1.101.3.4.1.2";

		// Token: 0x04000231 RID: 561
		internal const string szOID_NIST_AES192_CBC = "2.16.840.1.101.3.4.1.22";

		// Token: 0x04000232 RID: 562
		internal const string szOID_NIST_AES256_CBC = "2.16.840.1.101.3.4.1.42";

		// Token: 0x04000233 RID: 563
		internal const string szOID_RSA_SMIMEalg = "1.2.840.113549.1.9.16.3";

		// Token: 0x04000234 RID: 564
		internal const string szOID_RSA_SMIMEalgESDH = "1.2.840.113549.1.9.16.3.5";

		// Token: 0x04000235 RID: 565
		internal const string szOID_RSA_SMIMEalgCMS3DESwrap = "1.2.840.113549.1.9.16.3.6";

		// Token: 0x04000236 RID: 566
		internal const string szOID_RSA_SMIMEalgCMSRC2wrap = "1.2.840.113549.1.9.16.3.7";

		// Token: 0x04000237 RID: 567
		internal const string szOID_X957_DSA = "1.2.840.10040.4.1";

		// Token: 0x04000238 RID: 568
		internal const string szOID_X957_sha1DSA = "1.2.840.10040.4.3";

		// Token: 0x04000239 RID: 569
		internal const string szOID_OIWSEC_sha1RSASign = "1.3.14.3.2.29";

		// Token: 0x0400023A RID: 570
		internal const uint CERT_ALT_NAME_OTHER_NAME = 1U;

		// Token: 0x0400023B RID: 571
		internal const uint CERT_ALT_NAME_RFC822_NAME = 2U;

		// Token: 0x0400023C RID: 572
		internal const uint CERT_ALT_NAME_DNS_NAME = 3U;

		// Token: 0x0400023D RID: 573
		internal const uint CERT_ALT_NAME_X400_ADDRESS = 4U;

		// Token: 0x0400023E RID: 574
		internal const uint CERT_ALT_NAME_DIRECTORY_NAME = 5U;

		// Token: 0x0400023F RID: 575
		internal const uint CERT_ALT_NAME_EDI_PARTY_NAME = 6U;

		// Token: 0x04000240 RID: 576
		internal const uint CERT_ALT_NAME_URL = 7U;

		// Token: 0x04000241 RID: 577
		internal const uint CERT_ALT_NAME_IP_ADDRESS = 8U;

		// Token: 0x04000242 RID: 578
		internal const uint CERT_ALT_NAME_REGISTERED_ID = 9U;

		// Token: 0x04000243 RID: 579
		internal const uint CERT_RDN_ANY_TYPE = 0U;

		// Token: 0x04000244 RID: 580
		internal const uint CERT_RDN_ENCODED_BLOB = 1U;

		// Token: 0x04000245 RID: 581
		internal const uint CERT_RDN_OCTET_STRING = 2U;

		// Token: 0x04000246 RID: 582
		internal const uint CERT_RDN_NUMERIC_STRING = 3U;

		// Token: 0x04000247 RID: 583
		internal const uint CERT_RDN_PRINTABLE_STRING = 4U;

		// Token: 0x04000248 RID: 584
		internal const uint CERT_RDN_TELETEX_STRING = 5U;

		// Token: 0x04000249 RID: 585
		internal const uint CERT_RDN_T61_STRING = 5U;

		// Token: 0x0400024A RID: 586
		internal const uint CERT_RDN_VIDEOTEX_STRING = 6U;

		// Token: 0x0400024B RID: 587
		internal const uint CERT_RDN_IA5_STRING = 7U;

		// Token: 0x0400024C RID: 588
		internal const uint CERT_RDN_GRAPHIC_STRING = 8U;

		// Token: 0x0400024D RID: 589
		internal const uint CERT_RDN_VISIBLE_STRING = 9U;

		// Token: 0x0400024E RID: 590
		internal const uint CERT_RDN_ISO646_STRING = 9U;

		// Token: 0x0400024F RID: 591
		internal const uint CERT_RDN_GENERAL_STRING = 10U;

		// Token: 0x04000250 RID: 592
		internal const uint CERT_RDN_UNIVERSAL_STRING = 11U;

		// Token: 0x04000251 RID: 593
		internal const uint CERT_RDN_INT4_STRING = 11U;

		// Token: 0x04000252 RID: 594
		internal const uint CERT_RDN_BMP_STRING = 12U;

		// Token: 0x04000253 RID: 595
		internal const uint CERT_RDN_UNICODE_STRING = 12U;

		// Token: 0x04000254 RID: 596
		internal const uint CERT_RDN_UTF8_STRING = 13U;

		// Token: 0x04000255 RID: 597
		internal const uint CERT_RDN_TYPE_MASK = 255U;

		// Token: 0x04000256 RID: 598
		internal const uint CERT_RDN_FLAGS_MASK = 4278190080U;

		// Token: 0x04000257 RID: 599
		internal const uint CERT_STORE_CTRL_RESYNC = 1U;

		// Token: 0x04000258 RID: 600
		internal const uint CERT_STORE_CTRL_NOTIFY_CHANGE = 2U;

		// Token: 0x04000259 RID: 601
		internal const uint CERT_STORE_CTRL_COMMIT = 3U;

		// Token: 0x0400025A RID: 602
		internal const uint CERT_STORE_CTRL_AUTO_RESYNC = 4U;

		// Token: 0x0400025B RID: 603
		internal const uint CERT_STORE_CTRL_CANCEL_NOTIFY = 5U;

		// Token: 0x0400025C RID: 604
		internal const uint CERT_ID_ISSUER_SERIAL_NUMBER = 1U;

		// Token: 0x0400025D RID: 605
		internal const uint CERT_ID_KEY_IDENTIFIER = 2U;

		// Token: 0x0400025E RID: 606
		internal const uint CERT_ID_SHA1_HASH = 3U;

		// Token: 0x0400025F RID: 607
		internal const string MS_ENHANCED_PROV = "Microsoft Enhanced Cryptographic Provider v1.0";

		// Token: 0x04000260 RID: 608
		internal const string MS_STRONG_PROV = "Microsoft Strong Cryptographic Provider";

		// Token: 0x04000261 RID: 609
		internal const string MS_DEF_PROV = "Microsoft Base Cryptographic Provider v1.0";

		// Token: 0x04000262 RID: 610
		internal const string MS_DEF_DSS_DH_PROV = "Microsoft Base DSS and Diffie-Hellman Cryptographic Provider";

		// Token: 0x04000263 RID: 611
		internal const string MS_ENH_DSS_DH_PROV = "Microsoft Enhanced DSS and Diffie-Hellman Cryptographic Provider";

		// Token: 0x04000264 RID: 612
		internal const string DummySignerCommonName = "CN=Dummy Signer";

		// Token: 0x04000265 RID: 613
		internal const uint PROV_RSA_FULL = 1U;

		// Token: 0x04000266 RID: 614
		internal const uint PROV_DSS_DH = 13U;

		// Token: 0x04000267 RID: 615
		internal const uint ALG_TYPE_ANY = 0U;

		// Token: 0x04000268 RID: 616
		internal const uint ALG_TYPE_DSS = 512U;

		// Token: 0x04000269 RID: 617
		internal const uint ALG_TYPE_RSA = 1024U;

		// Token: 0x0400026A RID: 618
		internal const uint ALG_TYPE_BLOCK = 1536U;

		// Token: 0x0400026B RID: 619
		internal const uint ALG_TYPE_STREAM = 2048U;

		// Token: 0x0400026C RID: 620
		internal const uint ALG_TYPE_DH = 2560U;

		// Token: 0x0400026D RID: 621
		internal const uint ALG_TYPE_SECURECHANNEL = 3072U;

		// Token: 0x0400026E RID: 622
		internal const uint ALG_CLASS_ANY = 0U;

		// Token: 0x0400026F RID: 623
		internal const uint ALG_CLASS_SIGNATURE = 8192U;

		// Token: 0x04000270 RID: 624
		internal const uint ALG_CLASS_MSG_ENCRYPT = 16384U;

		// Token: 0x04000271 RID: 625
		internal const uint ALG_CLASS_DATA_ENCRYPT = 24576U;

		// Token: 0x04000272 RID: 626
		internal const uint ALG_CLASS_HASH = 32768U;

		// Token: 0x04000273 RID: 627
		internal const uint ALG_CLASS_KEY_EXCHANGE = 40960U;

		// Token: 0x04000274 RID: 628
		internal const uint ALG_CLASS_ALL = 57344U;

		// Token: 0x04000275 RID: 629
		internal const uint ALG_SID_ANY = 0U;

		// Token: 0x04000276 RID: 630
		internal const uint ALG_SID_RSA_ANY = 0U;

		// Token: 0x04000277 RID: 631
		internal const uint ALG_SID_RSA_PKCS = 1U;

		// Token: 0x04000278 RID: 632
		internal const uint ALG_SID_RSA_MSATWORK = 2U;

		// Token: 0x04000279 RID: 633
		internal const uint ALG_SID_RSA_ENTRUST = 3U;

		// Token: 0x0400027A RID: 634
		internal const uint ALG_SID_RSA_PGP = 4U;

		// Token: 0x0400027B RID: 635
		internal const uint ALG_SID_DSS_ANY = 0U;

		// Token: 0x0400027C RID: 636
		internal const uint ALG_SID_DSS_PKCS = 1U;

		// Token: 0x0400027D RID: 637
		internal const uint ALG_SID_DSS_DMS = 2U;

		// Token: 0x0400027E RID: 638
		internal const uint ALG_SID_DES = 1U;

		// Token: 0x0400027F RID: 639
		internal const uint ALG_SID_3DES = 3U;

		// Token: 0x04000280 RID: 640
		internal const uint ALG_SID_DESX = 4U;

		// Token: 0x04000281 RID: 641
		internal const uint ALG_SID_IDEA = 5U;

		// Token: 0x04000282 RID: 642
		internal const uint ALG_SID_CAST = 6U;

		// Token: 0x04000283 RID: 643
		internal const uint ALG_SID_SAFERSK64 = 7U;

		// Token: 0x04000284 RID: 644
		internal const uint ALG_SID_SAFERSK128 = 8U;

		// Token: 0x04000285 RID: 645
		internal const uint ALG_SID_3DES_112 = 9U;

		// Token: 0x04000286 RID: 646
		internal const uint ALG_SID_CYLINK_MEK = 12U;

		// Token: 0x04000287 RID: 647
		internal const uint ALG_SID_RC5 = 13U;

		// Token: 0x04000288 RID: 648
		internal const uint ALG_SID_AES_128 = 14U;

		// Token: 0x04000289 RID: 649
		internal const uint ALG_SID_AES_192 = 15U;

		// Token: 0x0400028A RID: 650
		internal const uint ALG_SID_AES_256 = 16U;

		// Token: 0x0400028B RID: 651
		internal const uint ALG_SID_AES = 17U;

		// Token: 0x0400028C RID: 652
		internal const uint ALG_SID_SKIPJACK = 10U;

		// Token: 0x0400028D RID: 653
		internal const uint ALG_SID_TEK = 11U;

		// Token: 0x0400028E RID: 654
		internal const uint ALG_SID_RC2 = 2U;

		// Token: 0x0400028F RID: 655
		internal const uint ALG_SID_RC4 = 1U;

		// Token: 0x04000290 RID: 656
		internal const uint ALG_SID_SEAL = 2U;

		// Token: 0x04000291 RID: 657
		internal const uint ALG_SID_DH_SANDF = 1U;

		// Token: 0x04000292 RID: 658
		internal const uint ALG_SID_DH_EPHEM = 2U;

		// Token: 0x04000293 RID: 659
		internal const uint ALG_SID_AGREED_KEY_ANY = 3U;

		// Token: 0x04000294 RID: 660
		internal const uint ALG_SID_KEA = 4U;

		// Token: 0x04000295 RID: 661
		internal const uint ALG_SID_MD2 = 1U;

		// Token: 0x04000296 RID: 662
		internal const uint ALG_SID_MD4 = 2U;

		// Token: 0x04000297 RID: 663
		internal const uint ALG_SID_MD5 = 3U;

		// Token: 0x04000298 RID: 664
		internal const uint ALG_SID_SHA = 4U;

		// Token: 0x04000299 RID: 665
		internal const uint ALG_SID_SHA1 = 4U;

		// Token: 0x0400029A RID: 666
		internal const uint ALG_SID_MAC = 5U;

		// Token: 0x0400029B RID: 667
		internal const uint ALG_SID_RIPEMD = 6U;

		// Token: 0x0400029C RID: 668
		internal const uint ALG_SID_RIPEMD160 = 7U;

		// Token: 0x0400029D RID: 669
		internal const uint ALG_SID_SSL3SHAMD5 = 8U;

		// Token: 0x0400029E RID: 670
		internal const uint ALG_SID_HMAC = 9U;

		// Token: 0x0400029F RID: 671
		internal const uint ALG_SID_TLS1PRF = 10U;

		// Token: 0x040002A0 RID: 672
		internal const uint ALG_SID_HASH_REPLACE_OWF = 11U;

		// Token: 0x040002A1 RID: 673
		internal const uint ALG_SID_SSL3_MASTER = 1U;

		// Token: 0x040002A2 RID: 674
		internal const uint ALG_SID_SCHANNEL_MASTER_HASH = 2U;

		// Token: 0x040002A3 RID: 675
		internal const uint ALG_SID_SCHANNEL_MAC_KEY = 3U;

		// Token: 0x040002A4 RID: 676
		internal const uint ALG_SID_PCT1_MASTER = 4U;

		// Token: 0x040002A5 RID: 677
		internal const uint ALG_SID_SSL2_MASTER = 5U;

		// Token: 0x040002A6 RID: 678
		internal const uint ALG_SID_TLS1_MASTER = 6U;

		// Token: 0x040002A7 RID: 679
		internal const uint ALG_SID_SCHANNEL_ENC_KEY = 7U;

		// Token: 0x040002A8 RID: 680
		internal const uint CALG_MD2 = 32769U;

		// Token: 0x040002A9 RID: 681
		internal const uint CALG_MD4 = 32770U;

		// Token: 0x040002AA RID: 682
		internal const uint CALG_MD5 = 32771U;

		// Token: 0x040002AB RID: 683
		internal const uint CALG_SHA = 32772U;

		// Token: 0x040002AC RID: 684
		internal const uint CALG_SHA1 = 32772U;

		// Token: 0x040002AD RID: 685
		internal const uint CALG_MAC = 32773U;

		// Token: 0x040002AE RID: 686
		internal const uint CALG_RSA_SIGN = 9216U;

		// Token: 0x040002AF RID: 687
		internal const uint CALG_DSS_SIGN = 8704U;

		// Token: 0x040002B0 RID: 688
		internal const uint CALG_NO_SIGN = 8192U;

		// Token: 0x040002B1 RID: 689
		internal const uint CALG_RSA_KEYX = 41984U;

		// Token: 0x040002B2 RID: 690
		internal const uint CALG_DES = 26113U;

		// Token: 0x040002B3 RID: 691
		internal const uint CALG_3DES_112 = 26121U;

		// Token: 0x040002B4 RID: 692
		internal const uint CALG_3DES = 26115U;

		// Token: 0x040002B5 RID: 693
		internal const uint CALG_DESX = 26116U;

		// Token: 0x040002B6 RID: 694
		internal const uint CALG_RC2 = 26114U;

		// Token: 0x040002B7 RID: 695
		internal const uint CALG_RC4 = 26625U;

		// Token: 0x040002B8 RID: 696
		internal const uint CALG_SEAL = 26626U;

		// Token: 0x040002B9 RID: 697
		internal const uint CALG_DH_SF = 43521U;

		// Token: 0x040002BA RID: 698
		internal const uint CALG_DH_EPHEM = 43522U;

		// Token: 0x040002BB RID: 699
		internal const uint CALG_AGREEDKEY_ANY = 43523U;

		// Token: 0x040002BC RID: 700
		internal const uint CALG_KEA_KEYX = 43524U;

		// Token: 0x040002BD RID: 701
		internal const uint CALG_HUGHES_MD5 = 40963U;

		// Token: 0x040002BE RID: 702
		internal const uint CALG_SKIPJACK = 26122U;

		// Token: 0x040002BF RID: 703
		internal const uint CALG_TEK = 26123U;

		// Token: 0x040002C0 RID: 704
		internal const uint CALG_CYLINK_MEK = 26124U;

		// Token: 0x040002C1 RID: 705
		internal const uint CALG_SSL3_SHAMD5 = 32776U;

		// Token: 0x040002C2 RID: 706
		internal const uint CALG_SSL3_MASTER = 19457U;

		// Token: 0x040002C3 RID: 707
		internal const uint CALG_SCHANNEL_MASTER_HASH = 19458U;

		// Token: 0x040002C4 RID: 708
		internal const uint CALG_SCHANNEL_MAC_KEY = 19459U;

		// Token: 0x040002C5 RID: 709
		internal const uint CALG_SCHANNEL_ENC_KEY = 19463U;

		// Token: 0x040002C6 RID: 710
		internal const uint CALG_PCT1_MASTER = 19460U;

		// Token: 0x040002C7 RID: 711
		internal const uint CALG_SSL2_MASTER = 19461U;

		// Token: 0x040002C8 RID: 712
		internal const uint CALG_TLS1_MASTER = 19462U;

		// Token: 0x040002C9 RID: 713
		internal const uint CALG_RC5 = 26125U;

		// Token: 0x040002CA RID: 714
		internal const uint CALG_HMAC = 32777U;

		// Token: 0x040002CB RID: 715
		internal const uint CALG_TLS1PRF = 32778U;

		// Token: 0x040002CC RID: 716
		internal const uint CALG_HASH_REPLACE_OWF = 32779U;

		// Token: 0x040002CD RID: 717
		internal const uint CALG_AES_128 = 26126U;

		// Token: 0x040002CE RID: 718
		internal const uint CALG_AES_192 = 26127U;

		// Token: 0x040002CF RID: 719
		internal const uint CALG_AES_256 = 26128U;

		// Token: 0x040002D0 RID: 720
		internal const uint CALG_AES = 26129U;

		// Token: 0x040002D1 RID: 721
		internal const uint CRYPT_FIRST = 1U;

		// Token: 0x040002D2 RID: 722
		internal const uint CRYPT_NEXT = 2U;

		// Token: 0x040002D3 RID: 723
		internal const uint PP_ENUMALGS_EX = 22U;

		// Token: 0x040002D4 RID: 724
		internal const uint CRYPT_VERIFYCONTEXT = 4026531840U;

		// Token: 0x040002D5 RID: 725
		internal const uint CRYPT_NEWKEYSET = 8U;

		// Token: 0x040002D6 RID: 726
		internal const uint CRYPT_DELETEKEYSET = 16U;

		// Token: 0x040002D7 RID: 727
		internal const uint CRYPT_MACHINE_KEYSET = 32U;

		// Token: 0x040002D8 RID: 728
		internal const uint CRYPT_SILENT = 64U;

		// Token: 0x040002D9 RID: 729
		internal const uint CRYPT_USER_KEYSET = 4096U;

		// Token: 0x040002DA RID: 730
		internal const uint PKCS12_ALWAYS_CNG_KSP = 512U;

		// Token: 0x040002DB RID: 731
		internal const uint PKCS12_NO_PERSIST_KEY = 32768U;

		// Token: 0x040002DC RID: 732
		internal const uint CRYPT_EXPORTABLE = 1U;

		// Token: 0x040002DD RID: 733
		internal const uint CRYPT_USER_PROTECTED = 2U;

		// Token: 0x040002DE RID: 734
		internal const uint CRYPT_CREATE_SALT = 4U;

		// Token: 0x040002DF RID: 735
		internal const uint CRYPT_UPDATE_KEY = 8U;

		// Token: 0x040002E0 RID: 736
		internal const uint CRYPT_NO_SALT = 16U;

		// Token: 0x040002E1 RID: 737
		internal const uint CRYPT_PREGEN = 64U;

		// Token: 0x040002E2 RID: 738
		internal const uint CRYPT_RECIPIENT = 16U;

		// Token: 0x040002E3 RID: 739
		internal const uint CRYPT_INITIATOR = 64U;

		// Token: 0x040002E4 RID: 740
		internal const uint CRYPT_ONLINE = 128U;

		// Token: 0x040002E5 RID: 741
		internal const uint CRYPT_SF = 256U;

		// Token: 0x040002E6 RID: 742
		internal const uint CRYPT_CREATE_IV = 512U;

		// Token: 0x040002E7 RID: 743
		internal const uint CRYPT_KEK = 1024U;

		// Token: 0x040002E8 RID: 744
		internal const uint CRYPT_DATA_KEY = 2048U;

		// Token: 0x040002E9 RID: 745
		internal const uint CRYPT_VOLATILE = 4096U;

		// Token: 0x040002EA RID: 746
		internal const uint CRYPT_SGCKEY = 8192U;

		// Token: 0x040002EB RID: 747
		internal const uint CRYPT_ARCHIVABLE = 16384U;

		// Token: 0x040002EC RID: 748
		internal const byte CUR_BLOB_VERSION = 2;

		// Token: 0x040002ED RID: 749
		internal const byte SIMPLEBLOB = 1;

		// Token: 0x040002EE RID: 750
		internal const byte PUBLICKEYBLOB = 6;

		// Token: 0x040002EF RID: 751
		internal const byte PRIVATEKEYBLOB = 7;

		// Token: 0x040002F0 RID: 752
		internal const byte PLAINTEXTKEYBLOB = 8;

		// Token: 0x040002F1 RID: 753
		internal const byte OPAQUEKEYBLOB = 9;

		// Token: 0x040002F2 RID: 754
		internal const byte PUBLICKEYBLOBEX = 10;

		// Token: 0x040002F3 RID: 755
		internal const byte SYMMETRICWRAPKEYBLOB = 11;

		// Token: 0x040002F4 RID: 756
		internal const uint DSS_MAGIC = 827544388U;

		// Token: 0x040002F5 RID: 757
		internal const uint DSS_PRIVATE_MAGIC = 844321604U;

		// Token: 0x040002F6 RID: 758
		internal const uint DSS_PUB_MAGIC_VER3 = 861098820U;

		// Token: 0x040002F7 RID: 759
		internal const uint DSS_PRIV_MAGIC_VER3 = 877876036U;

		// Token: 0x040002F8 RID: 760
		internal const uint RSA_PUB_MAGIC = 826364754U;

		// Token: 0x040002F9 RID: 761
		internal const uint RSA_PRIV_MAGIC = 843141970U;

		// Token: 0x040002FA RID: 762
		internal const uint CRYPT_ACQUIRE_CACHE_FLAG = 1U;

		// Token: 0x040002FB RID: 763
		internal const uint CRYPT_ACQUIRE_USE_PROV_INFO_FLAG = 2U;

		// Token: 0x040002FC RID: 764
		internal const uint CRYPT_ACQUIRE_COMPARE_KEY_FLAG = 4U;

		// Token: 0x040002FD RID: 765
		internal const uint CRYPT_ACQUIRE_SILENT_FLAG = 64U;

		// Token: 0x040002FE RID: 766
		internal const uint CRYPT_ACQUIRE_PREFER_NCRYPT_KEY_FLAG = 131072U;

		// Token: 0x040002FF RID: 767
		internal const uint CMSG_BARE_CONTENT_FLAG = 1U;

		// Token: 0x04000300 RID: 768
		internal const uint CMSG_LENGTH_ONLY_FLAG = 2U;

		// Token: 0x04000301 RID: 769
		internal const uint CMSG_DETACHED_FLAG = 4U;

		// Token: 0x04000302 RID: 770
		internal const uint CMSG_AUTHENTICATED_ATTRIBUTES_FLAG = 8U;

		// Token: 0x04000303 RID: 771
		internal const uint CMSG_CONTENTS_OCTETS_FLAG = 16U;

		// Token: 0x04000304 RID: 772
		internal const uint CMSG_MAX_LENGTH_FLAG = 32U;

		// Token: 0x04000305 RID: 773
		internal const uint CMSG_TYPE_PARAM = 1U;

		// Token: 0x04000306 RID: 774
		internal const uint CMSG_CONTENT_PARAM = 2U;

		// Token: 0x04000307 RID: 775
		internal const uint CMSG_BARE_CONTENT_PARAM = 3U;

		// Token: 0x04000308 RID: 776
		internal const uint CMSG_INNER_CONTENT_TYPE_PARAM = 4U;

		// Token: 0x04000309 RID: 777
		internal const uint CMSG_SIGNER_COUNT_PARAM = 5U;

		// Token: 0x0400030A RID: 778
		internal const uint CMSG_SIGNER_INFO_PARAM = 6U;

		// Token: 0x0400030B RID: 779
		internal const uint CMSG_SIGNER_CERT_INFO_PARAM = 7U;

		// Token: 0x0400030C RID: 780
		internal const uint CMSG_SIGNER_HASH_ALGORITHM_PARAM = 8U;

		// Token: 0x0400030D RID: 781
		internal const uint CMSG_SIGNER_AUTH_ATTR_PARAM = 9U;

		// Token: 0x0400030E RID: 782
		internal const uint CMSG_SIGNER_UNAUTH_ATTR_PARAM = 10U;

		// Token: 0x0400030F RID: 783
		internal const uint CMSG_CERT_COUNT_PARAM = 11U;

		// Token: 0x04000310 RID: 784
		internal const uint CMSG_CERT_PARAM = 12U;

		// Token: 0x04000311 RID: 785
		internal const uint CMSG_CRL_COUNT_PARAM = 13U;

		// Token: 0x04000312 RID: 786
		internal const uint CMSG_CRL_PARAM = 14U;

		// Token: 0x04000313 RID: 787
		internal const uint CMSG_ENVELOPE_ALGORITHM_PARAM = 15U;

		// Token: 0x04000314 RID: 788
		internal const uint CMSG_RECIPIENT_COUNT_PARAM = 17U;

		// Token: 0x04000315 RID: 789
		internal const uint CMSG_RECIPIENT_INDEX_PARAM = 18U;

		// Token: 0x04000316 RID: 790
		internal const uint CMSG_RECIPIENT_INFO_PARAM = 19U;

		// Token: 0x04000317 RID: 791
		internal const uint CMSG_HASH_ALGORITHM_PARAM = 20U;

		// Token: 0x04000318 RID: 792
		internal const uint CMSG_HASH_DATA_PARAM = 21U;

		// Token: 0x04000319 RID: 793
		internal const uint CMSG_COMPUTED_HASH_PARAM = 22U;

		// Token: 0x0400031A RID: 794
		internal const uint CMSG_ENCRYPT_PARAM = 26U;

		// Token: 0x0400031B RID: 795
		internal const uint CMSG_ENCRYPTED_DIGEST = 27U;

		// Token: 0x0400031C RID: 796
		internal const uint CMSG_ENCODED_SIGNER = 28U;

		// Token: 0x0400031D RID: 797
		internal const uint CMSG_ENCODED_MESSAGE = 29U;

		// Token: 0x0400031E RID: 798
		internal const uint CMSG_VERSION_PARAM = 30U;

		// Token: 0x0400031F RID: 799
		internal const uint CMSG_ATTR_CERT_COUNT_PARAM = 31U;

		// Token: 0x04000320 RID: 800
		internal const uint CMSG_ATTR_CERT_PARAM = 32U;

		// Token: 0x04000321 RID: 801
		internal const uint CMSG_CMS_RECIPIENT_COUNT_PARAM = 33U;

		// Token: 0x04000322 RID: 802
		internal const uint CMSG_CMS_RECIPIENT_INDEX_PARAM = 34U;

		// Token: 0x04000323 RID: 803
		internal const uint CMSG_CMS_RECIPIENT_ENCRYPTED_KEY_INDEX_PARAM = 35U;

		// Token: 0x04000324 RID: 804
		internal const uint CMSG_CMS_RECIPIENT_INFO_PARAM = 36U;

		// Token: 0x04000325 RID: 805
		internal const uint CMSG_UNPROTECTED_ATTR_PARAM = 37U;

		// Token: 0x04000326 RID: 806
		internal const uint CMSG_SIGNER_CERT_ID_PARAM = 38U;

		// Token: 0x04000327 RID: 807
		internal const uint CMSG_CMS_SIGNER_INFO_PARAM = 39U;

		// Token: 0x04000328 RID: 808
		internal const uint CMSG_CTRL_VERIFY_SIGNATURE = 1U;

		// Token: 0x04000329 RID: 809
		internal const uint CMSG_CTRL_DECRYPT = 2U;

		// Token: 0x0400032A RID: 810
		internal const uint CMSG_CTRL_VERIFY_HASH = 5U;

		// Token: 0x0400032B RID: 811
		internal const uint CMSG_CTRL_ADD_SIGNER = 6U;

		// Token: 0x0400032C RID: 812
		internal const uint CMSG_CTRL_DEL_SIGNER = 7U;

		// Token: 0x0400032D RID: 813
		internal const uint CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR = 8U;

		// Token: 0x0400032E RID: 814
		internal const uint CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR = 9U;

		// Token: 0x0400032F RID: 815
		internal const uint CMSG_CTRL_ADD_CERT = 10U;

		// Token: 0x04000330 RID: 816
		internal const uint CMSG_CTRL_DEL_CERT = 11U;

		// Token: 0x04000331 RID: 817
		internal const uint CMSG_CTRL_ADD_CRL = 12U;

		// Token: 0x04000332 RID: 818
		internal const uint CMSG_CTRL_DEL_CRL = 13U;

		// Token: 0x04000333 RID: 819
		internal const uint CMSG_CTRL_ADD_ATTR_CERT = 14U;

		// Token: 0x04000334 RID: 820
		internal const uint CMSG_CTRL_DEL_ATTR_CERT = 15U;

		// Token: 0x04000335 RID: 821
		internal const uint CMSG_CTRL_KEY_TRANS_DECRYPT = 16U;

		// Token: 0x04000336 RID: 822
		internal const uint CMSG_CTRL_KEY_AGREE_DECRYPT = 17U;

		// Token: 0x04000337 RID: 823
		internal const uint CMSG_CTRL_MAIL_LIST_DECRYPT = 18U;

		// Token: 0x04000338 RID: 824
		internal const uint CMSG_CTRL_VERIFY_SIGNATURE_EX = 19U;

		// Token: 0x04000339 RID: 825
		internal const uint CMSG_CTRL_ADD_CMS_SIGNER_INFO = 20U;

		// Token: 0x0400033A RID: 826
		internal const uint CMSG_VERIFY_SIGNER_PUBKEY = 1U;

		// Token: 0x0400033B RID: 827
		internal const uint CMSG_VERIFY_SIGNER_CERT = 2U;

		// Token: 0x0400033C RID: 828
		internal const uint CMSG_VERIFY_SIGNER_CHAIN = 3U;

		// Token: 0x0400033D RID: 829
		internal const uint CMSG_VERIFY_SIGNER_NULL = 4U;

		// Token: 0x0400033E RID: 830
		internal const uint CMSG_DATA = 1U;

		// Token: 0x0400033F RID: 831
		internal const uint CMSG_SIGNED = 2U;

		// Token: 0x04000340 RID: 832
		internal const uint CMSG_ENVELOPED = 3U;

		// Token: 0x04000341 RID: 833
		internal const uint CMSG_SIGNED_AND_ENVELOPED = 4U;

		// Token: 0x04000342 RID: 834
		internal const uint CMSG_HASHED = 5U;

		// Token: 0x04000343 RID: 835
		internal const uint CMSG_ENCRYPTED = 6U;

		// Token: 0x04000344 RID: 836
		internal const uint CMSG_KEY_TRANS_RECIPIENT = 1U;

		// Token: 0x04000345 RID: 837
		internal const uint CMSG_KEY_AGREE_RECIPIENT = 2U;

		// Token: 0x04000346 RID: 838
		internal const uint CMSG_MAIL_LIST_RECIPIENT = 3U;

		// Token: 0x04000347 RID: 839
		internal const uint CMSG_KEY_AGREE_ORIGINATOR_CERT = 1U;

		// Token: 0x04000348 RID: 840
		internal const uint CMSG_KEY_AGREE_ORIGINATOR_PUBLIC_KEY = 2U;

		// Token: 0x04000349 RID: 841
		internal const uint CMSG_KEY_AGREE_EPHEMERAL_KEY_CHOICE = 1U;

		// Token: 0x0400034A RID: 842
		internal const uint CMSG_KEY_AGREE_STATIC_KEY_CHOICE = 2U;

		// Token: 0x0400034B RID: 843
		internal const uint CMSG_ENVELOPED_RECIPIENT_V0 = 0U;

		// Token: 0x0400034C RID: 844
		internal const uint CMSG_ENVELOPED_RECIPIENT_V2 = 2U;

		// Token: 0x0400034D RID: 845
		internal const uint CMSG_ENVELOPED_RECIPIENT_V3 = 3U;

		// Token: 0x0400034E RID: 846
		internal const uint CMSG_ENVELOPED_RECIPIENT_V4 = 4U;

		// Token: 0x0400034F RID: 847
		internal const uint CMSG_KEY_TRANS_PKCS_1_5_VERSION = 0U;

		// Token: 0x04000350 RID: 848
		internal const uint CMSG_KEY_TRANS_CMS_VERSION = 2U;

		// Token: 0x04000351 RID: 849
		internal const uint CMSG_KEY_AGREE_VERSION = 3U;

		// Token: 0x04000352 RID: 850
		internal const uint CMSG_MAIL_LIST_VERSION = 4U;

		// Token: 0x04000353 RID: 851
		internal const uint CRYPT_RC2_40BIT_VERSION = 160U;

		// Token: 0x04000354 RID: 852
		internal const uint CRYPT_RC2_56BIT_VERSION = 52U;

		// Token: 0x04000355 RID: 853
		internal const uint CRYPT_RC2_64BIT_VERSION = 120U;

		// Token: 0x04000356 RID: 854
		internal const uint CRYPT_RC2_128BIT_VERSION = 58U;

		// Token: 0x04000357 RID: 855
		internal const int E_NOTIMPL = -2147483647;

		// Token: 0x04000358 RID: 856
		internal const int E_FILENOTFOUND = -2147024894;

		// Token: 0x04000359 RID: 857
		internal const int E_OUTOFMEMORY = -2147024882;

		// Token: 0x0400035A RID: 858
		internal const int NTE_NO_KEY = -2146893811;

		// Token: 0x0400035B RID: 859
		internal const int NTE_BAD_PUBLIC_KEY = -2146893803;

		// Token: 0x0400035C RID: 860
		internal const int NTE_BAD_KEYSET = -2146893802;

		// Token: 0x0400035D RID: 861
		internal const int CRYPT_E_MSG_ERROR = -2146889727;

		// Token: 0x0400035E RID: 862
		internal const int CRYPT_E_UNKNOWN_ALGO = -2146889726;

		// Token: 0x0400035F RID: 863
		internal const int CRYPT_E_INVALID_MSG_TYPE = -2146889724;

		// Token: 0x04000360 RID: 864
		internal const int CRYPT_E_RECIPIENT_NOT_FOUND = -2146889717;

		// Token: 0x04000361 RID: 865
		internal const int CRYPT_E_SIGNER_NOT_FOUND = -2146889714;

		// Token: 0x04000362 RID: 866
		internal const int CRYPT_E_ATTRIBUTES_MISSING = -2146889713;

		// Token: 0x04000363 RID: 867
		internal const int CRYPT_E_BAD_ENCODE = -2146885630;

		// Token: 0x04000364 RID: 868
		internal const int CRYPT_E_NOT_FOUND = -2146885628;

		// Token: 0x04000365 RID: 869
		internal const int CRYPT_E_NO_MATCH = -2146885623;

		// Token: 0x04000366 RID: 870
		internal const int CRYPT_E_NO_SIGNER = -2146885618;

		// Token: 0x04000367 RID: 871
		internal const int CRYPT_E_REVOKED = -2146885616;

		// Token: 0x04000368 RID: 872
		internal const int CRYPT_E_NO_REVOCATION_CHECK = -2146885614;

		// Token: 0x04000369 RID: 873
		internal const int CRYPT_E_REVOCATION_OFFLINE = -2146885613;

		// Token: 0x0400036A RID: 874
		internal const int CRYPT_E_ASN1_BADTAG = -2146881269;

		// Token: 0x0400036B RID: 875
		internal const int TRUST_E_CERT_SIGNATURE = -2146869244;

		// Token: 0x0400036C RID: 876
		internal const int TRUST_E_BASIC_CONSTRAINTS = -2146869223;

		// Token: 0x0400036D RID: 877
		internal const int CERT_E_EXPIRED = -2146762495;

		// Token: 0x0400036E RID: 878
		internal const int CERT_E_VALIDITYPERIODNESTING = -2146762494;

		// Token: 0x0400036F RID: 879
		internal const int CERT_E_UNTRUSTEDROOT = -2146762487;

		// Token: 0x04000370 RID: 880
		internal const int CERT_E_CHAINING = -2146762486;

		// Token: 0x04000371 RID: 881
		internal const int TRUST_E_FAIL = -2146762485;

		// Token: 0x04000372 RID: 882
		internal const int CERT_E_REVOKED = -2146762484;

		// Token: 0x04000373 RID: 883
		internal const int CERT_E_UNTRUSTEDTESTROOT = -2146762483;

		// Token: 0x04000374 RID: 884
		internal const int CERT_E_REVOCATION_FAILURE = -2146762482;

		// Token: 0x04000375 RID: 885
		internal const int CERT_E_WRONG_USAGE = -2146762480;

		// Token: 0x04000376 RID: 886
		internal const int CERT_E_INVALID_POLICY = -2146762477;

		// Token: 0x04000377 RID: 887
		internal const int CERT_E_INVALID_NAME = -2146762476;

		// Token: 0x04000378 RID: 888
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x04000379 RID: 889
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x0400037A RID: 890
		internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;

		// Token: 0x0400037B RID: 891
		internal const int ERROR_CANCELLED = 1223;

		// Token: 0x02000094 RID: 148
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct BLOBHEADER
		{
			// Token: 0x04000535 RID: 1333
			internal byte bType;

			// Token: 0x04000536 RID: 1334
			internal byte bVersion;

			// Token: 0x04000537 RID: 1335
			internal short reserved;

			// Token: 0x04000538 RID: 1336
			internal uint aiKeyAlg;
		}

		// Token: 0x02000095 RID: 149
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_ALT_NAME_INFO
		{
			// Token: 0x04000539 RID: 1337
			internal uint cAltEntry;

			// Token: 0x0400053A RID: 1338
			internal IntPtr rgAltEntry;
		}

		// Token: 0x02000096 RID: 150
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_BASIC_CONSTRAINTS_INFO
		{
			// Token: 0x0400053B RID: 1339
			internal CAPI.CRYPT_BIT_BLOB SubjectType;

			// Token: 0x0400053C RID: 1340
			internal bool fPathLenConstraint;

			// Token: 0x0400053D RID: 1341
			internal uint dwPathLenConstraint;

			// Token: 0x0400053E RID: 1342
			internal uint cSubtreesConstraint;

			// Token: 0x0400053F RID: 1343
			internal IntPtr rgSubtreesConstraint;
		}

		// Token: 0x02000097 RID: 151
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_BASIC_CONSTRAINTS2_INFO
		{
			// Token: 0x04000540 RID: 1344
			internal int fCA;

			// Token: 0x04000541 RID: 1345
			internal int fPathLenConstraint;

			// Token: 0x04000542 RID: 1346
			internal uint dwPathLenConstraint;
		}

		// Token: 0x02000098 RID: 152
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_PARA
		{
			// Token: 0x04000543 RID: 1347
			internal uint cbSize;

			// Token: 0x04000544 RID: 1348
			internal CAPI.CERT_USAGE_MATCH RequestedUsage;

			// Token: 0x04000545 RID: 1349
			internal CAPI.CERT_USAGE_MATCH RequestedIssuancePolicy;

			// Token: 0x04000546 RID: 1350
			internal uint dwUrlRetrievalTimeout;

			// Token: 0x04000547 RID: 1351
			internal bool fCheckRevocationFreshnessTime;

			// Token: 0x04000548 RID: 1352
			internal uint dwRevocationFreshnessTime;
		}

		// Token: 0x02000099 RID: 153
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_POLICY_PARA
		{
			// Token: 0x06000556 RID: 1366 RVA: 0x0001BC5F File Offset: 0x00019E5F
			internal CERT_CHAIN_POLICY_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.dwFlags = 0U;
				this.pvExtraPolicyPara = IntPtr.Zero;
			}

			// Token: 0x04000549 RID: 1353
			internal uint cbSize;

			// Token: 0x0400054A RID: 1354
			internal uint dwFlags;

			// Token: 0x0400054B RID: 1355
			internal IntPtr pvExtraPolicyPara;
		}

		// Token: 0x0200009A RID: 154
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_POLICY_STATUS
		{
			// Token: 0x06000557 RID: 1367 RVA: 0x0001BC7A File Offset: 0x00019E7A
			internal CERT_CHAIN_POLICY_STATUS(int size)
			{
				this.cbSize = (uint)size;
				this.dwError = 0U;
				this.lChainIndex = IntPtr.Zero;
				this.lElementIndex = IntPtr.Zero;
				this.pvExtraPolicyStatus = IntPtr.Zero;
			}

			// Token: 0x0400054C RID: 1356
			internal uint cbSize;

			// Token: 0x0400054D RID: 1357
			internal uint dwError;

			// Token: 0x0400054E RID: 1358
			internal IntPtr lChainIndex;

			// Token: 0x0400054F RID: 1359
			internal IntPtr lElementIndex;

			// Token: 0x04000550 RID: 1360
			internal IntPtr pvExtraPolicyStatus;
		}

		// Token: 0x0200009B RID: 155
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CONTEXT
		{
			// Token: 0x04000551 RID: 1361
			internal uint dwCertEncodingType;

			// Token: 0x04000552 RID: 1362
			internal IntPtr pbCertEncoded;

			// Token: 0x04000553 RID: 1363
			internal uint cbCertEncoded;

			// Token: 0x04000554 RID: 1364
			internal IntPtr pCertInfo;

			// Token: 0x04000555 RID: 1365
			internal IntPtr hCertStore;
		}

		// Token: 0x0200009C RID: 156
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_DSS_PARAMETERS
		{
			// Token: 0x04000556 RID: 1366
			internal CAPI.CRYPTOAPI_BLOB p;

			// Token: 0x04000557 RID: 1367
			internal CAPI.CRYPTOAPI_BLOB q;

			// Token: 0x04000558 RID: 1368
			internal CAPI.CRYPTOAPI_BLOB g;
		}

		// Token: 0x0200009D RID: 157
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_ENHKEY_USAGE
		{
			// Token: 0x04000559 RID: 1369
			internal uint cUsageIdentifier;

			// Token: 0x0400055A RID: 1370
			internal IntPtr rgpszUsageIdentifier;
		}

		// Token: 0x0200009E RID: 158
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_EXTENSION
		{
			// Token: 0x0400055B RID: 1371
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x0400055C RID: 1372
			internal bool fCritical;

			// Token: 0x0400055D RID: 1373
			internal CAPI.CRYPTOAPI_BLOB Value;
		}

		// Token: 0x0200009F RID: 159
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_ID
		{
			// Token: 0x0400055E RID: 1374
			internal uint dwIdChoice;

			// Token: 0x0400055F RID: 1375
			internal CAPI.CERT_ID_UNION Value;
		}

		// Token: 0x020000A0 RID: 160
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		internal struct CERT_ID_UNION
		{
			// Token: 0x04000560 RID: 1376
			[FieldOffset(0)]
			internal CAPI.CERT_ISSUER_SERIAL_NUMBER IssuerSerialNumber;

			// Token: 0x04000561 RID: 1377
			[FieldOffset(0)]
			internal CAPI.CRYPTOAPI_BLOB KeyId;

			// Token: 0x04000562 RID: 1378
			[FieldOffset(0)]
			internal CAPI.CRYPTOAPI_BLOB HashId;
		}

		// Token: 0x020000A1 RID: 161
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_ISSUER_SERIAL_NUMBER
		{
			// Token: 0x04000563 RID: 1379
			internal CAPI.CRYPTOAPI_BLOB Issuer;

			// Token: 0x04000564 RID: 1380
			internal CAPI.CRYPTOAPI_BLOB SerialNumber;
		}

		// Token: 0x020000A2 RID: 162
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_INFO
		{
			// Token: 0x04000565 RID: 1381
			internal uint dwVersion;

			// Token: 0x04000566 RID: 1382
			internal CAPI.CRYPTOAPI_BLOB SerialNumber;

			// Token: 0x04000567 RID: 1383
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;

			// Token: 0x04000568 RID: 1384
			internal CAPI.CRYPTOAPI_BLOB Issuer;

			// Token: 0x04000569 RID: 1385
			internal System.Runtime.InteropServices.ComTypes.FILETIME NotBefore;

			// Token: 0x0400056A RID: 1386
			internal System.Runtime.InteropServices.ComTypes.FILETIME NotAfter;

			// Token: 0x0400056B RID: 1387
			internal CAPI.CRYPTOAPI_BLOB Subject;

			// Token: 0x0400056C RID: 1388
			internal CAPI.CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;

			// Token: 0x0400056D RID: 1389
			internal CAPI.CRYPT_BIT_BLOB IssuerUniqueId;

			// Token: 0x0400056E RID: 1390
			internal CAPI.CRYPT_BIT_BLOB SubjectUniqueId;

			// Token: 0x0400056F RID: 1391
			internal uint cExtension;

			// Token: 0x04000570 RID: 1392
			internal IntPtr rgExtension;
		}

		// Token: 0x020000A3 RID: 163
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_KEY_USAGE_RESTRICTION_INFO
		{
			// Token: 0x04000571 RID: 1393
			internal uint cCertPolicyId;

			// Token: 0x04000572 RID: 1394
			internal IntPtr rgCertPolicyId;

			// Token: 0x04000573 RID: 1395
			internal CAPI.CRYPT_BIT_BLOB RestrictedKeyUsage;
		}

		// Token: 0x020000A4 RID: 164
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_NAME_INFO
		{
			// Token: 0x04000574 RID: 1396
			internal uint cRDN;

			// Token: 0x04000575 RID: 1397
			internal IntPtr rgRDN;
		}

		// Token: 0x020000A5 RID: 165
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_NAME_VALUE
		{
			// Token: 0x04000576 RID: 1398
			internal uint dwValueType;

			// Token: 0x04000577 RID: 1399
			internal CAPI.CRYPTOAPI_BLOB Value;
		}

		// Token: 0x020000A6 RID: 166
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_OTHER_NAME
		{
			// Token: 0x04000578 RID: 1400
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x04000579 RID: 1401
			internal CAPI.CRYPTOAPI_BLOB Value;
		}

		// Token: 0x020000A7 RID: 167
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_POLICY_ID
		{
			// Token: 0x0400057A RID: 1402
			internal uint cCertPolicyElementId;

			// Token: 0x0400057B RID: 1403
			internal IntPtr rgpszCertPolicyElementId;
		}

		// Token: 0x020000A8 RID: 168
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_POLICIES_INFO
		{
			// Token: 0x0400057C RID: 1404
			internal uint cPolicyInfo;

			// Token: 0x0400057D RID: 1405
			internal IntPtr rgPolicyInfo;
		}

		// Token: 0x020000A9 RID: 169
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_POLICY_INFO
		{
			// Token: 0x0400057E RID: 1406
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszPolicyIdentifier;

			// Token: 0x0400057F RID: 1407
			internal uint cPolicyQualifier;

			// Token: 0x04000580 RID: 1408
			internal IntPtr rgPolicyQualifier;
		}

		// Token: 0x020000AA RID: 170
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_POLICY_QUALIFIER_INFO
		{
			// Token: 0x04000581 RID: 1409
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszPolicyQualifierId;

			// Token: 0x04000582 RID: 1410
			private CAPI.CRYPTOAPI_BLOB Qualifier;
		}

		// Token: 0x020000AB RID: 171
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_PUBLIC_KEY_INFO
		{
			// Token: 0x04000583 RID: 1411
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER Algorithm;

			// Token: 0x04000584 RID: 1412
			internal CAPI.CRYPT_BIT_BLOB PublicKey;
		}

		// Token: 0x020000AC RID: 172
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_PUBLIC_KEY_INFO2
		{
			// Token: 0x04000585 RID: 1413
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER2 Algorithm;

			// Token: 0x04000586 RID: 1414
			internal CAPI.CRYPT_BIT_BLOB PublicKey;
		}

		// Token: 0x020000AD RID: 173
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_RDN
		{
			// Token: 0x04000587 RID: 1415
			internal uint cRDNAttr;

			// Token: 0x04000588 RID: 1416
			internal IntPtr rgRDNAttr;
		}

		// Token: 0x020000AE RID: 174
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_RDN_ATTR
		{
			// Token: 0x04000589 RID: 1417
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x0400058A RID: 1418
			internal uint dwValueType;

			// Token: 0x0400058B RID: 1419
			internal CAPI.CRYPTOAPI_BLOB Value;
		}

		// Token: 0x020000AF RID: 175
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_TEMPLATE_EXT
		{
			// Token: 0x0400058C RID: 1420
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x0400058D RID: 1421
			internal uint dwMajorVersion;

			// Token: 0x0400058E RID: 1422
			private bool fMinorVersion;

			// Token: 0x0400058F RID: 1423
			private uint dwMinorVersion;
		}

		// Token: 0x020000B0 RID: 176
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_TRUST_STATUS
		{
			// Token: 0x04000590 RID: 1424
			internal uint dwErrorStatus;

			// Token: 0x04000591 RID: 1425
			internal uint dwInfoStatus;
		}

		// Token: 0x020000B1 RID: 177
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_USAGE_MATCH
		{
			// Token: 0x04000592 RID: 1426
			internal uint dwType;

			// Token: 0x04000593 RID: 1427
			internal CAPI.CERT_ENHKEY_USAGE Usage;
		}

		// Token: 0x020000B2 RID: 178
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CMS_RECIPIENT_INFO
		{
			// Token: 0x04000594 RID: 1428
			internal uint dwRecipientChoice;

			// Token: 0x04000595 RID: 1429
			internal IntPtr pRecipientInfo;
		}

		// Token: 0x020000B3 RID: 179
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CMS_SIGNER_INFO
		{
			// Token: 0x04000596 RID: 1430
			internal uint dwVersion;

			// Token: 0x04000597 RID: 1431
			internal CAPI.CERT_ID SignerId;

			// Token: 0x04000598 RID: 1432
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;

			// Token: 0x04000599 RID: 1433
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashEncryptionAlgorithm;

			// Token: 0x0400059A RID: 1434
			internal CAPI.CRYPTOAPI_BLOB EncryptedHash;

			// Token: 0x0400059B RID: 1435
			internal CAPI.CRYPT_ATTRIBUTES AuthAttrs;

			// Token: 0x0400059C RID: 1436
			internal CAPI.CRYPT_ATTRIBUTES UnauthAttrs;
		}

		// Token: 0x020000B4 RID: 180
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA
		{
			// Token: 0x06000558 RID: 1368 RVA: 0x0001BCAB File Offset: 0x00019EAB
			internal CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.dwSignerIndex = 0U;
				this.blob = default(CAPI.CRYPTOAPI_BLOB);
			}

			// Token: 0x0400059D RID: 1437
			internal uint cbSize;

			// Token: 0x0400059E RID: 1438
			internal uint dwSignerIndex;

			// Token: 0x0400059F RID: 1439
			internal CAPI.CRYPTOAPI_BLOB blob;
		}

		// Token: 0x020000B5 RID: 181
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_DECRYPT_PARA
		{
			// Token: 0x06000559 RID: 1369 RVA: 0x0001BCC7 File Offset: 0x00019EC7
			internal CMSG_CTRL_DECRYPT_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.hCryptProv = IntPtr.Zero;
				this.dwKeySpec = 0U;
				this.dwRecipientIndex = 0U;
			}

			// Token: 0x040005A0 RID: 1440
			internal uint cbSize;

			// Token: 0x040005A1 RID: 1441
			internal IntPtr hCryptProv;

			// Token: 0x040005A2 RID: 1442
			internal uint dwKeySpec;

			// Token: 0x040005A3 RID: 1443
			internal uint dwRecipientIndex;
		}

		// Token: 0x020000B6 RID: 182
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA
		{
			// Token: 0x0600055A RID: 1370 RVA: 0x0001BCE9 File Offset: 0x00019EE9
			internal CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.dwSignerIndex = 0U;
				this.dwUnauthAttrIndex = 0U;
			}

			// Token: 0x040005A4 RID: 1444
			internal uint cbSize;

			// Token: 0x040005A5 RID: 1445
			internal uint dwSignerIndex;

			// Token: 0x040005A6 RID: 1446
			internal uint dwUnauthAttrIndex;
		}

		// Token: 0x020000B7 RID: 183
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_KEY_TRANS_DECRYPT_PARA
		{
			// Token: 0x040005A7 RID: 1447
			internal uint cbSize;

			// Token: 0x040005A8 RID: 1448
			[SecurityCritical]
			internal SafeCryptProvHandle hCryptProv;

			// Token: 0x040005A9 RID: 1449
			internal uint dwKeySpec;

			// Token: 0x040005AA RID: 1450
			internal IntPtr pKeyTrans;

			// Token: 0x040005AB RID: 1451
			internal uint dwRecipientIndex;
		}

		// Token: 0x020000B8 RID: 184
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO
		{
			// Token: 0x040005AC RID: 1452
			internal uint cbSize;

			// Token: 0x040005AD RID: 1453
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;

			// Token: 0x040005AE RID: 1454
			internal IntPtr pvKeyEncryptionAuxInfo;

			// Token: 0x040005AF RID: 1455
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyWrapAlgorithm;

			// Token: 0x040005B0 RID: 1456
			internal IntPtr pvKeyWrapAuxInfo;

			// Token: 0x040005B1 RID: 1457
			internal IntPtr hCryptProv;

			// Token: 0x040005B2 RID: 1458
			internal uint dwKeySpec;

			// Token: 0x040005B3 RID: 1459
			internal uint dwKeyChoice;

			// Token: 0x040005B4 RID: 1460
			internal IntPtr pEphemeralAlgorithmOrSenderId;

			// Token: 0x040005B5 RID: 1461
			internal CAPI.CRYPTOAPI_BLOB UserKeyingMaterial;

			// Token: 0x040005B6 RID: 1462
			internal uint cRecipientEncryptedKeys;

			// Token: 0x040005B7 RID: 1463
			internal IntPtr rgpRecipientEncryptedKeys;
		}

		// Token: 0x020000B9 RID: 185
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO
		{
			// Token: 0x040005B8 RID: 1464
			internal uint cbSize;

			// Token: 0x040005B9 RID: 1465
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;

			// Token: 0x040005BA RID: 1466
			internal IntPtr pvKeyEncryptionAuxInfo;

			// Token: 0x040005BB RID: 1467
			internal IntPtr hCryptProv;

			// Token: 0x040005BC RID: 1468
			internal CAPI.CRYPT_BIT_BLOB RecipientPublicKey;

			// Token: 0x040005BD RID: 1469
			internal CAPI.CERT_ID RecipientId;
		}

		// Token: 0x020000BA RID: 186
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_RC2_AUX_INFO
		{
			// Token: 0x0600055B RID: 1371 RVA: 0x0001BD00 File Offset: 0x00019F00
			internal CMSG_RC2_AUX_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.dwBitLen = 0U;
			}

			// Token: 0x040005BE RID: 1470
			internal uint cbSize;

			// Token: 0x040005BF RID: 1471
			internal uint dwBitLen;
		}

		// Token: 0x020000BB RID: 187
		internal struct CMSG_RECIPIENT_ENCODE_INFO
		{
			// Token: 0x040005C0 RID: 1472
			internal uint dwRecipientChoice;

			// Token: 0x040005C1 RID: 1473
			internal IntPtr pRecipientInfo;
		}

		// Token: 0x020000BC RID: 188
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO
		{
			// Token: 0x040005C2 RID: 1474
			internal uint cbSize;

			// Token: 0x040005C3 RID: 1475
			internal CAPI.CRYPT_BIT_BLOB RecipientPublicKey;

			// Token: 0x040005C4 RID: 1476
			internal CAPI.CERT_ID RecipientId;

			// Token: 0x040005C5 RID: 1477
			internal System.Runtime.InteropServices.ComTypes.FILETIME Date;

			// Token: 0x040005C6 RID: 1478
			internal IntPtr pOtherAttr;
		}

		// Token: 0x020000BD RID: 189
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_ENVELOPED_ENCODE_INFO
		{
			// Token: 0x0600055C RID: 1372 RVA: 0x0001BD10 File Offset: 0x00019F10
			internal CMSG_ENVELOPED_ENCODE_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.hCryptProv = IntPtr.Zero;
				this.ContentEncryptionAlgorithm = default(CAPI.CRYPT_ALGORITHM_IDENTIFIER);
				this.pvEncryptionAuxInfo = IntPtr.Zero;
				this.cRecipients = 0U;
				this.rgpRecipients = IntPtr.Zero;
				this.rgCmsRecipients = IntPtr.Zero;
				this.cCertEncoded = 0U;
				this.rgCertEncoded = IntPtr.Zero;
				this.cCrlEncoded = 0U;
				this.rgCrlEncoded = IntPtr.Zero;
				this.cAttrCertEncoded = 0U;
				this.rgAttrCertEncoded = IntPtr.Zero;
				this.cUnprotectedAttr = 0U;
				this.rgUnprotectedAttr = IntPtr.Zero;
			}

			// Token: 0x040005C7 RID: 1479
			internal uint cbSize;

			// Token: 0x040005C8 RID: 1480
			internal IntPtr hCryptProv;

			// Token: 0x040005C9 RID: 1481
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER ContentEncryptionAlgorithm;

			// Token: 0x040005CA RID: 1482
			internal IntPtr pvEncryptionAuxInfo;

			// Token: 0x040005CB RID: 1483
			internal uint cRecipients;

			// Token: 0x040005CC RID: 1484
			internal IntPtr rgpRecipients;

			// Token: 0x040005CD RID: 1485
			internal IntPtr rgCmsRecipients;

			// Token: 0x040005CE RID: 1486
			internal uint cCertEncoded;

			// Token: 0x040005CF RID: 1487
			internal IntPtr rgCertEncoded;

			// Token: 0x040005D0 RID: 1488
			internal uint cCrlEncoded;

			// Token: 0x040005D1 RID: 1489
			internal IntPtr rgCrlEncoded;

			// Token: 0x040005D2 RID: 1490
			internal uint cAttrCertEncoded;

			// Token: 0x040005D3 RID: 1491
			internal IntPtr rgAttrCertEncoded;

			// Token: 0x040005D4 RID: 1492
			internal uint cUnprotectedAttr;

			// Token: 0x040005D5 RID: 1493
			internal IntPtr rgUnprotectedAttr;
		}

		// Token: 0x020000BE RID: 190
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_KEY_AGREE_DECRYPT_PARA
		{
			// Token: 0x0600055D RID: 1373 RVA: 0x0001BDAB File Offset: 0x00019FAB
			internal CMSG_CTRL_KEY_AGREE_DECRYPT_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.hCryptProv = IntPtr.Zero;
				this.dwKeySpec = 0U;
				this.pKeyAgree = IntPtr.Zero;
				this.dwRecipientIndex = 0U;
				this.dwRecipientEncryptedKeyIndex = 0U;
				this.OriginatorPublicKey = default(CAPI.CRYPT_BIT_BLOB);
			}

			// Token: 0x040005D6 RID: 1494
			internal uint cbSize;

			// Token: 0x040005D7 RID: 1495
			internal IntPtr hCryptProv;

			// Token: 0x040005D8 RID: 1496
			internal uint dwKeySpec;

			// Token: 0x040005D9 RID: 1497
			internal IntPtr pKeyAgree;

			// Token: 0x040005DA RID: 1498
			internal uint dwRecipientIndex;

			// Token: 0x040005DB RID: 1499
			internal uint dwRecipientEncryptedKeyIndex;

			// Token: 0x040005DC RID: 1500
			internal CAPI.CRYPT_BIT_BLOB OriginatorPublicKey;
		}

		// Token: 0x020000BF RID: 191
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_AGREE_RECIPIENT_INFO
		{
			// Token: 0x040005DD RID: 1501
			internal uint dwVersion;

			// Token: 0x040005DE RID: 1502
			internal uint dwOriginatorChoice;
		}

		// Token: 0x020000C0 RID: 192
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO
		{
			// Token: 0x040005DF RID: 1503
			internal uint dwVersion;

			// Token: 0x040005E0 RID: 1504
			internal uint dwOriginatorChoice;

			// Token: 0x040005E1 RID: 1505
			internal CAPI.CERT_ID OriginatorCertId;

			// Token: 0x040005E2 RID: 1506
			internal IntPtr Padding;

			// Token: 0x040005E3 RID: 1507
			internal CAPI.CRYPTOAPI_BLOB UserKeyingMaterial;

			// Token: 0x040005E4 RID: 1508
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;

			// Token: 0x040005E5 RID: 1509
			internal uint cRecipientEncryptedKeys;

			// Token: 0x040005E6 RID: 1510
			internal IntPtr rgpRecipientEncryptedKeys;
		}

		// Token: 0x020000C1 RID: 193
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO
		{
			// Token: 0x040005E7 RID: 1511
			internal uint dwVersion;

			// Token: 0x040005E8 RID: 1512
			internal uint dwOriginatorChoice;

			// Token: 0x040005E9 RID: 1513
			internal CAPI.CERT_PUBLIC_KEY_INFO OriginatorPublicKeyInfo;

			// Token: 0x040005EA RID: 1514
			internal CAPI.CRYPTOAPI_BLOB UserKeyingMaterial;

			// Token: 0x040005EB RID: 1515
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;

			// Token: 0x040005EC RID: 1516
			internal uint cRecipientEncryptedKeys;

			// Token: 0x040005ED RID: 1517
			internal IntPtr rgpRecipientEncryptedKeys;
		}

		// Token: 0x020000C2 RID: 194
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_RECIPIENT_ENCRYPTED_KEY_INFO
		{
			// Token: 0x040005EE RID: 1518
			internal CAPI.CERT_ID RecipientId;

			// Token: 0x040005EF RID: 1519
			internal CAPI.CRYPTOAPI_BLOB EncryptedKey;

			// Token: 0x040005F0 RID: 1520
			internal System.Runtime.InteropServices.ComTypes.FILETIME Date;

			// Token: 0x040005F1 RID: 1521
			internal IntPtr pOtherAttr;
		}

		// Token: 0x020000C3 RID: 195
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA
		{
			// Token: 0x0600055E RID: 1374 RVA: 0x0001BDEB File Offset: 0x00019FEB
			internal CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.hCryptProv = IntPtr.Zero;
				this.dwSignerIndex = 0U;
				this.dwSignerType = 0U;
				this.pvSigner = IntPtr.Zero;
			}

			// Token: 0x040005F2 RID: 1522
			internal uint cbSize;

			// Token: 0x040005F3 RID: 1523
			internal IntPtr hCryptProv;

			// Token: 0x040005F4 RID: 1524
			internal uint dwSignerIndex;

			// Token: 0x040005F5 RID: 1525
			internal uint dwSignerType;

			// Token: 0x040005F6 RID: 1526
			internal IntPtr pvSigner;
		}

		// Token: 0x020000C4 RID: 196
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_TRANS_RECIPIENT_INFO
		{
			// Token: 0x040005F7 RID: 1527
			internal uint dwVersion;

			// Token: 0x040005F8 RID: 1528
			internal CAPI.CERT_ID RecipientId;

			// Token: 0x040005F9 RID: 1529
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;

			// Token: 0x040005FA RID: 1530
			internal CAPI.CRYPTOAPI_BLOB EncryptedKey;
		}

		// Token: 0x020000C5 RID: 197
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_SIGNED_ENCODE_INFO
		{
			// Token: 0x0600055F RID: 1375 RVA: 0x0001BE18 File Offset: 0x0001A018
			internal CMSG_SIGNED_ENCODE_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.cSigners = 0U;
				this.rgSigners = IntPtr.Zero;
				this.cCertEncoded = 0U;
				this.rgCertEncoded = IntPtr.Zero;
				this.cCrlEncoded = 0U;
				this.rgCrlEncoded = IntPtr.Zero;
				this.cAttrCertEncoded = 0U;
				this.rgAttrCertEncoded = IntPtr.Zero;
			}

			// Token: 0x040005FB RID: 1531
			internal uint cbSize;

			// Token: 0x040005FC RID: 1532
			internal uint cSigners;

			// Token: 0x040005FD RID: 1533
			internal IntPtr rgSigners;

			// Token: 0x040005FE RID: 1534
			internal uint cCertEncoded;

			// Token: 0x040005FF RID: 1535
			internal IntPtr rgCertEncoded;

			// Token: 0x04000600 RID: 1536
			internal uint cCrlEncoded;

			// Token: 0x04000601 RID: 1537
			internal IntPtr rgCrlEncoded;

			// Token: 0x04000602 RID: 1538
			internal uint cAttrCertEncoded;

			// Token: 0x04000603 RID: 1539
			internal IntPtr rgAttrCertEncoded;
		}

		// Token: 0x020000C6 RID: 198
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_SIGNER_ENCODE_INFO
		{
			// Token: 0x06000560 RID: 1376
			[DllImport("kernel32.dll", SetLastError = true)]
			internal static extern IntPtr LocalFree(IntPtr hMem);

			// Token: 0x06000561 RID: 1377
			[DllImport("advapi32.dll", SetLastError = true)]
			internal static extern bool CryptReleaseContext([In] IntPtr hProv, [In] uint dwFlags);

			// Token: 0x06000562 RID: 1378 RVA: 0x0001BE74 File Offset: 0x0001A074
			internal CMSG_SIGNER_ENCODE_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.pCertInfo = IntPtr.Zero;
				this.hCryptProv = IntPtr.Zero;
				this.dwKeySpec = 0U;
				this.HashAlgorithm = default(CAPI.CRYPT_ALGORITHM_IDENTIFIER);
				this.pvHashAuxInfo = IntPtr.Zero;
				this.cAuthAttr = 0U;
				this.rgAuthAttr = IntPtr.Zero;
				this.cUnauthAttr = 0U;
				this.rgUnauthAttr = IntPtr.Zero;
				this.SignerId = default(CAPI.CERT_ID);
				this.HashEncryptionAlgorithm = default(CAPI.CRYPT_ALGORITHM_IDENTIFIER);
				this.pvHashEncryptionAuxInfo = IntPtr.Zero;
			}

			// Token: 0x06000563 RID: 1379 RVA: 0x0001BF04 File Offset: 0x0001A104
			[SecuritySafeCritical]
			internal void Dispose()
			{
				this.hCryptProv = IntPtr.Zero;
				if (this.SignerId.Value.KeyId.pbData != IntPtr.Zero)
				{
					CAPI.CMSG_SIGNER_ENCODE_INFO.LocalFree(this.SignerId.Value.KeyId.pbData);
				}
				if (this.rgAuthAttr != IntPtr.Zero)
				{
					CAPI.CMSG_SIGNER_ENCODE_INFO.LocalFree(this.rgAuthAttr);
				}
				if (this.rgUnauthAttr != IntPtr.Zero)
				{
					CAPI.CMSG_SIGNER_ENCODE_INFO.LocalFree(this.rgUnauthAttr);
				}
			}

			// Token: 0x04000604 RID: 1540
			internal uint cbSize;

			// Token: 0x04000605 RID: 1541
			internal IntPtr pCertInfo;

			// Token: 0x04000606 RID: 1542
			internal IntPtr hCryptProv;

			// Token: 0x04000607 RID: 1543
			internal uint dwKeySpec;

			// Token: 0x04000608 RID: 1544
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;

			// Token: 0x04000609 RID: 1545
			internal IntPtr pvHashAuxInfo;

			// Token: 0x0400060A RID: 1546
			internal uint cAuthAttr;

			// Token: 0x0400060B RID: 1547
			internal IntPtr rgAuthAttr;

			// Token: 0x0400060C RID: 1548
			internal uint cUnauthAttr;

			// Token: 0x0400060D RID: 1549
			internal IntPtr rgUnauthAttr;

			// Token: 0x0400060E RID: 1550
			internal CAPI.CERT_ID SignerId;

			// Token: 0x0400060F RID: 1551
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashEncryptionAlgorithm;

			// Token: 0x04000610 RID: 1552
			internal IntPtr pvHashEncryptionAuxInfo;
		}

		// Token: 0x020000C7 RID: 199
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_SIGNER_INFO
		{
			// Token: 0x04000611 RID: 1553
			internal uint dwVersion;

			// Token: 0x04000612 RID: 1554
			internal CAPI.CRYPTOAPI_BLOB Issuer;

			// Token: 0x04000613 RID: 1555
			internal CAPI.CRYPTOAPI_BLOB SerialNumber;

			// Token: 0x04000614 RID: 1556
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;

			// Token: 0x04000615 RID: 1557
			internal CAPI.CRYPT_ALGORITHM_IDENTIFIER HashEncryptionAlgorithm;

			// Token: 0x04000616 RID: 1558
			internal CAPI.CRYPTOAPI_BLOB EncryptedHash;

			// Token: 0x04000617 RID: 1559
			internal CAPI.CRYPT_ATTRIBUTES AuthAttrs;

			// Token: 0x04000618 RID: 1560
			internal CAPI.CRYPT_ATTRIBUTES UnauthAttrs;
		}

		// Token: 0x020000C8 RID: 200
		// (Invoke) Token: 0x06000565 RID: 1381
		internal delegate bool PFN_CMSG_STREAM_OUTPUT(IntPtr pvArg, IntPtr pbData, uint cbData, bool fFinal);

		// Token: 0x020000C9 RID: 201
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class CMSG_STREAM_INFO
		{
			// Token: 0x06000568 RID: 1384 RVA: 0x0001BF94 File Offset: 0x0001A194
			internal CMSG_STREAM_INFO(uint cbContent, CAPI.PFN_CMSG_STREAM_OUTPUT pfnStreamOutput, IntPtr pvArg)
			{
				this.cbContent = cbContent;
				this.pfnStreamOutput = pfnStreamOutput;
				this.pvArg = pvArg;
			}

			// Token: 0x04000619 RID: 1561
			internal uint cbContent;

			// Token: 0x0400061A RID: 1562
			internal CAPI.PFN_CMSG_STREAM_OUTPUT pfnStreamOutput;

			// Token: 0x0400061B RID: 1563
			internal IntPtr pvArg;
		}

		// Token: 0x020000CA RID: 202
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_ALGORITHM_IDENTIFIER
		{
			// Token: 0x0400061C RID: 1564
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x0400061D RID: 1565
			internal CAPI.CRYPTOAPI_BLOB Parameters;
		}

		// Token: 0x020000CB RID: 203
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_ALGORITHM_IDENTIFIER2
		{
			// Token: 0x0400061E RID: 1566
			internal IntPtr pszObjId;

			// Token: 0x0400061F RID: 1567
			internal CAPI.CRYPTOAPI_BLOB Parameters;
		}

		// Token: 0x020000CC RID: 204
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_ATTRIBUTE
		{
			// Token: 0x04000620 RID: 1568
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x04000621 RID: 1569
			internal uint cValue;

			// Token: 0x04000622 RID: 1570
			internal IntPtr rgValue;
		}

		// Token: 0x020000CD RID: 205
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_ATTRIBUTES
		{
			// Token: 0x04000623 RID: 1571
			internal uint cAttr;

			// Token: 0x04000624 RID: 1572
			internal IntPtr rgAttr;
		}

		// Token: 0x020000CE RID: 206
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_ATTRIBUTE_TYPE_VALUE
		{
			// Token: 0x04000625 RID: 1573
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x04000626 RID: 1574
			internal CAPI.CRYPTOAPI_BLOB Value;
		}

		// Token: 0x020000CF RID: 207
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_BIT_BLOB
		{
			// Token: 0x04000627 RID: 1575
			internal uint cbData;

			// Token: 0x04000628 RID: 1576
			internal IntPtr pbData;

			// Token: 0x04000629 RID: 1577
			internal uint cUnusedBits;
		}

		// Token: 0x020000D0 RID: 208
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_KEY_PROV_INFO
		{
			// Token: 0x0400062A RID: 1578
			internal string pwszContainerName;

			// Token: 0x0400062B RID: 1579
			internal string pwszProvName;

			// Token: 0x0400062C RID: 1580
			internal uint dwProvType;

			// Token: 0x0400062D RID: 1581
			internal uint dwFlags;

			// Token: 0x0400062E RID: 1582
			internal uint cProvParam;

			// Token: 0x0400062F RID: 1583
			internal IntPtr rgProvParam;

			// Token: 0x04000630 RID: 1584
			internal uint dwKeySpec;
		}

		// Token: 0x020000D1 RID: 209
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_OID_INFO
		{
			// Token: 0x06000569 RID: 1385 RVA: 0x0001BFB1 File Offset: 0x0001A1B1
			internal CRYPT_OID_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.pszOID = null;
				this.pwszName = null;
				this.dwGroupId = 0U;
				this.Algid = 0U;
				this.ExtraInfo = default(CAPI.CRYPTOAPI_BLOB);
			}

			// Token: 0x04000631 RID: 1585
			internal uint cbSize;

			// Token: 0x04000632 RID: 1586
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszOID;

			// Token: 0x04000633 RID: 1587
			internal string pwszName;

			// Token: 0x04000634 RID: 1588
			internal uint dwGroupId;

			// Token: 0x04000635 RID: 1589
			internal uint Algid;

			// Token: 0x04000636 RID: 1590
			internal CAPI.CRYPTOAPI_BLOB ExtraInfo;
		}

		// Token: 0x020000D2 RID: 210
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_RC2_CBC_PARAMETERS
		{
			// Token: 0x04000637 RID: 1591
			internal uint dwVersion;

			// Token: 0x04000638 RID: 1592
			internal bool fIV;

			// Token: 0x04000639 RID: 1593
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			internal byte[] rgbIV;
		}

		// Token: 0x020000D3 RID: 211
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPTOAPI_BLOB
		{
			// Token: 0x0400063A RID: 1594
			internal uint cbData;

			// Token: 0x0400063B RID: 1595
			internal IntPtr pbData;
		}

		// Token: 0x020000D4 RID: 212
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class CRYPTUI_SELECTCERTIFICATE_STRUCTW
		{
			// Token: 0x0400063C RID: 1596
			internal uint dwSize;

			// Token: 0x0400063D RID: 1597
			internal IntPtr hwndParent;

			// Token: 0x0400063E RID: 1598
			internal uint dwFlags;

			// Token: 0x0400063F RID: 1599
			internal string szTitle;

			// Token: 0x04000640 RID: 1600
			internal uint dwDontUseColumn;

			// Token: 0x04000641 RID: 1601
			internal string szDisplayString;

			// Token: 0x04000642 RID: 1602
			internal IntPtr pFilterCallback;

			// Token: 0x04000643 RID: 1603
			internal IntPtr pDisplayCallback;

			// Token: 0x04000644 RID: 1604
			internal IntPtr pvCallbackData;

			// Token: 0x04000645 RID: 1605
			internal uint cDisplayStores;

			// Token: 0x04000646 RID: 1606
			internal IntPtr rghDisplayStores;

			// Token: 0x04000647 RID: 1607
			internal uint cStores;

			// Token: 0x04000648 RID: 1608
			internal IntPtr rghStores;

			// Token: 0x04000649 RID: 1609
			internal uint cPropSheetPages;

			// Token: 0x0400064A RID: 1610
			internal IntPtr rgPropSheetPages;

			// Token: 0x0400064B RID: 1611
			internal IntPtr hSelectedCertStore;
		}

		// Token: 0x020000D5 RID: 213
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class CRYPTUI_VIEWCERTIFICATE_STRUCTW
		{
			// Token: 0x0400064C RID: 1612
			internal uint dwSize;

			// Token: 0x0400064D RID: 1613
			internal IntPtr hwndParent;

			// Token: 0x0400064E RID: 1614
			internal uint dwFlags;

			// Token: 0x0400064F RID: 1615
			internal string szTitle;

			// Token: 0x04000650 RID: 1616
			internal IntPtr pCertContext;

			// Token: 0x04000651 RID: 1617
			internal IntPtr rgszPurposes;

			// Token: 0x04000652 RID: 1618
			internal uint cPurposes;

			// Token: 0x04000653 RID: 1619
			internal IntPtr pCryptProviderData;

			// Token: 0x04000654 RID: 1620
			internal bool fpCryptProviderDataTrustedUsage;

			// Token: 0x04000655 RID: 1621
			internal uint idxSigner;

			// Token: 0x04000656 RID: 1622
			internal uint idxCert;

			// Token: 0x04000657 RID: 1623
			internal bool fCounterSigner;

			// Token: 0x04000658 RID: 1624
			internal uint idxCounterSigner;

			// Token: 0x04000659 RID: 1625
			internal uint cStores;

			// Token: 0x0400065A RID: 1626
			internal IntPtr rghStores;

			// Token: 0x0400065B RID: 1627
			internal uint cPropSheetPages;

			// Token: 0x0400065C RID: 1628
			internal IntPtr rgPropSheetPages;

			// Token: 0x0400065D RID: 1629
			internal uint nStartPage;
		}

		// Token: 0x020000D6 RID: 214
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DSSPUBKEY
		{
			// Token: 0x0400065E RID: 1630
			internal uint magic;

			// Token: 0x0400065F RID: 1631
			internal uint bitlen;
		}

		// Token: 0x020000D7 RID: 215
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct PROV_ENUMALGS_EX
		{
			// Token: 0x04000660 RID: 1632
			internal uint aiAlgid;

			// Token: 0x04000661 RID: 1633
			internal uint dwDefaultLen;

			// Token: 0x04000662 RID: 1634
			internal uint dwMinLen;

			// Token: 0x04000663 RID: 1635
			internal uint dwMaxLen;

			// Token: 0x04000664 RID: 1636
			internal uint dwProtocols;

			// Token: 0x04000665 RID: 1637
			internal uint dwNameLen;

			// Token: 0x04000666 RID: 1638
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			internal byte[] szName;

			// Token: 0x04000667 RID: 1639
			internal uint dwLongNameLen;

			// Token: 0x04000668 RID: 1640
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
			internal byte[] szLongName;
		}

		// Token: 0x020000D8 RID: 216
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct RSAPUBKEY
		{
			// Token: 0x04000669 RID: 1641
			internal uint magic;

			// Token: 0x0400066A RID: 1642
			internal uint bitlen;

			// Token: 0x0400066B RID: 1643
			internal uint pubexp;
		}

		// Token: 0x020000D9 RID: 217
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		internal static class CAPISafe
		{
			// Token: 0x0600056C RID: 1388
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern IntPtr GetProcAddress([In] SafeLibraryHandle hModule, [MarshalAs(UnmanagedType.LPStr)] [In] string lpProcName);

			// Token: 0x0600056D RID: 1389
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeLocalAllocHandle LocalAlloc([In] uint uFlags, [In] IntPtr sizetdwBytes);

			// Token: 0x0600056E RID: 1390
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "LoadLibraryA", SetLastError = true)]
			internal static extern SafeLibraryHandle LoadLibrary([MarshalAs(UnmanagedType.LPStr)] [In] string lpFileName);

			// Token: 0x0600056F RID: 1391
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeCertContextHandle CertCreateCertificateContext([In] uint dwCertEncodingType, [In] SafeLocalAllocHandle pbCertEncoded, [In] uint cbCertEncoded);

			// Token: 0x06000570 RID: 1392
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeCertContextHandle CertDuplicateCertificateContext([In] IntPtr pCertContext);

			// Token: 0x06000571 RID: 1393
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CertFreeCertificateContext([In] IntPtr pCertContext);

			// Token: 0x06000572 RID: 1394
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CertGetCertificateChain([In] IntPtr hChainEngine, [In] SafeCertContextHandle pCertContext, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME pTime, [In] SafeCertStoreHandle hAdditionalStore, [In] ref CAPI.CERT_CHAIN_PARA pChainPara, [In] uint dwFlags, [In] IntPtr pvReserved, [In] [Out] ref SafeCertChainHandle ppChainContext);

			// Token: 0x06000573 RID: 1395
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CertGetCertificateContextProperty([In] SafeCertContextHandle pCertContext, [In] uint dwPropId, [In] [Out] SafeLocalAllocHandle pvData, [In] [Out] ref uint pcbData);

			// Token: 0x06000574 RID: 1396
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CertGetCertificateContextProperty([In] SafeCertContextHandle pCertContext, [In] uint dwPropId, out IntPtr data, [In] [Out] ref uint pcbData);

			// Token: 0x06000575 RID: 1397
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern uint CertGetPublicKeyLength([In] uint dwCertEncodingType, [In] IntPtr pPublicKey);

			// Token: 0x06000576 RID: 1398
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern uint CertNameToStrW([In] uint dwCertEncodingType, [In] IntPtr pName, [In] uint dwStrType, [In] [Out] SafeLocalAllocHandle psz, [In] uint csz);

			// Token: 0x06000577 RID: 1399
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CertVerifyCertificateChainPolicy([In] IntPtr pszPolicyOID, [In] SafeCertChainHandle pChainContext, [In] ref CAPI.CERT_CHAIN_POLICY_PARA pPolicyPara, [In] [Out] ref CAPI.CERT_CHAIN_POLICY_STATUS pPolicyStatus);

			// Token: 0x06000578 RID: 1400
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptAcquireCertificatePrivateKey([In] SafeCertContextHandle pCert, [In] uint dwFlags, [In] IntPtr pvReserved, [In] [Out] ref IntPtr phCryptProv, [In] [Out] ref uint pdwKeySpec, [In] [Out] ref bool pfCallerFreeProv);

			// Token: 0x06000579 RID: 1401
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptDecodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] IntPtr pbEncoded, [In] uint cbEncoded, [In] uint dwFlags, [In] [Out] SafeLocalAllocHandle pvStructInfo, [In] [Out] IntPtr pcbStructInfo);

			// Token: 0x0600057A RID: 1402
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptDecodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] byte[] pbEncoded, [In] uint cbEncoded, [In] uint dwFlags, [In] [Out] SafeLocalAllocHandle pvStructInfo, [In] [Out] IntPtr pcbStructInfo);

			// Token: 0x0600057B RID: 1403
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptEncodeObject([In] uint dwCertEncodingType, [In] IntPtr lpszStructType, [In] IntPtr pvStructInfo, [In] [Out] SafeLocalAllocHandle pbEncoded, [In] [Out] IntPtr pcbEncoded);

			// Token: 0x0600057C RID: 1404
			[DllImport("crypt32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptEncodeObject([In] uint dwCertEncodingType, [MarshalAs(UnmanagedType.LPStr)] [In] string lpszStructType, [In] IntPtr pvStructInfo, [In] [Out] SafeLocalAllocHandle pbEncoded, [In] [Out] IntPtr pcbEncoded);

			// Token: 0x0600057D RID: 1405
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern IntPtr CryptFindOIDInfo([In] uint dwKeyType, [In] IntPtr pvKey, [In] uint dwGroupId);

			// Token: 0x0600057E RID: 1406
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern IntPtr CryptFindOIDInfo([In] uint dwKeyType, [In] SafeLocalAllocHandle pvKey, [In] uint dwGroupId);

			// Token: 0x0600057F RID: 1407
			[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptGetProvParam([In] SafeCryptProvHandle hProv, [In] uint dwParam, [In] IntPtr pbData, [In] IntPtr pdwDataLen, [In] uint dwFlags);

			// Token: 0x06000580 RID: 1408
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptMsgGetParam([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwParamType, [In] uint dwIndex, [In] [Out] IntPtr pvData, [In] [Out] IntPtr pcbData);

			// Token: 0x06000581 RID: 1409
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptMsgGetParam([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwParamType, [In] uint dwIndex, [In] [Out] SafeLocalAllocHandle pvData, [In] [Out] IntPtr pcbData);

			// Token: 0x06000582 RID: 1410
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeCryptMsgHandle CryptMsgOpenToDecode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr hCryptProv, [In] IntPtr pRecipientInfo, [In] IntPtr pStreamInfo);

			// Token: 0x06000583 RID: 1411
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptMsgUpdate([In] SafeCryptMsgHandle hCryptMsg, [In] byte[] pbData, [In] uint cbData, [In] bool fFinal);

			// Token: 0x06000584 RID: 1412
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptMsgUpdate([In] SafeCryptMsgHandle hCryptMsg, [In] IntPtr pbData, [In] uint cbData, [In] bool fFinal);

			// Token: 0x06000585 RID: 1413
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptMsgVerifyCountersignatureEncoded([In] IntPtr hCryptProv, [In] uint dwEncodingType, [In] IntPtr pbSignerInfo, [In] uint cbSignerInfo, [In] IntPtr pbSignerInfoCountersignature, [In] uint cbSignerInfoCountersignature, [In] IntPtr pciCountersigner);

			// Token: 0x06000586 RID: 1414
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", SetLastError = true)]
			internal static extern IntPtr LocalFree(IntPtr handle);

			// Token: 0x06000587 RID: 1415
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", SetLastError = true)]
			internal static extern void ZeroMemory(IntPtr handle, uint length);

			// Token: 0x06000588 RID: 1416
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("advapi32.dll", SetLastError = true)]
			internal static extern int LsaNtStatusToWinError([In] int status);
		}

		// Token: 0x020000DA RID: 218
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		internal static class CAPIUnsafe
		{
			// Token: 0x06000589 RID: 1417
			[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "CryptAcquireContextA")]
			internal static extern bool CryptAcquireContext([In] [Out] ref SafeCryptProvHandle hCryptProv, [MarshalAs(UnmanagedType.LPStr)] [In] string pszContainer, [MarshalAs(UnmanagedType.LPStr)] [In] string pszProvider, [In] uint dwProvType, [In] uint dwFlags);

			// Token: 0x0600058A RID: 1418
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CertAddCertificateContextToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext);

			// Token: 0x0600058B RID: 1419
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CertAddCertificateLinkToStore([In] SafeCertStoreHandle hCertStore, [In] SafeCertContextHandle pCertContext, [In] uint dwAddDisposition, [In] [Out] SafeCertContextHandle ppStoreContext);

			// Token: 0x0600058C RID: 1420
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern IntPtr CertEnumCertificatesInStore([In] SafeCertStoreHandle hCertStore, [In] IntPtr pPrevCertContext);

			// Token: 0x0600058D RID: 1421
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeCertContextHandle CertFindCertificateInStore([In] SafeCertStoreHandle hCertStore, [In] uint dwCertEncodingType, [In] uint dwFindFlags, [In] uint dwFindType, [In] IntPtr pvFindPara, [In] SafeCertContextHandle pPrevCertContext);

			// Token: 0x0600058E RID: 1422
			[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern SafeCertStoreHandle CertOpenStore([In] IntPtr lpszStoreProvider, [In] uint dwMsgAndCertEncodingType, [In] IntPtr hCryptProv, [In] uint dwFlags, [In] string pvPara);

			// Token: 0x0600058F RID: 1423
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeCertContextHandle CertCreateSelfSignCertificate([In] SafeCryptProvHandle hProv, [In] IntPtr pSubjectIssuerBlob, [In] uint dwFlags, [In] IntPtr pKeyProvInfo, [In] IntPtr pSignatureAlgorithm, [In] IntPtr pStartTime, [In] IntPtr pEndTime, [In] IntPtr pExtensions);

			// Token: 0x06000590 RID: 1424
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptMsgControl([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwFlags, [In] uint dwCtrlType, [In] IntPtr pvCtrlPara);

			// Token: 0x06000591 RID: 1425
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool CryptMsgCountersign([In] SafeCryptMsgHandle hCryptMsg, [In] uint dwIndex, [In] uint cCountersigners, [In] IntPtr rgCountersigners);

			// Token: 0x06000592 RID: 1426
			[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [In] IntPtr pszInnerContentObjID, [In] IntPtr pStreamInfo);

			// Token: 0x06000593 RID: 1427
			[DllImport("crypt32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeCryptMsgHandle CryptMsgOpenToEncode([In] uint dwMsgEncodingType, [In] uint dwFlags, [In] uint dwMsgType, [In] IntPtr pvMsgEncodeInfo, [MarshalAs(UnmanagedType.LPStr)] [In] string pszInnerContentObjID, [In] IntPtr pStreamInfo);

			// Token: 0x06000594 RID: 1428
			[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool CryptProtectData([In] IntPtr pDataIn, [In] string szDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In] [Out] IntPtr pDataBlob);

			// Token: 0x06000595 RID: 1429
			[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool CryptUnprotectData([In] IntPtr pDataIn, [In] IntPtr ppszDataDescr, [In] IntPtr pOptionalEntropy, [In] IntPtr pvReserved, [In] IntPtr pPromptStruct, [In] uint dwFlags, [In] [Out] IntPtr pDataBlob);

			// Token: 0x06000596 RID: 1430
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern int SystemFunction040([In] [Out] byte[] pDataIn, [In] uint cbDataIn, [In] uint dwFlags);

			// Token: 0x06000597 RID: 1431
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern int SystemFunction041([In] [Out] byte[] pDataIn, [In] uint cbDataIn, [In] uint dwFlags);

			// Token: 0x06000598 RID: 1432
			[DllImport("cryptui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern SafeCertContextHandle CryptUIDlgSelectCertificateW([MarshalAs(UnmanagedType.LPStruct)] [In] [Out] CAPI.CRYPTUI_SELECTCERTIFICATE_STRUCTW csc);

			// Token: 0x06000599 RID: 1433
			[DllImport("cryptui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool CryptUIDlgViewCertificateW([MarshalAs(UnmanagedType.LPStruct)] [In] CAPI.CRYPTUI_VIEWCERTIFICATE_STRUCTW ViewInfo, [In] [Out] IntPtr pfPropertiesChanged);
		}
	}
}
