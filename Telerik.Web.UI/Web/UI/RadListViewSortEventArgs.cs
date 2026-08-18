using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200194F RID: 6479
	public class RadListViewSortEventArgs : RadListViewCommandEventArgs
	{
		// Token: 0x0600FAA5 RID: 64165 RVA: 0x00386C7C File Offset: 0x00384E7C
		public RadListViewSortEventArgs(RadListView ownerListView, RadListViewItem item, object commandSource, object argument) : base(item, commandSource, "Sort", argument)
		{
			this._sortExpression = (string)base.CommandArgument;
			this._ownerListView = ownerListView;
		}

		// Token: 0x17004BBC RID: 19388
		// (get) Token: 0x0600FAA6 RID: 64166 RVA: 0x00386CA5 File Offset: 0x00384EA5
		protected virtual RadListView OwnerListView
		{
			get
			{
				return this._ownerListView ?? this.ListViewItem.OwnerListView;
			}
		}

		// Token: 0x17004BBD RID: 19389
		// (get) Token: 0x0600FAA7 RID: 64167 RVA: 0x00386CBC File Offset: 0x00384EBC
		public string SortExpression
		{
			get
			{
				return this._sortExpression;
			}
		}

		// Token: 0x17004BBE RID: 19390
		// (get) Token: 0x0600FAA8 RID: 64168 RVA: 0x00386CC4 File Offset: 0x00384EC4
		public RadListViewSortOrder OldSortOrder
		{
			get
			{
				RadListView ownerListView = this.OwnerListView;
				if (ownerListView.SortExpressions.ContainsExpression(this._sortExpression))
				{
					RadListViewSortExpression expression = ownerListView.SortExpressions.GetExpression(this._sortExpression);
					if (expression != null)
					{
						return expression.SortOrder;
					}
				}
				return RadListViewSortOrder.None;
			}
		}

		// Token: 0x17004BBF RID: 19391
		// (get) Token: 0x0600FAA9 RID: 64169 RVA: 0x00386D08 File Offset: 0x00384F08
		public RadListViewSortOrder NewSortOrder
		{
			get
			{
				RadListView ownerListView = this.OwnerListView;
				if (ownerListView.SortExpressions.ContainsExpression(this._sortExpression))
				{
					RadListViewSortExpression expression = ownerListView.SortExpressions.GetExpression(this._sortExpression);
					if (expression != null)
					{
						if (ownerListView.SortExpressions.AllowNaturalSort)
						{
							if (expression.SortOrder == RadListViewSortOrder.None)
							{
								return RadListViewSortOrder.Ascending;
							}
							if (expression.SortOrder == RadListViewSortOrder.Ascending)
							{
								return RadListViewSortOrder.Descending;
							}
							if (expression.SortOrder == RadListViewSortOrder.Descending)
							{
								return RadListViewSortOrder.None;
							}
						}
						else
						{
							if (expression.SortOrder == RadListViewSortOrder.Ascending)
							{
								return RadListViewSortOrder.Descending;
							}
							if (expression.SortOrder == RadListViewSortOrder.Descending)
							{
								return RadListViewSortOrder.Ascending;
							}
						}
					}
				}
				return RadListViewSortOrder.Ascending;
			}
		}

		// Token: 0x0600FAAA RID: 64170 RVA: 0x00386D88 File Offset: 0x00384F88
		public override void ExecuteCommand(object source)
		{
			this.OwnerListView.FireSorting(this);
			if (this.Canceled)
			{
				return;
			}
			this.OwnerListView.SortExpressions.ChangeSortOrder(this._sortExpression);
			this.OwnerListView.ClearEditItems();
			this.OwnerListView.ClearSelectedIndexes();
			this.OwnerListView.ObtainDataSource(RadListViewRebindReason.PostBackEvent);
			this.OwnerListView.DataBind();
		}

		// Token: 0x0600FAAB RID: 64171 RVA: 0x00386DF0 File Offset: 0x00384FF0
		public static void HandleSorting(RadListView ownerListView, object commandSource, string commandArgument)
		{
			RadListViewSortEventArgs radListViewSortEventArgs = new RadListViewSortEventArgs(ownerListView, null, commandSource, commandArgument);
			radListViewSortEventArgs.ExecuteCommand(commandSource);
		}

		// Token: 0x04004753 RID: 18259
		private readonly RadListView _ownerListView;

		// Token: 0x04004754 RID: 18260
		private string _sortExpression;
	}
}
