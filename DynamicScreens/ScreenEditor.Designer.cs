namespace DynamicScreens
{
	// Token: 0x02000013 RID: 19
	public partial class ScreenEditor : global::System.Windows.Forms.Form
	{
		// Token: 0x0600016F RID: 367 RVA: 0x0000F7F4 File Offset: 0x0000E7F4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				if (this._model != null)
				{
				}
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000F83C File Offset: 0x0000E83C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DynamicScreens.ScreenEditor));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.tabControl1 = new global::System.Windows.Forms.TabControl();
			this.tabPage1 = new global::System.Windows.Forms.TabPage();
			this.tv_design = new global::Aga.Controls.Tree.TreeViewAdv();
			this.cm_nodes = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.entergroupCaptionsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MENU_markFieldsWithAGroupDescriptor = new global::System.Windows.Forms.ToolStripMenuItem();
			this.createNewFieldsByEnteringCaptionsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.commonSettingsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.setAsGroupBoxTitleToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.setAsPhoneNumberToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new global::System.Windows.Forms.ToolStripSeparator();
			this.convertToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.convertTextBoxToRichTextBoxupgradeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.convertRichTextBoxToTextBoxdowngradeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MENU_misc = new global::System.Windows.Forms.ToolStripMenuItem();
			this.whatOtherFormsDoesThisControlBelongToToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.editThelistToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.getACommaSeparatedListOfControlidsForSelectedFieldsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.nodeIcon1 = new global::Aga.Controls.Tree.NodeControls.NodeIcon();
			this.nodeTextBox1 = new global::Aga.Controls.Tree.NodeControls.NodeTextBox();
			this.tp_preview = new global::System.Windows.Forms.TabPage();
			this.p_data = new global::System.Windows.Forms.Panel();
			this.splitter1 = new global::System.Windows.Forms.Splitter();
			this.p_top = new global::System.Windows.Forms.Panel();
			this.toolStrip2 = new global::System.Windows.Forms.ToolStrip();
			this.toolStripComboBox1 = new global::System.Windows.Forms.ToolStripComboBox();
			this.toolStripButton1 = new global::System.Windows.Forms.ToolStripButton();
			this.btn_viewScreenControlInfo = new global::System.Windows.Forms.ToolStripButton();
			this.navigationPane1 = new global::DevComponents.DotNetBar.NavigationPane();
			this.navigationPanePanel1 = new global::DevComponents.DotNetBar.NavigationPanePanel();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_label = new global::System.Windows.Forms.ToolStripButton();
			this.btn_checkbox = new global::System.Windows.Forms.ToolStripButton();
			this.btn_textbox = new global::System.Windows.Forms.ToolStripButton();
			this.btn_radioButtonGroup = new global::System.Windows.Forms.ToolStripButton();
			this.btn_dropList = new global::System.Windows.Forms.ToolStripButton();
			this.btn_date = new global::System.Windows.Forms.ToolStripButton();
			this.btn_richTextBox = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_staffDropList = new global::System.Windows.Forms.ToolStripButton();
			this.btn_dynamicTable = new global::System.Windows.Forms.ToolStripButton();
			this.btn_table = new global::System.Windows.Forms.ToolStripButton();
			this.btn_picture = new global::System.Windows.Forms.ToolStripButton();
			this.btn_fileList = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_groupBox = new global::System.Windows.Forms.ToolStripButton();
			this.btn_columnBreak = new global::System.Windows.Forms.ToolStripButton();
			this.btn_blankSpace = new global::System.Windows.Forms.ToolStripButton();
			this.btn_tabControl = new global::System.Windows.Forms.ToolStripButton();
			this.btn_tabPage = new global::System.Windows.Forms.ToolStripButton();
			this.btn_listSelectItem = new global::System.Windows.Forms.ToolStripButton();
			this.btn_hrule = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_multiCheckHeader = new global::System.Windows.Forms.ToolStripButton();
			this.btn_multiCheckbox = new global::System.Windows.Forms.ToolStripButton();
			this.multiCheckboxWithTextboxToolStripMenuItem = new global::System.Windows.Forms.ToolStripButton();
			this.multiCheckboxWithDroplistToolStripMenuItem = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_multiLineTextbox = new global::System.Windows.Forms.ToolStripButton();
			this.pane_mainControls = new global::DevComponents.DotNetBar.ButtonItem();
			this.navigationPanePanel3 = new global::DevComponents.DotNetBar.NavigationPanePanel();
			this.toolStrip5 = new global::System.Windows.Forms.ToolStrip();
			this.toolStripSeparator5 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new global::System.Windows.Forms.ToolStripLabel();
			this.btn_accommodationCheckbox = new global::System.Windows.Forms.ToolStripButton();
			this.btn_accommodationTextbox = new global::System.Windows.Forms.ToolStripButton();
			this.btn_accommodationDatePicker = new global::System.Windows.Forms.ToolStripButton();
			this.btn_accommodationDropList = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator8 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel2 = new global::System.Windows.Forms.ToolStripLabel();
			this.btn_perStudentForm = new global::System.Windows.Forms.ToolStripButton();
			this.btn_dynamicControlsChooser = new global::System.Windows.Forms.ToolStripButton();
			this.btn_multiItemDbChooser = new global::System.Windows.Forms.ToolStripButton();
			this.btn_infoBox = new global::System.Windows.Forms.ToolStripButton();
			this.btn_calcButton = new global::System.Windows.Forms.ToolStripButton();
			this.btn_caseList = new global::System.Windows.Forms.ToolStripButton();
			this.btn_caseComboBox = new global::System.Windows.Forms.ToolStripButton();
			this.btn_emailHistory = new global::System.Windows.Forms.ToolStripButton();
			this.btn_appHistory = new global::System.Windows.Forms.ToolStripButton();
			this.pane_accomm = new global::DevComponents.DotNetBar.ButtonItem();
			this.navigationPanePanel4 = new global::DevComponents.DotNetBar.NavigationPanePanel();
			this.p_existingFields = new global::System.Windows.Forms.Panel();
			this.treeView_existingControls = new global::AutoComboBox.MyControls.TreeViewMS();
			this.imageList2 = new global::System.Windows.Forms.ImageList(this.components);
			this.expandableSplitter1 = new global::DevComponents.DotNetBar.ExpandableSplitter();
			this.lbl_existingControlsInstructions = new global::System.Windows.Forms.Label();
			this.panelbar_existingFields = new global::DevComponents.DotNetBar.ButtonItem();
			this.dockSite2 = new global::DevComponents.DotNetBar.DockSite();
			this.bar3 = new global::DevComponents.DotNetBar.Bar();
			this.panelDockContainer3 = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.lv_lists = new global::AutoComboBox.ListViewEx();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.toolStrip4 = new global::System.Windows.Forms.ToolStrip();
			this.btn_newList = new global::System.Windows.Forms.ToolStripButton();
			this.btn_editList = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_refreshGroups = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator11 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_list_rename = new global::System.Windows.Forms.ToolStripButton();
			this.btn_list_delete = new global::System.Windows.Forms.ToolStripButton();
			this.btn_list_undelete = new global::System.Windows.Forms.ToolStripButton();
			this.dockContainerItem4 = new global::DevComponents.DotNetBar.DockContainerItem();
			this.bar4 = new global::DevComponents.DotNetBar.Bar();
			this.panelDockContainer2 = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.propertyGrid1 = new global::System.Windows.Forms.PropertyGrid();
			this.button1 = new global::System.Windows.Forms.Button();
			this.Properties = new global::DevComponents.DotNetBar.DockContainerItem();
			this.dockContainerItem5 = new global::DevComponents.DotNetBar.DockContainerItem();
			this.dockSite1 = new global::DevComponents.DotNetBar.DockSite();
			this.bar1 = new global::DevComponents.DotNetBar.Bar();
			this.panelDockContainer5 = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.dockContainerItem6 = new global::DevComponents.DotNetBar.DockContainerItem();
			this.panelDockContainer1 = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.panelDockContainer4 = new global::DevComponents.DotNetBar.PanelDockContainer();
			this.dockContainerItem1 = new global::DevComponents.DotNetBar.DockContainerItem();
			this.dockSite3 = new global::DevComponents.DotNetBar.DockSite();
			this.dockSite4 = new global::DevComponents.DotNetBar.DockSite();
			this.dockSite5 = new global::DevComponents.DotNetBar.DockSite();
			this.dockSite6 = new global::DevComponents.DotNetBar.DockSite();
			this.dotNetBarManager1 = new global::DevComponents.DotNetBar.DotNetBarManager(this.components);
			this.dockSite8 = new global::DevComponents.DotNetBar.DockSite();
			this.dockSite7 = new global::DevComponents.DotNetBar.DockSite();
			this.p_bottom = new global::System.Windows.Forms.Panel();
			this.toolStrip3 = new global::System.Windows.Forms.ToolStrip();
			this.btn_generateDefaultValuesXml = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator9 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_apply = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator7 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_close = new global::System.Windows.Forms.ToolStripButton();
			this.menuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.findToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.functionsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.btn_pullInPreviouslyDeletedField = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new global::System.Windows.Forms.ToolStripSeparator();
			this.convertSelectedControlExistingDataToENCRYPTEDToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.convertSelectedControlExistingDataToNOTENCRYPTEDToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.convertADroplistFromRegularTextbasedToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.convertADroplistFromTextbasedToRegularToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.dockContainerItem2 = new global::DevComponents.DotNetBar.DockContainerItem();
			this.dockContainerItem3 = new global::DevComponents.DotNetBar.DockContainerItem();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.cm_nodes.SuspendLayout();
			this.tp_preview.SuspendLayout();
			this.p_top.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.navigationPane1.SuspendLayout();
			this.navigationPanePanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.navigationPanePanel3.SuspendLayout();
			this.toolStrip5.SuspendLayout();
			this.navigationPanePanel4.SuspendLayout();
			this.p_existingFields.SuspendLayout();
			this.dockSite2.SuspendLayout();
			this.bar3.BeginInit();
			this.bar3.SuspendLayout();
			this.panelDockContainer3.SuspendLayout();
			this.toolStrip4.SuspendLayout();
			this.bar4.BeginInit();
			this.bar4.SuspendLayout();
			this.panelDockContainer2.SuspendLayout();
			this.dockSite1.SuspendLayout();
			this.bar1.BeginInit();
			this.bar1.SuspendLayout();
			this.panelDockContainer5.SuspendLayout();
			this.p_bottom.SuspendLayout();
			this.toolStrip3.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "text.png");
			this.imageList1.Images.SetKeyName(1, "check.png");
			this.imageList1.Images.SetKeyName(2, "element_into_input.png");
			this.imageList1.Images.SetKeyName(3, "preferences.png");
			this.imageList1.Images.SetKeyName(4, "elements2.png");
			this.imageList1.Images.SetKeyName(5, "photo_portrait.png");
			this.imageList1.Images.SetKeyName(6, "table.png");
			this.imageList1.Images.SetKeyName(7, "about.png");
			this.imageList1.Images.SetKeyName(8, "selection.png");
			this.imageList1.Images.SetKeyName(9, "disks.png");
			this.imageList1.Images.SetKeyName(10, "window.png");
			this.imageList1.Images.SetKeyName(11, "column_add_after.png");
			this.imageList1.Images.SetKeyName(12, "index.png");
			this.imageList1.Images.SetKeyName(13, "calendar.png");
			this.imageList1.Images.SetKeyName(14, "hrule.png");
			this.imageList1.Images.SetKeyName(15, "checks.png");
			this.imageList1.Images.SetKeyName(16, "textWithMulticheck.png");
			this.imageList1.Images.SetKeyName(17, "droplistWithMultiCheck.png");
			this.imageList1.Images.SetKeyName(18, "note_add.png");
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tp_preview);
			this.tabControl1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new global::System.Drawing.Point(198, 24);
			this.tabControl1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new global::System.Drawing.Size(420, 704);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new global::System.EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tabPage1.Controls.Add(this.tv_design);
			this.tabPage1.Location = new global::System.Drawing.Point(4, 25);
			this.tabPage1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabPage1.Size = new global::System.Drawing.Size(412, 675);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Design";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.tv_design.AllowDrop = true;
			this.tv_design.BackColor = global::System.Drawing.SystemColors.Window;
			this.tv_design.ContextMenuStrip = this.cm_nodes;
			this.tv_design.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.tv_design.DefaultToolTipProvider = null;
			this.tv_design.DisplayDraggingNodes = true;
			this.tv_design.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tv_design.DragDropMarkColor = global::System.Drawing.Color.Black;
			this.tv_design.DragDropMarkWidth = 2f;
			this.tv_design.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.tv_design.Indent = 65;
			this.tv_design.LineColor = global::System.Drawing.SystemColors.ControlDark;
			this.tv_design.Location = new global::System.Drawing.Point(3, 4);
			this.tv_design.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tv_design.Model = null;
			this.tv_design.Name = "tv_design";
			this.tv_design.NodeControls.Add(this.nodeIcon1);
			this.tv_design.NodeControls.Add(this.nodeTextBox1);
			this.tv_design.RowHeight = 22;
			this.tv_design.SelectedNode = null;
			this.tv_design.SelectionMode = 1;
			this.tv_design.Size = new global::System.Drawing.Size(406, 667);
			this.tv_design.TabIndex = 19;
			this.tv_design.Text = "treeViewAdv1";
			this.tv_design.ItemDrag += new global::System.Windows.Forms.ItemDragEventHandler(this.tv_design_ItemDrag);
			this.tv_design.SelectionChanged += new global::System.EventHandler(this.tv_design_SelectionChanged);
			this.tv_design.TextChanged += new global::System.EventHandler(this.tv_design_TextChanged);
			this.tv_design.DragDrop += new global::System.Windows.Forms.DragEventHandler(this.treeViewAdv1_DragDrop);
			this.tv_design.DragEnter += new global::System.Windows.Forms.DragEventHandler(this.tv_design_DragEnter);
			this.tv_design.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.tv_design_KeyDown);
			this.tv_design.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.tv_design_KeyPress_1);
			this.tv_design.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.tv_design_KeyUp);
			this.cm_nodes.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.entergroupCaptionsToolStripMenuItem,
				this.MENU_markFieldsWithAGroupDescriptor,
				this.createNewFieldsByEnteringCaptionsToolStripMenuItem,
				this.toolStripMenuItem2,
				this.setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem,
				this.toolStripMenuItem3,
				this.commonSettingsToolStripMenuItem,
				this.setAsGroupBoxTitleToolStripMenuItem,
				this.setAsPhoneNumberToolStripMenuItem,
				this.toolStripMenuItem5,
				this.convertToolStripMenuItem,
				this.MENU_misc,
				this.toolStripMenuItem4,
				this.getACommaSeparatedListOfControlidsForSelectedFieldsToolStripMenuItem
			});
			this.cm_nodes.Name = "cm_nodes";
			this.cm_nodes.Size = new global::System.Drawing.Size(386, 314);
			this.cm_nodes.Opening += new global::System.ComponentModel.CancelEventHandler(this.cm_nodes_Opening);
			this.entergroupCaptionsToolStripMenuItem.Image = global::DynamicScreens.Properties.Resources.about;
			this.entergroupCaptionsToolStripMenuItem.Name = "entergroupCaptionsToolStripMenuItem";
			this.entergroupCaptionsToolStripMenuItem.Size = new global::System.Drawing.Size(385, 22);
			this.entergroupCaptionsToolStripMenuItem.Text = "Enter &group captions for selected items";
			this.entergroupCaptionsToolStripMenuItem.Click += new global::System.EventHandler(this.entergroupCaptionsToolStripMenuItem_Click);
			this.MENU_markFieldsWithAGroupDescriptor.Name = "MENU_markFieldsWithAGroupDescriptor";
			this.MENU_markFieldsWithAGroupDescriptor.Size = new global::System.Drawing.Size(385, 22);
			this.MENU_markFieldsWithAGroupDescriptor.Text = "Mark fields with a group descriptor";
			this.MENU_markFieldsWithAGroupDescriptor.Click += new global::System.EventHandler(this.MENU_markFieldsWithAGroupDescriptor_Click);
			this.createNewFieldsByEnteringCaptionsToolStripMenuItem.Name = "createNewFieldsByEnteringCaptionsToolStripMenuItem";
			this.createNewFieldsByEnteringCaptionsToolStripMenuItem.Size = new global::System.Drawing.Size(385, 22);
			this.createNewFieldsByEnteringCaptionsToolStripMenuItem.Text = "Create new fields by entering captions";
			this.createNewFieldsByEnteringCaptionsToolStripMenuItem.Click += new global::System.EventHandler(this.createNewFieldsByEnteringCaptionsToolStripMenuItem_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new global::System.Drawing.Size(382, 6);
			this.setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem.Image = global::DynamicScreens.Properties.Resources.colors;
			this.setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem.Name = "setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem";
			this.setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem.Size = new global::System.Drawing.Size(385, 22);
			this.setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem.Text = "Set common background colours for selected group-boxes";
			this.setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem.Click += new global::System.EventHandler(this.setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem_Click);
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new global::System.Drawing.Size(382, 6);
			this.commonSettingsToolStripMenuItem.Enabled = false;
			this.commonSettingsToolStripMenuItem.Name = "commonSettingsToolStripMenuItem";
			this.commonSettingsToolStripMenuItem.Size = new global::System.Drawing.Size(385, 22);
			this.commonSettingsToolStripMenuItem.Text = "Common Settings:";
			this.setAsGroupBoxTitleToolStripMenuItem.Image = global::DynamicScreens.Properties.Resources.presentation_chart;
			this.setAsGroupBoxTitleToolStripMenuItem.Name = "setAsGroupBoxTitleToolStripMenuItem";
			this.setAsGroupBoxTitleToolStripMenuItem.Size = new global::System.Drawing.Size(385, 22);
			this.setAsGroupBoxTitleToolStripMenuItem.Text = "Set as group box title";
			this.setAsGroupBoxTitleToolStripMenuItem.Click += new global::System.EventHandler(this.setAsGroupBoxTitleToolStripMenuItem_Click);
			this.setAsPhoneNumberToolStripMenuItem.Name = "setAsPhoneNumberToolStripMenuItem";
			this.setAsPhoneNumberToolStripMenuItem.Size = new global::System.Drawing.Size(385, 22);
			this.setAsPhoneNumberToolStripMenuItem.Text = "Set as phone number";
			this.setAsPhoneNumberToolStripMenuItem.Click += new global::System.EventHandler(this.setAsPhoneNumberToolStripMenuItem_Click);
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new global::System.Drawing.Size(382, 6);
			this.convertToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.convertTextBoxToRichTextBoxupgradeToolStripMenuItem,
				this.convertRichTextBoxToTextBoxdowngradeToolStripMenuItem
			});
			this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
			this.convertToolStripMenuItem.Size = new global::System.Drawing.Size(385, 22);
			this.convertToolStripMenuItem.Text = "&Convert";
			this.convertTextBoxToRichTextBoxupgradeToolStripMenuItem.Name = "convertTextBoxToRichTextBoxupgradeToolStripMenuItem";
			this.convertTextBoxToRichTextBoxupgradeToolStripMenuItem.Size = new global::System.Drawing.Size(312, 22);
			this.convertTextBoxToRichTextBoxupgradeToolStripMenuItem.Text = "Convert TextBox to RichTextBox (upgrade)";
			this.convertTextBoxToRichTextBoxupgradeToolStripMenuItem.Click += new global::System.EventHandler(this.convertTextBoxToRichTextBoxupgradeToolStripMenuItem_Click);
			this.convertRichTextBoxToTextBoxdowngradeToolStripMenuItem.Name = "convertRichTextBoxToTextBoxdowngradeToolStripMenuItem";
			this.convertRichTextBoxToTextBoxdowngradeToolStripMenuItem.Size = new global::System.Drawing.Size(312, 22);
			this.convertRichTextBoxToTextBoxdowngradeToolStripMenuItem.Text = "Convert RichTextBox to TextBox (downgrade)";
			this.convertRichTextBoxToTextBoxdowngradeToolStripMenuItem.Click += new global::System.EventHandler(this.convertRichTextBoxToTextBoxdowngradeToolStripMenuItem_Click);
			this.MENU_misc.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.whatOtherFormsDoesThisControlBelongToToolStripMenuItem,
				this.editThelistToolStripMenuItem
			});
			this.MENU_misc.Name = "MENU_misc";
			this.MENU_misc.Size = new global::System.Drawing.Size(385, 22);
			this.MENU_misc.Text = "&Miscellaneous";
			this.whatOtherFormsDoesThisControlBelongToToolStripMenuItem.Name = "whatOtherFormsDoesThisControlBelongToToolStripMenuItem";
			this.whatOtherFormsDoesThisControlBelongToToolStripMenuItem.Size = new global::System.Drawing.Size(317, 22);
			this.whatOtherFormsDoesThisControlBelongToToolStripMenuItem.Text = "What other forms does this control belong to?";
			this.whatOtherFormsDoesThisControlBelongToToolStripMenuItem.Click += new global::System.EventHandler(this.whatOtherFormsDoesThisControlBelongToToolStripMenuItem_Click);
			this.editThelistToolStripMenuItem.Name = "editThelistToolStripMenuItem";
			this.editThelistToolStripMenuItem.Size = new global::System.Drawing.Size(317, 22);
			this.editThelistToolStripMenuItem.Text = "Edit the &list";
			this.editThelistToolStripMenuItem.Click += new global::System.EventHandler(this.editThelistToolStripMenuItem_Click);
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new global::System.Drawing.Size(382, 6);
			this.getACommaSeparatedListOfControlidsForSelectedFieldsToolStripMenuItem.Name = "getACommaSeparatedListOfControlidsForSelectedFieldsToolStripMenuItem";
			this.getACommaSeparatedListOfControlidsForSelectedFieldsToolStripMenuItem.Size = new global::System.Drawing.Size(385, 22);
			this.getACommaSeparatedListOfControlidsForSelectedFieldsToolStripMenuItem.Text = "Get a comma separated list of controlids for selected fields";
			this.getACommaSeparatedListOfControlidsForSelectedFieldsToolStripMenuItem.Click += new global::System.EventHandler(this.getACommaSeparatedListOfControlidsForSelectedFieldsToolStripMenuItem_Click);
			this.nodeIcon1.DataPropertyName = "Icon";
			this.nodeIcon1.LeftMargin = 1;
			this.nodeIcon1.ParentColumn = null;
			this.nodeTextBox1.DataPropertyName = "Text";
			this.nodeTextBox1.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.nodeTextBox1.IncrementalSearchEnabled = true;
			this.nodeTextBox1.LeftMargin = 3;
			this.nodeTextBox1.ParentColumn = null;
			this.tp_preview.AutoScroll = true;
			this.tp_preview.BackColor = global::System.Drawing.Color.Transparent;
			this.tp_preview.Controls.Add(this.p_data);
			this.tp_preview.Controls.Add(this.splitter1);
			this.tp_preview.Controls.Add(this.p_top);
			this.tp_preview.Location = new global::System.Drawing.Point(4, 25);
			this.tp_preview.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tp_preview.Name = "tp_preview";
			this.tp_preview.Padding = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tp_preview.Size = new global::System.Drawing.Size(412, 675);
			this.tp_preview.TabIndex = 1;
			this.tp_preview.Text = "Preview";
			this.tp_preview.Click += new global::System.EventHandler(this.tp_preview_Click);
			this.p_data.AccessibleName = "Data form";
			this.p_data.AutoScroll = true;
			this.p_data.BackColor = global::System.Drawing.SystemColors.ControlLight;
			this.p_data.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.p_data.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.p_data.Location = new global::System.Drawing.Point(6, 60);
			this.p_data.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.p_data.Name = "p_data";
			this.p_data.Size = new global::System.Drawing.Size(403, 611);
			this.p_data.TabIndex = 0;
			this.p_data.TabStop = true;
			this.splitter1.Location = new global::System.Drawing.Point(3, 60);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new global::System.Drawing.Size(3, 611);
			this.splitter1.TabIndex = 4;
			this.splitter1.TabStop = false;
			this.splitter1.Visible = false;
			this.p_top.BackColor = global::System.Drawing.SystemColors.ActiveCaption;
			this.p_top.Controls.Add(this.toolStrip2);
			this.p_top.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_top.Location = new global::System.Drawing.Point(3, 4);
			this.p_top.Name = "p_top";
			this.p_top.Size = new global::System.Drawing.Size(406, 56);
			this.p_top.TabIndex = 2;
			this.toolStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripComboBox1,
				this.toolStripButton1,
				this.btn_viewScreenControlInfo
			});
			this.toolStrip2.Location = new global::System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new global::System.Drawing.Size(406, 25);
			this.toolStrip2.TabIndex = 3;
			this.toolStrip2.Text = "toolStrip2";
			this.toolStripComboBox1.Items.AddRange(new object[]
			{
				"1024x768",
				" 800x600",
				"1280x600",
				"1280x720",
				"1280x768",
				"1280x800",
				"1280x900",
				"1280x960",
				"1280x1024",
				"1360x768",
				"1360x1024",
				"1440x900",
				"1600x1200",
				"1680x1050",
				""
			});
			this.toolStripComboBox1.Name = "toolStripComboBox1";
			this.toolStripComboBox1.Size = new global::System.Drawing.Size(121, 25);
			this.toolStripComboBox1.SelectedIndexChanged += new global::System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged_1);
			this.toolStripButton1.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButton1.Image");
			this.toolStripButton1.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "View control defn info";
			this.toolStripButton1.Click += new global::System.EventHandler(this.toolStripButton1_Click);
			this.btn_viewScreenControlInfo.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btn_viewScreenControlInfo.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_viewScreenControlInfo.Image");
			this.btn_viewScreenControlInfo.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_viewScreenControlInfo.Name = "btn_viewScreenControlInfo";
			this.btn_viewScreenControlInfo.Size = new global::System.Drawing.Size(23, 22);
			this.btn_viewScreenControlInfo.Text = "View screen control info";
			this.btn_viewScreenControlInfo.Click += new global::System.EventHandler(this.btn_viewScreenControlInfo_Click);
			this.navigationPane1.Controls.Add(this.navigationPanePanel1);
			this.navigationPane1.Controls.Add(this.navigationPanePanel3);
			this.navigationPane1.Controls.Add(this.navigationPanePanel4);
			this.navigationPane1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.navigationPane1.ItemPaddingBottom = 2;
			this.navigationPane1.ItemPaddingTop = 2;
			this.navigationPane1.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.pane_mainControls,
				this.pane_accomm,
				this.panelbar_existingFields
			});
			this.navigationPane1.Location = new global::System.Drawing.Point(0, 0);
			this.navigationPane1.Name = "navigationPane1";
			this.navigationPane1.NavigationBarHeight = 137;
			this.navigationPane1.Padding = new global::System.Windows.Forms.Padding(1);
			this.navigationPane1.Size = new global::System.Drawing.Size(189, 724);
			this.navigationPane1.TabIndex = 20;
			this.navigationPane1.TitlePanel.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.navigationPane1.TitlePanel.Font = new global::System.Drawing.Font("Tahoma", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.navigationPane1.TitlePanel.Location = new global::System.Drawing.Point(1, 1);
			this.navigationPane1.TitlePanel.Name = "panelTitle";
			this.navigationPane1.TitlePanel.Size = new global::System.Drawing.Size(187, 24);
			this.navigationPane1.TitlePanel.Style.BackColor1.ColorSchemePart = 51;
			this.navigationPane1.TitlePanel.Style.BackColor2.ColorSchemePart = 52;
			this.navigationPane1.TitlePanel.Style.Border = 1;
			this.navigationPane1.TitlePanel.Style.BorderColor.ColorSchemePart = 53;
			this.navigationPane1.TitlePanel.Style.BorderSide = 8;
			this.navigationPane1.TitlePanel.Style.ForeColor.ColorSchemePart = 54;
			this.navigationPane1.TitlePanel.Style.GradientAngle = 90;
			this.navigationPane1.TitlePanel.Style.MarginLeft = 4;
			this.navigationPane1.TitlePanel.TabIndex = 0;
			this.navigationPane1.TitlePanel.Text = "Existing fields";
			this.navigationPanePanel1.Controls.Add(this.toolStrip1);
			this.navigationPanePanel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.navigationPanePanel1.Location = new global::System.Drawing.Point(1, 25);
			this.navigationPanePanel1.Name = "navigationPanePanel1";
			this.navigationPanePanel1.ParentItem = this.pane_mainControls;
			this.navigationPanePanel1.Size = new global::System.Drawing.Size(187, 561);
			this.navigationPanePanel1.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.navigationPanePanel1.Style.BackColor1.ColorSchemePart = 0;
			this.navigationPanePanel1.Style.BackColor2.ColorSchemePart = 1;
			this.navigationPanePanel1.Style.BorderColor.ColorSchemePart = 8;
			this.navigationPanePanel1.Style.ForeColor.ColorSchemePart = 40;
			this.navigationPanePanel1.Style.GradientAngle = 90;
			this.navigationPanePanel1.TabIndex = 2;
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_label,
				this.btn_checkbox,
				this.btn_textbox,
				this.btn_radioButtonGroup,
				this.btn_dropList,
				this.btn_date,
				this.btn_richTextBox,
				this.toolStripSeparator6,
				this.btn_staffDropList,
				this.btn_dynamicTable,
				this.btn_table,
				this.btn_picture,
				this.btn_fileList,
				this.toolStripSeparator4,
				this.btn_groupBox,
				this.btn_columnBreak,
				this.btn_blankSpace,
				this.btn_tabControl,
				this.btn_tabPage,
				this.btn_listSelectItem,
				this.btn_hrule,
				this.toolStripSeparator1,
				this.btn_multiCheckHeader,
				this.btn_multiCheckbox,
				this.multiCheckboxWithTextboxToolStripMenuItem,
				this.multiCheckboxWithDroplistToolStripMenuItem,
				this.toolStripSeparator2,
				this.btn_multiLineTextbox
			});
			this.toolStrip1.LayoutStyle = global::System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(187, 561);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.TabStop = true;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_label.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_label.Image");
			this.btn_label.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_label.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_label.Name = "btn_label";
			this.btn_label.Size = new global::System.Drawing.Size(185, 24);
			this.btn_label.Text = "&Label";
			this.btn_label.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_label.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_checkbox.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_checkbox.Image");
			this.btn_checkbox.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_checkbox.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_checkbox.Name = "btn_checkbox";
			this.btn_checkbox.Size = new global::System.Drawing.Size(185, 24);
			this.btn_checkbox.Text = "Checkbox";
			this.btn_checkbox.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_checkbox.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_textbox.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_textbox.Image");
			this.btn_textbox.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_textbox.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_textbox.Name = "btn_textbox";
			this.btn_textbox.Size = new global::System.Drawing.Size(185, 24);
			this.btn_textbox.Text = "&Textbox";
			this.btn_textbox.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_textbox.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_radioButtonGroup.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_radioButtonGroup.Image");
			this.btn_radioButtonGroup.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_radioButtonGroup.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_radioButtonGroup.Name = "btn_radioButtonGroup";
			this.btn_radioButtonGroup.Size = new global::System.Drawing.Size(185, 24);
			this.btn_radioButtonGroup.Text = "Radiobutton";
			this.btn_radioButtonGroup.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_radioButtonGroup.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_dropList.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_dropList.Image");
			this.btn_dropList.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_dropList.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_dropList.Name = "btn_dropList";
			this.btn_dropList.Size = new global::System.Drawing.Size(185, 24);
			this.btn_dropList.Text = "Drop List";
			this.btn_dropList.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_dropList.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_date.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_date.Image");
			this.btn_date.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_date.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_date.Name = "btn_date";
			this.btn_date.Size = new global::System.Drawing.Size(185, 24);
			this.btn_date.Text = "Date";
			this.btn_date.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_date.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_richTextBox.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_richTextBox.Image");
			this.btn_richTextBox.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_richTextBox.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_richTextBox.Name = "btn_richTextBox";
			this.btn_richTextBox.Size = new global::System.Drawing.Size(185, 24);
			this.btn_richTextBox.Text = "Rich textbox";
			this.btn_richTextBox.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_richTextBox.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new global::System.Drawing.Size(185, 6);
			this.btn_staffDropList.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_staffDropList.Image");
			this.btn_staffDropList.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_staffDropList.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_staffDropList.Name = "btn_staffDropList";
			this.btn_staffDropList.Size = new global::System.Drawing.Size(185, 24);
			this.btn_staffDropList.Text = "Staff Drop List";
			this.btn_staffDropList.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_staffDropList.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_dynamicTable.Image = global::DynamicScreens.Properties.Resources.table;
			this.btn_dynamicTable.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_dynamicTable.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_dynamicTable.Name = "btn_dynamicTable";
			this.btn_dynamicTable.Size = new global::System.Drawing.Size(185, 24);
			this.btn_dynamicTable.Text = "Dynamic Ta&ble";
			this.btn_dynamicTable.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_table.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_table.Image");
			this.btn_table.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_table.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_table.Name = "btn_table";
			this.btn_table.Size = new global::System.Drawing.Size(185, 24);
			this.btn_table.Text = "Ta&ble";
			this.btn_table.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_table.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_picture.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_picture.Image");
			this.btn_picture.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_picture.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_picture.Name = "btn_picture";
			this.btn_picture.Size = new global::System.Drawing.Size(185, 24);
			this.btn_picture.Text = "&Picture";
			this.btn_picture.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_picture.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_fileList.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_fileList.Image");
			this.btn_fileList.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_fileList.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_fileList.Name = "btn_fileList";
			this.btn_fileList.Size = new global::System.Drawing.Size(185, 24);
			this.btn_fileList.Text = "File List";
			this.btn_fileList.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_fileList.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new global::System.Drawing.Size(185, 6);
			this.btn_groupBox.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_groupBox.Image");
			this.btn_groupBox.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_groupBox.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_groupBox.Name = "btn_groupBox";
			this.btn_groupBox.Size = new global::System.Drawing.Size(185, 24);
			this.btn_groupBox.Text = "Group Box";
			this.btn_groupBox.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_groupBox.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_columnBreak.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_columnBreak.Image");
			this.btn_columnBreak.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_columnBreak.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_columnBreak.Name = "btn_columnBreak";
			this.btn_columnBreak.Size = new global::System.Drawing.Size(185, 24);
			this.btn_columnBreak.Text = "Column Break";
			this.btn_columnBreak.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_columnBreak.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_blankSpace.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_blankSpace.Image");
			this.btn_blankSpace.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_blankSpace.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_blankSpace.Name = "btn_blankSpace";
			this.btn_blankSpace.Size = new global::System.Drawing.Size(185, 24);
			this.btn_blankSpace.Text = "Blank Space";
			this.btn_blankSpace.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_blankSpace.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_tabControl.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_tabControl.Image");
			this.btn_tabControl.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_tabControl.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_tabControl.Name = "btn_tabControl";
			this.btn_tabControl.Size = new global::System.Drawing.Size(185, 24);
			this.btn_tabControl.Text = "Tab Control";
			this.btn_tabControl.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_tabControl.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_tabPage.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_tabPage.Image");
			this.btn_tabPage.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_tabPage.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_tabPage.Name = "btn_tabPage";
			this.btn_tabPage.Size = new global::System.Drawing.Size(185, 24);
			this.btn_tabPage.Text = "Tab Page";
			this.btn_tabPage.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_tabPage.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_listSelectItem.Image = global::DynamicScreens.Properties.Resources.check;
			this.btn_listSelectItem.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_listSelectItem.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_listSelectItem.Name = "btn_listSelectItem";
			this.btn_listSelectItem.Size = new global::System.Drawing.Size(185, 24);
			this.btn_listSelectItem.Text = "List select item";
			this.btn_listSelectItem.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_hrule.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_hrule.Image");
			this.btn_hrule.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_hrule.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_hrule.Name = "btn_hrule";
			this.btn_hrule.Size = new global::System.Drawing.Size(185, 24);
			this.btn_hrule.Text = "Horizontal rule";
			this.btn_hrule.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_hrule.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(185, 6);
			this.btn_multiCheckHeader.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_multiCheckHeader.Image");
			this.btn_multiCheckHeader.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_multiCheckHeader.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_multiCheckHeader.Name = "btn_multiCheckHeader";
			this.btn_multiCheckHeader.Size = new global::System.Drawing.Size(157, 24);
			this.btn_multiCheckHeader.Text = "Multi-Checkbox Header";
			this.btn_multiCheckHeader.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_multiCheckHeader.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_multiCheckbox.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_multiCheckbox.Image");
			this.btn_multiCheckbox.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_multiCheckbox.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_multiCheckbox.Name = "btn_multiCheckbox";
			this.btn_multiCheckbox.Size = new global::System.Drawing.Size(116, 24);
			this.btn_multiCheckbox.Text = "Multi-Checkbox";
			this.btn_multiCheckbox.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_multiCheckbox.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.multiCheckboxWithTextboxToolStripMenuItem.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("multiCheckboxWithTextboxToolStripMenuItem.Image");
			this.multiCheckboxWithTextboxToolStripMenuItem.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.multiCheckboxWithTextboxToolStripMenuItem.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.multiCheckboxWithTextboxToolStripMenuItem.Name = "multiCheckboxWithTextboxToolStripMenuItem";
			this.multiCheckboxWithTextboxToolStripMenuItem.Size = new global::System.Drawing.Size(136, 24);
			this.multiCheckboxWithTextboxToolStripMenuItem.Text = "Multi-check textbox";
			this.multiCheckboxWithTextboxToolStripMenuItem.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.multiCheckboxWithTextboxToolStripMenuItem.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.multiCheckboxWithDroplistToolStripMenuItem.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("multiCheckboxWithDroplistToolStripMenuItem.Image");
			this.multiCheckboxWithDroplistToolStripMenuItem.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.multiCheckboxWithDroplistToolStripMenuItem.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.multiCheckboxWithDroplistToolStripMenuItem.Name = "multiCheckboxWithDroplistToolStripMenuItem";
			this.multiCheckboxWithDroplistToolStripMenuItem.Size = new global::System.Drawing.Size(138, 24);
			this.multiCheckboxWithDroplistToolStripMenuItem.Text = "Multi-check droplist";
			this.multiCheckboxWithDroplistToolStripMenuItem.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.multiCheckboxWithDroplistToolStripMenuItem.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(185, 6);
			this.btn_multiLineTextbox.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_multiLineTextbox.Image");
			this.btn_multiLineTextbox.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_multiLineTextbox.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_multiLineTextbox.Name = "btn_multiLineTextbox";
			this.btn_multiLineTextbox.Size = new global::System.Drawing.Size(124, 24);
			this.btn_multiLineTextbox.Text = "Multi-line textbox";
			this.btn_multiLineTextbox.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_multiLineTextbox.Click += new global::System.EventHandler(this.btn_multiLineTextbox_Click);
			this.btn_multiLineTextbox.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.pane_mainControls.ButtonStyle = 2;
			this.pane_mainControls.Checked = true;
			this.pane_mainControls.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pane_mainControls.Image");
			this.pane_mainControls.ImagePaddingHorizontal = 8;
			this.pane_mainControls.Name = "pane_mainControls";
			this.pane_mainControls.OptionGroup = "navBar";
			this.pane_mainControls.Text = "Standard controls";
			this.pane_mainControls.Click += new global::System.EventHandler(this.pane_mainControls_Click);
			this.navigationPanePanel3.Controls.Add(this.toolStrip5);
			this.navigationPanePanel3.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.navigationPanePanel3.Location = new global::System.Drawing.Point(1, 1);
			this.navigationPanePanel3.Name = "navigationPanePanel3";
			this.navigationPanePanel3.ParentItem = this.pane_accomm;
			this.navigationPanePanel3.Size = new global::System.Drawing.Size(187, 585);
			this.navigationPanePanel3.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.navigationPanePanel3.Style.BackColor1.ColorSchemePart = 0;
			this.navigationPanePanel3.Style.BackColor2.ColorSchemePart = 1;
			this.navigationPanePanel3.Style.BorderColor.ColorSchemePart = 53;
			this.navigationPanePanel3.Style.ForeColor.ColorSchemePart = 40;
			this.navigationPanePanel3.Style.GradientAngle = 90;
			this.navigationPanePanel3.TabIndex = 4;
			this.toolStrip5.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.toolStrip5.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip5.ImageScalingSize = new global::System.Drawing.Size(20, 20);
			this.toolStrip5.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripSeparator5,
				this.toolStripLabel1,
				this.btn_accommodationCheckbox,
				this.btn_accommodationTextbox,
				this.btn_accommodationDatePicker,
				this.btn_accommodationDropList,
				this.toolStripSeparator8,
				this.toolStripLabel2,
				this.btn_perStudentForm,
				this.btn_dynamicControlsChooser,
				this.btn_multiItemDbChooser,
				this.btn_infoBox,
				this.btn_calcButton,
				this.btn_caseList,
				this.btn_caseComboBox,
				this.btn_emailHistory,
				this.btn_appHistory
			});
			this.toolStrip5.LayoutStyle = global::System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.toolStrip5.Location = new global::System.Drawing.Point(0, 0);
			this.toolStrip5.Name = "toolStrip5";
			this.toolStrip5.Size = new global::System.Drawing.Size(187, 585);
			this.toolStrip5.TabIndex = 3;
			this.toolStrip5.TabStop = true;
			this.toolStrip5.Text = "toolStrip5";
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new global::System.Drawing.Size(185, 6);
			this.toolStripLabel1.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Bold);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new global::System.Drawing.Size(185, 15);
			this.toolStripLabel1.Text = "Accommodation controls";
			this.btn_accommodationCheckbox.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_accommodationCheckbox.Image");
			this.btn_accommodationCheckbox.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_accommodationCheckbox.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_accommodationCheckbox.Name = "btn_accommodationCheckbox";
			this.btn_accommodationCheckbox.Size = new global::System.Drawing.Size(185, 24);
			this.btn_accommodationCheckbox.Text = "Accommodation checkbox";
			this.btn_accommodationCheckbox.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_accommodationCheckbox.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_accommodationTextbox.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_accommodationTextbox.Image");
			this.btn_accommodationTextbox.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_accommodationTextbox.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_accommodationTextbox.Name = "btn_accommodationTextbox";
			this.btn_accommodationTextbox.Size = new global::System.Drawing.Size(185, 24);
			this.btn_accommodationTextbox.Text = "Accommodation textbox";
			this.btn_accommodationTextbox.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_accommodationTextbox.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_accommodationDatePicker.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_accommodationDatePicker.Image");
			this.btn_accommodationDatePicker.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_accommodationDatePicker.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_accommodationDatePicker.Name = "btn_accommodationDatePicker";
			this.btn_accommodationDatePicker.Size = new global::System.Drawing.Size(185, 24);
			this.btn_accommodationDatePicker.Text = "Accommodation date picker";
			this.btn_accommodationDatePicker.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_accommodationDatePicker.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_accommodationDropList.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_accommodationDropList.Image");
			this.btn_accommodationDropList.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_accommodationDropList.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_accommodationDropList.Name = "btn_accommodationDropList";
			this.btn_accommodationDropList.Size = new global::System.Drawing.Size(185, 24);
			this.btn_accommodationDropList.Text = "Accommodation drop list";
			this.btn_accommodationDropList.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_accommodationDropList.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new global::System.Drawing.Size(185, 6);
			this.toolStripLabel2.Font = new global::System.Drawing.Font("Segoe UI", 9f, global::System.Drawing.FontStyle.Bold);
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new global::System.Drawing.Size(185, 15);
			this.toolStripLabel2.Text = "Other controls";
			this.btn_perStudentForm.Image = global::DynamicScreens.Properties.Resources.form_blue;
			this.btn_perStudentForm.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_perStudentForm.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_perStudentForm.Name = "btn_perStudentForm";
			this.btn_perStudentForm.Size = new global::System.Drawing.Size(185, 24);
			this.btn_perStudentForm.Text = "Form settings";
			this.btn_perStudentForm.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_perStudentForm.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_dynamicControlsChooser.Image = global::DynamicScreens.Properties.Resources.droplistWithMultiCheck;
			this.btn_dynamicControlsChooser.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_dynamicControlsChooser.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_dynamicControlsChooser.Name = "btn_dynamicControlsChooser";
			this.btn_dynamicControlsChooser.Size = new global::System.Drawing.Size(185, 24);
			this.btn_dynamicControlsChooser.Text = "Dynamic Controls Chooser";
			this.btn_dynamicControlsChooser.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_dynamicControlsChooser.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_multiItemDbChooser.Image = global::DynamicScreens.Properties.Resources.sort_descending;
			this.btn_multiItemDbChooser.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_multiItemDbChooser.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_multiItemDbChooser.Name = "btn_multiItemDbChooser";
			this.btn_multiItemDbChooser.Size = new global::System.Drawing.Size(185, 24);
			this.btn_multiItemDbChooser.Text = "Multi-item chooser (DB)";
			this.btn_multiItemDbChooser.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_multiItemDbChooser.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_infoBox.Image = global::DynamicScreens.Properties.Resources.document_attachment;
			this.btn_infoBox.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_infoBox.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_infoBox.Name = "btn_infoBox";
			this.btn_infoBox.Size = new global::System.Drawing.Size(185, 24);
			this.btn_infoBox.Text = "Info box";
			this.btn_infoBox.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_infoBox.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_calcButton.Image = global::DynamicScreens.Properties.Resources.calculator;
			this.btn_calcButton.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_calcButton.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_calcButton.Name = "btn_calcButton";
			this.btn_calcButton.Size = new global::System.Drawing.Size(185, 24);
			this.btn_calcButton.Text = "Calculation button";
			this.btn_calcButton.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_caseList.Image = global::DynamicScreens.Properties.Resources.briefcase2;
			this.btn_caseList.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_caseList.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_caseList.Name = "btn_caseList";
			this.btn_caseList.Size = new global::System.Drawing.Size(185, 24);
			this.btn_caseList.Text = "Case List";
			this.btn_caseList.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_caseComboBox.Image = global::DynamicScreens.Properties.Resources.briefcase2;
			this.btn_caseComboBox.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_caseComboBox.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_caseComboBox.Name = "btn_caseComboBox";
			this.btn_caseComboBox.Size = new global::System.Drawing.Size(185, 24);
			this.btn_caseComboBox.Text = "Case drop list";
			this.btn_caseComboBox.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_caseComboBox.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_emailHistory.Image = global::DynamicScreens.Properties.Resources.mail;
			this.btn_emailHistory.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_emailHistory.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_emailHistory.Name = "btn_emailHistory";
			this.btn_emailHistory.Size = new global::System.Drawing.Size(185, 24);
			this.btn_emailHistory.Text = "Email history";
			this.btn_emailHistory.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_emailHistory.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.btn_appHistory.Image = global::DynamicScreens.Properties.Resources.alarmclock;
			this.btn_appHistory.ImageAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_appHistory.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_appHistory.Name = "btn_appHistory";
			this.btn_appHistory.Size = new global::System.Drawing.Size(185, 24);
			this.btn_appHistory.Text = "Appointment history";
			this.btn_appHistory.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_appHistory.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.toolStripButton1_MouseDown);
			this.pane_accomm.ButtonStyle = 2;
			this.pane_accomm.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pane_accomm.Image");
			this.pane_accomm.ImagePaddingHorizontal = 8;
			this.pane_accomm.Name = "pane_accomm";
			this.pane_accomm.OptionGroup = "navBar";
			this.pane_accomm.Text = "Miscellaneous";
			this.navigationPanePanel4.Controls.Add(this.p_existingFields);
			this.navigationPanePanel4.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.navigationPanePanel4.Location = new global::System.Drawing.Point(1, 1);
			this.navigationPanePanel4.Name = "navigationPanePanel4";
			this.navigationPanePanel4.ParentItem = this.panelbar_existingFields;
			this.navigationPanePanel4.Size = new global::System.Drawing.Size(187, 585);
			this.navigationPanePanel4.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.navigationPanePanel4.Style.BackColor1.ColorSchemePart = 0;
			this.navigationPanePanel4.Style.BackColor2.ColorSchemePart = 1;
			this.navigationPanePanel4.Style.BorderColor.ColorSchemePart = 53;
			this.navigationPanePanel4.Style.ForeColor.ColorSchemePart = 40;
			this.navigationPanePanel4.Style.GradientAngle = 90;
			this.navigationPanePanel4.TabIndex = 5;
			this.p_existingFields.Controls.Add(this.treeView_existingControls);
			this.p_existingFields.Controls.Add(this.expandableSplitter1);
			this.p_existingFields.Controls.Add(this.lbl_existingControlsInstructions);
			this.p_existingFields.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.p_existingFields.Location = new global::System.Drawing.Point(0, 0);
			this.p_existingFields.Name = "p_existingFields";
			this.p_existingFields.Padding = new global::System.Windows.Forms.Padding(1);
			this.p_existingFields.Size = new global::System.Drawing.Size(187, 585);
			this.p_existingFields.TabIndex = 4;
			this.treeView_existingControls.AllowDrop = true;
			this.treeView_existingControls.BackColor = global::System.Drawing.SystemColors.Window;
			this.treeView_existingControls.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.treeView_existingControls.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.treeView_existingControls.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.treeView_existingControls.HideSelection = false;
			this.treeView_existingControls.ImageIndex = 0;
			this.treeView_existingControls.ImageList = this.imageList2;
			this.treeView_existingControls.LineColor = global::System.Drawing.Color.FromArgb(172, 168, 153);
			this.treeView_existingControls.Location = new global::System.Drawing.Point(1, 76);
			this.treeView_existingControls.Name = "treeView_existingControls";
			this.treeView_existingControls.SelectedImageIndex = 0;
			this.treeView_existingControls.SelectedNodes = (global::System.Collections.ArrayList)componentResourceManager.GetObject("treeView_existingControls.SelectedNodes");
			this.treeView_existingControls.Size = new global::System.Drawing.Size(185, 508);
			this.treeView_existingControls.TabIndex = 0;
			this.treeView_existingControls.DragDrop += new global::System.Windows.Forms.DragEventHandler(this.treeView_existingControls_DragDrop);
			this.treeView_existingControls.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.treeView_existingControls_MouseDown);
			this.imageList2.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList2.ImageStream");
			this.imageList2.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList2.Images.SetKeyName(0, "breakpoint.png");
			this.imageList2.Images.SetKeyName(1, "text.png");
			this.imageList2.Images.SetKeyName(2, "about.png");
			this.imageList2.Images.SetKeyName(3, "check.png");
			this.imageList2.Images.SetKeyName(4, "elements2.png");
			this.imageList2.Images.SetKeyName(5, "element_into_input.png");
			this.imageList2.Images.SetKeyName(6, "photo_portrait.png");
			this.imageList2.Images.SetKeyName(7, "table.png");
			this.imageList2.Images.SetKeyName(8, "disks.png");
			this.imageList2.Images.SetKeyName(9, "window.png");
			this.imageList2.Images.SetKeyName(10, "column_add_after.png");
			this.imageList2.Images.SetKeyName(11, "selection.png");
			this.imageList2.Images.SetKeyName(12, "index.png");
			this.imageList2.Images.SetKeyName(13, "calendar.png");
			this.imageList2.Images.SetKeyName(14, "hrule.png");
			this.imageList2.Images.SetKeyName(15, "folder.png");
			this.imageList2.Images.SetKeyName(16, "document_plain.png");
			this.imageList2.Images.SetKeyName(17, "masks.png");
			this.imageList2.Images.SetKeyName(18, "text_rich_colored.png");
			this.imageList2.Images.SetKeyName(19, "users1.png");
			this.imageList2.Images.SetKeyName(20, "disks.png");
			this.imageList2.Images.SetKeyName(21, "checks.png");
			this.imageList2.Images.SetKeyName(22, "textWithMulticheck.png");
			this.imageList2.Images.SetKeyName(23, "droplistWithMultiCheck.png");
			this.imageList2.Images.SetKeyName(24, "note_add.png");
			this.imageList2.Images.SetKeyName(25, "form_blue.png");
			this.expandableSplitter1.BackColor2 = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.BackColor2SchemePart = 53;
			this.expandableSplitter1.BackColorSchemePart = 51;
			this.expandableSplitter1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.expandableSplitter1.ExpandableControl = this.lbl_existingControlsInstructions;
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
			this.expandableSplitter1.Location = new global::System.Drawing.Point(1, 73);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new global::System.Drawing.Size(185, 3);
			this.expandableSplitter1.TabIndex = 5;
			this.expandableSplitter1.TabStop = false;
			this.lbl_existingControlsInstructions.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.lbl_existingControlsInstructions.Font = new global::System.Drawing.Font("Segoe UI", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_existingControlsInstructions.Location = new global::System.Drawing.Point(1, 1);
			this.lbl_existingControlsInstructions.Name = "lbl_existingControlsInstructions";
			this.lbl_existingControlsInstructions.Size = new global::System.Drawing.Size(185, 72);
			this.lbl_existingControlsInstructions.TabIndex = 4;
			this.lbl_existingControlsInstructions.Text = "Drag controls to the right using the right mouse button; select multiple controls using CTRL/SHIFT and the left mouse button.  Fields dragged to the right will exist on multiple forms.";
			this.panelbar_existingFields.ButtonStyle = 2;
			this.panelbar_existingFields.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("panelbar_existingFields.Image");
			this.panelbar_existingFields.ImagePaddingHorizontal = 8;
			this.panelbar_existingFields.Name = "panelbar_existingFields";
			this.panelbar_existingFields.OptionGroup = "navBar";
			this.panelbar_existingFields.Text = "Existing fields";
			this.dockSite2.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			this.dockSite2.Controls.Add(this.bar3);
			this.dockSite2.Controls.Add(this.bar4);
			this.dockSite2.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.dockSite2.DocumentDockContainer = new global::DevComponents.DotNetBar.DocumentDockContainer(new global::DevComponents.DotNetBar.DocumentBaseContainer[]
			{
				new global::DevComponents.DotNetBar.DocumentBarContainer(this.bar4, 364, 412),
				new global::DevComponents.DotNetBar.DocumentBarContainer(this.bar3, 364, 335)
			}, 1);
			this.dockSite2.Location = new global::System.Drawing.Point(618, 24);
			this.dockSite2.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dockSite2.Name = "dockSite2";
			this.dockSite2.Size = new global::System.Drawing.Size(367, 750);
			this.dockSite2.TabIndex = 10;
			this.dockSite2.TabStop = false;
			this.bar3.AccessibleDescription = "DotNetBar Bar (bar3)";
			this.bar3.AccessibleName = "DotNetBar Bar";
			this.bar3.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ToolBar;
			this.bar3.AutoSyncBarCaption = true;
			this.bar3.CloseSingleTab = true;
			this.bar3.Controls.Add(this.panelDockContainer3);
			this.bar3.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.bar3.GrabHandleStyle = 8;
			this.bar3.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.dockContainerItem4
			});
			this.bar3.LayoutType = 2;
			this.bar3.Location = new global::System.Drawing.Point(0, 415);
			this.bar3.Name = "bar3";
			this.bar3.Size = new global::System.Drawing.Size(367, 335);
			this.bar3.Stretch = true;
			this.bar3.Style = 2;
			this.bar3.TabIndex = 1;
			this.bar3.TabStop = false;
			this.bar3.Text = "Lists";
			this.panelDockContainer3.Controls.Add(this.lv_lists);
			this.panelDockContainer3.Controls.Add(this.toolStrip4);
			this.panelDockContainer3.Location = new global::System.Drawing.Point(3, 23);
			this.panelDockContainer3.Name = "panelDockContainer3";
			this.panelDockContainer3.Size = new global::System.Drawing.Size(361, 309);
			this.panelDockContainer3.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockContainer3.Style.BackColor1.ColorSchemePart = 0;
			this.panelDockContainer3.Style.BackColor2.ColorSchemePart = 1;
			this.panelDockContainer3.Style.BorderColor.ColorSchemePart = 8;
			this.panelDockContainer3.Style.ForeColor.ColorSchemePart = 40;
			this.panelDockContainer3.Style.GradientAngle = 90;
			this.panelDockContainer3.TabIndex = 0;
			this.panelDockContainer3.Text = "Lists";
			this.lv_lists.AutoSortingEnabled = false;
			this.lv_lists.BackColourSelected = global::System.Drawing.Color.LightBlue;
			this.lv_lists.CalcButtonCid = 0;
			this.lv_lists.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1
			});
			this.lv_lists.DefaultSortByAsc = true;
			this.lv_lists.DefaultSortByColInd = -1;
			this.lv_lists.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lv_lists.DrawMode = global::System.Windows.Forms.DrawMode.Normal;
			this.lv_lists.EmailTemplateId = 0;
			this.lv_lists.EnterTriggersDoubleClickEvent = false;
			this.lv_lists.FullRowSelect = true;
			this.lv_lists.GridLines = true;
			this.lv_lists.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.None;
			this.lv_lists.IsFileList = false;
			this.lv_lists.ItemHeight = 16;
			this.lv_lists.Location = new global::System.Drawing.Point(0, 46);
			this.lv_lists.Name = "lv_lists";
			this.lv_lists.NoDeleting = false;
			this.lv_lists.NoEditing = false;
			this.lv_lists.Size = new global::System.Drawing.Size(361, 263);
			this.lv_lists.TabIndex = 1;
			this.lv_lists.Tag2 = null;
			this.lv_lists.UseCompatibleStateImageBehavior = false;
			this.lv_lists.View = global::System.Windows.Forms.View.Details;
			this.lv_lists.DoubleClick += new global::System.EventHandler(this.lv_lists_DoubleClick);
			this.columnHeader1.Width = 277;
			this.toolStrip4.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip4.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_newList,
				this.btn_editList,
				this.toolStripSeparator3,
				this.btn_refreshGroups,
				this.toolStripSeparator11,
				this.btn_list_rename,
				this.btn_list_delete,
				this.btn_list_undelete
			});
			this.toolStrip4.LayoutStyle = global::System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStrip4.Location = new global::System.Drawing.Point(0, 0);
			this.toolStrip4.Name = "toolStrip4";
			this.toolStrip4.Size = new global::System.Drawing.Size(361, 46);
			this.toolStrip4.TabIndex = 19999;
			this.toolStrip4.TabStop = true;
			this.toolStrip4.Text = "toolStrip4";
			this.btn_newList.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_newList.Image");
			this.btn_newList.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_newList.Name = "btn_newList";
			this.btn_newList.Size = new global::System.Drawing.Size(69, 20);
			this.btn_newList.Text = "&New list";
			this.btn_newList.Click += new global::System.EventHandler(this.btn_newList_Click);
			this.btn_editList.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_editList.Image");
			this.btn_editList.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_editList.Name = "btn_editList";
			this.btn_editList.Size = new global::System.Drawing.Size(65, 20);
			this.btn_editList.Text = "&Edit list";
			this.btn_editList.Click += new global::System.EventHandler(this.btn_editList_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new global::System.Drawing.Size(6, 23);
			this.btn_refreshGroups.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_refreshGroups.Image");
			this.btn_refreshGroups.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_refreshGroups.Name = "btn_refreshGroups";
			this.btn_refreshGroups.Size = new global::System.Drawing.Size(66, 20);
			this.btn_refreshGroups.Text = "Refresh";
			this.btn_refreshGroups.Click += new global::System.EventHandler(this.btn_refreshGroups_Click);
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new global::System.Drawing.Size(6, 23);
			this.btn_list_rename.Image = global::DynamicScreens.Properties.Resources.about;
			this.btn_list_rename.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_list_rename.Name = "btn_list_rename";
			this.btn_list_rename.Size = new global::System.Drawing.Size(88, 20);
			this.btn_list_rename.Text = "Re&name list";
			this.btn_list_rename.Click += new global::System.EventHandler(this.btn_list_rename_Click);
			this.btn_list_delete.Image = global::DynamicScreens.Properties.Resources.delete;
			this.btn_list_delete.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_list_delete.Name = "btn_list_delete";
			this.btn_list_delete.Size = new global::System.Drawing.Size(78, 20);
			this.btn_list_delete.Text = "&Delete list";
			this.btn_list_delete.Click += new global::System.EventHandler(this.btn_list_delete_Click);
			this.btn_list_undelete.Image = global::DynamicScreens.Properties.Resources.note_add;
			this.btn_list_undelete.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_list_undelete.Name = "btn_list_undelete";
			this.btn_list_undelete.Size = new global::System.Drawing.Size(97, 20);
			this.btn_list_undelete.Text = "&Un-delete list";
			this.btn_list_undelete.Click += new global::System.EventHandler(this.btn_list_undelete_Click);
			this.dockContainerItem4.Control = this.panelDockContainer3;
			this.dockContainerItem4.Name = "dockContainerItem4";
			this.dockContainerItem4.Text = "Lists";
			this.bar4.AccessibleDescription = "DotNetBar Bar (bar4)";
			this.bar4.AccessibleName = "DotNetBar Bar";
			this.bar4.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ToolBar;
			this.bar4.AutoSyncBarCaption = true;
			this.bar4.Controls.Add(this.panelDockContainer2);
			this.bar4.GrabHandleStyle = 8;
			this.bar4.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.Properties
			});
			this.bar4.LayoutType = 2;
			this.bar4.Location = new global::System.Drawing.Point(3, 0);
			this.bar4.Name = "bar4";
			this.bar4.Size = new global::System.Drawing.Size(364, 412);
			this.bar4.Stretch = true;
			this.bar4.Style = 2;
			this.bar4.TabIndex = 2;
			this.bar4.TabStop = false;
			this.bar4.Text = "Properties";
			this.panelDockContainer2.Controls.Add(this.propertyGrid1);
			this.panelDockContainer2.Controls.Add(this.button1);
			this.panelDockContainer2.Location = new global::System.Drawing.Point(3, 23);
			this.panelDockContainer2.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelDockContainer2.Name = "panelDockContainer2";
			this.panelDockContainer2.Size = new global::System.Drawing.Size(358, 386);
			this.panelDockContainer2.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockContainer2.Style.BackColor1.ColorSchemePart = 0;
			this.panelDockContainer2.Style.BackColor2.ColorSchemePart = 1;
			this.panelDockContainer2.Style.BorderColor.ColorSchemePart = 8;
			this.panelDockContainer2.Style.ForeColor.ColorSchemePart = 40;
			this.panelDockContainer2.Style.GradientAngle = 90;
			this.panelDockContainer2.TabIndex = 0;
			this.propertyGrid1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.Font = new global::System.Drawing.Font("Arial", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.propertyGrid1.Location = new global::System.Drawing.Point(0, 0);
			this.propertyGrid1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new global::System.Drawing.Size(358, 386);
			this.propertyGrid1.TabIndex = 2;
			this.propertyGrid1.PropertyValueChanged += new global::System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
			this.button1.Location = new global::System.Drawing.Point(89, 422);
			this.button1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(87, 28);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			this.Properties.Control = this.panelDockContainer2;
			this.Properties.Name = "Properties";
			this.Properties.Text = "Properties";
			this.dockContainerItem5.Name = "dockContainerItem5";
			this.dockContainerItem5.Text = "dockContainerItem5";
			this.dockSite1.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			this.dockSite1.Controls.Add(this.bar1);
			this.dockSite1.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.dockSite1.DocumentDockContainer = new global::DevComponents.DotNetBar.DocumentDockContainer(new global::DevComponents.DotNetBar.DocumentBaseContainer[]
			{
				new global::DevComponents.DotNetBar.DocumentBarContainer(this.bar1, 195, 750)
			}, 0);
			this.dockSite1.Location = new global::System.Drawing.Point(0, 24);
			this.dockSite1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dockSite1.Name = "dockSite1";
			this.dockSite1.Size = new global::System.Drawing.Size(198, 750);
			this.dockSite1.TabIndex = 9;
			this.dockSite1.TabStop = false;
			this.bar1.AccessibleDescription = "DotNetBar Bar (bar1)";
			this.bar1.AccessibleName = "DotNetBar Bar";
			this.bar1.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ToolBar;
			this.bar1.AutoSyncBarCaption = true;
			this.bar1.CloseSingleTab = true;
			this.bar1.Controls.Add(this.panelDockContainer5);
			this.bar1.GrabHandleStyle = 8;
			this.bar1.Items.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.dockContainerItem6
			});
			this.bar1.LayoutType = 2;
			this.bar1.Location = new global::System.Drawing.Point(0, 0);
			this.bar1.Name = "bar1";
			this.bar1.Size = new global::System.Drawing.Size(195, 750);
			this.bar1.Stretch = true;
			this.bar1.Style = 2;
			this.bar1.TabIndex = 0;
			this.bar1.TabStop = false;
			this.bar1.Text = "Toolbox";
			this.panelDockContainer5.Controls.Add(this.navigationPane1);
			this.panelDockContainer5.Location = new global::System.Drawing.Point(3, 23);
			this.panelDockContainer5.Name = "panelDockContainer5";
			this.panelDockContainer5.Size = new global::System.Drawing.Size(189, 724);
			this.panelDockContainer5.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockContainer5.Style.BackColor1.ColorSchemePart = 0;
			this.panelDockContainer5.Style.BackColor2.ColorSchemePart = 1;
			this.panelDockContainer5.Style.BorderColor.ColorSchemePart = 8;
			this.panelDockContainer5.Style.ForeColor.ColorSchemePart = 40;
			this.panelDockContainer5.Style.GradientAngle = 90;
			this.panelDockContainer5.TabIndex = 0;
			this.dockContainerItem6.Control = this.panelDockContainer5;
			this.dockContainerItem6.Name = "dockContainerItem6";
			this.dockContainerItem6.Text = "Toolbox";
			this.panelDockContainer1.Location = new global::System.Drawing.Point(3, 23);
			this.panelDockContainer1.Name = "panelDockContainer1";
			this.panelDockContainer1.Size = new global::System.Drawing.Size(155, 469);
			this.panelDockContainer1.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockContainer1.Style.BackColor1.ColorSchemePart = 51;
			this.panelDockContainer1.Style.BackColor2.ColorSchemePart = 52;
			this.panelDockContainer1.Style.Border = 1;
			this.panelDockContainer1.Style.BorderColor.ColorSchemePart = 53;
			this.panelDockContainer1.Style.ForeColor.ColorSchemePart = 54;
			this.panelDockContainer1.Style.GradientAngle = 90;
			this.panelDockContainer1.TabIndex = 0;
			this.panelDockContainer4.Location = new global::System.Drawing.Point(3, 23);
			this.panelDockContainer4.Name = "panelDockContainer4";
			this.panelDockContainer4.Size = new global::System.Drawing.Size(155, 469);
			this.panelDockContainer4.Style.Alignment = global::System.Drawing.StringAlignment.Center;
			this.panelDockContainer4.Style.BackColor1.ColorSchemePart = 0;
			this.panelDockContainer4.Style.BackColor2.ColorSchemePart = 1;
			this.panelDockContainer4.Style.BorderColor.ColorSchemePart = 8;
			this.panelDockContainer4.Style.ForeColor.ColorSchemePart = 40;
			this.panelDockContainer4.Style.GradientAngle = 90;
			this.panelDockContainer4.TabIndex = 2;
			this.dockContainerItem1.Name = "dockContainerItem1";
			this.dockContainerItem1.Text = "dockContainerItem1";
			this.dockSite3.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			this.dockSite3.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.dockSite3.DocumentDockContainer = new global::DevComponents.DotNetBar.DocumentDockContainer();
			this.dockSite3.Location = new global::System.Drawing.Point(0, 24);
			this.dockSite3.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dockSite3.Name = "dockSite3";
			this.dockSite3.Size = new global::System.Drawing.Size(985, 0);
			this.dockSite3.TabIndex = 11;
			this.dockSite3.TabStop = false;
			this.dockSite4.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			this.dockSite4.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.dockSite4.DocumentDockContainer = new global::DevComponents.DotNetBar.DocumentDockContainer();
			this.dockSite4.Location = new global::System.Drawing.Point(0, 774);
			this.dockSite4.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dockSite4.Name = "dockSite4";
			this.dockSite4.Size = new global::System.Drawing.Size(985, 0);
			this.dockSite4.TabIndex = 12;
			this.dockSite4.TabStop = false;
			this.dockSite5.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			this.dockSite5.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.dockSite5.Location = new global::System.Drawing.Point(0, 24);
			this.dockSite5.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dockSite5.Name = "dockSite5";
			this.dockSite5.Size = new global::System.Drawing.Size(0, 750);
			this.dockSite5.TabIndex = 13;
			this.dockSite5.TabStop = false;
			this.dockSite6.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			this.dockSite6.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.dockSite6.Location = new global::System.Drawing.Point(985, 24);
			this.dockSite6.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dockSite6.Name = "dockSite6";
			this.dockSite6.Size = new global::System.Drawing.Size(0, 750);
			this.dockSite6.TabIndex = 14;
			this.dockSite6.TabStop = false;
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(112);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(131139);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(131137);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(131158);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(131160);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(131162);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(131161);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(46);
			this.dotNetBarManager1.AutoDispatchShortcuts.Add(45);
			this.dotNetBarManager1.BottomDockSite = this.dockSite4;
			this.dotNetBarManager1.DefinitionName = "";
			this.dotNetBarManager1.EnableFullSizeDock = false;
			this.dotNetBarManager1.LeftDockSite = this.dockSite1;
			this.dotNetBarManager1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			this.dotNetBarManager1.ParentForm = this;
			this.dotNetBarManager1.RightDockSite = this.dockSite2;
			this.dotNetBarManager1.Style = 2;
			this.dotNetBarManager1.ToolbarBottomDockSite = this.dockSite8;
			this.dotNetBarManager1.ToolbarLeftDockSite = this.dockSite5;
			this.dotNetBarManager1.ToolbarRightDockSite = this.dockSite6;
			this.dotNetBarManager1.ToolbarTopDockSite = this.dockSite7;
			this.dotNetBarManager1.TopDockSite = this.dockSite3;
			this.dockSite8.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			this.dockSite8.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.dockSite8.Location = new global::System.Drawing.Point(0, 774);
			this.dockSite8.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dockSite8.Name = "dockSite8";
			this.dockSite8.Size = new global::System.Drawing.Size(985, 0);
			this.dockSite8.TabIndex = 16;
			this.dockSite8.TabStop = false;
			this.dockSite7.AccessibleRole = global::System.Windows.Forms.AccessibleRole.Window;
			this.dockSite7.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.dockSite7.Location = new global::System.Drawing.Point(0, 24);
			this.dockSite7.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.dockSite7.Name = "dockSite7";
			this.dockSite7.Size = new global::System.Drawing.Size(985, 0);
			this.dockSite7.TabIndex = 15;
			this.dockSite7.TabStop = false;
			this.p_bottom.Controls.Add(this.toolStrip3);
			this.p_bottom.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.p_bottom.Location = new global::System.Drawing.Point(198, 728);
			this.p_bottom.Name = "p_bottom";
			this.p_bottom.Padding = new global::System.Windows.Forms.Padding(2);
			this.p_bottom.Size = new global::System.Drawing.Size(420, 46);
			this.p_bottom.TabIndex = 17;
			this.toolStrip3.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip3.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip3.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip3.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_generateDefaultValuesXml,
				this.toolStripSeparator9,
				this.btn_apply,
				this.toolStripSeparator7,
				this.btn_save,
				this.btn_close
			});
			this.toolStrip3.Location = new global::System.Drawing.Point(2, 2);
			this.toolStrip3.Name = "toolStrip3";
			this.toolStrip3.Size = new global::System.Drawing.Size(416, 39);
			this.toolStrip3.TabIndex = 0;
			this.toolStrip3.Text = "toolStrip3";
			this.btn_generateDefaultValuesXml.Image = global::DynamicScreens.Properties.Resources.export1;
			this.btn_generateDefaultValuesXml.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_generateDefaultValuesXml.Name = "btn_generateDefaultValuesXml";
			this.btn_generateDefaultValuesXml.Size = new global::System.Drawing.Size(167, 36);
			this.btn_generateDefaultValuesXml.Text = "Get default values";
			this.btn_generateDefaultValuesXml.Click += new global::System.EventHandler(this.btn_generateDefaultValuesXml_Click);
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new global::System.Drawing.Size(6, 39);
			this.btn_apply.Image = global::DynamicScreens.Properties.Resources.check;
			this.btn_apply.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_apply.Name = "btn_apply";
			this.btn_apply.Size = new global::System.Drawing.Size(83, 36);
			this.btn_apply.Text = "&Apply";
			this.btn_apply.Click += new global::System.EventHandler(this.btn_apply_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new global::System.Drawing.Size(6, 39);
			this.btn_save.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_save.Image");
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(80, 36);
			this.btn_save.Text = "&Save";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_close.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("btn_close.Image");
			this.btn_close.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new global::System.Drawing.Size(85, 36);
			this.btn_close.Text = "&Close";
			this.btn_close.Click += new global::System.EventHandler(this.btn_close_Click);
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fileToolStripMenuItem,
				this.toolStripMenuItem6,
				this.functionsToolStripMenuItem
			});
			this.menuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new global::System.Drawing.Size(985, 24);
			this.menuStrip1.TabIndex = 18;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.exitToolStripMenuItem
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new global::System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new global::System.Drawing.Size(92, 22);
			this.exitToolStripMenuItem.Text = "&Exit";
			this.exitToolStripMenuItem.Click += new global::System.EventHandler(this.exitToolStripMenuItem_Click);
			this.toolStripMenuItem6.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.findToolStripMenuItem
			});
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new global::System.Drawing.Size(39, 20);
			this.toolStripMenuItem6.Text = "&Edit";
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.Size = new global::System.Drawing.Size(97, 22);
			this.findToolStripMenuItem.Text = "&Find";
			this.findToolStripMenuItem.Click += new global::System.EventHandler(this.findToolStripMenuItem_Click);
			this.functionsToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_pullInPreviouslyDeletedField,
				this.toolStripSeparator10,
				this.convertSelectedControlExistingDataToENCRYPTEDToolStripMenuItem,
				this.convertSelectedControlExistingDataToNOTENCRYPTEDToolStripMenuItem,
				this.toolStripMenuItem1,
				this.convertADroplistFromRegularTextbasedToolStripMenuItem,
				this.convertADroplistFromTextbasedToRegularToolStripMenuItem
			});
			this.functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
			this.functionsToolStripMenuItem.Size = new global::System.Drawing.Size(71, 20);
			this.functionsToolStripMenuItem.Text = "F&unctions";
			this.btn_pullInPreviouslyDeletedField.Name = "btn_pullInPreviouslyDeletedField";
			this.btn_pullInPreviouslyDeletedField.Size = new global::System.Drawing.Size(382, 22);
			this.btn_pullInPreviouslyDeletedField.Text = "Pull in previously deleted field";
			this.btn_pullInPreviouslyDeletedField.Click += new global::System.EventHandler(this.btn_pullInPreviouslyDeletedField_Click);
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new global::System.Drawing.Size(379, 6);
			this.convertSelectedControlExistingDataToENCRYPTEDToolStripMenuItem.Name = "convertSelectedControlExistingDataToENCRYPTEDToolStripMenuItem";
			this.convertSelectedControlExistingDataToENCRYPTEDToolStripMenuItem.Size = new global::System.Drawing.Size(382, 22);
			this.convertSelectedControlExistingDataToENCRYPTEDToolStripMenuItem.Text = "Convert selected control existing data to ENCRYPTED";
			this.convertSelectedControlExistingDataToENCRYPTEDToolStripMenuItem.Click += new global::System.EventHandler(this.convertSelectedControlExistingDataToENCRYPTEDToolStripMenuItem_Click);
			this.convertSelectedControlExistingDataToNOTENCRYPTEDToolStripMenuItem.Name = "convertSelectedControlExistingDataToNOTENCRYPTEDToolStripMenuItem";
			this.convertSelectedControlExistingDataToNOTENCRYPTEDToolStripMenuItem.Size = new global::System.Drawing.Size(382, 22);
			this.convertSelectedControlExistingDataToNOTENCRYPTEDToolStripMenuItem.Text = "Convert selected control existing data to NOT ENCRYPTED";
			this.convertSelectedControlExistingDataToNOTENCRYPTEDToolStripMenuItem.Click += new global::System.EventHandler(this.convertSelectedControlExistingDataToNOTENCRYPTEDToolStripMenuItem_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new global::System.Drawing.Size(379, 6);
			this.convertADroplistFromRegularTextbasedToolStripMenuItem.Name = "convertADroplistFromRegularTextbasedToolStripMenuItem";
			this.convertADroplistFromRegularTextbasedToolStripMenuItem.Size = new global::System.Drawing.Size(382, 22);
			this.convertADroplistFromRegularTextbasedToolStripMenuItem.Text = "Convert selected drop-list from regular -> text-based";
			this.convertADroplistFromRegularTextbasedToolStripMenuItem.Click += new global::System.EventHandler(this.convertADroplistFromRegularTextbasedToolStripMenuItem_Click);
			this.convertADroplistFromTextbasedToRegularToolStripMenuItem.Name = "convertADroplistFromTextbasedToRegularToolStripMenuItem";
			this.convertADroplistFromTextbasedToRegularToolStripMenuItem.Size = new global::System.Drawing.Size(382, 22);
			this.convertADroplistFromTextbasedToRegularToolStripMenuItem.Text = "Convert selected drop-list from text-based to regular";
			this.convertADroplistFromTextbasedToRegularToolStripMenuItem.Click += new global::System.EventHandler(this.convertADroplistFromTextbasedToRegularToolStripMenuItem_Click);
			this.dockContainerItem2.Control = this.panelDockContainer4;
			this.dockContainerItem2.Name = "dockContainerItem2";
			this.dockContainerItem2.Text = "Existing controls";
			this.dockContainerItem3.Control = this.panelDockContainer1;
			this.dockContainerItem3.Name = "dockContainerItem3";
			this.dockContainerItem3.Text = "Controls";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(985, 774);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.p_bottom);
			base.Controls.Add(this.dockSite2);
			base.Controls.Add(this.dockSite1);
			base.Controls.Add(this.dockSite3);
			base.Controls.Add(this.dockSite4);
			base.Controls.Add(this.dockSite5);
			base.Controls.Add(this.dockSite6);
			base.Controls.Add(this.dockSite7);
			base.Controls.Add(this.dockSite8);
			base.Controls.Add(this.menuStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MainMenuStrip = this.menuStrip1;
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "ScreenEditor";
			this.Text = "ClockWork Form Editor";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.ScreenEditor_FormClosing);
			base.Load += new global::System.EventHandler(this.ScreenEditor_Load);
			base.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.ScreenEditor_KeyDown);
			base.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.ScreenEditor_KeyUp);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.cm_nodes.ResumeLayout(false);
			this.tp_preview.ResumeLayout(false);
			this.p_top.ResumeLayout(false);
			this.p_top.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.navigationPane1.ResumeLayout(false);
			this.navigationPanePanel1.ResumeLayout(false);
			this.navigationPanePanel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.navigationPanePanel3.ResumeLayout(false);
			this.navigationPanePanel3.PerformLayout();
			this.toolStrip5.ResumeLayout(false);
			this.toolStrip5.PerformLayout();
			this.navigationPanePanel4.ResumeLayout(false);
			this.p_existingFields.ResumeLayout(false);
			this.dockSite2.ResumeLayout(false);
			this.bar3.EndInit();
			this.bar3.ResumeLayout(false);
			this.panelDockContainer3.ResumeLayout(false);
			this.panelDockContainer3.PerformLayout();
			this.toolStrip4.ResumeLayout(false);
			this.toolStrip4.PerformLayout();
			this.bar4.EndInit();
			this.bar4.ResumeLayout(false);
			this.panelDockContainer2.ResumeLayout(false);
			this.dockSite1.ResumeLayout(false);
			this.bar1.EndInit();
			this.bar1.ResumeLayout(false);
			this.panelDockContainer5.ResumeLayout(false);
			this.p_bottom.ResumeLayout(false);
			this.p_bottom.PerformLayout();
			this.toolStrip3.ResumeLayout(false);
			this.toolStrip3.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000086 RID: 134
		private global::Aga.Controls.Tree.TreeModel _model;

		// Token: 0x0400008B RID: 139
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400008C RID: 140
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x0400008D RID: 141
		private global::System.Windows.Forms.TabControl tabControl1;

		// Token: 0x0400008E RID: 142
		private global::DevComponents.DotNetBar.DockSite dockSite2;

		// Token: 0x0400008F RID: 143
		private global::DevComponents.DotNetBar.DockSite dockSite1;

		// Token: 0x04000090 RID: 144
		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItem1;

		// Token: 0x04000091 RID: 145
		private global::DevComponents.DotNetBar.DockSite dockSite3;

		// Token: 0x04000092 RID: 146
		private global::DevComponents.DotNetBar.DockSite dockSite4;

		// Token: 0x04000093 RID: 147
		private global::DevComponents.DotNetBar.DockSite dockSite5;

		// Token: 0x04000094 RID: 148
		private global::DevComponents.DotNetBar.DockSite dockSite6;

		// Token: 0x04000095 RID: 149
		private global::DevComponents.DotNetBar.DotNetBarManager dotNetBarManager1;

		// Token: 0x04000096 RID: 150
		private global::DevComponents.DotNetBar.DockSite dockSite7;

		// Token: 0x04000097 RID: 151
		private global::DevComponents.DotNetBar.DockSite dockSite8;

		// Token: 0x04000098 RID: 152
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000099 RID: 153
		private global::System.Windows.Forms.ToolStripButton btn_textbox;

		// Token: 0x0400009A RID: 154
		private global::System.Windows.Forms.ToolStripButton btn_label;

		// Token: 0x0400009B RID: 155
		private global::System.Windows.Forms.ToolStripButton btn_checkbox;

		// Token: 0x0400009C RID: 156
		private global::System.Windows.Forms.ToolStripButton btn_radioButtonGroup;

		// Token: 0x0400009D RID: 157
		private global::System.Windows.Forms.ToolStripButton btn_dropList;

		// Token: 0x0400009E RID: 158
		private global::System.Windows.Forms.ToolStripButton btn_picture;

		// Token: 0x0400009F RID: 159
		private global::System.Windows.Forms.ToolStripButton btn_table;

		// Token: 0x040000A0 RID: 160
		private global::System.Windows.Forms.ToolStripButton btn_fileList;

		// Token: 0x040000A1 RID: 161
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x040000A2 RID: 162
		private global::System.Windows.Forms.ToolStripButton btn_groupBox;

		// Token: 0x040000A3 RID: 163
		private global::System.Windows.Forms.ToolStripButton btn_columnBreak;

		// Token: 0x040000A4 RID: 164
		private global::System.Windows.Forms.ToolStripButton btn_blankSpace;

		// Token: 0x040000A5 RID: 165
		private global::System.Windows.Forms.ToolStripButton btn_tabControl;

		// Token: 0x040000A6 RID: 166
		private global::System.Windows.Forms.TabPage tabPage1;

		// Token: 0x040000A7 RID: 167
		private global::Aga.Controls.Tree.TreeViewAdv tv_design;

		// Token: 0x040000A8 RID: 168
		private global::Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox1;

		// Token: 0x040000A9 RID: 169
		private global::Aga.Controls.Tree.NodeControls.NodeIcon nodeIcon1;

		// Token: 0x040000AA RID: 170
		private global::System.Windows.Forms.ImageList imageList2;

		// Token: 0x040000AB RID: 171
		private global::System.Windows.Forms.Panel p_data;

		// Token: 0x040000AC RID: 172
		private global::System.Windows.Forms.TabPage tp_preview;

		// Token: 0x040000AD RID: 173
		private global::System.Windows.Forms.ToolStripButton btn_date;

		// Token: 0x040000AE RID: 174
		private global::System.Windows.Forms.ToolStripButton btn_hrule;

		// Token: 0x040000AF RID: 175
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x040000B0 RID: 176
		private global::System.Windows.Forms.Panel p_top;

		// Token: 0x040000B1 RID: 177
		private global::System.Windows.Forms.Splitter splitter1;

		// Token: 0x040000B2 RID: 178
		private global::System.Windows.Forms.Panel p_bottom;

		// Token: 0x040000B3 RID: 179
		private global::System.Windows.Forms.ToolStrip toolStrip3;

		// Token: 0x040000B4 RID: 180
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x040000B5 RID: 181
		private global::System.Windows.Forms.ToolStrip toolStrip2;

		// Token: 0x040000B6 RID: 182
		private global::System.Windows.Forms.ToolStripComboBox toolStripComboBox1;

		// Token: 0x040000B7 RID: 183
		private global::System.Windows.Forms.ToolStripButton btn_close;

		// Token: 0x040000B8 RID: 184
		private global::System.Windows.Forms.ToolStripButton btn_tabPage;

		// Token: 0x040000B9 RID: 185
		private global::DevComponents.DotNetBar.Bar bar3;

		// Token: 0x040000BA RID: 186
		private global::DevComponents.DotNetBar.PanelDockContainer panelDockContainer3;

		// Token: 0x040000BB RID: 187
		private global::System.Windows.Forms.ToolStrip toolStrip4;

		// Token: 0x040000BC RID: 188
		private global::System.Windows.Forms.ToolStripButton btn_newList;

		// Token: 0x040000BD RID: 189
		private global::System.Windows.Forms.ToolStripButton btn_editList;

		// Token: 0x040000BE RID: 190
		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItem4;

		// Token: 0x040000BF RID: 191
		private global::AutoComboBox.ListViewEx lv_lists;

		// Token: 0x040000C0 RID: 192
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x040000C1 RID: 193
		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItem5;

		// Token: 0x040000C2 RID: 194
		private global::DevComponents.DotNetBar.Bar bar4;

		// Token: 0x040000C3 RID: 195
		private global::DevComponents.DotNetBar.PanelDockContainer panelDockContainer2;

		// Token: 0x040000C4 RID: 196
		private global::System.Windows.Forms.PropertyGrid propertyGrid1;

		// Token: 0x040000C5 RID: 197
		private global::System.Windows.Forms.Button button1;

		// Token: 0x040000C6 RID: 198
		private global::DevComponents.DotNetBar.DockContainerItem Properties;

		// Token: 0x040000C7 RID: 199
		private global::System.Windows.Forms.ToolStripButton toolStripButton1;

		// Token: 0x040000C8 RID: 200
		private global::System.Windows.Forms.ToolStripButton btn_staffDropList;

		// Token: 0x040000C9 RID: 201
		private global::System.Windows.Forms.ContextMenuStrip cm_nodes;

		// Token: 0x040000CA RID: 202
		private global::System.Windows.Forms.ToolStripMenuItem entergroupCaptionsToolStripMenuItem;

		// Token: 0x040000CB RID: 203
		private global::System.Windows.Forms.MenuStrip menuStrip1;

		// Token: 0x040000CC RID: 204
		private global::System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

		// Token: 0x040000CD RID: 205
		private global::System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

		// Token: 0x040000CE RID: 206
		private global::System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem;

		// Token: 0x040000CF RID: 207
		private global::System.Windows.Forms.ToolStripMenuItem convertSelectedControlExistingDataToENCRYPTEDToolStripMenuItem;

		// Token: 0x040000D0 RID: 208
		private global::System.Windows.Forms.ToolStripMenuItem convertSelectedControlExistingDataToNOTENCRYPTEDToolStripMenuItem;

		// Token: 0x040000D1 RID: 209
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

		// Token: 0x040000D2 RID: 210
		private global::System.Windows.Forms.ToolStripButton btn_refreshGroups;

		// Token: 0x040000D3 RID: 211
		private global::System.Windows.Forms.ToolStripMenuItem createNewFieldsByEnteringCaptionsToolStripMenuItem;

		// Token: 0x040000D4 RID: 212
		private global::AutoComboBox.MyControls.TreeViewMS treeView_existingControls;

		// Token: 0x040000D5 RID: 213
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;

		// Token: 0x040000D6 RID: 214
		private global::System.Windows.Forms.ToolStripMenuItem convertADroplistFromRegularTextbasedToolStripMenuItem;

		// Token: 0x040000D7 RID: 215
		private global::System.Windows.Forms.ToolStripMenuItem convertADroplistFromTextbasedToRegularToolStripMenuItem;

		// Token: 0x040000D8 RID: 216
		private global::System.Windows.Forms.ToolStripButton btn_richTextBox;

		// Token: 0x040000D9 RID: 217
		private global::System.Windows.Forms.ToolStripButton btn_viewScreenControlInfo;

		// Token: 0x040000DA RID: 218
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

		// Token: 0x040000DB RID: 219
		private global::System.Windows.Forms.ToolStripButton btn_multiCheckbox;

		// Token: 0x040000DC RID: 220
		private global::System.Windows.Forms.ToolStripButton multiCheckboxWithTextboxToolStripMenuItem;

		// Token: 0x040000DD RID: 221
		private global::System.Windows.Forms.ToolStripButton multiCheckboxWithDroplistToolStripMenuItem;

		// Token: 0x040000DE RID: 222
		private global::System.Windows.Forms.ToolStripButton btn_multiCheckHeader;

		// Token: 0x040000DF RID: 223
		private global::System.Windows.Forms.ToolStripButton btn_multiLineTextbox;

		// Token: 0x040000E0 RID: 224
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;

		// Token: 0x040000E1 RID: 225
		private global::System.Windows.Forms.ToolStripMenuItem setCommonBackgroundColoursForSelectedGroupboxesToolStripMenuItem;

		// Token: 0x040000E2 RID: 226
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;

		// Token: 0x040000E3 RID: 227
		private global::System.Windows.Forms.ToolStripMenuItem commonSettingsToolStripMenuItem;

		// Token: 0x040000E4 RID: 228
		private global::System.Windows.Forms.ToolStripMenuItem setAsGroupBoxTitleToolStripMenuItem;

		// Token: 0x040000E5 RID: 229
		private global::System.Windows.Forms.ToolStripMenuItem setAsPhoneNumberToolStripMenuItem;

		// Token: 0x040000E6 RID: 230
		private global::DevComponents.DotNetBar.NavigationPane navigationPane1;

		// Token: 0x040000E7 RID: 231
		private global::DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel1;

		// Token: 0x040000E8 RID: 232
		private global::DevComponents.DotNetBar.ButtonItem pane_mainControls;

		// Token: 0x040000E9 RID: 233
		private global::DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel3;

		// Token: 0x040000EA RID: 234
		private global::DevComponents.DotNetBar.ButtonItem pane_accomm;

		// Token: 0x040000EB RID: 235
		private global::DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel4;

		// Token: 0x040000EC RID: 236
		private global::DevComponents.DotNetBar.ButtonItem panelbar_existingFields;

		// Token: 0x040000ED RID: 237
		private global::DevComponents.DotNetBar.PanelDockContainer panelDockContainer1;

		// Token: 0x040000EE RID: 238
		private global::DevComponents.DotNetBar.PanelDockContainer panelDockContainer4;

		// Token: 0x040000EF RID: 239
		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItem2;

		// Token: 0x040000F0 RID: 240
		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItem3;

		// Token: 0x040000F1 RID: 241
		private global::System.Windows.Forms.Panel p_existingFields;

		// Token: 0x040000F2 RID: 242
		private global::DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;

		// Token: 0x040000F3 RID: 243
		private global::System.Windows.Forms.Label lbl_existingControlsInstructions;

		// Token: 0x040000F4 RID: 244
		private global::System.Windows.Forms.ToolStrip toolStrip5;

		// Token: 0x040000F5 RID: 245
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

		// Token: 0x040000F6 RID: 246
		private global::System.Windows.Forms.ToolStripButton btn_accommodationCheckbox;

		// Token: 0x040000F7 RID: 247
		private global::DevComponents.DotNetBar.Bar bar1;

		// Token: 0x040000F8 RID: 248
		private global::DevComponents.DotNetBar.PanelDockContainer panelDockContainer5;

		// Token: 0x040000F9 RID: 249
		private global::DevComponents.DotNetBar.DockContainerItem dockContainerItem6;

		// Token: 0x040000FA RID: 250
		private global::System.Windows.Forms.ToolStripButton btn_accommodationTextbox;

		// Token: 0x040000FB RID: 251
		private global::System.Windows.Forms.ToolStripButton btn_accommodationDatePicker;

		// Token: 0x040000FC RID: 252
		private global::System.Windows.Forms.ToolStripButton btn_accommodationDropList;

		// Token: 0x040000FD RID: 253
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator6;

		// Token: 0x040000FE RID: 254
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;

		// Token: 0x040000FF RID: 255
		private global::System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;

		// Token: 0x04000100 RID: 256
		private global::System.Windows.Forms.ToolStripMenuItem convertTextBoxToRichTextBoxupgradeToolStripMenuItem;

		// Token: 0x04000101 RID: 257
		private global::System.Windows.Forms.ToolStripMenuItem convertRichTextBoxToTextBoxdowngradeToolStripMenuItem;

		// Token: 0x04000102 RID: 258
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;

		// Token: 0x04000103 RID: 259
		private global::System.Windows.Forms.ToolStripMenuItem getACommaSeparatedListOfControlidsForSelectedFieldsToolStripMenuItem;

		// Token: 0x04000104 RID: 260
		private global::System.Windows.Forms.ToolStripMenuItem MENU_markFieldsWithAGroupDescriptor;

		// Token: 0x04000105 RID: 261
		private global::System.Windows.Forms.ToolStripButton btn_apply;

		// Token: 0x04000106 RID: 262
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator7;

		// Token: 0x04000107 RID: 263
		private global::System.Windows.Forms.ToolStripLabel toolStripLabel1;

		// Token: 0x04000108 RID: 264
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator8;

		// Token: 0x04000109 RID: 265
		private global::System.Windows.Forms.ToolStripLabel toolStripLabel2;

		// Token: 0x0400010A RID: 266
		private global::System.Windows.Forms.ToolStripButton btn_perStudentForm;

		// Token: 0x0400010B RID: 267
		private global::System.Windows.Forms.ToolStripButton btn_listSelectItem;

		// Token: 0x0400010C RID: 268
		private global::System.Windows.Forms.ToolStripButton btn_dynamicTable;

		// Token: 0x0400010D RID: 269
		private global::System.Windows.Forms.ToolStripButton btn_dynamicControlsChooser;

		// Token: 0x0400010E RID: 270
		private global::System.Windows.Forms.ToolStripButton btn_multiItemDbChooser;

		// Token: 0x0400010F RID: 271
		private global::System.Windows.Forms.ToolStripButton btn_generateDefaultValuesXml;

		// Token: 0x04000110 RID: 272
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator9;

		// Token: 0x04000111 RID: 273
		private global::System.Windows.Forms.ToolStripMenuItem btn_pullInPreviouslyDeletedField;

		// Token: 0x04000112 RID: 274
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator10;

		// Token: 0x04000113 RID: 275
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;

		// Token: 0x04000114 RID: 276
		private global::System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;

		// Token: 0x04000115 RID: 277
		private global::System.Windows.Forms.ToolStripButton btn_infoBox;

		// Token: 0x04000116 RID: 278
		private global::System.Windows.Forms.ToolStripButton btn_calcButton;

		// Token: 0x04000117 RID: 279
		private global::System.Windows.Forms.ToolStripButton btn_caseList;

		// Token: 0x04000118 RID: 280
		private global::System.Windows.Forms.ToolStripButton btn_caseComboBox;

		// Token: 0x04000119 RID: 281
		private global::System.Windows.Forms.ToolStripButton btn_emailHistory;

		// Token: 0x0400011A RID: 282
		private global::System.Windows.Forms.ToolStripButton btn_appHistory;

		// Token: 0x0400011B RID: 283
		private global::System.Windows.Forms.ToolStripMenuItem MENU_misc;

		// Token: 0x0400011C RID: 284
		private global::System.Windows.Forms.ToolStripMenuItem whatOtherFormsDoesThisControlBelongToToolStripMenuItem;

		// Token: 0x0400011D RID: 285
		private global::System.Windows.Forms.ToolStripMenuItem editThelistToolStripMenuItem;

		// Token: 0x0400011E RID: 286
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator11;

		// Token: 0x0400011F RID: 287
		private global::System.Windows.Forms.ToolStripButton btn_list_rename;

		// Token: 0x04000120 RID: 288
		private global::System.Windows.Forms.ToolStripButton btn_list_delete;

		// Token: 0x04000121 RID: 289
		private global::System.Windows.Forms.ToolStripButton btn_list_undelete;
	}
}
