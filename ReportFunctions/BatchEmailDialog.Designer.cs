namespace ReportFunctions
{
	// Token: 0x0200003F RID: 63
	public partial class BatchEmailDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060003AE RID: 942 RVA: 0x000434E0 File Offset: 0x000424E0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0004351C File Offset: 0x0004251C
		private void InitializeComponent()
		{
			global::System.Resources.ResourceManager resourceManager = new global::System.Resources.ResourceManager(typeof(global::ReportFunctions.BatchEmailDialog));
			this.cmb_emailField = new global::AutoComboBox.AutoComboBox();
			this.cmb_okToEmailField = new global::AutoComboBox.AutoComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.chk_ignoreNotOkToEmail = new global::System.Windows.Forms.CheckBox();
			this.btn_cancel = new global::System.Windows.Forms.Button();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btn_ok = new global::System.Windows.Forms.Button();
			this.label3 = new global::System.Windows.Forms.Label();
			this.rbtn_lookupTheEmailAddresses = new global::System.Windows.Forms.RadioButton();
			this.rbtn_useEmbeddedEmailAddresses = new global::System.Windows.Forms.RadioButton();
			this.label4 = new global::System.Windows.Forms.Label();
			this.cmb_col = new global::AutoComboBox.AutoComboBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.cmb_email2Field = new global::AutoComboBox.AutoComboBox();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.cmb_emailField.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_emailField.AllowUserToEnterAnyText = true;
			this.cmb_emailField.AutoCompleteEnabled = true;
			this.cmb_emailField.ChildLookupGroupId = 0;
			this.cmb_emailField.GotoNextItemOnDoubleClick = false;
			this.cmb_emailField.Location = new global::System.Drawing.Point(184, 40);
			this.cmb_emailField.LookupGroupId = 0;
			this.cmb_emailField.Name = "cmb_emailField";
			this.cmb_emailField.Size = new global::System.Drawing.Size(360, 24);
			this.cmb_emailField.TabIndex = 0;
			this.cmb_emailField.TryToSelectOnFocusLeave = true;
			this.cmb_okToEmailField.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_okToEmailField.AllowUserToEnterAnyText = true;
			this.cmb_okToEmailField.AutoCompleteEnabled = true;
			this.cmb_okToEmailField.ChildLookupGroupId = 0;
			this.cmb_okToEmailField.GotoNextItemOnDoubleClick = false;
			this.cmb_okToEmailField.Location = new global::System.Drawing.Point(184, 104);
			this.cmb_okToEmailField.LookupGroupId = 0;
			this.cmb_okToEmailField.Name = "cmb_okToEmailField";
			this.cmb_okToEmailField.Size = new global::System.Drawing.Size(360, 24);
			this.cmb_okToEmailField.TabIndex = 1;
			this.cmb_okToEmailField.TryToSelectOnFocusLeave = true;
			this.label1.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(56, 40);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(88, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "Email field:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(56, 104);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(120, 32);
			this.label2.TabIndex = 3;
			this.label2.Text = "Ok to email field:";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.chk_ignoreNotOkToEmail.Location = new global::System.Drawing.Point(56, 144);
			this.chk_ignoreNotOkToEmail.Name = "chk_ignoreNotOkToEmail";
			this.chk_ignoreNotOkToEmail.Size = new global::System.Drawing.Size(496, 24);
			this.chk_ignoreNotOkToEmail.TabIndex = 4;
			this.chk_ignoreNotOkToEmail.Text = "&Don't include emails for students who have the \"Ok to email\" field un-checked";
			this.btn_cancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancel.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_cancel.Location = new global::System.Drawing.Point(446, 2);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(104, 36);
			this.btn_cancel.TabIndex = 5;
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.panel1.Controls.Add(this.btn_ok);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.btn_cancel);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel1.DockPadding.All = 2;
			this.panel1.Location = new global::System.Drawing.Point(0, 260);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(552, 40);
			this.panel1.TabIndex = 6;
			this.btn_ok.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_ok.Location = new global::System.Drawing.Point(326, 2);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(104, 36);
			this.btn_ok.TabIndex = 6;
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.label3.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.label3.Location = new global::System.Drawing.Point(430, 2);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(16, 36);
			this.label3.TabIndex = 7;
			this.rbtn_lookupTheEmailAddresses.Location = new global::System.Drawing.Point(8, 8);
			this.rbtn_lookupTheEmailAddresses.Name = "rbtn_lookupTheEmailAddresses";
			this.rbtn_lookupTheEmailAddresses.Size = new global::System.Drawing.Size(328, 24);
			this.rbtn_lookupTheEmailAddresses.TabIndex = 7;
			this.rbtn_lookupTheEmailAddresses.Text = "Lookup the email addresses";
			this.rbtn_lookupTheEmailAddresses.CheckedChanged += new global::System.EventHandler(this.rbtn_lookupTheEmailAddresses_CheckedChanged);
			this.rbtn_useEmbeddedEmailAddresses.Location = new global::System.Drawing.Point(8, 192);
			this.rbtn_useEmbeddedEmailAddresses.Name = "rbtn_useEmbeddedEmailAddresses";
			this.rbtn_useEmbeddedEmailAddresses.Size = new global::System.Drawing.Size(328, 24);
			this.rbtn_useEmbeddedEmailAddresses.TabIndex = 8;
			this.rbtn_useEmbeddedEmailAddresses.Text = "Use the email addresses already in this table";
			this.rbtn_useEmbeddedEmailAddresses.CheckedChanged += new global::System.EventHandler(this.rbtn_useEmbeddedEmailAddresses_CheckedChanged);
			this.label4.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(56, 224);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(88, 24);
			this.label4.TabIndex = 10;
			this.label4.Text = "Email field:";
			this.label4.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.cmb_col.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_col.AllowUserToEnterAnyText = true;
			this.cmb_col.AutoCompleteEnabled = true;
			this.cmb_col.ChildLookupGroupId = 0;
			this.cmb_col.GotoNextItemOnDoubleClick = false;
			this.cmb_col.Location = new global::System.Drawing.Point(184, 224);
			this.cmb_col.LookupGroupId = 0;
			this.cmb_col.Name = "cmb_col";
			this.cmb_col.Size = new global::System.Drawing.Size(360, 24);
			this.cmb_col.TabIndex = 9;
			this.cmb_col.TryToSelectOnFocusLeave = true;
			this.label5.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new global::System.Drawing.Point(56, 72);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(120, 24);
			this.label5.TabIndex = 11;
			this.label5.Text = "Secondary email field:";
			this.label5.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.cmb_email2Field.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_email2Field.AllowUserToEnterAnyText = true;
			this.cmb_email2Field.AutoCompleteEnabled = true;
			this.cmb_email2Field.ChildLookupGroupId = 0;
			this.cmb_email2Field.GotoNextItemOnDoubleClick = false;
			this.cmb_email2Field.Location = new global::System.Drawing.Point(184, 72);
			this.cmb_email2Field.LookupGroupId = 0;
			this.cmb_email2Field.Name = "cmb_email2Field";
			this.cmb_email2Field.Size = new global::System.Drawing.Size(360, 24);
			this.cmb_email2Field.TabIndex = 12;
			this.cmb_email2Field.TryToSelectOnFocusLeave = true;
			base.AcceptButton = this.btn_ok;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.CancelButton = this.btn_cancel;
			base.ClientSize = new global::System.Drawing.Size(552, 300);
			base.Controls.Add(this.cmb_email2Field);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.cmb_col);
			base.Controls.Add(this.rbtn_useEmbeddedEmailAddresses);
			base.Controls.Add(this.rbtn_lookupTheEmailAddresses);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.chk_ignoreNotOkToEmail);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmb_okToEmailField);
			base.Controls.Add(this.cmb_emailField);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.Name = "BatchEmailDialog";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Batch email";
			base.Load += new global::System.EventHandler(this.BatchEmailDialog_Load);
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x040001CC RID: 460
		private global::AutoComboBox.AutoComboBox cmb_emailField;

		// Token: 0x040001CD RID: 461
		private global::AutoComboBox.AutoComboBox cmb_okToEmailField;

		// Token: 0x040001CE RID: 462
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040001CF RID: 463
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040001D0 RID: 464
		private global::System.Windows.Forms.CheckBox chk_ignoreNotOkToEmail;

		// Token: 0x040001D1 RID: 465
		private global::System.Windows.Forms.Button btn_cancel;

		// Token: 0x040001D2 RID: 466
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040001D3 RID: 467
		private global::System.Windows.Forms.Button btn_ok;

		// Token: 0x040001D4 RID: 468
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040001D5 RID: 469
		private global::System.Windows.Forms.RadioButton rbtn_lookupTheEmailAddresses;

		// Token: 0x040001D6 RID: 470
		private global::System.Windows.Forms.RadioButton rbtn_useEmbeddedEmailAddresses;

		// Token: 0x040001D7 RID: 471
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040001D8 RID: 472
		private global::AutoComboBox.AutoComboBox cmb_col;

		// Token: 0x040001D9 RID: 473
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040001DA RID: 474
		private global::AutoComboBox.AutoComboBox cmb_email2Field;

		// Token: 0x040001DB RID: 475
		private global::System.ComponentModel.Container components = null;
	}
}
