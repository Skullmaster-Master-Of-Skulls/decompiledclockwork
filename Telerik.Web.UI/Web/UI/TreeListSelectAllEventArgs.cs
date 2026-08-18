using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200121A RID: 4634
	public class TreeListSelectAllEventArgs : TreeListCommandEventArgs
	{
		// Token: 0x0600BF4C RID: 48972 RVA: 0x002A59EA File Offset: 0x002A3BEA
		public TreeListSelectAllEventArgs(TreeListItem item, object commandSource, object argument) : base(item, commandSource, "SelectAll", argument)
		{
		}

		// Token: 0x0600BF4D RID: 48973 RVA: 0x002A59FA File Offset: 0x002A3BFA
		public override void ExecuteCommand(object source)
		{
			if (!this.Canceled)
			{
				this.Item.OwnerTreeList.SelectAllItems();
			}
		}
	}
}
