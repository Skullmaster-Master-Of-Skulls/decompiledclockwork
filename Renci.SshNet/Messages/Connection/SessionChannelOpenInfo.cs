using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000A7 RID: 167
	internal class SessionChannelOpenInfo : ChannelOpenInfo
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001E49C File Offset: 0x0001C69C
		public override string ChannelType
		{
			get
			{
				return "session";
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001E4A3 File Offset: 0x0001C6A3
		public SessionChannelOpenInfo()
		{
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		public SessionChannelOpenInfo(byte[] data)
		{
			base.Load(data);
		}

		// Token: 0x04000324 RID: 804
		public const string Name = "session";
	}
}
