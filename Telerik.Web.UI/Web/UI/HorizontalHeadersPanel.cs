using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduler.Views;

namespace Telerik.Web.UI
{
	// Token: 0x02001A20 RID: 6688
	internal sealed class HorizontalHeadersPanel : WebControl
	{
		// Token: 0x17004E9F RID: 20127
		// (get) Token: 0x060103BB RID: 66491 RVA: 0x003A0CC5 File Offset: 0x0039EEC5
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17004EA0 RID: 20128
		// (get) Token: 0x060103BC RID: 66492 RVA: 0x003A0CC9 File Offset: 0x0039EEC9
		public SchedulerTable HeaderTable
		{
			get
			{
				return this._headerTable;
			}
		}

		// Token: 0x060103BD RID: 66493 RVA: 0x003A0CD4 File Offset: 0x0039EED4
		public HorizontalHeadersPanel()
		{
			this.CssClass = "rsInnerFix";
			this._headerTable = new SchedulerTable();
			this.HeaderTable.CssClass = "rsHorizontalHeaderTable";
			this.HeaderTable.Rows.Add(new TableRow());
			base.Style[HtmlTextWriterStyle.Overflow] = "hidden";
			this.Controls.Add(this.HeaderTable);
		}

		// Token: 0x060103BE RID: 66494 RVA: 0x003A0D48 File Offset: 0x0039EF48
		internal void AddHeader(SchedulerHeader header)
		{
			TableHeaderCell cell = HorizontalHeadersPanel.CreateHeaderCell(header);
			this.HeaderTable.Rows[0].Cells.Add(cell);
			this.AddSubHeaders(header, 1);
		}

		// Token: 0x060103BF RID: 66495 RVA: 0x003A0D84 File Offset: 0x0039EF84
		private void AddSubHeaders(SchedulerHeader header, int rowIndex)
		{
			foreach (SchedulerHeader header2 in header.SubHeaders)
			{
				TableHeaderCell cell = HorizontalHeadersPanel.CreateHeaderCell(header2);
				if (rowIndex >= this.HeaderTable.Rows.Count)
				{
					TableRow tableRow = new TableRow();
					this.HeaderTable.Rows.Add(tableRow);
					if (!header.SubHeadersVisible)
					{
						tableRow.CssClass = "rsHidden";
					}
				}
				this.HeaderTable.Rows[rowIndex].Cells.Add(cell);
				this.AddSubHeaders(header2, rowIndex + 1);
			}
		}

		// Token: 0x060103C0 RID: 66496 RVA: 0x003A0E3C File Offset: 0x0039F03C
		private static TableHeaderCell CreateHeaderCell(SchedulerHeader header)
		{
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			tableHeaderCell.Controls.Add(header);
			int columnSpan = HorizontalHeadersPanel.GetColumnSpan(header);
			if (columnSpan > 1)
			{
				tableHeaderCell.ColumnSpan = columnSpan;
			}
			return tableHeaderCell;
		}

		// Token: 0x060103C1 RID: 66497 RVA: 0x003A0E70 File Offset: 0x0039F070
		private static int GetColumnSpan(SchedulerHeader header)
		{
			if (header.SubHeaders.Count > 0)
			{
				int num = 0;
				foreach (SchedulerHeader header2 in header.SubHeaders)
				{
					num += HorizontalHeadersPanel.GetColumnSpan(header2);
				}
				return num;
			}
			return 1;
		}

		// Token: 0x0400492B RID: 18731
		private readonly SchedulerTable _headerTable;
	}
}
