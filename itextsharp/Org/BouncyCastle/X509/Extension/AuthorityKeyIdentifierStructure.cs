using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509.Extension
{
	// Token: 0x020004FA RID: 1274
	public class AuthorityKeyIdentifierStructure : AuthorityKeyIdentifier
	{
		// Token: 0x06002B93 RID: 11155 RVA: 0x00108315 File Offset: 0x00107315
		public AuthorityKeyIdentifierStructure(Asn1OctetString encodedValue) : base((Asn1Sequence)X509ExtensionUtilities.FromExtensionValue(encodedValue))
		{
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x00108328 File Offset: 0x00107328
		private static Asn1Sequence FromCertificate(X509Certificate certificate)
		{
			Asn1Sequence result;
			try
			{
				GeneralName name = new GeneralName(PrincipalUtilities.GetIssuerX509Principal(certificate));
				if (certificate.Version == 3)
				{
					Asn1OctetString extensionValue = certificate.GetExtensionValue(X509Extensions.SubjectKeyIdentifier);
					if (extensionValue != null)
					{
						Asn1OctetString asn1OctetString = (Asn1OctetString)X509ExtensionUtilities.FromExtensionValue(extensionValue);
						return (Asn1Sequence)new AuthorityKeyIdentifier(asn1OctetString.GetOctets(), new GeneralNames(name), certificate.SerialNumber).ToAsn1Object();
					}
				}
				SubjectPublicKeyInfo spki = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(certificate.GetPublicKey());
				result = (Asn1Sequence)new AuthorityKeyIdentifier(spki, new GeneralNames(name), certificate.SerialNumber).ToAsn1Object();
			}
			catch (Exception exception)
			{
				throw new CertificateParsingException("Exception extracting certificate details", exception);
			}
			return result;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x001083D8 File Offset: 0x001073D8
		private static Asn1Sequence FromKey(AsymmetricKeyParameter pubKey)
		{
			Asn1Sequence result;
			try
			{
				SubjectPublicKeyInfo spki = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKey);
				result = (Asn1Sequence)new AuthorityKeyIdentifier(spki).ToAsn1Object();
			}
			catch (Exception arg)
			{
				throw new InvalidKeyException("can't process key: " + arg);
			}
			return result;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x00108424 File Offset: 0x00107424
		public AuthorityKeyIdentifierStructure(X509Certificate certificate) : base(AuthorityKeyIdentifierStructure.FromCertificate(certificate))
		{
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x00108432 File Offset: 0x00107432
		public AuthorityKeyIdentifierStructure(AsymmetricKeyParameter pubKey) : base(AuthorityKeyIdentifierStructure.FromKey(pubKey))
		{
		}
	}
}
