namespace DynamicScreens.CustomControls.DynamicControls
{
	// Token: 0x0200000A RID: 10
	public partial class CaseDetail : global::System.Windows.Forms.Form
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00005DD0 File Offset: 0x00004DD0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005E08 File Offset: 0x00004E08
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DynamicScreens.CustomControls.DynamicControls.CaseDetail));
			this.toolStrip2 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new global::System.Windows.Forms.ToolStripButton();
			this.p_top = new global::DevComponents.DotNetBar.PanelEx();
			this.p_firstNameLastName = new global::System.Windows.Forms.Panel();
			this.p_student_no = new global::System.Windows.Forms.Panel();
			this.txt_title = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			this.p_dateAdded = new global::System.Windows.Forms.Panel();
			this.dtp_dateAdded = new global::AutoComboBox.MyDateTimePicker();
			this.label2 = new global::System.Windows.Forms.Label();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.txt_student_no = new global::System.Windows.Forms.TextBox();
			this.lbl_studentNumNonEditable = new global::System.Windows.Forms.Label();
			this.p_clientsRespondents = new global::System.Windows.Forms.Panel();
			this.lv_clientsRespondents = new global::AutoComboBox.ListViewEx();
			this.columnHeader3 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new global::System.Windows.Forms.ColumnHeader();
			this.ts_clientsRespondents = new global::System.Windows.Forms.ToolStrip();
			this.btn_newClient = new global::System.Windows.Forms.ToolStripButton();
			this.btn_newRespondent = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_removeSelectedClientRespondent = new global::System.Windows.Forms.ToolStripButton();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.cm_clientsRespondents = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editSelectedToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.deleteSelectedToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.crossreferenceSearchToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.il_clientsRespondentsList = new global::System.Windows.Forms.ImageList(this.components);
			this.expandableSplitter1 = new global::DevComponents.DotNetBar.ExpandableSplitter();
			this.p_data = new global::AutoComboBox.MyPanel();
			this.toolStrip2.SuspendLayout();
			this.p_top.SuspendLayout();
			this.p_student_no.SuspendLayout();
			this.p_dateAdded.SuspendLayout();
			this.panel3.SuspendLayout();
			this.p_clientsRespondents.SuspendLayout();
			this.ts_clientsRespondents.SuspendLayout();
			this.cm_clientsRespondents.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip2.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip2.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip2.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_ok,
				this.toolStripButton2
			});
			this.toolStrip2.Location = new global::System.Drawing.Point(0, 623);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Padding = new global::System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStrip2.Size = new global::System.Drawing.Size(984, 39);
			this.toolStrip2.TabIndex = 11;
			this.toolStrip2.Text = "toolStrip2";
			this.btn_ok.Image = global::DynamicScreens.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(80, 36);
			this.btn_ok.Text = "&Save";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.toolStripButton2.Image = global::DynamicScreens.Properties.Resources.delete2;
			this.toolStripButton2.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new global::System.Drawing.Size(93, 36);
			this.toolStripButton2.Text = "&Cancel";
			this.toolStripButton2.Click += new global::System.EventHandler(this.toolStripButton2_Click);
			this.p_top.ColorSchemeStyle = 3;
			this.p_top.Controls.Add(this.p_firstNameLastName);
			this.p_top.Controls.Add(this.p_student_no);
			this.p_top.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_top.Location = new global::System.Drawing.Point(0, 0);
			this.p_top.Name = "p_top";
			this.p_top.Size = new global::System.Drawing.Size(984, 30);
			this.p_top.Style.BackColor1.Color = global::System.Drawing.SystemColors.HotTrack;
			this.p_top.Style.BackColor2.Color = global::System.Drawing.SystemColors.HotTrack;
			this.p_top.Style.Border = 2;
			this.p_top.Style.GradientAngle = 45;
			this.p_top.TabIndex = 12;
			this.p_firstNameLastName.BackColor = global::System.Drawing.Color.Transparent;
			this.p_firstNameLastName.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_firstNameLastName.Location = new global::System.Drawing.Point(0, 24);
			this.p_firstNameLastName.Name = "p_firstNameLastName";
			this.p_firstNameLastName.Padding = new global::System.Windows.Forms.Padding(1);
			this.p_firstNameLastName.Size = new global::System.Drawing.Size(984, 26);
			this.p_firstNameLastName.TabIndex = 20;
			this.p_student_no.BackColor = global::System.Drawing.Color.Transparent;
			this.p_student_no.Controls.Add(this.txt_title);
			this.p_student_no.Controls.Add(this.label3);
			this.p_student_no.Controls.Add(this.label1);
			this.p_student_no.Controls.Add(this.p_dateAdded);
			this.p_student_no.Controls.Add(this.panel3);
			this.p_student_no.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_student_no.Location = new global::System.Drawing.Point(0, 0);
			this.p_student_no.Name = "p_student_no";
			this.p_student_no.Padding = new global::System.Windows.Forms.Padding(1);
			this.p_student_no.Size = new global::System.Drawing.Size(984, 24);
			this.p_student_no.TabIndex = 1;
			this.txt_title.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_title.Location = new global::System.Drawing.Point(342, 1);
			this.txt_title.Name = "txt_title";
			this.txt_title.Size = new global::System.Drawing.Size(352, 20);
			this.txt_title.TabIndex = 8;
			this.label3.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.label3.Location = new global::System.Drawing.Point(694, 1);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(19, 22);
			this.label3.TabIndex = 9;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.label1.Font = new global::System.Drawing.Font("Arial", 10f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.ForeColor = global::System.Drawing.SystemColors.HighlightText;
			this.label1.Location = new global::System.Drawing.Point(265, 1);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(77, 22);
			this.label1.TabIndex = 7;
			this.label1.Text = "Title:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.p_dateAdded.Controls.Add(this.dtp_dateAdded);
			this.p_dateAdded.Controls.Add(this.label2);
			this.p_dateAdded.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.p_dateAdded.Location = new global::System.Drawing.Point(713, 1);
			this.p_dateAdded.Name = "p_dateAdded";
			this.p_dateAdded.Size = new global::System.Drawing.Size(270, 22);
			this.p_dateAdded.TabIndex = 6;
			this.dtp_dateAdded.BaseValue = new global::System.DateTime(2009, 10, 27, 11, 15, 14, 502);
			this.dtp_dateAdded.CustomFormat = "MMMM dd, yyyy";
			this.dtp_dateAdded.DefaultCustomFormat = null;
			this.dtp_dateAdded.Enabled = false;
			this.dtp_dateAdded.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.dtp_dateAdded.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_dateAdded.GreyedOut = false;
			this.dtp_dateAdded.Location = new global::System.Drawing.Point(0, 0);
			this.dtp_dateAdded.Name = "dtp_dateAdded";
			this.dtp_dateAdded.Size = new global::System.Drawing.Size(200, 22);
			this.dtp_dateAdded.TabIndex = 0;
			this.dtp_dateAdded.Value = new global::System.DateTime(2009, 10, 27, 11, 15, 14, 502);
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.label2.Font = new global::System.Drawing.Font("Arial", 10f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.ForeColor = global::System.Drawing.SystemColors.HighlightText;
			this.label2.Location = new global::System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(94, 22);
			this.label2.TabIndex = 4;
			this.label2.Text = "Date Added:";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.panel3.Controls.Add(this.txt_student_no);
			this.panel3.Controls.Add(this.lbl_studentNumNonEditable);
			this.panel3.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.panel3.Location = new global::System.Drawing.Point(1, 1);
			this.panel3.Name = "panel3";
			this.panel3.Padding = new global::System.Windows.Forms.Padding(1);
			this.panel3.Size = new global::System.Drawing.Size(264, 22);
			this.panel3.TabIndex = 2;
			this.txt_student_no.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
			this.txt_student_no.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_student_no.Location = new global::System.Drawing.Point(94, 1);
			this.txt_student_no.Name = "txt_student_no";
			this.txt_student_no.ReadOnly = true;
			this.txt_student_no.Size = new global::System.Drawing.Size(169, 20);
			this.txt_student_no.TabIndex = 4;
			this.lbl_studentNumNonEditable.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.lbl_studentNumNonEditable.Font = new global::System.Drawing.Font("Arial", 10f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_studentNumNonEditable.ForeColor = global::System.Drawing.SystemColors.HighlightText;
			this.lbl_studentNumNonEditable.Location = new global::System.Drawing.Point(1, 1);
			this.lbl_studentNumNonEditable.Name = "lbl_studentNumNonEditable";
			this.lbl_studentNumNonEditable.Size = new global::System.Drawing.Size(93, 20);
			this.lbl_studentNumNonEditable.TabIndex = 3;
			this.lbl_studentNumNonEditable.Text = "Case#:";
			this.lbl_studentNumNonEditable.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.p_clientsRespondents.Controls.Add(this.lv_clientsRespondents);
			this.p_clientsRespondents.Controls.Add(this.ts_clientsRespondents);
			this.p_clientsRespondents.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.p_clientsRespondents.Location = new global::System.Drawing.Point(0, 30);
			this.p_clientsRespondents.Name = "p_clientsRespondents";
			this.p_clientsRespondents.Size = new global::System.Drawing.Size(213, 593);
			this.p_clientsRespondents.TabIndex = 13;
			this.lv_clientsRespondents.AutoSortingEnabled = false;
			this.lv_clientsRespondents.BackColourSelected = global::System.Drawing.Color.LightBlue;
			this.lv_clientsRespondents.CalcButtonCid = 0;
			this.lv_clientsRespondents.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader3,
				this.columnHeader4
			});
			this.lv_clientsRespondents.DefaultSortByAsc = true;
			this.lv_clientsRespondents.DefaultSortByColInd = -1;
			this.lv_clientsRespondents.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lv_clientsRespondents.DrawMode = global::System.Windows.Forms.DrawMode.Normal;
			this.lv_clientsRespondents.EmailTemplateId = 0;
			this.lv_clientsRespondents.EnterTriggersDoubleClickEvent = false;
			this.lv_clientsRespondents.FullRowSelect = true;
			this.lv_clientsRespondents.GridLines = true;
			this.lv_clientsRespondents.IsFileList = false;
			this.lv_clientsRespondents.ItemHeight = 16;
			this.lv_clientsRespondents.Location = new global::System.Drawing.Point(0, 46);
			this.lv_clientsRespondents.Name = "lv_clientsRespondents";
			this.lv_clientsRespondents.NoDeleting = false;
			this.lv_clientsRespondents.NoEditing = false;
			this.lv_clientsRespondents.Size = new global::System.Drawing.Size(213, 547);
			this.lv_clientsRespondents.TabIndex = 0;
			this.lv_clientsRespondents.Tag2 = null;
			this.lv_clientsRespondents.UseCompatibleStateImageBehavior = false;
			this.lv_clientsRespondents.View = global::System.Windows.Forms.View.Details;
			this.columnHeader3.Text = "Client/Respondent";
			this.columnHeader3.Width = 124;
			this.columnHeader4.Text = "Type";
			this.ts_clientsRespondents.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ts_clientsRespondents.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_newClient,
				this.btn_newRespondent,
				this.toolStripSeparator1,
				this.btn_removeSelectedClientRespondent
			});
			this.ts_clientsRespondents.LayoutStyle = global::System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.ts_clientsRespondents.Location = new global::System.Drawing.Point(0, 0);
			this.ts_clientsRespondents.Name = "ts_clientsRespondents";
			this.ts_clientsRespondents.Size = new global::System.Drawing.Size(213, 46);
			this.ts_clientsRespondents.TabIndex = 0;
			this.ts_clientsRespondents.TabStop = true;
			this.ts_clientsRespondents.Text = "Clients and respondents";
			this.btn_newClient.Image = global::DynamicScreens.Properties.Resources.star_yellow_new;
			this.btn_newClient.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_newClient.Name = "btn_newClient";
			this.btn_newClient.Size = new global::System.Drawing.Size(83, 20);
			this.btn_newClient.Text = "New client";
			this.btn_newClient.Click += new global::System.EventHandler(this.btn_newClient_Click);
			this.btn_newRespondent.Image = global::DynamicScreens.Properties.Resources.star_yellow_new;
			this.btn_newRespondent.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_newRespondent.Name = "btn_newRespondent";
			this.btn_newRespondent.Size = new global::System.Drawing.Size(114, 20);
			this.btn_newRespondent.Text = "New respondent";
			this.btn_newRespondent.Click += new global::System.EventHandler(this.btn_newRespondent_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 23);
			this.btn_removeSelectedClientRespondent.Image = global::DynamicScreens.Properties.Resources.delete;
			this.btn_removeSelectedClientRespondent.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_removeSelectedClientRespondent.Name = "btn_removeSelectedClientRespondent";
			this.btn_removeSelectedClientRespondent.Size = new global::System.Drawing.Size(116, 20);
			this.btn_removeSelectedClientRespondent.Text = "Remove selected";
			this.btn_removeSelectedClientRespondent.Click += new global::System.EventHandler(this.btn_removeSelectedClientRespondent_Click);
			this.columnHeader1.Text = "Client /respondent";
			this.columnHeader1.Width = 148;
			this.columnHeader2.Text = "Type";
			this.cm_clientsRespondents.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItem1,
				this.toolStripMenuItem2,
				this.editSelectedToolStripMenuItem,
				this.toolStripMenuItem3,
				this.deleteSelectedToolStripMenuItem,
				this.toolStripMenuItem4,
				this.crossreferenceSearchToolStripMenuItem
			});
			this.cm_clientsRespondents.Name = "cm_clientsRespondents";
			this.cm_clientsRespondents.Size = new global::System.Drawing.Size(195, 126);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new global::System.Drawing.Size(194, 22);
			this.toolStripMenuItem1.Text = "New client";
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new global::System.Drawing.Size(194, 22);
			this.toolStripMenuItem2.Text = "New respondent";
			this.editSelectedToolStripMenuItem.Name = "editSelectedToolStripMenuItem";
			this.editSelectedToolStripMenuItem.Size = new global::System.Drawing.Size(194, 22);
			this.editSelectedToolStripMenuItem.Text = "&Edit selected";
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new global::System.Drawing.Size(191, 6);
			this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
			this.deleteSelectedToolStripMenuItem.Size = new global::System.Drawing.Size(194, 22);
			this.deleteSelectedToolStripMenuItem.Text = "Delete selected";
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new global::System.Drawing.Size(191, 6);
			this.crossreferenceSearchToolStripMenuItem.Name = "crossreferenceSearchToolStripMenuItem";
			this.crossreferenceSearchToolStripMenuItem.Size = new global::System.Drawing.Size(194, 22);
			this.crossreferenceSearchToolStripMenuItem.Text = "Cross-reference search";
			this.il_clientsRespondentsList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("il_clientsRespondentsList.ImageStream");
			this.il_clientsRespondentsList.TransparentColor = global::System.Drawing.Color.Transparent;
			this.il_clientsRespondentsList.Images.SetKeyName(0, "blank.png");
			this.il_clientsRespondentsList.Images.SetKeyName(1, "bullet_square_blue.png");
			this.il_clientsRespondentsList.Images.SetKeyName(2, "bullet_square_green.png");
			this.expandableSplitter1.BackColor2 = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.BackColor2SchemePart = 53;
			this.expandableSplitter1.BackColorSchemePart = 51;
			this.expandableSplitter1.ExpandableControl = this.p_clientsRespondents;
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
			this.expandableSplitter1.Location = new global::System.Drawing.Point(213, 30);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new global::System.Drawing.Size(10, 593);
			this.expandableSplitter1.TabIndex = 14;
			this.expandableSplitter1.TabStop = false;
			this.p_data.BalloonTip = null;
			this.p_data.Caption = "";
			this.p_data.DefaultActiveControl = 0;
			this.p_data.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.p_data.FirstName = null;
			this.p_data.IsDynamicScreenContainer = false;
			this.p_data.IsTopLevelDynamicControlsContainer = false;
			this.p_data.LastName = null;
			this.p_data.Location = new global::System.Drawing.Point(223, 30);
			this.p_data.Name = "p_data";
			this.p_data.Pid = 0;
			this.p_data.PrimaryClientDescription = null;
			this.p_data.PrimaryClientPid = 0;
			this.p_data.Screen = null;
			this.p_data.Size = new global::System.Drawing.Size(761, 593);
			this.p_data.Student_no = null;
			this.p_data.TabIndex = 1;
			this.p_data.Tag2 = null;
			this.p_data.Tag3 = null;
			this.p_data.TagInt = -1;
			this.p_data.Tooltip = null;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(984, 662);
			base.Controls.Add(this.p_data);
			base.Controls.Add(this.expandableSplitter1);
			base.Controls.Add(this.p_clientsRespondents);
			base.Controls.Add(this.p_top);
			base.Controls.Add(this.toolStrip2);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "CaseDetail";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Case Detail";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.CaseDetail_Load);
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.p_top.ResumeLayout(false);
			this.p_student_no.ResumeLayout(false);
			this.p_student_no.PerformLayout();
			this.p_dateAdded.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.p_clientsRespondents.ResumeLayout(false);
			this.p_clientsRespondents.PerformLayout();
			this.ts_clientsRespondents.ResumeLayout(false);
			this.ts_clientsRespondents.PerformLayout();
			this.cm_clientsRespondents.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000035 RID: 53
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000036 RID: 54
		private global::System.Windows.Forms.ToolStrip toolStrip2;

		// Token: 0x04000037 RID: 55
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x04000038 RID: 56
		private global::System.Windows.Forms.ToolStripButton toolStripButton2;

		// Token: 0x04000039 RID: 57
		private global::DevComponents.DotNetBar.PanelEx p_top;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.Panel p_firstNameLastName;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.Panel p_student_no;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.Panel p_dateAdded;

		// Token: 0x0400003D RID: 61
		private global::AutoComboBox.MyDateTimePicker dtp_dateAdded;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400003F RID: 63
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x04000040 RID: 64
		private global::System.Windows.Forms.TextBox txt_student_no;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.Label lbl_studentNumNonEditable;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.Panel p_clientsRespondents;

		// Token: 0x04000043 RID: 67
		private global::DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.ToolStrip ts_clientsRespondents;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.ToolStripButton btn_newClient;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.ToolStripButton btn_newRespondent;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.ToolStripButton btn_removeSelectedClientRespondent;

		// Token: 0x04000049 RID: 73
		private global::AutoComboBox.ListViewEx lv_clientsRespondents;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.ContextMenuStrip cm_clientsRespondents;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.ToolStripMenuItem editSelectedToolStripMenuItem;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.ColumnHeader columnHeader2;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

		// Token: 0x0400004F RID: 79
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

		// Token: 0x04000050 RID: 80
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;

		// Token: 0x04000051 RID: 81
		private global::System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;

		// Token: 0x04000052 RID: 82
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;

		// Token: 0x04000053 RID: 83
		private global::System.Windows.Forms.ToolStripMenuItem crossreferenceSearchToolStripMenuItem;

		// Token: 0x04000054 RID: 84
		private global::System.Windows.Forms.ImageList il_clientsRespondentsList;

		// Token: 0x04000055 RID: 85
		private global::AutoComboBox.MyPanel p_data;

		// Token: 0x04000056 RID: 86
		private global::System.Windows.Forms.ColumnHeader columnHeader3;

		// Token: 0x04000057 RID: 87
		private global::System.Windows.Forms.ColumnHeader columnHeader4;

		// Token: 0x04000058 RID: 88
		private global::System.Windows.Forms.TextBox txt_title;

		// Token: 0x04000059 RID: 89
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400005A RID: 90
		private global::System.Windows.Forms.Label label3;
	}
}
