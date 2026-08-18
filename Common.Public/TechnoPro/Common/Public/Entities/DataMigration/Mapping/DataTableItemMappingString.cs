using System;

namespace TechnoPro.Common.Public.Entities.DataMigration.Mapping
{
	// Token: 0x02000415 RID: 1045
	public class DataTableItemMappingString : DataTableItemMapping
	{
		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x000244DB File Offset: 0x000226DB
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x000244E3 File Offset: 0x000226E3
		public string OldValue { get; set; }

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x000244EC File Offset: 0x000226EC
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x000244F4 File Offset: 0x000226F4
		public string NewValue { get; set; }
	}
}
