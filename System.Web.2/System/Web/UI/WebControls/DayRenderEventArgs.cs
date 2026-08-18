using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003D4 RID: 980
	public sealed class DayRenderEventArgs
	{
		// Token: 0x06002F3A RID: 12090 RVA: 0x0009A394 File Offset: 0x00098594
		public DayRenderEventArgs(TableCell cell, CalendarDay day)
		{
			this.day = day;
			this.cell = cell;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x0009A3AA File Offset: 0x000985AA
		public DayRenderEventArgs(TableCell cell, CalendarDay day, string selectUrl)
		{
			this.day = day;
			this.cell = cell;
			this.selectUrl = selectUrl;
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x0009A3C7 File Offset: 0x000985C7
		public TableCell Cell
		{
			get
			{
				return this.cell;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x0009A3CF File Offset: 0x000985CF
		public CalendarDay Day
		{
			get
			{
				return this.day;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x0009A3D7 File Offset: 0x000985D7
		public string SelectUrl
		{
			get
			{
				return this.selectUrl;
			}
		}

		// Token: 0x04002031 RID: 8241
		private CalendarDay day;

		// Token: 0x04002032 RID: 8242
		private TableCell cell;

		// Token: 0x04002033 RID: 8243
		private string selectUrl;
	}
}
