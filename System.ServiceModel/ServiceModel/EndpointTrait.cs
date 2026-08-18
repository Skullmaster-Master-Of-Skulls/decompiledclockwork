using System;

namespace System.ServiceModel
{
	// Token: 0x020000EE RID: 238
	internal abstract class EndpointTrait<TChannel> where TChannel : class
	{
		// Token: 0x060004FC RID: 1276
		public abstract ChannelFactory<TChannel> CreateChannelFactory();
	}
}
