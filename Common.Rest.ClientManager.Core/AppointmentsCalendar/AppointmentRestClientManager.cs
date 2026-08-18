using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsCalendar
{
	// Token: 0x02000084 RID: 132
	public class AppointmentRestClientManager : BearerTokenRestProxy<IAppointmentClientManager>, IAppointmentClientManager, IWebService
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x0000E791 File Offset: 0x0000C991
		public AppointmentRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000E79B File Offset: 0x0000C99B
		public AppointmentRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		public int CreateAppointment(AppointmentDTO Appointment)
		{
			int num = base.Post<AppointmentDTO, int>(Appointment, "appointment");
			Appointment.AppointmentId = num;
			Task.Run(() => AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentWasCreatedAsync(Appointment));
			return num;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000E7F4 File Offset: 0x0000C9F4
		public void UpdateAppointment(AppointmentDTO Appointment)
		{
			BaseBasicAppointmentDTO baseBasicAppointmentDTO = this.LoadAppointment(Appointment.AppointmentId);
			base.Put<AppointmentDTO>(Appointment, "appointment");
			List<AttendeeDTO> collection = (from f in baseBasicAppointmentDTO.Attendees
			where Appointment.Attendees.FirstOrDefault((AttendeeDTO g) => g.Person.PersonId == f.Person.PersonId) == null
			select f).ToList<AttendeeDTO>();
			Appointment.Attendees.AddRange(collection);
			Task.Run(() => AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(Appointment));
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000E86F File Offset: 0x0000CA6F
		public void DeleteAppointment(int AppointmentId)
		{
			AppointmentDTO app0 = this.LoadAppointment(AppointmentId);
			base.Delete(string.Format("appointment/appid/{0}", AppointmentId));
			Task.Run(() => AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(app0));
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000E8AA File Offset: 0x0000CAAA
		public void DeleteAppointmentWithoutFiringNotifications(int AppointmentId)
		{
			base.Delete(string.Format("appointment/appid/{0}", AppointmentId));
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public IList<AppointmentDTO> LoadAppointments(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime)
		{
			return base.GetMany<AppointmentDTO>(string.Format("appointment/pids/{0}/apptypeids/{1}/range/{2}/{3}?hidecancelled={4}&perstudentdataicons={5}&peranonymousdataicons={6}", new object[]
			{
				PersonIds.CommaSeparatedValuesWithoutSpace<int>(),
				AppTypeIds.CommaSeparatedValuesWithoutSpace<int>(),
				StartDateTime,
				EndDateTime,
				HideCancelled,
				LoadPerStudentDataIcons,
				LoadPerAnonymousDataIcons
			}), true);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000E92B File Offset: 0x0000CB2B
		public AppointmentDTO LoadAppointment(int AppointmentId)
		{
			return base.Get<AppointmentDTO>(string.Format("appointment/appid/{0}", AppointmentId), true);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000E944 File Offset: 0x0000CB44
		public LoadAppointmentExtendedInfoResp LoadAppointmentExtendedInfo(int AppointmentId)
		{
			return base.Get<LoadAppointmentExtendedInfoResp>(string.Format("appointment/appointmentextendedinfo/appid/{0}", AppointmentId), true);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000E960 File Offset: 0x0000CB60
		public IList<AppointmentDTO> LoadAppointmentsWithSpecialPermissions(IList<int> PersonIds, IList<int> AppTypeIds, bool HideCancelled, DateTime StartDateTime, int NumDays, out IDictionary<int, IList<eAppointmentPermissionRestriction>> permissionRestrictions)
		{
			LoadAppointmentsWithSpecialPermissionsResp loadAppointmentsWithSpecialPermissionsResp = base.Get<LoadAppointmentsWithSpecialPermissionsResp>(string.Format("appointment/appointmentswithspecialpermissisions/pids/{0}/apptypeids/{1}/start/{2}/numdays/{3}", new object[]
			{
				PersonIds.CommaSeparatedValuesWithoutSpace<int>(),
				AppTypeIds.CommaSeparatedValuesWithoutSpace<int>(),
				StartDateTime,
				NumDays
			}), true);
			permissionRestrictions = ((loadAppointmentsWithSpecialPermissionsResp != null) ? loadAppointmentsWithSpecialPermissionsResp.PermissionRestrictions : null);
			if (loadAppointmentsWithSpecialPermissionsResp == null)
			{
				return null;
			}
			return loadAppointmentsWithSpecialPermissionsResp.Appointments;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		public void CancelAppointment(int AppointmentId, AppCancelInfoDTO CancelInfo)
		{
			AppointmentDTO app0 = this.LoadAppointment(AppointmentId);
			CancelAppointmentReq cancelAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelAppointmentReq>();
			cancelAppointmentReq.AppointmentId = AppointmentId;
			cancelAppointmentReq.AppointmentCancelInfo = CancelInfo;
			base.Post<CancelAppointmentReq>(cancelAppointmentReq, "appointment/cancel");
			Task.Run(() => AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(app0));
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000EA1D File Offset: 0x0000CC1D
		public void UnCancelAppointment(int AppointmentId)
		{
			AppointmentDTO app0 = this.LoadAppointment(AppointmentId);
			base.Post(string.Format("appointment/uncancel/appid/{0}", AppointmentId));
			Task.Run(() => AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(app0));
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000EA58 File Offset: 0x0000CC58
		public void MarkAppointmentTentative(int AppointmentId)
		{
			AppointmentDTO app0 = this.LoadAppointment(AppointmentId);
			base.Post(string.Format("appointment/markappointmenttentative/appid/{0}", AppointmentId));
			Task.Run(() => AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(app0));
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000EA93 File Offset: 0x0000CC93
		public void UnMarkAppointmentTentative(int AppointmentId)
		{
			AppointmentDTO app0 = this.LoadAppointment(AppointmentId);
			base.Post(string.Format("appointment/unmarkappointmenttentative/appid/{0}", AppointmentId));
			Task.Run(() => AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(app0));
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00002BEE File Offset: 0x00000DEE
		public int RecoverDeletedAppointment(int AppointmentId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00002BEE File Offset: 0x00000DEE
		public void MergeAllAppointments(int PersonIdNew, int PersonIdOld)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000EACE File Offset: 0x0000CCCE
		public AppointmentDTO LoadDeletedAppointmentById(int AppointmentId)
		{
			return base.Get<AppointmentDTO>(string.Format("appointment/deleted/appid/{0}", AppointmentId), true);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		public void UpdateAppointmentParts(AppointmentDTO Appointment, eAppointmentPart PartsToUpdate)
		{
			UpdateCalendarAppointmentPartsReq updateCalendarAppointmentPartsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCalendarAppointmentPartsReq>();
			updateCalendarAppointmentPartsReq.Appointment = Appointment;
			updateCalendarAppointmentPartsReq.PartsToUpdate = PartsToUpdate;
			base.Put<UpdateCalendarAppointmentPartsReq>(updateCalendarAppointmentPartsReq, "appointment/calendarappointmentparts");
			Task.Run(() => AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(Appointment));
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000EB40 File Offset: 0x0000CD40
		public void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText)
		{
			InsertOrUpdateAppointmentMemoReq insertOrUpdateAppointmentMemoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentMemoReq>();
			insertOrUpdateAppointmentMemoReq.AppointmentId = AppointmentId;
			insertOrUpdateAppointmentMemoReq.MemoText = MemoText;
			base.Post<InsertOrUpdateAppointmentMemoReq>(insertOrUpdateAppointmentMemoReq, "appointment/memo");
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000EB72 File Offset: 0x0000CD72
		public int GetNumberOfAppointmentsWithAppType(int appTypeId)
		{
			return base.Get<int>(string.Format("appointment/numberofappointmentswithapptype/apptypeid/{0}", appTypeId), true);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000EB8B File Offset: 0x0000CD8B
		public void SwapAppointmentTypeForAllAppointments(int appTypeIdToReplace, int appTypeIdToKeep)
		{
			base.Post(string.Format("appointment/swapapptypeforallappointments/apptypeidtoreplace/{0}/apptypeidtokeep/{1}", appTypeIdToReplace, appTypeIdToKeep));
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000EBA9 File Offset: 0x0000CDA9
		public IList<BaseBasicAppointmentDTO> FreeTimeSearch(FreeTimeSearchContextDTO FreeTimeSearchContext)
		{
			return base.Post<FreeTimeSearchContextDTO, IList<BaseBasicAppointmentDTO>>(FreeTimeSearchContext, "appointment/freetimesearch");
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000EBB7 File Offset: 0x0000CDB7
		public AppointmentsWithAvailabilityAndTimetableDTO LoadAppointmentsAndAvailability(AppointmentLoadOptionsDTO LoadOptions)
		{
			return base.Post<AppointmentLoadOptionsDTO, AppointmentsWithAvailabilityAndTimetableDTO>(LoadOptions, "appointment/loadappointmentsandavailability");
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
		public async Task<AppointmentsWithAvailabilityAndTimetableDTO> LoadAppointmentsAndAvailabilityAsync(AppointmentLoadOptionsDTO LoadOptions)
		{
			return await this.PostAsync<AppointmentLoadOptionsDTO, AppointmentsWithAvailabilityAndTimetableDTO>(LoadOptions, "appointment/loadappointmentsandavailability").ConfigureAwait(false);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000EC15 File Offset: 0x0000CE15
		public IList<BaseBasicAppointmentDTO> LoadBasicAppointmentInformationByUserAndDateRange(int PersonId, DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			return base.GetMany<BaseBasicAppointmentDTO>(string.Format("appointment/basicappointment/pid/{0}/range/{1}/{2}?hidecancelled={3}", new object[]
			{
				PersonId,
				StartDate,
				EndDate,
				HideCancelled
			}), true);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000EC54 File Offset: 0x0000CE54
		public DatesAndAppointmentsWithAvailabilityAndTimetable LoadAppointmentsFromDatabase(LoadAppointmentsFromDatabaseParameters loadAppsParams)
		{
			IList<DateTime> dateTimes = loadAppsParams.dateTimes;
			if (dateTimes.Count < 1)
			{
				return null;
			}
			DateTime date = dateTimes[0].Date;
			DateTime ed = dateTimes[dateTimes.Count - 1];
			List<AppType> list;
			if (loadAppsParams.overrideAppTypeIdsToShow == null)
			{
				list = null;
			}
			else
			{
				list = new List<AppType>(loadAppsParams.overrideAppTypeIdsToShow.Length);
				list.AddRange(from appTypeId in loadAppsParams.overrideAppTypeIdsToShow
				select new AppType
				{
					AppTypeId = appTypeId
				});
			}
			IList<int> appTypeIds;
			if (list == null)
			{
				appTypeIds = null;
			}
			else
			{
				appTypeIds = list.ConvertAll<int>((AppType g) => g.AppTypeId);
			}
			AppointmentsWithAvailabilityAndTimetableDTO appsWithAvailabilityAndTimetable = this.LoadAppointmentsFromDatabase(appTypeIds, date, ed, loadAppsParams.pids, loadAppsParams.hideCancelledAppointments, loadAppsParams.perStudentShowIconsForFilledOutPerStudentScreensOnAppointments, loadAppsParams.anonymousShowIconsForFilledOutAnonymousScreensOnAppointments, loadAppsParams.studentPids);
			return new DatesAndAppointmentsWithAvailabilityAndTimetable
			{
				AppsWithAvailabilityAndTimetable = appsWithAvailabilityAndTimetable,
				DateTimes = dateTimes
			};
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000ED44 File Offset: 0x0000CF44
		public async Task<DatesAndAppointmentsWithAvailabilityAndTimetable> LoadAppointmentsFromDatabaseAsync(LoadAppointmentsFromDatabaseParameters loadAppsParams)
		{
			IList<DateTime> dateTimes = loadAppsParams.dateTimes;
			DatesAndAppointmentsWithAvailabilityAndTimetable result;
			if (dateTimes.Count < 1)
			{
				result = null;
			}
			else
			{
				DateTime date = dateTimes[0].Date;
				DateTime ed = dateTimes[dateTimes.Count - 1];
				List<AppType> list;
				if (loadAppsParams.overrideAppTypeIdsToShow == null)
				{
					list = null;
				}
				else
				{
					list = new List<AppType>(loadAppsParams.overrideAppTypeIdsToShow.Length);
					list.AddRange(from appTypeId in loadAppsParams.overrideAppTypeIdsToShow
					select new AppType
					{
						AppTypeId = appTypeId
					});
				}
				IList<int> appTypeIds;
				if (list == null)
				{
					appTypeIds = null;
				}
				else
				{
					appTypeIds = list.ConvertAll<int>((AppType g) => g.AppTypeId);
				}
				AppointmentsWithAvailabilityAndTimetableDTO appsWithAvailabilityAndTimetable = await this.LoadAppointmentsFromDatabaseAsync(appTypeIds, date, ed, loadAppsParams.pids, loadAppsParams.hideCancelledAppointments, loadAppsParams.perStudentShowIconsForFilledOutPerStudentScreensOnAppointments, loadAppsParams.anonymousShowIconsForFilledOutAnonymousScreensOnAppointments, loadAppsParams.studentPids).ConfigureAwait(false);
				result = new DatesAndAppointmentsWithAvailabilityAndTimetable
				{
					AppsWithAvailabilityAndTimetable = appsWithAvailabilityAndTimetable,
					DateTimes = dateTimes
				};
			}
			return result;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000ED94 File Offset: 0x0000CF94
		public AppointmentsWithAvailabilityAndTimetableDTO LoadAppointmentsFromDatabase(IList<int> appTypeIds, DateTime sd, DateTime ed, IList<int> pids, bool hideCancelledAppointments, bool perStudentShowIconsForFilledOutPerStudentScreensOnAppointments, bool anonymousShowIconsForFilledOutAnonymousScreensOnAppointments, IList<int> studentPersonIdsForTimetableLoad)
		{
			IAppointmentClientManager appointmentClientManager = ObjectFactory.Resolve<IAppointmentClientManager>();
			AppointmentLoadOptionsDTO loadOptions = new AppointmentLoadOptionsDTO
			{
				LoadRecurringSchedule = true,
				PersonIds = pids,
				AppointmentTypeIds = appTypeIds,
				HideCancelledAppointments = hideCancelledAppointments,
				LoadPerStudentDataIcons = perStudentShowIconsForFilledOutPerStudentScreensOnAppointments,
				LoadPerAnonymousDataIcons = anonymousShowIconsForFilledOutAnonymousScreensOnAppointments,
				StartDateTime = sd,
				EndDateTime = ed,
				StudentPersonIdsForTimetableLoad = studentPersonIdsForTimetableLoad
			};
			return appointmentClientManager.LoadAppointmentsAndAvailability(loadOptions);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		public async Task<AppointmentsWithAvailabilityAndTimetableDTO> LoadAppointmentsFromDatabaseAsync(IList<int> appTypeIds, DateTime sd, DateTime ed, IList<int> pids, bool hideCancelledAppointments, bool perStudentShowIconsForFilledOutPerStudentScreensOnAppointments, bool anonymousShowIconsForFilledOutAnonymousScreensOnAppointments, IList<int> studentPersonIdsForTimetableLoad)
		{
			IAppointmentClientManager appointmentClientManager = ObjectFactory.Resolve<IAppointmentClientManager>();
			AppointmentLoadOptionsDTO loadOptions = new AppointmentLoadOptionsDTO
			{
				LoadRecurringSchedule = true,
				PersonIds = pids,
				AppointmentTypeIds = appTypeIds,
				HideCancelledAppointments = hideCancelledAppointments,
				LoadPerStudentDataIcons = perStudentShowIconsForFilledOutPerStudentScreensOnAppointments,
				LoadPerAnonymousDataIcons = anonymousShowIconsForFilledOutAnonymousScreensOnAppointments,
				StartDateTime = sd,
				EndDateTime = ed,
				StudentPersonIdsForTimetableLoad = studentPersonIdsForTimetableLoad
			};
			return await appointmentClientManager.LoadAppointmentsAndAvailabilityAsync(loadOptions).ConfigureAwait(false);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000EE7C File Offset: 0x0000D07C
		public void UpdateAppointmentDateAndTime(int AppointmentId, DateTime NewStartDateTime, DateTime NewEndDateTime)
		{
			UpdateAppointmentDateAndTimeReq updateAppointmentDateAndTimeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAppointmentDateAndTimeReq>();
			updateAppointmentDateAndTimeReq.AppointmentId = AppointmentId;
			updateAppointmentDateAndTimeReq.NewStartDateTime = NewStartDateTime;
			updateAppointmentDateAndTimeReq.NewEndDateTime = NewEndDateTime;
			base.Put<UpdateAppointmentDateAndTimeReq>(updateAppointmentDateAndTimeReq, "appointment/appointmentdateandtime");
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		public AppointmentDTO LoadAppointmentWithSpecialPermissions(int appointmentId, out IList<eAppointmentPermissionRestriction> permissionRestrictions)
		{
			LoadAppointmentWithSpecialPermissionsResp loadAppointmentWithSpecialPermissionsResp = base.Get<LoadAppointmentWithSpecialPermissionsResp>(string.Format("appointment/appointmentwithspecialpermissisions/appid/{0}", appointmentId), true);
			permissionRestrictions = loadAppointmentWithSpecialPermissionsResp.PermissionRestrictions;
			return loadAppointmentWithSpecialPermissionsResp.Appointment;
		}
	}
}
