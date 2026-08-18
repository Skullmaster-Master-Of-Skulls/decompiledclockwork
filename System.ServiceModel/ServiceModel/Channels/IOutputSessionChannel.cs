using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200070F RID: 1807
	[__DynamicallyInvokable]
	public interface IOutputSessionChannel : IOutputChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
	{
	}
}
