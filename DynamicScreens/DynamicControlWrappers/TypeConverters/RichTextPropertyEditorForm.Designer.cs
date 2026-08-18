namespace DynamicScreens.DynamicControlWrappers.TypeConverters
{
	// Token: 0x0200005E RID: 94
	public partial class RichTextPropertyEditorForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x00040D50 File Offset: 0x0003FD50
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00040D88 File Offset: 0x0003FD88
		private void InitializeComponent()
		{
			this.richTextBox = new global::AutoComboBox.MyControls.MyRichText();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.textBox = new global::System.Windows.Forms.TextBox();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_switchRichPlain = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.richTextBox.Caption = "";
			this.richTextBox.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.richTextBox.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.richTextBox.Location = new global::System.Drawing.Point(0, 0);
			this.richTextBox.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.OnlyAllowAdding = false;
			this.richTextBox.PlainText = "";
			this.richTextBox.ReadOnly = false;
			this.richTextBox.ScrollBars = global::System.Windows.Forms.RichTextBoxScrollBars.Both;
			this.richTextBox.Size = new global::System.Drawing.Size(436, 303);
			this.richTextBox.TabIndex = 0;
			this.richTextBox.Visible = false;
			this.richTextBox.WhoAmIName = "";
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(24, 24);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_switchRichPlain,
				this.toolStripSeparator1,
				this.btn_ok,
				this.btn_cancel
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 303);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(436, 31);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.TabStop = true;
			this.toolStrip1.Text = "toolStrip1";
			this.btn_ok.Image = global::DynamicScreens.Properties.Resources.check2;
			this.btn_ok.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new global::System.Drawing.Size(53, 28);
			this.btn_ok.Text = "&Ok";
			this.btn_ok.Click += new global::System.EventHandler(this.btn_ok_Click);
			this.btn_cancel.Image = global::DynamicScreens.Properties.Resources.delete2;
			this.btn_cancel.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new global::System.Drawing.Size(76, 28);
			this.btn_cancel.Text = "&Cancel";
			this.btn_cancel.Click += new global::System.EventHandler(this.btn_cancel_Click);
			this.textBox.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.textBox.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBox.Location = new global::System.Drawing.Point(0, 0);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.Size = new global::System.Drawing.Size(436, 303);
			this.textBox.TabIndex = 2;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 31);
			this.btn_switchRichPlain.Image = global::DynamicScreens.Properties.Resources.text_marked;
			this.btn_switchRichPlain.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_switchRichPlain.Name = "btn_switchRichPlain";
			this.btn_switchRichPlain.Size = new global::System.Drawing.Size(141, 28);
			this.btn_switchRichPlain.Text = "Switch to rich text";
			this.btn_switchRichPlain.Click += new global::System.EventHandler(this.btn_switchRichPlain_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(436, 334);
			base.Controls.Add(this.textBox);
			base.Controls.Add(this.richTextBox);
			base.Controls.Add(this.toolStrip1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RichTextPropertyEditorForm";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit text";
			base.Load += new global::System.EventHandler(this.RichTextPropertyEditor_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000374 RID: 884
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000375 RID: 885
		private global::AutoComboBox.MyControls.MyRichText richTextBox;

		// Token: 0x04000376 RID: 886
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000377 RID: 887
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x04000378 RID: 888
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x04000379 RID: 889
		private global::System.Windows.Forms.TextBox textBox;

		// Token: 0x0400037A RID: 890
		private global::System.Windows.Forms.ToolStripButton btn_switchRichPlain;

		// Token: 0x0400037B RID: 891
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
