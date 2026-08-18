using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000B0 RID: 176
	public class KeepAliveRequestInfo : RequestInfo
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0001E9D1 File Offset: 0x0001CBD1
		public override string RequestName
		{
			get
			{
				return "keepalive@openssh.com";
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001E69A File Offset: 0x0001C89A
		public KeepAliveRequestInfo()
		{
			base.WantReply = false;
		}

		// Token: 0x0400033B RID: 827
		public const string Name = "keepalive@openssh.com";
	}
}
