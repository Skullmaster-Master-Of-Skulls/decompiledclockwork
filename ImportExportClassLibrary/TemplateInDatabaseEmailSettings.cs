using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EncryptionClassLibrary;
using ImportExportClassLibrary.Properties;
using UnivOleDb;

namespace ImportExportClassLibrary
{
	// Token: 0x02000022 RID: 34
	public partial class TemplateInDatabaseEmailSettings : Form
	{
		// Token: 0x060000EA RID: 234 RVA: 0x000060A3 File Offset: 0x000050A3
		public TemplateInDatabaseEmailSettings()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000060B1 File Offset: 0x000050B1
		public TemplateInDatabaseEmailSettings(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int templateId)
		{
			this.templateId = templateId;
			this.da = da;
			this.tripleDES = tripleDES;
			this.InitializeComponent();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000060D4 File Offset: 0x000050D4
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000060DC File Offset: 0x000050DC
		private void btn_save_Click(object sender, EventArgs e)
		{
			this.da.SelectCommand.CommandText = "UPDATE emailtemplates SET eattachments=@esubject,eto=@eto,ecc=@ecc,ebcc=@ebcc,ebody=@ebody,blankreplacements=@from,warningifmissingcodes=@attach WHERE templateid=@id";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@esubject", this.txt_subject.Text);
			this.da.SelectCommand.Parameters.Add("@eto", this.txt_to.Text);
			this.da.SelectCommand.Parameters.Add("@ecc", this.txt_cc.Text);
			this.da.SelectCommand.Parameters.Add("@ebcc", this.txt_bcc.Text);
			this.da.SelectCommand.Parameters.Add("@ebody", this.txt_body.Text);
			this.da.SelectCommand.Parameters.Add("@id", this.templateId);
			this.da.SelectCommand.Parameters.Add("@from", this.txt_from.Text);
			this.da.SelectCommand.Parameters.Add("@attach", this.txt_attach.Text);
			this.da.Fill(new DataTable());
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006261 File Offset: 0x00005261
		private void TemplateInDatabaseEmailSettings_Load(object sender, EventArgs e)
		{
			this.LoadTemplateInfo();
			this.ToScreen();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006270 File Offset: 0x00005270
		private void LoadTemplateInfo()
		{
			this.da.SelectCommand.CommandText = "SELECT * FROM emailtemplates WHERE templateid=@id";
			this.da.SelectCommand.Parameters.Clear();
			this.da.SelectCommand.Parameters.Add("@id", this.templateId);
			this.t = new DataTable();
			this.da.Fill(this.t);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000062EC File Offset: 0x000052EC
		private void ToScreen()
		{
			if (this.t.Rows.Count > 0)
			{
				DataRow dataRow = this.t.Rows[0];
				this.txt_subject.Text = dataRow["eattachments"].ToString();
				this.txt_to.Text = dataRow["eto"].ToString();
				this.txt_cc.Text = dataRow["ecc"].ToString();
				this.txt_bcc.Text = dataRow["ebcc"].ToString();
				this.txt_body.Text = dataRow["ebody"].ToString();
				this.txt_from.Text = dataRow["blankreplacements"].ToString();
				this.txt_attach.Text = dataRow["warningifmissingcodes"].ToString();
			}
		}

		// Token: 0x0400004F RID: 79
		private int templateId;

		// Token: 0x04000050 RID: 80
		private UnivDataAdapter da;

		// Token: 0x04000051 RID: 81
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x04000052 RID: 82
		private DataTable t;
	}
}
