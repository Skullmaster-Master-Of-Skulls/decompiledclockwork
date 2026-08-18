using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200038A RID: 906
	internal class SecurityTokenProviderContainer
	{
		// Token: 0x0600217A RID: 8570 RVA: 0x0007BA8D File Offset: 0x00079C8D
		public SecurityTokenProviderContainer(SecurityTokenProvider tokenProvider)
		{
			if (tokenProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenProvider");
			}
			this.tokenProvider = tokenProvider;
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0007BAAF File Offset: 0x00079CAF
		public SecurityTokenProvider TokenProvider
		{
			get
			{
				return this.tokenProvider;
			}
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0007BAB7 File Offset: 0x00079CB7
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Close(TimeSpan timeout)
		{
			SecurityUtils.CloseTokenProviderIfRequired(this.tokenProvider, timeout);
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0007BAC5 File Offset: 0x00079CC5
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Open(TimeSpan timeout)
		{
			SecurityUtils.OpenTokenProviderIfRequired(this.tokenProvider, timeout);
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0007BAD3 File Offset: 0x00079CD3
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Abort()
		{
			SecurityUtils.AbortTokenProviderIfRequired(this.tokenProvider);
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0007BAE0 File Offset: 0x00079CE0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public X509Certificate2 GetCertificate(TimeSpan timeout)
		{
			X509SecurityToken x509SecurityToken = this.tokenProvider.GetToken(timeout) as X509SecurityToken;
			if (x509SecurityToken != null)
			{
				return x509SecurityToken.Certificate;
			}
			return null;
		}

		// Token: 0x04001F54 RID: 8020
		private SecurityTokenProvider tokenProvider;
	}
}
