using System;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000261 RID: 609
	public class OtherRevVals : Asn1Encodable
	{
		// Token: 0x06001709 RID: 5897 RVA: 0x000850AC File Offset: 0x000840AC
		public static OtherRevVals GetInstance(object obj)
		{
			if (obj == null || obj is OtherRevVals)
			{
				return (OtherRevVals)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherRevVals((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherRevVals' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00085100 File Offset: 0x00084100
		private OtherRevVals(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.otherRevValType = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.otherRevVals = seq[1].ToAsn1Object();
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00085173 File Offset: 0x00084173
		public OtherRevVals(DerObjectIdentifier otherRevValType, Asn1Encodable otherRevVals)
		{
			if (otherRevValType == null)
			{
				throw new ArgumentNullException("otherRevValType");
			}
			if (otherRevVals == null)
			{
				throw new ArgumentNullException("otherRevVals");
			}
			this.otherRevValType = otherRevValType;
			this.otherRevVals = otherRevVals.ToAsn1Object();
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x000851AA File Offset: 0x000841AA
		public DerObjectIdentifier OtherRevValType
		{
			get
			{
				return this.otherRevValType;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x000851B2 File Offset: 0x000841B2
		public Asn1Object OtherRevValsObject
		{
			get
			{
				return this.otherRevVals;
			}
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x000851BC File Offset: 0x000841BC
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.otherRevValType,
				this.otherRevVals
			});
		}

		// Token: 0x04000FC9 RID: 4041
		private readonly DerObjectIdentifier otherRevValType;

		// Token: 0x04000FCA RID: 4042
		private readonly Asn1Object otherRevVals;
	}
}
