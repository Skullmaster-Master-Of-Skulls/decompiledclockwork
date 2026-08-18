using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI.WebControls;
using System.Windows.Media;
using Telerik.Web.UI.Export;
using Telerik.Windows.Documents.Media;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Model.Shapes;
using Telerik.Windows.Documents.Spreadsheet.PropertySystem;
using Telerik.Windows.Documents.Spreadsheet.Theming;
using Telerik.Windows.Documents.Spreadsheet.Utilities;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000150 RID: 336
	public class XlsxRenderer : IDisposable
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x000305FA File Offset: 0x0002E7FA
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00030602 File Offset: 0x0002E802
		public ExportAutoFitWidthMode AutoFitWidthMode { get; set; }

		// Token: 0x06000D4B RID: 3403 RVA: 0x0003060B File Offset: 0x0002E80B
		public XlsxRenderer(ExportStructure structure)
		{
			this._structure = structure;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0003061C File Offset: 0x0002E81C
		public Workbook CreateWorkbook()
		{
			this.workbook = new Workbook();
			this.workbook.History.IsEnabled = false;
			this.workbook.SuspendLayoutUpdate();
			foreach (Table tbl in this._structure.Tables)
			{
				this.CreateWorksheet(tbl, this.workbook);
			}
			return this.workbook;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000306FC File Offset: 0x0002E8FC
		public byte[] Render(Workbook workbook = null)
		{
			byte[] output = null;
			if (workbook == null)
			{
				workbook = this.CreateWorkbook();
			}
			Thread thread = new Thread(delegate()
			{
				IWorkbookFormatProvider workbookFormatProvider = new XlsxFormatProvider();
				using (MemoryStream memoryStream = new MemoryStream())
				{
					workbookFormatProvider.Export(workbook, memoryStream);
					output = memoryStream.ToArray();
				}
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			return output;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003075C File Offset: 0x0002E95C
		private void CreateWorksheet(Table tbl, Workbook wb)
		{
			Worksheet worksheet = wb.Worksheets.Add();
			this.defaultStyle = wb.Styles.GetByName("Normal");
			if (!string.IsNullOrEmpty(tbl.Title))
			{
				worksheet.Name = ((tbl.Title.Length > 31) ? tbl.Title.Substring(0, 31) : tbl.Title);
			}
			this.PrepareWorksheetRowsAndColumns(worksheet, tbl);
			foreach (Cell cell in tbl.Cells)
			{
				int num = (cell.ColIndex <= 0) ? 0 : (cell.ColIndex - 1);
				int num2 = (cell.RowIndex <= 0) ? 0 : (cell.RowIndex - 1);
				CellSelection cellSelection = worksheet.Cells[num2, num];
				if (!string.IsNullOrEmpty(cell.Format))
				{
					cellSelection.SetFormat(new CellValueFormat(cell.Format));
				}
				this.SetCellValue(cellSelection, cell);
				if (!string.IsNullOrEmpty(cell.Hyperlink))
				{
					this.SetHyperLink(new CellIndex(num2, num), worksheet, cell);
				}
				if (cell.TextWrap)
				{
					cellSelection.SetIsWrapped(true);
				}
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
					if (!cell.Style.IsEmpty)
					{
						this.ApplyRangeStyle(cellSelection, cell.Style);
					}
				}
				else
				{
					CellSelection cellSelection2 = worksheet.Cells[new CellRange(num2, num, num2 + cell.Rowspan - 1, num + cell.Colspan - 1)];
					cellSelection2.Merge();
					if (!cell.Style.IsEmpty)
					{
						this.ApplyRangeStyle(cellSelection2, cell.Style);
					}
				}
			}
			foreach (Telerik.Web.UI.ExportInfrastructure.Image image in tbl.Images)
			{
				int num3 = image.ImageRange.Start.X - 1;
				int num4 = image.ImageRange.Start.Y - 1;
				int x = image.ImageRange.End.X;
				int y = image.ImageRange.End.Y;
				FloatingImage floatingImage = new FloatingImage(worksheet, new CellIndex(num4, num3), 0.0, 0.0);
				ImageSource imageSource = image.GetImageSource();
				if (imageSource != null)
				{
					floatingImage.ImageSource = imageSource;
					floatingImage.Width = UnitHelper.PointToDip((double)((float)image.Width * 0.72f));
					floatingImage.Height = UnitHelper.PointToDip((double)((float)image.Height * 0.72f));
					worksheet.Shapes.Add(floatingImage);
					tbl.ImageCount++;
				}
			}
			if (worksheet.Columns.Count > 0)
			{
				ColumnSelection columnSelection = worksheet.Columns[0, worksheet.Columns.Count - 1];
				switch (this.AutoFitWidthMode)
				{
				case ExportAutoFitWidthMode.Disabled:
					break;
				case ExportAutoFitWidthMode.AutoFitExpandOnly:
					columnSelection.AutoFitWidth(true, false);
					return;
				case ExportAutoFitWidthMode.AutoFitAll:
					columnSelection.AutoFitWidth();
					return;
				case ExportAutoFitWidthMode.ExpandToFitNumberValuesWidth:
					columnSelection.ExpandToFitNumberValuesWidth();
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00030DB4 File Offset: 0x0002EFB4
		private void SetHyperLink(CellIndex cellIndex, Worksheet ws, Cell cell)
		{
			HyperlinkInfo hyperlinkInfo = null;
			string text = cell.Hyperlink.Replace("&nbsp;", " ");
			if (text.StartsWith("="))
			{
				hyperlinkInfo = HyperlinkInfo.CreateInDocumentHyperlink(text.Substring(1), null);
			}
			else if (text.StartsWith("mailto:"))
			{
				hyperlinkInfo = HyperlinkInfo.CreateMailtoHyperlink(text.Substring(7));
			}
			else if (Regex.IsMatch(text, "^(https?|ftp)://.*$"))
			{
				hyperlinkInfo = HyperlinkInfo.CreateHyperlink(text, null);
			}
			else if (!string.IsNullOrEmpty(text))
			{
				hyperlinkInfo = HyperlinkInfo.CreateHyperlink(text, null);
			}
			ws.Hyperlinks.Add(cellIndex, hyperlinkInfo);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00030E48 File Offset: 0x0002F048
		private void SetCellValue(CellSelection xlsCell, Cell cell)
		{
			if (cell.Value == null)
			{
				return;
			}
			object value = cell.Value;
			string fullName;
			switch (fullName = value.GetType().FullName)
			{
			case "System.Boolean":
				xlsCell.SetValue((bool)value);
				return;
			case "System.Int16":
			case "System.Int32":
			case "System.Int64":
			case "System.Double":
			case "System.Byte":
			case "System.Char":
			case "System.Decimal":
			case "System.SByte":
			case "System.Single":
			case "System.UInt16":
			case "System.UInt32":
			case "System.UInt64":
				xlsCell.SetValue(Convert.ToDouble(value));
				return;
			case "System.DateTime":
				xlsCell.SetValue((DateTime)value);
				return;
			}
			xlsCell.SetValue(value.ToString());
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00030FC4 File Offset: 0x0002F1C4
		private RowSelection GetWorksheetRow(int rowIndex, Worksheet ws)
		{
			int count = ws.Rows.Count;
			if (rowIndex > count)
			{
				for (int i = count; i <= rowIndex; i++)
				{
					ws.Rows.Insert(rowIndex);
				}
			}
			return ws.Rows[rowIndex - 1];
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0003100C File Offset: 0x0002F20C
		private ColumnSelection GetWorksheetColumn(int columnIndex, Worksheet ws)
		{
			int count = ws.Columns.Count;
			if (columnIndex > count)
			{
				for (int i = count; i <= columnIndex; i++)
				{
					ws.Columns.Insert(columnIndex);
				}
			}
			return ws.Columns[columnIndex - 1];
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00031054 File Offset: 0x0002F254
		private void PrepareWorksheetRowsAndColumns(Worksheet ws, Table tbl)
		{
			foreach (Row row in tbl.Rows)
			{
				RowSelection worksheetRow = this.GetWorksheetRow(row.Index, ws);
				if (row.Height > 0.0)
				{
					worksheetRow.SetHeight(new RowHeight(this.ConvertRowUnitsToDip(row.Height, this._structure.RowHeightUnit), true));
				}
			}
			foreach (Column column in tbl.Columns)
			{
				ColumnSelection worksheetColumn = this.GetWorksheetColumn(column.Index, ws);
				if (column.Width > 0.0)
				{
					worksheetColumn.SetWidth(new ColumnWidth(this.ConvertColumnUnitsToDip(column.Width, this._structure.ColumnWidthUnit), true));
				}
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00031160 File Offset: 0x0002F360
		private double ConvertColumnUnitsToDip(double value, ExportUnitType targetUnits)
		{
			double result = value;
			switch (targetUnits)
			{
			case ExportUnitType.FormatDefault:
				result = XlsxRenderer.ConvertColumnExcelWidthToPixelWidth(this.workbook, value);
				break;
			case ExportUnitType.Point:
				result = UnitHelper.PointToDip(value);
				break;
			}
			return result;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00031198 File Offset: 0x0002F398
		private double ConvertRowUnitsToDip(double value, ExportUnitType targetUnits)
		{
			double result = value;
			switch (targetUnits)
			{
			case ExportUnitType.FormatDefault:
			case ExportUnitType.Point:
				result = UnitHelper.PointToDip(value);
				break;
			}
			return result;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000311C4 File Offset: 0x0002F3C4
		internal static double ConvertColumnPixelWidthToExcelWidth(Workbook workbook, double columnWidthInPixels)
		{
			double maxDigitWidthInNormalStyle = XlsxRenderer.GetMaxDigitWidthInNormalStyle(workbook);
			double num = SpreadsheetDefaultValues.LeftCellMargin + SpreadsheetDefaultValues.RightCellMargin;
			double result = 0.0;
			if (columnWidthInPixels > 0.0)
			{
				result = Math.Floor((columnWidthInPixels + num) / maxDigitWidthInNormalStyle * 256.0) / 256.0;
			}
			return result;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0003121C File Offset: 0x0002F41C
		internal static double ConvertColumnExcelWidthToPixelWidth(Workbook workbook, double excelWidth)
		{
			Guard.ThrowExceptionIfNull<Workbook>(workbook, "workbook");
			double maxDigitWidthInNormalStyle = XlsxRenderer.GetMaxDigitWidthInNormalStyle(workbook);
			double num = SpreadsheetDefaultValues.LeftCellMargin + SpreadsheetDefaultValues.RightCellMargin;
			return Math.Max(0.0, Math.Floor((256.0 * excelWidth + Math.Floor(128.0 / maxDigitWidthInNormalStyle)) / 256.0 * maxDigitWidthInNormalStyle) - num);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00031284 File Offset: 0x0002F484
		internal static double GetMaxDigitWidthInNormalStyle(Workbook workbook)
		{
			Assembly assembly = Assembly.Load("Telerik.Windows.Documents.Spreadsheet");
			Type type = assembly.GetType("Telerik.Windows.Documents.Spreadsheet.Utilities.UnitHelper");
			return (double)type.GetMethod("GetMaxDigitWidthInNormalStyle", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[]
			{
				workbook
			});
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000312CC File Offset: 0x0002F4CC
		private void ApplyRangeStyle(CellSelection xlsRange, ExportStyle cellStyle)
		{
			if (cellStyle.BackColor == System.Drawing.Color.Empty)
			{
				xlsRange.ClearFill();
			}
			else
			{
				xlsRange.SetFill(new PatternFill(0, this.GetThemableColor(cellStyle.BackColor).LocalValue, Colors.Transparent));
			}
			xlsRange.SetForeColor(this.GetThemableColor(cellStyle.ForeColor));
			if (cellStyle.HorizontalAlign != HorizontalAlign.NotSet)
			{
				xlsRange.SetHorizontalAlignment(Utils.ConvertXlsxHorizontalAlign(cellStyle.HorizontalAlign));
			}
			if (cellStyle.VerticalAlign != VerticalAlign.NotSet)
			{
				xlsRange.SetVerticalAlignment(Utils.ConvertXlsxVerticalAlign(cellStyle.VerticalAlign));
			}
			if (!Utils.IsEmptyFontStyle(cellStyle.Font) && !string.IsNullOrEmpty(cellStyle.Font.Name))
			{
				xlsRange.SetFontFamily(new ThemableFontFamily(cellStyle.Font.Name));
			}
			if (!cellStyle.Font.Size.IsEmpty)
			{
				if (cellStyle.Font.Size.Type == FontSize.AsUnit)
				{
					xlsRange.SetFontSize((double)((float)cellStyle.Font.Size.Unit.Value * 1.33333f));
				}
				else
				{
					xlsRange.SetFontSize((double)((float)Utils.FontSizeToPoints(cellStyle.Font.Size.Type)));
				}
			}
			xlsRange.SetIsBold(cellStyle.Font.Bold);
			xlsRange.SetIsItalic(cellStyle.Font.Italic);
			xlsRange.SetUnderline(cellStyle.Font.Underline ? 1 : 0);
			this.ApplyRangeBorderStyle(xlsRange, cellStyle);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00031446 File Offset: 0x0002F646
		private ThemableColor GetThemableColor(System.Drawing.Color color)
		{
			return ThemableColor.FromArgb(color.A, color.R, color.G, color.B);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003146C File Offset: 0x0002F66C
		private void ApplyRangeBorderStyle(CellSelection xlsRange, ExportStyle cellStyle)
		{
			CellBorders cellBorders = new CellBorders();
			if (!cellStyle.BorderTopColor.IsEmpty || cellStyle.BorderTopStyle != BorderStyle.NotSet)
			{
				cellBorders.Top = new CellBorder(Utils.ConvertXlsxBorderStyle(cellStyle.BorderTopStyle), this.GetThemableColor(cellStyle.BorderTopColor));
			}
			if (!cellStyle.BorderBottomColor.IsEmpty || cellStyle.BorderBottomStyle != BorderStyle.NotSet)
			{
				cellBorders.Bottom = new CellBorder(Utils.ConvertXlsxBorderStyle(cellStyle.BorderBottomStyle), this.GetThemableColor(cellStyle.BorderBottomColor));
			}
			if (!cellStyle.BorderLeftColor.IsEmpty || cellStyle.BorderLeftStyle != BorderStyle.NotSet)
			{
				cellBorders.Left = new CellBorder(Utils.ConvertXlsxBorderStyle(cellStyle.BorderLeftStyle), this.GetThemableColor(cellStyle.BorderLeftColor));
			}
			if (!cellStyle.BorderRightColor.IsEmpty || cellStyle.BorderRightStyle != BorderStyle.NotSet)
			{
				cellBorders.Right = new CellBorder(Utils.ConvertXlsxBorderStyle(cellStyle.BorderRightStyle), this.GetThemableColor(cellStyle.BorderRightColor));
			}
			xlsRange.SetBorders(cellBorders);
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00031570 File Offset: 0x0002F770
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x000315CD File Offset: 0x0002F7CD
		internal Font DefaultFont
		{
			get
			{
				if (this.defaultFont == null)
				{
					ThemeFonts themeFonts = PredefinedThemeSchemes.DefaultTheme.FontScheme[SpreadsheetDefaultValues.DefaultFontFamily.ThemeFontType];
					this.defaultFont = new Font(themeFonts[0].FontFamily.Source, (float)this.defaultStyle.FontSize);
				}
				return this.defaultFont;
			}
			set
			{
				this.defaultFont = value;
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000315D6 File Offset: 0x0002F7D6
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.defaultFont.Dispose();
				this.workbook.Dispose();
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x000315F1 File Offset: 0x0002F7F1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400034B RID: 843
		private ExportStructure _structure;

		// Token: 0x0400034C RID: 844
		internal Workbook workbook;

		// Token: 0x0400034D RID: 845
		private CellStyle defaultStyle;

		// Token: 0x0400034E RID: 846
		private Font defaultFont;
	}
}
