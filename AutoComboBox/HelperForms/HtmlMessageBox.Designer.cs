namespace AutoComboBox.HelperForms
{
	// Token: 0x0200003C RID: 60
	public partial class HtmlMessageBox : global::System.Windows.Forms.Form
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00012348 File Offset: 0x00011348
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00012380 File Offset: 0x00011380
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.HelperForms.HtmlMessageBox));
			this.btn_close = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_print = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.myWebBrowser1 = new global::AutoComboBox.MyControls.MyWebBrowser();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.btn_close.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_close.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new global::System.Drawing.Size(85, 36);
			this.btn_close.Text = "&Close";
			this.btn_close.Click += new global::System.EventHandler(this.btn_close_Click);
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_print,
				this.toolStripSeparator1,
				this.btn_close
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 427);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(623, 39);
			this.toolStrip1.TabIndex = 14;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_print.Image = global::AutoComboBox.Properties.Resources.printer;
			this.btn_print.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_print.Name = "btn_print";
			this.btn_print.Size = new global::System.Drawing.Size(76, 36);
			this.btn_print.Text = "&Print";
			this.btn_print.Click += new global::System.EventHandler(this.btn_print_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 39);
			this.myWebBrowser1.Css = componentResourceManager.GetString("myWebBrowser1.Css");
			this.myWebBrowser1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.myWebBrowser1.Location = new global::System.Drawing.Point(0, 0);
			this.myWebBrowser1.MyPanel = null;
			this.myWebBrowser1.Name = "myWebBrowser1";
			this.myWebBrowser1.Size = new global::System.Drawing.Size(623, 427);
			this.myWebBrowser1.TabIndex = 15;
			this.myWebBrowser1.Title = "Form summary";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(623, 466);
			base.Controls.Add(this.myWebBrowser1);
			base.Controls.Add(this.toolStrip1);
			base.Name = "HtmlMessageBox";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "HtmlMessageBox";
			base.Load += new global::System.EventHandler(this.HtmlMessageBox_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001D8 RID: 472
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040001D9 RID: 473
		private global::System.Windows.Forms.ToolStripButton btn_close;

		// Token: 0x040001DA RID: 474
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040001DB RID: 475
		private global::AutoComboBox.MyControls.MyWebBrowser myWebBrowser1;

		// Token: 0x040001DC RID: 476
		private global::System.Windows.Forms.ToolStripButton btn_print;

		// Token: 0x040001DD RID: 477
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
