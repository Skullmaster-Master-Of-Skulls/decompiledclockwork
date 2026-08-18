using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000317 RID: 791
	public class OtherRecipientInfo : Asn1Encodable
	{
		// Token: 0x06001CC8 RID: 7368 RVA: 0x000ABBD4 File Offset: 0x000AABD4
		public OtherRecipientInfo(DerObjectIdentifier oriType, Asn1Encodable oriValue)
		{
			this.oriType = oriType;
			this.oriValue = oriValue;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x000ABBEA File Offset: 0x000AABEA
		public OtherRecipientInfo(Asn1Sequence seq)
		{
			this.oriType = DerObjectIdentifier.GetInstance(seq[0]);
			this.oriValue = seq[1];
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x000ABC11 File Offset: 0x000AAC11
		public static OtherRecipientInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OtherRecipientInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x000ABC20 File Offset: 0x000AAC20
		public static OtherRecipientInfo GetInstance(object obj)
		{
			if (obj == null || obj is OtherRecipientInfo)
			{
				return (OtherRecipientInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherRecipientInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid OtherRecipientInfo: " + obj.GetType().Name);
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x000ABC6D File Offset: 0x000AAC6D
		public DerObjectIdentifier OriType
		{
			get
			{
				return this.oriType;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x000ABC75 File Offset: 0x000AAC75
		public Asn1Encodable OriValue
		{
			get
			{
				return this.oriValue;
			}
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x000ABC80 File Offset: 0x000AAC80
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.oriType,
				this.oriValue
			});
		}

		// Token: 0x040013D8 RID: 5080
		private DerObjectIdentifier oriType;

		// Token: 0x040013D9 RID: 5081
		private Asn1Encodable oriValue;
	}
}
