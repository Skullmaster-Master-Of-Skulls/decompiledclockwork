namespace DynamicScreens.CustomControls.DynamicControls
{
	// Token: 0x0200002E RID: 46
	public partial class CaseNewClient : global::System.Windows.Forms.Form
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0001F02C File Offset: 0x0001E02C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001F064 File Offset: 0x0001E064
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DynamicScreens.CustomControls.DynamicControls.CaseNewClient));
			this.ctrlStudentStaffGroupRoomChooser2 = new global::TechnoPro.Common.UI.WinForms.People.Controls.PeopleChoosers.CtrlStudentStaffGroupRoomChooser2();
			this.toolStrip2 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new global::System.Windows.Forms.ToolStripButton();
			this.label1 = new global::System.Windows.Forms.Label();
			this.rbtn_client = new global::System.Windows.Forms.RadioButton();
			this.rbtn_respondent = new global::System.Windows.Forms.RadioButton();
			this.label2 = new global::System.Windows.Forms.Label();
			this.btn_addNewUser = new global::System.Windows.Forms.Button();
			this.rbtn_primaryClient = new global::System.Windows.Forms.RadioButton();
			this.p_crossRef = new global::System.Windows.Forms.Panel();
			this.lv_cases = new global::AutoComboBox.ListViewEx();
			this.columnHeader4 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new global::System.Windows.Forms.ColumnHeader();
			this.label3 = new global::System.Windows.Forms.Label();
			this.p_main = new global::System.Windows.Forms.Panel();
			this.expandableSplitter1 = new global::DevComponents.DotNetBar.ExpandableSplitter();
			this.toolStrip2.SuspendLayout();
			this.p_crossRef.SuspendLayout();
			this.p_main.SuspendLayout();
			base.SuspendLayout();
			this.ctrlStudentStaffGroupRoomChooser2.AccessibleDescription = "Search for a student, staff, room or group";
			this.ctrlStudentStaffGroupRoomChooser2.AccessibleName = "Search for a student, staff, room or group";
			this.ctrlStudentStaffGroupRoomChooser2.ClearSearchBoxOnFocusLeave = false;
			this.ctrlStudentStaffGroupRoomChooser2.ClearTextOnFocusLeave = false;
			this.ctrlStudentStaffGroupRoomChooser2.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.ctrlStudentStaffGroupRoomChooser2.GroupSelectionMode = 0;
			this.ctrlStudentStaffGroupRoomChooser2.Location = new global::System.Drawing.Point(10, 69);
			this.ctrlStudentStaffGroupRoomChooser2.Margin = new global::System.Windows.Forms.Padding(3, 5, 3, 5);
			this.ctrlStudentStaffGroupRoomChooser2.Name = "ctrlStudentStaffGroupRoomChooser2";
			this.ctrlStudentStaffGroupRoomChooser2.Size = new global::System.Drawing.Size(306, 34);
			this.ctrlStudentStaffGroupRoomChooser2.TabIndex = 0;
			this.ctrlStudentStaffGroupRoomChooser2.OnResultSelected += new global::TechnoPro.Common.UI.WinForms.People.Controls.PeopleChoosers.PersonChooserCalendarEventHandler(this.ctrlStudentStaffGroupRoomChooser2_OnResultSelected);
			this.toolStrip2.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip2.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip2.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_ok,
				this.toolStripButton2
			});
			this.toolStrip2.Location = new global::System.Drawing.Point(0, 195);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Padding = new global::System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStrip2.Size = new global::System.Drawing.Size(764, 39);
			this.toolStrip2.TabIndex = 12;
			this.toolStrip2.Text = "toolStrip2";
			this.btn_ok.Image = global::DynamicScreens.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.toolStripButton2.Image = global::DynamicScreens.Properties.Resources.delete2;
			this.toolStripButton2.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new global::System.Drawing.Size(93, 36);
			this.toolStripButton2.Text = "&Cancel";
			this.toolStripButton2.Click += new global::System.EventHandler(this.toolStripButton2_Click);
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(9, 48);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(93, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Existing user:";
			this.rbtn_client.AutoSize = true;
			this.rbtn_client.Location = new global::System.Drawing.Point(147, 12);
			this.rbtn_client.Name = "rbtn_client";
			this.rbtn_client.Size = new global::System.Drawing.Size(59, 20);
			this.rbtn_client.TabIndex = 14;
			this.rbtn_client.TabStop = true;
			this.rbtn_client.Text = "C&lient";
			this.rbtn_client.UseVisualStyleBackColor = true;
			this.rbtn_respondent.AutoSize = true;
			this.rbtn_respondent.Location = new global::System.Drawing.Point(244, 12);
			this.rbtn_respondent.Name = "rbtn_respondent";
			this.rbtn_respondent.Size = new global::System.Drawing.Size(95, 20);
			this.rbtn_respondent.TabIndex = 15;
			this.rbtn_respondent.TabStop = true;
			this.rbtn_respondent.Text = "&Respondent";
			this.rbtn_respondent.UseVisualStyleBackColor = true;
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(12, 128);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(21, 16);
			this.label2.TabIndex = 17;
			this.label2.Text = "or";
			this.btn_addNewUser.Location = new global::System.Drawing.Point(109, 120);
			this.btn_addNewUser.Name = "btn_addNewUser";
			this.btn_addNewUser.Size = new global::System.Drawing.Size(119, 32);
			this.btn_addNewUser.TabIndex = 18;
			this.btn_addNewUser.Text = "Add new";
			this.btn_addNewUser.UseVisualStyleBackColor = true;
			this.btn_addNewUser.Click += new global::System.EventHandler(this.btn_addNewUser_Click);
			this.rbtn_primaryClient.AutoSize = true;
			this.rbtn_primaryClient.Location = new global::System.Drawing.Point(12, 12);
			this.rbtn_primaryClient.Name = "rbtn_primaryClient";
			this.rbtn_primaryClient.Size = new global::System.Drawing.Size(106, 20);
			this.rbtn_primaryClient.TabIndex = 19;
			this.rbtn_primaryClient.TabStop = true;
			this.rbtn_primaryClient.Text = "&Primary client";
			this.rbtn_primaryClient.UseVisualStyleBackColor = true;
			this.p_crossRef.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.p_crossRef.Controls.Add(this.lv_cases);
			this.p_crossRef.Controls.Add(this.label3);
			this.p_crossRef.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.p_crossRef.Location = new global::System.Drawing.Point(392, 0);
			this.p_crossRef.Name = "p_crossRef";
			this.p_crossRef.Size = new global::System.Drawing.Size(372, 195);
			this.p_crossRef.TabIndex = 20;
			this.lv_cases.AutoSortingEnabled = false;
			this.lv_cases.BackColourSelected = global::System.Drawing.Color.LightBlue;
			this.lv_cases.CalcButtonCid = 0;
			this.lv_cases.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader4,
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3
			});
			this.lv_cases.DefaultSortByAsc = true;
			this.lv_cases.DefaultSortByColInd = -1;
			this.lv_cases.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lv_cases.DrawMode = global::System.Windows.Forms.DrawMode.Normal;
			this.lv_cases.EmailTemplateId = 0;
			this.lv_cases.EnterTriggersDoubleClickEvent = false;
			this.lv_cases.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lv_cases.FullRowSelect = true;
			this.lv_cases.GridLines = true;
			this.lv_cases.IsFileList = false;
			this.lv_cases.ItemHeight = 16;
			this.lv_cases.Location = new global::System.Drawing.Point(0, 16);
			this.lv_cases.Name = "lv_cases";
			this.lv_cases.NoDeleting = false;
			this.lv_cases.NoEditing = false;
			this.lv_cases.Size = new global::System.Drawing.Size(368, 175);
			this.lv_cases.TabIndex = 15;
			this.lv_cases.Tag2 = null;
			this.lv_cases.UseCompatibleStateImageBehavior = false;
			this.lv_cases.View = global::System.Windows.Forms.View.Details;
			this.columnHeader4.Text = "Case #";
			this.columnHeader4.Width = 83;
			this.columnHeader1.Text = "Date";
			this.columnHeader1.Width = 91;
			this.columnHeader2.Text = "Status";
			this.columnHeader2.Width = 87;
			this.columnHeader3.Text = "Who";
			this.columnHeader3.Width = 78;
			this.label3.AutoSize = true;
			this.label3.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label3.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(234, 16);
			this.label3.TabIndex = 14;
			this.label3.Text = "Existing cases for the selected user:";
			this.p_main.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.p_main.Controls.Add(this.ctrlStudentStaffGroupRoomChooser2);
			this.p_main.Controls.Add(this.rbtn_primaryClient);
			this.p_main.Controls.Add(this.label1);
			this.p_main.Controls.Add(this.rbtn_client);
			this.p_main.Controls.Add(this.btn_addNewUser);
			this.p_main.Controls.Add(this.rbtn_respondent);
			this.p_main.Controls.Add(this.label2);
			this.p_main.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.p_main.Location = new global::System.Drawing.Point(0, 0);
			this.p_main.Name = "p_main";
			this.p_main.Size = new global::System.Drawing.Size(382, 195);
			this.p_main.TabIndex = 21;
			this.expandableSplitter1.BackColor2 = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.BackColor2SchemePart = 53;
			this.expandableSplitter1.BackColorSchemePart = 51;
			this.expandableSplitter1.ExpandableControl = this.p_crossRef;
			this.expandableSplitter1.ExpandFillColor = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.ExpandFillColorSchemePart = 53;
			this.expandableSplitter1.ExpandLineColor = global::System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.ExpandLineColorSchemePart = 40;
			this.expandableSplitter1.GripDarkColor = global::System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.GripDarkColorSchemePart = 40;
			this.expandableSplitter1.GripLightColor = global::System.Drawing.Color.FromArgb(223, 237, 254);
			this.expandableSplitter1.GripLightColorSchemePart = 0;
			this.expandableSplitter1.HotBackColor = global::System.Drawing.Color.FromArgb(254, 142, 75);
			this.expandableSplitter1.HotBackColor2 = global::System.Drawing.Color.FromArgb(255, 207, 139);
			this.expandableSplitter1.HotBackColor2SchemePart = 35;
			this.expandableSplitter1.HotBackColorSchemePart = 34;
			this.expandableSplitter1.HotExpandFillColor = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.HotExpandFillColorSchemePart = 53;
			this.expandableSplitter1.HotExpandLineColor = global::System.Drawing.SystemColors.ControlText;
			this.expandableSplitter1.HotExpandLineColorSchemePart = 40;
			this.expandableSplitter1.HotGripDarkColor = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.HotGripDarkColorSchemePart = 53;
			this.expandableSplitter1.HotGripLightColor = global::System.Drawing.Color.FromArgb(223, 237, 254);
			this.expandableSplitter1.HotGripLightColorSchemePart = 0;
			this.expandableSplitter1.Location = new global::System.Drawing.Point(382, 0);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new global::System.Drawing.Size(10, 195);
			this.expandableSplitter1.TabIndex = 22;
			this.expandableSplitter1.TabStop = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(764, 234);
			base.Controls.Add(this.p_crossRef);
			base.Controls.Add(this.expandableSplitter1);
			base.Controls.Add(this.p_main);
			base.Controls.Add(this.toolStrip2);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "CaseNewClient";
			this.Text = "New client or respondent";
			base.Load += new global::System.EventHandler(this.CaseNewClient_Load);
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.p_crossRef.ResumeLayout(false);
			this.p_crossRef.PerformLayout();
			this.p_main.ResumeLayout(false);
			this.p_main.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001D7 RID: 471
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040001D8 RID: 472
		private global::TechnoPro.Common.UI.WinForms.People.Controls.PeopleChoosers.CtrlStudentStaffGroupRoomChooser2 ctrlStudentStaffGroupRoomChooser2;

		// Token: 0x040001D9 RID: 473
		private global::System.Windows.Forms.ToolStrip toolStrip2;

		// Token: 0x040001DA RID: 474
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x040001DB RID: 475
		private global::System.Windows.Forms.ToolStripButton toolStripButton2;

		// Token: 0x040001DC RID: 476
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040001DD RID: 477
		private global::System.Windows.Forms.RadioButton rbtn_client;

		// Token: 0x040001DE RID: 478
		private global::System.Windows.Forms.RadioButton rbtn_respondent;

		// Token: 0x040001DF RID: 479
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040001E0 RID: 480
		private global::System.Windows.Forms.Button btn_addNewUser;

		// Token: 0x040001E1 RID: 481
		private global::System.Windows.Forms.RadioButton rbtn_primaryClient;

		// Token: 0x040001E2 RID: 482
		private global::System.Windows.Forms.Panel p_crossRef;

		// Token: 0x040001E3 RID: 483
		private global::AutoComboBox.ListViewEx lv_cases;

		// Token: 0x040001E4 RID: 484
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040001E5 RID: 485
		private global::System.Windows.Forms.Panel p_main;

		// Token: 0x040001E6 RID: 486
		private global::DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;

		// Token: 0x040001E7 RID: 487
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x040001E8 RID: 488
		private global::System.Windows.Forms.ColumnHeader columnHeader2;

		// Token: 0x040001E9 RID: 489
		private global::System.Windows.Forms.ColumnHeader columnHeader3;

		// Token: 0x040001EA RID: 490
		private global::System.Windows.Forms.ColumnHeader columnHeader4;
	}
}
