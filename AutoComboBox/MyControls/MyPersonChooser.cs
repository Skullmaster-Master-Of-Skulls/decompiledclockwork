using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000022 RID: 34
	public class MyPersonChooser : UserControl
	{
		// Token: 0x06000107 RID: 263 RVA: 0x0000BC8C File Offset: 0x0000AC8C
		public MyPersonChooser()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000BCA8 File Offset: 0x0000ACA8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000BCE0 File Offset: 0x0000ACE0
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "MyPersonChooser";
			base.Size = new Size(545, 66);
			base.ResumeLayout(false);
		}

		// Token: 0x04000158 RID: 344
		private IContainer components = null;
	}
}
