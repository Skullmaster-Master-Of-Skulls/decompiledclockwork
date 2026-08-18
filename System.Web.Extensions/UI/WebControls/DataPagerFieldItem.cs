using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000094 RID: 148
	public class DataPagerFieldItem : Control, INonBindingContainer, INamingContainer
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x0001C3FD File Offset: 0x0001A5FD
		public DataPagerFieldItem(DataPagerField field, DataPager pager)
		{
			this._field = field;
			this._pager = pager;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001C413 File Offset: 0x0001A613
		public DataPager Pager
		{
			get
			{
				return this._pager;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001C41B File Offset: 0x0001A61B
		public DataPagerField PagerField
		{
			get
			{
				return this._field;
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001C424 File Offset: 0x0001A624
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				DataPagerFieldCommandEventArgs args = new DataPagerFieldCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x04000252 RID: 594
		private DataPagerField _field;

		// Token: 0x04000253 RID: 595
		private DataPager _pager;
	}
}
