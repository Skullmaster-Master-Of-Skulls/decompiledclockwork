using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports.RunReportResults
{
	// Token: 0x02000238 RID: 568
	public class RunFunctionResultWithData
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x00018121 File Offset: 0x00016321
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x00018129 File Offset: 0x00016329
		public RunFunctionResult Result { get; set; }

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00018132 File Offset: 0x00016332
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x0001813A File Offset: 0x0001633A
		public RunFunctionData Data { get; set; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x00018143 File Offset: 0x00016343
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x0001814B File Offset: 0x0001634B
		public IList<ReportParameter> ReportParametersOut { get; set; }
	}
}
