using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010C7 RID: 4295
	public class GridDeletedEventArgs : GridDataChangeEventArgs
	{
		// Token: 0x0600AF5A RID: 44890 RVA: 0x0025F725 File Offset: 0x0025D925
		public GridDeletedEventArgs(int affectedRows, Exception e, GridEditableItem item) : base(affectedRows, e, item)
		{
		}
	}
}
