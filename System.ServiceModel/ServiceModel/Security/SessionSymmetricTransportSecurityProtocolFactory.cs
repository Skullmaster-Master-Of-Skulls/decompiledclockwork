using System;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000323 RID: 803
	internal class SessionSymmetricTransportSecurityProtocolFactory : TransportSecurityProtocolFactory
	{
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x0006A390 File Offset: 0x00068590
		public override bool SupportsReplayDetection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0006A393 File Offset: 0x00068593
		// (set) Token: 0x06001C37 RID: 7223 RVA: 0x0006A39B File Offset: 0x0006859B
		public SecurityTokenParameters SecurityTokenParameters
		{
			get
			{
				return this.securityTokenParameters;
			}
			set
			{
				base.ThrowIfImmutable();
				this.securityTokenParameters = value;
			}
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0006A3AA File Offset: 0x000685AA
		protected override SecurityProtocol OnCreateSecurityProtocol(EndpointAddress target, Uri via, object listenerSecurityState, TimeSpan timeout)
		{
			if (base.ActAsInitiator)
			{
				return new InitiatorSessionSymmetricTransportSecurityProtocol(this, target, via);
			}
			return new AcceptorSessionSymmetricTransportSecurityProtocol(this);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0006A3C4 File Offset: 0x000685C4
		public override void OnOpen(TimeSpan timeout)
		{
			base.OnOpen(timeout);
			if (this.SecurityTokenParameters == null)
			{
				base.OnPropertySettingsError("SecurityTokenParameters", true);
			}
			if (this.SecurityTokenParameters.RequireDerivedKeys)
			{
				base.ExpectKeyDerivation = true;
				this.derivedKeyTokenParameters = new SessionDerivedKeySecurityTokenParameters(base.ActAsInitiator);
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0006A411 File Offset: 0x00068611
		internal SecurityTokenParameters GetTokenParameters()
		{
			if (this.derivedKeyTokenParameters != null)
			{
				return this.derivedKeyTokenParameters;
			}
			return this.securityTokenParameters;
		}

		// Token: 0x04001DC8 RID: 7624
		private SecurityTokenParameters securityTokenParameters;

		// Token: 0x04001DC9 RID: 7625
		private SessionDerivedKeySecurityTokenParameters derivedKeyTokenParameters;
	}
}
