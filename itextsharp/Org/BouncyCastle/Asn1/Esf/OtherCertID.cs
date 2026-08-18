using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x020004D6 RID: 1238
	public class OtherCertID : Asn1Encodable
	{
		// Token: 0x06002A2A RID: 10794 RVA: 0x00100544 File Offset: 0x000FF544
		public static OtherCertID GetInstance(object obj)
		{
			if (obj == null || obj is OtherCertID)
			{
				return (OtherCertID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherCertID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherCertID' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x00100598 File Offset: 0x000FF598
		private OtherCertID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.otherCertHash = OtherHash.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.issuerSerial = IssuerSerial.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x00100622 File Offset: 0x000FF622
		public OtherCertID(OtherHash otherCertHash) : this(otherCertHash, null)
		{
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x0010062C File Offset: 0x000FF62C
		public OtherCertID(OtherHash otherCertHash, IssuerSerial issuerSerial)
		{
			if (otherCertHash == null)
			{
				throw new ArgumentNullException("otherCertHash");
			}
			this.otherCertHash = otherCertHash;
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002A2E RID: 10798 RVA: 0x00100650 File Offset: 0x000FF650
		public OtherHash OtherCertHash
		{
			get
			{
				return this.otherCertHash;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x00100658 File Offset: 0x000FF658
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x00100660 File Offset: 0x000FF660
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.otherCertHash.ToAsn1Object()
			});
			if (this.issuerSerial != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerSerial.ToAsn1Object()
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001D6F RID: 7535
		private readonly OtherHash otherCertHash;

		// Token: 0x04001D70 RID: 7536
		private readonly IssuerSerial issuerSerial;
	}
}
