using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CAA RID: 3242
	[Flags]
	public enum FieldRoles
	{
		// Token: 0x0400213C RID: 8508
		None = 0,
		// Token: 0x0400213D RID: 8509
		Value = 1,
		// Token: 0x0400213E RID: 8510
		Row = 2,
		// Token: 0x0400213F RID: 8511
		Column = 4,
		// Token: 0x04002140 RID: 8512
		Filter = 8,
		// Token: 0x04002141 RID: 8513
		All = 15
	}
}
