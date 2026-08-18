using System;
using System.Data;

namespace TechnoPro.Common.Reports.Public.Entities.Database
{
	// Token: 0x02000009 RID: 9
	public class QueryResultRO
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000210D File Offset: 0x0000030D
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002115 File Offset: 0x00000315
		public DataTable DataTable { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000211E File Offset: 0x0000031E
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002126 File Offset: 0x00000326
		public int Id { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000212F File Offset: 0x0000032F
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002137 File Offset: 0x00000337
		public string ErrorMessage { get; set; }
	}
}
