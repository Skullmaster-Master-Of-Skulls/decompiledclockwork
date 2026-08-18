using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000832 RID: 2098
	internal abstract class SchedulerRenderer : ISchedulerRenderer
	{
		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x06004DB4 RID: 19892
		public abstract bool ShouldRenderFooter { get; }

		// Token: 0x06004DB5 RID: 19893
		protected abstract void CreateNavigationPane(Control container);

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x06004DB6 RID: 19894
		protected abstract RadScheduler Owner { get; }

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x06004DB7 RID: 19895
		// (set) Token: 0x06004DB8 RID: 19896
		public abstract ISchedulerView View { get; protected set; }

		// Token: 0x06004DB9 RID: 19897 RVA: 0x000F3998 File Offset: 0x000F1B98
		public virtual Control GetContent()
		{
			WebControl webControl = this.CreateTopWrap();
			if (this.Owner.ShowHeader)
			{
				this.CreateNavigationPane(webControl);
			}
			webControl.Controls.Add(this.GetInnerContent());
			return webControl;
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x000F39D4 File Offset: 0x000F1BD4
		public virtual Control GetInnerContent()
		{
			return new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "DUMMY"
			};
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x000F39F8 File Offset: 0x000F1BF8
		protected virtual void SetContentTableWidth(Table contentTable)
		{
			string value = string.Empty;
			int num = 0;
			foreach (object obj in contentTable.Rows)
			{
				TableRow tableRow = (TableRow)obj;
				num = Math.Max(num, tableRow.Cells.Count);
			}
			if (this.Owner.UseHorizontalScrolling)
			{
				double value2 = this.Owner.ColumnWidth.Value * (double)num;
				value = SchedulerUnit.GetValue(value2, this.Owner.ColumnWidth.Type);
			}
			else if (HttpContext.Current != null)
			{
				HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
				if (!browser.IsBrowser("IE") || browser.MajorVersion >= 8)
				{
					value = Unit.Percentage(100.0).ToString();
				}
			}
			contentTable.Style[HtmlTextWriterStyle.Width] = value;
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x000F3B0C File Offset: 0x000F1D0C
		protected static SchedulerTopTable CreateTopTable(Control container, string contentCssClass)
		{
			SchedulerTopTable schedulerTopTable = new SchedulerTopTable();
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.Controls.Add(schedulerTopTable);
			webControl.CssClass = "rsContent " + contentCssClass;
			container.Controls.Add(webControl);
			return schedulerTopTable;
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x000F3B51 File Offset: 0x000F1D51
		protected void SetTableHeight(Table table, int rowNum, Unit rowHeight)
		{
			this.SetTableHeight(table, rowNum, rowHeight, 0);
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x000F3B60 File Offset: 0x000F1D60
		protected void SetTableHeight(Table table, int rowNum, Unit rowHeight, int totalBorderHeight)
		{
			double value = rowHeight.Value * (double)rowNum + (double)totalBorderHeight;
			table.Style[HtmlTextWriterStyle.Height] = SchedulerUnit.GetValue(value, rowHeight.Type);
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x000F3B98 File Offset: 0x000F1D98
		protected virtual void SetScrollAreaOverflow(SchedulerTopTable topTable)
		{
			if (this.Owner.OverflowBehavior == OverflowBehavior.Scroll)
			{
				topTable.ContentScrollArea.Style[HtmlTextWriterStyle.OverflowY] = "scroll";
			}
			else if (this.Owner.OverflowBehavior == OverflowBehavior.Auto)
			{
				topTable.ContentScrollArea.Style[HtmlTextWriterStyle.OverflowY] = "auto";
			}
			if (this.Owner.UseHorizontalScrolling)
			{
				topTable.ContentScrollArea.Style[HtmlTextWriterStyle.OverflowX] = "scroll";
			}
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x000F3C18 File Offset: 0x000F1E18
		protected void AddColumnHeaders(SchedulerTopTable topTable)
		{
			topTable.ShowColumnHeaders = true;
			foreach (ViewHeader header in this.View.ColumnHeaders)
			{
				topTable.HorizontalHeaderPanel.AddHeader(this.CreateSchedulerHeader(header));
			}
			topTable.HorizontalHeaderPanel.HeaderTable.Style[HtmlTextWriterStyle.Height] = this.HorizontalHeaderHeight.ToString();
			this.SetContentTableWidth(topTable.HorizontalHeaderPanel.HeaderTable);
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x000F3CB8 File Offset: 0x000F1EB8
		protected void AddHorizontalHeaders(SchedulerTopTable topTable)
		{
			topTable.ShowColumnHeaders = true;
			foreach (ViewHeader viewHeader in this.View.ColumnHeaders)
			{
				SchedulerHeader schedulerHeader = this.CreateSchedulerHeader(viewHeader);
				this.AddSubHeaders(viewHeader, schedulerHeader);
				topTable.HorizontalHeaderPanel.AddHeader(schedulerHeader);
			}
			Table headerTable = topTable.HorizontalHeaderPanel.HeaderTable;
			int num = 0;
			foreach (object obj in headerTable.Rows)
			{
				TableRow tableRow = (TableRow)obj;
				if (!tableRow.CssClass.Contains("rsHidden"))
				{
					num++;
				}
			}
			this.SetTableHeight(headerTable, num, this.HorizontalHeaderHeight);
			this.SetContentTableWidth(topTable.HorizontalHeaderPanel.HeaderTable);
		}

		// Token: 0x06004DC2 RID: 19906 RVA: 0x000F3DB8 File Offset: 0x000F1FB8
		private void AddSubHeaders(ViewHeader viewHeader, SchedulerHeader schedulerHeader)
		{
			foreach (ViewHeader viewHeader2 in viewHeader.SubHeaders)
			{
				SchedulerHeader schedulerHeader2 = this.CreateSchedulerHeader(viewHeader2);
				schedulerHeader.SubHeaders.Add(schedulerHeader2);
				this.AddSubHeaders(viewHeader2, schedulerHeader2);
			}
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x000F3E1C File Offset: 0x000F201C
		protected virtual SchedulerHeader CreateSchedulerHeader(ViewHeader header)
		{
			SchedulerHeader result;
			if (header.Resource != null && !this.Owner.DesignMode)
			{
				SchedulerResourceContainer schedulerResourceContainer = new SchedulerResourceContainer(this.Owner);
				schedulerResourceContainer.Resource = header.Resource;
				header.Resource.HeaderControls.Add(schedulerResourceContainer);
				result = new SchedulerHeader(schedulerResourceContainer);
				this.Owner.ResourceHeaderTemplate.InstantiateIn(schedulerResourceContainer);
			}
			else
			{
				result = new SchedulerHeader(header.Text, header.SubHeadersVisible);
			}
			return result;
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x000F3E9A File Offset: 0x000F209A
		protected virtual void SetTopTableStyles(SchedulerTopTable topTable)
		{
			this.SetRowHeadersWidth(topTable);
			if (!this.Owner.UseHorizontalScrolling)
			{
				topTable.ContentWrapper.Style[HtmlTextWriterStyle.Width] = "100%";
			}
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x000F3EC8 File Offset: 0x000F20C8
		private void SetRowHeadersWidth(SchedulerTopTable topTable)
		{
			if (topTable.VerticalHeaderPanel.InnerTable.Rows.Count <= 0)
			{
				return;
			}
			if (this.Owner.RowHeaderWidth == Unit.Pixel(52))
			{
				return;
			}
			SchedulerTable innerTable = topTable.VerticalHeaderPanel.InnerTable;
			innerTable.CssClass += " rsVerticalHeaderSized";
			int count = topTable.VerticalHeaderPanel.InnerTable.Rows[0].Cells.Count;
			Unit width = new Unit(this.Owner.RowHeaderWidth.Value * (double)count, this.Owner.RowHeaderWidth.Type);
			if (count > 1)
			{
				foreach (object obj in topTable.VerticalHeaderPanel.InnerTable.Rows[0].Cells)
				{
					TableCell tableCell = (TableCell)obj;
					tableCell.Width = this.Owner.RowHeaderWidth;
				}
			}
			if (this.Owner.ResolvedRenderMode == RenderMode.Mobile)
			{
				using (IEnumerator enumerator2 = topTable.Rows.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						TableRow tableRow = (TableRow)obj2;
						if (tableRow.Cells.Count > 0)
						{
							tableRow.Cells[0].Width = this.Owner.RowHeaderWidth;
						}
					}
					return;
				}
			}
			foreach (object obj3 in topTable.Rows)
			{
				TableRow tableRow2 = (TableRow)obj3;
				if (tableRow2.Cells.Count > 0 && tableRow2.Cells[0].Controls.Count > 0)
				{
					WebControl webControl = (WebControl)tableRow2.Cells[0].Controls[0];
					webControl.Width = width;
				}
			}
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x000F4110 File Offset: 0x000F2310
		protected WebControl CreateTopWrap()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rsTopWrap";
			if (this.Owner.OverflowBehavior == OverflowBehavior.Expand)
			{
				WebControl webControl2 = webControl;
				webControl2.CssClass += " rsOverflowExpand";
			}
			return webControl;
		}

		// Token: 0x06004DC7 RID: 19911 RVA: 0x000F4155 File Offset: 0x000F2355
		protected HeaderControlFactory GetHeaderFactory(string dateLabel, RadScheduler owner)
		{
			return new HeaderControlFactory(dateLabel, owner);
		}

		// Token: 0x04001367 RID: 4967
		protected const string AnchorDateFormat = "yyyy-MM-dd";

		// Token: 0x04001368 RID: 4968
		protected readonly Unit HorizontalHeaderHeight = Unit.Pixel(25);
	}
}
