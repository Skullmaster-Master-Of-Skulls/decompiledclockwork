using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions
{
	// Token: 0x02000250 RID: 592
	public class OracleQueryRequest
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x0001866B File Offset: 0x0001686B
		public OracleQueryRequest()
		{
			this.Parameters = new List<OracleParameter>();
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00018681 File Offset: 0x00016881
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x00018689 File Offset: 0x00016889
		public eOracleQueryType QueryType { get; set; }

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x00018692 File Offset: 0x00016892
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x0001869A File Offset: 0x0001689A
		public string Sql { get; set; }

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x000186A3 File Offset: 0x000168A3
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x000186AB File Offset: 0x000168AB
		public List<OracleParameter> Parameters { get; set; }
	}
}
