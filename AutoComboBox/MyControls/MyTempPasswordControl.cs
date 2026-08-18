using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000048 RID: 72
	public class MyTempPasswordControl : UserControl
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x00017A14 File Offset: 0x00016A14
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00017A4C File Offset: 0x00016A4C
		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.txt_pwd = new TextBox();
			this.button1 = new Button();
			this.button2 = new Button();
			this.panel1 = new Panel();
			this.myDateTimePicker1 = new MyDateTimePicker();
			this.label2 = new Label();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(81, 10);
			this.label1.Name = "label1";
			this.label1.Size = new Size(102, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Temp password:";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.txt_pwd.Location = new Point(189, 7);
			this.txt_pwd.Name = "txt_pwd";
			this.txt_pwd.ReadOnly = true;
			this.txt_pwd.Size = new Size(155, 22);
			this.txt_pwd.TabIndex = 1;
			this.button1.Location = new Point(7, 32);
			this.button1.Name = "button1";
			this.button1.Size = new Size(59, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Clear";
			this.button1.UseVisualStyleBackColor = true;
			this.button2.Location = new Point(7, 7);
			this.button2.Name = "button2";
			this.button2.Size = new Size(59, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "New";
			this.button2.UseVisualStyleBackColor = true;
			this.panel1.Controls.Add(this.txt_pwd);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.myDateTimePicker1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Dock = DockStyle.Top;
			this.panel1.Location = new Point(4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new Padding(4);
			this.panel1.Size = new Size(442, 62);
			this.panel1.TabIndex = 4;
			this.myDateTimePicker1.BaseValue = new DateTime(2009, 11, 13, 15, 31, 2, 873);
			this.myDateTimePicker1.CustomFormat = "MMMM dd, yyyy";
			this.myDateTimePicker1.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.myDateTimePicker1.Format = DateTimePickerFormat.Custom;
			this.myDateTimePicker1.GreyedOut = false;
			this.myDateTimePicker1.Location = new Point(189, 35);
			this.myDateTimePicker1.Name = "myDateTimePicker1";
			this.myDateTimePicker1.Size = new Size(200, 22);
			this.myDateTimePicker1.TabIndex = 4;
			this.myDateTimePicker1.Value = new DateTime(2009, 11, 13, 15, 31, 2, 873);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(81, 35);
			this.label2.Name = "label2";
			this.label2.Size = new Size(52, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Expires";
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.panel1);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "MyTempPasswordControl";
			base.Padding = new Padding(4);
			base.Size = new Size(450, 73);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00017F2E File Offset: 0x00016F2E
		public MyTempPasswordControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x04000230 RID: 560
		private IContainer components = null;

		// Token: 0x04000231 RID: 561
		private Label label1;

		// Token: 0x04000232 RID: 562
		private TextBox txt_pwd;

		// Token: 0x04000233 RID: 563
		private Button button1;

		// Token: 0x04000234 RID: 564
		private Button button2;

		// Token: 0x04000235 RID: 565
		private Panel panel1;

		// Token: 0x04000236 RID: 566
		private Label label2;

		// Token: 0x04000237 RID: 567
		private MyDateTimePicker myDateTimePicker1;
	}
}
