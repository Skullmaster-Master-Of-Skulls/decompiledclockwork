using System;

namespace ReportFunctions
{
	// Token: 0x0200001C RID: 28
	public class DynamicDataColumn
	{
		// Token: 0x06000242 RID: 578 RVA: 0x000389DE File Offset: 0x000379DE
		public DynamicDataColumn(int controlId, string colname)
		{
			this.colName = colname;
			this.controlId = controlId;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000389F8 File Offset: 0x000379F8
		public int ControlId
		{
			get
			{
				return this.controlId;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00038A10 File Offset: 0x00037A10
		public string ColName
		{
			get
			{
				return this.colName;
			}
		}

		// Token: 0x0400010F RID: 271
		private string colName;

		// Token: 0x04000110 RID: 272
		private int controlId;
	}
}
