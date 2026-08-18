using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Export;
using Telerik.Web.UI.ExportInfrastructure;

namespace Telerik.Web.UI
{
	// Token: 0x02001221 RID: 4641
	internal class TreeListExportInfrastructureRenderer : IDisposable
	{
		// Token: 0x17003DBD RID: 15805
		// (get) Token: 0x0600BF6D RID: 49005 RVA: 0x002A62CF File Offset: 0x002A44CF
		internal Font DefaultFont
		{
			get
			{
				if (this._defaultFont == null)
				{
					this._defaultFont = new Font("Arial", 10f);
				}
				return this._defaultFont;
			}
		}

		// Token: 0x0600BF6E RID: 49006 RVA: 0x002A62F4 File Offset: 0x002A44F4
		internal TreeListExportInfrastructureRenderer(RadTreeList treeList, ExportFormat exportType)
		{
			this.treeList = treeList;
			this.workbook = new ExportStructure();
			this.workbook.ColumnWidthUnit = ExportUnitType.Pixel;
			this.workbook.RowHeightUnit = ExportUnitType.Pixel;
			this.baseWorksheet = new Telerik.Web.UI.ExportInfrastructure.Table();
			this.workbook.ColumnWidthUnit = ExportUnitType.Pixel;
			this.workbook.RowHeightUnit = ExportUnitType.Pixel;
			if (exportType == ExportFormat.Excel || exportType == ExportFormat.ExcelXlsx)
			{
				this.baseWorksheet.Title = treeList.ExportSettings.Excel.WorksheetName;
			}
			this.workbook.Tables.Add(this.baseWorksheet);
			this.currentContext = HttpContext.Current;
			if (exportType == ExportFormat.Excel || exportType == ExportFormat.ExcelXlsx)
			{
				this.baseWorksheet.FooterText = treeList.ExportSettings.Excel.PageFooter;
				this.baseWorksheet.HeaderText = treeList.ExportSettings.Excel.PageHeader;
				this.baseWorksheet.ShowGridlines = treeList.ExportSettings.Excel.ShowGridlines;
				this.baseWorksheet.TopMargin = treeList.ExportSettings.Excel.PageTopMargin;
				this.baseWorksheet.BottomMargin = treeList.ExportSettings.Excel.PageBottomMargin;
				this.baseWorksheet.LeftMargin = treeList.ExportSettings.Excel.PageLeftMargin;
				this.baseWorksheet.RightMargin = treeList.ExportSettings.Excel.PageRightMargin;
				this.baseWorksheet.Landscape = treeList.ExportSettings.Excel.RotatePaper;
				this.baseWorksheet.PageSize = treeList.ExportSettings.Excel.PaperSize;
				return;
			}
			if (exportType == ExportFormat.Word)
			{
				this.baseWorksheet.FooterText = treeList.ExportSettings.Word.PageFooter;
				this.baseWorksheet.HeaderText = treeList.ExportSettings.Word.PageHeader;
				this.baseWorksheet.ShowGridlines = treeList.ExportSettings.Word.ShowGridlines;
				this.baseWorksheet.TopMargin = treeList.ExportSettings.Word.PageTopMargin;
				this.baseWorksheet.BottomMargin = treeList.ExportSettings.Word.PageBottomMargin;
				this.baseWorksheet.LeftMargin = treeList.ExportSettings.Word.PageLeftMargin;
				this.baseWorksheet.RightMargin = treeList.ExportSettings.Word.PageRightMargin;
				this.baseWorksheet.Landscape = treeList.ExportSettings.Word.RotatePaper;
				this.baseWorksheet.PageSize = treeList.ExportSettings.Word.PaperSize;
			}
		}

		// Token: 0x0600BF6F RID: 49007 RVA: 0x002A6591 File Offset: 0x002A4791
		internal TreeListExportInfrastructureRenderer(RadTreeList treeList) : this(treeList, ExportFormat.Excel)
		{
		}

