using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000062 RID: 98
	public class MyPasswordStrength : UserControl
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0001C2E4 File Offset: 0x0001B2E4
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0001C2FC File Offset: 0x0001B2FC
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
				this.CalculateStrength();
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001C30D File Offset: 0x0001B30D
		public MyPasswordStrength()
		{
			this.InitializeComponent();
			this.progressBar1.Maximum = 4;
			this.password = "";
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001C340 File Offset: 0x0001B340
		private void CalculateStrength()
		{
			if (string.IsNullOrEmpty(this.password))
			{
				this.progressBar1.Value = 0;
			}
			else
			{
				double num = this.checkEffectiveBitSize();
				if (num <= 32.0)
				{
					this.progressBar1.Value = 1;
				}
				else if (num <= 64.0)
				{
					this.progressBar1.Value = 2;
				}
				else if (num <= 128.0)
				{
					this.progressBar1.Value = 3;
				}
				else
				{
					this.progressBar1.Value = 4;
				}
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001C3EC File Offset: 0x0001B3EC
		private double checkEffectiveBitSize()
		{
			int length = this.password.Length;
			int charSetUsed = this.getCharSetUsed(this.password);
			return Math.Log(Math.Pow((double)charSetUsed, (double)length)) / Math.Log(2.0);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001C440 File Offset: 0x0001B440
		private int getCharSetUsed(string pass)
		{
			int num = 0;
			if (this.containsNumbers(pass))
			{
				num += 10;
			}
			if (this.containsLowerCaseChars(pass))
			{
				num += 26;
			}
			if (this.containsUpperCaseChars(pass))
			{
				num += 26;
			}
			if (this.containsPunctuation(pass))
			{
				num += 31;
			}
			return num;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001C4AC File Offset: 0x0001B4AC
		private bool containsNumbers(string str)
		{
			Regex regex = new Regex("[\\d]");
			return regex.IsMatch(str);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001C4D0 File Offset: 0x0001B4D0
		private bool containsLowerCaseChars(string str)
		{
			Regex regex = new Regex("[a-z]");
			return regex.IsMatch(str);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001C4F4 File Offset: 0x0001B4F4
		private bool containsUpperCaseChars(string str)
		{
			Regex regex = new Regex("[A-Z]");
			return regex.IsMatch(str);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001C518 File Offset: 0x0001B518
		private bool containsPunctuation(string str)
		{
			Regex regex = new Regex("[\\W|_]");
			return regex.IsMatch(str);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001C53C File Offset: 0x0001B53C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001C574 File Offset: 0x0001B574
		private void InitializeComponent()
		{
			this.progressBar1 = new ProgressBar();
			this.label1 = new Label();
			base.SuspendLayout();
			this.progressBar1.Dock = DockStyle.Top;
			this.progressBar1.Location = new Point(0, 0);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new Size(305, 23);
			this.progressBar1.TabIndex = 0;
			this.label1.Dock = DockStyle.Fill;
			this.label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(0, 23);
			this.label1.Name = "label1";
			this.label1.Padding = new Padding(0, 4, 0, 0);
			this.label1.Size = new Size(305, 39);
			this.label1.TabIndex = 1;
			this.label1.Text = "* This meter does not guarantee that your password is strong and should only be used as a personal reference.";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.label1);
			base.Controls.Add(this.progressBar1);
			base.Name = "MyPasswordStrength";
			base.Size = new Size(305, 62);
			base.ResumeLayout(false);
		}

		// Token: 0x04000363 RID: 867
		private string password;

		// Token: 0x04000364 RID: 868
		private IContainer components = null;

		// Token: 0x04000365 RID: 869
		private ProgressBar progressBar1;

		// Token: 0x04000366 RID: 870
		private Label label1;
	}
}
