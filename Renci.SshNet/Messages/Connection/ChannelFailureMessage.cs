using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x0200009E RID: 158
	[Message("SSH_MSG_CHANNEL_FAILURE", 100)]
	public class ChannelFailureMessage : ChannelMessage
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ChannelFailureMessage()
		{
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001DC34 File Offset: 0x0001BE34
		public ChannelFailureMessage(uint localChannelNumber) : base(localChannelNumber)
		{
		}
	}
}
