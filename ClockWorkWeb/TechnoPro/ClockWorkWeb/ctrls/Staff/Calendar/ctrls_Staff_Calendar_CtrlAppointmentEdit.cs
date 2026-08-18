using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using Databases;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.TextFormat.Adapters;
using TechnoPro.Common.UI.ClientManager.Web.Core.Appointments;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Appointments;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Staff.Calendar
{
	// Token: 0x0200013E RID: 318
	public class ctrls_Staff_Calendar_CtrlAppointmentEdit : UserControl
	{
		// Token: 0x06000995 RID: 2453 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00043A44 File Offset: 0x00041C44
		public bool InitNew(DateTime startDateTime, DateTime endDateTime)
		{
			this.Init();
			this.ClearAppointment();
			this.dp_date.SelectedDate = new DateTime?(startDateTime.Date);
			this.tp_start.SelectedTime = new TimeSpan?(startDateTime.TimeOfDay);
			this.tp_end.SelectedTime = new TimeSpan?(endDateTime.TimeOfDay);
			return true;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00043AB0 File Offset: 0x00041CB0
		public bool InitEdit(object appointmentID)
		{
			this.Init();
			this.ClearAppointment();
			int num;
			int.TryParse((appointmentID == null) ? "" : appointmentID.ToString().Trim(), out num);
			this.appIdEnc.Value = Convert.ToBase64String(DatabaseLayerFactory.ClockWork.Encryption.Encrypt(num.ToString()));
			bool flag = num < 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
				AppointmentDTO appointmentDTO = appointmentClientManager.LoadAppointment(num);
				bool flag2 = appointmentDTO == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					this.txt_subTitle.Text = (appointmentDTO.SubTitle ?? "");
					this.txt_memo.Text = (string.IsNullOrEmpty(appointmentDTO.Memo) ? "" : appointmentDTO.Memo.ConvertRtfToPlainText().Trim());
					this.txt_location.Text = (appointmentDTO.Location ?? "");
					this.chk_cancelled.Checked = appointmentDTO.IsCancelled;
					this.chk_private.Checked = appointmentDTO.IsPrivate;
					this.dp_date.SelectedDate = new DateTime?(appointmentDTO.StartDateTime.Date);
					this.tp_start.SelectedDate = new DateTime?(appointmentDTO.StartDateTime);
					this.tp_end.SelectedDate = new DateTime?(appointmentDTO.EndDateTime);
					int num2 = (appointmentDTO.AppType == null) ? 0 : appointmentDTO.AppType.AppTypeId;
					this.cmb_appType.SelectedValue = num2.ToString();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00043C4C File Offset: 0x00041E4C
		private void ClearAppointment()
		{
			this.txt_subTitle.Text = "";
			this.txt_memo.Text = "";
			this.txt_location.Text = "";
			this.chk_cancelled.Checked = false;
			this.chk_private.Checked = false;
			this.dp_date.SelectedDate = null;
			this.tp_start.SelectedTime = null;
			this.tp_end.SelectedTime = null;
			this.cmb_appType.SelectedValue = "0";
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00043CF8 File Offset: 0x00041EF8
		private new void Init()
		{
			bool flag = !string.IsNullOrEmpty(this.setupCompleted.Value);
			if (!flag)
			{
				IAppointmentTypeWebClientManager appointmentTypeWebClientManager = new AppointmentTypeWebClientManager();
				IList<AppTypeDTO> dataSource = appointmentTypeWebClientManager.LoadAllowedAppTypes();
				this.cmb_appType.DataSource = dataSource;
				this.cmb_appType.DataBind();
				this.setupCompleted.Value = "1";
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600099A RID: 2458 RVA: 0x00043D58 File Offset: 0x00041F58
		// (remove) Token: 0x0600099B RID: 2459 RVA: 0x00043D90 File Offset: 0x00041F90
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<UserEventArgs> OnLoggedInUserPidRequested;

		// Token: 0x0600099C RID: 2460 RVA: 0x00043DC8 File Offset: 0x00041FC8
		private int LookupStaffPid()
		{
			EventHandler<UserEventArgs> onLoggedInUserPidRequested = this.OnLoggedInUserPidRequested;
			bool flag = onLoggedInUserPidRequested != null;
			int result;
			if (flag)
			{
				UserEventArgs userEventArgs = new UserEventArgs();
				onLoggedInUserPidRequested(this, userEventArgs);
				result = userEventArgs.PersonId;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00043E04 File Offset: 0x00042004
		protected void btn_save_Click(object sender, EventArgs e)
		{
			int num = 0;
			try
			{
				string text = this.appIdEnc.Value ?? "";
				bool flag = !string.IsNullOrEmpty(text);
				if (flag)
				{
					byte[] encryptedText = Convert.FromBase64String(text);
					string s = DatabaseLayerFactory.ClockWork.Encryption.Decrypt(encryptedText);
					int.TryParse(s, out num);
				}
			}
			catch (Exception ex)
			{
			}
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			AppointmentDTO appointmentDTO;
			if (num <= 0)
			{
				(appointmentDTO = new AppointmentDTO()).Attendees = new List<AttendeeDTO>
				{
					new AttendeeDTO
					{
						Person = new PersonBaseDTO
						{
							PersonId = this.LookupStaffPid()
						}
					}
				};
			}
			else
			{
				appointmentDTO = appointmentClientManager.LoadAppointment(num);
			}
			AppointmentDTO appointmentDTO2 = appointmentDTO;
			DateTime date = this.dp_date.SelectedDate.Value.Date;
			appointmentDTO2.StartDateTime = date.Add(this.tp_start.SelectedTime.Value);
			appointmentDTO2.EndDateTime = date.Add(this.tp_end.SelectedTime.Value);
			string text2 = this.txt_memo.Text.Trim();
			bool flag2 = text2.Length < 1;
			if (flag2)
			{
				appointmentDTO2.Memo = "";
			}
			else
			{
				string a = string.IsNullOrEmpty(appointmentDTO2.Memo) ? "" : appointmentDTO2.Memo.ConvertRtfToPlainText().Trim();
				bool flag3 = a != text2;
				if (flag3)
				{
					appointmentDTO2.Memo = text2.ConvertPlainTextToRtf();
				}
			}
			appointmentDTO2.IsCancelled = this.chk_cancelled.Checked;
			appointmentDTO2.IsPrivate = this.chk_private.Checked;
			string selectedValue = this.cmb_appType.SelectedValue;
			int num2;
			bool flag4 = selectedValue.Length < 1 || selectedValue == "0" || !int.TryParse(selectedValue, out num2);
			if (flag4)
			{
				num2 = 0;
			}
			bool flag5 = num2 < 1;
			if (flag5)
			{
				appointmentDTO2.AppType = null;
			}
			else
			{
				bool flag6 = ((appointmentDTO2.AppType == null) ? 0 : appointmentDTO2.AppType.AppTypeId) != num2;
				if (flag6)
				{
					appointmentDTO2.AppType = new AppTypeDTO
					{
						AppTypeId = num2
					};
				}
			}
			appointmentDTO2.Location = this.txt_location.Text.Trim();
			appointmentDTO2.SubTitle = this.txt_subTitle.Text.Trim();
			bool flag7 = appointmentDTO2.AppointmentId > 0;
			if (flag7)
			{
				appointmentClientManager.UpdateAppointment(appointmentDTO2);
			}
			else
			{
				appointmentClientManager.CreateAppointment(appointmentDTO2);
			}
			this.FireOnSaveCompleted();
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000440A4 File Offset: 0x000422A4
		private void FireOnSaveCompleted()
		{
			EventHandler onSaveCompleted = this.OnSaveCompleted;
			bool flag = onSaveCompleted != null;
			if (flag)
			{
				onSaveCompleted(this, new EventArgs());
			}
		}

		// Token: 0x04000780 RID: 1920
		public EventHandler OnSaveCompleted;

		// Token: 0x04000781 RID: 1921
		protected Panel p_editApp;

		// Token: 0x04000782 RID: 1922
		protected Label lbl_date;

		// Token: 0x04000783 RID: 1923
		protected RadDatePicker dp_date;

		// Token: 0x04000784 RID: 1924
		protected Label lbl_time;

		// Token: 0x04000785 RID: 1925
		protected RadTimePicker tp_start;

		// Token: 0x04000786 RID: 1926
		protected RadTimePicker tp_end;

		// Token: 0x04000787 RID: 1927
		protected Label lbl_subtitle;

		// Token: 0x04000788 RID: 1928
		protected TextBox txt_subTitle;

		// Token: 0x04000789 RID: 1929
		protected Label lbl_location;

		// Token: 0x0400078A RID: 1930
		protected TextBox txt_location;

		// Token: 0x0400078B RID: 1931
		protected CheckBox chk_cancelled;

		// Token: 0x0400078C RID: 1932
		protected CheckBox chk_private;

		// Token: 0x0400078D RID: 1933
		protected Label lbl_appType;

		// Token: 0x0400078E RID: 1934
		protected DropDownList cmb_appType;

		// Token: 0x0400078F RID: 1935
		protected Label lbl_memo;

		// Token: 0x04000790 RID: 1936
		protected TextBox txt_memo;

		// Token: 0x04000791 RID: 1937
		protected HiddenField appIdEnc;

		// Token: 0x04000792 RID: 1938
		protected HiddenField setupCompleted;

		// Token: 0x04000793 RID: 1939
		protected Button btn_save;

		// Token: 0x04000794 RID: 1940
		protected Button btn_cancel;
	}
}
