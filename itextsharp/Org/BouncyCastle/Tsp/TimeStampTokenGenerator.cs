using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020004FC RID: 1276
	public class TimeStampTokenGenerator
	{
		// Token: 0x06002B9C RID: 11164 RVA: 0x001084CA File Offset: 0x001074CA
		public TimeStampTokenGenerator(AsymmetricKeyParameter key, X509Certificate cert, string digestOID, string tsaPolicyOID) : this(key, cert, digestOID, tsaPolicyOID, null, null)
		{
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x001084DC File Offset: 0x001074DC
		public TimeStampTokenGenerator(AsymmetricKeyParameter key, X509Certificate cert, string digestOID, string tsaPolicyOID, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.key = key;
			this.cert = cert;
			this.digestOID = digestOID;
			this.tsaPolicyOID = tsaPolicyOID;
			this.unsignedAttr = unsignedAttr;
			TspUtil.ValidateCertificate(cert);
			Hashtable hashtable;
			if (signedAttr != null)
			{
				hashtable = signedAttr.ToHashtable();
			}
			else
			{
				hashtable = new Hashtable();
			}
			try
			{
				byte[] hash = DigestUtilities.CalculateDigest("SHA-1", cert.GetEncoded());
				EssCertID essCertID = new EssCertID(hash);
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = new Org.BouncyCastle.Asn1.Cms.Attribute(PkcsObjectIdentifiers.IdAASigningCertificate, new DerSet(new SigningCertificate(essCertID)));
				hashtable[attribute.AttrType] = attribute;
			}
			catch (CertificateEncodingException e)
			{
				throw new TspException("Exception processing certificate.", e);
			}
			catch (SecurityUtilityException e2)
			{
				throw new TspException("Can't find a SHA-1 implementation.", e2);
			}
			this.signedAttr = new Org.BouncyCastle.Asn1.Cms.AttributeTable(hashtable);
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x001085C8 File Offset: 0x001075C8
		public void SetCertificates(IX509Store certificates)
		{
			this.x509Certs = certificates;
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x001085D1 File Offset: 0x001075D1
		public void SetCrls(IX509Store crls)
		{
			this.x509Crls = crls;
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x001085DA File Offset: 0x001075DA
		public void SetAccuracySeconds(int accuracySeconds)
		{
			this.accuracySeconds = accuracySeconds;
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x001085E3 File Offset: 0x001075E3
		public void SetAccuracyMillis(int accuracyMillis)
		{
			this.accuracyMillis = accuracyMillis;
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x001085EC File Offset: 0x001075EC
		public void SetAccuracyMicros(int accuracyMicros)
		{
			this.accuracyMicros = accuracyMicros;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x001085F5 File Offset: 0x001075F5
		public void SetOrdering(bool ordering)
		{
			this.ordering = ordering;
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x001085FE File Offset: 0x001075FE
		public void SetTsa(GeneralName tsa)
		{
			this.tsa = tsa;
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x00108608 File Offset: 0x00107608
		public TimeStampToken Generate(TimeStampRequest request, BigInteger serialNumber, DateTime genTime)
		{
			DerObjectIdentifier objectID = new DerObjectIdentifier(request.MessageImprintAlgOid);
			AlgorithmIdentifier hashAlgorithm = new AlgorithmIdentifier(objectID, DerNull.Instance);
			MessageImprint messageImprint = new MessageImprint(hashAlgorithm, request.GetMessageImprintDigest());
			Accuracy accuracy = null;
			if (this.accuracySeconds > 0 || this.accuracyMillis > 0 || this.accuracyMicros > 0)
			{
				DerInteger seconds = null;
				if (this.accuracySeconds > 0)
				{
					seconds = new DerInteger(this.accuracySeconds);
				}
				DerInteger millis = null;
				if (this.accuracyMillis > 0)
				{
					millis = new DerInteger(this.accuracyMillis);
				}
				DerInteger micros = null;
				if (this.accuracyMicros > 0)
				{
					micros = new DerInteger(this.accuracyMicros);
				}
				accuracy = new Accuracy(seconds, millis, micros);
			}
			DerBoolean derBoolean = null;
			if (this.ordering)
			{
				derBoolean = DerBoolean.GetInstance(this.ordering);
			}
			DerInteger nonce = null;
			if (request.Nonce != null)
			{
				nonce = new DerInteger(request.Nonce);
			}
			DerObjectIdentifier tsaPolicyId = new DerObjectIdentifier(this.tsaPolicyOID);
			if (request.ReqPolicy != null)
			{
				tsaPolicyId = new DerObjectIdentifier(request.ReqPolicy);
			}
			TstInfo tstInfo = new TstInfo(tsaPolicyId, messageImprint, new DerInteger(serialNumber), new DerGeneralizedTime(genTime), accuracy, derBoolean, nonce, this.tsa, request.Extensions);
			TimeStampToken result;
			try
			{
				CmsSignedDataGenerator cmsSignedDataGenerator = new CmsSignedDataGenerator();
				byte[] derEncoded = tstInfo.GetDerEncoded();
				if (request.CertReq)
				{
					cmsSignedDataGenerator.AddCertificates(this.x509Certs);
				}
				cmsSignedDataGenerator.AddCrls(this.x509Crls);
				cmsSignedDataGenerator.AddSigner(this.key, this.cert, this.digestOID, this.signedAttr, this.unsignedAttr);
				CmsSignedData signedData = cmsSignedDataGenerator.Generate(PkcsObjectIdentifiers.IdCTTstInfo.Id, new CmsProcessableByteArray(derEncoded), true);
				result = new TimeStampToken(signedData);
			}
			catch (CmsException e)
			{
				throw new TspException("Error generating time-stamp token", e);
			}
			catch (IOException e2)
			{
				throw new TspException("Exception encoding info", e2);
			}
			catch (X509StoreException e3)
			{
				throw new TspException("Exception handling CertStore", e3);
			}
			return result;
		}

		// Token: 0x04001E30 RID: 7728
		private int accuracySeconds = -1;

		// Token: 0x04001E31 RID: 7729
		private int accuracyMillis = -1;

		// Token: 0x04001E32 RID: 7730
		private int accuracyMicros = -1;

		// Token: 0x04001E33 RID: 7731
		private bool ordering;

		// Token: 0x04001E34 RID: 7732
		private GeneralName tsa;

		// Token: 0x04001E35 RID: 7733
		private string tsaPolicyOID;

		// Token: 0x04001E36 RID: 7734
		private AsymmetricKeyParameter key;

		// Token: 0x04001E37 RID: 7735
		private X509Certificate cert;

		// Token: 0x04001E38 RID: 7736
		private string digestOID;

		// Token: 0x04001E39 RID: 7737
		private Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr;

		// Token: 0x04001E3A RID: 7738
		private Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr;

		// Token: 0x04001E3B RID: 7739
		private IX509Store x509Certs;

		// Token: 0x04001E3C RID: 7740
		private IX509Store x509Crls;
	}
}
