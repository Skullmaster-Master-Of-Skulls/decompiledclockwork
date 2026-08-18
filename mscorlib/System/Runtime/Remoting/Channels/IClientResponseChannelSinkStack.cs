using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006B4 RID: 1716
	[ComVisible(true)]
	public interface IClientResponseChannelSinkStack
	{
		// Token: 0x06003DF4 RID: 15860
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void AsyncProcessResponse(ITransportHeaders headers, Stream stream);

		// Token: 0x06003DF5 RID: 15861
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void DispatchReplyMessage(IMessage msg);

		// Token: 0x06003DF6 RID: 15862
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void DispatchException(Exception e);
	}
}
