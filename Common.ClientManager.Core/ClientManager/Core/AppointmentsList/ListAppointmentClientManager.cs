using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsList;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsList;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsList
{
	// Token: 0x02000098 RID: 152
	public class ListAppointmentClientManager : IListAppointmentClientManager, IWebService
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x00018564 File Offset: 0x00016764
		[DebuggerStepThrough]
		private Task NotifyOthersAppointmentChangedOrDeletedAsync(ListAppointmentDTO Appointment)
		{
			ListAppointmentClientManager.<NotifyOthersAppointmentChangedOrDeletedAsync>d__0 <NotifyOthersAppointmentChangedOrDeletedAsync>d__ = new ListAppointmentClientManager.<NotifyOthersAppointmentChangedOrDeletedAsync>d__0();
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>4__this = this;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.Appointment = Appointment;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>1__state = -1;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>t__builder.Start<ListAppointmentClientManager.<NotifyOthersAppointmentChangedOrDeletedAsync>d__0>(ref <NotifyOthersAppointmentChangedOrDeletedAsync>d__);
			return <NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000185B0 File Offset: 0x000167B0
		[DebuggerStepThrough]
		private Task NotifyOthersAppointmentWasCreatedAsync(ListAppointmentDTO Appointment)
		{
			ListAppointmentClientManager.<NotifyOthersAppointmentWasCreatedAsync>d__1 <NotifyOthersAppointmentWasCreatedAsync>d__ = new ListAppointmentClientManager.<NotifyOthersAppointmentWasCreatedAsync>d__1();
			<NotifyOthersAppointmentWasCreatedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOthersAppointmentWasCreatedAsync>d__.<>4__this = this;
			<NotifyOthersAppointmentWasCreatedAsync>d__.Appointment = Appointment;
			<NotifyOthersAppointmentWasCreatedAsync>d__.<>1__state = -1;
			<NotifyOthersAppointmentWasCreatedAsync>d__.<>t__builder.Start<ListAppointmentClientManager.<NotifyOthersAppointmentWasCreatedAsync>d__1>(ref <NotifyOthersAppointmentWasCreatedAsync>d__);
			return <NotifyOthersAppointmentWasCreatedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000185FC File Offset: 0x000167FC
		public BinaryFileDTO PrintMedicalCalendar(DateTime StartDate, int NumDays, IList<PersonBaseDTO> Staff, eFileFormatDTO OutputFormat, bool HideCancelled)
		{
			PrintMedicalCalendarReq printMedicalCalendarReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<PrintMedicalCalendarReq>();
			BaseReportMessageReq baseReportMessageReq = printMedicalCalendarReq;
			ApplicationContext applicationContext = printMedicalCalendarReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			printMedicalCalendarReq.StartDate = StartDate;
			printMedicalCalendarReq.NumDays = NumDays;
			printMedicalCalendarReq.Staff = Staff;
			printMedicalCalendarReq.OutputFormat = OutputFormat;
			printMedicalCalendarReq.HideCancelled = HideCancelled;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().PrintMedicalCalendar(printMedicalCalendarReq).File;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001866C File Offset: 0x0001686C
		public int CreateListAppointment(ListAppointmentDTO Appointment)
		{
			CreateListAppointmentReq createListAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateListAppointmentReq>();
			createListAppointmentReq.Appointment = Appointment;
			int appointmentId = ClientServiceFactory.GetClientInstance<IListAppointment>().CreateListAppointment(createListAppointmentReq).AppointmentId;
			this.NotifyOthersAppointmentWasCreatedAsync(Appointment);
			return appointmentId;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000186AC File Offset: 0x000168AC
		public ListAppointmentDTO LoadAppointmentById(int AppointmentId, bool LoadIsStudentsFirstAppointment = false)
		{
			LoadAppointmentByIdReq loadAppointmentByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentByIdReq>();
			loadAppointmentByIdReq.AppointmentId = AppointmentId;
			loadAppointmentByIdReq.LoadIsStudentsFirstAppointment = LoadIsStudentsFirstAppointment;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().LoadAppointmentById(loadAppointmentByIdReq).Appointment;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000186EC File Offset: 0x000168EC
		public void CancelListAppointment(int AppointmentId)
		{
			CancelListAppointmentReq cancelListAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelListAppointmentReq>();
			cancelListAppointmentReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IListAppointment>().CancelListAppointment(cancelListAppointmentReq);
			this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false));
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00018728 File Offset: 0x00016928
		public void UnCancelListAppointment(int AppointmentId)
		{
			UnCancelListAppointmentReq unCancelListAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UnCancelListAppointmentReq>();
			unCancelListAppointmentReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IListAppointment>().UnCancelListAppointment(unCancelListAppointmentReq);
			this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false));
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00018764 File Offset: 0x00016964
		public void MarkListAppointmentAsTentative(int Appointmentid)
		{
			MarkListAppointmentAsTentativeReq markListAppointmentAsTentativeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkListAppointmentAsTentativeReq>();
			markListAppointmentAsTentativeReq.AppointmentId = Appointmentid;
			ClientServiceFactory.GetClientInstance<IListAppointment>().MarkListAppointmentAsTentative(markListAppointmentAsTentativeReq);
			this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(Appointmentid, false));
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000187A0 File Offset: 0x000169A0
		public void UnMarkListAppointmentAsTentative(int Appointmentid)
		{
			UnMarkListAppointmentAsTentativeReq unMarkListAppointmentAsTentativeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UnMarkListAppointmentAsTentativeReq>();
			unMarkListAppointmentAsTentativeReq.AppointmentId = Appointmentid;
			ClientServiceFactory.GetClientInstance<IListAppointment>().UnMarkListAppointmentAsTentative(unMarkListAppointmentAsTentativeReq);
			this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(Appointmentid, false));
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000187DC File Offset: 0x000169DC
		public void DeleteListAppointment(int AppointmentId)
		{
			ListAppointmentDTO appointment = this.LoadAppointmentById(AppointmentId, false);
			DeleteListAppointmentReq deleteListAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteListAppointmentReq>();
			deleteListAppointmentReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IListAppointment>().DeleteListAppointment(deleteListAppointmentReq);
			this.NotifyOthersAppointmentChangedOrDeletedAsync(appointment);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001881C File Offset: 0x00016A1C
		public void UpdateListAppointment(ListAppointmentDTO Appointment)
		{
			ListAppointmentDTO listAppointmentDTO = this.LoadAppointmentById(Appointment.AppointmentId, false);
			UpdateListAppointmentReq updateListAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateListAppointmentReq>();
			updateListAppointmentReq.Appointment = Appointment;
			ClientServiceFactory.GetClientInstance<IListAppointment>().UpdateListAppointment(updateListAppointmentReq);
			IEnumerable<AttendeeDTO> source = from f in listAppointmentDTO.Attendees
			where Appointment.Attendees.FirstOrDefault((AttendeeDTO g) => g.Person.PersonId == f.Person.PersonId) == null
			select f;
			Appointment.Attendees.AddRange(source.ToList<AttendeeDTO>());
			this.NotifyOthersAppointmentChangedOrDeletedAsync(Appointment);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000188AC File Offset: 0x00016AAC
		public void CreateAvailabilities(List<Availability2ItemDTO> Availabilities)
		{
			CreateAvailabilitiesReq createAvailabilitiesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateAvailabilitiesReq>();
			createAvailabilitiesReq.Availabilities = Availabilities;
			ClientServiceFactory.GetClientInstance<IListAppointment>().CreateAvailabilities(createAvailabilitiesReq);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000188DC File Offset: 0x00016ADC
		public void DeleteAvailability(List<int> AvailabilityIds)
		{
			DeleteAvailabilityReq deleteAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAvailabilityReq>();
			deleteAvailabilityReq.AvailabilityIds = AvailabilityIds;
			ClientServiceFactory.GetClientInstance<IListAppointment>().DeleteAvailability(deleteAvailabilityReq);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001890C File Offset: 0x00016B0C
		public void UpdateAvailability(List<Availability2ItemDTO> Availabilities)
		{
			UpdateAvailabilityReq updateAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAvailabilityReq>();
			updateAvailabilityReq.Availabilities = Availabilities;
			ClientServiceFactory.GetClientInstance<IListAppointment>().UpdateAvailability(updateAvailabilityReq);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001893C File Offset: 0x00016B3C
		public IList<Availability2ItemDTO> LoadOverlappingAvailabilities(int PersonId, DateTime StartDateTime, DateTime EndDateTime)
		{
			LoadOverlappingAvailabilitiesReq loadOverlappingAvailabilitiesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadOverlappingAvailabilitiesReq>();
			loadOverlappingAvailabilitiesReq.PersonId = PersonId;
			loadOverlappingAvailabilitiesReq.StartDateTime = StartDateTime;
			loadOverlappingAvailabilitiesReq.EndDateTime = EndDateTime;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().LoadOverlappingAvailabilities(loadOverlappingAvailabilitiesReq).Items;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00018984 File Offset: 0x00016B84
		public IList<Availability2ItemDTO> FreeTimeSearch(List<int> PersonIds, DateTime StartDateTime, DateTime EndDateTime)
		{
			FreeTimeSearchReq freeTimeSearchReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FreeTimeSearchReq>();
			freeTimeSearchReq.PersonIds = PersonIds;
			freeTimeSearchReq.StartDateTime = StartDateTime;
			freeTimeSearchReq.EndDateTime = EndDateTime;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().FreeTimeSearch(freeTimeSearchReq).Items;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000189C8 File Offset: 0x00016BC8
		public IList<ClosedDayDTO> LoadClosedDays(IList<int> PersonIds, DateTime StartDate, DateTime EndDate)
		{
			LoadClosedDaysReq loadClosedDaysReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadClosedDaysReq>();
			loadClosedDaysReq.PersonIds = PersonIds;
			loadClosedDaysReq.StartDate = StartDate;
			loadClosedDaysReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().LoadClosedDays(loadClosedDaysReq).ClosedDays;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00018A10 File Offset: 0x00016C10
		public ClosedDayDTO IsDayClosed(int PersonId, DateTime Date)
		{
			IsDayClosedReq isDayClosedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsDayClosedReq>();
			isDayClosedReq.PersonId = PersonId;
			isDayClosedReq.Date = Date;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().IsDayClosed(isDayClosedReq).DayClosed;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00018A50 File Offset: 0x00016C50
		public void CreateClosedDay(IList<ClosedDayDTO> ClosedDays)
		{
			CreateClosedDayReq createClosedDayReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateClosedDayReq>();
			createClosedDayReq.ClosedDays = ClosedDays;
			ClientServiceFactory.GetClientInstance<IListAppointment>().CreateClosedDay(createClosedDayReq);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00018A80 File Offset: 0x00016C80
		public void DeleteClosedDay(int PersonId, DateTime Date)
		{
			DeleteClosedDayReq deleteClosedDayReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteClosedDayReq>();
			deleteClosedDayReq.PersonId = PersonId;
			deleteClosedDayReq.Date = Date;
			ClientServiceFactory.GetClientInstance<IListAppointment>().DeleteClosedDay(deleteClosedDayReq);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00018AB8 File Offset: 0x00016CB8
		public IList<Availability2ItemDTO> LoadAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays)
		{
			LoadAvailabilityReq loadAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailabilityReq>();
			loadAvailabilityReq.PersonIds = PersonIds;
			loadAvailabilityReq.StartDate = StartDate;
			loadAvailabilityReq.NumDays = NumDays;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().LoadAvailability(loadAvailabilityReq).Availability;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00018B00 File Offset: 0x00016D00
		public IList<ListAppointmentDTO> LoadAppointments(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment)
		{
			LoadAppointmentsReq loadAppointmentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentsReq>();
			loadAppointmentsReq.PersonIds = PersonIds;
			loadAppointmentsReq.StartDate = StartDate;
			loadAppointmentsReq.NumDays = NumDays;
			loadAppointmentsReq.LoadIsStudentsFirstAppointment = LoadIsStudentsFirstAppointment;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().LoadAppointments(loadAppointmentsReq).Appointments;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00018B50 File Offset: 0x00016D50
		public IList<ListAppointmentOrAvailabilityDTO> LoadAppointmentsWithAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment, bool HideCancelledAppointments)
		{
			LoadAppointmentsWithAvailabilityReq loadAppointmentsWithAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentsWithAvailabilityReq>();
			loadAppointmentsWithAvailabilityReq.PersonIds = PersonIds;
			loadAppointmentsWithAvailabilityReq.StartDate = StartDate;
			loadAppointmentsWithAvailabilityReq.NumDays = NumDays;
			loadAppointmentsWithAvailabilityReq.LoadIsStudentsFirstAppointment = LoadIsStudentsFirstAppointment;
			loadAppointmentsWithAvailabilityReq.HideCancelledAppointments = HideCancelledAppointments;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().LoadAppointmentsWithAvailability(loadAppointmentsWithAvailabilityReq).Appointments;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00018BA8 File Offset: 0x00016DA8
		public void MarkIn(int AppointmentId, bool newIn)
		{
			MarkInReq markInReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkInReq>();
			markInReq.AppointmentId = AppointmentId;
			markInReq.NewIn = newIn;
			ClientServiceFactory.GetClientInstance<IListAppointment>().MarkIn(markInReq);
			this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false));
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00018BEC File Offset: 0x00016DEC
		public void MarkNoShow(int AppointmentId, bool newNoShow)
		{
			MarkNoShowReq markNoShowReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkNoShowReq>();
			markNoShowReq.AppointmentId = AppointmentId;
			markNoShowReq.NewNoShow = newNoShow;
			ClientServiceFactory.GetClientInstance<IListAppointment>().MarkNoShow(markNoShowReq);
			this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false));
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00018C30 File Offset: 0x00016E30
		public void MarkConfirmed(int AppointmentId, bool newConfirmed)
		{
			MarkConfirmedReq markConfirmedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkConfirmedReq>();
			markConfirmedReq.AppointmentId = AppointmentId;
			markConfirmedReq.NewConfirmed = newConfirmed;
			ClientServiceFactory.GetClientInstance<IListAppointment>().MarkConfirmed(markConfirmedReq);
			this.NotifyOthersAppointmentChangedOrDeletedAsync(this.LoadAppointmentById(AppointmentId, false));
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00018C74 File Offset: 0x00016E74
		public Dictionary<DateTime, eAvailabilityCode> LoadSingleDayAvailabilityStatusesByUser(int PersonId, DateTime StartDate, int NumDays)
		{
			LoadSingleDayAvailabilityStatusesByUserReq loadSingleDayAvailabilityStatusesByUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadSingleDayAvailabilityStatusesByUserReq>();
			loadSingleDayAvailabilityStatusesByUserReq.PersonId = PersonId;
			loadSingleDayAvailabilityStatusesByUserReq.StartDate = StartDate;
			loadSingleDayAvailabilityStatusesByUserReq.NumDays = NumDays;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().LoadSingleDayAvailabilityStatusesByUser(loadSingleDayAvailabilityStatusesByUserReq).Items;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00018CBC File Offset: 0x00016EBC
		public void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText)
		{
			InsertOrUpdateAppointmentMemoReq insertOrUpdateAppointmentMemoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentMemoReq>();
			insertOrUpdateAppointmentMemoReq.AppointmentId = AppointmentId;
			insertOrUpdateAppointmentMemoReq.MemoText = MemoText;
			ClientServiceFactory.GetClientInstance<IListAppointment>().InsertOrUpdateAppointmentMemo(insertOrUpdateAppointmentMemoReq);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00018CF4 File Offset: 0x00016EF4
		public void FixAvailabilityAppointmentMappings(DateTime StartDate, DateTime EndDate)
		{
			FixAvailabilityAppointmentMappingsReq fixAvailabilityAppointmentMappingsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FixAvailabilityAppointmentMappingsReq>();
			fixAvailabilityAppointmentMappingsReq.StartDate = StartDate;
			fixAvailabilityAppointmentMappingsReq.EndDate = EndDate;
			ClientServiceFactory.GetClientInstance<IListAppointment>().FixAvailabilityAppointmentMappings(fixAvailabilityAppointmentMappingsReq);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00018D2C File Offset: 0x00016F2C
		public IList<Availability2MarkerDTO> LoadAvailability2Markers()
		{
			LoadAvailability2MarkersReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailability2MarkersReq>();
			return ClientServiceFactory.GetClientInstance<IListAppointment>().LoadAvailability2Markers(request).Markers;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00018D5C File Offset: 0x00016F5C
		public int CreateAvailability2Marker(Availability2MarkerDTO Marker)
		{
			CreateAvailability2MarkerReq createAvailability2MarkerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateAvailability2MarkerReq>();
			createAvailability2MarkerReq.Marker = Marker;
			return ClientServiceFactory.GetClientInstance<IListAppointment>().CreateAvailability2Marker(createAvailability2MarkerReq).Availability2MarkerId;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00018D94 File Offset: 0x00016F94
		public void DeleteAvailability2Marker(int Availability2MarkerId)
		{
			DeleteAvailability2MarkerReq deleteAvailability2MarkerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAvailability2MarkerReq>();
			deleteAvailability2MarkerReq.Availability2MarkerId = Availability2MarkerId;
			ClientServiceFactory.GetClientInstance<IListAppointment>().DeleteAvailability2Marker(deleteAvailability2MarkerReq);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00018DC4 File Offset: 0x00016FC4
		public void UpdateAvailability2Marker(Availability2MarkerDTO Marker)
		{
			UpdateAvailability2MarkerReq updateAvailability2MarkerReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAvailability2MarkerReq>();
			updateAvailability2MarkerReq.Marker = Marker;
			ClientServiceFactory.GetClientInstance<IListAppointment>().UpdateAvailability2Marker(updateAvailability2MarkerReq);
		}
	}
}
