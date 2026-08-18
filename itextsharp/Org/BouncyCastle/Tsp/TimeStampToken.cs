using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Ess;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020003CB RID: 971
	public class TimeStampToken
	{
		// Token: 0x060021C6 RID: 8646 RVA: 0x000CCF80 File Offset: 0x000CBF80
		public TimeStampToken(Org.BouncyCastle.Asn1.Cms.ContentInfo contentInfo) : this(new CmsSignedData(contentInfo))
		{
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000CCF90 File Offset: 0x000CBF90
		public TimeStampToken(CmsSignedData signedData)
		{
			this.tsToken = signedData;
			if (!this.tsToken.SignedContentType.Equals(PkcsObjectIdentifiers.IdCTTstInfo))
			{
				throw new TspValidationException("ContentInfo object not for a time stamp.");
			}
			ICollection signers = this.tsToken.GetSignerInfos().GetSigners();
			if (signers.Count != 1)
			{
				throw new ArgumentException("Time-stamp token signed by " + signers.Count + " signers, but it must contain just the TSA signature.");
			}
			IEnumerator enumerator = signers.GetEnumerator();
			enumerator.MoveNext();
			this.tsaSignerInfo = (SignerInformation)enumerator.Current;
			try
			{
				CmsProcessable signedContent = this.tsToken.SignedContent;
				MemoryStream memoryStream = new MemoryStream();
				signedContent.Write(memoryStream);
				this.tstInfo = new TimeStampTokenInfo(TstInfo.GetInstance(Asn1Object.FromByteArray(memoryStream.ToArray())));
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = this.tsaSignerInfo.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificate];
				if (attribute != null)
				{
					SigningCertificate instance = SigningCertificate.GetInstance(attribute.AttrValues[0]);
					this.certID = new TimeStampToken.CertID(EssCertID.GetInstance(instance.GetCerts()[0]));
				}
				else
				{
					attribute = this.tsaSignerInfo.SignedAttributes[PkcsObjectIdentifiers.IdAASigningCertificateV2];
					if (attribute == null)
					{
						throw new TspValidationException("no signing certificate attribute found, time stamp invalid.");
					}
					SigningCertificateV2 instance2 = SigningCertificateV2.GetInstance(attribute.AttrValues[0]);
					this.certID = new TimeStampToken.CertID(EssCertIDv2.GetInstance(instance2.GetCerts()[0]));
				}
			}
			catch (CmsException ex)
			{
				throw new TspException(ex.Message, ex.InnerException);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x000CD120 File Offset: 0x000CC120
		public TimeStampTokenInfo TimeStampInfo
		{
			get
			{
				return this.tstInfo;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x000CD128 File Offset: 0x000CC128
		public SignerID SignerID
		{
			get
			{
				return this.tsaSignerInfo.SignerID;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x000CD135 File Offset: 0x000CC135
		public Org.BouncyCastle.Asn1.Cms.AttributeTable SignedAttributes
		{
			get
			{
				return this.tsaSignerInfo.SignedAttributes;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x000CD142 File Offset: 0x000CC142
		public Org.BouncyCastle.Asn1.Cms.AttributeTable UnsignedAttributes
		{
			get
			{
				return this.tsaSignerInfo.UnsignedAttributes;
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000CD14F File Offset: 0x000CC14F
		public IX509Store GetCertificates(string type)
		{
			return this.tsToken.GetCertificates(type);
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000CD15D File Offset: 0x000CC15D
		public IX509Store GetCrls(string type)
		{
			return this.tsToken.GetCrls(type);
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000CD16C File Offset: 0x000CC16C
		public void Validate(X509Certificate cert)
		{
			try
			{
				byte[] b = DigestUtilities.CalculateDigest(this.certID.GetHashAlgorithm(), cert.GetEncoded());
				if (!Arrays.ConstantTimeAreEqual(this.certID.GetCertHash(), b))
				{
					throw new TspValidationException("certificate hash does not match certID hash.");
				}
				if (this.certID.IssuerSerial != null)
				{
					if (!this.certID.IssuerSerial.Serial.Value.Equals(cert.SerialNumber))
					{
						throw new TspValidationException("certificate serial number does not match certID for signature.");
					}
					GeneralName[] names = this.certID.IssuerSerial.Issuer.GetNames();
					X509Name issuerX509Principal = PrincipalUtilities.GetIssuerX509Principal(cert);
					bool flag = false;
					for (int num = 0; num != names.Length; num++)
					{
						if (names[num].TagNo == 4 && X509Name.GetInstance(names[num].Name).Equivalent(issuerX509Principal))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						throw new TspValidationException("certificate name does not match certID for signature. ");
					}
				}
				TspUtil.ValidateCertificate(cert);
				cert.CheckValidity(this.tstInfo.GenTime);
				if (!this.tsaSignerInfo.Verify(cert))
				{
					throw new TspValidationException("signature not created by certificate.");
				}
			}
			catch (CmsException ex)
			{
				if (ex.InnerException != null)
				{
					throw new TspException(ex.Message, ex.InnerException);
				}
				throw new TspException("CMS exception: " + ex, ex);
			}
			catch (CertificateEncodingException ex2)
			{
				throw new TspException("problem processing certificate: " + ex2, ex2);
			}
			catch (SecurityUtilityException ex3)
			{
				throw new TspException("cannot find algorithm: " + ex3.Message, ex3);
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000CD334 File Offset: 0x000CC334
		public CmsSignedData ToCmsSignedData()
		{
			return this.tsToken;
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000CD33C File Offset: 0x000CC33C
		public byte[] GetEncoded()
		{
			return this.tsToken.GetEncoded();
		}

		// Token: 0x04001746 RID: 5958
		private readonly CmsSignedData tsToken;

		// Token: 0x04001747 RID: 5959
		private readonly SignerInformation tsaSignerInfo;

		// Token: 0x04001748 RID: 5960
		private readonly TimeStampTokenInfo tstInfo;

		// Token: 0x04001749 RID: 5961
		private readonly TimeStampToken.CertID certID;

		// Token: 0x020003CC RID: 972
		private class CertID
		{
			// Token: 0x060021D1 RID: 8657 RVA: 0x000CD349 File Offset: 0x000CC349
			internal CertID(EssCertID certID)
			{
				this.certID = certID;
				this.certIDv2 = null;
			}

			// Token: 0x060021D2 RID: 8658 RVA: 0x000CD35F File Offset: 0x000CC35F
			internal CertID(EssCertIDv2 certID)
			{
				this.certIDv2 = certID;
				this.certID = null;
			}

			// Token: 0x060021D3 RID: 8659 RVA: 0x000CD378 File Offset: 0x000CC378
			public string GetHashAlgorithm()
			{
				if (this.certID != null)
				{
					return "SHA-1";
				}
				if (NistObjectIdentifiers.IdSha256.Equals(this.certIDv2.HashAlgorithm.ObjectID))
				{
					return "SHA-256";
				}
				return this.certIDv2.HashAlgorithm.ObjectID.Id;
			}

			// Token: 0x060021D4 RID: 8660 RVA: 0x000CD3CA File Offset: 0x000CC3CA
			public byte[] GetCertHash()
			{
				if (this.certID == null)
				{
					return this.certIDv2.GetCertHash();
				}
				return this.certID.GetCertHash();
			}

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x060021D5 RID: 8661 RVA: 0x000CD3EB File Offset: 0x000CC3EB
			public IssuerSerial IssuerSerial
			{
				get
				{
					if (this.certID == null)
					{
						return this.certIDv2.IssuerSerial;
					}
					return this.certID.IssuerSerial;
				}
			}

			// Token: 0x0400174A RID: 5962
			private EssCertID certID;

			// Token: 0x0400174B RID: 5963
			private EssCertIDv2 certIDv2;
		}
	}
}
