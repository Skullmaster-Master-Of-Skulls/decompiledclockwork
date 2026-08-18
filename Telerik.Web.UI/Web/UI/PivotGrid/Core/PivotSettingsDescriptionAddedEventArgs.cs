using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000694 RID: 1684
	internal class PivotSettingsDescriptionAddedEventArgs : EventArgs
	{
		// Token: 0x06003D1A RID: 15642 RVA: 0x000C4C04 File Offset: 0x000C2E04
		public PivotSettingsDescriptionAddedEventArgs(object description)
		{
			this.Description = description;
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x000C4C13 File Offset: 0x000C2E13
		// (set) Token: 0x06003D1C RID: 15644 RVA: 0x000C4C1B File Offset: 0x000C2E1B
		public object Description { get; private set; }
	}
}
