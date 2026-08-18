using System;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x020006A8 RID: 1704
	internal class EngineTaskCompletedEventArgs : EventArgs
	{
		// Token: 0x06003D72 RID: 15730 RVA: 0x000C5A3B File Offset: 0x000C3C3B
		public EngineTaskCompletedEventArgs(Exception error)
		{
			this.Error = error;
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x000C5A4A File Offset: 0x000C3C4A
		// (set) Token: 0x06003D74 RID: 15732 RVA: 0x000C5A52 File Offset: 0x000C3C52
		public Exception Error { get; private set; }
	}
}
