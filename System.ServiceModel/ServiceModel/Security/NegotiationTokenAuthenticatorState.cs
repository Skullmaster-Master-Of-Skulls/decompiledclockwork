using System;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000300 RID: 768
	internal class NegotiationTokenAuthenticatorState : IDisposable
	{
		// Token: 0x06001A39 RID: 6713 RVA: 0x000626CD File Offset: 0x000608CD
		public NegotiationTokenAuthenticatorState()
		{
			this.thisLock = new object();
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x000626E0 File Offset: 0x000608E0
		public object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x000626E8 File Offset: 0x000608E8
		public bool IsNegotiationCompleted
		{
			get
			{
				return this.isNegotiationCompleted;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x000626F0 File Offset: 0x000608F0
		public SecurityContextSecurityToken ServiceToken
		{
			get
			{
				this.CheckCompleted();
				return this.serviceToken;
			}
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000626FE File Offset: 0x000608FE
		public virtual void Dispose()
		{
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00062700 File Offset: 0x00060900
		public void SetServiceToken(SecurityContextSecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			this.serviceToken = token;
			this.isNegotiationCompleted = true;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00062723 File Offset: 0x00060923
		public virtual string GetRemoteIdentityName()
		{
			if (this.isNegotiationCompleted)
			{
				return SecurityUtils.GetIdentityNamesFromPolicies(this.serviceToken.AuthorizationPolicies);
			}
			return string.Empty;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00062743 File Offset: 0x00060943
		private void CheckCompleted()
		{
			if (!this.isNegotiationCompleted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NegotiationIsNotCompleted")));
			}
		}

		// Token: 0x04001D0E RID: 7438
		private bool isNegotiationCompleted;

		// Token: 0x04001D0F RID: 7439
		private SecurityContextSecurityToken serviceToken;

		// Token: 0x04001D10 RID: 7440
		private object thisLock;
	}
}
