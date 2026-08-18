using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008E9 RID: 2281
	internal sealed class MsmqInputChannelListener : MsmqInputChannelListenerBase
	{
		// Token: 0x06005703 RID: 22275 RVA: 0x0013F417 File Offset: 0x0013D617
		internal MsmqInputChannelListener(MsmqBindingElementBase bindingElement, BindingContext context, MsmqReceiveParameters receiveParameters) : base(bindingElement, context, receiveParameters)
		{
			base.SetSecurityTokenAuthenticator(MsmqUri.NetMsmqAddressTranslator.Scheme, context);
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x0013F433 File Offset: 0x0013D633
		protected override IInputChannel CreateInputChannel(MsmqInputChannelListenerBase listener)
		{
			return new MsmqInputChannel(listener as MsmqInputChannelListener);
		}
	}
}
