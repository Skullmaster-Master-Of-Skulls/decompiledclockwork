using System;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200043F RID: 1087
	public class MapItemDataBoundEventArgs : EventArgs
	{
		// Token: 0x060026F1 RID: 9969 RVA: 0x0007ED6A File Offset: 0x0007CF6A
		internal MapItemDataBoundEventArgs(object item, object dataItem)
		{
			this.Item = item;
			this.DataItem = dataItem;
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060026F2 RID: 9970 RVA: 0x0007ED80 File Offset: 0x0007CF80
		// (set) Token: 0x060026F3 RID: 9971 RVA: 0x0007ED88 File Offset: 0x0007CF88
		public object DataItem { get; private set; }

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060026F4 RID: 9972 RVA: 0x0007ED91 File Offset: 0x0007CF91
		// (set) Token: 0x060026F5 RID: 9973 RVA: 0x0007ED99 File Offset: 0x0007CF99
		public object Item { get; private set; }
	}
}
