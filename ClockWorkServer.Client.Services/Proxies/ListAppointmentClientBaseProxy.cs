using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200001F RID: 31
	internal class ListAppointmentClientBaseProxy : ClientBase<IListAppointment>, IListAppointment, IService
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000064B9 File Offset: 0x000046B9
		public ListAppointmentClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000064C4 File Offset: 0x000046C4
		public ListAppointmentClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000064D0 File Offset: 0x000046D0
		public CancelListAppointmentResp CancelListAppointment(CancelListAppointmentReq Request)
		{
			return base.Channel.CancelListAppointment(Request);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000064EE File Offset: 0x000046EE
		public void CreateAvailabilities(CreateAvailabilitiesReq Request)
		{
			base.Channel.CreateAvailabilities(Request);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000064FE File Offset: 0x000046FE
		public void CreateClosedDay(CreateClosedDayReq Request)
		{
			base.Channel.CreateClosedDay(Request);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006510 File Offset: 0x00004710
		public CreateListAppointmentResp CreateListAppointment(CreateListAppointmentReq Request)
		{
			return base.Channel.CreateListAppointment(Request);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000652E File Offset: 0x0000472E
		public void DeleteAvailability(DeleteAvailabilityReq Request)
		{
			base.Channel.DeleteAvailability(Request);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000653E File Offset: 0x0000473E
		public void DeleteClosedDay(DeleteClosedDayReq Request)
		{
			base.Channel.DeleteClosedDay(Request);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006550 File Offset: 0x00004750
		public DeleteListAppointmentResp DeleteListAppointment(DeleteListAppointmentReq Request)
		{
			return base.Channel.DeleteListAppointment(Request);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006570 File Offset: 0x00004770
		public FreeTimeSearchResp FreeTimeSearch(FreeTimeSearchReq Request)
		{
			return base.Channel.FreeTimeSearch(Request);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006590 File Offset: 0x00004790
		public IsDayClosedResp IsDayClosed(IsDayClosedReq Request)
		{
			return base.Channel.IsDayClosed(Request);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000065B0 File Offset: 0x000047B0
		public LoadAppointmentsResp LoadAppointments(LoadAppointmentsReq Request)
		{
			return base.Channel.LoadAppointments(Request);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000065D0 File Offset: 0x000047D0
		public LoadAppointmentsWithAvailabilityResp LoadAppointmentsWithAvailability(LoadAppointmentsWithAvailabilityReq Request)
		{
			return base.Channel.LoadAppointmentsWithAvailability(Request);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000065F0 File Offset: 0x000047F0
		public LoadAvailabilityResp LoadAvailability(LoadAvailabilityReq Request)
		{
			return base.Channel.LoadAvailability(Request);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006610 File Offset: 0x00004810
		public LoadClosedDaysResp LoadClosedDays(LoadClosedDaysReq Request)
		{
			return base.Channel.LoadClosedDays(Request);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006630 File Offset: 0x00004830
		public LoadOverlappingAvailabilitiesResp LoadOverlappingAvailabilities(LoadOverlappingAvailabilitiesReq Request)
		{
			return base.Channel.LoadOverlappingAvailabilities(Request);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006650 File Offset: 0x00004850
		public MarkListAppointmentAsTentativeResp MarkListAppointmentAsTentative(MarkListAppointmentAsTentativeReq Request)
		{
			return base.Channel.MarkListAppointmentAsTentative(Request);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006670 File Offset: 0x00004870
		public PrintMedicalCalendarResp PrintMedicalCalendar(PrintMedicalCalendarReq Request)
		{
			return base.Channel.PrintMedicalCalendar(Request);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006690 File Offset: 0x00004890
		public UnCancelListAppointmentResp UnCancelListAppointment(UnCancelListAppointmentReq Request)
		{
			return base.Channel.UnCancelListAppointment(Request);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000066B0 File Offset: 0x000048B0
		public UnMarkListAppointmentAsTentativeResp UnMarkListAppointmentAsTentative(UnMarkListAppointmentAsTentativeReq Request)
		{
			return base.Channel.UnMarkListAppointmentAsTentative(Request);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000066CE File Offset: 0x000048CE
		public void UpdateAvailability(UpdateAvailabilityReq Request)
		{
			base.Channel.UpdateAvailability(Request);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000066E0 File Offset: 0x000048E0
		public UpdateListAppointmentResp UpdateListAppointment(UpdateListAppointmentReq Request)
		{
			return base.Channel.UpdateListAppointment(Request);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006700 File Offset: 0x00004900
		public LoadAppointmentByIdResp LoadAppointmentById(LoadAppointmentByIdReq Request)
		{
			return base.Channel.LoadAppointmentById(Request);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000671E File Offset: 0x0000491E
		public void MarkConfirmed(MarkConfirmedReq Request)
		{
			base.Channel.MarkConfirmed(Request);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000672E File Offset: 0x0000492E
		public void MarkIn(MarkInReq Request)
		{
			base.Channel.MarkIn(Request);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000673E File Offset: 0x0000493E
		public void MarkNoShow(MarkNoShowReq Request)
		{
			base.Channel.MarkNoShow(Request);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006750 File Offset: 0x00004950
		public LoadSingleDayAvailabilityStatusesByUserResp LoadSingleDayAvailabilityStatusesByUser(LoadSingleDayAvailabilityStatusesByUserReq Request)
		{
			return base.Channel.LoadSingleDayAvailabilityStatusesByUser(Request);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006770 File Offset: 0x00004970
		public LoadAvailabilityByIdResp LoadAvailabilityById(LoadAvailabilityByIdReq Request)
		{
			return base.Channel.LoadAvailabilityById(Request);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006790 File Offset: 0x00004990
		public LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithReq Request)
		{
			return base.Channel.LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(Request);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000067AE File Offset: 0x000049AE
		public void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request)
		{
			base.Channel.InsertOrUpdateAppointmentMemo(Request);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000067BE File Offset: 0x000049BE
		public void FixAvailabilityAppointmentMappings(FixAvailabilityAppointmentMappingsReq Request)
		{
			base.Channel.FixAvailabilityAppointmentMappings(Request);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000067D0 File Offset: 0x000049D0
		public CreateAvailability2MarkerResp CreateAvailability2Marker(CreateAvailability2MarkerReq Request)
		{
			return base.Channel.CreateAvailability2Marker(Request);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000067EE File Offset: 0x000049EE
		public void DeleteAvailability2Marker(DeleteAvailability2MarkerReq Request)
		{
			base.Channel.DeleteAvailability2Marker(Request);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006800 File Offset: 0x00004A00
		public LoadAvailability2MarkersResp LoadAvailability2Markers(LoadAvailability2MarkersReq Request)
		{
			return base.Channel.LoadAvailability2Markers(Request);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000681E File Offset: 0x00004A1E
		public void UpdateAvailability2Marker(UpdateAvailability2MarkerReq Request)
		{
			base.Channel.UpdateAvailability2Marker(Request);
		}
	}
}
