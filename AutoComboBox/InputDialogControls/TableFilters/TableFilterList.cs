using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.InputDialogControls.TableFilters
{
	// Token: 0x02000072 RID: 114
	public class TableFilterList : UserControl
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x00023C02 File Offset: 0x00022C02
		public TableFilterList()
		{
			this.InitializeComponent();
			this.dataSource = null;
			this.items = new TableFilterControlCollection();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00023C2D File Offset: 0x00022C2D
		public TableFilterList(object dataSource)
		{
			this.InitializeComponent();
			this.dataSource = dataSource;
			this.items = new TableFilterControlCollection();
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00023C58 File Offset: 0x00022C58
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00023C70 File Offset: 0x00022C70
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
				foreach (object obj in this.items)
				{
					TableFilterControl tableFilterControl = (TableFilterControl)obj;
					tableFilterControl.DataSource = this.dataSource;
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00023CE4 File Offset: 0x00022CE4
		public TableFilterCollection Items
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00023CF7 File Offset: 0x00022CF7
		private void TableFilterList_Load(object sender, EventArgs e)
		{
			this.AddItem();
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00023D04 File Offset: 0x00022D04
		private void AddItem()
		{
			TableFilterControl tableFilterControl;
			if (this.dataSource == null)
			{
				tableFilterControl = new TableFilterControl();
			}
			else
			{
				tableFilterControl = new TableFilterControl(this.dataSource);
			}
			base.Controls.Add(tableFilterControl);
			tableFilterControl.Dock = DockStyle.Top;
			this.items.Add(tableFilterControl);
			tableFilterControl.RemoveItem += this.tfc_RemoveItem;
			tableFilterControl.AddItem += this.tfc_AddItem;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00023D7E File Offset: 0x00022D7E
		private void tfc_AddItem(object sender, EventArgs e)
		{
			this.AddItem();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00023D88 File Offset: 0x00022D88
		private void tfc_RemoveItem(object sender, EventArgs e)
		{
			if (sender is TableFilterControl)
			{
				TableFilterControl tableFilterControl = (TableFilterControl)sender;
				base.Controls.Remove(tableFilterControl);
				this.items.Remove(tableFilterControl);
				tableFilterControl.RemoveItem -= this.tfc_RemoveItem;
				tableFilterControl.AddItem -= this.tfc_AddItem;
				tableFilterControl.Dispose();
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00023DF8 File Offset: 0x00022DF8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				if (this.items.Count > 0)
				{
					foreach (object obj in this.items)
					{
						TableFilterControl tableFilterControl = (TableFilterControl)obj;
						base.Controls.Remove(tableFilterControl);
						tableFilterControl.RemoveItem -= this.tfc_RemoveItem;
						tableFilterControl.AddItem -= this.tfc_AddItem;
						tableFilterControl.Dispose();
					}
					this.items.Clear();
				}
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00023EE4 File Offset: 0x00022EE4
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "TableFilterList";
			base.Size = new Size(614, 358);
			base.Load += this.TableFilterList_Load;
			base.ResumeLayout(false);
		}

		// Token: 0x040003DF RID: 991
		private object dataSource;

		// Token: 0x040003E0 RID: 992
		private TableFilterControlCollection items;

		// Token: 0x040003E1 RID: 993
		private IContainer components = null;
	}
}
