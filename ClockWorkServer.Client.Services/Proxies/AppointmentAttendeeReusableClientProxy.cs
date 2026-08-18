using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000044 RID: 68
	public class AppointmentAttendeeReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointmentAttendee>, IAppointmentAttendee, IService
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000A23E File Offset: 0x0000843E
		public AppointmentAttendeeReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000A249 File Offset: 0x00008449
		public AppointmentAttendeeReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000A258 File Offset: 0x00008458
		public void DeleteAttendee(DeleteAttendeeReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAttendee(Request);
			});
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000A290 File Offset: 0x00008490
		public void DeleteAttendeeByAttendeeId(DeleteAttendeeByAttendeeIdReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAttendeeByAttendeeId(Request);
			});
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public InsertOrUpdateAppointmentAttendeeResp InsertOrUpdateAppointmentAttendee(InsertOrUpdateAppointmentAttendeeReq Request)
		{
			return this.WrapServiceMethod<InsertOrUpdateAppointmentAttendeeResp>(() => this.Proxy.InsertOrUpdateAppointmentAttendee(Request));
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000A300 File Offset: 0x00008500
		public void InsertOrUpdateAppointmentAttendees(InsertOrUpdateAppointmentAttendeesReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.InsertOrUpdateAppointmentAttendees(Request);
			});
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000A338 File Offset: 0x00008538
		public LoadAttendeeByAttendeeIdResp LoadAttendeeByAttendeeId(LoadAttendeeByAttendeeIdReq Request)
		{
			return this.WrapServiceMethod<LoadAttendeeByAttendeeIdResp>(() => this.Proxy.LoadAttendeeByAttendeeId(Request));
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000A370 File Offset: 0x00008570
		public LoadAttendeeByIdResp LoadAttendeeById(LoadAttendeeByIdReq Request)
		{
			return this.WrapServiceMethod<LoadAttendeeByIdResp>(() => this.Proxy.LoadAttendeeById(Request));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000A3A8 File Offset: 0x000085A8
		public LoadAttendeesByAppointmentIdResp LoadAttendeesByAppointmentId(LoadAttendeesByAppointmentIdReq Request)
		{
			return this.WrapServiceMethod<LoadAttendeesByAppointmentIdResp>(() => this.Proxy.LoadAttendeesByAppointmentId(Request));
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000A3E0 File Offset: 0x000085E0
		public void RemoveAttendeesNotInList(RemoveAttendeesNotInListReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.RemoveAttendeesNotInList(Request);
			});
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000A418 File Offset: 0x00008618
		public void SwapAttendee(SwapAttendeeReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SwapAttendee(Request);
			});
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000A450 File Offset: 0x00008650
		public void UpdateAttendeeNoShow(UpdateAttendeeNoShowReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateAttendeeNoShow(Request);
			});
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000A488 File Offset: 0x00008688
		public void UpdateMiscCodeValue(UpdateMiscCodeValueReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateMiscCodeValue(Request);
			});
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public void UpdateMiscCodeValueByAttendeeId(UpdateMiscCodeValueByAttendeeIdReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateMiscCodeValueByAttendeeId(Request);
			});
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000A4F8 File Offset: 0x000086F8
		public void UpdateNoShowValue(UpdateNoShowValueReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateNoShowValue(Request);
			});
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000A530 File Offset: 0x00008730
		public void UpdateNoShowValueByAttendeeId(UpdateNoShowValueByAttendeeIdReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateNoShowValueByAttendeeId(Request);
			});
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000A568 File Offset: 0x00008768
		public GetDoubleBookedAttendeesResp GetDoubleBookedAttendees(GetDoubleBookedAttendeesReq Request)
		{
			return this.WrapServiceMethod<GetDoubleBookedAttendeesResp>(() => this.Proxy.GetDoubleBookedAttendees(Request));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A5A0 File Offset: 0x000087A0
		public TryToRemoveAttendeesResp TryToRemoveAttendees(TryToRemoveAttendeesReq request)
		{
			return this.WrapServiceMethod<TryToRemoveAttendeesResp>(() => this.Proxy.TryToRemoveAttendees(request));
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public LoadAttendeesByAppointmentIdsResp LoadAttendeesByAppointmentIds(LoadAttendeesByAppointmentIdsReq Request)
		{
			return this.WrapServiceMethod<LoadAttendeesByAppointmentIdsResp>(() => this.Proxy.LoadAttendeesByAppointmentIds(Request));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000A610 File Offset: 0x00008810
		public IsAttendeeDoubleBookedResp IsAttendeeDoubleBooked(IsAttendeeDoubleBookedReq Request)
		{
			return this.WrapServiceMethod<IsAttendeeDoubleBookedResp>(() => this.Proxy.IsAttendeeDoubleBooked(Request));
		}
	}
}
