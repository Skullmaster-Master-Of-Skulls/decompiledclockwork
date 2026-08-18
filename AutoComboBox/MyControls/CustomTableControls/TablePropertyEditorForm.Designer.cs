namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000078 RID: 120
	public partial class TablePropertyEditorForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x00026C2C File Offset: 0x00025C2C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00026C64 File Offset: 0x00025C64
		private void InitializeComponent()
		{
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripButton2,
				this.toolStripButton1
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 398);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(569, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.TabStop = true;
			this.toolStripButton1.Image = global::AutoComboBox.Properties.Resources.delete2;
			this.toolStripButton1.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new global::System.Drawing.Size(59, 22);
			this.toolStripButton1.Text = "&Cancel";
			this.toolStripButton1.Click += new global::System.EventHandler(this.toolStripButton1_Click);
			this.toolStripButton2.Image = global::AutoComboBox.Properties.Resources.check2;
			this.toolStripButton2.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new global::System.Drawing.Size(40, 22);
			this.toolStripButton2.Text = "&Ok";
			this.toolStripButton2.Click += new global::System.EventHandler(this.toolStripButton2_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(569, 423);
			base.Controls.Add(this.toolStrip1);
			base.Name = "TablePropertyEditorForm";
			this.Text = "TablePropertyEditorForm";
			base.Load += new global::System.EventHandler(this.TablePropertyEditorForm_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040003FD RID: 1021
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040003FE RID: 1022
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040003FF RID: 1023
		private global::System.Windows.Forms.ToolStripButton toolStripButton1;

		// Token: 0x04000400 RID: 1024
		private global::System.Windows.Forms.ToolStripButton toolStripButton2;
	}
}
