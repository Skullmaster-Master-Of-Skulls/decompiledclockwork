using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000193 RID: 403
	public class X509NTAuthChainTrustValidator : X509CertificateValidator
	{
		// Token: 0x06000D2F RID: 3375 RVA: 0x0003D7E5 File Offset: 0x0003B9E5
		public X509NTAuthChainTrustValidator() : this(false, null)
		{
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003D7EF File Offset: 0x0003B9EF
		public X509NTAuthChainTrustValidator(bool useMachineContext, X509ChainPolicy chainPolicy)
		{
			this.useMachineContext = useMachineContext;
			this.chainPolicy = chainPolicy;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003D80C File Offset: 0x0003BA0C
		public override void Validate(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			X509CertificateChain x509CertificateChain = new X509CertificateChain(this.useMachineContext, this.chainPolicyOID);
			if (this.chainPolicy != null)
			{
				x509CertificateChain.ChainPolicy = this.chainPolicy;
			}
			if (!x509CertificateChain.Build(certificate))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID4070", new object[]
				{
					X509Util.GetCertificateId(certificate),
					X509NTAuthChainTrustValidator.GetChainStatusInformation(x509CertificateChain.ChainStatus)
				})));
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0003D894 File Offset: 0x0003BA94
		private static string GetChainStatusInformation(X509ChainStatus[] chainStatus)
		{
			if (chainStatus != null)
			{
				StringBuilder stringBuilder = new StringBuilder(128);
				for (int i = 0; i < chainStatus.Length; i++)
				{
					stringBuilder.Append(chainStatus[i].StatusInformation);
					stringBuilder.Append(" ");
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x04000CB0 RID: 3248
		private bool useMachineContext;

		// Token: 0x04000CB1 RID: 3249
		private X509ChainPolicy chainPolicy;

		// Token: 0x04000CB2 RID: 3250
		private uint chainPolicyOID = 6U;
	}
}
