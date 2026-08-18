using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x0200008A RID: 138
	public class MyTimePickerAcc : UserControl
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x0002E094 File Offset: 0x0002D094
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002E0CC File Offset: 0x0002D0CC
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.label5 = new Label();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.txt_ampm = new TextBox();
			this.txt_minute = new TextBox();
			this.txt_hour = new TextBox();
			this.label1 = new Label();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 83f));
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label4, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txt_ampm, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.txt_minute, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.txt_hour, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Dock = DockStyle.Left;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Margin = new Padding(4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.Size = new Size(241, 55);
			this.tableLayoutPanel1.TabIndex = 2;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(68, 38);
			this.label5.Margin = new Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new Size(0, 16);
			this.label5.TabIndex = 7;
			this.label4.AutoSize = true;
			this.label4.Dock = DockStyle.Fill;
			this.label4.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(162, 38);
			this.label4.Margin = new Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new Size(75, 17);
			this.label4.TabIndex = 6;
			this.label4.Text = "AM / PM";
			this.label3.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(68, 0);
			this.label3.Margin = new Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new Size(15, 32);
			this.label3.TabIndex = 5;
			this.label3.Text = ":";
			this.label3.TextAlign = ContentAlignment.MiddleCenter;
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(91, 38);
			this.label2.Margin = new Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(57, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "MINUTE";
			this.txt_ampm.AccessibleDescription = "am / pm";
			this.txt_ampm.AccessibleName = "am / pm";
			this.txt_ampm.CharacterCasing = CharacterCasing.Upper;
			this.txt_ampm.Location = new Point(164, 6);
			this.txt_ampm.Margin = new Padding(6);
			this.txt_ampm.Name = "txt_ampm";
			this.txt_ampm.Size = new Size(40, 26);
			this.txt_ampm.TabIndex = 3;
			this.txt_ampm.KeyPress += this.txt_year_KeyPress;
			this.txt_minute.AccessibleDescription = "Minute";
			this.txt_minute.AccessibleName = "Minute";
			this.txt_minute.CharacterCasing = CharacterCasing.Upper;
			this.txt_minute.Location = new Point(93, 6);
			this.txt_minute.Margin = new Padding(6);
			this.txt_minute.Name = "txt_minute";
			this.txt_minute.Size = new Size(59, 26);
			this.txt_minute.TabIndex = 2;
			this.txt_minute.Leave += this.txt_minute_Leave;
			this.txt_hour.AccessibleDescription = "Hour";
			this.txt_hour.AccessibleName = "Hour";
			this.txt_hour.CharacterCasing = CharacterCasing.Upper;
			this.txt_hour.Location = new Point(6, 6);
			this.txt_hour.Margin = new Padding(6);
			this.txt_hour.Name = "txt_hour";
			this.txt_hour.Size = new Size(52, 26);
			this.txt_hour.TabIndex = 0;
			this.txt_hour.TextAlign = HorizontalAlignment.Center;
			this.txt_hour.Leave += this.txt_hour_Leave;
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(4, 38);
			this.label1.Margin = new Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(45, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "HOUR";
			base.AutoScaleDimensions = new SizeF(9f, 18f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tableLayoutPanel1);
			this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(4);
			base.Name = "MyTimePickerAcc";
			base.Size = new Size(292, 55);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002E8A3 File Offset: 0x0002D8A3
		public MyTimePickerAcc()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0002E8BC File Offset: 0x0002D8BC
		private void txt_year_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 'a' || e.KeyChar == 'A')
			{
				this.txt_ampm.Text = "AM";
			}
			else if (e.KeyChar == 'p' || e.KeyChar == 'P')
			{
				this.txt_ampm.Text = "PM";
			}
			e.Handled = true;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0002E938 File Offset: 0x0002D938
		private void txt_hour_Leave(object sender, EventArgs e)
		{
			int num;
			int hourOnscreen = this.GetHourOnscreen(out num);
			this.txt_hour.Text = hourOnscreen.ToString();
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0002E964 File Offset: 0x0002D964
		private int GetHourOnscreen(out int err)
		{
			int num = 0;
			string text = this.OnlyKeepDigits(this.txt_hour.Text);
			int num2;
			if (text.Length > 0)
			{
				num2 = int.Parse(text);
			}
			else
			{
				num++;
				num2 = DateTime.Now.Hour;
			}
			if (num2 < 0)
			{
				num++;
				num2 = 0;
			}
			else if (num2 > 23)
			{
				num++;
				num2 = DateTime.Now.Hour;
			}
			if (num2 > 12)
			{
				num2 -= 12;
				num += 2;
			}
			err = num;
			return num2;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002EA14 File Offset: 0x0002DA14
		private string OnlyKeepDigits(string s)
		{
			string text = "";
			foreach (char c in s)
			{
				if (char.IsDigit(c))
				{
					text += c;
				}
			}
			return text;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002EA74 File Offset: 0x0002DA74
		private void txt_minute_Leave(object sender, EventArgs e)
		{
			int num;
			int minuteOnscreen = this.GetMinuteOnscreen(out num);
			if (minuteOnscreen < 10)
			{
				this.txt_minute.Text = "0" + minuteOnscreen.ToString();
			}
			else
			{
				this.txt_minute.Text = minuteOnscreen.ToString();
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002EACC File Offset: 0x0002DACC
		private int GetMinuteOnscreen(out int err)
		{
			int num = 0;
			string text = this.OnlyKeepDigits(this.txt_minute.Text);
			int num2;
			if (text.Length > 0)
			{
				num2 = int.Parse(text);
			}
			else
			{
				num2 = 0;
				num = 1;
			}
			if (num2 < 0)
			{
				num2 = 0;
				num = 2;
			}
			else if (num2 >= 60)
			{
				num2 = 59;
				num = 3;
			}
			err = num;
			return num2;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0002EB40 File Offset: 0x0002DB40
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0002EBB8 File Offset: 0x0002DBB8
		public DateTime Time
		{
			get
			{
				int num2;
				int num = this.GetHourOnscreen(out num2);
				if (this.txt_ampm.Text.CompareTo("PM") == 0 && num < 12)
				{
					num += 12;
				}
				int minuteOnscreen = this.GetMinuteOnscreen(out num2);
				DateTime now = DateTime.Now;
				return new DateTime(now.Year, now.Month, now.Day, num, minuteOnscreen, 0);
			}
			set
			{
				int num = value.Hour;
				int minute = value.Minute;
				if (num == 12)
				{
					this.txt_ampm.Text = "PM";
				}
				else if (num > 12)
				{
					this.txt_ampm.Text = "PM";
					num -= 12;
				}
				else
				{
					this.txt_ampm.Text = "AM";
				}
				this.txt_hour.Text = num.ToString();
				this.txt_minute.Text = ((minute < 10) ? ("0" + minute.ToString()) : minute.ToString());
			}
		}

		// Token: 0x04000493 RID: 1171
		private IContainer components = null;

		// Token: 0x04000494 RID: 1172
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000495 RID: 1173
		private Label label3;

		// Token: 0x04000496 RID: 1174
		private Label label2;

		// Token: 0x04000497 RID: 1175
		private TextBox txt_ampm;

		// Token: 0x04000498 RID: 1176
		private TextBox txt_minute;

		// Token: 0x04000499 RID: 1177
		private TextBox txt_hour;

		// Token: 0x0400049A RID: 1178
		private Label label1;

		// Token: 0x0400049B RID: 1179
		private Label label4;

		// Token: 0x0400049C RID: 1180
		private Label label5;
	}
}
