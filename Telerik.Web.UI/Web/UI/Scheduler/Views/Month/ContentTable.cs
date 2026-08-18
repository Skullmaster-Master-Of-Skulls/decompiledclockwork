using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02001A53 RID: 6739
	internal class ContentTable : SchedulerTable
	{
		// Token: 0x17004F5A RID: 20314
		// (get) Token: 0x0601057B RID: 66939 RVA: 0x003A5E7C File Offset: 0x003A407C
		public IEnumerable<TableCell> AllCells
		{
			get
			{
				foreach (object obj in this.Rows)
				{
					TableRow row = (TableRow)obj;
					foreach (object obj2 in row.Cells)
					{
						TableCell cell = (TableCell)obj2;
						yield return cell;
					}
				}
				yield break;
			}
		}

		// Token: 0x0601057C RID: 66940 RVA: 0x003A5E9C File Offset: 0x003A409C
		public ContentTable()
		{
			TableStyle tableStyle = (TableStyle)base.ControlStyle;
			tableStyle.CssClass = "rsContentTable";
			base.Style["table-layout"] = "fixed";
		}

		// Token: 0x0601057D RID: 66941 RVA: 0x003A5EDB File Offset: 0x003A40DB
		public void SyncCellHeight()
		{
			this.AddCellPadding(this.GetMaxRowHeight());
		}

		// Token: 0x0601057E RID: 66942 RVA: 0x003A5EE9 File Offset: 0x003A40E9
		public void SetMinimumCellHeight(int cellHeight)
		{
			this.AddCellPadding(cellHeight);
		}

		// Token: 0x0601057F RID: 66943 RVA: 0x003A5EF4 File Offset: 0x003A40F4
		public void SyncCellHeight(int rowIndex)
		{
			TableRow tableRow = this.Rows[rowIndex];
			int maxRowHeight = this.GetMaxRowHeight(rowIndex);
			foreach (object obj in tableRow.Cells)
			{
				TableCell cell = (TableCell)obj;
				ContentTable.AddCellPadding(cell, maxRowHeight);
			}
		}

		// Token: 0x06010580 RID: 66944 RVA: 0x003A5F68 File Offset: 0x003A4168
		public void SyncRowHeight(ContentTable sourceTable)
		{
			if (this.Rows.Count != sourceTable.Rows.Count)
			{
				return;
			}
			for (int i = 0; i < sourceTable.Rows.Count; i++)
			{
				TableRow tableRow = sourceTable.Rows[i];
				TableRow tableRow2 = this.Rows[i];
				if (tableRow.Cells.Count != tableRow2.Cells.Count)
				{
					return;
				}
				for (int j = 0; j < tableRow.Cells.Count; j++)
				{
					ContentTable.AddCellPadding(tableRow2.Cells[j], tableRow.Cells[j].Controls.Count);
					ContentTable.AddCellPadding(tableRow.Cells[j], tableRow2.Cells[j].Controls.Count);
				}
			}
		}

		// Token: 0x06010581 RID: 66945 RVA: 0x003A6041 File Offset: 0x003A4241
		public int GetMaxRowHeight(int rowIndex)
		{
			return ContentTable.GetMaxCellHeight(this.Rows[rowIndex].Cells);
		}

		// Token: 0x06010582 RID: 66946 RVA: 0x003A605C File Offset: 0x003A425C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Rows.Count > 0)
			{
				TableRow tableRow = this.Rows[0];
				tableRow.CssClass += " rsFirstRow";
				TableRow tableRow2 = this.Rows[this.Rows.Count - 1];
				tableRow2.CssClass += " rsLastRow";
			}
		}

		// Token: 0x06010583 RID: 66947 RVA: 0x003A60CC File Offset: 0x003A42CC
		private int GetMaxRowHeight()
		{
			return ContentTable.GetMaxCellHeight(this.AllCells);
		}

		// Token: 0x06010584 RID: 66948 RVA: 0x003A60DC File Offset: 0x003A42DC
		private static int GetMaxCellHeight(IEnumerable cells)
		{
			int num = 0;
			foreach (object obj in cells)
			{
				TableCell tableCell = (TableCell)obj;
				num = Math.Max(num, tableCell.Controls.Count);
			}
			return num;
		}

		// Token: 0x06010585 RID: 66949 RVA: 0x003A6140 File Offset: 0x003A4340
		private void AddCellPadding(int targetHeight)
		{
			foreach (TableCell cell in this.AllCells)
			{
				ContentTable.AddCellPadding(cell, targetHeight);
			}
		}

		// Token: 0x06010586 RID: 66950 RVA: 0x003A6190 File Offset: 0x003A4390
		private static void AddCellPadding(Control cell, int targetHeight)
		{
			for (int i = cell.Controls.Count; i < targetHeight; i++)
			{
				MonthViewCellWrapper child = new MonthViewCellWrapper(1);
				cell.Controls.Add(child);
			}
		}
	}
}
