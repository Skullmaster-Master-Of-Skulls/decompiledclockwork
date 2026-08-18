using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x020001A2 RID: 418
	[ToolboxBitmap(typeof(DataGridViewButtonColumn), "DataGridViewButtonColumn.bmp")]
	public class DataGridViewButtonColumn : DataGridViewColumn
	{
		// Token: 0x06001D22 RID: 7458 RVA: 0x000893CC File Offset: 0x000875CC
		public DataGridViewButtonColumn() : base(new DataGridViewButtonCell())
		{
			this.DefaultCellStyle = new DataGridViewCellStyle
			{
				AlignmentInternal = DataGridViewContentAlignment.MiddleCenter
			};
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x000893F9 File Offset: 0x000875F9
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x00089401 File Offset: 0x00087601
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
				if (value != null && !(value is DataGridViewButtonCell))
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[]
					{
						"System.Windows.Forms.DataGridViewButtonCell"
					}));
				}
				base.CellTemplate = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x00089433 File Offset: 0x00087633
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x0008943B File Offset: 0x0008763B
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

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x00089444 File Offset: 0x00087644
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x00089470 File Offset: 0x00087670
		[DefaultValue(FlatStyle.Standard)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ButtonColumnFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewButtonCell)this.CellTemplate).FlatStyle;
			}
			set
			{
				if (this.FlatStyle != value)
				{
					((DataGridViewButtonCell)this.CellTemplate).FlatStyle = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewButtonCell dataGridViewButtonCell = dataGridViewRow.Cells[base.Index] as DataGridViewButtonCell;
							if (dataGridViewButtonCell != null)
							{
								dataGridViewButtonCell.FlatStyleInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x000894FB File Offset: 0x000876FB
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x00089504 File Offset: 0x00087704
		[DefaultValue(null)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ButtonColumnTextDescr")]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (!string.Equals(value, this.text, StringComparison.Ordinal))
				{
					this.text = value;
					if (base.DataGridView != null)
					{
						if (this.UseColumnTextForButtonValue)
						{
							base.DataGridView.OnColumnCommonChange(base.Index);
							return;
						}
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewButtonCell dataGridViewButtonCell = dataGridViewRow.Cells[base.Index] as DataGridViewButtonCell;
							if (dataGridViewButtonCell != null && dataGridViewButtonCell.UseColumnTextForButtonValue)
							{
								base.DataGridView.OnColumnCommonChange(base.Index);
								return;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x000895BE File Offset: 0x000877BE
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x000895E8 File Offset: 0x000877E8
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_ButtonColumnUseColumnTextForButtonValueDescr")]
		public bool UseColumnTextForButtonValue
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewButtonCell)this.CellTemplate).UseColumnTextForButtonValue;
			}
			set
			{
				if (this.UseColumnTextForButtonValue != value)
				{
					((DataGridViewButtonCell)this.CellTemplate).UseColumnTextForButtonValueInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewButtonCell dataGridViewButtonCell = dataGridViewRow.Cells[base.Index] as DataGridViewButtonCell;
							if (dataGridViewButtonCell != null)
							{
								dataGridViewButtonCell.UseColumnTextForButtonValueInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00089674 File Offset: 0x00087874
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewButtonColumn dataGridViewButtonColumn;
			if (type == DataGridViewButtonColumn.columnType)
			{
				dataGridViewButtonColumn = new DataGridViewButtonColumn();
			}
			else
			{
				dataGridViewButtonColumn = (DataGridViewButtonColumn)Activator.CreateInstance(type);
			}
			if (dataGridViewButtonColumn != null)
			{
				base.CloneInternal(dataGridViewButtonColumn);
				dataGridViewButtonColumn.Text = this.text;
			}
			return dataGridViewButtonColumn;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000896C0 File Offset: 0x000878C0
		private bool ShouldSerializeDefaultCellStyle()
		{
			if (!base.HasDefaultCellStyle)
			{
				return false;
			}
			DataGridViewCellStyle defaultCellStyle = this.DefaultCellStyle;
			return !defaultCellStyle.BackColor.IsEmpty || !defaultCellStyle.ForeColor.IsEmpty || !defaultCellStyle.SelectionBackColor.IsEmpty || !defaultCellStyle.SelectionForeColor.IsEmpty || defaultCellStyle.Font != null || !defaultCellStyle.IsNullValueDefault || !defaultCellStyle.IsDataSourceNullValueDefault || !string.IsNullOrEmpty(defaultCellStyle.Format) || !defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) || defaultCellStyle.Alignment != DataGridViewContentAlignment.MiddleCenter || defaultCellStyle.WrapMode != DataGridViewTriState.NotSet || defaultCellStyle.Tag != null || !defaultCellStyle.Padding.Equals(Padding.Empty);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0008979C File Offset: 0x0008799C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewButtonColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000C92 RID: 3218
		private static Type columnType = typeof(DataGridViewButtonColumn);

		// Token: 0x04000C93 RID: 3219
		private string text;
	}
}
