using System;

namespace System.Data
{
	// Token: 0x020000B7 RID: 183
	public interface IColumnMapping
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000C4C RID: 3148
		// (set) Token: 0x06000C4D RID: 3149
		string DataSetColumn { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000C4E RID: 3150
		// (set) Token: 0x06000C4F RID: 3151
		string SourceColumn { get; set; }
	}
}
