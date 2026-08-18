using System;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000260 RID: 608
	public class SignerLocation : Asn1Encodable
	{
		// Token: 0x06001702 RID: 5890 RVA: 0x00084EA0 File Offset: 0x00083EA0
		public SignerLocation(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.countryName = DerUtf8String.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.localityName = DerUtf8String.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
				{
					bool explicitly = asn1TaggedObject.IsExplicit();
					this.postalAddress = Asn1Sequence.GetInstance(asn1TaggedObject, explicitly);
					if (this.postalAddress != null && this.postalAddress.Count > 6)
					{
						throw new ArgumentException("postal address must contain less than 6 strings");
					}
					break;
				}
				default:
					throw new ArgumentException("illegal tag");
				}
			}
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00084F78 File Offset: 0x00083F78
		public SignerLocation(DerUtf8String countryName, DerUtf8String localityName, Asn1Sequence postalAddress)
		{
			if (postalAddress != null && postalAddress.Count > 6)
			{
				throw new ArgumentException("postal address must contain less than 6 strings");
			}
			if (countryName != null)
			{
				this.countryName = DerUtf8String.GetInstance(countryName.ToAsn1Object());
			}
			if (localityName != null)
			{
				this.localityName = DerUtf8String.GetInstance(localityName.ToAsn1Object());
			}
			if (postalAddress != null)
			{
				this.postalAddress = (Asn1Sequence)postalAddress.ToAsn1Object();
			}
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00084FDE File Offset: 0x00083FDE
		public static SignerLocation GetInstance(object obj)
		{
			if (obj == null || obj is SignerLocation)
			{
				return (SignerLocation)obj;
			}
			return new SignerLocation(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00084FFD File Offset: 0x00083FFD
		public DerUtf8String CountryName
		{
			get
			{
				return this.countryName;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00085005 File Offset: 0x00084005
		public DerUtf8String LocalityName
		{
			get
			{
				return this.localityName;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0008500D File Offset: 0x0008400D
		public Asn1Sequence PostalAddress
		{
			get
			{
				return this.postalAddress;
			}
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00085018 File Offset: 0x00084018
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.countryName != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.countryName)
				});
			}
			if (this.localityName != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.localityName)
				});
			}
			if (this.postalAddress != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.postalAddress)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000FC6 RID: 4038
		private DerUtf8String countryName;

		// Token: 0x04000FC7 RID: 4039
		private DerUtf8String localityName;

		// Token: 0x04000FC8 RID: 4040
		private Asn1Sequence postalAddress;
	}
}
