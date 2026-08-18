using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000ED RID: 237
	internal class ChannelEventArgs : EventArgs
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00024090 File Offset: 0x00022290
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x00024098 File Offset: 0x00022298
		public uint ChannelNumber { get; private set; }

		// Token: 0x06000A78 RID: 2680 RVA: 0x000240A1 File Offset: 0x000222A1
		public ChannelEventArgs(uint channelNumber)
		{
			this.ChannelNumber = channelNumber;
		}
	}
}
