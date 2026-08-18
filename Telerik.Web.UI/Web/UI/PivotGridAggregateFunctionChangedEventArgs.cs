using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000C30 RID: 3120
	public class PivotGridAggregateFunctionChangedEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x0600765C RID: 30300 RVA: 0x001B77E5 File Offset: 0x001B59E5
		public PivotGridAggregateFunctionChangedEventArgs(PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "AggregateFunctionChanged", argument)
		{
		}

		// Token: 0x1700267A RID: 9850
		// (get) Token: 0x0600765D RID: 30301 RVA: 0x001B77F5 File Offset: 0x001B59F5
		public RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.Item.OwnerPivotGrid;
			}
		}

		// Token: 0x0600765E RID: 30302 RVA: 0x001B7804 File Offset: 0x001B5A04
		public override void ExecuteCommand(object source)
		{
			string[] array = base.CommandArgument.ToString().Split(new char[]
			{
				';'
			});
			if (array.Length == 2)
			{
				array[0].ToString();
				PivotGridAggregateField pivotGridAggregateField = this.OwnerPivotGrid.Fields.GetFieldByUniqueName(array[0]) as PivotGridAggregateField;
				int aggregate;
				if (pivotGridAggregateField != null && int.TryParse(array[1].ToString(), out aggregate))
				{
					pivotGridAggregateField.Aggregate = (PivotGridAggregate)aggregate;
					this.OwnerPivotGrid.ResetPivotModel();
					this.OwnerPivotGrid.ObtainDataSource(PivotGridRebindReason.PostBackEvent);
					this.OwnerPivotGrid.DataBind();
				}
			}
		}
	}
}
