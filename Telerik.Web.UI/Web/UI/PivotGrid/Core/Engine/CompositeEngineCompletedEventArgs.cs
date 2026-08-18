using System;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x020006A5 RID: 1701
	internal class CompositeEngineCompletedEventArgs : EventArgs
	{
		// Token: 0x06003D5D RID: 15709 RVA: 0x000C591B File Offset: 0x000C3B1B
		public CompositeEngineCompletedEventArgs(Exception error)
		{
			this.Error = error;
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06003D5E RID: 15710 RVA: 0x000C592A File Offset: 0x000C3B2A
		// (set) Token: 0x06003D5F RID: 15711 RVA: 0x000C5932 File Offset: 0x000C3B32
		public Exception Error { get; private set; }
	}
}
