namespace DynamicScreens.Dialogs
{
	// Token: 0x0200001A RID: 26
	public partial class DynamicControlChooserForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00017488 File Offset: 0x00016488
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000174C0 File Offset: 0x000164C0
		private void InitializeComponent()
		{
			this.dynamicControlChooser1 = new global::DynamicScreens.CustomControls.DynamicControlChooser();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.dynamicControlChooser1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.dynamicControlChooser1.Location = new global::System.Drawing.Point(0, 0);
			this.dynamicControlChooser1.Margin = new global::System.Windows.Forms.Padding(3, 5, 3, 5);
			this.dynamicControlChooser1.Name = "dynamicControlChooser1";
			this.dynamicControlChooser1.Size = new global::System.Drawing.Size(331, 395);
			this.dynamicControlChooser1.TabIndex = 0;
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 395);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(331, 39);
			this.toolStrip1.TabIndex = 11;
			this.toolStrip1.TabStop = true;
			this.btn_ok.Image = global::DynamicScreens.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::DynamicScreens.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(331, 434);
			base.Controls.Add(this.dynamicControlChooser1);
			base.Controls.Add(this.toolStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "DynamicControlChooserForm";
			this.Text = "Choose Dynamic Fields";
			base.Load += new global::System.EventHandler(this.DynamicControlChooserForm_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400013B RID: 315
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400013C RID: 316
		private global::DynamicScreens.CustomControls.DynamicControlChooser dynamicControlChooser1;

		// Token: 0x0400013D RID: 317
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x0400013E RID: 318
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x0400013F RID: 319
		private global::System.Windows.Forms.ToolStripButton btn_cancel;
	}
}
