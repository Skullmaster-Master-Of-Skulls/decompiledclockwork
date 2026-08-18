namespace ClockWorkAPI.DynamicDataForms.FormWrappers
{
	// Token: 0x0200001E RID: 30
	public partial class ftmDataPerStudent : global::System.Windows.Forms.Form
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00007870 File Offset: 0x00006870
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000078A8 File Offset: 0x000068A8
		private void InitializeComponent()
		{
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_save = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.lbl_student = new global::System.Windows.Forms.Label();
			this.toolStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_save,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 523);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(784, 39);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.TabStop = true;
			this.btn_save.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_save.Image = global::ClockWorkAPI.Properties.Resources.check2;
			this.btn_save.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_save.Name = "btn_save";
			this.btn_save.Size = new global::System.Drawing.Size(80, 36);
			this.btn_save.Text = "&Save";
			this.btn_save.Click += new global::System.EventHandler(this.btn_save_Click);
			this.btn_cancel.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.btn_cancel.Image = global::ClockWorkAPI.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(93, 36);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.panel1.Controls.Add(this.lbl_student);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new global::System.Drawing.Point(0, 0);
			this.panel1.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(784, 32);
			this.panel1.TabIndex = 2;
			this.lbl_student.BackColor = global::System.Drawing.SystemColors.Highlight;
			this.lbl_student.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lbl_student.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_student.ForeColor = global::System.Drawing.SystemColors.HighlightText;
			this.lbl_student.Location = new global::System.Drawing.Point(0, 0);
			this.lbl_student.Name = "lbl_student";
			this.lbl_student.Size = new global::System.Drawing.Size(784, 32);
			this.lbl_student.TabIndex = 0;
			this.lbl_student.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 16f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(784, 562);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.toolStrip1);
			this.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.Name = "ftmDataPerStudent";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Data";
			base.Load += new global::System.EventHandler(this.ftmDataPerStudent_Load);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.ftmDataPerStudent_FormClosing);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000A0 RID: 160
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040000A1 RID: 161
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x040000A2 RID: 162
		private global::System.Windows.Forms.ToolStripButton btn_save;

		// Token: 0x040000A3 RID: 163
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x040000A4 RID: 164
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040000A5 RID: 165
		private global::System.Windows.Forms.Label lbl_student;
	}
}
