using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000C33 RID: 3123
	public class PivotGridExpandCollapseEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x06007664 RID: 30308 RVA: 0x001B7B62 File Offset: 0x001B5D62
		public PivotGridExpandCollapseEventArgs(PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "ExpandCollapse", argument)
		{
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x001B7B74 File Offset: 0x001B5D74
		public override void ExecuteCommand(object source)
		{
			if (this.Canceled)
			{
				return;
			}
			string value = base.CommandArgument.ToString().Split(new char[]
			{
				'_'
			})[1];
			string value2 = base.CommandArgument.ToString().Split(new char[]
			{
				'_'
			})[2];
			bool flag = base.CommandArgument.ToString().Split(new char[]
			{
				'_'
			})[0] == "0";
			RadPivotGrid ownerPivotGrid = this.Item.OwnerPivotGrid;
			if (flag)
			{
				ownerPivotGrid.rowGroupExpandCollapseSlot = new PivotGridGroupSlot(Convert.ToInt32(value), Convert.ToInt32(value2));
			}
			else
			{
				ownerPivotGrid.columnGroupExpandCollapseSlot = new PivotGridGroupSlot(Convert.ToInt32(value), Convert.ToInt32(value2));
			}
			PivotGridRebindReason rebindReason = PivotGridRebindReason.PostBackEvent;
			ownerPivotGrid.ObtainDataSource(rebindReason, false);
			ownerPivotGrid.DataBind();
		}
	}
}
