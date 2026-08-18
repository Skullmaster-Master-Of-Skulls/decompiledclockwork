using System;

namespace Microsoft.Owin.Security.OAuth.Messages
{
	// Token: 0x02000006 RID: 6
	public class TokenEndpointRequest
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002348 File Offset: 0x00000548
		public TokenEndpointRequest(IReadableStringCollection parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			Func<string, string> func = new Func<string, string>(parameters.Get);
			this.Parameters = parameters;
			this.GrantType = func("grant_type");
			this.ClientId = func("client_id");
			if (string.Equals(this.GrantType, "authorization_code", StringComparison.Ordinal))
			{
				this.AuthorizationCodeGrant = new TokenEndpointRequestAuthorizationCode
				{
					Code = func("code"),
					RedirectUri = func("redirect_uri")
				};
				return;
			}
			if (string.Equals(this.GrantType, "client_credentials", StringComparison.Ordinal))
			{
				this.ClientCredentialsGrant = new TokenEndpointRequestClientCredentials
				{
					Scope = (func("scope") ?? string.Empty).Split(new char[]
					{
						' '
					})
				};
				return;
			}
			if (string.Equals(this.GrantType, "refresh_token", StringComparison.Ordinal))
			{
				this.RefreshTokenGrant = new TokenEndpointRequestRefreshToken
				{
					RefreshToken = func("refresh_token"),
					Scope = (func("scope") ?? string.Empty).Split(new char[]
					{
						' '
					})
				};
				return;
			}
			if (string.Equals(this.GrantType, "password", StringComparison.Ordinal))
			{
				this.ResourceOwnerPasswordCredentialsGrant = new TokenEndpointRequestResourceOwnerPasswordCredentials
				{
					UserName = func("username"),
					Password = func("password"),
					Scope = (func("scope") ?? string.Empty).Split(new char[]
					{
						' '
					})
				};
				return;
			}
			if (!string.IsNullOrEmpty(this.GrantType))
			{
				this.CustomExtensionGrant = new TokenEndpointRequestCustomExtension
				{
					Parameters = parameters
				};
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000252F File Offset: 0x0000072F
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002537 File Offset: 0x00000737
		public IReadableStringCollection Parameters { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002540 File Offset: 0x00000740
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002548 File Offset: 0x00000748
		public string GrantType { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002551 File Offset: 0x00000751
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002559 File Offset: 0x00000759
		public string ClientId { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002562 File Offset: 0x00000762
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000256A File Offset: 0x0000076A
		public TokenEndpointRequestAuthorizationCode AuthorizationCodeGrant { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002573 File Offset: 0x00000773
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000257B File Offset: 0x0000077B
		public TokenEndpointRequestClientCredentials ClientCredentialsGrant { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002584 File Offset: 0x00000784
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000258C File Offset: 0x0000078C
		public TokenEndpointRequestRefreshToken RefreshTokenGrant { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002595 File Offset: 0x00000795
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000259D File Offset: 0x0000079D
		public TokenEndpointRequestResourceOwnerPasswordCredentials ResourceOwnerPasswordCredentialsGrant { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025A6 File Offset: 0x000007A6
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000025AE File Offset: 0x000007AE
		public TokenEndpointRequestCustomExtension CustomExtensionGrant { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025B7 File Offset: 0x000007B7
		public bool IsAuthorizationCodeGrantType
		{
			get
			{
				return this.AuthorizationCodeGrant != null;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000025C5 File Offset: 0x000007C5
		public bool IsClientCredentialsGrantType
		{
			get
			{
				return this.ClientCredentialsGrant != null;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000025D3 File Offset: 0x000007D3
		public bool IsRefreshTokenGrantType
		{
			get
			{
				return this.RefreshTokenGrant != null;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025E1 File Offset: 0x000007E1
		public bool IsResourceOwnerPasswordCredentialsGrantType
		{
			get
			{
				return this.ResourceOwnerPasswordCredentialsGrant != null;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000025EF File Offset: 0x000007EF
		public bool IsCustomExtensionGrantType
		{
			get
			{
				return this.CustomExtensionGrant != null;
			}
		}
	}
}
