namespace AutoComboBox.InputDialogControls
{
	// Token: 0x0200002B RID: 43
	public partial class InputPersonChooser : global::System.Windows.Forms.Form
	{
		// Token: 0x0600012C RID: 300 RVA: 0x0000CF08 File Offset: 0x0000BF08
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000CF40 File Offset: 0x0000BF40
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputDialogControls.InputPersonChooser));
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.btn_close = new global::System.Windows.Forms.Button();
			this.btn_select = new global::System.Windows.Forms.Button();
			this.tabControl1 = new global::System.Windows.Forms.TabControl();
			this.tp_studentNames = new global::System.Windows.Forms.TabPage();
			this.tp_studentNumbers = new global::System.Windows.Forms.TabPage();
			this.tp_staff = new global::System.Windows.Forms.TabPage();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.cmb_studentName = new global::AutoComboBox.AutoComboBox();
			this.cmb_student_no = new global::AutoComboBox.AutoComboBox();
			this.cmb_staff = new global::AutoComboBox.AutoComboBox();
			this.tabControl1.SuspendLayout();
			this.tp_studentNames.SuspendLayout();
			this.tp_studentNumbers.SuspendLayout();
			this.tp_staff.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(6, 15);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(128, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Student by name:";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(6, 15);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(141, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Student by number:";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(6, 15);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(44, 18);
			this.label3.TabIndex = 4;
			this.label3.Text = "Staff:";
			this.btn_close.Location = new global::System.Drawing.Point(391, 6);
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new global::System.Drawing.Size(108, 33);
			this.btn_close.TabIndex = 6;
			this.btn_close.Text = "&Cancel";
			this.btn_close.UseVisualStyleBackColor = true;
			this.btn_close.Click += new global::System.EventHandler(this.button1_Click);
			this.btn_select.Location = new global::System.Drawing.Point(267, 6);
			this.btn_select.Name = "btn_select";
			this.btn_select.Size = new global::System.Drawing.Size(99, 32);
			this.btn_select.TabIndex = 7;
			this.btn_select.Text = "&Select";
			this.btn_select.UseVisualStyleBackColor = true;
			this.btn_select.Click += new global::System.EventHandler(this.btn_select_Click);
			this.tabControl1.Controls.Add(this.tp_studentNames);
			this.tabControl1.Controls.Add(this.tp_studentNumbers);
			this.tabControl1.Controls.Add(this.tp_staff);
			this.tabControl1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new global::System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new global::System.Drawing.Size(503, 90);
			this.tabControl1.TabIndex = 8;
			this.tabControl1.SelectedIndexChanged += new global::System.EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tp_studentNames.Controls.Add(this.label1);
			this.tp_studentNames.Controls.Add(this.cmb_studentName);
			this.tp_studentNames.Location = new global::System.Drawing.Point(4, 27);
			this.tp_studentNames.Name = "tp_studentNames";
			this.tp_studentNames.Padding = new global::System.Windows.Forms.Padding(3);
			this.tp_studentNames.Size = new global::System.Drawing.Size(495, 59);
			this.tp_studentNames.TabIndex = 0;
			this.tp_studentNames.Text = "Student names";
			this.tp_studentNames.UseVisualStyleBackColor = true;
			this.tp_studentNumbers.Controls.Add(this.label2);
			this.tp_studentNumbers.Controls.Add(this.cmb_student_no);
			this.tp_studentNumbers.Location = new global::System.Drawing.Point(4, 27);
			this.tp_studentNumbers.Name = "tp_studentNumbers";
			this.tp_studentNumbers.Padding = new global::System.Windows.Forms.Padding(3);
			this.tp_studentNumbers.Size = new global::System.Drawing.Size(495, 59);
			this.tp_studentNumbers.TabIndex = 1;
			this.tp_studentNumbers.Text = "Student numbers";
			this.tp_studentNumbers.UseVisualStyleBackColor = true;
			this.tp_staff.Controls.Add(this.label3);
			this.tp_staff.Controls.Add(this.cmb_staff);
			this.tp_staff.Location = new global::System.Drawing.Point(4, 27);
			this.tp_staff.Name = "tp_staff";
			this.tp_staff.Size = new global::System.Drawing.Size(495, 59);
			this.tp_staff.TabIndex = 2;
			this.tp_staff.Text = "Staff";
			this.tp_staff.UseVisualStyleBackColor = true;
			this.panel1.Controls.Add(this.btn_select);
			this.panel1.Controls.Add(this.btn_close);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new global::System.Drawing.Point(0, 90);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(503, 47);
			this.panel1.TabIndex = 9;
			this.cmb_studentName.AccessibleDescription = "Student by name";
			this.cmb_studentName.AccessibleName = "Student by name";
			this.cmb_studentName.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_studentName.AllowUserToEnterAnyText = true;
			this.cmb_studentName.AutoCompleteEnabled = true;
			this.cmb_studentName.ChildLookupGroupId = 0;
			this.cmb_studentName.FormattingEnabled = true;
			this.cmb_studentName.GotoNextItemOnDoubleClick = false;
			this.cmb_studentName.Location = new global::System.Drawing.Point(175, 15);
			this.cmb_studentName.LookupGroupId = 0;
			this.cmb_studentName.Margin = new global::System.Windows.Forms.Padding(4);
			this.cmb_studentName.Name = "cmb_studentName";
			this.cmb_studentName.Size = new global::System.Drawing.Size(316, 26);
			this.cmb_studentName.TabIndex = 0;
			this.cmb_studentName.TryToSelectOnFocusLeave = true;
			this.cmb_studentName.EnterPressed += new global::System.Windows.Forms.KeyPressEventHandler(this.cmb_studentName_EnterPressed);
			this.cmb_student_no.AccessibleDescription = "Student by number";
			this.cmb_student_no.AccessibleName = "Student by number";
			this.cmb_student_no.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_student_no.AllowUserToEnterAnyText = true;
			this.cmb_student_no.AutoCompleteEnabled = true;
			this.cmb_student_no.ChildLookupGroupId = 0;
			this.cmb_student_no.FormattingEnabled = true;
			this.cmb_student_no.GotoNextItemOnDoubleClick = false;
			this.cmb_student_no.Location = new global::System.Drawing.Point(175, 15);
			this.cmb_student_no.LookupGroupId = 0;
			this.cmb_student_no.Margin = new global::System.Windows.Forms.Padding(4);
			this.cmb_student_no.Name = "cmb_student_no";
			this.cmb_student_no.Size = new global::System.Drawing.Size(206, 26);
			this.cmb_student_no.TabIndex = 3;
			this.cmb_student_no.TryToSelectOnFocusLeave = true;
			this.cmb_student_no.EnterPressed += new global::System.Windows.Forms.KeyPressEventHandler(this.cmb_student_no_EnterPressed);
			this.cmb_staff.AccessibleDescription = "Staff";
			this.cmb_staff.AccessibleName = "Staff";
			this.cmb_staff.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_staff.AllowUserToEnterAnyText = true;
			this.cmb_staff.AutoCompleteEnabled = true;
			this.cmb_staff.ChildLookupGroupId = 0;
			this.cmb_staff.FormattingEnabled = true;
			this.cmb_staff.GotoNextItemOnDoubleClick = false;
			this.cmb_staff.Location = new global::System.Drawing.Point(175, 15);
			this.cmb_staff.LookupGroupId = 0;
			this.cmb_staff.Margin = new global::System.Windows.Forms.Padding(4);
			this.cmb_staff.Name = "cmb_staff";
			this.cmb_staff.Size = new global::System.Drawing.Size(316, 26);
			this.cmb_staff.TabIndex = 5;
			this.cmb_staff.TryToSelectOnFocusLeave = true;
			this.cmb_staff.EnterPressed += new global::System.Windows.Forms.KeyPressEventHandler(this.cmb_staff_EnterPressed);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(9f, 18f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(503, 137);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.panel1);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Margin = new global::System.Windows.Forms.Padding(4);
			base.Name = "InputPersonChooser";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Choose a student or staff";
			base.Load += new global::System.EventHandler(this.InputPersonChooser_Load);
			base.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.InputPersonChooser_KeyUp);
			this.tabControl1.ResumeLayout(false);
			this.tp_studentNames.ResumeLayout(false);
			this.tp_studentNames.PerformLayout();
			this.tp_studentNumbers.ResumeLayout(false);
			this.tp_studentNumbers.PerformLayout();
			this.tp_staff.ResumeLayout(false);
			this.tp_staff.PerformLayout();
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000170 RID: 368
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000171 RID: 369
		private global::AutoComboBox.AutoComboBox cmb_studentName;

		// Token: 0x04000172 RID: 370
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000173 RID: 371
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000174 RID: 372
		private global::AutoComboBox.AutoComboBox cmb_student_no;

		// Token: 0x04000175 RID: 373
		private global::AutoComboBox.AutoComboBox cmb_staff;

		// Token: 0x04000176 RID: 374
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000177 RID: 375
		private global::System.Windows.Forms.Button btn_close;

		// Token: 0x04000178 RID: 376
		private global::System.Windows.Forms.Button btn_select;

		// Token: 0x04000179 RID: 377
		private global::System.Windows.Forms.TabControl tabControl1;

		// Token: 0x0400017A RID: 378
		private global::System.Windows.Forms.TabPage tp_studentNames;

		// Token: 0x0400017B RID: 379
		private global::System.Windows.Forms.TabPage tp_studentNumbers;

		// Token: 0x0400017C RID: 380
		private global::System.Windows.Forms.TabPage tp_staff;

		// Token: 0x0400017D RID: 381
		private global::System.Windows.Forms.Panel panel1;
	}
}
