using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000702 RID: 1794
	[__DynamicallyInvokable]
	public interface IChannelFactory : ICommunicationObject
	{
		// Token: 0x060044A9 RID: 17577
		[__DynamicallyInvokable]
		T GetProperty<T>() where T : class;
	}
}
