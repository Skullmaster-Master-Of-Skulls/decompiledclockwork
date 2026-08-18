using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000C3E RID: 3134
	public class PivotGridSortEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x06007695 RID: 30357 RVA: 0x001B86F5 File Offset: 0x001B68F5
		public PivotGridSortEventArgs(RadPivotGrid ownerPivotGrid, PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "Sort", argument)
		{
			this.sortExpression = (string)base.CommandArgument;
			this.ownerPivotGrid = ownerPivotGrid;
		}

		// Token: 0x1700268E RID: 9870
		// (get) Token: 0x06007696 RID: 30358 RVA: 0x001B871E File Offset: 0x001B691E
		protected virtual RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.ownerPivotGrid ?? this.Item.OwnerPivotGrid;
			}
		}

		// Token: 0x1700268F RID: 9871
		// (get) Token: 0x06007697 RID: 30359 RVA: 0x001B8735 File Offset: 0x001B6935
		public string SortExpression
		{
			get
			{
				return this.sortExpression;
			}
		}

		// Token: 0x17002690 RID: 9872
		// (get) Token: 0x06007698 RID: 30360 RVA: 0x001B8740 File Offset: 0x001B6940
		public PivotGridSortOrder OldSortOrder
		{
			get
			{
				RadPivotGrid radPivotGrid = this.OwnerPivotGrid;
				if (radPivotGrid.SortExpressions.ContainsExpression(this.sortExpression))
				{
					PivotGridSortExpression expression = radPivotGrid.SortExpressions.GetExpression(this.sortExpression);
					if (expression != null)
					{
						return expression.SortOrder;
					}
				}
				return PivotGridSortOrder.Ascending;
			}
		}

		// Token: 0x17002691 RID: 9873
		// (get) Token: 0x06007699 RID: 30361 RVA: 0x001B8784 File Offset: 0x001B6984
		public PivotGridSortOrder NewSortOrder
		{
			get
			{
				RadPivotGrid radPivotGrid = this.OwnerPivotGrid;
				if (radPivotGrid.SortExpressions.ContainsExpression(this.sortExpression))
				{
					PivotGridSortExpression expression = radPivotGrid.SortExpressions.GetExpression(this.sortExpression);
					if (expression != null)
					{
						if (expression.SortOrder == PivotGridSortOrder.Ascending)
						{
							return PivotGridSortOrder.Descending;
						}
						if (expression.SortOrder == PivotGridSortOrder.Descending)
						{
							return PivotGridSortOrder.None;
						}
						if (expression.SortOrder == PivotGridSortOrder.None)
						{
							return PivotGridSortOrder.Ascending;
						}
					}
				}
				return PivotGridSortOrder.Ascending;
			}
		}

		// Token: 0x0600769A RID: 30362 RVA: 0x001B87E4 File Offset: 0x001B69E4
		public override void ExecuteCommand(object source)
		{
			this.OwnerPivotGrid.FireSorting(this);
			if (this.Canceled)
			{
				return;
			}
			this.OwnerPivotGrid.SortExpressions.ChangeSortOrder(this.sortExpression, this.ownerPivotGrid.AllowNaturalSort);
			this.OwnerPivotGrid.ResetPivotModel();
			this.OwnerPivotGrid.ObtainDataSource(PivotGridRebindReason.PostBackEvent);
			this.OwnerPivotGrid.DataBind();
		}

		// Token: 0x0600769B RID: 30363 RVA: 0x001B884C File Offset: 0x001B6A4C
		public static void HandleSorting(RadPivotGrid ownerPivotGrid, object commandSource, string commandArgument)
		{
			PivotGridSortEventArgs pivotGridSortEventArgs = new PivotGridSortEventArgs(ownerPivotGrid, null, commandSource, commandArgument);
			pivotGridSortEventArgs.ExecuteCommand(commandSource);
		}

		// Token: 0x04002097 RID: 8343
		private readonly RadPivotGrid ownerPivotGrid;

		// Token: 0x04002098 RID: 8344
		private string sortExpression;
	}
}
