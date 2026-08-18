using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001946 RID: 6470
	public class RadListViewDeletedEventArgs : RadListViewDataChangeEventArgs
	{
		// Token: 0x0600FA7C RID: 64124 RVA: 0x00386851 File Offset: 0x00384A51
		public RadListViewDeletedEventArgs(int affectedRows, Exception e, RadListViewDataItem item) : base(affectedRows, e, item)
		{
		}
	}
}
