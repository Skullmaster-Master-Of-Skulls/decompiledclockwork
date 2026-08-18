namespace ClockWorkAPI.DynamicDataForms
{
	// Token: 0x02000026 RID: 38
	public partial class VariableValuesEditByForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000209 RID: 521 RVA: 0x0000BB98 File Offset: 0x0000AB98
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000BBD0 File Offset: 0x0000ABD0
		private void InitializeComponent()
		{
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.dataPerStudent1 = new global::ClockWorkAPI.DynamicDataForms.DataPerStudent();
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
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 623);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(984, 39);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.TabStop = true;
			this.btn_ok.Image = global::ClockWorkAPI.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(64, 36);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::ClockWorkAPI.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.dataPerStudent1.AutoScroll = true;
			this.dataPerStudent1.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataPerStudent1.ControlsTable = null;
			this.dataPerStudent1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.dataPerStudent1.DynamicScreenInvisibleCids = null;
			this.dataPerStudent1.DynamicScreenReadOnlyCids = null;
			this.dataPerStudent1.Location = new global::System.Drawing.Point(0, 0);
			this.dataPerStudent1.Name = "dataPerStudent1";
			this.dataPerStudent1.OverridePanelBackgroundColour = global::System.Drawing.Color.Transparent;
			this.dataPerStudent1.OverridePanelColourEnabled = false;
			this.dataPerStudent1.OverridePanelForegroundColour = global::System.Drawing.Color.Transparent;
			this.dataPerStudent1.Size = new global::System.Drawing.Size(984, 623);
			this.dataPerStudent1.TabIndex = 0;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(984, 662);
			base.Controls.Add(this.dataPerStudent1);
			base.Controls.Add(this.toolStrip1);
			base.Name = "VariableValuesEditByForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Generate document";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000F7 RID: 247
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040000F8 RID: 248
		private global::ClockWorkAPI.DynamicDataForms.DataPerStudent dataPerStudent1;

		// Token: 0x040000F9 RID: 249
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040000FA RID: 250
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x040000FB RID: 251
		private global::System.Windows.Forms.ToolStripButton btn_cancel;
	}
}
