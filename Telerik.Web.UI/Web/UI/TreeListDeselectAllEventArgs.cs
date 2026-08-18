using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200120F RID: 4623
	public class TreeListDeselectAllEventArgs : TreeListCommandEventArgs
	{
		// Token: 0x0600BF1F RID: 48927 RVA: 0x002A5511 File Offset: 0x002A3711
		public TreeListDeselectAllEventArgs(TreeListItem item, object commandSource, object argument) : base(item, commandSource, "DeselectAll", argument)
		{
		}

		// Token: 0x0600BF20 RID: 48928 RVA: 0x002A5521 File Offset: 0x002A3721
		public override void ExecuteCommand(object source)
		{
			if (!this.Canceled)
			{
				this.Item.OwnerTreeList.DeselectAllItems();
			}
		}
	}
}
