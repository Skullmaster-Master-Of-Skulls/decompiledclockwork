using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Reports.Public.Entities.Database
{
	// Token: 0x02000008 RID: 8
	public class QueryRequestRO
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000020EB File Offset: 0x000002EB
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000020F3 File Offset: 0x000002F3
		public string Sql { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000020FC File Offset: 0x000002FC
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002104 File Offset: 0x00000304
		public List<CommonParameterRO> Parameters { get; set; }
	}
}
