using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using AutoComboBox;
using SettingsPermissions;
using TechnoPro.Common.UI.ClientManager.OldUserSettings;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x0200003F RID: 63
	public partial class BatchEmailDialog : Form
	{
		// Token: 0x060003AD RID: 941 RVA: 0x000434B1 File Offset: 0x000424B1
		public BatchEmailDialog(UnivDataAdapter da, Settings settings, DataTable t_original)
		{
			this.InitializeComponent();
			this.da = da;
			this.settings = settings;
			this.t_original = t_original;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00043FA4 File Offset: 0x00042FA4
		private void BatchEmailDialog_Load(object sender, EventArgs e)
		{
			this.da.SelectCommand.CommandText = "SELECT dsc.screennum,dsc.controlid,dsc.ordernum,dc.controlcode,dc.controlcaption,dc.setting1,dc.setting2,dc.setting3,dc.defaultvalue FROM dynamicscreencontrols dsc LEFT JOIN dynamiccontrols dc ON dc.controlid=dsc.controlid WHERE dsc.screennum=1 ORDER BY dc.controlcaption";
			DataTable dataTable = new DataTable();
			this.da.SelectCommand.Parameters.Clear();
			string text;
			this.da.Fill(dataTable, out text);
			this.cmb_emailField.DataSource = dataTable;
			this.cmb_emailField.DisplayMember = "controlcaption";
			this.cmb_emailField.ValueMember = "controlid";
			this.cmb_email2Field.DataSource = dataTable.Copy();
			this.cmb_email2Field.DisplayMember = "controlcaption";
			this.cmb_email2Field.ValueMember = "controlid";
			this.cmb_okToEmailField.DataSource = dataTable.Copy();
			this.cmb_okToEmailField.DisplayMember = "controlcaption";
			this.cmb_okToEmailField.ValueMember = "controlid";
			this.emailControlId = OldUserSettingClientManager.CurrentInstance.GetSetting(260);
			this.email2ControlId = OldUserSettingClientManager.CurrentInstance.GetSetting(448);
			this.okToEmailControlId = OldUserSettingClientManager.CurrentInstance.GetSetting(259);
			this.ignoreEmailsNotOkToEmail = OldUserSettingClientManager.CurrentInstance.IntToBool(OldUserSettingClientManager.CurrentInstance.GetSetting(403));
			this.chk_ignoreNotOkToEmail.Checked = this.ignoreEmailsNotOkToEmail;
			if (this.emailControlId > 0)
			{
				this.cmb_emailField.SelectIndexByValueMember(this.emailControlId);
			}
			else
			{
				this.cmb_emailField.SelectIndexByTextContains("email");
			}
			if (this.email2ControlId > 0)
			{
				this.cmb_email2Field.SelectIndexByValueMember(this.email2ControlId);
			}
			else
			{
				this.cmb_email2Field.SelectIndexByTextContains("email");
			}
			if (this.okToEmailControlId > 0)
			{
				this.cmb_okToEmailField.SelectIndexByValueMember(this.okToEmailControlId);
			}
			else
			{
				this.cmb_okToEmailField.SelectIndexByTextContains("ok to email");
			}
			dataTable = new DataTable();
			dataTable.Columns.Add("colind", typeof(int));
			dataTable.Columns.Add("colname");
			int num = -1;
			for (int i = 0; i < this.t_original.Columns.Count; i++)
			{
				string columnName = this.t_original.Columns[i].ColumnName;
				object[] values = new object[]
				{
					i,
					columnName
				};
				dataTable.Rows.Add(values);
				if (num < 0 && columnName.ToLower().IndexOf("email") >= 0)
				{
					num = i;
				}
			}
			this.cmb_col.DataSource = dataTable;
			this.cmb_col.DisplayMember = "colname";
			this.cmb_col.ValueMember = "colind";
			if (num >= 0)
			{
				this.cmb_col.SelectedIndex = num;
			}
			if (!this.t_original.Columns.Contains("personid") && !this.t_original.Columns.Contains("student_no"))
			{
				this.rbtn_useEmbeddedEmailAddresses.Checked = true;
			}
			else
			{
				this.rbtn_lookupTheEmailAddresses.Checked = true;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00044304 File Offset: 0x00043304
		private void btn_ok_Click(object sender, EventArgs e)
		{
			bool flag = true;
			if (this.rbtn_lookupTheEmailAddresses.Checked)
			{
				int num = this.EmailControlId;
				if (num <= 0)
				{
					MessageBox.Show("Please select the email field to use first!");
					flag = false;
				}
			}
			else
			{
				int emailColInd = this.EmailColInd;
				if (emailColInd <= 0)
				{
					MessageBox.Show("Please select the email field to use first!");
					flag = false;
				}
			}
			if (flag)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00044381 File Offset: 0x00043381
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0004438B File Offset: 0x0004338B
		private void rbtn_lookupTheEmailAddresses_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateRadioButtonEnabledControls();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00044395 File Offset: 0x00043395
		private void rbtn_useEmbeddedEmailAddresses_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateRadioButtonEnabledControls();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000443A0 File Offset: 0x000433A0
		private void UpdateRadioButtonEnabledControls()
		{
			bool @checked = this.rbtn_lookupTheEmailAddresses.Checked;
			this.cmb_emailField.Enabled = @checked;
			this.cmb_okToEmailField.Enabled = @checked;
			this.chk_ignoreNotOkToEmail.Enabled = @checked;
			this.cmb_col.Enabled = !@checked;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000443F4 File Offset: 0x000433F4
		public int EmailControlId
		{
			get
			{
				DataRow dataRow = this.cmb_emailField.SelectedDataRow();
				return (dataRow == null) ? -1 : ((int)dataRow[this.cmb_emailField.ValueMember]);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00044430 File Offset: 0x00043430
		public int EmailSecondaryControlId
		{
			get
			{
				DataRow dataRow = this.cmb_email2Field.SelectedDataRow();
				return (dataRow == null) ? -1 : ((int)dataRow[this.cmb_email2Field.ValueMember]);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0004446C File Offset: 0x0004346C
		public int OkToEmailControlId
		{
			get
			{
				DataRow dataRow = this.cmb_okToEmailField.SelectedDataRow();
				return (dataRow == null) ? -1 : ((int)dataRow[this.cmb_okToEmailField.ValueMember]);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x000444A8 File Offset: 0x000434A8
		public bool IgnoreNotOkToEmail_emails
		{
			get
			{
				return this.chk_ignoreNotOkToEmail.Checked;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000444C8 File Offset: 0x000434C8
		public bool LookupEmails
		{
			get
			{
				return this.rbtn_lookupTheEmailAddresses.Checked;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000444E8 File Offset: 0x000434E8
		public int EmailColInd
		{
			get
			{
				DataRow dataRow = this.cmb_col.SelectedDataRow();
				return (dataRow == null) ? -1 : ((int)dataRow[this.cmb_col.ValueMember]);
			}
		}

		// Token: 0x040001DC RID: 476
		private UnivDataAdapter da;

		// Token: 0x040001DD RID: 477
		private Settings settings;

		// Token: 0x040001DE RID: 478
		private DataTable t_original;

		// Token: 0x040001DF RID: 479
		private int emailControlId;

		// Token: 0x040001E0 RID: 480
		private int email2ControlId;

		// Token: 0x040001E1 RID: 481
		private int okToEmailControlId;

		// Token: 0x040001E2 RID: 482
		private bool ignoreEmailsNotOkToEmail;
	}
}
