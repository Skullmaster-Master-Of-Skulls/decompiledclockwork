using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002B9 RID: 697
	internal partial class DataGridViewCellStyleBuilder : Form
	{
		// Token: 0x06001B9F RID: 7071 RVA: 0x000A51AC File Offset: 0x000A33AC
		public DataGridViewCellStyleBuilder(IServiceProvider serviceProvider, IComponent comp)
		{
			this.InitializeComponent();
			this.InitializeGrids();
			this.listenerDataGridView = new DataGridView();
			this.serviceProvider = serviceProvider;
			this.comp = comp;
			if (this.serviceProvider != null)
			{
				this.helpService = (IHelpService)serviceProvider.GetService(typeof(IHelpService));
			}
			this.cellStyleProperties.Site = new DataGridViewComponentPropertyGridSite(serviceProvider, comp);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000A521C File Offset: 0x000A341C
		private void InitializeGrids()
		{
			this.sampleDataGridViewSelected.Size = new Size(100, this.Font.Height + 9);
			this.sampleDataGridView.Size = new Size(100, this.Font.Height + 9);
			this.sampleDataGridView.AccessibilityObject.Name = SR.GetString("CellStyleBuilderNormalPreviewAccName");
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			dataGridViewRow.Cells.Add(new DataGridViewCellStyleBuilder.DialogDataGridViewCell());
			dataGridViewRow.Cells[0].Value = "####";
			dataGridViewRow.Cells[0].AccessibilityObject.Name = SR.GetString("CellStyleBuilderSelectedPreviewAccName");
			this.sampleDataGridViewSelected.Columns.Add(new DataGridViewTextBoxColumn());
			this.sampleDataGridViewSelected.Rows.Add(dataGridViewRow);
			this.sampleDataGridViewSelected.Rows[0].Selected = true;
			this.sampleDataGridViewSelected.AccessibilityObject.Name = SR.GetString("CellStyleBuilderSelectedPreviewAccName");
			dataGridViewRow = new DataGridViewRow();
			dataGridViewRow.Cells.Add(new DataGridViewCellStyleBuilder.DialogDataGridViewCell());
			dataGridViewRow.Cells[0].Value = "####";
			dataGridViewRow.Cells[0].AccessibilityObject.Name = SR.GetString("CellStyleBuilderNormalPreviewAccName");
			this.sampleDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
			this.sampleDataGridView.Rows.Add(dataGridViewRow);
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x000A539E File Offset: 0x000A359E
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x000A53A8 File Offset: 0x000A35A8
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
			set
			{
				this.cellStyle = new DataGridViewCellStyle(value);
				this.cellStyleProperties.SelectedObject = this.cellStyle;
				this.ListenerDataGridViewDefaultCellStyleChanged(null, EventArgs.Empty);
				this.listenerDataGridView.DefaultCellStyle = this.cellStyle;
				this.listenerDataGridView.DefaultCellStyleChanged += this.ListenerDataGridViewDefaultCellStyleChanged;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x000A5406 File Offset: 0x000A3606
		public ITypeDescriptorContext Context
		{
			set
			{
				this.context = value;
			}
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000A5410 File Offset: 0x000A3610
		private void ListenerDataGridViewDefaultCellStyleChanged(object sender, EventArgs e)
		{
			DataGridViewCellStyle defaultCellStyle = new DataGridViewCellStyle(this.cellStyle);
			this.sampleDataGridView.DefaultCellStyle = defaultCellStyle;
			this.sampleDataGridViewSelected.DefaultCellStyle = defaultCellStyle;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x000A5C91 File Offset: 0x000A3E91
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & Keys.Modifiers) == Keys.None && (keyData & Keys.KeyCode) == Keys.Escape)
			{
				base.Close();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000A5CB6 File Offset: 0x000A3EB6
		private void DataGridViewCellStyleBuilder_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this.DataGridViewCellStyleBuilder_HelpRequestHandled();
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000A5CC5 File Offset: 0x000A3EC5
		private void DataGridViewCellStyleBuilder_HelpRequested(object sender, HelpEventArgs e)
		{
			e.Handled = true;
			this.DataGridViewCellStyleBuilder_HelpRequestHandled();
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x000A5CD4 File Offset: 0x000A3ED4
		private void DataGridViewCellStyleBuilder_HelpRequestHandled()
		{
			IHelpService helpService = this.context.GetService(typeof(IHelpService)) as IHelpService;
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword("vs.CellStyleDialog");
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x000A5D0C File Offset: 0x000A3F0C
		private void DataGridViewCellStyleBuilder_Load(object sender, EventArgs e)
		{
			this.sampleDataGridView.ClearSelection();
			this.sampleDataGridView.Rows[0].Height = this.sampleDataGridView.Height;
			this.sampleDataGridView.Columns[0].Width = this.sampleDataGridView.Width;
			this.sampleDataGridViewSelected.Rows[0].Height = this.sampleDataGridViewSelected.Height;
			this.sampleDataGridViewSelected.Columns[0].Width = this.sampleDataGridViewSelected.Width;
			this.sampleDataGridView.Layout += this.sampleDataGridView_Layout;
			this.sampleDataGridViewSelected.Layout += this.sampleDataGridView_Layout;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x000A5DD6 File Offset: 0x000A3FD6
		private void sampleDataGridView_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
		{
			if ((e.StateChanged & DataGridViewElementStates.Selected) != DataGridViewElementStates.None && (e.Cell.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None)
			{
				this.sampleDataGridView.ClearSelection();
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000A5E00 File Offset: 0x000A4000
		private void sampleDataGridView_Layout(object sender, LayoutEventArgs e)
		{
			DataGridView dataGridView = (DataGridView)sender;
			dataGridView.Rows[0].Height = dataGridView.Height;
			dataGridView.Columns[0].Width = dataGridView.Width;
		}

		// Token: 0x0400167B RID: 5755
		private DataGridView listenerDataGridView;

		// Token: 0x04001685 RID: 5765
		private IHelpService helpService;

		// Token: 0x04001686 RID: 5766
		private IComponent comp;

		// Token: 0x04001687 RID: 5767
		private IServiceProvider serviceProvider;

		// Token: 0x04001688 RID: 5768
		private DataGridViewCellStyle cellStyle;

		// Token: 0x04001689 RID: 5769
		private ITypeDescriptorContext context;

		// Token: 0x0200054F RID: 1359
		private class DialogDataGridViewCell : DataGridViewTextBoxCell
		{
			// Token: 0x06003134 RID: 12596 RVA: 0x0010CC26 File Offset: 0x0010AE26
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (this.accObj == null)
				{
					this.accObj = new DataGridViewCellStyleBuilder.DialogDataGridViewCell.DialogDataGridViewCellAccessibleObject(this);
				}
				return this.accObj;
			}

			// Token: 0x04002126 RID: 8486
			private DataGridViewCellStyleBuilder.DialogDataGridViewCell.DialogDataGridViewCellAccessibleObject accObj;

			// Token: 0x020005EC RID: 1516
			private class DialogDataGridViewCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
			{
				// Token: 0x060034DC RID: 13532 RVA: 0x0011F00D File Offset: 0x0011D20D
				public DialogDataGridViewCellAccessibleObject(DataGridViewCell owner) : base(owner)
				{
				}

				// Token: 0x17000A35 RID: 2613
				// (get) Token: 0x060034DD RID: 13533 RVA: 0x0011F021 File Offset: 0x0011D221
				// (set) Token: 0x060034DE RID: 13534 RVA: 0x0011F029 File Offset: 0x0011D229
				public override string Name
				{
					get
					{
						return this.name;
					}
					set
					{
						this.name = value;
					}
				}

				// Token: 0x0400233E RID: 9022
				private string name = "";
			}
		}
	}
}
