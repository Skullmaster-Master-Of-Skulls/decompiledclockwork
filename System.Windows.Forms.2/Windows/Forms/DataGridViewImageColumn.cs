using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000201 RID: 513
	[ToolboxBitmap(typeof(DataGridViewImageColumn), "DataGridViewImageColumn.bmp")]
	public class DataGridViewImageColumn : DataGridViewColumn
	{
		// Token: 0x0600215A RID: 8538 RVA: 0x0009D899 File Offset: 0x0009BA99
		public DataGridViewImageColumn() : this(false)
		{
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0009D8A4 File Offset: 0x0009BAA4
		public DataGridViewImageColumn(bool valuesAreIcons) : base(new DataGridViewImageCell(valuesAreIcons))
		{
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			dataGridViewCellStyle.AlignmentInternal = DataGridViewContentAlignment.MiddleCenter;
			if (valuesAreIcons)
			{
				dataGridViewCellStyle.NullValue = DataGridViewImageCell.ErrorIcon;
			}
			else
			{
				dataGridViewCellStyle.NullValue = DataGridViewImageCell.ErrorBitmap;
			}
			this.DefaultCellStyle = dataGridViewCellStyle;
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x000893F9 File Offset: 0x000875F9
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x0009D8ED File Offset: 0x0009BAED
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
				if (value != null && !(value is DataGridViewImageCell))
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[]
					{
						"System.Windows.Forms.DataGridViewImageCell"
					}));
				}
				base.CellTemplate = value;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x00089433 File Offset: 0x00087633
		// (set) Token: 0x0600215F RID: 8543 RVA: 0x0008943B File Offset: 0x0008763B
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

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x0009D91F File Offset: 0x0009BB1F
		// (set) Token: 0x06002161 RID: 8545 RVA: 0x0009D944 File Offset: 0x0009BB44
		[Browsable(true)]
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridViewImageColumn_DescriptionDescr")]
		public string Description
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ImageCellTemplate.Description;
			}
			set
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				this.ImageCellTemplate.Description = value;
				if (base.DataGridView != null)
				{
					DataGridViewRowCollection rows = base.DataGridView.Rows;
					int count = rows.Count;
					for (int i = 0; i < count; i++)
					{
						DataGridViewRow dataGridViewRow = rows.SharedRow(i);
						DataGridViewImageCell dataGridViewImageCell = dataGridViewRow.Cells[base.Index] as DataGridViewImageCell;
						if (dataGridViewImageCell != null)
						{
							dataGridViewImageCell.Description = value;
						}
					}
				}
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x0009D9C8 File Offset: 0x0009BBC8
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x0009D9D0 File Offset: 0x0009BBD0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnCommonChange(base.Index);
				}
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x0009D9F2 File Offset: 0x0009BBF2
		// (set) Token: 0x06002165 RID: 8549 RVA: 0x0009D9FA File Offset: 0x0009BBFA
		[DefaultValue(null)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridViewImageColumn_ImageDescr")]
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.image = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnCommonChange(base.Index);
				}
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x0009DA1C File Offset: 0x0009BC1C
		private DataGridViewImageCell ImageCellTemplate
		{
			get
			{
				return (DataGridViewImageCell)this.CellTemplate;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x0009DA2C File Offset: 0x0009BC2C
		// (set) Token: 0x06002168 RID: 8552 RVA: 0x0009DA64 File Offset: 0x0009BC64
		[DefaultValue(DataGridViewImageCellLayout.Normal)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridViewImageColumn_ImageLayoutDescr")]
		public DataGridViewImageCellLayout ImageLayout
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				DataGridViewImageCellLayout dataGridViewImageCellLayout = this.ImageCellTemplate.ImageLayout;
				if (dataGridViewImageCellLayout == DataGridViewImageCellLayout.NotSet)
				{
					dataGridViewImageCellLayout = DataGridViewImageCellLayout.Normal;
				}
				return dataGridViewImageCellLayout;
			}
			set
			{
				if (this.ImageLayout != value)
				{
					this.ImageCellTemplate.ImageLayout = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewImageCell dataGridViewImageCell = dataGridViewRow.Cells[base.Index] as DataGridViewImageCell;
							if (dataGridViewImageCell != null)
							{
								dataGridViewImageCell.ImageLayoutInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x0009DAEA File Offset: 0x0009BCEA
		// (set) Token: 0x0600216A RID: 8554 RVA: 0x0009DB10 File Offset: 0x0009BD10
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ValuesAreIcons
		{
			get
			{
				if (this.ImageCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.ImageCellTemplate.ValueIsIcon;
			}
			set
			{
				if (this.ValuesAreIcons != value)
				{
					this.ImageCellTemplate.ValueIsIconInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewImageCell dataGridViewImageCell = dataGridViewRow.Cells[base.Index] as DataGridViewImageCell;
							if (dataGridViewImageCell != null)
							{
								dataGridViewImageCell.ValueIsIconInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
					if (value && this.DefaultCellStyle.NullValue is Bitmap && (Bitmap)this.DefaultCellStyle.NullValue == DataGridViewImageCell.ErrorBitmap)
					{
						this.DefaultCellStyle.NullValue = DataGridViewImageCell.ErrorIcon;
						return;
					}
					if (!value && this.DefaultCellStyle.NullValue is Icon && (Icon)this.DefaultCellStyle.NullValue == DataGridViewImageCell.ErrorIcon)
					{
						this.DefaultCellStyle.NullValue = DataGridViewImageCell.ErrorBitmap;
					}
				}
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0009DC14 File Offset: 0x0009BE14
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewImageColumn dataGridViewImageColumn;
			if (type == DataGridViewImageColumn.columnType)
			{
				dataGridViewImageColumn = new DataGridViewImageColumn();
			}
			else
			{
				dataGridViewImageColumn = (DataGridViewImageColumn)Activator.CreateInstance(type);
			}
			if (dataGridViewImageColumn != null)
			{
				base.CloneInternal(dataGridViewImageColumn);
				dataGridViewImageColumn.Icon = this.icon;
				dataGridViewImageColumn.Image = this.image;
			}
			return dataGridViewImageColumn;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0009DC6C File Offset: 0x0009BE6C
		private bool ShouldSerializeDefaultCellStyle()
		{
			DataGridViewImageCell dataGridViewImageCell = this.CellTemplate as DataGridViewImageCell;
			if (dataGridViewImageCell == null)
			{
				return true;
			}
			if (!base.HasDefaultCellStyle)
			{
				return false;
			}
			object obj;
			if (dataGridViewImageCell.ValueIsIcon)
			{
				obj = DataGridViewImageCell.ErrorIcon;
			}
			else
			{
				obj = DataGridViewImageCell.ErrorBitmap;
			}
			DataGridViewCellStyle defaultCellStyle = this.DefaultCellStyle;
			return !defaultCellStyle.BackColor.IsEmpty || !defaultCellStyle.ForeColor.IsEmpty || !defaultCellStyle.SelectionBackColor.IsEmpty || !defaultCellStyle.SelectionForeColor.IsEmpty || defaultCellStyle.Font != null || !obj.Equals(defaultCellStyle.NullValue) || !defaultCellStyle.IsDataSourceNullValueDefault || !string.IsNullOrEmpty(defaultCellStyle.Format) || !defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) || defaultCellStyle.Alignment != DataGridViewContentAlignment.MiddleCenter || defaultCellStyle.WrapMode != DataGridViewTriState.NotSet || defaultCellStyle.Tag != null || !defaultCellStyle.Padding.Equals(Padding.Empty);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0009DD78 File Offset: 0x0009BF78
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewImageColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000DF9 RID: 3577
		private static Type columnType = typeof(DataGridViewImageColumn);

		// Token: 0x04000DFA RID: 3578
		private Image image;

		// Token: 0x04000DFB RID: 3579
		private Icon icon;
	}
}
