using System;
using System.Text;
using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000232 RID: 562
	public class PkixCertPathBuilderResult : PkixCertPathValidatorResult
	{
		// Token: 0x060015F3 RID: 5619 RVA: 0x0008090F File Offset: 0x0007F90F
		public PkixCertPathBuilderResult(PkixCertPath certPath, TrustAnchor trustAnchor, PkixPolicyNode policyTree, AsymmetricKeyParameter subjectPublicKey) : base(trustAnchor, policyTree, subjectPublicKey)
		{
			if (certPath == null)
			{
				throw new ArgumentNullException("certPath");
			}
			this.certPath = certPath;
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00080930 File Offset: 0x0007F930
		public PkixCertPath CertPath
		{
			get
			{
				return this.certPath;
			}
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00080938 File Offset: 0x0007F938
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SimplePKIXCertPathBuilderResult: [\n");
			stringBuilder.Append("  Certification Path: ").Append(this.CertPath).Append('\n');
			stringBuilder.Append("  Trust Anchor: ").Append(base.TrustAnchor.TrustedCert.IssuerDN.ToString()).Append('\n');
			stringBuilder.Append("  Subject Public Key: ").Append(base.SubjectPublicKey).Append("\n]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000F3A RID: 3898
		private PkixCertPath certPath;
	}
}
