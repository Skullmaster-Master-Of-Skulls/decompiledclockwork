using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000713 RID: 1811
	[__DynamicallyInvokable]
	public interface IRequestSessionChannel : IRequestChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
	{
	}
}
