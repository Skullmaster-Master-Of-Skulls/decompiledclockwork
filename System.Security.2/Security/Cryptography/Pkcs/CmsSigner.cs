using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200006E RID: 110
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CmsSigner
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x000168AC File Offset: 0x00014AAC
		public CmsSigner() : this(SubjectIdentifierType.IssuerAndSerialNumber, null)
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000168B6 File Offset: 0x00014AB6
		public CmsSigner(SubjectIdentifierType signerIdentifierType) : this(signerIdentifierType, null)
		{
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000168C0 File Offset: 0x00014AC0
		public CmsSigner(X509Certificate2 certificate) : this(SubjectIdentifierType.IssuerAndSerialNumber, certificate)
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000168CA File Offset: 0x00014ACA
		[SecuritySafeCritical]
		public CmsSigner(CspParameters parameters) : this(SubjectIdentifierType.SubjectKeyIdentifier, PkcsUtils.CreateDummyCertificate(parameters))
		{
			this.m_dummyCert = true;
			this.IncludeOption = X509IncludeOption.None;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000168E8 File Offset: 0x00014AE8
		public CmsSigner(SubjectIdentifierType signerIdentifierType, X509Certificate2 certificate)
		{
			switch (signerIdentifierType)
			{
			case SubjectIdentifierType.Unknown:
				this.SignerIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
				this.IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			case SubjectIdentifierType.IssuerAndSerialNumber:
				this.SignerIdentifierType = signerIdentifierType;
				this.IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			case SubjectIdentifierType.SubjectKeyIdentifier:
				this.SignerIdentifierType = signerIdentifierType;
				this.IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			case SubjectIdentifierType.NoSignature:
				this.SignerIdentifierType = signerIdentifierType;
				this.IncludeOption = X509IncludeOption.None;
				break;
			default:
				this.SignerIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
				this.IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			}
			this.Certificate = certificate;
			string oidValue = LocalAppContextSwitches.CmsUseInsecureHashAlgorithms ? "1.3.14.3.2.26" : "2.16.840.1.101.3.4.2.1";
			this.DigestAlgorithm = Oid.FromOidValue(oidValue, OidGroup.HashAlgorithm);
			this.m_signedAttributes = new CryptographicAttributeObjectCollection();
			this.m_unsignedAttributes = new CryptographicAttributeObjectCollection();
			this.m_certificates = new X509Certificate2Collection();
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x000169AA File Offset: 0x00014BAA
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x000169B4 File Offset: 0x00014BB4
		public SubjectIdentifierType SignerIdentifierType
		{
			get
			{
				return this.m_signerIdentifierType;
			}
			set
			{
				if (value != SubjectIdentifierType.IssuerAndSerialNumber && value != SubjectIdentifierType.SubjectKeyIdentifier && value != SubjectIdentifierType.NoSignature)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Arg_EnumIllegalVal"), new object[]
					{
						"value"
					}));
				}
				if (this.m_dummyCert && value != SubjectIdentifierType.SubjectKeyIdentifier)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Arg_EnumIllegalVal"), new object[]
					{
						"value"
					}));
				}
				this.m_signerIdentifierType = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00016A30 File Offset: 0x00014C30
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00016A38 File Offset: 0x00014C38
		public X509Certificate2 Certificate
		{
			get
			{
				return this.m_certificate;
			}
			set
			{
				this.m_certificate = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00016A41 File Offset: 0x00014C41
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00016A49 File Offset: 0x00014C49
		public Oid DigestAlgorithm
		{
			get
			{
				return this.m_digestAlgorithm;
			}
			set
			{
				this.m_digestAlgorithm = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00016A52 File Offset: 0x00014C52
		public CryptographicAttributeObjectCollection SignedAttributes
		{
			get
			{
				return this.m_signedAttributes;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00016A5A File Offset: 0x00014C5A
		public CryptographicAttributeObjectCollection UnsignedAttributes
		{
			get
			{
				return this.m_unsignedAttributes;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00016A62 File Offset: 0x00014C62
		public X509Certificate2Collection Certificates
		{
			get
			{
				return this.m_certificates;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00016A6A File Offset: 0x00014C6A
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00016A72 File Offset: 0x00014C72
		public X509IncludeOption IncludeOption
		{
			get
			{
				return this.m_includeOption;
			}
			set
			{
				if (value < X509IncludeOption.None || value > X509IncludeOption.WholeChain)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SecurityResources.GetResourceString("Arg_EnumIllegalVal"), new object[]
					{
						"value"
					}));
				}
				this.m_includeOption = value;
			}
		}

		// Token: 0x040004C1 RID: 1217
		private SubjectIdentifierType m_signerIdentifierType;

		// Token: 0x040004C2 RID: 1218
		private X509Certificate2 m_certificate;

		// Token: 0x040004C3 RID: 1219
		private Oid m_digestAlgorithm;

		// Token: 0x040004C4 RID: 1220
		private CryptographicAttributeObjectCollection m_signedAttributes;

		// Token: 0x040004C5 RID: 1221
		private CryptographicAttributeObjectCollection m_unsignedAttributes;

		// Token: 0x040004C6 RID: 1222
		private X509Certificate2Collection m_certificates;

		// Token: 0x040004C7 RID: 1223
		private X509IncludeOption m_includeOption;

		// Token: 0x040004C8 RID: 1224
		private bool m_dummyCert;

		// Token: 0x040004C9 RID: 1225
		private const string Sha256Oid = "2.16.840.1.101.3.4.2.1";
	}
}
