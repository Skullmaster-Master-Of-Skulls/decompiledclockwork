using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000021 RID: 33
	internal class AppointmentClientBaseProxy : ClientBase<IAppointment>, IAppointment, IService
	{
		// Token: 0x060001DC RID: 476 RVA: 0x00006D9B File Offset: 0x00004F9B
		public AppointmentClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006DA6 File Offset: 0x00004FA6
		public AppointmentClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006DB4 File Offset: 0x00004FB4
		public LoadDeletedAppointmentByIdResp LoadDeletedAppointmentById(LoadDeletedAppointmentByIdReq request)
		{
			return base.Channel.LoadDeletedAppointmentById(request);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006DD2 File Offset: 0x00004FD2
		public void CancelAppointment(CancelAppointmentReq request)
		{
			base.Channel.CancelAppointment(request);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006DE2 File Offset: 0x00004FE2
		public void UnCancelAppointment(UnCancelAppointmentReq request)
		{
			base.Channel.UnCancelAppointment(request);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006DF4 File Offset: 0x00004FF4
		public LoadAppointmentsResp LoadAppointments(LoadAppointmentsReq request)
		{
			return base.Channel.LoadAppointments(request);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006E12 File Offset: 0x00005012
		public void UnMarkAppointmentTentative(UnMarkAppointmentTentativeReq request)
		{
			base.Channel.UnMarkAppointmentTentative(request);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006E22 File Offset: 0x00005022
		public void MarkAppointmentTentative(MarkAppointmentTentativeReq request)
		{
			base.Channel.MarkAppointmentTentative(request);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006E34 File Offset: 0x00005034
		public LoadAppointmentByIdResp LoadAppointmentById(LoadAppointmentByIdReq request)
		{
			return base.Channel.LoadAppointmentById(request);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006E54 File Offset: 0x00005054
		public LoadAppointmentExtendedInfoResp LoadAppointmentExtendedInfo(LoadAppointmentExtendedInfoReq request)
		{
			return base.Channel.LoadAppointmentExtendedInfo(request);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006E72 File Offset: 0x00005072
		public void DeleteAppointment(DeleteAppointmentReq request)
		{
			base.Channel.DeleteAppointment(request);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006E84 File Offset: 0x00005084
		public CreateAppointmentResp CreateAppointment(CreateAppointmentReq Request)
		{
			return base.Channel.CreateAppointment(Request);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006EA4 File Offset: 0x000050A4
		public UpdateAppointmentResp UpdateAppointment(UpdateAppointmentReq Request)
		{
			return base.Channel.UpdateAppointment(Request);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006EC2 File Offset: 0x000050C2
		public void UpdateCalendarAppointmentParts(UpdateCalendarAppointmentPartsReq Request)
		{
			base.Channel.UpdateCalendarAppointmentParts(Request);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006ED2 File Offset: 0x000050D2
		public void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request)
		{
			base.Channel.InsertOrUpdateAppointmentMemo(Request);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006EE4 File Offset: 0x000050E4
		public FreeTimeSearchResp FreeTimeSearch(FreeTimeSearchReq Request)
		{
			return base.Channel.FreeTimeSearch(Request);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006F04 File Offset: 0x00005104
		public LoadAppointmentsAndAvailabilityResp LoadAppointmentsAndAvailability(LoadAppointmentsAndAvailabilityReq Request)
		{
			return base.Channel.LoadAppointmentsAndAvailability(Request);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006F24 File Offset: 0x00005124
		[DebuggerStepThrough]
		public Task<LoadAppointmentsAndAvailabilityResp> LoadAppointmentsAndAvailabilityAsync(LoadAppointmentsAndAvailabilityReq Request)
		{
			AppointmentClientBaseProxy.<LoadAppointmentsAndAvailabilityAsync>d__17 <LoadAppointmentsAndAvailabilityAsync>d__ = new AppointmentClientBaseProxy.<LoadAppointmentsAndAvailabilityAsync>d__17();
			<LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAppointmentsAndAvailabilityResp>.Create();
			<LoadAppointmentsAndAvailabilityAsync>d__.<>4__this = this;
			<LoadAppointmentsAndAvailabilityAsync>d__.Request = Request;
			<LoadAppointmentsAndAvailabilityAsync>d__.<>1__state = -1;
			<LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder.Start<AppointmentClientBaseProxy.<LoadAppointmentsAndAvailabilityAsync>d__17>(ref <LoadAppointmentsAndAvailabilityAsync>d__);
			return <LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006F70 File Offset: 0x00005170
		public LoadBasicAppointmentInformationByUserAndDateRangeResp LoadBasicAppointmentInformationByUserAndDateRange(LoadBasicAppointmentInformationByUserAndDateRangeReq Request)
		{
			return base.Channel.LoadBasicAppointmentInformationByUserAndDateRange(Request);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006F90 File Offset: 0x00005190
		public UpdateAppointmentExternalIdResp UpdateAppointmentExternalId(UpdateAppointmentExternalIdReq request)
		{
			return base.Channel.UpdateAppointmentExternalId(request);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006FB0 File Offset: 0x000051B0
		public GetNumberOfAppointmentsWithAppTypeResp GetNumberOfAppointmentsWithAppType(GetNumberOfAppointmentsWithAppTypeReq Request)
		{
			return base.Channel.GetNumberOfAppointmentsWithAppType(Request);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006FD0 File Offset: 0x000051D0
		public SwapAppointmentTypeForAllAppointmentsResp SwapAppointmentTypeForAllAppointments(SwapAppointmentTypeForAllAppointmentsReq Request)
		{
			return base.Channel.SwapAppointmentTypeForAllAppointments(Request);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006FF0 File Offset: 0x000051F0
		public LoadAppointmentsWithSpecialPermissionsResp LoadAppointmentsWithSpecialPermissions(LoadAppointmentsWithSpecialPermissionsReq Request)
		{
			return base.Channel.LoadAppointmentsWithSpecialPermissions(Request);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007010 File Offset: 0x00005210
		public LoadAppointmentWithSpecialPermissionsResp LoadAppointmentWithSpecialPermissions(LoadAppointmentWithSpecialPermissionsReq Request)
		{
			return base.Channel.LoadAppointmentWithSpecialPermissions(Request);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007030 File Offset: 0x00005230
		public UpdateAppointmentDateAndTimeResp UpdateAppointmentDateAndTime(UpdateAppointmentDateAndTimeReq Request)
		{
			return base.Channel.UpdateAppointmentDateAndTime(Request);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007050 File Offset: 0x00005250
		public CancelAttendeeAppointmentResp CancelAttendeeAppointment(CancelAttendeeAppointmentReq Request)
		{
			return base.Channel.CancelAttendeeAppointment(Request);
		}
	}
}
