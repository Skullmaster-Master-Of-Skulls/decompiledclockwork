using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.X509
{
	// Token: 0x020002E1 RID: 737
	public class X509V2CrlGenerator
	{
		// Token: 0x06001B55 RID: 6997 RVA: 0x000A4D06 File Offset: 0x000A3D06
		public X509V2CrlGenerator()
		{
			this.tbsGen = new V2TbsCertListGenerator();
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000A4D24 File Offset: 0x000A3D24
		public void Reset()
		{
			this.tbsGen = new V2TbsCertListGenerator();
			this.extGenerator.Reset();
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x000A4D3C File Offset: 0x000A3D3C
		public void SetIssuerDN(X509Name issuer)
		{
			this.tbsGen.SetIssuer(issuer);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000A4D4A File Offset: 0x000A3D4A
		public void SetThisUpdate(DateTime date)
		{
			this.tbsGen.SetThisUpdate(new Time(date));
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000A4D5D File Offset: 0x000A3D5D
		public void SetNextUpdate(DateTime date)
		{
			this.tbsGen.SetNextUpdate(new Time(date));
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000A4D70 File Offset: 0x000A3D70
		public void AddCrlEntry(BigInteger userCertificate, DateTime revocationDate, int reason)
		{
			this.tbsGen.AddCrlEntry(new DerInteger(userCertificate), new Time(revocationDate), reason);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000A4D8A File Offset: 0x000A3D8A
		public void AddCrlEntry(BigInteger userCertificate, DateTime revocationDate, int reason, DateTime invalidityDate)
		{
			this.tbsGen.AddCrlEntry(new DerInteger(userCertificate), new Time(revocationDate), reason, new DerGeneralizedTime(invalidityDate));
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x000A4DAB File Offset: 0x000A3DAB
		public void AddCrlEntry(BigInteger userCertificate, DateTime revocationDate, X509Extensions extensions)
		{
			this.tbsGen.AddCrlEntry(new DerInteger(userCertificate), new Time(revocationDate), extensions);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000A4DC8 File Offset: 0x000A3DC8
		public void AddCrl(X509Crl other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			ISet revokedCertificates = other.GetRevokedCertificates();
			if (revokedCertificates != null)
			{
				foreach (object obj in revokedCertificates)
				{
					X509CrlEntry x509CrlEntry = (X509CrlEntry)obj;
					try
					{
						this.tbsGen.AddCrlEntry(Asn1Sequence.GetInstance(Asn1Object.FromByteArray(x509CrlEntry.GetEncoded())));
					}
					catch (IOException e)
					{
						throw new CrlException("exception processing encoding of CRL", e);
					}
				}
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x000A4E68 File Offset: 0x000A3E68
		public void SetSignatureAlgorithm(string signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			try
			{
				this.sigOID = X509Utilities.GetAlgorithmOid(signatureAlgorithm);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Unknown signature type requested", innerException);
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOID, signatureAlgorithm);
			this.tbsGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x000A4ECC File Offset: 0x000A3ECC
		public void AddExtension(string oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000A4EE1 File Offset: 0x000A3EE1
		public void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, extensionValue);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000A4EF1 File Offset: 0x000A3EF1
		public void AddExtension(string oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, new DerOctetString(extensionValue));
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000A4F0B File Offset: 0x000A3F0B
		public void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, new DerOctetString(extensionValue));
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x000A4F20 File Offset: 0x000A3F20
		public X509Crl Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x000A4F2C File Offset: 0x000A3F2C
		public X509Crl Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			TbsCertificateList tbsCertificateList = this.GenerateCertList();
			byte[] signatureForObject;
			try
			{
				signatureForObject = X509Utilities.GetSignatureForObject(this.sigOID, this.signatureAlgorithm, privateKey, random, tbsCertificateList);
			}
			catch (IOException e)
			{
				throw new CrlException("cannot generate CRL encoding", e);
			}
			return this.GenerateJcaObject(tbsCertificateList, signatureForObject);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x000A4F7C File Offset: 0x000A3F7C
		private TbsCertificateList GenerateCertList()
		{
			if (!this.extGenerator.IsEmpty)
			{
				this.tbsGen.SetExtensions(this.extGenerator.Generate());
			}
			return this.tbsGen.GenerateTbsCertList();
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x000A4FAC File Offset: 0x000A3FAC
		private X509Crl GenerateJcaObject(TbsCertificateList tbsCrl, byte[] signature)
		{
			return new X509Crl(CertificateList.GetInstance(new DerSequence(new Asn1Encodable[]
			{
				tbsCrl,
				this.sigAlgId,
				new DerBitString(signature)
			})));
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x000A4FE6 File Offset: 0x000A3FE6
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x040012F0 RID: 4848
		private readonly X509ExtensionsGenerator extGenerator = new X509ExtensionsGenerator();

		// Token: 0x040012F1 RID: 4849
		private V2TbsCertListGenerator tbsGen;

		// Token: 0x040012F2 RID: 4850
		private DerObjectIdentifier sigOID;

		// Token: 0x040012F3 RID: 4851
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x040012F4 RID: 4852
		private string signatureAlgorithm;
	}
}
