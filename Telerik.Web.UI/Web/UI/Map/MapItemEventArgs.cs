using System;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200043E RID: 1086
	public class MapItemEventArgs : EventArgs
	{
		// Token: 0x060026EE RID: 9966 RVA: 0x0007ED4A File Offset: 0x0007CF4A
		internal MapItemEventArgs(object item)
		{
			this.Item = item;
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x0007ED59 File Offset: 0x0007CF59
		// (set) Token: 0x060026F0 RID: 9968 RVA: 0x0007ED61 File Offset: 0x0007CF61
		public object Item { get; private set; }
	}
}
