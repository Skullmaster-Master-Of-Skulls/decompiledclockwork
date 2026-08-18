using System;

namespace TechnoPro.Common.UI.Web.Entity.Web.EventArgs
{
	// Token: 0x02000016 RID: 22
	public class AddMenuItemEventArgs : EventArgs
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000271A File Offset: 0x0000091A
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002722 File Offset: 0x00000922
		public bool AbortAddingMenuItem { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000272B File Offset: 0x0000092B
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002733 File Offset: 0x00000933
		public eClockWorkWebPage MenuItem { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000273C File Offset: 0x0000093C
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002744 File Offset: 0x00000944
		public string MenuItemTitle { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000274D File Offset: 0x0000094D
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002755 File Offset: 0x00000955
		public string NavigatePage { get; set; }
	}
}
