using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001945 RID: 6469
	public class RadListViewDataChangeEventArgs : EventArgs
	{
		// Token: 0x0600FA73 RID: 64115 RVA: 0x003867E9 File Offset: 0x003849E9
		public RadListViewDataChangeEventArgs(int affectedRows, Exception e, RadListViewDataItem item)
		{
			this.AffectedRows = affectedRows;
			this.ExceptionHandled = false;
			this.Exception = e;
			this.Item = item;
		}

		// Token: 0x17004BAB RID: 19371
		// (get) Token: 0x0600FA74 RID: 64116 RVA: 0x0038680D File Offset: 0x00384A0D
		// (set) Token: 0x0600FA75 RID: 64117 RVA: 0x00386815 File Offset: 0x00384A15
		public int AffectedRows { get; private set; }

		// Token: 0x17004BAC RID: 19372
		// (get) Token: 0x0600FA76 RID: 64118 RVA: 0x0038681E File Offset: 0x00384A1E
		// (set) Token: 0x0600FA77 RID: 64119 RVA: 0x00386826 File Offset: 0x00384A26
		public Exception Exception { get; private set; }

		// Token: 0x17004BAD RID: 19373
		// (get) Token: 0x0600FA78 RID: 64120 RVA: 0x0038682F File Offset: 0x00384A2F
		// (set) Token: 0x0600FA79 RID: 64121 RVA: 0x00386837 File Offset: 0x00384A37
		public RadListViewDataItem Item { get; private set; }

		// Token: 0x17004BAE RID: 19374
		// (get) Token: 0x0600FA7A RID: 64122 RVA: 0x00386840 File Offset: 0x00384A40
		// (set) Token: 0x0600FA7B RID: 64123 RVA: 0x00386848 File Offset: 0x00384A48
		public bool ExceptionHandled { get; set; }
	}
}
