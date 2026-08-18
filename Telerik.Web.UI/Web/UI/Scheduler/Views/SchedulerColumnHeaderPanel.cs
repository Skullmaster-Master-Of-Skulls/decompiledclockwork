using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A82 RID: 6786
	internal class SchedulerColumnHeaderPanel : WebControl
	{
		// Token: 0x17004FCB RID: 20427
		// (get) Token: 0x060106E7 RID: 67303 RVA: 0x003ABE6A File Offset: 0x003AA06A
		// (set) Token: 0x060106E8 RID: 67304 RVA: 0x003ABE72 File Offset: 0x003AA072
		private RadScheduler Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x17004FCC RID: 20428
		// (get) Token: 0x060106E9 RID: 67305 RVA: 0x003ABE7B File Offset: 0x003AA07B
		// (set) Token: 0x060106EA RID: 67306 RVA: 0x003ABE83 File Offset: 0x003AA083
		private ISchedulerView View
		{
			get
			{
				return this._view;
			}
			set
			{
				this._view = value;
			}
		}

		// Token: 0x17004FCD RID: 20429
		// (get) Token: 0x060106EB RID: 67307 RVA: 0x003ABE8C File Offset: 0x003AA08C
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17004FCE RID: 20430
		// (get) Token: 0x060106EC RID: 67308 RVA: 0x003ABE90 File Offset: 0x003AA090
		// (set) Token: 0x060106ED RID: 67309 RVA: 0x003ABE98 File Offset: 0x003AA098
		public TableRow ContentRow
		{
			get
			{
				return this._contentRow;
			}
			set
			{
				this._contentRow = value;
			}
		}

		// Token: 0x060106EE RID: 67310 RVA: 0x003ABEA1 File Offset: 0x003AA0A1
		public SchedulerColumnHeaderPanel(RadScheduler owner, ISchedulerView view, GroupingDirection groupingDirection, string additionalCssClass)
		{
			this.CreateSchedulerColumnHeaderPanel(owner, view, groupingDirection, additionalCssClass);
		}

		// Token: 0x060106EF RID: 67311 RVA: 0x003ABEB4 File Offset: 0x003AA0B4
		private void CreateSchedulerColumnHeaderPanel(RadScheduler owner, ISchedulerView view, GroupingDirection groupingDirection, string additionalCssClass)
		{
			this.Owner = owner;
			this.View = view;
			this._groupingDirection = groupingDirection;
			this.CssClass = string.Format("{0} {1}", "rsColumnHeader", owner.ActiveModel.CssClass);
			this.CreateColumnHeaderTable(additionalCssClass);
		}

		// Token: 0x060106F0 RID: 67312 RVA: 0x003ABEF4 File Offset: 0x003AA0F4
		private void CreateColumnHeaderTable(string additionalCssClass)
		{
			SchedulerTable schedulerTable = new SchedulerTable();
			this.Controls.Add(schedulerTable);
			schedulerTable.Style[HtmlTextWriterStyle.Width] = "100%";
			schedulerTable.CssClass = additionalCssClass;
			this.ContentRow = new TableRow();
			schedulerTable.Controls.Add(this.ContentRow);
			this.AddColumnHeaderSpacerCells(this.ContentRow);
			this.AddColumnHeaderCells(this.ContentRow);
			this.AddScrollerFillCell(this.ContentRow);
		}

		// Token: 0x060106F1 RID: 67313 RVA: 0x003ABF6C File Offset: 0x003AA16C
		private void AddColumnHeaderSpacerCells(Control container)
		{
			for (int i = 0; i < this.View.RowHeadersDepth; i++)
			{
				TableCell tableCell = new TableCell();
				container.Controls.Add(tableCell);
				if (i == this.View.RowHeadersDepth - 1)
				{
					tableCell.CssClass = "rsColumnHeaderLastSpacerCell";
				}
				else
				{
					tableCell.CssClass = "rsColumnHeaderSpacerCell";
				}
				Control child = new WebControl(HtmlTextWriterTag.Div);
				tableCell.Controls.Add(child);
			}
		}

		// Token: 0x060106F2 RID: 67314 RVA: 0x003ABFDD File Offset: 0x003AA1DD
		private void AddColumnHeaderCells(Control container)
		{
			if (this.GroupingDirection == GroupingDirection.Horizontal)
			{
				this.AddHorizontalColumnHeaderCells(container);
				return;
			}
			this.AddVerticalColumnHeaderCells(container);
		}

		// Token: 0x17004FCF RID: 20431
		// (get) Token: 0x060106F3 RID: 67315 RVA: 0x003ABFF7 File Offset: 0x003AA1F7
		private GroupingDirection GroupingDirection
		{
			get
			{
				return this._groupingDirection;
			}
		}

		// Token: 0x060106F4 RID: 67316 RVA: 0x003AC000 File Offset: 0x003AA200
		private void AddVerticalColumnHeaderCells(Control container)
		{
			TableCell tableCell = new TableCell();
			tableCell.Width = Unit.Percentage(100.0);
			tableCell.CssClass = "rsColumnHeaderCell";
			container.Controls.Add(tableCell);
			SchedulerTable schedulerTable = new SchedulerTable();
			schedulerTable.CssClass = "rsColumnHeaderTable";
			tableCell.Controls.Add(schedulerTable);
			TableRow tableRow = new TableRow();
			tableRow.CssClass = "rsColumnHeaderRow";
			schedulerTable.Rows.Add(tableRow);
			foreach (ViewHeader viewHeader in this.View.ColumnHeaders)
			{
				TableHeaderCell tableHeaderCell = new TableHeaderCell();
				this.InstantiateHeaderTemplate(viewHeader, tableHeaderCell);
				if (!string.IsNullOrEmpty(viewHeader.ClassName))
				{
					tableHeaderCell.CssClass = viewHeader.ClassName;
				}
				if (viewHeader.SubHeaders.Count > 0)
				{
					tableHeaderCell.ColumnSpan = viewHeader.SubHeaders.Count;
				}
				tableRow.Cells.Add(tableHeaderCell);
			}
		}

		// Token: 0x060106F5 RID: 67317 RVA: 0x003AC114 File Offset: 0x003AA314
		private void AddHorizontalColumnHeaderCells(Control container)
		{
			int num = 0;
			int num2 = 0;
			if (this.View.ColumnHeaders.Count > 0)
			{
				num = 100 / this.View.ColumnHeaders.Count;
				num2 = 100 % this.View.ColumnHeaders.Count;
			}
			foreach (ViewHeader viewHeader in this.View.ColumnHeaders)
			{
				int num3 = num;
				if (num2 > 0)
				{
					num3++;
					num2--;
				}
				TableCell tableCell = new TableCell();
				tableCell.Width = Unit.Percentage((double)num3);
				tableCell.CssClass = "rsColumnHeaderCell";
				container.Controls.Add(tableCell);
				SchedulerTable schedulerTable = new SchedulerTable();
				schedulerTable.CssClass = "rsColumnHeaderTable";
				tableCell.Controls.Add(schedulerTable);
				TableRow tableRow = new TableRow();
				tableRow.CssClass = "rsColumnHeaderRow";
				schedulerTable.Rows.Add(tableRow);
				TableHeaderCell tableHeaderCell = new TableHeaderCell();
				this.InstantiateHeaderTemplate(viewHeader, tableHeaderCell);
				tableRow.Cells.Add(tableHeaderCell);
				if (!string.IsNullOrEmpty(viewHeader.ClassName))
				{
					tableHeaderCell.CssClass = viewHeader.ClassName;
				}
				if (viewHeader.SubHeaders.Count > 0)
				{
					tableHeaderCell.ColumnSpan = viewHeader.SubHeaders.Count;
					TableHeaderCell tableHeaderCell2 = tableHeaderCell;
					tableHeaderCell2.CssClass += " rsLastCell";
				}
				if (this.View.ColumnHeadersDepth > 1)
				{
					TableRow tableRow2 = new TableRow();
					tableRow2.CssClass = "rsColumnHeaderRow";
					schedulerTable.Rows.Add(tableRow2);
					foreach (ViewHeader viewHeader2 in viewHeader.SubHeaders)
					{
						TableHeaderCell tableHeaderCell3 = new TableHeaderCell();
						this.InstantiateHeaderTemplate(viewHeader2, tableHeaderCell3);
						if (!string.IsNullOrEmpty(viewHeader2.ClassName))
						{
							tableHeaderCell3.CssClass = viewHeader2.ClassName;
						}
						tableRow2.Cells.Add(tableHeaderCell3);
					}
					if (tableRow2.Cells.Count > 0)
					{
						TableCell tableCell2 = tableRow2.Cells[tableRow2.Cells.Count - 1];
						tableCell2.CssClass += " rsLastCell";
					}
				}
			}
		}

		// Token: 0x060106F6 RID: 67318 RVA: 0x003AC398 File Offset: 0x003AA598
		private void InstantiateHeaderTemplate(ViewHeader header, TableCell headerCell)
		{
			if (header.Resource != null)
			{
				SchedulerResourceContainer schedulerResourceContainer = new SchedulerResourceContainer(this.Owner);
				schedulerResourceContainer.Resource = header.Resource;
				header.Resource.HeaderControls.Add(schedulerResourceContainer);
				headerCell.Controls.Add(schedulerResourceContainer);
				this.Owner.ResourceHeaderTemplate.InstantiateIn(schedulerResourceContainer);
				return;
			}
			headerCell.Text = HttpUtility.HtmlEncode(header.Text);
		}

		// Token: 0x060106F7 RID: 67319 RVA: 0x003AC40C File Offset: 0x003AA60C
		private void AddScrollerFillCell(Control container)
		{
			if (this.Owner.OverflowBehavior == OverflowBehavior.Scroll)
			{
				TableCell tableCell = new TableCell();
				container.Controls.Add(tableCell);
				tableCell.CssClass = "rsColumnHeaderLastCell";
				Control child = new WebControl(HtmlTextWriterTag.Div);
				tableCell.Controls.Add(child);
			}
		}

		// Token: 0x040049AE RID: 18862
		private RadScheduler _owner;

		// Token: 0x040049AF RID: 18863
		private TableRow _contentRow;

		// Token: 0x040049B0 RID: 18864
		private ISchedulerView _view;

		// Token: 0x040049B1 RID: 18865
		private GroupingDirection _groupingDirection;
	}
}
