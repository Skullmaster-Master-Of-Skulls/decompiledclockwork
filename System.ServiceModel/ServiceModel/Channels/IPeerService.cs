using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A3B RID: 2619
	[ServiceContract(Name = "PeerService", Namespace = "http://schemas.microsoft.com/net/2006/05/peer", SessionMode = SessionMode.Required, CallbackContract = typeof(IPeerProxy))]
	internal interface IPeerService : IPeerServiceContract
	{
	}
}
