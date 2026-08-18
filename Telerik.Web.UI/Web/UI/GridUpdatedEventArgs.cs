using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010C5 RID: 4293
	public class GridUpdatedEventArgs : GridDataChangeEventArgs
	{
		// Token: 0x0600AF52 RID: 44882 RVA: 0x0025F6D5 File Offset: 0x0025D8D5
		public GridUpdatedEventArgs(int affectedRows, Exception e, GridEditableItem item) : base(affectedRows, e, item)
		{
			this._keepInEditMode = false;
		}

		// Token: 0x1700389C RID: 14492
		// (get) Token: 0x0600AF53 RID: 44883 RVA: 0x0025F6E7 File Offset: 0x0025D8E7
		// (set) Token: 0x0600AF54 RID: 44884 RVA: 0x0025F6EF File Offset: 0x0025D8EF
		public bool KeepInEditMode
		{
			get
			{
				return this._keepInEditMode;
			}
			set
			{
				this._keepInEditMode = value;
			}
		}

		// Token: 0x1700389D RID: 14493
		// (get) Token: 0x0600AF55 RID: 44885 RVA: 0x0025F6F8 File Offset: 0x0025D8F8
		// (set) Token: 0x0600AF56 RID: 44886 RVA: 0x0025F700 File Offset: 0x0025D900
		public bool SuppressRebind
		{
			get
			{
				return this._suppressRebind;
			}
			set
			{
				this._suppressRebind = value;
			}
		}

		// Token: 0x04002E30 RID: 11824
		private bool _keepInEditMode;

		// Token: 0x04002E31 RID: 11825
		private bool _suppressRebind;
	}
}
