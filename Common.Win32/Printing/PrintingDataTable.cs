using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace TechnoPro.Common.Win32.Printing
{
	// Token: 0x02000017 RID: 23
	public class PrintingDataTable
	{
		// Token: 0x06000081 RID: 129 RVA: 0x000048F4 File Offset: 0x00002AF4
		public PrintingDataTable()
		{
			this.printDocument = new PrintDocument();
			this.printDocument.BeginPrint += this.printDocument_BeginPrint;
			this.printDocument.PrintPage += this.printDocument_PrintPage;
			this.printDocument.EndPrint += this.printDocument_EndPrint;
			this.printDialog = new PrintDialog();
			this.printDialog.UseEXDialog = true;
			this.printDialog.Document = this.printDocument;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000498A File Offset: 0x00002B8A
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004992 File Offset: 0x00002B92
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000499C File Offset: 0x00002B9C
		public static void PrintDataTable(IWin32Window owner, string Title, DataTable TableToPrint, Font PrintFont, bool printPreview, bool landscape = true, int[] ColsToPrint = null)
		{
			if (TableToPrint == null)
			{
				return;
			}
			PrintingDataTable printingDataTable = new PrintingDataTable
			{
				Title = Title
			};
			int[] array;
			if (ColsToPrint == null)
			{
				List<int> list = new List<int>();
				for (int i = 0; i < TableToPrint.Columns.Count; i++)
				{
					if (TableToPrint.Columns[i].ColumnMapping != MappingType.Hidden)
					{
						list.Add(i);
					}
				}
				array = list.ToArray();
			}
			else
			{
				array = ColsToPrint;
			}
			printingDataTable.PrintDataTable(TableToPrint, array, landscape, PrintFont, printPreview, owner);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004A10 File Offset: 0x00002C10
		public void PrintDataTable(DataTable DataTableToPrint, int[] ColsToPrint, bool defaultLandscape, Font PrintFont, bool printPreview, IWin32Window owner)
		{
			this.printFont = PrintFont;
			this.printFontHeader = new Font(this.printFont, FontStyle.Bold);
			this.printDocument.DefaultPageSettings.Landscape = defaultLandscape;
			this.t = DataTableToPrint;
			if (ColsToPrint == null)
			{
				this.colsToPrint = new int[this.t.Columns.Count];
				for (int i = 0; i < this.t.Columns.Count; i++)
				{
					this.colsToPrint[i] = i;
				}
			}
			else
			{
				this.colsToPrint = ColsToPrint;
			}
			if (this.printDialog.ShowDialog() == DialogResult.OK)
			{
				if (printPreview)
				{
					PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
					printPreviewDialog.Document = this.printDocument;
					if (owner != null)
					{
						printPreviewDialog.ShowDialog(owner);
					}
					else
					{
						printPreviewDialog.ShowDialog();
					}
				}
				else
				{
					this.printDocument.Print();
				}
			}
			this.printFont.Dispose();
			this.printFontHeader.Dispose();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004AF7 File Offset: 0x00002CF7
		private void printDocument_BeginPrint(object sender, PrintEventArgs e)
		{
			this.currentDataRowIndex = 0;
			this.newDocument = true;
			this.maxWidths = null;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004B10 File Offset: 0x00002D10
		private void CalculateMaxWidths(Graphics g)
		{
			this.maxWidths = new int[this.colsToPrint.Length];
			for (int i = 0; i < this.colsToPrint.Length; i++)
			{
				int num = Convert.ToInt32(g.MeasureString(this.t.Columns[this.colsToPrint[i]].ColumnName, this.printFontHeader).Width);
				foreach (object obj in this.t.Rows)
				{
					string text = ((DataRow)obj)[i].ToString().Trim();
					int num2 = Convert.ToInt32(g.MeasureString(text, this.printFont).Width);
					if (num2 > num)
					{
						num = num2;
					}
				}
				this.maxWidths[i] = num;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004C10 File Offset: 0x00002E10
		private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Near;
			stringFormat.LineAlignment = StringAlignment.Center;
			stringFormat.Trimming = StringTrimming.EllipsisCharacter;
			int num = 100;
			int num2 = Convert.ToInt32(e.Graphics.MeasureString("wqzyt", this.printFontHeader).Height) + 6;
			int num3 = e.MarginBounds.Top;
			if (this.currentDataRowIndex == 0 && !string.IsNullOrEmpty(this.title))
			{
				e.Graphics.DrawString(this.title, this.printFontHeader, Brushes.Black, (float)e.MarginBounds.X, (float)num3);
				num3 += Convert.ToInt32(e.Graphics.MeasureString(this.title, this.printFont).Height) + 8;
			}
			if (this.maxWidths == null)
			{
				this.CalculateMaxWidths(e.Graphics);
			}
			for (int i = 0; i < this.colsToPrint.Length; i++)
			{
				int num4 = this.maxWidths[i];
				e.Graphics.FillRectangle(Brushes.AliceBlue, num, num3, num4, num2);
				e.Graphics.DrawRectangle(Pens.Black, num, num3, num4, num2);
				e.Graphics.DrawString(this.t.Columns[this.colsToPrint[i]].Caption, this.printFontHeader, Brushes.Black, (float)num, (float)(num3 + 3));
				num += num4;
			}
			num3 += num2;
			while (this.currentDataRowIndex < this.t.Rows.Count)
			{
				num = 100;
				DataRow dataRow = this.t.Rows[this.currentDataRowIndex];
				for (int j = 0; j < this.colsToPrint.Length; j++)
				{
					DataColumn column = this.t.Columns[j];
					int num5 = this.maxWidths[j];
					e.Graphics.DrawRectangle(Pens.Black, num, num3, num5, num2);
					e.Graphics.DrawString(dataRow[column].ToString(), this.printFont, Brushes.Black, (float)num, (float)(num3 + 3));
					num += num5;
				}
				this.currentDataRowIndex++;
				num3 += num2;
				if (this.y + num2 >= e.MarginBounds.Bottom)
				{
					break;
				}
			}
			e.HasMorePages = (this.currentDataRowIndex < this.t.Rows.Count);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004E80 File Offset: 0x00003080
		private string GetRowString(DataRow dr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (int columnIndex in this.colsToPrint)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(", ");
				}
				if (dr[columnIndex] != DBNull.Value)
				{
					if (dr[columnIndex] is DateTime)
					{
						stringBuilder.Append(((DateTime)dr[columnIndex]).ToString("MMM dd, yyyy"));
					}
					else
					{
						stringBuilder.Append(dr[columnIndex].ToString());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004F1E File Offset: 0x0000311E
		private void printDocument_EndPrint(object sender, PrintEventArgs e)
		{
		}

		// Token: 0x04000047 RID: 71
		private DataTable t;

		// Token: 0x04000048 RID: 72
		private int[] colsToPrint;

		// Token: 0x04000049 RID: 73
		private PrintDialog printDialog;

		// Token: 0x0400004A RID: 74
		private PrintDocument printDocument;

		// Token: 0x0400004B RID: 75
		private Font printFont;

		// Token: 0x0400004C RID: 76
		private Font printFontHeader;

		// Token: 0x0400004D RID: 77
		private int[] maxWidths;

		// Token: 0x0400004E RID: 78
		private bool newDocument;

		// Token: 0x0400004F RID: 79
		private int currentDataRowIndex;

		// Token: 0x04000050 RID: 80
		private int y;

		// Token: 0x04000051 RID: 81
		private string title = "";
	}
}
