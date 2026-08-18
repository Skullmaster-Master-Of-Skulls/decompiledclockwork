using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.Appointments;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.UserSettingsPermissions;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.TextFormat.Adapters;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsCalendar;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x020000FF RID: 255
	public class calendar : Page
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x000385E0 File Offset: 0x000367E0
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Redirect("StaffCalendar.aspx", true);
			int pid = calendar.GetPid();
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
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000386A0 File Offset: 0x000368A0
		private static int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000386BC File Offset: 0x000368BC
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

		// Token: 0x06000769 RID: 1897 RVA: 0x000387D4 File Offset: 0x000369D4
		[WebMethod]
		public static AppointmentWrapper LoadAppointment(int appId)
		{
			int pid = calendar.GetPid();
			bool flag = pid < 1;
			AppointmentWrapper result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
				AppointmentDTO appointmentDTO = appointmentClientManager.LoadAppointment(appId);
				result = ((appointmentDTO == null) ? null : new AppointmentWrapper(appointmentDTO));
			}
			return result;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00038818 File Offset: 0x00036A18
		[WebMethod]
		public static IList<CalendarEvent> LoadCalendar(DateTime startDate, DateTime endDate, int[] pids, bool hideCancelled)
		{
			int pid = calendar.GetPid();
			bool flag = pid < 1;
			IList<CalendarEvent> result;
			if (flag)
			{
				result = new List<CalendarEvent>();
			}
			else
			{
				pids = new int[]
				{
					1
				};
				IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
				IDictionary<int, IList<eAppointmentPermissionRestriction>> restrictions;
				IList<AppointmentDTO> source = appointmentClientManager.LoadAppointmentsWithSpecialPermissions(pids.ToList<int>(), null, hideCancelled, startDate, Convert.ToInt32((endDate.Date - startDate.Date).TotalDays) + 1, out restrictions);
				result = (from app in source
				select new CalendarEvent(app, restrictions.ContainsKey(app.AppointmentId) ? restrictions[app.AppointmentId] : null)).ToList<CalendarEvent>();
			}
			return result;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000388B0 File Offset: 0x00036AB0
		[WebMethod]
		public static IList<AppTypeDTO> LoadAppointmentTypes()
		{
			int pid = calendar.GetPid();
			bool flag = pid < 1;
			IList<AppTypeDTO> result;
			if (flag)
			{
				result = new List<AppTypeDTO>();
			}
			else
			{
				IAppointmentTypeClientManager appointmentTypeClientManager = new AppointmentTypeClientManager();
				result = appointmentTypeClientManager.LoadAllowedAppTypes();
			}
			return result;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000388E4 File Offset: 0x00036AE4
		[WebMethod]
		public static SaveAppointmentResult SaveAppointment(AppointmentToUpdateWrapper app)
		{
			bool flag = app == null;
			SaveAppointmentResult result;
			if (flag)
			{
				result = new SaveAppointmentResult("Invalid parameters");
			}
			else
			{
				bool flag2 = app.AppointmentId < 1;
				if (flag2)
				{
					result = new SaveAppointmentResult("Invalid appointment id");
				}
				else
				{
					bool flag3 = app.EndTimeSeconds <= app.StartTimeSeconds;
					if (flag3)
					{
						result = new SaveAppointmentResult("Invalid time - end time is same or before start time");
					}
					else
					{
						TimeSpan value = TimeSpan.FromSeconds((double)app.StartTimeSeconds);
						TimeSpan value2 = TimeSpan.FromSeconds((double)app.EndTimeSeconds);
						DateTime dateTime = app.Date.Date.Add(value);
						DateTime dateTime2 = app.Date.Date.Add(value2);
						bool flag4 = calendar.HasCutoffPassedForInsertingOrModifying(dateTime, dateTime2);
						if (flag4)
						{
							result = new SaveAppointmentResult("Cannot use the date specified - it is after the cutoff period allowed.");
						}
						else
						{
							IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
							IList<eAppointmentPermissionRestriction> list;
							AppointmentDTO appointmentDTO = appointmentClientManager.LoadAppointmentWithSpecialPermissions(app.AppointmentId, out list);
							bool flag5 = appointmentDTO == null;
							if (flag5)
							{
								result = new SaveAppointmentResult("Error - can't find original appointment.");
							}
							else
							{
								bool flag6 = list != null && list.HasRestriction(new eAppointmentPermissionRestrictionResult[1]);
								if (flag6)
								{
									result = new SaveAppointmentResult("Not allowed to modify this appointment");
								}
								else
								{
									bool flag7 = app.AppTypeId < 1;
									if (flag7)
									{
										IPermissionClientManager permissionClientManager = new PermissionClientManager();
										bool flag8 = !permissionClientManager.IsPersonAllowed(UserPermissionEnum.CreateModifyAppWithNoAppType);
										if (flag8)
										{
											return new SaveAppointmentResult("Cannot modify an appointment to have no appointment type (due to permission restrictions)");
										}
									}
									appointmentDTO.StartDateTime = dateTime;
									appointmentDTO.EndDateTime = dateTime2;
									string memo = appointmentDTO.Memo;
									string a = (((memo != null) ? memo.ConvertRtfToPlainText() : null) ?? "").Trim();
									string text = (app.MemoPlainText ?? "").Trim();
									bool flag9 = a != text;
									if (flag9)
									{
										appointmentDTO.Memo = text.ConvertPlainTextToRtf();
									}
									appointmentDTO.SubTitle = (app.Subject ?? "").Trim();
									BaseBasicAppointmentDTO baseBasicAppointmentDTO = appointmentDTO;
									object appType;
									if (app.AppTypeId <= 0)
									{
										appType = null;
									}
									else
									{
										(appType = new AppTypeDTO()).AppTypeId = app.AppTypeId;
									}
									baseBasicAppointmentDTO.AppType = appType;
									appointmentDTO.IsCancelled = app.IsCancelled;
									appointmentDTO.IsPrivate = app.IsPrivate;
									appointmentClientManager.UpdateAppointment(appointmentDTO);
									result = new SaveAppointmentResult(appointmentDTO.AppointmentId);
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00038B40 File Offset: 0x00036D40
		private static bool HasCutoffPassedForInsertingOrModifying(DateTime start, DateTime end)
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

		// Token: 0x040005C6 RID: 1478
		private const string KeyAllowedGids = "staffCalendar_gids";

		// Token: 0x040005C7 RID: 1479
		protected ScriptManager bbb;
	}
}
