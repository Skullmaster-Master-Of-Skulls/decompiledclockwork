using System;
using System.Net;
using System.Security.Principal;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x02000340 RID: 832
	[__DynamicallyInvokable]
	public sealed class WindowsClientCredential
	{
		// Token: 0x06001E2E RID: 7726 RVA: 0x000700E0 File Offset: 0x0006E2E0
		internal WindowsClientCredential()
		{
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000700F8 File Offset: 0x0006E2F8
		internal WindowsClientCredential(WindowsClientCredential other)
		{
			if (other.windowsCredentials != null)
			{
				this.windowsCredentials = SecurityUtils.GetNetworkCredentialsCopy(other.windowsCredentials);
			}
			this.allowedImpersonationLevel = other.allowedImpersonationLevel;
			this.allowNtlm = other.allowNtlm;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x00070156 File Offset: 0x0006E356
		// (set) Token: 0x06001E31 RID: 7729 RVA: 0x00070160 File Offset: 0x0006E360
		[__DynamicallyInvokable]
		public TokenImpersonationLevel AllowedImpersonationLevel
		{
			[__DynamicallyInvokable]
			get
			{
				return this.allowedImpersonationLevel;
			}
			[__DynamicallyInvokable]
			set
			{
				this.ThrowIfImmutable();
				if ((value == TokenImpersonationLevel.None || value == TokenImpersonationLevel.Anonymous) && UnsafeNativeMethods.IsTailoredApplication.Value)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("UnsupportedTokenImpersonationLevel", new object[]
					{
						"AllowedImpersonationLevel",
						value.ToString()
					})));
				}
				this.allowedImpersonationLevel = value;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x000701C5 File Offset: 0x0006E3C5
		// (set) Token: 0x06001E33 RID: 7731 RVA: 0x000701E0 File Offset: 0x0006E3E0
		[__DynamicallyInvokable]
		public NetworkCredential ClientCredential
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.windowsCredentials == null)
				{
					this.windowsCredentials = new NetworkCredential();
				}
				return this.windowsCredentials;
			}
			[__DynamicallyInvokable]
			set
			{
				this.ThrowIfImmutable();
				this.windowsCredentials = value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x000701EF File Offset: 0x0006E3EF
		// (set) Token: 0x06001E35 RID: 7733 RVA: 0x000701F7 File Offset: 0x0006E3F7
		[Obsolete("This property is deprecated and is maintained for backward compatibility only. The local machine policy will be used to determine if NTLM should be used.")]
		public bool AllowNtlm
		{
			get
			{
				return this.allowNtlm;
			}
			set
			{
				this.ThrowIfImmutable();
				this.allowNtlm = value;
			}
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00070206 File Offset: 0x0006E406
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0007020F File Offset: 0x0006E40F
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E6D RID: 7789
		internal const TokenImpersonationLevel DefaultImpersonationLevel = TokenImpersonationLevel.Identification;

		// Token: 0x04001E6E RID: 7790
		private TokenImpersonationLevel allowedImpersonationLevel = TokenImpersonationLevel.Identification;

		// Token: 0x04001E6F RID: 7791
		private NetworkCredential windowsCredentials;

		// Token: 0x04001E70 RID: 7792
		private bool allowNtlm = true;

		// Token: 0x04001E71 RID: 7793
		private bool isReadOnly;
	}
}
