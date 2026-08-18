using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Internal;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x020001A1 RID: 417
	public class DataGridViewButtonCell : DataGridViewCell
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x00087D9C File Offset: 0x00085F9C
		// (set) Token: 0x06001CFF RID: 7423 RVA: 0x00087DC2 File Offset: 0x00085FC2
		private ButtonState ButtonState
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewButtonCell.PropButtonCellState, out flag);
				if (flag)
				{
					return (ButtonState)integer;
				}
				return ButtonState.Normal;
			}
			set
			{
				if (this.ButtonState != value)
				{
					base.Properties.SetInteger(DataGridViewButtonCell.PropButtonCellState, (int)value);
				}
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x00015ECC File Offset: 0x000140CC
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x00087DE0 File Offset: 0x00085FE0
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x00087E08 File Offset: 0x00086008
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewButtonCell.PropButtonCellFlatStyle, out flag);
				if (flag)
				{
					return (FlatStyle)integer;
				}
				return FlatStyle.Standard;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
				}
				if (value != this.FlatStyle)
				{
					base.Properties.SetInteger(DataGridViewButtonCell.PropButtonCellFlatStyle, (int)value);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x17000647 RID: 1607
		// (set) Token: 0x06001D03 RID: 7427 RVA: 0x00087E5B File Offset: 0x0008605B
		internal FlatStyle FlatStyleInternal
		{
			set
			{
				if (value != this.FlatStyle)
				{
					base.Properties.SetInteger(DataGridViewButtonCell.PropButtonCellFlatStyle, (int)value);
				}
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x00087E77 File Offset: 0x00086077
		public override Type FormattedValueType
		{
			get
			{
				return DataGridViewButtonCell.defaultFormattedValueType;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x00087E80 File Offset: 0x00086080
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x00087EAB File Offset: 0x000860AB
		[DefaultValue(false)]
		public bool UseColumnTextForButtonValue
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewButtonCell.PropButtonCellUseColumnTextForButtonValue, out flag);
				return flag && integer != 0;
			}
			set
			{
				if (value != this.UseColumnTextForButtonValue)
				{
					base.Properties.SetInteger(DataGridViewButtonCell.PropButtonCellUseColumnTextForButtonValue, value ? 1 : 0);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x1700064A RID: 1610
		// (set) Token: 0x06001D07 RID: 7431 RVA: 0x00087ED3 File Offset: 0x000860D3
		internal bool UseColumnTextForButtonValueInternal
		{
			set
			{
				if (value != this.UseColumnTextForButtonValue)
				{
					base.Properties.SetInteger(DataGridViewButtonCell.PropButtonCellUseColumnTextForButtonValue, value ? 1 : 0);
				}
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x00087EF8 File Offset: 0x000860F8
		public override Type ValueType
		{
			get
			{
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				return DataGridViewButtonCell.defaultValueType;
			}
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00087F1C File Offset: 0x0008611C
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewButtonCell dataGridViewButtonCell;
			if (type == DataGridViewButtonCell.cellType)
			{
				dataGridViewButtonCell = new DataGridViewButtonCell();
			}
			else
			{
				dataGridViewButtonCell = (DataGridViewButtonCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewButtonCell);
			dataGridViewButtonCell.FlatStyleInternal = this.FlatStyle;
			dataGridViewButtonCell.UseColumnTextForButtonValueInternal = this.UseColumnTextForButtonValue;
			return dataGridViewButtonCell;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00087F71 File Offset: 0x00086171
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject(this);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00087F7C File Offset: 0x0008617C
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null)
			{
				return Rectangle.Empty;
			}
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates elementState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out elementState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, elementState, null, null, cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, true, false, false);
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00087FD4 File Offset: 0x000861D4
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null || !base.DataGridView.ShowCellErrors || string.IsNullOrEmpty(this.GetErrorText(rowIndex)))
			{
				return Rectangle.Empty;
			}
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates elementState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out elementState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, elementState, null, this.GetErrorText(rowIndex), cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, false, true, false);
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0008804C File Offset: 0x0008624C
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
			Rectangle stdBorderWidths = base.StdBorderWidths;
			int num = stdBorderWidths.Left + stdBorderWidths.Width + cellStyle.Padding.Horizontal;
			int num2 = stdBorderWidths.Top + stdBorderWidths.Height + cellStyle.Padding.Vertical;
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			string text = base.GetFormattedValue(rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize) as string;
			if (string.IsNullOrEmpty(text))
			{
				text = " ";
			}
			TextFormatFlags flags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			int num3;
			int num4;
			if (base.DataGridView.ApplyVisualStylesToInnerCells)
			{
				Rectangle themeMargins = DataGridViewButtonCell.GetThemeMargins(graphics);
				num3 = themeMargins.X + themeMargins.Width;
				num4 = themeMargins.Y + themeMargins.Height;
			}
			else
			{
				num4 = (num3 = 5);
			}
			Size result;
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
				{
					if (cellStyle.WrapMode == DataGridViewTriState.True && text.Length > 1 && constraintSize.Height - num2 - num4 - 2 > 0)
					{
						result = new Size(DataGridViewCell.MeasureTextWidth(graphics, text, cellStyle.Font, constraintSize.Height - num2 - num4 - 2, flags), 0);
					}
					else
					{
						result = new Size(DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, flags).Width, 0);
					}
				}
				else if (cellStyle.WrapMode == DataGridViewTriState.True && text.Length > 1)
				{
					result = DataGridViewCell.MeasureTextPreferredSize(graphics, text, cellStyle.Font, 5f, flags);
				}
				else
				{
					result = DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, flags);
				}
			}
			else if (cellStyle.WrapMode == DataGridViewTriState.True && text.Length > 1 && constraintSize.Width - num - num3 - 4 > 0)
			{
				result = new Size(0, DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, constraintSize.Width - num - num3 - 4, flags));
			}
			else
			{
				result = new Size(0, DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, flags).Height);
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				result.Width += num + num3 + 4;
				if (base.DataGridView.ShowCellErrors)
				{
					result.Width = Math.Max(result.Width, num + 8 + (int)DataGridViewCell.iconsWidth);
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				result.Height += num2 + num4 + 2;
				if (base.DataGridView.ShowCellErrors)
				{
					result.Height = Math.Max(result.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return result;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000882F8 File Offset: 0x000864F8
		private static Rectangle GetThemeMargins(Graphics g)
		{
			if (DataGridViewButtonCell.rectThemeMargins.X == -1)
			{
				Rectangle bounds = new Rectangle(0, 0, 100, 100);
				Rectangle backgroundContentRectangle = DataGridViewButtonCell.DataGridViewButtonCellRenderer.DataGridViewButtonRenderer.GetBackgroundContentRectangle(g, bounds);
				DataGridViewButtonCell.rectThemeMargins.X = backgroundContentRectangle.X;
				DataGridViewButtonCell.rectThemeMargins.Y = backgroundContentRectangle.Y;
				DataGridViewButtonCell.rectThemeMargins.Width = 100 - backgroundContentRectangle.Right;
				DataGridViewButtonCell.rectThemeMargins.Height = 100 - backgroundContentRectangle.Bottom;
			}
			return DataGridViewButtonCell.rectThemeMargins;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0008837C File Offset: 0x0008657C
		protected override object GetValue(int rowIndex)
		{
			if (this.UseColumnTextForButtonValue && base.DataGridView != null && base.DataGridView.NewRowIndex != rowIndex && base.OwningColumn != null && base.OwningColumn is DataGridViewButtonColumn)
			{
				return ((DataGridViewButtonColumn)base.OwningColumn).Text;
			}
			return base.GetValue(rowIndex);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000883D4 File Offset: 0x000865D4
		protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyCode == Keys.Space && !e.Alt && !e.Control && !e.Shift;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000883FB File Offset: 0x000865FB
		protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyCode == Keys.Space;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00088407 File Offset: 0x00086607
		protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00088418 File Offset: 0x00086618
		protected override bool MouseEnterUnsharesRow(int rowIndex)
		{
			return base.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && rowIndex == base.DataGridView.MouseDownCellAddress.Y;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00088458 File Offset: 0x00086658
		protected override bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return (this.ButtonState & ButtonState.Pushed) > ButtonState.Normal;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00088407 File Offset: 0x00086607
		protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return e.Button == MouseButtons.Left;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0008846C File Offset: 0x0008666C
		protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.KeyCode == Keys.Space && !e.Alt && !e.Control && !e.Shift)
			{
				this.UpdateButtonState(this.ButtonState | ButtonState.Checked, rowIndex);
				e.Handled = true;
			}
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000884C0 File Offset: 0x000866C0
		protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.KeyCode == Keys.Space)
			{
				this.UpdateButtonState(this.ButtonState & ~ButtonState.Checked, rowIndex);
				if (!e.Alt && !e.Control && !e.Shift)
				{
					base.RaiseCellClick(new DataGridViewCellEventArgs(base.ColumnIndex, rowIndex));
					if (base.DataGridView != null && base.ColumnIndex < base.DataGridView.Columns.Count && rowIndex < base.DataGridView.Rows.Count)
					{
						base.RaiseCellContentClick(new DataGridViewCellEventArgs(base.ColumnIndex, rowIndex));
					}
					e.Handled = true;
				}
			}
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0008856C File Offset: 0x0008676C
		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (this.ButtonState != ButtonState.Normal)
			{
				this.UpdateButtonState(ButtonState.Normal, rowIndex);
			}
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x00088587 File Offset: 0x00086787
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left && DataGridViewButtonCell.mouseInContentBounds)
			{
				this.UpdateButtonState(this.ButtonState | ButtonState.Pushed, e.RowIndex);
			}
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000885C0 File Offset: 0x000867C0
		protected override void OnMouseLeave(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (DataGridViewButtonCell.mouseInContentBounds)
			{
				DataGridViewButtonCell.mouseInContentBounds = false;
				if (base.ColumnIndex >= 0 && rowIndex >= 0 && (base.DataGridView.ApplyVisualStylesToInnerCells || this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup))
				{
					base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
				}
			}
			if ((this.ButtonState & ButtonState.Pushed) != ButtonState.Normal && base.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && rowIndex == base.DataGridView.MouseDownCellAddress.Y)
			{
				this.UpdateButtonState(this.ButtonState & ~ButtonState.Pushed, rowIndex);
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00088674 File Offset: 0x00086874
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			bool flag = DataGridViewButtonCell.mouseInContentBounds;
			DataGridViewButtonCell.mouseInContentBounds = base.GetContentBounds(e.RowIndex).Contains(e.X, e.Y);
			if (flag != DataGridViewButtonCell.mouseInContentBounds)
			{
				if (base.DataGridView.ApplyVisualStylesToInnerCells || this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
				{
					base.DataGridView.InvalidateCell(base.ColumnIndex, e.RowIndex);
				}
				if (e.ColumnIndex == base.DataGridView.MouseDownCellAddress.X && e.RowIndex == base.DataGridView.MouseDownCellAddress.Y && Control.MouseButtons == MouseButtons.Left)
				{
					if ((this.ButtonState & ButtonState.Pushed) == ButtonState.Normal && DataGridViewButtonCell.mouseInContentBounds && base.DataGridView.CellMouseDownInContentBounds)
					{
						this.UpdateButtonState(this.ButtonState | ButtonState.Pushed, e.RowIndex);
					}
					else if ((this.ButtonState & ButtonState.Pushed) != ButtonState.Normal && !DataGridViewButtonCell.mouseInContentBounds)
					{
						this.UpdateButtonState(this.ButtonState & ~ButtonState.Pushed, e.RowIndex);
					}
				}
			}
			base.OnMouseMove(e);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000887AA File Offset: 0x000869AA
		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.Button == MouseButtons.Left)
			{
				this.UpdateButtonState(this.ButtonState & ~ButtonState.Pushed, e.RowIndex);
			}
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000887DC File Offset: 0x000869DC
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, elementState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00088814 File Offset: 0x00086A14
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			bool flag = (elementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			bool flag2 = currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex;
			string text = formattedValue as string;
			SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
			SolidBrush cachedBrush2 = base.DataGridView.GetCachedBrush(flag ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle = cellBounds;
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			rectangle.Offset(rectangle2.X, rectangle2.Y);
			rectangle.Width -= rectangle2.Right;
			rectangle.Height -= rectangle2.Bottom;
			Rectangle result;
			if (rectangle.Height > 0 && rectangle.Width > 0)
			{
				if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
				{
					g.FillRectangle(cachedBrush, rectangle);
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
				if (rectangle.Height > 0 && rectangle.Width > 0 && (paint || computeContentBounds))
				{
					if (this.FlatStyle == FlatStyle.Standard || this.FlatStyle == FlatStyle.System)
					{
						if (base.DataGridView.ApplyVisualStylesToInnerCells)
						{
							if (paint && DataGridViewCell.PaintContentBackground(paintParts))
							{
								PushButtonState pushButtonState = PushButtonState.Normal;
								if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal)
								{
									pushButtonState = PushButtonState.Pressed;
								}
								else if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewButtonCell.mouseInContentBounds)
								{
									pushButtonState = PushButtonState.Hot;
								}
								if (DataGridViewCell.PaintFocus(paintParts) && flag2 && base.DataGridView.ShowFocusCues && base.DataGridView.Focused)
								{
									pushButtonState |= PushButtonState.Default;
								}
								DataGridViewButtonCell.DataGridViewButtonCellRenderer.DrawButton(g, rectangle, (int)pushButtonState);
							}
							result = rectangle;
							rectangle = DataGridViewButtonCell.DataGridViewButtonCellRenderer.DataGridViewButtonRenderer.GetBackgroundContentRectangle(g, rectangle);
						}
						else
						{
							if (paint && DataGridViewCell.PaintContentBackground(paintParts))
							{
								ControlPaint.DrawBorder(g, rectangle, SystemColors.Control, (this.ButtonState == ButtonState.Normal) ? ButtonBorderStyle.Outset : ButtonBorderStyle.Inset);
							}
							result = rectangle;
							rectangle.Inflate(-SystemInformation.Border3DSize.Width, -SystemInformation.Border3DSize.Height);
						}
					}
					else if (this.FlatStyle == FlatStyle.Flat)
					{
						rectangle.Inflate(-1, -1);
						if (paint && DataGridViewCell.PaintContentBackground(paintParts))
						{
							ButtonBaseAdapter.DrawDefaultBorder(g, rectangle, cachedBrush2.Color, true);
							if (cachedBrush.Color.A == 255)
							{
								if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal)
								{
									ButtonBaseAdapter.ColorData colorData = ButtonBaseAdapter.PaintFlatRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
									IntPtr hdc = g.GetHdc();
									try
									{
										using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
										{
											WindowsBrush windowsBrush;
											if (colorData.options.highContrast)
											{
												windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, colorData.buttonShadow);
											}
											else
											{
												windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, colorData.lowHighlight);
											}
											try
											{
												ButtonBaseAdapter.PaintButtonBackground(windowsGraphics, rectangle, windowsBrush);
												goto IL_4C2;
											}
											finally
											{
												windowsBrush.Dispose();
											}
										}
									}
									finally
									{
										g.ReleaseHdc();
									}
								}
								if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewButtonCell.mouseInContentBounds)
								{
									IntPtr hdc2 = g.GetHdc();
									try
									{
										using (WindowsGraphics windowsGraphics2 = WindowsGraphics.FromHdc(hdc2))
										{
											Color controlDark = SystemColors.ControlDark;
											using (WindowsBrush windowsBrush2 = new WindowsSolidBrush(windowsGraphics2.DeviceContext, controlDark))
											{
												ButtonBaseAdapter.PaintButtonBackground(windowsGraphics2, rectangle, windowsBrush2);
											}
										}
									}
									finally
									{
										g.ReleaseHdc();
									}
								}
							}
						}
						IL_4C2:
						result = rectangle;
					}
					else
					{
						rectangle.Inflate(-1, -1);
						if (paint && DataGridViewCell.PaintContentBackground(paintParts))
						{
							if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal)
							{
								ButtonBaseAdapter.ColorData colorData2 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
								ButtonBaseAdapter.DrawDefaultBorder(g, rectangle, colorData2.options.highContrast ? colorData2.windowText : colorData2.windowFrame, true);
								ControlPaint.DrawBorder(g, rectangle, colorData2.options.highContrast ? colorData2.windowText : colorData2.buttonShadow, ButtonBorderStyle.Solid);
							}
							else if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewButtonCell.mouseInContentBounds)
							{
								ButtonBaseAdapter.ColorData colorData3 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
								ButtonBaseAdapter.DrawDefaultBorder(g, rectangle, colorData3.options.highContrast ? colorData3.windowText : colorData3.buttonShadow, false);
								ButtonBaseAdapter.Draw3DLiteBorder(g, rectangle, colorData3, true);
							}
							else
							{
								ButtonBaseAdapter.ColorData colorData4 = ButtonBaseAdapter.PaintPopupRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
								ButtonBaseAdapter.DrawDefaultBorder(g, rectangle, colorData4.options.highContrast ? colorData4.windowText : colorData4.buttonShadow, false);
								ButtonBaseAdapter.DrawFlatBorder(g, rectangle, colorData4.options.highContrast ? colorData4.windowText : colorData4.buttonShadow);
							}
						}
						result = rectangle;
					}
				}
				else if (computeErrorIconBounds)
				{
					if (!string.IsNullOrEmpty(errorText))
					{
						result = base.ComputeErrorIconBounds(cellValueBounds);
					}
					else
					{
						result = Rectangle.Empty;
					}
				}
				else
				{
					result = Rectangle.Empty;
				}
				if (paint && DataGridViewCell.PaintFocus(paintParts) && flag2 && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && rectangle.Width > 2 * SystemInformation.Border3DSize.Width + 1 && rectangle.Height > 2 * SystemInformation.Border3DSize.Height + 1)
				{
					if (this.FlatStyle == FlatStyle.System || this.FlatStyle == FlatStyle.Standard)
					{
						ControlPaint.DrawFocusRectangle(g, Rectangle.Inflate(rectangle, -1, -1), Color.Empty, SystemColors.Control);
					}
					else if (this.FlatStyle == FlatStyle.Flat)
					{
						if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal || (base.DataGridView.CurrentCellAddress.Y == rowIndex && base.DataGridView.CurrentCellAddress.X == base.ColumnIndex))
						{
							ButtonBaseAdapter.ColorData colorData5 = ButtonBaseAdapter.PaintFlatRender(g, cellStyle.ForeColor, cellStyle.BackColor, base.DataGridView.Enabled).Calculate();
							string text2 = (text != null) ? text : string.Empty;
							ButtonBaseAdapter.LayoutOptions layoutOptions = ButtonFlatAdapter.PaintFlatLayout(g, true, SystemInformation.HighContrast, 1, rectangle, Padding.Empty, false, cellStyle.Font, text2, base.DataGridView.Enabled, DataGridViewUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.DataGridView.RightToLeft);
							layoutOptions.everettButtonCompat = false;
							ButtonBaseAdapter.LayoutData layoutData = layoutOptions.Layout();
							ButtonBaseAdapter.DrawFlatFocus(g, layoutData.focus, colorData5.options.highContrast ? colorData5.windowText : colorData5.constrastButtonShadow);
						}
					}
					else if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal || (base.DataGridView.CurrentCellAddress.Y == rowIndex && base.DataGridView.CurrentCellAddress.X == base.ColumnIndex))
					{
						bool up = this.ButtonState == ButtonState.Normal;
						string text3 = (text != null) ? text : string.Empty;
						ButtonBaseAdapter.LayoutOptions layoutOptions2 = ButtonPopupAdapter.PaintPopupLayout(g, up, SystemInformation.HighContrast ? 2 : 1, rectangle, Padding.Empty, false, cellStyle.Font, text3, base.DataGridView.Enabled, DataGridViewUtilities.ComputeDrawingContentAlignmentForCellStyleAlignment(cellStyle.Alignment), base.DataGridView.RightToLeft);
						layoutOptions2.everettButtonCompat = false;
						ButtonBaseAdapter.LayoutData layoutData2 = layoutOptions2.Layout();
						ControlPaint.DrawFocusRectangle(g, layoutData2.focus, cellStyle.ForeColor, cellStyle.BackColor);
					}
				}
				if (text != null && paint && DataGridViewCell.PaintContentForeground(paintParts))
				{
					rectangle.Offset(2, 1);
					rectangle.Width -= 4;
					rectangle.Height -= 2;
					if ((this.ButtonState & (ButtonState.Checked | ButtonState.Pushed)) != ButtonState.Normal && this.FlatStyle != FlatStyle.Flat && this.FlatStyle != FlatStyle.Popup)
					{
						rectangle.Offset(1, 1);
						int num = rectangle.Width;
						rectangle.Width = num - 1;
						num = rectangle.Height;
						rectangle.Height = num - 1;
					}
					if (rectangle.Width > 0 && rectangle.Height > 0)
					{
						Color color;
						if (base.DataGridView.ApplyVisualStylesToInnerCells && (this.FlatStyle == FlatStyle.System || this.FlatStyle == FlatStyle.Standard))
						{
							color = DataGridViewButtonCell.DataGridViewButtonCellRenderer.DataGridViewButtonRenderer.GetColor(ColorProperty.TextColor);
						}
						else
						{
							color = cachedBrush2.Color;
						}
						TextFormatFlags flags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
						TextRenderer.DrawText(g, text, cellStyle.Font, rectangle, color, flags);
					}
				}
				if (base.DataGridView.ShowCellErrors && paint && DataGridViewCell.PaintErrorIcon(paintParts))
				{
					base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, cellValueBounds, errorText);
				}
			}
			else
			{
				result = Rectangle.Empty;
			}
			return result;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000892D4 File Offset: 0x000874D4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewButtonCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x00089330 File Offset: 0x00087530
		private void UpdateButtonState(ButtonState newButtonState, int rowIndex)
		{
			if (this.ButtonState != newButtonState)
			{
				this.ButtonState = newButtonState;
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
		}

		// Token: 0x04000C85 RID: 3205
		private static readonly int PropButtonCellFlatStyle = PropertyStore.CreateKey();

		// Token: 0x04000C86 RID: 3206
		private static readonly int PropButtonCellState = PropertyStore.CreateKey();

		// Token: 0x04000C87 RID: 3207
		private static readonly int PropButtonCellUseColumnTextForButtonValue = PropertyStore.CreateKey();

		// Token: 0x04000C88 RID: 3208
		private static readonly VisualStyleElement ButtonElement = VisualStyleElement.Button.PushButton.Normal;

		// Token: 0x04000C89 RID: 3209
		private const byte DATAGRIDVIEWBUTTONCELL_themeMargin = 100;

		// Token: 0x04000C8A RID: 3210
		private const byte DATAGRIDVIEWBUTTONCELL_horizontalTextMargin = 2;

		// Token: 0x04000C8B RID: 3211
		private const byte DATAGRIDVIEWBUTTONCELL_verticalTextMargin = 1;

		// Token: 0x04000C8C RID: 3212
		private const byte DATAGRIDVIEWBUTTONCELL_textPadding = 5;

		// Token: 0x04000C8D RID: 3213
		private static Rectangle rectThemeMargins = new Rectangle(-1, -1, 0, 0);

		// Token: 0x04000C8E RID: 3214
		private static bool mouseInContentBounds = false;

		// Token: 0x04000C8F RID: 3215
		private static Type defaultFormattedValueType = typeof(string);

		// Token: 0x04000C90 RID: 3216
		private static Type defaultValueType = typeof(object);

		// Token: 0x04000C91 RID: 3217
		private static Type cellType = typeof(DataGridViewButtonCell);

		// Token: 0x02000665 RID: 1637
		private class DataGridViewButtonCellRenderer
		{
			// Token: 0x060065FA RID: 26106 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewButtonCellRenderer()
			{
			}

			// Token: 0x17001619 RID: 5657
			// (get) Token: 0x060065FB RID: 26107 RVA: 0x0017C840 File Offset: 0x0017AA40
			public static VisualStyleRenderer DataGridViewButtonRenderer
			{
				get
				{
					if (DataGridViewButtonCell.DataGridViewButtonCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewButtonCell.DataGridViewButtonCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewButtonCell.ButtonElement);
					}
					return DataGridViewButtonCell.DataGridViewButtonCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x060065FC RID: 26108 RVA: 0x0017C85D File Offset: 0x0017AA5D
			public static void DrawButton(Graphics g, Rectangle bounds, int buttonState)
			{
				DataGridViewButtonCell.DataGridViewButtonCellRenderer.DataGridViewButtonRenderer.SetParameters(DataGridViewButtonCell.ButtonElement.ClassName, DataGridViewButtonCell.ButtonElement.Part, buttonState);
				DataGridViewButtonCell.DataGridViewButtonCellRenderer.DataGridViewButtonRenderer.DrawBackground(g, bounds, Rectangle.Truncate(g.ClipBounds));
			}

			// Token: 0x04003A5F RID: 14943
			private static VisualStyleRenderer visualStyleRenderer;
		}

		// Token: 0x02000666 RID: 1638
		protected class DataGridViewButtonCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			// Token: 0x060065FD RID: 26109 RVA: 0x0017C895 File Offset: 0x0017AA95
			public DataGridViewButtonCellAccessibleObject(DataGridViewCell owner) : base(owner)
			{
			}

			// Token: 0x1700161A RID: 5658
			// (get) Token: 0x060065FE RID: 26110 RVA: 0x0017C89E File Offset: 0x0017AA9E
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("DataGridView_AccButtonCellDefaultAction");
				}
			}

			// Token: 0x060065FF RID: 26111 RVA: 0x0017C8AC File Offset: 0x0017AAAC
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (base.IsOwnerCellDestroyed())
				{
					return;
				}
				DataGridViewButtonCell dataGridViewButtonCell = (DataGridViewButtonCell)base.Owner;
				DataGridView dataGridView = dataGridViewButtonCell.DataGridView;
				if (dataGridView != null && dataGridViewButtonCell.RowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
				}
				if (dataGridViewButtonCell.OwningColumn != null && dataGridViewButtonCell.OwningRow != null)
				{
					dataGridView.OnCellClickInternal(new DataGridViewCellEventArgs(dataGridViewButtonCell.ColumnIndex, dataGridViewButtonCell.RowIndex));
					dataGridView.OnCellContentClickInternal(new DataGridViewCellEventArgs(dataGridViewButtonCell.ColumnIndex, dataGridViewButtonCell.RowIndex));
				}
			}

			// Token: 0x06006600 RID: 26112 RVA: 0x00011A20 File Offset: 0x0000FC20
			public override int GetChildCount()
			{
				return 0;
			}

			// Token: 0x06006601 RID: 26113 RVA: 0x0017C92F File Offset: 0x0017AB2F
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerCellDestroyed() && (AccessibilityImprovements.Level2 || base.IsIAccessibleExSupported());
			}

			// Token: 0x06006602 RID: 26114 RVA: 0x0017C94A File Offset: 0x0017AB4A
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50000;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}
