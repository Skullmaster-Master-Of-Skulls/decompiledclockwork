using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000084 RID: 132
	internal static class PkcsUtils
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0001854C File Offset: 0x0001674C
		internal static uint AlignedLength(uint length)
		{
			return length + 7U & 4294967288U;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00018554 File Offset: 0x00016754
		[SecuritySafeCritical]
		internal static bool CmsSupported()
		{
			if (PkcsUtils.m_cmsSupported == -1)
			{
				using (SafeLibraryHandle safeLibraryHandle = CAPI.CAPISafe.LoadLibrary("Crypt32.dll"))
				{
					if (!safeLibraryHandle.IsInvalid)
					{
						IntPtr procAddress = CAPI.CAPISafe.GetProcAddress(safeLibraryHandle, "CryptMsgVerifyCountersignatureEncodedEx");
						PkcsUtils.m_cmsSupported = ((procAddress == IntPtr.Zero) ? 0 : 1);
					}
				}
			}
			return PkcsUtils.m_cmsSupported != 0;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000185CC File Offset: 0x000167CC
		[SecuritySafeCritical]
		internal static RecipientInfoType GetRecipientInfoType(X509Certificate2 certificate)
		{
			RecipientInfoType result = RecipientInfoType.Unknown;
			if (certificate != null)
			{
				CAPI.CERT_CONTEXT cert_CONTEXT = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(X509Utils.GetCertContext(certificate).DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT));
				CAPI.CERT_INFO cert_INFO = (CAPI.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPI.CERT_INFO));
				uint num = X509Utils.OidToAlgId(cert_INFO.SubjectPublicKeyInfo.Algorithm.pszObjId);
				if (num == 41984U)
				{
					result = RecipientInfoType.KeyTransport;
				}
				else if (num == 43521U || num == 43522U)
				{
					result = RecipientInfoType.KeyAgreement;
				}
				else
				{
					result = RecipientInfoType.Unknown;
				}
			}
			return result;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00018654 File Offset: 0x00016854
		[SecurityCritical]
		internal unsafe static int GetMaxKeyLength(SafeCryptProvHandle safeCryptProvHandle, uint algId)
		{
			uint dwFlags = 1U;
			uint num = (uint)Marshal.SizeOf(typeof(CAPI.PROV_ENUMALGS_EX));
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.PROV_ENUMALGS_EX))));
			using (safeLocalAllocHandle)
			{
				while (CAPI.CAPISafe.CryptGetProvParam(safeCryptProvHandle, 22U, safeLocalAllocHandle.DangerousGetHandle(), new IntPtr((void*)(&num)), dwFlags))
				{
					CAPI.PROV_ENUMALGS_EX prov_ENUMALGS_EX = (CAPI.PROV_ENUMALGS_EX)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.PROV_ENUMALGS_EX));
					if (prov_ENUMALGS_EX.aiAlgid == algId)
					{
						return (int)prov_ENUMALGS_EX.dwMaxLen;
					}
					dwFlags = 0U;
				}
			}
			throw new CryptographicException(-2146889726);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00018708 File Offset: 0x00016908
		[SecurityCritical]
		internal unsafe static uint GetVersion(SafeCryptMsgHandle safeCryptMsgHandle)
		{
			uint result = 0U;
			uint num = (uint)Marshal.SizeOf(typeof(uint));
			if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 30U, 0U, new IntPtr((void*)(&result)), new IntPtr((void*)(&num))))
			{
				PkcsUtils.checkErr(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00018750 File Offset: 0x00016950
		[SecurityCritical]
		internal unsafe static uint GetMessageType(SafeCryptMsgHandle safeCryptMsgHandle)
		{
			uint result = 0U;
			uint num = (uint)Marshal.SizeOf(typeof(uint));
			if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 1U, 0U, new IntPtr((void*)(&result)), new IntPtr((void*)(&num))))
			{
				PkcsUtils.checkErr(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00018794 File Offset: 0x00016994
		[SecurityCritical]
		internal unsafe static AlgorithmIdentifier GetAlgorithmIdentifier(SafeCryptMsgHandle safeCryptMsgHandle)
		{
			AlgorithmIdentifier result = new AlgorithmIdentifier();
			uint num = 0U;
			if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 15U, 0U, IntPtr.Zero, new IntPtr((void*)(&num))))
			{
				PkcsUtils.checkErr(Marshal.GetLastWin32Error());
			}
			if (num > 0U)
			{
				SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
				if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 15U, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
				{
					PkcsUtils.checkErr(Marshal.GetLastWin32Error());
				}
				CAPI.CRYPT_ALGORITHM_IDENTIFIER algorithmIdentifier = (CAPI.CRYPT_ALGORITHM_IDENTIFIER)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER));
				result = new AlgorithmIdentifier(algorithmIdentifier);
				safeLocalAllocHandle.Dispose();
			}
			return result;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00018824 File Offset: 0x00016A24
		[SecurityCritical]
		internal unsafe static void GetParam(SafeCryptMsgHandle safeCryptMsgHandle, uint paramType, uint index, out SafeLocalAllocHandle pvData, out uint cbData)
		{
			cbData = 0U;
			pvData = SafeLocalAllocHandle.InvalidHandle;
			fixed (uint* ptr = &cbData)
			{
				uint* value = ptr;
				if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, paramType, index, pvData, new IntPtr((void*)value)))
				{
					PkcsUtils.checkErr(Marshal.GetLastWin32Error());
				}
				if (cbData > 0U)
				{
					pvData = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)cbData)));
					if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, paramType, index, pvData, new IntPtr((void*)value)))
					{
						PkcsUtils.checkErr(Marshal.GetLastWin32Error());
					}
				}
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00018898 File Offset: 0x00016A98
		[SecurityCritical]
		internal unsafe static void GetParam(SafeCryptMsgHandle safeCryptMsgHandle, uint paramType, uint index, out byte[] pvData, out uint cbData)
		{
			cbData = 0U;
			pvData = new byte[0];
			fixed (uint* ptr = &cbData)
			{
				uint* value = ptr;
				if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, paramType, index, IntPtr.Zero, new IntPtr((void*)value)))
				{
					PkcsUtils.checkErr(Marshal.GetLastWin32Error());
				}
				if (cbData > 0U)
				{
					pvData = new byte[cbData];
					fixed (byte* ptr2 = &pvData[0])
					{
						byte* value2 = ptr2;
						if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, paramType, index, new IntPtr((void*)value2), new IntPtr((void*)value)))
						{
							PkcsUtils.checkErr(Marshal.GetLastWin32Error());
						}
					}
				}
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00018918 File Offset: 0x00016B18
		[SecurityCritical]
		internal unsafe static X509Certificate2Collection GetCertificates(SafeCryptMsgHandle safeCryptMsgHandle)
		{
			uint num = 0U;
			uint num2 = (uint)Marshal.SizeOf(typeof(uint));
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 11U, 0U, new IntPtr((void*)(&num)), new IntPtr((void*)(&num2))))
			{
				PkcsUtils.checkErr(Marshal.GetLastWin32Error());
			}
			for (uint num3 = 0U; num3 < num; num3 += 1U)
			{
				uint num4 = 0U;
				SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
				PkcsUtils.GetParam(safeCryptMsgHandle, 12U, num3, out invalidHandle, out num4);
				if (num4 > 0U)
				{
					SafeCertContextHandle safeCertContextHandle = CAPI.CAPISafe.CertCreateCertificateContext(65537U, invalidHandle, num4);
					if (safeCertContextHandle == null || safeCertContextHandle.IsInvalid)
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					x509Certificate2Collection.Add(new X509Certificate2(safeCertContextHandle.DangerousGetHandle()));
					safeCertContextHandle.Dispose();
				}
			}
			return x509Certificate2Collection;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000189D0 File Offset: 0x00016BD0
		[SecurityCritical]
		internal static byte[] GetContent(SafeCryptMsgHandle safeCryptMsgHandle)
		{
			uint num = 0U;
			byte[] result = new byte[0];
			PkcsUtils.GetParam(safeCryptMsgHandle, 2U, 0U, out result, out num);
			return result;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000189F4 File Offset: 0x00016BF4
		[SecurityCritical]
		internal static Oid GetContentType(SafeCryptMsgHandle safeCryptMsgHandle)
		{
			uint num = 0U;
			byte[] array = new byte[0];
			PkcsUtils.GetParam(safeCryptMsgHandle, 4U, 0U, out array, out num);
			if (array.Length != 0 && array[array.Length - 1] == 0)
			{
				byte[] array2 = new byte[array.Length - 1];
				Array.Copy(array, 0, array2, 0, array2.Length);
				array = array2;
			}
			return new Oid(Encoding.ASCII.GetString(array));
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00018A4C File Offset: 0x00016C4C
		[SecurityCritical]
		internal static byte[] GetMessage(SafeCryptMsgHandle safeCryptMsgHandle)
		{
			uint num = 0U;
			byte[] result = new byte[0];
			PkcsUtils.GetParam(safeCryptMsgHandle, 29U, 0U, out result, out num);
			return result;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00018A70 File Offset: 0x00016C70
		[SecurityCritical]
		internal unsafe static int GetSignerIndex(SafeCryptMsgHandle safeCrytpMsgHandle, SignerInfo signerInfo, int startIndex)
		{
			uint num = 0U;
			uint num2 = (uint)Marshal.SizeOf(typeof(uint));
			if (!CAPI.CAPISafe.CryptMsgGetParam(safeCrytpMsgHandle, 5U, 0U, new IntPtr((void*)(&num)), new IntPtr((void*)(&num2))))
			{
				PkcsUtils.checkErr(Marshal.GetLastWin32Error());
			}
			for (int i = startIndex; i < (int)num; i++)
			{
				uint num3 = 0U;
				if (!CAPI.CAPISafe.CryptMsgGetParam(safeCrytpMsgHandle, 6U, (uint)i, IntPtr.Zero, new IntPtr((void*)(&num3))))
				{
					PkcsUtils.checkErr(Marshal.GetLastWin32Error());
				}
				if (num3 > 0U)
				{
					SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num3)));
					if (!CAPI.CAPISafe.CryptMsgGetParam(safeCrytpMsgHandle, 6U, (uint)i, safeLocalAllocHandle, new IntPtr((void*)(&num3))))
					{
						PkcsUtils.checkErr(Marshal.GetLastWin32Error());
					}
					CAPI.CMSG_SIGNER_INFO cmsgSignerInfo = signerInfo.GetCmsgSignerInfo();
					CAPI.CMSG_SIGNER_INFO cmsg_SIGNER_INFO = (CAPI.CMSG_SIGNER_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_INFO));
					if (X509Utils.MemEqual((byte*)((void*)cmsgSignerInfo.Issuer.pbData), cmsgSignerInfo.Issuer.cbData, (byte*)((void*)cmsg_SIGNER_INFO.Issuer.pbData), cmsg_SIGNER_INFO.Issuer.cbData) && X509Utils.MemEqual((byte*)((void*)cmsgSignerInfo.SerialNumber.pbData), cmsgSignerInfo.SerialNumber.cbData, (byte*)((void*)cmsg_SIGNER_INFO.SerialNumber.pbData), cmsg_SIGNER_INFO.SerialNumber.cbData))
					{
						return i;
					}
					safeLocalAllocHandle.Dispose();
				}
			}
			throw new CryptographicException(-2146889714);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00018BD8 File Offset: 0x00016DD8
		[SecurityCritical]
		internal unsafe static CryptographicAttributeObjectCollection GetUnprotectedAttributes(SafeCryptMsgHandle safeCryptMsgHandle)
		{
			uint num = 0U;
			CryptographicAttributeObjectCollection result = new CryptographicAttributeObjectCollection();
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 37U, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != -2146889713)
				{
					PkcsUtils.checkErr(Marshal.GetLastWin32Error());
				}
			}
			if (num > 0U)
			{
				SafeLocalAllocHandle safeLocalAllocHandle2;
				safeLocalAllocHandle = (safeLocalAllocHandle2 = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)num))));
				try
				{
					if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 37U, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num))))
					{
						PkcsUtils.checkErr(Marshal.GetLastWin32Error());
					}
					result = new CryptographicAttributeObjectCollection(safeLocalAllocHandle);
				}
				finally
				{
					if (safeLocalAllocHandle2 != null)
					{
						((IDisposable)safeLocalAllocHandle2).Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00018C7C File Offset: 0x00016E7C
		[SecurityCritical]
		internal unsafe static X509IssuerSerial DecodeIssuerSerial(CAPI.CERT_ISSUER_SERIAL_NUMBER pIssuerAndSerial)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			uint num = CAPI.CAPISafe.CertNameToStrW(65537U, new IntPtr((void*)(&pIssuerAndSerial.Issuer)), 33554435U, safeLocalAllocHandle, 0U);
			if (num <= 1U)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)(checked(2U * num)))));
			num = CAPI.CAPISafe.CertNameToStrW(65537U, new IntPtr((void*)(&pIssuerAndSerial.Issuer)), 33554435U, safeLocalAllocHandle, num);
			if (num <= 1U)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			X509IssuerSerial result = default(X509IssuerSerial);
			result.IssuerName = Marshal.PtrToStringUni(safeLocalAllocHandle.DangerousGetHandle());
			byte[] array = new byte[pIssuerAndSerial.SerialNumber.cbData];
			Marshal.Copy(pIssuerAndSerial.SerialNumber.pbData, array, 0, array.Length);
			result.SerialNumber = X509Utils.EncodeHexStringFromInt(array);
			safeLocalAllocHandle.Dispose();
			return result;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00018D50 File Offset: 0x00016F50
		[SecuritySafeCritical]
		internal static string DecodeOctetString(byte[] encodedOctetString)
		{
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			if (!CAPI.DecodeObject(new IntPtr(25L), encodedOctetString, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (num == 0U)
			{
				return string.Empty;
			}
			CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB = (CAPI.CRYPTOAPI_BLOB)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CRYPTOAPI_BLOB));
			if (cryptoapi_BLOB.cbData == 0U)
			{
				return string.Empty;
			}
			int num2 = (int)(cryptoapi_BLOB.cbData / 2U);
			for (int i = 0; i < num2; i++)
			{
				if (Marshal.ReadInt16(cryptoapi_BLOB.pbData, i * 2) == 0)
				{
					num2 = i;
					break;
				}
			}
			string result = Marshal.PtrToStringUni(cryptoapi_BLOB.pbData, num2);
			safeLocalAllocHandle.Dispose();
			return result;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00018DF8 File Offset: 0x00016FF8
		[SecuritySafeCritical]
		internal static byte[] DecodeOctetBytes(byte[] encodedOctetString)
		{
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			if (!CAPI.DecodeObject(new IntPtr(25L), encodedOctetString, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (num == 0U)
			{
				return new byte[0];
			}
			byte[] result;
			using (safeLocalAllocHandle)
			{
				result = CAPI.BlobToByteArray(safeLocalAllocHandle.DangerousGetHandle());
			}
			return result;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00018E60 File Offset: 0x00017060
		internal static byte[] EncodeOctetString(string octetString)
		{
			byte[] array = new byte[2 * (octetString.Length + 1)];
			Encoding.Unicode.GetBytes(octetString, 0, octetString.Length, array, 0);
			return PkcsUtils.EncodeOctetString(array);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00018E98 File Offset: 0x00017098
		[SecuritySafeCritical]
		internal unsafe static byte[] EncodeOctetString(byte[] octets)
		{
			byte* value;
			if (octets == null || octets.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &octets[0];
			}
			CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPI.CRYPTOAPI_BLOB);
			cryptoapi_BLOB.cbData = (uint)octets.Length;
			cryptoapi_BLOB.pbData = new IntPtr((void*)value);
			byte[] result = new byte[0];
			if (!CAPI.EncodeObject(new IntPtr(25L), new IntPtr(&cryptoapi_BLOB), out result))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00018F0C File Offset: 0x0001710C
		internal static string DecodeObjectIdentifier(byte[] encodedObjId, int offset)
		{
			StringBuilder stringBuilder = new StringBuilder("");
			if (0 < encodedObjId.Length - offset)
			{
				byte b = encodedObjId[offset];
				stringBuilder.Append((b / 40).ToString(null, null));
				stringBuilder.Append(".");
				stringBuilder.Append((b % 40).ToString(null, null));
				ulong num = 0UL;
				for (int i = offset + 1; i < encodedObjId.Length; i++)
				{
					byte b2 = encodedObjId[i];
					num = (num << 7) + (ulong)((long)(b2 & 127));
					if ((b2 & 128) == 0)
					{
						stringBuilder.Append(".");
						stringBuilder.Append(num.ToString(null, null));
						num = 0UL;
					}
				}
				if (num != 0UL)
				{
					throw new CryptographicException(-2146885630);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00018FD0 File Offset: 0x000171D0
		internal static CmsRecipientCollection SelectRecipients(SubjectIdentifierType recipientIdentifierType)
		{
			X509Store x509Store = new X509Store("AddressBook");
			x509Store.Open(OpenFlags.OpenExistingOnly);
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection(x509Store.Certificates);
			foreach (X509Certificate2 x509Certificate in x509Store.Certificates)
			{
				if (x509Certificate.NotBefore <= DateTime.Now && x509Certificate.NotAfter >= DateTime.Now)
				{
					bool flag = true;
					foreach (X509Extension x509Extension in x509Certificate.Extensions)
					{
						if (string.Compare(x509Extension.Oid.Value, "2.5.29.15", StringComparison.OrdinalIgnoreCase) == 0)
						{
							X509KeyUsageExtension x509KeyUsageExtension = new X509KeyUsageExtension();
							x509KeyUsageExtension.CopyFrom(x509Extension);
							if ((x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.KeyEncipherment) == X509KeyUsageFlags.None && (x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.KeyAgreement) == X509KeyUsageFlags.None)
							{
								flag = false;
								break;
							}
							break;
						}
					}
					if (flag)
					{
						x509Certificate2Collection.Add(x509Certificate);
					}
				}
			}
			if (x509Certificate2Collection.Count < 1)
			{
				throw new CryptographicException(-2146889717);
			}
			X509Certificate2Collection x509Certificate2Collection2 = X509Certificate2UI.SelectFromCollection(x509Certificate2Collection, null, null, X509SelectionFlag.MultiSelection);
			if (x509Certificate2Collection2.Count < 1)
			{
				throw new CryptographicException(1223);
			}
			return new CmsRecipientCollection(recipientIdentifierType, x509Certificate2Collection2);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000190F8 File Offset: 0x000172F8
		internal static X509Certificate2 SelectSignerCertificate()
		{
			X509Store x509Store = new X509Store();
			x509Store.Open(OpenFlags.OpenExistingOnly | OpenFlags.IncludeArchived);
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			foreach (X509Certificate2 x509Certificate in x509Store.Certificates)
			{
				if (x509Certificate.HasPrivateKey && x509Certificate.NotBefore <= DateTime.Now && x509Certificate.NotAfter >= DateTime.Now)
				{
					bool flag = true;
					foreach (X509Extension x509Extension in x509Certificate.Extensions)
					{
						if (string.Compare(x509Extension.Oid.Value, "2.5.29.15", StringComparison.OrdinalIgnoreCase) == 0)
						{
							X509KeyUsageExtension x509KeyUsageExtension = new X509KeyUsageExtension();
							x509KeyUsageExtension.CopyFrom(x509Extension);
							if ((x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.DigitalSignature) == X509KeyUsageFlags.None && (x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.NonRepudiation) == X509KeyUsageFlags.None)
							{
								flag = false;
								break;
							}
							break;
						}
					}
					if (flag)
					{
						x509Certificate2Collection.Add(x509Certificate);
					}
				}
			}
			if (x509Certificate2Collection.Count < 1)
			{
				throw new CryptographicException(-2146889714);
			}
			x509Certificate2Collection = X509Certificate2UI.SelectFromCollection(x509Certificate2Collection, null, null, X509SelectionFlag.SingleSelection);
			if (x509Certificate2Collection.Count < 1)
			{
				throw new CryptographicException(1223);
			}
			return x509Certificate2Collection[0];
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00019220 File Offset: 0x00017420
		[SecuritySafeCritical]
		internal static AsnEncodedDataCollection GetAsnEncodedDataCollection(CAPI.CRYPT_ATTRIBUTE cryptAttribute)
		{
			AsnEncodedDataCollection asnEncodedDataCollection = new AsnEncodedDataCollection();
			Oid oid = new Oid(cryptAttribute.pszObjId);
			string value = oid.Value;
			for (uint num = 0U; num < cryptAttribute.cValue; num += 1U)
			{
				IntPtr pBlob = new IntPtr(checked((long)cryptAttribute.rgValue + (long)(unchecked((ulong)num) * (ulong)(unchecked((long)Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB)))))));
				Pkcs9AttributeObject asnEncodedData = new Pkcs9AttributeObject(oid, CAPI.BlobToByteArray(pBlob));
				Pkcs9AttributeObject pkcs9AttributeObject = CryptoConfig.CreateFromName(value) as Pkcs9AttributeObject;
				if (pkcs9AttributeObject != null)
				{
					pkcs9AttributeObject.CopyFrom(asnEncodedData);
					asnEncodedData = pkcs9AttributeObject;
				}
				asnEncodedDataCollection.Add(asnEncodedData);
			}
			return asnEncodedDataCollection;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000192B4 File Offset: 0x000174B4
		[SecurityCritical]
		internal static AsnEncodedDataCollection GetAsnEncodedDataCollection(CAPI.CRYPT_ATTRIBUTE_TYPE_VALUE cryptAttribute)
		{
			return new AsnEncodedDataCollection
			{
				new Pkcs9AttributeObject(new Oid(cryptAttribute.pszObjId), CAPI.BlobToByteArray(cryptAttribute.Value))
			};
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000192EC File Offset: 0x000174EC
		[SecurityCritical]
		internal unsafe static IntPtr CreateCryptAttributes(CryptographicAttributeObjectCollection attributes)
		{
			attributes = attributes.DeepCopy();
			if (attributes.Count == 0)
			{
				return IntPtr.Zero;
			}
			uint num = 0U;
			checked
			{
				uint num2 = PkcsUtils.AlignedLength((uint)Marshal.SizeOf(typeof(PkcsUtils.I_CRYPT_ATTRIBUTE)));
				uint num3 = PkcsUtils.AlignedLength((uint)Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB)));
				foreach (CryptographicAttributeObject cryptographicAttributeObject in attributes)
				{
					num += num2;
					num += PkcsUtils.AlignedLength((uint)(cryptographicAttributeObject.Oid.Value.Length + 1));
					foreach (AsnEncodedData asnEncodedData in cryptographicAttributeObject.Values)
					{
						num += num3;
						num += PkcsUtils.AlignedLength((uint)asnEncodedData.RawData.Length);
					}
				}
				SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)(unchecked((ulong)num))));
				PkcsUtils.I_CRYPT_ATTRIBUTE* ptr = (PkcsUtils.I_CRYPT_ATTRIBUTE*)((void*)safeLocalAllocHandle.DangerousGetHandle());
				IntPtr value = new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + (long)(unchecked((ulong)num2) * (ulong)(unchecked((long)attributes.Count))));
				foreach (CryptographicAttributeObject cryptographicAttributeObject2 in attributes)
				{
					byte* ptr2 = (byte*)((void*)value);
					byte[] array = new byte[cryptographicAttributeObject2.Oid.Value.Length + 1];
					CAPI.CRYPTOAPI_BLOB* ptr3 = (CAPI.CRYPTOAPI_BLOB*)(ptr2 + PkcsUtils.AlignedLength((uint)array.Length));
					ptr->pszObjId = (IntPtr)((void*)ptr2);
					ptr->cValue = (uint)cryptographicAttributeObject2.Values.Count;
					ptr->rgValue = (IntPtr)((void*)ptr3);
					Encoding.ASCII.GetBytes(cryptographicAttributeObject2.Oid.Value, 0, cryptographicAttributeObject2.Oid.Value.Length, array, 0);
					Marshal.Copy(array, 0, ptr->pszObjId, array.Length);
					IntPtr intPtr = new IntPtr(ptr3 + unchecked((long)cryptographicAttributeObject2.Values.Count) * (long)(unchecked((ulong)num3)) / (long)sizeof(CAPI.CRYPTOAPI_BLOB));
					foreach (AsnEncodedData asnEncodedData2 in cryptographicAttributeObject2.Values)
					{
						byte[] rawData = asnEncodedData2.RawData;
						if (rawData.Length != 0)
						{
							ptr3->cbData = (uint)rawData.Length;
							ptr3->pbData = intPtr;
							Marshal.Copy(rawData, 0, intPtr, rawData.Length);
							intPtr = new IntPtr((long)intPtr + (long)(unchecked((ulong)PkcsUtils.AlignedLength(checked((uint)rawData.Length)))));
						}
						ptr3++;
					}
					ptr++;
					value = intPtr;
				}
				GC.SuppressFinalize(safeLocalAllocHandle);
				return safeLocalAllocHandle.DangerousGetHandle();
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001955C File Offset: 0x0001775C
		[SecuritySafeCritical]
		internal static CAPI.CMSG_SIGNER_ENCODE_INFO CreateSignerEncodeInfo(CmsSigner signer, out SafeCryptProvHandle hProv)
		{
			return PkcsUtils.CreateSignerEncodeInfo(signer, false, out hProv);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00019568 File Offset: 0x00017768
		[SecuritySafeCritical]
		internal unsafe static CAPI.CMSG_SIGNER_ENCODE_INFO CreateSignerEncodeInfo(CmsSigner signer, bool silent, out SafeCryptProvHandle hProv)
		{
			CAPI.CMSG_SIGNER_ENCODE_INFO cmsg_SIGNER_ENCODE_INFO = new CAPI.CMSG_SIGNER_ENCODE_INFO(Marshal.SizeOf(typeof(CAPI.CMSG_SIGNER_ENCODE_INFO)));
			SafeCryptProvHandle invalidHandle = SafeCryptProvHandle.InvalidHandle;
			uint dwKeySpec = 0U;
			cmsg_SIGNER_ENCODE_INFO.HashAlgorithm.pszObjId = signer.DigestAlgorithm.Value;
			if (string.Compare(signer.Certificate.PublicKey.Oid.Value, "1.2.840.10040.4.1", StringComparison.Ordinal) == 0)
			{
				cmsg_SIGNER_ENCODE_INFO.HashEncryptionAlgorithm.pszObjId = "1.2.840.10040.4.3";
			}
			cmsg_SIGNER_ENCODE_INFO.cAuthAttr = (uint)signer.SignedAttributes.Count;
			cmsg_SIGNER_ENCODE_INFO.rgAuthAttr = PkcsUtils.CreateCryptAttributes(signer.SignedAttributes);
			cmsg_SIGNER_ENCODE_INFO.cUnauthAttr = (uint)signer.UnsignedAttributes.Count;
			cmsg_SIGNER_ENCODE_INFO.rgUnauthAttr = PkcsUtils.CreateCryptAttributes(signer.UnsignedAttributes);
			if (signer.SignerIdentifierType == SubjectIdentifierType.NoSignature)
			{
				cmsg_SIGNER_ENCODE_INFO.HashEncryptionAlgorithm.pszObjId = "1.3.6.1.5.5.7.6.2";
				cmsg_SIGNER_ENCODE_INFO.pCertInfo = IntPtr.Zero;
				cmsg_SIGNER_ENCODE_INFO.dwKeySpec = dwKeySpec;
				if (!CAPI.CryptAcquireContext(ref invalidHandle, null, null, 1U, 4026531840U))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				cmsg_SIGNER_ENCODE_INFO.hCryptProv = invalidHandle.DangerousGetHandle();
				hProv = invalidHandle;
				cmsg_SIGNER_ENCODE_INFO.SignerId.dwIdChoice = 1U;
				X500DistinguishedName x500DistinguishedName = new X500DistinguishedName("CN=Dummy Signer");
				x500DistinguishedName.Oid = Oid.FromOidValue("1.3.6.1.4.1.311.21.9", OidGroup.ExtensionOrAttribute);
				cmsg_SIGNER_ENCODE_INFO.SignerId.Value.IssuerSerialNumber.Issuer.cbData = (uint)x500DistinguishedName.RawData.Length;
				SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)cmsg_SIGNER_ENCODE_INFO.SignerId.Value.IssuerSerialNumber.Issuer.cbData)));
				Marshal.Copy(x500DistinguishedName.RawData, 0, safeLocalAllocHandle.DangerousGetHandle(), x500DistinguishedName.RawData.Length);
				cmsg_SIGNER_ENCODE_INFO.SignerId.Value.IssuerSerialNumber.Issuer.pbData = safeLocalAllocHandle.DangerousGetHandle();
				GC.SuppressFinalize(safeLocalAllocHandle);
				cmsg_SIGNER_ENCODE_INFO.SignerId.Value.IssuerSerialNumber.SerialNumber.cbData = 1U;
				SafeLocalAllocHandle safeLocalAllocHandle2 = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)cmsg_SIGNER_ENCODE_INFO.SignerId.Value.IssuerSerialNumber.SerialNumber.cbData)));
				byte* ptr = (byte*)((void*)safeLocalAllocHandle2.DangerousGetHandle());
				*ptr = 0;
				cmsg_SIGNER_ENCODE_INFO.SignerId.Value.IssuerSerialNumber.SerialNumber.pbData = safeLocalAllocHandle2.DangerousGetHandle();
				GC.SuppressFinalize(safeLocalAllocHandle2);
				return cmsg_SIGNER_ENCODE_INFO;
			}
			else
			{
				SafeCertContextHandle certContext = X509Utils.GetCertContext(signer.Certificate);
				int certPrivateKey = PkcsUtils.GetCertPrivateKey(certContext, out invalidHandle, out dwKeySpec);
				if (certPrivateKey != 0)
				{
					throw new CryptographicException(certPrivateKey);
				}
				cmsg_SIGNER_ENCODE_INFO.dwKeySpec = dwKeySpec;
				cmsg_SIGNER_ENCODE_INFO.hCryptProv = invalidHandle.DangerousGetHandle();
				hProv = invalidHandle;
				CAPI.CERT_CONTEXT cert_CONTEXT = *(CAPI.CERT_CONTEXT*)((void*)certContext.DangerousGetHandle());
				cmsg_SIGNER_ENCODE_INFO.pCertInfo = cert_CONTEXT.pCertInfo;
				if (signer.SignerIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
				{
					uint num = 0U;
					SafeLocalAllocHandle safeLocalAllocHandle3 = SafeLocalAllocHandle.InvalidHandle;
					if (!CAPI.CAPISafe.CertGetCertificateContextProperty(certContext, 20U, safeLocalAllocHandle3, ref num))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					if (num > 0U)
					{
						safeLocalAllocHandle3 = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)num)));
						if (!CAPI.CAPISafe.CertGetCertificateContextProperty(certContext, 20U, safeLocalAllocHandle3, ref num))
						{
							throw new CryptographicException(Marshal.GetLastWin32Error());
						}
						cmsg_SIGNER_ENCODE_INFO.SignerId.dwIdChoice = 2U;
						cmsg_SIGNER_ENCODE_INFO.SignerId.Value.KeyId.cbData = num;
						cmsg_SIGNER_ENCODE_INFO.SignerId.Value.KeyId.pbData = safeLocalAllocHandle3.DangerousGetHandle();
						GC.SuppressFinalize(safeLocalAllocHandle3);
					}
				}
				return cmsg_SIGNER_ENCODE_INFO;
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000198C8 File Offset: 0x00017AC8
		[SecurityCritical]
		internal static int GetCertPrivateKey(SafeCertContextHandle safeCertContextHandle, out SafeCryptProvHandle safeCryptProvHandle, out uint keySpec)
		{
			bool ownsHandle = false;
			uint size = (uint)IntPtr.Size;
			safeCryptProvHandle = null;
			IntPtr zero;
			if (CAPI.CAPISafe.CertGetCertificateContextProperty(safeCertContextHandle, 78U, out zero, ref size))
			{
				keySpec = 0U;
				safeCryptProvHandle = new SafeCryptProvHandle(zero, safeCertContextHandle);
				return 0;
			}
			CspParameters cspParameters = new CspParameters();
			if (!X509Utils.GetPrivateKeyInfo(safeCertContextHandle, ref cspParameters))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (string.Compare(cspParameters.ProviderName, "Microsoft Base Cryptographic Provider v1.0", StringComparison.OrdinalIgnoreCase) == 0)
			{
				SafeCryptProvHandle invalidHandle = SafeCryptProvHandle.InvalidHandle;
				if (CAPI.CryptAcquireContext(ref invalidHandle, cspParameters.KeyContainerName, "Microsoft Enhanced Cryptographic Provider v1.0", 1U, 0U) || CAPI.CryptAcquireContext(ref invalidHandle, cspParameters.KeyContainerName, "Microsoft Strong Cryptographic Provider", 1U, 0U))
				{
					safeCryptProvHandle = invalidHandle;
				}
			}
			keySpec = (uint)cspParameters.KeyNumber;
			int result = 0;
			uint num = 6U;
			if (cspParameters.ProviderType == 0)
			{
				num |= 131072U;
			}
			if (safeCryptProvHandle == null || safeCryptProvHandle.IsInvalid)
			{
				zero = IntPtr.Zero;
				if (CAPI.CAPISafe.CryptAcquireCertificatePrivateKey(safeCertContextHandle, num, IntPtr.Zero, ref zero, ref keySpec, ref ownsHandle))
				{
					safeCryptProvHandle = new SafeCryptProvHandle(zero, ownsHandle);
				}
				else
				{
					result = Marshal.GetHRForLastWin32Error();
				}
			}
			return result;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000199BC File Offset: 0x00017BBC
		[SecuritySafeCritical]
		internal static X509Certificate2Collection CreateBagOfCertificates(CmsSigner signer)
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			x509Certificate2Collection.AddRange(signer.Certificates);
			if (signer.IncludeOption != X509IncludeOption.None)
			{
				if (signer.IncludeOption == X509IncludeOption.EndCertOnly)
				{
					x509Certificate2Collection.Add(signer.Certificate);
				}
				else
				{
					int num = 1;
					X509Chain x509Chain = new X509Chain();
					x509Chain.Build(signer.Certificate);
					if (x509Chain.ChainStatus.Length != 0 && (x509Chain.ChainStatus[0].Status & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.PartialChain)
					{
						throw new CryptographicException(-2146762486);
					}
					if (signer.IncludeOption == X509IncludeOption.WholeChain)
					{
						num = x509Chain.ChainElements.Count;
					}
					else if (x509Chain.ChainElements.Count > 1)
					{
						num = x509Chain.ChainElements.Count - 1;
					}
					for (int i = 0; i < num; i++)
					{
						x509Certificate2Collection.Add(x509Chain.ChainElements[i].Certificate);
					}
				}
			}
			return x509Certificate2Collection;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00019AA0 File Offset: 0x00017CA0
		[SecurityCritical]
		internal unsafe static SafeLocalAllocHandle CreateEncodedCertBlob(X509Certificate2Collection certificates)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			certificates = new X509Certificate2Collection(certificates);
			checked
			{
				if (certificates.Count > 0)
				{
					safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(certificates.Count * Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB))));
					CAPI.CRYPTOAPI_BLOB* ptr = (CAPI.CRYPTOAPI_BLOB*)((void*)safeLocalAllocHandle.DangerousGetHandle());
					foreach (X509Certificate2 certificate in certificates)
					{
						SafeCertContextHandle certContext = X509Utils.GetCertContext(certificate);
						CAPI.CERT_CONTEXT cert_CONTEXT = *(CAPI.CERT_CONTEXT*)((void*)certContext.DangerousGetHandle());
						ptr->cbData = cert_CONTEXT.cbCertEncoded;
						ptr->pbData = cert_CONTEXT.pbCertEncoded;
						ptr++;
					}
				}
				return safeLocalAllocHandle;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00019B4C File Offset: 0x00017D4C
		[SecuritySafeCritical]
		internal unsafe static uint AddCertsToMessage(SafeCryptMsgHandle safeCryptMsgHandle, X509Certificate2Collection bagOfCerts, X509Certificate2Collection chainOfCerts)
		{
			uint num = 0U;
			foreach (X509Certificate2 x509Certificate in chainOfCerts)
			{
				X509Certificate2Collection x509Certificate2Collection = bagOfCerts.Find(X509FindType.FindByThumbprint, x509Certificate.Thumbprint, false);
				if (x509Certificate2Collection.Count == 0)
				{
					SafeCertContextHandle certContext = X509Utils.GetCertContext(x509Certificate);
					CAPI.CERT_CONTEXT cert_CONTEXT = *(CAPI.CERT_CONTEXT*)((void*)certContext.DangerousGetHandle());
					CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPI.CRYPTOAPI_BLOB);
					cryptoapi_BLOB.cbData = cert_CONTEXT.cbCertEncoded;
					cryptoapi_BLOB.pbData = cert_CONTEXT.pbCertEncoded;
					if (!CAPI.CryptMsgControl(safeCryptMsgHandle, 0U, 10U, new IntPtr(&cryptoapi_BLOB)))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					num += 1U;
				}
			}
			return num;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00019BF4 File Offset: 0x00017DF4
		internal static X509Certificate2 FindCertificate(SubjectIdentifier identifier, X509Certificate2Collection certificates)
		{
			X509Certificate2 result = null;
			if (certificates != null && certificates.Count > 0)
			{
				SubjectIdentifierType type = identifier.Type;
				if (type != SubjectIdentifierType.IssuerAndSerialNumber)
				{
					if (type == SubjectIdentifierType.SubjectKeyIdentifier)
					{
						X509Certificate2Collection x509Certificate2Collection = certificates.Find(X509FindType.FindBySubjectKeyIdentifier, identifier.Value, false);
						if (x509Certificate2Collection.Count > 0)
						{
							result = x509Certificate2Collection[0];
						}
					}
				}
				else
				{
					X509Certificate2Collection x509Certificate2Collection = certificates.Find(X509FindType.FindByIssuerDistinguishedName, ((X509IssuerSerial)identifier.Value).IssuerName, false);
					if (x509Certificate2Collection.Count > 0)
					{
						x509Certificate2Collection = x509Certificate2Collection.Find(X509FindType.FindBySerialNumber, ((X509IssuerSerial)identifier.Value).SerialNumber, false);
						if (x509Certificate2Collection.Count > 0)
						{
							result = x509Certificate2Collection[0];
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00019C9C File Offset: 0x00017E9C
		private static void checkErr(int err)
		{
			if (-2146889724 != err)
			{
				throw new CryptographicException(err);
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00019CB0 File Offset: 0x00017EB0
		[SecuritySafeCritical]
		internal unsafe static X509Certificate2 CreateDummyCertificate(CspParameters parameters)
		{
			SafeCertContextHandle safeCertContextHandle = SafeCertContextHandle.InvalidHandle;
			SafeCryptProvHandle invalidHandle = SafeCryptProvHandle.InvalidHandle;
			uint num = 0U;
			if ((parameters.Flags & CspProviderFlags.UseMachineKeyStore) != CspProviderFlags.NoFlags)
			{
				num |= 32U;
			}
			if ((parameters.Flags & CspProviderFlags.UseDefaultKeyContainer) != CspProviderFlags.NoFlags)
			{
				num |= 4026531840U;
			}
			if ((parameters.Flags & CspProviderFlags.NoPrompt) != CspProviderFlags.NoFlags)
			{
				num |= 64U;
			}
			if (!CAPI.CryptAcquireContext(ref invalidHandle, parameters.KeyContainerName, parameters.ProviderName, (uint)parameters.ProviderType, num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			CAPI.CRYPT_KEY_PROV_INFO crypt_KEY_PROV_INFO = default(CAPI.CRYPT_KEY_PROV_INFO);
			crypt_KEY_PROV_INFO.pwszProvName = parameters.ProviderName;
			crypt_KEY_PROV_INFO.pwszContainerName = parameters.KeyContainerName;
			crypt_KEY_PROV_INFO.dwProvType = (uint)parameters.ProviderType;
			crypt_KEY_PROV_INFO.dwKeySpec = (uint)parameters.KeyNumber;
			crypt_KEY_PROV_INFO.dwFlags = (((parameters.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore) ? 32U : 0U);
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CRYPT_KEY_PROV_INFO))));
			Marshal.StructureToPtr(crypt_KEY_PROV_INFO, safeLocalAllocHandle.DangerousGetHandle(), false);
			CAPI.CRYPT_ALGORITHM_IDENTIFIER crypt_ALGORITHM_IDENTIFIER = default(CAPI.CRYPT_ALGORITHM_IDENTIFIER);
			crypt_ALGORITHM_IDENTIFIER.pszObjId = "1.3.14.3.2.29";
			SafeLocalAllocHandle safeLocalAllocHandle2 = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER))));
			Marshal.StructureToPtr(crypt_ALGORITHM_IDENTIFIER, safeLocalAllocHandle2.DangerousGetHandle(), false);
			X500DistinguishedName x500DistinguishedName = new X500DistinguishedName("cn=CMS Signer Dummy Certificate");
			byte[] array;
			byte* value;
			if ((array = x500DistinguishedName.RawData) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPI.CRYPTOAPI_BLOB);
			cryptoapi_BLOB.cbData = (uint)x500DistinguishedName.RawData.Length;
			cryptoapi_BLOB.pbData = new IntPtr((void*)value);
			safeCertContextHandle = CAPI.CAPIUnsafe.CertCreateSelfSignCertificate(invalidHandle, new IntPtr((void*)(&cryptoapi_BLOB)), 1U, safeLocalAllocHandle.DangerousGetHandle(), safeLocalAllocHandle2.DangerousGetHandle(), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			array = null;
			Marshal.DestroyStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CRYPT_KEY_PROV_INFO));
			safeLocalAllocHandle.Dispose();
			Marshal.DestroyStructure(safeLocalAllocHandle2.DangerousGetHandle(), typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER));
			safeLocalAllocHandle2.Dispose();
			if (safeCertContextHandle == null || safeCertContextHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			X509Certificate2 result = new X509Certificate2(safeCertContextHandle.DangerousGetHandle());
			safeCertContextHandle.Dispose();
			return result;
		}

		// Token: 0x04000511 RID: 1297
		private static volatile int m_cmsSupported = -1;

		// Token: 0x020000E2 RID: 226
		private struct I_CRYPT_ATTRIBUTE
		{
			// Token: 0x04000698 RID: 1688
			internal IntPtr pszObjId;

			// Token: 0x04000699 RID: 1689
			internal uint cValue;

			// Token: 0x0400069A RID: 1690
			internal IntPtr rgValue;
		}
	}
}
