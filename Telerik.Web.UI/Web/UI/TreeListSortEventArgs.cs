using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200121C RID: 4636
	public class TreeListSortEventArgs : TreeListCommandEventArgs
	{
		// Token: 0x0600BF50 RID: 48976 RVA: 0x002A5A67 File Offset: 0x002A3C67
		public TreeListSortEventArgs(RadTreeList ownerTreeList, TreeListItem item, object commandSource, object argument) : base(item, commandSource, "Sort", argument)
		{
			this._sortExpression = (string)base.CommandArgument;
			this._ownerTreeList = ownerTreeList;
		}

		// Token: 0x17003DB9 RID: 15801
		// (get) Token: 0x0600BF51 RID: 48977 RVA: 0x002A5A90 File Offset: 0x002A3C90
		protected virtual RadTreeList OwnerTreeList
		{
			get
			{
				return this._ownerTreeList ?? this.Item.OwnerTreeList;
			}
		}

		// Token: 0x17003DBA RID: 15802
		// (get) Token: 0x0600BF52 RID: 48978 RVA: 0x002A5AA7 File Offset: 0x002A3CA7
		public string SortExpression
		{
			get
			{
				return this._sortExpression;
			}
		}

		// Token: 0x17003DBB RID: 15803
		// (get) Token: 0x0600BF53 RID: 48979 RVA: 0x002A5AB0 File Offset: 0x002A3CB0
		public TreeListSortOrder OldSortOrder
		{
			get
			{
				RadTreeList ownerTreeList = this.OwnerTreeList;
				if (ownerTreeList.SortExpressions.ContainsExpression(this._sortExpression))
				{
					TreeListSortExpression expression = ownerTreeList.SortExpressions.GetExpression(this._sortExpression);
					if (expression != null)
					{
						return expression.SortOrder;
					}
				}
				return TreeListSortOrder.None;
			}
		}

		// Token: 0x17003DBC RID: 15804
		// (get) Token: 0x0600BF54 RID: 48980 RVA: 0x002A5AF4 File Offset: 0x002A3CF4
		public TreeListSortOrder NewSortOrder
		{
			get
			{
				RadTreeList ownerTreeList = this.OwnerTreeList;
				if (ownerTreeList.SortExpressions.ContainsExpression(this._sortExpression))
				{
					TreeListSortExpression expression = ownerTreeList.SortExpressions.GetExpression(this._sortExpression);
					if (expression != null)
					{
						if (ownerTreeList.SortExpressions.AllowNaturalSort)
						{
							if (expression.SortOrder == TreeListSortOrder.None)
							{
								return TreeListSortOrder.Ascending;
							}
							if (expression.SortOrder == TreeListSortOrder.Ascending)
							{
								return TreeListSortOrder.Descending;
							}
							if (expression.SortOrder == TreeListSortOrder.Descending)
							{
								return TreeListSortOrder.None;
							}
						}
						else
						{
							if (expression.SortOrder == TreeListSortOrder.Ascending)
							{
								return TreeListSortOrder.Descending;
							}
							if (expression.SortOrder == TreeListSortOrder.Descending)
							{
								return TreeListSortOrder.Ascending;
							}
						}
					}
				}
				return TreeListSortOrder.Ascending;
			}
		}

		// Token: 0x0600BF55 RID: 48981 RVA: 0x002A5B74 File Offset: 0x002A3D74
		public override void ExecuteCommand(object source)
		{
			this.OwnerTreeList.FireSorting(this);
			if (this.Canceled)
			{
				return;
			}
			this.OwnerTreeList.EditIndexes.Clear();
			this.OwnerTreeList.InsertIndexes.Clear();
			this.OwnerTreeList.SortExpressions.ChangeSortOrder(this._sortExpression);
			this.OwnerTreeList.ObtainDataSource(TreeListRebindReason.PostBackEvent);
			this.OwnerTreeList.DataBind();
		}

		// Token: 0x0600BF56 RID: 48982 RVA: 0x002A5BE4 File Offset: 0x002A3DE4
		public static void HandleSorting(RadTreeList ownerTreeList, object commandSource, string commandArgument)
		{
			TreeListSortEventArgs treeListSortEventArgs = new TreeListSortEventArgs(ownerTreeList, null, commandSource, commandArgument);
			treeListSortEventArgs.ExecuteCommand(commandSource);
		}

		// Token: 0x04003239 RID: 12857
		private readonly RadTreeList _ownerTreeList;

		// Token: 0x0400323A RID: 12858
		private string _sortExpression;
	}
}
