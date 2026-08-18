using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000205 RID: 517
	public class DataGridViewLinkCell : DataGridViewCell
	{
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0009E138 File Offset: 0x0009C338
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x0009E1A4 File Offset: 0x0009C3A4
		public Color ActiveLinkColor
		{
			get
			{
				if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellActiveLinkColor))
				{
					return (Color)base.Properties.GetObject(DataGridViewLinkCell.PropLinkCellActiveLinkColor);
				}
				if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
				{
					return this.HighContrastLinkColor;
				}
				if (!AccessibilityImprovements.Level5)
				{
					return LinkUtilities.IEActiveLinkColor;
				}
				if (!this.Selected)
				{
					return LinkUtilities.IEActiveLinkColor;
				}
				return SystemColors.HighlightText;
			}
			set
			{
				if (!value.Equals(this.ActiveLinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellActiveLinkColor, value);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x1700078A RID: 1930
		// (set) Token: 0x06002188 RID: 8584 RVA: 0x0009E210 File Offset: 0x0009C410
		internal Color ActiveLinkColorInternal
		{
			set
			{
				if (!value.Equals(this.ActiveLinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellActiveLinkColor, value);
				}
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0009E244 File Offset: 0x0009C444
		private bool ShouldSerializeActiveLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.ActiveLinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.ActiveLinkColor.Equals(LinkUtilities.IEActiveLinkColor);
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x00015ECC File Offset: 0x000140CC
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x0009E2A2 File Offset: 0x0009C4A2
		public override Type FormattedValueType
		{
			get
			{
				return DataGridViewLinkCell.defaultFormattedValueType;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0009E2AC File Offset: 0x0009C4AC
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x0009E2D4 File Offset: 0x0009C4D4
		[DefaultValue(LinkBehavior.SystemDefault)]
		public LinkBehavior LinkBehavior
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewLinkCell.PropLinkCellLinkBehavior, out flag);
				if (flag)
				{
					return (LinkBehavior)integer;
				}
				return LinkBehavior.SystemDefault;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(LinkBehavior));
				}
				if (value != this.LinkBehavior)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellLinkBehavior, (int)value);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x1700078E RID: 1934
		// (set) Token: 0x0600218E RID: 8590 RVA: 0x0009E350 File Offset: 0x0009C550
		internal LinkBehavior LinkBehaviorInternal
		{
			set
			{
				if (value != this.LinkBehavior)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellLinkBehavior, (int)value);
				}
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x0009E36C File Offset: 0x0009C56C
		// (set) Token: 0x06002190 RID: 8592 RVA: 0x0009E3D8 File Offset: 0x0009C5D8
		public Color LinkColor
		{
			get
			{
				if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellLinkColor))
				{
					return (Color)base.Properties.GetObject(DataGridViewLinkCell.PropLinkCellLinkColor);
				}
				if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
				{
					return this.HighContrastLinkColor;
				}
				if (!AccessibilityImprovements.Level5)
				{
					return LinkUtilities.IELinkColor;
				}
				if (!this.Selected)
				{
					return LinkUtilities.IELinkColor;
				}
				return SystemColors.HighlightText;
			}
			set
			{
				if (!value.Equals(this.LinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellLinkColor, value);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x17000790 RID: 1936
		// (set) Token: 0x06002191 RID: 8593 RVA: 0x0009E444 File Offset: 0x0009C644
		internal Color LinkColorInternal
		{
			set
			{
				if (!value.Equals(this.LinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellLinkColor, value);
				}
			}
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0009E478 File Offset: 0x0009C678
		private bool ShouldSerializeLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.LinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.LinkColor.Equals(LinkUtilities.IELinkColor);
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x0009E4D8 File Offset: 0x0009C6D8
		// (set) Token: 0x06002194 RID: 8596 RVA: 0x0009E4FE File Offset: 0x0009C6FE
		private LinkState LinkState
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewLinkCell.PropLinkCellLinkState, out flag);
				if (flag)
				{
					return (LinkState)integer;
				}
				return LinkState.Normal;
			}
			set
			{
				if (this.LinkState != value)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellLinkState, (int)value);
				}
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x0009E51A File Offset: 0x0009C71A
		// (set) Token: 0x06002196 RID: 8598 RVA: 0x0009E52C File Offset: 0x0009C72C
		public bool LinkVisited
		{
			get
			{
				return this.linkVisitedSet && this.linkVisited;
			}
			set
			{
				this.linkVisitedSet = true;
				if (value != this.LinkVisited)
				{
					this.linkVisited = value;
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0009E580 File Offset: 0x0009C780
		private bool ShouldSerializeLinkVisited()
		{
			return this.linkVisitedSet = true;
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x0009E598 File Offset: 0x0009C798
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x0009E5C4 File Offset: 0x0009C7C4
		[DefaultValue(true)]
		public bool TrackVisitedState
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewLinkCell.PropLinkCellTrackVisitedState, out flag);
				return !flag || integer != 0;
			}
			set
			{
				if (value != this.TrackVisitedState)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellTrackVisitedState, value ? 1 : 0);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x17000794 RID: 1940
		// (set) Token: 0x0600219A RID: 8602 RVA: 0x0009E620 File Offset: 0x0009C820
		internal bool TrackVisitedStateInternal
		{
			set
			{
				if (value != this.TrackVisitedState)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellTrackVisitedState, value ? 1 : 0);
				}
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x0009E644 File Offset: 0x0009C844
		// (set) Token: 0x0600219C RID: 8604 RVA: 0x0009E66F File Offset: 0x0009C86F
		[DefaultValue(false)]
		public bool UseColumnTextForLinkValue
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewLinkCell.PropLinkCellUseColumnTextForLinkValue, out flag);
				return flag && integer != 0;
			}
			set
			{
				if (value != this.UseColumnTextForLinkValue)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellUseColumnTextForLinkValue, value ? 1 : 0);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x17000796 RID: 1942
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x0009E697 File Offset: 0x0009C897
		internal bool UseColumnTextForLinkValueInternal
		{
			set
			{
				if (value != this.UseColumnTextForLinkValue)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellUseColumnTextForLinkValue, value ? 1 : 0);
				}
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x0009E6BC File Offset: 0x0009C8BC
		// (set) Token: 0x0600219F RID: 8607 RVA: 0x0009E734 File Offset: 0x0009C934
		public Color VisitedLinkColor
		{
			get
			{
				if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellVisitedLinkColor))
				{
					return (Color)base.Properties.GetObject(DataGridViewLinkCell.PropLinkCellVisitedLinkColor);
				}
				if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
				{
					if (!this.Selected)
					{
						return LinkUtilities.GetVisitedLinkColor();
					}
					return SystemColors.HighlightText;
				}
				else
				{
					if (!AccessibilityImprovements.Level5)
					{
						return LinkUtilities.IEVisitedLinkColor;
					}
					if (!this.Selected)
					{
						return LinkUtilities.IEVisitedLinkColor;
					}
					return SystemColors.HighlightText;
				}
			}
			set
			{
				if (!value.Equals(this.VisitedLinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellVisitedLinkColor, value);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x17000798 RID: 1944
		// (set) Token: 0x060021A0 RID: 8608 RVA: 0x0009E7A0 File Offset: 0x0009C9A0
		internal Color VisitedLinkColorInternal
		{
			set
			{
				if (!value.Equals(this.VisitedLinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellVisitedLinkColor, value);
				}
			}
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0009E7D4 File Offset: 0x0009C9D4
		private bool ShouldSerializeVisitedLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.VisitedLinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.VisitedLinkColor.Equals(LinkUtilities.IEVisitedLinkColor);
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x0009E832 File Offset: 0x0009CA32
		private Color HighContrastLinkColor
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (!this.Selected)
				{
					return SystemColors.HotTrack;
				}
				return SystemColors.HighlightText;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x0009E848 File Offset: 0x0009CA48
		public override Type ValueType
		{
			get
			{
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				return DataGridViewLinkCell.defaultValueType;
			}
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0009E86C File Offset: 0x0009CA6C
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewLinkCell dataGridViewLinkCell;
			if (type == DataGridViewLinkCell.cellType)
			{
				dataGridViewLinkCell = new DataGridViewLinkCell();
			}
			else
			{
				dataGridViewLinkCell = (DataGridViewLinkCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewLinkCell);
			if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellActiveLinkColor))
			{
				dataGridViewLinkCell.ActiveLinkColorInternal = this.ActiveLinkColor;
			}
			if (base.Properties.ContainsInteger(DataGridViewLinkCell.PropLinkCellUseColumnTextForLinkValue))
			{
				dataGridViewLinkCell.UseColumnTextForLinkValueInternal = this.UseColumnTextForLinkValue;
			}
			if (base.Properties.ContainsInteger(DataGridViewLinkCell.PropLinkCellLinkBehavior))
			{
				dataGridViewLinkCell.LinkBehaviorInternal = this.LinkBehavior;
			}
			if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellLinkColor))
			{
				dataGridViewLinkCell.LinkColorInternal = this.LinkColor;
			}
			if (base.Properties.ContainsInteger(DataGridViewLinkCell.PropLinkCellTrackVisitedState))
			{
				dataGridViewLinkCell.TrackVisitedStateInternal = this.TrackVisitedState;
			}
			if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellVisitedLinkColor))
			{
				dataGridViewLinkCell.VisitedLinkColorInternal = this.VisitedLinkColor;
			}
			if (this.linkVisitedSet)
			{
				dataGridViewLinkCell.LinkVisited = this.LinkVisited;
			}
			return dataGridViewLinkCell;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0009E974 File Offset: 0x0009CB74
		private bool LinkBoundsContainPoint(int x, int y, int rowIndex)
		{
			return base.GetContentBounds(rowIndex).Contains(x, y);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0009E992 File Offset: 0x0009CB92
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject(this);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0009E99C File Offset: 0x0009CB9C
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

		// Token: 0x060021A8 RID: 8616 RVA: 0x0009EA10 File Offset: 0x0009CC10
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
			object value = this.GetValue(rowIndex);
			object formattedValue = this.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates cellState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out cellState, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, cellState, formattedValue, this.GetErrorText(rowIndex), cellStyle, advancedBorderStyle, DataGridViewPaintParts.ContentForeground, false, true, false);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0009EAA4 File Offset: 0x0009CCA4
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
						int num3 = constraintSize.Height - num2 - 1 - 1;
						if ((cellStyle.Alignment & DataGridViewLinkCell.anyBottom) != DataGridViewContentAlignment.NotSet)
						{
							num3--;
						}
						result = new Size(DataGridViewCell.MeasureTextWidth(graphics, text, cellStyle.Font, Math.Max(1, num3), flags), 0);
					}
					else
					{
						result = DataGridViewCell.MeasureTextPreferredSize(graphics, text, cellStyle.Font, 5f, flags);
					}
				}
				else
				{
					result = new Size(0, DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, Math.Max(1, constraintSize.Width - num - 1 - 2), flags));
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
				result.Width += 3 + num;
				if (base.DataGridView.ShowCellErrors)
				{
					result.Width = Math.Max(result.Width, num + 8 + (int)DataGridViewCell.iconsWidth);
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				result.Height += 2 + num2;
				if ((cellStyle.Alignment & DataGridViewLinkCell.anyBottom) != DataGridViewContentAlignment.NotSet)
				{
					result.Height++;
				}
				if (base.DataGridView.ShowCellErrors)
				{
					result.Height = Math.Max(result.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return result;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0009ED10 File Offset: 0x0009CF10
		protected override object GetValue(int rowIndex)
		{
			if (this.UseColumnTextForLinkValue && base.DataGridView != null && base.DataGridView.NewRowIndex != rowIndex && base.OwningColumn != null && base.OwningColumn is DataGridViewLinkColumn)
			{
				return ((DataGridViewLinkColumn)base.OwningColumn).Text;
			}
			return base.GetValue(rowIndex);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0009ED68 File Offset: 0x0009CF68
		protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyCode != Keys.Space || e.Alt || e.Control || e.Shift || (this.TrackVisitedState && !this.LinkVisited);
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0009EDA1 File Offset: 0x0009CFA1
		protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex);
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x0009EDBB File Offset: 0x0009CFBB
		protected override bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return this.LinkState > LinkState.Normal;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0009EDC6 File Offset: 0x0009CFC6
		protected override bool MouseMoveUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex))
			{
				if ((this.LinkState & LinkState.Hover) == LinkState.Normal)
				{
					return true;
				}
			}
			else if ((this.LinkState & LinkState.Hover) != LinkState.Normal)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x0009EDFB File Offset: 0x0009CFFB
		protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return this.TrackVisitedState && this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0009EE20 File Offset: 0x0009D020
		protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.KeyCode == Keys.Space && !e.Alt && !e.Control && !e.Shift)
			{
				base.RaiseCellClick(new DataGridViewCellEventArgs(base.ColumnIndex, rowIndex));
				if (base.DataGridView != null && base.ColumnIndex < base.DataGridView.Columns.Count && rowIndex < base.DataGridView.Rows.Count)
				{
					base.RaiseCellContentClick(new DataGridViewCellEventArgs(base.ColumnIndex, rowIndex));
					if (this.TrackVisitedState)
					{
						this.LinkVisited = true;
					}
				}
				e.Handled = true;
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x0009EEC8 File Offset: 0x0009D0C8
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex))
			{
				this.LinkState |= LinkState.Active;
				base.DataGridView.InvalidateCell(base.ColumnIndex, e.RowIndex);
			}
			base.OnMouseDown(e);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x0009EF24 File Offset: 0x0009D124
		protected override void OnMouseLeave(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (DataGridViewLinkCell.dataGridViewCursor != null)
			{
				base.DataGridView.Cursor = DataGridViewLinkCell.dataGridViewCursor;
				DataGridViewLinkCell.dataGridViewCursor = null;
			}
			if (this.LinkState != LinkState.Normal)
			{
				this.LinkState = LinkState.Normal;
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
			base.OnMouseLeave(rowIndex);
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0009EF88 File Offset: 0x0009D188
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex))
			{
				if ((this.LinkState & LinkState.Hover) == LinkState.Normal)
				{
					this.LinkState |= LinkState.Hover;
					base.DataGridView.InvalidateCell(base.ColumnIndex, e.RowIndex);
				}
				if (DataGridViewLinkCell.dataGridViewCursor == null)
				{
					DataGridViewLinkCell.dataGridViewCursor = base.DataGridView.UserSetCursor;
				}
				if (base.DataGridView.Cursor != Cursors.Hand)
				{
					base.DataGridView.Cursor = Cursors.Hand;
				}
			}
			else if ((this.LinkState & LinkState.Hover) != LinkState.Normal)
			{
				this.LinkState &= (LinkState)(-2);
				base.DataGridView.Cursor = DataGridViewLinkCell.dataGridViewCursor;
				base.DataGridView.InvalidateCell(base.ColumnIndex, e.RowIndex);
			}
			base.OnMouseMove(e);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x0009F074 File Offset: 0x0009D274
		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex) && this.TrackVisitedState)
			{
				this.LinkVisited = true;
			}
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x0009F0A8 File Offset: 0x0009D2A8
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0009F0E0 File Offset: 0x0009D2E0
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle result = Rectangle.Empty;
			Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
			Rectangle rectangle2 = cellBounds;
			rectangle2.Offset(rectangle.X, rectangle.Y);
			rectangle2.Width -= rectangle.Right;
			rectangle2.Height -= rectangle.Bottom;
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			bool flag = currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex;
			bool flag2 = (cellState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag2) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
			if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
			{
				g.FillRectangle(cachedBrush, rectangle2);
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
			Rectangle rectangle3 = rectangle2;
			string text = formattedValue as string;
			if (text != null && (paint || computeContentBounds))
			{
				rectangle2.Offset(1, 1);
				rectangle2.Width -= 3;
				rectangle2.Height -= 2;
				if ((cellStyle.Alignment & DataGridViewLinkCell.anyBottom) != DataGridViewContentAlignment.NotSet)
				{
					rectangle2.Height--;
				}
				Font font = null;
				Font font2 = null;
				bool isActive = (this.LinkState & LinkState.Active) == LinkState.Active;
				LinkUtilities.EnsureLinkFontsInternal(cellStyle.Font, this.LinkBehavior, ref font, ref font2, isActive);
				TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
				if (paint)
				{
					if (rectangle2.Width > 0 && rectangle2.Height > 0)
					{
						if (flag && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && DataGridViewCell.PaintFocus(paintParts))
						{
							Rectangle textBounds = DataGridViewUtilities.GetTextBounds(rectangle2, text, textFormatFlags, cellStyle, (this.LinkState == LinkState.Hover) ? font2 : font);
							if ((cellStyle.Alignment & DataGridViewLinkCell.anyLeft) != DataGridViewContentAlignment.NotSet)
							{
								int num = textBounds.X;
								textBounds.X = num - 1;
								num = textBounds.Width;
								textBounds.Width = num + 1;
							}
							else if ((cellStyle.Alignment & DataGridViewLinkCell.anyRight) != DataGridViewContentAlignment.NotSet)
							{
								int num = textBounds.X;
								textBounds.X = num + 1;
								num = textBounds.Width;
								textBounds.Width = num + 1;
							}
							textBounds.Height += 2;
							ControlPaint.DrawFocusRectangle(g, textBounds, Color.Empty, cachedBrush.Color);
						}
						Color foreColor;
						if ((this.LinkState & LinkState.Active) == LinkState.Active)
						{
							foreColor = this.ActiveLinkColor;
						}
						else if (this.LinkVisited)
						{
							foreColor = this.VisitedLinkColor;
						}
						else
						{
							foreColor = this.LinkColor;
						}
						if (DataGridViewCell.PaintContentForeground(paintParts))
						{
							if ((textFormatFlags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
							{
								textFormatFlags |= TextFormatFlags.EndEllipsis;
							}
							TextRenderer.DrawText(g, text, (this.LinkState == LinkState.Hover) ? font2 : font, rectangle2, foreColor, textFormatFlags);
						}
					}
					else if (flag && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && DataGridViewCell.PaintFocus(paintParts) && rectangle3.Width > 0 && rectangle3.Height > 0)
					{
						ControlPaint.DrawFocusRectangle(g, rectangle3, Color.Empty, cachedBrush.Color);
					}
				}
				else
				{
					result = DataGridViewUtilities.GetTextBounds(rectangle2, text, textFormatFlags, cellStyle, (this.LinkState == LinkState.Hover) ? font2 : font);
				}
				font.Dispose();
				font2.Dispose();
			}
			else if (paint || computeContentBounds)
			{
				if (flag && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && DataGridViewCell.PaintFocus(paintParts) && paint && rectangle2.Width > 0 && rectangle2.Height > 0)
				{
					ControlPaint.DrawFocusRectangle(g, rectangle2, Color.Empty, cachedBrush.Color);
				}
			}
			else if (computeErrorIconBounds && !string.IsNullOrEmpty(errorText))
			{
				result = base.ComputeErrorIconBounds(rectangle3);
			}
			if (base.DataGridView.ShowCellErrors && paint && DataGridViewCell.PaintErrorIcon(paintParts))
			{
				base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, rectangle3, errorText);
			}
			return result;
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0009F5C4 File Offset: 0x0009D7C4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewLinkCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x04000E05 RID: 3589
		private static readonly DataGridViewContentAlignment anyLeft = (DataGridViewContentAlignment)273;

		// Token: 0x04000E06 RID: 3590
		private static readonly DataGridViewContentAlignment anyRight = (DataGridViewContentAlignment)1092;

		// Token: 0x04000E07 RID: 3591
		private static readonly DataGridViewContentAlignment anyBottom = (DataGridViewContentAlignment)1792;

		// Token: 0x04000E08 RID: 3592
		private static Type defaultFormattedValueType = typeof(string);

		// Token: 0x04000E09 RID: 3593
		private static Type defaultValueType = typeof(object);

		// Token: 0x04000E0A RID: 3594
		private static Type cellType = typeof(DataGridViewLinkCell);

		// Token: 0x04000E0B RID: 3595
		private static readonly int PropLinkCellActiveLinkColor = PropertyStore.CreateKey();

		// Token: 0x04000E0C RID: 3596
		private static readonly int PropLinkCellLinkBehavior = PropertyStore.CreateKey();

		// Token: 0x04000E0D RID: 3597
		private static readonly int PropLinkCellLinkColor = PropertyStore.CreateKey();

		// Token: 0x04000E0E RID: 3598
		private static readonly int PropLinkCellLinkState = PropertyStore.CreateKey();

		// Token: 0x04000E0F RID: 3599
		private static readonly int PropLinkCellTrackVisitedState = PropertyStore.CreateKey();

		// Token: 0x04000E10 RID: 3600
		private static readonly int PropLinkCellUseColumnTextForLinkValue = PropertyStore.CreateKey();

		// Token: 0x04000E11 RID: 3601
		private static readonly int PropLinkCellVisitedLinkColor = PropertyStore.CreateKey();

		// Token: 0x04000E12 RID: 3602
		private const byte DATAGRIDVIEWLINKCELL_horizontalTextMarginLeft = 1;

		// Token: 0x04000E13 RID: 3603
		private const byte DATAGRIDVIEWLINKCELL_horizontalTextMarginRight = 2;

		// Token: 0x04000E14 RID: 3604
		private const byte DATAGRIDVIEWLINKCELL_verticalTextMarginTop = 1;

		// Token: 0x04000E15 RID: 3605
		private const byte DATAGRIDVIEWLINKCELL_verticalTextMarginBottom = 1;

		// Token: 0x04000E16 RID: 3606
		private bool linkVisited;

		// Token: 0x04000E17 RID: 3607
		private bool linkVisitedSet;

		// Token: 0x04000E18 RID: 3608
		private static Cursor dataGridViewCursor = null;

		// Token: 0x02000674 RID: 1652
		protected class DataGridViewLinkCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			// Token: 0x06006688 RID: 26248 RVA: 0x0017C895 File Offset: 0x0017AA95
			public DataGridViewLinkCellAccessibleObject(DataGridViewCell owner) : base(owner)
			{
			}

			// Token: 0x1700164A RID: 5706
			// (get) Token: 0x06006689 RID: 26249 RVA: 0x0017F152 File Offset: 0x0017D352
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("DataGridView_AccLinkCellDefaultAction");
				}
			}

			// Token: 0x0600668A RID: 26250 RVA: 0x0017F160 File Offset: 0x0017D360
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (base.IsOwnerCellDestroyed())
				{
					return;
				}
				DataGridViewLinkCell dataGridViewLinkCell = (DataGridViewLinkCell)base.Owner;
				DataGridView dataGridView = dataGridViewLinkCell.DataGridView;
				if (dataGridView != null && dataGridViewLinkCell.RowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
				}
				if (dataGridViewLinkCell.OwningColumn != null && dataGridViewLinkCell.OwningRow != null)
				{
					dataGridView.OnCellContentClickInternal(new DataGridViewCellEventArgs(dataGridViewLinkCell.ColumnIndex, dataGridViewLinkCell.RowIndex));
				}
			}

			// Token: 0x0600668B RID: 26251 RVA: 0x00011A20 File Offset: 0x0000FC20
			public override int GetChildCount()
			{
				return 0;
			}

			// Token: 0x0600668C RID: 26252 RVA: 0x0017C92F File Offset: 0x0017AB2F
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerCellDestroyed() && (AccessibilityImprovements.Level2 || base.IsIAccessibleExSupported());
			}

			// Token: 0x0600668D RID: 26253 RVA: 0x0017F1CC File Offset: 0x0017D3CC
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50005;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}
