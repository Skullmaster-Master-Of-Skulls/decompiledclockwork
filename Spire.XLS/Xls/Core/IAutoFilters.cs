using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001F1 RID: 497
	public interface IAutoFilters : IExcelApplication
	{
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06001C6A RID: 7274
		// (set) Token: 0x06001C6B RID: 7275
		IXLSRange Range { get; set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06001C6C RID: 7276
		int Count { get; }

		// Token: 0x17000A8D RID: 2701
		IAutoFilter this[int columnIndex]
		{
			get;
		}
	}
}
