using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000038 RID: 56
	public class MyDateTimePickerAcc : UserControl, MyDynamicControl
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00010D98 File Offset: 0x0000FD98
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00010DD0 File Offset: 0x0000FDD0
		private void InitializeComponent()
		{
			this.txt_day = new TextBox();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.label3 = new Label();
			this.label2 = new Label();
			this.txt_year = new TextBox();
			this.txt_month = new TextBox();
			this.label1 = new Label();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.txt_day.AccessibleDescription = "Day";
			this.txt_day.AccessibleName = "Day";
			this.txt_day.Dock = DockStyle.Fill;
			this.txt_day.Location = new Point(4, 4);
			this.txt_day.Margin = new Padding(4);
			this.txt_day.Name = "txt_day";
			this.txt_day.Size = new Size(48, 26);
			this.txt_day.TabIndex = 0;
			this.txt_day.KeyPress += this.txt_day_KeyPress;
			this.txt_day.KeyUp += this.txt_day_KeyUp;
			this.txt_day.Leave += this.txt_day_Leave;
			this.tableLayoutPanel1.AccessibleRole = AccessibleRole.Pane;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txt_year, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.txt_month, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txt_day, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Dock = DockStyle.Left;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.Size = new Size(312, 55);
			this.tableLayoutPanel1.TabIndex = 1;
			this.label3.AutoSize = true;
			this.label3.Dock = DockStyle.Fill;
			this.label3.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(224, 34);
			this.label3.Name = "label3";
			this.label3.Size = new Size(85, 21);
			this.label3.TabIndex = 5;
			this.label3.Text = "YEAR";
			this.label2.AutoSize = true;
			this.label2.Dock = DockStyle.Fill;
			this.label2.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(59, 34);
			this.label2.Name = "label2";
			this.label2.Size = new Size(159, 21);
			this.label2.TabIndex = 4;
			this.label2.Text = "MONTH";
			this.txt_year.AccessibleDescription = "Year";
			this.txt_year.AccessibleName = "Year";
			this.txt_year.Dock = DockStyle.Fill;
			this.txt_year.Location = new Point(225, 4);
			this.txt_year.Margin = new Padding(4);
			this.txt_year.Name = "txt_year";
			this.txt_year.Size = new Size(83, 26);
			this.txt_year.TabIndex = 3;
			this.txt_year.KeyPress += this.txt_year_KeyPress;
			this.txt_year.KeyUp += this.txt_year_KeyUp;
			this.txt_year.Leave += this.txt_year_Leave;
			this.txt_month.AccessibleDescription = "Month";
			this.txt_month.AccessibleName = "Month";
			this.txt_month.Dock = DockStyle.Fill;
			this.txt_month.Location = new Point(60, 4);
			this.txt_month.Margin = new Padding(4);
			this.txt_month.Name = "txt_month";
			this.txt_month.Size = new Size(157, 26);
			this.txt_month.TabIndex = 2;
			this.txt_month.KeyPress += this.txt_month_KeyPress;
			this.txt_month.KeyUp += this.txt_month_KeyUp;
			this.txt_month.Leave += this.txt_month_Leave;
			this.label1.AutoSize = true;
			this.label1.Dock = DockStyle.Fill;
			this.label1.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(3, 34);
			this.label1.Name = "label1";
			this.label1.Size = new Size(50, 21);
			this.label1.TabIndex = 1;
			this.label1.Text = "DAY";
			base.AutoScaleDimensions = new SizeF(9f, 18f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tableLayoutPanel1);
			this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(4);
			base.Name = "MyDateTimePickerAcc";
			base.Size = new Size(336, 55);
			base.Validating += this.MyDateTimePickerAcc_Validating;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00011498 File Offset: 0x00010498
		public MyDateTimePickerAcc()
		{
			this.InitializeComponent();
			this.Date = DateTime.Now;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0001155C File Offset: 0x0001055C
		public object ReportObject
		{
			get
			{
				object result;
				if (this.Date == DateTime.MinValue)
				{
					result = null;
				}
				else
				{
					result = this.Date;
				}
				return result;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00011594 File Offset: 0x00010594
		public new string ToString()
		{
			string result;
			if (this.Date != DateTime.MinValue)
			{
				result = this.Date.ToString("yyyy-MM-dd");
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000115DC File Offset: 0x000105DC
		public void FromString(string s)
		{
			if (s.Length > 0)
			{
				try
				{
					this.Date = DateTime.Parse(s);
				}
				catch
				{
				}
			}
			this.Date = DateTime.MinValue;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00011630 File Offset: 0x00010630
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0001164C File Offset: 0x0001064C
		public DateTime Date
		{
			get
			{
				int num;
				return this.ParseOnscreen(out num);
			}
			set
			{
				this.txt_day.Text = value.ToString(this.displayFormatDay);
				this.txt_month.Text = value.ToString(this.displayFormatMonth);
				this.txt_year.Text = value.ToString(this.disaplyFormatYear);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000116A8 File Offset: 0x000106A8
		private int GetYearOnScreen(out int err)
		{
			int num = 0;
			string text = this.OnlyKeepDigits(this.txt_year.Text);
			int num2;
			if (text.Length < 1)
			{
				num2 = DateTime.Now.Year;
				num = 4;
			}
			else
			{
				num2 = int.Parse(text);
			}
			if (num2 < 50)
			{
				num2 += 2000;
			}
			else if (num2 < 100)
			{
				num2 += 1900;
			}
			err = num;
			return num2;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00011730 File Offset: 0x00010730
		private DateTime ParseOnscreen(out int encounteredError)
		{
			int num = 0;
			int num2;
			int yearOnScreen = this.GetYearOnScreen(out num2);
			num += num2;
			int num3;
			int monthOnScreen = this.GetMonthOnScreen(out num3);
			num += num3;
			string text = this.OnlyKeepDigits(this.txt_day.Text);
			int num4;
			if (text.Length > 0)
			{
				num4 = int.Parse(text);
			}
			else
			{
				num4 = DateTime.Now.Day;
				num++;
			}
			DateTime result;
			try
			{
				int num5 = DateTime.DaysInMonth(yearOnScreen, monthOnScreen);
				if (num4 < 1)
				{
					num++;
					num4 = 1;
				}
				else if (num4 > num5)
				{
					num4 = num5;
					num++;
				}
				encounteredError = num;
				result = new DateTime(yearOnScreen, monthOnScreen, num4);
			}
			catch
			{
				num += 8;
				encounteredError = num;
				result = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			}
			return result;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00011844 File Offset: 0x00010844
		public bool FilledIn
		{
			get
			{
				return this.Date != DateTime.MinValue;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00011868 File Offset: 0x00010868
		private int GetMonthOnScreen(out int err)
		{
			int num = 0;
			string text = this.txt_month.Text.Trim();
			string text2 = this.OnlyKeepDigits(text);
			int num2;
			if (text2.Length > 0)
			{
				num2 = int.Parse(text2);
			}
			else if (text.Length > 0)
			{
				text = text.ToLower();
				int num3 = Array.IndexOf<string>(this.months, text);
				if (num3 >= 0)
				{
					num2 = num3 + 1;
				}
				else
				{
					num2 = 0;
					for (int i = 0; i < this.months.Length; i++)
					{
						if (this.months[i].IndexOf(text) == 0)
						{
							num2 = i + 1;
							break;
						}
					}
					if (num2 <= 0)
					{
						num2 = DateTime.Now.Month;
						num += 2;
					}
				}
			}
			else
			{
				num2 = DateTime.Now.Month;
				num += 2;
			}
			err = num;
			return num2;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00011978 File Offset: 0x00010978
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

		// Token: 0x060001E3 RID: 483 RVA: 0x000119D8 File Offset: 0x000109D8
		private void txt_day_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '.' || e.KeyChar == '/' || e.KeyChar == '-')
			{
				base.ActiveControl = this.txt_month;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00011A1C File Offset: 0x00010A1C
		private void txt_month_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '.' || e.KeyChar == '/' || e.KeyChar == '-')
			{
				base.ActiveControl = this.txt_year;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00011A60 File Offset: 0x00010A60
		private void txt_year_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '.' || e.KeyChar == '/' || e.KeyChar == '-')
			{
				base.ActiveControl = this.txt_day;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00011AA4 File Offset: 0x00010AA4
		private void txt_day_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Right)
			{
				base.ActiveControl = this.txt_month;
			}
			else if (e.KeyCode == Keys.Left)
			{
				base.ActiveControl = this.txt_year;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00011AF0 File Offset: 0x00010AF0
		private void txt_month_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Right)
			{
				base.ActiveControl = this.txt_year;
			}
			else if (e.KeyCode == Keys.Left)
			{
				base.ActiveControl = this.txt_day;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00011B3C File Offset: 0x00010B3C
		private void txt_year_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Right)
			{
				base.ActiveControl = this.txt_day;
			}
			else if (e.KeyCode == Keys.Left)
			{
				base.ActiveControl = this.txt_month;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00011B88 File Offset: 0x00010B88
		private void MyDateTimePickerAcc_Validating(object sender, CancelEventArgs e)
		{
			int num;
			DateTime dateTime = this.ParseOnscreen(out num);
			if (num > 0)
			{
				string text = "";
				if (num % 1 == 1)
				{
					text = "day";
				}
				if (num % 2 == 2)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "month";
				}
				if (num % 4 == 4)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "year";
				}
				if (num % 8 == 8)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "unknown";
				}
				MessageBox.Show(string.Concat(new string[]
				{
					"There was a problem with the date you entered: ",
					text,
					".  The date has been corrected to: ",
					dateTime.ToString("MMMM dd, yyyy"),
					".  If this is not correct please use Shift-Tab to go back and change the date value to the desired date."
				}), "Date error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00011CBC File Offset: 0x00010CBC
		private void txt_month_Leave(object sender, EventArgs e)
		{
			int num;
			int monthOnScreen = this.GetMonthOnScreen(out num);
			DateTime dateTime = new DateTime(DateTime.Now.Year, monthOnScreen, 1);
			this.txt_month.Text = dateTime.ToString(this.displayFormatMonth);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00011D04 File Offset: 0x00010D04
		private void txt_day_Leave(object sender, EventArgs e)
		{
			string text = this.OnlyKeepDigits(this.txt_day.Text);
			if (text.Length < 1)
			{
				text = "1";
			}
			int num = int.Parse(text);
			if (num < 1)
			{
				num = 1;
			}
			else if (num > 31)
			{
				num = 31;
			}
			DateTime dateTime = new DateTime(DateTime.Now.Year, 12, num);
			this.txt_day.Text = dateTime.ToString(this.displayFormatDay);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00011D98 File Offset: 0x00010D98
		private void txt_year_Leave(object sender, EventArgs e)
		{
			int num;
			int yearOnScreen = this.GetYearOnScreen(out num);
			DateTime now = DateTime.Now;
			DateTime dateTime;
			try
			{
				dateTime = new DateTime(yearOnScreen, 1, 1);
			}
			catch
			{
				dateTime = DateTime.Now;
			}
			this.txt_year.Text = dateTime.ToString(this.disaplyFormatYear);
		}

		// Token: 0x040001C3 RID: 451
		private IContainer components = null;

		// Token: 0x040001C4 RID: 452
		private TextBox txt_day;

		// Token: 0x040001C5 RID: 453
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x040001C6 RID: 454
		private Label label3;

		// Token: 0x040001C7 RID: 455
		private Label label2;

		// Token: 0x040001C8 RID: 456
		private TextBox txt_year;

		// Token: 0x040001C9 RID: 457
		private TextBox txt_month;

		// Token: 0x040001CA RID: 458
		private Label label1;

		// Token: 0x040001CB RID: 459
		private string displayFormatMonth = "MMMM";

		// Token: 0x040001CC RID: 460
		private string displayFormatDay = "dd";

		// Token: 0x040001CD RID: 461
		private string disaplyFormatYear = "yyyy";

		// Token: 0x040001CE RID: 462
		private string[] months = new string[]
		{
			"january",
			"february",
			"march",
			"april",
			"may",
			"june",
			"july",
			"august",
			"september",
			"october",
			"november",
			"december"
		};
	}
}
