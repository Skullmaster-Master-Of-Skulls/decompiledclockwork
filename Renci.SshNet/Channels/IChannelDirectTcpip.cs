using System;
using System.Net.Sockets;
using Renci.SshNet.Common;

namespace Renci.SshNet.Channels
{
	// Token: 0x0200010A RID: 266
	internal interface IChannelDirectTcpip : IDisposable
	{
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000B55 RID: 2901
		// (remove) Token: 0x06000B56 RID: 2902
		event EventHandler<ExceptionEventArgs> Exception;

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B57 RID: 2903
		bool IsOpen { get; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B58 RID: 2904
		uint LocalChannelNumber { get; }

		// Token: 0x06000B59 RID: 2905
		void Open(string remoteHost, uint port, IForwardedPort forwardedPort, Socket socket);

		// Token: 0x06000B5A RID: 2906
		void Bind();

		// Token: 0x06000B5B RID: 2907
		void Close();
	}
}
