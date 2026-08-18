using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000703 RID: 1795
	[__DynamicallyInvokable]
	public interface IChannelFactory<TChannel> : IChannelFactory, ICommunicationObject
	{
		// Token: 0x060044AA RID: 17578
		[__DynamicallyInvokable]
		TChannel CreateChannel(EndpointAddress to);

		// Token: 0x060044AB RID: 17579
		[__DynamicallyInvokable]
		TChannel CreateChannel(EndpointAddress to, Uri via);
	}
}
