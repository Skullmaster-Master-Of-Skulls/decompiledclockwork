using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509.Extension
{
	// Token: 0x02000427 RID: 1063
	public class SubjectKeyIdentifierStructure : SubjectKeyIdentifier
	{
		// Token: 0x06002433 RID: 9267 RVA: 0x000DC814 File Offset: 0x000DB814
		public SubjectKeyIdentifierStructure(Asn1OctetString encodedValue) : base((Asn1OctetString)X509ExtensionUtilities.FromExtensionValue(encodedValue))
		{
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000DC828 File Offset: 0x000DB828
		private static Asn1OctetString FromPublicKey(AsymmetricKeyParameter pubKey)
		{
			Asn1OctetString result;
			try
			{
				SubjectPublicKeyInfo spki = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKey);
				result = (Asn1OctetString)new SubjectKeyIdentifier(spki).ToAsn1Object();
			}
			catch (Exception ex)
			{
				throw new CertificateParsingException("Exception extracting certificate details: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000DC878 File Offset: 0x000DB878
		public SubjectKeyIdentifierStructure(AsymmetricKeyParameter pubKey) : base(SubjectKeyIdentifierStructure.FromPublicKey(pubKey))
		{
		}
	}
}
