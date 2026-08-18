using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AutoComboBox.MyControls;

namespace AutoComboBox
{
	// Token: 0x0200009A RID: 154
	public class MyDateTimePicker : DateTimePicker, MyDynamicControl
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00030664 File Offset: 0x0002F664
		public object ReportObject
		{
			get
			{
				object result;
				if (this.Value == DateTime.MinValue)
				{
					result = null;
				}
				else
				{
					result = this.Value;
				}
				return result;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0003069C File Offset: 0x0002F69C
		public void InformCalcButtonOfChange()
		{
			if (this.calcButtonCid > 0)
			{
				Control parent = ListViewEx.GetParent(this);
				Control control = ListViewEx.FindControl(parent, this.calcButtonCid);
				if (control != null && control is MyDynamicControl)
				{
					MyDynamicControl myDynamicControl = (MyDynamicControl)control;
					myDynamicControl.Refresh();
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x000306F8 File Offset: 0x0002F6F8
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00030710 File Offset: 0x0002F710
		public int CalcButtonCid
		{
			get
			{
				return this.calcButtonCid;
			}
			set
			{
				this.calcButtonCid = value;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0003071C File Offset: 0x0002F71C
		public new string ToString()
		{
			DateTime value = this.Value;
			string result;
			if (value == DateTime.MinValue)
			{
				result = "";
			}
			else
			{
				result = this.Value.ToString("yyyy-MM-dd");
			}
			return result;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00030764 File Offset: 0x0002F764
		public void FromString(string s)
		{
			if (s.Trim().Length > 0)
			{
				try
				{
					this.Value = DateTime.Parse(s);
				}
				catch
				{
				}
			}
			else
			{
				this.Value = DateTime.MinValue;
			}
		}

		// Token: 0x1700013B RID: 315
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x000307C0 File Offset: 0x0002F7C0
		public MyCheckBox SyncedCheckbox
		{
			set
			{
				this.syncedCheckbox = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000307CC File Offset: 0x0002F7CC
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x000307E4 File Offset: 0x0002F7E4
		public bool GreyedOut
		{
			get
			{
				return this.greyedOut;
			}
			set
			{
				this.greyedOut = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x000307F0 File Offset: 0x0002F7F0
		public bool FilledIn
		{
			get
			{
				return this.Value != DateTime.MinValue;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00030814 File Offset: 0x0002F814
		public MyDateTimePicker()
		{
			this.InitializeComponent();
			this.oldCustomFormat = base.CustomFormat;
			this.oldFormat = base.Format;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000308B0 File Offset: 0x0002F8B0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.syncedCheckbox = null;
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000308F4 File Offset: 0x0002F8F4
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0003090C File Offset: 0x0002F90C
		public DateTime BaseValue
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00030918 File Offset: 0x0002F918
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0003092F File Offset: 0x0002F92F
		public string DefaultCustomFormat { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00030938 File Offset: 0x0002F938
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00030968 File Offset: 0x0002F968
		public new DateTime Value
		{
			get
			{
				DateTime result;
				if (this.bIsNull)
				{
					result = DateTime.MinValue;
				}
				else
				{
					result = base.Value;
				}
				return result;
			}
			set
			{
				if (value == DateTime.MinValue)
				{
					if (!this.bIsNull)
					{
						this.oldFormat = base.Format;
						this.oldCustomFormat = base.CustomFormat;
						this.bIsNull = true;
					}
					base.Format = DateTimePickerFormat.Custom;
					if (this.Focused)
					{
						base.CustomFormat = this.customFormatForNullHighlight;
					}
					else
					{
						base.CustomFormat = this.customFormatForNull;
					}
					base.OnValueChanged(new EventArgs());
				}
				else
				{
					if (this.bIsNull)
					{
						base.Format = this.oldFormat;
						base.CustomFormat = this.oldCustomFormat;
						this.bIsNull = false;
					}
					base.Value = value;
				}
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00030A2E File Offset: 0x0002FA2E
		protected override void OnValueChanged(EventArgs eventargs)
		{
			this.FireLeave();
			base.OnValueChanged(eventargs);
			this.InformCalcButtonOfChange();
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00030A48 File Offset: 0x0002FA48
		public void FireLeave()
		{
			if (this.syncedCheckbox != null)
			{
				this.syncedCheckbox.Checked = (this.Value != DateTime.MinValue);
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00030A84 File Offset: 0x0002FA84
		protected override void OnCloseUp(EventArgs eventargs)
		{
			if (Control.MouseButtons == MouseButtons.None)
			{
				if (this.bIsNull)
				{
					base.Format = this.oldFormat;
					base.CustomFormat = this.oldCustomFormat;
					this.bIsNull = false;
				}
			}
			base.OnCloseUp(eventargs);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00030ADC File Offset: 0x0002FADC
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.Value = DateTime.MinValue;
			}
			else if (e.KeyCode == Keys.Return)
			{
				bool flag = this.Value == DateTime.MinValue;
				if (flag)
				{
					this.Value = DateTime.Now;
					if (flag)
					{
						this.SimulateKeyPress(39);
					}
				}
			}
			else if (e.KeyCode == Keys.F10)
			{
				base.Format = DateTimePickerFormat.Short;
			}
			else if (e.KeyCode == Keys.F11)
			{
				base.Format = DateTimePickerFormat.Long;
			}
			else if (e.KeyCode == Keys.F12)
			{
				base.Format = DateTimePickerFormat.Custom;
			}
			else if (e.KeyCode == Keys.F5)
			{
				MessageBox.Show(this.currFocus.ToString());
			}
			if (!e.Handled)
			{
				base.OnKeyUp(e);
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00030BEC File Offset: 0x0002FBEC
		private void InitializeComponent()
		{
			base.CustomFormat = "MMMM dd, yyyy";
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Format = DateTimePickerFormat.Custom;
			base.Enter += this.MyDateTimePicker_Enter_1;
			base.KeyUp += this.MyDateTimePicker_KeyUp;
			base.Leave += this.MyDateTimePicker_Leave_1;
			base.KeyPress += this.MyDateTimePicker_KeyPress;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00030C73 File Offset: 0x0002FC73
		private void MyDateTimePicker_KeyUp(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00030C78 File Offset: 0x0002FC78
		private void MyDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
		{
			char c = e.KeyChar;
			c = char.ToLower(c);
			if (char.IsLetterOrDigit(c) && this.Value == DateTime.MinValue)
			{
				this.Value = DateTime.Now;
				this.SimulateKeyPress(39);
				this.SimulateKeyPress((byte)c);
			}
			else if (char.IsDigit(c) && char.IsLetter(this.lastChars[1]))
			{
				e.Handled = true;
				this.lastChars[0] = ' ';
				this.lastChars[1] = ' ';
				this.SimulateKeyPress(39);
				this.SimulateKeyPress((byte)c);
			}
			else if (char.IsLetter(c))
			{
				int num = 0;
				int month = this.Value.Month;
				bool handled = true;
				switch (c)
				{
				case 'a':
					if (this.lastChars[1] == 'j')
					{
						num = 1;
					}
					else if (this.lastChars[1] == 'a')
					{
						if (month == 4)
						{
							num = 8;
						}
						else
						{
							num = 4;
						}
					}
					else
					{
						num = 4;
					}
					goto IL_337;
				case 'd':
					num = 12;
					goto IL_337;
				case 'f':
					num = 2;
					goto IL_337;
				case 'j':
					if (this.lastChars[1] == 'j')
					{
						if (month == 1)
						{
							num = 6;
						}
						else if (month == 6)
						{
							num = 7;
						}
						else
						{
							num = 1;
						}
					}
					else
					{
						num = 1;
					}
					goto IL_337;
				case 'l':
					if (this.lastChars[0] == 'j' && this.lastChars[1] == 'u')
					{
						num = 7;
					}
					goto IL_337;
				case 'm':
					if (this.lastChars[1] == 'm')
					{
						if (month == 3)
						{
							num = 5;
						}
						else
						{
							num = 3;
						}
					}
					else
					{
						num = 3;
					}
					goto IL_337;
				case 'n':
					if (this.lastChars[0] == 'j' && this.lastChars[1] == 'u')
					{
						num = 6;
					}
					else
					{
						num = 11;
					}
					goto IL_337;
				case 'o':
					num = 10;
					goto IL_337;
				case 'p':
					if (this.lastChars[1] == 'a')
					{
						num = 4;
					}
					goto IL_337;
				case 'r':
					if (this.lastChars[0] == 'm' && this.lastChars[1] == 'a')
					{
						num = 3;
					}
					goto IL_337;
				case 's':
					num = 9;
					goto IL_337;
				case 'u':
					if (this.lastChars[1] == 'a')
					{
						num = 8;
					}
					else if (this.lastChars[1] == 'j')
					{
						if (month == 6)
						{
							num = 7;
						}
						else
						{
							num = 6;
						}
					}
					goto IL_337;
				case 'y':
					if (this.lastChars[0] == 'm' && this.lastChars[1] == 'a')
					{
						num = 5;
					}
					goto IL_337;
				}
				handled = false;
				IL_337:
				e.Handled = handled;
				if (num > 0)
				{
					if (this.Value == DateTime.MinValue)
					{
						this.Value = DateTime.Now;
					}
					try
					{
						this.Value = new DateTime(this.Value.Year, num, this.Value.Day, this.Value.Hour, this.Value.Minute, 0, 0);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
			this.lastChars[0] = this.lastChars[1];
			this.lastChars[1] = c;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00031088 File Offset: 0x00030088
		private void GotoPos(int pos)
		{
			int num = pos - this.currFocus;
			int num2;
			if (num < 0)
			{
				num2 = -num;
			}
			else
			{
				num2 = num;
			}
			for (int i = 0; i < num2; i++)
			{
				if (num < 0)
				{
					this.SimulateKeyPress(37);
				}
				else
				{
					this.SimulateKeyPress(39);
				}
			}
		}

		// Token: 0x060005F4 RID: 1524
		[DllImport("user32")]
		private static extern bool keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

		// Token: 0x060005F5 RID: 1525 RVA: 0x000310E2 File Offset: 0x000300E2
		public void SimulateKeyPress(byte KCC)
		{
			this.SimulateKeyDown(KCC);
			this.SimulateKeyUp(KCC);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000310F5 File Offset: 0x000300F5
		public void SimulateKeyUp(byte KCC)
		{
			MyDateTimePicker.keybd_event(KCC, 0, 2, 0);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00031102 File Offset: 0x00030102
		public void SimulateKeyDown(byte KCC)
		{
			MyDateTimePicker.keybd_event(KCC, 0, 0, 0);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0003110F File Offset: 0x0003010F
		private void MyDateTimePicker_Enter(object sender, EventArgs e)
		{
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00031112 File Offset: 0x00030112
		private void MyDateTimePicker_MouseUp(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00031115 File Offset: 0x00030115
		private void MyDateTimePicker_Leave(object sender, EventArgs e)
		{
			this.ResetFormat();
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00031120 File Offset: 0x00030120
		private void ResetFormat()
		{
			EventHandler method = new EventHandler(this.ResetFormat);
			if (base.Handle != IntPtr.Zero && base.IsHandleCreated)
			{
				base.BeginInvoke(method, new object[]
				{
					this,
					new EventArgs()
				});
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0003117C File Offset: 0x0003017C
		private void ResetFormat(object sender, EventArgs e)
		{
			if (this.Value != DateTime.MinValue)
			{
				base.SuspendLayout();
				DateTimePickerFormat format = base.Format;
				if (base.Format == DateTimePickerFormat.Short)
				{
					base.Format = DateTimePickerFormat.Long;
				}
				else
				{
					base.Format = DateTimePickerFormat.Short;
				}
				base.Format = format;
				base.ResumeLayout();
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000311E2 File Offset: 0x000301E2
		private void MyDateTimePicker_CloseUp(object sender, EventArgs e)
		{
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000311E8 File Offset: 0x000301E8
		private void MyDateTimePicker_Leave_1(object sender, EventArgs e)
		{
			if (this.Value == DateTime.MinValue)
			{
				base.CustomFormat = this.customFormatForNull;
				this.Refresh();
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00031224 File Offset: 0x00030224
		private void MyDateTimePicker_Enter_1(object sender, EventArgs e)
		{
			if (this.Value == DateTime.MinValue)
			{
				base.CustomFormat = this.customFormatForNullHighlight;
				this.Refresh();
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00031260 File Offset: 0x00030260
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			if (this.greyedOut)
			{
				SolidBrush brush = new SolidBrush(SystemColors.Highlight);
				pevent.Graphics.FillRectangle(brush, pevent.ClipRectangle);
			}
			else
			{
				base.OnPaintBackground(pevent);
			}
		}

		// Token: 0x040004C5 RID: 1221
		public const byte KEYEVENTF_EXTENDEDKEY = 1;

		// Token: 0x040004C6 RID: 1222
		public const byte KEYEVENTF_KEYUP = 2;

		// Token: 0x040004C7 RID: 1223
		private Container components = null;

		// Token: 0x040004C8 RID: 1224
		private int calcButtonCid = 0;

		// Token: 0x040004C9 RID: 1225
		private DateTimePickerFormat oldFormat = DateTimePickerFormat.Long;

		// Token: 0x040004CA RID: 1226
		private string oldCustomFormat = null;

		// Token: 0x040004CB RID: 1227
		private bool bIsNull = false;

		// Token: 0x040004CC RID: 1228
		private string customFormatForNull = " ";

		// Token: 0x040004CD RID: 1229
		private string customFormatForNullHighlight = "__ / __ / __";

		// Token: 0x040004CE RID: 1230
		private bool greyedOut = false;

		// Token: 0x040004CF RID: 1231
		private MyCheckBox syncedCheckbox = null;

		// Token: 0x040004D0 RID: 1232
		private int currFocus = 0;

		// Token: 0x040004D1 RID: 1233
		private char[] lastChars = new char[]
		{
			' ',
			' '
		};
	}
}
