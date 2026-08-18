using System;
using System.Text;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x020001DB RID: 475
	public class PkixCertPathValidatorResult
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0006B2AC File Offset: 0x0006A2AC
		public PkixPolicyNode PolicyTree
		{
			get
			{
				return this.policyTree;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0006B2B4 File Offset: 0x0006A2B4
		public TrustAnchor TrustAnchor
		{
			get
			{
				return this.trustAnchor;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0006B2BC File Offset: 0x0006A2BC
		public AsymmetricKeyParameter SubjectPublicKey
		{
			get
			{
				return this.subjectPublicKey;
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0006B2C4 File Offset: 0x0006A2C4
		public PkixCertPathValidatorResult(TrustAnchor trustAnchor, PkixPolicyNode policyTree, AsymmetricKeyParameter subjectPublicKey)
		{
			if (subjectPublicKey == null)
			{
				throw new NullReferenceException("subjectPublicKey must be non-null");
			}
			if (trustAnchor == null)
			{
				throw new NullReferenceException("trustAnchor must be non-null");
			}
			this.trustAnchor = trustAnchor;
			this.policyTree = policyTree;
			this.subjectPublicKey = subjectPublicKey;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0006B2FD File Offset: 0x0006A2FD
		public object Clone()
		{
			return new PkixCertPathValidatorResult(this.TrustAnchor, this.PolicyTree, this.SubjectPublicKey);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0006B318 File Offset: 0x0006A318
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PKIXCertPathValidatorResult: [ \n");
			stringBuilder.Append("  Trust Anchor: ").Append(this.TrustAnchor).Append('\n');
			stringBuilder.Append("  Policy Tree: ").Append(this.PolicyTree).Append('\n');
			stringBuilder.Append("  Subject Public Key: ").Append(this.SubjectPublicKey).Append("\n]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000D46 RID: 3398
		private TrustAnchor trustAnchor;

		// Token: 0x04000D47 RID: 3399
		private PkixPolicyNode policyTree;

		// Token: 0x04000D48 RID: 3400
		private AsymmetricKeyParameter subjectPublicKey;
	}
}
