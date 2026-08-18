using System;

namespace Telerik.Charting
{
	// Token: 0x020017B1 RID: 6065
	public class PaletteItemsCollection : ChartingStateManagedCollection<PaletteItem>
	{
		// Token: 0x0600EC34 RID: 60468 RVA: 0x0035B200 File Offset: 0x00359400
		public PaletteItem GetItem(int index)
		{
			return base.List[index];
		}
	}
}
