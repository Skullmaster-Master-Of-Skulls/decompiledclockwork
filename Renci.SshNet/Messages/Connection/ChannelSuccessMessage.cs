using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000B9 RID: 185
	[Message("SSH_MSG_CHANNEL_SUCCESS", 99)]
	public class ChannelSuccessMessage : ChannelMessage
	{
		// Token: 0x06000890 RID: 2192 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ChannelSuccessMessage()
		{
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001DC34 File Offset: 0x0001BE34
		public ChannelSuccessMessage(uint localChannelNumber) : base(localChannelNumber)
		{
		}
	}
}
