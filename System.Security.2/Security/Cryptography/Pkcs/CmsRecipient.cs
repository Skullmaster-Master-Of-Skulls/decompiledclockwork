using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200006B RID: 107
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CmsRecipient
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x000044A9 File Offset: 0x000026A9
		private CmsRecipient()
		{
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00016570 File Offset: 0x00014770
		public CmsRecipient(X509Certificate2 certificate) : this(SubjectIdentifierType.IssuerAndSerialNumber, certificate)
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001657A File Offset: 0x0001477A
		public CmsRecipient(SubjectIdentifierType recipientIdentifierType, X509Certificate2 certificate)
		{
			this.Reset(recipientIdentifierType, certificate);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001658A File Offset: 0x0001478A
		public SubjectIdentifierType RecipientIdentifierType
		{
			get
			{
				return this.m_recipientIdentifierType;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00016592 File Offset: 0x00014792
		public X509Certificate2 Certificate
		{
			get
			{
				return this.m_certificate;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001659C File Offset: 0x0001479C
		private void Reset(SubjectIdentifierType recipientIdentifierType, X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			switch (recipientIdentifierType)
			{
			case SubjectIdentifierType.Unknown:
				recipientIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
				break;
			case SubjectIdentifierType.IssuerAndSerialNumber:
				break;
			case SubjectIdentifierType.SubjectKeyIdentifier:
				if (!PkcsUtils.CmsSupported())
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Not_Supported"));
				}
				break;
			default:
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Subject_Identifier_Type"), recipientIdentifierType.ToString());
			}
			this.m_recipientIdentifierType = recipientIdentifierType;
			this.m_certificate = certificate;
		}

		// Token: 0x040004BC RID: 1212
		private SubjectIdentifierType m_recipientIdentifierType;

		// Token: 0x040004BD RID: 1213
		private X509Certificate2 m_certificate;
	}
}
