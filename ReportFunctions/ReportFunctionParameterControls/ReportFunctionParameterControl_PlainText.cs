using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ReportFunctions.ReportFunctionParameterControls
{
	// Token: 0x02000007 RID: 7
	public class ReportFunctionParameterControl_PlainText : UserControl, iReportFunctionParameter
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002A47 File Offset: 0x00001A47
		public ReportFunctionParameterControl_PlainText()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A60 File Offset: 0x00001A60
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002A7D File Offset: 0x00001A7D
		public string Parameter
		{
			get
			{
				return this.textBox1.Text;
			}
			set
			{
				this.textBox1.Text = value;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A90 File Offset: 0x00001A90
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AC8 File Offset: 0x00001AC8
		private void InitializeComponent()
		{
			this.textBox1 = new TextBox();
			base.SuspendLayout();
			this.textBox1.Dock = DockStyle.Fill;
			this.textBox1.Location = new Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = ScrollBars.Both;
			this.textBox1.Size = new Size(150, 150);
			this.textBox1.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.textBox1);
			base.Name = "ReportFunctionParameterControl_PlainText";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002BA8 File Offset: 0x00001BA8
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002BC8 File Offset: 0x00001BC8
		public bool AllowTab
		{
			get
			{
				return this.textBox1.AcceptsTab;
			}
			set
			{
				if (value)
				{
					this.textBox1.TabStop = false;
					this.textBox1.AcceptsTab = true;
				}
				else
				{
					this.textBox1.TabStop = true;
					this.textBox1.AcceptsTab = false;
				}
			}
		}

		// Token: 0x0400009B RID: 155
		private IContainer components = null;

		// Token: 0x0400009C RID: 156
		private TextBox textBox1;
	}
}
