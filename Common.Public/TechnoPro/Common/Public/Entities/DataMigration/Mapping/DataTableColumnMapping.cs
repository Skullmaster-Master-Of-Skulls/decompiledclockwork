using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DataMigration.Mapping
{
	// Token: 0x02000413 RID: 1043
	public class DataTableColumnMapping
	{
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x000244B9 File Offset: 0x000226B9
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x000244C1 File Offset: 0x000226C1
		public string ColumnName { get; set; }

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x000244CA File Offset: 0x000226CA
		// (set) Token: 0x06001FDC RID: 8156 RVA: 0x000244D2 File Offset: 0x000226D2
		public IList<DataTableItemMapping> ItemMappings { get; set; }
	}
}
