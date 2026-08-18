using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000975 RID: 2421
	public class TreeMapItemDataBoundEventArguments : EventArgs
	{
		// Token: 0x06005BFF RID: 23551 RVA: 0x00118931 File Offset: 0x00116B31
		public TreeMapItemDataBoundEventArguments(TreeMapItem item)
		{
			this._item = item;
		}

		// Token: 0x17001E51 RID: 7761
		// (get) Token: 0x06005C00 RID: 23552 RVA: 0x00118940 File Offset: 0x00116B40
		public TreeMapItem Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x0400161D RID: 5661
		private TreeMapItem _item;
	}
}
