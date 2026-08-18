using System;
using System.IdentityModel.Selectors;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000321 RID: 801
	internal class SessionSymmetricMessageSecurityProtocolFactory : MessageSecurityProtocolFactory
	{
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0006A20F File Offset: 0x0006840F
		// (set) Token: 0x06001C25 RID: 7205 RVA: 0x0006A217 File Offset: 0x00068417
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

		// Token: 0x06001C26 RID: 7206 RVA: 0x0006A228 File Offset: 0x00068428
		public override EndpointIdentity GetIdentityOfSelf()
		{
			if (base.SecurityTokenManager is IEndpointIdentityProvider)
			{
				SecurityTokenRequirement securityTokenRequirement = base.CreateRecipientSecurityTokenRequirement();
				this.SecurityTokenParameters.InitializeSecurityTokenRequirement(securityTokenRequirement);
				return ((IEndpointIdentityProvider)base.SecurityTokenManager).GetIdentityOfSelf(securityTokenRequirement);
			}
			return base.GetIdentityOfSelf();
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0006A26D File Offset: 0x0006846D
		protected override SecurityProtocol OnCreateSecurityProtocol(EndpointAddress target, Uri via, object listenerSecurityState, TimeSpan timeout)
		{
			if (base.ActAsInitiator)
			{
				return new InitiatorSessionSymmetricMessageSecurityProtocol(this, target, via);
			}
			return new AcceptorSessionSymmetricMessageSecurityProtocol(this, null);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0006A288 File Offset: 0x00068488
		public override void OnOpen(TimeSpan timeout)
		{
			if (this.SecurityTokenParameters == null)
			{
				base.OnPropertySettingsError("SecurityTokenParameters", true);
			}
			if (this.SecurityTokenParameters.RequireDerivedKeys)
			{
				base.ExpectKeyDerivation = true;
				this.derivedKeyTokenParameters = new SessionDerivedKeySecurityTokenParameters(base.ActAsInitiator);
			}
			base.OnOpen(timeout);
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0006A2D5 File Offset: 0x000684D5
		internal SecurityTokenParameters GetTokenParameters()
		{
			if (this.derivedKeyTokenParameters != null)
			{
				return this.derivedKeyTokenParameters;
			}
			return this.securityTokenParameters;
		}

		// Token: 0x04001DC5 RID: 7621
		private SecurityTokenParameters securityTokenParameters;

		// Token: 0x04001DC6 RID: 7622
		private SessionDerivedKeySecurityTokenParameters derivedKeyTokenParameters;
	}
}
