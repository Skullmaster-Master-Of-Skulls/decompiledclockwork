namespace AutoComboBox.InputDialogControls
{
	// Token: 0x020000E2 RID: 226
	public partial class AccessibilityOptions : global::System.Windows.Forms.Form
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x000445A4 File Offset: 0x000435A4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000445DC File Offset: 0x000435DC
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::AutoComboBox.InputDialogControls.AccessibilityOptions));
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_close = new global::System.Windows.Forms.ToolStripButton();
			this.btn_fakeClose = new global::System.Windows.Forms.Button();
			this.lv = new global::System.Windows.Forms.ListView();
			this.columnHeader1 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_close
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 458);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new global::System.Windows.Forms.Padding(0, 0, 2, 0);
			this.toolStrip1.Size = new global::System.Drawing.Size(635, 39);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.TabStop = true;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_close.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_close.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new global::System.Drawing.Size(85, 36);
			this.btn_close.Text = "&Close";
			this.btn_close.Click += new global::System.EventHandler(this.btn_close_Click);
			this.btn_fakeClose.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btn_fakeClose.Location = new global::System.Drawing.Point(344, 195);
			this.btn_fakeClose.Name = "btn_fakeClose";
			this.btn_fakeClose.Size = new global::System.Drawing.Size(0, 0);
			this.btn_fakeClose.TabIndex = 2;
			this.btn_fakeClose.TabStop = false;
			this.btn_fakeClose.UseVisualStyleBackColor = true;
			this.btn_fakeClose.Click += new global::System.EventHandler(this.btn_fakeClose_Click);
			this.lv.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.lv.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new global::System.Drawing.Point(0, 0);
			this.lv.Name = "lv";
			this.lv.Size = new global::System.Drawing.Size(635, 458);
			this.lv.TabIndex = 3;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = global::System.Windows.Forms.View.Details;
			this.lv.SizeChanged += new global::System.EventHandler(this.lv_SizeChanged);
			this.columnHeader1.Text = "Description";
			this.columnHeader1.Width = 399;
			this.columnHeader2.Text = "Command";
			this.columnHeader2.Width = 204;
			base.AcceptButton = this.btn_fakeClose;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(9f, 18f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btn_fakeClose;
			base.ClientSize = new global::System.Drawing.Size(635, 497);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.btn_fakeClose);
			base.Controls.Add(this.toolStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(4);
			base.Name = "AccessibilityOptions";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			base.Load += new global::System.EventHandler(this.AccessibilityOptions_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000650 RID: 1616
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000651 RID: 1617
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000652 RID: 1618
		private global::System.Windows.Forms.ToolStripButton btn_close;

		// Token: 0x04000653 RID: 1619
		private global::System.Windows.Forms.Button btn_fakeClose;

		// Token: 0x04000654 RID: 1620
		private global::System.Windows.Forms.ListView lv;

		// Token: 0x04000655 RID: 1621
		private global::System.Windows.Forms.ColumnHeader columnHeader1;

		// Token: 0x04000656 RID: 1622
		private global::System.Windows.Forms.ColumnHeader columnHeader2;
	}
}
