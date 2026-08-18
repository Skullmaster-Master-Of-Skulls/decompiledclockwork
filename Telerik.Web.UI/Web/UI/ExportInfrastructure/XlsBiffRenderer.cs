using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using Telerik.Web.UI.ExcelBiff;
using Telerik.Web.UI.Export;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A51 RID: 2641
	public class XlsBiffRenderer : IDisposable
	{
		// Token: 0x170021B9 RID: 8633
		// (get) Token: 0x06006651 RID: 26193 RVA: 0x0017E756 File Offset: 0x0017C956
		// (set) Token: 0x06006652 RID: 26194 RVA: 0x0017E788 File Offset: 0x0017C988
		internal System.Drawing.Font DefaultFont
		{
			get
			{
				if (this.defaultFont == null)
				{
					this.defaultFont = new System.Drawing.Font(this.workbook.DefaultFontName, this.workbook.DefaultFontSize);
				}
				return this.defaultFont;
			}
			set
			{
				this.defaultFont = value;
			}
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x0017E791 File Offset: 0x0017C991
		public XlsBiffRenderer(ExportStructure structure)
		{
			this._structure = structure;
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x0017E7A0 File Offset: 0x0017C9A0
		public byte[] Render()
		{
			this.workbook = new Workbook();
			foreach (Table tbl in this._structure.Tables)
			{
				this.CreateWorksheet(tbl, this.workbook);
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.workbook.Save(memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06006655 RID: 26197 RVA: 0x0017E838 File Offset: 0x0017CA38
		private void CreateWorksheet(Table tbl, Workbook wb)
		{
			Worksheet worksheet = wb.AddWorksheet();
			this.SetWorksheetOptions(tbl, worksheet);
			worksheet.ShowGridlines = true;
			if (!string.IsNullOrEmpty(tbl.Title))
			{
				worksheet.Name = tbl.Title;
			}
			this.PrepareWorksheetRowsAndColumns(worksheet, tbl);
			foreach (Cell cell in tbl.Cells)
			{
				int num = (cell.ColIndex <= 0) ? 0 : (cell.ColIndex - 1);
				int num2 = (cell.RowIndex <= 0) ? 0 : (cell.RowIndex - 1);
				Cell cell2 = worksheet.CellGrid[num, num2];
				cell2.Value = cell.Value;
				cell2.Format = cell.Format;
				cell2.TextWrap = cell.TextWrap;
				Column column = tbl.Columns.GetColumn(num + 1);
				if (column != null && !column.Style.IsEmpty)
				{
					cell.Style.ImportStyle(column.Style);
					if (column.Style.HasBorderStyles)
					{
						if (num2 == column.FirstRowIndex - 1)
						{
							cell.Style.ImportBorderStyle(column.Style, CellBorderPosition.Top);
						}
						else if (num2 == column.LastRowIndex - 1)
						{
							cell.Style.ImportBorderStyle(column.Style, CellBorderPosition.Bottom);
						}
						else
						{
							cell.Style.ImportBorderStyle(column.Style, CellBorderPosition.ColumnMiddle);
						}
					}
				}
				Row row = tbl.Rows.GetRow(num2 + 1);
				if (row != null && !row.Style.IsEmpty)
				{
					cell.Style.ImportStyle(row.Style);
					if (row.Style.HasBorderStyles)
					{
						if (num == row.FirstColumnIndex - 1)
						{
							cell.Style.ImportBorderStyle(row.Style, CellBorderPosition.Left);
						}
						else if (num == row.LastColumnIndex - 1)
						{
							cell.Style.ImportBorderStyle(row.Style, CellBorderPosition.Right);
						}
						else
						{
							cell.Style.ImportBorderStyle(row.Style, CellBorderPosition.RowMiddle);
						}
					}
				}
				if (!tbl.Style.IsEmpty)
				{
					cell.Style.ImportStyle(tbl.Style);
					if (tbl.Style.HasBorderStyles)
					{
						if (cell.RowIndex == tbl.Cells.FirstCellRowIndex && cell.ColIndex == tbl.Cells.FirstCellColumnIndex)
						{
							cell.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableTopLeft);
						}
						else if (cell.RowIndex == tbl.Cells.LastCellRowIndex && cell.ColIndex == tbl.Cells.FirstCellColumnIndex)
						{
							cell.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableBottomLeft);
						}
						else if (cell.RowIndex == tbl.Cells.LastCellRowIndex && cell.ColIndex == tbl.Cells.LastCellColumnIndex)
						{
							cell.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableBottomRight);
						}
						else if (cell.RowIndex == tbl.Cells.FirstCellRowIndex && cell.ColIndex == tbl.Cells.LastCellColumnIndex)
						{
							cell.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableTopRight);
						}
						else if (cell.RowIndex == tbl.Cells.FirstCellRowIndex)
						{
							cell.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableTop);
						}
						else if (cell.RowIndex == tbl.Cells.LastCellRowIndex)
						{
							cell.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableBottom);
						}
						else if (cell.ColIndex == tbl.Cells.FirstCellColumnIndex)
						{
							cell.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableLeft);
						}
						else if (cell.ColIndex == tbl.Cells.LastCellColumnIndex)
						{
							cell.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableRight);
						}
					}
				}
				if (cell.Colspan <= 1 && cell.Rowspan <= 1)
				{
					this.ApplyCellStyle(cell2, cell.Style);
				}
				else
				{
					Range range = new Range(worksheet, num, num2, cell.Colspan, cell.Rowspan);
					worksheet.MergedRanges.Add(range);
					this.ApplyRangeStyle(range, cell.Style);
				}
			}
			foreach (Telerik.Web.UI.ExportInfrastructure.Image image in tbl.Images)
			{
				int num3 = image.ImageRange.Start.X - 1;
				int num4 = image.ImageRange.Start.Y - 1;
				int num5 = image.ImageRange.End.X - num3 - 1;
				int num6 = image.ImageRange.End.Y - num4 - 1;
				System.Drawing.Image image2 = image.GetImage();
				if (image2 != null)
				{
					Range range2 = new Range(worksheet, num3, num4, image.AutoSize ? 0 : num5, image.AutoSize ? 0 : num6);
					try
					{
						range2.AddPicture(image2, 0.0, 0.0, image.AutoSize ? 0.0 : Utils.GetPointsPerUnit(image2.Width), image.AutoSize ? 0.0 : Utils.GetPointsPerUnit(image2.Height));
						image2.Dispose();
					}
					catch (Exception ex)
					{
						if (ex is ExternalException)
						{
							throw new ExportInfrastructureException("External exception occured while exporting an image to Excel BIFF format");
						}
						if (ex is ArgumentNullException)
						{
							throw new ExportInfrastructureException("Invalid image path!");
						}
						throw;
					}
					tbl.ImageCount++;
				}
			}
		}

		// Token: 0x06006656 RID: 26198 RVA: 0x0017EE28 File Offset: 0x0017D028
		private void SetWorksheetOptions(Table tbl, Worksheet ws)
		{
			SizeF paperKindDimensions = TreeListExcelExportSettings.GetPaperKindDimensions(tbl.PageSize);
			int paperSizeIndex = PaperSizeIndex.GetPaperSizeIndex(paperKindDimensions);
			ws.PageSize = ((paperSizeIndex == 0) ? default(SizeF) : paperKindDimensions);
			ws.PageFooter = tbl.FooterText;
			ws.PageHeader = tbl.HeaderText;
			ws.ShowGridlines = tbl.ShowGridlines;
			ws.Margins.Top = Utils.GetInchesPerUnit(tbl.TopMargin);
			ws.Margins.Bottom = Utils.GetInchesPerUnit(tbl.BottomMargin);
			ws.Margins.Left = Utils.GetInchesPerUnit(tbl.LeftMargin);
			ws.Margins.Right = Utils.GetInchesPerUnit(tbl.RightMargin);
			ws.Landscape = tbl.Landscape;
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x0017EEE8 File Offset: 0x0017D0E8
		private Row GetWorksheetRow(int rowIndex, Worksheet ws)
		{
			int count = ws.Rows.Count;
			if (rowIndex > count)
			{
				for (int i = count; i <= rowIndex; i++)
				{
					ws.AddRow();
				}
			}
			return ws.Rows[rowIndex - 1];
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x0017EF28 File Offset: 0x0017D128
		private Column GetWorksheetColumn(int columnIndex, Worksheet ws)
		{
			int count = ws.Columns.Count;
			if (columnIndex > count)
			{
				for (int i = count; i <= columnIndex; i++)
				{
					ws.AddColumn();
				}
			}
			return ws.Columns[columnIndex - 1];
		}

		// Token: 0x06006659 RID: 26201 RVA: 0x0017EF68 File Offset: 0x0017D168
		private void PrepareWorksheetRowsAndColumns(Worksheet ws, Table tbl)
		{
			foreach (Row row in tbl.Rows)
			{
				Row worksheetRow = this.GetWorksheetRow(row.Index, ws);
				if (row.Height > 0.0)
				{
					worksheetRow.Height = this.ConvertToPoint(row.Height, this._structure.RowHeightUnit);
				}
			}
			foreach (Column column in tbl.Columns)
			{
				Column worksheetColumn = this.GetWorksheetColumn(column.Index, ws);
				if (column.Width > 0.0)
				{
					worksheetColumn.Width = this.ConvertToColumnUnits(column.Width, this._structure.ColumnWidthUnit);
				}
			}
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x0017F068 File Offset: 0x0017D268
		private double ConvertToPoint(double value, ExportUnitType targetUnits)
		{
			double result = value;
			if (targetUnits == ExportUnitType.Pixel)
			{
				result = XlsBiffRenderer.DipToPoint(value);
			}
			return result;
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x0017F084 File Offset: 0x0017D284
		public static double DipToPoint(double value)
		{
			double num = 96.0;
			double num2 = 72.0;
			return value * num2 / num;
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x0017F0AC File Offset: 0x0017D2AC
		private double ConvertToColumnUnits(double value, ExportUnitType targetUnits)
		{
			double result = value;
			ExcelConverter excelConverter = new ExcelConverter(this.DefaultFont);
			switch (targetUnits)
			{
			case ExportUnitType.Point:
				result = excelConverter.PointsToCharacters(value);
				break;
			case ExportUnitType.Pixel:
				result = excelConverter.PixelsToCharacters(value);
				break;
			}
			return result;
		}

		// Token: 0x0600665D RID: 26205 RVA: 0x0017F0F0 File Offset: 0x0017D2F0
		private void ApplyRangeStyle(Range xlsRange, ExportStyle cellStyle)
		{
			xlsRange.BackgroundColor = cellStyle.BackColor;
			xlsRange.Color = cellStyle.ForeColor;
			if (cellStyle.HorizontalAlign != HorizontalAlign.NotSet)
			{
				xlsRange.HorizontalAlignment = Utils.ConvertHorizontalAlign(cellStyle.HorizontalAlign);
			}
			if (cellStyle.VerticalAlign != VerticalAlign.NotSet)
			{
				xlsRange.VerticalAlignment = Utils.ConvertVerticalAlign(cellStyle.VerticalAlign);
			}
			if (!Utils.IsEmptyFontStyle(cellStyle.Font) && string.IsNullOrEmpty(cellStyle.Font.Name))
			{
				cellStyle.Font.Name = this._structure.DefaultFont.Name;
			}
			xlsRange.FontName = cellStyle.Font.Name;
			if (cellStyle.Font.Size.IsEmpty)
			{
				cellStyle.Font.Size = new FontUnit((double)this._structure.DefaultFont.SizeInPoints);
			}
			else if (cellStyle.Font.Size.Type == FontSize.AsUnit)
			{
				cellStyle.Font.Size = cellStyle.Font.Size;
			}
			else
			{
				cellStyle.Font.Size = new FontUnit(Utils.FontSizeToPoints(cellStyle.Font.Size.Type));
			}
			xlsRange.FontSizeInPoints = (float)cellStyle.Font.Size.Unit.Value;
			xlsRange.FontBold = cellStyle.Font.Bold;
			xlsRange.FontItalic = cellStyle.Font.Italic;
			xlsRange.FontStrikeout = cellStyle.Font.Strikeout;
			xlsRange.FontUnderline = cellStyle.Font.Underline;
			this.ApplyRangeBorderStyle(xlsRange, cellStyle);
		}

		// Token: 0x0600665E RID: 26206 RVA: 0x0017F290 File Offset: 0x0017D490
		private void ApplyCellStyle(Cell xlsCell, ExportStyle cellStyle)
		{
			xlsCell.BackgroundColor = cellStyle.BackColor;
			xlsCell.Color = cellStyle.ForeColor;
			if (cellStyle.HorizontalAlign != HorizontalAlign.NotSet)
			{
				xlsCell.HorizontalAlignment = Utils.ConvertHorizontalAlign(cellStyle.HorizontalAlign);
			}
			if (cellStyle.VerticalAlign != VerticalAlign.NotSet)
			{
				xlsCell.VerticalAlignment = Utils.ConvertVerticalAlign(cellStyle.VerticalAlign);
			}
			if (!Utils.IsEmptyFontStyle(cellStyle.Font) && string.IsNullOrEmpty(cellStyle.Font.Name))
			{
				cellStyle.Font.Name = this._structure.DefaultFont.Name;
			}
			xlsCell.FontName = cellStyle.Font.Name;
			if (!cellStyle.Font.Size.IsEmpty)
			{
				if (cellStyle.Font.Size.Type == FontSize.AsUnit)
				{
					cellStyle.Font.Size = cellStyle.Font.Size;
				}
				else
				{
					cellStyle.Font.Size = new FontUnit(Utils.FontSizeToPoints(cellStyle.Font.Size.Type));
				}
			}
			else
			{
				cellStyle.Font.Size = new FontUnit((double)this._structure.DefaultFont.SizeInPoints);
			}
			xlsCell.FontSizeInPoints = (float)cellStyle.Font.Size.Unit.Value;
			xlsCell.FontBold = cellStyle.Font.Bold;
			xlsCell.FontItalic = cellStyle.Font.Italic;
			xlsCell.FontStrikeout = cellStyle.Font.Strikeout;
			xlsCell.FontUnderline = cellStyle.Font.Underline;
			this.ApplyCellBorderStyle(xlsCell, cellStyle);
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x0017F430 File Offset: 0x0017D630
		private void ApplyRangeBorderStyle(Range xlsRange, ExportStyle cellStyle)
		{
			if (!cellStyle.BorderTopColor.IsEmpty)
			{
				xlsRange.Borders.Top.Color = cellStyle.BorderTopColor;
			}
			if (!cellStyle.BorderBottomColor.IsEmpty)
			{
				xlsRange.Borders.Bottom.Color = cellStyle.BorderBottomColor;
			}
			if (!cellStyle.BorderLeftColor.IsEmpty)
			{
				xlsRange.Borders.Left.Color = cellStyle.BorderLeftColor;
			}
			if (!cellStyle.BorderRightColor.IsEmpty)
			{
				xlsRange.Borders.Right.Color = cellStyle.BorderRightColor;
			}
			if (cellStyle.BorderTopStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
			{
				xlsRange.Borders.Top.Style = Utils.ConvertBorderStyle(cellStyle.BorderTopStyle);
			}
			if (cellStyle.BorderBottomStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
			{
				xlsRange.Borders.Bottom.Style = Utils.ConvertBorderStyle(cellStyle.BorderBottomStyle);
			}
			if (cellStyle.BorderLeftStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
			{
				xlsRange.Borders.Left.Style = Utils.ConvertBorderStyle(cellStyle.BorderLeftStyle);
			}
			if (cellStyle.BorderRightStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
			{
				xlsRange.Borders.Right.Style = Utils.ConvertBorderStyle(cellStyle.BorderRightStyle);
			}
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x0017F564 File Offset: 0x0017D764
		private void ApplyCellBorderStyle(Cell xlsCell, ExportStyle cellStyle)
		{
			if (!cellStyle.BorderTopColor.IsEmpty)
			{
				xlsCell.TopBorderColor = cellStyle.BorderTopColor;
			}
			if (!cellStyle.BorderBottomColor.IsEmpty)
			{
				xlsCell.BottomBorderColor = cellStyle.BorderBottomColor;
			}
			if (!cellStyle.BorderLeftColor.IsEmpty)
			{
				xlsCell.LeftBorderColor = cellStyle.BorderLeftColor;
			}
			if (!cellStyle.BorderRightColor.IsEmpty)
			{
				xlsCell.RightBorderColor = cellStyle.BorderRightColor;
			}
			if (cellStyle.BorderTopStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
			{
				xlsCell.TopBorderStyle = Utils.ConvertBorderStyle(cellStyle.BorderTopStyle);
			}
			if (cellStyle.BorderBottomStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
			{
				xlsCell.BottomBorderStyle = Utils.ConvertBorderStyle(cellStyle.BorderBottomStyle);
			}
			if (cellStyle.BorderLeftStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
			{
				xlsCell.LeftBorderStyle = Utils.ConvertBorderStyle(cellStyle.BorderLeftStyle);
			}
			if (cellStyle.BorderRightStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
			{
				xlsCell.RightBorderStyle = Utils.ConvertBorderStyle(cellStyle.BorderRightStyle);
			}
		}

		// Token: 0x06006661 RID: 26209 RVA: 0x0017F645 File Offset: 0x0017D845
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x0017F654 File Offset: 0x0017D854
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.defaultFont != null)
			{
				this.defaultFont.Dispose();
				this.defaultFont = null;
			}
		}

		// Token: 0x040018D4 RID: 6356
		private ExportStructure _structure;

		// Token: 0x040018D5 RID: 6357
		internal Workbook workbook;

		// Token: 0x040018D6 RID: 6358
		private System.Drawing.Font defaultFont;
	}
}
