using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DAE RID: 3502
	[Flags]
	public enum PivotGridZoneType
	{
		// Token: 0x04002416 RID: 9238
		Filter = 1,
		// Token: 0x04002417 RID: 9239
		Aggregate = 2,
		// Token: 0x04002418 RID: 9240
		Column = 4,
		// Token: 0x04002419 RID: 9241
		Row = 8,
		// Token: 0x0400241A RID: 9242
		Data = 16,
		// Token: 0x0400241B RID: 9243
		ColumnHeader = 32
	}
}
