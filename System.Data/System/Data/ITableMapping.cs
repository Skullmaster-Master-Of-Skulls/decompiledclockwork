using System;

namespace System.Data
{
	// Token: 0x020000C2 RID: 194
	public interface ITableMapping
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000CA3 RID: 3235
		IColumnMappingCollection ColumnMappings { get; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000CA4 RID: 3236
		// (set) Token: 0x06000CA5 RID: 3237
		string DataSetTable { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000CA6 RID: 3238
		// (set) Token: 0x06000CA7 RID: 3239
		string SourceTable { get; set; }
	}
}
