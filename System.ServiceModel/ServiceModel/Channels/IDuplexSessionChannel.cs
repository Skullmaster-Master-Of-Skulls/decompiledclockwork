using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000708 RID: 1800
	[__DynamicallyInvokable]
	public interface IDuplexSessionChannel : IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
	{
	}
}
