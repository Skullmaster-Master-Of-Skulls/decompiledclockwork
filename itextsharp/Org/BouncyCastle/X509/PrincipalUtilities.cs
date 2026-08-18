using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509
{
	// Token: 0x020000FE RID: 254
	public class PrincipalUtilities
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x0003363C File Offset: 0x0003263C
		public static X509Name GetIssuerX509Principal(X509Certificate cert)
		{
			X509Name issuer;
			try
			{
				TbsCertificateStructure instance = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(cert.GetTbsCertificate()));
				issuer = instance.Issuer;
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("Could not extract issuer", e);
			}
			return issuer;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00033684 File Offset: 0x00032684
		public static X509Name GetSubjectX509Principal(X509Certificate cert)
		{
			X509Name subject;
			try
			{
				TbsCertificateStructure instance = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(cert.GetTbsCertificate()));
				subject = instance.Subject;
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("Could not extract subject", e);
			}
			return subject;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000336CC File Offset: 0x000326CC
		public static X509Name GetIssuerX509Principal(X509Crl crl)
		{
			X509Name issuer;
			try
			{
				TbsCertificateList instance = TbsCertificateList.GetInstance(Asn1Object.FromByteArray(crl.GetTbsCertList()));
				issuer = instance.Issuer;
			}
			catch (Exception e)
			{
				throw new CrlException("Could not extract issuer", e);
			}
			return issuer;
		}
	}
}
