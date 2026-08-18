using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000EF RID: 239
	internal class ChannelOpenConfirmedEventArgs : ChannelEventArgs
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x000240D2 File Offset: 0x000222D2
		public ChannelOpenConfirmedEventArgs(uint remoteChannelNumber, uint initialWindowSize, uint maximumPacketSize) : base(remoteChannelNumber)
		{
			this.InitialWindowSize = initialWindowSize;
			this.MaximumPacketSize = maximumPacketSize;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x000240E9 File Offset: 0x000222E9
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x000240F1 File Offset: 0x000222F1
		public uint InitialWindowSize { get; private set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x000240FA File Offset: 0x000222FA
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00024102 File Offset: 0x00022302
		public uint MaximumPacketSize { get; private set; }
	}
}
