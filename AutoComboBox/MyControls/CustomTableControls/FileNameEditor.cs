using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000066 RID: 102
	public class FileNameEditor : ColumnTypeEditorPanel
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0001D6CE File Offset: 0x0001C6CE
		public FileNameEditor(FileNameDef target) : this()
		{
			this.__target = target;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001D6E0 File Offset: 0x0001C6E0
		private FileNameEditor()
		{
			this.InitializeComponent();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001D6F9 File Offset: 0x0001C6F9
		public override void save()
		{
			this.__target.Value = this.textBox1.Text;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001D713 File Offset: 0x0001C713
		private void FileNameEditor_Load(object sender, EventArgs e)
		{
			this.textBox1.Text = this.__target.Value;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001D730 File Offset: 0x0001C730
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001D768 File Offset: 0x0001C768
		private void InitializeComponent()
		{
			this.textBox1 = new TextBox();
			this.label1 = new Label();
			base.SuspendLayout();
			this.textBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.textBox1.Location = new Point(6, 26);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(415, 20);
			this.textBox1.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 10);
			this.label1.Name = "label1";
			this.label1.Size = new Size(77, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Enter file name";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBox1);
			base.Name = "FileNameEditor";
			base.Size = new Size(427, 53);
			base.Load += this.FileNameEditor_Load;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400037A RID: 890
		private FileNameDef __target;

		// Token: 0x0400037B RID: 891
		private IContainer components = null;

		// Token: 0x0400037C RID: 892
		private TextBox textBox1;

		// Token: 0x0400037D RID: 893
		private Label label1;
	}
}
