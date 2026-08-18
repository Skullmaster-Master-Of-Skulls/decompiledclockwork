using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AutoComboBox.HelperForms;
using AutoComboBox.Properties;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.LookupCourses;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.MiscTableSettings;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.LookupCourses;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Settings;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000065 RID: 101
	public class MyDateTimePickerForAccommodationsExpiry : UserControl, MyDynamicControl
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0001C8BC File Offset: 0x0001B8BC
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0001C8D9 File Offset: 0x0001B8D9
		public DateTime Value
		{
			get
			{
				return this.dtp.Value;
			}
			set
			{
				this.dtp.Value = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0001C8EC File Offset: 0x0001B8EC
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0001C909 File Offset: 0x0001B909
		public string Title
		{
			get
			{
				return this.label1.Text;
			}
			set
			{
				this.label1.Text = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0001C91C File Offset: 0x0001B91C
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0001C939 File Offset: 0x0001B939
		public DateTimePickerFormat Format
		{
			get
			{
				return this.dtp.Format;
			}
			set
			{
				this.dtp.Format = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0001C94C File Offset: 0x0001B94C
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0001C969 File Offset: 0x0001B969
		public string CustomFormat
		{
			get
			{
				return this.dtp.CustomFormat;
			}
			set
			{
				this.dtp.CustomFormat = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0001C97C File Offset: 0x0001B97C
		public MyDateTimePicker Dtp
		{
			get
			{
				return this.dtp;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0001C994 File Offset: 0x0001B994
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0001C9AC File Offset: 0x0001B9AC
		public DateDefaultValue DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001C9B6 File Offset: 0x0001B9B6
		public MyDateTimePickerForAccommodationsExpiry()
		{
			this.InitializeComponent();
			this.defaultValue = DateDefaultValue.empty;
			this.dtp.Value = DateTime.MinValue;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001C9E8 File Offset: 0x0001B9E8
		string MyDynamicControl.ToString()
		{
			string result;
			if (this.FilledIn)
			{
				result = this.dtp.Value.ToString("yyyy-MM-dd");
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001CA27 File Offset: 0x0001BA27
		void MyDynamicControl.Refresh()
		{
			this.Refresh();
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0001CA34 File Offset: 0x0001BA34
		public bool FilledIn
		{
			get
			{
				return this.dtp.Value != DateTime.MinValue;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001CA5C File Offset: 0x0001BA5C
		public void FromString(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				DateTime value;
				if (DateTime.TryParse(s, out value))
				{
					this.dtp.Value = value;
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0001CA94 File Offset: 0x0001BA94
		public object ReportObject
		{
			get
			{
				object result;
				if (!this.FilledIn)
				{
					result = null;
				}
				else
				{
					result = this.dtp.Value;
				}
				return result;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001CAC4 File Offset: 0x0001BAC4
		private void btn_setEndOfCurrentSchoolYear_Click(object sender, EventArgs e)
		{
			this.dtp.Value = this.GetEndOfCurrentTerm();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001CAD9 File Offset: 0x0001BAD9
		private void btn_setEndOfSchoolYear_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001CADC File Offset: 0x0001BADC
		private DateTime GetEndOfCurrentTerm()
		{
			ISessionClientManager sessionClientManager = new SessionClientManager();
			SessionDTO currentSession = sessionClientManager.GetCurrentSession();
			return currentSession.EndDate;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001CB04 File Offset: 0x0001BB04
		private DateTime GetEndOfCurrentSchoolYear()
		{
			ISessionClientManager sessionClientManager = new SessionClientManager();
			SessionDTO sessionDTO = sessionClientManager.GetCurrentSession().Clone();
			DateTime now = DateTime.Now;
			for (int i = 0; i < 5; i++)
			{
				DateTime t = new DateTime(now.Year, 2, 1);
				if (sessionDTO.StartDate <= t)
				{
					return sessionDTO.EndDate;
				}
				sessionDTO = sessionClientManager.AddSession(sessionDTO, 1);
			}
			return sessionDTO.EndDate;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001CB84 File Offset: 0x0001BB84
		public void ClearDate()
		{
			this.dtp.Value = DateTime.MinValue;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001CB98 File Offset: 0x0001BB98
		public void SetDateToEndOfCurrentTerm()
		{
			this.dtp.Value = this.GetEndOfCurrentTerm();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001CBAD File Offset: 0x0001BBAD
		public void SetDateToEndOfCurrentYear()
		{
			this.dtp.Value = this.GetEndOfCurrentSchoolYear();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001CBC4 File Offset: 0x0001BBC4
		private void btn_userPresetDate_Click(object sender, EventArgs e)
		{
			IList<DateTime> presetDates = this.GetPresetDates();
			if (presetDates.Count < 1)
			{
				DialogResult dialogResult = MessageBox.Show("There are no preset dates currently stored. Would you like to add a new preset date now?", "No preset dates available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					this.EditPresetDates();
				}
			}
			else
			{
				this.FillPresetDatesContextMenu(presetDates);
				this.cms_presetDates.Show(this.btn_userPresetDate, this.btn_userPresetDate.PointToClient(Cursor.Position));
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001CC3C File Offset: 0x0001BC3C
		private void EditPresetDates()
		{
			FrmAccommodationExpiryPresetDatesEdit frmAccommodationExpiryPresetDatesEdit = new FrmAccommodationExpiryPresetDatesEdit();
			frmAccommodationExpiryPresetDatesEdit.Init(this.GetPresetDates());
			DialogResult dialogResult = frmAccommodationExpiryPresetDatesEdit.ShowDialog(base.TopLevelControl);
			if (dialogResult == DialogResult.OK)
			{
				IList<DateTime> selectedDates = frmAccommodationExpiryPresetDatesEdit.SelectedDates;
				this.SavePresetDates(selectedDates);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001CC84 File Offset: 0x0001BC84
		private IList<DateTime> GetPresetDates()
		{
			IMiscTableSettingsClientManagers miscTableSettingsClientManagers = new MiscTableSettingsClientManagers();
			LoadMiscSettingValueResp loadMiscSettingValueResp = miscTableSettingsClientManagers.LoadMiscSettingValue(new LoadMiscSettingValueReq
			{
				Code = 1256,
				WhoAmI = ClientCache.CurrentInstance.whoAmIId
			});
			string presetDatesStr = loadMiscSettingValueResp.Value ?? "";
			return this.ParsePresetDatesFromString(presetDatesStr);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001CD00 File Offset: 0x0001BD00
		private void SavePresetDates(IList<DateTime> presetDates)
		{
			IMiscTableSettingsClientManagers miscTableSettingsClientManagers = new MiscTableSettingsClientManagers();
			IMiscTableSettingsClientManagers miscTableSettingsClientManagers2 = miscTableSettingsClientManagers;
			SaveMiscSettingValueReq saveMiscSettingValueReq = new SaveMiscSettingValueReq();
			saveMiscSettingValueReq.Code = 1256;
			saveMiscSettingValueReq.WhoAmI = ClientCache.CurrentInstance.whoAmIId;
			saveMiscSettingValueReq.Value = string.Join("`", presetDates.ToList<DateTime>().ConvertAll<string>((DateTime g) => g.ToString("yyyy-MM-dd")).ToArray());
			miscTableSettingsClientManagers2.SaveMiscSettingValue(saveMiscSettingValueReq);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001CDAC File Offset: 0x0001BDAC
		private void FillPresetDatesContextMenu(IList<DateTime> presetDates)
		{
			this.cms_presetDates.SuspendLayout();
			try
			{
				this.cms_presetDates.Items.Clear();
				foreach (DateTime dateTime in presetDates)
				{
					ToolStripMenuItem mi = new ToolStripMenuItem(dateTime.ToString("MMMM d, yyyy"));
					mi.Tag = dateTime;
					mi.Click += delegate(object sender, EventArgs e)
					{
						this.dtp.Value = (DateTime)mi.Tag;
					};
					this.cms_presetDates.Items.Add(mi);
				}
			}
			finally
			{
				this.cms_presetDates.ResumeLayout();
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001CEB8 File Offset: 0x0001BEB8
		private IList<DateTime> ParsePresetDatesFromString(string presetDatesStr)
		{
			string[] array = presetDatesStr.Split(new char[]
			{
				'`'
			}, StringSplitOptions.RemoveEmptyEntries);
			List<DateTime> list = new List<DateTime>();
			DateTime now = DateTime.Now;
			int year = now.Year;
			foreach (string s in array)
			{
				DateTime dateTime;
				if (DateTime.TryParse(s, out dateTime))
				{
					DateTime dateTime2 = new DateTime(year, dateTime.Month, dateTime.Day);
					if (dateTime2 < now)
					{
						dateTime2 = new DateTime(year + 1, dateTime.Month, dateTime.Day);
					}
					list.Add(dateTime2);
				}
			}
			list.Sort((DateTime d1, DateTime d2) => d1.CompareTo(d2));
			return list;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001CFA4 File Offset: 0x0001BFA4
		private void btn_previousTerm_Click(object sender, EventArgs e)
		{
			ISessionClientManager sessionClientManager = new SessionClientManager();
			DateTime value = this.dtp.Value;
			SessionDTO sessionByDate = sessionClientManager.GetSessionByDate(value);
			SessionDTO sessionDTO = sessionClientManager.SubtractSession(sessionByDate, 1);
			this.dtp.Value = sessionDTO.EndDate;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001CFE8 File Offset: 0x0001BFE8
		private void btn_nextTerm_Click(object sender, EventArgs e)
		{
			ISessionClientManager sessionClientManager = new SessionClientManager();
			DateTime value = this.dtp.Value;
			SessionDTO sessionByDate = sessionClientManager.GetSessionByDate(value);
			SessionDTO sessionDTO = sessionClientManager.AddSession(sessionByDate, 1);
			this.dtp.Value = sessionDTO.EndDate;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001D02B File Offset: 0x0001C02B
		private void btn_editPresetDates_Click(object sender, EventArgs e)
		{
			this.EditPresetDates();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001D038 File Offset: 0x0001C038
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001D070 File Offset: 0x0001C070
		private void InitializeComponent()
		{
			this.components = new Container();
			this.dtp = new MyDateTimePicker();
			this.label1 = new Label();
			this.btn_setEndOfCurrentSchoolYear = new Button();
			this.btn_userPresetDate = new Button();
			this.toolTip1 = new ToolTip(this.components);
			this.btn_previousTerm = new Button();
			this.btn_nextTerm = new Button();
			this.btn_editPresetDates = new Button();
			this.cms_presetDates = new ContextMenuStrip(this.components);
			base.SuspendLayout();
			this.dtp.BaseValue = new DateTime(2010, 4, 25, 9, 25, 47, 932);
			this.dtp.CalcButtonCid = 0;
			this.dtp.CustomFormat = "MMMM dd, yyyy";
			this.dtp.DefaultCustomFormat = null;
			this.dtp.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.dtp.Format = DateTimePickerFormat.Custom;
			this.dtp.GreyedOut = false;
			this.dtp.Location = new Point(3, 27);
			this.dtp.Margin = new Padding(3, 4, 3, 4);
			this.dtp.Name = "dtp";
			this.dtp.Size = new Size(180, 22);
			this.dtp.TabIndex = 1;
			this.dtp.Value = new DateTime(2010, 4, 25, 9, 25, 47, 932);
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(0, 8);
			this.label1.Name = "label1";
			this.label1.Size = new Size(183, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Accommodations will expiry on:";
			this.btn_setEndOfCurrentSchoolYear.AccessibleName = "Set to end of current term";
			this.btn_setEndOfCurrentSchoolYear.Location = new Point(222, 3);
			this.btn_setEndOfCurrentSchoolYear.Name = "btn_setEndOfCurrentSchoolYear";
			this.btn_setEndOfCurrentSchoolYear.Size = new Size(86, 22);
			this.btn_setEndOfCurrentSchoolYear.TabIndex = 3;
			this.btn_setEndOfCurrentSchoolYear.Text = "End of term";
			this.btn_setEndOfCurrentSchoolYear.UseVisualStyleBackColor = true;
			this.btn_setEndOfCurrentSchoolYear.Click += this.btn_setEndOfCurrentSchoolYear_Click;
			this.btn_userPresetDate.AccessibleName = "Use a preset date";
			this.btn_userPresetDate.Location = new Point(189, 27);
			this.btn_userPresetDate.Name = "btn_userPresetDate";
			this.btn_userPresetDate.Size = new Size(119, 22);
			this.btn_userPresetDate.TabIndex = 5;
			this.btn_userPresetDate.Text = "Use preset date";
			this.btn_userPresetDate.UseVisualStyleBackColor = true;
			this.btn_userPresetDate.Click += this.btn_userPresetDate_Click;
			this.btn_previousTerm.AccessibleName = "Set to end date of previous term";
			this.btn_previousTerm.Image = Resources.arrow_left_blue;
			this.btn_previousTerm.Location = new Point(189, 3);
			this.btn_previousTerm.Name = "btn_previousTerm";
			this.btn_previousTerm.Size = new Size(27, 22);
			this.btn_previousTerm.TabIndex = 2;
			this.toolTip1.SetToolTip(this.btn_previousTerm, "Set to end date of previous term");
			this.btn_previousTerm.UseVisualStyleBackColor = true;
			this.btn_previousTerm.Click += this.btn_previousTerm_Click;
			this.btn_nextTerm.AccessibleName = "Set to end date of next term";
			this.btn_nextTerm.Image = Resources.arrow_right_blue;
			this.btn_nextTerm.Location = new Point(313, 3);
			this.btn_nextTerm.Name = "btn_nextTerm";
			this.btn_nextTerm.Size = new Size(27, 22);
			this.btn_nextTerm.TabIndex = 4;
			this.toolTip1.SetToolTip(this.btn_nextTerm, "Set to end date of next term");
			this.btn_nextTerm.UseVisualStyleBackColor = true;
			this.btn_nextTerm.Click += this.btn_nextTerm_Click;
			this.btn_editPresetDates.AccessibleName = "Edit preset dates";
			this.btn_editPresetDates.Location = new Point(313, 27);
			this.btn_editPresetDates.Name = "btn_editPresetDates";
			this.btn_editPresetDates.Size = new Size(27, 22);
			this.btn_editPresetDates.TabIndex = 6;
			this.btn_editPresetDates.Text = "...";
			this.toolTip1.SetToolTip(this.btn_editPresetDates, "Edit preset dates");
			this.btn_editPresetDates.UseVisualStyleBackColor = true;
			this.btn_editPresetDates.Click += this.btn_editPresetDates_Click;
			this.cms_presetDates.Name = "cms_presetDates";
			this.cms_presetDates.Size = new Size(153, 26);
			base.AutoScaleDimensions = new SizeF(6f, 14f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.BorderStyle = BorderStyle.Fixed3D;
			base.Controls.Add(this.btn_editPresetDates);
			base.Controls.Add(this.btn_previousTerm);
			base.Controls.Add(this.btn_nextTerm);
			base.Controls.Add(this.btn_userPresetDate);
			base.Controls.Add(this.btn_setEndOfCurrentSchoolYear);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.dtp);
			this.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "MyDateTimePickerForAccommodationsExpiry";
			base.Size = new Size(344, 55);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400036D RID: 877
		private DateDefaultValue defaultValue;

		// Token: 0x0400036E RID: 878
		private IContainer components = null;

		// Token: 0x0400036F RID: 879
		private MyDateTimePicker dtp;

		// Token: 0x04000370 RID: 880
		private Label label1;

		// Token: 0x04000371 RID: 881
		private Button btn_setEndOfCurrentSchoolYear;

		// Token: 0x04000372 RID: 882
		private Button btn_userPresetDate;

		// Token: 0x04000373 RID: 883
		private Button btn_nextTerm;

		// Token: 0x04000374 RID: 884
		private ToolTip toolTip1;

		// Token: 0x04000375 RID: 885
		private Button btn_previousTerm;

		// Token: 0x04000376 RID: 886
		private Button btn_editPresetDates;

		// Token: 0x04000377 RID: 887
		private ContextMenuStrip cms_presetDates;
	}
}
