using System;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x0200024F RID: 591
	public class OracleParameter
	{
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00018627 File Offset: 0x00016827
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0001862F File Offset: 0x0001682F
		public bool IsOutParameter { get; set; }

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00018638 File Offset: 0x00016838
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x00018640 File Offset: 0x00016840
		public string Name { get; set; }

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00018649 File Offset: 0x00016849
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x00018651 File Offset: 0x00016851
		public object Value { get; set; }

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0001865A File Offset: 0x0001685A
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x00018662 File Offset: 0x00016862
		public string OracleDbType { get; set; }
	}
}
