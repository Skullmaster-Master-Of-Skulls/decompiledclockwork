using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001C3 RID: 451
	internal interface IPeerResolverClient : IPeerResolverContract, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
	{
	}
}
