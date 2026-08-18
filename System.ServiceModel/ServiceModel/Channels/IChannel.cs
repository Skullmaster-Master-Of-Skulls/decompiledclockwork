using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000701 RID: 1793
	[__DynamicallyInvokable]
	public interface IChannel : ICommunicationObject
	{
		// Token: 0x060044A8 RID: 17576
		[__DynamicallyInvokable]
		T GetProperty<T>() where T : class;
	}
}
