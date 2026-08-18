using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E3E RID: 3646
	public class RibbonBarApplicationMenuItemClickEventArgs : EventArgs
	{
		// Token: 0x17002BCF RID: 11215
		// (get) Token: 0x06008AA1 RID: 35489 RVA: 0x001F9E94 File Offset: 0x001F8094
		public RibbonBarApplicationMenuItemBase Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x06008AA2 RID: 35490 RVA: 0x001F9E9C File Offset: 0x001F809C
		public RibbonBarApplicationMenuItemClickEventArgs(RibbonBarApplicationMenuItemBase item)
		{
			this._item = item;
		}

		// Token: 0x040026C3 RID: 9923
		private RibbonBarApplicationMenuItemBase _item;
	}
}
