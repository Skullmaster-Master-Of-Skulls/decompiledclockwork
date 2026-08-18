using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A6D RID: 6765
	internal class SchedulerRowHeaderTable : SchedulerTable
	{
		// Token: 0x17004FA2 RID: 20386
		// (get) Token: 0x06010659 RID: 67161 RVA: 0x003A97C6 File Offset: 0x003A79C6
		// (set) Token: 0x0601065A RID: 67162 RVA: 0x003A97CE File Offset: 0x003A79CE
		public ISchedulerView SchedulerView
		{
			get
			{
				return this._view;
			}
			private set
			{
				this._view = value;
			}
		}

		// Token: 0x17004FA3 RID: 20387
		// (get) Token: 0x0601065B RID: 67163 RVA: 0x003A97D7 File Offset: 0x003A79D7
		// (set) Token: 0x0601065C RID: 67164 RVA: 0x003A97DF File Offset: 0x003A79DF
		public RadScheduler Owner
		{
			get
			{
				return this._owner;
			}
			private set
			{
				this._owner = value;
			}
		}

		// Token: 0x0601065D RID: 67165 RVA: 0x003A97E8 File Offset: 0x003A79E8
		public SchedulerRowHeaderTable(ISchedulerView view, RadScheduler owner)
		{
			this.CreateSchedulerRowHeaderTable(view, owner);
		}

		// Token: 0x0601065E RID: 67166 RVA: 0x003A97F8 File Offset: 0x003A79F8
		private void CreateSchedulerRowHeaderTable(ISchedulerView view, RadScheduler owner)
		{
			this.SchedulerView = view;
			this.Owner = owner;
			this.CssClass = "rsHoursTable";
			foreach (ViewHeader viewHeader in this.SchedulerView.RowHeaders)
			{
				List<TableRow> list = new List<TableRow>();
				TableRow tableRow = new TableRow();
				tableRow.Style[HtmlTextWriterStyle.Height] = this.SchedulerView.Owner.RowHeight + "px";
				list.Add(tableRow);
				TableHeaderCell tableHeaderCell = new TableHeaderCell();
				if (viewHeader.ColumnSpan > 1)
				{
					tableHeaderCell.ColumnSpan = viewHeader.ColumnSpan;
				}
				if (!string.IsNullOrEmpty(viewHeader.ClassName))
				{
					tableHeaderCell.CssClass = viewHeader.ClassName;
				}
				tableRow.Controls.Add(tableHeaderCell);
				if (viewHeader.SubHeaders.Count > 0)
				{
					if (viewHeader.SubHeaders.Count > 1)
					{
						tableHeaderCell.RowSpan = viewHeader.SubHeaders.Count;
					}
					this.AddSubheaders(list, viewHeader);
				}
				this.AddHeaderCellContent(tableHeaderCell, viewHeader);
				foreach (TableRow row in list)
				{
					this.Rows.Add(row);
				}
			}
		}

		// Token: 0x0601065F RID: 67167 RVA: 0x003A9984 File Offset: 0x003A7B84
		private void AddSubheaders(IList<TableRow> rows, ViewHeader header)
		{
			while (rows.Count < header.SubHeaders.Count)
			{
				rows.Add(new TableRow());
			}
			for (int i = 0; i < rows.Count; i++)
			{
				ViewHeader viewHeader = header.SubHeaders[i];
				TableHeaderCell tableHeaderCell = new TableHeaderCell();
				if (!string.IsNullOrEmpty(viewHeader.ClassName))
				{
					tableHeaderCell.CssClass = viewHeader.ClassName;
				}
				rows[i].Cells.Add(tableHeaderCell);
				this.AddHeaderCellContent(tableHeaderCell, viewHeader);
			}
		}

		// Token: 0x06010660 RID: 67168 RVA: 0x003A9A0C File Offset: 0x003A7C0C
		private void AddHeaderCellContent(Control headerCell, ViewHeader header)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rsHoursWrapper";
			headerCell.Controls.Add(webControl);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Div);
			webControl2.CssClass = "rsHoursContent";
			webControl.Controls.Add(webControl2);
			if (header.Resource != null)
			{
				SchedulerResourceContainer schedulerResourceContainer = new SchedulerResourceContainer(this.Owner);
				schedulerResourceContainer.Resource = header.Resource;
				header.Resource.HeaderControls.Add(schedulerResourceContainer);
				webControl2.Controls.Add(schedulerResourceContainer);
				this.Owner.ResourceHeaderTemplate.InstantiateIn(schedulerResourceContainer);
				return;
			}
			LiteralControl literalControl = new LiteralControl();
			literalControl.Text = header.Text;
			webControl2.Controls.Add(literalControl);
		}

		// Token: 0x0400499E RID: 18846
		private ISchedulerView _view;

		// Token: 0x0400499F RID: 18847
		private RadScheduler _owner;
	}
}
