using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000206 RID: 518
	[ToolboxBitmap(typeof(DataGridViewLinkColumn), "DataGridViewLinkColumn.bmp")]
	public class DataGridViewLinkColumn : DataGridViewColumn
	{
		// Token: 0x060021B9 RID: 8633 RVA: 0x0009F6C4 File Offset: 0x0009D8C4
		public DataGridViewLinkColumn() : base(new DataGridViewLinkCell())
		{
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x0009F6D1 File Offset: 0x0009D8D1
		// (set) Token: 0x060021BB RID: 8635 RVA: 0x0009F6FC File Offset: 0x0009D8FC
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_LinkColumnActiveLinkColorDescr")]
		public Color ActiveLinkColor
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).ActiveLinkColor;
			}
			set
			{
				if (!this.ActiveLinkColor.Equals(value))
				{
					((DataGridViewLinkCell)this.CellTemplate).ActiveLinkColorInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.ActiveLinkColorInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0009F79C File Offset: 0x0009D99C
		private bool ShouldSerializeActiveLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.ActiveLinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.ActiveLinkColor.Equals(LinkUtilities.IEActiveLinkColor);
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x000893F9 File Offset: 0x000875F9
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x0009F7FA File Offset: 0x0009D9FA
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
				if (value != null && !(value is DataGridViewLinkCell))
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[]
					{
						"System.Windows.Forms.DataGridViewLinkCell"
					}));
				}
				base.CellTemplate = value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0009F82C File Offset: 0x0009DA2C
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x0009F858 File Offset: 0x0009DA58
		[DefaultValue(LinkBehavior.SystemDefault)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_LinkColumnLinkBehaviorDescr")]
		public LinkBehavior LinkBehavior
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).LinkBehavior;
			}
			set
			{
				if (!this.LinkBehavior.Equals(value))
				{
					((DataGridViewLinkCell)this.CellTemplate).LinkBehavior = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.LinkBehaviorInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x0009F8F8 File Offset: 0x0009DAF8
		// (set) Token: 0x060021C2 RID: 8642 RVA: 0x0009F924 File Offset: 0x0009DB24
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_LinkColumnLinkColorDescr")]
		public Color LinkColor
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).LinkColor;
			}
			set
			{
				if (!this.LinkColor.Equals(value))
				{
					((DataGridViewLinkCell)this.CellTemplate).LinkColorInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.LinkColorInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0009F9C4 File Offset: 0x0009DBC4
		private bool ShouldSerializeLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.LinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.LinkColor.Equals(LinkUtilities.IELinkColor);
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x0009FA22 File Offset: 0x0009DC22
		// (set) Token: 0x060021C5 RID: 8645 RVA: 0x0009FA2C File Offset: 0x0009DC2C
		[DefaultValue(null)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_LinkColumnTextDescr")]
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
						if (this.UseColumnTextForLinkValue)
						{
							base.DataGridView.OnColumnCommonChange(base.Index);
							return;
						}
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null && dataGridViewLinkCell.UseColumnTextForLinkValue)
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

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x0009FAE6 File Offset: 0x0009DCE6
		// (set) Token: 0x060021C7 RID: 8647 RVA: 0x0009FB10 File Offset: 0x0009DD10
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_LinkColumnTrackVisitedStateDescr")]
		public bool TrackVisitedState
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).TrackVisitedState;
			}
			set
			{
				if (this.TrackVisitedState != value)
				{
					((DataGridViewLinkCell)this.CellTemplate).TrackVisitedStateInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.TrackVisitedStateInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x0009FB9B File Offset: 0x0009DD9B
		// (set) Token: 0x060021C9 RID: 8649 RVA: 0x0009FBC8 File Offset: 0x0009DDC8
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_LinkColumnUseColumnTextForLinkValueDescr")]
		public bool UseColumnTextForLinkValue
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).UseColumnTextForLinkValue;
			}
			set
			{
				if (this.UseColumnTextForLinkValue != value)
				{
					((DataGridViewLinkCell)this.CellTemplate).UseColumnTextForLinkValueInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.UseColumnTextForLinkValueInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x0009FC53 File Offset: 0x0009DE53
		// (set) Token: 0x060021CB RID: 8651 RVA: 0x0009FC80 File Offset: 0x0009DE80
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_LinkColumnVisitedLinkColorDescr")]
		public Color VisitedLinkColor
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).VisitedLinkColor;
			}
			set
			{
				if (!this.VisitedLinkColor.Equals(value))
				{
					((DataGridViewLinkCell)this.CellTemplate).VisitedLinkColorInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.VisitedLinkColorInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0009FD20 File Offset: 0x0009DF20
		private bool ShouldSerializeVisitedLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.VisitedLinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.VisitedLinkColor.Equals(LinkUtilities.IEVisitedLinkColor);
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x0009FD80 File Offset: 0x0009DF80
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewLinkColumn dataGridViewLinkColumn;
			if (type == DataGridViewLinkColumn.columnType)
			{
				dataGridViewLinkColumn = new DataGridViewLinkColumn();
			}
			else
			{
				dataGridViewLinkColumn = (DataGridViewLinkColumn)Activator.CreateInstance(type);
			}
			if (dataGridViewLinkColumn != null)
			{
				base.CloneInternal(dataGridViewLinkColumn);
				dataGridViewLinkColumn.Text = this.text;
			}
			return dataGridViewLinkColumn;
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x0009FDCC File Offset: 0x0009DFCC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewLinkColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000E19 RID: 3609
		private static Type columnType = typeof(DataGridViewLinkColumn);

		// Token: 0x04000E1A RID: 3610
		private string text;
	}
}
