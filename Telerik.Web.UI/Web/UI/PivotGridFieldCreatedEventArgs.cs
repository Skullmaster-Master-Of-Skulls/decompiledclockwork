using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DD2 RID: 3538
	public class PivotGridFieldCreatedEventArgs : EventArgs
	{
		// Token: 0x1700298C RID: 10636
		// (get) Token: 0x06008393 RID: 33683 RVA: 0x001DFE02 File Offset: 0x001DE002
		// (set) Token: 0x06008394 RID: 33684 RVA: 0x001DFE0A File Offset: 0x001DE00A
		public PivotGridField Field { get; internal set; }

		// Token: 0x06008395 RID: 33685 RVA: 0x001DFE13 File Offset: 0x001DE013
		public PivotGridFieldCreatedEventArgs(PivotGridField field)
		{
			this.Field = field;
		}
	}
}
