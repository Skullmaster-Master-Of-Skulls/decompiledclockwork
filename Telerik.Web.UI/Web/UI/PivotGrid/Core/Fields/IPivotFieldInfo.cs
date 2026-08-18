using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x020006B2 RID: 1714
	public interface IPivotFieldInfo
	{
		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06003DBC RID: 15804
		string Name { get; }

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x06003DBD RID: 15805
		string DisplayName { get; }

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06003DBE RID: 15806
		Type DataType { get; }

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06003DBF RID: 15807
		FieldRoles PreferredRole { get; }

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06003DC0 RID: 15808
		FieldRoles AllowedRoles { get; }

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06003DC1 RID: 15809
		bool AutoGenerateField { get; }
	}
}
