using System;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004B1 RID: 1201
	internal sealed partial class SqlDataSourceAdvancedOptionsForm : DesignerForm
	{
		// Token: 0x06002B79 RID: 11129 RVA: 0x000EFDA3 File Offset: 0x000EEDA3
		public SqlDataSourceAdvancedOptionsForm(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			this.InitializeComponent();
			this.InitializeUI();
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000EFDB8 File Offset: 0x000EEDB8
		// (set) Token: 0x06002B7B RID: 11131 RVA: 0x000EFDC5 File Offset: 0x000EEDC5
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

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x000EFDD9 File Offset: 0x000EEDD9
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.SqlDataSource.AdvancedOptions";
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x000EFDE0 File Offset: 0x000EEDE0
		// (set) Token: 0x06002B7E RID: 11134 RVA: 0x000EFDED File Offset: 0x000EEDED
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

		// Token: 0x06002B80 RID: 11136 RVA: 0x000F01B8 File Offset: 0x000EF1B8
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

		// Token: 0x06002B81 RID: 11137 RVA: 0x000F029A File Offset: 0x000EF29A
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000F02A9 File Offset: 0x000EF2A9
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFonts();
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000F02B8 File Offset: 0x000EF2B8
		private void OnGenerateCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.UpdateEnabledState();
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000F02C0 File Offset: 0x000EF2C0
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000F02CF File Offset: 0x000EF2CF
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

		// Token: 0x06002B86 RID: 11142 RVA: 0x000F0300 File Offset: 0x000EF300
		private void UpdateEnabledState()
		{
			bool @checked = this._generateCheckBox.Checked;
			this._optimisticCheckBox.Enabled = @checked;
			this._optimisticHelpLabel.Enabled = @checked;
			if (!@checked)
			{
				this._optimisticCheckBox.Checked = false;
			}
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000F0340 File Offset: 0x000EF340
		private void UpdateFonts()
		{
			Font font = new Font(this.Font, FontStyle.Bold);
			this._generateCheckBox.Font = font;
			this._optimisticCheckBox.Font = font;
		}
	}
}