		// Token: 0x0600BF70 RID: 49008 RVA: 0x002A65B8 File Offset: 0x002A47B8
		internal byte[] Render()
		{
			TableRowCollection rows = this.treeList.GetTreeListTable().Rows;
			int num = -1;
			using (List<TreeListHierarchyIndex>.Enumerator enumerator = this.treeList.ExpandedIndexes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TreeListHierarchyIndex expIndex = enumerator.Current;
					TreeListDataItem treeListDataItem = this.treeList.Items.Find((TreeListDataItem hi) => hi.HierarchyIndex == expIndex);
					if (treeListDataItem != null && treeListDataItem.CanExpand && expIndex.NestedLevel > num)
					{
						num = expIndex.NestedLevel;
					}
				}
			}
			for (int i = 0; i <= num + 1; i++)
			{
				this.baseWorksheet.Columns.AddColumn(i + 1);
			}
			int num2 = -1;
			for (int j = 0; j < rows.Count; j++)
			{
				TreeListItem treeListItem = rows[j] as TreeListItem;
				int num3 = 0;
				if (!treeListItem.IsExportable)
				{
					treeListItem.Visible = false;
				}
				if (num2 == -1 && treeListItem.Visible)
				{
					num2 = j;
				}
				if (treeListItem.Visible)
				{
					int num4 = j - num2;
					treeListItem.PrepareItemStyle();
					if (treeListItem.Height != Unit.Empty)
					{
						this.baseWorksheet.Rows[num4 + 1].Height = Utils.GetPixelsPerUnit(treeListItem.Height);
					}
					else
					{
						this.baseWorksheet.Rows.AddRow(num4 + 1);
					}
					int expandCollapseCellIndex = this.GetExpandCollapseCellIndex(treeListItem);
					int count = treeListItem.Cells.Count;
					int num5 = 0;
					for (int k = 0; k < count; k++)
					{
						TableCell tableCell = treeListItem.Cells[k];
						TreeListColumn columnByTableCell = this.GetColumnByTableCell(tableCell);
						if (columnByTableCell != null && (!columnByTableCell.Visible || !columnByTableCell.Display) && (!(treeListItem is TreeListPagerItem) || k != 0))
						{
							num5++;
						}
						else
						{
							if (j == num2)
							{
								int columnSpan = treeListItem.Cells[0].ColumnSpan;
								if (columnByTableCell != null && columnByTableCell.HeaderStyle.Width != Unit.Empty)
								{
									this.baseWorksheet.Columns[k + columnSpan].Width = Utils.GetPixelsPerUnit(columnByTableCell.HeaderStyle.Width);
								}
								else
								{
									this.baseWorksheet.Columns.AddColumn(k + 1);
								}
							}
							Cell cell = this.baseWorksheet.Cells[k + num3 - num5 + 1, num4 + 1];
							cell.Value = this.ParseCellContent(tableCell, k + num3 - num5, num4, expandCollapseCellIndex);
							this.PrepareEICellStyle(cell, tableCell);
							int columnSpan2 = tableCell.ColumnSpan;
							if (columnSpan2 > 1)
							{
								int num6 = k + columnSpan2;
								for (int l = k + 1; l <= num6; l++)
								{
									Cell cell2 = this.baseWorksheet.Cells[l - num5 + 1, num4 + 1];
									this.DuplicateCellStyle(this.baseWorksheet.Cells[k - num5 + 1, num4 + 1], cell2);
								}
								Cell cell3 = this.workbook.Tables[0].Cells[k - num5 + 1, num4 + 1];
								cell3.Colspan = columnSpan2;
								cell3.Rowspan = 1;
								num3 += columnSpan2 - 1;
							}
						}
					}
				}
			}
			this.treeList.CallOnInfrastructureExporting(new TreeListInfrastructureExportingEventArgs(this.workbook, this.treeList.CurrentExportFormat.Value));
			byte[] result = null;
			switch (this.treeList.CurrentExportFormat.Value)
			{
			case ExportFormat.Excel:
				result = new XlsBiffRenderer(this.workbook).Render();
				break;
			case ExportFormat.ExcelXlsx:
				result = new XlsxRenderer(this.workbook).Render(null);
				break;
			case ExportFormat.Word:
				result = new DocxRenderer(this.workbook).Render();
				break;
			}
			return result;
		}

		// Token: 0x0600BF71 RID: 49009 RVA: 0x002A69DC File Offset: 0x002A4BDC
		private int GetExpandCollapseCellIndex(TableRow currentRow)
		{
			Control control = currentRow.FindControl("ExpandCollapseButton");
			int result = -1;
			if (control != null)
			{
				TableCell currentCell = control.Parent as TableCell;
				result = this.GetCellIndex(currentCell);
			}
			return result;
		}

