using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001154 RID: 4436
	public class GridPageChangedEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B4A4 RID: 46244 RVA: 0x0027C6F7 File Offset: 0x0027A8F7
		public GridPageChangedEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "Page", argument)
		{
			base.SetCommandSource(commandSource);
		}

		// Token: 0x17003A52 RID: 14930
		// (get) Token: 0x0600B4A5 RID: 46245 RVA: 0x0027C70E File Offset: 0x0027A90E
		public int NewPageIndex
		{
			get
			{
				return this.newPageIndex;
			}
		}

		// Token: 0x0600B4A6 RID: 46246 RVA: 0x0027C718 File Offset: 0x0027A918
		public override void ExecuteCommand(object source)
		{
			string text = (string)base.CommandArgument;
			int num = base.Item.OwnerTableView.CurrentPageIndex;
			int pageSize = base.Item.OwnerTableView.PageSize;
			if (string.Compare(text, "ChangePageSize", true) == 0)
			{
				if ((base.Item.FindControl("ChangePageSizeTextBox") as RadNumericTextBox).Text.Length > 0)
				{
					pageSize = int.Parse((base.Item.FindControl("ChangePageSizeTextBox") as RadNumericTextBox).Text);
				}
			}
			else if (string.Compare(text, "GoToPage", true) == 0)
			{
				num = int.Parse((base.Item.FindControl("GoToPageTextBox") as RadNumericTextBox).Text) - 1;
			}
			else if (string.Compare(text, "Next", true) == 0)
			{
				num++;
				if (num > base.Item.OwnerTableView.PageCount - 1 && (base.Item.OwnerTableView.AllowCustomPaging || base.Item.OwnerTableView.OwnerGrid.AllowCustomPaging))
				{
					num = base.Item.OwnerTableView.PageCount - 1;
				}
				base.Item.OwnerTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToLast;
			}
			else if (string.Compare(text, "Prev", true) == 0)
			{
				num--;
				if (num < 0)
				{
					return;
				}
			}
			else if (string.Compare(text, "First", true) == 0)
			{
				num = 0;
			}
			else if (string.Compare(text, "Last", true) == 0)
			{
				base.Item.OwnerTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToLast;
				if (base.Item.OwnerTableView.AllowCustomPaging || base.Item.OwnerTableView.OwnerGrid.AllowCustomPaging)
				{
					num = base.Item.OwnerTableView.PageCount - 1;
				}
				else
				{
					num = int.MaxValue;
				}
				if (base.Item.OwnerTableView.OwnerGrid.EnableLinqExpressions || base.Item.OwnerTableView.IsDataSourceViewWithFiltering())
				{
					num = base.Item.OwnerTableView.PageCount - 1;
				}
			}
			else
			{
				try
				{
					num = int.Parse(text) - 1;
				}
				catch (Exception inner)
				{
					throw new GridException("Invalid event argument of Page command. Valid values are: Next, Prev, First, Last ot an integer", inner);
				}
			}
			if (string.Compare(text, "ChangePageSize", true) != 0)
			{
				this.newPageIndex = num;
				if (!base.Item.OwnerTableView.topSliderChanged || ((GridPagerItem)((RadSlider)source).NamingContainer).IsTopPager)
				{
					base.Item.OwnerTableView.OwnerGrid.CallOnPageIndexChanged(this);
				}
				if (base.Canceled)
				{
					return;
				}
			}
			GridRebindReason gridRebindReason = GridRebindReason.PostBackEvent;
			if (base.Item.OwnerTableView.IsClone)
			{
				gridRebindReason |= GridRebindReason.DetailTableBinding;
			}
			if (string.Compare(text, "ChangePageSize", true) == 0)
			{
				try
				{
					base.Item.OwnerTableView.PageSize = pageSize;
					if (base.Item.OwnerTableView == base.Item.OwnerTableView.OwnerGrid.MasterTableView)
					{
						base.Item.OwnerTableView.OwnerGrid.PageSize = pageSize;
					}
					base.Item.OwnerTableView.CurrentPageIndex = 0;
					goto IL_332;
				}
				catch (Exception inner2)
				{
					throw new GridException("The specified value cannot be set on PageSize", inner2);
				}
			}
			try
			{
				base.Item.OwnerTableView.CurrentPageIndex = num;
			}
			catch (Exception inner3)
			{
				throw new GridException("The specified value cannot be set on CurrentPageIndex", inner3);
			}
			IL_332:
			if (!base.Item.OwnerTableView.EnableViewState || !base.Item.OwnerTableView.OwnerGrid.EnableViewState)
			{
				base.Item.OwnerTableView.DataSource = null;
				base.Item.OwnerTableView.OwnerGrid.DataSource = null;
			}
			base.Item.OwnerTableView.ObtainDataSource(gridRebindReason);
			base.Item.OwnerTableView.ClearEditItems();
			base.Item.OwnerTableView.DataBind();
		}

		// Token: 0x04002F9B RID: 12187
		private int newPageIndex;
	}
}
