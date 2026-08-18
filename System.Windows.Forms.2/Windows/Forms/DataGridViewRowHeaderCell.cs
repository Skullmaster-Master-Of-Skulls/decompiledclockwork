using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000210 RID: 528
	public class DataGridViewRowHeaderCell : DataGridViewHeaderCell
	{
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000A51F1 File Offset: 0x000A33F1
		private static Bitmap LeftArrowBitmap
		{
			get
			{
				if (DataGridViewRowHeaderCell.leftArrowBmp == null)
				{
					DataGridViewRowHeaderCell.leftArrowBmp = DataGridViewRowHeaderCell.GetBitmapFromIcon("DataGridViewRow.left.ico");
				}
				return DataGridViewRowHeaderCell.leftArrowBmp;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x000A520E File Offset: 0x000A340E
		private static Bitmap LeftArrowStarBitmap
		{
			get
			{
				if (DataGridViewRowHeaderCell.leftArrowStarBmp == null)
				{
					DataGridViewRowHeaderCell.leftArrowStarBmp = DataGridViewRowHeaderCell.GetBitmapFromIcon("DataGridViewRow.leftstar.ico");
				}
				return DataGridViewRowHeaderCell.leftArrowStarBmp;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x000A522B File Offset: 0x000A342B
		private static Bitmap PencilLTRBitmap
		{
			get
			{
				if (DataGridViewRowHeaderCell.pencilLTRBmp == null)
				{
					DataGridViewRowHeaderCell.pencilLTRBmp = DataGridViewRowHeaderCell.GetBitmapFromIcon("DataGridViewRow.pencil_ltr.ico");
				}
				return DataGridViewRowHeaderCell.pencilLTRBmp;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x000A5248 File Offset: 0x000A3448
		private static Bitmap PencilRTLBitmap
		{
			get
			{
				if (DataGridViewRowHeaderCell.pencilRTLBmp == null)
				{
					DataGridViewRowHeaderCell.pencilRTLBmp = DataGridViewRowHeaderCell.GetBitmapFromIcon("DataGridViewRow.pencil_rtl.ico");
				}
				return DataGridViewRowHeaderCell.pencilRTLBmp;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x000A5265 File Offset: 0x000A3465
		private static Bitmap RightArrowBitmap
		{
			get
			{
				if (DataGridViewRowHeaderCell.rightArrowBmp == null)
				{
					DataGridViewRowHeaderCell.rightArrowBmp = DataGridViewRowHeaderCell.GetBitmapFromIcon("DataGridViewRow.right.ico");
				}
				return DataGridViewRowHeaderCell.rightArrowBmp;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x000A5282 File Offset: 0x000A3482
		private static Bitmap RightArrowStarBitmap
		{
			get
			{
				if (DataGridViewRowHeaderCell.rightArrowStarBmp == null)
				{
					DataGridViewRowHeaderCell.rightArrowStarBmp = DataGridViewRowHeaderCell.GetBitmapFromIcon("DataGridViewRow.rightstar.ico");
				}
				return DataGridViewRowHeaderCell.rightArrowStarBmp;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000A529F File Offset: 0x000A349F
		private static Bitmap StarBitmap
		{
			get
			{
				if (DataGridViewRowHeaderCell.starBmp == null)
				{
					DataGridViewRowHeaderCell.starBmp = DataGridViewRowHeaderCell.GetBitmapFromIcon("DataGridViewRow.star.ico");
				}
				return DataGridViewRowHeaderCell.starBmp;
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000A52BC File Offset: 0x000A34BC
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewRowHeaderCell dataGridViewRowHeaderCell;
			if (type == DataGridViewRowHeaderCell.cellType)
			{
				dataGridViewRowHeaderCell = new DataGridViewRowHeaderCell();
			}
			else
			{
				dataGridViewRowHeaderCell = (DataGridViewRowHeaderCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewRowHeaderCell);
			dataGridViewRowHeaderCell.Value = base.Value;
			return dataGridViewRowHeaderCell;
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000A5305 File Offset: 0x000A3505
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewRowHeaderCell.DataGridViewRowHeaderCellAccessibleObject(this);
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000A530D File Offset: 0x000A350D
		private static Bitmap GetArrowBitmap(bool rightToLeft)
		{
			if (!rightToLeft)
			{
				return DataGridViewRowHeaderCell.RightArrowBitmap;
			}
			return DataGridViewRowHeaderCell.LeftArrowBitmap;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000A531D File Offset: 0x000A351D
		private static Bitmap GetArrowStarBitmap(bool rightToLeft)
		{
			if (!rightToLeft)
			{
				return DataGridViewRowHeaderCell.RightArrowStarBitmap;
			}
			return DataGridViewRowHeaderCell.LeftArrowStarBitmap;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000A5330 File Offset: 0x000A3530
		private static Bitmap GetBitmapFromIcon(string iconName)
		{
			Size size = new Size((int)DataGridViewCell.iconsWidth, (int)DataGridViewCell.iconsHeight);
			Icon icon = new Icon(BitmapSelector.GetResourceStream(typeof(DataGridViewRowHeaderCell), iconName), size);
			Bitmap bitmap = icon.ToBitmap();
			icon.Dispose();
			if (DpiHelper.IsScalingRequired && (bitmap.Size.Width != (int)DataGridViewCell.iconsWidth || bitmap.Size.Height != (int)DataGridViewCell.iconsHeight))
			{
				Bitmap bitmap2 = DpiHelper.CreateResizedBitmap(bitmap, size);
				if (bitmap2 != null)
				{
					bitmap.Dispose();
					bitmap = bitmap2;
				}
			}
			return bitmap;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000A53BC File Offset: 0x000A35BC
		protected override object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			object value = this.GetValue(rowIndex);
			StringBuilder stringBuilder = new StringBuilder(64);
			if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
			{
				if (inFirstRow)
				{
					stringBuilder.Append("<TABLE>");
				}
				stringBuilder.Append("<TR>");
				stringBuilder.Append("<TD ALIGN=\"center\">");
				if (value != null)
				{
					stringBuilder.Append("<B>");
					DataGridViewCell.FormatPlainTextAsHtml(value.ToString(), new StringWriter(stringBuilder, CultureInfo.CurrentCulture));
					stringBuilder.Append("</B>");
				}
				else
				{
					stringBuilder.Append("&nbsp;");
				}
				stringBuilder.Append("</TD>");
				if (lastCell)
				{
					stringBuilder.Append("</TR>");
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

		// Token: 0x06002291 RID: 8849 RVA: 0x000A5548 File Offset: 0x000A3748
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || base.OwningRow == null)
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates dataGridViewElementState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out dataGridViewElementState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementState, value, null, cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, true, false, false);
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000A55A8 File Offset: 0x000A37A8
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || !base.DataGridView.ShowRowErrors || string.IsNullOrEmpty(this.GetErrorText(rowIndex)))
			{
				return Rectangle.Empty;
			}
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates dataGridViewElementState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out dataGridViewElementState, out rectangle);
			object value = this.GetValue(rowIndex);
			object formattedValue = this.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementState, formattedValue, this.GetErrorText(rowIndex), cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, false, true, false);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000A5632 File Offset: 0x000A3832
		protected internal override string GetErrorText(int rowIndex)
		{
			if (base.OwningRow == null)
			{
				return base.GetErrorText(rowIndex);
			}
			return base.OwningRow.GetErrorText(rowIndex);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000A5650 File Offset: 0x000A3850
		public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
		{
			if (base.DataGridView != null && (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count))
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			ContextMenuStrip contextMenuStrip = base.GetContextMenuStrip(rowIndex);
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

		// Token: 0x06002295 RID: 8853 RVA: 0x000A56AC File Offset: 0x000A38AC
		public override DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
		{
			DataGridViewCellStyle dataGridViewCellStyle = (inheritedCellStyle == null) ? new DataGridViewCellStyle() : inheritedCellStyle;
			DataGridViewCellStyle dataGridViewCellStyle2 = null;
			if (base.HasStyle)
			{
				dataGridViewCellStyle2 = base.Style;
			}
			DataGridViewCellStyle rowHeadersDefaultCellStyle = base.DataGridView.RowHeadersDefaultCellStyle;
			DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
			if (includeColors)
			{
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = dataGridViewCellStyle2.BackColor;
				}
				else if (!rowHeadersDefaultCellStyle.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = rowHeadersDefaultCellStyle.BackColor;
				}
				else
				{
					dataGridViewCellStyle.BackColor = defaultCellStyle.BackColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = dataGridViewCellStyle2.ForeColor;
				}
				else if (!rowHeadersDefaultCellStyle.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = rowHeadersDefaultCellStyle.ForeColor;
				}
				else
				{
					dataGridViewCellStyle.ForeColor = defaultCellStyle.ForeColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = dataGridViewCellStyle2.SelectionBackColor;
				}
				else if (!rowHeadersDefaultCellStyle.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = rowHeadersDefaultCellStyle.SelectionBackColor;
				}
				else
				{
					dataGridViewCellStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = dataGridViewCellStyle2.SelectionForeColor;
				}
				else if (!rowHeadersDefaultCellStyle.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = rowHeadersDefaultCellStyle.SelectionForeColor;
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
			else if (rowHeadersDefaultCellStyle.Font != null)
			{
				dataGridViewCellStyle.Font = rowHeadersDefaultCellStyle.Font;
			}
			else
			{
				dataGridViewCellStyle.Font = defaultCellStyle.Font;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = dataGridViewCellStyle2.NullValue;
			}
			else if (!rowHeadersDefaultCellStyle.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = rowHeadersDefaultCellStyle.NullValue;
			}
			else
			{
				dataGridViewCellStyle.NullValue = defaultCellStyle.NullValue;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = dataGridViewCellStyle2.DataSourceNullValue;
			}
			else if (!rowHeadersDefaultCellStyle.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = rowHeadersDefaultCellStyle.DataSourceNullValue;
			}
			else
			{
				dataGridViewCellStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = dataGridViewCellStyle2.Format;
			}
			else if (rowHeadersDefaultCellStyle.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = rowHeadersDefaultCellStyle.Format;
			}
			else
			{
				dataGridViewCellStyle.Format = defaultCellStyle.Format;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = dataGridViewCellStyle2.FormatProvider;
			}
			else if (!rowHeadersDefaultCellStyle.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = rowHeadersDefaultCellStyle.FormatProvider;
			}
			else
			{
				dataGridViewCellStyle.FormatProvider = defaultCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = dataGridViewCellStyle2.Alignment;
			}
			else if (rowHeadersDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = rowHeadersDefaultCellStyle.Alignment;
			}
			else
			{
				dataGridViewCellStyle.AlignmentInternal = defaultCellStyle.Alignment;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = dataGridViewCellStyle2.WrapMode;
			}
			else if (rowHeadersDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = rowHeadersDefaultCellStyle.WrapMode;
			}
			else
			{
				dataGridViewCellStyle.WrapModeInternal = defaultCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Tag != null)
			{
				dataGridViewCellStyle.Tag = dataGridViewCellStyle2.Tag;
			}
			else if (rowHeadersDefaultCellStyle.Tag != null)
			{
				dataGridViewCellStyle.Tag = rowHeadersDefaultCellStyle.Tag;
			}
			else
			{
				dataGridViewCellStyle.Tag = defaultCellStyle.Tag;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = dataGridViewCellStyle2.Padding;
			}
			else if (rowHeadersDefaultCellStyle.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = rowHeadersDefaultCellStyle.Padding;
			}
			else
			{
				dataGridViewCellStyle.PaddingInternal = defaultCellStyle.Padding;
			}
			return dataGridViewCellStyle;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000A5A5A File Offset: 0x000A3C5A
		private static Bitmap GetPencilBitmap(bool rightToLeft)
		{
			if (!rightToLeft)
			{
				return DataGridViewRowHeaderCell.PencilLTRBitmap;
			}
			return DataGridViewRowHeaderCell.PencilRTLBitmap;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000A5A6C File Offset: 0x000A3C6C
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
			DataGridViewAdvancedBorderStyle advancedBorderStyle = base.OwningRow.AdjustRowHeaderBorderStyle(base.DataGridView.AdvancedRowHeadersBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, false, false, false, false);
			Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
			int num = rectangle.Left + rectangle.Width + cellStyle.Padding.Horizontal;
			int num2 = rectangle.Top + rectangle.Height + cellStyle.Padding.Vertical;
			TextFormatFlags flags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			if (base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				Rectangle themeMargins = DataGridViewHeaderCell.GetThemeMargins(graphics);
				num += themeMargins.Y;
				num += themeMargins.Height;
				num2 += themeMargins.X;
				num2 += themeMargins.Width;
			}
			object obj = this.GetValue(rowIndex);
			if (!(obj is string))
			{
				obj = null;
			}
			return DataGridViewUtilities.GetPreferredRowHeaderSize(graphics, (string)obj, cellStyle, num, num2, base.DataGridView.ShowRowErrors, true, constraintSize, flags);
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000A5B97 File Offset: 0x000A3D97
		protected override object GetValue(int rowIndex)
		{
			if (base.DataGridView != null && (rowIndex < -1 || rowIndex >= base.DataGridView.Rows.Count))
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			return base.Properties.GetObject(DataGridViewCell.PropCellValue);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000A5BD4 File Offset: 0x000A3DD4
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000A5C0C File Offset: 0x000A3E0C
		private Rectangle PaintPrivate(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			Rectangle result = Rectangle.Empty;
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle = cellBounds;
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			rectangle.Offset(rectangle2.X, rectangle2.Y);
			rectangle.Width -= rectangle2.Right;
			rectangle.Height -= rectangle2.Bottom;
			Rectangle destRect = rectangle;
			bool flag = (dataGridViewElementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			if (base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
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
				if (destRect.Width > 0 && destRect.Height > 0)
				{
					if (paint && DataGridViewCell.PaintBackground(paintParts))
					{
						int headerState = 1;
						if (base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect)
						{
							if (base.ButtonState != ButtonState.Normal)
							{
								headerState = 3;
							}
							else if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == -1)
							{
								headerState = 2;
							}
							else if (flag)
							{
								headerState = 3;
							}
						}
						using (Bitmap bitmap = new Bitmap(destRect.Height, destRect.Width))
						{
							using (Graphics graphics2 = Graphics.FromImage(bitmap))
							{
								DataGridViewRowHeaderCell.DataGridViewRowHeaderCellRenderer.DrawHeader(graphics2, new Rectangle(0, 0, destRect.Height, destRect.Width), headerState);
								bitmap.RotateFlip(base.DataGridView.RightToLeftInternal ? RotateFlipType.Rotate90FlipNone : RotateFlipType.Rotate90FlipX);
								graphics.DrawImage(bitmap, destRect, new Rectangle(0, 0, destRect.Width, destRect.Height), GraphicsUnit.Pixel);
							}
						}
					}
					Rectangle themeMargins = DataGridViewHeaderCell.GetThemeMargins(graphics);
					if (base.DataGridView.RightToLeftInternal)
					{
						rectangle.X += themeMargins.Height;
					}
					else
					{
						rectangle.X += themeMargins.Y;
					}
					rectangle.Width -= themeMargins.Y + themeMargins.Height;
					rectangle.Height -= themeMargins.X + themeMargins.Width;
					rectangle.Y += themeMargins.X;
				}
			}
			else
			{
				if (rectangle.Width > 0 && rectangle.Height > 0)
				{
					SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
					if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
					{
						graphics.FillRectangle(cachedBrush, rectangle);
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
			Bitmap bitmap2 = null;
			if (rectangle.Width > 0 && rectangle.Height > 0)
			{
				Rectangle cellValueBounds = rectangle;
				string text = formattedValue as string;
				if (!string.IsNullOrEmpty(text))
				{
					if (rectangle.Width >= (int)(DataGridViewCell.iconsWidth + 6) && rectangle.Height >= (int)(DataGridViewCell.iconsHeight + 4))
					{
						if (paint && DataGridViewCell.PaintContentBackground(paintParts))
						{
							if (base.DataGridView.CurrentCellAddress.Y == rowIndex)
							{
								if (base.DataGridView.VirtualMode)
								{
									if (base.DataGridView.IsCurrentRowDirty && base.DataGridView.ShowEditingIcon)
									{
										bitmap2 = DataGridViewRowHeaderCell.GetPencilBitmap(base.DataGridView.RightToLeftInternal);
									}
									else if (base.DataGridView.NewRowIndex == rowIndex)
									{
										bitmap2 = DataGridViewRowHeaderCell.GetArrowStarBitmap(base.DataGridView.RightToLeftInternal);
									}
									else
									{
										bitmap2 = DataGridViewRowHeaderCell.GetArrowBitmap(base.DataGridView.RightToLeftInternal);
									}
								}
								else if (base.DataGridView.IsCurrentCellDirty && base.DataGridView.ShowEditingIcon)
								{
									bitmap2 = DataGridViewRowHeaderCell.GetPencilBitmap(base.DataGridView.RightToLeftInternal);
								}
								else if (base.DataGridView.NewRowIndex == rowIndex)
								{
									bitmap2 = DataGridViewRowHeaderCell.GetArrowStarBitmap(base.DataGridView.RightToLeftInternal);
								}
								else
								{
									bitmap2 = DataGridViewRowHeaderCell.GetArrowBitmap(base.DataGridView.RightToLeftInternal);
								}
							}
							else if (base.DataGridView.NewRowIndex == rowIndex)
							{
								bitmap2 = DataGridViewRowHeaderCell.StarBitmap;
							}
							if (bitmap2 != null)
							{
								Color foreColor;
								if (base.DataGridView.ApplyVisualStylesToHeaderCells)
								{
									foreColor = DataGridViewRowHeaderCell.DataGridViewRowHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
								}
								else
								{
									foreColor = (flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
								}
								Bitmap obj = bitmap2;
								lock (obj)
								{
									this.PaintIcon(graphics, bitmap2, rectangle, foreColor);
								}
							}
						}
						if (!base.DataGridView.RightToLeftInternal)
						{
							rectangle.X += (int)(DataGridViewCell.iconsWidth + 6);
						}
						rectangle.Width -= (int)(DataGridViewCell.iconsWidth + 6);
					}
					rectangle.Offset(4, 1);
					rectangle.Width -= 9;
					rectangle.Height -= 2;
					if (rectangle.Width > 0 && rectangle.Height > 0)
					{
						TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
						if (base.DataGridView.ShowRowErrors && rectangle.Width > (int)(DataGridViewCell.iconsWidth + 6))
						{
							Size maxBounds = new Size(rectangle.Width - (int)DataGridViewCell.iconsWidth - 6, rectangle.Height);
							if (DataGridViewCell.TextFitsInBounds(graphics, text, cellStyle.Font, maxBounds, textFormatFlags))
							{
								if (base.DataGridView.RightToLeftInternal)
								{
									rectangle.X += (int)(DataGridViewCell.iconsWidth + 6);
								}
								rectangle.Width -= (int)(DataGridViewCell.iconsWidth + 6);
							}
						}
						if (DataGridViewCell.PaintContentForeground(paintParts))
						{
							if (paint)
							{
								Color foreColor2;
								if (base.DataGridView.ApplyVisualStylesToHeaderCells)
								{
									foreColor2 = DataGridViewRowHeaderCell.DataGridViewRowHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
								}
								else
								{
									foreColor2 = (flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
								}
								if ((textFormatFlags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
								{
									textFormatFlags |= TextFormatFlags.EndEllipsis;
								}
								TextRenderer.DrawText(graphics, text, cellStyle.Font, rectangle, foreColor2, textFormatFlags);
							}
							else if (computeContentBounds)
							{
								result = DataGridViewUtilities.GetTextBounds(rectangle, text, textFormatFlags, cellStyle);
							}
						}
					}
					if (cellValueBounds.Width >= (int)(9 + 2 * DataGridViewCell.iconsWidth))
					{
						if (paint && base.DataGridView.ShowRowErrors && DataGridViewCell.PaintErrorIcon(paintParts))
						{
							this.PaintErrorIcon(graphics, clipBounds, cellValueBounds, errorText);
						}
						else if (computeErrorIconBounds && !string.IsNullOrEmpty(errorText))
						{
							result = base.ComputeErrorIconBounds(cellValueBounds);
						}
					}
				}
				else
				{
					if (rectangle.Width >= (int)(DataGridViewCell.iconsWidth + 6) && rectangle.Height >= (int)(DataGridViewCell.iconsHeight + 4) && paint && DataGridViewCell.PaintContentBackground(paintParts))
					{
						if (base.DataGridView.CurrentCellAddress.Y == rowIndex)
						{
							if (base.DataGridView.VirtualMode)
							{
								if (base.DataGridView.IsCurrentRowDirty && base.DataGridView.ShowEditingIcon)
								{
									bitmap2 = DataGridViewRowHeaderCell.GetPencilBitmap(base.DataGridView.RightToLeftInternal);
								}
								else if (base.DataGridView.NewRowIndex == rowIndex)
								{
									bitmap2 = DataGridViewRowHeaderCell.GetArrowStarBitmap(base.DataGridView.RightToLeftInternal);
								}
								else
								{
									bitmap2 = DataGridViewRowHeaderCell.GetArrowBitmap(base.DataGridView.RightToLeftInternal);
								}
							}
							else if (base.DataGridView.IsCurrentCellDirty && base.DataGridView.ShowEditingIcon)
							{
								bitmap2 = DataGridViewRowHeaderCell.GetPencilBitmap(base.DataGridView.RightToLeftInternal);
							}
							else if (base.DataGridView.NewRowIndex == rowIndex)
							{
								bitmap2 = DataGridViewRowHeaderCell.GetArrowStarBitmap(base.DataGridView.RightToLeftInternal);
							}
							else
							{
								bitmap2 = DataGridViewRowHeaderCell.GetArrowBitmap(base.DataGridView.RightToLeftInternal);
							}
						}
						else if (base.DataGridView.NewRowIndex == rowIndex)
						{
							bitmap2 = DataGridViewRowHeaderCell.StarBitmap;
						}
						if (bitmap2 != null)
						{
							Bitmap obj2 = bitmap2;
							lock (obj2)
							{
								Color foreColor3;
								if (base.DataGridView.ApplyVisualStylesToHeaderCells)
								{
									foreColor3 = DataGridViewRowHeaderCell.DataGridViewRowHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
								}
								else
								{
									foreColor3 = (flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
								}
								this.PaintIcon(graphics, bitmap2, rectangle, foreColor3);
							}
						}
					}
					if (cellValueBounds.Width >= (int)(9 + 2 * DataGridViewCell.iconsWidth))
					{
						if (paint && base.DataGridView.ShowRowErrors && DataGridViewCell.PaintErrorIcon(paintParts))
						{
							base.PaintErrorIcon(graphics, cellStyle, rowIndex, cellBounds, cellValueBounds, errorText);
						}
						else if (computeErrorIconBounds && !string.IsNullOrEmpty(errorText))
						{
							result = base.ComputeErrorIconBounds(cellValueBounds);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000A6630 File Offset: 0x000A4830
		private void PaintIcon(Graphics g, Bitmap bmp, Rectangle bounds, Color foreColor)
		{
			Rectangle destRect = new Rectangle(base.DataGridView.RightToLeftInternal ? (bounds.Right - 3 - (int)DataGridViewCell.iconsWidth) : (bounds.Left + 3), bounds.Y + (bounds.Height - (int)DataGridViewCell.iconsHeight) / 2, (int)DataGridViewCell.iconsWidth, (int)DataGridViewCell.iconsHeight);
			DataGridViewRowHeaderCell.colorMap[0].NewColor = foreColor;
			DataGridViewRowHeaderCell.colorMap[0].OldColor = Color.Black;
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetRemapTable(DataGridViewRowHeaderCell.colorMap, ColorAdjustType.Bitmap);
			g.DrawImage(bmp, destRect, 0, 0, (int)DataGridViewCell.iconsWidth, (int)DataGridViewCell.iconsHeight, GraphicsUnit.Pixel, imageAttributes);
			imageAttributes.Dispose();
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000A66DC File Offset: 0x000A48DC
		protected override bool SetValue(int rowIndex, object value)
		{
			object value2 = this.GetValue(rowIndex);
			if (value != null || base.Properties.ContainsObject(DataGridViewCell.PropCellValue))
			{
				base.Properties.SetObject(DataGridViewCell.PropCellValue, value);
			}
			if (base.DataGridView != null && value2 != value)
			{
				base.RaiseCellValueChanged(new DataGridViewCellEventArgs(-1, rowIndex));
			}
			return true;
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000A6734 File Offset: 0x000A4934
		public override string ToString()
		{
			return "DataGridViewRowHeaderCell { RowIndex=" + base.RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
		}

		// Token: 0x04000E3B RID: 3643
		private static readonly VisualStyleElement HeaderElement = VisualStyleElement.Header.Item.Normal;

		// Token: 0x04000E3C RID: 3644
		private static ColorMap[] colorMap = new ColorMap[]
		{
			new ColorMap()
		};

		// Token: 0x04000E3D RID: 3645
		private static Bitmap rightArrowBmp = null;

		// Token: 0x04000E3E RID: 3646
		private static Bitmap leftArrowBmp = null;

		// Token: 0x04000E3F RID: 3647
		private static Bitmap rightArrowStarBmp;

		// Token: 0x04000E40 RID: 3648
		private static Bitmap leftArrowStarBmp;

		// Token: 0x04000E41 RID: 3649
		private static Bitmap pencilLTRBmp = null;

		// Token: 0x04000E42 RID: 3650
		private static Bitmap pencilRTLBmp = null;

		// Token: 0x04000E43 RID: 3651
		private static Bitmap starBmp = null;

		// Token: 0x04000E44 RID: 3652
		private static Type cellType = typeof(DataGridViewRowHeaderCell);

		// Token: 0x04000E45 RID: 3653
		private const byte DATAGRIDVIEWROWHEADERCELL_iconMarginWidth = 3;

		// Token: 0x04000E46 RID: 3654
		private const byte DATAGRIDVIEWROWHEADERCELL_iconMarginHeight = 2;

		// Token: 0x04000E47 RID: 3655
		private const byte DATAGRIDVIEWROWHEADERCELL_contentMarginWidth = 3;

		// Token: 0x04000E48 RID: 3656
		private const byte DATAGRIDVIEWROWHEADERCELL_horizontalTextMarginLeft = 1;

		// Token: 0x04000E49 RID: 3657
		private const byte DATAGRIDVIEWROWHEADERCELL_horizontalTextMarginRight = 2;

		// Token: 0x04000E4A RID: 3658
		private const byte DATAGRIDVIEWROWHEADERCELL_verticalTextMargin = 1;

		// Token: 0x0200067A RID: 1658
		private class DataGridViewRowHeaderCellRenderer
		{
			// Token: 0x060066C1 RID: 26305 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewRowHeaderCellRenderer()
			{
			}

			// Token: 0x1700165D RID: 5725
			// (get) Token: 0x060066C2 RID: 26306 RVA: 0x0018041E File Offset: 0x0017E61E
			public static VisualStyleRenderer VisualStyleRenderer
			{
				get
				{
					if (DataGridViewRowHeaderCell.DataGridViewRowHeaderCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewRowHeaderCell.DataGridViewRowHeaderCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewRowHeaderCell.HeaderElement);
					}
					return DataGridViewRowHeaderCell.DataGridViewRowHeaderCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x060066C3 RID: 26307 RVA: 0x0018043B File Offset: 0x0017E63B
			public static void DrawHeader(Graphics g, Rectangle bounds, int headerState)
			{
				DataGridViewRowHeaderCell.DataGridViewRowHeaderCellRenderer.VisualStyleRenderer.SetParameters(DataGridViewRowHeaderCell.HeaderElement.ClassName, DataGridViewRowHeaderCell.HeaderElement.Part, headerState);
				DataGridViewRowHeaderCell.DataGridViewRowHeaderCellRenderer.VisualStyleRenderer.DrawBackground(g, bounds, Rectangle.Truncate(g.ClipBounds));
			}

			// Token: 0x04003A84 RID: 14980
			private static VisualStyleRenderer visualStyleRenderer;
		}

		// Token: 0x0200067B RID: 1659
		protected class DataGridViewRowHeaderCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			// Token: 0x060066C4 RID: 26308 RVA: 0x0017C895 File Offset: 0x0017AA95
			public DataGridViewRowHeaderCellAccessibleObject(DataGridViewRowHeaderCell owner) : base(owner)
			{
			}

			// Token: 0x1700165E RID: 5726
			// (get) Token: 0x060066C5 RID: 26309 RVA: 0x00180474 File Offset: 0x0017E674
			public override Rectangle Bounds
			{
				get
				{
					if (base.IsOwnerCellDestroyed() || base.Owner.OwningRow == null)
					{
						return Rectangle.Empty;
					}
					Rectangle bounds = this.ParentPrivate.Bounds;
					bounds.Width = base.Owner.DataGridView.RowHeadersWidth;
					return bounds;
				}
			}

			// Token: 0x1700165F RID: 5727
			// (get) Token: 0x060066C6 RID: 26310 RVA: 0x001804C0 File Offset: 0x0017E6C0
			public override string DefaultAction
			{
				get
				{
					if ((!base.IsOwnerCellDestroyed() && base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect) || base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect)
					{
						return SR.GetString("DataGridView_RowHeaderCellAccDefaultAction");
					}
					return string.Empty;
				}
			}

			// Token: 0x17001660 RID: 5728
			// (get) Token: 0x060066C7 RID: 26311 RVA: 0x00180500 File Offset: 0x0017E700
			public override string Name
			{
				get
				{
					if (this.ParentPrivate != null)
					{
						return this.ParentPrivate.Name;
					}
					return string.Empty;
				}
			}

			// Token: 0x17001661 RID: 5729
			// (get) Token: 0x060066C8 RID: 26312 RVA: 0x0018051B File Offset: 0x0017E71B
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.ParentPrivate;
				}
			}

			// Token: 0x17001662 RID: 5730
			// (get) Token: 0x060066C9 RID: 26313 RVA: 0x00180523 File Offset: 0x0017E723
			private AccessibleObject ParentPrivate
			{
				get
				{
					if (base.IsOwnerCellDestroyed() || base.Owner.OwningRow == null)
					{
						return null;
					}
					return base.Owner.OwningRow.AccessibilityObject;
				}
			}

			// Token: 0x17001663 RID: 5731
			// (get) Token: 0x060066CA RID: 26314 RVA: 0x0018054C File Offset: 0x0017E74C
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.RowHeader;
				}
			}

			// Token: 0x17001664 RID: 5732
			// (get) Token: 0x060066CB RID: 26315 RVA: 0x00180550 File Offset: 0x0017E750
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
					if ((base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect || base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect) && base.Owner.OwningRow != null && base.Owner.OwningRow.Selected)
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			// Token: 0x17001665 RID: 5733
			// (get) Token: 0x060066CC RID: 26316 RVA: 0x0017F055 File Offset: 0x0017D255
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x060066CD RID: 26317 RVA: 0x001805D4 File Offset: 0x0017E7D4
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (!base.IsOwnerCellDestroyed() && (base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect || base.Owner.DataGridView.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect) && base.Owner.OwningRow != null)
				{
					base.Owner.OwningRow.Selected = true;
				}
			}

			// Token: 0x060066CE RID: 26318 RVA: 0x00180630 File Offset: 0x0017E830
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (base.IsOwnerCellDestroyed())
				{
					return null;
				}
				switch (navigationDirection)
				{
				case AccessibleNavigation.Up:
					if (base.Owner.OwningRow == null)
					{
						return null;
					}
					if (base.Owner.OwningRow.Index == base.Owner.DataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible))
					{
						if (base.Owner.DataGridView.ColumnHeadersVisible)
						{
							return base.Owner.DataGridView.AccessibilityObject.GetChild(0).GetChild(0);
						}
						return null;
					}
					else
					{
						int previousRow = base.Owner.DataGridView.Rows.GetPreviousRow(base.Owner.OwningRow.Index, DataGridViewElementStates.Visible);
						int rowCount = base.Owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, previousRow);
						if (base.Owner.DataGridView.ColumnHeadersVisible)
						{
							return base.Owner.DataGridView.AccessibilityObject.GetChild(rowCount + 1).GetChild(0);
						}
						return base.Owner.DataGridView.AccessibilityObject.GetChild(rowCount).GetChild(0);
					}
					break;
				case AccessibleNavigation.Down:
				{
					if (base.Owner.OwningRow == null)
					{
						return null;
					}
					if (base.Owner.OwningRow.Index == base.Owner.DataGridView.Rows.GetLastRow(DataGridViewElementStates.Visible))
					{
						return null;
					}
					int nextRow = base.Owner.DataGridView.Rows.GetNextRow(base.Owner.OwningRow.Index, DataGridViewElementStates.Visible);
					int rowCount2 = base.Owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, nextRow);
					if (base.Owner.DataGridView.ColumnHeadersVisible)
					{
						return base.Owner.DataGridView.AccessibilityObject.GetChild(1 + rowCount2).GetChild(0);
					}
					return base.Owner.DataGridView.AccessibilityObject.GetChild(rowCount2).GetChild(0);
				}
				case AccessibleNavigation.Next:
					if (base.Owner.OwningRow != null && base.Owner.DataGridView.Columns.GetColumnCount(DataGridViewElementStates.Visible) > 0)
					{
						return this.ParentPrivate.GetChild(1);
					}
					return null;
				case AccessibleNavigation.Previous:
					return null;
				}
				return null;
			}

			// Token: 0x060066CF RID: 26319 RVA: 0x00180868 File Offset: 0x0017EA68
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
					DataGridViewRowHeaderCell dataGridViewRowHeaderCell = (DataGridViewRowHeaderCell)base.Owner;
					DataGridView dataGridView = dataGridViewRowHeaderCell.DataGridView;
					if (dataGridView == null)
					{
						return;
					}
					if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
					{
						dataGridView.FocusInternal();
					}
					if (dataGridViewRowHeaderCell.OwningRow != null && (dataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect || dataGridView.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect))
					{
						if ((flags & (AccessibleSelection.TakeSelection | AccessibleSelection.AddSelection)) != AccessibleSelection.None)
						{
							dataGridViewRowHeaderCell.OwningRow.Selected = true;
							return;
						}
						if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection)
						{
							dataGridViewRowHeaderCell.OwningRow.Selected = false;
						}
					}
					return;
				}
			}

			// Token: 0x060066D0 RID: 26320 RVA: 0x001808FC File Offset: 0x0017EAFC
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerCellDestroyed() || base.Owner.OwningRow == null)
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return base.Owner.OwningRow.AccessibilityObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					if (base.Owner.DataGridView.Columns.GetColumnCount(DataGridViewElementStates.Visible) > 0)
					{
						return base.Owner.OwningRow.AccessibilityObject.GetChild(1);
					}
					return null;
				}
				return null;
			}

			// Token: 0x060066D1 RID: 26321 RVA: 0x0018097C File Offset: 0x0017EB7C
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
