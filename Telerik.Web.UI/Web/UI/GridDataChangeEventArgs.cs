using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010C4 RID: 4292
	public class GridDataChangeEventArgs : EventArgs
	{
		// Token: 0x0600AF4C RID: 44876 RVA: 0x0025F688 File Offset: 0x0025D888
		public GridDataChangeEventArgs(int affectedRows, Exception e, GridEditableItem item)
		{
			this._affectedRows = affectedRows;
			this._exceptionHandled = false;
			this._exception = e;
			this._item = item;
		}

		// Token: 0x17003898 RID: 14488
		// (get) Token: 0x0600AF4D RID: 44877 RVA: 0x0025F6AC File Offset: 0x0025D8AC
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
		}

		// Token: 0x17003899 RID: 14489
		// (get) Token: 0x0600AF4E RID: 44878 RVA: 0x0025F6B4 File Offset: 0x0025D8B4
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x1700389A RID: 14490
		// (get) Token: 0x0600AF4F RID: 44879 RVA: 0x0025F6BC File Offset: 0x0025D8BC
		public GridEditableItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x1700389B RID: 14491
		// (get) Token: 0x0600AF50 RID: 44880 RVA: 0x0025F6C4 File Offset: 0x0025D8C4
		// (set) Token: 0x0600AF51 RID: 44881 RVA: 0x0025F6CC File Offset: 0x0025D8CC
		public bool ExceptionHandled
		{
			get
			{
				return this._exceptionHandled;
			}
			set
			{
				this._exceptionHandled = value;
			}
		}

		// Token: 0x04002E2C RID: 11820
		private int _affectedRows;

		// Token: 0x04002E2D RID: 11821
		private Exception _exception;

		// Token: 0x04002E2E RID: 11822
		private bool _exceptionHandled;

		// Token: 0x04002E2F RID: 11823
		private GridEditableItem _item;
	}
}
