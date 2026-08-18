using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000623 RID: 1571
	public class UserNotice : Asn1Encodable
	{
		// Token: 0x06003563 RID: 13667 RVA: 0x0014B45C File Offset: 0x0014A45C
		public UserNotice(NoticeReference noticeRef, DisplayText explicitText)
		{
			this.noticeRef = noticeRef;
			this.explicitText = explicitText;
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x0014B472 File Offset: 0x0014A472
		public UserNotice(NoticeReference noticeRef, string str)
		{
			this.noticeRef = noticeRef;
			this.explicitText = new DisplayText(str);
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x0014B490 File Offset: 0x0014A490
		public UserNotice(Asn1Sequence seq)
		{
			if (seq.Count == 2)
			{
				this.noticeRef = NoticeReference.GetInstance(seq[0]);
				this.explicitText = DisplayText.GetInstance(seq[1]);
				return;
			}
			if (seq.Count != 1)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			if (seq[0].ToAsn1Object() is Asn1Sequence)
			{
				this.noticeRef = NoticeReference.GetInstance(seq[0]);
				return;
			}
			this.explicitText = DisplayText.GetInstance(seq[0]);
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x0014B530 File Offset: 0x0014A530
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.noticeRef != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.noticeRef
				});
			}
			if (this.explicitText != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.explicitText
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040023AB RID: 9131
		internal NoticeReference noticeRef;

		// Token: 0x040023AC RID: 9132
		internal DisplayText explicitText;
	}
}
