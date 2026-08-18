using System;
using Renci.SshNet.Messages.Connection;

namespace Renci.SshNet.Common
{
	// Token: 0x020000F1 RID: 241
	internal class ChannelRequestEventArgs : EventArgs
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0002415D File Offset: 0x0002235D
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00024165 File Offset: 0x00022365
		public RequestInfo Info { get; private set; }

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002416E File Offset: 0x0002236E
		public ChannelRequestEventArgs(RequestInfo info)
		{
			this.Info = info;
		}
	}
}
