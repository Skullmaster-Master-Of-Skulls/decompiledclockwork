using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;

namespace Org.BouncyCastle.X509
{
	// Token: 0x0200022B RID: 555
	public class X509V1CertificateGenerator
	{
		// Token: 0x0600159A RID: 5530 RVA: 0x0007D722 File Offset: 0x0007C722
		public X509V1CertificateGenerator()
		{
			this.tbsGen = new V1TbsCertificateGenerator();
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0007D735 File Offset: 0x0007C735
		public void Reset()
		{
			this.tbsGen = new V1TbsCertificateGenerator();
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0007D742 File Offset: 0x0007C742
		public void SetSerialNumber(BigInteger serialNumber)
		{
			if (serialNumber.SignValue <= 0)
			{
				throw new ArgumentException("serial number must be a positive integer", "serialNumber");
			}
			this.tbsGen.SetSerialNumber(new DerInteger(serialNumber));
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0007D76E File Offset: 0x0007C76E
		public void SetIssuerDN(X509Name issuer)
		{
			this.tbsGen.SetIssuer(issuer);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0007D77C File Offset: 0x0007C77C
		public void SetNotBefore(DateTime date)
		{
			this.tbsGen.SetStartDate(new Time(date));
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0007D78F File Offset: 0x0007C78F
		public void SetNotAfter(DateTime date)
		{
			this.tbsGen.SetEndDate(new Time(date));
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0007D7A2 File Offset: 0x0007C7A2
		public void SetSubjectDN(X509Name subject)
		{
			this.tbsGen.SetSubject(subject);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0007D7B0 File Offset: 0x0007C7B0
		public void SetPublicKey(AsymmetricKeyParameter publicKey)
		{
			try
			{
				this.tbsGen.SetSubjectPublicKeyInfo(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey));
			}
			catch (Exception ex)
			{
				throw new ArgumentException("unable to process key - " + ex.ToString());
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0007D7F8 File Offset: 0x0007C7F8
		public void SetSignatureAlgorithm(string signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			try
			{
				this.sigOID = X509Utilities.GetAlgorithmOid(signatureAlgorithm);
			}
			catch (Exception)
			{
				throw new ArgumentException("Unknown signature type requested", "signatureAlgorithm");
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOID, signatureAlgorithm);
			this.tbsGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0007D860 File Offset: 0x0007C860
		public X509Certificate Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0007D86C File Offset: 0x0007C86C
		public X509Certificate Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			TbsCertificateStructure tbsCertificateStructure = this.tbsGen.GenerateTbsCertificate();
			byte[] signatureForObject;
			try
			{
				signatureForObject = X509Utilities.GetSignatureForObject(this.sigOID, this.signatureAlgorithm, privateKey, random, tbsCertificateStructure);
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("exception encoding TBS cert", e);
			}
			X509Certificate result;
			try
			{
				result = this.GenerateJcaObject(tbsCertificateStructure, signatureForObject);
			}
			catch (CertificateParsingException e2)
			{
				throw new CertificateEncodingException("exception producing certificate object", e2);
			}
			return result;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0007D8E0 File Offset: 0x0007C8E0
		private X509Certificate GenerateJcaObject(TbsCertificateStructure tbsCert, byte[] signature)
		{
			return new X509Certificate(new X509CertificateStructure(tbsCert, this.sigAlgId, new DerBitString(signature)));
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x0007D8F9 File Offset: 0x0007C8F9
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x04000F28 RID: 3880
		private V1TbsCertificateGenerator tbsGen;

		// Token: 0x04000F29 RID: 3881
		private DerObjectIdentifier sigOID;

		// Token: 0x04000F2A RID: 3882
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x04000F2B RID: 3883
		private string signatureAlgorithm;
	}
}
