using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001170 RID: 4464
	public class GridHeaderSortCommandEventArgs : GridSortCommandEventArgs
	{
		// Token: 0x0600B5EA RID: 46570 RVA: 0x00280A93 File Offset: 0x0027EC93
		public GridHeaderSortCommandEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, argument)
		{
			this.SetFromArgument(argument.ToString());
		}

		// Token: 0x17003AD9 RID: 15065
		// (get) Token: 0x0600B5EB RID: 46571 RVA: 0x00280AAA File Offset: 0x0027ECAA
		public override GridSortOrder NewSortOrder
		{
			get
			{
				return this.newSortOrder;
			}
		}

		// Token: 0x0600B5EC RID: 46572 RVA: 0x00280AB4 File Offset: 0x0027ECB4
		public override void ExecuteCommand(object source)
		{
			GridRebindReason gridRebindReason = GridRebindReason.PostBackEvent;
			if (base.Item.OwnerTableView.IsClone)
			{
				gridRebindReason |= GridRebindReason.DetailTableBinding;
			}
			base.Item.OwnerTableView.OwnerGrid.CallOnSortCommand(this);
			if (base.Canceled)
			{
				return;
			}
			GridSortExpression gridSortExpression;
			base.Item.OwnerTableView.SortExpressions.TryGetExpression(base.CommandArgument.ToString(), out gridSortExpression);
			if (gridSortExpression == null)
			{
				base.Item.OwnerTableView.SortExpressions.AddSortExpression(base.CommandArgument.ToString());
			}
			else
			{
				gridSortExpression.SortOrder = this.NewSortOrder;
			}
			base.Item.OwnerTableView.ClearEditItems();
			base.Item.OwnerTableView.ObtainDataSource(gridRebindReason);
			base.Item.OwnerTableView.DataBind();
		}

		// Token: 0x0600B5ED RID: 46573 RVA: 0x00280B7D File Offset: 0x0027ED7D
		private void SetFromArgument(string argument)
		{
			if (argument.EndsWith(" ASC"))
			{
				this.newSortOrder = GridSortOrder.Ascending;
				return;
			}
			if (argument.EndsWith(" DESC"))
			{
				this.newSortOrder = GridSortOrder.Descending;
				return;
			}
			this.newSortOrder = GridSortOrder.None;
		}

		// Token: 0x04002FF5 RID: 12277
		private GridSortOrder newSortOrder;
	}
}
