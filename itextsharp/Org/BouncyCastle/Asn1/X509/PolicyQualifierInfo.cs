using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000513 RID: 1299
	public class PolicyQualifierInfo : Asn1Encodable
	{
		// Token: 0x06002C6C RID: 11372 RVA: 0x0010EA52 File Offset: 0x0010DA52
		public PolicyQualifierInfo(DerObjectIdentifier policyQualifierId, Asn1Encodable qualifier)
		{
			this.policyQualifierId = policyQualifierId;
			this.qualifier = qualifier;
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x0010EA68 File Offset: 0x0010DA68
		public PolicyQualifierInfo(string cps)
		{
			this.policyQualifierId = PolicyQualifierID.IdQtCps;
			this.qualifier = new DerIA5String(cps);
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x0010EA88 File Offset: 0x0010DA88
		private PolicyQualifierInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.policyQualifierId = DerObjectIdentifier.GetInstance(seq[0]);
			this.qualifier = seq[1];
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x0010EAE4 File Offset: 0x0010DAE4
		public static PolicyQualifierInfo GetInstance(object obj)
		{
			if (obj is PolicyQualifierInfo)
			{
				return (PolicyQualifierInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PolicyQualifierInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x0010EB34 File Offset: 0x0010DB34
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.policyQualifierId,
				this.qualifier
			});
		}

		// Token: 0x04001E9D RID: 7837
		internal readonly DerObjectIdentifier policyQualifierId;

		// Token: 0x04001E9E RID: 7838
		internal readonly Asn1Encodable qualifier;
	}
}
