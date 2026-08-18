using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x020001C8 RID: 456
	public class DataGridViewComboBoxCell : DataGridViewCell
	{
		// Token: 0x06001FC8 RID: 8136 RVA: 0x00097770 File Offset: 0x00095970
		public DataGridViewComboBoxCell()
		{
			this.flags = 8;
			if (!DataGridViewComboBoxCell.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					DataGridViewComboBoxCell.offset2X = DpiHelper.LogicalToDeviceUnitsX(DataGridViewComboBoxCell.OFFSET_2PIXELS);
					DataGridViewComboBoxCell.offset2Y = DpiHelper.LogicalToDeviceUnitsY(DataGridViewComboBoxCell.OFFSET_2PIXELS);
					DataGridViewComboBoxCell.nonXPTriangleWidth = (byte)DpiHelper.LogicalToDeviceUnitsX(7);
					DataGridViewComboBoxCell.nonXPTriangleHeight = (byte)DpiHelper.LogicalToDeviceUnitsY(4);
				}
				DataGridViewComboBoxCell.isScalingInitialized = true;
			}
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000977D4 File Offset: 0x000959D4
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level2)
			{
				return new DataGridViewComboBoxCell.DataGridViewComboBoxCellAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x000977EA File Offset: 0x000959EA
		// (set) Token: 0x06001FCB RID: 8139 RVA: 0x000977F8 File Offset: 0x000959F8
		[DefaultValue(true)]
		public virtual bool AutoComplete
		{
			get
			{
				return (this.flags & 8) > 0;
			}
			set
			{
				if (value != this.AutoComplete)
				{
					if (value)
					{
						this.flags |= 8;
					}
					else
					{
						this.flags = (byte)((int)this.flags & -9);
					}
					if (this.OwnsEditingComboBox(base.RowIndex))
					{
						if (value)
						{
							this.EditingComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
							this.EditingComboBox.AutoCompleteMode = AutoCompleteMode.Append;
							return;
						}
						this.EditingComboBox.AutoCompleteMode = AutoCompleteMode.None;
						this.EditingComboBox.AutoCompleteSource = AutoCompleteSource.None;
					}
				}
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0009787C File Offset: 0x00095A7C
		// (set) Token: 0x06001FCD RID: 8141 RVA: 0x0009788A File Offset: 0x00095A8A
		private CurrencyManager DataManager
		{
			get
			{
				return this.GetDataManager(base.DataGridView);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellDataManager))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellDataManager, value);
				}
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x000978B2 File Offset: 0x00095AB2
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x000978C4 File Offset: 0x00095AC4
		public virtual object DataSource
		{
			get
			{
				return base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellDataSource);
			}
			set
			{
				if (value != null && !(value is IList) && !(value is IListSource))
				{
					throw new ArgumentException(SR.GetString("BadDataSourceForComplexBinding"));
				}
				if (this.DataSource != value)
				{
					this.DataManager = null;
					this.UnwireDataSource();
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellDataSource, value);
					this.WireDataSource(value);
					this.CreateItemsFromDataSource = true;
					DataGridViewComboBoxCell.cachedDropDownWidth = -1;
					try
					{
						this.InitializeDisplayMemberPropertyDescriptor(this.DisplayMember);
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
						this.DisplayMemberInternal = null;
					}
					try
					{
						this.InitializeValueMemberPropertyDescriptor(this.ValueMember);
					}
					catch (Exception ex2)
					{
						if (ClientUtils.IsCriticalException(ex2))
						{
							throw;
						}
						this.ValueMemberInternal = null;
					}
					if (value == null)
					{
						this.DisplayMemberInternal = null;
						this.ValueMemberInternal = null;
					}
					if (this.OwnsEditingComboBox(base.RowIndex))
					{
						this.EditingComboBox.DataSource = value;
						this.InitializeComboBoxText();
						return;
					}
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x000979CC File Offset: 0x00095BCC
		// (set) Token: 0x06001FD1 RID: 8145 RVA: 0x000979F9 File Offset: 0x00095BF9
		[DefaultValue("")]
		public virtual string DisplayMember
		{
			get
			{
				object @object = base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMember);
				if (@object == null)
				{
					return string.Empty;
				}
				return (string)@object;
			}
			set
			{
				this.DisplayMemberInternal = value;
				if (this.OwnsEditingComboBox(base.RowIndex))
				{
					this.EditingComboBox.DisplayMember = value;
					this.InitializeComboBoxText();
					return;
				}
				base.OnCommonChange();
			}
		}

		// Token: 0x17000721 RID: 1825
		// (set) Token: 0x06001FD2 RID: 8146 RVA: 0x00097A29 File Offset: 0x00095C29
		private string DisplayMemberInternal
		{
			set
			{
				this.InitializeDisplayMemberPropertyDescriptor(value);
				if ((value != null && value.Length > 0) || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMember))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMember, value);
				}
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x00097A61 File Offset: 0x00095C61
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x00097A78 File Offset: 0x00095C78
		private PropertyDescriptor DisplayMemberProperty
		{
			get
			{
				return (PropertyDescriptor)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMemberProp);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMemberProp))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellDisplayMemberProp, value);
				}
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x00097AA0 File Offset: 0x00095CA0
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x00097AC8 File Offset: 0x00095CC8
		[DefaultValue(DataGridViewComboBoxDisplayStyle.DropDownButton)]
		public DataGridViewComboBoxDisplayStyle DisplayStyle
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyle, out flag);
				if (flag)
				{
					return (DataGridViewComboBoxDisplayStyle)integer;
				}
				return DataGridViewComboBoxDisplayStyle.DropDownButton;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewComboBoxDisplayStyle));
				}
				if (value != this.DisplayStyle)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyle, (int)value);
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

		// Token: 0x17000724 RID: 1828
		// (set) Token: 0x06001FD7 RID: 8151 RVA: 0x00097B44 File Offset: 0x00095D44
		internal DataGridViewComboBoxDisplayStyle DisplayStyleInternal
		{
			set
			{
				if (value != this.DisplayStyle)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyle, (int)value);
				}
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x00097B60 File Offset: 0x00095D60
		// (set) Token: 0x06001FD9 RID: 8153 RVA: 0x00097B8C File Offset: 0x00095D8C
		[DefaultValue(false)]
		public bool DisplayStyleForCurrentCellOnly
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyleForCurrentCellOnly, out flag);
				return flag && integer != 0;
			}
			set
			{
				if (value != this.DisplayStyleForCurrentCellOnly)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyleForCurrentCellOnly, value ? 1 : 0);
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

		// Token: 0x17000726 RID: 1830
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x00097BE8 File Offset: 0x00095DE8
		internal bool DisplayStyleForCurrentCellOnlyInternal
		{
			set
			{
				if (value != this.DisplayStyleForCurrentCellOnly)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellDisplayStyleForCurrentCellOnly, value ? 1 : 0);
				}
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x00097C0A File Offset: 0x00095E0A
		private Type DisplayType
		{
			get
			{
				if (this.DisplayMemberProperty != null)
				{
					return this.DisplayMemberProperty.PropertyType;
				}
				if (this.ValueMemberProperty != null)
				{
					return this.ValueMemberProperty.PropertyType;
				}
				return DataGridViewComboBoxCell.defaultFormattedValueType;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x00097C39 File Offset: 0x00095E39
		private TypeConverter DisplayTypeConverter
		{
			get
			{
				if (base.DataGridView != null)
				{
					return base.DataGridView.GetCachedTypeConverter(this.DisplayType);
				}
				return TypeDescriptor.GetConverter(this.DisplayType);
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x00097C60 File Offset: 0x00095E60
		// (set) Token: 0x06001FDE RID: 8158 RVA: 0x00097C88 File Offset: 0x00095E88
		[DefaultValue(1)]
		public virtual int DropDownWidth
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewComboBoxCell.PropComboBoxCellDropDownWidth, out flag);
				if (!flag)
				{
					return 1;
				}
				return integer;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("DropDownWidth", value, SR.GetString("DataGridViewComboBoxCell_DropDownWidthOutOfRange", new object[]
					{
						1.ToString(CultureInfo.CurrentCulture)
					}));
				}
				base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellDropDownWidth, value);
				if (this.OwnsEditingComboBox(base.RowIndex))
				{
					this.EditingComboBox.DropDownWidth = value;
				}
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x00097CF6 File Offset: 0x00095EF6
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x00097D0D File Offset: 0x00095F0D
		private DataGridViewComboBoxEditingControl EditingComboBox
		{
			get
			{
				return (DataGridViewComboBoxEditingControl)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellEditingComboBox);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellEditingComboBox))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellEditingComboBox, value);
				}
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00097D35 File Offset: 0x00095F35
		public override Type EditType
		{
			get
			{
				return DataGridViewComboBoxCell.defaultEditType;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x00097D3C File Offset: 0x00095F3C
		// (set) Token: 0x06001FE3 RID: 8163 RVA: 0x00097D64 File Offset: 0x00095F64
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewComboBoxCell.PropComboBoxCellFlatStyle, out flag);
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
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellFlatStyle, (int)value);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x1700072D RID: 1837
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x00097DB7 File Offset: 0x00095FB7
		internal FlatStyle FlatStyleInternal
		{
			set
			{
				if (value != this.FlatStyle)
				{
					base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellFlatStyle, (int)value);
				}
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00097DD3 File Offset: 0x00095FD3
		public override Type FormattedValueType
		{
			get
			{
				return DataGridViewComboBoxCell.defaultFormattedValueType;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x00097DDA File Offset: 0x00095FDA
		internal bool HasItems
		{
			get
			{
				return base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellItems) && base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellItems) != null;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x00097E03 File Offset: 0x00096003
		[Browsable(false)]
		public virtual DataGridViewComboBoxCell.ObjectCollection Items
		{
			get
			{
				return this.GetItems(base.DataGridView);
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x00097E14 File Offset: 0x00096014
		// (set) Token: 0x06001FE9 RID: 8169 RVA: 0x00097E3C File Offset: 0x0009603C
		[DefaultValue(8)]
		public virtual int MaxDropDownItems
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewComboBoxCell.PropComboBoxCellMaxDropDownItems, out flag);
				if (flag)
				{
					return integer;
				}
				return 8;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("MaxDropDownItems", value, SR.GetString("DataGridViewComboBoxCell_MaxDropDownItemsOutOfRange", new object[]
					{
						1.ToString(CultureInfo.CurrentCulture),
						100.ToString(CultureInfo.CurrentCulture)
					}));
				}
				base.Properties.SetInteger(DataGridViewComboBoxCell.PropComboBoxCellMaxDropDownItems, value);
				if (this.OwnsEditingComboBox(base.RowIndex))
				{
					this.EditingComboBox.MaxDropDownItems = value;
				}
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x00097EC4 File Offset: 0x000960C4
		private bool PaintXPThemes
		{
			get
			{
				return this.FlatStyle != FlatStyle.Flat && this.FlatStyle != FlatStyle.Popup && base.DataGridView.ApplyVisualStylesToInnerCells;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x00097EF6 File Offset: 0x000960F6
		private static bool PostXPThemesExist
		{
			get
			{
				return VisualStyleRenderer.IsElementDefined(VisualStyleElement.ComboBox.ReadOnlyButton.Normal);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x00097F02 File Offset: 0x00096102
		// (set) Token: 0x06001FED RID: 8173 RVA: 0x00097F10 File Offset: 0x00096110
		[DefaultValue(false)]
		public virtual bool Sorted
		{
			get
			{
				return (this.flags & 2) > 0;
			}
			set
			{
				if (value != this.Sorted)
				{
					if (value)
					{
						if (this.DataSource != null)
						{
							throw new ArgumentException(SR.GetString("ComboBoxSortWithDataSource"));
						}
						this.Items.SortInternal();
						this.flags |= 2;
					}
					else
					{
						this.flags = (byte)((int)this.flags & -3);
					}
					if (this.OwnsEditingComboBox(base.RowIndex))
					{
						this.EditingComboBox.Sorted = value;
					}
				}
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x00097F89 File Offset: 0x00096189
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x00097FA0 File Offset: 0x000961A0
		internal DataGridViewComboBoxColumn TemplateComboBoxColumn
		{
			get
			{
				return (DataGridViewComboBoxColumn)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellColumnTemplate);
			}
			set
			{
				base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellColumnTemplate, value);
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00097FB4 File Offset: 0x000961B4
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x00097FE1 File Offset: 0x000961E1
		[DefaultValue("")]
		public virtual string ValueMember
		{
			get
			{
				object @object = base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellValueMember);
				if (@object == null)
				{
					return string.Empty;
				}
				return (string)@object;
			}
			set
			{
				this.ValueMemberInternal = value;
				if (this.OwnsEditingComboBox(base.RowIndex))
				{
					this.EditingComboBox.ValueMember = value;
					this.InitializeComboBoxText();
					return;
				}
				base.OnCommonChange();
			}
		}

		// Token: 0x17000737 RID: 1847
		// (set) Token: 0x06001FF2 RID: 8178 RVA: 0x00098011 File Offset: 0x00096211
		private string ValueMemberInternal
		{
			set
			{
				this.InitializeValueMemberPropertyDescriptor(value);
				if ((value != null && value.Length > 0) || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellValueMember))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellValueMember, value);
				}
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x00098049 File Offset: 0x00096249
		// (set) Token: 0x06001FF4 RID: 8180 RVA: 0x00098060 File Offset: 0x00096260
		private PropertyDescriptor ValueMemberProperty
		{
			get
			{
				return (PropertyDescriptor)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellValueMemberProp);
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(DataGridViewComboBoxCell.PropComboBoxCellValueMemberProp))
				{
					base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellValueMemberProp, value);
				}
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x00098088 File Offset: 0x00096288
		public override Type ValueType
		{
			get
			{
				if (this.ValueMemberProperty != null)
				{
					return this.ValueMemberProperty.PropertyType;
				}
				if (this.DisplayMemberProperty != null)
				{
					return this.DisplayMemberProperty.PropertyType;
				}
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				return DataGridViewComboBoxCell.defaultValueType;
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000980D4 File Offset: 0x000962D4
		internal override void CacheEditingControl()
		{
			this.EditingComboBox = (base.DataGridView.EditingControl as DataGridViewComboBoxEditingControl);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000980EC File Offset: 0x000962EC
		private void CheckDropDownList(int x, int y, int rowIndex)
		{
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
			DataGridViewAdvancedBorderStyle advancedBorderStyle = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, false, false, false, false);
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
			rectangle.X += inheritedStyle.Padding.Left;
			rectangle.Y += inheritedStyle.Padding.Top;
			rectangle.Width += inheritedStyle.Padding.Right;
			rectangle.Height += inheritedStyle.Padding.Bottom;
			Size size = this.GetSize(rowIndex);
			Size size2 = new Size(size.Width - rectangle.X - rectangle.Width, size.Height - rectangle.Y - rectangle.Height);
			int num;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				num = Math.Min(this.GetDropDownButtonHeight(graphics, inheritedStyle), size2.Height - 2);
			}
			int num2 = Math.Min(SystemInformation.HorizontalScrollBarThumbWidth, size2.Width - 6 - 1);
			if (num > 0 && num2 > 0 && y >= rectangle.Y + 1 && y <= rectangle.Y + 1 + num)
			{
				if (base.DataGridView.RightToLeftInternal)
				{
					if (x >= rectangle.X + 1 && x <= rectangle.X + num2 + 1)
					{
						this.EditingComboBox.DroppedDown = true;
						return;
					}
				}
				else if (x >= size.Width - rectangle.Width - num2 - 1 && x <= size.Width - rectangle.Width - 1)
				{
					this.EditingComboBox.DroppedDown = true;
				}
			}
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000982C8 File Offset: 0x000964C8
		private void CheckNoDataSource()
		{
			if (this.DataSource != null)
			{
				throw new ArgumentException(SR.GetString("DataSourceLocksItems"));
			}
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x000982E4 File Offset: 0x000964E4
		private void ComboBox_DropDown(object sender, EventArgs e)
		{
			ComboBox editingComboBox = this.EditingComboBox;
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn = base.OwningColumn as DataGridViewComboBoxColumn;
			if (dataGridViewComboBoxColumn != null)
			{
				DataGridViewAutoSizeColumnMode inheritedAutoSizeMode = dataGridViewComboBoxColumn.GetInheritedAutoSizeMode(base.DataGridView);
				if (inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.ColumnHeader && inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill && inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.None)
				{
					if (this.DropDownWidth == 1)
					{
						if (DataGridViewComboBoxCell.cachedDropDownWidth == -1)
						{
							int num = -1;
							if ((this.HasItems || this.CreateItemsFromDataSource) && this.Items.Count > 0)
							{
								foreach (object item in this.Items)
								{
									Size size = TextRenderer.MeasureText(editingComboBox.GetItemText(item), editingComboBox.Font);
									if (size.Width > num)
									{
										num = size.Width;
									}
								}
							}
							DataGridViewComboBoxCell.cachedDropDownWidth = num + 2 + SystemInformation.VerticalScrollBarWidth;
						}
						UnsafeNativeMethods.SendMessage(new HandleRef(editingComboBox, editingComboBox.Handle), 352, DataGridViewComboBoxCell.cachedDropDownWidth, 0);
						return;
					}
				}
				else
				{
					int num2 = (int)((long)UnsafeNativeMethods.SendMessage(new HandleRef(editingComboBox, editingComboBox.Handle), 351, 0, 0));
					if (num2 != this.DropDownWidth)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(editingComboBox, editingComboBox.Handle), 352, this.DropDownWidth, 0);
					}
				}
			}
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00098448 File Offset: 0x00096648
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewComboBoxCell dataGridViewComboBoxCell;
			if (type == DataGridViewComboBoxCell.cellType)
			{
				dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
			}
			else
			{
				dataGridViewComboBoxCell = (DataGridViewComboBoxCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewComboBoxCell);
			dataGridViewComboBoxCell.DropDownWidth = this.DropDownWidth;
			dataGridViewComboBoxCell.MaxDropDownItems = this.MaxDropDownItems;
			dataGridViewComboBoxCell.CreateItemsFromDataSource = false;
			dataGridViewComboBoxCell.DataSource = this.DataSource;
			dataGridViewComboBoxCell.DisplayMember = this.DisplayMember;
			dataGridViewComboBoxCell.ValueMember = this.ValueMember;
			if (this.HasItems && this.DataSource == null && this.Items.Count > 0)
			{
				dataGridViewComboBoxCell.Items.AddRangeInternal(this.Items.InnerArray.ToArray());
			}
			dataGridViewComboBoxCell.AutoComplete = this.AutoComplete;
			dataGridViewComboBoxCell.Sorted = this.Sorted;
			dataGridViewComboBoxCell.FlatStyleInternal = this.FlatStyle;
			dataGridViewComboBoxCell.DisplayStyleInternal = this.DisplayStyle;
			dataGridViewComboBoxCell.DisplayStyleForCurrentCellOnlyInternal = this.DisplayStyleForCurrentCellOnly;
			return dataGridViewComboBoxCell;
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x0009853D File Offset: 0x0009673D
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x0009854A File Offset: 0x0009674A
		private bool CreateItemsFromDataSource
		{
			get
			{
				return (this.flags & 4) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 4;
					return;
				}
				this.flags = (byte)((int)this.flags & -5);
			}
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0009856F File Offset: 0x0009676F
		private void DataSource_Disposed(object sender, EventArgs e)
		{
			this.DataSource = null;
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00098578 File Offset: 0x00096778
		private void DataSource_Initialized(object sender, EventArgs e)
		{
			ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null)
			{
				supportInitializeNotification.Initialized -= this.DataSource_Initialized;
			}
			this.flags = (byte)((int)this.flags & -17);
			this.InitializeDisplayMemberPropertyDescriptor(this.DisplayMember);
			this.InitializeValueMemberPropertyDescriptor(this.ValueMember);
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000985D0 File Offset: 0x000967D0
		public override void DetachEditingControl()
		{
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView == null || dataGridView.EditingControl == null)
			{
				throw new InvalidOperationException();
			}
			if (this.EditingComboBox != null && (this.flags & 32) != 0)
			{
				this.EditingComboBox.DropDown -= this.ComboBox_DropDown;
				this.flags = (byte)((int)this.flags & -33);
			}
			this.EditingComboBox = null;
			base.DetachEditingControl();
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x0009863C File Offset: 0x0009683C
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
			object editedFormattedValue = base.GetEditedFormattedValue(value, rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates elementState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out elementState, out rectangle);
			Rectangle rectangle2;
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, elementState, editedFormattedValue, null, cellStyle, advancedBorderStyle, out rectangle2, DataGridViewPaintParts.ContentForeground, true, false, false, false);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000986B0 File Offset: 0x000968B0
		private CurrencyManager GetDataManager(DataGridView dataGridView)
		{
			CurrencyManager currencyManager = (CurrencyManager)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellDataManager);
			if (currencyManager == null && this.DataSource != null && dataGridView != null && dataGridView.BindingContext != null && this.DataSource != Convert.DBNull)
			{
				ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
				if (supportInitializeNotification != null && !supportInitializeNotification.IsInitialized)
				{
					if ((this.flags & 16) == 0)
					{
						supportInitializeNotification.Initialized += this.DataSource_Initialized;
						this.flags |= 16;
					}
				}
				else
				{
					currencyManager = (CurrencyManager)dataGridView.BindingContext[this.DataSource];
					this.DataManager = currencyManager;
				}
			}
			return currencyManager;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00098760 File Offset: 0x00096960
		private int GetDropDownButtonHeight(Graphics graphics, DataGridViewCellStyle cellStyle)
		{
			int num = 4;
			if (this.PaintXPThemes)
			{
				if (DataGridViewComboBoxCell.PostXPThemesExist)
				{
					num = 8;
				}
				else
				{
					num = 6;
				}
			}
			return DataGridViewCell.MeasureTextHeight(graphics, " ", cellStyle.Font, int.MaxValue, TextFormatFlags.Default) + num;
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000987A0 File Offset: 0x000969A0
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
			object editedFormattedValue = base.GetEditedFormattedValue(value, rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle advancedBorderStyle;
			DataGridViewElementStates elementState;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out advancedBorderStyle, out elementState, out rectangle);
			Rectangle rectangle2;
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, elementState, editedFormattedValue, this.GetErrorText(rowIndex), cellStyle, advancedBorderStyle, out rectangle2, DataGridViewPaintParts.ContentForeground, false, true, false, false);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00098834 File Offset: 0x00096A34
		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if (valueTypeConverter == null)
			{
				if (this.ValueMemberProperty != null)
				{
					valueTypeConverter = this.ValueMemberProperty.Converter;
				}
				else if (this.DisplayMemberProperty != null)
				{
					valueTypeConverter = this.DisplayMemberProperty.Converter;
				}
			}
			if (value == null || (this.ValueType != null && !this.ValueType.IsAssignableFrom(value.GetType()) && value != DBNull.Value))
			{
				if (value == null)
				{
					return base.GetFormattedValue(null, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
				}
				if (base.DataGridView != null)
				{
					DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs = new DataGridViewDataErrorEventArgs(new FormatException(SR.GetString("DataGridViewComboBoxCell_InvalidValue")), base.ColumnIndex, rowIndex, context);
					base.RaiseDataError(dataGridViewDataErrorEventArgs);
					if (dataGridViewDataErrorEventArgs.ThrowException)
					{
						throw dataGridViewDataErrorEventArgs.Exception;
					}
				}
				return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
			}
			else
			{
				string text = value as string;
				if ((this.DataManager != null && (this.ValueMemberProperty != null || this.DisplayMemberProperty != null)) || !string.IsNullOrEmpty(this.ValueMember) || !string.IsNullOrEmpty(this.DisplayMember))
				{
					object value2;
					if (!this.LookupDisplayValue(rowIndex, value, out value2))
					{
						if (value == DBNull.Value)
						{
							value2 = DBNull.Value;
						}
						else if (text != null && string.IsNullOrEmpty(text) && this.DisplayType == typeof(string))
						{
							value2 = string.Empty;
						}
						else if (base.DataGridView != null)
						{
							DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs2 = new DataGridViewDataErrorEventArgs(new ArgumentException(SR.GetString("DataGridViewComboBoxCell_InvalidValue")), base.ColumnIndex, rowIndex, context);
							base.RaiseDataError(dataGridViewDataErrorEventArgs2);
							if (dataGridViewDataErrorEventArgs2.ThrowException)
							{
								throw dataGridViewDataErrorEventArgs2.Exception;
							}
							if (this.OwnsEditingComboBox(rowIndex))
							{
								((IDataGridViewEditingControl)this.EditingComboBox).EditingControlValueChanged = true;
								base.DataGridView.NotifyCurrentCellDirty(true);
							}
						}
					}
					return base.GetFormattedValue(value2, rowIndex, ref cellStyle, this.DisplayTypeConverter, formattedValueTypeConverter, context);
				}
				if (!this.Items.Contains(value) && value != DBNull.Value && (!(value is string) || !string.IsNullOrEmpty(text)))
				{
					if (base.DataGridView != null)
					{
						DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs3 = new DataGridViewDataErrorEventArgs(new ArgumentException(SR.GetString("DataGridViewComboBoxCell_InvalidValue")), base.ColumnIndex, rowIndex, context);
						base.RaiseDataError(dataGridViewDataErrorEventArgs3);
						if (dataGridViewDataErrorEventArgs3.ThrowException)
						{
							throw dataGridViewDataErrorEventArgs3.Exception;
						}
					}
					if (this.Items.Count > 0)
					{
						value = this.Items[0];
					}
					else
					{
						value = string.Empty;
					}
				}
				return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
			}
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00098A90 File Offset: 0x00096C90
		internal string GetItemDisplayText(object item)
		{
			object itemDisplayValue = this.GetItemDisplayValue(item);
			if (itemDisplayValue == null)
			{
				return string.Empty;
			}
			return Convert.ToString(itemDisplayValue, CultureInfo.CurrentCulture);
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00098ABC File Offset: 0x00096CBC
		internal object GetItemDisplayValue(object item)
		{
			bool flag = false;
			object result = null;
			if (this.DisplayMemberProperty != null)
			{
				result = this.DisplayMemberProperty.GetValue(item);
				flag = true;
			}
			else if (this.ValueMemberProperty != null)
			{
				result = this.ValueMemberProperty.GetValue(item);
				flag = true;
			}
			else if (!string.IsNullOrEmpty(this.DisplayMember))
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(item).Find(this.DisplayMember, true);
				if (propertyDescriptor != null)
				{
					result = propertyDescriptor.GetValue(item);
					flag = true;
				}
			}
			else if (!string.IsNullOrEmpty(this.ValueMember))
			{
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(item).Find(this.ValueMember, true);
				if (propertyDescriptor2 != null)
				{
					result = propertyDescriptor2.GetValue(item);
					flag = true;
				}
			}
			if (!flag)
			{
				result = item;
			}
			return result;
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00098B64 File Offset: 0x00096D64
		internal DataGridViewComboBoxCell.ObjectCollection GetItems(DataGridView dataGridView)
		{
			DataGridViewComboBoxCell.ObjectCollection objectCollection = (DataGridViewComboBoxCell.ObjectCollection)base.Properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellItems);
			if (objectCollection == null)
			{
				objectCollection = new DataGridViewComboBoxCell.ObjectCollection(this);
				base.Properties.SetObject(DataGridViewComboBoxCell.PropComboBoxCellItems, objectCollection);
			}
			if (this.CreateItemsFromDataSource)
			{
				objectCollection.ClearInternal();
				CurrencyManager dataManager = this.GetDataManager(dataGridView);
				if (dataManager != null && dataManager.Count != -1)
				{
					object[] array = new object[dataManager.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = dataManager[i];
					}
					objectCollection.AddRangeInternal(array);
				}
				if (dataManager != null || (this.flags & 16) == 0)
				{
					this.CreateItemsFromDataSource = false;
				}
			}
			return objectCollection;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00098C08 File Offset: 0x00096E08
		internal object GetItemValue(object item)
		{
			bool flag = false;
			object result = null;
			if (this.ValueMemberProperty != null)
			{
				result = this.ValueMemberProperty.GetValue(item);
				flag = true;
			}
			else if (this.DisplayMemberProperty != null)
			{
				result = this.DisplayMemberProperty.GetValue(item);
				flag = true;
			}
			else if (!string.IsNullOrEmpty(this.ValueMember))
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(item).Find(this.ValueMember, true);
				if (propertyDescriptor != null)
				{
					result = propertyDescriptor.GetValue(item);
					flag = true;
				}
			}
			if (!flag && !string.IsNullOrEmpty(this.DisplayMember))
			{
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(item).Find(this.DisplayMember, true);
				if (propertyDescriptor2 != null)
				{
					result = propertyDescriptor2.GetValue(item);
					flag = true;
				}
			}
			if (!flag)
			{
				result = item;
			}
			return result;
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00098CB0 File Offset: 0x00096EB0
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
			Size result = Size.Empty;
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			Rectangle stdBorderWidths = base.StdBorderWidths;
			int num = stdBorderWidths.Left + stdBorderWidths.Width + cellStyle.Padding.Horizontal;
			int num2 = stdBorderWidths.Top + stdBorderWidths.Height + cellStyle.Padding.Vertical;
			TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			string text = base.GetFormattedValue(rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize) as string;
			if (!string.IsNullOrEmpty(text))
			{
				result = DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags);
			}
			else
			{
				result = DataGridViewCell.MeasureTextSize(graphics, " ", cellStyle.Font, textFormatFlags);
			}
			if (freeDimensionFromConstraint == DataGridViewFreeDimension.Height)
			{
				result.Width = 0;
			}
			else if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
			{
				result.Height = 0;
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				result.Width += SystemInformation.HorizontalScrollBarThumbWidth + 1 + 6 + num;
				if (base.DataGridView.ShowCellErrors)
				{
					result.Width = Math.Max(result.Width, num + SystemInformation.HorizontalScrollBarThumbWidth + 1 + 8 + (int)DataGridViewCell.iconsWidth);
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				if (this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
				{
					result.Height += 6;
				}
				else
				{
					result.Height += 8;
				}
				result.Height += num2;
				if (base.DataGridView.ShowCellErrors)
				{
					result.Height = Math.Max(result.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return result;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x00098E68 File Offset: 0x00097068
		private void InitializeComboBoxText()
		{
			((IDataGridViewEditingControl)this.EditingComboBox).EditingControlValueChanged = false;
			int editingControlRowIndex = ((IDataGridViewEditingControl)this.EditingComboBox).EditingControlRowIndex;
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, editingControlRowIndex, false);
			this.EditingComboBox.Text = (string)this.GetFormattedValue(this.GetValue(editingControlRowIndex), editingControlRowIndex, ref inheritedStyle, null, null, DataGridViewDataErrorContexts.Formatting);
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x00098EBC File Offset: 0x000970BC
		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			ComboBox comboBox = base.DataGridView.EditingControl as ComboBox;
			if (comboBox != null)
			{
				if ((this.GetInheritedState(rowIndex) & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
				{
					base.DataGridView.EditingPanel.BackColor = dataGridViewCellStyle.SelectionBackColor;
				}
				IntPtr handle;
				if (comboBox.ParentInternal != null)
				{
					handle = comboBox.ParentInternal.Handle;
				}
				handle = comboBox.Handle;
				comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				comboBox.FormattingEnabled = true;
				comboBox.MaxDropDownItems = this.MaxDropDownItems;
				comboBox.DropDownWidth = this.DropDownWidth;
				comboBox.DataSource = null;
				comboBox.ValueMember = null;
				comboBox.Items.Clear();
				comboBox.DataSource = this.DataSource;
				comboBox.DisplayMember = this.DisplayMember;
				comboBox.ValueMember = this.ValueMember;
				if (this.HasItems && this.DataSource == null && this.Items.Count > 0)
				{
					comboBox.Items.AddRange(this.Items.InnerArray.ToArray());
				}
				comboBox.Sorted = this.Sorted;
				comboBox.FlatStyle = this.FlatStyle;
				if (this.AutoComplete)
				{
					comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
					comboBox.AutoCompleteMode = AutoCompleteMode.Append;
				}
				else
				{
					comboBox.AutoCompleteMode = AutoCompleteMode.None;
					comboBox.AutoCompleteSource = AutoCompleteSource.None;
				}
				string text = initialFormattedValue as string;
				if (text == null)
				{
					text = string.Empty;
				}
				comboBox.Text = text;
				if ((this.flags & 32) == 0)
				{
					comboBox.DropDown += this.ComboBox_DropDown;
					this.flags |= 32;
				}
				DataGridViewComboBoxCell.cachedDropDownWidth = -1;
				this.EditingComboBox = (base.DataGridView.EditingControl as DataGridViewComboBoxEditingControl);
				if (base.GetHeight(rowIndex) > 21)
				{
					Rectangle cellDisplayRectangle = base.DataGridView.GetCellDisplayRectangle(base.ColumnIndex, rowIndex, true);
					cellDisplayRectangle.Y += 21;
					cellDisplayRectangle.Height -= 21;
					base.DataGridView.Invalidate(cellDisplayRectangle);
				}
			}
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000990B8 File Offset: 0x000972B8
		private void InitializeDisplayMemberPropertyDescriptor(string displayMember)
		{
			if (this.DataManager != null)
			{
				if (string.IsNullOrEmpty(displayMember))
				{
					this.DisplayMemberProperty = null;
					return;
				}
				BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(displayMember);
				this.DataManager = (base.DataGridView.BindingContext[this.DataSource, bindingMemberInfo.BindingPath] as CurrencyManager);
				PropertyDescriptorCollection itemProperties = this.DataManager.GetItemProperties();
				PropertyDescriptor propertyDescriptor = itemProperties.Find(bindingMemberInfo.BindingField, true);
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("DataGridViewComboBoxCell_FieldNotFound", new object[]
					{
						displayMember
					}));
				}
				this.DisplayMemberProperty = propertyDescriptor;
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0009914C File Offset: 0x0009734C
		private void InitializeValueMemberPropertyDescriptor(string valueMember)
		{
			if (this.DataManager != null)
			{
				if (string.IsNullOrEmpty(valueMember))
				{
					this.ValueMemberProperty = null;
					return;
				}
				BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(valueMember);
				this.DataManager = (base.DataGridView.BindingContext[this.DataSource, bindingMemberInfo.BindingPath] as CurrencyManager);
				PropertyDescriptorCollection itemProperties = this.DataManager.GetItemProperties();
				PropertyDescriptor propertyDescriptor = itemProperties.Find(bindingMemberInfo.BindingField, true);
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("DataGridViewComboBoxCell_FieldNotFound", new object[]
					{
						valueMember
					}));
				}
				this.ValueMemberProperty = propertyDescriptor;
			}
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000991E0 File Offset: 0x000973E0
		private object ItemFromComboBoxDataSource(PropertyDescriptor property, object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			object result = null;
			if (this.DataManager.List is IBindingList && ((IBindingList)this.DataManager.List).SupportsSearching)
			{
				int num = ((IBindingList)this.DataManager.List).Find(property, key);
				if (num != -1)
				{
					result = this.DataManager.List[num];
				}
			}
			else
			{
				for (int i = 0; i < this.DataManager.List.Count; i++)
				{
					object obj = this.DataManager.List[i];
					object value = property.GetValue(obj);
					if (key.Equals(value))
					{
						result = obj;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0009929C File Offset: 0x0009749C
		private object ItemFromComboBoxItems(int rowIndex, string field, object key)
		{
			object obj = null;
			if (this.OwnsEditingComboBox(rowIndex))
			{
				obj = this.EditingComboBox.SelectedItem;
				object obj2 = null;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj).Find(field, true);
				if (propertyDescriptor != null)
				{
					obj2 = propertyDescriptor.GetValue(obj);
				}
				if (obj2 == null || !obj2.Equals(key))
				{
					obj = null;
				}
			}
			if (obj == null)
			{
				foreach (object obj3 in this.Items)
				{
					object obj4 = null;
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(obj3).Find(field, true);
					if (propertyDescriptor2 != null)
					{
						obj4 = propertyDescriptor2.GetValue(obj3);
					}
					if (obj4 != null && obj4.Equals(key))
					{
						obj = obj3;
						break;
					}
				}
			}
			if (obj == null)
			{
				if (this.OwnsEditingComboBox(rowIndex))
				{
					obj = this.EditingComboBox.SelectedItem;
					if (obj == null || !obj.Equals(key))
					{
						obj = null;
					}
				}
				if (obj == null && this.Items.Contains(key))
				{
					obj = key;
				}
			}
			return obj;
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x000993A0 File Offset: 0x000975A0
		public override bool KeyEntersEditMode(KeyEventArgs e)
		{
			return (((char.IsLetterOrDigit((char)e.KeyCode) && (e.KeyCode < Keys.F1 || e.KeyCode > Keys.F24)) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide) || (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.OemBackslash) || (e.KeyCode == Keys.Space && !e.Shift) || e.KeyCode == Keys.F4 || ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && e.Alt)) && (!e.Alt || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && !e.Control) || base.KeyEntersEditMode(e);
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00099468 File Offset: 0x00097668
		private bool LookupDisplayValue(int rowIndex, object value, out object displayValue)
		{
			object obj;
			if (this.DisplayMemberProperty != null || this.ValueMemberProperty != null)
			{
				obj = this.ItemFromComboBoxDataSource((this.ValueMemberProperty != null) ? this.ValueMemberProperty : this.DisplayMemberProperty, value);
			}
			else
			{
				obj = this.ItemFromComboBoxItems(rowIndex, string.IsNullOrEmpty(this.ValueMember) ? this.DisplayMember : this.ValueMember, value);
			}
			if (obj == null)
			{
				displayValue = null;
				return false;
			}
			displayValue = this.GetItemDisplayValue(obj);
			return true;
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000994E0 File Offset: 0x000976E0
		private bool LookupValue(object formattedValue, out object value)
		{
			if (formattedValue == null)
			{
				value = null;
				return true;
			}
			object obj;
			if (this.DisplayMemberProperty != null || this.ValueMemberProperty != null)
			{
				obj = this.ItemFromComboBoxDataSource((this.DisplayMemberProperty != null) ? this.DisplayMemberProperty : this.ValueMemberProperty, formattedValue);
			}
			else
			{
				obj = this.ItemFromComboBoxItems(base.RowIndex, string.IsNullOrEmpty(this.DisplayMember) ? this.ValueMember : this.DisplayMember, formattedValue);
			}
			if (obj == null)
			{
				value = null;
				return false;
			}
			value = this.GetItemValue(obj);
			return true;
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00099562 File Offset: 0x00097762
		protected override void OnDataGridViewChanged()
		{
			if (base.DataGridView != null)
			{
				this.InitializeDisplayMemberPropertyDescriptor(this.DisplayMember);
				this.InitializeValueMemberPropertyDescriptor(this.ValueMember);
			}
			base.OnDataGridViewChanged();
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0009958A File Offset: 0x0009778A
		protected override void OnEnter(int rowIndex, bool throughMouseClick)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (throughMouseClick && base.DataGridView.EditMode != DataGridViewEditMode.EditOnEnter)
			{
				this.flags |= 1;
			}
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x000995B4 File Offset: 0x000977B4
		private void OnItemsCollectionChanged()
		{
			if (this.TemplateComboBoxColumn != null)
			{
				this.TemplateComboBoxColumn.OnItemsCollectionChanged();
			}
			DataGridViewComboBoxCell.cachedDropDownWidth = -1;
			if (this.OwnsEditingComboBox(base.RowIndex))
			{
				this.InitializeComboBoxText();
				return;
			}
			base.OnCommonChange();
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x000995EA File Offset: 0x000977EA
		protected override void OnLeave(int rowIndex, bool throughMouseClick)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			this.flags = (byte)((int)this.flags & -2);
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x00099608 File Offset: 0x00097808
		protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (currentCellAddress.X == e.ColumnIndex && currentCellAddress.Y == e.RowIndex)
			{
				if ((this.flags & 1) != 0)
				{
					this.flags = (byte)((int)this.flags & -2);
					return;
				}
				if ((this.EditingComboBox == null || !this.EditingComboBox.DroppedDown) && base.DataGridView.EditMode != DataGridViewEditMode.EditProgrammatically && base.DataGridView.BeginEdit(true) && this.EditingComboBox != null && this.DisplayStyle != DataGridViewComboBoxDisplayStyle.Nothing)
				{
					this.CheckDropDownList(e.X, e.Y, e.RowIndex);
				}
			}
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x000996C0 File Offset: 0x000978C0
		protected override void OnMouseEnter(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (this.DisplayStyle == DataGridViewComboBoxDisplayStyle.ComboBox && this.FlatStyle == FlatStyle.Popup)
			{
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
			base.OnMouseEnter(rowIndex);
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x000996F8 File Offset: 0x000978F8
		protected override void OnMouseLeave(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (DataGridViewComboBoxCell.mouseInDropDownButtonBounds)
			{
				DataGridViewComboBoxCell.mouseInDropDownButtonBounds = false;
				if (base.ColumnIndex >= 0 && rowIndex >= 0 && (this.FlatStyle == FlatStyle.Standard || this.FlatStyle == FlatStyle.System) && base.DataGridView.ApplyVisualStylesToInnerCells)
				{
					base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
				}
			}
			if (this.DisplayStyle == DataGridViewComboBoxDisplayStyle.ComboBox && this.FlatStyle == FlatStyle.Popup)
			{
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
			base.OnMouseEnter(rowIndex);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00099784 File Offset: 0x00097984
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if ((this.FlatStyle == FlatStyle.Standard || this.FlatStyle == FlatStyle.System) && base.DataGridView.ApplyVisualStylesToInnerCells)
			{
				int rowIndex = e.RowIndex;
				DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
				bool singleVerticalBorderAdded = !base.DataGridView.RowHeadersVisible && base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single;
				bool singleHorizontalBorderAdded = !base.DataGridView.ColumnHeadersVisible && base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single;
				bool isFirstDisplayedColumn = rowIndex == base.DataGridView.FirstDisplayedScrollingRowIndex;
				bool isFirstDisplayedRow = base.OwningColumn.Index == base.DataGridView.FirstDisplayedColumnIndex;
				bool flag = base.OwningColumn.Index == base.DataGridView.FirstDisplayedScrollingColumnIndex;
				DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
				DataGridViewAdvancedBorderStyle advancedBorderStyle = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
				Rectangle cellDisplayRectangle = base.DataGridView.GetCellDisplayRectangle(base.OwningColumn.Index, rowIndex, false);
				if (flag)
				{
					cellDisplayRectangle.X -= base.DataGridView.FirstDisplayedScrollingColumnHiddenWidth;
					cellDisplayRectangle.Width += base.DataGridView.FirstDisplayedScrollingColumnHiddenWidth;
				}
				DataGridViewElementStates rowState = base.DataGridView.Rows.GetRowState(rowIndex);
				DataGridViewElementStates dataGridViewElementStates = base.CellStateFromColumnRowStates(rowState);
				dataGridViewElementStates |= this.State;
				Rectangle rectangle;
				using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
				{
					this.PaintPrivate(graphics, cellDisplayRectangle, cellDisplayRectangle, rowIndex, dataGridViewElementStates, null, null, inheritedStyle, advancedBorderStyle, out rectangle, DataGridViewPaintParts.ContentForeground, false, false, true, false);
				}
				bool flag2 = rectangle.Contains(base.DataGridView.PointToClient(Control.MousePosition));
				if (flag2 != DataGridViewComboBoxCell.mouseInDropDownButtonBounds)
				{
					DataGridViewComboBoxCell.mouseInDropDownButtonBounds = flag2;
					base.DataGridView.InvalidateCell(e.ColumnIndex, rowIndex);
				}
			}
			base.OnMouseMove(e);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00099980 File Offset: 0x00097B80
		private bool OwnsEditingComboBox(int rowIndex)
		{
			return rowIndex != -1 && this.EditingComboBox != null && rowIndex == ((IDataGridViewEditingControl)this.EditingComboBox).EditingControlRowIndex;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000999A0 File Offset: 0x00097BA0
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			Rectangle rectangle;
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, elementState, formattedValue, errorText, cellStyle, advancedBorderStyle, out rectangle, paintParts, false, false, false, true);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x000999DC File Offset: 0x00097BDC
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, out Rectangle dropDownButtonRect, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool computeDropDownButtonRect, bool paint)
		{
			Rectangle result = Rectangle.Empty;
			dropDownButtonRect = Rectangle.Empty;
			bool flag = this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup;
			bool flag2 = this.FlatStyle == FlatStyle.Popup && base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex;
			bool flag3 = !flag && base.DataGridView.ApplyVisualStylesToInnerCells;
			bool flag4 = flag3 && DataGridViewComboBoxCell.PostXPThemesExist;
			ComboBoxState state = ComboBoxState.Normal;
			if (base.DataGridView.MouseEnteredCellAddress.Y == rowIndex && base.DataGridView.MouseEnteredCellAddress.X == base.ColumnIndex && DataGridViewComboBoxCell.mouseInDropDownButtonBounds)
			{
				state = ComboBoxState.Hot;
			}
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
			Rectangle rectangle2 = cellBounds;
			rectangle2.Offset(rectangle.X, rectangle.Y);
			rectangle2.Width -= rectangle.Right;
			rectangle2.Height -= rectangle.Bottom;
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			bool flag5 = currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex;
			bool flag6 = flag5 && base.DataGridView.EditingControl != null;
			bool flag7 = (elementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			bool flag8 = this.DisplayStyle == DataGridViewComboBoxDisplayStyle.ComboBox && ((this.DisplayStyleForCurrentCellOnly && flag5) || !this.DisplayStyleForCurrentCellOnly);
			bool flag9 = this.DisplayStyle != DataGridViewComboBoxDisplayStyle.Nothing && ((this.DisplayStyleForCurrentCellOnly && flag5) || !this.DisplayStyleForCurrentCellOnly);
			SolidBrush cachedBrush;
			if (DataGridViewCell.PaintSelectionBackground(paintParts) && flag7 && !flag6)
			{
				cachedBrush = base.DataGridView.GetCachedBrush(cellStyle.SelectionBackColor);
			}
			else
			{
				cachedBrush = base.DataGridView.GetCachedBrush(cellStyle.BackColor);
			}
			if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255 && rectangle2.Width > 0 && rectangle2.Height > 0)
			{
				DataGridViewCell.PaintPadding(g, rectangle2, cellStyle, cachedBrush, base.DataGridView.RightToLeftInternal);
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
			if (paint && rectangle2.Width > 0 && rectangle2.Height > 0)
			{
				if (flag3 && flag8)
				{
					if (flag4 && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
					{
						g.FillRectangle(cachedBrush, rectangle2.Left, rectangle2.Top, rectangle2.Width, rectangle2.Height);
					}
					if (DataGridViewCell.PaintContentBackground(paintParts))
					{
						if (flag4)
						{
							DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawBorder(g, rectangle2);
						}
						else
						{
							DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawTextBox(g, rectangle2, state);
						}
					}
					if (!flag4 && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255 && rectangle2.Width > 2 && rectangle2.Height > 2)
					{
						g.FillRectangle(cachedBrush, rectangle2.Left + 1, rectangle2.Top + 1, rectangle2.Width - 2, rectangle2.Height - 2);
					}
				}
				else if (DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
				{
					if (flag4 && flag9 && !flag8)
					{
						g.DrawRectangle(SystemPens.ControlLightLight, new Rectangle(rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1));
					}
					else
					{
						g.FillRectangle(cachedBrush, rectangle2.Left, rectangle2.Top, rectangle2.Width, rectangle2.Height);
					}
				}
			}
			int num = Math.Min(SystemInformation.HorizontalScrollBarThumbWidth, rectangle2.Width - 6 - 1);
			if (!flag6)
			{
				int num2;
				if (flag3 || flag)
				{
					num2 = Math.Min(this.GetDropDownButtonHeight(g, cellStyle), flag4 ? rectangle2.Height : (rectangle2.Height - 2));
				}
				else
				{
					num2 = Math.Min(this.GetDropDownButtonHeight(g, cellStyle), rectangle2.Height - 4);
				}
				if (num > 0 && num2 > 0)
				{
					Rectangle rectangle3;
					if (flag3 || flag)
					{
						if (flag4)
						{
							rectangle3 = new Rectangle(base.DataGridView.RightToLeftInternal ? rectangle2.Left : (rectangle2.Right - num), rectangle2.Top, num, num2);
						}
						else
						{
							rectangle3 = new Rectangle(base.DataGridView.RightToLeftInternal ? (rectangle2.Left + 1) : (rectangle2.Right - num - 1), rectangle2.Top + 1, num, num2);
						}
					}
					else
					{
						rectangle3 = new Rectangle(base.DataGridView.RightToLeftInternal ? (rectangle2.Left + 2) : (rectangle2.Right - num - 2), rectangle2.Top + 2, num, num2);
					}
					if (flag4 && flag9 && !flag8)
					{
						dropDownButtonRect = rectangle2;
					}
					else
					{
						dropDownButtonRect = rectangle3;
					}
					if (paint && DataGridViewCell.PaintContentBackground(paintParts))
					{
						if (flag9)
						{
							if (flag)
							{
								g.FillRectangle(SystemBrushes.Control, rectangle3);
							}
							else if (flag3)
							{
								if (flag4)
								{
									if (flag8)
									{
										DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawDropDownButton(g, rectangle3, state, base.DataGridView.RightToLeftInternal);
									}
									else
									{
										DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawReadOnlyButton(g, rectangle2, state);
										DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawDropDownButton(g, rectangle3, ComboBoxState.Normal);
									}
									if (SystemInformation.HighContrast && AccessibilityImprovements.Level1)
									{
										cachedBrush = base.DataGridView.GetCachedBrush(cellStyle.BackColor);
									}
								}
								else
								{
									DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.DrawDropDownButton(g, rectangle3, state);
								}
							}
							else
							{
								g.FillRectangle(SystemBrushes.Control, rectangle3);
							}
						}
						if (!flag && !flag3 && (flag8 || flag9))
						{
							Color control = SystemColors.Control;
							Color color = control;
							bool flag10 = control.ToKnownColor() == SystemColors.Control.ToKnownColor();
							bool highContrast = SystemInformation.HighContrast;
							Color color2;
							Color color3;
							Color color4;
							if (control == SystemColors.Control)
							{
								color2 = SystemColors.ControlDark;
								color3 = SystemColors.ControlDarkDark;
								color4 = SystemColors.ControlLightLight;
							}
							else
							{
								color2 = ControlPaint.Dark(control);
								color4 = ControlPaint.LightLight(control);
								if (highContrast)
								{
									color3 = ControlPaint.LightLight(control);
								}
								else
								{
									color3 = ControlPaint.DarkDark(control);
								}
							}
							color2 = g.GetNearestColor(color2);
							color3 = g.GetNearestColor(color3);
							color = g.GetNearestColor(color);
							color4 = g.GetNearestColor(color4);
							Pen pen;
							if (flag10)
							{
								if (SystemInformation.HighContrast)
								{
									pen = SystemPens.ControlLight;
								}
								else
								{
									pen = SystemPens.Control;
								}
							}
							else
							{
								pen = new Pen(color4);
							}
							if (flag9)
							{
								g.DrawLine(pen, rectangle3.X, rectangle3.Y, rectangle3.X + rectangle3.Width - 1, rectangle3.Y);
								g.DrawLine(pen, rectangle3.X, rectangle3.Y, rectangle3.X, rectangle3.Y + rectangle3.Height - 1);
							}
							if (flag8)
							{
								g.DrawLine(pen, rectangle2.X, rectangle2.Y + rectangle2.Height - 1, rectangle2.X + rectangle2.Width - 1, rectangle2.Y + rectangle2.Height - 1);
								g.DrawLine(pen, rectangle2.X + rectangle2.Width - 1, rectangle2.Y, rectangle2.X + rectangle2.Width - 1, rectangle2.Y + rectangle2.Height - 1);
							}
							if (flag10)
							{
								pen = SystemPens.ControlDarkDark;
							}
							else
							{
								pen.Color = color3;
							}
							if (flag9)
							{
								g.DrawLine(pen, rectangle3.X, rectangle3.Y + rectangle3.Height - 1, rectangle3.X + rectangle3.Width - 1, rectangle3.Y + rectangle3.Height - 1);
								g.DrawLine(pen, rectangle3.X + rectangle3.Width - 1, rectangle3.Y, rectangle3.X + rectangle3.Width - 1, rectangle3.Y + rectangle3.Height - 1);
							}
							if (flag8)
							{
								g.DrawLine(pen, rectangle2.X, rectangle2.Y, rectangle2.X + rectangle2.Width - 2, rectangle2.Y);
								g.DrawLine(pen, rectangle2.X, rectangle2.Y, rectangle2.X, rectangle2.Y + rectangle2.Height - 1);
							}
							if (flag10)
							{
								pen = SystemPens.ControlLightLight;
							}
							else
							{
								pen.Color = color;
							}
							if (flag9)
							{
								g.DrawLine(pen, rectangle3.X + 1, rectangle3.Y + 1, rectangle3.X + rectangle3.Width - 2, rectangle3.Y + 1);
								g.DrawLine(pen, rectangle3.X + 1, rectangle3.Y + 1, rectangle3.X + 1, rectangle3.Y + rectangle3.Height - 2);
							}
							if (flag10)
							{
								pen = SystemPens.ControlDark;
							}
							else
							{
								pen.Color = color2;
							}
							if (flag9)
							{
								g.DrawLine(pen, rectangle3.X + 1, rectangle3.Y + rectangle3.Height - 2, rectangle3.X + rectangle3.Width - 2, rectangle3.Y + rectangle3.Height - 2);
								g.DrawLine(pen, rectangle3.X + rectangle3.Width - 2, rectangle3.Y + 1, rectangle3.X + rectangle3.Width - 2, rectangle3.Y + rectangle3.Height - 2);
							}
							if (!flag10)
							{
								pen.Dispose();
							}
						}
						if (num >= 5 && num2 >= 3 && flag9)
						{
							if (flag)
							{
								Point point = new Point(rectangle3.Left + rectangle3.Width / 2, rectangle3.Top + rectangle3.Height / 2);
								point.X += rectangle3.Width % 2;
								point.Y += rectangle3.Height % 2;
								g.FillPolygon(SystemBrushes.ControlText, new Point[]
								{
									new Point(point.X - DataGridViewComboBoxCell.offset2X, point.Y - 1),
									new Point(point.X + DataGridViewComboBoxCell.offset2X + 1, point.Y - 1),
									new Point(point.X, point.Y + DataGridViewComboBoxCell.offset2Y)
								});
							}
							else if (!flag3)
							{
								int num3 = rectangle3.X;
								rectangle3.X = num3 - 1;
								num3 = rectangle3.Width;
								rectangle3.Width = num3 + 1;
								Point point2 = new Point(rectangle3.Left + (rectangle3.Width - 1) / 2, rectangle3.Top + (rectangle3.Height + (int)DataGridViewComboBoxCell.nonXPTriangleHeight) / 2);
								point2.X += (rectangle3.Width + 1) % 2;
								point2.Y += rectangle3.Height % 2;
								Point point3 = new Point(point2.X - (int)((DataGridViewComboBoxCell.nonXPTriangleWidth - 1) / 2), point2.Y - (int)DataGridViewComboBoxCell.nonXPTriangleHeight);
								Point point4 = new Point(point2.X + (int)((DataGridViewComboBoxCell.nonXPTriangleWidth - 1) / 2), point2.Y - (int)DataGridViewComboBoxCell.nonXPTriangleHeight);
								g.FillPolygon(SystemBrushes.ControlText, new Point[]
								{
									point3,
									point4,
									point2
								});
								g.DrawLine(SystemPens.ControlText, point3.X, point3.Y, point4.X, point4.Y);
								num3 = rectangle3.X;
								rectangle3.X = num3 + 1;
								num3 = rectangle3.Width;
								rectangle3.Width = num3 - 1;
							}
						}
						if (flag2 && flag8)
						{
							int num3 = rectangle3.Y;
							rectangle3.Y = num3 - 1;
							num3 = rectangle3.Height;
							rectangle3.Height = num3 + 1;
							g.DrawRectangle(SystemPens.ControlDark, rectangle3);
						}
					}
				}
			}
			Rectangle cellValueBounds = rectangle2;
			Rectangle rectangle4 = Rectangle.Inflate(rectangle2, -2, -2);
			if (flag4)
			{
				int num3;
				if (!base.DataGridView.RightToLeftInternal)
				{
					num3 = rectangle4.X;
					rectangle4.X = num3 - 1;
				}
				num3 = rectangle4.Width;
				rectangle4.Width = num3 + 1;
			}
			if (flag9)
			{
				if (flag3 || flag)
				{
					cellValueBounds.Width -= num;
					rectangle4.Width -= num;
					if (base.DataGridView.RightToLeftInternal)
					{
						cellValueBounds.X += num;
						rectangle4.X += num;
					}
				}
				else
				{
					cellValueBounds.Width -= num + 1;
					rectangle4.Width -= num + 1;
					if (base.DataGridView.RightToLeftInternal)
					{
						cellValueBounds.X += num + 1;
						rectangle4.X += num + 1;
					}
				}
			}
			if (rectangle4.Width > 1 && rectangle4.Height > 1)
			{
				if (flag5 && !flag6 && DataGridViewCell.PaintFocus(paintParts) && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && paint)
				{
					if (flag)
					{
						Rectangle rectangle5 = rectangle4;
						int num3;
						if (!base.DataGridView.RightToLeftInternal)
						{
							num3 = rectangle5.X;
							rectangle5.X = num3 - 1;
						}
						num3 = rectangle5.Width;
						rectangle5.Width = num3 + 1;
						num3 = rectangle5.Y;
						rectangle5.Y = num3 - 1;
						rectangle5.Height += 2;
						ControlPaint.DrawFocusRectangle(g, rectangle5, Color.Empty, cachedBrush.Color);
					}
					else if (flag4)
					{
						Rectangle rectangle6 = rectangle4;
						int num3 = rectangle6.X;
						rectangle6.X = num3 + 1;
						rectangle6.Width -= 2;
						num3 = rectangle6.Y;
						rectangle6.Y = num3 + 1;
						rectangle6.Height -= 2;
						if (rectangle6.Width > 0 && rectangle6.Height > 0)
						{
							ControlPaint.DrawFocusRectangle(g, rectangle6, Color.Empty, cachedBrush.Color);
						}
					}
					else
					{
						ControlPaint.DrawFocusRectangle(g, rectangle4, Color.Empty, cachedBrush.Color);
					}
				}
				if (flag2)
				{
					int num3 = rectangle2.Width;
					rectangle2.Width = num3 - 1;
					num3 = rectangle2.Height;
					rectangle2.Height = num3 - 1;
					if (!flag6 && paint && DataGridViewCell.PaintContentBackground(paintParts) && flag8)
					{
						g.DrawRectangle(SystemPens.ControlDark, rectangle2);
					}
				}
				string text = formattedValue as string;
				if (text != null)
				{
					int num4 = (cellStyle.WrapMode == DataGridViewTriState.True) ? 0 : 1;
					if (base.DataGridView.RightToLeftInternal)
					{
						rectangle4.Offset(0, num4);
						rectangle4.Width += 2;
					}
					else
					{
						rectangle4.Offset(-1, num4);
						rectangle4.Width++;
					}
					rectangle4.Height -= num4;
					if (rectangle4.Width > 0 && rectangle4.Height > 0)
					{
						TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
						if (!flag6 && paint)
						{
							if (DataGridViewCell.PaintContentForeground(paintParts))
							{
								if ((textFormatFlags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
								{
									textFormatFlags |= TextFormatFlags.EndEllipsis;
								}
								Color foreColor;
								if (flag4 && (flag9 || flag8))
								{
									foreColor = DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.VisualStyleRenderer.GetColor(ColorProperty.TextColor);
								}
								else
								{
									foreColor = (flag7 ? cellStyle.SelectionForeColor : cellStyle.ForeColor);
								}
								TextRenderer.DrawText(g, text, cellStyle.Font, rectangle4, foreColor, textFormatFlags);
							}
						}
						else if (computeContentBounds)
						{
							result = DataGridViewUtilities.GetTextBounds(rectangle4, text, textFormatFlags, cellStyle);
						}
					}
				}
				if (base.DataGridView.ShowCellErrors && paint && DataGridViewCell.PaintErrorIcon(paintParts))
				{
					base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, cellValueBounds, errorText);
					if (flag6)
					{
						return Rectangle.Empty;
					}
				}
			}
			if (computeErrorIconBounds)
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
			return result;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0009AADC File Offset: 0x00098CDC
		public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			if (valueTypeConverter == null)
			{
				if (this.ValueMemberProperty != null)
				{
					valueTypeConverter = this.ValueMemberProperty.Converter;
				}
				else if (this.DisplayMemberProperty != null)
				{
					valueTypeConverter = this.DisplayMemberProperty.Converter;
				}
			}
			if ((this.DataManager != null && (this.DisplayMemberProperty != null || this.ValueMemberProperty != null)) || !string.IsNullOrEmpty(this.DisplayMember) || !string.IsNullOrEmpty(this.ValueMember))
			{
				object obj = base.ParseFormattedValueInternal(this.DisplayType, formattedValue, cellStyle, formattedValueTypeConverter, this.DisplayTypeConverter);
				object obj2 = obj;
				if (!this.LookupValue(obj2, out obj))
				{
					if (obj2 != DBNull.Value)
					{
						throw new FormatException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Formatter_CantConvert"), new object[]
						{
							obj,
							this.DisplayType
						}));
					}
					obj = DBNull.Value;
				}
				return obj;
			}
			return base.ParseFormattedValueInternal(this.ValueType, formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0009ABC0 File Offset: 0x00098DC0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewComboBoxCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0009AC1C File Offset: 0x00098E1C
		private void UnwireDataSource()
		{
			IComponent component = this.DataSource as IComponent;
			if (component != null)
			{
				component.Disposed -= this.DataSource_Disposed;
			}
			ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null && (this.flags & 16) != 0)
			{
				supportInitializeNotification.Initialized -= this.DataSource_Initialized;
				this.flags = (byte)((int)this.flags & -17);
			}
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0009AC88 File Offset: 0x00098E88
		private void WireDataSource(object dataSource)
		{
			IComponent component = dataSource as IComponent;
			if (component != null)
			{
				component.Disposed += this.DataSource_Disposed;
			}
		}

		// Token: 0x04000D6A RID: 3434
		private static readonly int PropComboBoxCellDataSource = PropertyStore.CreateKey();

		// Token: 0x04000D6B RID: 3435
		private static readonly int PropComboBoxCellDisplayMember = PropertyStore.CreateKey();

		// Token: 0x04000D6C RID: 3436
		private static readonly int PropComboBoxCellValueMember = PropertyStore.CreateKey();

		// Token: 0x04000D6D RID: 3437
		private static readonly int PropComboBoxCellItems = PropertyStore.CreateKey();

		// Token: 0x04000D6E RID: 3438
		private static readonly int PropComboBoxCellDropDownWidth = PropertyStore.CreateKey();

		// Token: 0x04000D6F RID: 3439
		private static readonly int PropComboBoxCellMaxDropDownItems = PropertyStore.CreateKey();

		// Token: 0x04000D70 RID: 3440
		private static readonly int PropComboBoxCellEditingComboBox = PropertyStore.CreateKey();

		// Token: 0x04000D71 RID: 3441
		private static readonly int PropComboBoxCellValueMemberProp = PropertyStore.CreateKey();

		// Token: 0x04000D72 RID: 3442
		private static readonly int PropComboBoxCellDisplayMemberProp = PropertyStore.CreateKey();

		// Token: 0x04000D73 RID: 3443
		private static readonly int PropComboBoxCellDataManager = PropertyStore.CreateKey();

		// Token: 0x04000D74 RID: 3444
		private static readonly int PropComboBoxCellColumnTemplate = PropertyStore.CreateKey();

		// Token: 0x04000D75 RID: 3445
		private static readonly int PropComboBoxCellFlatStyle = PropertyStore.CreateKey();

		// Token: 0x04000D76 RID: 3446
		private static readonly int PropComboBoxCellDisplayStyle = PropertyStore.CreateKey();

		// Token: 0x04000D77 RID: 3447
		private static readonly int PropComboBoxCellDisplayStyleForCurrentCellOnly = PropertyStore.CreateKey();

		// Token: 0x04000D78 RID: 3448
		private const byte DATAGRIDVIEWCOMBOBOXCELL_margin = 3;

		// Token: 0x04000D79 RID: 3449
		private const byte DATAGRIDVIEWCOMBOBOXCELL_nonXPTriangleHeight = 4;

		// Token: 0x04000D7A RID: 3450
		private const byte DATAGRIDVIEWCOMBOBOXCELL_nonXPTriangleWidth = 7;

		// Token: 0x04000D7B RID: 3451
		private const byte DATAGRIDVIEWCOMBOBOXCELL_horizontalTextMarginLeft = 0;

		// Token: 0x04000D7C RID: 3452
		private const byte DATAGRIDVIEWCOMBOBOXCELL_verticalTextMarginTopWithWrapping = 0;

		// Token: 0x04000D7D RID: 3453
		private const byte DATAGRIDVIEWCOMBOBOXCELL_verticalTextMarginTopWithoutWrapping = 1;

		// Token: 0x04000D7E RID: 3454
		private const byte DATAGRIDVIEWCOMBOBOXCELL_ignoreNextMouseClick = 1;

		// Token: 0x04000D7F RID: 3455
		private const byte DATAGRIDVIEWCOMBOBOXCELL_sorted = 2;

		// Token: 0x04000D80 RID: 3456
		private const byte DATAGRIDVIEWCOMBOBOXCELL_createItemsFromDataSource = 4;

		// Token: 0x04000D81 RID: 3457
		private const byte DATAGRIDVIEWCOMBOBOXCELL_autoComplete = 8;

		// Token: 0x04000D82 RID: 3458
		private const byte DATAGRIDVIEWCOMBOBOXCELL_dataSourceInitializedHookedUp = 16;

		// Token: 0x04000D83 RID: 3459
		private const byte DATAGRIDVIEWCOMBOBOXCELL_dropDownHookedUp = 32;

		// Token: 0x04000D84 RID: 3460
		internal const int DATAGRIDVIEWCOMBOBOXCELL_defaultMaxDropDownItems = 8;

		// Token: 0x04000D85 RID: 3461
		private static Type defaultFormattedValueType = typeof(string);

		// Token: 0x04000D86 RID: 3462
		private static Type defaultEditType = typeof(DataGridViewComboBoxEditingControl);

		// Token: 0x04000D87 RID: 3463
		private static Type defaultValueType = typeof(object);

		// Token: 0x04000D88 RID: 3464
		private static Type cellType = typeof(DataGridViewComboBoxCell);

		// Token: 0x04000D89 RID: 3465
		private byte flags;

		// Token: 0x04000D8A RID: 3466
		private static bool mouseInDropDownButtonBounds = false;

		// Token: 0x04000D8B RID: 3467
		private static int cachedDropDownWidth = -1;

		// Token: 0x04000D8C RID: 3468
		private static bool isScalingInitialized = false;

		// Token: 0x04000D8D RID: 3469
		private static int OFFSET_2PIXELS = 2;

		// Token: 0x04000D8E RID: 3470
		private static int offset2X = DataGridViewComboBoxCell.OFFSET_2PIXELS;

		// Token: 0x04000D8F RID: 3471
		private static int offset2Y = DataGridViewComboBoxCell.OFFSET_2PIXELS;

		// Token: 0x04000D90 RID: 3472
		private static byte nonXPTriangleHeight = 4;

		// Token: 0x04000D91 RID: 3473
		private static byte nonXPTriangleWidth = 7;

		// Token: 0x0200066E RID: 1646
		[ListBindable(false)]
		public class ObjectCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006653 RID: 26195 RVA: 0x0017E899 File Offset: 0x0017CA99
			public ObjectCollection(DataGridViewComboBoxCell owner)
			{
				this.owner = owner;
			}

			// Token: 0x1700163C RID: 5692
			// (get) Token: 0x06006654 RID: 26196 RVA: 0x0017E8A8 File Offset: 0x0017CAA8
			private IComparer Comparer
			{
				get
				{
					if (this.comparer == null)
					{
						this.comparer = new DataGridViewComboBoxCell.ItemComparer(this.owner);
					}
					return this.comparer;
				}
			}

			// Token: 0x1700163D RID: 5693
			// (get) Token: 0x06006655 RID: 26197 RVA: 0x0017E8C9 File Offset: 0x0017CAC9
			public int Count
			{
				get
				{
					return this.InnerArray.Count;
				}
			}

			// Token: 0x1700163E RID: 5694
			// (get) Token: 0x06006656 RID: 26198 RVA: 0x0017E8D6 File Offset: 0x0017CAD6
			internal ArrayList InnerArray
			{
				get
				{
					if (this.items == null)
					{
						this.items = new ArrayList();
					}
					return this.items;
				}
			}

			// Token: 0x1700163F RID: 5695
			// (get) Token: 0x06006657 RID: 26199 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001640 RID: 5696
			// (get) Token: 0x06006658 RID: 26200 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001641 RID: 5697
			// (get) Token: 0x06006659 RID: 26201 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001642 RID: 5698
			// (get) Token: 0x0600665A RID: 26202 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600665B RID: 26203 RVA: 0x0017E8F4 File Offset: 0x0017CAF4
			public int Add(object item)
			{
				this.owner.CheckNoDataSource();
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				int result = this.InnerArray.Add(item);
				bool flag = false;
				if (this.owner.Sorted)
				{
					try
					{
						this.InnerArray.Sort(this.Comparer);
						result = this.InnerArray.IndexOf(item);
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							this.InnerArray.Remove(item);
						}
					}
				}
				this.owner.OnItemsCollectionChanged();
				return result;
			}

			// Token: 0x0600665C RID: 26204 RVA: 0x0017E984 File Offset: 0x0017CB84
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			// Token: 0x0600665D RID: 26205 RVA: 0x0017E98D File Offset: 0x0017CB8D
			public void AddRange(params object[] items)
			{
				this.owner.CheckNoDataSource();
				this.AddRangeInternal(items);
				this.owner.OnItemsCollectionChanged();
			}

			// Token: 0x0600665E RID: 26206 RVA: 0x0017E98D File Offset: 0x0017CB8D
			public void AddRange(DataGridViewComboBoxCell.ObjectCollection value)
			{
				this.owner.CheckNoDataSource();
				this.AddRangeInternal(value);
				this.owner.OnItemsCollectionChanged();
			}

			// Token: 0x0600665F RID: 26207 RVA: 0x0017E9AC File Offset: 0x0017CBAC
			internal void AddRangeInternal(ICollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				using (IEnumerator enumerator = items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw new InvalidOperationException(SR.GetString("InvalidNullItemInCollection"));
						}
					}
				}
				this.InnerArray.AddRange(items);
				if (this.owner.Sorted)
				{
					this.InnerArray.Sort(this.Comparer);
				}
			}

			// Token: 0x06006660 RID: 26208 RVA: 0x0017EA40 File Offset: 0x0017CC40
			internal void SortInternal()
			{
				this.InnerArray.Sort(this.Comparer);
			}

			// Token: 0x17001643 RID: 5699
			public virtual object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.InnerArray.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.InnerArray[index];
				}
				set
				{
					this.owner.CheckNoDataSource();
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					if (index < 0 || index >= this.InnerArray.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.InnerArray[index] = value;
					this.owner.OnItemsCollectionChanged();
				}
			}

			// Token: 0x06006663 RID: 26211 RVA: 0x0017EB36 File Offset: 0x0017CD36
			public void Clear()
			{
				if (this.InnerArray.Count > 0)
				{
					this.owner.CheckNoDataSource();
					this.InnerArray.Clear();
					this.owner.OnItemsCollectionChanged();
				}
			}

			// Token: 0x06006664 RID: 26212 RVA: 0x0017EB67 File Offset: 0x0017CD67
			internal void ClearInternal()
			{
				this.InnerArray.Clear();
			}

			// Token: 0x06006665 RID: 26213 RVA: 0x0017EB74 File Offset: 0x0017CD74
			public bool Contains(object value)
			{
				return this.IndexOf(value) != -1;
			}

			// Token: 0x06006666 RID: 26214 RVA: 0x0017EB84 File Offset: 0x0017CD84
			public void CopyTo(object[] destination, int arrayIndex)
			{
				int count = this.InnerArray.Count;
				for (int i = 0; i < count; i++)
				{
					destination[i + arrayIndex] = this.InnerArray[i];
				}
			}

			// Token: 0x06006667 RID: 26215 RVA: 0x0017EBBC File Offset: 0x0017CDBC
			void ICollection.CopyTo(Array destination, int index)
			{
				int count = this.InnerArray.Count;
				for (int i = 0; i < count; i++)
				{
					destination.SetValue(this.InnerArray[i], i + index);
				}
			}

			// Token: 0x06006668 RID: 26216 RVA: 0x0017EBF6 File Offset: 0x0017CDF6
			public IEnumerator GetEnumerator()
			{
				return this.InnerArray.GetEnumerator();
			}

			// Token: 0x06006669 RID: 26217 RVA: 0x0017EC03 File Offset: 0x0017CE03
			public int IndexOf(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.InnerArray.IndexOf(value);
			}

			// Token: 0x0600666A RID: 26218 RVA: 0x0017EC20 File Offset: 0x0017CE20
			public void Insert(int index, object item)
			{
				this.owner.CheckNoDataSource();
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				if (index < 0 || index > this.InnerArray.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.Sorted)
				{
					this.Add(item);
					return;
				}
				this.InnerArray.Insert(index, item);
				this.owner.OnItemsCollectionChanged();
			}

			// Token: 0x0600666B RID: 26219 RVA: 0x0017ECB8 File Offset: 0x0017CEB8
			public void Remove(object value)
			{
				int num = this.InnerArray.IndexOf(value);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x0600666C RID: 26220 RVA: 0x0017ECE0 File Offset: 0x0017CEE0
			public void RemoveAt(int index)
			{
				this.owner.CheckNoDataSource();
				if (index < 0 || index >= this.InnerArray.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.InnerArray.RemoveAt(index);
				this.owner.OnItemsCollectionChanged();
			}

			// Token: 0x04003A6B RID: 14955
			private DataGridViewComboBoxCell owner;

			// Token: 0x04003A6C RID: 14956
			private ArrayList items;

			// Token: 0x04003A6D RID: 14957
			private IComparer comparer;
		}

		// Token: 0x0200066F RID: 1647
		private sealed class ItemComparer : IComparer
		{
			// Token: 0x0600666D RID: 26221 RVA: 0x0017ED53 File Offset: 0x0017CF53
			public ItemComparer(DataGridViewComboBoxCell dataGridViewComboBoxCell)
			{
				this.dataGridViewComboBoxCell = dataGridViewComboBoxCell;
			}

			// Token: 0x0600666E RID: 26222 RVA: 0x0017ED64 File Offset: 0x0017CF64
			public int Compare(object item1, object item2)
			{
				if (item1 == null)
				{
					if (item2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (item2 == null)
					{
						return 1;
					}
					string itemDisplayText = this.dataGridViewComboBoxCell.GetItemDisplayText(item1);
					string itemDisplayText2 = this.dataGridViewComboBoxCell.GetItemDisplayText(item2);
					CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
					return compareInfo.Compare(itemDisplayText, itemDisplayText2, CompareOptions.StringSort);
				}
			}

			// Token: 0x04003A6E RID: 14958
			private DataGridViewComboBoxCell dataGridViewComboBoxCell;
		}

		// Token: 0x02000670 RID: 1648
		private class DataGridViewComboBoxCellRenderer
		{
			// Token: 0x0600666F RID: 26223 RVA: 0x00002843 File Offset: 0x00000A43
			private DataGridViewComboBoxCellRenderer()
			{
			}

			// Token: 0x17001644 RID: 5700
			// (get) Token: 0x06006670 RID: 26224 RVA: 0x0017EDB2 File Offset: 0x0017CFB2
			public static VisualStyleRenderer VisualStyleRenderer
			{
				get
				{
					if (DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxReadOnlyButton);
					}
					return DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer;
				}
			}

			// Token: 0x06006671 RID: 26225 RVA: 0x0017EDCF File Offset: 0x0017CFCF
			public static void DrawTextBox(Graphics g, Rectangle bounds, ComboBoxState state)
			{
				ComboBoxRenderer.DrawTextBox(g, bounds, state);
			}

			// Token: 0x06006672 RID: 26226 RVA: 0x0017EDD9 File Offset: 0x0017CFD9
			public static void DrawDropDownButton(Graphics g, Rectangle bounds, ComboBoxState state)
			{
				ComboBoxRenderer.DrawDropDownButton(g, bounds, state);
			}

			// Token: 0x06006673 RID: 26227 RVA: 0x0017EDE4 File Offset: 0x0017CFE4
			public static void DrawBorder(Graphics g, Rectangle bounds)
			{
				if (DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer == null)
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxBorder);
				}
				else
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.SetParameters(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxBorder.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxBorder.Part, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxBorder.State);
				}
				DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			}

			// Token: 0x06006674 RID: 26228 RVA: 0x0017EE40 File Offset: 0x0017D040
			public static void DrawDropDownButton(Graphics g, Rectangle bounds, ComboBoxState state, bool rightToLeft)
			{
				if (rightToLeft)
				{
					if (DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer == null)
					{
						DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonLeft.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonLeft.Part, (int)state);
					}
					else
					{
						DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.SetParameters(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonLeft.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonLeft.Part, (int)state);
					}
				}
				else if (DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer == null)
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonRight.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonRight.Part, (int)state);
				}
				else
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.SetParameters(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonRight.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxDropDownButtonRight.Part, (int)state);
				}
				DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			}

			// Token: 0x06006675 RID: 26229 RVA: 0x0017EEEC File Offset: 0x0017D0EC
			public static void DrawReadOnlyButton(Graphics g, Rectangle bounds, ComboBoxState state)
			{
				if (DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer == null)
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer = new VisualStyleRenderer(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxReadOnlyButton.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxReadOnlyButton.Part, (int)state);
				}
				else
				{
					DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.SetParameters(DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxReadOnlyButton.ClassName, DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.ComboBoxReadOnlyButton.Part, (int)state);
				}
				DataGridViewComboBoxCell.DataGridViewComboBoxCellRenderer.visualStyleRenderer.DrawBackground(g, bounds);
			}

			// Token: 0x04003A6F RID: 14959
			[ThreadStatic]
			private static VisualStyleRenderer visualStyleRenderer;

			// Token: 0x04003A70 RID: 14960
			private static readonly VisualStyleElement ComboBoxBorder = VisualStyleElement.ComboBox.Border.Normal;

			// Token: 0x04003A71 RID: 14961
			private static readonly VisualStyleElement ComboBoxDropDownButtonRight = VisualStyleElement.ComboBox.DropDownButtonRight.Normal;

			// Token: 0x04003A72 RID: 14962
			private static readonly VisualStyleElement ComboBoxDropDownButtonLeft = VisualStyleElement.ComboBox.DropDownButtonLeft.Normal;

			// Token: 0x04003A73 RID: 14963
			private static readonly VisualStyleElement ComboBoxReadOnlyButton = VisualStyleElement.ComboBox.ReadOnlyButton.Normal;
		}

		// Token: 0x02000671 RID: 1649
		[ComVisible(true)]
		protected class DataGridViewComboBoxCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			// Token: 0x06006677 RID: 26231 RVA: 0x0017C895 File Offset: 0x0017AA95
			public DataGridViewComboBoxCellAccessibleObject(DataGridViewCell owner) : base(owner)
			{
			}

			// Token: 0x06006678 RID: 26232 RVA: 0x0017EF76 File Offset: 0x0017D176
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerCellDestroyed();
			}

			// Token: 0x06006679 RID: 26233 RVA: 0x0017EF81 File Offset: 0x0017D181
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50003;
				}
				if (AccessibilityImprovements.Level4 && propertyID == 30028)
				{
					return this.IsPatternSupported(10005);
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x0600667A RID: 26234 RVA: 0x0017EFBD File Offset: 0x0017D1BD
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerCellDestroyed() && ((AccessibilityImprovements.Level4 && patternId == 10005) || base.IsPatternSupported(patternId));
			}

			// Token: 0x17001645 RID: 5701
			// (get) Token: 0x0600667B RID: 26235 RVA: 0x0017EFE4 File Offset: 0x0017D1E4
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (!AccessibilityImprovements.Level4)
					{
						return base.ExpandCollapseState;
					}
					DataGridViewCell owner = base.Owner;
					object obj;
					if (owner == null)
					{
						obj = null;
					}
					else
					{
						PropertyStore properties = owner.Properties;
						obj = ((properties != null) ? properties.GetObject(DataGridViewComboBoxCell.PropComboBoxCellEditingComboBox) : null);
					}
					DataGridViewComboBoxEditingControl dataGridViewComboBoxEditingControl = obj as DataGridViewComboBoxEditingControl;
					if (dataGridViewComboBoxEditingControl == null)
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					if (!dataGridViewComboBoxEditingControl.DroppedDown)
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					return UnsafeNativeMethods.ExpandCollapseState.Expanded;
				}
			}
		}
	}
}
