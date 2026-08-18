using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x020001B3 RID: 435
	[TypeConverter(typeof(DataGridViewCellStyleConverter))]
	[Editor("System.Windows.Forms.Design.DataGridViewCellStyleEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public class DataGridViewCellStyle : ICloneable
	{
		// Token: 0x06001E75 RID: 7797 RVA: 0x0008F970 File Offset: 0x0008DB70
		public DataGridViewCellStyle()
		{
			this.propertyStore = new PropertyStore();
			this.scope = DataGridViewCellStyleScopes.None;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0008F98C File Offset: 0x0008DB8C
		public DataGridViewCellStyle(DataGridViewCellStyle dataGridViewCellStyle)
		{
			if (dataGridViewCellStyle == null)
			{
				throw new ArgumentNullException("dataGridViewCellStyle");
			}
			this.propertyStore = new PropertyStore();
			this.scope = DataGridViewCellStyleScopes.None;
			this.BackColor = dataGridViewCellStyle.BackColor;
			this.ForeColor = dataGridViewCellStyle.ForeColor;
			this.SelectionBackColor = dataGridViewCellStyle.SelectionBackColor;
			this.SelectionForeColor = dataGridViewCellStyle.SelectionForeColor;
			this.Font = dataGridViewCellStyle.Font;
			this.NullValue = dataGridViewCellStyle.NullValue;
			this.DataSourceNullValue = dataGridViewCellStyle.DataSourceNullValue;
			this.Format = dataGridViewCellStyle.Format;
			if (!dataGridViewCellStyle.IsFormatProviderDefault)
			{
				this.FormatProvider = dataGridViewCellStyle.FormatProvider;
			}
			this.AlignmentInternal = dataGridViewCellStyle.Alignment;
			this.WrapModeInternal = dataGridViewCellStyle.WrapMode;
			this.Tag = dataGridViewCellStyle.Tag;
			this.PaddingInternal = dataGridViewCellStyle.Padding;
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x0008FA64 File Offset: 0x0008DC64
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x0008FA8C File Offset: 0x0008DC8C
		[SRDescription("DataGridViewCellStyleAlignmentDescr")]
		[DefaultValue(DataGridViewContentAlignment.NotSet)]
		[SRCategory("CatLayout")]
		public DataGridViewContentAlignment Alignment
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(DataGridViewCellStyle.PropAlignment, out flag);
				if (flag)
				{
					return (DataGridViewContentAlignment)integer;
				}
				return DataGridViewContentAlignment.NotSet;
			}
			set
			{
				if (value <= DataGridViewContentAlignment.MiddleCenter)
				{
					if (value <= DataGridViewContentAlignment.TopRight)
					{
						if (value <= DataGridViewContentAlignment.TopCenter || value == DataGridViewContentAlignment.TopRight)
						{
							goto IL_5C;
						}
					}
					else if (value == DataGridViewContentAlignment.MiddleLeft || value == DataGridViewContentAlignment.MiddleCenter)
					{
						goto IL_5C;
					}
				}
				else if (value <= DataGridViewContentAlignment.BottomLeft)
				{
					if (value == DataGridViewContentAlignment.MiddleRight || value == DataGridViewContentAlignment.BottomLeft)
					{
						goto IL_5C;
					}
				}
				else if (value == DataGridViewContentAlignment.BottomCenter || value == DataGridViewContentAlignment.BottomRight)
				{
					goto IL_5C;
				}
				throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewContentAlignment));
				IL_5C:
				this.AlignmentInternal = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x0008FAFC File Offset: 0x0008DCFC
		internal DataGridViewContentAlignment AlignmentInternal
		{
			set
			{
				if (this.Alignment != value)
				{
					this.Properties.SetInteger(DataGridViewCellStyle.PropAlignment, (int)value);
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
				}
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x0008FB1F File Offset: 0x0008DD1F
		// (set) Token: 0x06001E7B RID: 7803 RVA: 0x0008FB34 File Offset: 0x0008DD34
		[SRCategory("CatAppearance")]
		public Color BackColor
		{
			get
			{
				return this.Properties.GetColor(DataGridViewCellStyle.PropBackColor);
			}
			set
			{
				Color backColor = this.BackColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(DataGridViewCellStyle.PropBackColor))
				{
					this.Properties.SetColor(DataGridViewCellStyle.PropBackColor, value);
				}
				if (!backColor.Equals(this.BackColor))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Color);
				}
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x0008FB95 File Offset: 0x0008DD95
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x0008FBC0 File Offset: 0x0008DDC0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object DataSourceNullValue
		{
			get
			{
				if (this.Properties.ContainsObject(DataGridViewCellStyle.PropDataSourceNullValue))
				{
					return this.Properties.GetObject(DataGridViewCellStyle.PropDataSourceNullValue);
				}
				return DBNull.Value;
			}
			set
			{
				object dataSourceNullValue = this.DataSourceNullValue;
				if (dataSourceNullValue == value || (dataSourceNullValue != null && dataSourceNullValue.Equals(value)))
				{
					return;
				}
				if (value == DBNull.Value && this.Properties.ContainsObject(DataGridViewCellStyle.PropDataSourceNullValue))
				{
					this.Properties.RemoveObject(DataGridViewCellStyle.PropDataSourceNullValue);
				}
				else
				{
					this.Properties.SetObject(DataGridViewCellStyle.PropDataSourceNullValue, value);
				}
				this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0008FC29 File Offset: 0x0008DE29
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x0008FC40 File Offset: 0x0008DE40
		[SRCategory("CatAppearance")]
		public Font Font
		{
			get
			{
				return (Font)this.Properties.GetObject(DataGridViewCellStyle.PropFont);
			}
			set
			{
				Font font = this.Font;
				if (value != null || this.Properties.ContainsObject(DataGridViewCellStyle.PropFont))
				{
					this.Properties.SetObject(DataGridViewCellStyle.PropFont, value);
				}
				if ((font == null && value != null) || (font != null && value == null) || (font != null && value != null && !font.Equals(this.Font)))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Font);
				}
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0008FCA1 File Offset: 0x0008DEA1
		// (set) Token: 0x06001E81 RID: 7809 RVA: 0x0008FCB4 File Offset: 0x0008DEB4
		[SRCategory("CatAppearance")]
		public Color ForeColor
		{
			get
			{
				return this.Properties.GetColor(DataGridViewCellStyle.PropForeColor);
			}
			set
			{
				Color foreColor = this.ForeColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(DataGridViewCellStyle.PropForeColor))
				{
					this.Properties.SetColor(DataGridViewCellStyle.PropForeColor, value);
				}
				if (!foreColor.Equals(this.ForeColor))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.ForeColor);
				}
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0008FD18 File Offset: 0x0008DF18
		// (set) Token: 0x06001E83 RID: 7811 RVA: 0x0008FD48 File Offset: 0x0008DF48
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRCategory("CatBehavior")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string Format
		{
			get
			{
				object @object = this.Properties.GetObject(DataGridViewCellStyle.PropFormat);
				if (@object == null)
				{
					return string.Empty;
				}
				return (string)@object;
			}
			set
			{
				string format = this.Format;
				if ((value != null && value.Length > 0) || this.Properties.ContainsObject(DataGridViewCellStyle.PropFormat))
				{
					this.Properties.SetObject(DataGridViewCellStyle.PropFormat, value);
				}
				if (!format.Equals(this.Format))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
				}
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0008FDA0 File Offset: 0x0008DFA0
		// (set) Token: 0x06001E85 RID: 7813 RVA: 0x0008FDD0 File Offset: 0x0008DFD0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IFormatProvider FormatProvider
		{
			get
			{
				object @object = this.Properties.GetObject(DataGridViewCellStyle.PropFormatProvider);
				if (@object == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return (IFormatProvider)@object;
			}
			set
			{
				object @object = this.Properties.GetObject(DataGridViewCellStyle.PropFormatProvider);
				this.Properties.SetObject(DataGridViewCellStyle.PropFormatProvider, value);
				if (value != @object)
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
				}
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0008FE0A File Offset: 0x0008E00A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsDataSourceNullValueDefault
		{
			get
			{
				return !this.Properties.ContainsObject(DataGridViewCellStyle.PropDataSourceNullValue) || this.Properties.GetObject(DataGridViewCellStyle.PropDataSourceNullValue) == DBNull.Value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0008FE37 File Offset: 0x0008E037
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsFormatProviderDefault
		{
			get
			{
				return this.Properties.GetObject(DataGridViewCellStyle.PropFormatProvider) == null;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0008FE4C File Offset: 0x0008E04C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsNullValueDefault
		{
			get
			{
				if (!this.Properties.ContainsObject(DataGridViewCellStyle.PropNullValue))
				{
					return true;
				}
				object @object = this.Properties.GetObject(DataGridViewCellStyle.PropNullValue);
				return @object is string && @object.Equals("");
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0008FE93 File Offset: 0x0008E093
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x0008FEC0 File Offset: 0x0008E0C0
		[DefaultValue("")]
		[TypeConverter(typeof(StringConverter))]
		[SRCategory("CatData")]
		public object NullValue
		{
			get
			{
				if (this.Properties.ContainsObject(DataGridViewCellStyle.PropNullValue))
				{
					return this.Properties.GetObject(DataGridViewCellStyle.PropNullValue);
				}
				return "";
			}
			set
			{
				object nullValue = this.NullValue;
				if (nullValue == value || (nullValue != null && nullValue.Equals(value)))
				{
					return;
				}
				if (value is string && value.Equals("") && this.Properties.ContainsObject(DataGridViewCellStyle.PropNullValue))
				{
					this.Properties.RemoveObject(DataGridViewCellStyle.PropNullValue);
				}
				else
				{
					this.Properties.SetObject(DataGridViewCellStyle.PropNullValue, value);
				}
				this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0008FF36 File Offset: 0x0008E136
		// (set) Token: 0x06001E8C RID: 7820 RVA: 0x0008FF48 File Offset: 0x0008E148
		[SRCategory("CatLayout")]
		public Padding Padding
		{
			get
			{
				return this.Properties.GetPadding(DataGridViewCellStyle.PropPadding);
			}
			set
			{
				if (value.Left < 0 || value.Right < 0 || value.Top < 0 || value.Bottom < 0)
				{
					if (value.All != -1)
					{
						value.All = 0;
					}
					else
					{
						value.Left = Math.Max(0, value.Left);
						value.Right = Math.Max(0, value.Right);
						value.Top = Math.Max(0, value.Top);
						value.Bottom = Math.Max(0, value.Bottom);
					}
				}
				this.PaddingInternal = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x0008FFE8 File Offset: 0x0008E1E8
		internal Padding PaddingInternal
		{
			set
			{
				if (value != this.Padding)
				{
					this.Properties.SetPadding(DataGridViewCellStyle.PropPadding, value);
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
				}
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x00090010 File Offset: 0x0008E210
		internal PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x00090018 File Offset: 0x0008E218
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x00090020 File Offset: 0x0008E220
		internal DataGridViewCellStyleScopes Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x00090029 File Offset: 0x0008E229
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x0009003C File Offset: 0x0008E23C
		[SRCategory("CatAppearance")]
		public Color SelectionBackColor
		{
			get
			{
				return this.Properties.GetColor(DataGridViewCellStyle.PropSelectionBackColor);
			}
			set
			{
				Color selectionBackColor = this.SelectionBackColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(DataGridViewCellStyle.PropSelectionBackColor))
				{
					this.Properties.SetColor(DataGridViewCellStyle.PropSelectionBackColor, value);
				}
				if (!selectionBackColor.Equals(this.SelectionBackColor))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Color);
				}
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0009009D File Offset: 0x0008E29D
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x000900B0 File Offset: 0x0008E2B0
		[SRCategory("CatAppearance")]
		public Color SelectionForeColor
		{
			get
			{
				return this.Properties.GetColor(DataGridViewCellStyle.PropSelectionForeColor);
			}
			set
			{
				Color selectionForeColor = this.SelectionForeColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(DataGridViewCellStyle.PropSelectionForeColor))
				{
					this.Properties.SetColor(DataGridViewCellStyle.PropSelectionForeColor, value);
				}
				if (!selectionForeColor.Equals(this.SelectionForeColor))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Color);
				}
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x00090111 File Offset: 0x0008E311
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x00090123 File Offset: 0x0008E323
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Tag
		{
			get
			{
				return this.Properties.GetObject(DataGridViewCellStyle.PropTag);
			}
			set
			{
				if (value != null || this.Properties.ContainsObject(DataGridViewCellStyle.PropTag))
				{
					this.Properties.SetObject(DataGridViewCellStyle.PropTag, value);
				}
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x0009014C File Offset: 0x0008E34C
		// (set) Token: 0x06001E98 RID: 7832 RVA: 0x00090172 File Offset: 0x0008E372
		[DefaultValue(DataGridViewTriState.NotSet)]
		[SRCategory("CatLayout")]
		public DataGridViewTriState WrapMode
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(DataGridViewCellStyle.PropWrapMode, out flag);
				if (flag)
				{
					return (DataGridViewTriState)integer;
				}
				return DataGridViewTriState.NotSet;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewTriState));
				}
				this.WrapModeInternal = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (set) Token: 0x06001E99 RID: 7833 RVA: 0x000901A1 File Offset: 0x0008E3A1
		internal DataGridViewTriState WrapModeInternal
		{
			set
			{
				if (this.WrapMode != value)
				{
					this.Properties.SetInteger(DataGridViewCellStyle.PropWrapMode, (int)value);
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
				}
			}
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000901C4 File Offset: 0x0008E3C4
		internal void AddScope(DataGridView dataGridView, DataGridViewCellStyleScopes scope)
		{
			this.scope |= scope;
			this.dataGridView = dataGridView;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x000901DC File Offset: 0x0008E3DC
		public virtual void ApplyStyle(DataGridViewCellStyle dataGridViewCellStyle)
		{
			if (dataGridViewCellStyle == null)
			{
				throw new ArgumentNullException("dataGridViewCellStyle");
			}
			if (!dataGridViewCellStyle.BackColor.IsEmpty)
			{
				this.BackColor = dataGridViewCellStyle.BackColor;
			}
			if (!dataGridViewCellStyle.ForeColor.IsEmpty)
			{
				this.ForeColor = dataGridViewCellStyle.ForeColor;
			}
			if (!dataGridViewCellStyle.SelectionBackColor.IsEmpty)
			{
				this.SelectionBackColor = dataGridViewCellStyle.SelectionBackColor;
			}
			if (!dataGridViewCellStyle.SelectionForeColor.IsEmpty)
			{
				this.SelectionForeColor = dataGridViewCellStyle.SelectionForeColor;
			}
			if (dataGridViewCellStyle.Font != null)
			{
				this.Font = dataGridViewCellStyle.Font;
			}
			if (!dataGridViewCellStyle.IsNullValueDefault)
			{
				this.NullValue = dataGridViewCellStyle.NullValue;
			}
			if (!dataGridViewCellStyle.IsDataSourceNullValueDefault)
			{
				this.DataSourceNullValue = dataGridViewCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle.Format.Length != 0)
			{
				this.Format = dataGridViewCellStyle.Format;
			}
			if (!dataGridViewCellStyle.IsFormatProviderDefault)
			{
				this.FormatProvider = dataGridViewCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				this.AlignmentInternal = dataGridViewCellStyle.Alignment;
			}
			if (dataGridViewCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				this.WrapModeInternal = dataGridViewCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle.Tag != null)
			{
				this.Tag = dataGridViewCellStyle.Tag;
			}
			if (dataGridViewCellStyle.Padding != Padding.Empty)
			{
				this.PaddingInternal = dataGridViewCellStyle.Padding;
			}
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x0009032A File Offset: 0x0008E52A
		public virtual DataGridViewCellStyle Clone()
		{
			return new DataGridViewCellStyle(this);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00090334 File Offset: 0x0008E534
		public override bool Equals(object o)
		{
			DataGridViewCellStyle dataGridViewCellStyle = o as DataGridViewCellStyle;
			return dataGridViewCellStyle != null && this.GetDifferencesFrom(dataGridViewCellStyle) == DataGridViewCellStyleDifferences.None;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00090358 File Offset: 0x0008E558
		internal DataGridViewCellStyleDifferences GetDifferencesFrom(DataGridViewCellStyle dgvcs)
		{
			bool flag = dgvcs.Alignment != this.Alignment || dgvcs.DataSourceNullValue != this.DataSourceNullValue || dgvcs.Font != this.Font || dgvcs.Format != this.Format || dgvcs.FormatProvider != this.FormatProvider || dgvcs.NullValue != this.NullValue || dgvcs.Padding != this.Padding || dgvcs.Tag != this.Tag || dgvcs.WrapMode != this.WrapMode;
			bool flag2 = dgvcs.BackColor != this.BackColor || dgvcs.ForeColor != this.ForeColor || dgvcs.SelectionBackColor != this.SelectionBackColor || dgvcs.SelectionForeColor != this.SelectionForeColor;
			if (flag)
			{
				return DataGridViewCellStyleDifferences.AffectPreferredSize;
			}
			if (flag2)
			{
				return DataGridViewCellStyleDifferences.DoNotAffectPreferredSize;
			}
			return DataGridViewCellStyleDifferences.None;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00090450 File Offset: 0x0008E650
		public override int GetHashCode()
		{
			return WindowsFormsUtils.GetCombinedHashCodes(new int[]
			{
				(int)this.Alignment,
				(int)this.WrapMode,
				this.Padding.GetHashCode(),
				this.Format.GetHashCode(),
				this.BackColor.GetHashCode(),
				this.ForeColor.GetHashCode(),
				this.SelectionBackColor.GetHashCode(),
				this.SelectionForeColor.GetHashCode(),
				(this.Font == null) ? 1 : this.Font.GetHashCode(),
				(this.NullValue == null) ? 1 : this.NullValue.GetHashCode(),
				(this.DataSourceNullValue == null) ? 1 : this.DataSourceNullValue.GetHashCode(),
				(this.Tag == null) ? 1 : this.Tag.GetHashCode()
			});
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00090563 File Offset: 0x0008E763
		private void OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal property)
		{
			if (this.dataGridView != null && this.scope != DataGridViewCellStyleScopes.None)
			{
				this.dataGridView.OnCellStyleContentChanged(this, property);
			}
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00090582 File Offset: 0x0008E782
		internal void RemoveScope(DataGridViewCellStyleScopes scope)
		{
			this.scope &= ~scope;
			if (this.scope == DataGridViewCellStyleScopes.None)
			{
				this.dataGridView = null;
			}
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000905A4 File Offset: 0x0008E7A4
		private bool ShouldSerializeBackColor()
		{
			bool result;
			this.Properties.GetColor(DataGridViewCellStyle.PropBackColor, out result);
			return result;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000905C5 File Offset: 0x0008E7C5
		private bool ShouldSerializeFont()
		{
			return this.Properties.GetObject(DataGridViewCellStyle.PropFont) != null;
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x000905DC File Offset: 0x0008E7DC
		private bool ShouldSerializeForeColor()
		{
			bool result;
			this.Properties.GetColor(DataGridViewCellStyle.PropForeColor, out result);
			return result;
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x000905FD File Offset: 0x0008E7FD
		private bool ShouldSerializeFormatProvider()
		{
			return this.Properties.GetObject(DataGridViewCellStyle.PropFormatProvider) != null;
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x00090612 File Offset: 0x0008E812
		private bool ShouldSerializePadding()
		{
			return this.Padding != Padding.Empty;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00090624 File Offset: 0x0008E824
		private bool ShouldSerializeSelectionBackColor()
		{
			bool result;
			this.Properties.GetObject(DataGridViewCellStyle.PropSelectionBackColor, out result);
			return result;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00090648 File Offset: 0x0008E848
		private bool ShouldSerializeSelectionForeColor()
		{
			bool result;
			this.Properties.GetColor(DataGridViewCellStyle.PropSelectionForeColor, out result);
			return result;
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0009066C File Offset: 0x0008E86C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append("DataGridViewCellStyle {");
			bool flag = true;
			if (this.BackColor != Color.Empty)
			{
				stringBuilder.Append(" BackColor=" + this.BackColor.ToString());
				flag = false;
			}
			if (this.ForeColor != Color.Empty)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" ForeColor=" + this.ForeColor.ToString());
				flag = false;
			}
			if (this.SelectionBackColor != Color.Empty)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" SelectionBackColor=" + this.SelectionBackColor.ToString());
				flag = false;
			}
			if (this.SelectionForeColor != Color.Empty)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" SelectionForeColor=" + this.SelectionForeColor.ToString());
				flag = false;
			}
			if (this.Font != null)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" Font=" + this.Font.ToString());
				flag = false;
			}
			if (!this.IsNullValueDefault && this.NullValue != null)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" NullValue=" + this.NullValue.ToString());
				flag = false;
			}
			if (!this.IsDataSourceNullValueDefault && this.DataSourceNullValue != null)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" DataSourceNullValue=" + this.DataSourceNullValue.ToString());
				flag = false;
			}
			if (!string.IsNullOrEmpty(this.Format))
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" Format=" + this.Format);
				flag = false;
			}
			if (this.WrapMode != DataGridViewTriState.NotSet)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" WrapMode=" + this.WrapMode.ToString());
				flag = false;
			}
			if (this.Alignment != DataGridViewContentAlignment.NotSet)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" Alignment=" + this.Alignment.ToString());
				flag = false;
			}
			if (this.Padding != Padding.Empty)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" Padding=" + this.Padding.ToString());
				flag = false;
			}
			if (this.Tag != null)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" Tag=" + this.Tag.ToString());
			}
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00090994 File Offset: 0x0008EB94
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x04000CEA RID: 3306
		private static readonly int PropAlignment = PropertyStore.CreateKey();

		// Token: 0x04000CEB RID: 3307
		private static readonly int PropBackColor = PropertyStore.CreateKey();

		// Token: 0x04000CEC RID: 3308
		private static readonly int PropDataSourceNullValue = PropertyStore.CreateKey();

		// Token: 0x04000CED RID: 3309
		private static readonly int PropFont = PropertyStore.CreateKey();

		// Token: 0x04000CEE RID: 3310
		private static readonly int PropForeColor = PropertyStore.CreateKey();

		// Token: 0x04000CEF RID: 3311
		private static readonly int PropFormat = PropertyStore.CreateKey();

		// Token: 0x04000CF0 RID: 3312
		private static readonly int PropFormatProvider = PropertyStore.CreateKey();

		// Token: 0x04000CF1 RID: 3313
		private static readonly int PropNullValue = PropertyStore.CreateKey();

		// Token: 0x04000CF2 RID: 3314
		private static readonly int PropPadding = PropertyStore.CreateKey();

		// Token: 0x04000CF3 RID: 3315
		private static readonly int PropSelectionBackColor = PropertyStore.CreateKey();

		// Token: 0x04000CF4 RID: 3316
		private static readonly int PropSelectionForeColor = PropertyStore.CreateKey();

		// Token: 0x04000CF5 RID: 3317
		private static readonly int PropTag = PropertyStore.CreateKey();

		// Token: 0x04000CF6 RID: 3318
		private static readonly int PropWrapMode = PropertyStore.CreateKey();

		// Token: 0x04000CF7 RID: 3319
		private const string DATAGRIDVIEWCELLSTYLE_nullText = "";

		// Token: 0x04000CF8 RID: 3320
		private DataGridViewCellStyleScopes scope;

		// Token: 0x04000CF9 RID: 3321
		private PropertyStore propertyStore;

		// Token: 0x04000CFA RID: 3322
		private DataGridView dataGridView;

		// Token: 0x02000668 RID: 1640
		internal enum DataGridViewCellStylePropertyInternal
		{
			// Token: 0x04003A64 RID: 14948
			Color,
			// Token: 0x04003A65 RID: 14949
			Other,
			// Token: 0x04003A66 RID: 14950
			Font,
			// Token: 0x04003A67 RID: 14951
			ForeColor
		}
	}
}
