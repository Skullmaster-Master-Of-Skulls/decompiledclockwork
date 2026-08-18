using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x0200020C RID: 524
	public class Admissions : Asn1Encodable
	{
		// Token: 0x0600140A RID: 5130 RVA: 0x00072E0C File Offset: 0x00071E0C
		public static Admissions GetInstance(object obj)
		{
			if (obj == null || obj is Admissions)
			{
				return (Admissions)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Admissions((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00072E60 File Offset: 0x00071E60
		private Admissions(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable is Asn1TaggedObject)
			{
				switch (((Asn1TaggedObject)asn1Encodable).TagNo)
				{
				case 0:
					this.admissionAuthority = GeneralName.GetInstance((Asn1TaggedObject)asn1Encodable, true);
					break;
				case 1:
					this.namingAuthority = NamingAuthority.GetInstance((Asn1TaggedObject)asn1Encodable, true);
					break;
				default:
					throw new ArgumentException("Bad tag number: " + ((Asn1TaggedObject)asn1Encodable).TagNo);
				}
				enumerator.MoveNext();
				asn1Encodable = (Asn1Encodable)enumerator.Current;
			}
			if (asn1Encodable is Asn1TaggedObject)
			{
				int tagNo = ((Asn1TaggedObject)asn1Encodable).TagNo;
				if (tagNo != 1)
				{
					throw new ArgumentException("Bad tag number: " + ((Asn1TaggedObject)asn1Encodable).TagNo);
				}
				this.namingAuthority = NamingAuthority.GetInstance((Asn1TaggedObject)asn1Encodable, true);
				enumerator.MoveNext();
				asn1Encodable = (Asn1Encodable)enumerator.Current;
			}
			this.professionInfos = Asn1Sequence.GetInstance(asn1Encodable);
			if (enumerator.MoveNext())
			{
				throw new ArgumentException("Bad object encountered: " + enumerator.Current.GetType().Name);
			}
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00072FC3 File Offset: 0x00071FC3
		public Admissions(GeneralName admissionAuthority, NamingAuthority namingAuthority, ProfessionInfo[] professionInfos)
		{
			this.admissionAuthority = admissionAuthority;
			this.namingAuthority = namingAuthority;
			this.professionInfos = new DerSequence(professionInfos);
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00072FE5 File Offset: 0x00071FE5
		public virtual GeneralName AdmissionAuthority
		{
			get
			{
				return this.admissionAuthority;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00072FED File Offset: 0x00071FED
		public virtual NamingAuthority NamingAuthority
		{
			get
			{
				return this.namingAuthority;
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00072FF8 File Offset: 0x00071FF8
		public ProfessionInfo[] GetProfessionInfos()
		{
			ProfessionInfo[] array = new ProfessionInfo[this.professionInfos.Count];
			int num = 0;
			foreach (object obj in this.professionInfos)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				array[num++] = ProfessionInfo.GetInstance(obj2);
			}
			return array;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00073070 File Offset: 0x00072070
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.admissionAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.admissionAuthority)
				});
			}
			if (this.namingAuthority != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.namingAuthority)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.professionInfos
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000DD7 RID: 3543
		private readonly GeneralName admissionAuthority;

		// Token: 0x04000DD8 RID: 3544
		private readonly NamingAuthority namingAuthority;

		// Token: 0x04000DD9 RID: 3545
		private readonly Asn1Sequence professionInfos;
	}
}
