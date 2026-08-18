using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsCalendar
{
	// Token: 0x0200009A RID: 154
	public class AppointmentClientManager : IAppointmentClientManager, IWebService
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00018F50 File Offset: 0x00017150
		public int CreateAppointment(AppointmentDTO Appointment)
		{
			CreateAppointmentReq createAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateAppointmentReq>();
			createAppointmentReq.Appointment = Appointment;
			int appointmentId = ClientServiceFactory.GetClientInstance<IAppointment>().CreateAppointment(createAppointmentReq).AppointmentId;
			Appointment.AppointmentId = appointmentId;
			AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentWasCreatedAsync(Appointment);
			return appointmentId;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00018F9C File Offset: 0x0001719C
		public void UpdateAppointment(AppointmentDTO Appointment)
		{
			AppointmentDTO appointmentDTO = this.LoadAppointment(Appointment.AppointmentId);
			UpdateAppointmentReq updateAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAppointmentReq>();
			updateAppointmentReq.Appointment = Appointment;
			ClientServiceFactory.GetClientInstance<IAppointment>().UpdateAppointment(updateAppointmentReq);
			List<AttendeeDTO> collection = (from f in appointmentDTO.Attendees
			where Appointment.Attendees.FirstOrDefault((AttendeeDTO g) => g.Person.PersonId == f.Person.PersonId) == null
			select f).ToList<AttendeeDTO>();
			Appointment.Attendees.AddRange(collection);
			AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(Appointment);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00019030 File Offset: 0x00017230
		public void DeleteAppointment(int AppointmentId)
		{
			AppointmentDTO appointment = this.LoadAppointment(AppointmentId);
			DeleteAppointmentReq deleteAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAppointmentReq>();
			deleteAppointmentReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IAppointment>().DeleteAppointment(deleteAppointmentReq);
			AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(appointment);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00019074 File Offset: 0x00017274
		public void DeleteAppointmentWithoutFiringNotifications(int AppointmentId)
		{
			AppointmentDTO appointmentDTO = this.LoadAppointment(AppointmentId);
			DeleteAppointmentReq deleteAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAppointmentReq>();
			deleteAppointmentReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IAppointment>().DeleteAppointment(deleteAppointmentReq);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000190AC File Offset: 0x000172AC
		public IList<AppointmentDTO> LoadAppointments(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime)
		{
			LoadAppointmentsReq loadAppointmentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentsReq>();
			loadAppointmentsReq.PersonIds = PersonIds;
			loadAppointmentsReq.AppTypeIds = AppTypeIds;
			loadAppointmentsReq.LoadPerStudentDataIcons = LoadPerStudentDataIcons;
			loadAppointmentsReq.LoadPerAnonymousDataIcons = LoadPerAnonymousDataIcons;
			loadAppointmentsReq.StartDateTime = StartDateTime;
			loadAppointmentsReq.EndDateTime = EndDateTime;
			loadAppointmentsReq.HideCancelled = HideCancelled;
			return ClientServiceFactory.GetClientInstance<IAppointment>().LoadAppointments(loadAppointmentsReq).Appointments;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00019118 File Offset: 0x00017318
		public AppointmentDTO LoadAppointment(int AppointmentId)
		{
			LoadAppointmentByIdReq loadAppointmentByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentByIdReq>();
			loadAppointmentByIdReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<IAppointment>().LoadAppointmentById(loadAppointmentByIdReq).Appointment;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00019150 File Offset: 0x00017350
		public LoadAppointmentExtendedInfoResp LoadAppointmentExtendedInfo(int AppointmentId)
		{
			LoadAppointmentExtendedInfoReq loadAppointmentExtendedInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentExtendedInfoReq>();
			loadAppointmentExtendedInfoReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<IAppointment>().LoadAppointmentExtendedInfo(loadAppointmentExtendedInfoReq);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00019180 File Offset: 0x00017380
		public IList<AppointmentDTO> LoadAppointmentsWithSpecialPermissions(IList<int> PersonIds, IList<int> AppTypeIds, bool HideCancelled, DateTime StartDateTime, int NumDays, out IDictionary<int, IList<eAppointmentPermissionRestriction>> permissionRestrictions)
		{
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			LoadAppointmentsWithSpecialPermissionsReq loadAppointmentsWithSpecialPermissionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentsWithSpecialPermissionsReq>();
			loadAppointmentsWithSpecialPermissionsReq.PersonIds = PersonIds;
			loadAppointmentsWithSpecialPermissionsReq.AppTypeIds = AppTypeIds;
			loadAppointmentsWithSpecialPermissionsReq.HideCancelled = HideCancelled;
			loadAppointmentsWithSpecialPermissionsReq.StartDateTime = StartDateTime;
			loadAppointmentsWithSpecialPermissionsReq.NumDays = NumDays;
			LoadAppointmentsWithSpecialPermissionsResp loadAppointmentsWithSpecialPermissionsResp = ClientServiceFactory.GetClientInstance<IAppointment>().LoadAppointmentsWithSpecialPermissions(loadAppointmentsWithSpecialPermissionsReq);
			permissionRestrictions = ((loadAppointmentsWithSpecialPermissionsResp != null) ? loadAppointmentsWithSpecialPermissionsResp.PermissionRestrictions : null);
			return (loadAppointmentsWithSpecialPermissionsResp != null) ? loadAppointmentsWithSpecialPermissionsResp.Appointments : null;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000191F4 File Offset: 0x000173F4
		public void CancelAppointment(int AppointmentId, AppCancelInfoDTO CancelInfo)
		{
			AppointmentDTO appointment = this.LoadAppointment(AppointmentId);
			CancelAppointmentReq cancelAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelAppointmentReq>();
			cancelAppointmentReq.AppointmentId = AppointmentId;
			cancelAppointmentReq.AppointmentCancelInfo = CancelInfo;
			ClientServiceFactory.GetClientInstance<IAppointment>().CancelAppointment(cancelAppointmentReq);
			AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(appointment);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00019240 File Offset: 0x00017440
		public void UnCancelAppointment(int AppointmentId)
		{
			AppointmentDTO appointment = this.LoadAppointment(AppointmentId);
			UnCancelAppointmentReq unCancelAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UnCancelAppointmentReq>();
			unCancelAppointmentReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IAppointment>().UnCancelAppointment(unCancelAppointmentReq);
			AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(appointment);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00019284 File Offset: 0x00017484
		public void MarkAppointmentTentative(int AppointmentId)
		{
			AppointmentDTO appointment = this.LoadAppointment(AppointmentId);
			MarkAppointmentTentativeReq markAppointmentTentativeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkAppointmentTentativeReq>();
			markAppointmentTentativeReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IAppointment>().MarkAppointmentTentative(markAppointmentTentativeReq);
			AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(appointment);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000192C8 File Offset: 0x000174C8
		public void UnMarkAppointmentTentative(int AppointmentId)
		{
			AppointmentDTO appointment = this.LoadAppointment(AppointmentId);
			UnMarkAppointmentTentativeReq unMarkAppointmentTentativeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UnMarkAppointmentTentativeReq>();
			unMarkAppointmentTentativeReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IAppointment>().UnMarkAppointmentTentative(unMarkAppointmentTentativeReq);
			AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(appointment);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000387F File Offset: 0x00001A7F
		public int RecoverDeletedAppointment(int AppointmentId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000387F File Offset: 0x00001A7F
		public void MergeAllAppointments(int PersonIdNew, int PersonIdOld)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001930C File Offset: 0x0001750C
		public AppointmentDTO LoadDeletedAppointmentById(int AppointmentId)
		{
			LoadDeletedAppointmentByIdReq loadDeletedAppointmentByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDeletedAppointmentByIdReq>();
			loadDeletedAppointmentByIdReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<IAppointment>().LoadDeletedAppointmentById(loadDeletedAppointmentByIdReq).Appointment;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00019344 File Offset: 0x00017544
		public void UpdateAppointmentParts(AppointmentDTO Appointment, eAppointmentPart PartsToUpdate)
		{
			UpdateCalendarAppointmentPartsReq updateCalendarAppointmentPartsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCalendarAppointmentPartsReq>();
			updateCalendarAppointmentPartsReq.Appointment = Appointment;
			updateCalendarAppointmentPartsReq.PartsToUpdate = PartsToUpdate;
			ClientServiceFactory.GetClientInstance<IAppointment>().UpdateCalendarAppointmentParts(updateCalendarAppointmentPartsReq);
			AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(Appointment);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00019388 File Offset: 0x00017588
		public void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText)
		{
			InsertOrUpdateAppointmentMemoReq insertOrUpdateAppointmentMemoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentMemoReq>();
			insertOrUpdateAppointmentMemoReq.AppointmentId = AppointmentId;
			insertOrUpdateAppointmentMemoReq.MemoText = MemoText;
			ClientServiceFactory.GetClientInstance<IAppointment>().InsertOrUpdateAppointmentMemo(insertOrUpdateAppointmentMemoReq);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000193C0 File Offset: 0x000175C0
		public int GetNumberOfAppointmentsWithAppType(int appTypeId)
		{
			GetNumberOfAppointmentsWithAppTypeReq getNumberOfAppointmentsWithAppTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetNumberOfAppointmentsWithAppTypeReq>();
			getNumberOfAppointmentsWithAppTypeReq.AppTypeId = appTypeId;
			return ClientServiceFactory.GetClientInstance<IAppointment>().GetNumberOfAppointmentsWithAppType(getNumberOfAppointmentsWithAppTypeReq).NumberOfAppointments;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000193F8 File Offset: 0x000175F8
		public void SwapAppointmentTypeForAllAppointments(int appTypeIdToReplace, int appTypeIdToKeep)
		{
			SwapAppointmentTypeForAllAppointmentsReq swapAppointmentTypeForAllAppointmentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SwapAppointmentTypeForAllAppointmentsReq>();
			swapAppointmentTypeForAllAppointmentsReq.AppTypeIdToReplace = appTypeIdToReplace;
			swapAppointmentTypeForAllAppointmentsReq.AppTypeIdToKeep = appTypeIdToKeep;
			ClientServiceFactory.GetClientInstance<IAppointment>().SwapAppointmentTypeForAllAppointments(swapAppointmentTypeForAllAppointmentsReq);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00019430 File Offset: 0x00017630
		public IList<BaseBasicAppointmentDTO> FreeTimeSearch(FreeTimeSearchContextDTO FreeTimeSearchContext)
		{
			FreeTimeSearchReq freeTimeSearchReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FreeTimeSearchReq>();
			freeTimeSearchReq.FreeTimeSearchContext = FreeTimeSearchContext;
			return ClientServiceFactory.GetClientInstance<IAppointment>().FreeTimeSearch(freeTimeSearchReq).AvailableSlots;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00019468 File Offset: 0x00017668
		public AppointmentsWithAvailabilityAndTimetableDTO LoadAppointmentsAndAvailability(AppointmentLoadOptionsDTO LoadOptions)
		{
			LoadAppointmentsAndAvailabilityReq loadAppointmentsAndAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentsAndAvailabilityReq>();
			loadAppointmentsAndAvailabilityReq.LoadOptions = LoadOptions;
			return ClientServiceFactory.GetClientInstance<IAppointment>().LoadAppointmentsAndAvailability(loadAppointmentsAndAvailabilityReq).AppointmentsWithAvailabilityAndTimetable;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000194A0 File Offset: 0x000176A0
		[DebuggerStepThrough]
		public Task<AppointmentsWithAvailabilityAndTimetableDTO> LoadAppointmentsAndAvailabilityAsync(AppointmentLoadOptionsDTO LoadOptions)
		{
			AppointmentClientManager.<LoadAppointmentsAndAvailabilityAsync>d__21 <LoadAppointmentsAndAvailabilityAsync>d__ = new AppointmentClientManager.<LoadAppointmentsAndAvailabilityAsync>d__21();
			<LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AppointmentsWithAvailabilityAndTimetableDTO>.Create();
			<LoadAppointmentsAndAvailabilityAsync>d__.<>4__this = this;
			<LoadAppointmentsAndAvailabilityAsync>d__.LoadOptions = LoadOptions;
			<LoadAppointmentsAndAvailabilityAsync>d__.<>1__state = -1;
			<LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder.Start<AppointmentClientManager.<LoadAppointmentsAndAvailabilityAsync>d__21>(ref <LoadAppointmentsAndAvailabilityAsync>d__);
			return <LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000194EC File Offset: 0x000176EC
		public IList<BaseBasicAppointmentDTO> LoadBasicAppointmentInformationByUserAndDateRange(int PersonId, DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			LoadBasicAppointmentInformationByUserAndDateRangeReq loadBasicAppointmentInformationByUserAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadBasicAppointmentInformationByUserAndDateRangeReq>();
			loadBasicAppointmentInformationByUserAndDateRangeReq.PersonId = PersonId;
			loadBasicAppointmentInformationByUserAndDateRangeReq.StartDate = StartDate;
			loadBasicAppointmentInformationByUserAndDateRangeReq.EndDate = EndDate;
			loadBasicAppointmentInformationByUserAndDateRangeReq.HideCancelledAppointments = HideCancelled;
			return ClientServiceFactory.GetClientInstance<IAppointment>().LoadBasicAppointmentInformationByUserAndDateRange(loadBasicAppointmentInformationByUserAndDateRangeReq).Appointments;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001953C File Offset: 0x0001773C
		public DatesAndAppointmentsWithAvailabilityAndTimetable LoadAppointmentsFromDatabase(LoadAppointmentsFromDatabaseParameters loadAppsParams)
		{
			IList<DateTime> dateTimes = loadAppsParams.dateTimes;
			bool flag = dateTimes.Count < 1;
			DatesAndAppointmentsWithAvailabilityAndTimetable result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DateTime date = dateTimes[0].Date;
				DateTime ed = dateTimes[dateTimes.Count - 1];
				bool flag2 = loadAppsParams.overrideAppTypeIdsToShow == null;
				List<AppType> list;
				if (flag2)
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
				result = new DatesAndAppointmentsWithAvailabilityAndTimetable
				{
					AppsWithAvailabilityAndTimetable = appsWithAvailabilityAndTimetable,
					DateTimes = dateTimes
				};
			}
			return result;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001964C File Offset: 0x0001784C
		[DebuggerStepThrough]
		public Task<DatesAndAppointmentsWithAvailabilityAndTimetable> LoadAppointmentsFromDatabaseAsync(LoadAppointmentsFromDatabaseParameters loadAppsParams)
		{
			AppointmentClientManager.<LoadAppointmentsFromDatabaseAsync>d__24 <LoadAppointmentsFromDatabaseAsync>d__ = new AppointmentClientManager.<LoadAppointmentsFromDatabaseAsync>d__24();
			<LoadAppointmentsFromDatabaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DatesAndAppointmentsWithAvailabilityAndTimetable>.Create();
			<LoadAppointmentsFromDatabaseAsync>d__.<>4__this = this;
			<LoadAppointmentsFromDatabaseAsync>d__.loadAppsParams = loadAppsParams;
			<LoadAppointmentsFromDatabaseAsync>d__.<>1__state = -1;
			<LoadAppointmentsFromDatabaseAsync>d__.<>t__builder.Start<AppointmentClientManager.<LoadAppointmentsFromDatabaseAsync>d__24>(ref <LoadAppointmentsFromDatabaseAsync>d__);
			return <LoadAppointmentsFromDatabaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00019698 File Offset: 0x00017898
		public AppointmentsWithAvailabilityAndTimetableDTO LoadAppointmentsFromDatabase(IList<int> appTypeIds, DateTime sd, DateTime ed, IList<int> pids, bool hideCancelledAppointments, bool perStudentShowIconsForFilledOutPerStudentScreensOnAppointments, bool anonymousShowIconsForFilledOutAnonymousScreensOnAppointments, IList<int> studentPersonIdsForTimetableLoad)
		{
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
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

		// Token: 0x060005BF RID: 1471 RVA: 0x0001970C File Offset: 0x0001790C
		[DebuggerStepThrough]
		public Task<AppointmentsWithAvailabilityAndTimetableDTO> LoadAppointmentsFromDatabaseAsync(IList<int> appTypeIds, DateTime sd, DateTime ed, IList<int> pids, bool hideCancelledAppointments, bool perStudentShowIconsForFilledOutPerStudentScreensOnAppointments, bool anonymousShowIconsForFilledOutAnonymousScreensOnAppointments, IList<int> studentPersonIdsForTimetableLoad)
		{
			AppointmentClientManager.<LoadAppointmentsFromDatabaseAsync>d__26 <LoadAppointmentsFromDatabaseAsync>d__ = new AppointmentClientManager.<LoadAppointmentsFromDatabaseAsync>d__26();
			<LoadAppointmentsFromDatabaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AppointmentsWithAvailabilityAndTimetableDTO>.Create();
			<LoadAppointmentsFromDatabaseAsync>d__.<>4__this = this;
			<LoadAppointmentsFromDatabaseAsync>d__.appTypeIds = appTypeIds;
			<LoadAppointmentsFromDatabaseAsync>d__.sd = sd;
			<LoadAppointmentsFromDatabaseAsync>d__.ed = ed;
			<LoadAppointmentsFromDatabaseAsync>d__.pids = pids;
			<LoadAppointmentsFromDatabaseAsync>d__.hideCancelledAppointments = hideCancelledAppointments;
			<LoadAppointmentsFromDatabaseAsync>d__.perStudentShowIconsForFilledOutPerStudentScreensOnAppointments = perStudentShowIconsForFilledOutPerStudentScreensOnAppointments;
			<LoadAppointmentsFromDatabaseAsync>d__.anonymousShowIconsForFilledOutAnonymousScreensOnAppointments = anonymousShowIconsForFilledOutAnonymousScreensOnAppointments;
			<LoadAppointmentsFromDatabaseAsync>d__.studentPersonIdsForTimetableLoad = studentPersonIdsForTimetableLoad;
			<LoadAppointmentsFromDatabaseAsync>d__.<>1__state = -1;
			<LoadAppointmentsFromDatabaseAsync>d__.<>t__builder.Start<AppointmentClientManager.<LoadAppointmentsFromDatabaseAsync>d__26>(ref <LoadAppointmentsFromDatabaseAsync>d__);
			return <LoadAppointmentsFromDatabaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00019790 File Offset: 0x00017990
		public void UpdateAppointmentDateAndTime(int AppointmentId, DateTime NewStartDateTime, DateTime NewEndDateTime)
		{
			UpdateAppointmentDateAndTimeReq updateAppointmentDateAndTimeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAppointmentDateAndTimeReq>();
			updateAppointmentDateAndTimeReq.AppointmentId = AppointmentId;
			updateAppointmentDateAndTimeReq.NewStartDateTime = NewStartDateTime;
			updateAppointmentDateAndTimeReq.NewEndDateTime = NewEndDateTime;
			ClientServiceFactory.GetClientInstance<IAppointment>().UpdateAppointmentDateAndTime(updateAppointmentDateAndTimeReq);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000197D0 File Offset: 0x000179D0
		public AppointmentDTO LoadAppointmentWithSpecialPermissions(int appointmentId, out IList<eAppointmentPermissionRestriction> permissionRestrictions)
		{
			LoadAppointmentWithSpecialPermissionsReq loadAppointmentWithSpecialPermissionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentWithSpecialPermissionsReq>();
			loadAppointmentWithSpecialPermissionsReq.AppointmentId = appointmentId;
			LoadAppointmentWithSpecialPermissionsResp loadAppointmentWithSpecialPermissionsResp = ClientServiceFactory.GetClientInstance<IAppointment>().LoadAppointmentWithSpecialPermissions(loadAppointmentWithSpecialPermissionsReq);
			permissionRestrictions = ((loadAppointmentWithSpecialPermissionsResp != null) ? loadAppointmentWithSpecialPermissionsResp.PermissionRestrictions : null);
			return (loadAppointmentWithSpecialPermissionsResp != null) ? loadAppointmentWithSpecialPermissionsResp.Appointment : null;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001981C File Offset: 0x00017A1C
		public void CancelAttendeeAppointment(int appointmentId, int personId, AppCancelInfoDTO CancelInfo)
		{
			CancelAttendeeAppointmentReq cancelAttendeeAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelAttendeeAppointmentReq>();
			cancelAttendeeAppointmentReq.AppointmentId = appointmentId;
			cancelAttendeeAppointmentReq.PersonId = personId;
			cancelAttendeeAppointmentReq.CancelInfo = CancelInfo;
			ClientServiceFactory.GetClientInstance<IAppointment>().CancelAttendeeAppointment(cancelAttendeeAppointmentReq);
		}
	}
}
