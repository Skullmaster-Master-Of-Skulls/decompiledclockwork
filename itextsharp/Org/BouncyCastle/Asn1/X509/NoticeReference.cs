using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000204 RID: 516
	public class NoticeReference : Asn1Encodable
	{
		// Token: 0x060013D9 RID: 5081 RVA: 0x000725E0 File Offset: 0x000715E0
		public NoticeReference(string orgName, ArrayList numbers)
		{
			this.organization = new DisplayText(orgName);
			object obj = numbers[0];
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (obj is int)
			{
				foreach (object obj2 in numbers)
				{
					int value = (int)obj2;
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerInteger(value)
					});
				}
			}
			this.noticeNumbers = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00072684 File Offset: 0x00071684
		public NoticeReference(string orgName, Asn1Sequence numbers)
		{
			this.organization = new DisplayText(orgName);
			this.noticeNumbers = numbers;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0007269F File Offset: 0x0007169F
		public NoticeReference(int displayTextType, string orgName, Asn1Sequence numbers)
		{
			this.organization = new DisplayText(displayTextType, orgName);
			this.noticeNumbers = numbers;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x000726BC File Offset: 0x000716BC
		private NoticeReference(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.organization = DisplayText.GetInstance(seq[0]);
			this.noticeNumbers = Asn1Sequence.GetInstance(seq[1]);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0007271C File Offset: 0x0007171C
		public static NoticeReference GetInstance(object obj)
		{
			if (obj is NoticeReference)
			{
				return (NoticeReference)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new NoticeReference((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0007276C File Offset: 0x0007176C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.organization,
				this.noticeNumbers
			});
		}

		// Token: 0x04000DC0 RID: 3520
		internal readonly DisplayText organization;

		// Token: 0x04000DC1 RID: 3521
		internal readonly Asn1Sequence noticeNumbers;
	}
}
