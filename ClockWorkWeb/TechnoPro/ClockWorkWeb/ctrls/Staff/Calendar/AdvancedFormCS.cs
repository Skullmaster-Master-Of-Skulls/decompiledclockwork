using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.Appointments;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsCalendar;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Staff.Calendar
{
	// Token: 0x0200013C RID: 316
	public class AdvancedFormCS : UserControl
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00043074 File Offset: 0x00041274
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x000430A5 File Offset: 0x000412A5
		private bool FormInitialized
		{
			get
			{
				return (bool)(this.ViewState["FormInitialized"] ?? false);
			}
			set
			{
				this.ViewState["FormInitialized"] = value;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x000430BF File Offset: 0x000412BF
		protected RadScheduler Owner
		{
			get
			{
				return this.Appointment.Owner;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x000430CC File Offset: 0x000412CC
		protected Appointment Appointment
		{
			get
			{
				SchedulerFormContainer schedulerFormContainer = (SchedulerFormContainer)base.BindingContainer;
				return schedulerFormContainer.Appointment;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x000430F0 File Offset: 0x000412F0
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00043118 File Offset: 0x00041318
		[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
		public string IsPrivate
		{
			get
			{
				return this.chkIsPrivate.Checked.ToString();
			}
			set
			{
				bool @checked;
				bool.TryParse(value, out @checked);
				this.chkIsPrivate.Checked = @checked;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0004313C File Offset: 0x0004133C
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x00043164 File Offset: 0x00041364
		[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
		public string IsCancelled
		{
			get
			{
				return this.chkIsCancelled.Checked.ToString();
			}
			set
			{
				bool @checked;
				bool.TryParse(value, out @checked);
				this.chkIsCancelled.Checked = @checked;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00043188 File Offset: 0x00041388
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x000431A8 File Offset: 0x000413A8
		[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
		public string AppTypeIdStr
		{
			get
			{
				return this.cmb_appType.SelectedValue;
			}
			set
			{
				foreach (object obj in this.cmb_appType.Items)
				{
					DropDownListItem dropDownListItem = (DropDownListItem)obj;
					bool flag = dropDownListItem.Value != value;
					if (!flag)
					{
						dropDownListItem.Selected = true;
						break;
					}
				}
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00043220 File Offset: 0x00041420
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00043240 File Offset: 0x00041440
		[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
		public string RoomStr
		{
			get
			{
				return this.hiddenRoom.Value;
			}
			set
			{
				string text = value ?? "";
				this.hiddenRoom.Value = text;
				RoomAndLocation roomAndLocation = text.GetRoomAndLocation();
				RadInputControl room = this.Room;
				string format = "{0} {1}";
				object obj;
				if (roomAndLocation == null)
				{
					obj = null;
				}
				else
				{
					AppointmentRoomDTO room2 = roomAndLocation.Room;
					obj = ((room2 != null) ? room2.RoomTitle : null);
				}
				room.Text = string.Format(format, obj ?? "", ((roomAndLocation != null) ? roomAndLocation.Location : null) ?? "");
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x000432BC File Offset: 0x000414BC
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00043308 File Offset: 0x00041508
		[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
		public string MemoPlainText
		{
			get
			{
				string text = this.txt_memoPlainText.Text.Trim();
				string b = (this.memoPlainTextOriginal.Value ?? "").Trim();
				return (text == b) ? null : text;
			}
			set
			{
				string text = (value ?? "").Trim();
				this.memoPlainTextOriginal.Value = text;
				this.txt_memoPlainText.Text = text;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00043340 File Offset: 0x00041540
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00043390 File Offset: 0x00041590
		[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
		public string AttendeesSerialized
		{
			get
			{
				return string.Join("`", (from g in this.lbUsers.CheckedItems
				select g.Value).ToArray<string>());
			}
			set
			{
				string attendeesSerialized = value ?? "";
				IList<AttendeeDTO> attendees = attendeesSerialized.GetAttendees();
				this.lbUsers.Items.Clear();
				foreach (AttendeeDTO attendeeDTO in attendees)
				{
					PersonBaseDTO person = attendeeDTO.Person;
					RadListBoxItem item = new RadListBoxItem(((person != null) ? person.FirstName : null) ?? "", attendeeDTO.GetAttendeeSerialized())
					{
						Checked = true
					};
					this.lbUsers.Items.Add(item);
				}
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0004343C File Offset: 0x0004163C
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x00003E0A File Offset: 0x0000200A
		[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
		public string AppTypeTitle
		{
			get
			{
				string appTypeIdStr = this.AppTypeIdStr;
				bool flag = string.IsNullOrEmpty(appTypeIdStr);
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					foreach (object obj in this.cmb_appType.Items)
					{
						DropDownListItem dropDownListItem = (DropDownListItem)obj;
						bool flag2 = dropDownListItem.Value == appTypeIdStr;
						if (flag2)
						{
							return dropDownListItem.Text;
						}
					}
					result = "";
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x000434DC File Offset: 0x000416DC
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x000434F4 File Offset: 0x000416F4
		public RadSchedulerAdvancedFormAdvancedFormMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00043500 File Offset: 0x00041700
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x0004351D File Offset: 0x0004171D
		[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
		public string Subject
		{
			get
			{
				return this.SubjectText.Text;
			}
			set
			{
				this.SubjectText.Text = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00043530 File Offset: 0x00041730
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x000435B0 File Offset: 0x000417B0
		[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
		public DateTime Start
		{
			get
			{
				DateTime displayDate = this.StartDate.SelectedDate.Value.Date;
				bool @checked = this.AllDayEvent.Checked;
				if (@checked)
				{
					displayDate = displayDate.Date;
				}
				else
				{
					TimeSpan timeOfDay = this.StartTime.SelectedDate.Value.TimeOfDay;
					displayDate = displayDate.Add(timeOfDay);
				}
				return this.Owner.DisplayToUtc(displayDate);
			}
			set
			{
				this.StartDate.SelectedDate = new DateTime?(this.Owner.UtcToDisplay(value));
				this.StartTime.SelectedDate = new DateTime?(this.Owner.UtcToDisplay(value));
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x000435F0 File Offset: 0x000417F0
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x00043681 File Offset: 0x00041881
		[Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
		public DateTime End
		{
			get
			{
				DateTime displayDate = this.StartDate.SelectedDate.Value.Date;
				bool @checked = this.AllDayEvent.Checked;
				if (@checked)
				{
					displayDate = displayDate.Date.AddDays(1.0);
				}
				else
				{
					TimeSpan timeOfDay = this.EndTime.SelectedDate.Value.TimeOfDay;
					displayDate = displayDate.Add(timeOfDay);
				}
				return this.Owner.DisplayToUtc(displayDate);
			}
			set
			{
				this.EndTime.SelectedDate = new DateTime?(this.Owner.UtcToDisplay(value));
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000436A4 File Offset: 0x000418A4
		protected void Page_Load(object sender, EventArgs e)
		{
			this.UpdateButton.ValidationGroup = this.Owner.ValidationGroup;
			this.UpdateButton.CommandName = ((this.Mode == RadSchedulerAdvancedFormAdvancedFormMode.Edit) ? "Update" : "Insert");
			this.InitializeStrings();
			bool flag = !this.FormInitialized;
			if (flag)
			{
				AdvancedFormCS.<>c__DisplayClass41_0 CS$<>8__locals1 = new AdvancedFormCS.<>c__DisplayClass41_0();
				IAppointmentTypeClientManager appointmentTypeClientManager = new AppointmentTypeClientManager();
				IList<AppTypeDTO> list = appointmentTypeClientManager.LoadAllowedAppTypes();
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				AdvancedFormCS.<>c__DisplayClass41_0 CS$<>8__locals2 = CS$<>8__locals1;
				string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.STAFF_Appointments_AllowedAppTypeIds);
				List<int> list2;
				if (settingValue == null)
				{
					list2 = null;
				}
				else
				{
					list2 = (from n in settingValue.Trim().Split(new char[]
					{
						','
					}).Select(delegate(string m)
					{
						string s = m.Trim();
						int num;
						return int.TryParse(s, out num) ? num : 0;
					})
					where n > 0
					select n).ToList<int>();
				}
				CS$<>8__locals2.webAllowedAppTypeIds = (list2 ?? new List<int>());
				bool flag2 = CS$<>8__locals1.webAllowedAppTypeIds.Count > 0;
				if (flag2)
				{
					list = (from g in list
					where CS$<>8__locals1.webAllowedAppTypeIds.Any((int h) => h == g.AppTypeId)
					select g).ToList<AppTypeDTO>();
				}
				foreach (AppTypeDTO appTypeDTO in list)
				{
					this.cmb_appType.Items.Add(new DropDownListItem(appTypeDTO.Description ?? "", appTypeDTO.AppTypeId.ToString()));
				}
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00043840 File Offset: 0x00041A40
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			bool flag = !this.FormInitialized;
			if (flag)
			{
				bool flag2 = this.IsAllDayAppointment(this.Appointment);
				if (flag2)
				{
				}
				this.FormInitialized = true;
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0004387F File Offset: 0x00041A7F
		protected void AdvOptionsScroll_DataBinding(object sender, EventArgs e)
		{
			this.AllDayEvent.Checked = this.IsAllDayAppointment(this.Appointment);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0004389A File Offset: 0x00041A9A
		protected void DurationValidator_OnServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = (this.End - this.Start > TimeSpan.Zero);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000438C0 File Offset: 0x00041AC0
		private void InitializeStrings()
		{
			this.StartDateValidator.ErrorMessage = this.Owner.Localization.AdvancedStartDateRequired;
			this.StartDateValidator.ValidationGroup = this.Owner.ValidationGroup;
			this.StartTimeValidator.ErrorMessage = this.Owner.Localization.AdvancedStartTimeRequired;
			this.StartTimeValidator.ValidationGroup = this.Owner.ValidationGroup;
			this.EndTimeValidator.ErrorMessage = this.Owner.Localization.AdvancedEndTimeRequired;
			this.EndTimeValidator.ValidationGroup = this.Owner.ValidationGroup;
			this.DurationValidator.ErrorMessage = this.Owner.Localization.AdvancedStartTimeBeforeEndTime;
			this.DurationValidator.ValidationGroup = this.Owner.ValidationGroup;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0004399C File Offset: 0x00041B9C
		private bool IsAllDayAppointment(Appointment app)
		{
			return app.Start.Hour == 0 && app.Start.Minute == 1 && app.End.Hour == 23 && app.End.Minute == 59;
		}

		// Token: 0x04000764 RID: 1892
		private RadSchedulerAdvancedFormAdvancedFormMode mode = RadSchedulerAdvancedFormAdvancedFormMode.Insert;

		// Token: 0x04000765 RID: 1893
		protected LinkButton AdvancedEditCloseButton;

		// Token: 0x04000766 RID: 1894
		protected Panel AdvOptionsScroll;

		// Token: 0x04000767 RID: 1895
		protected RadDatePicker StartDate;

		// Token: 0x04000768 RID: 1896
		protected CheckBox AllDayEvent;

		// Token: 0x04000769 RID: 1897
		protected RadTimePicker StartTime;

		// Token: 0x0400076A RID: 1898
		protected Label lblTo;

		// Token: 0x0400076B RID: 1899
		protected RadTimePicker EndTime;

		// Token: 0x0400076C RID: 1900
		protected RadDropDownList cmb_appType;

		// Token: 0x0400076D RID: 1901
		protected RadTextBox SubjectText;

		// Token: 0x0400076E RID: 1902
		protected RequiredFieldValidator StartDateValidator;

		// Token: 0x0400076F RID: 1903
		protected RequiredFieldValidator StartTimeValidator;

		// Token: 0x04000770 RID: 1904
		protected RequiredFieldValidator EndTimeValidator;

		// Token: 0x04000771 RID: 1905
		protected CustomValidator DurationValidator;

		// Token: 0x04000772 RID: 1906
		protected CheckBox chkIsCancelled;

		// Token: 0x04000773 RID: 1907
		protected CheckBox chkIsPrivate;

		// Token: 0x04000774 RID: 1908
		protected RadTextBox Room;

		// Token: 0x04000775 RID: 1909
		protected HiddenField hiddenRoom;

		// Token: 0x04000776 RID: 1910
		protected RadListBox lbUsers;

		// Token: 0x04000777 RID: 1911
		protected TextBox txt_memoPlainText;

		// Token: 0x04000778 RID: 1912
		protected HiddenField memoPlainTextOriginal;

		// Token: 0x04000779 RID: 1913
		protected RadCalendar SharedCalendar;

		// Token: 0x0400077A RID: 1914
		protected Panel ButtonsPanel;

		// Token: 0x0400077B RID: 1915
		protected LinkButton UpdateButton;

		// Token: 0x0400077C RID: 1916
		protected LinkButton CancelButton;
	}
}
