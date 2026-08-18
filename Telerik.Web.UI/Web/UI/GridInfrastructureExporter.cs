using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Export;
using Telerik.Web.UI.ExportInfrastructure;

namespace Telerik.Web.UI
{
	// Token: 0x02000B73 RID: 2931
	internal class GridInfrastructureExporter : IDisposable
	{
		// Token: 0x06006E83 RID: 28291 RVA: 0x00199AC8 File Offset: 0x00197CC8
		public GridInfrastructureExporter(GridTableView gridTableView)
		{
			this.tableView = gridTableView;
			this.structure = new ExportStructure();
			this.structure.ColumnWidthUnit = ExportUnitType.Pixel;
			this.structure.RowHeightUnit = ExportUnitType.Pixel;
			string text = this.tableView.OwnerGrid.ExportSettings.Excel.WorksheetName;
			if (string.IsNullOrEmpty(text))
			{
				text = this.tableView.OwnerGrid.ExportSettings.FileName;
			}
			this.exportTable = new Telerik.Web.UI.ExportInfrastructure.Table(text);
		}

		// Token: 0x06006E84 RID: 28292 RVA: 0x00199B4A File Offset: 0x00197D4A
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06006E85 RID: 28293 RVA: 0x00199B53 File Offset: 0x00197D53
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.structure != null)
			{
				this.structure.Dispose();
			}
		}

		// Token: 0x06006E86 RID: 28294 RVA: 0x00199B8C File Offset: 0x00197D8C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		internal bool CreateRow(GridItem currentItem, ref int invisibleRows, int row, bool isMultiRow)
		{
			GridHeaderItem[] array = null;
			if ((!(currentItem is GridHeaderItem) || this.tableView.ShowHeader) && currentItem.Visible && currentItem.Display && currentItem.Cells.Count != 0)
			{
				if (isMultiRow)
				{
					array = (from i in currentItem.Parent.Controls.OfType<GridHeaderItem>()
					where i.Cells.Count > 0 && i.Visible
					select i).ToArray<GridHeaderItem>();
				}
				currentItem.PrepareItemStyle();
				this.ApplyRowStyle(currentItem, this.exportTable.Rows[row + 1 - invisibleRows]);
				if (currentItem.Height != Unit.Empty)
				{
					this.exportTable.Rows[row + 1 - invisibleRows].Height = Utils.GetPixelsPerUnit(currentItem.Height);
				}
				int count = currentItem.Cells.Count;
				int num = 0;
				int num2 = 1;
				bool hideStructureColumns = this.tableView.OwnerGrid.ExportSettings.HideStructureColumns;
				for (int j = 0; j < count; j++)
				{
					TableCell tableCell = currentItem.Cells[j];
					GridColumn columnByTableCell = this.GetColumnByTableCell(tableCell, isMultiRow, array);
					if ((columnByTableCell != null || !tableCell.Visible) && (!tableCell.Visible || !columnByTableCell.Visible || !columnByTableCell.Display || (hideStructureColumns && (columnByTableCell is GridGroupSplitterColumn || columnByTableCell is GridExpandColumn || columnByTableCell is GridRowIndicatorColumn))))
					{
						num++;
					}
					else
					{
						if (columnByTableCell != null && columnByTableCell.HeaderStyle.Width != Unit.Empty)
						{
							this.exportTable.Columns[j + 1 - num].Width = Utils.GetPixelsPerUnit(columnByTableCell.HeaderStyle.Width);
						}
						if (!string.IsNullOrEmpty(this.tableView.Caption) && row == 1 && j == num)
						{
							Cell cell = this.exportTable.Cells[j + 1 - num, row - invisibleRows];
							Cell cell2 = cell;
							int colspan;
							if (this.tableView.Items.Count <= 0)
							{
								colspan = this.tableView.RenderColumns.Count((GridColumn c) => c.Visible);
							}
							else
							{
								colspan = this.tableView.Items[0].Cells.Count - num;
							}
							cell2.Colspan = colspan;
							cell.Value = this.tableView.Caption;
						}
						int num3 = row + 1 - invisibleRows;
						num2 = this.CalculateColumnIndex(num2, num3);
						Cell cell3 = this.exportTable.Cells[num2, num3];
						cell3.Colspan = ((tableCell.ColumnSpan <= 0) ? 1 : tableCell.ColumnSpan);
						cell3.Rowspan = ((tableCell.RowSpan <= 0) ? 1 : tableCell.RowSpan);
						if (tableCell.RowSpan > 1)
						{
							this.FindSpannedCells(cell3);
						}
						cell3.Value = this.ParseCellContent(tableCell, isMultiRow, array);
						string text = this.ParseDataFormatString(columnByTableCell);
						if (!string.IsNullOrEmpty(text) && !this.tableView.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings)
						{
							cell3.Format = text;
						}
						num2 += cell3.Colspan;
						if (!this.tableView.OwnerGrid.ExportSettings.ExportOnlyData)
						{
							foreach (object obj in tableCell.Controls)
							{
								Control control = (Control)obj;
								System.Web.UI.WebControls.Image image = control as System.Web.UI.WebControls.Image;
								bool autoFitImages = this.tableView.OwnerGrid.ExportSettings.Excel.AutoFitImages;
								if (image != null)
								{
									this.exportTable.InsertImage(cell3, image.ImageUrl, autoFitImages);
								}
								RadBinaryImage radBinaryImage = control as RadBinaryImage;
								if (radBinaryImage != null)
								{
									this.exportTable.InsertImage(cell3, radBinaryImage.ImageUrl, autoFitImages);
								}
							}
						}
						this.ApplyCellStyle(tableCell, cell3);
					}
				}
				return true;
			}
			if (!isMultiRow)
			{
				invisibleRows++;
				return false;
			}
			return currentItem.Visible && currentItem.Display && currentItem.Cells.Count != 0;
		}

		// Token: 0x06006E87 RID: 28295 RVA: 0x00199FD0 File Offset: 0x001981D0
		internal ExportStructure GenerateStructure()
		{
			this.structure.Tables.Add(this.exportTable);
			this.structure.ColumnWidthUnit = ExportUnitType.Pixel;
			this.structure.RowHeightUnit = ExportUnitType.Pixel;
			TableRowCollection rows = this.tableView.GetGridTable().Rows;
			GridMultiRowItem gridMultiRowItem = null;
			int count = rows.Count;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.spannedCells = new List<Point>();
			if (!string.IsNullOrEmpty(this.tableView.Caption))
			{
				num2 = 1;
			}
			int i;
			for (i = num2; i < count + num2; i++)
			{
				GridItem gridItem = rows[i - num2] as GridItem;
				if (gridItem is GridMultiRowItem)
				{
					if (gridItem is GridTFoot)
					{
						if (gridItem.Visible)
						{
							gridMultiRowItem = (gridItem as GridMultiRowItem);
						}
						num++;
					}
					else
					{
						foreach (object obj in gridItem.Controls)
						{
							GridItem currentItem = (GridItem)obj;
							if (this.CreateRow(currentItem, ref num, i + num3, true))
							{
								num3++;
							}
						}
						num3--;
					}
				}
				else
				{
					this.CreateRow(gridItem, ref num, i + num3, false);
				}
			}
			if (gridMultiRowItem != null && gridMultiRowItem.Visible && gridMultiRowItem.Controls.Count >= 1)
			{
				foreach (object obj2 in gridMultiRowItem.Controls)
				{
					GridItem currentItem2 = (GridItem)obj2;
					if (this.CreateRow(currentItem2, ref num, i + num3, true))
					{
						num3++;
					}
				}
			}
			return this.structure;
		}

		// Token: 0x06006E88 RID: 28296 RVA: 0x0019A1A4 File Offset: 0x001983A4
		private int CalculateColumnIndex(int eiCellIndex, int y)
		{
			int num = eiCellIndex;
			if (this.spannedCells.Count == 0)
			{
				return eiCellIndex;
			}
			if (this.spannedCells.Contains(new Point(eiCellIndex, y)))
			{
				if (!this.collectionIsSorted)
				{
					this.spannedCells.Sort(new PointComparer());
					this.collectionIsSorted = true;
				}
				foreach (Point point in this.spannedCells)
				{
					if (point.Y == y && point.X == num)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06006E89 RID: 28297 RVA: 0x0019A250 File Offset: 0x00198450
		private void FindSpannedCells(Cell exportCell)
		{
			Point index = exportCell.Index;
			for (int i = 1; i < exportCell.Rowspan; i++)
			{
				for (int j = 0; j < exportCell.Colspan; j++)
				{
					this.spannedCells.Add(new Point(j + index.X, i + index.Y));
					this.collectionIsSorted = false;
				}
			}
		}

		// Token: 0x06006E8A RID: 28298 RVA: 0x0019A2B0 File Offset: 0x001984B0
		private string ParseDataFormatString(GridColumn currentCol)
		{
			string text = string.Empty;
			GridBoundColumn gridBoundColumn = currentCol as GridBoundColumn;
			GridCalculatedColumn gridCalculatedColumn;
			GridButtonColumn gridButtonColumn;
			GridHyperLinkColumn gridHyperLinkColumn;
			GridAttachmentColumn gridAttachmentColumn;
			if (gridBoundColumn != null)
			{
				text = gridBoundColumn.DataFormatString;
			}
			else if ((gridCalculatedColumn = (currentCol as GridCalculatedColumn)) != null)
			{
				text = gridCalculatedColumn.DataFormatString;
			}
			else if ((gridButtonColumn = (currentCol as GridButtonColumn)) != null)
			{
				text = gridButtonColumn.DataTextFormatString;
			}
			else if ((gridHyperLinkColumn = (currentCol as GridHyperLinkColumn)) != null)
			{
				text = gridHyperLinkColumn.DataTextFormatString;
			}
			else if ((gridAttachmentColumn = (currentCol as GridAttachmentColumn)) != null)
			{
				text = gridAttachmentColumn.DataTextFormatString;
			}
			if (!string.IsNullOrEmpty(text) && text.StartsWith("{0:") && text.EndsWith("}"))
			{
				text = GridInfrastructureExporter.ClearStandardFormatStrings(text);
			}
			return text;
		}

		// Token: 0x06006E8B RID: 28299 RVA: 0x0019A34E File Offset: 0x0019854E
		internal static string ClearStandardFormatStrings(string formatString)
		{
			formatString = formatString.Substring(3, formatString.Length - 4);
			if (Regex.IsMatch(formatString.ToLowerInvariant(), "^[c,d,e,f,g,n,p,r,x]{1}\\d*$"))
			{
				formatString = string.Empty;
			}
			return formatString;
		}

		// Token: 0x06006E8C RID: 28300 RVA: 0x0019A37C File Offset: 0x0019857C
		private void ApplyColumnStyle(GridColumn currentCol, Column exportColumn)
		{
			if (!currentCol.ItemStyle.BackColor.IsEmpty)
			{
				exportColumn.Style.BackColor = currentCol.ItemStyle.BackColor;
			}
			if (!currentCol.ItemStyle.BorderColor.IsEmpty)
			{
				exportColumn.Style.BorderTopColor = (exportColumn.Style.BorderBottomColor = (exportColumn.Style.BorderLeftColor = (exportColumn.Style.BorderRightColor = currentCol.ItemStyle.BorderColor)));
			}
			if (currentCol.ItemStyle.BorderStyle != BorderStyle.NotSet)
			{
				exportColumn.Style.BorderTopStyle = (exportColumn.Style.BorderBottomStyle = (exportColumn.Style.BorderLeftStyle = (exportColumn.Style.BorderRightStyle = currentCol.ItemStyle.BorderStyle)));
			}
			if (!Utils.IsEmptyFontStyle(currentCol.ItemStyle.Font))
			{
				exportColumn.Style.Font.Bold = currentCol.ItemStyle.Font.Bold;
				exportColumn.Style.Font.Italic = currentCol.ItemStyle.Font.Italic;
				exportColumn.Style.Font.Overline = currentCol.ItemStyle.Font.Overline;
				exportColumn.Style.Font.Strikeout = currentCol.ItemStyle.Font.Strikeout;
				exportColumn.Style.Font.Underline = currentCol.ItemStyle.Font.Underline;
				if (!string.IsNullOrEmpty(currentCol.ItemStyle.Font.Name))
				{
					exportColumn.Style.Font.Name = currentCol.ItemStyle.Font.Name;
				}
				if (!currentCol.ItemStyle.Font.Size.IsEmpty)
				{
					exportColumn.Style.Font.Size = currentCol.ItemStyle.Font.Size;
				}
			}
			if (!currentCol.ItemStyle.ForeColor.IsEmpty)
			{
				exportColumn.Style.ForeColor = currentCol.ItemStyle.ForeColor;
			}
			if (currentCol.ItemStyle.HorizontalAlign != HorizontalAlign.NotSet)
			{
				exportColumn.Style.HorizontalAlign = currentCol.ItemStyle.HorizontalAlign;
			}
			if (currentCol.ItemStyle.VerticalAlign != VerticalAlign.NotSet)
			{
				exportColumn.Style.VerticalAlign = currentCol.ItemStyle.VerticalAlign;
			}
		}

		// Token: 0x06006E8D RID: 28301 RVA: 0x0019A5F8 File Offset: 0x001987F8
		private void ApplyRowStyle(GridItem currentItem, Row exportRow)
		{
			HorizontalAlign defaultCellAlignment = this.tableView.OwnerGrid.ExportSettings.Excel.DefaultCellAlignment;
			if (!currentItem.BackColor.IsEmpty)
			{
				exportRow.Style.BackColor = currentItem.BackColor;
			}
			if (!currentItem.BorderColor.IsEmpty)
			{
				exportRow.Style.BorderTopColor = (exportRow.Style.BorderBottomColor = (exportRow.Style.BorderLeftColor = (exportRow.Style.BorderRightColor = currentItem.BorderColor)));
			}
			if (currentItem.BorderStyle != BorderStyle.NotSet)
			{
				exportRow.Style.BorderTopStyle = (exportRow.Style.BorderBottomStyle = (exportRow.Style.BorderLeftStyle = (exportRow.Style.BorderRightStyle = currentItem.BorderStyle)));
			}
			if (!Utils.IsEmptyFontStyle(currentItem.Font))
			{
				exportRow.Style.Font.Bold = currentItem.Font.Bold;
				exportRow.Style.Font.Italic = currentItem.Font.Italic;
				exportRow.Style.Font.Overline = currentItem.Font.Overline;
				exportRow.Style.Font.Strikeout = currentItem.Font.Strikeout;
				exportRow.Style.Font.Underline = currentItem.Font.Underline;
				if (!string.IsNullOrEmpty(currentItem.Font.Name))
				{
					exportRow.Style.Font.Name = currentItem.Font.Name;
				}
				if (!currentItem.Font.Size.IsEmpty)
				{
					exportRow.Style.Font.Size = currentItem.Font.Size;
				}
			}
			if (!currentItem.ForeColor.IsEmpty)
			{
				exportRow.Style.ForeColor = currentItem.ForeColor;
			}
			if (currentItem.HorizontalAlign != HorizontalAlign.NotSet || defaultCellAlignment != HorizontalAlign.NotSet)
			{
				exportRow.Style.HorizontalAlign = ((defaultCellAlignment != HorizontalAlign.NotSet) ? defaultCellAlignment : currentItem.HorizontalAlign);
			}
			if (currentItem.VerticalAlign != VerticalAlign.NotSet)
			{
				exportRow.Style.VerticalAlign = currentItem.VerticalAlign;
			}
		}

		// Token: 0x06006E8E RID: 28302 RVA: 0x0019A82C File Offset: 0x00198A2C
		private void ApplyCellStyle(TableCell currentCell, Cell exportCell)
		{
			if (!currentCell.BackColor.IsEmpty)
			{
				exportCell.Style.BackColor = currentCell.BackColor;
			}
			if (!currentCell.BorderColor.IsEmpty)
			{
				exportCell.Style.BorderTopColor = (exportCell.Style.BorderBottomColor = (exportCell.Style.BorderLeftColor = (exportCell.Style.BorderRightColor = currentCell.BorderColor)));
			}
			if (currentCell.BorderStyle != BorderStyle.NotSet)
			{
				exportCell.Style.BorderTopStyle = (exportCell.Style.BorderBottomStyle = (exportCell.Style.BorderLeftStyle = (exportCell.Style.BorderRightStyle = currentCell.BorderStyle)));
			}
			if (!Utils.IsEmptyFontStyle(currentCell.Font))
			{
				exportCell.Style.Font.Bold = currentCell.Font.Bold;
				exportCell.Style.Font.Italic = currentCell.Font.Italic;
				exportCell.Style.Font.Overline = currentCell.Font.Overline;
				exportCell.Style.Font.Strikeout = currentCell.Font.Strikeout;
				exportCell.Style.Font.Underline = currentCell.Font.Underline;
				if (!string.IsNullOrEmpty(currentCell.Font.Name))
				{
					exportCell.Style.Font.Name = currentCell.Font.Name;
				}
				if (!currentCell.Font.Size.IsEmpty)
				{
					exportCell.Style.Font.Size = currentCell.Font.Size;
				}
			}
			if (!currentCell.ForeColor.IsEmpty)
			{
				exportCell.Style.ForeColor = currentCell.ForeColor;
			}
			if (currentCell.HorizontalAlign != HorizontalAlign.NotSet)
			{
				exportCell.Style.HorizontalAlign = currentCell.HorizontalAlign;
			}
			if (currentCell.VerticalAlign != VerticalAlign.NotSet)
			{
				exportCell.Style.VerticalAlign = currentCell.VerticalAlign;
			}
		}

		// Token: 0x06006E8F RID: 28303 RVA: 0x0019AA3C File Offset: 0x00198C3C
		private object ParseCellContent(TableCell currentCell, bool isMultiRow, GridHeaderItem[] subHeaders)
		{
			object result = null;
			if (currentCell.HasControls())
			{
				string cellText = string.Empty;
				Control control = currentCell.Controls[0];
				if (control.GetType().FullName == "System.Web.DynamicData.DynamicControl")
				{
					Control control2 = control.Controls[0];
					cellText = this.ParseCellControls(control2.Controls, true);
				}
				else
				{
					cellText = this.ParseCellControls(currentCell.Controls, false);
				}
				result = Utils.SanitizeCellText(cellText);
			}
			else
			{
				if (currentCell.Parent is GridDataItem || currentCell.Parent is GridFooterItem)
				{
					Type type = this.GetCellDataFieldType(currentCell, isMultiRow, subHeaders);
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
				if (currentCell.Parent is GridGroupHeaderItem)
				{
					result = Utils.SanitizeCellText(currentCell.Text).Replace("<p>", "").Replace("</p>", "");
				}
				else
				{
					result = Utils.SanitizeCellText(currentCell.Text);
				}
			}
			return result;
		}

		// Token: 0x06006E90 RID: 28304 RVA: 0x0019AE44 File Offset: 0x00199044
		private string ParseCellControls(ControlCollection controlCollection, bool decodeHtmlTags)
		{
			string text = string.Empty;
			foreach (object obj in controlCollection)
			{
				Control control = (Control)obj;
				if (control is ITextControl)
				{
					text += ((ITextControl)control).Text.TrimEnd(new char[0]);
				}
				if (control is ICheckBoxControl)
				{
					text += ((ICheckBoxControl)control).Checked.ToString();
				}
				if (!this.tableView.OwnerGrid.ExportSettings.ExportOnlyData && control is IButtonControl)
				{
					text += ((IButtonControl)control).Text.TrimEnd(new char[0]);
				}
			}
			if (!decodeHtmlTags)
			{
				return text;
			}
			return HttpUtility.HtmlDecode(text);
		}

		// Token: 0x06006E91 RID: 28305 RVA: 0x0019AF38 File Offset: 0x00199138
		private GridColumn GetColumnByTableCell(TableCell currentCell, bool isMultiRow, GridHeaderItem[] headerItems)
		{
			GridItem gridItem = currentCell.Parent as GridItem;
			Control parent = gridItem.Parent;
			int cellIndex = gridItem.Cells.GetCellIndex(currentCell);
			int num = 0;
			int num2 = 0;
			while (num2 < gridItem.Cells.Count && gridItem.Cells[num2] != currentCell)
			{
				if (!gridItem.Cells[num2].Visible)
				{
					num++;
				}
				num2++;
			}
			if (gridItem is GridGroupHeaderItem && cellIndex > 0)
			{
				return null;
			}
			if (isMultiRow && currentCell.Parent is GridHeaderItem)
			{
				if (headerItems.Last<GridHeaderItem>() == currentCell.Parent && currentCell.Visible)
				{
					if (headerItems.Count<GridHeaderItem>() == 1)
					{
						return this.tableView.RenderColumns[cellIndex];
					}
					if (this.tableView.AutoGenerateColumns)
					{
						return this.tableView.AutoGeneratedColumns[cellIndex];
					}
				}
				else if (headerItems.First<GridHeaderItem>() != currentCell.Parent)
				{
					return null;
				}
				List<GridColumn> list = (from c in this.tableView.RenderColumns
				where c.Visible
				select c).ToList<GridColumn>();
				if (list.Count > 0 && headerItems.Length > 1)
				{
					if (cellIndex - num < list.Count)
					{
						return list[cellIndex - num];
					}
					return null;
				}
				else
				{
					if (cellIndex < this.tableView.RenderColumns.Length)
					{
						return this.tableView.RenderColumns[cellIndex];
					}
					return null;
				}
			}
			else
			{
				if (cellIndex < this.tableView.RenderColumns.Length)
				{
					return this.tableView.RenderColumns[cellIndex];
				}
				return null;
			}
		}

		// Token: 0x06006E92 RID: 28306 RVA: 0x0019B0BC File Offset: 0x001992BC
		private Type GetCellDataFieldType(TableCell currentCell, bool isMultiRow, GridHeaderItem[] subHeaders)
		{
			Type result = typeof(string);
			GridColumn columnByTableCell = this.GetColumnByTableCell(currentCell, isMultiRow, subHeaders);
			if (columnByTableCell != null)
			{
				result = columnByTableCell.DataType;
			}
			return result;
		}

		// Token: 0x04001DD5 RID: 7637
		private GridTableView tableView;

		// Token: 0x04001DD6 RID: 7638
		private ExportStructure structure;

		// Token: 0x04001DD7 RID: 7639
		private Telerik.Web.UI.ExportInfrastructure.Table exportTable;

		// Token: 0x04001DD8 RID: 7640
		private List<Point> spannedCells;

		// Token: 0x04001DD9 RID: 7641
		private bool collectionIsSorted;
	}
}
