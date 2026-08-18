using System;
using System.ComponentModel.Design.Data;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200011F RID: 287
	internal partial class SqlDataSourceWizardForm : WizardForm
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x00043028 File Offset: 0x00041228
		public SqlDataSourceWizardForm(IServiceProvider serviceProvider, SqlDataSourceDesigner sqlDataSourceDesigner, IDataEnvironment dataEnvironment) : base(serviceProvider)
		{
			base.Glyph = BitmapSelector.CreateBitmap(typeof(SqlDataSourceWizardForm), "datasourcewizard.bmp");
			this._dataEnvironment = dataEnvironment;
			this._sqlDataSource = (SqlDataSource)sqlDataSourceDesigner.Component;
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this.Text = SR.GetString("ConfigureDataSource_Title", new object[]
			{
				this._sqlDataSource.ID
			});
			this._connectionPanel = this.CreateConnectionPanel();
			base.SetPanels(new WizardPanel[]
			{
				this._connectionPanel
			});
			this._saveConfiguredConnectionPanel = new SqlDataSourceSaveConfiguredConnectionPanel(this._sqlDataSourceDesigner, this._dataEnvironment);
			base.RegisterPanel(this._saveConfiguredConnectionPanel);
			this._configureParametersPanel = new SqlDataSourceConfigureParametersPanel(this._sqlDataSourceDesigner);
			base.RegisterPanel(this._configureParametersPanel);
			this._configureSelectPanel = new SqlDataSourceConfigureSelectPanel(this._sqlDataSourceDesigner);
			base.RegisterPanel(this._configureSelectPanel);
			this._customCommandPanel = new SqlDataSourceCustomCommandPanel(this._sqlDataSourceDesigner);
			base.RegisterPanel(this._customCommandPanel);
			this._summaryPanel = new SqlDataSourceSummaryPanel(this._sqlDataSourceDesigner);
			base.RegisterPanel(this._summaryPanel);
			base.Size += new Size(0, 40);
			this.MinimumSize = base.Size;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00043176 File Offset: 0x00041376
		internal DesignerDataConnection DesignerDataConnection
		{
			get
			{
				return this._designerDataConnection;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0004317E File Offset: 0x0004137E
		internal IDataEnvironment DataEnvironment
		{
			get
			{
				return this._dataEnvironment;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00043186 File Offset: 0x00041386
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.ConfigureDataSource";
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0004318D File Offset: 0x0004138D
		internal SqlDataSourceDesigner SqlDataSourceDesigner
		{
			get
			{
				return this._sqlDataSourceDesigner;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00043195 File Offset: 0x00041395
		protected virtual SqlDataSourceConnectionPanel CreateConnectionPanel()
		{
			return new SqlDataSourceDataConnectionChooserPanel(this.SqlDataSourceDesigner, this.DataEnvironment);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000431A8 File Offset: 0x000413A8
		internal SqlDataSourceConfigureParametersPanel GetConfigureParametersPanel()
		{
			this._configureParametersPanel.ResetUI();
			return this._configureParametersPanel;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000431BB File Offset: 0x000413BB
		internal SqlDataSourceConfigureSelectPanel GetConfigureSelectPanel()
		{
			this._configureSelectPanel.ResetUI();
			return this._configureSelectPanel;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000431CE File Offset: 0x000413CE
		internal SqlDataSourceCustomCommandPanel GetCustomCommandPanel()
		{
			this._customCommandPanel.ResetUI();
			return this._customCommandPanel;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000431E1 File Offset: 0x000413E1
		internal SqlDataSourceSaveConfiguredConnectionPanel GetSaveConfiguredConnectionPanel()
		{
			this._saveConfiguredConnectionPanel.ResetUI();
			return this._saveConfiguredConnectionPanel;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000431F4 File Offset: 0x000413F4
		internal SqlDataSourceSummaryPanel GetSummaryPanel()
		{
			this._summaryPanel.ResetUI();
			return this._summaryPanel;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00043207 File Offset: 0x00041407
		protected override void OnPanelChanging(WizardPanelChangingEventArgs e)
		{
			base.OnPanelChanging(e);
			if (e.CurrentPanel == this._connectionPanel)
			{
				this._designerDataConnection = this._connectionPanel.DataConnection;
			}
		}

		// Token: 0x04000651 RID: 1617
		private SqlDataSourceConnectionPanel _connectionPanel;

		// Token: 0x04000652 RID: 1618
		private SqlDataSource _sqlDataSource;

		// Token: 0x04000653 RID: 1619
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x04000654 RID: 1620
		private IDataEnvironment _dataEnvironment;

		// Token: 0x04000655 RID: 1621
		private DesignerDataConnection _designerDataConnection;

		// Token: 0x04000656 RID: 1622
		private SqlDataSourceSaveConfiguredConnectionPanel _saveConfiguredConnectionPanel;

		// Token: 0x04000657 RID: 1623
		private SqlDataSourceConfigureParametersPanel _configureParametersPanel;

		// Token: 0x04000658 RID: 1624
		private SqlDataSourceConfigureSelectPanel _configureSelectPanel;

		// Token: 0x04000659 RID: 1625
		private SqlDataSourceCustomCommandPanel _customCommandPanel;

		// Token: 0x0400065A RID: 1626
		private SqlDataSourceSummaryPanel _summaryPanel;
	}
}
