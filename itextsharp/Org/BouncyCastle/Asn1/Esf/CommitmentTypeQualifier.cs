using System;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200038B RID: 907
	public class CommitmentTypeQualifier : Asn1Encodable
	{
		// Token: 0x06001F90 RID: 8080 RVA: 0x000BC606 File Offset: 0x000BB606
		public CommitmentTypeQualifier(DerObjectIdentifier commitmentTypeIdentifier) : this(commitmentTypeIdentifier, null)
		{
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000BC610 File Offset: 0x000BB610
		public CommitmentTypeQualifier(DerObjectIdentifier commitmentTypeIdentifier, Asn1Encodable qualifier)
		{
			if (commitmentTypeIdentifier == null)
			{
				throw new ArgumentNullException("commitmentTypeIdentifier");
			}
			this.commitmentTypeIdentifier = commitmentTypeIdentifier;
			if (qualifier != null)
			{
				this.qualifier = qualifier.ToAsn1Object();
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000BC63C File Offset: 0x000BB63C
		public CommitmentTypeQualifier(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.commitmentTypeIdentifier = (DerObjectIdentifier)seq[0].ToAsn1Object();
			if (seq.Count > 1)
			{
				this.qualifier = seq[1].ToAsn1Object();
			}
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000BC6C4 File Offset: 0x000BB6C4
		public static CommitmentTypeQualifier GetInstance(object obj)
		{
			if (obj == null || obj is CommitmentTypeQualifier)
			{
				return (CommitmentTypeQualifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CommitmentTypeQualifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CommitmentTypeQualifier' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x000BC716 File Offset: 0x000BB716
		public DerObjectIdentifier CommitmentTypeIdentifier
		{
			get
			{
				return this.commitmentTypeIdentifier;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x000BC71E File Offset: 0x000BB71E
		public Asn1Object Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000BC728 File Offset: 0x000BB728
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.commitmentTypeIdentifier
			});
			if (this.qualifier != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.qualifier
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040015DA RID: 5594
		private readonly DerObjectIdentifier commitmentTypeIdentifier;

		// Token: 0x040015DB RID: 5595
		private readonly Asn1Object qualifier;
	}
}
