namespace AutoComboBox.InputDialogControls
{
	// Token: 0x020000CF RID: 207
	public partial class frmSaveClose : global::System.Windows.Forms.Form
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x0003E8AC File Offset: 0x0003D8AC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0003E8E4 File Offset: 0x0003D8E4
		private void InitializeComponent()
		{
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 498);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(484, 39);
			this.toolStrip1.TabIndex = 11;
			this.toolStrip1.TabStop = true;
			this.btn_ok.Image = global::AutoComboBox.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(80, 36);
			this.btn_ok.Text = "&Save";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(484, 537);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmSaveClose";
			this.Text = "Edit";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040005F9 RID: 1529
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040005FA RID: 1530
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040005FB RID: 1531
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x040005FC RID: 1532
		private global::System.Windows.Forms.ToolStripButton btn_cancel;
	}
}
