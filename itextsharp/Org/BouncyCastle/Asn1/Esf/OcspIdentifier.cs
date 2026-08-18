using System;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200044E RID: 1102
	public class OcspIdentifier : Asn1Encodable
	{
		// Token: 0x06002541 RID: 9537 RVA: 0x000E1D88 File Offset: 0x000E0D88
		public static OcspIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is OcspIdentifier)
			{
				return (OcspIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OcspIdentifier' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000E1DDC File Offset: 0x000E0DDC
		private OcspIdentifier(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.ocspResponderID = ResponderID.GetInstance(seq[0].ToAsn1Object());
			this.producedAt = (DerGeneralizedTime)seq[1].ToAsn1Object();
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000E1E54 File Offset: 0x000E0E54
		public OcspIdentifier(ResponderID ocspResponderID, DateTime producedAt)
		{
			if (ocspResponderID == null)
			{
				throw new ArgumentNullException();
			}
			this.ocspResponderID = ocspResponderID;
			this.producedAt = new DerGeneralizedTime(producedAt);
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x000E1E78 File Offset: 0x000E0E78
		public ResponderID OcspResponderID
		{
			get
			{
				return this.ocspResponderID;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x000E1E80 File Offset: 0x000E0E80
		public DateTime ProducedAt
		{
			get
			{
				return this.producedAt.ToDateTime();
			}
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000E1E90 File Offset: 0x000E0E90
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.ocspResponderID,
				this.producedAt
			});
		}

		// Token: 0x04001A1D RID: 6685
		private readonly ResponderID ocspResponderID;

		// Token: 0x04001A1E RID: 6686
		private readonly DerGeneralizedTime producedAt;
	}
}
