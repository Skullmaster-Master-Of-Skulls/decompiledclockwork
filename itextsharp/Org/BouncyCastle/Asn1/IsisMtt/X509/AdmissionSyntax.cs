using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x020005BB RID: 1467
	public class AdmissionSyntax : Asn1Encodable
	{
		// Token: 0x06003272 RID: 12914 RVA: 0x00138F94 File Offset: 0x00137F94
		public static AdmissionSyntax GetInstance(object obj)
		{
			if (obj == null || obj is AdmissionSyntax)
			{
				return (AdmissionSyntax)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AdmissionSyntax((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x00138FE8 File Offset: 0x00137FE8
		private AdmissionSyntax(Asn1Sequence seq)
		{
			switch (seq.Count)
			{
			case 1:
				this.contentsOfAdmissions = Asn1Sequence.GetInstance(seq[0]);
				return;
			case 2:
				this.admissionAuthority = GeneralName.GetInstance(seq[0]);
				this.contentsOfAdmissions = Asn1Sequence.GetInstance(seq[1]);
				return;
			default:
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x00139066 File Offset: 0x00138066
		public AdmissionSyntax(GeneralName admissionAuthority, Asn1Sequence contentsOfAdmissions)
		{
			this.admissionAuthority = admissionAuthority;
			this.contentsOfAdmissions = contentsOfAdmissions;
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x0013907C File Offset: 0x0013807C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.admissionAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.admissionAuthority
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.contentsOfAdmissions
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x001390D1 File Offset: 0x001380D1
		public virtual GeneralName AdmissionAuthority
		{
			get
			{
				return this.admissionAuthority;
			}
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x001390DC File Offset: 0x001380DC
		public virtual Admissions[] GetContentsOfAdmissions()
		{
			Admissions[] array = new Admissions[this.contentsOfAdmissions.Count];
			for (int i = 0; i < this.contentsOfAdmissions.Count; i++)
			{
				array[i] = Admissions.GetInstance(this.contentsOfAdmissions[i]);
			}
			return array;
		}

		// Token: 0x04002285 RID: 8837
		private readonly GeneralName admissionAuthority;

		// Token: 0x04002286 RID: 8838
		private readonly Asn1Sequence contentsOfAdmissions;
	}
}
