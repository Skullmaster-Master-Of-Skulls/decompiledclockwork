using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000AB RID: 171
	public class EndOfWriteRequestInfo : RequestInfo
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001E693 File Offset: 0x0001C893
		public override string RequestName
		{
			get
			{
				return "eow@openssh.com";
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001E69A File Offset: 0x0001C89A
		public EndOfWriteRequestInfo()
		{
			base.WantReply = false;
		}

		// Token: 0x0400032D RID: 813
		public const string Name = "eow@openssh.com";
	}
}