		// Token: 0x0600BF72 RID: 49010 RVA: 0x002A6A10 File Offset: 0x002A4C10
		private void DecorateExpandCollapseCell(TableCell currentCell, int col, int row)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			Unit width = Unit.Empty;
			Unit width2 = Unit.Empty;
			Unit height = Unit.Empty;
			Unit height2 = Unit.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			bool fitToCell = true;
			if (this.treeList.CurrentExportFormat == ExportFormat.Excel || this.treeList.CurrentExportFormat == ExportFormat.ExcelXlsx)
			{
				TreeListExcelExpandCollapseCellStyle expandCollapseCellStyle = this.treeList.ExportSettings.Excel.ExpandCollapseCellStyle;
				text = expandCollapseCellStyle.ExpandImageUrl;
				text2 = expandCollapseCellStyle.CollapseImageUrl;
				width = expandCollapseCellStyle.ExpandImageWidth;
				width2 = expandCollapseCellStyle.CollapseImageWidth;
				height = expandCollapseCellStyle.ExpandImageHeight;
				height2 = expandCollapseCellStyle.CollapseImageHeight;
				text3 = expandCollapseCellStyle.ExpandText;
				text4 = expandCollapseCellStyle.CollapseText;
				fitToCell = expandCollapseCellStyle.EnableImageBestFit;
				currentCell.MergeStyle(expandCollapseCellStyle);
			}
			else if (this.treeList.CurrentExportFormat == ExportFormat.Word)
			{
				TreeListWordExpandCollapseCellStyle expandCollapseCellStyle2 = this.treeList.ExportSettings.Word.ExpandCollapseCellStyle;
				text = expandCollapseCellStyle2.ExpandImageUrl;
				text2 = expandCollapseCellStyle2.CollapseImageUrl;
				width = expandCollapseCellStyle2.ExpandImageWidth;
				width2 = expandCollapseCellStyle2.CollapseImageWidth;
				height = expandCollapseCellStyle2.ExpandImageHeight;
				height2 = expandCollapseCellStyle2.CollapseImageHeight;
				text3 = expandCollapseCellStyle2.ExpandText;
				text4 = expandCollapseCellStyle2.CollapseText;
				fitToCell = true;
				currentCell.MergeStyle(expandCollapseCellStyle2);
			}
			if (!(currentCell.Attributes["ExpandCollapseAttribute"] == "Expand"))
			{
				if (currentCell.Attributes["ExpandCollapseAttribute"] == "Collapse")
				{
					if (!string.IsNullOrEmpty(text2))
					{
						this.InsertImage(text2, col, row, width2, height2, fitToCell);
						return;
					}
					currentCell.Text = text4;
				}
				return;
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.InsertImage(text, col, row, width, height, fitToCell);
				return;
			}
			currentCell.Text = text3;
		}

		// Token: 0x0600BF73 RID: 49011 RVA: 0x002A6C0C File Offset: 0x002A4E0C
		private void DuplicateCellStyle(Cell cell, Cell cell2)
		{
			cell2.Style.BackColor = cell.Style.BackColor;
			cell2.Style.BorderBottomColor = cell.Style.BorderBottomColor;
			cell2.Style.BorderBottomStyle = cell.Style.BorderBottomStyle;
			cell2.Style.ForeColor = cell.Style.ForeColor;
			cell2.Style.Font.Bold = cell.Style.Font.Bold;
			cell2.Style.Font.Italic = cell.Style.Font.Italic;
			cell2.Style.Font.Name = cell.Style.Font.Name;
			cell2.Style.Font.Size = cell.Style.Font.Size;
			cell2.Style.Font.Strikeout = cell.Style.Font.Strikeout;
			cell2.Style.Font.Underline = cell.Style.Font.Underline;
			cell2.Format = cell.Format;
			cell2.Style.HorizontalAlign = cell.Style.HorizontalAlign;
			cell2.Style.BorderLeftColor = cell.Style.BorderLeftColor;
			cell2.Style.BorderLeftStyle = cell.Style.BorderLeftStyle;
			cell2.Style.BorderRightColor = cell.Style.BorderRightColor;
			cell2.Style.BorderRightStyle = cell.Style.BorderRightStyle;
			cell2.RotationAngle = cell.RotationAngle;
			cell2.RTL = cell.RTL;
			cell2.TextWrap = cell.TextWrap;
			cell2.Style.BorderTopColor = cell.Style.BorderTopColor;
			cell2.Style.BorderTopStyle = cell.Style.BorderTopStyle;
			cell2.Style.VerticalAlign = cell.Style.VerticalAlign;
		}

		// Token: 0x0600BF74 RID: 49012 RVA: 0x002A6E14 File Offset: 0x002A5014
		private void PrepareEICellStyle(Cell eiCell, TableCell gridCell)
		{
			this.PrepareCellStyle(gridCell);
			eiCell.Style.BackColor = gridCell.BackColor;
			eiCell.Style.ForeColor = gridCell.ForeColor;
			eiCell.TextWrap = gridCell.Wrap;
			eiCell.Style.Font = gridCell.Font;
			eiCell.Style.HorizontalAlign = ((gridCell.HorizontalAlign == HorizontalAlign.NotSet) ? HorizontalAlign.Left : gridCell.HorizontalAlign);
			eiCell.Style.VerticalAlign = gridCell.VerticalAlign;
			eiCell.Style.BorderBottomColor = (eiCell.Style.BorderTopColor = (eiCell.Style.BorderLeftColor = (eiCell.Style.BorderRightColor = gridCell.BorderColor)));
			eiCell.Style.BorderBottomStyle = (eiCell.Style.BorderTopStyle = (eiCell.Style.BorderLeftStyle = (eiCell.Style.BorderRightStyle = gridCell.BorderStyle)));
		}

		// Token: 0x0600BF75 RID: 49013 RVA: 0x002A6F10 File Offset: 0x002A5110
		private void PrepareCellStyle(TableCell gridCell)
		{
			TreeListItem treeListItem = gridCell.Parent as TreeListItem;
			if (gridCell.BackColor.IsEmpty)
			{
				gridCell.BackColor = treeListItem.BackColor;
			}
			if (gridCell.ForeColor.IsEmpty)
			{
				gridCell.ForeColor = treeListItem.ForeColor;
			}
			if (string.IsNullOrEmpty(gridCell.Font.Name))
			{
				gridCell.Font.Name = treeListItem.Font.Name;
			}
			if (gridCell.Font.Size.IsEmpty)
			{
				gridCell.Font.Size = treeListItem.Font.Size;
			}
			if (gridCell.HorizontalAlign == HorizontalAlign.NotSet)
			{
				gridCell.HorizontalAlign = treeListItem.HorizontalAlign;
			}
			if (gridCell.VerticalAlign == VerticalAlign.NotSet)
			{
				gridCell.VerticalAlign = treeListItem.VerticalAlign;
			}
			if (gridCell.BorderColor.IsEmpty)
			{
				gridCell.BorderColor = treeListItem.BorderColor;
			}
			if (gridCell.BorderStyle == BorderStyle.NotSet)
			{
				gridCell.BorderStyle = treeListItem.BorderStyle;
			}
		}

		// Token: 0x0600BF76 RID: 49014 RVA: 0x002A7010 File Offset: 0x002A5210
		private object ParseCellContent(TableCell currentCell, int col, int row, int expandCollapseCellIndex)
		{
			object result = null;
			if (currentCell.HasControls())
			{
				TreeListDataItem treeListDataItem = currentCell.Parent as TreeListDataItem;
				int cellIndex = this.GetCellIndex(currentCell);
				if (treeListDataItem != null && expandCollapseCellIndex == cellIndex)
				{
					Button button = currentCell.Controls[0] as Button;
					if (button != null && button.ID.Contains("ExpandCollapseButton"))
					{
						if (button.Visible)
						{
							string value = treeListDataItem.Expanded ? "Collapse" : "Expand";
							currentCell.Attributes.Add("ExpandCollapseAttribute", value);
							this.DecorateExpandCollapseCell(currentCell, col, row);
							return Utils.SanitizeCellText(currentCell.Text);
						}
						return string.Empty;
					}
				}
				string text = string.Empty;
				foreach (object obj in currentCell.Controls)
				{
					Control control = (Control)obj;
					if (control is LiteralControl)
					{
						text += (control as LiteralControl).Text.TrimEnd(new char[0]);
					}
					if (control.GetType() == typeof(System.Web.UI.WebControls.Image))
					{
						System.Web.UI.WebControls.Image image = (System.Web.UI.WebControls.Image)control;
						if (!string.IsNullOrEmpty(image.ImageUrl))
						{
							this.InsertImage(image.ImageUrl, col, row, false);
						}
						else if (!string.IsNullOrEmpty(image.AlternateText))
						{
							text += Utils.SanitizeCellText(image.AlternateText);
						}
					}
					else if (control is ImageButton)
					{
						ImageButton imageButton = control as ImageButton;
						if (!string.IsNullOrEmpty(imageButton.ImageUrl))
						{
							this.InsertImage(imageButton.ImageUrl, col, row, false);
						}
						else if (!string.IsNullOrEmpty(imageButton.AlternateText))
						{
							text += Utils.SanitizeCellText(imageButton.AlternateText);
						}
					}
				}
				result = Utils.SanitizeCellText(text);
			}
			else
			{
				TreeListDataItem treeListDataItem2 = currentCell.Parent as TreeListDataItem;
				if (treeListDataItem2 != null)
				{
					Type type = this.GetCellDataFieldType(currentCell);
					if (type == null)
					{
						type = typeof(string);
					}
					try
					{
						string fullName;
						switch (fullName = type.FullName)
						{
						case "System.String":
							result = Utils.SanitizeCellText(currentCell.Text);
							break;
						case "System.Boolean":
							result = Convert.ToBoolean(currentCell.Text);
							break;
						case "System.Int16":
							result = Convert.ToInt16(currentCell.Text);
							break;
						case "System.Int32":
							result = Convert.ToInt32(currentCell.Text);
							break;
						case "System.Int64":
							result = Convert.ToInt64(currentCell.Text);
							break;
						case "System.Double":
							result = Convert.ToDecimal(currentCell.Text);
							break;
						case "System.DateTime":
							result = Convert.ToDateTime(currentCell.Text);
							break;
						case "System.Byte":
							result = Convert.ToByte(currentCell.Text);
							break;
						case "System.Char":
							result = Convert.ToChar(currentCell.Text);
							break;
						case "System.Decimal":
							result = Convert.ToDecimal(currentCell.Text);
							break;
						case "System.Guid":
							result = Utils.ConvertToGuid(currentCell.Text);
							break;
						case "System.SByte":
							result = Convert.ToSByte(currentCell.Text);
							break;
						case "System.Single":
							result = Convert.ToSingle(currentCell.Text);
							break;
						case "System.TimeSpan":
							result = Utils.ConvertToTimeSpan(currentCell.Text);
							break;
						case "System.UInt16":
							result = Convert.ToUInt16(currentCell.Text);
							break;
						case "System.UInt32":
							result = Convert.ToUInt32(currentCell.Text);
							break;
						case "System.UInt64":
							result = Convert.ToUInt64(currentCell.Text);
							break;
						}
						return result;
					}
					catch (Exception ex)
					{
						if (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
						{
							return Utils.SanitizeCellText(currentCell.Text);
						}
						throw;
					}
				}
				result = Utils.SanitizeCellText(currentCell.Text);
			}
			return result;
		}

		// Token: 0x0600BF77 RID: 49015 RVA: 0x002A7554 File Offset: 0x002A5754
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void InsertImage(string imageUrl, int col, int row, Unit width, Unit height, bool fitToCell)
		{
			byte[] array = null;
			System.Drawing.Image image = null;
			if (Regex.IsMatch(imageUrl, "^http.?://"))
			{
				WebClient webClient = new WebClient();
				array = webClient.DownloadData(imageUrl);
				MemoryStream stream = new MemoryStream(array);
				image = System.Drawing.Image.FromStream(stream);
			}
			else
			{
				string text = this.currentContext.Server.MapPath(imageUrl);
				FileInfo fileInfo = new FileInfo(text);
				if (fileInfo.Exists && fileInfo.Length < 2147483647L)
				{
					array = new byte[fileInfo.Length];
					using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read))
					{
						fileStream.Read(array, 0, (int)fileInfo.Length);
					}
					image = System.Drawing.Image.FromFile(text);
				}
			}
			if (image != null)
			{
				try
				{
					Cell cell = this.workbook.Tables[0].Cells[col + 1, row + 1];
					this.workbook.Tables[0].InsertImage(cell, array, fitToCell);
					image.Dispose();
				}
				catch (Exception ex)
				{
					if (ex is ExternalException)
					{
						throw new TreeListExcelExportException("External exception occured while exporting an image");
					}
					if (ex is ArgumentNullException)
					{
						throw new TreeListExcelExportException("Invalid image path!");
					}
					throw;
				}
			}
		}

		// Token: 0x0600BF78 RID: 49016 RVA: 0x002A7698 File Offset: 0x002A5898
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void InsertImage(string imageUrl, int col, int row, bool fitToCell)
		{
			this.InsertImage(imageUrl, col, row, Unit.Empty, Unit.Empty, fitToCell);
		}

		// Token: 0x0600BF79 RID: 49017 RVA: 0x002A76B0 File Offset: 0x002A58B0
		private short GetImageHeight(System.Web.UI.WebControls.Image img)
		{
			string filename = this.currentContext.Server.MapPath(img.ImageUrl);
			Unit height;
			if (img.Height == Unit.Empty)
			{
				System.Drawing.Image image = System.Drawing.Image.FromFile(filename);
				height = new Unit((double)image.Height, UnitType.Pixel);
				image.Dispose();
			}
			else
			{
				height = img.Height;
			}
			return (short)Utils.GetPointsPerUnit(height);
		}

		// Token: 0x0600BF7A RID: 49018 RVA: 0x002A7714 File Offset: 0x002A5914
		private short GetImageWidth(System.Web.UI.WebControls.Image img)
		{
			string filename = this.currentContext.Server.MapPath(img.ImageUrl);
			Unit width;
			if (img.Width == Unit.Empty)
			{
				System.Drawing.Image image = System.Drawing.Image.FromFile(filename);
				width = new Unit((double)image.Width, UnitType.Pixel);
				image.Dispose();
			}
			else
			{
				width = img.Width;
			}
			return (short)Utils.GetPointsPerUnit(width);
		}

		// Token: 0x0600BF7B RID: 49019 RVA: 0x002A7778 File Offset: 0x002A5978
		private TreeListColumn GetColumnByTableCell(TableCell cell)
		{
			TreeListColumn result = null;
			int cellIndex = this.GetCellIndex(cell);
			if (cell.Parent is TreeListDataItem)
			{
				TreeListDataItem treeListDataItem = cell.Parent as TreeListDataItem;
				if (cellIndex > treeListDataItem.HierarchyIndex.NestedLevel)
				{
					result = this.treeList.RenderColumns[cellIndex - treeListDataItem.HierarchyIndex.NestedLevel - 1];
				}
			}
			else if (cell.Parent is TreeListFooterItem)
			{
				TreeListFooterItem treeListFooterItem = cell.Parent as TreeListFooterItem;
				if (cellIndex > treeListFooterItem.HierarchyIndex.NestedLevel)
				{
					int num = cellIndex - treeListFooterItem.HierarchyIndex.NestedLevel - 1;
					if (num > 0)
					{
						result = this.treeList.RenderColumns[num - 1];
					}
				}
			}
			else if (cellIndex < this.treeList.RenderColumns.Length)
			{
				result = this.treeList.RenderColumns[cellIndex];
			}
			return result;
		}

		// Token: 0x0600BF7C RID: 49020 RVA: 0x002A7848 File Offset: 0x002A5A48
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private Type GetCellDataFieldType(TableCell currentCell)
		{
			Type result = typeof(string);
			TreeListColumn columnByTableCell = this.GetColumnByTableCell(currentCell);
			if (columnByTableCell != null && columnByTableCell is TreeListBoundColumn)
			{
				result = (columnByTableCell as TreeListBoundColumn).DataType;
			}
			return result;
		}

		// Token: 0x0600BF7D RID: 49021 RVA: 0x002A7880 File Offset: 0x002A5A80
		private int GetCellIndex(TableCell currentCell)
		{
			TableRow tableRow = currentCell.Parent as TableRow;
			return tableRow.Cells.GetCellIndex(currentCell);
		}

		// Token: 0x0600BF7E RID: 49022 RVA: 0x002A78A5 File Offset: 0x002A5AA5
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600BF7F RID: 49023 RVA: 0x002A78AE File Offset: 0x002A5AAE
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._defaultFont != null)
			{
				this.workbook.Dispose();
				this._defaultFont.Dispose();
			}
		}

		// Token: 0x0400323E RID: 12862
		internal const float defaultCharHeight = 10f;

		// Token: 0x0400323F RID: 12863
		private ExportStructure workbook;

		// Token: 0x04003240 RID: 12864
		private Telerik.Web.UI.ExportInfrastructure.Table baseWorksheet;

		// Token: 0x04003241 RID: 12865
		private RadTreeList treeList;

		// Token: 0x04003242 RID: 12866
		private HttpContext currentContext;

		// Token: 0x04003243 RID: 12867
		private Font _defaultFont;
	}
}
