using System;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI
{
	// Token: 0x02000674 RID: 1652
	public sealed class PivotGridPrepareDescriptionForFieldEventArgs : EventArgs
	{
		// Token: 0x06003C62 RID: 15458 RVA: 0x000C3B8F File Offset: 0x000C1D8F
		internal PivotGridPrepareDescriptionForFieldEventArgs(PivotGridField field, IDescriptionBase description, DataProviderDescriptionType descriptionType)
		{
			this.Field = field;
			this.Description = description;
			this.DescriptionType = descriptionType;
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06003C63 RID: 15459 RVA: 0x000C3BAC File Offset: 0x000C1DAC
		// (set) Token: 0x06003C64 RID: 15460 RVA: 0x000C3BB4 File Offset: 0x000C1DB4
		public PivotGridField Field { get; private set; }

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06003C65 RID: 15461 RVA: 0x000C3BBD File Offset: 0x000C1DBD
		// (set) Token: 0x06003C66 RID: 15462 RVA: 0x000C3BC5 File Offset: 0x000C1DC5
		public DataProviderDescriptionType DescriptionType { get; private set; }

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x000C3BCE File Offset: 0x000C1DCE
		// (set) Token: 0x06003C68 RID: 15464 RVA: 0x000C3BD6 File Offset: 0x000C1DD6
		public IDescriptionBase Description { get; set; }
	}
}
