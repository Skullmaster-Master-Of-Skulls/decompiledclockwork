using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001A3E RID: 6718
	internal class SchedulerTopTable : SchedulerTable
	{
		// Token: 0x060104A0 RID: 66720 RVA: 0x003A3992 File Offset: 0x003A1B92
		public SchedulerTopTable()
		{
			this.CreateSchedulerTopTable();
		}

		// Token: 0x060104A1 RID: 66721 RVA: 0x003A39A7 File Offset: 0x003A1BA7
		private void CreateSchedulerTopTable()
		{
			this._rowHeaders = new List<TableCell>();
			this.Width = Unit.Percentage(100.0);
			this.CreateHeaderRow();
			this.CreateAllDayRow();
			this.CreateContentRow();
		}

		// Token: 0x060104A2 RID: 66722 RVA: 0x003A39DC File Offset: 0x003A1BDC
		private void CreateAllDayRow()
		{
			this._allDayRow = new TableRow();
			this.AllDayRow.Visible = false;
			this._allDayHeaderCell = new TableHeaderCell();
			this.AllDayHeaderCell.CssClass = "rsAllDayHeader";
			this._rowHeaders.Add(this.AllDayHeaderCell);
			this.AllDayRow.Cells.Add(this.AllDayHeaderCell);
			TableCell tableCell = new TableCell();
			tableCell.CssClass = "rsHorizontalHeaderWrapper";
			this.AllDayRow.Cells.Add(tableCell);
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.Style[HtmlTextWriterStyle.Overflow] = "hidden";
			webControl.Style[HtmlTextWriterStyle.Position] = "relative";
			tableCell.Controls.Add(webControl);
			this._allDayContentWrapper = new WebControl(HtmlTextWriterTag.Div);
			this._allDayContentWrapper.Style[HtmlTextWriterStyle.Overflow] = "hidden";
			this._allDayContentWrapper.Style[HtmlTextWriterStyle.Position] = "relative";
			this._allDayContentWrapper.CssClass = "rsInnerFix";
			webControl.Controls.Add(this.AllDayContentWrapper);
			this.Rows.Add(this.AllDayRow);
		}

		// Token: 0x060104A3 RID: 66723 RVA: 0x003A3B0C File Offset: 0x003A1D0C
		private void CreateContentRow()
		{
			TableRow tableRow = new TableRow();
			this._verticalHeadersWrapper = new TableCell();
			this.VerticalHeadersWrapper.CssClass = "rsVerticalHeaderWrapper";
			this._rowHeaders.Add(this.VerticalHeadersWrapper);
			tableRow.Cells.Add(this.VerticalHeadersWrapper);
			this._fixerWrapper = new WebControl(HtmlTextWriterTag.Div);
			this.VerticalHeaderFixerWrapper.Style[HtmlTextWriterStyle.Overflow] = "hidden";
			this.VerticalHeaderFixerWrapper.Style[HtmlTextWriterStyle.Position] = "relative";
			this.VerticalHeadersWrapper.Controls.Add(this.VerticalHeaderFixerWrapper);
			this._verticalHeaderPanel = new VerticalHeadersPanel();
			this.VerticalHeaderFixerWrapper.Controls.Add(this.VerticalHeaderPanel);
			this._contentWrapper = new TableCell();
			this.ContentWrapper.CssClass = "rsContentWrapper";
			this._contentScrollArea = new WebControl(HtmlTextWriterTag.Div);
			this.ContentScrollArea.CssClass = "rsContentScrollArea";
			this.ContentWrapper.Controls.Add(this.ContentScrollArea);
			tableRow.Cells.Add(this.ContentWrapper);
			this.Rows.Add(tableRow);
		}

		// Token: 0x060104A4 RID: 66724 RVA: 0x003A3C3C File Offset: 0x003A1E3C
		private void CreateHeaderRow()
		{
			this._columnHeaderRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.CssClass = "rsSpacerCell";
			tableCell.Controls.Add(new WebControl(HtmlTextWriterTag.Div));
			this._rowHeaders.Add(tableCell);
			this.ColumnHeaderRow.Cells.Add(tableCell);
			TableCell tableCell2 = new TableCell();
			tableCell2.CssClass = "rsHorizontalHeaderWrapper";
			this.ColumnHeaderRow.Cells.Add(tableCell2);
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			tableCell2.Controls.Add(webControl);
			this._horizontalHeaderPanel = new HorizontalHeadersPanel();
			webControl.Controls.Add(this.HorizontalHeaderPanel);
			this.ColumnHeaderRow.Visible = false;
			this.Rows.Add(this.ColumnHeaderRow);
		}

		// Token: 0x17004EF8 RID: 20216
		// (get) Token: 0x060104A5 RID: 66725 RVA: 0x003A3D06 File Offset: 0x003A1F06
		// (set) Token: 0x060104A6 RID: 66726 RVA: 0x003A3D13 File Offset: 0x003A1F13
		public bool ShowColumnHeaders
		{
			get
			{
				return this.ColumnHeaderRow.Visible;
			}
			set
			{
				this.ColumnHeaderRow.Visible = value;
			}
		}

		// Token: 0x17004EF9 RID: 20217
		// (get) Token: 0x060104A7 RID: 66727 RVA: 0x003A3D21 File Offset: 0x003A1F21
		// (set) Token: 0x060104A8 RID: 66728 RVA: 0x003A3D2C File Offset: 0x003A1F2C
		public bool ShowRowHeaders
		{
			get
			{
				return this._rowHeadersVisible;
			}
			set
			{
				foreach (TableCell tableCell in this._rowHeaders)
				{
					tableCell.Visible = value;
				}
				this._rowHeadersVisible = value;
			}
		}

		// Token: 0x17004EFA RID: 20218
		// (get) Token: 0x060104A9 RID: 66729 RVA: 0x003A3D88 File Offset: 0x003A1F88
		// (set) Token: 0x060104AA RID: 66730 RVA: 0x003A3D95 File Offset: 0x003A1F95
		public bool ShowAllDayRow
		{
			get
			{
				return this.AllDayRow.Visible;
			}
			set
			{
				this.AllDayRow.Visible = value;
			}
		}

		// Token: 0x17004EFB RID: 20219
		// (get) Token: 0x060104AB RID: 66731 RVA: 0x003A3DA3 File Offset: 0x003A1FA3
		public HorizontalHeadersPanel HorizontalHeaderPanel
		{
			get
			{
				return this._horizontalHeaderPanel;
			}
		}

		// Token: 0x17004EFC RID: 20220
		// (get) Token: 0x060104AC RID: 66732 RVA: 0x003A3DAB File Offset: 0x003A1FAB
		public VerticalHeadersPanel VerticalHeaderPanel
		{
			get
			{
				return this._verticalHeaderPanel;
			}
		}

		// Token: 0x17004EFD RID: 20221
		// (get) Token: 0x060104AD RID: 66733 RVA: 0x003A3DB3 File Offset: 0x003A1FB3
		public WebControl ContentScrollArea
		{
			get
			{
				return this._contentScrollArea;
			}
		}

		// Token: 0x17004EFE RID: 20222
		// (get) Token: 0x060104AE RID: 66734 RVA: 0x003A3DBB File Offset: 0x003A1FBB
		public TableHeaderCell AllDayHeaderCell
		{
			get
			{
				return this._allDayHeaderCell;
			}
		}

		// Token: 0x17004EFF RID: 20223
		// (get) Token: 0x060104AF RID: 66735 RVA: 0x003A3DC3 File Offset: 0x003A1FC3
		public WebControl AllDayContentWrapper
		{
			get
			{
				return this._allDayContentWrapper;
			}
		}

		// Token: 0x17004F00 RID: 20224
		// (get) Token: 0x060104B0 RID: 66736 RVA: 0x003A3DCB File Offset: 0x003A1FCB
		public TableRow ColumnHeaderRow
		{
			get
			{
				return this._columnHeaderRow;
			}
		}

		// Token: 0x17004F01 RID: 20225
		// (get) Token: 0x060104B1 RID: 66737 RVA: 0x003A3DD3 File Offset: 0x003A1FD3
		public TableRow AllDayRow
		{
			get
			{
				return this._allDayRow;
			}
		}

		// Token: 0x17004F02 RID: 20226
		// (get) Token: 0x060104B2 RID: 66738 RVA: 0x003A3DDB File Offset: 0x003A1FDB
		public WebControl VerticalHeaderFixerWrapper
		{
			get
			{
				return this._fixerWrapper;
			}
		}

		// Token: 0x17004F03 RID: 20227
		// (get) Token: 0x060104B3 RID: 66739 RVA: 0x003A3DE3 File Offset: 0x003A1FE3
		public TableCell VerticalHeadersWrapper
		{
			get
			{
				return this._verticalHeadersWrapper;
			}
		}

		// Token: 0x17004F04 RID: 20228
		// (get) Token: 0x060104B4 RID: 66740 RVA: 0x003A3DEB File Offset: 0x003A1FEB
		public TableCell ContentWrapper
		{
			get
			{
				return this._contentWrapper;
			}
		}

		// Token: 0x04004962 RID: 18786
		private TableRow _columnHeaderRow;

		// Token: 0x04004963 RID: 18787
		private bool _rowHeadersVisible = true;

		// Token: 0x04004964 RID: 18788
		private List<TableCell> _rowHeaders;

		// Token: 0x04004965 RID: 18789
		private TableRow _allDayRow;

		// Token: 0x04004966 RID: 18790
		private HorizontalHeadersPanel _horizontalHeaderPanel;

		// Token: 0x04004967 RID: 18791
		private VerticalHeadersPanel _verticalHeaderPanel;

		// Token: 0x04004968 RID: 18792
		private WebControl _contentScrollArea;

		// Token: 0x04004969 RID: 18793
		private TableHeaderCell _allDayHeaderCell;

		// Token: 0x0400496A RID: 18794
		private WebControl _allDayContentWrapper;

		// Token: 0x0400496B RID: 18795
		private WebControl _fixerWrapper;

		// Token: 0x0400496C RID: 18796
		private TableCell _verticalHeadersWrapper;

		// Token: 0x0400496D RID: 18797
		private TableCell _contentWrapper;
	}
}
