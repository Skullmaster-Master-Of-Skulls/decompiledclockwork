using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006E9 RID: 1769
	[ComVisible(true)]
	public interface IClientChannelSink : IChannelSinkBase
	{
		// Token: 0x06003F4E RID: 16206
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream);

		// Token: 0x06003F4F RID: 16207
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream);

		// Token: 0x06003F50 RID: 16208
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream);

		// Token: 0x06003F51 RID: 16209
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		Stream GetRequestStream(IMessage msg, ITransportHeaders headers);

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06003F52 RID: 16210
		IClientChannelSink NextChannelSink { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; }
	}
}
