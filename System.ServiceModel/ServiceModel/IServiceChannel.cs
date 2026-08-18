using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x020000FD RID: 253
	public interface IServiceChannel : IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000549 RID: 1353
		Uri ListenUri { get; }
	}
}
