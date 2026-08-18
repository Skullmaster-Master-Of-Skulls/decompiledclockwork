using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200009B RID: 155
	public class WhoenteredEditor : ColumnTypeEditorPanel
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x000312A5 File Offset: 0x000302A5
		public WhoenteredEditor(WhoenteredDef target) : this()
		{
			this.__target = target;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000312B7 File Offset: 0x000302B7
		private WhoenteredEditor()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000312D0 File Offset: 0x000302D0
		public override void save()
		{
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000312D3 File Offset: 0x000302D3
		private void WhoenteredEditor_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000312D8 File Offset: 0x000302D8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00031310 File Offset: 0x00030310
		private void InitializeComponent()
		{
			this.comboBoxEx1 = new ComboBoxEx();
			this.label1 = new Label();
			base.SuspendLayout();
			this.comboBoxEx1.DisplayMember = "Text";
			this.comboBoxEx1.DrawMode = DrawMode.OwnerDrawFixed;
			this.comboBoxEx1.FormattingEnabled = true;
			this.comboBoxEx1.ItemHeight = 14;
			this.comboBoxEx1.Location = new Point(3, 30);
			this.comboBoxEx1.Name = "comboBoxEx1";
			this.comboBoxEx1.Size = new Size(121, 20);
			this.comboBoxEx1.TabIndex = 0;
			this.comboBoxEx1.Text = "Whoentered";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(170, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "This feature is not yet implemented";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBoxEx1);
			base.Name = "WhoenteredEditor";
			base.Size = new Size(206, 53);
			base.Load += this.WhoenteredEditor_Load;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040004D3 RID: 1235
		private WhoenteredDef __target;

		// Token: 0x040004D4 RID: 1236
		private IContainer components = null;

		// Token: 0x040004D5 RID: 1237
		private ComboBoxEx comboBoxEx1;

		// Token: 0x040004D6 RID: 1238
		private Label label1;
	}
}
