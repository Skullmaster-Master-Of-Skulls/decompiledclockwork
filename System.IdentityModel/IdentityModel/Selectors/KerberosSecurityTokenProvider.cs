using System;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001A2 RID: 418
	public class KerberosSecurityTokenProvider : SecurityTokenProvider
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x0003ED12 File Offset: 0x0003CF12
		public KerberosSecurityTokenProvider(string servicePrincipalName) : this(servicePrincipalName, TokenImpersonationLevel.Identification)
		{
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0003ED1C File Offset: 0x0003CF1C
		public KerberosSecurityTokenProvider(string servicePrincipalName, TokenImpersonationLevel tokenImpersonationLevel) : this(servicePrincipalName, tokenImpersonationLevel, null)
		{
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0003ED28 File Offset: 0x0003CF28
		public KerberosSecurityTokenProvider(string servicePrincipalName, TokenImpersonationLevel tokenImpersonationLevel, NetworkCredential networkCredential)
		{
			if (servicePrincipalName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("servicePrincipalName");
			}
			if (tokenImpersonationLevel != TokenImpersonationLevel.Identification && tokenImpersonationLevel != TokenImpersonationLevel.Impersonation)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("tokenImpersonationLevel", SR.GetString("ImpersonationLevelNotSupported", new object[]
				{
					tokenImpersonationLevel
				})));
			}
			this.servicePrincipalName = servicePrincipalName;
			this.tokenImpersonationLevel = tokenImpersonationLevel;
			this.networkCredential = networkCredential;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0003ED99 File Offset: 0x0003CF99
		public string ServicePrincipalName
		{
			get
			{
				return this.servicePrincipalName;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0003EDA1 File Offset: 0x0003CFA1
		public TokenImpersonationLevel TokenImpersonationLevel
		{
			get
			{
				return this.tokenImpersonationLevel;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0003EDA9 File Offset: 0x0003CFA9
		public NetworkCredential NetworkCredential
		{
			get
			{
				return this.networkCredential;
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0003EDB1 File Offset: 0x0003CFB1
		internal SecurityToken GetToken(TimeSpan timeout, ChannelBinding channelbinding)
		{
			return new KerberosRequestorSecurityToken(this.ServicePrincipalName, this.TokenImpersonationLevel, this.NetworkCredential, SecurityUniqueId.Create().Value, channelbinding);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0003EDD5 File Offset: 0x0003CFD5
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			return this.GetToken(timeout, null);
		}

		// Token: 0x04000CD3 RID: 3283
		private string servicePrincipalName;

		// Token: 0x04000CD4 RID: 3284
		private TokenImpersonationLevel tokenImpersonationLevel;

		// Token: 0x04000CD5 RID: 3285
		private NetworkCredential networkCredential;
	}
}
