using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A3A RID: 2618
	[ServiceContract(Name = "PeerService", Namespace = "http://schemas.microsoft.com/net/2006/05/peer", SessionMode = SessionMode.Required, CallbackContract = typeof(IPeerService))]
	internal interface IPeerProxy : IPeerServiceContract, IOutputChannel, IChannel, ICommunicationObject
	{
	}
}
