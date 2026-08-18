using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B5C RID: 7004
	public sealed class RadTreeViewContextMenuEventArgs : EventArgs
	{
		// Token: 0x06010F79 RID: 69497 RVA: 0x003C13BA File Offset: 0x003BF5BA
		public RadTreeViewContextMenuEventArgs(RadTreeNode node, RadMenuItem menuItem)
		{
			this._node = node;
			this._menuItem = menuItem;
		}

		// Token: 0x170052CD RID: 21197
		// (get) Token: 0x06010F7A RID: 69498 RVA: 0x003C13D0 File Offset: 0x003BF5D0
		public RadMenuItem MenuItem
		{
			get
			{
				return this._menuItem;
			}
		}

		// Token: 0x170052CE RID: 21198
		// (get) Token: 0x06010F7B RID: 69499 RVA: 0x003C13D8 File Offset: 0x003BF5D8
		public RadTreeNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x04004BE2 RID: 19426
		private readonly RadMenuItem _menuItem;

		// Token: 0x04004BE3 RID: 19427
		private readonly RadTreeNode _node;
	}
}
