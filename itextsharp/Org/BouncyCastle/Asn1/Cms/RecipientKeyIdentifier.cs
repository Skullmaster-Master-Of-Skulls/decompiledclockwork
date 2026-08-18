using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000490 RID: 1168
	public class RecipientKeyIdentifier : Asn1Encodable
	{
		// Token: 0x0600278E RID: 10126 RVA: 0x000EE452 File Offset: 0x000ED452
		public RecipientKeyIdentifier(Asn1OctetString subjectKeyIdentifier, DerGeneralizedTime date, OtherKeyAttribute other)
		{
			this.subjectKeyIdentifier = subjectKeyIdentifier;
			this.date = date;
			this.other = other;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000EE470 File Offset: 0x000ED470
		public RecipientKeyIdentifier(Asn1Sequence seq)
		{
			this.subjectKeyIdentifier = Asn1OctetString.GetInstance(seq[0]);
			switch (seq.Count)
			{
			case 1:
				return;
			case 2:
				if (seq[1] is DerGeneralizedTime)
				{
					this.date = (DerGeneralizedTime)seq[1];
					return;
				}
				this.other = OtherKeyAttribute.GetInstance(seq[2]);
				return;
			case 3:
				this.date = (DerGeneralizedTime)seq[1];
				this.other = OtherKeyAttribute.GetInstance(seq[2]);
				return;
			default:
				throw new ArgumentException("Invalid RecipientKeyIdentifier");
			}
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000EE516 File Offset: 0x000ED516
		public static RecipientKeyIdentifier GetInstance(Asn1TaggedObject ato, bool explicitly)
		{
			return RecipientKeyIdentifier.GetInstance(Asn1Sequence.GetInstance(ato, explicitly));
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000EE524 File Offset: 0x000ED524
		public static RecipientKeyIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is RecipientKeyIdentifier)
			{
				return (RecipientKeyIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RecipientKeyIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid RecipientKeyIdentifier: " + obj.GetType().Name);
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x000EE571 File Offset: 0x000ED571
		public Asn1OctetString SubjectKeyIdentifier
		{
			get
			{
				return this.subjectKeyIdentifier;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x000EE579 File Offset: 0x000ED579
		public DerGeneralizedTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x000EE581 File Offset: 0x000ED581
		public OtherKeyAttribute OtherKeyAttribute
		{
			get
			{
				return this.other;
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000EE58C File Offset: 0x000ED58C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.subjectKeyIdentifier
			});
			if (this.date != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.date
				});
			}
			if (this.other != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.other
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001B30 RID: 6960
		private Asn1OctetString subjectKeyIdentifier;

		// Token: 0x04001B31 RID: 6961
		private DerGeneralizedTime date;

		// Token: 0x04001B32 RID: 6962
		private OtherKeyAttribute other;
	}
}
