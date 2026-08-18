using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001171 RID: 4465
	public class GridClearSortCommandEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B5EE RID: 46574 RVA: 0x00280BB0 File Offset: 0x0027EDB0
		public GridClearSortCommandEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "ClearSort", argument)
		{
			this.SortExpression = (base.CommandArgument as string);
		}

		// Token: 0x17003ADA RID: 15066
		// (get) Token: 0x0600B5EF RID: 46575 RVA: 0x00280BD1 File Offset: 0x0027EDD1
		// (set) Token: 0x0600B5F0 RID: 46576 RVA: 0x00280BD9 File Offset: 0x0027EDD9
		public string SortExpression { get; private set; }

		// Token: 0x17003ADB RID: 15067
		// (get) Token: 0x0600B5F1 RID: 46577 RVA: 0x00280BE4 File Offset: 0x0027EDE4
		public GridSortOrder SortOrder
		{
			get
			{
				if (base.Item.OwnerTableView.SortExpressions.ContainsExpression(this.SortExpression))
				{
					return base.Item.OwnerTableView.SortExpressions.GetExpression(this.SortExpression).SortOrder;
				}
				return GridSortOrder.None;
			}
		}

		// Token: 0x0600B5F2 RID: 46578 RVA: 0x00280C30 File Offset: 0x0027EE30
		public override void ExecuteCommand(object source)
		{
			if (!string.IsNullOrEmpty(this.SortExpression) && !base.Item.OwnerTableView.SortExpressions.ContainsExpression(this.SortExpression))
			{
				return;
			}
			if (string.IsNullOrEmpty(this.SortExpression))
			{
				base.Item.OwnerTableView.SortExpressions.Clear();
			}
			else
			{
				base.Item.OwnerTableView.SortExpressions.RemoveSortExpression(base.Item.OwnerTableView.SortExpressions.GetExpression(this.SortExpression));
			}
			base.Item.OwnerTableView.ClearEditItems();
			base.Item.OwnerTableView.ObtainDataSource(base.Item.OwnerTableView.IsClone ? GridRebindReason.DetailTableBinding : GridRebindReason.PostBackEvent);
			base.Item.OwnerTableView.DataBind();
		}
	}
}
