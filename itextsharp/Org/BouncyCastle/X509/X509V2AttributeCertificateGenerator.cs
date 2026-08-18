using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.X509
{
	// Token: 0x020001D4 RID: 468
	public class X509V2AttributeCertificateGenerator
	{
		// Token: 0x0600123F RID: 4671 RVA: 0x0006881D File Offset: 0x0006781D
		public X509V2AttributeCertificateGenerator()
		{
			this.acInfoGen = new V2AttributeCertificateInfoGenerator();
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0006883B File Offset: 0x0006783B
		public void Reset()
		{
			this.acInfoGen = new V2AttributeCertificateInfoGenerator();
			this.extGenerator.Reset();
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00068853 File Offset: 0x00067853
		public void SetHolder(AttributeCertificateHolder holder)
		{
			this.acInfoGen.SetHolder(holder.holder);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00068866 File Offset: 0x00067866
		public void SetIssuer(AttributeCertificateIssuer issuer)
		{
			this.acInfoGen.SetIssuer(AttCertIssuer.GetInstance(issuer.form));
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0006887E File Offset: 0x0006787E
		public void SetSerialNumber(BigInteger serialNumber)
		{
			this.acInfoGen.SetSerialNumber(new DerInteger(serialNumber));
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00068891 File Offset: 0x00067891
		public void SetNotBefore(DateTime date)
		{
			this.acInfoGen.SetStartDate(new DerGeneralizedTime(date));
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000688A4 File Offset: 0x000678A4
		public void SetNotAfter(DateTime date)
		{
			this.acInfoGen.SetEndDate(new DerGeneralizedTime(date));
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000688B8 File Offset: 0x000678B8
		public void SetSignatureAlgorithm(string signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			try
			{
				this.sigOID = X509Utilities.GetAlgorithmOid(signatureAlgorithm);
			}
			catch (Exception)
			{
				throw new ArgumentException("Unknown signature type requested");
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOID, signatureAlgorithm);
			this.acInfoGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0006891C File Offset: 0x0006791C
		public void AddAttribute(X509Attribute attribute)
		{
			this.acInfoGen.AddAttribute(AttributeX509.GetInstance(attribute.ToAsn1Object()));
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00068934 File Offset: 0x00067934
		public void SetIssuerUniqueId(bool[] iui)
		{
			throw Platform.CreateNotImplementedException("SetIssuerUniqueId()");
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00068940 File Offset: 0x00067940
		public void AddExtension(string oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00068955 File Offset: 0x00067955
		public void AddExtension(string oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0006896A File Offset: 0x0006796A
		public IX509AttributeCertificate Generate(AsymmetricKeyParameter publicKey)
		{
			return this.Generate(publicKey, null);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00068974 File Offset: 0x00067974
		public IX509AttributeCertificate Generate(AsymmetricKeyParameter publicKey, SecureRandom random)
		{
			if (!this.extGenerator.IsEmpty)
			{
				this.acInfoGen.SetExtensions(this.extGenerator.Generate());
			}
			AttributeCertificateInfo attributeCertificateInfo = this.acInfoGen.GenerateAttributeCertificateInfo();
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				attributeCertificateInfo,
				this.sigAlgId
			});
			IX509AttributeCertificate result;
			try
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerBitString(X509Utilities.GetSignatureForObject(this.sigOID, this.signatureAlgorithm, publicKey, random, attributeCertificateInfo))
				});
				result = new X509V2AttributeCertificate(AttributeCertificate.GetInstance(new DerSequence(asn1EncodableVector)));
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("constructed invalid certificate", e);
			}
			return result;
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00068A3C File Offset: 0x00067A3C
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x04000CE5 RID: 3301
		private readonly X509ExtensionsGenerator extGenerator = new X509ExtensionsGenerator();

		// Token: 0x04000CE6 RID: 3302
		private V2AttributeCertificateInfoGenerator acInfoGen;

		// Token: 0x04000CE7 RID: 3303
		private DerObjectIdentifier sigOID;

		// Token: 0x04000CE8 RID: 3304
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x04000CE9 RID: 3305
		private string signatureAlgorithm;
	}
}
