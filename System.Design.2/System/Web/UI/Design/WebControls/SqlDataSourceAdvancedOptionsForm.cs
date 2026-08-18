using System;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000106 RID: 262
	internal sealed partial class SqlDataSourceAdvancedOptionsForm : DesignerForm
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x000352C9 File Offset: 0x000334C9
		public SqlDataSourceAdvancedOptionsForm(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			this.InitializeComponent();
			this.InitializeUI();
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x000352E5 File Offset: 0x000334E5
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x000352F2 File Offset: 0x000334F2
		public bool GenerateStatements
		{
			get
			{
				return this._generateCheckBox.Checked;
			}
			set
			{
				this._generateCheckBox.Checked = value;
				this.UpdateEnabledState();
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00035306 File Offset: 0x00033506
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.AdvancedOptions";
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0003530D File Offset: 0x0003350D
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0003531A File Offset: 0x0003351A
		public bool OptimisticConcurrency
		{
			get
			{
				return this._optimisticCheckBox.Checked;
			}
			set
			{
				this._optimisticCheckBox.Checked = value;
				this.UpdateEnabledState();
			}
		}

		// Token: 0x1700022B RID: 555
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0003532E File Offset: 0x0003352E
		public bool OptimisticConcurrencySupported
		{
			set
			{
				this._optimisticSupported = value;
				this.UpdateEnabledState();
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000356F4 File Offset: 0x000338F4
		private void InitializeUI()
		{
			this._helpLabel.Text = SR.GetString("SqlDataSourceAdvancedOptionsForm_HelpLabel");
			this._generateCheckBox.Text = SR.GetString("SqlDataSourceAdvancedOptionsForm_GenerateCheckBox");
			this._generateHelpLabel.Text = SR.GetString("SqlDataSourceAdvancedOptionsForm_GenerateHelpLabel");
			this._optimisticCheckBox.Text = SR.GetString("SqlDataSourceAdvancedOptionsForm_OptimisticCheckBox");
			this._optimisticHelpLabel.Text = SR.GetString("SqlDataSourceAdvancedOptionsForm_OptimisticLabel");
			this.Text = SR.GetString("SqlDataSourceAdvancedOptionsForm_Caption");
			this._generateCheckBox.AccessibleDescription = this._generateHelpLabel.Text;
			this._optimisticCheckBox.AccessibleDescription = this._optimisticHelpLabel.Text;
			this._okButton.Text = SR.GetString("OK");
			this._cancelButton.Text = SR.GetString("Cancel");
			this.UpdateFonts();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002AF61 File Offset: 0x00029161
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000357D6 File Offset: 0x000339D6
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFonts();
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000357E5 File Offset: 0x000339E5
		private void OnGenerateCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.UpdateEnabledState();
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000357ED File Offset: 0x000339ED
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x000357FC File Offset: 0x000339FC
		public void SetAllowAutogenerate(bool allowAutogenerate)
		{
			if (!allowAutogenerate)
			{
				this._generateCheckBox.Checked = false;
				this._generateCheckBox.Enabled = false;
				this._generateHelpLabel.Enabled = false;
				this.UpdateEnabledState();
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0003582C File Offset: 0x00033A2C
		private void UpdateEnabledState()
		{
			bool flag = this._generateCheckBox.Checked && this._optimisticSupported;
			this._optimisticCheckBox.Enabled = flag;
			this._optimisticHelpLabel.Enabled = flag;
			if (!flag)
			{
				this._optimisticCheckBox.Checked = false;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00035878 File Offset: 0x00033A78
		private void UpdateFonts()
		{
			Font font = new Font(this.Font, FontStyle.Bold);
			this._generateCheckBox.Font = font;
			this._optimisticCheckBox.Font = font;
		}

		// Token: 0x04000573 RID: 1395
		private bool _optimisticSupported = true;
	}
}
