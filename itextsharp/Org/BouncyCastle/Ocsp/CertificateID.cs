using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x020003D1 RID: 977
	public class CertificateID
	{
		// Token: 0x060021FC RID: 8700 RVA: 0x000CDED9 File Offset: 0x000CCED9
		public CertificateID(CertID id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = id;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000CDEF8 File Offset: 0x000CCEF8
		public CertificateID(string hashAlgorithm, X509Certificate issuerCert, BigInteger serialNumber)
		{
			AlgorithmIdentifier hashAlg = new AlgorithmIdentifier(new DerObjectIdentifier(hashAlgorithm), DerNull.Instance);
			this.id = CertificateID.createCertID(hashAlg, issuerCert, new DerInteger(serialNumber));
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x000CDF2F File Offset: 0x000CCF2F
		public string HashAlgOid
		{
			get
			{
				return this.id.HashAlgorithm.ObjectID.Id;
			}
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000CDF46 File Offset: 0x000CCF46
		public byte[] GetIssuerNameHash()
		{
			return this.id.IssuerNameHash.GetOctets();
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000CDF58 File Offset: 0x000CCF58
		public byte[] GetIssuerKeyHash()
		{
			return this.id.IssuerKeyHash.GetOctets();
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x000CDF6A File Offset: 0x000CCF6A
		public BigInteger SerialNumber
		{
			get
			{
				return this.id.SerialNumber.Value;
			}
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x000CDF7C File Offset: 0x000CCF7C
		public bool MatchesIssuer(X509Certificate issuerCert)
		{
			return CertificateID.createCertID(this.id.HashAlgorithm, issuerCert, this.id.SerialNumber).Equals(this.id);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000CDFA5 File Offset: 0x000CCFA5
		public CertID ToAsn1Object()
		{
			return this.id;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000CDFB0 File Offset: 0x000CCFB0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			CertificateID certificateID = obj as CertificateID;
			return certificateID != null && this.id.ToAsn1Object().Equals(certificateID.id.ToAsn1Object());
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000CDFEA File Offset: 0x000CCFEA
		public override int GetHashCode()
		{
			return this.id.ToAsn1Object().GetHashCode();
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000CDFFC File Offset: 0x000CCFFC
		private static CertID createCertID(AlgorithmIdentifier hashAlg, X509Certificate issuerCert, DerInteger serialNumber)
		{
			CertID result;
			try
			{
				string algorithm = hashAlg.ObjectID.Id;
				X509Name subjectX509Principal = PrincipalUtilities.GetSubjectX509Principal(issuerCert);
				byte[] str = DigestUtilities.CalculateDigest(algorithm, subjectX509Principal.GetEncoded());
				AsymmetricKeyParameter publicKey = issuerCert.GetPublicKey();
				SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
				byte[] str2 = DigestUtilities.CalculateDigest(algorithm, subjectPublicKeyInfo.PublicKeyData.GetBytes());
				result = new CertID(hashAlg, new DerOctetString(str), new DerOctetString(str2), serialNumber);
			}
			catch (Exception ex)
			{
				throw new OcspException("problem creating ID: " + ex, ex);
			}
			return result;
		}

		// Token: 0x04001753 RID: 5971
		public const string HashSha1 = "1.3.14.3.2.26";

		// Token: 0x04001754 RID: 5972
		private readonly CertID id;
	}
}
