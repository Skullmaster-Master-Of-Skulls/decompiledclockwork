using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox;
using ReportFunctions.Properties;

namespace ReportFunctions
{
	// Token: 0x0200000B RID: 11
	public partial class BatchEmailOptions : Form
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00005D8C File Offset: 0x00004D8C
		public BatchEmailOptions()
		{
			this.selectedSendMode = BatchEmail.BatchEmailSendMode.DontSendEmails;
			this.InitializeComponent();
			this.btn_dontSendAnyEmails.TitleFontSize = 18;
			this.btn_previewEmails.TitleFontSize = 18;
			this.btn_sendAllEmails.TitleFontSize = 18;
			this.btn_sendFirstEmail.TitleFontSize = 18;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00005DF0 File Offset: 0x00004DF0
		public BatchEmail.BatchEmailSendMode SelectedSendMode
		{
			get
			{
				return this.selectedSendMode;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005E08 File Offset: 0x00004E08
		private void btn_sendAllEmails_Click(object sender, EventArgs e)
		{
			this.selectedSendMode = BatchEmail.BatchEmailSendMode.SendEmails;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005E21 File Offset: 0x00004E21
		private void btn_dontSendAnyEmails_Click(object sender, EventArgs e)
		{
			this.selectedSendMode = BatchEmail.BatchEmailSendMode.DontSendEmails;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005E3A File Offset: 0x00004E3A
		private void btn_sendFirstEmail_Click(object sender, EventArgs e)
		{
			this.selectedSendMode = BatchEmail.BatchEmailSendMode.SendFirstEmail;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005E53 File Offset: 0x00004E53
		private void btn_previewEmails_Click(object sender, EventArgs e)
		{
			this.selectedSendMode = BatchEmail.BatchEmailSendMode.PreviewEmails;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00005E6C File Offset: 0x00004E6C
		private void BatchEmailOptions_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x040000DA RID: 218
		private BatchEmail.BatchEmailSendMode selectedSendMode;
	}
}
