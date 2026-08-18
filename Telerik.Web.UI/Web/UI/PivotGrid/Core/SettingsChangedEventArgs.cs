using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D51 RID: 3409
	public class SettingsChangedEventArgs : EventArgs
	{
		// Token: 0x17002887 RID: 10375
		// (get) Token: 0x06007F1F RID: 32543 RVA: 0x001D11BD File Offset: 0x001CF3BD
		// (set) Token: 0x06007F20 RID: 32544 RVA: 0x001D11C5 File Offset: 0x001CF3C5
		public SettingsNode OriginalSource { get; internal set; }
	}
}
