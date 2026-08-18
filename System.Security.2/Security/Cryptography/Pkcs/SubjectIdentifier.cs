using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200007E RID: 126
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SubjectIdentifier
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x000044A9 File Offset: 0x000026A9
		private SubjectIdentifier()
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00017924 File Offset: 0x00015B24
		[SecurityCritical]
		internal SubjectIdentifier(CAPI.CERT_INFO certInfo) : this(certInfo.Issuer, certInfo.SerialNumber)
		{
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00017938 File Offset: 0x00015B38
		[SecurityCritical]
		internal SubjectIdentifier(CAPI.CMSG_SIGNER_INFO signerInfo) : this(signerInfo.Issuer, signerInfo.SerialNumber)
		{
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001794C File Offset: 0x00015B4C
		internal SubjectIdentifier(SubjectIdentifierType type, object value)
		{
			this.Reset(type, value);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001795C File Offset: 0x00015B5C
		[SecurityCritical]
		internal unsafe SubjectIdentifier(CAPI.CRYPTOAPI_BLOB issuer, CAPI.CRYPTOAPI_BLOB serialNumber)
		{
			bool flag = true;
			byte* ptr = (byte*)((void*)serialNumber.pbData);
			for (uint num = 0U; num < serialNumber.cbData; num += 1U)
			{
				if (*(ptr++) != 0)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				byte[] array = new byte[issuer.cbData];
				Marshal.Copy(issuer.pbData, array, 0, array.Length);
				X500DistinguishedName x500DistinguishedName = new X500DistinguishedName(array);
				if (string.Compare("CN=Dummy Signer", x500DistinguishedName.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.Reset(SubjectIdentifierType.NoSignature, null);
					return;
				}
			}
			checked
			{
				if (flag)
				{
					this.m_type = SubjectIdentifierType.SubjectKeyIdentifier;
					this.m_value = string.Empty;
					uint num2 = 0U;
					SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
					if (CAPI.DecodeObject(new IntPtr(7L), issuer.pbData, issuer.cbData, out invalidHandle, out num2))
					{
						using (invalidHandle)
						{
							CAPI.CERT_NAME_INFO cert_NAME_INFO = (CAPI.CERT_NAME_INFO)Marshal.PtrToStructure(invalidHandle.DangerousGetHandle(), typeof(CAPI.CERT_NAME_INFO));
							for (uint num3 = 0U; num3 < cert_NAME_INFO.cRDN; num3 += 1U)
							{
								CAPI.CERT_RDN cert_RDN = (CAPI.CERT_RDN)Marshal.PtrToStructure(new IntPtr((long)cert_NAME_INFO.rgRDN + (long)(unchecked((ulong)num3) * (ulong)(unchecked((long)Marshal.SizeOf(typeof(CAPI.CERT_RDN)))))), typeof(CAPI.CERT_RDN));
								for (uint num4 = 0U; num4 < cert_RDN.cRDNAttr; num4 += 1U)
								{
									CAPI.CERT_RDN_ATTR cert_RDN_ATTR = (CAPI.CERT_RDN_ATTR)Marshal.PtrToStructure(new IntPtr((long)cert_RDN.rgRDNAttr + (long)(unchecked((ulong)num4) * (ulong)(unchecked((long)Marshal.SizeOf(typeof(CAPI.CERT_RDN_ATTR)))))), typeof(CAPI.CERT_RDN_ATTR));
									if (string.Compare("1.3.6.1.4.1.311.10.7.1", cert_RDN_ATTR.pszObjId, StringComparison.OrdinalIgnoreCase) == 0 && cert_RDN_ATTR.dwValueType == 2U)
									{
										byte[] array2 = new byte[cert_RDN_ATTR.Value.cbData];
										Marshal.Copy(cert_RDN_ATTR.Value.pbData, array2, 0, array2.Length);
										this.Reset(SubjectIdentifierType.SubjectKeyIdentifier, X509Utils.EncodeHexString(array2));
										return;
									}
								}
							}
						}
					}
				}
				CAPI.CERT_ISSUER_SERIAL_NUMBER pIssuerAndSerial;
				pIssuerAndSerial.Issuer = issuer;
				pIssuerAndSerial.SerialNumber = serialNumber;
				X509IssuerSerial x509IssuerSerial = PkcsUtils.DecodeIssuerSerial(pIssuerAndSerial);
				this.Reset(SubjectIdentifierType.IssuerAndSerialNumber, x509IssuerSerial);
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00017BA4 File Offset: 0x00015DA4
		[SecurityCritical]
		internal SubjectIdentifier(CAPI.CERT_ID certId)
		{
			uint dwIdChoice = certId.dwIdChoice;
			if (dwIdChoice == 1U)
			{
				X509IssuerSerial x509IssuerSerial = PkcsUtils.DecodeIssuerSerial(certId.Value.IssuerSerialNumber);
				this.Reset(SubjectIdentifierType.IssuerAndSerialNumber, x509IssuerSerial);
				return;
			}
			if (dwIdChoice != 2U)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type"), certId.dwIdChoice.ToString(CultureInfo.InvariantCulture));
			}
			byte[] array = new byte[certId.Value.KeyId.cbData];
			Marshal.Copy(certId.Value.KeyId.pbData, array, 0, array.Length);
			this.Reset(SubjectIdentifierType.SubjectKeyIdentifier, X509Utils.EncodeHexString(array));
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00017C45 File Offset: 0x00015E45
		public SubjectIdentifierType Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00017C4D File Offset: 0x00015E4D
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00017C58 File Offset: 0x00015E58
		internal void Reset(SubjectIdentifierType type, object value)
		{
			switch (type)
			{
			case SubjectIdentifierType.Unknown:
			case SubjectIdentifierType.NoSignature:
				break;
			case SubjectIdentifierType.IssuerAndSerialNumber:
				if (value.GetType() != typeof(X509IssuerSerial))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch"), value.GetType().ToString());
				}
				break;
			case SubjectIdentifierType.SubjectKeyIdentifier:
				if (!PkcsUtils.CmsSupported())
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Not_Supported"));
				}
				if (value.GetType() != typeof(string))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch"), value.GetType().ToString());
				}
				break;
			default:
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type"), type.ToString());
			}
			this.m_type = type;
			this.m_value = value;
		}

		// Token: 0x040004FF RID: 1279
		private SubjectIdentifierType m_type;

		// Token: 0x04000500 RID: 1280
		private object m_value;
	}
}
