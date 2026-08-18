using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006B8 RID: 1720
	[ComVisible(true)]
	public interface IServerResponseChannelSinkStack
	{
		// Token: 0x06003E01 RID: 15873
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream);

		// Token: 0x06003E02 RID: 15874
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		Stream GetResponseStream(IMessage msg, ITransportHeaders headers);
	}
}
