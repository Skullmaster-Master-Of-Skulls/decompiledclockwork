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
	// Token: 0x02000020 RID: 32
	public class AppointmentReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointment>, IAppointment, IService
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000682E File Offset: 0x00004A2E
		public AppointmentReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006839 File Offset: 0x00004A39
		public AppointmentReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006848 File Offset: 0x00004A48
		public void CancelAppointment(CancelAppointmentReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.CancelAppointment(request);
			});
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006880 File Offset: 0x00004A80
		public void UnCancelAppointment(UnCancelAppointmentReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UnCancelAppointment(request);
			});
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000068B8 File Offset: 0x00004AB8
		public LoadAppointmentsResp LoadAppointments(LoadAppointmentsReq request)
		{
			return this.WrapServiceMethod<LoadAppointmentsResp>(() => this.Proxy.LoadAppointments(request));
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000068F0 File Offset: 0x00004AF0
		public void UnMarkAppointmentTentative(UnMarkAppointmentTentativeReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UnMarkAppointmentTentative(request);
			});
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006928 File Offset: 0x00004B28
		public void MarkAppointmentTentative(MarkAppointmentTentativeReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.MarkAppointmentTentative(request);
			});
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006960 File Offset: 0x00004B60
		public LoadAppointmentByIdResp LoadAppointmentById(LoadAppointmentByIdReq request)
		{
			return this.WrapServiceMethod<LoadAppointmentByIdResp>(() => this.Proxy.LoadAppointmentById(request));
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006998 File Offset: 0x00004B98
		public LoadAppointmentExtendedInfoResp LoadAppointmentExtendedInfo(LoadAppointmentExtendedInfoReq request)
		{
			return this.WrapServiceMethod<LoadAppointmentExtendedInfoResp>(() => this.Proxy.LoadAppointmentExtendedInfo(request));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000069D0 File Offset: 0x00004BD0
		public void DeleteAppointment(DeleteAppointmentReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAppointment(request);
			});
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006A08 File Offset: 0x00004C08
		public LoadDeletedAppointmentByIdResp LoadDeletedAppointmentById(LoadDeletedAppointmentByIdReq request)
		{
			return this.WrapServiceMethod<LoadDeletedAppointmentByIdResp>(() => this.Proxy.LoadDeletedAppointmentById(request));
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006A40 File Offset: 0x00004C40
		public CreateAppointmentResp CreateAppointment(CreateAppointmentReq Request)
		{
			return this.WrapServiceMethod<CreateAppointmentResp>(() => this.Proxy.CreateAppointment(Request));
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006A78 File Offset: 0x00004C78
		public UpdateAppointmentResp UpdateAppointment(UpdateAppointmentReq Request)
		{
			return this.WrapServiceMethod<UpdateAppointmentResp>(() => this.Proxy.UpdateAppointment(Request));
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006AB0 File Offset: 0x00004CB0
		public void UpdateCalendarAppointmentParts(UpdateCalendarAppointmentPartsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateCalendarAppointmentParts(Request);
			});
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006AE8 File Offset: 0x00004CE8
		public void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.InsertOrUpdateAppointmentMemo(Request);
			});
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006B20 File Offset: 0x00004D20
		public FreeTimeSearchResp FreeTimeSearch(FreeTimeSearchReq Request)
		{
			return this.WrapServiceMethod<FreeTimeSearchResp>(() => this.Proxy.FreeTimeSearch(Request));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00006B58 File Offset: 0x00004D58
		public LoadAppointmentsAndAvailabilityResp LoadAppointmentsAndAvailability(LoadAppointmentsAndAvailabilityReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentsAndAvailabilityResp>(() => this.Proxy.LoadAppointmentsAndAvailability(Request));
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006B90 File Offset: 0x00004D90
		public LoadBasicAppointmentInformationByUserAndDateRangeResp LoadBasicAppointmentInformationByUserAndDateRange(LoadBasicAppointmentInformationByUserAndDateRangeReq Request)
		{
			return this.WrapServiceMethod<LoadBasicAppointmentInformationByUserAndDateRangeResp>(() => this.Proxy.LoadBasicAppointmentInformationByUserAndDateRange(Request));
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006BC8 File Offset: 0x00004DC8
		public UpdateAppointmentExternalIdResp UpdateAppointmentExternalId(UpdateAppointmentExternalIdReq request)
		{
			return this.WrapServiceMethod<UpdateAppointmentExternalIdResp>(() => this.Proxy.UpdateAppointmentExternalId(request));
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006C00 File Offset: 0x00004E00
		public GetNumberOfAppointmentsWithAppTypeResp GetNumberOfAppointmentsWithAppType(GetNumberOfAppointmentsWithAppTypeReq Request)
		{
			return this.WrapServiceMethod<GetNumberOfAppointmentsWithAppTypeResp>(() => this.Proxy.GetNumberOfAppointmentsWithAppType(Request));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006C38 File Offset: 0x00004E38
		public SwapAppointmentTypeForAllAppointmentsResp SwapAppointmentTypeForAllAppointments(SwapAppointmentTypeForAllAppointmentsReq Request)
		{
			return this.WrapServiceMethod<SwapAppointmentTypeForAllAppointmentsResp>(() => this.Proxy.SwapAppointmentTypeForAllAppointments(Request));
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006C70 File Offset: 0x00004E70
		public LoadAppointmentsWithSpecialPermissionsResp LoadAppointmentsWithSpecialPermissions(LoadAppointmentsWithSpecialPermissionsReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentsWithSpecialPermissionsResp>(() => this.Proxy.LoadAppointmentsWithSpecialPermissions(Request));
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public LoadAppointmentWithSpecialPermissionsResp LoadAppointmentWithSpecialPermissions(LoadAppointmentWithSpecialPermissionsReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentWithSpecialPermissionsResp>(() => this.Proxy.LoadAppointmentWithSpecialPermissions(Request));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006CE0 File Offset: 0x00004EE0
		public UpdateAppointmentDateAndTimeResp UpdateAppointmentDateAndTime(UpdateAppointmentDateAndTimeReq Request)
		{
			return this.WrapServiceMethod<UpdateAppointmentDateAndTimeResp>(() => this.Proxy.UpdateAppointmentDateAndTime(Request));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006D18 File Offset: 0x00004F18
		public CancelAttendeeAppointmentResp CancelAttendeeAppointment(CancelAttendeeAppointmentReq Request)
		{
			return this.WrapServiceMethod<CancelAttendeeAppointmentResp>(() => this.Proxy.CancelAttendeeAppointment(Request));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006D50 File Offset: 0x00004F50
		[DebuggerStepThrough]
		public Task<LoadAppointmentsAndAvailabilityResp> LoadAppointmentsAndAvailabilityAsync(LoadAppointmentsAndAvailabilityReq Request)
		{
			AppointmentReusableClientProxy.<LoadAppointmentsAndAvailabilityAsync>d__25 <LoadAppointmentsAndAvailabilityAsync>d__ = new AppointmentReusableClientProxy.<LoadAppointmentsAndAvailabilityAsync>d__25();
			<LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAppointmentsAndAvailabilityResp>.Create();
			<LoadAppointmentsAndAvailabilityAsync>d__.<>4__this = this;
			<LoadAppointmentsAndAvailabilityAsync>d__.Request = Request;
			<LoadAppointmentsAndAvailabilityAsync>d__.<>1__state = -1;
			<LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder.Start<AppointmentReusableClientProxy.<LoadAppointmentsAndAvailabilityAsync>d__25>(ref <LoadAppointmentsAndAvailabilityAsync>d__);
			return <LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder.Task;
		}
	}
}
