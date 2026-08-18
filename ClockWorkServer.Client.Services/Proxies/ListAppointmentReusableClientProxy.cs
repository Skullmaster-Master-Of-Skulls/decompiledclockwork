using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200001E RID: 30
	public class ListAppointmentReusableClientProxy : WCFTokenBasedReusableClientProxy<IListAppointment>, IListAppointment, IService
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00005D6A File Offset: 0x00003F6A
		public ListAppointmentReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005D75 File Offset: 0x00003F75
		public ListAppointmentReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005D84 File Offset: 0x00003F84
		public CancelListAppointmentResp CancelListAppointment(CancelListAppointmentReq Request)
		{
			return this.WrapServiceMethod<CancelListAppointmentResp>(() => this.Proxy.CancelListAppointment(Request));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005DBC File Offset: 0x00003FBC
		public void CreateAvailabilities(CreateAvailabilitiesReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.CreateAvailabilities(Request);
			});
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005DF4 File Offset: 0x00003FF4
		public void CreateClosedDay(CreateClosedDayReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.CreateClosedDay(Request);
			});
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005E2C File Offset: 0x0000402C
		public CreateListAppointmentResp CreateListAppointment(CreateListAppointmentReq Request)
		{
			return this.WrapServiceMethod<CreateListAppointmentResp>(() => this.Proxy.CreateListAppointment(Request));
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005E64 File Offset: 0x00004064
		public void DeleteAvailability(DeleteAvailabilityReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAvailability(Request);
			});
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005E9C File Offset: 0x0000409C
		public void DeleteClosedDay(DeleteClosedDayReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteClosedDay(Request);
			});
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005ED4 File Offset: 0x000040D4
		public DeleteListAppointmentResp DeleteListAppointment(DeleteListAppointmentReq Request)
		{
			return this.WrapServiceMethod<DeleteListAppointmentResp>(() => this.Proxy.DeleteListAppointment(Request));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005F0C File Offset: 0x0000410C
		public FreeTimeSearchResp FreeTimeSearch(FreeTimeSearchReq Request)
		{
			return this.WrapServiceMethod<FreeTimeSearchResp>(() => this.Proxy.FreeTimeSearch(Request));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005F44 File Offset: 0x00004144
		public IsDayClosedResp IsDayClosed(IsDayClosedReq Request)
		{
			return this.WrapServiceMethod<IsDayClosedResp>(() => this.Proxy.IsDayClosed(Request));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005F7C File Offset: 0x0000417C
		public LoadAppointmentsResp LoadAppointments(LoadAppointmentsReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentsResp>(() => this.Proxy.LoadAppointments(Request));
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005FB4 File Offset: 0x000041B4
		public LoadAppointmentsWithAvailabilityResp LoadAppointmentsWithAvailability(LoadAppointmentsWithAvailabilityReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentsWithAvailabilityResp>(() => this.Proxy.LoadAppointmentsWithAvailability(Request));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005FEC File Offset: 0x000041EC
		public LoadAvailabilityResp LoadAvailability(LoadAvailabilityReq Request)
		{
			return this.WrapServiceMethod<LoadAvailabilityResp>(() => this.Proxy.LoadAvailability(Request));
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006024 File Offset: 0x00004224
		public LoadClosedDaysResp LoadClosedDays(LoadClosedDaysReq Request)
		{
			return this.WrapServiceMethod<LoadClosedDaysResp>(() => this.Proxy.LoadClosedDays(Request));
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000605C File Offset: 0x0000425C
		public LoadOverlappingAvailabilitiesResp LoadOverlappingAvailabilities(LoadOverlappingAvailabilitiesReq Request)
		{
			return this.WrapServiceMethod<LoadOverlappingAvailabilitiesResp>(() => this.Proxy.LoadOverlappingAvailabilities(Request));
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006094 File Offset: 0x00004294
		public MarkListAppointmentAsTentativeResp MarkListAppointmentAsTentative(MarkListAppointmentAsTentativeReq Request)
		{
			return this.WrapServiceMethod<MarkListAppointmentAsTentativeResp>(() => this.Proxy.MarkListAppointmentAsTentative(Request));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000060CC File Offset: 0x000042CC
		public PrintMedicalCalendarResp PrintMedicalCalendar(PrintMedicalCalendarReq Request)
		{
			return this.WrapServiceMethod<PrintMedicalCalendarResp>(() => this.Proxy.PrintMedicalCalendar(Request));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006104 File Offset: 0x00004304
		public UnCancelListAppointmentResp UnCancelListAppointment(UnCancelListAppointmentReq Request)
		{
			return this.WrapServiceMethod<UnCancelListAppointmentResp>(() => this.Proxy.UnCancelListAppointment(Request));
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000613C File Offset: 0x0000433C
		public UnMarkListAppointmentAsTentativeResp UnMarkListAppointmentAsTentative(UnMarkListAppointmentAsTentativeReq Request)
		{
			return this.WrapServiceMethod<UnMarkListAppointmentAsTentativeResp>(() => this.Proxy.UnMarkListAppointmentAsTentative(Request));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006174 File Offset: 0x00004374
		public void UpdateAvailability(UpdateAvailabilityReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateAvailability(Request);
			});
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000061AC File Offset: 0x000043AC
		public UpdateListAppointmentResp UpdateListAppointment(UpdateListAppointmentReq Request)
		{
			return this.WrapServiceMethod<UpdateListAppointmentResp>(() => this.Proxy.UpdateListAppointment(Request));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000061E4 File Offset: 0x000043E4
		public LoadAppointmentByIdResp LoadAppointmentById(LoadAppointmentByIdReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentByIdResp>(() => this.Proxy.LoadAppointmentById(Request));
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000621C File Offset: 0x0000441C
		public void MarkConfirmed(MarkConfirmedReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.MarkConfirmed(Request);
			});
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006254 File Offset: 0x00004454
		public void MarkIn(MarkInReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.MarkIn(Request);
			});
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000628C File Offset: 0x0000448C
		public void MarkNoShow(MarkNoShowReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.MarkNoShow(Request);
			});
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000062C4 File Offset: 0x000044C4
		public LoadSingleDayAvailabilityStatusesByUserResp LoadSingleDayAvailabilityStatusesByUser(LoadSingleDayAvailabilityStatusesByUserReq Request)
		{
			return this.WrapServiceMethod<LoadSingleDayAvailabilityStatusesByUserResp>(() => this.Proxy.LoadSingleDayAvailabilityStatusesByUser(Request));
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000062FC File Offset: 0x000044FC
		public LoadAvailabilityByIdResp LoadAvailabilityById(LoadAvailabilityByIdReq Request)
		{
			return this.WrapServiceMethod<LoadAvailabilityByIdResp>(() => this.Proxy.LoadAvailabilityById(Request));
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006334 File Offset: 0x00004534
		public LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithReq Request)
		{
			return this.WrapServiceMethod<LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp>(() => this.Proxy.LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(Request));
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000636C File Offset: 0x0000456C
		public void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.InsertOrUpdateAppointmentMemo(Request);
			});
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000063A4 File Offset: 0x000045A4
		public void FixAvailabilityAppointmentMappings(FixAvailabilityAppointmentMappingsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.FixAvailabilityAppointmentMappings(Request);
			});
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000063DC File Offset: 0x000045DC
		public CreateAvailability2MarkerResp CreateAvailability2Marker(CreateAvailability2MarkerReq Request)
		{
			return this.WrapServiceMethod<CreateAvailability2MarkerResp>(() => this.Proxy.CreateAvailability2Marker(Request));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006414 File Offset: 0x00004614
		public void DeleteAvailability2Marker(DeleteAvailability2MarkerReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAvailability2Marker(Request);
			});
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000644C File Offset: 0x0000464C
		public LoadAvailability2MarkersResp LoadAvailability2Markers(LoadAvailability2MarkersReq Request)
		{
			return this.WrapServiceMethod<LoadAvailability2MarkersResp>(() => this.Proxy.LoadAvailability2Markers(Request));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006484 File Offset: 0x00004684
		public void UpdateAvailability2Marker(UpdateAvailability2MarkerReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateAvailability2Marker(Request);
			});
		}
	}
}
