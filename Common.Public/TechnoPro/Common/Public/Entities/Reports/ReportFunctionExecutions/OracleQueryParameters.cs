using System;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x0200024C RID: 588
	public class OracleQueryParameters
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x000185CC File Offset: 0x000167CC
		public OracleQueryParameters()
		{
			this.Query = new OracleQueryRequest();
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x000185E2 File Offset: 0x000167E2
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x000185EA File Offset: 0x000167EA
		public string ConnectionString { get; set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x000185F3 File Offset: 0x000167F3
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x000185FB File Offset: 0x000167FB
		public OracleQueryRequest Query { get; set; }
	}
}
