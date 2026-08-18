using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000C31 RID: 3121
	public class PivotGridAggregateLabelChangeEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x0600765F RID: 30303 RVA: 0x001B7894 File Offset: 0x001B5A94
		public PivotGridAggregateLabelChangeEventArgs(PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "AggregateChange", argument)
		{
		}

		// Token: 0x1700267B RID: 9851
		// (get) Token: 0x06007660 RID: 30304 RVA: 0x001B78A4 File Offset: 0x001B5AA4
		protected virtual RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.Item.OwnerPivotGrid;
			}
		}

		// Token: 0x06007661 RID: 30305 RVA: 0x001B78B4 File Offset: 0x001B5AB4
		public override void ExecuteCommand(object source)
		{
			this.OwnerPivotGrid.FireAggregateLabelChange(this);
			if (this.Canceled)
			{
				return;
			}
			string[] array = base.CommandArgument.ToString().Split(new char[]
			{
				';'
			});
			string s = array[0];
			int aggregatesPosition;
			if (!int.TryParse(s, out aggregatesPosition))
			{
				return;
			}
			string s2 = array[1];
			int aggregatesLevel;
			if (!int.TryParse(s2, out aggregatesLevel))
			{
				return;
			}
			this.OwnerPivotGrid.AggregatesPosition = (PivotGridAxis)aggregatesPosition;
			this.OwnerPivotGrid.AggregatesLevel = aggregatesLevel;
			PivotGridRebindReason rebindReason = PivotGridRebindReason.PostBackEvent;
			this.OwnerPivotGrid.ResetPivotModel();
			this.OwnerPivotGrid.ObtainDataSource(rebindReason, false);
			this.OwnerPivotGrid.DataBind();
		}
	}
}
