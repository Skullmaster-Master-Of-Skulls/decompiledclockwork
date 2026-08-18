using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509.SigI
{
	// Token: 0x020002B4 RID: 692
	public class PersonalData : Asn1Encodable
	{
		// Token: 0x06001A2B RID: 6699 RVA: 0x0009B02C File Offset: 0x0009A02C
		public static PersonalData GetInstance(object obj)
		{
			if (obj == null || obj is PersonalData)
			{
				return (PersonalData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PersonalData((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0009B080 File Offset: 0x0009A080
		private PersonalData(Asn1Sequence seq)
		{
			if (seq.Count < 1)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.nameOrPseudonym = NameOrPseudonym.GetInstance(enumerator.Current);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(obj);
				switch (instance.TagNo)
				{
				case 0:
					this.nameDistinguisher = DerInteger.GetInstance(instance, false).Value;
					break;
				case 1:
					this.dateOfBirth = DerGeneralizedTime.GetInstance(instance, false);
					break;
				case 2:
					this.placeOfBirth = DirectoryString.GetInstance(instance, true);
					break;
				case 3:
					this.gender = DerPrintableString.GetInstance(instance, false).GetString();
					break;
				case 4:
					this.postalAddress = DirectoryString.GetInstance(instance, true);
					break;
				default:
					throw new ArgumentException("Bad tag number: " + instance.TagNo);
				}
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0009B187 File Offset: 0x0009A187
		public PersonalData(NameOrPseudonym nameOrPseudonym, BigInteger nameDistinguisher, DerGeneralizedTime dateOfBirth, DirectoryString placeOfBirth, string gender, DirectoryString postalAddress)
		{
			this.nameOrPseudonym = nameOrPseudonym;
			this.dateOfBirth = dateOfBirth;
			this.gender = gender;
			this.nameDistinguisher = nameDistinguisher;
			this.postalAddress = postalAddress;
			this.placeOfBirth = placeOfBirth;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x0009B1BC File Offset: 0x0009A1BC
		public NameOrPseudonym NameOrPseudonym
		{
			get
			{
				return this.nameOrPseudonym;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x0009B1C4 File Offset: 0x0009A1C4
		public BigInteger NameDistinguisher
		{
			get
			{
				return this.nameDistinguisher;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x0009B1CC File Offset: 0x0009A1CC
		public DerGeneralizedTime DateOfBirth
		{
			get
			{
				return this.dateOfBirth;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x0009B1D4 File Offset: 0x0009A1D4
		public DirectoryString PlaceOfBirth
		{
			get
			{
				return this.placeOfBirth;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x0009B1DC File Offset: 0x0009A1DC
		public string Gender
		{
			get
			{
				return this.gender;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x0009B1E4 File Offset: 0x0009A1E4
		public DirectoryString PostalAddress
		{
			get
			{
				return this.postalAddress;
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0009B1EC File Offset: 0x0009A1EC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.nameOrPseudonym
			});
			if (this.nameDistinguisher != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, new DerInteger(this.nameDistinguisher))
				});
			}
			if (this.dateOfBirth != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.dateOfBirth)
				});
			}
			if (this.placeOfBirth != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.placeOfBirth)
				});
			}
			if (this.gender != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 3, new DerPrintableString(this.gender, true))
				});
			}
			if (this.postalAddress != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 4, this.postalAddress)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001170 RID: 4464
		private readonly NameOrPseudonym nameOrPseudonym;

		// Token: 0x04001171 RID: 4465
		private readonly BigInteger nameDistinguisher;

		// Token: 0x04001172 RID: 4466
		private readonly DerGeneralizedTime dateOfBirth;

		// Token: 0x04001173 RID: 4467
		private readonly DirectoryString placeOfBirth;

		// Token: 0x04001174 RID: 4468
		private readonly string gender;

		// Token: 0x04001175 RID: 4469
		private readonly DirectoryString postalAddress;
	}
}
