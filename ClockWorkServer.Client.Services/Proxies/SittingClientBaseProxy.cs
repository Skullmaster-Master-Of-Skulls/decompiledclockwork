using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000031 RID: 49
	internal class SittingClientBaseProxy : ClientBase<ISitting>, ISitting, IService
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00008755 File Offset: 0x00006955
		public SittingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00008760 File Offset: 0x00006960
		public SittingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000876C File Offset: 0x0000696C
		public CreateSittingResp CreateSitting(CreateSittingReq Request)
		{
			return base.Channel.CreateSitting(Request);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000878C File Offset: 0x0000698C
		public GetSittingEffectiveTimeRangeResp GetSittingEffectiveTimeRange(GetSittingEffectiveTimeRangeReq request)
		{
			return base.Channel.GetSittingEffectiveTimeRange(request);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000087AC File Offset: 0x000069AC
		public LoadSittingByIdResp LoadSittingById(LoadSittingByIdReq request)
		{
			return base.Channel.LoadSittingById(request);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000087CC File Offset: 0x000069CC
		public LoadSittingTestsResp LoadSittingTests(LoadSittingTestsReq request)
		{
			return base.Channel.LoadSittingTests(request);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000087EC File Offset: 0x000069EC
		public LoadSittingsResp LoadSittings(LoadSittingsReq request)
		{
			return base.Channel.LoadSittings(request);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000880A File Offset: 0x00006A0A
		public void UpdateSitting(UpdateSittingReq request)
		{
			base.Channel.UpdateSitting(request);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000881C File Offset: 0x00006A1C
		public LoadSittingsByDateRangeResp LoadSittingsByDateRange(LoadSittingsByDateRangeReq Request)
		{
			return base.Channel.LoadSittingsByDateRange(Request);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000883A File Offset: 0x00006A3A
		public void ClearSittingOnAppointment(ClearSittingOnAppointmentReq Request)
		{
			base.Channel.ClearSittingOnAppointment(Request);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000884A File Offset: 0x00006A4A
		public void SetSittingOnAppointment(SetSittingOnAppointmentReq Request)
		{
			base.Channel.SetSittingOnAppointment(Request);
		}
	}
}
