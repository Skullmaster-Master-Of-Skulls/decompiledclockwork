using System;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000022 RID: 34
	public class ReportNode
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000295B0 File Offset: 0x000277B0
		// (set) Token: 0x06000266 RID: 614 RVA: 0x000295C8 File Offset: 0x000277C8
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

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000295D4 File Offset: 0x000277D4
		// (set) Token: 0x06000268 RID: 616 RVA: 0x000295EC File Offset: 0x000277EC
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

		// Token: 0x040000EB RID: 235
		private string title;

		// Token: 0x040000EC RID: 236
		private int orderNum;
	}
}
