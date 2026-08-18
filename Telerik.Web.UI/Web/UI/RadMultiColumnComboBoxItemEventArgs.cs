using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200005C RID: 92
	public class RadMultiColumnComboBoxItemEventArgs : EventArgs
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00007725 File Offset: 0x00005925
		public RadMultiColumnComboBoxItemEventArgs(MultiColumnComboBoxItem item)
		{
			this.Item = item;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00007734 File Offset: 0x00005934
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000773C File Offset: 0x0000593C
		public MultiColumnComboBoxItem Item { get; set; }
	}
}
