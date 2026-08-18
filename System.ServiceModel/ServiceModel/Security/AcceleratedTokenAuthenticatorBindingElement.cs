using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x0200032A RID: 810
	internal class AcceleratedTokenAuthenticatorBindingElement : BindingElement
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x0006B6CF File Offset: 0x000698CF
		public AcceleratedTokenAuthenticatorBindingElement(AcceleratedTokenAuthenticator authenticator)
		{
			this.authenticator = authenticator;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0006B6DE File Offset: 0x000698DE
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return this.authenticator.BuildNegotiationChannelListener<TChannel>(context);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0006B6FF File Offset: 0x000698FF
		public override BindingElement Clone()
		{
			return new AcceleratedTokenAuthenticatorBindingElement(this.authenticator);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0006B70C File Offset: 0x0006990C
		public override T GetProperty<T>(BindingContext context)
		{
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)this.authenticator.BootstrapSecurityBindingElement.GetProperty<ISecurityCapabilities>(context));
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x04001DDF RID: 7647
		private AcceleratedTokenAuthenticator authenticator;
	}
}
