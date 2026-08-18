using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000045 RID: 69
	internal class AppointmentAttendeeClientBaseProxy : ClientBase<IAppointmentAttendee>, IAppointmentAttendee, IService
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000A648 File Offset: 0x00008848
		public AppointmentAttendeeClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000A653 File Offset: 0x00008853
		public AppointmentAttendeeClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A65F File Offset: 0x0000885F
		public void DeleteAttendee(DeleteAttendeeReq Request)
		{
			base.Channel.DeleteAttendee(Request);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A66F File Offset: 0x0000886F
		public void DeleteAttendeeByAttendeeId(DeleteAttendeeByAttendeeIdReq Request)
		{
			base.Channel.DeleteAttendeeByAttendeeId(Request);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000A680 File Offset: 0x00008880
		public InsertOrUpdateAppointmentAttendeeResp InsertOrUpdateAppointmentAttendee(InsertOrUpdateAppointmentAttendeeReq Request)
		{
			return base.Channel.InsertOrUpdateAppointmentAttendee(Request);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000A69E File Offset: 0x0000889E
		public void InsertOrUpdateAppointmentAttendees(InsertOrUpdateAppointmentAttendeesReq Request)
		{
			base.Channel.InsertOrUpdateAppointmentAttendees(Request);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000A6B0 File Offset: 0x000088B0
		public LoadAttendeeByAttendeeIdResp LoadAttendeeByAttendeeId(LoadAttendeeByAttendeeIdReq Request)
		{
			return base.Channel.LoadAttendeeByAttendeeId(Request);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public LoadAttendeeByIdResp LoadAttendeeById(LoadAttendeeByIdReq Request)
		{
			return base.Channel.LoadAttendeeById(Request);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000A6F0 File Offset: 0x000088F0
		public LoadAttendeesByAppointmentIdResp LoadAttendeesByAppointmentId(LoadAttendeesByAppointmentIdReq Request)
		{
			return base.Channel.LoadAttendeesByAppointmentId(Request);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A70E File Offset: 0x0000890E
		public void RemoveAttendeesNotInList(RemoveAttendeesNotInListReq Request)
		{
			base.Channel.RemoveAttendeesNotInList(Request);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000A71E File Offset: 0x0000891E
		public void SwapAttendee(SwapAttendeeReq Request)
		{
			base.Channel.SwapAttendee(Request);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000A72E File Offset: 0x0000892E
		public void UpdateAttendeeNoShow(UpdateAttendeeNoShowReq Request)
		{
			base.Channel.UpdateAttendeeNoShow(Request);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000A73E File Offset: 0x0000893E
		public void UpdateMiscCodeValue(UpdateMiscCodeValueReq Request)
		{
			base.Channel.UpdateMiscCodeValue(Request);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000A74E File Offset: 0x0000894E
		public void UpdateMiscCodeValueByAttendeeId(UpdateMiscCodeValueByAttendeeIdReq Request)
		{
			base.Channel.UpdateMiscCodeValueByAttendeeId(Request);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000A75E File Offset: 0x0000895E
		public void UpdateNoShowValue(UpdateNoShowValueReq Request)
		{
			base.Channel.UpdateNoShowValue(Request);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000A76E File Offset: 0x0000896E
		public void UpdateNoShowValueByAttendeeId(UpdateNoShowValueByAttendeeIdReq Request)
		{
			base.Channel.UpdateNoShowValueByAttendeeId(Request);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000A780 File Offset: 0x00008980
		public GetDoubleBookedAttendeesResp GetDoubleBookedAttendees(GetDoubleBookedAttendeesReq Request)
		{
			return base.Channel.GetDoubleBookedAttendees(Request);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000A7A0 File Offset: 0x000089A0
		public TryToRemoveAttendeesResp TryToRemoveAttendees(TryToRemoveAttendeesReq request)
		{
			return base.Channel.TryToRemoveAttendees(request);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000A7C0 File Offset: 0x000089C0
		public LoadAttendeesByAppointmentIdsResp LoadAttendeesByAppointmentIds(LoadAttendeesByAppointmentIdsReq Request)
		{
			return base.Channel.LoadAttendeesByAppointmentIds(Request);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000A7E0 File Offset: 0x000089E0
		public IsAttendeeDoubleBookedResp IsAttendeeDoubleBooked(IsAttendeeDoubleBookedReq Request)
		{
			return base.Channel.IsAttendeeDoubleBooked(Request);
		}
	}
}
