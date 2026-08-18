using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200116E RID: 4462
	public class GridSortCommandEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B5E2 RID: 46562 RVA: 0x0028084A File Offset: 0x0027EA4A
		public GridSortCommandEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "Sort", argument)
		{
			base.SetCommandSource(commandSource);
			this.sortExpression = (string)base.CommandArgument;
		}

		// Token: 0x0600B5E3 RID: 46563 RVA: 0x00280874 File Offset: 0x0027EA74
		public GridSortCommandEventArgs(GridItem item, object commandSource, object argument, GridSortOrder oldSortOrder, GridSortOrder newSortOrder) : base(item, commandSource, "Sort", argument)
		{
			base.SetCommandSource(commandSource);
			this.sortExpression = (string)base.CommandArgument;
			this.oldSortOrder = new GridSortOrder?(oldSortOrder);
			this.newSortOrder = new GridSortOrder?(newSortOrder);
		}

		// Token: 0x17003AD5 RID: 15061
		// (get) Token: 0x0600B5E4 RID: 46564 RVA: 0x002808C1 File Offset: 0x0027EAC1
		public string SortExpression
		{
			get
			{
				return this.sortExpression;
			}
		}

		// Token: 0x17003AD6 RID: 15062
		// (get) Token: 0x0600B5E5 RID: 46565 RVA: 0x002808CC File Offset: 0x0027EACC
		public GridSortOrder OldSortOrder
		{
			get
			{
				if (this.oldSortOrder != null)
				{
					return this.oldSortOrder.Value;
				}
				if (base.Item.OwnerTableView.SortExpressions.ContainsExpression(this.sortExpression))
				{
					GridSortExpression expression = base.Item.OwnerTableView.SortExpressions.GetExpression(this.sortExpression);
					if (expression != null)
					{
						return expression.SortOrder;
					}
				}
				return GridSortOrder.None;
			}
		}

		// Token: 0x17003AD7 RID: 15063
		// (get) Token: 0x0600B5E6 RID: 46566 RVA: 0x00280938 File Offset: 0x0027EB38
		public virtual GridSortOrder NewSortOrder
		{
			get
			{
				if (this.newSortOrder != null)
				{
					return this.newSortOrder.Value;
				}
				if (base.Item.OwnerTableView.SortExpressions.ContainsExpression(this.sortExpression))
				{
					GridSortExpression expression = base.Item.OwnerTableView.SortExpressions.GetExpression(this.sortExpression);
					if (expression != null)
					{
						if (base.Item.OwnerTableView.SortExpressions.AllowNaturalSort)
						{
							if (expression.SortOrder == GridSortOrder.None)
							{
								return GridSortOrder.Ascending;
							}
							if (expression.SortOrder == GridSortOrder.Ascending)
							{
								return GridSortOrder.Descending;
							}
							if (expression.SortOrder == GridSortOrder.Descending)
							{
								return GridSortOrder.None;
							}
						}
						else
						{
							if (expression.SortOrder == GridSortOrder.Ascending)
							{
								return GridSortOrder.Descending;
							}
							if (expression.SortOrder == GridSortOrder.Descending)
							{
								return GridSortOrder.Ascending;
							}
						}
					}
				}
				return GridSortOrder.Ascending;
			}
		}

		// Token: 0x0600B5E7 RID: 46567 RVA: 0x002809E8 File Offset: 0x0027EBE8
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
			base.Item.OwnerTableView.SortExpressions.ChangeSortOrder(this.sortExpression);
			base.Item.OwnerTableView.ClearEditItems();
			base.Item.OwnerTableView.ObtainDataSource(gridRebindReason);
			base.Item.OwnerTableView.DataBind();
		}

		// Token: 0x04002FF1 RID: 12273
		private GridSortOrder? oldSortOrder;

		// Token: 0x04002FF2 RID: 12274
		private GridSortOrder? newSortOrder;

		// Token: 0x04002FF3 RID: 12275
		private string sortExpression;
	}
}
