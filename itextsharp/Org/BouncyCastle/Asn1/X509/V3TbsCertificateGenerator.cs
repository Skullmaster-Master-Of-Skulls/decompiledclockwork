using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200013F RID: 319
	public class V3TbsCertificateGenerator
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x00040A61 File Offset: 0x0003FA61
		public void SetSerialNumber(DerInteger serialNumber)
		{
			this.serialNumber = serialNumber;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00040A6A File Offset: 0x0003FA6A
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00040A73 File Offset: 0x0003FA73
		public void SetIssuer(X509Name issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00040A7C File Offset: 0x0003FA7C
		public void SetStartDate(DerUtcTime startDate)
		{
			this.startDate = new Time(startDate);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00040A8A File Offset: 0x0003FA8A
		public void SetStartDate(Time startDate)
		{
			this.startDate = startDate;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00040A93 File Offset: 0x0003FA93
		public void SetEndDate(DerUtcTime endDate)
		{
			this.endDate = new Time(endDate);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00040AA1 File Offset: 0x0003FAA1
		public void SetEndDate(Time endDate)
		{
			this.endDate = endDate;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00040AAA File Offset: 0x0003FAAA
		public void SetSubject(X509Name subject)
		{
			this.subject = subject;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00040AB3 File Offset: 0x0003FAB3
		public void SetIssuerUniqueID(DerBitString uniqueID)
		{
			this.issuerUniqueID = uniqueID;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00040ABC File Offset: 0x0003FABC
		public void SetSubjectUniqueID(DerBitString uniqueID)
		{
			this.subjectUniqueID = uniqueID;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00040AC5 File Offset: 0x0003FAC5
		public void SetSubjectPublicKeyInfo(SubjectPublicKeyInfo pubKeyInfo)
		{
			this.subjectPublicKeyInfo = pubKeyInfo;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00040AD0 File Offset: 0x0003FAD0
		public void SetExtensions(X509Extensions extensions)
		{
			this.extensions = extensions;
			if (extensions != null)
			{
				X509Extension extension = extensions.GetExtension(X509Extensions.SubjectAlternativeName);
				if (extension != null && extension.IsCritical)
				{
					this.altNamePresentAndCritical = true;
				}
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00040B08 File Offset: 0x0003FB08
		public TbsCertificateStructure GenerateTbsCertificate()
		{
			if (this.serialNumber == null || this.signature == null || this.issuer == null || this.startDate == null || this.endDate == null || (this.subject == null && !this.altNamePresentAndCritical) || this.subjectPublicKeyInfo == null)
			{
				throw new InvalidOperationException("not all mandatory fields set in V3 TBScertificate generator");
			}
			DerSequence derSequence = new DerSequence(new Asn1Encodable[]
			{
				this.startDate,
				this.endDate
			});
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.serialNumber,
				this.signature,
				this.issuer,
				derSequence
			});
			if (this.subject != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.subject
				});
			}
			else
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					DerSequence.Empty
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.subjectPublicKeyInfo
			});
			if (this.issuerUniqueID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.issuerUniqueID)
				});
			}
			if (this.subjectUniqueID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, this.subjectUniqueID)
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(3, this.extensions)
				});
			}
			return new TbsCertificateStructure(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x0400090C RID: 2316
		internal DerTaggedObject version = new DerTaggedObject(0, new DerInteger(2));

		// Token: 0x0400090D RID: 2317
		internal DerInteger serialNumber;

		// Token: 0x0400090E RID: 2318
		internal AlgorithmIdentifier signature;

		// Token: 0x0400090F RID: 2319
		internal X509Name issuer;

		// Token: 0x04000910 RID: 2320
		internal Time startDate;

		// Token: 0x04000911 RID: 2321
		internal Time endDate;

		// Token: 0x04000912 RID: 2322
		internal X509Name subject;

		// Token: 0x04000913 RID: 2323
		internal SubjectPublicKeyInfo subjectPublicKeyInfo;

		// Token: 0x04000914 RID: 2324
		internal X509Extensions extensions;

		// Token: 0x04000915 RID: 2325
		private bool altNamePresentAndCritical;

		// Token: 0x04000916 RID: 2326
		private DerBitString issuerUniqueID;

		// Token: 0x04000917 RID: 2327
		private DerBitString subjectUniqueID;
	}
}
