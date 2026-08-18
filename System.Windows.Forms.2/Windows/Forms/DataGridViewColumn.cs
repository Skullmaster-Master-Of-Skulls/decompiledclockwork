using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x020001BF RID: 447
	[Designer("System.Windows.Forms.Design.DataGridViewColumnDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[TypeConverter(typeof(DataGridViewColumnConverter))]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class DataGridViewColumn : DataGridViewBand, IComponent, IDisposable
	{
		// Token: 0x06001F16 RID: 7958 RVA: 0x00093482 File Offset: 0x00091682
		public DataGridViewColumn() : this(null)
		{
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0009348C File Offset: 0x0009168C
		public DataGridViewColumn(DataGridViewCell cellTemplate)
		{
			this.fillWeight = 100f;
			this.usedFillWeight = 100f;
			base.Thickness = this.ScaleToCurrentDpi(100);
			base.MinimumThickness = this.ScaleToCurrentDpi(5);
			this.name = string.Empty;
			this.bandIsRow = false;
			this.displayIndex = -1;
			this.cellTemplate = cellTemplate;
			this.autoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00093509 File Offset: 0x00091709
		private int ScaleToCurrentDpi(int value)
		{
			if (!DpiHelper.EnableDataGridViewControlHighDpiImprovements)
			{
				return value;
			}
			return DpiHelper.LogicalToDeviceUnits(value, 0);
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0009351B File Offset: 0x0009171B
		// (set) Token: 0x06001F1A RID: 7962 RVA: 0x00093524 File Offset: 0x00091724
		[SRCategory("CatLayout")]
		[DefaultValue(DataGridViewAutoSizeColumnMode.NotSet)]
		[SRDescription("DataGridViewColumn_AutoSizeModeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public DataGridViewAutoSizeColumnMode AutoSizeMode
		{
			get
			{
				return this.autoSizeMode;
			}
			set
			{
				switch (value)
				{
				case DataGridViewAutoSizeColumnMode.NotSet:
				case DataGridViewAutoSizeColumnMode.None:
				case DataGridViewAutoSizeColumnMode.ColumnHeader:
				case DataGridViewAutoSizeColumnMode.AllCellsExceptHeader:
				case DataGridViewAutoSizeColumnMode.AllCells:
				case DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader:
				case DataGridViewAutoSizeColumnMode.DisplayedCells:
					goto IL_4D;
				case (DataGridViewAutoSizeColumnMode)3:
				case (DataGridViewAutoSizeColumnMode)5:
				case (DataGridViewAutoSizeColumnMode)7:
				case (DataGridViewAutoSizeColumnMode)9:
					break;
				default:
					if (value == DataGridViewAutoSizeColumnMode.Fill)
					{
						goto IL_4D;
					}
					break;
				}
				throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewAutoSizeColumnMode));
				IL_4D:
				if (this.autoSizeMode != value)
				{
					if (this.Visible && base.DataGridView != null)
					{
						if (!base.DataGridView.ColumnHeadersVisible && (value == DataGridViewAutoSizeColumnMode.ColumnHeader || (value == DataGridViewAutoSizeColumnMode.NotSet && base.DataGridView.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.ColumnHeader)))
						{
							throw new InvalidOperationException(SR.GetString("DataGridViewColumn_AutoSizeCriteriaCannotUseInvisibleHeaders"));
						}
						if (this.Frozen && (value == DataGridViewAutoSizeColumnMode.Fill || (value == DataGridViewAutoSizeColumnMode.NotSet && base.DataGridView.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)))
						{
							throw new InvalidOperationException(SR.GetString("DataGridViewColumn_FrozenColumnCannotAutoFill"));
						}
					}
					DataGridViewAutoSizeColumnMode inheritedAutoSizeMode = this.InheritedAutoSizeMode;
					bool flag = inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill && inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.None && inheritedAutoSizeMode > DataGridViewAutoSizeColumnMode.NotSet;
					this.autoSizeMode = value;
					if (base.DataGridView == null)
					{
						if (this.InheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill && this.InheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.None && this.InheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.NotSet)
						{
							if (!flag)
							{
								base.CachedThickness = base.Thickness;
								return;
							}
						}
						else if (base.Thickness != base.CachedThickness && flag)
						{
							base.ThicknessInternal = base.CachedThickness;
							return;
						}
					}
					else
					{
						base.DataGridView.OnAutoSizeColumnModeChanged(this, inheritedAutoSizeMode);
					}
				}
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x0009367C File Offset: 0x0009187C
		// (set) Token: 0x06001F1C RID: 7964 RVA: 0x00093684 File Offset: 0x00091884
		internal TypeConverter BoundColumnConverter
		{
			get
			{
				return this.boundColumnConverter;
			}
			set
			{
				this.boundColumnConverter = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x0009368D File Offset: 0x0009188D
		// (set) Token: 0x06001F1E RID: 7966 RVA: 0x00093695 File Offset: 0x00091895
		internal int BoundColumnIndex
		{
			get
			{
				return this.boundColumnIndex;
			}
			set
			{
				this.boundColumnIndex = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x0009369E File Offset: 0x0009189E
		// (set) Token: 0x06001F20 RID: 7968 RVA: 0x000936A6 File Offset: 0x000918A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DataGridViewCell CellTemplate
		{
			get
			{
				return this.cellTemplate;
			}
			set
			{
				this.cellTemplate = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x000936AF File Offset: 0x000918AF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Type CellType
		{
			get
			{
				if (this.cellTemplate != null)
				{
					return this.cellTemplate.GetType();
				}
				return null;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x000936C6 File Offset: 0x000918C6
		// (set) Token: 0x06001F23 RID: 7971 RVA: 0x000936CE File Offset: 0x000918CE
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ColumnContextMenuStripDescr")]
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x000936D7 File Offset: 0x000918D7
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x000936DF File Offset: 0x000918DF
		[Browsable(true)]
		[DefaultValue("")]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.DataGridViewColumnDataPropertyNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("DataGridView_ColumnDataPropertyNameDescr")]
		[SRCategory("CatData")]
		public string DataPropertyName
		{
			get
			{
				return this.dataPropertyName;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value != this.dataPropertyName)
				{
					this.dataPropertyName = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnDataPropertyNameChanged(this);
					}
				}
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x00093714 File Offset: 0x00091914
		// (set) Token: 0x06001F27 RID: 7975 RVA: 0x0009371C File Offset: 0x0009191C
		[Browsable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ColumnDefaultCellStyleDescr")]
		public override DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				return base.DefaultCellStyle;
			}
			set
			{
				base.DefaultCellStyle = value;
			}
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00093728 File Offset: 0x00091928
		private bool ShouldSerializeDefaultCellStyle()
		{
			if (!base.HasDefaultCellStyle)
			{
				return false;
			}
			DataGridViewCellStyle defaultCellStyle = this.DefaultCellStyle;
			return !defaultCellStyle.BackColor.IsEmpty || !defaultCellStyle.ForeColor.IsEmpty || !defaultCellStyle.SelectionBackColor.IsEmpty || !defaultCellStyle.SelectionForeColor.IsEmpty || defaultCellStyle.Font != null || !defaultCellStyle.IsNullValueDefault || !defaultCellStyle.IsDataSourceNullValueDefault || !string.IsNullOrEmpty(defaultCellStyle.Format) || !defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) || defaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet || defaultCellStyle.WrapMode != DataGridViewTriState.NotSet || defaultCellStyle.Tag != null || !defaultCellStyle.Padding.Equals(Padding.Empty);
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x00093801 File Offset: 0x00091A01
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x00093809 File Offset: 0x00091A09
		internal int DesiredFillWidth
		{
			get
			{
				return this.desiredFillWidth;
			}
			set
			{
				this.desiredFillWidth = value;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x00093812 File Offset: 0x00091A12
		// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0009381A File Offset: 0x00091A1A
		internal int DesiredMinimumWidth
		{
			get
			{
				return this.desiredMinimumWidth;
			}
			set
			{
				this.desiredMinimumWidth = value;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00093823 File Offset: 0x00091A23
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0009382C File Offset: 0x00091A2C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int DisplayIndex
		{
			get
			{
				return this.displayIndex;
			}
			set
			{
				if (this.displayIndex != value)
				{
					if (value == 2147483647)
					{
						throw new ArgumentOutOfRangeException("DisplayIndex", value, SR.GetString("DataGridViewColumn_DisplayIndexTooLarge", new object[]
						{
							int.MaxValue.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (base.DataGridView != null)
					{
						if (value < 0)
						{
							throw new ArgumentOutOfRangeException("DisplayIndex", value, SR.GetString("DataGridViewColumn_DisplayIndexNegative"));
						}
						if (value >= base.DataGridView.Columns.Count)
						{
							throw new ArgumentOutOfRangeException("DisplayIndex", value, SR.GetString("DataGridViewColumn_DisplayIndexExceedsColumnCount"));
						}
						base.DataGridView.OnColumnDisplayIndexChanging(this, value);
						this.displayIndex = value;
						try
						{
							base.DataGridView.InDisplayIndexAdjustments = true;
							base.DataGridView.OnColumnDisplayIndexChanged_PreNotification();
							base.DataGridView.OnColumnDisplayIndexChanged(this);
							base.DataGridView.OnColumnDisplayIndexChanged_PostNotification();
							return;
						}
						finally
						{
							base.DataGridView.InDisplayIndexAdjustments = false;
						}
					}
					if (value < -1)
					{
						throw new ArgumentOutOfRangeException("DisplayIndex", value, SR.GetString("DataGridViewColumn_DisplayIndexTooNegative"));
					}
					this.displayIndex = value;
				}
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x00093964 File Offset: 0x00091B64
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x00093972 File Offset: 0x00091B72
		internal bool DisplayIndexHasChanged
		{
			get
			{
				return (this.flags & 16) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 16;
					return;
				}
				this.flags = (byte)((int)this.flags & -17);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (set) Token: 0x06001F31 RID: 7985 RVA: 0x00093998 File Offset: 0x00091B98
		internal int DisplayIndexInternal
		{
			set
			{
				this.displayIndex = value;
			}
		}

		// Token: 0x14000184 RID: 388
		// (add) Token: 0x06001F32 RID: 7986 RVA: 0x000939A1 File Offset: 0x00091BA1
		// (remove) Token: 0x06001F33 RID: 7987 RVA: 0x000939BA File Offset: 0x00091BBA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler Disposed
		{
			add
			{
				this.disposed = (EventHandler)Delegate.Combine(this.disposed, value);
			}
			remove
			{
				this.disposed = (EventHandler)Delegate.Remove(this.disposed, value);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x000939D3 File Offset: 0x00091BD3
		// (set) Token: 0x06001F35 RID: 7989 RVA: 0x000939DB File Offset: 0x00091BDB
		[DefaultValue(0)]
		[SRCategory("CatLayout")]
		[SRDescription("DataGridView_ColumnDividerWidthDescr")]
		public int DividerWidth
		{
			get
			{
				return base.DividerThickness;
			}
			set
			{
				base.DividerThickness = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x000939E4 File Offset: 0x00091BE4
		// (set) Token: 0x06001F37 RID: 7991 RVA: 0x000939EC File Offset: 0x00091BEC
		[SRCategory("CatLayout")]
		[DefaultValue(100f)]
		[SRDescription("DataGridViewColumn_FillWeightDescr")]
		public float FillWeight
		{
			get
			{
				return this.fillWeight;
			}
			set
			{
				if (value <= 0f)
				{
					throw new ArgumentOutOfRangeException("FillWeight", SR.GetString("InvalidLowBoundArgument", new object[]
					{
						"FillWeight",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (value > 65535f)
				{
					throw new ArgumentOutOfRangeException("FillWeight", SR.GetString("InvalidHighBoundArgumentEx", new object[]
					{
						"FillWeight",
						value.ToString(CultureInfo.CurrentCulture),
						ushort.MaxValue.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnFillWeightChanging(this, value);
					this.fillWeight = value;
					base.DataGridView.OnColumnFillWeightChanged(this);
					return;
				}
				this.fillWeight = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (set) Token: 0x06001F38 RID: 7992 RVA: 0x00093AC3 File Offset: 0x00091CC3
		internal float FillWeightInternal
		{
			set
			{
				this.fillWeight = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x00093ACC File Offset: 0x00091CCC
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x00093AD4 File Offset: 0x00091CD4
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		[SRCategory("CatLayout")]
		[SRDescription("DataGridView_ColumnFrozenDescr")]
		public override bool Frozen
		{
			get
			{
				return base.Frozen;
			}
			set
			{
				base.Frozen = value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x00093ADD File Offset: 0x00091CDD
		// (set) Token: 0x06001F3C RID: 7996 RVA: 0x00093AEA File Offset: 0x00091CEA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataGridViewColumnHeaderCell HeaderCell
		{
			get
			{
				return (DataGridViewColumnHeaderCell)base.HeaderCellCore;
			}
			set
			{
				base.HeaderCellCore = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x00093AF4 File Offset: 0x00091CF4
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x00093B2C File Offset: 0x00091D2C
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ColumnHeaderTextDescr")]
		[Localizable(true)]
		public string HeaderText
		{
			get
			{
				if (!base.HasHeaderCell)
				{
					return string.Empty;
				}
				string text = this.HeaderCell.Value as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if ((value != null || base.HasHeaderCell) && this.HeaderCell.ValueType != null && this.HeaderCell.ValueType.IsAssignableFrom(typeof(string)))
				{
					this.HeaderCell.Value = value;
				}
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00093B7F File Offset: 0x00091D7F
		private bool ShouldSerializeHeaderText()
		{
			return base.HasHeaderCell && this.HeaderCell.ContainsLocalValue;
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x00093B96 File Offset: 0x00091D96
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataGridViewAutoSizeColumnMode InheritedAutoSizeMode
		{
			get
			{
				return this.GetInheritedAutoSizeMode(base.DataGridView);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x00093BA4 File Offset: 0x00091DA4
		[Browsable(false)]
		public override DataGridViewCellStyle InheritedStyle
		{
			get
			{
				DataGridViewCellStyle dataGridViewCellStyle = null;
				if (base.HasDefaultCellStyle)
				{
					dataGridViewCellStyle = this.DefaultCellStyle;
				}
				if (base.DataGridView == null)
				{
					return dataGridViewCellStyle;
				}
				DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
				DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.BackColor.IsEmpty)
				{
					dataGridViewCellStyle2.BackColor = dataGridViewCellStyle.BackColor;
				}
				else
				{
					dataGridViewCellStyle2.BackColor = defaultCellStyle.BackColor;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle2.ForeColor = dataGridViewCellStyle.ForeColor;
				}
				else
				{
					dataGridViewCellStyle2.ForeColor = defaultCellStyle.ForeColor;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle2.SelectionBackColor = dataGridViewCellStyle.SelectionBackColor;
				}
				else
				{
					dataGridViewCellStyle2.SelectionBackColor = defaultCellStyle.SelectionBackColor;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle2.SelectionForeColor = dataGridViewCellStyle.SelectionForeColor;
				}
				else
				{
					dataGridViewCellStyle2.SelectionForeColor = defaultCellStyle.SelectionForeColor;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.Font != null)
				{
					dataGridViewCellStyle2.Font = dataGridViewCellStyle.Font;
				}
				else
				{
					dataGridViewCellStyle2.Font = defaultCellStyle.Font;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsNullValueDefault)
				{
					dataGridViewCellStyle2.NullValue = dataGridViewCellStyle.NullValue;
				}
				else
				{
					dataGridViewCellStyle2.NullValue = defaultCellStyle.NullValue;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsDataSourceNullValueDefault)
				{
					dataGridViewCellStyle2.DataSourceNullValue = dataGridViewCellStyle.DataSourceNullValue;
				}
				else
				{
					dataGridViewCellStyle2.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.Format.Length != 0)
				{
					dataGridViewCellStyle2.Format = dataGridViewCellStyle.Format;
				}
				else
				{
					dataGridViewCellStyle2.Format = defaultCellStyle.Format;
				}
				if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsFormatProviderDefault)
				{
					dataGridViewCellStyle2.FormatProvider = dataGridViewCellStyle.FormatProvider;
				}
				else
				{
					dataGridViewCellStyle2.FormatProvider = defaultCellStyle.FormatProvider;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
				{
					dataGridViewCellStyle2.AlignmentInternal = dataGridViewCellStyle.Alignment;
				}
				else
				{
					dataGridViewCellStyle2.AlignmentInternal = defaultCellStyle.Alignment;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.WrapMode != DataGridViewTriState.NotSet)
				{
					dataGridViewCellStyle2.WrapModeInternal = dataGridViewCellStyle.WrapMode;
				}
				else
				{
					dataGridViewCellStyle2.WrapModeInternal = defaultCellStyle.WrapMode;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.Tag != null)
				{
					dataGridViewCellStyle2.Tag = dataGridViewCellStyle.Tag;
				}
				else
				{
					dataGridViewCellStyle2.Tag = defaultCellStyle.Tag;
				}
				if (dataGridViewCellStyle != null && dataGridViewCellStyle.Padding != Padding.Empty)
				{
					dataGridViewCellStyle2.PaddingInternal = dataGridViewCellStyle.Padding;
				}
				else
				{
					dataGridViewCellStyle2.PaddingInternal = defaultCellStyle.Padding;
				}
				return dataGridViewCellStyle2;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x00093DEF File Offset: 0x00091FEF
		// (set) Token: 0x06001F43 RID: 8003 RVA: 0x00093DFC File Offset: 0x00091FFC
		internal bool IsBrowsableInternal
		{
			get
			{
				return (this.flags & 8) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 8;
					return;
				}
				this.flags = (byte)((int)this.flags & -9);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x00093E21 File Offset: 0x00092021
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsDataBound
		{
			get
			{
				return this.IsDataBoundInternal;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x00093E29 File Offset: 0x00092029
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x00093E36 File Offset: 0x00092036
		internal bool IsDataBoundInternal
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

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00093E5B File Offset: 0x0009205B
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x00093E63 File Offset: 0x00092063
		[DefaultValue(5)]
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[SRDescription("DataGridView_ColumnMinimumWidthDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int MinimumWidth
		{
			get
			{
				return base.MinimumThickness;
			}
			set
			{
				base.MinimumThickness = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00093E6C File Offset: 0x0009206C
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x00093EA0 File Offset: 0x000920A0
		[Browsable(false)]
		public string Name
		{
			get
			{
				if (this.Site != null && !string.IsNullOrEmpty(this.Site.Name))
				{
					this.name = this.Site.Name;
				}
				return this.name;
			}
			set
			{
				string b = this.name;
				if (string.IsNullOrEmpty(value))
				{
					this.name = string.Empty;
				}
				else
				{
					this.name = value;
				}
				if (base.DataGridView != null && !string.Equals(this.name, b, StringComparison.Ordinal))
				{
					base.DataGridView.OnColumnNameChanged(this);
				}
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x00093EF3 File Offset: 0x000920F3
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x00093EFC File Offset: 0x000920FC
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ColumnReadOnlyDescr")]
		public override bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				if (this.IsDataBound && base.DataGridView != null && base.DataGridView.DataConnection != null && this.boundColumnIndex != -1 && base.DataGridView.DataConnection.DataFieldIsReadOnly(this.boundColumnIndex) && !value)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_ColumnBoundToAReadOnlyFieldMustRemainReadOnly"));
				}
				base.ReadOnly = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x00093F61 File Offset: 0x00092161
		// (set) Token: 0x06001F4E RID: 8014 RVA: 0x00093F69 File Offset: 0x00092169
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ColumnResizableDescr")]
		public override DataGridViewTriState Resizable
		{
			get
			{
				return base.Resizable;
			}
			set
			{
				base.Resizable = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x00093F72 File Offset: 0x00092172
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x00093F7A File Offset: 0x0009217A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ISite Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x00093F83 File Offset: 0x00092183
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x00093FA0 File Offset: 0x000921A0
		[DefaultValue(DataGridViewColumnSortMode.NotSortable)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ColumnSortModeDescr")]
		public DataGridViewColumnSortMode SortMode
		{
			get
			{
				if ((this.flags & 1) != 0)
				{
					return DataGridViewColumnSortMode.Automatic;
				}
				if ((this.flags & 2) != 0)
				{
					return DataGridViewColumnSortMode.Programmatic;
				}
				return DataGridViewColumnSortMode.NotSortable;
			}
			set
			{
				if (value != this.SortMode)
				{
					if (value != DataGridViewColumnSortMode.NotSortable)
					{
						if (base.DataGridView != null && !base.DataGridView.InInitialization && value == DataGridViewColumnSortMode.Automatic && (base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect))
						{
							throw new InvalidOperationException(SR.GetString("DataGridViewColumn_SortModeAndSelectionModeClash", new object[]
							{
								value.ToString(),
								base.DataGridView.SelectionMode.ToString()
							}));
						}
						if (value == DataGridViewColumnSortMode.Automatic)
						{
							this.flags = (byte)((int)this.flags & -3);
							this.flags |= 1;
						}
						else
						{
							this.flags = (byte)((int)this.flags & -2);
							this.flags |= 2;
						}
					}
					else
					{
						this.flags = (byte)((int)this.flags & -2);
						this.flags = (byte)((int)this.flags & -3);
					}
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnSortModeChanged(this);
					}
				}
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x000940B0 File Offset: 0x000922B0
		// (set) Token: 0x06001F54 RID: 8020 RVA: 0x000940BD File Offset: 0x000922BD
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ColumnToolTipTextDescr")]
		public string ToolTipText
		{
			get
			{
				return this.HeaderCell.ToolTipText;
			}
			set
			{
				if (string.Compare(this.ToolTipText, value, false, CultureInfo.InvariantCulture) != 0)
				{
					this.HeaderCell.ToolTipText = value;
					if (base.DataGridView != null)
					{
						base.DataGridView.OnColumnToolTipTextChanged(this);
					}
				}
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001F55 RID: 8021 RVA: 0x000940F3 File Offset: 0x000922F3
		// (set) Token: 0x06001F56 RID: 8022 RVA: 0x000940FB File Offset: 0x000922FB
		internal float UsedFillWeight
		{
			get
			{
				return this.usedFillWeight;
			}
			set
			{
				this.usedFillWeight = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x00094104 File Offset: 0x00092304
		// (set) Token: 0x06001F58 RID: 8024 RVA: 0x0009411B File Offset: 0x0009231B
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Type ValueType
		{
			get
			{
				return (Type)base.Properties.GetObject(DataGridViewColumn.PropDataGridViewColumnValueType);
			}
			set
			{
				base.Properties.SetObject(DataGridViewColumn.PropDataGridViewColumnValueType, value);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x0009412E File Offset: 0x0009232E
		// (set) Token: 0x06001F5A RID: 8026 RVA: 0x00094136 File Offset: 0x00092336
		[DefaultValue(true)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ColumnVisibleDescr")]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x0009413F File Offset: 0x0009233F
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x00094147 File Offset: 0x00092347
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("DataGridView_ColumnWidthDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int Width
		{
			get
			{
				return base.Thickness;
			}
			set
			{
				base.Thickness = value;
			}
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00094150 File Offset: 0x00092350
		public override object Clone()
		{
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)Activator.CreateInstance(base.GetType());
			if (dataGridViewColumn != null)
			{
				this.CloneInternal(dataGridViewColumn);
			}
			return dataGridViewColumn;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0009417C File Offset: 0x0009237C
		internal void CloneInternal(DataGridViewColumn dataGridViewColumn)
		{
			base.CloneInternal(dataGridViewColumn);
			dataGridViewColumn.name = this.Name;
			dataGridViewColumn.displayIndex = -1;
			dataGridViewColumn.HeaderText = this.HeaderText;
			dataGridViewColumn.DataPropertyName = this.DataPropertyName;
			if (dataGridViewColumn.CellTemplate != null)
			{
				dataGridViewColumn.cellTemplate = (DataGridViewCell)this.CellTemplate.Clone();
			}
			else
			{
				dataGridViewColumn.cellTemplate = null;
			}
			if (base.HasHeaderCell)
			{
				dataGridViewColumn.HeaderCell = (DataGridViewColumnHeaderCell)this.HeaderCell.Clone();
			}
			dataGridViewColumn.AutoSizeMode = this.AutoSizeMode;
			dataGridViewColumn.SortMode = this.SortMode;
			dataGridViewColumn.FillWeightInternal = this.FillWeight;
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00094224 File Offset: 0x00092424
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					lock (this)
					{
						if (this.site != null && this.site.Container != null)
						{
							this.site.Container.Remove(this);
						}
						if (this.disposed != null)
						{
							this.disposed(this, EventArgs.Empty);
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x000942B4 File Offset: 0x000924B4
		internal DataGridViewAutoSizeColumnMode GetInheritedAutoSizeMode(DataGridView dataGridView)
		{
			if (dataGridView != null && this.autoSizeMode == DataGridViewAutoSizeColumnMode.NotSet)
			{
				DataGridViewAutoSizeColumnsMode autoSizeColumnsMode = dataGridView.AutoSizeColumnsMode;
				switch (autoSizeColumnsMode)
				{
				case DataGridViewAutoSizeColumnsMode.ColumnHeader:
					return DataGridViewAutoSizeColumnMode.ColumnHeader;
				case (DataGridViewAutoSizeColumnsMode)3:
				case (DataGridViewAutoSizeColumnsMode)5:
				case (DataGridViewAutoSizeColumnsMode)7:
				case (DataGridViewAutoSizeColumnsMode)9:
					break;
				case DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader:
					return DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
				case DataGridViewAutoSizeColumnsMode.AllCells:
					return DataGridViewAutoSizeColumnMode.AllCells;
				case DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader:
					return DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
				case DataGridViewAutoSizeColumnsMode.DisplayedCells:
					return DataGridViewAutoSizeColumnMode.DisplayedCells;
				default:
					if (autoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill)
					{
						return DataGridViewAutoSizeColumnMode.Fill;
					}
					break;
				}
				return DataGridViewAutoSizeColumnMode.None;
			}
			return this.autoSizeMode;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0009431C File Offset: 0x0009251C
		public virtual int GetPreferredWidth(DataGridViewAutoSizeColumnMode autoSizeColumnMode, bool fixedHeight)
		{
			if (autoSizeColumnMode == DataGridViewAutoSizeColumnMode.NotSet || autoSizeColumnMode == DataGridViewAutoSizeColumnMode.None || autoSizeColumnMode == DataGridViewAutoSizeColumnMode.Fill)
			{
				throw new ArgumentException(SR.GetString("DataGridView_NeedColumnAutoSizingCriteria", new object[]
				{
					"autoSizeColumnMode"
				}));
			}
			switch (autoSizeColumnMode)
			{
			case DataGridViewAutoSizeColumnMode.NotSet:
			case DataGridViewAutoSizeColumnMode.None:
			case DataGridViewAutoSizeColumnMode.ColumnHeader:
			case DataGridViewAutoSizeColumnMode.AllCellsExceptHeader:
			case DataGridViewAutoSizeColumnMode.AllCells:
			case DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader:
			case DataGridViewAutoSizeColumnMode.DisplayedCells:
				goto IL_77;
			case (DataGridViewAutoSizeColumnMode)3:
			case (DataGridViewAutoSizeColumnMode)5:
			case (DataGridViewAutoSizeColumnMode)7:
			case (DataGridViewAutoSizeColumnMode)9:
				break;
			default:
				if (autoSizeColumnMode == DataGridViewAutoSizeColumnMode.Fill)
				{
					goto IL_77;
				}
				break;
			}
			throw new InvalidEnumArgumentException("value", (int)autoSizeColumnMode, typeof(DataGridViewAutoSizeColumnMode));
			IL_77:
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView == null)
			{
				return -1;
			}
			int num = 0;
			if (dataGridView.ColumnHeadersVisible && (autoSizeColumnMode & DataGridViewAutoSizeColumnMode.ColumnHeader) != DataGridViewAutoSizeColumnMode.NotSet)
			{
				int num2;
				if (fixedHeight)
				{
					num2 = this.HeaderCell.GetPreferredWidth(-1, dataGridView.ColumnHeadersHeight);
				}
				else
				{
					num2 = this.HeaderCell.GetPreferredSize(-1).Width;
				}
				if (num < num2)
				{
					num = num2;
				}
			}
			if ((autoSizeColumnMode & DataGridViewAutoSizeColumnMode.AllCellsExceptHeader) != DataGridViewAutoSizeColumnMode.NotSet)
			{
				for (int num3 = dataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible); num3 != -1; num3 = dataGridView.Rows.GetNextRow(num3, DataGridViewElementStates.Visible))
				{
					DataGridViewRow dataGridViewRow = dataGridView.Rows.SharedRow(num3);
					int num2;
					if (fixedHeight)
					{
						num2 = dataGridViewRow.Cells[base.Index].GetPreferredWidth(num3, dataGridViewRow.Thickness);
					}
					else
					{
						num2 = dataGridViewRow.Cells[base.Index].GetPreferredSize(num3).Width;
					}
					if (num < num2)
					{
						num = num2;
					}
				}
			}
			else if ((autoSizeColumnMode & DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader) != DataGridViewAutoSizeColumnMode.NotSet)
			{
				int height = dataGridView.LayoutInfo.Data.Height;
				int num4 = 0;
				int num3 = dataGridView.Rows.GetFirstRow(DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible);
				while (num3 != -1 && num4 < height)
				{
					DataGridViewRow dataGridViewRow = dataGridView.Rows.SharedRow(num3);
					int num2;
					if (fixedHeight)
					{
						num2 = dataGridViewRow.Cells[base.Index].GetPreferredWidth(num3, dataGridViewRow.Thickness);
					}
					else
					{
						num2 = dataGridViewRow.Cells[base.Index].GetPreferredSize(num3).Width;
					}
					if (num < num2)
					{
						num = num2;
					}
					num4 += dataGridViewRow.Thickness;
					num3 = dataGridView.Rows.GetNextRow(num3, DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible);
				}
				if (num4 < height)
				{
					num3 = dataGridView.DisplayedBandsInfo.FirstDisplayedScrollingRow;
					while (num3 != -1 && num4 < height)
					{
						DataGridViewRow dataGridViewRow = dataGridView.Rows.SharedRow(num3);
						int num2;
						if (fixedHeight)
						{
							num2 = dataGridViewRow.Cells[base.Index].GetPreferredWidth(num3, dataGridViewRow.Thickness);
						}
						else
						{
							num2 = dataGridViewRow.Cells[base.Index].GetPreferredSize(num3).Width;
						}
						if (num < num2)
						{
							num = num2;
						}
						num4 += dataGridViewRow.Thickness;
						num3 = dataGridView.Rows.GetNextRow(num3, DataGridViewElementStates.Visible);
					}
				}
			}
			return num;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x000945DC File Offset: 0x000927DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewColumn { Name=");
			stringBuilder.Append(this.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000D2E RID: 3374
		private const float DATAGRIDVIEWCOLUMN_defaultFillWeight = 100f;

		// Token: 0x04000D2F RID: 3375
		private const int DATAGRIDVIEWCOLUMN_defaultWidth = 100;

		// Token: 0x04000D30 RID: 3376
		private const int DATAGRIDVIEWCOLUMN_defaultMinColumnThickness = 5;

		// Token: 0x04000D31 RID: 3377
		private const byte DATAGRIDVIEWCOLUMN_automaticSort = 1;

		// Token: 0x04000D32 RID: 3378
		private const byte DATAGRIDVIEWCOLUMN_programmaticSort = 2;

		// Token: 0x04000D33 RID: 3379
		private const byte DATAGRIDVIEWCOLUMN_isDataBound = 4;

		// Token: 0x04000D34 RID: 3380
		private const byte DATAGRIDVIEWCOLUMN_isBrowsableInternal = 8;

		// Token: 0x04000D35 RID: 3381
		private const byte DATAGRIDVIEWCOLUMN_displayIndexHasChangedInternal = 16;

		// Token: 0x04000D36 RID: 3382
		private byte flags;

		// Token: 0x04000D37 RID: 3383
		private DataGridViewCell cellTemplate;

		// Token: 0x04000D38 RID: 3384
		private string name;

		// Token: 0x04000D39 RID: 3385
		private int displayIndex;

		// Token: 0x04000D3A RID: 3386
		private int desiredFillWidth;

		// Token: 0x04000D3B RID: 3387
		private int desiredMinimumWidth;

		// Token: 0x04000D3C RID: 3388
		private float fillWeight;

		// Token: 0x04000D3D RID: 3389
		private float usedFillWeight;

		// Token: 0x04000D3E RID: 3390
		private DataGridViewAutoSizeColumnMode autoSizeMode;

		// Token: 0x04000D3F RID: 3391
		private int boundColumnIndex = -1;

		// Token: 0x04000D40 RID: 3392
		private string dataPropertyName = string.Empty;

		// Token: 0x04000D41 RID: 3393
		private TypeConverter boundColumnConverter;

		// Token: 0x04000D42 RID: 3394
		private ISite site;

		// Token: 0x04000D43 RID: 3395
		private EventHandler disposed;

		// Token: 0x04000D44 RID: 3396
		private static readonly int PropDataGridViewColumnValueType = PropertyStore.CreateKey();
	}
}
