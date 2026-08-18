using System;

namespace System.Data
{
	// Token: 0x0200010B RID: 267
	public interface ITableMapping
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060010CE RID: 4302
		IColumnMappingCollection ColumnMappings { get; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060010CF RID: 4303
		// (set) Token: 0x060010D0 RID: 4304
		string DataSetTable { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060010D1 RID: 4305
		// (set) Token: 0x060010D2 RID: 4306
		string SourceTable { get; set; }
	}
}
