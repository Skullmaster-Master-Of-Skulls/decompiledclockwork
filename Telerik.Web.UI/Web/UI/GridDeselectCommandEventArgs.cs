using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010DA RID: 4314
	public class GridDeselectCommandEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B0C7 RID: 45255 RVA: 0x00264110 File Offset: 0x00262310
		public GridDeselectCommandEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "Deselect", argument)
		{
		}

		// Token: 0x0600B0C8 RID: 45256 RVA: 0x00264120 File Offset: 0x00262320
		public override void ExecuteCommand(object source)
		{
			base.Item.Selected = false;
			base.Item.OwnerTableView.TrackSelection(base.Item, false);
			base.Item.OwnerTableView.OwnerGrid.CallOnSelectedIndexChanged(EventArgs.Empty);
		}
	}
}
