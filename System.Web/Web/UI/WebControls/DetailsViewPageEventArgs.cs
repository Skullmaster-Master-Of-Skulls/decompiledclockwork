using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000565 RID: 1381
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewPageEventArgs : CancelEventArgs
	{
		// Token: 0x0600442C RID: 17452 RVA: 0x0011990A File Offset: 0x0011890A
		public DetailsViewPageEventArgs(int newPageIndex)
		{
			this._newPageIndex = newPageIndex;
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x0600442D RID: 17453 RVA: 0x00119919 File Offset: 0x00118919
		// (set) Token: 0x0600442E RID: 17454 RVA: 0x00119921 File Offset: 0x00118921
		public int NewPageIndex
		{
			get
			{
				return this._newPageIndex;
			}
			set
			{
				this._newPageIndex = value;
			}
		}

		// Token: 0x040029A2 RID: 10658
		private int _newPageIndex;
	}
}
