using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x020001BD RID: 445
	[ToolboxBitmap(typeof(DataGridViewCheckBoxColumn), "DataGridViewCheckBoxColumn.bmp")]
	public class DataGridViewCheckBoxColumn : DataGridViewColumn
	{
		// Token: 0x06001F03 RID: 7939 RVA: 0x00092EA9 File Offset: 0x000910A9
		public DataGridViewCheckBoxColumn() : this(false)
		{
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00092EB4 File Offset: 0x000910B4
		public DataGridViewCheckBoxColumn(bool threeState) : base(new DataGridViewCheckBoxCell(threeState))
		{
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			dataGridViewCellStyle.AlignmentInternal = DataGridViewContentAlignment.MiddleCenter;
			if (threeState)
			{
				dataGridViewCellStyle.NullValue = CheckState.Indeterminate;
			}
			else
			{
				dataGridViewCellStyle.NullValue = false;
			}
			this.DefaultCellStyle = dataGridViewCellStyle;
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x000893F9 File Offset: 0x000875F9
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x00092EFF File Offset: 0x000910FF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				if (value != null && !(value is DataGridViewCheckBoxCell))
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[]
					{
						"System.Windows.Forms.DataGridViewCheckBoxCell"
					}));
				}
				base.CellTemplate = value;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x00092F31 File Offset: 0x00091131
		private DataGridViewCheckBoxCell CheckBoxCellTemplate
		{
			get
			{
				return (DataGridViewCheckBoxCell)this.CellTemplate;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x00089433 File Offset: 0x00087633
		// (set) Token: 0x06001F09 RID: 7945 RVA: 0x0008943B File Offset: 0x0008763B
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

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x00092F3E File Offset: 0x0009113E
		// (set) Token: 0x06001F0B RID: 7947 RVA: 0x00092F64 File Offset: 0x00091164
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_CheckBoxColumnFalseValueDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object FalseValue
		{
			get
			{
				if (this.CheckBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.CheckBoxCellTemplate.FalseValue;
			}
			set
			{
				if (this.FalseValue != value)
				{
					this.CheckBoxCellTemplate.FalseValueInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell != null)
							{
								dataGridViewCheckBoxCell.FalseValueInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x00092FEA File Offset: 0x000911EA
		// (set) Token: 0x06001F0D RID: 7949 RVA: 0x00093010 File Offset: 0x00091210
		[DefaultValue(FlatStyle.Standard)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_CheckBoxColumnFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				if (this.CheckBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.CheckBoxCellTemplate.FlatStyle;
			}
			set
			{
				if (this.FlatStyle != value)
				{
					this.CheckBoxCellTemplate.FlatStyle = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell != null)
							{
								dataGridViewCheckBoxCell.FlatStyleInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00093096 File Offset: 0x00091296
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x000930BC File Offset: 0x000912BC
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_CheckBoxColumnIndeterminateValueDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object IndeterminateValue
		{
			get
			{
				if (this.CheckBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.CheckBoxCellTemplate.IndeterminateValue;
			}
			set
			{
				if (this.IndeterminateValue != value)
				{
					this.CheckBoxCellTemplate.IndeterminateValueInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell != null)
							{
								dataGridViewCheckBoxCell.IndeterminateValueInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00093142 File Offset: 0x00091342
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00093168 File Offset: 0x00091368
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_CheckBoxColumnThreeStateDescr")]
		public bool ThreeState
		{
			get
			{
				if (this.CheckBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.CheckBoxCellTemplate.ThreeState;
			}
			set
			{
				if (this.ThreeState != value)
				{
					this.CheckBoxCellTemplate.ThreeStateInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell != null)
							{
								dataGridViewCheckBoxCell.ThreeStateInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
					if (value && this.DefaultCellStyle.NullValue is bool && !(bool)this.DefaultCellStyle.NullValue)
					{
						this.DefaultCellStyle.NullValue = CheckState.Indeterminate;
						return;
					}
					if (!value && this.DefaultCellStyle.NullValue is CheckState && (CheckState)this.DefaultCellStyle.NullValue == CheckState.Indeterminate)
					{
						this.DefaultCellStyle.NullValue = false;
					}
				}
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x00093263 File Offset: 0x00091463
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x00093288 File Offset: 0x00091488
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_CheckBoxColumnTrueValueDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object TrueValue
		{
			get
			{
				if (this.CheckBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.CheckBoxCellTemplate.TrueValue;
			}
			set
			{
				if (this.TrueValue != value)
				{
					this.CheckBoxCellTemplate.TrueValueInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewCheckBoxCell dataGridViewCheckBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewCheckBoxCell;
							if (dataGridViewCheckBoxCell != null)
							{
								dataGridViewCheckBoxCell.TrueValueInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00093310 File Offset: 0x00091510
		private bool ShouldSerializeDefaultCellStyle()
		{
			DataGridViewCheckBoxCell dataGridViewCheckBoxCell = this.CellTemplate as DataGridViewCheckBoxCell;
			if (dataGridViewCheckBoxCell == null)
			{
				return true;
			}
			object obj;
			if (dataGridViewCheckBoxCell.ThreeState)
			{
				obj = CheckState.Indeterminate;
			}
			else
			{
				obj = false;
			}
			if (!base.HasDefaultCellStyle)
			{
				return false;
			}
			DataGridViewCellStyle defaultCellStyle = this.DefaultCellStyle;
			return !defaultCellStyle.BackColor.IsEmpty || !defaultCellStyle.ForeColor.IsEmpty || !defaultCellStyle.SelectionBackColor.IsEmpty || !defaultCellStyle.SelectionForeColor.IsEmpty || defaultCellStyle.Font != null || !defaultCellStyle.NullValue.Equals(obj) || !defaultCellStyle.IsDataSourceNullValueDefault || !string.IsNullOrEmpty(defaultCellStyle.Format) || !defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) || defaultCellStyle.Alignment != DataGridViewContentAlignment.MiddleCenter || defaultCellStyle.WrapMode != DataGridViewTriState.NotSet || defaultCellStyle.Tag != null || !defaultCellStyle.Padding.Equals(Padding.Empty);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0009341C File Offset: 0x0009161C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewCheckBoxColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}
	}
}
