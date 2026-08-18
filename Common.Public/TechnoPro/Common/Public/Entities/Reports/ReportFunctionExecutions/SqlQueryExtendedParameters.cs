using System;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x02000253 RID: 595
	public class SqlQueryExtendedParameters
	{
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0001872B File Offset: 0x0001692B
		// (set) Token: 0x06001200 RID: 4608 RVA: 0x00018733 File Offset: 0x00016933
		public int OverrideTimeout { get; set; }

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0001873C File Offset: 0x0001693C
		// (set) Token: 0x06001202 RID: 4610 RVA: 0x00018744 File Offset: 0x00016944
		public string Sql { get; set; }
	}
}
