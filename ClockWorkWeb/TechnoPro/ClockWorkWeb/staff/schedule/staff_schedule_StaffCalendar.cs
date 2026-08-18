using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkWeb.ctrls.Staff;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.UserSettingsPermissions;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsCalendar;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;
using TechnoPro.Common.UI.Web.Entity;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x02000109 RID: 265
	public class staff_schedule_StaffCalendar : Page
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x0003980C File Offset: 0x00037A0C
		protected void Page_Init(object sender, EventArgs e)
		{
			this.GetPid();
			ScriptManager current = ScriptManager.GetCurrent(this.Page);
			current.Scripts.Add(new ScriptReference(base.ResolveUrl("~/js/AdvancedForm.js")));
			staff_schedule_StaffCalendar.eCalendarPermission[] userCalendarPermissions = this.GetUserCalendarPermissions();
			this.RadScheduler1.AllowDelete = !userCalendarPermissions.Contains(staff_schedule_StaffCalendar.eCalendarPermission.NotAllowedToDelete);
			this.RadScheduler1.AllowEdit = !userCalendarPermissions.Contains(staff_schedule_StaffCalendar.eCalendarPermission.NotAllowedToEdit);
			this.RadScheduler1.AllowInsert = !userCalendarPermissions.Contains(staff_schedule_StaffCalendar.eCalendarPermission.NotAllowedToInsert);
			ClockWorkAppointmentProvider provider = this.GetProvider();
			provider.ShowCancelledAppointments = this.GetShowCancelled();
			this.RadScheduler1.Provider = provider;
			this.RadScheduler1.SelectedDate = DateTime.Now.Date;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x000398D0 File Offset: 0x00037AD0
		private staff_schedule_StaffCalendar.eCalendarPermission[] GetUserCalendarPermissions()
		{
			IPermissionClientManager permissionClientManager = new PermissionClientManager();
			List<staff_schedule_StaffCalendar.eCalendarPermission> list = new List<staff_schedule_StaffCalendar.eCalendarPermission>();
			bool flag = !permissionClientManager.IsPersonAllowed(UserPermissionEnum.BookAppointments);
			if (flag)
			{
				list.Add(staff_schedule_StaffCalendar.eCalendarPermission.NotAllowedToInsert);
			}
			bool flag2 = !permissionClientManager.IsPersonAllowed(UserPermissionEnum.DeleteAppointments);
			if (flag2)
			{
				list.Add(staff_schedule_StaffCalendar.eCalendarPermission.NotAllowedToDelete);
			}
			bool flag3 = !permissionClientManager.IsPersonAllowed(UserPermissionEnum.ModifyAppointments);
			if (flag3)
			{
				list.Add(staff_schedule_StaffCalendar.eCalendarPermission.NotAllowedToEdit);
			}
			return list.ToArray();
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00039940 File Offset: 0x00037B40
		private ClockWorkAppointmentProvider GetProvider()
		{
			bool flag = this.Session["Telerik.Web.Examples.Scheduler.AdvancedFormTemplate.DefaultCS"] == null || !base.IsPostBack;
			ClockWorkAppointmentProvider result;
			if (flag)
			{
				ClockWorkAppointmentProvider clockWorkAppointmentProvider = new ClockWorkAppointmentProvider(this.GetWhoseCalendarToView());
				this.Session["Telerik.Web.Examples.Scheduler.AdvancedFormTemplate.DefaultCS"] = clockWorkAppointmentProvider;
				result = clockWorkAppointmentProvider;
			}
			else
			{
				result = (ClockWorkAppointmentProvider)this.Session["Telerik.Web.Examples.Scheduler.AdvancedFormTemplate.DefaultCS"];
			}
			return result;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000399AC File Offset: 0x00037BAC
		protected void Page_Load(object sender, EventArgs e)
		{
			string key = "OnSubmitScript";
			string script = "disableDiv();";
			base.ClientScript.RegisterOnSubmitStatement(base.GetType(), key, script);
			int pid = this.GetPid();
			bool settingValue = new WebSettingsClientManager().GetSettingValue<bool>(Setting.MODULES_STAFF_CALENDAR);
			bool flag = !settingValue;
			if (flag)
			{
				base.Response.Redirect("~/user/misc/NotAllowed.aspx?code=module", true);
			}
			bool flag2 = false;
			IList<int> gids = this.GetAllowedGids();
			IPersonBaseClientManager personBaseClientManager = new PersonBaseClientManager();
			PersonBaseDTO personBaseDTO = personBaseClientManager.LoadPerson(pid);
			bool flag3 = ((personBaseDTO != null) ? personBaseDTO.Groups.FirstOrDefault((GroupDTO g) => gids.Contains(g.GroupId)) : null) != null;
			if (flag3)
			{
				flag2 = true;
			}
			bool flag4 = !flag2;
			if (flag4)
			{
				base.Response.Redirect("~/user/misc/notallowed.aspx?code=notstaff&step=1", true);
			}
			bool flag5 = !this.Page.IsPostBack;
			if (flag5)
			{
				this.ctrlStaffChooser1.Init(gids);
				object obj = this.Session["apptUsingListCalendar"];
				bool flag6 = obj != null && (bool)obj;
				bool flag7 = flag6;
				if (flag7)
				{
					base.Response.Redirect("StaffCalendarList.aspx", true);
				}
				bool flag8 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag8)
				{
					((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.Staff_Calendar);
				}
				Style style = new Style();
				this.Page.Header.StyleSheet.CreateStyleRule(style, this, ".rsAptResize { visibility: hidden; }");
				this.ctrlStaffChooser1.SetSelectedPid(this.GetWhoseCalendarToView());
				this.btn_changeHideCancelled.Text = (this.GetShowCancelled() ? "Hide cancelled" : "Show cancelled");
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void RadScheduler1_DataBound(object sender, EventArgs e)
		{
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00039B70 File Offset: 0x00037D70
		protected void RadScheduler1_AppointmentCreated(object sender, AppointmentCreatedEventArgs e)
		{
			bool flag = e.Appointment.RecurrenceState == RecurrenceState.Master || e.Appointment.RecurrenceState == RecurrenceState.Occurrence;
			if (flag)
			{
				Panel child = new Panel
				{
					CssClass = "rsAptRecurrence"
				};
				e.Container.Controls.AddAt(0, child);
			}
			bool flag2 = e.Appointment.RecurrenceState == RecurrenceState.Exception;
			if (flag2)
			{
				Panel child2 = new Panel
				{
					CssClass = "rsAptRecurrenceException"
				};
				e.Container.Controls.AddAt(0, child2);
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00039C04 File Offset: 0x00037E04
		protected void RadScheduler1_AppointmentDataBound(object sender, SchedulerEventArgs e)
		{
			Telerik.Web.UI.Appointment appointment = e.Appointment;
			AppTypeDTO appType = appointment.GetAppType();
			string text = ((appType != null) ? appType.Description : null) ?? "";
			string text2 = appointment.GetSubTitle() ?? "";
			e.Appointment.ToolTip = string.Join(" ", (from g in new string[]
			{
				text,
				text2
			}
			select (g ?? "").Trim() into h
			where h.Length > 0
			select h).ToArray<string>());
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00039CB8 File Offset: 0x00037EB8
		private IList<int> GetAllowedGids()
		{
			SessionCaching currentInstance = SessionCaching.CurrentInstance;
			IList<int> list = (IList<int>)currentInstance["staffCalendar_gids"];
			bool flag = list == null;
			if (flag)
			{
				list = new List<int>();
				string text = new WebSettingsClientManager().GetSettingValue<string>(Setting.STAFF_Appointments_AllowedViewCalendarGroupIds);
				bool flag2 = string.IsNullOrEmpty(text);
				if (flag2)
				{
					text = "2";
				}
				List<string> list2 = text.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ConvertAll<string>((string g) => g.Trim());
				foreach (string s in list2)
				{
					int num;
					bool flag3 = int.TryParse(s, out num) && !list.Contains(num) && num > 0;
					if (flag3)
					{
						list.Add(num);
					}
				}
				currentInstance.Insert("staffCalendar_gids", list);
			}
			return list;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00039DD0 File Offset: 0x00037FD0
		protected void btn_goToListView_Click(object sender, EventArgs e)
		{
			this.Session.Add("apptUsingListCalendar", true);
			base.Response.Redirect("~/staff/schedule/StaffCalendarList.aspx", true);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00039DFC File Offset: 0x00037FFC
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00039E20 File Offset: 0x00038020
		private int GetWhoseCalendarToView()
		{
			SessionCaching currentInstance = SessionCaching.CurrentInstance;
			PersonBaseDTO personBaseDTO = (PersonBaseDTO)currentInstance["staffCalendar_viewingPerson"];
			bool flag = personBaseDTO != null && personBaseDTO.PersonId > 0;
			int result;
			if (flag)
			{
				result = personBaseDTO.PersonId;
			}
			else
			{
				result = this.GetPid();
			}
			return result;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00039E6C File Offset: 0x0003806C
		protected void btn_chooseSql_Click(object sender, EventArgs e)
		{
			int selectedPid = this.ctrlStaffChooser1.SelectedPid;
			bool flag = selectedPid < 1;
			if (!flag)
			{
				IPeopleClientManager peopleClientManager = new PeopleClientManager();
				PersonBaseDTO personBaseDTO = peopleClientManager.LoadPerson(selectedPid);
				bool flag2 = personBaseDTO == null || personBaseDTO.PersonId < 1 || personBaseDTO.Groups == null;
				if (!flag2)
				{
					IList<int> gids = this.GetAllowedGids();
					bool flag3 = personBaseDTO.Groups.FirstOrDefault((GroupDTO g) => gids.Contains(g.GroupId)) == null;
					if (!flag3)
					{
						SessionCaching currentInstance = SessionCaching.CurrentInstance;
						currentInstance.Insert("staffCalendar_viewingPerson", personBaseDTO);
						this.RefreshCalendar();
					}
				}
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00039F10 File Offset: 0x00038110
		protected void btn_refresh_Click(object sender, EventArgs e)
		{
			this.RefreshCalendar();
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00039F1C File Offset: 0x0003811C
		protected void btn_backToMe_Click(object sender, EventArgs e)
		{
			IPeopleClientManager peopleClientManager = new PeopleClientManager();
			int pid = this.GetPid();
			PersonBaseDTO personBaseDTO;
			if ((personBaseDTO = peopleClientManager.LoadPerson(pid)) == null)
			{
				(personBaseDTO = new PersonBaseDTO()).PersonId = pid;
			}
			PersonBaseDTO value = personBaseDTO;
			SessionCaching currentInstance = SessionCaching.CurrentInstance;
			currentInstance.Insert("staffCalendar_viewingPerson", value);
			this.RefreshCalendar();
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000397A1 File Offset: 0x000379A1
		private void RefreshCalendar()
		{
			base.Response.Redirect("StaffCalendar.aspx", true);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00039F6C File Offset: 0x0003816C
		private bool GetShowCancelled()
		{
			SessionCaching currentInstance = SessionCaching.CurrentInstance;
			object obj = currentInstance["AppsShowCancelled"];
			return obj != null && (bool)obj;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00039F9C File Offset: 0x0003819C
		private void SetShowCancelled(bool showCancelled)
		{
			SessionCaching currentInstance = SessionCaching.CurrentInstance;
			currentInstance.Insert("AppsShowCancelled", showCancelled);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00039FC2 File Offset: 0x000381C2
		protected void btn_changeHideCancelled_Click(object sender, EventArgs e)
		{
			this.SetShowCancelled(!this.GetShowCancelled());
			this.RefreshCalendar();
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00039FDC File Offset: 0x000381DC
		protected void RadScheduler1_OnFormCreating(object sender, SchedulerFormCreatingEventArgs e)
		{
			this.GetPid();
			Telerik.Web.UI.Appointment appointment = e.Appointment;
			IList<eAppointmentPermissionRestriction> restrictions = appointment.GetRestrictions();
			List<eAppointmentPermissionRestriction> list;
			if (restrictions == null)
			{
				list = null;
			}
			else
			{
				list = restrictions.Where(delegate(eAppointmentPermissionRestriction g)
				{
					eAppointmentPermissionRestrictionResult result = g.GetAttribute<AppointmentPermissionRestrictionAttribute>().Result;
					return result == eAppointmentPermissionRestrictionResult.NotAllowedToModifyOrDelete || result == eAppointmentPermissionRestrictionResult.NotAllowedToDelete;
				}).ToList<eAppointmentPermissionRestriction>();
			}
			List<eAppointmentPermissionRestriction> list2 = list ?? new List<eAppointmentPermissionRestriction>();
			bool flag = list2.Count > 0;
			if (flag)
			{
				e.Cancel = true;
				this.ShowMessage(("Not allowed - " + list2[0].GetAttribute<AppointmentPermissionRestrictionAttribute>().Title) ?? "");
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0003A080 File Offset: 0x00038280
		private bool HasCutoffPassedForInsertingOrModifying(DateTime start, DateTime end)
		{
			IOldUserSettingClientManager oldUserSettingClientManager = new OldUserSettingClientManager();
			string settingValue_String = oldUserSettingClientManager.GetSettingValue_String(eSettingCode.SETTING_Appointments_DisallowCreatingEditingDeletingCutoff);
			CutoffTime cutoffTime = string.IsNullOrEmpty(settingValue_String) ? null : settingValue_String.CutoffTimeFromXml();
			DateTime? dateTime = (cutoffTime != null) ? cutoffTime.GetMinimumDateForBeforeTypeCutoff() : null;
			bool flag = dateTime == null || start <= dateTime.Value;
			bool flag2 = !flag;
			if (flag2)
			{
				CWLogger.Logger.Warn("ClockWorkAppointmentProvider:UpHasCutoffPassedForInsertingOrModifying:Not allowed to insert or modify:minDate={0}", (dateTime != null) ? dateTime.Value.ToString("yyyy-MM-dd h:mm tt") : "NULL");
			}
			return !flag;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0003A130 File Offset: 0x00038330
		private void ShowMessage(string message)
		{
			bool flag = string.IsNullOrEmpty(message);
			if (flag)
			{
				this.p_msg.Visible = false;
			}
			else
			{
				this.p_msg.Visible = true;
				this.lbl_msg.Text = (message ?? "");
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0003A17C File Offset: 0x0003837C
		protected void RadScheduler1_OnAppointmentInsert(object sender, AppointmentInsertEventArgs e)
		{
			bool flag = this.HasCutoffPassedForInsertingOrModifying(e.Appointment.Start, e.Appointment.End);
			if (flag)
			{
				this.ShowMessage("Cannot create the appointment with the date specified - it is after the cutoff period allowed for creating appointments");
				e.Cancel = true;
			}
			else
			{
				AppointmentDTO appointmentDTO = ClockWorkAppointmentProvider.CreateClockWorkAppFromTelerikApp(e.Appointment);
				AppTypeDTO appType = appointmentDTO.AppType;
				bool flag2 = ((appType != null) ? appType.AppTypeId : 0) < 1;
				if (flag2)
				{
					IPermissionClientManager permissionClientManager = new PermissionClientManager();
					bool flag3 = !permissionClientManager.IsPersonAllowed(UserPermissionEnum.CreateModifyAppWithNoAppType);
					if (flag3)
					{
						this.ShowMessage("Cannot create an appointment with no appointment type (due to permission restrictions)");
						e.Cancel = true;
					}
				}
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0003A218 File Offset: 0x00038418
		protected void RadScheduler1_OnAppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
		{
			Telerik.Web.UI.Appointment appointment = e.Appointment;
			bool flag = this.HasCutoffPassedForInsertingOrModifying(appointment.Start, appointment.End);
			if (flag)
			{
				this.ShowMessage("Cannot use the date specified - it is after the cutoff period allowed.");
				e.Cancel = true;
			}
			else
			{
				IList<eAppointmentPermissionRestriction> restrictions = appointment.GetRestrictions();
				bool flag2 = restrictions != null && restrictions.HasRestriction(new eAppointmentPermissionRestrictionResult[1]);
				if (flag2)
				{
					this.ShowMessage("Not allowed to modify this appointment");
					e.Cancel = true;
				}
				else
				{
					AppointmentDTO appointmentDTO = ClockWorkAppointmentProvider.CreateClockWorkAppFromTelerikApp(e.Appointment);
					AppTypeDTO appType = appointmentDTO.AppType;
					bool flag3 = ((appType != null) ? appType.AppTypeId : 0) < 1;
					if (flag3)
					{
						IPermissionClientManager permissionClientManager = new PermissionClientManager();
						bool flag4 = !permissionClientManager.IsPersonAllowed(UserPermissionEnum.CreateModifyAppWithNoAppType);
						if (flag4)
						{
							this.ShowMessage("Cannot modify an appointment to have no appointment type (due to permission restrictions)");
							e.Cancel = true;
						}
					}
				}
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0003A2EC File Offset: 0x000384EC
		protected void RadScheduler1_OnAppointmentDelete(object sender, AppointmentDeleteEventArgs e)
		{
			AppointmentDTO appointmentDTO = ClockWorkAppointmentProvider.CreateClockWorkAppFromTelerikApp(e.Appointment);
			PersonBaseDTO whoBooked = appointmentDTO.WhoBooked;
			bool flag = ((whoBooked != null) ? whoBooked.PersonId : 0) != this.GetPid();
			if (flag)
			{
				IPermissionClientManager permissionClientManager = new PermissionClientManager();
				bool flag2 = !permissionClientManager.IsPersonAllowed(UserPermissionEnum.DeleteAppointmentsIDidntCreate);
				if (flag2)
				{
					this.ShowMessage("Not allowed to delete appointments you didn't create");
					e.Cancel = true;
				}
			}
		}

		// Token: 0x040005FD RID: 1533
		private const string ProviderSessionKey = "Telerik.Web.Examples.Scheduler.AdvancedFormTemplate.DefaultCS";

		// Token: 0x040005FE RID: 1534
		private const string KeyViewingPerson = "staffCalendar_viewingPerson";

		// Token: 0x040005FF RID: 1535
		private const string KeyAllowedGids = "staffCalendar_gids";

		// Token: 0x04000600 RID: 1536
		private const string showCancelledKey = "AppsShowCancelled";

		// Token: 0x04000601 RID: 1537
		protected RadScriptManager bbb;

		// Token: 0x04000602 RID: 1538
		protected RadScriptBlock RadScriptBlock1;

		// Token: 0x04000603 RID: 1539
		protected RadAjaxManager RadAjaxManager1;

		// Token: 0x04000604 RID: 1540
		protected RadAjaxLoadingPanel RadAjaxLoadingPanel1;

		// Token: 0x04000605 RID: 1541
		protected Panel p_calendarControls;

		// Token: 0x04000606 RID: 1542
		protected Panel p_msg;

		// Token: 0x04000607 RID: 1543
		protected Label lbl_msg;

		// Token: 0x04000608 RID: 1544
		protected Panel p_gotoListView;

		// Token: 0x04000609 RID: 1545
		protected LinkButton btn_goToListView;

		// Token: 0x0400060A RID: 1546
		protected ImageButton btn_refresh;

		// Token: 0x0400060B RID: 1547
		protected ctrls_Staff_CtrlStaffChooser ctrlStaffChooser1;

		// Token: 0x0400060C RID: 1548
		protected Button btn_chooseStaff;

		// Token: 0x0400060D RID: 1549
		protected Button btn_backToMe;

		// Token: 0x0400060E RID: 1550
		protected Button btn_changeHideCancelled;

		// Token: 0x0400060F RID: 1551
		protected RadScheduler RadScheduler1;

		// Token: 0x02000232 RID: 562
		internal enum eCalendarPermission
		{
			// Token: 0x04000AEB RID: 2795
			NotAllowedToDelete,
			// Token: 0x04000AEC RID: 2796
			NotAllowedToEdit,
			// Token: 0x04000AED RID: 2797
			NotAllowedToInsert
		}
	}
}
