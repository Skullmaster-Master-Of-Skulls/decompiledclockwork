using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x0200021D RID: 541
	public class DataGridViewTextBoxCell : DataGridViewCell
	{
		// Token: 0x0600232D RID: 9005 RVA: 0x000A74F8 File Offset: 0x000A56F8
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level1)
			{
				return new DataGridViewTextBoxCell.DataGridViewTextBoxCellAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x000A750E File Offset: 0x000A570E
		// (set) Token: 0x0600232F RID: 9007 RVA: 0x000A7525 File Offset: 0x000A5725
		private DataGridViewTextBoxEditingControl EditingTextBox
		{
			get
			{
				return (DataGridViewTextBoxEditingControl)base.Properties.GetObject(DataGridViewTextBoxCell.PropTextBoxCellEditingTextBox);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewTextBoxCell.PropTextBoxCellEditingTextBox))
				{
					base.Properties.SetObject(DataGridViewTextBoxCell.PropTextBoxCellEditingTextBox, value);
				}
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x000A754D File Offset: 0x000A574D
		public override Type FormattedValueType
		{
			get
			{
				return DataGridViewTextBoxCell.defaultFormattedValueType;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x000A7554 File Offset: 0x000A5754
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x000A7580 File Offset: 0x000A5780
		[DefaultValue(32767)]
		public virtual int MaxInputLength
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewTextBoxCell.PropTextBoxCellMaxInputLength, out flag);
				if (flag)
				{
					return integer;
				}
				return 32767;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MaxInputLength", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"MaxInputLength",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				base.Properties.SetInteger(DataGridViewTextBoxCell.PropTextBoxCellMaxInputLength, value);
				if (this.OwnsEditingTextBox(base.RowIndex))
				{
					this.EditingTextBox.MaxLength = value;
				}
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x000A7600 File Offset: 0x000A5800
		public override Type ValueType
		{
			get
			{
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				return DataGridViewTextBoxCell.defaultValueType;
			}
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000A7624 File Offset: 0x000A5824
		internal override void CacheEditingControl()
		{
			this.EditingTextBox = (base.DataGridView.EditingControl as DataGridViewTextBoxEditingControl);
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000A763C File Offset: 0x000A583C
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewTextBoxCell dataGridViewTextBoxCell;
			if (type == DataGridViewTextBoxCell.cellType)
			{
				dataGridViewTextBoxCell = new DataGridViewTextBoxCell();
			}
			else
			{
				dataGridViewTextBoxCell = (DataGridViewTextBoxCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewTextBoxCell);
			dataGridViewTextBoxCell.MaxInputLength = this.MaxInputLength;
			return dataGridViewTextBoxCell;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000A7688 File Offset: 0x000A5888
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override void DetachEditingControl()
		{
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView == null || dataGridView.EditingControl == null)
			{
				throw new InvalidOperationException();
			}
			TextBox textBox = dataGridView.EditingControl as TextBox;
			if (textBox != null)
			{
				textBox.ClearUndo();
			}
			this.EditingTextBox = null;
			base.DetachEditingControl();
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000A76D0 File Offset: 0x000A58D0
		private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds, DataGridViewCellStyle cellStyle)
		{
			TextBox textBox = base.DataGridView.EditingControl as TextBox;
			int width = editingControlBounds.Width;
			if (textBox != null)
			{
				DataGridViewContentAlignment alignment = cellStyle.Alignment;
				if (alignment <= DataGridViewContentAlignment.MiddleCenter)
				{
					switch (alignment)
					{
					case DataGridViewContentAlignment.TopLeft:
						break;
					case DataGridViewContentAlignment.TopCenter:
						goto IL_EF;
					case (DataGridViewContentAlignment)3:
						goto IL_171;
					case DataGridViewContentAlignment.TopRight:
						goto IL_116;
					default:
						if (alignment != DataGridViewContentAlignment.MiddleLeft)
						{
							if (alignment != DataGridViewContentAlignment.MiddleCenter)
							{
								goto IL_171;
							}
							goto IL_EF;
						}
						break;
					}
				}
				else if (alignment <= DataGridViewContentAlignment.BottomLeft)
				{
					if (alignment == DataGridViewContentAlignment.MiddleRight)
					{
						goto IL_116;
					}
					if (alignment != DataGridViewContentAlignment.BottomLeft)
					{
						goto IL_171;
					}
				}
				else
				{
					if (alignment == DataGridViewContentAlignment.BottomCenter)
					{
						goto IL_EF;
					}
					if (alignment != DataGridViewContentAlignment.BottomRight)
					{
						goto IL_171;
					}
					goto IL_116;
				}
				if (base.DataGridView.RightToLeftInternal)
				{
					editingControlBounds.X++;
					editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 3 - 2);
					goto IL_171;
				}
				editingControlBounds.X += 3;
				editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 3 - 1);
				goto IL_171;
				IL_EF:
				editingControlBounds.X++;
				editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 3);
				goto IL_171;
				IL_116:
				if (base.DataGridView.RightToLeftInternal)
				{
					editingControlBounds.X += 3;
					editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 4);
				}
				else
				{
					editingControlBounds.X++;
					editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 4 - 1);
				}
				IL_171:
				DataGridViewContentAlignment alignment2 = cellStyle.Alignment;
				if (alignment2 > DataGridViewContentAlignment.MiddleCenter)
				{
					if (alignment2 <= DataGridViewContentAlignment.BottomLeft)
					{
						if (alignment2 == DataGridViewContentAlignment.MiddleRight)
						{
							goto IL_1FB;
						}
						if (alignment2 != DataGridViewContentAlignment.BottomLeft)
						{
							goto IL_226;
						}
					}
					else if (alignment2 != DataGridViewContentAlignment.BottomCenter && alignment2 != DataGridViewContentAlignment.BottomRight)
					{
						goto IL_226;
					}
					editingControlBounds.Height = Math.Max(0, editingControlBounds.Height - 1);
					goto IL_226;
				}
				if (alignment2 <= DataGridViewContentAlignment.TopRight)
				{
					if (alignment2 - DataGridViewContentAlignment.TopLeft > 1 && alignment2 != DataGridViewContentAlignment.TopRight)
					{
						goto IL_226;
					}
					editingControlBounds.Y += 2;
					editingControlBounds.Height = Math.Max(0, editingControlBounds.Height - 2);
					goto IL_226;
				}
				else if (alignment2 != DataGridViewContentAlignment.MiddleLeft && alignment2 != DataGridViewContentAlignment.MiddleCenter)
				{
					goto IL_226;
				}
				IL_1FB:
				int height = editingControlBounds.Height;
				editingControlBounds.Height = height + 1;
				IL_226:
				int num;
				if (cellStyle.WrapMode == DataGridViewTriState.False)
				{
					num = textBox.PreferredSize.Height;
				}
				else
				{
					string text = (string)((IDataGridViewEditingControl)textBox).GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
					if (string.IsNullOrEmpty(text))
					{
						text = " ";
					}
					TextFormatFlags flags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
					using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
					{
						num = DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, width, flags);
					}
				}
				if (num < editingControlBounds.Height)
				{
					DataGridViewContentAlignment alignment3 = cellStyle.Alignment;
					if (alignment3 > DataGridViewContentAlignment.MiddleCenter)
					{
						if (alignment3 <= DataGridViewContentAlignment.BottomLeft)
						{
							if (alignment3 == DataGridViewContentAlignment.MiddleRight)
							{
								goto IL_314;
							}
							if (alignment3 != DataGridViewContentAlignment.BottomLeft)
							{
								return editingControlBounds;
							}
						}
						else if (alignment3 != DataGridViewContentAlignment.BottomCenter && alignment3 != DataGridViewContentAlignment.BottomRight)
						{
							return editingControlBounds;
						}
						editingControlBounds.Y += editingControlBounds.Height - num;
						return editingControlBounds;
					}
					if (alignment3 <= DataGridViewContentAlignment.TopRight)
					{
						if (alignment3 - DataGridViewContentAlignment.TopLeft > 1 && alignment3 != DataGridViewContentAlignment.TopRight)
						{
							return editingControlBounds;
						}
						return editingControlBounds;
					}
					else if (alignment3 != DataGridViewContentAlignment.MiddleLeft && alignment3 != DataGridViewContentAlignment.MiddleCenter)
					{
						return editingControlBounds;
					}
					IL_314:
					editingControlBounds.Y += (editingControlBounds.Height - num) / 2;
				}
			}
			return editingControlBounds;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000A7A34 File Offset: 0x000A5C34
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
			object value = this.GetValue(rowIndex);
			object formattedValue = this.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates cellState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out cellState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, cellState, formattedValue, null, cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, true, false, false);
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000A7AA8 File Offset: 0x000A5CA8
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
			DataGridViewElementStates cellState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out cellState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, cellState, null, this.GetErrorText(rowIndex), cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, false, true, false);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000A7B20 File Offset: 0x000A5D20
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
			object formattedValue = base.GetFormattedValue(rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize);
			string text = formattedValue as string;
			if (string.IsNullOrEmpty(text))
			{
				text = " ";
			}
			TextFormatFlags flags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			Size result;
			if (cellStyle.WrapMode == DataGridViewTriState.True && text.Length > 1)
			{
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
				{
					if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
					{
						result = new Size(DataGridViewCell.MeasureTextWidth(graphics, text, cellStyle.Font, Math.Max(1, constraintSize.Height - num2 - 1 - 1), flags), 0);
					}
					else
					{
						result = DataGridViewCell.MeasureTextPreferredSize(graphics, text, cellStyle.Font, 5f, flags);
					}
				}
				else
				{
					result = new Size(0, DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, Math.Max(1, constraintSize.Width - num), flags));
				}
			}
			else if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
				{
					result = new Size(DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, flags).Width, 0);
				}
				else
				{
					result = DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, flags);
				}
			}
			else
			{
				result = new Size(0, DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, flags).Height);
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				result.Width += num;
				if (base.DataGridView.ShowCellErrors)
				{
					result.Width = Math.Max(result.Width, num + 8 + (int)DataGridViewCell.iconsWidth);
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				int num3 = (cellStyle.WrapMode == DataGridViewTriState.True) ? 1 : 2;
				result.Height += num3 + 1 + num2;
				if (base.DataGridView.ShowCellErrors)
				{
					result.Height = Math.Max(result.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return result;
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000A7D60 File Offset: 0x000A5F60
		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			TextBox textBox = base.DataGridView.EditingControl as TextBox;
			if (textBox != null)
			{
				textBox.BorderStyle = BorderStyle.None;
				textBox.AcceptsReturn = (textBox.Multiline = (dataGridViewCellStyle.WrapMode == DataGridViewTriState.True));
				textBox.MaxLength = this.MaxInputLength;
				string text = initialFormattedValue as string;
				if (text == null)
				{
					textBox.Text = string.Empty;
				}
				else
				{
					textBox.Text = text;
				}
				this.EditingTextBox = (base.DataGridView.EditingControl as DataGridViewTextBoxEditingControl);
			}
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000A7DEC File Offset: 0x000A5FEC
		public override bool KeyEntersEditMode(KeyEventArgs e)
		{
			return (((char.IsLetterOrDigit((char)e.KeyCode) && (e.KeyCode < Keys.F1 || e.KeyCode > Keys.F24)) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide) || (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.OemBackslash) || (e.KeyCode == Keys.Space && !e.Shift)) && !e.Alt && !e.Control) || base.KeyEntersEditMode(e);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000A7E77 File Offset: 0x000A6077
		protected override void OnEnter(int rowIndex, bool throughMouseClick)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (throughMouseClick)
			{
				this.flagsState |= 1;
			}
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000A7E94 File Offset: 0x000A6094
		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			this.flagsState = (byte)((int)this.flagsState & -2);
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000A7EB0 File Offset: 0x000A60B0
		protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (currentCellAddress.X == e.ColumnIndex && currentCellAddress.Y == e.RowIndex && e.Button == MouseButtons.Left)
			{
				if ((this.flagsState & 1) != 0)
				{
					this.flagsState = (byte)((int)this.flagsState & -2);
					return;
				}
				if (base.DataGridView.EditMode != DataGridViewEditMode.EditProgrammatically)
				{
					base.DataGridView.BeginEdit(true);
				}
			}
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000A7F33 File Offset: 0x000A6133
		private bool OwnsEditingTextBox(int rowIndex)
		{
			return rowIndex != -1 && this.EditingTextBox != null && rowIndex == ((IDataGridViewEditingControl)this.EditingTextBox).EditingControlRowIndex;
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x000A7F54 File Offset: 0x000A6154
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000A7F8C File Offset: 0x000A618C
		private Rectangle PaintPrivate(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			Rectangle result = Rectangle.Empty;
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
			Rectangle rectangle2 = cellBounds;
			rectangle2.Offset(rectangle.X, rectangle.Y);
			rectangle2.Width -= rectangle.Right;
			rectangle2.Height -= rectangle.Bottom;
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			bool flag = currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex;
			bool flag2 = flag && base.DataGridView.EditingControl != null;
			bool flag3 = (cellState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			SolidBrush cachedBrush;
			if (DataGridViewCell.PaintSelectionBackground(paintParts) && flag3 && !flag2)
			{
				cachedBrush = base.DataGridView.GetCachedBrush(cellStyle.SelectionBackColor);
			}
			else
			{
				cachedBrush = base.DataGridView.GetCachedBrush(cellStyle.BackColor);
			}
			if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255 && rectangle2.Width > 0 && rectangle2.Height > 0)
			{
				graphics.FillRectangle(cachedBrush, rectangle2);
			}
			if (cellStyle.Padding != Padding.Empty)
			{
				if (base.DataGridView.RightToLeftInternal)
				{
					rectangle2.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
				}
				else
				{
					rectangle2.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
				}
				rectangle2.Width -= cellStyle.Padding.Horizontal;
				rectangle2.Height -= cellStyle.Padding.Vertical;
			}
			if (paint && flag && !flag2 && DataGridViewCell.PaintFocus(paintParts) && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && rectangle2.Width > 0 && rectangle2.Height > 0)
			{
				ControlPaint.DrawFocusRectangle(graphics, rectangle2, Color.Empty, cachedBrush.Color);
			}
			Rectangle cellValueBounds = rectangle2;
			string text = formattedValue as string;
			if (text != null && ((paint && !flag2) || computeContentBounds))
			{
				int num = (cellStyle.WrapMode == DataGridViewTriState.True) ? 1 : 2;
				rectangle2.Offset(0, num);
				rectangle2.Width = rectangle2.Width;
				rectangle2.Height -= num + 1;
				if (rectangle2.Width > 0 && rectangle2.Height > 0)
				{
					TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
					if (paint)
					{
						if (DataGridViewCell.PaintContentForeground(paintParts))
						{
							if ((textFormatFlags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
							{
								textFormatFlags |= TextFormatFlags.EndEllipsis;
							}
							TextRenderer.DrawText(graphics, text, cellStyle.Font, rectangle2, flag3 ? cellStyle.SelectionForeColor : cellStyle.ForeColor, textFormatFlags);
						}
					}
					else
					{
						result = DataGridViewUtilities.GetTextBounds(rectangle2, text, textFormatFlags, cellStyle);
					}
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

		// Token: 0x06002343 RID: 9027 RVA: 0x000A82FC File Offset: 0x000A64FC
		public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			Rectangle editingControlBounds = this.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
			editingControlBounds = this.GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
			base.DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
			base.DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000A8370 File Offset: 0x000A6570
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewTextBoxCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x04000E7B RID: 3707
		private static readonly int PropTextBoxCellMaxInputLength = PropertyStore.CreateKey();

		// Token: 0x04000E7C RID: 3708
		private static readonly int PropTextBoxCellEditingTextBox = PropertyStore.CreateKey();

		// Token: 0x04000E7D RID: 3709
		private const byte DATAGRIDVIEWTEXTBOXCELL_ignoreNextMouseClick = 1;

		// Token: 0x04000E7E RID: 3710
		private const byte DATAGRIDVIEWTEXTBOXCELL_horizontalTextOffsetLeft = 3;

		// Token: 0x04000E7F RID: 3711
		private const byte DATAGRIDVIEWTEXTBOXCELL_horizontalTextOffsetRight = 4;

		// Token: 0x04000E80 RID: 3712
		private const byte DATAGRIDVIEWTEXTBOXCELL_horizontalTextMarginLeft = 0;

		// Token: 0x04000E81 RID: 3713
		private const byte DATAGRIDVIEWTEXTBOXCELL_horizontalTextMarginRight = 0;

		// Token: 0x04000E82 RID: 3714
		private const byte DATAGRIDVIEWTEXTBOXCELL_verticalTextOffsetTop = 2;

		// Token: 0x04000E83 RID: 3715
		private const byte DATAGRIDVIEWTEXTBOXCELL_verticalTextOffsetBottom = 1;

		// Token: 0x04000E84 RID: 3716
		private const byte DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginTopWithWrapping = 1;

		// Token: 0x04000E85 RID: 3717
		private const byte DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginTopWithoutWrapping = 2;

		// Token: 0x04000E86 RID: 3718
		private const byte DATAGRIDVIEWTEXTBOXCELL_verticalTextMarginBottom = 1;

		// Token: 0x04000E87 RID: 3719
		private const int DATAGRIDVIEWTEXTBOXCELL_maxInputLength = 32767;

		// Token: 0x04000E88 RID: 3720
		private byte flagsState;

		// Token: 0x04000E89 RID: 3721
		private static Type defaultFormattedValueType = typeof(string);

		// Token: 0x04000E8A RID: 3722
		private static Type defaultValueType = typeof(object);

		// Token: 0x04000E8B RID: 3723
		private static Type cellType = typeof(DataGridViewTextBoxCell);

		// Token: 0x0200067C RID: 1660
		protected class DataGridViewTextBoxCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			// Token: 0x060066D2 RID: 26322 RVA: 0x0017C895 File Offset: 0x0017AA95
			public DataGridViewTextBoxCellAccessibleObject(DataGridViewCell owner) : base(owner)
			{
			}

			// Token: 0x060066D3 RID: 26323 RVA: 0x0017EF76 File Offset: 0x0017D176
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerCellDestroyed();
			}

			// Token: 0x060066D4 RID: 26324 RVA: 0x00180A69 File Offset: 0x0017EC69
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50004;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}
