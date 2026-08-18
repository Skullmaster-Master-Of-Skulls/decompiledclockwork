namespace AutoComboBox.InputDialogControls
{
	// Token: 0x0200001F RID: 31
	public partial class ShowTextDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060000EC RID: 236 RVA: 0x0000A84C File Offset: 0x0000984C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000A884 File Offset: 0x00009884
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputDialogControls.ShowTextDialog));
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.toolStripDropDownButton1 = new global::System.Windows.Forms.ToolStripDropDownButton();
			this.printToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.printPreviewToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_close = new global::System.Windows.Forms.ToolStripButton();
			this.rtf = new global::AutoComboBox.MyControls.HtmlRichTextBox();
			this.toolStrip2 = new global::System.Windows.Forms.ToolStrip();
			this.btn_increaseFontSize = new global::System.Windows.Forms.ToolStripButton();
			this.btn_decreaseFontSize = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripDropDownButton1,
				this.toolStripSeparator1,
				this.btn_close
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 378);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(571, 39);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStripDropDownButton1.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.printToolStripMenuItem,
				this.printPreviewToolStripMenuItem
			});
			this.toolStripDropDownButton1.Image = global::AutoComboBox.Properties.Resources.printer;
			this.toolStripDropDownButton1.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new global::System.Drawing.Size(85, 36);
			this.toolStripDropDownButton1.Text = "&Print";
			this.printToolStripMenuItem.Image = global::AutoComboBox.Properties.Resources.printer;
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.Size = new global::System.Drawing.Size(184, 38);
			this.printToolStripMenuItem.Text = "&Print";
			this.printToolStripMenuItem.Click += new global::System.EventHandler(this.printToolStripMenuItem_Click);
			this.printPreviewToolStripMenuItem.Image = global::AutoComboBox.Properties.Resources.printer_view;
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new global::System.Drawing.Size(184, 38);
			this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
			this.printPreviewToolStripMenuItem.Click += new global::System.EventHandler(this.printPreviewToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.btn_close.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_close.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new global::System.Drawing.Size(85, 36);
			this.btn_close.Text = "&Close";
			this.btn_close.Click += new global::System.EventHandler(this.btn_close_Click);
			this.rtf.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.rtf.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.rtf.Location = new global::System.Drawing.Point(0, 25);
			this.rtf.Name = "rtf";
			this.rtf.ReadOnly = true;
			this.rtf.Size = new global::System.Drawing.Size(571, 353);
			this.rtf.TabIndex = 1;
			this.rtf.Text = "";
			this.toolStrip2.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip2.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_increaseFontSize,
				this.btn_decreaseFontSize
			});
			this.toolStrip2.Location = new global::System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new global::System.Drawing.Size(571, 25);
			this.toolStrip2.TabIndex = 3;
			this.toolStrip2.Text = "toolStrip2";
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
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(571, 417);
			base.Controls.Add(this.rtf);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.toolStrip2);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "ShowTextDialog";
			this.Text = "View";
			base.Load += new global::System.EventHandler(this.ShowTextDialog_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000147 RID: 327
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000148 RID: 328
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000149 RID: 329
		private global::System.Windows.Forms.ToolStripButton btn_close;

		// Token: 0x0400014A RID: 330
		private global::AutoComboBox.MyControls.HtmlRichTextBox rtf;

		// Token: 0x0400014B RID: 331
		private global::System.Windows.Forms.ToolStrip toolStrip2;

		// Token: 0x0400014C RID: 332
		private global::System.Windows.Forms.ToolStripButton btn_increaseFontSize;

		// Token: 0x0400014D RID: 333
		private global::System.Windows.Forms.ToolStripButton btn_decreaseFontSize;

		// Token: 0x0400014E RID: 334
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400014F RID: 335
		private global::System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;

		// Token: 0x04000150 RID: 336
		private global::System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;

		// Token: 0x04000151 RID: 337
		private global::System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
	}
}
