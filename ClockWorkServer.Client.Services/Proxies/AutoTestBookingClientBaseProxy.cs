using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000029 RID: 41
	internal class AutoTestBookingClientBaseProxy : ClientBase<IAutoTestBooking>, IAutoTestBooking, IService
	{
		// Token: 0x06000233 RID: 563 RVA: 0x000079B0 File Offset: 0x00005BB0
		public AutoTestBookingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000079BB File Offset: 0x00005BBB
		public AutoTestBookingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000079C8 File Offset: 0x00005BC8
		public CalculateBreakTimeResp CalculateBreakTime(CalculateBreakTimeReq Request)
		{
			return base.Channel.CalculateBreakTime(Request);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000079E8 File Offset: 0x00005BE8
		public CalculateExtraTimeResp CalculateExtraTime(CalculateExtraTimeReq Request)
		{
			return base.Channel.CalculateExtraTime(Request);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007A08 File Offset: 0x00005C08
		public FindPotentialBookingsExplicitResp FindPotentialBookingsExplicit(FindPotentialBookingsExplicitReq Request)
		{
			return base.Channel.FindPotentialBookingsExplicit(Request);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007A28 File Offset: 0x00005C28
		public LoadBaseAutoTestBookingSettingsResp LoadBaseAutoTestBookingSettings(LoadBaseAutoTestBookingSettingsReq Request)
		{
			return base.Channel.LoadBaseAutoTestBookingSettings(Request);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007A46 File Offset: 0x00005C46
		public void ClearAutoTestBookingCache(ClearAutoTestBookingCacheReq Request)
		{
			base.Channel.ClearAutoTestBookingCache(Request);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007A58 File Offset: 0x00005C58
		public LoadAvailableAssetsResp LoadAvailableAssets(LoadAvailableAssetsReq Request)
		{
			return base.Channel.LoadAvailableAssets(Request);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007A78 File Offset: 0x00005C78
		public LoadAvailableRoomsResp LoadAvailableRooms(LoadAvailableRoomsReq Request)
		{
			return base.Channel.LoadAvailableRooms(Request);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007A98 File Offset: 0x00005C98
		public LoadSpecialAccommodationsResp LoadSpecialAccommodations(LoadSpecialAccommodationsReq Request)
		{
			return base.Channel.LoadSpecialAccommodations(Request);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007AB8 File Offset: 0x00005CB8
		public LoadTestRulesResp LoadTestRules(LoadTestRulesReq Request)
		{
			return base.Channel.LoadTestRules(Request);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public ApplySpecialAccommodations2Resp ApplySpecialAccommodations(ApplySpecialAccommodations2Req Request)
		{
			return base.Channel.ApplySpecialAccommodations(Request);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007AF8 File Offset: 0x00005CF8
		public FindPotentialBookings2Resp FindPotentialBookings(FindPotentialBookings2Req Request)
		{
			return base.Channel.FindPotentialBookings(Request);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007B18 File Offset: 0x00005D18
		public TryToFindBookingResp TryToFindBooking(TryToFindBookingReq Request)
		{
			return base.Channel.TryToFindBooking(Request);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007B38 File Offset: 0x00005D38
		public AutoBookTestOrExamResp AutoBookTestOrExam(AutoBookTestOrExamReq Request)
		{
			return base.Channel.AutoBookTestOrExam(Request);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007B58 File Offset: 0x00005D58
		public AutoBookTestOrExamPreviewResp AutoBookTestOrExamPreview(AutoBookTestOrExamPreviewReq Request)
		{
			return base.Channel.AutoBookTestOrExamPreview(Request);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007B78 File Offset: 0x00005D78
		public AutoRescheduleTestOrExamResp AutoRescheduleTestOrExam(AutoRescheduleTestOrExamReq Request)
		{
			return base.Channel.AutoRescheduleTestOrExam(Request);
		}
	}
}
