namespace ImportExportClassLibrary
{
	// Token: 0x02000042 RID: 66
	public partial class ImportManager : global::System.Windows.Forms.Form
	{
		// Token: 0x0600026E RID: 622 RVA: 0x00018B2A File Offset: 0x00017B2A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00018B4C File Offset: 0x00017B4C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::ImportExportClassLibrary.ImportManager));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.lv = new global::AutoComboBox.ListViewEx();
			this.cm_lv = new global::System.Windows.Forms.ContextMenu();
			this.MENU_selectAllItems = new global::System.Windows.Forms.MenuItem();
			this.menuItem2 = new global::System.Windows.Forms.MenuItem();
			this.MENU_cmlv_ignoreThisItem = new global::System.Windows.Forms.MenuItem();
			this.MENU_cm_lv_importSpacer = new global::System.Windows.Forms.MenuItem();
			this.MENU_cm_lv_importThisItem = new global::System.Windows.Forms.MenuItem();
			this.MENU_cm_lv_problemSpacer = new global::System.Windows.Forms.MenuItem();
			this.MENU_cm_lv_problem = new global::System.Windows.Forms.MenuItem();
			this.menuItem1 = new global::System.Windows.Forms.MenuItem();
			this.MENU_discard = new global::System.Windows.Forms.MenuItem();
			this.il_problems = new global::System.Windows.Forms.ImageList(this.components);
			this.imageList2 = new global::System.Windows.Forms.ImageList(this.components);
			this.label1 = new global::System.Windows.Forms.Label();
			this.toolTip1 = new global::System.Windows.Forms.ToolTip(this.components);
			this.lbl_msg = new global::System.Windows.Forms.Label();
			this.cm_uniqueValuesDataGrid = new global::System.Windows.Forms.ContextMenu();
			this.MENU_DATAGRID_changeThisValue = new global::System.Windows.Forms.MenuItem();
			this.MENU_DATAGRID_removeThisItem = new global::System.Windows.Forms.MenuItem();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_uniqueColumnValues = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.explorerBar1 = new global::DevComponents.DotNetBar.ExplorerBar();
			this.explorerBarGroupItem1 = new global::DevComponents.DotNetBar.ExplorerBarGroupItem();
			this.btn_fixAllProblems = new global::DevComponents.DotNetBar.ButtonItem();
			this.btn_fixSelectedProblem = new global::DevComponents.DotNetBar.ButtonItem();
			this.btn_ignoreSelectedProblem = new global::DevComponents.DotNetBar.ButtonItem();
			this.btn_ignoreAllProblems = new global::DevComponents.DotNetBar.ButtonItem();
			this.explorerBarGroupItem2 = new global::DevComponents.DotNetBar.ExplorerBarGroupItem();
			this.btn_printList = new global::DevComponents.DotNetBar.ButtonItem();
			this.btn_printPreviewList = new global::DevComponents.DotNetBar.ButtonItem();
			this.btn_exportList = new global::DevComponents.DotNetBar.ButtonItem();
			this.btn_emailSelecteditems = new global::DevComponents.DotNetBar.ButtonItem();
			this.btn_exportToTemplate = new global::DevComponents.DotNetBar.ButtonItem();
			this.statusStrip1 = new global::System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1.SuspendLayout();
			this.explorerBar1.BeginInit();
			this.statusStrip1.SuspendLayout();
			base.SuspendLayout();
			this.imageList1.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = global::System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "");
			this.imageList1.Images.SetKeyName(1, "");
			this.imageList1.Images.SetKeyName(2, "");
			this.imageList1.Images.SetKeyName(3, "");
			this.imageList1.Images.SetKeyName(4, "");
			this.imageList1.Images.SetKeyName(5, "");
			this.imageList1.Images.SetKeyName(6, "");
			this.imageList1.Images.SetKeyName(7, "");
			this.imageList1.Images.SetKeyName(8, "");
			this.imageList1.Images.SetKeyName(9, "");
			this.imageList1.Images.SetKeyName(10, "");
			this.imageList1.Images.SetKeyName(11, "");
			this.imageList1.Images.SetKeyName(12, "");
			this.imageList1.Images.SetKeyName(13, "");
			this.imageList1.Images.SetKeyName(14, "");
			this.imageList1.Images.SetKeyName(15, "");
			this.imageList1.Images.SetKeyName(16, "");
			this.imageList1.Images.SetKeyName(17, "");
			this.imageList1.Images.SetKeyName(18, "");
			this.imageList1.Images.SetKeyName(19, "");
			this.imageList1.Images.SetKeyName(20, "");
			this.imageList1.Images.SetKeyName(21, "");
			this.lv.BackColourSelected = global::System.Drawing.Color.LightBlue;
			this.lv.ContextMenu = this.cm_lv;
			this.lv.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lv.DrawMode = global::System.Windows.Forms.DrawMode.Normal;
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.HideSelection = false;
			this.lv.ItemHeight = 16;
			this.lv.Location = new global::System.Drawing.Point(229, 59);
			this.lv.Name = "lv";
			this.lv.Size = new global::System.Drawing.Size(513, 364);
			this.lv.SmallImageList = this.il_problems;
			this.lv.TabIndex = 2;
			this.lv.Tag2 = null;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = global::System.Windows.Forms.View.Details;
			this.lv.ColumnClick += new global::System.Windows.Forms.ColumnClickEventHandler(this.lv_ColumnClick);
			this.lv.SelectedIndexChanged += new global::System.EventHandler(this.lv_SelectedIndexChanged);
			this.lv.DoubleClick += new global::System.EventHandler(this.lv_DoubleClick);
			this.cm_lv.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.MENU_selectAllItems,
				this.menuItem2,
				this.MENU_cmlv_ignoreThisItem,
				this.MENU_cm_lv_importSpacer,
				this.MENU_cm_lv_importThisItem,
				this.MENU_cm_lv_problemSpacer,
				this.MENU_cm_lv_problem,
				this.menuItem1,
				this.MENU_discard
			});
			this.cm_lv.Popup += new global::System.EventHandler(this.cm_lv_Popup);
			this.MENU_selectAllItems.Index = 0;
			this.MENU_selectAllItems.Text = "Select &All Items";
			this.MENU_selectAllItems.Click += new global::System.EventHandler(this.MENU_selectAllItems_Click);
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			this.MENU_cmlv_ignoreThisItem.Index = 2;
			this.MENU_cmlv_ignoreThisItem.Text = "Ignore This Item";
			this.MENU_cmlv_ignoreThisItem.Click += new global::System.EventHandler(this.MENU_cmlv_ignoreThisItem_Click);
			this.MENU_cm_lv_importSpacer.Index = 3;
			this.MENU_cm_lv_importSpacer.Text = "-";
			this.MENU_cm_lv_importSpacer.Visible = false;
			this.MENU_cm_lv_importThisItem.Index = 4;
			this.MENU_cm_lv_importThisItem.Text = "Import This Item";
			this.MENU_cm_lv_importThisItem.Visible = false;
			this.MENU_cm_lv_problemSpacer.Index = 5;
			this.MENU_cm_lv_problemSpacer.Text = "-";
			this.MENU_cm_lv_problem.Enabled = false;
			this.MENU_cm_lv_problem.Index = 6;
			this.MENU_cm_lv_problem.Text = "Problem: xxx";
			this.menuItem1.Index = 7;
			this.menuItem1.Text = "-";
			this.MENU_discard.Index = 8;
			this.MENU_discard.Text = "Discard This Item";
			this.MENU_discard.Click += new global::System.EventHandler(this.MENU_discard_Click);
			this.il_problems.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("il_problems.ImageStream");
			this.il_problems.TransparentColor = global::System.Drawing.Color.Transparent;
			this.il_problems.Images.SetKeyName(0, "");
			this.il_problems.Images.SetKeyName(1, "");
			this.il_problems.Images.SetKeyName(2, "");
			this.il_problems.Images.SetKeyName(3, "");
			this.il_problems.Images.SetKeyName(4, "");
			this.il_problems.Images.SetKeyName(5, "");
			this.il_problems.Images.SetKeyName(6, "");
			this.il_problems.Images.SetKeyName(7, "");
			this.il_problems.Images.SetKeyName(8, "");
			this.il_problems.Images.SetKeyName(9, "");
			this.il_problems.Images.SetKeyName(10, "");
			this.il_problems.Images.SetKeyName(11, "");
			this.il_problems.Images.SetKeyName(12, "");
			this.il_problems.Images.SetKeyName(13, "");
			this.il_problems.Images.SetKeyName(14, "");
			this.il_problems.Images.SetKeyName(15, "");
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
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(229, 22);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(513, 37);
			this.label1.TabIndex = 4;
			this.label1.Text = componentResourceManager.GetString("label1.Text");
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.BottomLeft;
			this.toolTip1.SetToolTip(this.label1, componentResourceManager.GetString("label1.ToolTip"));
			this.label1.Click += new global::System.EventHandler(this.label1_Click);
			this.lbl_msg.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.lbl_msg.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_msg.Location = new global::System.Drawing.Point(229, 423);
			this.lbl_msg.Name = "lbl_msg";
			this.lbl_msg.Size = new global::System.Drawing.Size(513, 36);
			this.lbl_msg.TabIndex = 6;
			this.lbl_msg.Text = "label2";
			this.lbl_msg.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl_msg.Visible = false;
			this.cm_uniqueValuesDataGrid.MenuItems.AddRange(new global::System.Windows.Forms.MenuItem[]
			{
				this.MENU_DATAGRID_changeThisValue,
				this.MENU_DATAGRID_removeThisItem
			});
			this.MENU_DATAGRID_changeThisValue.Index = 0;
			this.MENU_DATAGRID_changeThisValue.Text = "&Change this value (affects all items with this value)";
			this.MENU_DATAGRID_changeThisValue.Click += new global::System.EventHandler(this.MENU_DATAGRID_changeThisValue_Click);
			this.MENU_DATAGRID_removeThisItem.Index = 1;
			this.MENU_DATAGRID_removeThisItem.Text = "&Remove this item from the list (affects all items with this value)";
			this.MENU_DATAGRID_removeThisItem.Click += new global::System.EventHandler(this.MENU_DATAGRID_removeThisItem_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_uniqueColumnValues,
				this.toolStripSeparator1,
				this.btn_save,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(229, 459);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(513, 39);
			this.toolStrip1.TabIndex = 7;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_uniqueColumnValues.Image = global::ImportExportClassLibrary.Properties.Resources.table_selection_column;
			this.btn_uniqueColumnValues.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_uniqueColumnValues.Name = "btn_uniqueColumnValues";
			this.btn_uniqueColumnValues.Size = new global::System.Drawing.Size(194, 36);
			this.btn_uniqueColumnValues.Text = "&Unique column values";
			this.btn_uniqueColumnValues.Click += new global::System.EventHandler(this.btn_uniqueColumnValues_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.btn_save.Image = global::ImportExportClassLibrary.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(143, 36);
			this.btn_save.Text = "&Save changes";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_cancel.Image = global::ImportExportClassLibrary.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.explorerBar1.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ToolBar;
			this.explorerBar1.AntiAlias = true;
			this.explorerBar1.BackColor = global::System.Drawing.SystemColors.Control;
			this.explorerBar1.BackgroundStyle.BackColor1.ColorSchemePart = 55;
			this.explorerBar1.BackgroundStyle.BackColor2.ColorSchemePart = 56;
			this.explorerBar1.BackgroundStyle.GradientAngle = 90;
			this.explorerBar1.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.explorerBar1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.explorerBar1.GroupImages = null;
			this.explorerBar1.Groups.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.explorerBarGroupItem1,
				this.explorerBarGroupItem2
			});
			this.explorerBar1.Images = this.imageList2;
			this.explorerBar1.Location = new global::System.Drawing.Point(0, 0);
			this.explorerBar1.Name = "explorerBar1";
			this.explorerBar1.Size = new global::System.Drawing.Size(229, 498);
			this.explorerBar1.TabIndex = 9;
			this.explorerBar1.Text = "explorerBar1";
			this.explorerBar1.ThemeAware = true;
			this.explorerBarGroupItem1.BackgroundStyle.BackColor1.ColorSchemePart = 0;
			this.explorerBarGroupItem1.BackgroundStyle.Border = 1;
			this.explorerBarGroupItem1.BackgroundStyle.BorderColor.Color = global::System.Drawing.SystemColors.Window;
			this.explorerBarGroupItem1.ExpandBackColor = global::System.Drawing.SystemColors.Window;
			this.explorerBarGroupItem1.ExpandBorderColor = global::System.Drawing.SystemColors.InactiveCaption;
			this.explorerBarGroupItem1.Expanded = true;
			this.explorerBarGroupItem1.ExpandForeColor = global::System.Drawing.SystemColors.Highlight;
			this.explorerBarGroupItem1.ExpandHotBackColor = global::System.Drawing.SystemColors.Window;
			this.explorerBarGroupItem1.ExpandHotBorderColor = global::System.Drawing.SystemColors.ActiveCaption;
			this.explorerBarGroupItem1.HeaderHotStyle.BackColor1.Color = global::System.Drawing.SystemColors.Window;
			this.explorerBarGroupItem1.HeaderHotStyle.BackColor2.Color = global::System.Drawing.SystemColors.InactiveCaption;
			this.explorerBarGroupItem1.HeaderHotStyle.Font = new global::System.Drawing.Font("Tahoma", 11f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.World);
			this.explorerBarGroupItem1.HeaderHotStyle.ForeColor.Color = global::System.Drawing.SystemColors.ActiveCaption;
			this.explorerBarGroupItem1.HeaderStyle.BackColor1.Color = global::System.Drawing.SystemColors.Window;
			this.explorerBarGroupItem1.HeaderStyle.BackColor2.ColorSchemePart = 1;
			this.explorerBarGroupItem1.HeaderStyle.Font = new global::System.Drawing.Font("Tahoma", 11f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.World);
			this.explorerBarGroupItem1.HeaderStyle.ForeColor.Color = global::System.Drawing.SystemColors.ControlText;
			this.explorerBarGroupItem1.Name = "explorerBarGroupItem1";
			this.explorerBarGroupItem1.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btn_fixAllProblems,
				this.btn_fixSelectedProblem,
				this.btn_ignoreSelectedProblem,
				this.btn_ignoreAllProblems
			});
			this.explorerBarGroupItem1.Text = "Problems";
			this.explorerBarGroupItem1.ThemeAware = true;
			this.btn_fixAllProblems.ButtonStyle = 2;
			this.btn_fixAllProblems.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.btn_fixAllProblems.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_fixAllProblems.HotFontUnderline = true;
			this.btn_fixAllProblems.HotForeColor = global::System.Drawing.SystemColors.ControlDark;
			this.btn_fixAllProblems.HotTrackingStyle = 2;
			this.btn_fixAllProblems.ImageIndex = 12;
			this.btn_fixAllProblems.Name = "btn_fixAllProblems";
			this.btn_fixAllProblems.Text = "Fix &all problems";
			this.btn_fixAllProblems.Click += new global::System.EventHandler(this.btn_fixAllProblems_Click);
			this.btn_fixSelectedProblem.ButtonStyle = 2;
			this.btn_fixSelectedProblem.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.btn_fixSelectedProblem.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_fixSelectedProblem.HotFontUnderline = true;
			this.btn_fixSelectedProblem.HotForeColor = global::System.Drawing.SystemColors.ControlDark;
			this.btn_fixSelectedProblem.HotTrackingStyle = 2;
			this.btn_fixSelectedProblem.ImageIndex = 13;
			this.btn_fixSelectedProblem.Name = "btn_fixSelectedProblem";
			this.btn_fixSelectedProblem.Text = "&Fix selected problem";
			this.btn_fixSelectedProblem.Click += new global::System.EventHandler(this.btn_fixSelectedProblem_Click);
			this.btn_ignoreSelectedProblem.ButtonStyle = 2;
			this.btn_ignoreSelectedProblem.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.btn_ignoreSelectedProblem.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_ignoreSelectedProblem.HotFontUnderline = true;
			this.btn_ignoreSelectedProblem.HotForeColor = global::System.Drawing.SystemColors.ControlDark;
			this.btn_ignoreSelectedProblem.HotTrackingStyle = 2;
			this.btn_ignoreSelectedProblem.ImageIndex = 8;
			this.btn_ignoreSelectedProblem.Name = "btn_ignoreSelectedProblem";
			this.btn_ignoreSelectedProblem.Text = "I&gnore selected problem";
			this.btn_ignoreSelectedProblem.Click += new global::System.EventHandler(this.btn_ignoreSelectedProblem_Click);
			this.btn_ignoreAllProblems.ButtonStyle = 2;
			this.btn_ignoreAllProblems.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.btn_ignoreAllProblems.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_ignoreAllProblems.HotFontUnderline = true;
			this.btn_ignoreAllProblems.HotForeColor = global::System.Drawing.SystemColors.ControlDark;
			this.btn_ignoreAllProblems.HotTrackingStyle = 2;
			this.btn_ignoreAllProblems.ImageIndex = 8;
			this.btn_ignoreAllProblems.Name = "btn_ignoreAllProblems";
			this.btn_ignoreAllProblems.Text = "&Ignore all problems";
			this.btn_ignoreAllProblems.Click += new global::System.EventHandler(this.btn_ignoreAllProblems_Click);
			this.explorerBarGroupItem2.BackgroundStyle.BackColor1.ColorSchemePart = 0;
			this.explorerBarGroupItem2.BackgroundStyle.Border = 1;
			this.explorerBarGroupItem2.BackgroundStyle.BorderColor.Color = global::System.Drawing.SystemColors.Window;
			this.explorerBarGroupItem2.ExpandBackColor = global::System.Drawing.SystemColors.Window;
			this.explorerBarGroupItem2.ExpandBorderColor = global::System.Drawing.SystemColors.InactiveCaption;
			this.explorerBarGroupItem2.Expanded = true;
			this.explorerBarGroupItem2.ExpandForeColor = global::System.Drawing.SystemColors.Highlight;
			this.explorerBarGroupItem2.ExpandHotBackColor = global::System.Drawing.SystemColors.Window;
			this.explorerBarGroupItem2.ExpandHotBorderColor = global::System.Drawing.SystemColors.ActiveCaption;
			this.explorerBarGroupItem2.HeaderHotStyle.BackColor1.Color = global::System.Drawing.SystemColors.Window;
			this.explorerBarGroupItem2.HeaderHotStyle.BackColor2.Color = global::System.Drawing.SystemColors.InactiveCaption;
			this.explorerBarGroupItem2.HeaderHotStyle.Font = new global::System.Drawing.Font("Tahoma", 11f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.World);
			this.explorerBarGroupItem2.HeaderHotStyle.ForeColor.Color = global::System.Drawing.SystemColors.ActiveCaption;
			this.explorerBarGroupItem2.HeaderStyle.BackColor1.Color = global::System.Drawing.SystemColors.Window;
			this.explorerBarGroupItem2.HeaderStyle.BackColor2.ColorSchemePart = 1;
			this.explorerBarGroupItem2.HeaderStyle.Font = new global::System.Drawing.Font("Tahoma", 11f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.World);
			this.explorerBarGroupItem2.HeaderStyle.ForeColor.Color = global::System.Drawing.SystemColors.ControlText;
			this.explorerBarGroupItem2.Name = "explorerBarGroupItem2";
			this.explorerBarGroupItem2.SubItems.AddRange(new global::DevComponents.DotNetBar.BaseItem[]
			{
				this.btn_printList,
				this.btn_printPreviewList,
				this.btn_exportList,
				this.btn_emailSelecteditems,
				this.btn_exportToTemplate
			});
			this.explorerBarGroupItem2.Text = "List Options";
			this.explorerBarGroupItem2.ThemeAware = true;
			this.btn_printList.ButtonStyle = 2;
			this.btn_printList.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.btn_printList.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_printList.HotFontUnderline = true;
			this.btn_printList.HotForeColor = global::System.Drawing.SystemColors.ControlDark;
			this.btn_printList.HotTrackingStyle = 2;
			this.btn_printList.ImageIndex = 14;
			this.btn_printList.Name = "btn_printList";
			this.btn_printList.Text = "&Print list";
			this.btn_printList.Click += new global::System.EventHandler(this.btn_printList_Click);
			this.btn_printPreviewList.ButtonStyle = 2;
			this.btn_printPreviewList.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.btn_printPreviewList.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_printPreviewList.HotFontUnderline = true;
			this.btn_printPreviewList.HotForeColor = global::System.Drawing.SystemColors.ControlDark;
			this.btn_printPreviewList.HotTrackingStyle = 2;
			this.btn_printPreviewList.ImageIndex = 16;
			this.btn_printPreviewList.Name = "btn_printPreviewList";
			this.btn_printPreviewList.Text = "Print p&review list";
			this.btn_printPreviewList.Click += new global::System.EventHandler(this.btn_printPreviewList_Click);
			this.btn_exportList.ButtonStyle = 2;
			this.btn_exportList.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.btn_exportList.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_exportList.HotFontUnderline = true;
			this.btn_exportList.HotForeColor = global::System.Drawing.SystemColors.ControlDark;
			this.btn_exportList.HotTrackingStyle = 2;
			this.btn_exportList.ImageIndex = 18;
			this.btn_exportList.Name = "btn_exportList";
			this.btn_exportList.Text = "E&xport list";
			this.btn_exportList.Click += new global::System.EventHandler(this.btn_exportList_Click);
			this.btn_emailSelecteditems.ButtonStyle = 2;
			this.btn_emailSelecteditems.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.btn_emailSelecteditems.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_emailSelecteditems.HotFontUnderline = true;
			this.btn_emailSelecteditems.HotForeColor = global::System.Drawing.SystemColors.ControlDark;
			this.btn_emailSelecteditems.HotTrackingStyle = 2;
			this.btn_emailSelecteditems.ImageIndex = 20;
			this.btn_emailSelecteditems.Name = "btn_emailSelecteditems";
			this.btn_emailSelecteditems.Text = "&Email selected items";
			this.btn_emailSelecteditems.Click += new global::System.EventHandler(this.btn_emailSelecteditems_Click);
			this.btn_exportToTemplate.ButtonStyle = 2;
			this.btn_exportToTemplate.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.btn_exportToTemplate.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.btn_exportToTemplate.HotFontUnderline = true;
			this.btn_exportToTemplate.HotForeColor = global::System.Drawing.SystemColors.ControlDark;
			this.btn_exportToTemplate.HotTrackingStyle = 2;
			this.btn_exportToTemplate.ImageIndex = 22;
			this.btn_exportToTemplate.Name = "btn_exportToTemplate";
			this.btn_exportToTemplate.Text = "Export selected to &template";
			this.btn_exportToTemplate.Click += new global::System.EventHandler(this.btn_exportToTemplate_Click);
			this.statusStrip1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.statusStrip1.Font = new global::System.Drawing.Font("Tahoma", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.statusStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripStatusLabel1
			});
			this.statusStrip1.Location = new global::System.Drawing.Point(229, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new global::System.Drawing.Size(513, 22);
			this.statusStrip1.TabIndex = 10;
			this.statusStrip1.Text = "statusStrip1";
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new global::System.Drawing.Size(467, 17);
			this.toolStripStatusLabel1.Spring = true;
			this.toolStripStatusLabel1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripStatusLabel1.Click += new global::System.EventHandler(this.toolStripStatusLabel1_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.ClientSize = new global::System.Drawing.Size(742, 498);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.lbl_msg);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.explorerBar1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "ImportManager";
			this.Text = "Import Manager";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.ImportManager_Load);
			base.Closing += new global::System.ComponentModel.CancelEventHandler(this.ImportManager_Closing);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.explorerBar1.EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400013F RID: 319
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x04000140 RID: 320
		public global::AutoComboBox.ListViewEx lv;

		// Token: 0x04000141 RID: 321
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000142 RID: 322
		private global::System.Windows.Forms.ToolTip toolTip1;

		// Token: 0x04000143 RID: 323
		private global::System.Windows.Forms.ImageList il_problems;

		// Token: 0x04000144 RID: 324
		private global::System.Windows.Forms.ContextMenu cm_lv;

		// Token: 0x04000145 RID: 325
		private global::System.Windows.Forms.MenuItem MENU_cmlv_ignoreThisItem;

		// Token: 0x04000146 RID: 326
		private global::System.Windows.Forms.MenuItem MENU_cm_lv_problem;

		// Token: 0x04000147 RID: 327
		private global::System.Windows.Forms.MenuItem MENU_cm_lv_importThisItem;

		// Token: 0x04000148 RID: 328
		private global::System.Windows.Forms.MenuItem MENU_cm_lv_problemSpacer;

		// Token: 0x04000149 RID: 329
		private global::System.Windows.Forms.MenuItem MENU_cm_lv_importSpacer;

		// Token: 0x0400014A RID: 330
		private global::System.Windows.Forms.ImageList imageList2;

		// Token: 0x0400014B RID: 331
		private global::System.Windows.Forms.MenuItem MENU_discard;

		// Token: 0x0400014C RID: 332
		private global::System.Windows.Forms.MenuItem MENU_selectAllItems;

		// Token: 0x0400014D RID: 333
		private global::System.Windows.Forms.MenuItem menuItem2;

		// Token: 0x0400014E RID: 334
		private global::System.Windows.Forms.MenuItem menuItem1;

		// Token: 0x0400014F RID: 335
		private global::System.Windows.Forms.Label lbl_msg;

		// Token: 0x04000150 RID: 336
		private global::System.Windows.Forms.ContextMenu cm_uniqueValuesDataGrid;

		// Token: 0x04000151 RID: 337
		private global::System.Windows.Forms.MenuItem MENU_DATAGRID_changeThisValue;

		// Token: 0x04000152 RID: 338
		private global::System.Windows.Forms.MenuItem MENU_DATAGRID_removeThisItem;

		// Token: 0x04000153 RID: 339
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000154 RID: 340
		private global::System.Windows.Forms.ToolStripButton btn_uniqueColumnValues;

		// Token: 0x04000155 RID: 341
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000156 RID: 342
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x04000157 RID: 343
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x04000158 RID: 344
		private global::DevComponents.DotNetBar.ExplorerBar explorerBar1;

		// Token: 0x04000159 RID: 345
		private global::DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem1;

		// Token: 0x0400015A RID: 346
		private global::DevComponents.DotNetBar.ButtonItem btn_fixAllProblems;

		// Token: 0x0400015B RID: 347
		private global::DevComponents.DotNetBar.ButtonItem btn_fixSelectedProblem;

		// Token: 0x0400015C RID: 348
		private global::DevComponents.DotNetBar.ButtonItem btn_ignoreSelectedProblem;

		// Token: 0x0400015D RID: 349
		private global::DevComponents.DotNetBar.ButtonItem btn_ignoreAllProblems;

		// Token: 0x0400015E RID: 350
		private global::DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem2;

		// Token: 0x0400015F RID: 351
		private global::DevComponents.DotNetBar.ButtonItem btn_printList;

		// Token: 0x04000160 RID: 352
		private global::DevComponents.DotNetBar.ButtonItem btn_printPreviewList;

		// Token: 0x04000161 RID: 353
		private global::DevComponents.DotNetBar.ButtonItem btn_exportList;

		// Token: 0x04000162 RID: 354
		private global::DevComponents.DotNetBar.ButtonItem btn_emailSelecteditems;

		// Token: 0x04000163 RID: 355
		private global::DevComponents.DotNetBar.ButtonItem btn_exportToTemplate;

		// Token: 0x04000164 RID: 356
		private global::System.Windows.Forms.StatusStrip statusStrip1;

		// Token: 0x04000165 RID: 357
		private global::System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

		// Token: 0x04000166 RID: 358
		private global::System.ComponentModel.IContainer components;
	}
}
