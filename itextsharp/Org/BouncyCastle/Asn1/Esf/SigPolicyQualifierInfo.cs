using System;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x020001B8 RID: 440
	public class SigPolicyQualifierInfo : Asn1Encodable
	{
		// Token: 0x06001098 RID: 4248 RVA: 0x0005ED20 File Offset: 0x0005DD20
		public static SigPolicyQualifierInfo GetInstance(object obj)
		{
			if (obj == null || obj is SigPolicyQualifierInfo)
			{
				return (SigPolicyQualifierInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SigPolicyQualifierInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'SigPolicyQualifierInfo' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0005ED74 File Offset: 0x0005DD74
		private SigPolicyQualifierInfo(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.sigPolicyQualifierId = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.sigQualifier = seq[1].ToAsn1Object();
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0005EDE7 File Offset: 0x0005DDE7
		public SigPolicyQualifierInfo(DerObjectIdentifier sigPolicyQualifierId, Asn1Encodable sigQualifier)
		{
			this.sigPolicyQualifierId = sigPolicyQualifierId;
			this.sigQualifier = sigQualifier.ToAsn1Object();
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0005EE02 File Offset: 0x0005DE02
		public DerObjectIdentifier SigPolicyQualifierId
		{
			get
			{
				return this.sigPolicyQualifierId;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0005EE0A File Offset: 0x0005DE0A
		public Asn1Object SigQualifier
		{
			get
			{
				return this.sigQualifier;
			}
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0005EE14 File Offset: 0x0005DE14
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.sigPolicyQualifierId,
				this.sigQualifier
			});
		}

		// Token: 0x04000C2D RID: 3117
		private readonly DerObjectIdentifier sigPolicyQualifierId;

		// Token: 0x04000C2E RID: 3118
		private readonly Asn1Object sigQualifier;
	}
}
