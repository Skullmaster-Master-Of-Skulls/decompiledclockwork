using System;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000263 RID: 611
	public class CrlValidatedID : Asn1Encodable
	{
		// Token: 0x06001716 RID: 5910 RVA: 0x00085360 File Offset: 0x00084360
		public static CrlValidatedID GetInstance(object obj)
		{
			if (obj == null || obj is CrlValidatedID)
			{
				return (CrlValidatedID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlValidatedID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlValidatedID' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000853B4 File Offset: 0x000843B4
		private CrlValidatedID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.crlHash = OtherHash.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.crlIdentifier = CrlIdentifier.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0008543E File Offset: 0x0008443E
		public CrlValidatedID(OtherHash crlHash) : this(crlHash, null)
		{
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00085448 File Offset: 0x00084448
		public CrlValidatedID(OtherHash crlHash, CrlIdentifier crlIdentifier)
		{
			if (crlHash == null)
			{
				throw new ArgumentNullException("crlHash");
			}
			this.crlHash = crlHash;
			this.crlIdentifier = crlIdentifier;
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0008546C File Offset: 0x0008446C
		public OtherHash CrlHash
		{
			get
			{
				return this.crlHash;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x00085474 File Offset: 0x00084474
		public CrlIdentifier CrlIdentifier
		{
			get
			{
				return this.crlIdentifier;
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0008547C File Offset: 0x0008447C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.crlHash.ToAsn1Object()
			});
			if (this.crlIdentifier != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.crlIdentifier.ToAsn1Object()
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000FCD RID: 4045
		private readonly OtherHash crlHash;

		// Token: 0x04000FCE RID: 4046
		private readonly CrlIdentifier crlIdentifier;
	}
}
