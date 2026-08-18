using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;
using System.Windows;
using Telerik.Web.UI.Export;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Fields;
using Telerik.Windows.Documents.Flow.Model.Shapes;
using Telerik.Windows.Documents.Flow.Model.Styles;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.Utilities;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x0200014F RID: 335
	public class DocxRenderer
	{
		// Token: 0x06000D33 RID: 3379 RVA: 0x0002F4A4 File Offset: 0x0002D6A4
		public DocxRenderer(ExportStructure structure)
		{
			this._structure = structure;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0002F510 File Offset: 0x0002D710
		public byte[] Render()
		{
			byte[] output = null;
			this.flowDoc = new RadFlowDocument();
			foreach (Table tbl in this._structure.Tables)
			{
				this.CreateTable(tbl, this.flowDoc);
			}
			Thread thread = new Thread(delegate()
			{
				DocxFormatProvider docxFormatProvider = new DocxFormatProvider();
				using (MemoryStream memoryStream = new MemoryStream())
				{
					docxFormatProvider.Export(this.flowDoc, memoryStream);
					output = memoryStream.ToArray();
				}
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			return output;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0002F5C8 File Offset: 0x0002D7C8
		private void CreateTable(Table tbl, RadFlowDocument flowDoc)
		{
			Table table = flowDoc.Sections.AddSection().Blocks.AddTable();
			List<Point> list = new List<Point>();
			Point point = this.FindMaxCoordinates(tbl);
			string.IsNullOrEmpty(tbl.Title);
			for (int i = 0; i < point.Y; i++)
			{
				TableRow tableRow = table.Rows.AddTableRow();
				for (int j = 0; j < point.X; j++)
				{
					new Point(j + 1, i + 1);
					TableCell tableCell = new TableCell(flowDoc);
					Cell cellSafe = tbl.Cells.GetCellSafe(j + 1, i + 1);
					if (cellSafe != null)
					{
						if (cellSafe.Rowspan > 1 || cellSafe.Colspan > 1)
						{
							tableCell.RowSpan = cellSafe.Rowspan;
							tableCell.ColumnSpan = cellSafe.Colspan;
							this.FindRedundantCells(ref list, cellSafe);
						}
						if (string.IsNullOrEmpty(cellSafe.Hyperlink))
						{
							this.SetCellValue(tableCell, string.IsNullOrEmpty(cellSafe.Format) ? cellSafe.Value : this.ApplyTextFormat(cellSafe.Value, cellSafe.Format));
						}
						else
						{
							this.CreateHyperLink(tableCell, cellSafe.Value, cellSafe.Hyperlink);
						}
						Column column = tbl.Columns.GetColumn(j + 1);
						if (column != null && !column.Style.IsEmpty)
						{
							cellSafe.Style.ImportStyle(column.Style);
							if (column.Style.HasBorderStyles)
							{
								if (i == column.FirstRowIndex - 1)
								{
									cellSafe.Style.ImportBorderStyle(column.Style, CellBorderPosition.Top);
								}
								else if (i == column.LastRowIndex - 1)
								{
									cellSafe.Style.ImportBorderStyle(column.Style, CellBorderPosition.Bottom);
								}
								else
								{
									cellSafe.Style.ImportBorderStyle(column.Style, CellBorderPosition.ColumnMiddle);
								}
							}
						}
						Row row = tbl.Rows.GetRow(i + 1);
						if (row != null && !row.Style.IsEmpty)
						{
							cellSafe.Style.ImportStyle(row.Style);
							if (row.Style.HasBorderStyles)
							{
								if (j == row.FirstColumnIndex - 1)
								{
									cellSafe.Style.ImportBorderStyle(row.Style, CellBorderPosition.Left);
								}
								else if (j == row.LastColumnIndex - 1)
								{
									cellSafe.Style.ImportBorderStyle(row.Style, CellBorderPosition.Right);
								}
								else
								{
									cellSafe.Style.ImportBorderStyle(row.Style, CellBorderPosition.RowMiddle);
								}
							}
						}
						if (!tbl.Style.IsEmpty)
						{
							cellSafe.Style.ImportStyle(tbl.Style);
							if (tbl.Style.HasBorderStyles)
							{
								if (cellSafe.RowIndex == tbl.Cells.FirstCellRowIndex && cellSafe.ColIndex == tbl.Cells.FirstCellColumnIndex)
								{
									cellSafe.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableTopLeft);
								}
								else if (cellSafe.RowIndex == tbl.Cells.LastCellRowIndex && cellSafe.ColIndex == tbl.Cells.FirstCellColumnIndex)
								{
									cellSafe.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableBottomLeft);
								}
								else if (cellSafe.RowIndex == tbl.Cells.LastCellRowIndex && cellSafe.ColIndex == tbl.Cells.LastCellColumnIndex)
								{
									cellSafe.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableBottomRight);
								}
								else if (cellSafe.RowIndex == tbl.Cells.FirstCellRowIndex && cellSafe.ColIndex == tbl.Cells.LastCellColumnIndex)
								{
									cellSafe.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableTopRight);
								}
								else if (cellSafe.RowIndex == tbl.Cells.FirstCellRowIndex)
								{
									cellSafe.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableTop);
								}
								else if (cellSafe.RowIndex == tbl.Cells.LastCellRowIndex)
								{
									cellSafe.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableBottom);
								}
								else if (cellSafe.ColIndex == tbl.Cells.FirstCellColumnIndex)
								{
									cellSafe.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableLeft);
								}
								else if (cellSafe.ColIndex == tbl.Cells.LastCellColumnIndex)
								{
									cellSafe.Style.ImportBorderStyle(tbl.Style, CellBorderPosition.TableRight);
								}
							}
						}
						if (!cellSafe.Style.IsEmpty)
						{
							this.ApplyCellStyle(tableCell, cellSafe.Style);
						}
					}
					tableRow.Cells.Add(tableCell);
				}
			}
			this.PrepareWorksheetRowsAndColumns(table, tbl);
			this.InsertImages(tbl, table);
			list = (from p in list
			orderby p.Y descending, p.X descending
			select p).ToList<Point>();
			foreach (Point point2 in list)
			{
				if (table.Rows[point2.Y].Cells.Count > 0)
				{
					table.Rows[point2.Y].Cells.RemoveAt(point2.X);
				}
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002FB2C File Offset: 0x0002DD2C
		internal string ApplyTextFormat(object value, string format)
		{
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.SByte:
				return ((sbyte)value).ToString(format);
			case TypeCode.Byte:
				return ((byte)value).ToString(format);
			case TypeCode.Int16:
				return ((short)value).ToString(format);
			case TypeCode.UInt16:
				return ((ushort)value).ToString(format);
			case TypeCode.Int32:
				return ((int)value).ToString(format);
			case TypeCode.UInt32:
				return ((uint)value).ToString(format);
			case TypeCode.Int64:
				return ((long)value).ToString(format);
			case TypeCode.UInt64:
				return ((ulong)value).ToString(format);
			case TypeCode.Single:
				return ((float)value).ToString(format);
			case TypeCode.Double:
				return ((double)value).ToString(format);
			case TypeCode.Decimal:
				return ((decimal)value).ToString(format);
			case TypeCode.DateTime:
				return ((DateTime)value).ToString(format);
			default:
				return value.ToString();
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0002FC54 File Offset: 0x0002DE54
		private void InsertImages(Table tbl, Table flowTable)
		{
			foreach (Telerik.Web.UI.ExportInfrastructure.Image image in tbl.Images)
			{
				FloatingImage floatingImage = new FloatingImage(flowTable.Document);
				floatingImage.Image.ImageSource = image.GetImageSource();
				floatingImage.LayoutInCell = true;
				floatingImage.Image.Width = UnitHelper.PointToDip((double)((float)image.Width * 0.72f));
				floatingImage.Image.Height = UnitHelper.PointToDip((double)((float)image.Height * 0.72f));
				TableCell tableCell = flowTable.Rows[image.ImageRange.Start.Y - 1].Cells[image.ImageRange.Start.X - 1];
				if (tableCell.Blocks.Count == 0)
				{
					tableCell.Blocks.AddParagraph();
				}
				(tableCell.Blocks[0] as Paragraph).Inlines.Add(floatingImage);
				tbl.ImageCount++;
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0002FD88 File Offset: 0x0002DF88
		private void FindRedundantCells(ref List<Point> cellsToSkip, Cell eiCell)
		{
			for (int i = eiCell.Index.X; i < eiCell.Index.X + eiCell.Colspan; i++)
			{
				for (int j = eiCell.Index.Y; j < eiCell.Index.Y + eiCell.Rowspan; j++)
				{
					if (i != eiCell.Index.X || j != eiCell.Index.Y)
					{
						cellsToSkip.Add(new Point
						{
							X = i - 1,
							Y = j - 1
						});
					}
				}
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0002FE40 File Offset: 0x0002E040
		private Point FindMaxCoordinates(Table tbl)
		{
			int num = 0;
			int num2 = 0;
			foreach (Cell cell in tbl.Cells)
			{
				if (cell.Index.X > num2)
				{
					num2 = cell.Index.X;
				}
				if (cell.Index.Y > num)
				{
					num = cell.Index.Y;
				}
			}
			return new Point
			{
				X = num2,
				Y = num
			};
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0002FEEC File Offset: 0x0002E0EC
		private void SetCellValue(TableCell docxCell, object cellValue)
		{
			string text = null;
			if (cellValue != null)
			{
				text = cellValue.ToString();
			}
			Paragraph paragraph = docxCell.Blocks.AddParagraph();
			if (!string.IsNullOrEmpty(text))
			{
				if (text.Contains("\n"))
				{
					using (StringReader stringReader = new StringReader(text))
					{
						for (;;)
						{
							string text2 = stringReader.ReadLine();
							if (text2 == null)
							{
								break;
							}
							paragraph.Inlines.AddRun(text2);
							paragraph.Inlines.Add(new Break(docxCell.Document));
						}
						return;
					}
				}
				paragraph.Inlines.AddRun(text);
				return;
			}
			paragraph.Inlines.AddRun(string.Empty);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0002FF98 File Offset: 0x0002E198
		private void CreateHyperLink(TableCell docxCell, object cellValue, string hyperlink)
		{
			FieldInfo fieldInfo = new FieldInfo(this.flowDoc);
			Paragraph paragraph = docxCell.Blocks.AddParagraph();
			paragraph.Inlines.Add(fieldInfo.Start);
			paragraph.Inlines.Add(new Run(this.flowDoc)
			{
				Text = this.CreateHyperlinkCode(hyperlink, false, hyperlink)
			});
			paragraph.Inlines.Add(fieldInfo.Separator);
			paragraph.Inlines.Add(new Run(this.flowDoc)
			{
				Text = cellValue.ToString(),
				StyleId = this.GetHyperLinkStyle()
			});
			paragraph.Inlines.Add(fieldInfo.End);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00030047 File Offset: 0x0002E247
		private string GetHyperLinkStyle()
		{
			if (!this.flowDoc.StyleRepository.Contains("Hyperlink"))
			{
				this.flowDoc.StyleRepository.AddBuiltInStyle("Hyperlink");
			}
			return "Hyperlink";
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003007C File Offset: 0x0002E27C
		private string CreateHyperlinkCode(string uri, bool isAnchor, string tooltip)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" HYPERLINK ");
			if (isAnchor)
			{
				stringBuilder.Append("\\l ");
			}
			stringBuilder.Append("\"");
			stringBuilder.Append(Utils.AddSlashes(uri));
			stringBuilder.Append("\"");
			if (!string.IsNullOrEmpty(tooltip))
			{
				stringBuilder.Append(" \\o \"");
				stringBuilder.Append(Utils.AddSlashes(tooltip));
				stringBuilder.Append("\"");
			}
			stringBuilder.Append(" ");
			return stringBuilder.ToString();
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00030110 File Offset: 0x0002E310
		private void PrepareWorksheetRowsAndColumns(Table flowTable, Table tbl)
		{
			foreach (Row row in tbl.Rows)
			{
				if (row.Height > 0.0)
				{
					flowTable.Rows[row.Index - 1].Height = new TableRowHeight(2, this.ConvertToDip(row.Height, this._structure.RowHeightUnit));
				}
			}
			foreach (Column column in tbl.Columns)
			{
				if (column.Width > 0.0)
				{
					this.SetColumnWidth(flowTable, column.Index - 1, column.Width);
				}
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000301F8 File Offset: 0x0002E3F8
		private void SetColumnWidth(Table flowTable, int columnIndex, double columnWidth)
		{
			foreach (TableRow tableRow in flowTable.Rows)
			{
				if (columnIndex < tableRow.Cells.Count)
				{
					tableRow.Cells[columnIndex].PreferredWidth = new TableWidthUnit(1, this.ConvertToDip(columnWidth, this._structure.ColumnWidthUnit));
				}
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00030278 File Offset: 0x0002E478
		private double ConvertToDip(double value, ExportUnitType targetUnits)
		{
			double num = value;
			switch (targetUnits)
			{
			case ExportUnitType.FormatDefault:
				num = UnitHelper.TwipToDip(value);
				break;
			case ExportUnitType.Point:
				num = UnitHelper.PointToDip(num);
				break;
			}
			return num;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x000302AC File Offset: 0x0002E4AC
		private void ApplyCellStyle(TableCell docxCell, ExportStyle cellStyle)
		{
			Paragraph paragraph = docxCell.Blocks[0] as Paragraph;
			Run run = paragraph.Inlines[0] as Run;
			docxCell.Shading.BackgroundColor = this.GetThemableColor(cellStyle.BackColor);
			run.ForegroundColor = this.GetThemableColor(cellStyle.ForeColor);
			if (cellStyle.HorizontalAlign != HorizontalAlign.NotSet)
			{
				paragraph.TextAlignment = this.ConvertToDocxHorizontalAlignment(cellStyle.HorizontalAlign);
			}
			if (cellStyle.VerticalAlign != VerticalAlign.NotSet)
			{
				docxCell.VerticalAlignment = this.ConvertToDocxVerticalAlignment(cellStyle.VerticalAlign);
			}
			if (!Utils.IsEmptyFontStyle(cellStyle.Font))
			{
				run.FontFamily = new ThemableFontFamily(cellStyle.Font.Name);
			}
			if (!cellStyle.Font.Size.IsEmpty)
			{
				if (cellStyle.Font.Size.Type == FontSize.AsUnit)
				{
					run.FontSize = (double)((float)cellStyle.Font.Size.Unit.Value * 1.33333f);
				}
				else
				{
					run.FontSize = (double)((float)Utils.FontSizeToPoints(cellStyle.Font.Size.Type));
				}
			}
			run.FontWeight = (cellStyle.Font.Bold ? FontWeights.Bold : FontWeights.Regular);
			run.FontStyle = (cellStyle.Font.Italic ? FontStyles.Italic : FontStyles.Normal);
			run.Strikethrough = cellStyle.Font.Strikeout;
			run.Underline.Pattern = (cellStyle.Font.Underline ? 1 : 0);
			this.ApplyBorderStyle(docxCell, cellStyle);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003044C File Offset: 0x0002E64C
		private void ApplyBorderStyle(TableCell docxCell, ExportStyle cellStyle)
		{
			docxCell.Borders = new TableCellBorders(new Border(Utils.GetPointsPerUnit(cellStyle.BorderLeftWidth), this.ConvertToDocxBorderStyle(cellStyle.BorderLeftStyle), this.GetThemableColor(cellStyle.BorderLeftColor)), new Border(Utils.GetPointsPerUnit(cellStyle.BorderTopWidth), this.ConvertToDocxBorderStyle(cellStyle.BorderTopStyle), this.GetThemableColor(cellStyle.BorderTopColor)), new Border(Utils.GetPointsPerUnit(cellStyle.BorderRightWidth), this.ConvertToDocxBorderStyle(cellStyle.BorderRightStyle), this.GetThemableColor(cellStyle.BorderRightColor)), new Border(Utils.GetPointsPerUnit(cellStyle.BorderBottomWidth), this.ConvertToDocxBorderStyle(cellStyle.BorderBottomStyle), this.GetThemableColor(cellStyle.BorderBottomColor)));
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00030504 File Offset: 0x0002E704
		private BorderStyle ConvertToDocxBorderStyle(BorderStyle borderStyle)
		{
			BorderStyle result = 1;
			switch (borderStyle)
			{
			case BorderStyle.NotSet:
			case BorderStyle.None:
				result = 1;
				break;
			case BorderStyle.Dotted:
				result = 3;
				break;
			case BorderStyle.Dashed:
				result = 4;
				break;
			case BorderStyle.Solid:
				result = 2;
				break;
			case BorderStyle.Double:
				result = 8;
				break;
			case BorderStyle.Groove:
				result = 24;
				break;
			case BorderStyle.Ridge:
				result = 23;
				break;
			case BorderStyle.Inset:
				result = 26;
				break;
			case BorderStyle.Outset:
				result = 25;
				break;
			}
			return result;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0003056C File Offset: 0x0002E76C
		private VerticalAlignment ConvertToDocxVerticalAlignment(VerticalAlign verticalAlign)
		{
			VerticalAlignment result;
			switch (verticalAlign)
			{
			case VerticalAlign.Top:
				result = 0;
				break;
			case VerticalAlign.Middle:
				result = 2;
				break;
			default:
				result = 1;
				break;
			}
			return result;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00030598 File Offset: 0x0002E798
		private Alignment ConvertToDocxHorizontalAlignment(HorizontalAlign horizontalAlign)
		{
			Alignment result;
			switch (horizontalAlign)
			{
			case HorizontalAlign.Center:
				result = 1;
				break;
			case HorizontalAlign.Right:
				result = 2;
				break;
			case HorizontalAlign.Justify:
				result = 3;
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000305CC File Offset: 0x0002E7CC
		private ThemableColor GetThemableColor(Color color)
		{
			if (color.IsEmpty)
			{
				return null;
			}
			return ThemableColor.FromArgb(color.A, color.R, color.G, color.B);
		}

		// Token: 0x04000347 RID: 839
		private ExportStructure _structure;

		// Token: 0x04000348 RID: 840
		internal RadFlowDocument flowDoc;
	}
}
