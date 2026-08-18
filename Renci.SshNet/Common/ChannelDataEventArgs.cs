using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000EC RID: 236
	internal class ChannelDataEventArgs : ChannelEventArgs
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002406F File Offset: 0x0002226F
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x00024077 File Offset: 0x00022277
		public byte[] Data { get; private set; }

		// Token: 0x06000A75 RID: 2677 RVA: 0x00024080 File Offset: 0x00022280
		public ChannelDataEventArgs(uint channelNumber, byte[] data) : base(channelNumber)
		{
			this.Data = data;
		}
	}
}
