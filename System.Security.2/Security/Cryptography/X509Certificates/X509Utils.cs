using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000066 RID: 102
	internal class X509Utils
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x000044A9 File Offset: 0x000026A9
		private X509Utils()
		{
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001382C File Offset: 0x00011A2C
		internal static uint MapRevocationFlags(X509RevocationMode revocationMode, X509RevocationFlag revocationFlag)
		{
			uint num = 0U;
			if (revocationMode == X509RevocationMode.NoCheck)
			{
				return num;
			}
			if (revocationMode == X509RevocationMode.Offline)
			{
				num |= 2147483648U;
			}
			if (revocationFlag == X509RevocationFlag.EndCertificateOnly)
			{
				num |= 268435456U;
			}
			else if (revocationFlag == X509RevocationFlag.EntireChain)
			{
				num |= 536870912U;
			}
			else
			{
				num |= 1073741824U;
			}
			return num;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00013870 File Offset: 0x00011A70
		internal static string EncodeHexString(byte[] sArray)
		{
			return X509Utils.EncodeHexString(sArray, 0U, (uint)sArray.Length);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001387C File Offset: 0x00011A7C
		internal static string EncodeHexString(byte[] sArray, uint start, uint end)
		{
			string result = null;
			if (sArray != null)
			{
				char[] array = new char[(end - start) * 2U];
				uint num = start;
				uint num2 = 0U;
				while (num < end)
				{
					uint num3 = (uint)((sArray[(int)num] & 240) >> 4);
					array[(int)num2++] = X509Utils.hexValues[(int)num3];
					num3 = (uint)(sArray[(int)num] & 15);
					array[(int)num2++] = X509Utils.hexValues[(int)num3];
					num += 1U;
				}
				result = new string(array);
			}
			return result;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000138E3 File Offset: 0x00011AE3
		internal static string EncodeHexStringFromInt(byte[] sArray)
		{
			return X509Utils.EncodeHexStringFromInt(sArray, 0U, (uint)sArray.Length);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000138F0 File Offset: 0x00011AF0
		internal static string EncodeHexStringFromInt(byte[] sArray, uint start, uint end)
		{
			string result = null;
			if (sArray != null)
			{
				char[] array = new char[(end - start) * 2U];
				uint num = end;
				uint num2 = 0U;
				while (num-- > start)
				{
					uint num3 = (uint)(sArray[(int)num] & 240) >> 4;
					array[(int)num2++] = X509Utils.hexValues[(int)num3];
					num3 = (uint)(sArray[(int)num] & 15);
					array[(int)num2++] = X509Utils.hexValues[(int)num3];
				}
				result = new string(array);
			}
			return result;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00013957 File Offset: 0x00011B57
		internal static byte HexToByte(char val)
		{
			if (val <= '9' && val >= '0')
			{
				return (byte)(val - '0');
			}
			if (val >= 'a' && val <= 'f')
			{
				return (byte)(val - 'a' + '\n');
			}
			if (val >= 'A' && val <= 'F')
			{
				return (byte)(val - 'A' + '\n');
			}
			return byte.MaxValue;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00013994 File Offset: 0x00011B94
		internal static byte[] DecodeHexString(string s)
		{
			string text = Utils.DiscardWhiteSpaces(s);
			uint num = (uint)(text.Length / 2);
			byte[] array = new byte[num];
			int num2 = 0;
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num))
			{
				array[num3] = (byte)((int)X509Utils.HexToByte(text[num2]) << 4 | (int)X509Utils.HexToByte(text[num2 + 1]));
				num2 += 2;
				num3++;
			}
			return array;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000139F3 File Offset: 0x00011BF3
		[SecurityCritical]
		internal unsafe static bool MemEqual(byte* pbBuf1, uint cbBuf1, byte* pbBuf2, uint cbBuf2)
		{
			if (cbBuf1 != cbBuf2)
			{
				return false;
			}
			while (cbBuf1-- > 0U)
			{
				if (*(pbBuf1++) != *(pbBuf2++))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00013A18 File Offset: 0x00011C18
		[SecurityCritical]
		internal static SafeLocalAllocHandle StringToAnsiPtr(string s)
		{
			byte[] array = new byte[s.Length + 1];
			Encoding.ASCII.GetBytes(s, 0, s.Length, array, 0);
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(array.Length));
			Marshal.Copy(array, 0, safeLocalAllocHandle.DangerousGetHandle(), array.Length);
			return safeLocalAllocHandle;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00013A68 File Offset: 0x00011C68
		[SecurityCritical]
		internal static SafeCertContextHandle GetCertContext(X509Certificate2 certificate)
		{
			SafeCertContextHandle result = CAPI.CertDuplicateCertificateContext(certificate.Handle);
			GC.KeepAlive(certificate);
			return result;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013A88 File Offset: 0x00011C88
		[SecurityCritical]
		internal static bool GetPrivateKeyInfo(SafeCertContextHandle safeCertContext, ref CspParameters parameters)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			uint num = 0U;
			if (!CAPI.CAPISafe.CertGetCertificateContextProperty(safeCertContext, 2U, safeLocalAllocHandle, ref num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == -2146885628)
				{
					return false;
				}
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			else
			{
				safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
				if (CAPI.CAPISafe.CertGetCertificateContextProperty(safeCertContext, 2U, safeLocalAllocHandle, ref num))
				{
					CAPI.CRYPT_KEY_PROV_INFO crypt_KEY_PROV_INFO = (CAPI.CRYPT_KEY_PROV_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CRYPT_KEY_PROV_INFO));
					parameters.ProviderName = crypt_KEY_PROV_INFO.pwszProvName;
					parameters.KeyContainerName = crypt_KEY_PROV_INFO.pwszContainerName;
					parameters.ProviderType = (int)crypt_KEY_PROV_INFO.dwProvType;
					parameters.KeyNumber = (int)crypt_KEY_PROV_INFO.dwKeySpec;
					parameters.Flags = (((crypt_KEY_PROV_INFO.dwFlags & 32U) == 32U) ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
					safeLocalAllocHandle.Dispose();
					return true;
				}
				int lastWin32Error2 = Marshal.GetLastWin32Error();
				if (lastWin32Error2 == -2146885628)
				{
					return false;
				}
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00013B6C File Offset: 0x00011D6C
		[SecurityCritical]
		internal static SafeCertStoreHandle ExportToMemoryStore(X509Certificate2Collection collection, X509Certificate2Collection collection2 = null)
		{
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AllFlags);
			storePermission.Assert();
			SafeCertStoreHandle safeCertStoreHandle = CAPI.CertOpenStore(new IntPtr(2L), 65537U, IntPtr.Zero, 8704U, null);
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			X509Utils.AddToStore(safeCertStoreHandle, collection);
			if (collection2 != null)
			{
				X509Utils.AddToStore(safeCertStoreHandle, collection2);
			}
			return safeCertStoreHandle;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013BD0 File Offset: 0x00011DD0
		[SecurityCritical]
		private static void AddToStore(SafeCertStoreHandle safeCertStoreHandle, X509Certificate2Collection collection)
		{
			foreach (X509Certificate2 certificate in collection)
			{
				using (SafeCertContextHandle certContext = X509Utils.GetCertContext(certificate))
				{
					if (!CAPI.CertAddCertificateLinkToStore(safeCertStoreHandle, certContext, 4U, SafeCertContextHandle.InvalidHandle))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
				}
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00013C34 File Offset: 0x00011E34
		[SecuritySafeCritical]
		internal static uint OidToAlgId(string value)
		{
			SafeLocalAllocHandle pvKey = X509Utils.StringToAnsiPtr(value);
			CAPI.CRYPT_OID_INFO crypt_OID_INFO = CAPI.CryptFindOIDInfo(1U, pvKey, 0U);
			return crypt_OID_INFO.Algid;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00013C58 File Offset: 0x00011E58
		internal static bool IsSelfSigned(X509Chain chain)
		{
			X509ChainElementCollection chainElements = chain.ChainElements;
			if (chainElements.Count != 1)
			{
				return false;
			}
			X509Certificate2 certificate = chainElements[0].Certificate;
			return string.Compare(certificate.SubjectName.Name, certificate.IssuerName.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013CA8 File Offset: 0x00011EA8
		[SecurityCritical]
		internal static SafeLocalAllocHandle CopyOidsToUnmanagedMemory(OidCollection oids)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (oids == null || oids.Count == 0)
			{
				return safeLocalAllocHandle;
			}
			int num = oids.Count * Marshal.SizeOf(typeof(IntPtr));
			int num2 = 0;
			foreach (Oid oid in oids)
			{
				num2 += oid.Value.Length + 1;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)(num + num2))));
			IntPtr intPtr = new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + (long)num);
			for (int i = 0; i < oids.Count; i++)
			{
				Marshal.WriteIntPtr(new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + (long)(i * Marshal.SizeOf(typeof(IntPtr)))), intPtr);
				byte[] bytes = Encoding.ASCII.GetBytes(oids[i].Value);
				Marshal.Copy(bytes, 0, intPtr, bytes.Length);
				intPtr = new IntPtr((long)intPtr + (long)oids[i].Value.Length + 1L);
			}
			return safeLocalAllocHandle;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013DC4 File Offset: 0x00011FC4
		[SecurityCritical]
		internal static X509Certificate2Collection GetCertificates(SafeCertStoreHandle safeCertStoreHandle)
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			IntPtr intPtr = CAPI.CertEnumCertificatesInStore(safeCertStoreHandle, IntPtr.Zero);
			while (intPtr != IntPtr.Zero)
			{
				X509Certificate2 certificate = new X509Certificate2(intPtr);
				x509Certificate2Collection.Add(certificate);
				intPtr = CAPI.CertEnumCertificatesInStore(safeCertStoreHandle, intPtr);
			}
			return x509Certificate2Collection;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00013E0C File Offset: 0x0001200C
		[SecurityCritical]
		internal unsafe static int BuildChain(IntPtr hChainEngine, SafeCertContextHandle pCertContext, X509Certificate2Collection extraStore, OidCollection applicationPolicy, OidCollection certificatePolicy, X509RevocationMode revocationMode, X509RevocationFlag revocationFlag, DateTime verificationTime, TimeSpan timeout, ref SafeCertChainHandle ppChainContext)
		{
			if (pCertContext == null || pCertContext.IsInvalid)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_InvalidContextHandle"), "pCertContext");
			}
			SafeCertStoreHandle hAdditionalStore = SafeCertStoreHandle.InvalidHandle;
			if (extraStore != null && extraStore.Count > 0)
			{
				hAdditionalStore = X509Utils.ExportToMemoryStore(extraStore, null);
			}
			CAPI.CERT_CHAIN_PARA cert_CHAIN_PARA = default(CAPI.CERT_CHAIN_PARA);
			cert_CHAIN_PARA.cbSize = (uint)Marshal.SizeOf(cert_CHAIN_PARA);
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (applicationPolicy != null && applicationPolicy.Count > 0)
			{
				cert_CHAIN_PARA.RequestedUsage.dwType = 0U;
				cert_CHAIN_PARA.RequestedUsage.Usage.cUsageIdentifier = (uint)applicationPolicy.Count;
				safeLocalAllocHandle = X509Utils.CopyOidsToUnmanagedMemory(applicationPolicy);
				cert_CHAIN_PARA.RequestedUsage.Usage.rgpszUsageIdentifier = safeLocalAllocHandle.DangerousGetHandle();
			}
			SafeLocalAllocHandle safeLocalAllocHandle2 = SafeLocalAllocHandle.InvalidHandle;
			if (certificatePolicy != null && certificatePolicy.Count > 0)
			{
				cert_CHAIN_PARA.RequestedIssuancePolicy.dwType = 0U;
				cert_CHAIN_PARA.RequestedIssuancePolicy.Usage.cUsageIdentifier = (uint)certificatePolicy.Count;
				safeLocalAllocHandle2 = X509Utils.CopyOidsToUnmanagedMemory(certificatePolicy);
				cert_CHAIN_PARA.RequestedIssuancePolicy.Usage.rgpszUsageIdentifier = safeLocalAllocHandle2.DangerousGetHandle();
			}
			cert_CHAIN_PARA.dwUrlRetrievalTimeout = (uint)timeout.Milliseconds;
			System.Runtime.InteropServices.ComTypes.FILETIME filetime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
			*(long*)(&filetime) = verificationTime.ToFileTime();
			uint dwFlags = X509Utils.MapRevocationFlags(revocationMode, revocationFlag);
			if (!CAPI.CAPISafe.CertGetCertificateChain(hChainEngine, pCertContext, ref filetime, hAdditionalStore, ref cert_CHAIN_PARA, dwFlags, IntPtr.Zero, ref ppChainContext))
			{
				return Marshal.GetHRForLastWin32Error();
			}
			safeLocalAllocHandle.Dispose();
			safeLocalAllocHandle2.Dispose();
			return 0;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00013F70 File Offset: 0x00012170
		[SecurityCritical]
		internal unsafe static int VerifyCertificate(SafeCertContextHandle pCertContext, OidCollection applicationPolicy, OidCollection certificatePolicy, X509RevocationMode revocationMode, X509RevocationFlag revocationFlag, DateTime verificationTime, TimeSpan timeout, X509Certificate2Collection extraStore, IntPtr pszPolicy, IntPtr pdwErrorStatus)
		{
			if (pCertContext == null || pCertContext.IsInvalid)
			{
				throw new ArgumentException("pCertContext");
			}
			CAPI.CERT_CHAIN_POLICY_PARA cert_CHAIN_POLICY_PARA = new CAPI.CERT_CHAIN_POLICY_PARA(Marshal.SizeOf(typeof(CAPI.CERT_CHAIN_POLICY_PARA)));
			CAPI.CERT_CHAIN_POLICY_STATUS cert_CHAIN_POLICY_STATUS = new CAPI.CERT_CHAIN_POLICY_STATUS(Marshal.SizeOf(typeof(CAPI.CERT_CHAIN_POLICY_STATUS)));
			SafeCertChainHandle invalidHandle = SafeCertChainHandle.InvalidHandle;
			int num = X509Utils.BuildChain(new IntPtr(0L), pCertContext, extraStore, applicationPolicy, certificatePolicy, revocationMode, revocationFlag, verificationTime, timeout, ref invalidHandle);
			if (num != 0)
			{
				return num;
			}
			if (!CAPI.CAPISafe.CertVerifyCertificateChainPolicy(pszPolicy, invalidHandle, ref cert_CHAIN_POLICY_PARA, ref cert_CHAIN_POLICY_STATUS))
			{
				return Marshal.GetHRForLastWin32Error();
			}
			if (pdwErrorStatus != IntPtr.Zero)
			{
				*(int*)((void*)pdwErrorStatus) = (int)cert_CHAIN_POLICY_STATUS.dwError;
			}
			if (cert_CHAIN_POLICY_STATUS.dwError != 0U)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x040004AF RID: 1199
		private static readonly char[] hexValues = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};
	}
}
