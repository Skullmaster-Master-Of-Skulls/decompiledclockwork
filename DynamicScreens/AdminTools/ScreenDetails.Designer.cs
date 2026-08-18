namespace DynamicScreens.AdminTools
{
	// Token: 0x0200002A RID: 42
	public partial class ScreenDetails : global::System.Windows.Forms.Form
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0001CAD4 File Offset: 0x0001BAD4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001CB0C File Offset: 0x0001BB0C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DynamicScreens.AdminTools.ScreenDetails));
			this.toolStrip3 = new global::System.Windows.Forms.ToolStrip();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_close = new global::System.Windows.Forms.ToolStripButton();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.chk_showAsButton = new global::System.Windows.Forms.CheckBox();
			this.chk_enabled = new global::System.Windows.Forms.CheckBox();
			this.chk_bottomless = new global::System.Windows.Forms.CheckBox();
			this.label10 = new global::System.Windows.Forms.Label();
			this.label8 = new global::System.Windows.Forms.Label();
			this.txt_studentNumberCaption = new global::System.Windows.Forms.TextBox();
			this.txt_filledOutCid = new global::System.Windows.Forms.TextBox();
			this.txt_groupIds = new global::System.Windows.Forms.TextBox();
			this.txt_colPadding = new global::System.Windows.Forms.TextBox();
			this.label21 = new global::System.Windows.Forms.Label();
			this.label19 = new global::System.Windows.Forms.Label();
			this.label17 = new global::System.Windows.Forms.Label();
			this.label15 = new global::System.Windows.Forms.Label();
			this.label13 = new global::System.Windows.Forms.Label();
			this.label11 = new global::System.Windows.Forms.Label();
			this.label9 = new global::System.Windows.Forms.Label();
			this.txt_screenCaptionFrench = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			this.lbl_screenType = new global::System.Windows.Forms.Label();
			this.txt_screenCaption = new global::System.Windows.Forms.TextBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.btn_littleImage = new global::DevComponents.DotNetBar.ButtonX();
			this.btn_bigImage = new global::DevComponents.DotNetBar.ButtonX();
			this.txt_verticalControlPadding = new global::System.Windows.Forms.TextBox();
			this.txt_studentNumAutoGenerateRule = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.label23 = new global::System.Windows.Forms.Label();
			this.chk_studentNameIsHidden = new global::System.Windows.Forms.CheckBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.txt_groupName = new global::System.Windows.Forms.TextBox();
			this.panel4 = new global::System.Windows.Forms.Panel();
			this.btn_colWidthPercent_full = new global::System.Windows.Forms.Button();
			this.btn_colWidthPercent_half = new global::System.Windows.Forms.Button();
			this.btn_colWidthPercent_third = new global::System.Windows.Forms.Button();
			this.txt_colWidth = new global::System.Windows.Forms.TextBox();
			this.toolStrip3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel4.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip3.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip3.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip3.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip3.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip3.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_save,
				this.btn_close
			});
			this.toolStrip3.Location = new global::System.Drawing.Point(0, 577);
			this.toolStrip3.Name = "toolStrip3";
			this.toolStrip3.Size = new global::System.Drawing.Size(746, 39);
			this.toolStrip3.TabIndex = 32;
			this.toolStrip3.TabStop = true;
			this.btn_save.Image = global::DynamicScreens.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(80, 36);
			this.btn_save.Text = "&Save";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_close.Image = global::DynamicScreens.Properties.Resources.delete2;
			this.btn_close.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new global::System.Drawing.Size(85, 36);
			this.btn_close.Text = "&Close";
			this.btn_close.Click += new global::System.EventHandler(this.btn_close_Click);
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.tableLayoutPanel1);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new global::System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(746, 577);
			this.panel2.TabIndex = 4;
			this.tableLayoutPanel1.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 40f));
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 60f));
			this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.chk_bottomless, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.label10, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.txt_studentNumberCaption, 1, 12);
			this.tableLayoutPanel1.Controls.Add(this.txt_filledOutCid, 1, 11);
			this.tableLayoutPanel1.Controls.Add(this.txt_groupIds, 1, 10);
			this.tableLayoutPanel1.Controls.Add(this.txt_colPadding, 1, 9);
			this.tableLayoutPanel1.Controls.Add(this.label21, 0, 12);
			this.tableLayoutPanel1.Controls.Add(this.label19, 0, 11);
			this.tableLayoutPanel1.Controls.Add(this.label17, 0, 10);
			this.tableLayoutPanel1.Controls.Add(this.label15, 0, 9);
			this.tableLayoutPanel1.Controls.Add(this.label13, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this.label11, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.txt_screenCaptionFrench, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lbl_screenType, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txt_screenCaption, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.txt_verticalControlPadding, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.txt_studentNumAutoGenerateRule, 1, 13);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 13);
			this.tableLayoutPanel1.Controls.Add(this.label23, 0, 15);
			this.tableLayoutPanel1.Controls.Add(this.chk_studentNameIsHidden, 1, 15);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txt_groupName, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 8);
			this.tableLayoutPanel1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new global::System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 17;
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new global::System.Drawing.Size(729, 672);
			this.tableLayoutPanel1.TabIndex = 0;
			this.panel3.Controls.Add(this.chk_showAsButton);
			this.panel3.Controls.Add(this.chk_enabled);
			this.panel3.Location = new global::System.Drawing.Point(297, 134);
			this.panel3.Name = "panel3";
			this.panel3.Size = new global::System.Drawing.Size(372, 72);
			this.panel3.TabIndex = 9;
			this.chk_showAsButton.Location = new global::System.Drawing.Point(3, 37);
			this.chk_showAsButton.Name = "chk_showAsButton";
			this.chk_showAsButton.Size = new global::System.Drawing.Size(313, 28);
			this.chk_showAsButton.TabIndex = 11;
			this.chk_showAsButton.Text = "Show as button";
			this.chk_showAsButton.UseVisualStyleBackColor = true;
			this.chk_enabled.Location = new global::System.Drawing.Point(3, 3);
			this.chk_enabled.Name = "chk_enabled";
			this.chk_enabled.Size = new global::System.Drawing.Size(98, 28);
			this.chk_enabled.TabIndex = 10;
			this.chk_enabled.Text = "&Enabled";
			this.chk_enabled.UseVisualStyleBackColor = true;
			this.chk_bottomless.Location = new global::System.Drawing.Point(297, 215);
			this.chk_bottomless.Name = "chk_bottomless";
			this.chk_bottomless.Size = new global::System.Drawing.Size(388, 39);
			this.chk_bottomless.TabIndex = 12;
			this.chk_bottomless.Text = "&Bottom-less (fields will continue vertically without wrapping to the next column)";
			this.chk_bottomless.UseVisualStyleBackColor = true;
			this.label10.AutoSize = true;
			this.label10.Location = new global::System.Drawing.Point(6, 131);
			this.label10.Name = "label10";
			this.label10.Size = new global::System.Drawing.Size(0, 16);
			this.label10.TabIndex = 33;
			this.label8.AutoSize = true;
			this.label8.Location = new global::System.Drawing.Point(6, 212);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(0, 16);
			this.label8.TabIndex = 32;
			this.txt_studentNumberCaption.AccessibleDescription = "Student number caption";
			this.txt_studentNumberCaption.AccessibleName = "Student number caption";
			this.txt_studentNumberCaption.Location = new global::System.Drawing.Point(297, 512);
			this.txt_studentNumberCaption.Name = "txt_studentNumberCaption";
			this.txt_studentNumberCaption.Size = new global::System.Drawing.Size(277, 22);
			this.txt_studentNumberCaption.TabIndex = 28;
			this.txt_filledOutCid.AccessibleDescription = "Control id that indicates if this screen has been filled out";
			this.txt_filledOutCid.AccessibleName = "Control id that indicates if this screen has been filled out";
			this.txt_filledOutCid.Location = new global::System.Drawing.Point(297, 477);
			this.txt_filledOutCid.Name = "txt_filledOutCid";
			this.txt_filledOutCid.Size = new global::System.Drawing.Size(90, 22);
			this.txt_filledOutCid.TabIndex = 26;
			this.txt_groupIds.AccessibleDescription = "Group ids (list of comma separated group id numbers)";
			this.txt_groupIds.AccessibleName = "Group ids (list of comma separated group id numbers)";
			this.txt_groupIds.Location = new global::System.Drawing.Point(297, 426);
			this.txt_groupIds.Name = "txt_groupIds";
			this.txt_groupIds.Size = new global::System.Drawing.Size(385, 22);
			this.txt_groupIds.TabIndex = 24;
			this.txt_colPadding.AccessibleDescription = "Column padding in pixels";
			this.txt_colPadding.AccessibleName = "Column padding in pixels";
			this.txt_colPadding.Location = new global::System.Drawing.Point(297, 395);
			this.txt_colPadding.Name = "txt_colPadding";
			this.txt_colPadding.Size = new global::System.Drawing.Size(90, 22);
			this.txt_colPadding.TabIndex = 22;
			this.label21.AutoSize = true;
			this.label21.Location = new global::System.Drawing.Point(6, 509);
			this.label21.Name = "label21";
			this.label21.Size = new global::System.Drawing.Size(146, 16);
			this.label21.TabIndex = 27;
			this.label21.Text = "Student number caption";
			this.label19.AutoSize = true;
			this.label19.Location = new global::System.Drawing.Point(6, 474);
			this.label19.Name = "label19";
			this.label19.Size = new global::System.Drawing.Size(280, 32);
			this.label19.TabIndex = 25;
			this.label19.Text = "Control id that indicates if this screen has been filled out (for per app screens only)";
			this.label17.AutoSize = true;
			this.label17.Location = new global::System.Drawing.Point(6, 423);
			this.label17.Name = "label17";
			this.label17.Size = new global::System.Drawing.Size(274, 48);
			this.label17.TabIndex = 23;
			this.label17.Text = "Group ids (new students will be automatically added to these groups in addition to the students group)";
			this.label15.AutoSize = true;
			this.label15.Location = new global::System.Drawing.Point(6, 392);
			this.label15.Name = "label15";
			this.label15.Size = new global::System.Drawing.Size(147, 16);
			this.label15.TabIndex = 21;
			this.label15.Text = "Column padding (pixels)";
			this.label13.AutoSize = true;
			this.label13.Location = new global::System.Drawing.Point(6, 351);
			this.label13.Name = "label13";
			this.label13.Size = new global::System.Drawing.Size(181, 16);
			this.label13.TabIndex = 19;
			this.label13.Text = "Column width percent (ex. 35)";
			this.label11.AutoSize = true;
			this.label11.Location = new global::System.Drawing.Point(6, 320);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(147, 16);
			this.label11.TabIndex = 17;
			this.label11.Text = "Vertical control padding:";
			this.label9.AutoSize = true;
			this.label9.Location = new global::System.Drawing.Point(6, 260);
			this.label9.Name = "label9";
			this.label9.Size = new global::System.Drawing.Size(43, 16);
			this.label9.TabIndex = 13;
			this.label9.Text = "Icons:";
			this.txt_screenCaptionFrench.AccessibleDescription = "Screen caption french";
			this.txt_screenCaptionFrench.AccessibleName = "Screen caption french";
			this.txt_screenCaptionFrench.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_screenCaptionFrench.Location = new global::System.Drawing.Point(297, 103);
			this.txt_screenCaptionFrench.Name = "txt_screenCaptionFrench";
			this.txt_screenCaptionFrench.Size = new global::System.Drawing.Size(426, 22);
			this.txt_screenCaptionFrench.TabIndex = 8;
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(6, 100);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(145, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Screen Caption French:";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(6, 69);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(101, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Screen Caption:";
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(6, 3);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(39, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Type:";
			this.lbl_screenType.BackColor = global::System.Drawing.SystemColors.Highlight;
			this.lbl_screenType.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lbl_screenType.ForeColor = global::System.Drawing.SystemColors.HighlightText;
			this.lbl_screenType.Location = new global::System.Drawing.Point(297, 3);
			this.lbl_screenType.Name = "lbl_screenType";
			this.lbl_screenType.Size = new global::System.Drawing.Size(426, 32);
			this.lbl_screenType.TabIndex = 2;
			this.lbl_screenType.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.txt_screenCaption.AccessibleDescription = "Screen caption";
			this.txt_screenCaption.AccessibleName = "Screen caption";
			this.txt_screenCaption.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_screenCaption.Location = new global::System.Drawing.Point(297, 72);
			this.txt_screenCaption.Name = "txt_screenCaption";
			this.txt_screenCaption.Size = new global::System.Drawing.Size(426, 22);
			this.txt_screenCaption.TabIndex = 6;
			this.panel1.Controls.Add(this.btn_littleImage);
			this.panel1.Controls.Add(this.btn_bigImage);
			this.panel1.Location = new global::System.Drawing.Point(297, 263);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(259, 51);
			this.panel1.TabIndex = 14;
			this.btn_littleImage.AccessibleRole = global::System.Windows.Forms.AccessibleRole.PushButton;
			this.btn_littleImage.AntiAlias = true;
			this.btn_littleImage.ColorTable = 3;
			this.btn_littleImage.HotTrackingStyle = 3;
			this.btn_littleImage.Location = new global::System.Drawing.Point(59, 3);
			this.btn_littleImage.Name = "btn_littleImage";
			this.btn_littleImage.Size = new global::System.Drawing.Size(22, 22);
			this.btn_littleImage.TabIndex = 16;
			this.btn_littleImage.Click += new global::System.EventHandler(this.btn_littleImage_Click);
			this.btn_bigImage.AccessibleRole = global::System.Windows.Forms.AccessibleRole.PushButton;
			this.btn_bigImage.AntiAlias = true;
			this.btn_bigImage.ColorTable = 3;
			this.btn_bigImage.HotTrackingStyle = 3;
			this.btn_bigImage.Location = new global::System.Drawing.Point(3, 3);
			this.btn_bigImage.Name = "btn_bigImage";
			this.btn_bigImage.Size = new global::System.Drawing.Size(42, 42);
			this.btn_bigImage.TabIndex = 15;
			this.btn_bigImage.Click += new global::System.EventHandler(this.btn_bigImage_Click);
			this.txt_verticalControlPadding.AccessibleDescription = "Vertical control padding in pixels";
			this.txt_verticalControlPadding.AccessibleName = "Vertical control padding in pixels";
			this.txt_verticalControlPadding.Location = new global::System.Drawing.Point(297, 323);
			this.txt_verticalControlPadding.Name = "txt_verticalControlPadding";
			this.txt_verticalControlPadding.Size = new global::System.Drawing.Size(90, 22);
			this.txt_verticalControlPadding.TabIndex = 18;
			this.txt_verticalControlPadding.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.txt_studentNumAutoGenerateRule.AccessibleDescription = "Student number auto generate rule";
			this.txt_studentNumAutoGenerateRule.AccessibleName = "Student number auto generate rule";
			this.txt_studentNumAutoGenerateRule.Location = new global::System.Drawing.Point(297, 543);
			this.txt_studentNumAutoGenerateRule.Multiline = true;
			this.txt_studentNumAutoGenerateRule.Name = "txt_studentNumAutoGenerateRule";
			this.txt_studentNumAutoGenerateRule.Size = new global::System.Drawing.Size(372, 75);
			this.txt_studentNumAutoGenerateRule.TabIndex = 30;
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(6, 540);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(208, 16);
			this.label5.TabIndex = 29;
			this.label5.Text = "Student number auto generate rule";
			this.label23.AutoSize = true;
			this.label23.Location = new global::System.Drawing.Point(6, 627);
			this.label23.Name = "label23";
			this.label23.Size = new global::System.Drawing.Size(0, 16);
			this.label23.TabIndex = 24;
			this.chk_studentNameIsHidden.AccessibleDescription = "Student name hidden";
			this.chk_studentNameIsHidden.AccessibleName = "Student name hidden";
			this.chk_studentNameIsHidden.AutoSize = true;
			this.chk_studentNameIsHidden.Location = new global::System.Drawing.Point(297, 630);
			this.chk_studentNameIsHidden.Name = "chk_studentNameIsHidden";
			this.chk_studentNameIsHidden.Size = new global::System.Drawing.Size(150, 20);
			this.chk_studentNameIsHidden.TabIndex = 31;
			this.chk_studentNameIsHidden.Text = "Student name &hidden";
			this.chk_studentNameIsHidden.UseVisualStyleBackColor = true;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(6, 38);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(83, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Group name:";
			this.txt_groupName.AccessibleDescription = "Group name";
			this.txt_groupName.AccessibleName = "Group name";
			this.txt_groupName.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt_groupName.Location = new global::System.Drawing.Point(297, 41);
			this.txt_groupName.Name = "txt_groupName";
			this.txt_groupName.Size = new global::System.Drawing.Size(426, 22);
			this.txt_groupName.TabIndex = 4;
			this.panel4.Controls.Add(this.btn_colWidthPercent_full);
			this.panel4.Controls.Add(this.btn_colWidthPercent_half);
			this.panel4.Controls.Add(this.btn_colWidthPercent_third);
			this.panel4.Controls.Add(this.txt_colWidth);
			this.panel4.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new global::System.Drawing.Point(297, 354);
			this.panel4.Name = "panel4";
			this.panel4.Size = new global::System.Drawing.Size(426, 32);
			this.panel4.TabIndex = 34;
			this.btn_colWidthPercent_full.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_colWidthPercent_full.Location = new global::System.Drawing.Point(246, 3);
			this.btn_colWidthPercent_full.Name = "btn_colWidthPercent_full";
			this.btn_colWidthPercent_full.Size = new global::System.Drawing.Size(59, 23);
			this.btn_colWidthPercent_full.TabIndex = 24;
			this.btn_colWidthPercent_full.Text = "Full";
			this.btn_colWidthPercent_full.UseVisualStyleBackColor = true;
			this.btn_colWidthPercent_full.Click += new global::System.EventHandler(this.btn_colWidthPercent_full_Click);
			this.btn_colWidthPercent_half.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_colWidthPercent_half.Location = new global::System.Drawing.Point(181, 4);
			this.btn_colWidthPercent_half.Name = "btn_colWidthPercent_half";
			this.btn_colWidthPercent_half.Size = new global::System.Drawing.Size(59, 23);
			this.btn_colWidthPercent_half.TabIndex = 23;
			this.btn_colWidthPercent_half.Text = "1/2";
			this.btn_colWidthPercent_half.UseVisualStyleBackColor = true;
			this.btn_colWidthPercent_half.Click += new global::System.EventHandler(this.btn_colWidthPercent_half_Click);
			this.btn_colWidthPercent_third.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_colWidthPercent_third.Location = new global::System.Drawing.Point(116, 4);
			this.btn_colWidthPercent_third.Name = "btn_colWidthPercent_third";
			this.btn_colWidthPercent_third.Size = new global::System.Drawing.Size(59, 23);
			this.btn_colWidthPercent_third.TabIndex = 22;
			this.btn_colWidthPercent_third.Text = "1/3";
			this.btn_colWidthPercent_third.UseVisualStyleBackColor = true;
			this.btn_colWidthPercent_third.Click += new global::System.EventHandler(this.btn_colWidthPercent_third_Click);
			this.txt_colWidth.AccessibleDescription = "Column width percentage (example: 35)";
			this.txt_colWidth.AccessibleName = "Column width percentage (example: 35)";
			this.txt_colWidth.Location = new global::System.Drawing.Point(3, 3);
			this.txt_colWidth.Name = "txt_colWidth";
			this.txt_colWidth.Size = new global::System.Drawing.Size(90, 22);
			this.txt_colWidth.TabIndex = 21;
			this.txt_colWidth.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(746, 616);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.toolStrip3);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "ScreenDetails";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Screen Details";
			base.Load += new global::System.EventHandler(this.ScreenDetails_Load);
			this.toolStrip3.ResumeLayout(false);
			this.toolStrip3.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400019F RID: 415
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040001A0 RID: 416
		private global::System.Windows.Forms.ToolStrip toolStrip3;

		// Token: 0x040001A1 RID: 417
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x040001A2 RID: 418
		private global::System.Windows.Forms.ToolStripButton btn_close;

		// Token: 0x040001A3 RID: 419
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x040001A4 RID: 420
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

		// Token: 0x040001A5 RID: 421
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x040001A6 RID: 422
		private global::System.Windows.Forms.CheckBox chk_showAsButton;

		// Token: 0x040001A7 RID: 423
		private global::System.Windows.Forms.CheckBox chk_enabled;

		// Token: 0x040001A8 RID: 424
		private global::System.Windows.Forms.CheckBox chk_bottomless;

		// Token: 0x040001A9 RID: 425
		private global::System.Windows.Forms.Label label10;

		// Token: 0x040001AA RID: 426
		private global::System.Windows.Forms.Label label8;

		// Token: 0x040001AB RID: 427
		private global::System.Windows.Forms.TextBox txt_studentNumberCaption;

		// Token: 0x040001AC RID: 428
		private global::System.Windows.Forms.TextBox txt_filledOutCid;

		// Token: 0x040001AD RID: 429
		private global::System.Windows.Forms.TextBox txt_groupIds;

		// Token: 0x040001AE RID: 430
		private global::System.Windows.Forms.TextBox txt_colPadding;

		// Token: 0x040001AF RID: 431
		private global::System.Windows.Forms.Label label21;

		// Token: 0x040001B0 RID: 432
		private global::System.Windows.Forms.Label label19;

		// Token: 0x040001B1 RID: 433
		private global::System.Windows.Forms.Label label17;

		// Token: 0x040001B2 RID: 434
		private global::System.Windows.Forms.Label label15;

		// Token: 0x040001B3 RID: 435
		private global::System.Windows.Forms.Label label13;

		// Token: 0x040001B4 RID: 436
		private global::System.Windows.Forms.Label label11;

		// Token: 0x040001B5 RID: 437
		private global::System.Windows.Forms.Label label9;

		// Token: 0x040001B6 RID: 438
		private global::System.Windows.Forms.TextBox txt_screenCaptionFrench;

		// Token: 0x040001B7 RID: 439
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040001B8 RID: 440
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040001B9 RID: 441
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040001BA RID: 442
		private global::System.Windows.Forms.Label lbl_screenType;

		// Token: 0x040001BB RID: 443
		private global::System.Windows.Forms.TextBox txt_screenCaption;

		// Token: 0x040001BC RID: 444
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040001BD RID: 445
		private global::DevComponents.DotNetBar.ButtonX btn_littleImage;

		// Token: 0x040001BE RID: 446
		private global::DevComponents.DotNetBar.ButtonX btn_bigImage;

		// Token: 0x040001BF RID: 447
		private global::System.Windows.Forms.TextBox txt_verticalControlPadding;

		// Token: 0x040001C0 RID: 448
		private global::System.Windows.Forms.TextBox txt_studentNumAutoGenerateRule;

		// Token: 0x040001C1 RID: 449
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040001C2 RID: 450
		private global::System.Windows.Forms.Label label23;

		// Token: 0x040001C3 RID: 451
		private global::System.Windows.Forms.CheckBox chk_studentNameIsHidden;

		// Token: 0x040001C4 RID: 452
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040001C5 RID: 453
		private global::System.Windows.Forms.TextBox txt_groupName;

		// Token: 0x040001C6 RID: 454
		private global::System.Windows.Forms.Panel panel4;

		// Token: 0x040001C7 RID: 455
		private global::System.Windows.Forms.Button btn_colWidthPercent_third;

		// Token: 0x040001C8 RID: 456
		private global::System.Windows.Forms.TextBox txt_colWidth;

		// Token: 0x040001C9 RID: 457
		private global::System.Windows.Forms.Button btn_colWidthPercent_full;

		// Token: 0x040001CA RID: 458
		private global::System.Windows.Forms.Button btn_colWidthPercent_half;
	}
}
