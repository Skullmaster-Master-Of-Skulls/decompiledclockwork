using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x0200009C RID: 156
	[Message("SSH_MSG_CHANNEL_EOF", 96)]
	public class ChannelEofMessage : ChannelMessage
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ChannelEofMessage()
		{
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001DC34 File Offset: 0x0001BE34
		public ChannelEofMessage(uint localChannelNumber) : base(localChannelNumber)
		{
		}
	}
}
