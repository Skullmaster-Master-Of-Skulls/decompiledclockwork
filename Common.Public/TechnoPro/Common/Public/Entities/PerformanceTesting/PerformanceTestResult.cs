using System;

namespace TechnoPro.Common.Public.Entities.PerformanceTesting
{
	// Token: 0x02000254 RID: 596
	public class PerformanceTestResult
	{
		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0001874D File Offset: 0x0001694D
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x00018755 File Offset: 0x00016955
		public PerformanceTestTimeTaken ServiceTimeTaken { get; set; }

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0001875E File Offset: 0x0001695E
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x00018766 File Offset: 0x00016966
		public PerformanceTestTimeTaken ServiceManagerTimeTaken { get; set; }

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0001876F File Offset: 0x0001696F
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x00018777 File Offset: 0x00016977
		public PerformanceTestTimeTaken ManagerTimeTaken { get; set; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00018780 File Offset: 0x00016980
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x00018788 File Offset: 0x00016988
		public string Notes { get; set; }
	}
}
