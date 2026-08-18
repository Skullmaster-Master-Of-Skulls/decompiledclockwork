using System;

namespace ReportFunctions
{
	// Token: 0x0200000C RID: 12
	public class ReportNode
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00005E7C File Offset: 0x00004E7C
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00005E94 File Offset: 0x00004E94
		public int OrderNum
		{
			get
			{
				return this.orderNum;
			}
			set
			{
				this.orderNum = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00005EA0 File Offset: 0x00004EA0
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00005EB8 File Offset: 0x00004EB8
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x040000DB RID: 219
		private string title;

		// Token: 0x040000DC RID: 220
		private int orderNum;
	}
}
