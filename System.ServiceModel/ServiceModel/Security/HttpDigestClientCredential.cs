using System;
using System.Net;
using System.Security.Principal;

namespace System.ServiceModel.Security
{
	// Token: 0x02000336 RID: 822
	[__DynamicallyInvokable]
	public sealed class HttpDigestClientCredential
	{
		// Token: 0x06001DC5 RID: 7621 RVA: 0x0006E6DE File Offset: 0x0006C8DE
		internal HttpDigestClientCredential()
		{
			this.digestCredentials = new NetworkCredential();
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
		internal HttpDigestClientCredential(HttpDigestClientCredential other)
		{
			this.allowedImpersonationLevel = other.allowedImpersonationLevel;
			this.digestCredentials = SecurityUtils.GetNetworkCredentialsCopy(other.digestCredentials);
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0006E730 File Offset: 0x0006C930
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x0006E738 File Offset: 0x0006C938
		public TokenImpersonationLevel AllowedImpersonationLevel
		{
			get
			{
				return this.allowedImpersonationLevel;
			}
			set
			{
				this.ThrowIfImmutable();
				this.allowedImpersonationLevel = value;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x0006E747 File Offset: 0x0006C947
		// (set) Token: 0x06001DCA RID: 7626 RVA: 0x0006E74F File Offset: 0x0006C94F
		[__DynamicallyInvokable]
		public NetworkCredential ClientCredential
		{
			[__DynamicallyInvokable]
			get
			{
				return this.digestCredentials;
			}
			[__DynamicallyInvokable]
			set
			{
				this.ThrowIfImmutable();
				this.digestCredentials = value;
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0006E75E File Offset: 0x0006C95E
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x0006E767 File Offset: 0x0006C967
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E39 RID: 7737
		private TokenImpersonationLevel allowedImpersonationLevel = TokenImpersonationLevel.Identification;

		// Token: 0x04001E3A RID: 7738
		private NetworkCredential digestCredentials;

		// Token: 0x04001E3B RID: 7739
		private bool isReadOnly;
	}
}
