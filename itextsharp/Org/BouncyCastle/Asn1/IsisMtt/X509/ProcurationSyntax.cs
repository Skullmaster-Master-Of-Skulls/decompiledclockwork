using System;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x020002BA RID: 698
	public class ProcurationSyntax : Asn1Encodable
	{
		// Token: 0x06001A4E RID: 6734 RVA: 0x0009BA38 File Offset: 0x0009AA38
		public static ProcurationSyntax GetInstance(object obj)
		{
			if (obj == null || obj is ProcurationSyntax)
			{
				return (ProcurationSyntax)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ProcurationSyntax((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0009BA8C File Offset: 0x0009AA8C
		private ProcurationSyntax(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			foreach (object obj in seq)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(obj);
				switch (instance.TagNo)
				{
				case 1:
					this.country = DerPrintableString.GetInstance(instance, true).GetString();
					break;
				case 2:
					this.typeOfSubstitution = DirectoryString.GetInstance(instance, true);
					break;
				case 3:
				{
					Asn1Object @object = instance.GetObject();
					if (@object is Asn1TaggedObject)
					{
						this.thirdPerson = GeneralName.GetInstance(@object);
					}
					else
					{
						this.certRef = IssuerSerial.GetInstance(@object);
					}
					break;
				}
				default:
					throw new ArgumentException("Bad tag number: " + instance.TagNo);
				}
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0009BB75 File Offset: 0x0009AB75
		public ProcurationSyntax(string country, DirectoryString typeOfSubstitution, IssuerSerial certRef)
		{
			this.country = country;
			this.typeOfSubstitution = typeOfSubstitution;
			this.thirdPerson = null;
			this.certRef = certRef;
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0009BB99 File Offset: 0x0009AB99
		public ProcurationSyntax(string country, DirectoryString typeOfSubstitution, GeneralName thirdPerson)
		{
			this.country = country;
			this.typeOfSubstitution = typeOfSubstitution;
			this.thirdPerson = thirdPerson;
			this.certRef = null;
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0009BBBD File Offset: 0x0009ABBD
		public virtual string Country
		{
			get
			{
				return this.country;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0009BBC5 File Offset: 0x0009ABC5
		public virtual DirectoryString TypeOfSubstitution
		{
			get
			{
				return this.typeOfSubstitution;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0009BBCD File Offset: 0x0009ABCD
		public virtual GeneralName ThirdPerson
		{
			get
			{
				return this.thirdPerson;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x0009BBD5 File Offset: 0x0009ABD5
		public virtual IssuerSerial CertRef
		{
			get
			{
				return this.certRef;
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0009BBE0 File Offset: 0x0009ABE0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.country != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, new DerPrintableString(this.country, true))
				});
			}
			if (this.typeOfSubstitution != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.typeOfSubstitution)
				});
			}
			if (this.thirdPerson != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 3, this.thirdPerson)
				});
			}
			else
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 3, this.certRef)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400119B RID: 4507
		private readonly string country;

		// Token: 0x0400119C RID: 4508
		private readonly DirectoryString typeOfSubstitution;

		// Token: 0x0400119D RID: 4509
		private readonly GeneralName thirdPerson;

		// Token: 0x0400119E RID: 4510
		private readonly IssuerSerial certRef;
	}
}
