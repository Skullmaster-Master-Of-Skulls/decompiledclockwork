using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x020001C9 RID: 457
	[Designer("System.Windows.Forms.Design.DataGridViewComboBoxColumnDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxBitmap(typeof(DataGridViewComboBoxColumn), "DataGridViewComboBoxColumn.bmp")]
	public class DataGridViewComboBoxColumn : DataGridViewColumn
	{
		// Token: 0x06002023 RID: 8227 RVA: 0x0009ADC1 File Offset: 0x00098FC1
		public DataGridViewComboBoxColumn() : base(new DataGridViewComboBoxCell())
		{
			((DataGridViewComboBoxCell)base.CellTemplate).TemplateComboBoxColumn = this;
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0009ADDF File Offset: 0x00098FDF
		// (set) Token: 0x06002025 RID: 8229 RVA: 0x0009AE04 File Offset: 0x00099004
		[Browsable(true)]
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ComboBoxColumnAutoCompleteDescr")]
		public bool AutoComplete
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.AutoComplete;
			}
			set
			{
				if (this.AutoComplete != value)
				{
					this.ComboBoxCellTemplate.AutoComplete = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
							if (dataGridViewComboBoxCell != null)
							{
								dataGridViewComboBoxCell.AutoComplete = value;
							}
						}
					}
				}
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x000893F9 File Offset: 0x000875F9
		// (set) Token: 0x06002027 RID: 8231 RVA: 0x0009AE7C File Offset: 0x0009907C
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
				DataGridViewComboBoxCell dataGridViewComboBoxCell = value as DataGridViewComboBoxCell;
				if (value != null && dataGridViewComboBoxCell == null)
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[]
					{
						"System.Windows.Forms.DataGridViewComboBoxCell"
					}));
				}
				base.CellTemplate = value;
				if (value != null)
				{
					dataGridViewComboBoxCell.TemplateComboBoxColumn = this;
				}
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x0009AEC5 File Offset: 0x000990C5
		private DataGridViewComboBoxCell ComboBoxCellTemplate
		{
			get
			{
				return (DataGridViewComboBoxCell)this.CellTemplate;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x0009AED2 File Offset: 0x000990D2
		// (set) Token: 0x0600202A RID: 8234 RVA: 0x0009AEF8 File Offset: 0x000990F8
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_ComboBoxColumnDataSourceDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[AttributeProvider(typeof(IListSource))]
		public object DataSource
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.DataSource;
			}
			set
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ComboBoxCellTemplate.DataSource = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
						if (dataGridViewComboBoxCell != null)
						{
							dataGridViewComboBoxCell.DataSource = value;
						}
					}
					base.DataGridView.OnColumnCommonChange(base.Index);
				}
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x0009AF8D File Offset: 0x0009918D
		// (set) Token: 0x0600202C RID: 8236 RVA: 0x0009AFB4 File Offset: 0x000991B4
		[DefaultValue("")]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_ComboBoxColumnDisplayMemberDescr")]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string DisplayMember
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.DisplayMember;
			}
			set
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ComboBoxCellTemplate.DisplayMember = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
						if (dataGridViewComboBoxCell != null)
						{
							dataGridViewComboBoxCell.DisplayMember = value;
						}
					}
					base.DataGridView.OnColumnCommonChange(base.Index);
				}
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x0009B049 File Offset: 0x00099249
		// (set) Token: 0x0600202E RID: 8238 RVA: 0x0009B070 File Offset: 0x00099270
		[DefaultValue(DataGridViewComboBoxDisplayStyle.DropDownButton)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ComboBoxColumnDisplayStyleDescr")]
		public DataGridViewComboBoxDisplayStyle DisplayStyle
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.DisplayStyle;
			}
			set
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ComboBoxCellTemplate.DisplayStyle = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
						if (dataGridViewComboBoxCell != null)
						{
							dataGridViewComboBoxCell.DisplayStyleInternal = value;
						}
					}
					base.DataGridView.InvalidateColumn(base.Index);
				}
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x0009B105 File Offset: 0x00099305
		// (set) Token: 0x06002030 RID: 8240 RVA: 0x0009B12C File Offset: 0x0009932C
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ComboBoxColumnDisplayStyleForCurrentCellOnlyDescr")]
		public bool DisplayStyleForCurrentCellOnly
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.DisplayStyleForCurrentCellOnly;
			}
			set
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ComboBoxCellTemplate.DisplayStyleForCurrentCellOnly = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
						if (dataGridViewComboBoxCell != null)
						{
							dataGridViewComboBoxCell.DisplayStyleForCurrentCellOnlyInternal = value;
						}
					}
					base.DataGridView.InvalidateColumn(base.Index);
				}
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x0009B1C1 File Offset: 0x000993C1
		// (set) Token: 0x06002032 RID: 8242 RVA: 0x0009B1E8 File Offset: 0x000993E8
		[DefaultValue(1)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ComboBoxColumnDropDownWidthDescr")]
		public int DropDownWidth
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.DropDownWidth;
			}
			set
			{
				if (this.DropDownWidth != value)
				{
					this.ComboBoxCellTemplate.DropDownWidth = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
							if (dataGridViewComboBoxCell != null)
							{
								dataGridViewComboBoxCell.DropDownWidth = value;
							}
						}
					}
				}
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x0009B25D File Offset: 0x0009945D
		// (set) Token: 0x06002034 RID: 8244 RVA: 0x0009B288 File Offset: 0x00099488
		[DefaultValue(FlatStyle.Standard)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ComboBoxColumnFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewComboBoxCell)this.CellTemplate).FlatStyle;
			}
			set
			{
				if (this.FlatStyle != value)
				{
					((DataGridViewComboBoxCell)this.CellTemplate).FlatStyle = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
							if (dataGridViewComboBoxCell != null)
							{
								dataGridViewComboBoxCell.FlatStyleInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x0009B313 File Offset: 0x00099513
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_ComboBoxColumnItemsDescr")]
		public DataGridViewComboBoxCell.ObjectCollection Items
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.GetItems(base.DataGridView);
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x0009B33E File Offset: 0x0009953E
		// (set) Token: 0x06002037 RID: 8247 RVA: 0x0009B364 File Offset: 0x00099564
		[DefaultValue("")]
		[SRCategory("CatData")]
		[SRDescription("DataGridView_ComboBoxColumnValueMemberDescr")]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ValueMember
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.ValueMember;
			}
			set
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ComboBoxCellTemplate.ValueMember = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
						if (dataGridViewComboBoxCell != null)
						{
							dataGridViewComboBoxCell.ValueMember = value;
						}
					}
					base.DataGridView.OnColumnCommonChange(base.Index);
				}
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x0009B3F9 File Offset: 0x000995F9
		// (set) Token: 0x06002039 RID: 8249 RVA: 0x0009B420 File Offset: 0x00099620
		[DefaultValue(8)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ComboBoxColumnMaxDropDownItemsDescr")]
		public int MaxDropDownItems
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.MaxDropDownItems;
			}
			set
			{
				if (this.MaxDropDownItems != value)
				{
					this.ComboBoxCellTemplate.MaxDropDownItems = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
							if (dataGridViewComboBoxCell != null)
							{
								dataGridViewComboBoxCell.MaxDropDownItems = value;
							}
						}
					}
				}
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x0009B495 File Offset: 0x00099695
		// (set) Token: 0x0600203B RID: 8251 RVA: 0x0009B4BC File Offset: 0x000996BC
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_ComboBoxColumnSortedDescr")]
		public bool Sorted
		{
			get
			{
				if (this.ComboBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ComboBoxCellTemplate.Sorted;
			}
			set
			{
				if (this.Sorted != value)
				{
					this.ComboBoxCellTemplate.Sorted = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
							if (dataGridViewComboBoxCell != null)
							{
								dataGridViewComboBoxCell.Sorted = value;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0009B534 File Offset: 0x00099734
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewComboBoxColumn dataGridViewComboBoxColumn;
			if (type == DataGridViewComboBoxColumn.columnType)
			{
				dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn();
			}
			else
			{
				dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)Activator.CreateInstance(type);
			}
			if (dataGridViewComboBoxColumn != null)
			{
				base.CloneInternal(dataGridViewComboBoxColumn);
				((DataGridViewComboBoxCell)dataGridViewComboBoxColumn.CellTemplate).TemplateComboBoxColumn = dataGridViewComboBoxColumn;
			}
			return dataGridViewComboBoxColumn;
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x0009B588 File Offset: 0x00099788
		internal void OnItemsCollectionChanged()
		{
			if (base.DataGridView != null)
			{
				DataGridViewRowCollection rows = base.DataGridView.Rows;
				int count = rows.Count;
				object[] items = ((DataGridViewComboBoxCell)this.CellTemplate).Items.InnerArray.ToArray();
				for (int i = 0; i < count; i++)
				{
					DataGridViewRow dataGridViewRow = rows.SharedRow(i);
					DataGridViewComboBoxCell dataGridViewComboBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewComboBoxCell;
					if (dataGridViewComboBoxCell != null)
					{
						dataGridViewComboBoxCell.Items.ClearInternal();
						dataGridViewComboBoxCell.Items.AddRangeInternal(items);
					}
				}
				base.DataGridView.OnColumnCommonChange(base.Index);
			}
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x0009B62C File Offset: 0x0009982C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewComboBoxColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000D92 RID: 3474
		private static Type columnType = typeof(DataGridViewComboBoxColumn);
	}
}
