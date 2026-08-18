using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200092D RID: 2349
	public class RadTimelineItemEventArgs : EventArgs
	{
		// Token: 0x0600592B RID: 22827 RVA: 0x0010FF15 File Offset: 0x0010E115
		public RadTimelineItemEventArgs(TimelineItem item)
		{
			this.Item = item;
		}

		// Token: 0x17001D65 RID: 7525
		// (get) Token: 0x0600592C RID: 22828 RVA: 0x0010FF24 File Offset: 0x0010E124
		// (set) Token: 0x0600592D RID: 22829 RVA: 0x0010FF2C File Offset: 0x0010E12C
		public TimelineItem Item { get; set; }
	}
}
