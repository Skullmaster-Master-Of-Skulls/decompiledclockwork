using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000482 RID: 1154
	internal class X509Utils
	{
		// Token: 0x06002AB4 RID: 10932 RVA: 0x000C29C2 File Offset: 0x000C0BC2
		private X509Utils()
		{
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000C29CA File Offset: 0x000C0BCA
		internal static bool IsCertRdnCharString(uint dwValueType)
		{
			return (dwValueType & 255U) >= 3U;
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000C29DC File Offset: 0x000C0BDC
		internal static X509ContentType MapContentType(uint contentType)
		{
			switch (contentType)
			{
			case 1U:
				return X509ContentType.Cert;
			case 4U:
				return X509ContentType.SerializedStore;
			case 5U:
				return X509ContentType.SerializedCert;
			case 8U:
			case 9U:
				return X509ContentType.Pkcs7;
			case 10U:
				return X509ContentType.Authenticode;
			case 12U:
				return X509ContentType.Pfx;
			}
			return X509ContentType.Unknown;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000C2A30 File Offset: 0x000C0C30
		internal static uint MapKeyStorageFlags(X509KeyStorageFlags keyStorageFlags)
		{
			if (LocalAppContextSwitches.DoNotValidateX509KeyStorageFlags)
			{
				keyStorageFlags &= (X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet);
			}
			if ((keyStorageFlags & (X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet)) != keyStorageFlags)
			{
				throw new ArgumentException(SR.GetString("Arg_EnumIllegalVal", new object[]
				{
					(int)keyStorageFlags
				}), "keyStorageFlags");
			}
			X509KeyStorageFlags x509KeyStorageFlags = keyStorageFlags & (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet);
			if (x509KeyStorageFlags == (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet))
			{
				throw new ArgumentException(SR.GetString("Cryptography_X509_InvalidFlagCombination", new object[]
				{
					x509KeyStorageFlags
				}), "keyStorageFlags");
			}
			uint num = 0U;
			if ((keyStorageFlags & X509KeyStorageFlags.UserKeySet) == X509KeyStorageFlags.UserKeySet)
			{
				num |= 4096U;
			}
			else if ((keyStorageFlags & X509KeyStorageFlags.MachineKeySet) == X509KeyStorageFlags.MachineKeySet)
			{
				num |= 32U;
			}
			if ((keyStorageFlags & X509KeyStorageFlags.Exportable) == X509KeyStorageFlags.Exportable)
			{
				num |= 1U;
			}
			if ((keyStorageFlags & X509KeyStorageFlags.UserProtected) == X509KeyStorageFlags.UserProtected)
			{
				num |= 2U;
			}
			if ((keyStorageFlags & X509KeyStorageFlags.EphemeralKeySet) == X509KeyStorageFlags.EphemeralKeySet)
			{
				num |= 33280U;
			}
			return num;
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000C2AE8 File Offset: 0x000C0CE8
		internal static uint MapX509StoreFlags(StoreLocation storeLocation, OpenFlags flags)
		{
			uint num = 0U;
			uint num2 = (uint)(flags & (OpenFlags.ReadWrite | OpenFlags.MaxAllowed));
			if (num2 != 0U)
			{
				if (num2 == 2U)
				{
					num |= 4096U;
				}
			}
			else
			{
				num |= 32768U;
			}
			if ((flags & OpenFlags.OpenExistingOnly) == OpenFlags.OpenExistingOnly)
			{
				num |= 16384U;
			}
			if ((flags & OpenFlags.IncludeArchived) == OpenFlags.IncludeArchived)
			{
				num |= 512U;
			}
			if (storeLocation == StoreLocation.LocalMachine)
			{
				num |= 131072U;
			}
			else if (storeLocation == StoreLocation.CurrentUser)
			{
				num |= 65536U;
			}
			return num;
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000C2B50 File Offset: 0x000C0D50
		internal static uint MapNameType(X509NameType nameType)
		{
			uint result;
			switch (nameType)
			{
			case X509NameType.SimpleName:
				result = 4U;
				break;
			case X509NameType.EmailName:
				result = 1U;
				break;
			case X509NameType.UpnName:
				result = 8U;
				break;
			case X509NameType.DnsName:
			case X509NameType.DnsFromAlternativeName:
				result = 6U;
				break;
			case X509NameType.UrlName:
				result = 7U;
				break;
			default:
				throw new ArgumentException(SR.GetString("Argument_InvalidNameType"));
			}
			return result;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000C2BA4 File Offset: 0x000C0DA4
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

		// Token: 0x06002ABB RID: 10939 RVA: 0x000C2BE8 File Offset: 0x000C0DE8
		internal static string EncodeHexString(byte[] sArray)
		{
			return X509Utils.EncodeHexString(sArray, 0U, (uint)sArray.Length);
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000C2BF4 File Offset: 0x000C0DF4
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

		// Token: 0x06002ABD RID: 10941 RVA: 0x000C2C5C File Offset: 0x000C0E5C
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

		// Token: 0x06002ABE RID: 10942 RVA: 0x000C2CC3 File Offset: 0x000C0EC3
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

		// Token: 0x06002ABF RID: 10943 RVA: 0x000C2D00 File Offset: 0x000C0F00
		internal static uint AlignedLength(uint length)
		{
			return length + 7U & 4294967288U;
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000C2D08 File Offset: 0x000C0F08
		internal static string DiscardWhiteSpaces(string inputBuffer)
		{
			return X509Utils.DiscardWhiteSpaces(inputBuffer, 0, inputBuffer.Length);
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000C2D18 File Offset: 0x000C0F18
		internal static string DiscardWhiteSpaces(string inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			char[] array = new char[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return new string(array);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000C2D84 File Offset: 0x000C0F84
		internal static byte[] DecodeHexString(string s)
		{
			string text = X509Utils.DiscardWhiteSpaces(s);
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

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000C2DE4 File Offset: 0x000C0FE4
		internal static int GetHexArraySize(byte[] hex)
		{
			int num = hex.Length;
			while (num-- > 0 && hex[num] == 0)
			{
			}
			return num + 1;
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000C2E08 File Offset: 0x000C1008
		internal static SafeLocalAllocHandle ByteToPtr(byte[] managed)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(managed.Length));
			Marshal.Copy(managed, 0, safeLocalAllocHandle.DangerousGetHandle(), managed.Length);
			return safeLocalAllocHandle;
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000C2E38 File Offset: 0x000C1038
		internal unsafe static void memcpy(IntPtr source, IntPtr dest, uint size)
		{
			for (uint num = 0U; num < size; num += 1U)
			{
				*(UIntPtr)((long)dest + (long)((ulong)num)) = Marshal.ReadByte(new IntPtr((long)source + (long)((ulong)num)));
			}
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000C2E70 File Offset: 0x000C1070
		internal static byte[] PtrToByte(IntPtr unmanaged, uint size)
		{
			byte[] array = new byte[size];
			Marshal.Copy(unmanaged, array, 0, array.Length);
			return array;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000C2E90 File Offset: 0x000C1090
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

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000C2EB4 File Offset: 0x000C10B4
		internal static SafeLocalAllocHandle StringToAnsiPtr(string s)
		{
			byte[] array = new byte[s.Length + 1];
			Encoding.ASCII.GetBytes(s, 0, s.Length, array, 0);
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(array.Length));
			Marshal.Copy(array, 0, safeLocalAllocHandle.DangerousGetHandle(), array.Length);
			return safeLocalAllocHandle;
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000C2F04 File Offset: 0x000C1104
		internal static SafeLocalAllocHandle StringToUniPtr(string s)
		{
			byte[] array = new byte[2 * (s.Length + 1)];
			Encoding.Unicode.GetBytes(s, 0, s.Length, array, 0);
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(array.Length));
			Marshal.Copy(array, 0, safeLocalAllocHandle.DangerousGetHandle(), array.Length);
			return safeLocalAllocHandle;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x000C2F58 File Offset: 0x000C1158
		internal static SafeCertStoreHandle ExportToMemoryStore(X509Certificate2Collection collection)
		{
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AllFlags);
			storePermission.Assert();
			SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			safeCertStoreHandle = CAPI.CertOpenStore(new IntPtr(2L), 65537U, IntPtr.Zero, 8704U, null);
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			foreach (X509Certificate2 x509Certificate in collection)
			{
				if (!CAPI.CertAddCertificateLinkToStore(safeCertStoreHandle, x509Certificate.CertContext, 4U, SafeCertContextHandle.InvalidHandle))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			return safeCertStoreHandle;
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000C2FE8 File Offset: 0x000C11E8
		internal static uint OidToAlgId(string value)
		{
			SafeLocalAllocHandle pvKey = X509Utils.StringToAnsiPtr(value);
			CAPIBase.CRYPT_OID_INFO crypt_OID_INFO = CAPI.CryptFindOIDInfo(1U, pvKey, OidGroup.All);
			return crypt_OID_INFO.Algid;
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000C300C File Offset: 0x000C120C
		internal static string FindOidInfo(uint keyType, string keyValue, OidGroup oidGroup)
		{
			if (keyValue == null)
			{
				throw new ArgumentNullException("keyValue");
			}
			if (keyValue.Length == 0)
			{
				return null;
			}
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			string result;
			try
			{
				if (keyType != 1U)
				{
					if (keyType == 2U)
					{
						safeLocalAllocHandle = X509Utils.StringToUniPtr(keyValue);
					}
				}
				else
				{
					safeLocalAllocHandle = X509Utils.StringToAnsiPtr(keyValue);
				}
				CAPIBase.CRYPT_OID_INFO crypt_OID_INFO = CAPI.CryptFindOIDInfo(keyType, safeLocalAllocHandle, oidGroup);
				if (keyType == 1U)
				{
					result = crypt_OID_INFO.pwszName;
				}
				else
				{
					result = crypt_OID_INFO.pszOID;
				}
			}
			finally
			{
				safeLocalAllocHandle.Dispose();
			}
			return result;
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000C3088 File Offset: 0x000C1288
		internal static string FindOidInfoWithFallback(uint key, string value, OidGroup group)
		{
			string text = X509Utils.FindOidInfo(key, value, group);
			if (text == null && group != OidGroup.All)
			{
				text = X509Utils.FindOidInfo(key, value, OidGroup.All);
			}
			return text;
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000C30B0 File Offset: 0x000C12B0
		internal static void ValidateOidValue(string keyValue)
		{
			if (keyValue == null)
			{
				throw new ArgumentNullException("keyValue");
			}
			int length = keyValue.Length;
			if (length >= 2)
			{
				char c = keyValue[0];
				if ((c == '0' || c == '1' || c == '2') && keyValue[1] == '.' && keyValue[length - 1] != '.')
				{
					bool flag = false;
					for (int i = 1; i < length; i++)
					{
						if (!char.IsDigit(keyValue[i]))
						{
							if (keyValue[i] != '.' || keyValue[i + 1] == '.')
							{
								goto IL_82;
							}
							flag = true;
						}
					}
					if (flag)
					{
						return;
					}
				}
			}
			IL_82:
			throw new ArgumentException(SR.GetString("Argument_InvalidOidValue"));
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000C3150 File Offset: 0x000C1350
		internal static SafeLocalAllocHandle CopyOidsToUnmanagedMemory(OidCollection oids)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (oids == null || oids.Count == 0)
			{
				return safeLocalAllocHandle;
			}
			List<string> list = new List<string>();
			foreach (Oid oid in oids)
			{
				list.Add(oid.Value);
			}
			IntPtr zero = IntPtr.Zero;
			checked
			{
				int num = list.Count * Marshal.SizeOf(typeof(IntPtr));
				int num2 = 0;
				foreach (string text in list)
				{
					num2 += text.Length + 1;
				}
				safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)(unchecked((ulong)(checked((uint)num + (uint)num2))))));
				zero = new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + unchecked((long)num));
			}
			for (int i = 0; i < list.Count; i++)
			{
				Marshal.WriteIntPtr(new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + (long)(i * Marshal.SizeOf(typeof(IntPtr)))), zero);
				byte[] bytes = Encoding.ASCII.GetBytes(list[i]);
				Marshal.Copy(bytes, 0, zero, bytes.Length);
				zero = new IntPtr((long)zero + (long)list[i].Length + 1L);
			}
			return safeLocalAllocHandle;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000C32B4 File Offset: 0x000C14B4
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

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000C32FC File Offset: 0x000C14FC
		internal unsafe static int VerifyCertificate(SafeCertContextHandle pCertContext, OidCollection applicationPolicy, OidCollection certificatePolicy, X509RevocationMode revocationMode, X509RevocationFlag revocationFlag, DateTime verificationTime, TimeSpan timeout, X509Certificate2Collection extraStore, IntPtr pszPolicy, IntPtr pdwErrorStatus)
		{
			if (pCertContext == null || pCertContext.IsInvalid)
			{
				throw new ArgumentException("pCertContext");
			}
			CAPIBase.CERT_CHAIN_POLICY_PARA cert_CHAIN_POLICY_PARA = new CAPIBase.CERT_CHAIN_POLICY_PARA(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_POLICY_PARA)));
			CAPIBase.CERT_CHAIN_POLICY_STATUS cert_CHAIN_POLICY_STATUS = new CAPIBase.CERT_CHAIN_POLICY_STATUS(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_POLICY_STATUS)));
			SafeX509ChainHandle invalidHandle = SafeX509ChainHandle.InvalidHandle;
			int num = X509Chain.BuildChain(new IntPtr(0L), pCertContext, extraStore, applicationPolicy, certificatePolicy, revocationMode, revocationFlag, verificationTime, timeout, ref invalidHandle);
			if (num != 0)
			{
				return num;
			}
			if (!CAPISafe.CertVerifyCertificateChainPolicy(pszPolicy, invalidHandle, ref cert_CHAIN_POLICY_PARA, ref cert_CHAIN_POLICY_STATUS))
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

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000C33AC File Offset: 0x000C15AC
		internal static string GetSystemErrorString(int hr)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			uint num = CAPISafe.FormatMessage(4608U, IntPtr.Zero, (uint)hr, 0U, stringBuilder, (uint)stringBuilder.Capacity, IntPtr.Zero);
			if (num != 0U)
			{
				return stringBuilder.ToString();
			}
			return SR.GetString("Unknown_Error");
		}

		// Token: 0x0400265F RID: 9823
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
