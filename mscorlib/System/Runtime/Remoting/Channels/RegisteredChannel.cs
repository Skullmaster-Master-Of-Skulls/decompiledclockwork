using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006B0 RID: 1712
	internal class RegisteredChannel
	{
		// Token: 0x06003DE1 RID: 15841 RVA: 0x000D3C60 File Offset: 0x000D2C60
		internal RegisteredChannel(IChannel chnl)
		{
			this.channel = chnl;
			this.flags = 0;
			if (chnl is IChannelSender)
			{
				this.flags |= 1;
			}
			if (chnl is IChannelReceiver)
			{
				this.flags |= 2;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x000D3CAF File Offset: 0x000D2CAF
		internal virtual IChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x000D3CB7 File Offset: 0x000D2CB7
		internal virtual bool IsSender()
		{
			return (this.flags & 1) != 0;
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000D3CC7 File Offset: 0x000D2CC7
		internal virtual bool IsReceiver()
		{
			return (this.flags & 2) != 0;
		}

		// Token: 0x04001F90 RID: 8080
		private const byte SENDER = 1;

		// Token: 0x04001F91 RID: 8081
		private const byte RECEIVER = 2;

		// Token: 0x04001F92 RID: 8082
		private IChannel channel;

		// Token: 0x04001F93 RID: 8083
		private byte flags;
	}
}
