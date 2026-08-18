using System;
using System.IdentityModel;
using System.IdentityModel.Protocols.WSTrust;
using System.Security.Claims;

namespace System.ServiceModel.Security
{
	// Token: 0x0200036B RID: 875
	public class DispatchContext
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x00077A61 File Offset: 0x00075C61
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x00077A69 File Offset: 0x00075C69
		public ClaimsPrincipal Principal
		{
			get
			{
				return this.principal;
			}
			set
			{
				this.principal = value;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x00077A72 File Offset: 0x00075C72
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x00077A7A File Offset: 0x00075C7A
		public string RequestAction
		{
			get
			{
				return this.requestAction;
			}
			set
			{
				this.requestAction = value;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x00077A83 File Offset: 0x00075C83
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x00077A8B File Offset: 0x00075C8B
		public WSTrustMessage RequestMessage
		{
			get
			{
				return this.requestMessage;
			}
			set
			{
				this.requestMessage = value;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x00077A94 File Offset: 0x00075C94
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x00077A9C File Offset: 0x00075C9C
		public string ResponseAction
		{
			get
			{
				return this.responseAction;
			}
			set
			{
				this.responseAction = value;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x00077AA5 File Offset: 0x00075CA5
		// (set) Token: 0x06002004 RID: 8196 RVA: 0x00077AAD File Offset: 0x00075CAD
		public RequestSecurityTokenResponse ResponseMessage
		{
			get
			{
				return this.responseMessage;
			}
			set
			{
				this.responseMessage = value;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x00077AB6 File Offset: 0x00075CB6
		// (set) Token: 0x06002006 RID: 8198 RVA: 0x00077ABE File Offset: 0x00075CBE
		public SecurityTokenService SecurityTokenService
		{
			get
			{
				return this.securityTokenService;
			}
			set
			{
				this.securityTokenService = value;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x00077AC7 File Offset: 0x00075CC7
		// (set) Token: 0x06002008 RID: 8200 RVA: 0x00077ACF File Offset: 0x00075CCF
		public string TrustNamespace
		{
			get
			{
				return this.trustNamespace;
			}
			set
			{
				this.trustNamespace = value;
			}
		}

		// Token: 0x04001F07 RID: 7943
		private ClaimsPrincipal principal;

		// Token: 0x04001F08 RID: 7944
		private string requestAction;

		// Token: 0x04001F09 RID: 7945
		private WSTrustMessage requestMessage;

		// Token: 0x04001F0A RID: 7946
		private string responseAction;

		// Token: 0x04001F0B RID: 7947
		private RequestSecurityTokenResponse responseMessage;

		// Token: 0x04001F0C RID: 7948
		private SecurityTokenService securityTokenService;

		// Token: 0x04001F0D RID: 7949
		private string trustNamespace;
	}
}
