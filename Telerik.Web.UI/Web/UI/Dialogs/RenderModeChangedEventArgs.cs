using System;

namespace Telerik.Web.UI.Dialogs
{
	// Token: 0x020019E9 RID: 6633
	public class RenderModeChangedEventArgs : EventArgs
	{
		// Token: 0x060100AF RID: 65711 RVA: 0x00399921 File Offset: 0x00397B21
		public RenderModeChangedEventArgs(RenderMode value)
		{
			this.RenderMode = value;
		}

		// Token: 0x17004D79 RID: 19833
		// (get) Token: 0x060100B0 RID: 65712 RVA: 0x00399930 File Offset: 0x00397B30
		// (set) Token: 0x060100B1 RID: 65713 RVA: 0x00399938 File Offset: 0x00397B38
		public RenderMode RenderMode { get; private set; }
	}
}
