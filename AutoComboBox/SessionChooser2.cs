using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000D3 RID: 211
	public class SessionChooser2 : UserControl
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0003F040 File Offset: 0x0003E040
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x0003F05D File Offset: 0x0003E05D
		public bool ShowAllSessionsVisible
		{
			get
			{
				return this.chk_allSessions.Visible;
			}
			set
			{
				this.chk_allSessions.Visible = value;
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0003F070 File Offset: 0x0003E070
		public SessionChooser2()
		{
			this.InitializeComponent();
			this.sessions = new DataTable();
			this.dtpNowAdjusted = DateTime.Now;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0003F0D4 File Offset: 0x0003E0D4
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x0003F0F1 File Offset: 0x0003E0F1
		public bool AllSessionsEnabled
		{
			get
			{
				return this.chk_allSessions.Visible;
			}
			set
			{
				this.chk_allSessions.Visible = value;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0003F104 File Offset: 0x0003E104
		public void Init(DataTable sessions, DateTime dtpNowAdjusted, bool allowedToChangeDefaultForEveryone)
		{
			this.sessions = sessions;
			this.dtpNowAdjusted = dtpNowAdjusted;
			this.SetSession(dtpNowAdjusted, true);
			if (allowedToChangeDefaultForEveryone)
			{
				this.lbl_spacer.Visible = true;
				this.btn_changeDefaultForEveryone.Visible = true;
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0003F150 File Offset: 0x0003E150
		public void SetTermForSession(string sessionTerm)
		{
			if (!string.IsNullOrEmpty(sessionTerm))
			{
				this.lbl_sessionCaption.Text = char.ToUpper(sessionTerm[0]) + sessionTerm.Substring(1);
				this.chk_allSessions.Text = string.Format("All {0}s", sessionTerm);
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0003F1AC File Offset: 0x0003E1AC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0003F1E8 File Offset: 0x0003E1E8
		private void InitializeComponent()
		{
			this.btn_sessionForward = new Button();
			this.btn_now = new Button();
			this.btn_sessionBack = new Button();
			this.lbl_session = new Label();
			this.panel3 = new Panel();
			this.lbl_sessionCaption = new Label();
			this.chk_allSessions = new CheckBox();
			this.btn_changeDefaultForEveryone = new Button();
			this.lbl_spacer = new Label();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.btn_sessionForward.AccessibleDescription = "Next session";
			this.btn_sessionForward.AccessibleName = "Next session";
			this.btn_sessionForward.Dock = DockStyle.Right;
			this.btn_sessionForward.FlatStyle = FlatStyle.System;
			this.btn_sessionForward.Font = new Font("Marlett", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 2);
			this.btn_sessionForward.Location = new Point(321, 15);
			this.btn_sessionForward.Name = "btn_sessionForward";
			this.btn_sessionForward.Size = new Size(30, 27);
			this.btn_sessionForward.TabIndex = 47;
			this.btn_sessionForward.Text = "4";
			this.btn_sessionForward.Click += this.btn_sessionForward_Click;
			this.btn_now.Dock = DockStyle.Right;
			this.btn_now.FlatStyle = FlatStyle.System;
			this.btn_now.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btn_now.Location = new Point(279, 15);
			this.btn_now.Name = "btn_now";
			this.btn_now.Size = new Size(42, 27);
			this.btn_now.TabIndex = 48;
			this.btn_now.Text = "No&w";
			this.btn_now.Click += this.btn_now_Click;
			this.btn_sessionBack.AccessibleDescription = "Previous session";
			this.btn_sessionBack.AccessibleName = "Previous session";
			this.btn_sessionBack.Dock = DockStyle.Right;
			this.btn_sessionBack.FlatStyle = FlatStyle.System;
			this.btn_sessionBack.Font = new Font("Marlett", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 2);
			this.btn_sessionBack.Location = new Point(249, 15);
			this.btn_sessionBack.Name = "btn_sessionBack";
			this.btn_sessionBack.Size = new Size(30, 27);
			this.btn_sessionBack.TabIndex = 46;
			this.btn_sessionBack.Text = "3";
			this.btn_sessionBack.Click += this.btn_sessionBack_Click;
			this.lbl_session.BackColor = SystemColors.Highlight;
			this.lbl_session.BorderStyle = BorderStyle.Fixed3D;
			this.lbl_session.Dock = DockStyle.Fill;
			this.lbl_session.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lbl_session.ForeColor = SystemColors.HighlightText;
			this.lbl_session.Location = new Point(0, 15);
			this.lbl_session.Name = "lbl_session";
			this.lbl_session.Size = new Size(249, 27);
			this.lbl_session.TabIndex = 45;
			this.lbl_session.Text = "Unknown";
			this.lbl_session.TextAlign = ContentAlignment.MiddleLeft;
			this.lbl_session.Click += this.lbl_session_Click;
			this.panel3.Controls.Add(this.lbl_sessionCaption);
			this.panel3.Controls.Add(this.chk_allSessions);
			this.panel3.Dock = DockStyle.Top;
			this.panel3.Location = new Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(351, 15);
			this.panel3.TabIndex = 44;
			this.lbl_sessionCaption.Dock = DockStyle.Fill;
			this.lbl_sessionCaption.Font = new Font("Arial", 9f, FontStyle.Italic, GraphicsUnit.Point, 0);
			this.lbl_sessionCaption.Location = new Point(0, 0);
			this.lbl_sessionCaption.Name = "lbl_sessionCaption";
			this.lbl_sessionCaption.Size = new Size(263, 15);
			this.lbl_sessionCaption.TabIndex = 7;
			this.lbl_sessionCaption.Text = "Session:";
			this.lbl_sessionCaption.TextAlign = ContentAlignment.BottomLeft;
			this.chk_allSessions.Dock = DockStyle.Right;
			this.chk_allSessions.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.chk_allSessions.Location = new Point(263, 0);
			this.chk_allSessions.Name = "chk_allSessions";
			this.chk_allSessions.Size = new Size(88, 15);
			this.chk_allSessions.TabIndex = 37;
			this.chk_allSessions.Text = "All Sessions";
			this.chk_allSessions.CheckedChanged += this.chk_allSessions_CheckedChanged;
			this.btn_changeDefaultForEveryone.AccessibleDescription = "Set this as default for everyone";
			this.btn_changeDefaultForEveryone.AccessibleName = "Set this as default for everyone";
			this.btn_changeDefaultForEveryone.Dock = DockStyle.Right;
			this.btn_changeDefaultForEveryone.Location = new Point(373, 0);
			this.btn_changeDefaultForEveryone.Name = "btn_changeDefaultForEveryone";
			this.btn_changeDefaultForEveryone.Size = new Size(52, 42);
			this.btn_changeDefaultForEveryone.TabIndex = 49;
			this.btn_changeDefaultForEveryone.Text = "Set as default";
			this.btn_changeDefaultForEveryone.UseVisualStyleBackColor = true;
			this.btn_changeDefaultForEveryone.Visible = false;
			this.btn_changeDefaultForEveryone.Click += this.btn_changeDefaultForEveryone_Click;
			this.lbl_spacer.Dock = DockStyle.Right;
			this.lbl_spacer.Location = new Point(351, 0);
			this.lbl_spacer.Name = "lbl_spacer";
			this.lbl_spacer.Size = new Size(22, 42);
			this.lbl_spacer.TabIndex = 50;
			this.lbl_spacer.Visible = false;
			base.Controls.Add(this.lbl_session);
			base.Controls.Add(this.btn_sessionBack);
			base.Controls.Add(this.btn_now);
			base.Controls.Add(this.btn_sessionForward);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.lbl_spacer);
			base.Controls.Add(this.btn_changeDefaultForEveryone);
			this.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Name = "SessionChooser2";
			base.Size = new Size(425, 42);
			this.panel3.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600080C RID: 2060 RVA: 0x0003F93C File Offset: 0x0003E93C
		// (remove) Token: 0x0600080D RID: 2061 RVA: 0x0003F978 File Offset: 0x0003E978
		public event SessionChooser2.SessionMoved MoveBack = null;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600080E RID: 2062 RVA: 0x0003F9B4 File Offset: 0x0003E9B4
		// (remove) Token: 0x0600080F RID: 2063 RVA: 0x0003F9F0 File Offset: 0x0003E9F0
		public event SessionChooser2.SessionMoved MoveForward = null;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000810 RID: 2064 RVA: 0x0003FA2C File Offset: 0x0003EA2C
		// (remove) Token: 0x06000811 RID: 2065 RVA: 0x0003FA68 File Offset: 0x0003EA68
		public event SessionChooser2.SessionMoved MoveNow = null;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000812 RID: 2066 RVA: 0x0003FAA4 File Offset: 0x0003EAA4
		// (remove) Token: 0x06000813 RID: 2067 RVA: 0x0003FAE0 File Offset: 0x0003EAE0
		public event SessionChooser2.SessionMoved AllSessionsCheckedChanged = null;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000814 RID: 2068 RVA: 0x0003FB1C File Offset: 0x0003EB1C
		// (remove) Token: 0x06000815 RID: 2069 RVA: 0x0003FB58 File Offset: 0x0003EB58
		public event SessionChooser2.SessionMoved DtpNowAdjustedChanged = null;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000816 RID: 2070 RVA: 0x0003FB94 File Offset: 0x0003EB94
		// (remove) Token: 0x06000817 RID: 2071 RVA: 0x0003FBD0 File Offset: 0x0003EBD0
		public event EventHandler SetAsDefaultForEveryone = null;

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0003FC0C File Offset: 0x0003EC0C
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x0003FC24 File Offset: 0x0003EC24
		public DateTime DtpNowAdjusted
		{
			get
			{
				return this.dtpNowAdjusted;
			}
			set
			{
				this.dtpNowAdjusted = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0003FC30 File Offset: 0x0003EC30
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0003FC48 File Offset: 0x0003EC48
		public DataTable Sessions
		{
			get
			{
				return this.sessions;
			}
			set
			{
				this.sessions = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0003FC54 File Offset: 0x0003EC54
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x0003FC71 File Offset: 0x0003EC71
		public bool AllSessionsChecked
		{
			get
			{
				return this.chk_allSessions.Checked;
			}
			set
			{
				this.chk_allSessions.Checked = value;
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0003FC84 File Offset: 0x0003EC84
		public void SetSession(DateTime newNowAdjusted, bool isInitial)
		{
			DateTime dateTime = this.dtpNowAdjusted;
			this.dtpNowAdjusted = newNowAdjusted;
			object[] startEndSessionDates = SessionChooser2.GetStartEndSessionDates(this.sessions, this.dtpNowAdjusted);
			this.lbl_session.Tag = startEndSessionDates;
			DateTime dateTime2 = (DateTime)startEndSessionDates[0];
			this.dtpNowAdjusted = dateTime2;
			if (startEndSessionDates[2] != null)
			{
				DataRow dataRow = (DataRow)startEndSessionDates[2];
				this.lbl_session.Text = (string)dataRow[1] + " " + dateTime2.Year.ToString();
			}
			else
			{
				this.lbl_session.Text = "Unknown " + dateTime2.Year.ToString();
			}
			if (!isInitial)
			{
				this.FireDtpNowAdjustedChanged(new SessionEventArgs(this.dtpNowAdjusted, this.dtpNowAdjusted));
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0003FD64 File Offset: 0x0003ED64
		private void FireSetAsDefaultForEveryone()
		{
			if (this.SetAsDefaultForEveryone != null)
			{
				this.SetAsDefaultForEveryone(this, new EventArgs());
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0003FD94 File Offset: 0x0003ED94
		private void FireDtpNowAdjustedChanged(SessionEventArgs e)
		{
			if (this.DtpNowAdjustedChanged != null)
			{
				this.DtpNowAdjustedChanged(this, e);
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0003FDC0 File Offset: 0x0003EDC0
		private void FireMoveBack(SessionEventArgs e)
		{
			if (this.MoveBack != null)
			{
				this.MoveBack(this, e);
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0003FDEC File Offset: 0x0003EDEC
		private void FireMoveForward(SessionEventArgs e)
		{
			if (this.MoveForward != null)
			{
				this.MoveForward(this, e);
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0003FE18 File Offset: 0x0003EE18
		private void FireMoveNow(SessionEventArgs e)
		{
			if (this.MoveNow != null)
			{
				this.MoveNow(this, e);
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0003FE44 File Offset: 0x0003EE44
		private void FireAllSessionsCheckedChanged(SessionEventArgs e)
		{
			if (this.AllSessionsCheckedChanged != null)
			{
				this.AllSessionsCheckedChanged(this, e);
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0003FE70 File Offset: 0x0003EE70
		public static object[] GetStartEndSessionDates(DataTable sessions, DateTime nowAdjusted)
		{
			int month = nowAdjusted.Month;
			int day = nowAdjusted.Day;
			int year = nowAdjusted.Year;
			DateTime dateTime = nowAdjusted;
			DateTime dateTime2 = nowAdjusted;
			DataRow dataRow = null;
			foreach (object obj in sessions.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				int num = (int)dataRow2[2];
				int num2 = (int)dataRow2[4];
				int num3 = (int)dataRow2[3];
				int num4 = (int)dataRow2[5];
				if (num2 < num)
				{
					if (month >= num && day >= num3)
					{
						dateTime = new DateTime(year, num, num3, 0, 0, 0);
						dateTime2 = new DateTime(year + 1, num2, num4, 23, 59, 59);
						dataRow = dataRow2;
						break;
					}
					if (month <= num2 && day <= num4)
					{
						dateTime = new DateTime(year - 1, num, num3, 0, 0, 0);
						dateTime2 = new DateTime(year, num2, num4, 23, 59, 59);
						dataRow = dataRow2;
						break;
					}
				}
				else
				{
					DateTime t = new DateTime(2000, month, day);
					DateTime t2 = new DateTime(2000, num, num3);
					DateTime t3 = new DateTime(2000, num2, num4);
					if (t >= t2 && t <= t3)
					{
						dateTime = new DateTime(year, num, num3, 0, 0, 0);
						dateTime2 = new DateTime(year, num2, num4, 23, 59, 59);
						dataRow = dataRow2;
						break;
					}
				}
			}
			return new object[]
			{
				dateTime,
				dateTime2,
				dataRow
			};
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00040084 File Offset: 0x0003F084
		private void btn_now_Click(object sender, EventArgs e)
		{
			DateTime oldDtpNowAdjusted = this.dtpNowAdjusted;
			this.SetSession(DateTime.Now, false);
			this.FireMoveNow(new SessionEventArgs(oldDtpNowAdjusted, this.dtpNowAdjusted));
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000400BC File Offset: 0x0003F0BC
		private void btn_sessionForward_Click(object sender, EventArgs e)
		{
			if (this.lbl_session.Tag != null)
			{
				DateTime oldDtpNowAdjusted = this.dtpNowAdjusted;
				object[] array = (object[])this.lbl_session.Tag;
				DataRow dataRow = (DataRow)array[2];
				this.dtpNowAdjusted = ((DateTime)array[1]).AddDays(1.0);
				this.SetSession(this.dtpNowAdjusted, false);
				this.FireMoveForward(new SessionEventArgs(oldDtpNowAdjusted, this.dtpNowAdjusted));
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00040144 File Offset: 0x0003F144
		private void btn_sessionBack_Click(object sender, EventArgs e)
		{
			if (this.lbl_session.Tag != null)
			{
				DateTime oldDtpNowAdjusted = this.dtpNowAdjusted;
				object[] array = (object[])this.lbl_session.Tag;
				DataRow dataRow = (DataRow)array[2];
				this.dtpNowAdjusted = ((DateTime)array[0]).AddDays(-1.0);
				this.SetSession(this.dtpNowAdjusted, false);
				this.FireMoveBack(new SessionEventArgs(oldDtpNowAdjusted, this.dtpNowAdjusted));
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000401CC File Offset: 0x0003F1CC
		public DateTime[] GetCurrentSessionDates()
		{
			DateTime[] result;
			if (this.lbl_session.Tag != null)
			{
				object[] array = (object[])this.lbl_session.Tag;
				result = new DateTime[]
				{
					(DateTime)array[0],
					(DateTime)array[1]
				};
			}
			else
			{
				result = new DateTime[]
				{
					this.dtpNowAdjusted,
					this.dtpNowAdjusted
				};
			}
			return result;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00040264 File Offset: 0x0003F264
		public DateTime GetCurrentSessionStartDate()
		{
			DateTime[] currentSessionDates = this.GetCurrentSessionDates();
			return currentSessionDates[0];
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0004028C File Offset: 0x0003F28C
		public DateTime GetCurrentSessionEndDate()
		{
			DateTime[] currentSessionDates = this.GetCurrentSessionDates();
			return currentSessionDates[1];
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000402B4 File Offset: 0x0003F2B4
		private void chk_allSessions_CheckedChanged(object sender, EventArgs e)
		{
			bool enabled = !this.chk_allSessions.Checked;
			this.lbl_session.Enabled = enabled;
			this.btn_now.Enabled = enabled;
			this.btn_sessionBack.Enabled = enabled;
			this.btn_sessionForward.Enabled = enabled;
			this.FireAllSessionsCheckedChanged(new SessionEventArgs(this.dtpNowAdjusted, this.dtpNowAdjusted));
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0004031D File Offset: 0x0003F31D
		private void lbl_session_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00040320 File Offset: 0x0003F320
		private void btn_changeDefaultForEveryone_Click(object sender, EventArgs e)
		{
			this.FireSetAsDefaultForEveryone();
		}

		// Token: 0x04000607 RID: 1543
		private Container components = null;

		// Token: 0x04000608 RID: 1544
		private Button btn_sessionForward;

		// Token: 0x04000609 RID: 1545
		private Button btn_now;

		// Token: 0x0400060A RID: 1546
		private Button btn_sessionBack;

		// Token: 0x0400060B RID: 1547
		private Label lbl_session;

		// Token: 0x0400060C RID: 1548
		private Panel panel3;

		// Token: 0x0400060D RID: 1549
		private CheckBox chk_allSessions;

		// Token: 0x0400060E RID: 1550
		private Label lbl_sessionCaption;

		// Token: 0x04000615 RID: 1557
		private DateTime dtpNowAdjusted;

		// Token: 0x04000616 RID: 1558
		private Button btn_changeDefaultForEveryone;

		// Token: 0x04000617 RID: 1559
		private Label lbl_spacer;

		// Token: 0x04000618 RID: 1560
		private DataTable sessions;

		// Token: 0x020000D4 RID: 212
		// (Invoke) Token: 0x06000830 RID: 2096
		public delegate void SessionMoved(object sender, SessionEventArgs e);
	}
}
