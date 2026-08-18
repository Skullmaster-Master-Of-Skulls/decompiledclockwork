using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000081 RID: 129
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SubjectIdentifierOrKey
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x000044A9 File Offset: 0x000026A9
		private SubjectIdentifierOrKey()
		{
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00017D95 File Offset: 0x00015F95
		internal SubjectIdentifierOrKey(SubjectIdentifierOrKeyType type, object value)
		{
			this.Reset(type, value);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00017DA8 File Offset: 0x00015FA8
		[SecurityCritical]
		internal SubjectIdentifierOrKey(CAPI.CERT_ID certId)
		{
			uint dwIdChoice = certId.dwIdChoice;
			if (dwIdChoice == 1U)
			{
				X509IssuerSerial x509IssuerSerial = PkcsUtils.DecodeIssuerSerial(certId.Value.IssuerSerialNumber);
				this.Reset(SubjectIdentifierOrKeyType.IssuerAndSerialNumber, x509IssuerSerial);
				return;
			}
			if (dwIdChoice != 2U)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type"), certId.dwIdChoice.ToString(CultureInfo.InvariantCulture));
			}
			byte[] array = new byte[certId.Value.KeyId.cbData];
			Marshal.Copy(certId.Value.KeyId.pbData, array, 0, array.Length);
			this.Reset(SubjectIdentifierOrKeyType.SubjectKeyIdentifier, X509Utils.EncodeHexString(array));
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00017E49 File Offset: 0x00016049
		[SecurityCritical]
		internal SubjectIdentifierOrKey(CAPI.CERT_PUBLIC_KEY_INFO publicKeyInfo)
		{
			this.Reset(SubjectIdentifierOrKeyType.PublicKeyInfo, new PublicKeyInfo(publicKeyInfo));
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00017E5E File Offset: 0x0001605E
		public SubjectIdentifierOrKeyType Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00017E66 File Offset: 0x00016066
		public object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00017E70 File Offset: 0x00016070
		internal void Reset(SubjectIdentifierOrKeyType type, object value)
		{
			switch (type)
			{
			case SubjectIdentifierOrKeyType.Unknown:
				break;
			case SubjectIdentifierOrKeyType.IssuerAndSerialNumber:
				if (value.GetType() != typeof(X509IssuerSerial))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch"), value.GetType().ToString());
				}
				break;
			case SubjectIdentifierOrKeyType.SubjectKeyIdentifier:
				if (!PkcsUtils.CmsSupported())
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Not_Supported"));
				}
				if (value.GetType() != typeof(string))
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch"), value.GetType().ToString());
				}
				break;
			case SubjectIdentifierOrKeyType.PublicKeyInfo:
				if (!PkcsUtils.CmsSupported())
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Not_Supported"));
				}
				if (value.GetType() != typeof(PublicKeyInfo))
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

		// Token: 0x04000508 RID: 1288
		private SubjectIdentifierOrKeyType m_type;

		// Token: 0x04000509 RID: 1289
		private object m_value;
	}
}
