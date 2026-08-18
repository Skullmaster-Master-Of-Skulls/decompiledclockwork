using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x0200002E RID: 46
	public class MyTimePicker : UserControl
	{
		// Token: 0x06000150 RID: 336 RVA: 0x0000E6A9 File Offset: 0x0000D6A9
		public MyTimePicker()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000E6C4 File Offset: 0x0000D6C4
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000E6E1 File Offset: 0x0000D6E1
		public DateTime Value
		{
			get
			{
				return this.dateTimePicker1.Value;
			}
			set
			{
				this.dateTimePicker1.Value = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000E6F4 File Offset: 0x0000D6F4
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000E714 File Offset: 0x0000D714
		public int Hour
		{
			get
			{
				return this.Value.Hour;
			}
			set
			{
				DateTime value2 = this.Value;
				this.Value = new DateTime(value2.Year, value2.Month, value2.Day, value, value2.Minute, value2.Second);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000E75C File Offset: 0x0000D75C
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000E77C File Offset: 0x0000D77C
		public int Minute
		{
			get
			{
				return this.Value.Minute;
			}
			set
			{
				DateTime value2 = this.Value;
				this.Value = new DateTime(value2.Year, value2.Month, value2.Day, value2.Hour, value, value2.Second);
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000E7C4 File Offset: 0x0000D7C4
		public void AddMinutes(int minutes)
		{
			this.Value = this.Value.AddMinutes((double)minutes);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000E7EC File Offset: 0x0000D7EC
		public void SubtractMinutes(int minutes)
		{
			this.Value = this.Value.AddMinutes((double)(-(double)minutes));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000E814 File Offset: 0x0000D814
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000E84C File Offset: 0x0000D84C
		private void InitializeComponent()
		{
			this.dateTimePicker1 = new DateTimePicker();
			base.SuspendLayout();
			this.dateTimePicker1.CustomFormat = "h:mm tt";
			this.dateTimePicker1.Dock = DockStyle.Fill;
			this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new Point(0, 0);
			this.dateTimePicker1.Margin = new Padding(3, 4, 3, 4);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.ShowUpDown = true;
			this.dateTimePicker1.Size = new Size(105, 22);
			this.dateTimePicker1.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.dateTimePicker1);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "MyTimePicker";
			base.Size = new Size(105, 26);
			base.ResumeLayout(false);
		}

		// Token: 0x04000186 RID: 390
		private IContainer components = null;

		// Token: 0x04000187 RID: 391
		private DateTimePicker dateTimePicker1;
	}
}
