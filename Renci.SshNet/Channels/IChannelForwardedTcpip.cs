using System;
using System.Net;
using Renci.SshNet.Common;

namespace Renci.SshNet.Channels
{
	// Token: 0x0200010B RID: 267
	internal interface IChannelForwardedTcpip : IDisposable
	{
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06000B5C RID: 2908
		// (remove) Token: 0x06000B5D RID: 2909
		event EventHandler<ExceptionEventArgs> Exception;

		// Token: 0x06000B5E RID: 2910
		void Bind(IPEndPoint remoteEndpoint, IForwardedPort forwardedPort);

		// Token: 0x06000B5F RID: 2911
		void Close();
	}
}
