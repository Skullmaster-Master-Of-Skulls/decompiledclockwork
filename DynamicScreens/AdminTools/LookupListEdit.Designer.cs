namespace DynamicScreens.AdminTools
{
	// Token: 0x02000048 RID: 72
	public partial class LookupListEdit : global::System.Windows.Forms.Form
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x00035070 File Offset: 0x00034070
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

		// Token: 0x06000402 RID: 1026 RVA: 0x000350AC File Offset: 0x000340AC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DynamicScreens.AdminTools.LookupListEdit));
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.listView1 = new global::System.Windows.Forms.ListView();
			this.ch_description = new global::System.Windows.Forms.ColumnHeader();
			this.ch_children = new global::System.Windows.Forms.ColumnHeader();
			this.p_childList = new global::System.Windows.Forms.Panel();
			this.cmb_lists = new global::AutoComboBox.AutoComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.separator_multiple = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn2_addSpecial = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.addprovincesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripDropDownButton2 = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn2_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn2_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.btn2_up = new global::System.Windows.Forms.ToolStripButton();
			this.btn2_down = new global::System.Windows.Forms.ToolStripButton();
			this.btn2_addItem = new global::System.Windows.Forms.ToolStripButton();
			this.btn2_addMultiple = new global::System.Windows.Forms.ToolStripButton();
			this.btn2_removeItem = new global::System.Windows.Forms.ToolStripButton();
			this.btn2_export = new global::System.Windows.Forms.ToolStripMenuItem();
			this.p_childList.SuspendLayout();
			this.toolStrip1.SuspendLayout();
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
			this.listView1.AccessibleDescription = "List items";
			this.listView1.AccessibleName = "List items";
			this.listView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.ch_description,
				this.ch_children
			});
			this.listView1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new global::System.Drawing.Point(0, 30);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new global::System.Drawing.Size(762, 403);
			this.listView1.TabIndex = 3;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = global::System.Windows.Forms.View.Details;
			this.listView1.SizeChanged += new global::System.EventHandler(this.listView1_SizeChanged);
			this.listView1.DoubleClick += new global::System.EventHandler(this.listView1_DoubleClick);
			this.listView1.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
			this.ch_description.Text = "Description";
			this.ch_description.Width = 467;
			this.ch_children.Text = "Children";
			this.ch_children.Width = 207;
			this.p_childList.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.p_childList.Controls.Add(this.cmb_lists);
			this.p_childList.Controls.Add(this.label1);
			this.p_childList.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.p_childList.Location = new global::System.Drawing.Point(0, 0);
			this.p_childList.Name = "p_childList";
			this.p_childList.Padding = new global::System.Windows.Forms.Padding(2);
			this.p_childList.Size = new global::System.Drawing.Size(762, 30);
			this.p_childList.TabIndex = 0;
			this.p_childList.Visible = false;
			this.cmb_lists.AccessibleDescription = "Child list";
			this.cmb_lists.AccessibleName = "Child list";
			this.cmb_lists.AccessibleRole = global::System.Windows.Forms.AccessibleRole.ComboBox;
			this.cmb_lists.AllowUserToEnterAnyText = true;
			this.cmb_lists.AutoCompleteEnabled = true;
			this.cmb_lists.ChildLookupGroupId = 0;
			this.cmb_lists.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.cmb_lists.GotoNextItemOnDoubleClick = false;
			this.cmb_lists.Location = new global::System.Drawing.Point(80, 2);
			this.cmb_lists.LookupGroupId = 0;
			this.cmb_lists.Name = "cmb_lists";
			this.cmb_lists.Size = new global::System.Drawing.Size(270, 24);
			this.cmb_lists.TabIndex = 2;
			this.cmb_lists.TryToSelectOnFocusLeave = true;
			this.label1.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new global::System.Drawing.Point(2, 2);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(78, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Child List:";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(24, 24);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn2_up,
				this.btn2_down,
				this.separator_multiple,
				this.btn2_addItem,
				this.btn2_addMultiple,
				this.toolStripSeparator1,
				this.btn2_removeItem,
				this.btn2_addSpecial,
				this.toolStripSeparator2,
				this.toolStripDropDownButton2,
				this.toolStripSeparator3,
				this.btn2_save,
				this.btn2_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 433);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(762, 31);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.TabStop = true;
			this.toolStrip1.Text = "toolStrip1";
			this.separator_multiple.Name = "separator_multiple";
			this.separator_multiple.Size = new global::System.Drawing.Size(6, 31);
			this.separator_multiple.Visible = false;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 31);
			this.btn2_addSpecial.AccessibleDescription = "Add special";
			this.btn2_addSpecial.AccessibleName = "Add special";
			this.btn2_addSpecial.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.addprovincesToolStripMenuItem
			});
			this.btn2_addSpecial.Image = global::DynamicScreens.Properties.Resources.bullet_ball_glass_blue;
			this.btn2_addSpecial.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn2_addSpecial.Name = "btn2_addSpecial";
			this.btn2_addSpecial.Size = new global::System.Drawing.Size(113, 28);
			this.btn2_addSpecial.Text = "A&dd special";
			this.addprovincesToolStripMenuItem.AccessibleDescription = "Add provinces";
			this.addprovincesToolStripMenuItem.AccessibleName = "Add provinces";
			this.addprovincesToolStripMenuItem.Name = "addprovincesToolStripMenuItem";
			this.addprovincesToolStripMenuItem.Size = new global::System.Drawing.Size(167, 22);
			this.addprovincesToolStripMenuItem.Text = "Add &provinces";
			this.addprovincesToolStripMenuItem.Click += new global::System.EventHandler(this.addprovincesToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(6, 31);
			this.toolStripDropDownButton2.AccessibleDescription = "Print";
			this.toolStripDropDownButton2.AccessibleName = "Print";
			this.toolStripDropDownButton2.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn2_export
			});
			this.toolStripDropDownButton2.Image = global::DynamicScreens.Properties.Resources.export1;
			this.toolStripDropDownButton2.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
			this.toolStripDropDownButton2.Size = new global::System.Drawing.Size(83, 28);
			this.toolStripDropDownButton2.Text = "E&xport";
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new global::System.Drawing.Size(6, 31);
			this.btn2_save.AccessibleDescription = "Save";
			this.btn2_save.AccessibleName = "Save";
			this.btn2_save.Image = global::DynamicScreens.Properties.Resources.check2;
			this.btn2_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn2_save.Name = "btn2_save";
			this.btn2_save.Size = new global::System.Drawing.Size(64, 28);
			this.btn2_save.Text = "&Save";
			this.btn2_save.Click += new global::System.EventHandler(this.btn2_save_Click);
			this.btn2_cancel.AccessibleDescription = "Cancel";
			this.btn2_cancel.AccessibleName = "Cancel";
			this.btn2_cancel.Image = global::DynamicScreens.Properties.Resources.delete2;
			this.btn2_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn2_cancel.Name = "btn2_cancel";
			this.btn2_cancel.Size = new global::System.Drawing.Size(76, 28);
			this.btn2_cancel.Text = "&Cancel";
			this.btn2_cancel.Click += new global::System.EventHandler(this.btn2_cancel_Click);
			this.btn2_up.AccessibleDescription = "Move selected item order up";
			this.btn2_up.AccessibleName = "Move selected item order up";
			this.btn2_up.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btn2_up.Image = global::DynamicScreens.Properties.Resources.arrow_up_blue;
			this.btn2_up.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn2_up.Name = "btn2_up";
			this.btn2_up.Size = new global::System.Drawing.Size(28, 28);
			this.btn2_up.Text = "toolStripButton1";
			this.btn2_up.Visible = false;
			this.btn2_up.Click += new global::System.EventHandler(this.btn2_up_Click);
			this.btn2_down.AccessibleDescription = "Move selected item order down";
			this.btn2_down.AccessibleName = "Move selected item order down";
			this.btn2_down.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btn2_down.Image = global::DynamicScreens.Properties.Resources.arrow_down_blue;
			this.btn2_down.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn2_down.Name = "btn2_down";
			this.btn2_down.Size = new global::System.Drawing.Size(28, 28);
			this.btn2_down.Text = "toolStripButton2";
			this.btn2_down.Visible = false;
			this.btn2_down.Click += new global::System.EventHandler(this.btn2_down_Click);
			this.btn2_addItem.AccessibleDescription = "Add item";
			this.btn2_addItem.AccessibleName = "Add item";
			this.btn2_addItem.Image = global::DynamicScreens.Properties.Resources.add;
			this.btn2_addItem.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn2_addItem.Name = "btn2_addItem";
			this.btn2_addItem.Size = new global::System.Drawing.Size(88, 28);
			this.btn2_addItem.Text = "&Add item";
			this.btn2_addItem.Click += new global::System.EventHandler(this.btn2_addItem_Click);
			this.btn2_addMultiple.AccessibleDescription = "Add multiple items";
			this.btn2_addMultiple.AccessibleName = "Add multiple items";
			this.btn2_addMultiple.Image = global::DynamicScreens.Properties.Resources.add;
			this.btn2_addMultiple.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn2_addMultiple.Name = "btn2_addMultiple";
			this.btn2_addMultiple.Size = new global::System.Drawing.Size(108, 28);
			this.btn2_addMultiple.Text = "Add &multiple";
			this.btn2_addMultiple.Click += new global::System.EventHandler(this.btn2_addMultiple_Click);
			this.btn2_removeItem.AccessibleDescription = "Remove item";
			this.btn2_removeItem.AccessibleName = "Remove item";
			this.btn2_removeItem.Image = global::DynamicScreens.Properties.Resources.data_forbidden;
			this.btn2_removeItem.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn2_removeItem.Name = "btn2_removeItem";
			this.btn2_removeItem.Size = new global::System.Drawing.Size(111, 28);
			this.btn2_removeItem.Text = "&Remove item";
			this.btn2_removeItem.Click += new global::System.EventHandler(this.btn2_removeItem_Click);
			this.btn2_export.AccessibleDescription = "Export list";
			this.btn2_export.AccessibleName = "Export list";
			this.btn2_export.Image = global::DynamicScreens.Properties.Resources.export1;
			this.btn2_export.Name = "btn2_export";
			this.btn2_export.Size = new global::System.Drawing.Size(160, 30);
			this.btn2_export.Text = "E&xport list";
			this.btn2_export.Click += new global::System.EventHandler(this.btn2_export_Click);
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.ClientSize = new global::System.Drawing.Size(762, 464);
			base.Controls.Add(this.listView1);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.p_childList);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "LookupListEdit";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Lookup List";
			base.Load += new global::System.EventHandler(this.LookupListEdit_Load);
			base.Closing += new global::System.ComponentModel.CancelEventHandler(this.LookupListEdit_Closing);
			this.p_childList.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002CF RID: 719
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x040002D0 RID: 720
		private global::System.Windows.Forms.ListView listView1;

		// Token: 0x040002D1 RID: 721
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040002D3 RID: 723
		private global::System.Windows.Forms.Panel p_childList;

		// Token: 0x040002D4 RID: 724
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040002D5 RID: 725
		private global::AutoComboBox.AutoComboBox cmb_lists;

		// Token: 0x040002D6 RID: 726
		private global::System.Windows.Forms.ColumnHeader ch_description;

		// Token: 0x040002D7 RID: 727
		private global::System.Windows.Forms.ColumnHeader ch_children;

		// Token: 0x040002D8 RID: 728
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040002D9 RID: 729
		private global::System.Windows.Forms.ToolStripButton btn2_addItem;

		// Token: 0x040002DA RID: 730
		private global::System.Windows.Forms.ToolStripButton btn2_addMultiple;

		// Token: 0x040002DB RID: 731
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x040002DC RID: 732
		private global::System.Windows.Forms.ToolStripButton btn2_removeItem;

		// Token: 0x040002DD RID: 733
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x040002DE RID: 734
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

		// Token: 0x040002DF RID: 735
		private global::System.Windows.Forms.ToolStripButton btn2_save;

		// Token: 0x040002E0 RID: 736
		private global::System.Windows.Forms.ToolStripButton btn2_cancel;

		// Token: 0x040002E1 RID: 737
		private global::System.Windows.Forms.ToolStripDropDownButton btn2_addSpecial;

		// Token: 0x040002E2 RID: 738
		private global::System.Windows.Forms.ToolStripMenuItem addprovincesToolStripMenuItem;

		// Token: 0x040002E3 RID: 739
		private global::System.Windows.Forms.ToolStripButton btn2_up;

		// Token: 0x040002E4 RID: 740
		private global::System.Windows.Forms.ToolStripButton btn2_down;

		// Token: 0x040002E5 RID: 741
		private global::System.Windows.Forms.ToolStripSeparator separator_multiple;

		// Token: 0x040002E6 RID: 742
		private global::System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;

		// Token: 0x040002E7 RID: 743
		private global::System.Windows.Forms.ToolStripMenuItem btn2_export;
	}
}
