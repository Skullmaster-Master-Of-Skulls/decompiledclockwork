using System;

namespace TechnoPro.Common.UI.Web.Entity.Web.EventArgs
{
	// Token: 0x02000017 RID: 23
	public class DateArgs : EventArgs
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000275E File Offset: 0x0000095E
		public DateArgs()
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002768 File Offset: 0x00000968
		public DateArgs(DateTime dt)
		{
			this.Date = dt;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000277A File Offset: 0x0000097A
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002782 File Offset: 0x00000982
		public DateTime Date { get; set; }
	}
}
