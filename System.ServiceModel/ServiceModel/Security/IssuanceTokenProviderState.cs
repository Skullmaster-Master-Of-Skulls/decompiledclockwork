using System;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x0200032E RID: 814
	internal class IssuanceTokenProviderState : IDisposable
	{
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x0006CA44 File Offset: 0x0006AC44
		public bool IsNegotiationCompleted
		{
			get
			{
				return this.isNegotiationCompleted;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0006CA4C File Offset: 0x0006AC4C
		public GenericXmlSecurityToken ServiceToken
		{
			get
			{
				this.CheckCompleted();
				return this.serviceToken;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x0006CA5A File Offset: 0x0006AC5A
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x0006CA62 File Offset: 0x0006AC62
		public EndpointAddress TargetAddress
		{
			get
			{
				return this.targetAddress;
			}
			set
			{
				this.targetAddress = value;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x0006CA6B File Offset: 0x0006AC6B
		// (set) Token: 0x06001D29 RID: 7465 RVA: 0x0006CA73 File Offset: 0x0006AC73
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.remoteAddress;
			}
			set
			{
				this.remoteAddress = value;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x0006CA7C File Offset: 0x0006AC7C
		// (set) Token: 0x06001D2B RID: 7467 RVA: 0x0006CA84 File Offset: 0x0006AC84
		public string Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0006CA8D File Offset: 0x0006AC8D
		public virtual void Dispose()
		{
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0006CA8F File Offset: 0x0006AC8F
		public void SetServiceToken(GenericXmlSecurityToken serviceToken)
		{
			if (this.IsNegotiationCompleted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NegotiationIsCompleted")));
			}
			this.serviceToken = serviceToken;
			this.isNegotiationCompleted = true;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0006CAC1 File Offset: 0x0006ACC1
		private void CheckCompleted()
		{
			if (!this.IsNegotiationCompleted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NegotiationIsNotCompleted")));
			}
		}

		// Token: 0x04001DF8 RID: 7672
		private bool isNegotiationCompleted;

		// Token: 0x04001DF9 RID: 7673
		private GenericXmlSecurityToken serviceToken;

		// Token: 0x04001DFA RID: 7674
		private string context;

		// Token: 0x04001DFB RID: 7675
		private EndpointAddress targetAddress;

		// Token: 0x04001DFC RID: 7676
		private EndpointAddress remoteAddress;
	}
}
