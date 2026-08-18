using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000673 RID: 1651
	public class PivotGridDataProviderErrorEventArgs : EventArgs
	{
		// Token: 0x06003C5F RID: 15455 RVA: 0x000C3B6F File Offset: 0x000C1D6F
		public PivotGridDataProviderErrorEventArgs(Exception e)
		{
			this.Error = e;
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06003C60 RID: 15456 RVA: 0x000C3B7E File Offset: 0x000C1D7E
		// (set) Token: 0x06003C61 RID: 15457 RVA: 0x000C3B86 File Offset: 0x000C1D86
		public Exception Error { get; private set; }
	}
}
