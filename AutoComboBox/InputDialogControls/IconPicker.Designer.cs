namespace AutoComboBox.InputDialogControls
{
	// Token: 0x0200007F RID: 127
	public partial class IconPicker : global::System.Windows.Forms.Form
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x000289EC File Offset: 0x000279EC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00028A24 File Offset: 0x00027A24
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputDialogControls.IconPicker));
			this.toolbar = new global::System.Windows.Forms.ToolStrip();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel2 = new global::System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new global::System.Windows.Forms.StatusStrip();
			this.springPanel1 = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.iconsImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolbar.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.toolbar.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolbar.LayoutStyle = global::System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolbar.Location = new global::System.Drawing.Point(0, 0);
			this.toolbar.Name = "toolbar";
			this.toolbar.Size = new global::System.Drawing.Size(284, 217);
			this.toolbar.TabIndex = 6;
			this.toolbar.Text = "toolStrip1";
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_save,
				this.btn_cancel2
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 217);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(284, 25);
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(35, 22);
			this.btn_save.Text = "&Save";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_cancel2.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel2.Name = "btn_cancel2";
			this.btn_cancel2.Size = new global::System.Drawing.Size(47, 22);
			this.btn_cancel2.Text = "&Cancel";
			this.btn_cancel2.Click += new global::System.EventHandler(this.btn_cancel2_Click);
			this.statusStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.springPanel1
			});
			this.statusStrip1.Location = new global::System.Drawing.Point(0, 242);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new global::System.Drawing.Size(284, 22);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusStrip1";
			this.springPanel1.Name = "springPanel1";
			this.springPanel1.Size = new global::System.Drawing.Size(269, 17);
			this.springPanel1.Spring = true;
			this.springPanel1.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.iconsImageList.ImageStream = (global::System.Windows.Forms.ImageListStreamer)componentResourceManager.GetObject("iconsImageList.ImageStream");
			this.iconsImageList.TransparentColor = global::System.Drawing.Color.Transparent;
			this.iconsImageList.Images.SetKeyName(0, "");
			this.iconsImageList.Images.SetKeyName(1, "");
			this.iconsImageList.Images.SetKeyName(2, "");
			this.iconsImageList.Images.SetKeyName(3, "");
			this.iconsImageList.Images.SetKeyName(4, "");
			this.iconsImageList.Images.SetKeyName(5, "");
			this.iconsImageList.Images.SetKeyName(6, "");
			this.iconsImageList.Images.SetKeyName(7, "");
			this.iconsImageList.Images.SetKeyName(8, "");
			this.iconsImageList.Images.SetKeyName(9, "");
			this.iconsImageList.Images.SetKeyName(10, "");
			this.iconsImageList.Images.SetKeyName(11, "");
			this.iconsImageList.Images.SetKeyName(12, "");
			this.iconsImageList.Images.SetKeyName(13, "");
			this.iconsImageList.Images.SetKeyName(14, "");
			this.iconsImageList.Images.SetKeyName(15, "");
			this.iconsImageList.Images.SetKeyName(16, "");
			this.iconsImageList.Images.SetKeyName(17, "");
			this.iconsImageList.Images.SetKeyName(18, "");
			this.iconsImageList.Images.SetKeyName(19, "");
			this.iconsImageList.Images.SetKeyName(20, "");
			this.iconsImageList.Images.SetKeyName(21, "");
			this.iconsImageList.Images.SetKeyName(22, "");
			this.iconsImageList.Images.SetKeyName(23, "");
			this.iconsImageList.Images.SetKeyName(24, "");
			this.iconsImageList.Images.SetKeyName(25, "");
			this.iconsImageList.Images.SetKeyName(26, "");
			this.iconsImageList.Images.SetKeyName(27, "");
			this.iconsImageList.Images.SetKeyName(28, "");
			this.iconsImageList.Images.SetKeyName(29, "");
			this.iconsImageList.Images.SetKeyName(30, "");
			this.iconsImageList.Images.SetKeyName(31, "");
			this.iconsImageList.Images.SetKeyName(32, "");
			this.iconsImageList.Images.SetKeyName(33, "");
			this.iconsImageList.Images.SetKeyName(34, "");
			this.iconsImageList.Images.SetKeyName(35, "");
			this.iconsImageList.Images.SetKeyName(36, "");
			this.iconsImageList.Images.SetKeyName(37, "");
			this.iconsImageList.Images.SetKeyName(38, "");
			this.iconsImageList.Images.SetKeyName(39, "");
			this.iconsImageList.Images.SetKeyName(40, "");
			this.iconsImageList.Images.SetKeyName(41, "");
			this.iconsImageList.Images.SetKeyName(42, "");
			this.iconsImageList.Images.SetKeyName(43, "");
			this.iconsImageList.Images.SetKeyName(44, "");
			this.iconsImageList.Images.SetKeyName(45, "");
			this.iconsImageList.Images.SetKeyName(46, "");
			this.iconsImageList.Images.SetKeyName(47, "");
			this.iconsImageList.Images.SetKeyName(48, "");
			this.iconsImageList.Images.SetKeyName(49, "");
			this.iconsImageList.Images.SetKeyName(50, "");
			this.iconsImageList.Images.SetKeyName(51, "");
			this.iconsImageList.Images.SetKeyName(52, "");
			this.iconsImageList.Images.SetKeyName(53, "");
			this.iconsImageList.Images.SetKeyName(54, "");
			this.iconsImageList.Images.SetKeyName(55, "");
			this.iconsImageList.Images.SetKeyName(56, "");
			this.iconsImageList.Images.SetKeyName(57, "");
			this.iconsImageList.Images.SetKeyName(58, "");
			this.iconsImageList.Images.SetKeyName(59, "");
			this.iconsImageList.Images.SetKeyName(60, "");
			this.iconsImageList.Images.SetKeyName(61, "");
			this.iconsImageList.Images.SetKeyName(62, "");
			this.iconsImageList.Images.SetKeyName(63, "");
			this.iconsImageList.Images.SetKeyName(64, "");
			this.iconsImageList.Images.SetKeyName(65, "");
			this.iconsImageList.Images.SetKeyName(66, "");
			this.iconsImageList.Images.SetKeyName(67, "");
			this.iconsImageList.Images.SetKeyName(68, "");
			this.iconsImageList.Images.SetKeyName(69, "");
			this.iconsImageList.Images.SetKeyName(70, "");
			this.iconsImageList.Images.SetKeyName(71, "");
			this.iconsImageList.Images.SetKeyName(72, "");
			this.iconsImageList.Images.SetKeyName(73, "");
			this.iconsImageList.Images.SetKeyName(74, "");
			this.iconsImageList.Images.SetKeyName(75, "");
			this.iconsImageList.Images.SetKeyName(76, "");
			this.iconsImageList.Images.SetKeyName(77, "");
			this.iconsImageList.Images.SetKeyName(78, "");
			this.iconsImageList.Images.SetKeyName(79, "");
			this.iconsImageList.Images.SetKeyName(80, "");
			this.iconsImageList.Images.SetKeyName(81, "");
			this.iconsImageList.Images.SetKeyName(82, "");
			this.iconsImageList.Images.SetKeyName(83, "");
			this.iconsImageList.Images.SetKeyName(84, "");
			this.iconsImageList.Images.SetKeyName(85, "");
			this.iconsImageList.Images.SetKeyName(86, "");
			this.iconsImageList.Images.SetKeyName(87, "");
			this.iconsImageList.Images.SetKeyName(88, "");
			this.iconsImageList.Images.SetKeyName(89, "");
			this.iconsImageList.Images.SetKeyName(90, "");
			this.iconsImageList.Images.SetKeyName(91, "");
			this.iconsImageList.Images.SetKeyName(92, "");
			this.iconsImageList.Images.SetKeyName(93, "");
			this.iconsImageList.Images.SetKeyName(94, "");
			this.iconsImageList.Images.SetKeyName(95, "");
			this.iconsImageList.Images.SetKeyName(96, "");
			this.iconsImageList.Images.SetKeyName(97, "");
			this.iconsImageList.Images.SetKeyName(98, "");
			this.iconsImageList.Images.SetKeyName(99, "");
			this.iconsImageList.Images.SetKeyName(100, "");
			this.iconsImageList.Images.SetKeyName(101, "");
			this.iconsImageList.Images.SetKeyName(102, "");
			this.iconsImageList.Images.SetKeyName(103, "");
			this.iconsImageList.Images.SetKeyName(104, "");
			this.iconsImageList.Images.SetKeyName(105, "");
			this.iconsImageList.Images.SetKeyName(106, "");
			this.iconsImageList.Images.SetKeyName(107, "");
			this.iconsImageList.Images.SetKeyName(108, "");
			this.iconsImageList.Images.SetKeyName(109, "");
			this.iconsImageList.Images.SetKeyName(110, "");
			this.iconsImageList.Images.SetKeyName(111, "");
			this.iconsImageList.Images.SetKeyName(112, "");
			this.iconsImageList.Images.SetKeyName(113, "");
			this.iconsImageList.Images.SetKeyName(114, "");
			this.iconsImageList.Images.SetKeyName(115, "");
			this.iconsImageList.Images.SetKeyName(116, "");
			this.iconsImageList.Images.SetKeyName(117, "");
			this.iconsImageList.Images.SetKeyName(118, "");
			this.iconsImageList.Images.SetKeyName(119, "");
			this.iconsImageList.Images.SetKeyName(120, "");
			this.iconsImageList.Images.SetKeyName(121, "");
			this.iconsImageList.Images.SetKeyName(122, "");
			this.iconsImageList.Images.SetKeyName(123, "");
			this.iconsImageList.Images.SetKeyName(124, "");
			this.iconsImageList.Images.SetKeyName(125, "");
			this.iconsImageList.Images.SetKeyName(126, "");
			this.iconsImageList.Images.SetKeyName(127, "");
			this.iconsImageList.Images.SetKeyName(128, "");
			this.iconsImageList.Images.SetKeyName(129, "");
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(284, 264);
			base.Controls.Add(this.toolbar);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.statusStrip1);
			base.Name = "IconPicker";
			this.Text = "IconPicker";
			base.Load += new global::System.EventHandler(this.IconPicker_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000428 RID: 1064
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000429 RID: 1065
		private global::System.Windows.Forms.ToolStrip toolbar;

		// Token: 0x0400042A RID: 1066
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x0400042B RID: 1067
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x0400042C RID: 1068
		private global::System.Windows.Forms.ToolStripButton btn_cancel2;

		// Token: 0x0400042D RID: 1069
		private global::System.Windows.Forms.StatusStrip statusStrip1;

		// Token: 0x0400042E RID: 1070
		private global::System.Windows.Forms.ToolStripStatusLabel springPanel1;

		// Token: 0x0400042F RID: 1071
		public global::System.Windows.Forms.ImageList iconsImageList;
	}
}
