using System;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200048E RID: 1166
	public class CommitmentTypeIndication : Asn1Encodable
	{
		// Token: 0x06002778 RID: 10104 RVA: 0x000EE008 File Offset: 0x000ED008
		public static CommitmentTypeIndication GetInstance(object obj)
		{
			if (obj == null || obj is CommitmentTypeIndication)
			{
				return (CommitmentTypeIndication)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CommitmentTypeIndication((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CommitmentTypeIndication' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000EE05C File Offset: 0x000ED05C
		public CommitmentTypeIndication(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.commitmentTypeId = (DerObjectIdentifier)seq[0].ToAsn1Object();
			if (seq.Count > 1)
			{
				this.commitmentTypeQualifier = (Asn1Sequence)seq[1].ToAsn1Object();
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000EE0E6 File Offset: 0x000ED0E6
		public CommitmentTypeIndication(DerObjectIdentifier commitmentTypeId) : this(commitmentTypeId, null)
		{
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000EE0F0 File Offset: 0x000ED0F0
		public CommitmentTypeIndication(DerObjectIdentifier commitmentTypeId, Asn1Sequence commitmentTypeQualifier)
		{
			if (commitmentTypeId == null)
			{
				throw new ArgumentNullException("commitmentTypeId");
			}
			this.commitmentTypeId = commitmentTypeId;
			if (commitmentTypeQualifier != null)
			{
				this.commitmentTypeQualifier = commitmentTypeQualifier;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x000EE117 File Offset: 0x000ED117
		public DerObjectIdentifier CommitmentTypeID
		{
			get
			{
				return this.commitmentTypeId;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x000EE11F File Offset: 0x000ED11F
		public Asn1Sequence CommitmentTypeQualifier
		{
			get
			{
				return this.commitmentTypeQualifier;
			}
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000EE128 File Offset: 0x000ED128
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.commitmentTypeId
			});
			if (this.commitmentTypeQualifier != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.commitmentTypeQualifier
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001B2B RID: 6955
		private readonly DerObjectIdentifier commitmentTypeId;

		// Token: 0x04001B2C RID: 6956
		private readonly Asn1Sequence commitmentTypeQualifier;
	}
}
