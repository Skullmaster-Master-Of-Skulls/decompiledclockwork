using System;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000221 RID: 545
	public class DataGridViewTopLeftHeaderCell : DataGridViewColumnHeaderCell
	{
		// Token: 0x06002371 RID: 9073 RVA: 0x000A8AAF File Offset: 0x000A6CAF
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellAccessibleObject(this);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000A8AB8 File Offset: 0x000A6CB8
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
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates cellState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out cellState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, cellState, value, null, cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, true, false, false);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000A8B1C File Offset: 0x000A6D1C
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (rowIndex != -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates cellState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out cellState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, cellState, null, this.GetErrorText(rowIndex), cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, false, true, false);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000A8B7C File Offset: 0x000A6D7C
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
			Rectangle rectangle = this.BorderWidths(base.DataGridView.AdjustedTopLeftHeaderBorderStyle);
			int borderAndPaddingWidths = rectangle.Left + rectangle.Width + cellStyle.Padding.Horizontal;
			int borderAndPaddingHeights = rectangle.Top + rectangle.Height + cellStyle.Padding.Vertical;
			TextFormatFlags flags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			object obj = this.GetValue(rowIndex);
			if (!(obj is string))
			{
				obj = null;
			}
			return DataGridViewUtilities.GetPreferredRowHeaderSize(graphics, (string)obj, cellStyle, borderAndPaddingWidths, borderAndPaddingHeights, base.DataGridView.ShowCellErrors, false, constraintSize, flags);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000A8C5C File Offset: 0x000A6E5C
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000A8C94 File Offset: 0x000A6E94
		private Rectangle PaintPrivate(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			Rectangle result = Rectangle.Empty;
			Rectangle rectangle = cellBounds;
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			rectangle.Offset(rectangle2.X, rectangle2.Y);
			rectangle.Width -= rectangle2.Right;
			rectangle.Height -= rectangle2.Bottom;
			bool flag = (cellState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			if (paint && DataGridViewCell.PaintBackground(paintParts))
			{
				if (base.DataGridView.ApplyVisualStylesToHeaderCells)
				{
					int headerState = 1;
					if (base.ButtonState != ButtonState.Normal)
					{
						headerState = 3;
					}
					else if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex)
					{
						headerState = 2;
					}
					rectangle.Inflate(16, 16);
					DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.DrawHeader(graphics, rectangle, headerState);
					rectangle.Inflate(-16, -16);
				}
				else
				{
					SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
					if (cachedBrush.Color.A == 255)
					{
						graphics.FillRectangle(cachedBrush, rectangle);
					}
				}
			}
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
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
			Rectangle cellValueBounds = rectangle;
			string text = formattedValue as string;
			rectangle.Offset(1, 1);
			rectangle.Width -= 3;
			rectangle.Height -= 2;
			if (rectangle.Width > 0 && rectangle.Height > 0 && !string.IsNullOrEmpty(text) && (paint || computeContentBounds))
			{
				Color foreColor;
				if (base.DataGridView.ApplyVisualStylesToHeaderCells)
				{
					foreColor = DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
				}
				else
				{
					foreColor = (flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
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
						TextRenderer.DrawText(graphics, text, cellStyle.Font, rectangle, foreColor, textFormatFlags);
					}
				}
				else
				{
					result = DataGridViewUtilities.GetTextBounds(rectangle, text, textFormatFlags, cellStyle);
				}
			}
			else if (computeErrorIconBounds && !string.IsNullOrEmpty(errorText))
			{
				result = base.ComputeErrorIconBounds(cellValueBounds);
			}
			if (base.DataGridView.ShowCellErrors && paint && DataGridViewCell.PaintErrorIcon(paintParts))
			{
				base.PaintErrorIcon(graphics, cellStyle, rowIndex, cellBounds, cellValueBounds, errorText);
			}
			return result;
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000A8FD0 File Offset: 0x000A71D0
		protected override void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			base.PaintBorder(graphics, clipBounds, bounds, cellStyle, advancedBorderStyle);
			if (!base.DataGridView.RightToLeftInternal && base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				if (base.DataGridView.AdvancedColumnHeadersBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Inset)
				{
					Pen pen = null;
					Pen pen2 = null;
					base.GetContrastedPens(cellStyle.BackColor, ref pen, ref pen2);
					graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
					graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
					return;
				}
				if (base.DataGridView.AdvancedColumnHeadersBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Outset)
				{
					Pen pen3 = null;
					Pen pen4 = null;
					base.GetContrastedPens(cellStyle.BackColor, ref pen3, ref pen4);
					graphics.DrawLine(pen4, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
					graphics.DrawLine(pen4, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
					return;
				}
				if (base.DataGridView.AdvancedColumnHeadersBorderStyle.All == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					Pen pen5 = null;
					Pen pen6 = null;
					base.GetContrastedPens(cellStyle.BackColor, ref pen5, ref pen6);
					graphics.DrawLine(pen5, bounds.X + 1, bounds.Y + 1, bounds.X + 1, bounds.Bottom - 1);
					graphics.DrawLine(pen5, bounds.X + 1, bounds.Y + 1, bounds.Right - 1, bounds.Y + 1);
				}
			}
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000A917B File Offset: 0x000A737B
		public override string ToString()
		{
			return "DataGridViewTopLeftHeaderCell";
		}

		// Token: 0x04000E95 RID: 3733
		private static readonly VisualStyleElement HeaderElement = VisualStyleElement.Header.Item.Normal;

		// Token: 0x04000E96 RID: 3734
		private const byte DATAGRIDVIEWTOPLEFTHEADERCELL_horizontalTextMarginLeft = 1;

		// Token: 0x04000E97 RID: 3735
		private const byte DATAGRIDVIEWTOPLEFTHEADERCELL_horizontalTextMarginRight = 2;

		// Token: 0x04000E98 RID: 3736
		private const byte DATAGRIDVIEWTOPLEFTHEADERCELL_verticalTextMargin = 1;

		// Token: 0x0200067D RID: 1661
		private class DataGridViewTopLeftHeaderCellRenderer
		{
			// Token: 0x060066D5 RID: 26325 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewTopLeftHeaderCellRenderer()
			{
			}

			// Token: 0x17001666 RID: 5734
			// (get) Token: 0x060066D6 RID: 26326 RVA: 0x00180A85 File Offset: 0x0017EC85
			public static VisualStyleRenderer VisualStyleRenderer
			{
				get
				{
					if (DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewTopLeftHeaderCell.HeaderElement);
					}
					return DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x060066D7 RID: 26327 RVA: 0x00180AA2 File Offset: 0x0017ECA2
			public static void DrawHeader(Graphics g, Rectangle bounds, int headerState)
			{
				DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.VisualStyleRenderer.SetParameters(DataGridViewTopLeftHeaderCell.HeaderElement.ClassName, DataGridViewTopLeftHeaderCell.HeaderElement.Part, headerState);
				DataGridViewTopLeftHeaderCell.DataGridViewTopLeftHeaderCellRenderer.VisualStyleRenderer.DrawBackground(g, bounds, Rectangle.Truncate(g.ClipBounds));
			}

			// Token: 0x04003A85 RID: 14981
			private static VisualStyleRenderer visualStyleRenderer;
		}

		// Token: 0x0200067E RID: 1662
		protected class DataGridViewTopLeftHeaderCellAccessibleObject : DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject
		{
			// Token: 0x060066D8 RID: 26328 RVA: 0x00180ADA File Offset: 0x0017ECDA
			public DataGridViewTopLeftHeaderCellAccessibleObject(DataGridViewTopLeftHeaderCell owner) : base(owner)
			{
			}

			// Token: 0x17001667 RID: 5735
			// (get) Token: 0x060066D9 RID: 26329 RVA: 0x00180AE4 File Offset: 0x0017ECE4
			public override Rectangle Bounds
			{
				get
				{
					if (base.IsOwnerCellDestroyed())
					{
						return Rectangle.Empty;
					}
					Rectangle cellDisplayRectangle = base.Owner.DataGridView.GetCellDisplayRectangle(-1, -1, false);
					return base.Owner.DataGridView.RectangleToScreen(cellDisplayRectangle);
				}
			}

			// Token: 0x17001668 RID: 5736
			// (get) Token: 0x060066DA RID: 26330 RVA: 0x00180B24 File Offset: 0x0017ED24
			public override string DefaultAction
			{
				get
				{
					if (!base.IsOwnerCellDestroyed() && base.Owner.DataGridView.MultiSelect)
					{
						return SR.GetString("DataGridView_AccTopLeftColumnHeaderCellDefaultAction");
					}
					return string.Empty;
				}
			}

			// Token: 0x17001669 RID: 5737
			// (get) Token: 0x060066DB RID: 26331 RVA: 0x00180B50 File Offset: 0x0017ED50
			public override string Name
			{
				get
				{
					if (base.IsOwnerCellDestroyed())
					{
						return string.Empty;
					}
					object value = base.Owner.Value;
					if (value != null && !(value is string))
					{
						return string.Empty;
					}
					string value2 = value as string;
					if (!string.IsNullOrEmpty(value2))
					{
						return string.Empty;
					}
					if (base.Owner.DataGridView == null)
					{
						return string.Empty;
					}
					if (base.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return SR.GetString("DataGridView_AccTopLeftColumnHeaderCellName");
					}
					return SR.GetString("DataGridView_AccTopLeftColumnHeaderCellNameRTL");
				}
			}

			// Token: 0x1700166A RID: 5738
			// (get) Token: 0x060066DC RID: 26332 RVA: 0x00180BD8 File Offset: 0x0017EDD8
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
					if (base.Owner.DataGridView.AreAllCellsSelected(false))
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			// Token: 0x1700166B RID: 5739
			// (get) Token: 0x060066DD RID: 26333 RVA: 0x0017F055 File Offset: 0x0017D255
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x060066DE RID: 26334 RVA: 0x00180C2A File Offset: 0x0017EE2A
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (base.IsOwnerCellDestroyed())
				{
					return;
				}
				base.Owner.DataGridView.SelectAll();
			}

			// Token: 0x060066DF RID: 26335 RVA: 0x00180C48 File Offset: 0x0017EE48
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (base.IsOwnerCellDestroyed())
				{
					return null;
				}
				switch (navigationDirection)
				{
				case AccessibleNavigation.Left:
					if (base.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return null;
					}
					return this.NavigateForward();
				case AccessibleNavigation.Right:
					if (base.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return this.NavigateForward();
					}
					return null;
				case AccessibleNavigation.Next:
					return this.NavigateForward();
				case AccessibleNavigation.Previous:
					return null;
				default:
					return null;
				}
			}

			// Token: 0x060066E0 RID: 26336 RVA: 0x00180CBC File Offset: 0x0017EEBC
			private AccessibleObject NavigateForward()
			{
				if (base.IsOwnerCellDestroyed() || base.Owner.DataGridView.Columns.GetColumnCount(DataGridViewElementStates.Visible) == 0)
				{
					return null;
				}
				return base.Owner.DataGridView.AccessibilityObject.GetChild(0).GetChild(1);
			}

			// Token: 0x060066E1 RID: 26337 RVA: 0x00180D08 File Offset: 0x0017EF08
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if (base.Owner != null)
				{
					if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
					{
						base.Owner.DataGridView.FocusInternal();
						if (base.Owner.DataGridView.Columns.GetColumnCount(DataGridViewElementStates.Visible) > 0 && base.Owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible) > 0)
						{
							DataGridViewRow dataGridViewRow = base.Owner.DataGridView.Rows[base.Owner.DataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible)];
							DataGridViewColumn firstColumn = base.Owner.DataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Visible);
							base.Owner.DataGridView.SetCurrentCellAddressCoreInternal(firstColumn.Index, dataGridViewRow.Index, false, true, false);
						}
					}
					if ((flags & AccessibleSelection.AddSelection) == AccessibleSelection.AddSelection && base.Owner.DataGridView.MultiSelect)
					{
						base.Owner.DataGridView.SelectAll();
					}
					if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection && (flags & AccessibleSelection.AddSelection) == AccessibleSelection.None)
					{
						base.Owner.DataGridView.ClearSelection();
					}
					return;
				}
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
				{
					return;
				}
				throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
			}

			// Token: 0x060066E2 RID: 26338 RVA: 0x00180E2C File Offset: 0x0017F02C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerCellDestroyed())
				{
					return null;
				}
				DataGridView dataGridView = base.Owner.DataGridView;
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return dataGridView.AccessibilityObject.GetChild(0);
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					if (dataGridView.Columns.GetColumnCount(DataGridViewElementStates.Visible) == 0)
					{
						return null;
					}
					return this.NavigateForward();
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return null;
				default:
					return null;
				}
			}

			// Token: 0x060066E3 RID: 26339 RVA: 0x00180E8C File Offset: 0x0017F08C
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
					case 30008:
					case 30011:
					case 30012:
						goto IL_A4;
					case 30005:
						return this.Name;
					case 30007:
						return string.Empty;
					case 30009:
						break;
					case 30010:
						return !base.IsOwnerCellDestroyed() && base.Owner.DataGridView.Enabled;
					case 30013:
						return this.Help ?? string.Empty;
					default:
						if (propertyId != 30019 && propertyId != 30022)
						{
							goto IL_A4;
						}
						break;
					}
					return false;
				}
				IL_A4:
				return base.GetPropertyValue(propertyId);
			}
		}
	}
}
