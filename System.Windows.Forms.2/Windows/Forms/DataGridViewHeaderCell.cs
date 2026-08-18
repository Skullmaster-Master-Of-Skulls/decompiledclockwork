using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x020001FC RID: 508
	public class DataGridViewHeaderCell : DataGridViewCell
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x0009BB8C File Offset: 0x00099D8C
		protected ButtonState ButtonState
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewHeaderCell.PropButtonState, out flag);
				if (flag)
				{
					return (ButtonState)integer;
				}
				return ButtonState.Normal;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x0009BBB2 File Offset: 0x00099DB2
		private ButtonState ButtonStatePrivate
		{
			set
			{
				if (this.ButtonState != value)
				{
					base.Properties.SetInteger(DataGridViewHeaderCell.PropButtonState, (int)value);
				}
			}
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0009BBCE File Offset: 0x00099DCE
		protected override void Dispose(bool disposing)
		{
			if (this.FlipXPThemesBitmap != null && disposing)
			{
				this.FlipXPThemesBitmap.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x0009BBF0 File Offset: 0x00099DF0
		[Browsable(false)]
		public override bool Displayed
		{
			get
			{
				if (base.DataGridView == null || !base.DataGridView.Visible)
				{
					return false;
				}
				if (base.OwningRow != null)
				{
					return base.DataGridView.RowHeadersVisible && base.OwningRow.Displayed;
				}
				if (base.OwningColumn != null)
				{
					return base.DataGridView.ColumnHeadersVisible && base.OwningColumn.Displayed;
				}
				return base.DataGridView.LayoutInfo.TopLeftHeader != Rectangle.Empty;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x0009BC74 File Offset: 0x00099E74
		// (set) Token: 0x0600211F RID: 8479 RVA: 0x0009BC8B File Offset: 0x00099E8B
		internal Bitmap FlipXPThemesBitmap
		{
			get
			{
				return (Bitmap)base.Properties.GetObject(DataGridViewHeaderCell.PropFlipXPThemesBitmap);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewHeaderCell.PropFlipXPThemesBitmap))
				{
					base.Properties.SetObject(DataGridViewHeaderCell.PropFlipXPThemesBitmap, value);
				}
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x0009BCB3 File Offset: 0x00099EB3
		public override Type FormattedValueType
		{
			get
			{
				return DataGridViewHeaderCell.defaultFormattedValueType;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x0009BCBA File Offset: 0x00099EBA
		[Browsable(false)]
		public override bool Frozen
		{
			get
			{
				if (base.OwningRow != null)
				{
					return base.OwningRow.Frozen;
				}
				if (base.OwningColumn != null)
				{
					return base.OwningColumn.Frozen;
				}
				return base.DataGridView != null;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x0009BCEF File Offset: 0x00099EEF
		internal override bool HasValueType
		{
			get
			{
				return base.Properties.ContainsObject(DataGridViewHeaderCell.PropValueType) && base.Properties.GetObject(DataGridViewHeaderCell.PropValueType) != null;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x00013062 File Offset: 0x00011262
		// (set) Token: 0x06002124 RID: 8484 RVA: 0x0009BD18 File Offset: 0x00099F18
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool ReadOnly
		{
			get
			{
				return true;
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_HeaderCellReadOnlyProperty", new object[]
				{
					"ReadOnly"
				}));
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x0009BD38 File Offset: 0x00099F38
		[Browsable(false)]
		public override bool Resizable
		{
			get
			{
				if (base.OwningRow != null)
				{
					return base.OwningRow.Resizable == DataGridViewTriState.True || (base.DataGridView != null && base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing);
				}
				if (base.OwningColumn != null)
				{
					return base.OwningColumn.Resizable == DataGridViewTriState.True || (base.DataGridView != null && base.DataGridView.ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.EnableResizing);
				}
				return base.DataGridView != null && (base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing || base.DataGridView.ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.EnableResizing);
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x06002127 RID: 8487 RVA: 0x0009BDCE File Offset: 0x00099FCE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Selected
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_HeaderCellReadOnlyProperty", new object[]
				{
					"Selected"
				}));
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x0009BDF0 File Offset: 0x00099FF0
		// (set) Token: 0x06002129 RID: 8489 RVA: 0x0009BE23 File Offset: 0x0009A023
		public override Type ValueType
		{
			get
			{
				Type type = (Type)base.Properties.GetObject(DataGridViewHeaderCell.PropValueType);
				if (type != null)
				{
					return type;
				}
				return DataGridViewHeaderCell.defaultValueType;
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewHeaderCell.PropValueType))
				{
					base.Properties.SetObject(DataGridViewHeaderCell.PropValueType, value);
				}
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x0009BE54 File Offset: 0x0009A054
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				if (base.OwningRow != null)
				{
					return base.OwningRow.Visible && (base.DataGridView == null || base.DataGridView.RowHeadersVisible);
				}
				if (base.OwningColumn != null)
				{
					return base.OwningColumn.Visible && (base.DataGridView == null || base.DataGridView.ColumnHeadersVisible);
				}
				return base.DataGridView != null && base.DataGridView.RowHeadersVisible && base.DataGridView.ColumnHeadersVisible;
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0009BEE0 File Offset: 0x0009A0E0
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewHeaderCell dataGridViewHeaderCell;
			if (type == DataGridViewHeaderCell.cellType)
			{
				dataGridViewHeaderCell = new DataGridViewHeaderCell();
			}
			else
			{
				dataGridViewHeaderCell = (DataGridViewHeaderCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewHeaderCell);
			dataGridViewHeaderCell.Value = base.Value;
			return dataGridViewHeaderCell;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0009BF2C File Offset: 0x0009A12C
		public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
		{
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

		// Token: 0x0600212D RID: 8493 RVA: 0x0009BF5C File Offset: 0x0009A15C
		public override DataGridViewElementStates GetInheritedState(int rowIndex)
		{
			DataGridViewElementStates dataGridViewElementStates = DataGridViewElementStates.ReadOnly | DataGridViewElementStates.ResizableSet;
			if (base.OwningRow != null)
			{
				if ((base.DataGridView == null && rowIndex != -1) || (base.DataGridView != null && (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)))
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"rowIndex",
						rowIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (base.DataGridView != null && base.DataGridView.Rows.SharedRow(rowIndex) != base.OwningRow)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"rowIndex",
						rowIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				dataGridViewElementStates |= (base.OwningRow.GetState(rowIndex) & DataGridViewElementStates.Frozen);
				if (base.OwningRow.GetResizable(rowIndex) == DataGridViewTriState.True || (base.DataGridView != null && base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (base.OwningRow.GetVisible(rowIndex) && (base.DataGridView == null || base.DataGridView.RowHeadersVisible))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
					if (base.OwningRow.GetDisplayed(rowIndex))
					{
						dataGridViewElementStates |= DataGridViewElementStates.Displayed;
					}
				}
			}
			else if (base.OwningColumn != null)
			{
				if (rowIndex != -1)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				dataGridViewElementStates |= (base.OwningColumn.State & DataGridViewElementStates.Frozen);
				if (base.OwningColumn.Resizable == DataGridViewTriState.True || (base.DataGridView != null && base.DataGridView.ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.EnableResizing))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (base.OwningColumn.Visible && (base.DataGridView == null || base.DataGridView.ColumnHeadersVisible))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
					if (base.OwningColumn.Displayed)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Displayed;
					}
				}
			}
			else if (base.DataGridView != null)
			{
				if (rowIndex != -1)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				dataGridViewElementStates |= DataGridViewElementStates.Frozen;
				if (base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing || base.DataGridView.ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.EnableResizing)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (base.DataGridView.RowHeadersVisible && base.DataGridView.ColumnHeadersVisible)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
					if (base.DataGridView.LayoutInfo.TopLeftHeader != Rectangle.Empty)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Displayed;
					}
				}
			}
			return dataGridViewElementStates;
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0009C1A8 File Offset: 0x0009A3A8
		protected override Size GetSize(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				if (rowIndex != -1)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				return new Size(-1, -1);
			}
			else if (base.OwningColumn != null)
			{
				if (rowIndex != -1)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				return new Size(base.OwningColumn.Thickness, base.DataGridView.ColumnHeadersHeight);
			}
			else if (base.OwningRow != null)
			{
				if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (base.DataGridView.Rows.SharedRow(rowIndex) != base.OwningRow)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"rowIndex",
						rowIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				return new Size(base.DataGridView.RowHeadersWidth, base.OwningRow.GetHeight(rowIndex));
			}
			else
			{
				if (rowIndex != -1)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				return new Size(base.DataGridView.RowHeadersWidth, base.DataGridView.ColumnHeadersHeight);
			}
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0009C2C4 File Offset: 0x0009A4C4
		internal static Rectangle GetThemeMargins(Graphics g)
		{
			if (DataGridViewHeaderCell.rectThemeMargins.X == -1)
			{
				Rectangle bounds = new Rectangle(0, 0, 100, 100);
				Rectangle backgroundContentRectangle = DataGridViewHeaderCell.DataGridViewHeaderCellRenderer.VisualStyleRenderer.GetBackgroundContentRectangle(g, bounds);
				DataGridViewHeaderCell.rectThemeMargins.X = backgroundContentRectangle.X;
				DataGridViewHeaderCell.rectThemeMargins.Y = backgroundContentRectangle.Y;
				DataGridViewHeaderCell.rectThemeMargins.Width = 100 - backgroundContentRectangle.Right;
				DataGridViewHeaderCell.rectThemeMargins.Height = 100 - backgroundContentRectangle.Bottom;
				if (DataGridViewHeaderCell.rectThemeMargins.X == 3 && DataGridViewHeaderCell.rectThemeMargins.Y + DataGridViewHeaderCell.rectThemeMargins.Width + DataGridViewHeaderCell.rectThemeMargins.Height == 0)
				{
					DataGridViewHeaderCell.rectThemeMargins = new Rectangle(0, 0, 2, 3);
				}
				else
				{
					try
					{
						string fileName = Path.GetFileName(VisualStyleInformation.ThemeFilename);
						if (string.Equals(fileName, "Aero.msstyles", StringComparison.OrdinalIgnoreCase))
						{
							DataGridViewHeaderCell.rectThemeMargins = new Rectangle(2, 1, 0, 2);
						}
					}
					catch
					{
					}
				}
			}
			return DataGridViewHeaderCell.rectThemeMargins;
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x0009C3C8 File Offset: 0x0009A5C8
		protected override object GetValue(int rowIndex)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			return base.Properties.GetObject(DataGridViewCell.PropCellValue);
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0009C3E9 File Offset: 0x0009A5E9
		protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left && base.DataGridView.ApplyVisualStylesToHeaderCells;
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0009C408 File Offset: 0x0009A608
		protected override bool MouseEnterUnsharesRow(int rowIndex)
		{
			return base.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && rowIndex == base.DataGridView.MouseDownCellAddress.Y && base.DataGridView.ApplyVisualStylesToHeaderCells;
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0009C453 File Offset: 0x0009A653
		protected override bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return this.ButtonState != ButtonState.Normal && base.DataGridView.ApplyVisualStylesToHeaderCells;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0009C3E9 File Offset: 0x0009A5E9
		protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left && base.DataGridView.ApplyVisualStylesToHeaderCells;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0009C46C File Offset: 0x0009A66C
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left && base.DataGridView.ApplyVisualStylesToHeaderCells && !base.DataGridView.ResizingOperationAboutToStart)
			{
				this.UpdateButtonState(ButtonState.Pushed, e.RowIndex);
			}
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0009C4BC File Offset: 0x0009A6BC
		protected override void OnMouseEnter(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				if (base.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && rowIndex == base.DataGridView.MouseDownCellAddress.Y && this.ButtonState == ButtonState.Normal && Control.MouseButtons == MouseButtons.Left && !base.DataGridView.ResizingOperationAboutToStart)
				{
					this.UpdateButtonState(ButtonState.Pushed, rowIndex);
				}
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0009C54F File Offset: 0x0009A74F
		protected override void OnMouseLeave(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				if (this.ButtonState != ButtonState.Normal)
				{
					this.UpdateButtonState(ButtonState.Normal, rowIndex);
				}
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0009C589 File Offset: 0x0009A789
		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left && base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				this.UpdateButtonState(ButtonState.Normal, e.RowIndex);
			}
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0009C5BC File Offset: 0x0009A7BC
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			if (DataGridViewCell.PaintBackground(paintParts))
			{
				Rectangle rect = cellBounds;
				Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
				rect.Offset(rectangle.X, rectangle.Y);
				rect.Width -= rectangle.Right;
				rect.Height -= rectangle.Bottom;
				bool flag = (dataGridViewElementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
				SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
				if (cachedBrush.Color.A == 255)
				{
					graphics.FillRectangle(cachedBrush, rect);
				}
			}
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0009C694 File Offset: 0x0009A894
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewHeaderCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0009C6F0 File Offset: 0x0009A8F0
		private void UpdateButtonState(ButtonState newButtonState, int rowIndex)
		{
			this.ButtonStatePrivate = newButtonState;
			base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
		}

		// Token: 0x04000DD3 RID: 3539
		private const byte DATAGRIDVIEWHEADERCELL_themeMargin = 100;

		// Token: 0x04000DD4 RID: 3540
		private static Type defaultFormattedValueType = typeof(string);

		// Token: 0x04000DD5 RID: 3541
		private static Type defaultValueType = typeof(object);

		// Token: 0x04000DD6 RID: 3542
		private static Type cellType = typeof(DataGridViewHeaderCell);

		// Token: 0x04000DD7 RID: 3543
		private static Rectangle rectThemeMargins = new Rectangle(-1, -1, 0, 0);

		// Token: 0x04000DD8 RID: 3544
		private static readonly int PropValueType = PropertyStore.CreateKey();

		// Token: 0x04000DD9 RID: 3545
		private static readonly int PropButtonState = PropertyStore.CreateKey();

		// Token: 0x04000DDA RID: 3546
		private static readonly int PropFlipXPThemesBitmap = PropertyStore.CreateKey();

		// Token: 0x04000DDB RID: 3547
		private const string AEROTHEMEFILENAME = "Aero.msstyles";

		// Token: 0x02000672 RID: 1650
		private class DataGridViewHeaderCellRenderer
		{
			// Token: 0x0600667C RID: 26236 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewHeaderCellRenderer()
			{
			}

			// Token: 0x17001646 RID: 5702
			// (get) Token: 0x0600667D RID: 26237 RVA: 0x0017F038 File Offset: 0x0017D238
			public static VisualStyleRenderer VisualStyleRenderer
			{
				get
				{
					if (DataGridViewHeaderCell.DataGridViewHeaderCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewHeaderCell.DataGridViewHeaderCellRenderer.visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Header.Item.Normal);
					}
					return DataGridViewHeaderCell.DataGridViewHeaderCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x04003A74 RID: 14964
			private static VisualStyleRenderer visualStyleRenderer;
		}
	}
}
