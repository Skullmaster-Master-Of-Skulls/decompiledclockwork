using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000B3 RID: 179
	internal class ShellRequestInfo : RequestInfo
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0001EB93 File Offset: 0x0001CD93
		public override string RequestName
		{
			get
			{
				return "shell";
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001E57E File Offset: 0x0001C77E
		public ShellRequestInfo()
		{
			base.WantReply = true;
		}

		// Token: 0x04000344 RID: 836
		public const string Name = "shell";
	}
}
