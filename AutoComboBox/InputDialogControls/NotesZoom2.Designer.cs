namespace AutoComboBox.InputDialogControls
{
	// Token: 0x02000080 RID: 128
	public partial class NotesZoom2 : global::System.Windows.Forms.Form
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x0002A344 File Offset: 0x00029344
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0002A37C File Offset: 0x0002937C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputDialogControls.NotesZoom2));
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_suspend = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip2 = new global::System.Windows.Forms.ToolStrip();
			this.btn_spellCheck = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_increaseFontSize = new global::System.Windows.Forms.ToolStripButton();
			this.btn_decreaseFontSize = new global::System.Windows.Forms.ToolStripButton();
			this.textBox1 = new global::AutoComboBox.MyControls.MyRichText();
			this.btn_print = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_print,
				this.toolStripSeparator3,
				this.btn_suspend,
				this.toolStripSeparator2,
				this.btn_save,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 320);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(422, 39);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_suspend.Image = global::AutoComboBox.Properties.Resources.paperclip;
			this.btn_suspend.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_suspend.Name = "btn_suspend";
			this.btn_suspend.Size = new global::System.Drawing.Size(106, 36);
			this.btn_suspend.Text = "Sus&pend";
			this.btn_suspend.Click += new global::System.EventHandler(this.btn_suspend_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(6, 39);
			this.btn_save.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(80, 36);
			this.btn_save.Text = "&Save";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.toolStrip2.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip2.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_spellCheck,
				this.toolStripSeparator1,
				this.btn_increaseFontSize,
				this.btn_decreaseFontSize
			});
			this.toolStrip2.Location = new global::System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new global::System.Drawing.Size(422, 25);
			this.toolStrip2.TabIndex = 2;
			this.toolStrip2.Text = "toolStrip2";
			this.btn_spellCheck.Image = global::AutoComboBox.Properties.Resources.spellcheck;
			this.btn_spellCheck.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_spellCheck.Name = "btn_spellCheck";
			this.btn_spellCheck.Size = new global::System.Drawing.Size(82, 22);
			this.btn_spellCheck.Text = "S&pell check";
			this.btn_spellCheck.Click += new global::System.EventHandler(this.btn_spellCheck_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 25);
			this.btn_increaseFontSize.Image = global::AutoComboBox.Properties.Resources.font_add;
			this.btn_increaseFontSize.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_increaseFontSize.Name = "btn_increaseFontSize";
			this.btn_increaseFontSize.Size = new global::System.Drawing.Size(149, 22);
			this.btn_increaseFontSize.Text = "Increase font size (ctrl +)";
			this.btn_increaseFontSize.Click += new global::System.EventHandler(this.btn_increaseFontSize_Click);
			this.btn_decreaseFontSize.Image = global::AutoComboBox.Properties.Resources.font_delete;
			this.btn_decreaseFontSize.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_decreaseFontSize.Name = "btn_decreaseFontSize";
			this.btn_decreaseFontSize.Size = new global::System.Drawing.Size(152, 22);
			this.btn_decreaseFontSize.Text = "Decrease font size (ctrl -)";
			this.btn_decreaseFontSize.Click += new global::System.EventHandler(this.btn_decreaseFontSize_Click);
			this.textBox1.BaseReadOnly = false;
			this.textBox1.Caption = "";
			this.textBox1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBox1.Location = new global::System.Drawing.Point(0, 25);
			this.textBox1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.textBox1.Name = "textBox1";
			this.textBox1.OnlyAllowAdding = false;
			this.textBox1.PlainText = "";
			this.textBox1.ReadOnly = false;
			this.textBox1.ScrollBars = global::System.Windows.Forms.RichTextBoxScrollBars.Both;
			this.textBox1.Size = new global::System.Drawing.Size(422, 295);
			this.textBox1.TabIndex = 3;
			this.textBox1.WhoAmIName = "";
			this.btn_print.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btn_print.Image = global::AutoComboBox.Properties.Resources.printer;
			this.btn_print.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_print.Name = "btn_print";
			this.btn_print.Size = new global::System.Drawing.Size(36, 36);
			this.btn_print.Text = "Print";
			this.btn_print.Click += new global::System.EventHandler(this.btn_print_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new global::System.Drawing.Size(6, 39);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(422, 359);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.toolStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "NotesZoom2";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Notes";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.NotesZoom_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400043B RID: 1083
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400043C RID: 1084
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x0400043D RID: 1085
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x0400043E RID: 1086
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x0400043F RID: 1087
		private global::System.Windows.Forms.ToolStrip toolStrip2;

		// Token: 0x04000440 RID: 1088
		private global::System.Windows.Forms.ToolStripButton btn_spellCheck;

		// Token: 0x04000441 RID: 1089
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000442 RID: 1090
		private global::System.Windows.Forms.ToolStripButton btn_increaseFontSize;

		// Token: 0x04000443 RID: 1091
		private global::System.Windows.Forms.ToolStripButton btn_decreaseFontSize;

		// Token: 0x04000444 RID: 1092
		private global::AutoComboBox.MyControls.MyRichText textBox1;

		// Token: 0x04000445 RID: 1093
		private global::System.Windows.Forms.ToolStripButton btn_suspend;

		// Token: 0x04000446 RID: 1094
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000447 RID: 1095
		private global::System.Windows.Forms.ToolStripButton btn_print;

		// Token: 0x04000448 RID: 1096
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	}
}
