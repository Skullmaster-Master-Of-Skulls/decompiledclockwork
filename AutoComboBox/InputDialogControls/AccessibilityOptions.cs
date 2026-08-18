using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox.InputDialogControls
{
	// Token: 0x020000E2 RID: 226
	public partial class AccessibilityOptions : Form
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x00043B92 File Offset: 0x00042B92
		public AccessibilityOptions()
		{
			this.InitializeComponent();
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00043BAB File Offset: 0x00042BAB
		public AccessibilityOptions(AccessibilityOptions.OptionPages optionPage)
		{
			this.InitializeComponent();
			this.SetOptions(optionPage);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00043BCC File Offset: 0x00042BCC
		private void AccessibilityOptions_Load(object sender, EventArgs e)
		{
			base.ActiveControl = this.lv;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00043BDC File Offset: 0x00042BDC
		private void btn_fakeClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00043BE6 File Offset: 0x00042BE6
		private void btn_close_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00043BF0 File Offset: 0x00042BF0
		private void lv_SizeChanged(object sender, EventArgs e)
		{
			int num = this.lv.Width - SystemInformation.VerticalScrollBarWidth - this.lv.Columns[1].Width - 2;
			if (num > 0)
			{
				this.lv.Columns[0].Width = num;
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00043C50 File Offset: 0x00042C50
		private void SetOptions(AccessibilityOptions.OptionPages optionPage)
		{
			switch (optionPage)
			{
			case AccessibilityOptions.OptionPages.eScheduler:
				this.AddListViewItem("Move focus to the main ribbon toolbar menu (use right and left cursor keys and tab key to navigate once there)", "ALT (press and release)");
				this.AddListViewItem("Move between times on the calendar", "Cursor up, Cursor down");
				this.AddListViewItem("Move between days on the calendar", "Cursor left, Cursor right");
				this.AddListViewItem("Move between appointments on the calendar", "Tab, Shift+Tab");
				this.AddListViewItem("Go to today's date", "Home");
				this.AddListViewItem("Go forward or backward one week at a time", "Page up, Page down");
				this.AddListViewItem("Refresh schedule", "F5");
				this.AddListViewItem("Control+N", "Create a new appointment");
				this.AddListViewItem("Control+G", "Go-to a specified date");
				this.AddListViewItem("Edit the selected appointment", "ENTER");
				this.AddListViewItem("New student", "F8");
				this.AddListViewItem("Student info", "F2");
				this.AddListViewItem(Keys.Alt, Keys.F4, "Close ClockWork");
				break;
			case AccessibilityOptions.OptionPages.eAppointmentEdit:
				this.AddListViewItem("Cancelled (toggle)", "ALT+E");
				this.AddListViewItem("Private (toggle)", "ALT+V");
				this.AddListViewItem("Locked (toggle)", "ALT+K");
				this.AddListViewItem("Override colour\t", "ALT+O");
				this.AddListViewItem("View accommodations", "ALT+A");
				this.AddListViewItem("Calculate actual start/end times (tests)", "ALT+/");
				this.AddListViewItem("Options drop list", "ALT+P");
				this.AddListViewItem("Attendance / Fees", "ALT+F");
				this.AddListViewItem("Add active student", "ALT+I");
				this.AddListViewItem("Set course (Tests)", "ALT+.");
				this.AddListViewItem("Clear class writing time", "ALT+X");
				this.AddListViewItem("Add new student\t", "ALT+N");
				this.AddListViewItem("Save", "ALT+S");
				this.AddListViewItem("Close / cancel", "ALT+C");
				this.AddListViewItem("Multiple Bookings", "ALT+M");
				this.AddListViewItem("Add icon", "ALT++");
				this.AddListViewItem("Remove Icon", "ALT+-");
				this.AddListViewItem("Go to student list", "ALT+T");
				this.AddListViewItem("Go to staff list", "ALT+F");
				this.AddListViewItem("Mark student no-show", "ALT+W");
				this.AddListViewItem("Go to attendees list", "ALT+L");
				break;
			case AccessibilityOptions.OptionPages.eAccommodations:
				this.AddListViewItem(Keys.Alt, Keys.D1, "Go to the accommodations summary tab");
				this.AddListViewItem(Keys.Alt, Keys.D2, "Go to the accommodations template tab");
				this.AddListViewItem(Keys.Alt, Keys.D3, "Go to the Offline accommodations tab");
				this.AddListViewItem(Keys.Alt, Keys.D4, "Alt + numbers 4 - 9 will navigate to the course specific accommodations tab");
				this.AddListViewItem(Keys.Alt, Keys.G, "Generate letters and save");
				this.AddListViewItem(Keys.Alt, Keys.C, "Cancel and close this accommodations form");
				this.AddListViewItem(Keys.Alt, Keys.S, "Save without generating accommodation letters");
				break;
			case AccessibilityOptions.OptionPages.eAccommodationsGenerateLetterDialog:
				this.AddListViewItem(Keys.Alt, Keys.G, "Generate letters");
				this.AddListViewItem(Keys.Alt, Keys.E, "Toggle send via email");
				this.AddListViewItem(Keys.Alt, Keys.A, "Toggle send letter as attachment (send via email must be enabled)");
				this.AddListViewItem(Keys.Alt, Keys.N, "Toggle send single letter for all courses");
				this.AddListViewItem(Keys.Alt, Keys.C, "Cancel");
				break;
			case AccessibilityOptions.OptionPages.eAccommodationsSendEmails:
				this.AddListViewItem(Keys.Alt, Keys.A, "Send all pending emails for this student");
				this.AddListViewItem(Keys.Alt, Keys.C, "Cancel");
				break;
			case AccessibilityOptions.OptionPages.eSchedulerText:
				this.AddListViewItem("Move focus to the main ribbon toolbar menu (use right and left cursor keys and tab key to navigate once there)", "ALT (press and release)");
				this.AddListViewItem("Select a student", "F12");
				this.AddListViewItem("Search for a student", "F11");
				this.AddListViewItem("New student", "F8");
				this.AddListViewItem("Student info", "F2");
				this.AddListViewItem("Move between days on the calendar", "<cursor left>, <cursor right>");
				this.AddListViewItem("Move between days on the calendar (alternate)", "<page up>, <page down>");
				this.AddListViewItem("Move between appointments for the day", "<cursor up>, <cursor down>");
				this.AddListViewItem("Go to today's date", "<home>");
				this.AddListViewItem("Go to a specific day", "Control+G");
				this.AddListViewItem("Create a new appointment", "Control+N");
				this.AddListViewItem("Create a new point of contact", "Alt+P");
				this.AddListViewItem("Edit currently selected appointment", "<enter>");
				this.AddListViewItem("View another person's schedule", "ALT+V");
				this.AddListViewItem("Back to my schedule", "ALT+B");
				this.AddListViewItem("Delete selected appointment", "<delete>");
				this.AddListViewItem("Cancel selected appointment", "ALT+L");
				this.AddListViewItem("No-show selected appointment", "ALT+N");
				this.AddListViewItem("Hide / unhide cancelled appointments", "ALT+H");
				this.AddListViewItem("Manually refresh schedule", "F5");
				this.AddListViewItem("Move forward by one week on the calendar", "CTRL+RIGHT");
				this.AddListViewItem("Move backward by one week on the calendar", "CTRL+LEFT");
				this.AddListViewItem("Move forward by one month on the calendar", "CTRL+PAGEUP");
				this.AddListViewItem("Move backward by one month on the calendar", "CTRL+PAGEDOWN");
				this.AddListViewItem("Move forward by one year on the calendar", "CTRL+HOME");
				this.AddListViewItem("Move backward by one year on the calendar", "CTRL+END");
				this.AddListViewItem(Keys.Alt, Keys.F4, "Close ClockWork");
				break;
			case AccessibilityOptions.OptionPages.eAppointmentEditText:
				this.AddListViewItem("View appointment details tab", "ALT+P");
				this.AddListViewItem("View multiple bookings tab", "ALT+M");
				this.AddListViewItem("View assessment notes tab", "ALT+N");
				this.AddListViewItem("Add an attendee (Appointment details tab only)", "ALT+A");
				this.AddListViewItem("Remove selected attendee (Appointment details tab only)", "ALT+R");
				this.AddListViewItem("Add me (Appointment details tab only)", "ALT+D");
				this.AddListViewItem("Toggle selected attendee no-show status (Appointment details tab only)", "ALT+W");
				this.AddListViewItem("Toggle appointment status private (Appointment details tab only)", "ALT+V");
				this.AddListViewItem("Toggle appointment status cancelled (Appointment details tab only)", "ALT+E");
				this.AddListViewItem("Toggle appointment status locked (Appointment details tab only)", "ALT+K");
				this.AddListViewItem("Add multiple date (Multiple bookings tab only)", "ALT+A");
				this.AddListViewItem("Remove selected multiple date (Multiple bookings tab only)", "ALT+R");
				this.AddListViewItem("Clear all multiple dates (Multiple bookings tab only)", "ALT+L");
				this.AddListViewItem("Generate multiple dates and add them to the list (Multiple bookings tab only)", "ALT+G");
				this.AddListViewItem("Save appointment changes", "ALT+S");
				this.AddListViewItem("Cancel appointment changes", "ALT+C");
				break;
			case AccessibilityOptions.OptionPages.eTpEmailerMain:
				this.AddListViewItem(Keys.Control, Keys.M, "Move focus to the main toolbar (use right and left cursor keys to navigate once there)");
				this.AddListViewItem(Keys.Alt, Keys.S, "Send email and close this dialog");
				this.AddListViewItem(Keys.Alt, Keys.N, "Send email without closing this dialog");
				this.AddListViewItem(Keys.Alt, Keys.A, "Attach a file");
				this.AddListViewItem(Keys.Alt, Keys.P, "Print");
				this.AddListViewItem(Keys.Alt, Keys.C, "Close");
				this.AddListViewItem(Keys.Control, Keys.Add, "Increase font size");
				this.AddListViewItem(Keys.Control, Keys.Subtract, "Decrease font size");
				this.AddListViewItem(Keys.Control, Keys.D0, "Revert font size back to default");
				break;
			case AccessibilityOptions.OptionPages.eTasksForm:
				this.AddListViewItem(Keys.Control, Keys.M, "Move focus to the main toolbar (use right and left cursor keys to navigate once there)");
				this.AddListViewItem(Keys.Alt, Keys.A, "View tasks in all categories checkbox toggle");
				this.AddListViewItem(Keys.Alt, Keys.N, "Create new task");
				this.AddListViewItem(Keys.Alt, Keys.M, "Mark selected task(s) completed");
				this.AddListViewItem(Keys.Alt, Keys.D, "Delete selected task(s)");
				this.AddListViewItem(Keys.Alt, Keys.P, "Print tasks");
				this.AddListViewItem(Keys.Alt, Keys.S, "Save");
				this.AddListViewItem(Keys.Alt, Keys.C, "Close");
				this.AddListViewItem(Keys.Alt, Keys.T, "Create new task category");
				this.AddListViewItem(Keys.Alt, Keys.E, "Edit selected task category");
				this.AddListViewItem(Keys.Alt, Keys.L, "Delete selected task category");
				break;
			case AccessibilityOptions.OptionPages.eStudentInfo:
				this.AddListViewItem(Keys.Control, Keys.G, "Go to a tab");
				this.AddListViewItem("Move focus to the main ribbon toolbar menu (use right and left cursor keys and tab key to navigate once there)", "ALT (press and release)");
				this.AddListViewItem(Keys.Alt, Keys.Y, "Go to summary tab");
				this.AddListViewItem(Keys.Alt, Keys.R, "Go to courses tab");
				this.AddListViewItem(Keys.Alt, Keys.S, "Save all");
				this.AddListViewItem(Keys.Alt, Keys.C, "Close student info");
				this.AddListViewItem(Keys.Control, Keys.Tab, "Switch between open child windows in ClockWork");
				this.AddListViewItem("New student", "F8");
				this.AddListViewItem("Student info", "F2");
				break;
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00044520 File Offset: 0x00043520
		private ListViewItem AddListViewItem(string description, string command)
		{
			ListViewItem listViewItem = new ListViewItem(description);
			listViewItem.SubItems.Add(command);
			this.lv.Items.Add(listViewItem);
			return listViewItem;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0004455C File Offset: 0x0004355C
		private ListViewItem AddListViewItem(Keys functionKeyCode, Keys keyCode, string description)
		{
			ListViewItem listViewItem = new ListViewItem(description);
			AccessibilityOptions.ShortcutKey shortcutKey = new AccessibilityOptions.ShortcutKey(functionKeyCode, keyCode);
			listViewItem.SubItems.Add(shortcutKey.ToString());
			this.lv.Items.Add(listViewItem);
			return listViewItem;
		}

		// Token: 0x020000E3 RID: 227
		public enum OptionPages
		{
			// Token: 0x04000658 RID: 1624
			eUnknown,
			// Token: 0x04000659 RID: 1625
			eScheduler,
			// Token: 0x0400065A RID: 1626
			eAppointmentEdit,
			// Token: 0x0400065B RID: 1627
			ePerStudent,
			// Token: 0x0400065C RID: 1628
			ePerAppointment,
			// Token: 0x0400065D RID: 1629
			eAccommodations,
			// Token: 0x0400065E RID: 1630
			eAccommodationsGenerateLetterDialog,
			// Token: 0x0400065F RID: 1631
			eAccommodationsSendEmails,
			// Token: 0x04000660 RID: 1632
			eTests,
			// Token: 0x04000661 RID: 1633
			eAppSearch,
			// Token: 0x04000662 RID: 1634
			eSchedulerText,
			// Token: 0x04000663 RID: 1635
			eAppointmentEditText,
			// Token: 0x04000664 RID: 1636
			eTpEmailerMain,
			// Token: 0x04000665 RID: 1637
			eTasksForm,
			// Token: 0x04000666 RID: 1638
			eStudentInfo
		}

		// Token: 0x020000E4 RID: 228
		private enum CommonShortcut
		{
			// Token: 0x04000668 RID: 1640
			Control_M_Options,
			// Token: 0x04000669 RID: 1641
			Alt_S_Save,
			// Token: 0x0400066A RID: 1642
			Alt_C_Close
		}

		// Token: 0x020000E5 RID: 229
		internal class ShortcutKey
		{
			// Token: 0x170001CB RID: 459
			// (get) Token: 0x060008DC RID: 2268 RVA: 0x00044A6C File Offset: 0x00043A6C
			// (set) Token: 0x060008DD RID: 2269 RVA: 0x00044A84 File Offset: 0x00043A84
			public char KeyCode
			{
				get
				{
					return this.keyCode;
				}
				set
				{
					this.keyCode = value;
				}
			}

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x060008DE RID: 2270 RVA: 0x00044A90 File Offset: 0x00043A90
			public bool Ctrl
			{
				get
				{
					return this.ctrl;
				}
			}

			// Token: 0x170001CD RID: 461
			// (get) Token: 0x060008DF RID: 2271 RVA: 0x00044AA8 File Offset: 0x00043AA8
			public bool Shift
			{
				get
				{
					return this.shift;
				}
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00044AC0 File Offset: 0x00043AC0
			public bool Alt
			{
				get
				{
					return this.alt;
				}
			}

			// Token: 0x060008E1 RID: 2273 RVA: 0x00044AD8 File Offset: 0x00043AD8
			public ShortcutKey(Keys functionKeyCode, Keys keyCode)
			{
				this.keyCodeKey = keyCode;
				this.keyCode = (char)keyCode;
				this.ctrl = (functionKeyCode == Keys.Control);
				this.alt = (functionKeyCode == Keys.Alt);
				this.shift = (functionKeyCode == Keys.Shift);
			}

			// Token: 0x060008E2 RID: 2274 RVA: 0x00044B28 File Offset: 0x00043B28
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.ctrl)
				{
					stringBuilder.Append("Control+");
				}
				if (this.alt)
				{
					stringBuilder.Append("ALT+");
				}
				if (this.shift)
				{
					stringBuilder.Append("Shift+");
				}
				if (this.keyCodeKey == Keys.F4)
				{
					stringBuilder.Append("F4");
				}
				else
				{
					stringBuilder.Append(this.keyCode);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0400066B RID: 1643
			private char keyCode;

			// Token: 0x0400066C RID: 1644
			private bool ctrl;

			// Token: 0x0400066D RID: 1645
			private bool alt;

			// Token: 0x0400066E RID: 1646
			private bool shift;

			// Token: 0x0400066F RID: 1647
			private Keys keyCodeKey;
		}
	}
}
