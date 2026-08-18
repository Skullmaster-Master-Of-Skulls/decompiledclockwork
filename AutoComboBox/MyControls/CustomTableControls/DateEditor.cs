using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200003A RID: 58
	public class DateEditor : ColumnTypeEditorPanel
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x0001211C File Offset: 0x0001111C
		public DateEditor(DateDef target) : this()
		{
			this.__target = target;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0001212E File Offset: 0x0001112E
		private DateEditor()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00012147 File Offset: 0x00011147
		public override void save()
		{
			this.__target.Value = this.textBox1.Text;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00012161 File Offset: 0x00011161
		private void DateEditor_Load(object sender, EventArgs e)
		{
			this.textBox1.Text = this.__target.Value;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0001217C File Offset: 0x0001117C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000121B4 File Offset: 0x000111B4
		private void InitializeComponent()
		{
			this.textBox1 = new TextBox();
			this.label1 = new Label();
			base.SuspendLayout();
			this.textBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.textBox1.Location = new Point(6, 25);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(162, 20);
			this.textBox1.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(127, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Enter Date (in any format)";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBox1);
			base.Name = "DateEditor";
			base.Size = new Size(174, 51);
			base.Load += this.DateEditor_Load;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001D4 RID: 468
		private DateDef __target;

		// Token: 0x040001D5 RID: 469
		private IContainer components = null;

		// Token: 0x040001D6 RID: 470
		private TextBox textBox1;

		// Token: 0x040001D7 RID: 471
		private Label label1;
	}
}
