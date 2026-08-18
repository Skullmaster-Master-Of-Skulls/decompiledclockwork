namespace AutoComboBox
{
	// Token: 0x02000068 RID: 104
	public partial class InputMultipleOrderedItems : global::System.Windows.Forms.Form
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0001DC1C File Offset: 0x0001CC1C
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

		// Token: 0x060003B9 RID: 953 RVA: 0x0001DC58 File Offset: 0x0001CC58
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputMultipleOrderedItems));
			this.lv_from = new global::AutoComboBox.ListViewEx();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.lv_to = new global::AutoComboBox.ListViewEx();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.panel4 = new global::System.Windows.Forms.Panel();
			this.btn_moveDown = new global::System.Windows.Forms.Button();
			this.btn_moveUp = new global::System.Windows.Forms.Button();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.btn_moveAllLeft = new global::System.Windows.Forms.Button();
			this.btn_moveLeft = new global::System.Windows.Forms.Button();
			this.btn_moveAllRight = new global::System.Windows.Forms.Button();
			this.btn_moveRight = new global::System.Windows.Forms.Button();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.imageList1 = new global::System.Windows.Forms.ImageList(this.components);
			this.lbl_caption = new global::System.Windows.Forms.Label();
			this.chk_ascending = new global::System.Windows.Forms.CheckBox();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.lv_from.BackColourSelected = global::System.Drawing.Color.LightBlue;
			this.lv_from.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1
			});
			this.lv_from.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.lv_from.DrawMode = global::System.Windows.Forms.DrawMode.Normal;
			this.lv_from.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lv_from.FullRowSelect = true;
			this.lv_from.GridLines = true;
			this.lv_from.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.None;
			this.lv_from.HideSelection = false;
			this.lv_from.ItemHeight = 16;
			this.lv_from.Location = new global::System.Drawing.Point(0, 56);
			this.lv_from.Name = "lv_from";
			this.lv_from.Size = new global::System.Drawing.Size(224, 263);
			this.lv_from.TabIndex = 1;
			this.lv_from.Tag2 = null;
			this.lv_from.UseCompatibleStateImageBehavior = false;
			this.lv_from.View = global::System.Windows.Forms.View.Details;
			this.columnHeader1.Width = 191;
			this.lv_to.BackColourSelected = global::System.Drawing.Color.LightBlue;
			this.lv_to.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader2
			});
			this.lv_to.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.lv_to.DrawMode = global::System.Windows.Forms.DrawMode.Normal;
			this.lv_to.FullRowSelect = true;
			this.lv_to.GridLines = true;
			this.lv_to.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.None;
			this.lv_to.HideSelection = false;
			this.lv_to.ItemHeight = 16;
			this.lv_to.Location = new global::System.Drawing.Point(288, 56);
			this.lv_to.Name = "lv_to";
			this.lv_to.Size = new global::System.Drawing.Size(224, 263);
			this.lv_to.TabIndex = 2;
			this.lv_to.Tag2 = null;
			this.lv_to.UseCompatibleStateImageBehavior = false;
			this.lv_to.View = global::System.Windows.Forms.View.Details;
			this.columnHeader2.Width = 191;
			this.panel4.Controls.Add(this.btn_moveDown);
			this.panel4.Controls.Add(this.btn_moveUp);
			this.panel4.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.panel4.Location = new global::System.Drawing.Point(512, 56);
			this.panel4.Name = "panel4";
			this.panel4.Size = new global::System.Drawing.Size(64, 263);
			this.panel4.TabIndex = 33;
			this.btn_moveDown.Font = new global::System.Drawing.Font("Tahoma", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_moveDown.Location = new global::System.Drawing.Point(8, 80);
			this.btn_moveDown.Name = "btn_moveDown";
			this.btn_moveDown.Size = new global::System.Drawing.Size(48, 40);
			this.btn_moveDown.TabIndex = 1;
			this.btn_moveDown.Text = "move down";
			this.btn_moveDown.Click += new global::System.EventHandler(this.btn_moveDown_Click);
			this.btn_moveUp.Font = new global::System.Drawing.Font("Tahoma", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_moveUp.Location = new global::System.Drawing.Point(8, 32);
			this.btn_moveUp.Name = "btn_moveUp";
			this.btn_moveUp.Size = new global::System.Drawing.Size(48, 40);
			this.btn_moveUp.TabIndex = 0;
			this.btn_moveUp.Text = "move up";
			this.btn_moveUp.Click += new global::System.EventHandler(this.btn_moveUp_Click);
			this.panel2.Controls.Add(this.btn_moveAllLeft);
			this.panel2.Controls.Add(this.btn_moveLeft);
			this.panel2.Controls.Add(this.btn_moveAllRight);
			this.panel2.Controls.Add(this.btn_moveRight);
			this.panel2.Dock = global::System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new global::System.Drawing.Point(224, 56);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(64, 263);
			this.panel2.TabIndex = 32;
			this.btn_moveAllLeft.Font = new global::System.Drawing.Font("Tahoma", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_moveAllLeft.Location = new global::System.Drawing.Point(8, 224);
			this.btn_moveAllLeft.Name = "btn_moveAllLeft";
			this.btn_moveAllLeft.Size = new global::System.Drawing.Size(48, 40);
			this.btn_moveAllLeft.TabIndex = 3;
			this.btn_moveAllLeft.Text = "<<";
			this.btn_moveAllLeft.Click += new global::System.EventHandler(this.btn_moveAllLeft_Click);
			this.btn_moveLeft.Font = new global::System.Drawing.Font("Tahoma", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_moveLeft.Location = new global::System.Drawing.Point(8, 80);
			this.btn_moveLeft.Name = "btn_moveLeft";
			this.btn_moveLeft.Size = new global::System.Drawing.Size(48, 40);
			this.btn_moveLeft.TabIndex = 2;
			this.btn_moveLeft.Text = "<";
			this.btn_moveLeft.Click += new global::System.EventHandler(this.btn_moveLeft_Click);
			this.btn_moveAllRight.Font = new global::System.Drawing.Font("Tahoma", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_moveAllRight.Location = new global::System.Drawing.Point(8, 176);
			this.btn_moveAllRight.Name = "btn_moveAllRight";
			this.btn_moveAllRight.Size = new global::System.Drawing.Size(48, 40);
			this.btn_moveAllRight.TabIndex = 1;
			this.btn_moveAllRight.Text = ">>";
			this.btn_moveAllRight.Click += new global::System.EventHandler(this.btn_moveAllRight_Click);
			this.btn_moveRight.Font = new global::System.Drawing.Font("Tahoma", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_moveRight.Location = new global::System.Drawing.Point(8, 32);
			this.btn_moveRight.Name = "btn_moveRight";
			this.btn_moveRight.Size = new global::System.Drawing.Size(48, 40);
			this.btn_moveRight.TabIndex = 0;
			this.btn_moveRight.Text = ">";
			this.btn_moveRight.Click += new global::System.EventHandler(this.btn_moveRight_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 319);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(592, 39);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_ok.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
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
			this.lbl_caption.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.lbl_caption.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_caption.Location = new global::System.Drawing.Point(0, 0);
			this.lbl_caption.Name = "lbl_caption";
			this.lbl_caption.Size = new global::System.Drawing.Size(592, 32);
			this.lbl_caption.TabIndex = 35;
			this.lbl_caption.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.chk_ascending.Checked = true;
			this.chk_ascending.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.chk_ascending.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.chk_ascending.Location = new global::System.Drawing.Point(0, 32);
			this.chk_ascending.Name = "chk_ascending";
			this.chk_ascending.Size = new global::System.Drawing.Size(592, 24);
			this.chk_ascending.TabIndex = 36;
			this.chk_ascending.Text = "Sort ascending";
			this.chk_ascending.Visible = false;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 15);
			base.ClientSize = new global::System.Drawing.Size(592, 358);
			base.Controls.Add(this.panel4);
			base.Controls.Add(this.lv_to);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.lv_from);
			base.Controls.Add(this.chk_ascending);
			base.Controls.Add(this.lbl_caption);
			base.Controls.Add(this.toolStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "InputMultipleOrderedItems";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Item selector";
			base.Load += new global::System.EventHandler(this.InputMultipleOrderedItems_Load);
			this.panel4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400037E RID: 894
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x0400037F RID: 895
		private global::System.Windows.Forms.ColumnHeader columnHeader2;

		// Token: 0x04000380 RID: 896
		private global::System.Windows.Forms.Panel panel4;

		// Token: 0x04000381 RID: 897
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000382 RID: 898
		private global::AutoComboBox.ListViewEx lv_from;

		// Token: 0x04000383 RID: 899
		private global::AutoComboBox.ListViewEx lv_to;

		// Token: 0x04000384 RID: 900
		private global::System.Windows.Forms.Button btn_moveDown;

		// Token: 0x04000385 RID: 901
		private global::System.Windows.Forms.Button btn_moveUp;

		// Token: 0x04000386 RID: 902
		private global::System.Windows.Forms.Button btn_moveAllLeft;

		// Token: 0x04000387 RID: 903
		private global::System.Windows.Forms.Button btn_moveLeft;

		// Token: 0x04000388 RID: 904
		private global::System.Windows.Forms.Button btn_moveAllRight;

		// Token: 0x04000389 RID: 905
		private global::System.Windows.Forms.Button btn_moveRight;

		// Token: 0x0400038A RID: 906
		private global::System.Windows.Forms.ImageList imageList1;

		// Token: 0x0400038B RID: 907
		private global::System.Windows.Forms.Label lbl_caption;

		// Token: 0x0400038C RID: 908
		private global::System.Windows.Forms.CheckBox chk_ascending;

		// Token: 0x0400038D RID: 909
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x0400038E RID: 910
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x0400038F RID: 911
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x04000390 RID: 912
		private global::System.ComponentModel.IContainer components;
	}
}
