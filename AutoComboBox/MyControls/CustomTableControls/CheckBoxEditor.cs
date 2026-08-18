using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x020000CD RID: 205
	public class CheckBoxEditor : ColumnTypeEditorPanel
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x0003E2E0 File Offset: 0x0003D2E0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0003E318 File Offset: 0x0003D318
		private void InitializeComponent()
		{
			this.label1 = new Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 6);
			this.label1.Name = "label1";
			this.label1.Size = new Size(469, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "There is no property to be edited. A CheckBox would allow user to select yes or no on a parameter";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.label1);
			base.Name = "CheckBoxEditor";
			base.Size = new Size(486, 24);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0003E3FE File Offset: 0x0003D3FE
		public CheckBoxEditor()
		{
			this.InitializeComponent();
		}

		// Token: 0x040005F3 RID: 1523
		private IContainer components = null;

		// Token: 0x040005F4 RID: 1524
		private Label label1;
	}
}
