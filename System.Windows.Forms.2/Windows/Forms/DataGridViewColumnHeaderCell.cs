using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x020001C5 RID: 453
	public class DataGridViewColumnHeaderCell : DataGridViewHeaderCell
	{
		// Token: 0x06001FB2 RID: 8114 RVA: 0x00095C18 File Offset: 0x00093E18
		public DataGridViewColumnHeaderCell()
		{
			if (!DataGridViewColumnHeaderCell.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth = (byte)DpiHelper.LogicalToDeviceUnitsX(2);
					DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin = (byte)DpiHelper.LogicalToDeviceUnitsX(4);
					DataGridViewColumnHeaderCell.sortGlyphWidth = (byte)DpiHelper.LogicalToDeviceUnitsX(9);
					if (DataGridViewColumnHeaderCell.sortGlyphWidth % 2 == 0)
					{
						DataGridViewColumnHeaderCell.sortGlyphWidth += 1;
					}
					DataGridViewColumnHeaderCell.sortGlyphHeight = (byte)DpiHelper.LogicalToDeviceUnitsY(7);
				}
				DataGridViewColumnHeaderCell.isScalingInitialized = true;
			}
			this.sortGlyphDirection = SortOrder.None;
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x00095C8D File Offset: 0x00093E8D
		internal bool ContainsLocalValue
		{
			get
			{
				return base.Properties.ContainsObject(DataGridViewCell.PropCellValue);
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x00095C9F File Offset: 0x00093E9F
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x00095CA8 File Offset: 0x00093EA8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SortOrder SortGlyphDirection
		{
			get
			{
				return this.sortGlyphDirection;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SortOrder));
				}
				if (base.OwningColumn == null || base.DataGridView == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_CellDoesNotYetBelongToDataGridView"));
				}
				if (value != this.sortGlyphDirection)
				{
					if (base.OwningColumn.SortMode == DataGridViewColumnSortMode.NotSortable && value != SortOrder.None)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewColumnHeaderCell_SortModeAndSortGlyphDirectionClash", new object[]
						{
							value.ToString()
						}));
					}
					this.sortGlyphDirection = value;
					base.DataGridView.OnSortGlyphDirectionChanged(this);
				}
			}
		}

		// Token: 0x1700071A RID: 1818
		// (set) Token: 0x06001FB6 RID: 8118 RVA: 0x00095D4D File Offset: 0x00093F4D
		internal SortOrder SortGlyphDirectionInternal
		{
			set
			{
				this.sortGlyphDirection = value;
			}
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00095D58 File Offset: 0x00093F58
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewColumnHeaderCell dataGridViewColumnHeaderCell;
			if (type == DataGridViewColumnHeaderCell.cellType)
			{
				dataGridViewColumnHeaderCell = new DataGridViewColumnHeaderCell();
			}
			else
			{
				dataGridViewColumnHeaderCell = (DataGridViewColumnHeaderCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewColumnHeaderCell);
			dataGridViewColumnHeaderCell.Value = base.Value;
			return dataGridViewColumnHeaderCell;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00095DA1 File Offset: 0x00093FA1
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject(this);
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x00095DAC File Offset: 0x00093FAC
		protected override object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView == null)
			{
				return null;
			}
			object value = this.GetValue(rowIndex);
			StringBuilder stringBuilder = new StringBuilder(64);
			if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
			{
				if (firstCell)
				{
					stringBuilder.Append("<TABLE>");
					stringBuilder.Append("<THEAD>");
				}
				stringBuilder.Append("<TH>");
				if (value != null)
				{
					DataGridViewCell.FormatPlainTextAsHtml(value.ToString(), new StringWriter(stringBuilder, CultureInfo.CurrentCulture));
				}
				else
				{
					stringBuilder.Append("&nbsp;");
				}
				stringBuilder.Append("</TH>");
				if (lastCell)
				{
					stringBuilder.Append("</THEAD>");
					if (inLastRow)
					{
						stringBuilder.Append("</TABLE>");
					}
				}
				return stringBuilder.ToString();
			}
			bool flag = string.Equals(format, DataFormats.CommaSeparatedValue, StringComparison.OrdinalIgnoreCase);
			if (flag || string.Equals(format, DataFormats.Text, StringComparison.OrdinalIgnoreCase) || string.Equals(format, DataFormats.UnicodeText, StringComparison.OrdinalIgnoreCase))
			{
				if (value != null)
				{
					bool flag2 = false;
					int length = stringBuilder.Length;
					DataGridViewCell.FormatPlainText(value.ToString(), flag, new StringWriter(stringBuilder, CultureInfo.CurrentCulture), ref flag2);
					if (flag2)
					{
						stringBuilder.Insert(length, '"');
					}
				}
				if (lastCell)
				{
					if (!inLastRow)
					{
						stringBuilder.Append('\r');
						stringBuilder.Append('\n');
					}
				}
				else
				{
					stringBuilder.Append(flag ? ',' : '\t');
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00095F0C File Offset: 0x0009410C
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView == null || base.OwningColumn == null)
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates dataGridViewElementState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out dataGridViewElementState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementState, value, cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, false);
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00095F78 File Offset: 0x00094178
		public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			ContextMenuStrip contextMenuStrip = base.GetContextMenuStrip(-1);
			if (contextMenuStrip != null)
			{
				return contextMenuStrip;
			}
			if (base.DataGridView != null)
			{
				return base.DataGridView.ContextMenuStrip;
			}
			return null;
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00095FB8 File Offset: 0x000941B8
		public override DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			DataGridViewCellStyle dataGridViewCellStyle = (inheritedCellStyle == null) ? new DataGridViewCellStyle() : inheritedCellStyle;
			DataGridViewCellStyle dataGridViewCellStyle2 = null;
			if (base.HasStyle)
			{
				dataGridViewCellStyle2 = base.Style;
			}
			DataGridViewCellStyle columnHeadersDefaultCellStyle = base.DataGridView.ColumnHeadersDefaultCellStyle;
			DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
			if (includeColors)
			{
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = dataGridViewCellStyle2.BackColor;
				}
				else if (!columnHeadersDefaultCellStyle.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = columnHeadersDefaultCellStyle.BackColor;
				}
				else
				{
					dataGridViewCellStyle.BackColor = defaultCellStyle.BackColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = dataGridViewCellStyle2.ForeColor;
				}
				else if (!columnHeadersDefaultCellStyle.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = columnHeadersDefaultCellStyle.ForeColor;
				}
				else
				{
					dataGridViewCellStyle.ForeColor = defaultCellStyle.ForeColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = dataGridViewCellStyle2.SelectionBackColor;
				}
				else if (!columnHeadersDefaultCellStyle.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = columnHeadersDefaultCellStyle.SelectionBackColor;
				}
				else
				{
					dataGridViewCellStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = dataGridViewCellStyle2.SelectionForeColor;
				}
				else if (!columnHeadersDefaultCellStyle.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = columnHeadersDefaultCellStyle.SelectionForeColor;
				}
				else
				{
					dataGridViewCellStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
				}
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Font != null)
			{
				dataGridViewCellStyle.Font = dataGridViewCellStyle2.Font;
			}
			else if (columnHeadersDefaultCellStyle.Font != null)
			{
				dataGridViewCellStyle.Font = columnHeadersDefaultCellStyle.Font;
			}
			else
			{
				dataGridViewCellStyle.Font = defaultCellStyle.Font;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = dataGridViewCellStyle2.NullValue;
			}
			else if (!columnHeadersDefaultCellStyle.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = columnHeadersDefaultCellStyle.NullValue;
			}
			else
			{
				dataGridViewCellStyle.NullValue = defaultCellStyle.NullValue;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = dataGridViewCellStyle2.DataSourceNullValue;
			}
			else if (!columnHeadersDefaultCellStyle.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = columnHeadersDefaultCellStyle.DataSourceNullValue;
			}
			else
			{
				dataGridViewCellStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = dataGridViewCellStyle2.Format;
			}
			else if (columnHeadersDefaultCellStyle.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = columnHeadersDefaultCellStyle.Format;
			}
			else
			{
				dataGridViewCellStyle.Format = defaultCellStyle.Format;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = dataGridViewCellStyle2.FormatProvider;
			}
			else if (!columnHeadersDefaultCellStyle.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = columnHeadersDefaultCellStyle.FormatProvider;
			}
			else
			{
				dataGridViewCellStyle.FormatProvider = defaultCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = dataGridViewCellStyle2.Alignment;
			}
			else if (columnHeadersDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = columnHeadersDefaultCellStyle.Alignment;
			}
			else
			{
				dataGridViewCellStyle.AlignmentInternal = defaultCellStyle.Alignment;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = dataGridViewCellStyle2.WrapMode;
			}
			else if (columnHeadersDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = columnHeadersDefaultCellStyle.WrapMode;
			}
			else
			{
				dataGridViewCellStyle.WrapModeInternal = defaultCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Tag != null)
			{
				dataGridViewCellStyle.Tag = dataGridViewCellStyle2.Tag;
			}
			else if (columnHeadersDefaultCellStyle.Tag != null)
			{
				dataGridViewCellStyle.Tag = columnHeadersDefaultCellStyle.Tag;
			}
			else
			{
				dataGridViewCellStyle.Tag = defaultCellStyle.Tag;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = dataGridViewCellStyle2.Padding;
			}
			else if (columnHeadersDefaultCellStyle.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = columnHeadersDefaultCellStyle.Padding;
			}
			else
			{
				dataGridViewCellStyle.PaddingInternal = defaultCellStyle.Padding;
			}
			return dataGridViewCellStyle;
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00096378 File Offset: 0x00094578
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
			DataGridViewAdvancedBorderStyle advancedBorderStyle = base.DataGridView.AdjustColumnHeaderBorderStyle(base.DataGridView.AdvancedColumnHeadersBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, false, false);
			Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
			int num = rectangle.Left + rectangle.Width + cellStyle.Padding.Horizontal;
			int num2 = rectangle.Top + rectangle.Height + cellStyle.Padding.Vertical;
			TextFormatFlags flags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			string text = this.GetValue(rowIndex) as string;
			Size result;
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
				{
					result = new Size(0, 0);
					if (!string.IsNullOrEmpty(text))
					{
						if (cellStyle.WrapMode == DataGridViewTriState.True)
						{
							result = new Size(DataGridViewCell.MeasureTextWidth(graphics, text, cellStyle.Font, Math.Max(1, constraintSize.Height - num2 - 2), flags), 0);
						}
						else
						{
							result = new Size(DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, flags).Width, 0);
						}
					}
					if (constraintSize.Height - num2 - 2 > (int)DataGridViewColumnHeaderCell.sortGlyphHeight && base.OwningColumn != null && base.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
					{
						result.Width += (int)(DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin);
						if (!string.IsNullOrEmpty(text))
						{
							result.Width += (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth;
						}
					}
					result.Width = Math.Max(result.Width, 1);
				}
				else
				{
					if (!string.IsNullOrEmpty(text))
					{
						if (cellStyle.WrapMode == DataGridViewTriState.True)
						{
							result = DataGridViewCell.MeasureTextPreferredSize(graphics, text, cellStyle.Font, 5f, flags);
						}
						else
						{
							result = DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, flags);
						}
					}
					else
					{
						result = new Size(0, 0);
					}
					if (base.OwningColumn != null && base.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
					{
						result.Width += (int)(DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin);
						if (!string.IsNullOrEmpty(text))
						{
							result.Width += (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth;
						}
						result.Height = Math.Max(result.Height, (int)DataGridViewColumnHeaderCell.sortGlyphHeight);
					}
					result.Width = Math.Max(result.Width, 1);
					result.Height = Math.Max(result.Height, 1);
				}
			}
			else
			{
				int num3 = constraintSize.Width - num;
				result = new Size(0, 0);
				Size empty;
				if (num3 >= (int)(DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin) && base.OwningColumn != null && base.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
				{
					empty = new Size((int)(DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin), (int)DataGridViewColumnHeaderCell.sortGlyphHeight);
				}
				else
				{
					empty = Size.Empty;
				}
				if (num3 - 2 - 2 > 0 && !string.IsNullOrEmpty(text))
				{
					if (cellStyle.WrapMode == DataGridViewTriState.True)
					{
						if (empty.Width > 0 && num3 - 2 - 2 - (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth - empty.Width > 0)
						{
							result = new Size(0, DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, num3 - 2 - 2 - (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth - empty.Width, flags));
						}
						else
						{
							result = new Size(0, DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, num3 - 2 - 2, flags));
						}
					}
					else
					{
						result = new Size(0, DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, flags).Height);
					}
				}
				result.Height = Math.Max(result.Height, empty.Height);
				result.Height = Math.Max(result.Height, 1);
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				if (!string.IsNullOrEmpty(text))
				{
					result.Width += 4;
				}
				result.Width += num;
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				result.Height += 2 + num2;
			}
			if (base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				Rectangle themeMargins = DataGridViewHeaderCell.GetThemeMargins(graphics);
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
				{
					result.Width += themeMargins.X + themeMargins.Width;
				}
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
				{
					result.Height += themeMargins.Y + themeMargins.Height;
				}
			}
			return result;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x000967EE File Offset: 0x000949EE
		protected override object GetValue(int rowIndex)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (this.ContainsLocalValue)
			{
				return base.Properties.GetObject(DataGridViewCell.PropCellValue);
			}
			if (base.OwningColumn != null)
			{
				return base.OwningColumn.Name;
			}
			return null;
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00096830 File Offset: 0x00094A30
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, formattedValue, cellStyle, advancedBorderStyle, paintParts, true);
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00096864 File Offset: 0x00094A64
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object formattedValue, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool paint)
		{
			Rectangle result = Rectangle.Empty;
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle = cellBounds;
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			rectangle.Offset(rectangle2.X, rectangle2.Y);
			rectangle.Width -= rectangle2.Right;
			rectangle.Height -= rectangle2.Bottom;
			Rectangle rectangle3 = rectangle;
			bool flag = (dataGridViewElementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			if (base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				if (cellStyle.Padding != Padding.Empty && cellStyle.Padding != Padding.Empty)
				{
					if (base.DataGridView.RightToLeftInternal)
					{
						rectangle.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
					}
					else
					{
						rectangle.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
					}
					rectangle.Width -= cellStyle.Padding.Horizontal;
					rectangle.Height -= cellStyle.Padding.Vertical;
				}
				if (paint && DataGridViewCell.PaintBackground(paintParts) && rectangle3.Width > 0 && rectangle3.Height > 0)
				{
					int headerState = 1;
					if ((base.OwningColumn != null && base.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable) || base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
					{
						if (base.ButtonState != ButtonState.Normal)
						{
							headerState = 3;
						}
						else if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex)
						{
							headerState = 2;
						}
						else if (flag)
						{
							headerState = 3;
						}
					}
					if (this.IsHighlighted())
					{
						headerState = 3;
					}
					if (base.DataGridView.RightToLeftInternal)
					{
						Bitmap bitmap = base.FlipXPThemesBitmap;
						if (bitmap == null || bitmap.Width < rectangle3.Width || bitmap.Width > 2 * rectangle3.Width || bitmap.Height < rectangle3.Height || bitmap.Height > 2 * rectangle3.Height)
						{
							bitmap = (base.FlipXPThemesBitmap = new Bitmap(rectangle3.Width, rectangle3.Height));
						}
						Graphics g2 = Graphics.FromImage(bitmap);
						DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.DrawHeader(g2, new Rectangle(0, 0, rectangle3.Width, rectangle3.Height), headerState);
						bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
						g.DrawImage(bitmap, rectangle3, new Rectangle(bitmap.Width - rectangle3.Width, 0, rectangle3.Width, rectangle3.Height), GraphicsUnit.Pixel);
					}
					else
					{
						DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.DrawHeader(g, rectangle3, headerState);
					}
				}
				Rectangle themeMargins = DataGridViewHeaderCell.GetThemeMargins(g);
				rectangle.Y += themeMargins.Y;
				rectangle.Height -= themeMargins.Y + themeMargins.Height;
				if (base.DataGridView.RightToLeftInternal)
				{
					rectangle.X += themeMargins.Width;
					rectangle.Width -= themeMargins.X + themeMargins.Width;
				}
				else
				{
					rectangle.X += themeMargins.X;
					rectangle.Width -= themeMargins.X + themeMargins.Width;
				}
			}
			else
			{
				if (paint && DataGridViewCell.PaintBackground(paintParts) && rectangle3.Width > 0 && rectangle3.Height > 0)
				{
					SolidBrush cachedBrush = base.DataGridView.GetCachedBrush(((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) || this.IsHighlighted()) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
					if (cachedBrush.Color.A == 255)
					{
						g.FillRectangle(cachedBrush, rectangle3);
					}
				}
				if (cellStyle.Padding != Padding.Empty)
				{
					if (base.DataGridView.RightToLeftInternal)
					{
						rectangle.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
					}
					else
					{
						rectangle.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
					}
					rectangle.Width -= cellStyle.Padding.Horizontal;
					rectangle.Height -= cellStyle.Padding.Vertical;
				}
			}
			bool flag2 = false;
			Point point = new Point(0, 0);
			string text = formattedValue as string;
			rectangle.Y++;
			rectangle.Height -= 2;
			if (rectangle.Width - 2 - 2 > 0 && rectangle.Height > 0 && !string.IsNullOrEmpty(text))
			{
				rectangle.Offset(2, 0);
				rectangle.Width -= 4;
				Color foreColor;
				if (base.DataGridView.ApplyVisualStylesToHeaderCells)
				{
					foreColor = DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
				}
				else
				{
					foreColor = (flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
				}
				if (base.OwningColumn != null && base.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
				{
					int num = rectangle.Width - (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth - (int)DataGridViewColumnHeaderCell.sortGlyphWidth - (int)(2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin);
					if (num > 0)
					{
						bool flag3;
						int preferredTextHeight = DataGridViewCell.GetPreferredTextHeight(g, base.DataGridView.RightToLeftInternal, text, cellStyle, num, out flag3);
						if (preferredTextHeight <= rectangle.Height && !flag3)
						{
							flag2 = (this.SortGlyphDirection > SortOrder.None);
							rectangle.Width -= (int)(DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth + DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin);
							if (base.DataGridView.RightToLeftInternal)
							{
								rectangle.X += (int)(DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth + DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin);
								point = new Point(rectangle.Left - 2 - (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth - (int)DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin - (int)DataGridViewColumnHeaderCell.sortGlyphWidth, rectangle.Top + (rectangle.Height - (int)DataGridViewColumnHeaderCell.sortGlyphHeight) / 2);
							}
							else
							{
								point = new Point(rectangle.Right + 2 + (int)DataGridViewColumnHeaderCell.sortGlyphSeparatorWidth + (int)DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin, rectangle.Top + (rectangle.Height - (int)DataGridViewColumnHeaderCell.sortGlyphHeight) / 2);
							}
						}
					}
				}
				TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
				if (paint)
				{
					if (DataGridViewCell.PaintContentForeground(paintParts))
					{
						if ((textFormatFlags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
						{
							textFormatFlags |= TextFormatFlags.EndEllipsis;
						}
						TextRenderer.DrawText(g, text, cellStyle.Font, rectangle, foreColor, textFormatFlags);
					}
				}
				else
				{
					result = DataGridViewUtilities.GetTextBounds(rectangle, text, textFormatFlags, cellStyle);
				}
			}
			else if (paint && this.SortGlyphDirection != SortOrder.None && rectangle.Width >= (int)(DataGridViewColumnHeaderCell.sortGlyphWidth + 2 * DataGridViewColumnHeaderCell.sortGlyphHorizontalMargin) && rectangle.Height >= (int)DataGridViewColumnHeaderCell.sortGlyphHeight)
			{
				flag2 = true;
				point = new Point(rectangle.Left + (rectangle.Width - (int)DataGridViewColumnHeaderCell.sortGlyphWidth) / 2, rectangle.Top + (rectangle.Height - (int)DataGridViewColumnHeaderCell.sortGlyphHeight) / 2);
			}
			if (paint && flag2 && DataGridViewCell.PaintContentBackground(paintParts))
			{
				Pen pen = null;
				Pen pen2 = null;
				base.GetContrastedPens(cellStyle.BackColor, ref pen, ref pen2);
				if (this.SortGlyphDirection == SortOrder.Ascending)
				{
					DataGridViewAdvancedCellBorderStyle right = advancedBorderStyle.Right;
					if (right != DataGridViewAdvancedCellBorderStyle.Inset)
					{
						if (right - DataGridViewAdvancedCellBorderStyle.Outset <= 2)
						{
							g.DrawLine(pen, point.X, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y);
							g.DrawLine(pen, point.X + 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y);
							g.DrawLine(pen2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2);
							g.DrawLine(pen2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 3, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2);
							g.DrawLine(pen2, point.X, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
						}
						else
						{
							for (int i = 0; i < (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2); i++)
							{
								g.DrawLine(pen, point.X + i, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - i - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - i - 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - i - 1);
							}
							g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2));
						}
					}
					else
					{
						g.DrawLine(pen2, point.X, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y);
						g.DrawLine(pen2, point.X + 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y);
						g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2);
						g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 3, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 2);
						g.DrawLine(pen, point.X, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
					}
				}
				else
				{
					DataGridViewAdvancedCellBorderStyle right2 = advancedBorderStyle.Right;
					if (right2 != DataGridViewAdvancedCellBorderStyle.Inset)
					{
						if (right2 - DataGridViewAdvancedCellBorderStyle.Outset <= 2)
						{
							g.DrawLine(pen, point.X, point.Y + 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
							g.DrawLine(pen, point.X + 1, point.Y + 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
							g.DrawLine(pen2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + 1);
							g.DrawLine(pen2, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 3, point.Y + 1);
							g.DrawLine(pen2, point.X, point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y);
						}
						else
						{
							for (int j = 0; j < (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2); j++)
							{
								g.DrawLine(pen, point.X + j, point.Y + j + 2, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - j - 1, point.Y + j + 2);
							}
							g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) + 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) + 2);
						}
					}
					else
					{
						g.DrawLine(pen2, point.X, point.Y + 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
						g.DrawLine(pen2, point.X + 1, point.Y + 1, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2) - 1, point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1);
						g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y + 1);
						g.DrawLine(pen, point.X + (int)(DataGridViewColumnHeaderCell.sortGlyphWidth / 2), point.Y + (int)DataGridViewColumnHeaderCell.sortGlyphHeight - 1, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 3, point.Y + 1);
						g.DrawLine(pen, point.X, point.Y, point.X + (int)DataGridViewColumnHeaderCell.sortGlyphWidth - 2, point.Y);
					}
				}
			}
			return result;
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00097630 File Offset: 0x00095830
		private bool IsHighlighted()
		{
			return base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect && base.DataGridView.CurrentCell != null && base.DataGridView.CurrentCell.Selected && base.DataGridView.CurrentCell.OwningColumn == base.OwningColumn && AccessibilityImprovements.Level2;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0009768C File Offset: 0x0009588C
		protected override bool SetValue(int rowIndex, object value)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			object value2 = this.GetValue(rowIndex);
			base.Properties.SetObject(DataGridViewCell.PropCellValue, value);
			if (base.DataGridView != null && value2 != value)
			{
				base.RaiseCellValueChanged(new DataGridViewCellEventArgs(base.ColumnIndex, -1));
			}
			return true;
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x000976E0 File Offset: 0x000958E0
		public override string ToString()
		{
			return "DataGridViewColumnHeaderCell { ColumnIndex=" + base.ColumnIndex.ToString(CultureInfo.CurrentCulture) + " }";
		}

		// Token: 0x04000D55 RID: 3413
		private static readonly VisualStyleElement HeaderElement = VisualStyleElement.Header.Item.Normal;

		// Token: 0x04000D56 RID: 3414
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_sortGlyphSeparatorWidth = 2;

		// Token: 0x04000D57 RID: 3415
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_sortGlyphHorizontalMargin = 4;

		// Token: 0x04000D58 RID: 3416
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_sortGlyphWidth = 9;

		// Token: 0x04000D59 RID: 3417
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_sortGlyphHeight = 7;

		// Token: 0x04000D5A RID: 3418
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_horizontalTextMarginLeft = 2;

		// Token: 0x04000D5B RID: 3419
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_horizontalTextMarginRight = 2;

		// Token: 0x04000D5C RID: 3420
		private const byte DATAGRIDVIEWCOLUMNHEADERCELL_verticalMargin = 1;

		// Token: 0x04000D5D RID: 3421
		private static bool isScalingInitialized = false;

		// Token: 0x04000D5E RID: 3422
		private static byte sortGlyphSeparatorWidth = 2;

		// Token: 0x04000D5F RID: 3423
		private static byte sortGlyphHorizontalMargin = 4;

		// Token: 0x04000D60 RID: 3424
		private static byte sortGlyphWidth = 9;

		// Token: 0x04000D61 RID: 3425
		private static byte sortGlyphHeight = 7;

		// Token: 0x04000D62 RID: 3426
		private static Type cellType = typeof(DataGridViewColumnHeaderCell);

		// Token: 0x04000D63 RID: 3427
		private SortOrder sortGlyphDirection;

		// Token: 0x0200066C RID: 1644
		private class DataGridViewColumnHeaderCellRenderer
		{
			// Token: 0x0600663E RID: 26174 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewColumnHeaderCellRenderer()
			{
			}

			// Token: 0x17001632 RID: 5682
			// (get) Token: 0x0600663F RID: 26175 RVA: 0x0017E084 File Offset: 0x0017C284
			public static VisualStyleRenderer VisualStyleRenderer
			{
				get
				{
					if (DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewColumnHeaderCell.HeaderElement);
					}
					return DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x06006640 RID: 26176 RVA: 0x0017E0A4 File Offset: 0x0017C2A4
			public static void DrawHeader(Graphics g, Rectangle bounds, int headerState)
			{
				Rectangle rectangle = Rectangle.Truncate(g.ClipBounds);
				if (2 == headerState)
				{
					DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.SetParameters(DataGridViewColumnHeaderCell.HeaderElement);
					Rectangle clipRectangle = new Rectangle(bounds.Left, bounds.Bottom - 2, 2, 2);
					clipRectangle.Intersect(rectangle);
					DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.DrawBackground(g, bounds, clipRectangle);
					clipRectangle = new Rectangle(bounds.Right - 2, bounds.Bottom - 2, 2, 2);
					clipRectangle.Intersect(rectangle);
					DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.DrawBackground(g, bounds, clipRectangle);
				}
				DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.SetParameters(DataGridViewColumnHeaderCell.HeaderElement.ClassName, DataGridViewColumnHeaderCell.HeaderElement.Part, headerState);
				DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellRenderer.VisualStyleRenderer.DrawBackground(g, bounds, rectangle);
			}

			// Token: 0x04003A6A RID: 14954
			private static VisualStyleRenderer visualStyleRenderer;
		}

		// Token: 0x0200066D RID: 1645
		protected class DataGridViewColumnHeaderCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			// Token: 0x06006641 RID: 26177 RVA: 0x0017C895 File Offset: 0x0017AA95
			public DataGridViewColumnHeaderCellAccessibleObject(DataGridViewColumnHeaderCell owner) : base(owner)
			{
			}

			// Token: 0x17001633 RID: 5683
			// (get) Token: 0x06006642 RID: 26178 RVA: 0x0017E15A File Offset: 0x0017C35A
			public override Rectangle Bounds
			{
				get
				{
					if (base.IsOwnerCellDestroyed())
					{
						return Rectangle.Empty;
					}
					return base.GetAccessibleObjectBounds(this.ParentPrivate);
				}
			}

			// Token: 0x17001634 RID: 5684
			// (get) Token: 0x06006643 RID: 26179 RVA: 0x0017E178 File Offset: 0x0017C378
			public override string DefaultAction
			{
				get
				{
					if (base.IsOwnerCellDestroyed())
					{
						return string.Empty;
					}
					if (base.Owner.OwningColumn == null)
					{
						return string.Empty;
					}
					if (base.Owner.OwningColumn.SortMode == DataGridViewColumnSortMode.Automatic)
					{
						return SR.GetString("DataGridView_AccColumnHeaderCellDefaultAction");
					}
					if (base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
					{
						return SR.GetString("DataGridView_AccColumnHeaderCellSelectDefaultAction");
					}
					return string.Empty;
				}
			}

			// Token: 0x17001635 RID: 5685
			// (get) Token: 0x06006644 RID: 26180 RVA: 0x0017E1FA File Offset: 0x0017C3FA
			public override string Name
			{
				get
				{
					if (!base.IsOwnerCellDestroyed() && base.Owner.OwningColumn != null)
					{
						return base.Owner.OwningColumn.HeaderText;
					}
					return string.Empty;
				}
			}

			// Token: 0x17001636 RID: 5686
			// (get) Token: 0x06006645 RID: 26181 RVA: 0x0017E227 File Offset: 0x0017C427
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.ParentPrivate;
				}
			}

			// Token: 0x17001637 RID: 5687
			// (get) Token: 0x06006646 RID: 26182 RVA: 0x0017E22F File Offset: 0x0017C42F
			private AccessibleObject ParentPrivate
			{
				get
				{
					if (!base.IsOwnerCellDestroyed())
					{
						return base.Owner.DataGridView.AccessibilityObject.GetChild(0);
					}
					return null;
				}
			}

			// Token: 0x17001638 RID: 5688
			// (get) Token: 0x06006647 RID: 26183 RVA: 0x00177CA4 File Offset: 0x00175EA4
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ColumnHeader;
				}
			}

			// Token: 0x17001639 RID: 5689
			// (get) Token: 0x06006648 RID: 26184 RVA: 0x0017E254 File Offset: 0x0017C454
			public override AccessibleStates State
			{
				get
				{
					if (base.IsOwnerCellDestroyed())
					{
						return AccessibleStates.None;
					}
					AccessibleStates accessibleStates = AccessibleStates.Selectable;
					AccessibleStates state = base.State;
					if ((state & AccessibleStates.Offscreen) == AccessibleStates.Offscreen)
					{
						accessibleStates |= AccessibleStates.Offscreen;
					}
					if ((base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect) && base.Owner.OwningColumn != null && base.Owner.OwningColumn.Selected)
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			// Token: 0x1700163A RID: 5690
			// (get) Token: 0x06006649 RID: 26185 RVA: 0x000163B4 File Offset: 0x000145B4
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.Name;
				}
			}

			// Token: 0x1700163B RID: 5691
			// (get) Token: 0x0600664A RID: 26186 RVA: 0x0017E2D8 File Offset: 0x0017C4D8
			private int VisibleIndex
			{
				get
				{
					if (!base.IsOwnerCellDestroyed())
					{
						DataGridViewCell owner = base.Owner;
						if (((owner != null) ? owner.DataGridView : null) != null && base.Owner.OwningColumn != null)
						{
							if (!base.Owner.DataGridView.RowHeadersVisible)
							{
								return base.Owner.DataGridView.Columns.GetVisibleIndex(base.Owner.OwningColumn);
							}
							return base.Owner.DataGridView.Columns.GetVisibleIndex(base.Owner.OwningColumn) + 1;
						}
					}
					return -1;
				}
			}

			// Token: 0x0600664B RID: 26187 RVA: 0x0017E368 File Offset: 0x0017C568
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (base.IsOwnerCellDestroyed())
				{
					return;
				}
				DataGridViewColumnHeaderCell dataGridViewColumnHeaderCell = (DataGridViewColumnHeaderCell)base.Owner;
				DataGridView dataGridView = dataGridViewColumnHeaderCell.DataGridView;
				if (dataGridViewColumnHeaderCell.OwningColumn != null)
				{
					if (dataGridViewColumnHeaderCell.OwningColumn.SortMode == DataGridViewColumnSortMode.Automatic)
					{
						ListSortDirection direction = ListSortDirection.Ascending;
						if (dataGridView.SortedColumn == dataGridViewColumnHeaderCell.OwningColumn && dataGridView.SortOrder == SortOrder.Ascending)
						{
							direction = ListSortDirection.Descending;
						}
						dataGridView.Sort(dataGridViewColumnHeaderCell.OwningColumn, direction);
						return;
					}
					if (dataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || dataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
					{
						dataGridViewColumnHeaderCell.OwningColumn.Selected = true;
					}
				}
			}

			// Token: 0x0600664C RID: 26188 RVA: 0x0017E3F0 File Offset: 0x0017C5F0
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (base.IsOwnerCellDestroyed() || base.Owner.OwningColumn == null)
				{
					return null;
				}
				switch (navigationDirection)
				{
				case AccessibleNavigation.Left:
					if (base.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return this.NavigateBackward();
					}
					return this.NavigateForward();
				case AccessibleNavigation.Right:
					if (base.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return this.NavigateForward();
					}
					return this.NavigateBackward();
				case AccessibleNavigation.Next:
					return this.NavigateForward();
				case AccessibleNavigation.Previous:
					return this.NavigateBackward();
				case AccessibleNavigation.FirstChild:
					if (AccessibilityImprovements.Level5)
					{
						return null;
					}
					return base.Owner.DataGridView.AccessibilityObject.GetChild(0).GetChild(0);
				case AccessibleNavigation.LastChild:
				{
					if (AccessibilityImprovements.Level5)
					{
						return null;
					}
					AccessibleObject child = base.Owner.DataGridView.AccessibilityObject.GetChild(0);
					return child.GetChild(child.GetChildCount() - 1);
				}
				default:
					return null;
				}
			}

			// Token: 0x0600664D RID: 26189 RVA: 0x0017E4E0 File Offset: 0x0017C6E0
			private AccessibleObject NavigateBackward()
			{
				if (base.IsOwnerCellDestroyed())
				{
					return null;
				}
				if (base.Owner.OwningColumn == base.Owner.DataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Visible))
				{
					if (base.Owner.DataGridView.RowHeadersVisible)
					{
						return this.Parent.GetChild(0);
					}
					return null;
				}
				else if (AccessibilityImprovements.Level5)
				{
					AccessibleObject parent = this.Parent;
					if (parent == null)
					{
						return null;
					}
					return parent.GetChild(this.VisibleIndex - 1);
				}
				else
				{
					int index = base.Owner.DataGridView.Columns.GetPreviousColumn(base.Owner.OwningColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
					int num = base.Owner.DataGridView.Columns.ColumnIndexToActualDisplayIndex(index, DataGridViewElementStates.Visible);
					if (base.Owner.DataGridView.RowHeadersVisible)
					{
						return this.Parent.GetChild(num + 1);
					}
					return this.Parent.GetChild(num);
				}
			}

			// Token: 0x0600664E RID: 26190 RVA: 0x0017E5CC File Offset: 0x0017C7CC
			private AccessibleObject NavigateForward()
			{
				if (base.IsOwnerCellDestroyed() || base.Owner.OwningColumn == base.Owner.DataGridView.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None))
				{
					return null;
				}
				if (AccessibilityImprovements.Level5)
				{
					int visibleIndex = this.VisibleIndex;
					if (visibleIndex < 0)
					{
						return null;
					}
					AccessibleObject parent = this.Parent;
					if (parent == null)
					{
						return null;
					}
					return parent.GetChild(visibleIndex + 1);
				}
				else
				{
					int index = base.Owner.DataGridView.Columns.GetNextColumn(base.Owner.OwningColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
					int num = base.Owner.DataGridView.Columns.ColumnIndexToActualDisplayIndex(index, DataGridViewElementStates.Visible);
					if (base.Owner.DataGridView.RowHeadersVisible)
					{
						return this.Parent.GetChild(num + 1);
					}
					return this.Parent.GetChild(num);
				}
			}

			// Token: 0x0600664F RID: 26191 RVA: 0x0017E6A0 File Offset: 0x0017C8A0
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if (base.Owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				else
				{
					DataGridViewColumnHeaderCell dataGridViewColumnHeaderCell = (DataGridViewColumnHeaderCell)base.Owner;
					DataGridView dataGridView = dataGridViewColumnHeaderCell.DataGridView;
					if (dataGridView == null)
					{
						return;
					}
					if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
					{
						dataGridView.FocusInternal();
					}
					if (dataGridViewColumnHeaderCell.OwningColumn != null && (dataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || dataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect))
					{
						if ((flags & (AccessibleSelection.TakeSelection | AccessibleSelection.AddSelection)) != AccessibleSelection.None)
						{
							dataGridViewColumnHeaderCell.OwningColumn.Selected = true;
							return;
						}
						if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection)
						{
							dataGridViewColumnHeaderCell.OwningColumn.Selected = false;
						}
					}
					return;
				}
			}

			// Token: 0x06006650 RID: 26192 RVA: 0x0017E734 File Offset: 0x0017C934
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerCellDestroyed() || base.Owner.OwningColumn == null)
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this.Parent;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					return this.NavigateForward();
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return this.NavigateBackward();
				default:
					return null;
				}
			}

			// Token: 0x06006651 RID: 26193 RVA: 0x0017E782 File Offset: 0x0017C982
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerCellDestroyed() && (patternId.Equals(10018) || patternId.Equals(10000));
			}

			// Token: 0x06006652 RID: 26194 RVA: 0x0017E7AC File Offset: 0x0017C9AC
			internal override object GetPropertyValue(int propertyId)
			{
				if (AccessibilityImprovements.Level3)
				{
					switch (propertyId)
					{
					case 30003:
						return 50034;
					case 30004:
					case 30006:
					case 30011:
					case 30012:
						goto IL_D9;
					case 30005:
						return this.Name;
					case 30007:
						return string.Empty;
					case 30008:
						break;
					case 30009:
						return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
					case 30010:
						return !base.IsOwnerCellDestroyed() && base.Owner.DataGridView.Enabled;
					case 30013:
						return this.Help ?? string.Empty;
					default:
						if (propertyId != 30019)
						{
							if (propertyId != 30022)
							{
								goto IL_D9;
							}
							return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
						}
						break;
					}
					return false;
				}
				IL_D9:
				return base.GetPropertyValue(propertyId);
			}
		}
	}
}
