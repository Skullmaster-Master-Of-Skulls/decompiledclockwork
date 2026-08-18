using System;

namespace Telerik.Web.UI
{
	// Token: 0x020000B3 RID: 179
	public class ButtonListEventArgs : EventArgs
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x0001C177 File Offset: 0x0001A377
		public ButtonListEventArgs(ButtonListItem item)
		{
			this.Item = item;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001C186 File Offset: 0x0001A386
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001C18E File Offset: 0x0001A38E
		public ButtonListItem Item { get; set; }
	}
}
