using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x0200021E RID: 542
	[ToolboxBitmap(typeof(DataGridViewTextBoxColumn), "DataGridViewTextBoxColumn.bmp")]
	public class DataGridViewTextBoxColumn : DataGridViewColumn
	{
		// Token: 0x06002346 RID: 9030 RVA: 0x000A841A File Offset: 0x000A661A
		public DataGridViewTextBoxColumn() : base(new DataGridViewTextBoxCell())
		{
			this.SortMode = DataGridViewColumnSortMode.Automatic;
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000893F9 File Offset: 0x000875F9
		// (set) Token: 0x06002348 RID: 9032 RVA: 0x000A842E File Offset: 0x000A662E
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
				if (value != null && !(value is DataGridViewTextBoxCell))
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[]
					{
						"System.Windows.Forms.DataGridViewTextBoxCell"
					}));
				}
				base.CellTemplate = value;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x000A8460 File Offset: 0x000A6660
		// (set) Token: 0x0600234A RID: 9034 RVA: 0x000A8488 File Offset: 0x000A6688
		[DefaultValue(32767)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_TextBoxColumnMaxInputLengthDescr")]
		public int MaxInputLength
		{
			get
			{
				if (this.TextBoxCellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return this.TextBoxCellTemplate.MaxInputLength;
			}
			set
			{
				if (this.MaxInputLength != value)
				{
					this.TextBoxCellTemplate.MaxInputLength = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewTextBoxCell dataGridViewTextBoxCell = dataGridViewRow.Cells[base.Index] as DataGridViewTextBoxCell;
							if (dataGridViewTextBoxCell != null)
							{
								dataGridViewTextBoxCell.MaxInputLength = value;
							}
						}
					}
				}
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x000A84FD File Offset: 0x000A66FD
		// (set) Token: 0x0600234C RID: 9036 RVA: 0x000A8505 File Offset: 0x000A6705
		[DefaultValue(DataGridViewColumnSortMode.Automatic)]
		public new DataGridViewColumnSortMode SortMode
		{
			get
			{
				return base.SortMode;
			}
			set
			{
				base.SortMode = value;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x000A850E File Offset: 0x000A670E
		private DataGridViewTextBoxCell TextBoxCellTemplate
		{
			get
			{
				return (DataGridViewTextBoxCell)this.CellTemplate;
			}
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000A851C File Offset: 0x000A671C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewTextBoxColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000E8C RID: 3724
		private const int DATAGRIDVIEWTEXTBOXCOLUMN_maxInputLength = 32767;
	}
}
