using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020002B3 RID: 691
	public class V2AttributeCertificateInfoGenerator
	{
		// Token: 0x06001A1F RID: 6687 RVA: 0x0009AE4D File Offset: 0x00099E4D
		public V2AttributeCertificateInfoGenerator()
		{
			this.version = new DerInteger(1);
			this.attributes = new Asn1EncodableVector(new Asn1Encodable[0]);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0009AE72 File Offset: 0x00099E72
		public void SetHolder(Holder holder)
		{
			this.holder = holder;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0009AE7C File Offset: 0x00099E7C
		public void AddAttribute(string oid, Asn1Encodable value)
		{
			this.attributes.Add(new Asn1Encodable[]
			{
				new AttributeX509(new DerObjectIdentifier(oid), new DerSet(value))
			});
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0009AEB0 File Offset: 0x00099EB0
		public void AddAttribute(AttributeX509 attribute)
		{
			this.attributes.Add(new Asn1Encodable[]
			{
				attribute
			});
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0009AED4 File Offset: 0x00099ED4
		public void SetSerialNumber(DerInteger serialNumber)
		{
			this.serialNumber = serialNumber;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0009AEDD File Offset: 0x00099EDD
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0009AEE6 File Offset: 0x00099EE6
		public void SetIssuer(AttCertIssuer issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0009AEEF File Offset: 0x00099EEF
		public void SetStartDate(DerGeneralizedTime startDate)
		{
			this.startDate = startDate;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0009AEF8 File Offset: 0x00099EF8
		public void SetEndDate(DerGeneralizedTime endDate)
		{
			this.endDate = endDate;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0009AF01 File Offset: 0x00099F01
		public void SetIssuerUniqueID(DerBitString issuerUniqueID)
		{
			this.issuerUniqueID = issuerUniqueID;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0009AF0A File Offset: 0x00099F0A
		public void SetExtensions(X509Extensions extensions)
		{
			this.extensions = extensions;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0009AF14 File Offset: 0x00099F14
		public AttributeCertificateInfo GenerateAttributeCertificateInfo()
		{
			if (this.serialNumber == null || this.signature == null || this.issuer == null || this.startDate == null || this.endDate == null || this.holder == null || this.attributes == null)
			{
				throw new InvalidOperationException("not all mandatory fields set in V2 AttributeCertificateInfo generator");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.holder,
				this.issuer,
				this.signature,
				this.serialNumber
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new AttCertValidityPeriod(this.startDate, this.endDate)
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerSequence(this.attributes)
			});
			if (this.issuerUniqueID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerUniqueID
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.extensions
				});
			}
			return AttributeCertificateInfo.GetInstance(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x04001166 RID: 4454
		internal DerInteger version;

		// Token: 0x04001167 RID: 4455
		internal Holder holder;

		// Token: 0x04001168 RID: 4456
		internal AttCertIssuer issuer;

		// Token: 0x04001169 RID: 4457
		internal AlgorithmIdentifier signature;

		// Token: 0x0400116A RID: 4458
		internal DerInteger serialNumber;

		// Token: 0x0400116B RID: 4459
		internal Asn1EncodableVector attributes;

		// Token: 0x0400116C RID: 4460
		internal DerBitString issuerUniqueID;

		// Token: 0x0400116D RID: 4461
		internal X509Extensions extensions;

		// Token: 0x0400116E RID: 4462
		internal DerGeneralizedTime startDate;

		// Token: 0x0400116F RID: 4463
		internal DerGeneralizedTime endDate;
	}
}
