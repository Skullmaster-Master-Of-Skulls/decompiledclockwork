using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.UnivDataAccess
{
	// Token: 0x02000156 RID: 342
	public class QueryRequest
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x000118C4 File Offset: 0x0000FAC4
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x000118CC File Offset: 0x0000FACC
		public string Sql { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x000118D5 File Offset: 0x0000FAD5
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x000118DD File Offset: 0x0000FADD
		public List<CommonParameter> Parameters { get; set; }
	}
}
