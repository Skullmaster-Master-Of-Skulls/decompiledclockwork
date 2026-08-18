using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Security.Authentication.ExtendedProtection;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200039E RID: 926
	internal class ProviderBackedSecurityToken : SecurityToken
	{
		// Token: 0x060022A1 RID: 8865 RVA: 0x0007F10A File Offset: 0x0007D30A
		public ProviderBackedSecurityToken(SecurityTokenProvider tokenProvider, TimeSpan timeout)
		{
			this._lock = new object();
			if (tokenProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("tokenProvider"));
			}
			this._tokenProvider = tokenProvider;
			this._timeout = timeout;
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x0007F143 File Offset: 0x0007D343
		public SecurityTokenProvider TokenProvider
		{
			get
			{
				return this._tokenProvider;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x0007F14B File Offset: 0x0007D34B
		public ChannelBinding ChannelBinding
		{
			set
			{
				this._channelBinding = value;
			}
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0007F154 File Offset: 0x0007D354
		private void ResolveSecurityToken()
		{
			if (this._securityToken == null)
			{
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._securityToken == null)
					{
						ClientCredentialsSecurityTokenManager.KerberosSecurityTokenProviderWrapper kerberosSecurityTokenProviderWrapper = this._tokenProvider as ClientCredentialsSecurityTokenManager.KerberosSecurityTokenProviderWrapper;
						if (kerberosSecurityTokenProviderWrapper != null)
						{
							this._securityToken = kerberosSecurityTokenProviderWrapper.GetToken(new TimeoutHelper(this._timeout).RemainingTime(), this._channelBinding);
						}
						else
						{
							this._securityToken = this._tokenProvider.GetToken(new TimeoutHelper(this._timeout).RemainingTime());
						}
					}
				}
			}
			if (this._securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("SecurityTokenNotResolved", new object[]
				{
					this._tokenProvider.GetType().ToString()
				})));
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x0007F240 File Offset: 0x0007D440
		public SecurityToken Token
		{
			get
			{
				if (this._securityToken == null)
				{
					this.ResolveSecurityToken();
				}
				return this._securityToken;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0007F25A File Offset: 0x0007D45A
		public override string Id
		{
			get
			{
				if (this._securityToken == null)
				{
					this.ResolveSecurityToken();
				}
				return this._securityToken.Id;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x0007F279 File Offset: 0x0007D479
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				if (this._securityToken == null)
				{
					this.ResolveSecurityToken();
				}
				return this._securityToken.SecurityKeys;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x0007F298 File Offset: 0x0007D498
		public override DateTime ValidFrom
		{
			get
			{
				if (this._securityToken == null)
				{
					this.ResolveSecurityToken();
				}
				return this._securityToken.ValidFrom;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0007F2B7 File Offset: 0x0007D4B7
		public override DateTime ValidTo
		{
			get
			{
				if (this._securityToken == null)
				{
					this.ResolveSecurityToken();
				}
				return this._securityToken.ValidTo;
			}
		}

		// Token: 0x04001FB3 RID: 8115
		private SecurityTokenProvider _tokenProvider;

		// Token: 0x04001FB4 RID: 8116
		private volatile SecurityToken _securityToken;

		// Token: 0x04001FB5 RID: 8117
		private TimeSpan _timeout;

		// Token: 0x04001FB6 RID: 8118
		private ChannelBinding _channelBinding;

		// Token: 0x04001FB7 RID: 8119
		private object _lock;
	}
}
