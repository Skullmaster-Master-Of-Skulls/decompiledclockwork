using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000025 RID: 37
	public class NotesEditor : ColumnTypeEditorPanel
	{
		// Token: 0x0600010F RID: 271 RVA: 0x0000BDF0 File Offset: 0x0000ADF0
		public NotesEditor(NotesDef target) : this()
		{
			this.__target = target;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000BE02 File Offset: 0x0000AE02
		private NotesEditor()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000BE1B File Offset: 0x0000AE1B
		public override void save()
		{
			this.__target.Value = this.textBox1.Text;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000BE35 File Offset: 0x0000AE35
		private void NotesEditor_Load(object sender, EventArgs e)
		{
			this.textBox1.Text = this.__target.Value;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000BE50 File Offset: 0x0000AE50
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000BE88 File Offset: 0x0000AE88
		private void InitializeComponent()
		{
			this.textBox1 = new TextBox();
			this.label1 = new Label();
			base.SuspendLayout();
			this.textBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.textBox1.Location = new Point(3, 27);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(285, 193);
			this.textBox1.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 11);
			this.label1.Name = "label1";
			this.label1.Size = new Size(61, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Enter notes";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBox1);
			base.Name = "NotesEditor";
			base.Size = new Size(291, 223);
			base.Load += this.NotesEditor_Load;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000159 RID: 345
		private NotesDef __target;

		// Token: 0x0400015A RID: 346
		private IContainer components = null;

		// Token: 0x0400015B RID: 347
		private TextBox textBox1;

		// Token: 0x0400015C RID: 348
		private Label label1;
	}
}
