using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduler.Views;

namespace Telerik.Web.UI
{
	// Token: 0x02001A48 RID: 6728
	internal class VerticalHeadersPanel : WebControl
	{
		// Token: 0x17004F34 RID: 20276
		// (get) Token: 0x0601051B RID: 66843 RVA: 0x003A48A4 File Offset: 0x003A2AA4
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17004F35 RID: 20277
		// (get) Token: 0x0601051C RID: 66844 RVA: 0x003A48A8 File Offset: 0x003A2AA8
		public SchedulerTable InnerTable
		{
			get
			{
				return this._innerTable;
			}
		}

		// Token: 0x0601051D RID: 66845 RVA: 0x003A48B0 File Offset: 0x003A2AB0
		public VerticalHeadersPanel()
		{
			this.CreateVerticalHeadersPanel();
		}

		// Token: 0x0601051E RID: 66846 RVA: 0x003A48C0 File Offset: 0x003A2AC0
		private void CreateVerticalHeadersPanel()
		{
			base.Style[HtmlTextWriterStyle.Overflow] = "hidden";
			base.Style[HtmlTextWriterStyle.Height] = "100%";
			this._innerTable = new SchedulerTable();
			this.InnerTable.CssClass = "rsVerticalHeaderTable";
			this.Controls.Add(this.InnerTable);
		}

		// Token: 0x0601051F RID: 66847 RVA: 0x003A4920 File Offset: 0x003A2B20
		public void AddHeader(SchedulerHeader header)
		{
			TableRow tableRow = new TableRow();
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			tableHeaderCell.Controls.Add(header);
			tableRow.Cells.Add(tableHeaderCell);
			VerticalHeadersPanel.AddCssClass(tableRow, header.CssClass);
			foreach (object obj in header.Style.Keys)
			{
				string key = (string)obj;
				tableRow.Style[key] = header.Style[key];
			}
			header.Style.Remove("height");
			header.Style.Remove(HtmlTextWriterStyle.Height);
			this.InnerTable.Rows.Add(tableRow);
			int rowIndex = this.InnerTable.Rows.GetRowIndex(tableRow);
			this.AddSubHeaders(header, rowIndex);
			foreach (SchedulerHeader schedulerHeader in header.SubHeaders)
			{
				tableHeaderCell.RowSpan += Math.Max(1, schedulerHeader.SubHeaders.Count);
			}
		}

		// Token: 0x06010520 RID: 66848 RVA: 0x003A4A70 File Offset: 0x003A2C70
		private int AddSubHeaders(SchedulerHeader header, int rowIndex)
		{
			foreach (SchedulerHeader schedulerHeader in header.SubHeaders)
			{
				if (rowIndex >= this.InnerTable.Rows.Count)
				{
					this.InnerTable.Rows.Add(new TableRow());
				}
				TableHeaderCell tableHeaderCell = new TableHeaderCell();
				tableHeaderCell.Controls.Add(schedulerHeader);
				TableRow tableRow = this.InnerTable.Rows[rowIndex];
				tableRow.Cells.Add(tableHeaderCell);
				VerticalHeadersPanel.AddCssClass(tableRow, schedulerHeader.CssClass);
				schedulerHeader.CssClass = string.Empty;
				foreach (object obj in schedulerHeader.Style.Keys)
				{
					string key = (string)obj;
					tableRow.Style[key] = schedulerHeader.Style[key];
				}
				schedulerHeader.Style.Remove("height");
				schedulerHeader.Style.Remove(HtmlTextWriterStyle.Height);
				foreach (SchedulerHeader schedulerHeader2 in schedulerHeader.SubHeaders)
				{
					tableHeaderCell.RowSpan += Math.Max(1, schedulerHeader2.SubHeaders.Count);
				}
				if (schedulerHeader.SubHeaders.Count > 0)
				{
					rowIndex = this.AddSubHeaders(schedulerHeader, rowIndex);
				}
				else
				{
					rowIndex++;
				}
			}
			return rowIndex;
		}

		// Token: 0x06010521 RID: 66849 RVA: 0x003A4C58 File Offset: 0x003A2E58
		private static void AddCssClass(WebControl currentRow, string cssClass)
		{
			if (!string.IsNullOrEmpty(cssClass))
			{
				if (string.IsNullOrEmpty(currentRow.CssClass))
				{
					currentRow.CssClass = cssClass;
					return;
				}
				currentRow.CssClass = currentRow.CssClass + " " + cssClass;
			}
		}

		// Token: 0x04004971 RID: 18801
		private SchedulerTable _innerTable;
	}
}
