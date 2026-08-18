using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010D9 RID: 4313
	public class GridSelectCommandEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B0C5 RID: 45253 RVA: 0x002640C1 File Offset: 0x002622C1
		public GridSelectCommandEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "Select", argument)
		{
		}

		// Token: 0x0600B0C6 RID: 45254 RVA: 0x002640D1 File Offset: 0x002622D1
		public override void ExecuteCommand(object source)
		{
			base.Item.Selected = true;
			base.Item.OwnerTableView.TrackSelection(base.Item, true);
			base.Item.OwnerTableView.OwnerGrid.CallOnSelectedIndexChanged(EventArgs.Empty);
		}
	}
}
