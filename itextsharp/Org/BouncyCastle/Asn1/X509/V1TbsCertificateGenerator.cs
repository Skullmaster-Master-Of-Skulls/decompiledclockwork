using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000257 RID: 599
	public class V1TbsCertificateGenerator
	{
		// Token: 0x060016BF RID: 5823 RVA: 0x000837FD File Offset: 0x000827FD
		public void SetSerialNumber(DerInteger serialNumber)
		{
			this.serialNumber = serialNumber;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00083806 File Offset: 0x00082806
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0008380F File Offset: 0x0008280F
		public void SetIssuer(X509Name issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00083818 File Offset: 0x00082818
		public void SetStartDate(Time startDate)
		{
			this.startDate = startDate;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00083821 File Offset: 0x00082821
		public void SetStartDate(DerUtcTime startDate)
		{
			this.startDate = new Time(startDate);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0008382F File Offset: 0x0008282F
		public void SetEndDate(Time endDate)
		{
			this.endDate = endDate;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00083838 File Offset: 0x00082838
		public void SetEndDate(DerUtcTime endDate)
		{
			this.endDate = new Time(endDate);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00083846 File Offset: 0x00082846
		public void SetSubject(X509Name subject)
		{
			this.subject = subject;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0008384F File Offset: 0x0008284F
		public void SetSubjectPublicKeyInfo(SubjectPublicKeyInfo pubKeyInfo)
		{
			this.subjectPublicKeyInfo = pubKeyInfo;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00083858 File Offset: 0x00082858
		public TbsCertificateStructure GenerateTbsCertificate()
		{
			if (this.serialNumber == null || this.signature == null || this.issuer == null || this.startDate == null || this.endDate == null || this.subject == null || this.subjectPublicKeyInfo == null)
			{
				throw new InvalidOperationException("not all mandatory fields set in V1 TBScertificate generator");
			}
			return new TbsCertificateStructure(new DerSequence(new Asn1Encodable[]
			{
				this.serialNumber,
				this.signature,
				this.issuer,
				new DerSequence(new Asn1Encodable[]
				{
					this.startDate,
					this.endDate
				}),
				this.subject,
				this.subjectPublicKeyInfo
			}));
		}

		// Token: 0x04000FA3 RID: 4003
		internal DerTaggedObject version = new DerTaggedObject(0, new DerInteger(0));

		// Token: 0x04000FA4 RID: 4004
		internal DerInteger serialNumber;

		// Token: 0x04000FA5 RID: 4005
		internal AlgorithmIdentifier signature;

		// Token: 0x04000FA6 RID: 4006
		internal X509Name issuer;

		// Token: 0x04000FA7 RID: 4007
		internal Time startDate;

		// Token: 0x04000FA8 RID: 4008
		internal Time endDate;

		// Token: 0x04000FA9 RID: 4009
		internal X509Name subject;

		// Token: 0x04000FAA RID: 4010
		internal SubjectPublicKeyInfo subjectPublicKeyInfo;
	}
}
