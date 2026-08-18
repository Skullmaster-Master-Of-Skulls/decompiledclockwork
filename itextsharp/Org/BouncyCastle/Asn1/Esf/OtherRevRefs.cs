using System;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200040A RID: 1034
	public class OtherRevRefs : Asn1Encodable
	{
		// Token: 0x06002333 RID: 9011 RVA: 0x000D8D54 File Offset: 0x000D7D54
		public static OtherRevRefs GetInstance(object obj)
		{
			if (obj == null || obj is OtherRevRefs)
			{
				return (OtherRevRefs)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherRevRefs((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherRevRefs' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000D8DA8 File Offset: 0x000D7DA8
		private OtherRevRefs(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.otherRevRefType = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.otherRevRefs = seq[1].ToAsn1Object();
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000D8E1B File Offset: 0x000D7E1B
		public OtherRevRefs(DerObjectIdentifier otherRevRefType, Asn1Encodable otherRevRefs)
		{
			if (otherRevRefType == null)
			{
				throw new ArgumentNullException("otherRevRefType");
			}
			if (otherRevRefs == null)
			{
				throw new ArgumentNullException("otherRevRefs");
			}
			this.otherRevRefType = otherRevRefType;
			this.otherRevRefs = otherRevRefs.ToAsn1Object();
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x000D8E52 File Offset: 0x000D7E52
		public DerObjectIdentifier OtherRevRefType
		{
			get
			{
				return this.otherRevRefType;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x000D8E5A File Offset: 0x000D7E5A
		public Asn1Object OtherRevRefsObject
		{
			get
			{
				return this.otherRevRefs;
			}
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000D8E64 File Offset: 0x000D7E64
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.otherRevRefType,
				this.otherRevRefs
			});
		}

		// Token: 0x0400186C RID: 6252
		private readonly DerObjectIdentifier otherRevRefType;

		// Token: 0x0400186D RID: 6253
		private readonly Asn1Object otherRevRefs;
	}
}
