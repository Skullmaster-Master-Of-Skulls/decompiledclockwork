using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000F0 RID: 240
	internal class ChannelOpenFailedEventArgs : ChannelEventArgs
	{
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002410B File Offset: 0x0002230B
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x00024113 File Offset: 0x00022313
		public uint ReasonCode { get; private set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002411C File Offset: 0x0002231C
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00024124 File Offset: 0x00022324
		public string Description { get; private set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002412D File Offset: 0x0002232D
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00024135 File Offset: 0x00022335
		public string Language { get; private set; }

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002413E File Offset: 0x0002233E
		public ChannelOpenFailedEventArgs(uint channelNumber, uint reasonCode, string description, string language) : base(channelNumber)
		{
			this.ReasonCode = reasonCode;
			this.Description = description;
			this.Language = language;
		}
	}
}
