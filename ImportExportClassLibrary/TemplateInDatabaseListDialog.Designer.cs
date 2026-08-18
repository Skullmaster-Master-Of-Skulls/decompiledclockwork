namespace ImportExportClassLibrary
{
	// Token: 0x02000026 RID: 38
	public partial class TemplateInDatabaseListDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00007A0D File Offset: 0x00006A0D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007A2C File Offset: 0x00006A2C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ImportExportClassLibrary.TemplateInDatabaseListDialog));
			this.lv_templates = new global::DevComponents.DotNetBar.Controls.ListViewEx();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.cm_templates = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.editEmailForThisTemplateToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.rtf_preview = new global::System.Windows.Forms.RichTextBox();
			this.lbl_captionMessage = new global::DevComponents.DotNetBar.LabelX();
			this.btn_refreshPreview = new global::System.Windows.Forms.Button();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_selectTemplate = new global::System.Windows.Forms.ToolStripButton();
			this.btn_exportToExcel = new global::System.Windows.Forms.ToolStripButton();
			this.btn_chooseAFile = new global::System.Windows.Forms.ToolStripButton();
			this.btn_useBlankTemplate = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.splitContainer1 = new global::System.Windows.Forms.SplitContainer();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.toolstrip_modifyTemplates = new global::System.Windows.Forms.ToolStrip();
			this.btn_addTemplate = new global::System.Windows.Forms.ToolStripButton();
			this.btn_editTemplate = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_replaceTemplate = new global::System.Windows.Forms.ToolStripButton();
			this.btn_deleteTemplate = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_backup = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_viewCodes = new global::System.Windows.Forms.ToolStripButton();
			this.gb_dates = new global::System.Windows.Forms.GroupBox();
			this.chk_includeCancelledAndNoshow = new global::System.Windows.Forms.CheckBox();
			this.p_specificDateTime = new global::System.Windows.Forms.Panel();
			this.cmb_time = new global::AutoComboBox.AutoComboBox();
			this.dtp_date = new global::AutoComboBox.MyDateTimePicker();
			this.rbtn_specificDateTime = new global::System.Windows.Forms.RadioButton();
			this.rbtn_useExisting = new global::System.Windows.Forms.RadioButton();
			this.rb_useWhatISelected = new global::System.Windows.Forms.RadioButton();
			this.gb_sorting = new global::System.Windows.Forms.GroupBox();
			this.btn_sort = new global::System.Windows.Forms.Button();
			this.txt_sort = new global::System.Windows.Forms.TextBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.cm_templates.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.toolstrip_modifyTemplates.SuspendLayout();
			this.gb_dates.SuspendLayout();
			this.p_specificDateTime.SuspendLayout();
			this.gb_sorting.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.lv_templates.Border.Class = "ListViewBorder";
			this.lv_templates.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1
			});
			this.lv_templates.ContextMenuStrip = this.cm_templates;
			this.lv_templates.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lv_templates.FullRowSelect = true;
			this.lv_templates.GridLines = true;
			this.lv_templates.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lv_templates.Location = new global::System.Drawing.Point(0, 25);
			this.lv_templates.Margin = new global::System.Windows.Forms.Padding(4);
			this.lv_templates.Name = "lv_templates";
			this.lv_templates.Size = new global::System.Drawing.Size(472, 59);
			this.lv_templates.TabIndex = 0;
			this.lv_templates.UseCompatibleStateImageBehavior = false;
			this.lv_templates.View = global::System.Windows.Forms.View.Details;
			this.lv_templates.SizeChanged += new global::System.EventHandler(this.lv_templates_SizeChanged_1);
			this.lv_templates.DoubleClick += new global::System.EventHandler(this.lv_templates_DoubleClick);
			this.lv_templates.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.lv_templates_KeyDown);
			this.columnHeader1.Text = "Template name";
			this.columnHeader1.Width = 399;
			this.cm_templates.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.editEmailForThisTemplateToolStripMenuItem
			});
			this.cm_templates.Name = "cm_templates";
			this.cm_templates.Size = new global::System.Drawing.Size(217, 26);
			this.cm_templates.Opening += new global::System.ComponentModel.CancelEventHandler(this.cm_templates_Opening);
			this.editEmailForThisTemplateToolStripMenuItem.Name = "editEmailForThisTemplateToolStripMenuItem";
			this.editEmailForThisTemplateToolStripMenuItem.Size = new global::System.Drawing.Size(216, 22);
			this.editEmailForThisTemplateToolStripMenuItem.Text = "Edit email for this template";
			this.editEmailForThisTemplateToolStripMenuItem.Click += new global::System.EventHandler(this.editEmailForThisTemplateToolStripMenuItem_Click);
			this.rtf_preview.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.rtf_preview.Location = new global::System.Drawing.Point(6, 6);
			this.rtf_preview.Margin = new global::System.Windows.Forms.Padding(4);
			this.rtf_preview.Name = "rtf_preview";
			this.rtf_preview.Size = new global::System.Drawing.Size(264, 282);
			this.rtf_preview.TabIndex = 1;
			this.rtf_preview.Text = "";
			this.lbl_captionMessage.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.lbl_captionMessage.Location = new global::System.Drawing.Point(0, 0);
			this.lbl_captionMessage.Name = "lbl_captionMessage";
			this.lbl_captionMessage.PaddingLeft = 8;
			this.lbl_captionMessage.Size = new global::System.Drawing.Size(764, 31);
			this.lbl_captionMessage.TabIndex = 2;
			this.lbl_captionMessage.Text = "Please select a template from the list below:";
			this.btn_refreshPreview.Location = new global::System.Drawing.Point(3, 3);
			this.btn_refreshPreview.Name = "btn_refreshPreview";
			this.btn_refreshPreview.Size = new global::System.Drawing.Size(149, 35);
			this.btn_refreshPreview.TabIndex = 3;
			this.btn_refreshPreview.Text = "&Refresh preview";
			this.btn_refreshPreview.UseVisualStyleBackColor = true;
			this.btn_refreshPreview.Click += new global::System.EventHandler(this.btn_refreshPreview_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_selectTemplate,
				this.btn_exportToExcel,
				this.btn_chooseAFile,
				this.btn_useBlankTemplate,
				this.toolStripSeparator2,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 367);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(764, 39);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_selectTemplate.Image = global::ImportExportClassLibrary.Properties.Resources.check2;
			this.btn_selectTemplate.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_selectTemplate.Name = "btn_selectTemplate";
			this.btn_selectTemplate.Size = new global::System.Drawing.Size(152, 36);
			this.btn_selectTemplate.Text = "&Select template";
			this.btn_selectTemplate.Click += new global::System.EventHandler(this.btn_selectTemplate_Click);
			this.btn_exportToExcel.Image = global::ImportExportClassLibrary.Properties.Resources.excel;
			this.btn_exportToExcel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_exportToExcel.Name = "btn_exportToExcel";
			this.btn_exportToExcel.Size = new global::System.Drawing.Size(148, 36);
			this.btn_exportToExcel.Text = "Export to E&xcel";
			this.btn_exportToExcel.Visible = false;
			this.btn_exportToExcel.Click += new global::System.EventHandler(this.btn_exportToExcel_Click);
			this.btn_chooseAFile.Image = global::ImportExportClassLibrary.Properties.Resources.disk_yellow;
			this.btn_chooseAFile.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_chooseAFile.Name = "btn_chooseAFile";
			this.btn_chooseAFile.Size = new global::System.Drawing.Size(136, 36);
			this.btn_chooseAFile.Text = "Choose a &file";
			this.btn_chooseAFile.Click += new global::System.EventHandler(this.btn_chooseAFile_Click);
			this.btn_useBlankTemplate.Image = global::ImportExportClassLibrary.Properties.Resources.clipboard_empty;
			this.btn_useBlankTemplate.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_useBlankTemplate.Name = "btn_useBlankTemplate";
			this.btn_useBlankTemplate.Size = new global::System.Drawing.Size(177, 36);
			this.btn_useBlankTemplate.Text = "Use &blank template";
			this.btn_useBlankTemplate.Visible = false;
			this.btn_useBlankTemplate.Click += new global::System.EventHandler(this.btn_useBlankTemplate_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(6, 39);
			this.btn_cancel.Image = global::ImportExportClassLibrary.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.splitContainer1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new global::System.Drawing.Point(0, 31);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.AutoScroll = true;
			this.splitContainer1.Panel1.Controls.Add(this.panel2);
			this.splitContainer1.Panel1.Controls.Add(this.gb_dates);
			this.splitContainer1.Panel1.Controls.Add(this.gb_sorting);
			this.splitContainer1.Panel1.Padding = new global::System.Windows.Forms.Padding(6);
			this.splitContainer1.Panel2.Controls.Add(this.rtf_preview);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Panel2.Padding = new global::System.Windows.Forms.Padding(6);
			this.splitContainer1.Size = new global::System.Drawing.Size(764, 336);
			this.splitContainer1.SplitterDistance = 484;
			this.splitContainer1.TabIndex = 6;
			this.panel2.Controls.Add(this.lv_templates);
			this.panel2.Controls.Add(this.toolstrip_modifyTemplates);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new global::System.Drawing.Point(6, 6);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(472, 84);
			this.panel2.TabIndex = 10;
			this.toolstrip_modifyTemplates.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolstrip_modifyTemplates.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_addTemplate,
				this.btn_editTemplate,
				this.toolStripSeparator1,
				this.btn_replaceTemplate,
				this.btn_deleteTemplate,
				this.toolStripSeparator3,
				this.btn_backup,
				this.toolStripSeparator4,
				this.btn_viewCodes
			});
			this.toolstrip_modifyTemplates.Location = new global::System.Drawing.Point(0, 0);
			this.toolstrip_modifyTemplates.Name = "toolstrip_modifyTemplates";
			this.toolstrip_modifyTemplates.Size = new global::System.Drawing.Size(472, 25);
			this.toolstrip_modifyTemplates.TabIndex = 6;
			this.toolstrip_modifyTemplates.Text = "toolStrip2";
			this.toolstrip_modifyTemplates.Visible = false;
			this.btn_addTemplate.Image = global::ImportExportClassLibrary.Properties.Resources.star_yellow_add;
			this.btn_addTemplate.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_addTemplate.Name = "btn_addTemplate";
			this.btn_addTemplate.Size = new global::System.Drawing.Size(99, 22);
			this.btn_addTemplate.Text = "Add template";
			this.btn_addTemplate.Click += new global::System.EventHandler(this.btn_addTemplate_Click);
			this.btn_editTemplate.Image = global::ImportExportClassLibrary.Properties.Resources.edit;
			this.btn_editTemplate.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_editTemplate.Name = "btn_editTemplate";
			this.btn_editTemplate.Size = new global::System.Drawing.Size(47, 22);
			this.btn_editTemplate.Text = "&Edit";
			this.btn_editTemplate.Click += new global::System.EventHandler(this.btn_editTemplate_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 25);
			this.btn_replaceTemplate.Image = global::ImportExportClassLibrary.Properties.Resources.replace2;
			this.btn_replaceTemplate.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_replaceTemplate.Name = "btn_replaceTemplate";
			this.btn_replaceTemplate.Size = new global::System.Drawing.Size(68, 22);
			this.btn_replaceTemplate.Text = "Rep&lace";
			this.btn_replaceTemplate.Click += new global::System.EventHandler(this.btn_replaceTemplate_Click);
			this.btn_deleteTemplate.Image = global::ImportExportClassLibrary.Properties.Resources.delete;
			this.btn_deleteTemplate.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_deleteTemplate.Name = "btn_deleteTemplate";
			this.btn_deleteTemplate.Size = new global::System.Drawing.Size(60, 22);
			this.btn_deleteTemplate.Text = "&Delete";
			this.btn_deleteTemplate.Click += new global::System.EventHandler(this.btn_deleteTemplate_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new global::System.Drawing.Size(6, 25);
			this.btn_backup.Image = global::ImportExportClassLibrary.Properties.Resources.disk_yellow;
			this.btn_backup.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_backup.Name = "btn_backup";
			this.btn_backup.Size = new global::System.Drawing.Size(66, 22);
			this.btn_backup.Text = "&Backup";
			this.btn_backup.Click += new global::System.EventHandler(this.btn_backup_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new global::System.Drawing.Size(6, 25);
			this.btn_viewCodes.Image = global::ImportExportClassLibrary.Properties.Resources.clipboard_empty;
			this.btn_viewCodes.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_viewCodes.Name = "btn_viewCodes";
			this.btn_viewCodes.Size = new global::System.Drawing.Size(86, 22);
			this.btn_viewCodes.Text = "View codes";
			this.btn_viewCodes.Click += new global::System.EventHandler(this.btn_viewCodes_Click);
			this.gb_dates.Controls.Add(this.chk_includeCancelledAndNoshow);
			this.gb_dates.Controls.Add(this.p_specificDateTime);
			this.gb_dates.Controls.Add(this.rbtn_specificDateTime);
			this.gb_dates.Controls.Add(this.rbtn_useExisting);
			this.gb_dates.Controls.Add(this.rb_useWhatISelected);
			this.gb_dates.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.gb_dates.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.gb_dates.Location = new global::System.Drawing.Point(6, 90);
			this.gb_dates.Margin = new global::System.Windows.Forms.Padding(3, 8, 3, 3);
			this.gb_dates.Name = "gb_dates";
			this.gb_dates.Size = new global::System.Drawing.Size(472, 180);
			this.gb_dates.TabIndex = 8;
			this.gb_dates.TabStop = false;
			this.gb_dates.Text = "Dates";
			this.gb_dates.Visible = false;
			this.chk_includeCancelledAndNoshow.AutoSize = true;
			this.chk_includeCancelledAndNoshow.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.chk_includeCancelledAndNoshow.Location = new global::System.Drawing.Point(3, 146);
			this.chk_includeCancelledAndNoshow.Name = "chk_includeCancelledAndNoshow";
			this.chk_includeCancelledAndNoshow.Size = new global::System.Drawing.Size(466, 26);
			this.chk_includeCancelledAndNoshow.TabIndex = 4;
			this.chk_includeCancelledAndNoshow.Text = "Include cancelled and no-show";
			this.chk_includeCancelledAndNoshow.UseVisualStyleBackColor = true;
			this.chk_includeCancelledAndNoshow.Visible = false;
			this.p_specificDateTime.Controls.Add(this.cmb_time);
			this.p_specificDateTime.Controls.Add(this.dtp_date);
			this.p_specificDateTime.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_specificDateTime.Enabled = false;
			this.p_specificDateTime.Location = new global::System.Drawing.Point(3, 107);
			this.p_specificDateTime.Name = "p_specificDateTime";
			this.p_specificDateTime.Padding = new global::System.Windows.Forms.Padding(25, 0, 0, 0);
			this.p_specificDateTime.Size = new global::System.Drawing.Size(466, 39);
			this.p_specificDateTime.TabIndex = 2;
			this.cmb_time.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_time.AllowUserToEnterAnyText = true;
			this.cmb_time.AltValueMember = null;
			this.cmb_time.AutoCompleteEnabled = true;
			this.cmb_time.CalcButtonCid = 0;
			this.cmb_time.ChildLookupGroupId = 0;
			this.cmb_time.CidToNotifyWithValueMember = 0;
			this.cmb_time.Da = null;
			this.cmb_time.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_time.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.cmb_time.GotoNextItemOnDoubleClick = false;
			this.cmb_time.IgnoreScrollWheel = true;
			this.cmb_time.Items.AddRange(new object[]
			{
				"<all day>",
				" 6:00 am",
				" 6:30 am",
				" 7:00 am",
				" 7:30 am",
				" 8:00 am",
				" 8:30 am",
				" 9:00 am",
				" 9:30 am",
				"10:00 am",
				"10:30 am",
				"11:00 am",
				"11:30 am",
				"12:00 pm",
				"12:30 pm",
				" 1:00 pm",
				" 1:30 pm",
				" 2:00 pm",
				" 2:30 pm",
				" 3:00 pm",
				" 3:30 pm",
				" 4:00 pm",
				" 4:30 pm",
				" 5:00 pm",
				" 5:30 pm",
				" 6:00 pm",
				" 6:30 pm",
				" 7:00 pm",
				" 7:30 pm",
				" 8:00 pm",
				" 8:30 pm",
				" 9:00 pm",
				" 9:30 pm",
				"10:00 pm",
				"10:30 pm",
				"11:00 pm",
				"11:30 pm"
			});
			this.cmb_time.Location = new global::System.Drawing.Point(224, 8);
			this.cmb_time.LookupGroupId = 0;
			this.cmb_time.MaskedTextBox = null;
			this.cmb_time.Name = "cmb_time";
			this.cmb_time.Pid = 0;
			this.cmb_time.Size = new global::System.Drawing.Size(136, 24);
			this.cmb_time.Sql = "";
			this.cmb_time.TabIndex = 1;
			this.cmb_time.TripleDES = null;
			this.cmb_time.TryToSelectOnFocusLeave = true;
			this.dtp_date.BaseValue = new global::System.DateTime(2006, 11, 28, 13, 5, 41, 856);
			this.dtp_date.CalcButtonCid = 0;
			this.dtp_date.CustomFormat = "MMMM dd, yyyy";
			this.dtp_date.DefaultCustomFormat = null;
			this.dtp_date.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.dtp_date.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_date.GreyedOut = false;
			this.dtp_date.Location = new global::System.Drawing.Point(40, 8);
			this.dtp_date.Name = "dtp_date";
			this.dtp_date.Size = new global::System.Drawing.Size(168, 22);
			this.dtp_date.TabIndex = 0;
			this.dtp_date.Value = new global::System.DateTime(2006, 11, 28, 13, 5, 41, 856);
			this.rbtn_specificDateTime.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.rbtn_specificDateTime.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.rbtn_specificDateTime.Location = new global::System.Drawing.Point(3, 79);
			this.rbtn_specificDateTime.Name = "rbtn_specificDateTime";
			this.rbtn_specificDateTime.Size = new global::System.Drawing.Size(466, 28);
			this.rbtn_specificDateTime.TabIndex = 1;
			this.rbtn_specificDateTime.Text = "Only show for a specif&ic date / time";
			this.rbtn_specificDateTime.CheckedChanged += new global::System.EventHandler(this.rbtn_specificDateTime_CheckedChanged);
			this.rbtn_useExisting.Checked = true;
			this.rbtn_useExisting.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.rbtn_useExisting.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.rbtn_useExisting.Location = new global::System.Drawing.Point(3, 53);
			this.rbtn_useExisting.Name = "rbtn_useExisting";
			this.rbtn_useExisting.Size = new global::System.Drawing.Size(466, 26);
			this.rbtn_useExisting.TabIndex = 0;
			this.rbtn_useExisting.TabStop = true;
			this.rbtn_useExisting.Text = "Use what was on the &Tests listing";
			this.rbtn_useExisting.CheckedChanged += new global::System.EventHandler(this.rbtn_useExisting_CheckedChanged);
			this.rb_useWhatISelected.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.rb_useWhatISelected.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.rb_useWhatISelected.Location = new global::System.Drawing.Point(3, 25);
			this.rb_useWhatISelected.Name = "rb_useWhatISelected";
			this.rb_useWhatISelected.Size = new global::System.Drawing.Size(466, 28);
			this.rb_useWhatISelected.TabIndex = 3;
			this.rb_useWhatISelected.Text = "Use what I selected on the test booking listing";
			this.rb_useWhatISelected.Visible = false;
			this.gb_sorting.Controls.Add(this.btn_sort);
			this.gb_sorting.Controls.Add(this.txt_sort);
			this.gb_sorting.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.gb_sorting.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.gb_sorting.Location = new global::System.Drawing.Point(6, 270);
			this.gb_sorting.Name = "gb_sorting";
			this.gb_sorting.Size = new global::System.Drawing.Size(472, 60);
			this.gb_sorting.TabIndex = 9;
			this.gb_sorting.TabStop = false;
			this.gb_sorting.Text = "Sorting";
			this.gb_sorting.Visible = false;
			this.btn_sort.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.btn_sort.Location = new global::System.Drawing.Point(404, 25);
			this.btn_sort.Name = "btn_sort";
			this.btn_sort.Size = new global::System.Drawing.Size(65, 32);
			this.btn_sort.TabIndex = 1;
			this.btn_sort.Text = "...";
			this.btn_sort.Click += new global::System.EventHandler(this.btn_sort_Click);
			this.txt_sort.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.txt_sort.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txt_sort.Location = new global::System.Drawing.Point(3, 25);
			this.txt_sort.Multiline = true;
			this.txt_sort.Name = "txt_sort";
			this.txt_sort.ReadOnly = true;
			this.txt_sort.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txt_sort.Size = new global::System.Drawing.Size(305, 32);
			this.txt_sort.TabIndex = 0;
			this.panel1.Controls.Add(this.btn_refreshPreview);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new global::System.Drawing.Point(6, 288);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(264, 42);
			this.panel1.TabIndex = 4;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(9f, 18f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(764, 406);
			base.Controls.Add(this.splitContainer1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.lbl_captionMessage);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Margin = new global::System.Windows.Forms.Padding(4);
			base.Name = "TemplateInDatabaseListDialog";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Available templates";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.TemplateInDatabaseListDialog_FormClosing);
			base.Load += new global::System.EventHandler(this.TemplateInDatabaseListDialog_Load);
			base.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.TemplateInDatabaseListDialog_KeyDown);
			this.cm_templates.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.toolstrip_modifyTemplates.ResumeLayout(false);
			this.toolstrip_modifyTemplates.PerformLayout();
			this.gb_dates.ResumeLayout(false);
			this.gb_dates.PerformLayout();
			this.p_specificDateTime.ResumeLayout(false);
			this.gb_sorting.ResumeLayout(false);
			this.gb_sorting.PerformLayout();
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400005E RID: 94
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400005F RID: 95
		private global::DevComponents.DotNetBar.Controls.ListViewEx lv_templates;

		// Token: 0x04000060 RID: 96
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x04000061 RID: 97
		private global::System.Windows.Forms.RichTextBox rtf_preview;

		// Token: 0x04000062 RID: 98
		private global::DevComponents.DotNetBar.LabelX lbl_captionMessage;

		// Token: 0x04000063 RID: 99
		private global::System.Windows.Forms.Button btn_refreshPreview;

		// Token: 0x04000064 RID: 100
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000065 RID: 101
		private global::System.Windows.Forms.ToolStripButton btn_selectTemplate;

		// Token: 0x04000066 RID: 102
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x04000067 RID: 103
		private global::System.Windows.Forms.SplitContainer splitContainer1;

		// Token: 0x04000068 RID: 104
		private global::System.Windows.Forms.ToolStrip toolstrip_modifyTemplates;

		// Token: 0x04000069 RID: 105
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x0400006A RID: 106
		private global::System.Windows.Forms.ToolStripButton btn_addTemplate;

		// Token: 0x0400006B RID: 107
		private global::System.Windows.Forms.ToolStripButton btn_editTemplate;

		// Token: 0x0400006C RID: 108
		private global::System.Windows.Forms.ToolStripButton btn_deleteTemplate;

		// Token: 0x0400006D RID: 109
		private global::System.Windows.Forms.GroupBox gb_dates;

		// Token: 0x0400006E RID: 110
		private global::System.Windows.Forms.Panel p_specificDateTime;

		// Token: 0x0400006F RID: 111
		private global::AutoComboBox.AutoComboBox cmb_time;

		// Token: 0x04000070 RID: 112
		private global::AutoComboBox.MyDateTimePicker dtp_date;

		// Token: 0x04000071 RID: 113
		private global::System.Windows.Forms.RadioButton rbtn_specificDateTime;

		// Token: 0x04000072 RID: 114
		private global::System.Windows.Forms.RadioButton rbtn_useExisting;

		// Token: 0x04000073 RID: 115
		private global::System.Windows.Forms.GroupBox gb_sorting;

		// Token: 0x04000074 RID: 116
		private global::System.Windows.Forms.Button btn_sort;

		// Token: 0x04000075 RID: 117
		private global::System.Windows.Forms.TextBox txt_sort;

		// Token: 0x04000076 RID: 118
		private global::System.Windows.Forms.ToolStripButton btn_replaceTemplate;

		// Token: 0x04000077 RID: 119
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000078 RID: 120
		private global::System.Windows.Forms.ToolStripButton btn_useBlankTemplate;

		// Token: 0x04000079 RID: 121
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x0400007A RID: 122
		private global::System.Windows.Forms.ToolStripButton btn_chooseAFile;

		// Token: 0x0400007B RID: 123
		private global::System.Windows.Forms.ContextMenuStrip cm_templates;

		// Token: 0x0400007C RID: 124
		private global::System.Windows.Forms.ToolStripMenuItem editEmailForThisTemplateToolStripMenuItem;

		// Token: 0x0400007D RID: 125
		private global::System.Windows.Forms.ToolStripButton btn_exportToExcel;

		// Token: 0x0400007E RID: 126
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

		// Token: 0x0400007F RID: 127
		private global::System.Windows.Forms.ToolStripButton btn_backup;

		// Token: 0x04000080 RID: 128
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000081 RID: 129
		private global::System.Windows.Forms.RadioButton rb_useWhatISelected;

		// Token: 0x04000082 RID: 130
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

		// Token: 0x04000083 RID: 131
		private global::System.Windows.Forms.ToolStripButton btn_viewCodes;

		// Token: 0x04000084 RID: 132
		private global::System.Windows.Forms.CheckBox chk_includeCancelledAndNoshow;
	}
}
