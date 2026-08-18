using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DD3 RID: 3539
	public class PivotGridAddingFieldToZoneEventArgs : EventArgs
	{
		// Token: 0x06008396 RID: 33686 RVA: 0x001DFE22 File Offset: 0x001DE022
		public PivotGridAddingFieldToZoneEventArgs(PivotGridField field)
		{
			this.Field = field;
		}

		// Token: 0x1700298D RID: 10637
		// (get) Token: 0x06008397 RID: 33687 RVA: 0x001DFE31 File Offset: 0x001DE031
		// (set) Token: 0x06008398 RID: 33688 RVA: 0x001DFE39 File Offset: 0x001DE039
		public PivotGridField Field { get; internal set; }
	}
}
