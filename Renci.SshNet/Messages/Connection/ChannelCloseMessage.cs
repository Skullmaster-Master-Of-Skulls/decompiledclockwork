using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x0200009A RID: 154
	[Message("SSH_MSG_CHANNEL_CLOSE", 97)]
	public class ChannelCloseMessage : ChannelMessage
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ChannelCloseMessage()
		{
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001DC34 File Offset: 0x0001BE34
		public ChannelCloseMessage(uint localChannelNumber) : base(localChannelNumber)
		{
		}
	}
}
