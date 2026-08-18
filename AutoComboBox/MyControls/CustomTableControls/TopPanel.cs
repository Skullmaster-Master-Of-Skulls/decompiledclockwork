using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200001C RID: 28
	public class TopPanel : UserControl
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00008208 File Offset: 0x00007208
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008240 File Offset: 0x00007240
		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.tb_ColumnName = new TextBox();
			this.label2 = new Label();
			this.cb_ColumnType = new ComboBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(76, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Column Name:";
			this.tb_ColumnName.Location = new Point(85, 0);
			this.tb_ColumnName.Name = "tb_ColumnName";
			this.tb_ColumnName.Size = new Size(200, 20);
			this.tb_ColumnName.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(302, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(72, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Column Type:";
			this.cb_ColumnType.FormattingEnabled = true;
			this.cb_ColumnType.Location = new Point(380, 0);
			this.cb_ColumnType.Name = "cb_ColumnType";
			this.cb_ColumnType.Size = new Size(121, 21);
			this.cb_ColumnType.TabIndex = 3;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.cb_ColumnType);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.tb_ColumnName);
			base.Controls.Add(this.label1);
			base.Name = "TopPanel";
			base.Size = new Size(512, 23);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00008482 File Offset: 0x00007482
		public TopPanel()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000849C File Offset: 0x0000749C
		public TextBox ColumnNameTextBox
		{
			get
			{
				return this.tb_ColumnName;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000084B4 File Offset: 0x000074B4
		public string ColumnName
		{
			get
			{
				return this.tb_ColumnName.Text;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000084D4 File Offset: 0x000074D4
		public ComboBox ColumnTypeComboBox
		{
			get
			{
				return this.cb_ColumnType;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000084EC File Offset: 0x000074EC
		public int SelectedColumnType
		{
			get
			{
				return this.cb_ColumnType.SelectedIndex;
			}
		}

		// Token: 0x04000122 RID: 290
		private IContainer components = null;

		// Token: 0x04000123 RID: 291
		private Label label1;

		// Token: 0x04000124 RID: 292
		private TextBox tb_ColumnName;

		// Token: 0x04000125 RID: 293
		private Label label2;

		// Token: 0x04000126 RID: 294
		private ComboBox cb_ColumnType;
	}
}
