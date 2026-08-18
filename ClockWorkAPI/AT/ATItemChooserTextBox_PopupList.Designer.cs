namespace ClockWorkAPI.AT
{
	// Token: 0x02000071 RID: 113
	public partial class ATItemChooserTextBox_PopupList : global::System.Windows.Forms.Form
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x0001EE84 File Offset: 0x0001DE84
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001EEBC File Offset: 0x0001DEBC
		private void InitializeComponent()
		{
			this.lb = new global::System.Windows.Forms.ListBox();
			this.superTooltip1 = new global::DevComponents.DotNetBar.SuperTooltip();
			base.SuspendLayout();
			this.lb.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.lb.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.lb.FormattingEnabled = true;
			this.lb.ItemHeight = 14;
			this.lb.Location = new global::System.Drawing.Point(0, 0);
			this.lb.Name = "lb";
			this.lb.Size = new global::System.Drawing.Size(279, 130);
			this.lb.TabIndex = 0;
			this.lb.SelectedIndexChanged += new global::System.EventHandler(this.lb_SelectedIndexChanged);
			this.lb.DoubleClick += new global::System.EventHandler(this.lb_DoubleClick);
			this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(9f, 18f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(279, 142);
			base.Controls.Add(this.lb);
			this.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Margin = new global::System.Windows.Forms.Padding(4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ATItemChooserTextBox_PopupList";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ATItemChooserTextBox_PopupList";
			base.TopMost = true;
			base.ResumeLayout(false);
		}

		// Token: 0x040002FF RID: 767
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000300 RID: 768
		private global::System.Windows.Forms.ListBox lb;

		// Token: 0x04000301 RID: 769
		private global::DevComponents.DotNetBar.SuperTooltip superTooltip1;
	}
}
