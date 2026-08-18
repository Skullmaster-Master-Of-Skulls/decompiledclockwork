using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020004BD RID: 1213
	public interface IGridEditableColumn
	{
		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06002BDA RID: 11226
		bool IsEditable { get; }

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06002BDB RID: 11227
		// (set) Token: 0x06002BDC RID: 11228
		IGridColumnEditor ColumnEditor { get; set; }

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06002BDD RID: 11229
		GridEditableColumn Column { get; }

		// Token: 0x06002BDE RID: 11230
		bool ShouldExtractValues(GridEditableItem item);

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06002BDF RID: 11231
		// (set) Token: 0x06002BE0 RID: 11232
		GridForceExtractValues ForceExtractValue { get; set; }

		// Token: 0x06002BE1 RID: 11233
		void FillValues(IDictionary newValues, GridEditableItem editableItem);
	}
}
