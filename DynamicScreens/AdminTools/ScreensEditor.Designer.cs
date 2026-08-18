namespace DynamicScreens.AdminTools
{
	// Token: 0x0200004F RID: 79
	public partial class ScreensEditor : global::System.Windows.Forms.Form
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x00037CF4 File Offset: 0x00036CF4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00037D2C File Offset: 0x00036D2C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DynamicScreens.AdminTools.ScreensEditor));
			this.toolStrip3 = new global::System.Windows.Forms.ToolStrip();
			this.btn_addNewForm = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_close = new global::System.Windows.Forms.ToolStripButton();
			this.groupPanel1 = new global::DevComponents.DotNetBar.Controls.GroupPanel();
			this.tv = new global::System.Windows.Forms.TreeView();
			this.imageList3 = new global::System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_addNewScreen = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_editSelectedForm = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_editScreen = new global::System.Windows.Forms.ToolStripButton();
			this.cm_form = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.btn_editFormDetails = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.editfieldsOnFormToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.MENU_deleteForm = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toggleEnabledToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new global::System.Windows.Forms.ToolStripSeparator();
			this.copyAllFieldsOnThisFormToAnotherFormToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.expandableSplitter1 = new global::DevComponents.DotNetBar.ExpandableSplitter();
			this.groupPanel2 = new global::DevComponents.DotNetBar.Controls.GroupPanel();
			this.p_preview = new global::System.Windows.Forms.Panel();
			this.label1 = new global::System.Windows.Forms.Label();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.imageList2 = new global::System.Windows.Forms.ImageList(this.components);
			this.imageList4 = new global::System.Windows.Forms.ImageList(this.components);
			this.menuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.importScreenFromXmlToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.exportToXmlToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.exportallFormsToXmlToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip3.SuspendLayout();
			this.groupPanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.cm_form.SuspendLayout();
			this.groupPanel2.SuspendLayout();
			this.p_preview.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip3.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip3.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip3.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip3.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip3.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_addNewForm,
				this.toolStripSeparator2,
				this.btn_save,
				this.btn_close
			});
			this.toolStrip3.Location = new global::System.Drawing.Point(0, 475);
			this.toolStrip3.Name = "toolStrip3";
			this.toolStrip3.Size = new global::System.Drawing.Size(687, 39);
			this.toolStrip3.TabIndex = 2;
			this.toolStrip3.Text = "toolStrip3";
			this.toolStrip3.ItemClicked += new global::System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip3_ItemClicked);
			this.btn_addNewForm.Image = global::DynamicScreens.Properties.Resources.add;
			this.btn_addNewForm.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_addNewForm.Name = "btn_addNewForm";
			this.btn_addNewForm.Size = new global::System.Drawing.Size(140, 36);
			this.btn_addNewForm.Text = "&Add new form";
			this.btn_addNewForm.Click += new global::System.EventHandler(this.btn_addNewForm_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(6, 39);
			this.btn_save.Image = global::DynamicScreens.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(80, 36);
			this.btn_save.Text = "&Save";
			this.btn_save.Visible = false;
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_close.Image = global::DynamicScreens.Properties.Resources.delete2;
			this.btn_close.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new global::System.Drawing.Size(85, 36);
			this.btn_close.Text = "&Close";
			this.btn_close.Click += new global::System.EventHandler(this.btn_close_Click);
			this.groupPanel1.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.groupPanel1.ColorSchemeStyle = 4;
			this.groupPanel1.Controls.Add(this.tv);
			this.groupPanel1.Controls.Add(this.toolStrip1);
			this.groupPanel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.groupPanel1.IsShadowEnabled = true;
			this.groupPanel1.Location = new global::System.Drawing.Point(0, 24);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Padding = new global::System.Windows.Forms.Padding(4);
			this.groupPanel1.Size = new global::System.Drawing.Size(405, 451);
			this.groupPanel1.Style.BackColor2SchemePart = 52;
			this.groupPanel1.Style.BackColorGradientAngle = 90;
			this.groupPanel1.Style.BackColorSchemePart = 51;
			this.groupPanel1.Style.BorderBottom = 1;
			this.groupPanel1.Style.BorderBottomWidth = 1;
			this.groupPanel1.Style.BorderColorSchemePart = 53;
			this.groupPanel1.Style.BorderLeft = 1;
			this.groupPanel1.Style.BorderLeftWidth = 1;
			this.groupPanel1.Style.BorderRight = 1;
			this.groupPanel1.Style.BorderRightWidth = 1;
			this.groupPanel1.Style.BorderTop = 1;
			this.groupPanel1.Style.BorderTopWidth = 1;
			this.groupPanel1.Style.CornerDiameter = 4;
			this.groupPanel1.Style.CornerType = 2;
			this.groupPanel1.Style.TextAlignment = 1;
			this.groupPanel1.Style.TextColorSchemePart = 54;
			this.groupPanel1.Style.TextLineAlignment = 0;
			this.groupPanel1.TabIndex = 4;
			this.groupPanel1.Text = "Forms";
			this.tv.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tv.FullRowSelect = true;
			this.tv.HideSelection = false;
			this.tv.ImageIndex = 0;
			this.tv.ImageList = this.imageList3;
			this.tv.Indent = 45;
			this.tv.Location = new global::System.Drawing.Point(4, 29);
			this.tv.Name = "tv";
			this.tv.SelectedImageIndex = 0;
			this.tv.Size = new global::System.Drawing.Size(391, 392);
			this.tv.TabIndex = 2;
			this.tv.DoubleClick += new global::System.EventHandler(this.tv_DoubleClick);
			this.tv.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.tv_KeyDown);
			this.tv.MouseUp += new global::System.Windows.Forms.MouseEventHandler(this.tv_MouseUp);
			this.imageList3.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList3.ImageStream");
			this.imageList3.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList3.Images.SetKeyName(0, "folder.png");
			this.imageList3.Images.SetKeyName(1, "delete_yellowBurn.png");
			this.imageList3.Images.SetKeyName(2, "");
			this.imageList3.Images.SetKeyName(3, "");
			this.imageList3.Images.SetKeyName(4, "");
			this.imageList3.Images.SetKeyName(5, "");
			this.imageList3.Images.SetKeyName(6, "");
			this.imageList3.Images.SetKeyName(7, "");
			this.imageList3.Images.SetKeyName(8, "");
			this.imageList3.Images.SetKeyName(9, "");
			this.imageList3.Images.SetKeyName(10, "");
			this.imageList3.Images.SetKeyName(11, "");
			this.imageList3.Images.SetKeyName(12, "");
			this.imageList3.Images.SetKeyName(13, "");
			this.imageList3.Images.SetKeyName(14, "");
			this.imageList3.Images.SetKeyName(15, "");
			this.imageList3.Images.SetKeyName(16, "");
			this.imageList3.Images.SetKeyName(17, "");
			this.imageList3.Images.SetKeyName(18, "");
			this.imageList3.Images.SetKeyName(19, "");
			this.imageList3.Images.SetKeyName(20, "");
			this.imageList3.Images.SetKeyName(21, "");
			this.imageList3.Images.SetKeyName(22, "");
			this.imageList3.Images.SetKeyName(23, "");
			this.imageList3.Images.SetKeyName(24, "");
			this.imageList3.Images.SetKeyName(25, "");
			this.imageList3.Images.SetKeyName(26, "");
			this.imageList3.Images.SetKeyName(27, "");
			this.imageList3.Images.SetKeyName(28, "");
			this.imageList3.Images.SetKeyName(29, "");
			this.imageList3.Images.SetKeyName(30, "");
			this.imageList3.Images.SetKeyName(31, "");
			this.imageList3.Images.SetKeyName(32, "");
			this.imageList3.Images.SetKeyName(33, "");
			this.imageList3.Images.SetKeyName(34, "");
			this.imageList3.Images.SetKeyName(35, "");
			this.imageList3.Images.SetKeyName(36, "");
			this.imageList3.Images.SetKeyName(37, "");
			this.imageList3.Images.SetKeyName(38, "");
			this.imageList3.Images.SetKeyName(39, "");
			this.imageList3.Images.SetKeyName(40, "");
			this.imageList3.Images.SetKeyName(41, "");
			this.imageList3.Images.SetKeyName(42, "");
			this.imageList3.Images.SetKeyName(43, "");
			this.imageList3.Images.SetKeyName(44, "");
			this.imageList3.Images.SetKeyName(45, "");
			this.imageList3.Images.SetKeyName(46, "");
			this.imageList3.Images.SetKeyName(47, "");
			this.imageList3.Images.SetKeyName(48, "");
			this.imageList3.Images.SetKeyName(49, "");
			this.imageList3.Images.SetKeyName(50, "");
			this.imageList3.Images.SetKeyName(51, "");
			this.imageList3.Images.SetKeyName(52, "");
			this.imageList3.Images.SetKeyName(53, "");
			this.imageList3.Images.SetKeyName(54, "");
			this.imageList3.Images.SetKeyName(55, "");
			this.imageList3.Images.SetKeyName(56, "");
			this.imageList3.Images.SetKeyName(57, "");
			this.imageList3.Images.SetKeyName(58, "");
			this.imageList3.Images.SetKeyName(59, "");
			this.imageList3.Images.SetKeyName(60, "");
			this.imageList3.Images.SetKeyName(61, "");
			this.imageList3.Images.SetKeyName(62, "");
			this.imageList3.Images.SetKeyName(63, "");
			this.imageList3.Images.SetKeyName(64, "");
			this.imageList3.Images.SetKeyName(65, "");
			this.imageList3.Images.SetKeyName(66, "");
			this.imageList3.Images.SetKeyName(67, "");
			this.imageList3.Images.SetKeyName(68, "");
			this.imageList3.Images.SetKeyName(69, "");
			this.imageList3.Images.SetKeyName(70, "");
			this.imageList3.Images.SetKeyName(71, "");
			this.imageList3.Images.SetKeyName(72, "");
			this.imageList3.Images.SetKeyName(73, "");
			this.imageList3.Images.SetKeyName(74, "");
			this.imageList3.Images.SetKeyName(75, "");
			this.imageList3.Images.SetKeyName(76, "");
			this.imageList3.Images.SetKeyName(77, "");
			this.imageList3.Images.SetKeyName(78, "");
			this.imageList3.Images.SetKeyName(79, "");
			this.imageList3.Images.SetKeyName(80, "");
			this.imageList3.Images.SetKeyName(81, "");
			this.imageList3.Images.SetKeyName(82, "");
			this.imageList3.Images.SetKeyName(83, "");
			this.imageList3.Images.SetKeyName(84, "");
			this.imageList3.Images.SetKeyName(85, "");
			this.imageList3.Images.SetKeyName(86, "");
			this.imageList3.Images.SetKeyName(87, "");
			this.imageList3.Images.SetKeyName(88, "");
			this.imageList3.Images.SetKeyName(89, "");
			this.imageList3.Images.SetKeyName(90, "");
			this.imageList3.Images.SetKeyName(91, "");
			this.imageList3.Images.SetKeyName(92, "");
			this.imageList3.Images.SetKeyName(93, "");
			this.imageList3.Images.SetKeyName(94, "");
			this.imageList3.Images.SetKeyName(95, "");
			this.imageList3.Images.SetKeyName(96, "");
			this.imageList3.Images.SetKeyName(97, "");
			this.imageList3.Images.SetKeyName(98, "");
			this.imageList3.Images.SetKeyName(99, "");
			this.imageList3.Images.SetKeyName(100, "");
			this.imageList3.Images.SetKeyName(101, "");
			this.imageList3.Images.SetKeyName(102, "");
			this.imageList3.Images.SetKeyName(103, "");
			this.imageList3.Images.SetKeyName(104, "");
			this.imageList3.Images.SetKeyName(105, "");
			this.imageList3.Images.SetKeyName(106, "");
			this.imageList3.Images.SetKeyName(107, "");
			this.imageList3.Images.SetKeyName(108, "");
			this.imageList3.Images.SetKeyName(109, "");
			this.imageList3.Images.SetKeyName(110, "");
			this.imageList3.Images.SetKeyName(111, "");
			this.imageList3.Images.SetKeyName(112, "");
			this.imageList3.Images.SetKeyName(113, "");
			this.imageList3.Images.SetKeyName(114, "");
			this.imageList3.Images.SetKeyName(115, "");
			this.imageList3.Images.SetKeyName(116, "");
			this.imageList3.Images.SetKeyName(117, "");
			this.imageList3.Images.SetKeyName(118, "");
			this.imageList3.Images.SetKeyName(119, "");
			this.imageList3.Images.SetKeyName(120, "");
			this.imageList3.Images.SetKeyName(121, "");
			this.imageList3.Images.SetKeyName(122, "");
			this.imageList3.Images.SetKeyName(123, "");
			this.imageList3.Images.SetKeyName(124, "");
			this.imageList3.Images.SetKeyName(125, "");
			this.imageList3.Images.SetKeyName(126, "folder_window.png");
			this.imageList3.Images.SetKeyName(127, "table.png");
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_addNewScreen,
				this.toolStripSeparator1,
				this.btn_editSelectedForm,
				this.toolStripSeparator3,
				this.btn_editScreen
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(4, 4);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(391, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_addNewScreen.Image = global::DynamicScreens.Properties.Resources.add;
			this.btn_addNewScreen.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_addNewScreen.Name = "btn_addNewScreen";
			this.btn_addNewScreen.Size = new global::System.Drawing.Size(103, 22);
			this.btn_addNewScreen.Text = "&Add new form";
			this.btn_addNewScreen.Click += new global::System.EventHandler(this.btn_addNewScreen_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 25);
			this.btn_editSelectedForm.Image = global::DynamicScreens.Properties.Resources.edit;
			this.btn_editSelectedForm.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_editSelectedForm.Name = "btn_editSelectedForm";
			this.btn_editSelectedForm.Size = new global::System.Drawing.Size(170, 22);
			this.btn_editSelectedForm.Text = "&Edit fields on selected form";
			this.btn_editSelectedForm.Click += new global::System.EventHandler(this.btn_editSelectedForm_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new global::System.Drawing.Size(6, 25);
			this.btn_editScreen.Image = global::DynamicScreens.Properties.Resources.about;
			this.btn_editScreen.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_editScreen.Name = "btn_editScreen";
			this.btn_editScreen.Size = new global::System.Drawing.Size(159, 20);
			this.btn_editScreen.Text = "E&dit selected form details";
			this.btn_editScreen.Click += new global::System.EventHandler(this.btn_editScreen_Click);
			this.cm_form.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_editFormDetails,
				this.toolStripMenuItem1,
				this.editfieldsOnFormToolStripMenuItem,
				this.toolStripMenuItem2,
				this.MENU_deleteForm,
				this.toolStripSeparator4,
				this.toggleEnabledToolStripMenuItem,
				this.toolStripMenuItem5,
				this.copyAllFieldsOnThisFormToAnotherFormToolStripMenuItem
			});
			this.cm_form.Name = "cm_form";
			this.cm_form.Size = new global::System.Drawing.Size(304, 160);
			this.btn_editFormDetails.AccessibleDescription = "Edit form details";
			this.btn_editFormDetails.AccessibleName = "Edit form details";
			this.btn_editFormDetails.Name = "btn_editFormDetails";
			this.btn_editFormDetails.Size = new global::System.Drawing.Size(303, 22);
			this.btn_editFormDetails.Text = "&Edit form details";
			this.btn_editFormDetails.Click += new global::System.EventHandler(this.btn_editFormDetails_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new global::System.Drawing.Size(300, 6);
			this.editfieldsOnFormToolStripMenuItem.AccessibleDescription = "Edit fields on form";
			this.editfieldsOnFormToolStripMenuItem.AccessibleName = "Edit fields on form";
			this.editfieldsOnFormToolStripMenuItem.Name = "editfieldsOnFormToolStripMenuItem";
			this.editfieldsOnFormToolStripMenuItem.Size = new global::System.Drawing.Size(303, 22);
			this.editfieldsOnFormToolStripMenuItem.Text = "Edit &fields on form";
			this.editfieldsOnFormToolStripMenuItem.Click += new global::System.EventHandler(this.editfieldsOnFormToolStripMenuItem_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new global::System.Drawing.Size(300, 6);
			this.MENU_deleteForm.Name = "MENU_deleteForm";
			this.MENU_deleteForm.Size = new global::System.Drawing.Size(303, 22);
			this.MENU_deleteForm.Text = "&Delete form";
			this.MENU_deleteForm.Click += new global::System.EventHandler(this.MENU_deleteForm_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new global::System.Drawing.Size(300, 6);
			this.toggleEnabledToolStripMenuItem.AccessibleDescription = "Toggle form enabled";
			this.toggleEnabledToolStripMenuItem.AccessibleName = "Toggle form enabled";
			this.toggleEnabledToolStripMenuItem.Name = "toggleEnabledToolStripMenuItem";
			this.toggleEnabledToolStripMenuItem.Size = new global::System.Drawing.Size(303, 22);
			this.toggleEnabledToolStripMenuItem.Text = "&Toggle enabled";
			this.toggleEnabledToolStripMenuItem.Click += new global::System.EventHandler(this.toggleEnabledToolStripMenuItem_Click);
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new global::System.Drawing.Size(300, 6);
			this.copyAllFieldsOnThisFormToAnotherFormToolStripMenuItem.Name = "copyAllFieldsOnThisFormToAnotherFormToolStripMenuItem";
			this.copyAllFieldsOnThisFormToAnotherFormToolStripMenuItem.Size = new global::System.Drawing.Size(303, 22);
			this.copyAllFieldsOnThisFormToAnotherFormToolStripMenuItem.Text = "Copy all fields on this form to another form";
			this.copyAllFieldsOnThisFormToAnotherFormToolStripMenuItem.Click += new global::System.EventHandler(this.copyAllFieldsOnThisFormToAnotherFormToolStripMenuItem_Click);
			this.expandableSplitter1.BackColor2 = global::System.Drawing.Color.FromArgb(0, 45, 150);
			this.expandableSplitter1.BackColor2SchemePart = 53;
			this.expandableSplitter1.BackColorSchemePart = 51;
			this.expandableSplitter1.Dock = global::System.Windows.Forms.DockStyle.Right;
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
			this.expandableSplitter1.Location = new global::System.Drawing.Point(405, 24);
			this.expandableSplitter1.Name = "expandableSplitter1";
			this.expandableSplitter1.Size = new global::System.Drawing.Size(10, 451);
			this.expandableSplitter1.TabIndex = 6;
			this.expandableSplitter1.TabStop = false;
			this.expandableSplitter1.Visible = false;
			this.groupPanel2.CanvasColor = global::System.Drawing.SystemColors.Control;
			this.groupPanel2.ColorSchemeStyle = 4;
			this.groupPanel2.Controls.Add(this.p_preview);
			this.groupPanel2.Dock = global::System.Windows.Forms.DockStyle.Right;
			this.groupPanel2.IsShadowEnabled = true;
			this.groupPanel2.Location = new global::System.Drawing.Point(415, 24);
			this.groupPanel2.Name = "groupPanel2";
			this.groupPanel2.Padding = new global::System.Windows.Forms.Padding(4);
			this.groupPanel2.Size = new global::System.Drawing.Size(272, 451);
			this.groupPanel2.Style.BackColor2SchemePart = 52;
			this.groupPanel2.Style.BackColorGradientAngle = 90;
			this.groupPanel2.Style.BackColorSchemePart = 51;
			this.groupPanel2.Style.BorderBottom = 1;
			this.groupPanel2.Style.BorderBottomWidth = 1;
			this.groupPanel2.Style.BorderColorSchemePart = 53;
			this.groupPanel2.Style.BorderLeft = 1;
			this.groupPanel2.Style.BorderLeftWidth = 1;
			this.groupPanel2.Style.BorderRight = 1;
			this.groupPanel2.Style.BorderRightWidth = 1;
			this.groupPanel2.Style.BorderTop = 1;
			this.groupPanel2.Style.BorderTopWidth = 1;
			this.groupPanel2.Style.CornerDiameter = 4;
			this.groupPanel2.Style.CornerType = 2;
			this.groupPanel2.Style.TextAlignment = 1;
			this.groupPanel2.Style.TextColorSchemePart = 54;
			this.groupPanel2.Style.TextLineAlignment = 0;
			this.groupPanel2.TabIndex = 7;
			this.groupPanel2.Text = "Details";
			this.groupPanel2.Visible = false;
			this.p_preview.Controls.Add(this.label1);
			this.p_preview.Controls.Add(this.pictureBox1);
			this.p_preview.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_preview.Location = new global::System.Drawing.Point(4, 4);
			this.p_preview.Name = "p_preview";
			this.p_preview.Size = new global::System.Drawing.Size(258, 157);
			this.p_preview.TabIndex = 1;
			this.label1.AutoSize = true;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new global::System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(64, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Preview";
			this.pictureBox1.Location = new global::System.Drawing.Point(5, 22);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(248, 121);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.imageList2.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList2.ImageStream");
			this.imageList2.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList2.Images.SetKeyName(0, "");
			this.imageList2.Images.SetKeyName(1, "");
			this.imageList2.Images.SetKeyName(2, "");
			this.imageList2.Images.SetKeyName(3, "");
			this.imageList2.Images.SetKeyName(4, "");
			this.imageList2.Images.SetKeyName(5, "");
			this.imageList2.Images.SetKeyName(6, "");
			this.imageList2.Images.SetKeyName(7, "");
			this.imageList2.Images.SetKeyName(8, "");
			this.imageList2.Images.SetKeyName(9, "");
			this.imageList2.Images.SetKeyName(10, "");
			this.imageList2.Images.SetKeyName(11, "");
			this.imageList2.Images.SetKeyName(12, "");
			this.imageList2.Images.SetKeyName(13, "");
			this.imageList2.Images.SetKeyName(14, "");
			this.imageList2.Images.SetKeyName(15, "");
			this.imageList2.Images.SetKeyName(16, "");
			this.imageList2.Images.SetKeyName(17, "");
			this.imageList2.Images.SetKeyName(18, "");
			this.imageList2.Images.SetKeyName(19, "");
			this.imageList2.Images.SetKeyName(20, "");
			this.imageList2.Images.SetKeyName(21, "");
			this.imageList2.Images.SetKeyName(22, "");
			this.imageList2.Images.SetKeyName(23, "");
			this.imageList2.Images.SetKeyName(24, "");
			this.imageList2.Images.SetKeyName(25, "");
			this.imageList2.Images.SetKeyName(26, "");
			this.imageList2.Images.SetKeyName(27, "");
			this.imageList2.Images.SetKeyName(28, "");
			this.imageList2.Images.SetKeyName(29, "");
			this.imageList2.Images.SetKeyName(30, "");
			this.imageList2.Images.SetKeyName(31, "");
			this.imageList2.Images.SetKeyName(32, "");
			this.imageList2.Images.SetKeyName(33, "");
			this.imageList2.Images.SetKeyName(34, "");
			this.imageList2.Images.SetKeyName(35, "");
			this.imageList2.Images.SetKeyName(36, "");
			this.imageList2.Images.SetKeyName(37, "");
			this.imageList2.Images.SetKeyName(38, "");
			this.imageList2.Images.SetKeyName(39, "");
			this.imageList2.Images.SetKeyName(40, "");
			this.imageList2.Images.SetKeyName(41, "");
			this.imageList2.Images.SetKeyName(42, "");
			this.imageList2.Images.SetKeyName(43, "");
			this.imageList2.Images.SetKeyName(44, "");
			this.imageList2.Images.SetKeyName(45, "");
			this.imageList2.Images.SetKeyName(46, "");
			this.imageList2.Images.SetKeyName(47, "");
			this.imageList2.Images.SetKeyName(48, "");
			this.imageList2.Images.SetKeyName(49, "");
			this.imageList2.Images.SetKeyName(50, "");
			this.imageList2.Images.SetKeyName(51, "");
			this.imageList2.Images.SetKeyName(52, "");
			this.imageList2.Images.SetKeyName(53, "");
			this.imageList2.Images.SetKeyName(54, "");
			this.imageList2.Images.SetKeyName(55, "");
			this.imageList2.Images.SetKeyName(56, "");
			this.imageList2.Images.SetKeyName(57, "");
			this.imageList2.Images.SetKeyName(58, "");
			this.imageList2.Images.SetKeyName(59, "");
			this.imageList2.Images.SetKeyName(60, "");
			this.imageList2.Images.SetKeyName(61, "");
			this.imageList2.Images.SetKeyName(62, "");
			this.imageList2.Images.SetKeyName(63, "");
			this.imageList2.Images.SetKeyName(64, "");
			this.imageList2.Images.SetKeyName(65, "");
			this.imageList2.Images.SetKeyName(66, "");
			this.imageList2.Images.SetKeyName(67, "");
			this.imageList2.Images.SetKeyName(68, "");
			this.imageList2.Images.SetKeyName(69, "");
			this.imageList2.Images.SetKeyName(70, "");
			this.imageList2.Images.SetKeyName(71, "");
			this.imageList2.Images.SetKeyName(72, "");
			this.imageList2.Images.SetKeyName(73, "");
			this.imageList2.Images.SetKeyName(74, "");
			this.imageList2.Images.SetKeyName(75, "");
			this.imageList2.Images.SetKeyName(76, "");
			this.imageList2.Images.SetKeyName(77, "");
			this.imageList2.Images.SetKeyName(78, "");
			this.imageList2.Images.SetKeyName(79, "");
			this.imageList2.Images.SetKeyName(80, "");
			this.imageList2.Images.SetKeyName(81, "");
			this.imageList2.Images.SetKeyName(82, "");
			this.imageList2.Images.SetKeyName(83, "");
			this.imageList2.Images.SetKeyName(84, "");
			this.imageList2.Images.SetKeyName(85, "");
			this.imageList2.Images.SetKeyName(86, "");
			this.imageList2.Images.SetKeyName(87, "");
			this.imageList2.Images.SetKeyName(88, "");
			this.imageList2.Images.SetKeyName(89, "");
			this.imageList2.Images.SetKeyName(90, "");
			this.imageList2.Images.SetKeyName(91, "");
			this.imageList2.Images.SetKeyName(92, "");
			this.imageList2.Images.SetKeyName(93, "");
			this.imageList2.Images.SetKeyName(94, "");
			this.imageList2.Images.SetKeyName(95, "");
			this.imageList2.Images.SetKeyName(96, "");
			this.imageList2.Images.SetKeyName(97, "");
			this.imageList2.Images.SetKeyName(98, "");
			this.imageList2.Images.SetKeyName(99, "");
			this.imageList2.Images.SetKeyName(100, "");
			this.imageList2.Images.SetKeyName(101, "");
			this.imageList2.Images.SetKeyName(102, "");
			this.imageList2.Images.SetKeyName(103, "");
			this.imageList2.Images.SetKeyName(104, "");
			this.imageList2.Images.SetKeyName(105, "");
			this.imageList2.Images.SetKeyName(106, "");
			this.imageList2.Images.SetKeyName(107, "");
			this.imageList2.Images.SetKeyName(108, "");
			this.imageList4.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList4.ImageStream");
			this.imageList4.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList4.Images.SetKeyName(0, "");
			this.imageList4.Images.SetKeyName(1, "");
			this.imageList4.Images.SetKeyName(2, "");
			this.imageList4.Images.SetKeyName(3, "");
			this.imageList4.Images.SetKeyName(4, "");
			this.imageList4.Images.SetKeyName(5, "");
			this.imageList4.Images.SetKeyName(6, "");
			this.imageList4.Images.SetKeyName(7, "");
			this.imageList4.Images.SetKeyName(8, "");
			this.imageList4.Images.SetKeyName(9, "");
			this.imageList4.Images.SetKeyName(10, "");
			this.imageList4.Images.SetKeyName(11, "");
			this.imageList4.Images.SetKeyName(12, "");
			this.imageList4.Images.SetKeyName(13, "");
			this.imageList4.Images.SetKeyName(14, "");
			this.imageList4.Images.SetKeyName(15, "");
			this.imageList4.Images.SetKeyName(16, "");
			this.imageList4.Images.SetKeyName(17, "");
			this.imageList4.Images.SetKeyName(18, "");
			this.imageList4.Images.SetKeyName(19, "");
			this.imageList4.Images.SetKeyName(20, "");
			this.imageList4.Images.SetKeyName(21, "");
			this.imageList4.Images.SetKeyName(22, "");
			this.imageList4.Images.SetKeyName(23, "");
			this.imageList4.Images.SetKeyName(24, "");
			this.imageList4.Images.SetKeyName(25, "");
			this.imageList4.Images.SetKeyName(26, "");
			this.imageList4.Images.SetKeyName(27, "");
			this.imageList4.Images.SetKeyName(28, "");
			this.imageList4.Images.SetKeyName(29, "");
			this.imageList4.Images.SetKeyName(30, "");
			this.imageList4.Images.SetKeyName(31, "");
			this.imageList4.Images.SetKeyName(32, "");
			this.imageList4.Images.SetKeyName(33, "");
			this.imageList4.Images.SetKeyName(34, "");
			this.imageList4.Images.SetKeyName(35, "");
			this.imageList4.Images.SetKeyName(36, "");
			this.imageList4.Images.SetKeyName(37, "");
			this.imageList4.Images.SetKeyName(38, "");
			this.imageList4.Images.SetKeyName(39, "");
			this.imageList4.Images.SetKeyName(40, "");
			this.imageList4.Images.SetKeyName(41, "");
			this.imageList4.Images.SetKeyName(42, "");
			this.imageList4.Images.SetKeyName(43, "");
			this.imageList4.Images.SetKeyName(44, "");
			this.imageList4.Images.SetKeyName(45, "");
			this.imageList4.Images.SetKeyName(46, "");
			this.imageList4.Images.SetKeyName(47, "");
			this.imageList4.Images.SetKeyName(48, "");
			this.imageList4.Images.SetKeyName(49, "");
			this.imageList4.Images.SetKeyName(50, "");
			this.imageList4.Images.SetKeyName(51, "");
			this.imageList4.Images.SetKeyName(52, "");
			this.imageList4.Images.SetKeyName(53, "");
			this.imageList4.Images.SetKeyName(54, "");
			this.imageList4.Images.SetKeyName(55, "");
			this.imageList4.Images.SetKeyName(56, "");
			this.imageList4.Images.SetKeyName(57, "");
			this.imageList4.Images.SetKeyName(58, "");
			this.imageList4.Images.SetKeyName(59, "");
			this.imageList4.Images.SetKeyName(60, "");
			this.imageList4.Images.SetKeyName(61, "");
			this.imageList4.Images.SetKeyName(62, "");
			this.imageList4.Images.SetKeyName(63, "");
			this.imageList4.Images.SetKeyName(64, "");
			this.imageList4.Images.SetKeyName(65, "");
			this.imageList4.Images.SetKeyName(66, "");
			this.imageList4.Images.SetKeyName(67, "");
			this.imageList4.Images.SetKeyName(68, "");
			this.imageList4.Images.SetKeyName(69, "");
			this.imageList4.Images.SetKeyName(70, "");
			this.imageList4.Images.SetKeyName(71, "");
			this.imageList4.Images.SetKeyName(72, "");
			this.imageList4.Images.SetKeyName(73, "");
			this.imageList4.Images.SetKeyName(74, "");
			this.imageList4.Images.SetKeyName(75, "");
			this.imageList4.Images.SetKeyName(76, "");
			this.imageList4.Images.SetKeyName(77, "");
			this.imageList4.Images.SetKeyName(78, "");
			this.imageList4.Images.SetKeyName(79, "");
			this.imageList4.Images.SetKeyName(80, "");
			this.imageList4.Images.SetKeyName(81, "");
			this.imageList4.Images.SetKeyName(82, "");
			this.imageList4.Images.SetKeyName(83, "");
			this.imageList4.Images.SetKeyName(84, "");
			this.imageList4.Images.SetKeyName(85, "");
			this.imageList4.Images.SetKeyName(86, "");
			this.imageList4.Images.SetKeyName(87, "");
			this.imageList4.Images.SetKeyName(88, "");
			this.imageList4.Images.SetKeyName(89, "");
			this.imageList4.Images.SetKeyName(90, "");
			this.imageList4.Images.SetKeyName(91, "");
			this.imageList4.Images.SetKeyName(92, "");
			this.imageList4.Images.SetKeyName(93, "");
			this.imageList4.Images.SetKeyName(94, "");
			this.imageList4.Images.SetKeyName(95, "");
			this.imageList4.Images.SetKeyName(96, "");
			this.imageList4.Images.SetKeyName(97, "");
			this.imageList4.Images.SetKeyName(98, "");
			this.imageList4.Images.SetKeyName(99, "");
			this.imageList4.Images.SetKeyName(100, "");
			this.imageList4.Images.SetKeyName(101, "");
			this.imageList4.Images.SetKeyName(102, "");
			this.imageList4.Images.SetKeyName(103, "");
			this.imageList4.Images.SetKeyName(104, "");
			this.imageList4.Images.SetKeyName(105, "");
			this.imageList4.Images.SetKeyName(106, "");
			this.imageList4.Images.SetKeyName(107, "");
			this.imageList4.Images.SetKeyName(108, "");
			this.imageList4.Images.SetKeyName(109, "");
			this.imageList4.Images.SetKeyName(110, "");
			this.imageList4.Images.SetKeyName(111, "");
			this.imageList4.Images.SetKeyName(112, "");
			this.imageList4.Images.SetKeyName(113, "");
			this.imageList4.Images.SetKeyName(114, "");
			this.imageList4.Images.SetKeyName(115, "");
			this.imageList4.Images.SetKeyName(116, "");
			this.imageList4.Images.SetKeyName(117, "");
			this.imageList4.Images.SetKeyName(118, "");
			this.imageList4.Images.SetKeyName(119, "");
			this.imageList4.Images.SetKeyName(120, "");
			this.imageList4.Images.SetKeyName(121, "");
			this.imageList4.Images.SetKeyName(122, "");
			this.imageList4.Images.SetKeyName(123, "");
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fileToolStripMenuItem
			});
			this.menuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new global::System.Drawing.Size(687, 24);
			this.menuStrip1.TabIndex = 8;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.importScreenFromXmlToolStripMenuItem,
				this.exportToXmlToolStripMenuItem,
				this.toolStripMenuItem3,
				this.exportallFormsToXmlToolStripMenuItem,
				this.toolStripMenuItem4,
				this.exitToolStripMenuItem
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new global::System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			this.importScreenFromXmlToolStripMenuItem.Name = "importScreenFromXmlToolStripMenuItem";
			this.importScreenFromXmlToolStripMenuItem.Size = new global::System.Drawing.Size(198, 22);
			this.importScreenFromXmlToolStripMenuItem.Text = "&Import screen from xml";
			this.importScreenFromXmlToolStripMenuItem.Click += new global::System.EventHandler(this.importScreenFromXmlToolStripMenuItem_Click);
			this.exportToXmlToolStripMenuItem.Name = "exportToXmlToolStripMenuItem";
			this.exportToXmlToolStripMenuItem.Size = new global::System.Drawing.Size(198, 22);
			this.exportToXmlToolStripMenuItem.Text = "&Export form to xml";
			this.exportToXmlToolStripMenuItem.Click += new global::System.EventHandler(this.exportToXmlToolStripMenuItem_Click);
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new global::System.Drawing.Size(195, 6);
			this.exportallFormsToXmlToolStripMenuItem.Name = "exportallFormsToXmlToolStripMenuItem";
			this.exportallFormsToXmlToolStripMenuItem.Size = new global::System.Drawing.Size(198, 22);
			this.exportallFormsToXmlToolStripMenuItem.Text = "Export &all forms to xml";
			this.exportallFormsToXmlToolStripMenuItem.Click += new global::System.EventHandler(this.exportallFormsToXmlToolStripMenuItem_Click);
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new global::System.Drawing.Size(195, 6);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new global::System.Drawing.Size(198, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new global::System.EventHandler(this.exitToolStripMenuItem_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(9f, 18f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(687, 514);
			base.Controls.Add(this.groupPanel1);
			base.Controls.Add(this.expandableSplitter1);
			base.Controls.Add(this.groupPanel2);
			base.Controls.Add(this.toolStrip3);
			base.Controls.Add(this.menuStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			base.Margin = new global::System.Windows.Forms.Padding(4);
			base.Name = "ScreensEditor";
			this.Text = "ClockWork Forms Editor";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.ScreensEditor_Load);
			this.toolStrip3.ResumeLayout(false);
			this.toolStrip3.PerformLayout();
			this.groupPanel1.ResumeLayout(false);
			this.groupPanel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.cm_form.ResumeLayout(false);
			this.groupPanel2.ResumeLayout(false);
			this.p_preview.ResumeLayout(false);
			this.p_preview.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002F0 RID: 752
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040002F1 RID: 753
		private global::System.Windows.Forms.ToolStrip toolStrip3;

		// Token: 0x040002F2 RID: 754
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x040002F3 RID: 755
		private global::System.Windows.Forms.ToolStripButton btn_close;

		// Token: 0x040002F4 RID: 756
		private global::DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;

		// Token: 0x040002F5 RID: 757
		private global::DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;

		// Token: 0x040002F6 RID: 758
		private global::DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;

		// Token: 0x040002F7 RID: 759
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x040002F8 RID: 760
		private global::System.Windows.Forms.Panel p_preview;

		// Token: 0x040002F9 RID: 761
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040002FA RID: 762
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040002FB RID: 763
		private global::System.Windows.Forms.ToolStripButton btn_addNewScreen;

		// Token: 0x040002FC RID: 764
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x040002FD RID: 765
		private global::System.Windows.Forms.ToolStripButton btn_editSelectedForm;

		// Token: 0x040002FE RID: 766
		private global::System.Windows.Forms.TreeView tv;

		// Token: 0x040002FF RID: 767
		private global::System.Windows.Forms.ImageList imageList3;

		// Token: 0x04000300 RID: 768
		private global::System.Windows.Forms.ToolStripButton btn_addNewForm;

		// Token: 0x04000301 RID: 769
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000302 RID: 770
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

		// Token: 0x04000303 RID: 771
		private global::System.Windows.Forms.ToolStripButton btn_editScreen;

		// Token: 0x04000304 RID: 772
		private global::System.Windows.Forms.ImageList imageList2;

		// Token: 0x04000305 RID: 773
		private global::System.Windows.Forms.ImageList imageList4;

		// Token: 0x04000306 RID: 774
		private global::System.Windows.Forms.ContextMenuStrip cm_form;

		// Token: 0x04000307 RID: 775
		private global::System.Windows.Forms.ToolStripMenuItem btn_editFormDetails;

		// Token: 0x04000308 RID: 776
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;

		// Token: 0x04000309 RID: 777
		private global::System.Windows.Forms.ToolStripMenuItem editfieldsOnFormToolStripMenuItem;

		// Token: 0x0400030A RID: 778
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;

		// Token: 0x0400030B RID: 779
		private global::System.Windows.Forms.ToolStripMenuItem toggleEnabledToolStripMenuItem;

		// Token: 0x0400030C RID: 780
		private global::System.Windows.Forms.MenuStrip menuStrip1;

		// Token: 0x0400030D RID: 781
		private global::System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

		// Token: 0x0400030E RID: 782
		private global::System.Windows.Forms.ToolStripMenuItem importScreenFromXmlToolStripMenuItem;

		// Token: 0x0400030F RID: 783
		private global::System.Windows.Forms.ToolStripMenuItem exportToXmlToolStripMenuItem;

		// Token: 0x04000310 RID: 784
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;

		// Token: 0x04000311 RID: 785
		private global::System.Windows.Forms.ToolStripMenuItem exportallFormsToXmlToolStripMenuItem;

		// Token: 0x04000312 RID: 786
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;

		// Token: 0x04000313 RID: 787
		private global::System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

		// Token: 0x04000314 RID: 788
		private global::System.Windows.Forms.ToolStripMenuItem MENU_deleteForm;

		// Token: 0x04000315 RID: 789
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

		// Token: 0x04000316 RID: 790
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;

		// Token: 0x04000317 RID: 791
		private global::System.Windows.Forms.ToolStripMenuItem copyAllFieldsOnThisFormToAnotherFormToolStripMenuItem;
	}
}
