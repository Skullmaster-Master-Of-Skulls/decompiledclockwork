using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001969 RID: 6505
	public class RadDataPagerPageIndexChangeEventArgs : EventArgs
	{
		// Token: 0x0600FC11 RID: 64529 RVA: 0x0038CE60 File Offset: 0x0038B060
		public RadDataPagerPageIndexChangeEventArgs(int pageIndex, int startRowIndex)
		{
			this.NewPageIndex = pageIndex;
			this.NewStartRowIndex = startRowIndex;
		}

		// Token: 0x17004C25 RID: 19493
		// (get) Token: 0x0600FC12 RID: 64530 RVA: 0x0038CE76 File Offset: 0x0038B076
		// (set) Token: 0x0600FC13 RID: 64531 RVA: 0x0038CE7E File Offset: 0x0038B07E
		public int NewPageIndex { get; internal set; }

		// Token: 0x17004C26 RID: 19494
		// (get) Token: 0x0600FC14 RID: 64532 RVA: 0x0038CE87 File Offset: 0x0038B087
		// (set) Token: 0x0600FC15 RID: 64533 RVA: 0x0038CE8F File Offset: 0x0038B08F
		public int NewStartRowIndex { get; internal set; }
	}
}
