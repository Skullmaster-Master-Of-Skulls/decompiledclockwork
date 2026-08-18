namespace DynamicScreens.AdminTools
{
	// Token: 0x02000007 RID: 7
	public partial class ControlCaptionsEditor : global::System.Windows.Forms.Form
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00004130 File Offset: 0x00003130
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004168 File Offset: 0x00003168
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::DynamicScreens.AdminTools.ControlCaptionsEditor));
			this.txt = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.btn_ok = new global::System.Windows.Forms.ToolStripButton();
			this.btn_cancel = new global::System.Windows.Forms.ToolStripButton();
			this.btn_split = new global::System.Windows.Forms.ToolStripSeparator();
			this.btn_autoFormat = new global::System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.txt.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.txt.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.txt.Location = new global::System.Drawing.Point(0, 24);
			this.txt.Multiline = true;
			this.txt.Name = "txt";
			this.txt.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txt.Size = new global::System.Drawing.Size(734, 426);
			this.txt.TabIndex = 10;
			this.label2.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new global::System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(734, 24);
			this.label2.TabIndex = 9;
			this.label2.Text = "Please enter the control caption(s), <newline> separated:";
			this.label2.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Visible = false;
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.toolStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new global::System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.btn_ok,
				this.btn_cancel,
				this.btn_split,
				this.btn_autoFormat
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(0, 450);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(734, 39);
			this.toolStrip1.TabIndex = 8;
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
			this.btn_split.Name = "btn_split";
			this.btn_split.Size = new global::System.Drawing.Size(6, 39);
			this.btn_autoFormat.CheckOnClick = true;
			this.btn_autoFormat.Image = global::DynamicScreens.Properties.Resources.textWithMulticheck;
			this.btn_autoFormat.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.btn_autoFormat.Name = "btn_autoFormat";
			this.btn_autoFormat.Size = new global::System.Drawing.Size(124, 36);
			this.btn_autoFormat.Text = "Auto &format";
			this.btn_autoFormat.Click += new global::System.EventHandler(this.btn_autoFormat_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(734, 489);
			base.Controls.Add(this.txt);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.toolStrip1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "ControlCaptionsEditor";
			this.Text = "Control Captions Editor";
			base.Load += new global::System.EventHandler(this.ControlCaptionsEditor_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000019 RID: 25
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400001A RID: 26
		private global::System.Windows.Forms.TextBox txt;

		// Token: 0x0400001B RID: 27
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400001C RID: 28
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x0400001D RID: 29
		private global::System.Windows.Forms.ToolStripButton btn_ok;

		// Token: 0x0400001E RID: 30
		private global::System.Windows.Forms.ToolStripButton btn_cancel;

		// Token: 0x0400001F RID: 31
		private global::System.Windows.Forms.ToolStripSeparator btn_split;

		// Token: 0x04000020 RID: 32
		private global::System.Windows.Forms.ToolStripButton btn_autoFormat;
	}
}
