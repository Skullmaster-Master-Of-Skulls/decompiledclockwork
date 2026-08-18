using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000028 RID: 40
	public class AutoTestBookingReusableClientProxy : WCFTokenBasedReusableClientProxy<IAutoTestBooking>, IAutoTestBooking, IService
	{
		// Token: 0x06000222 RID: 546 RVA: 0x0000764E File Offset: 0x0000584E
		public AutoTestBookingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007659 File Offset: 0x00005859
		public AutoTestBookingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007668 File Offset: 0x00005868
		public CalculateBreakTimeResp CalculateBreakTime(CalculateBreakTimeReq Request)
		{
			return this.WrapServiceMethod<CalculateBreakTimeResp>(() => this.Proxy.CalculateBreakTime(Request));
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000076A0 File Offset: 0x000058A0
		public CalculateExtraTimeResp CalculateExtraTime(CalculateExtraTimeReq Request)
		{
			return this.WrapServiceMethod<CalculateExtraTimeResp>(() => this.Proxy.CalculateExtraTime(Request));
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000076D8 File Offset: 0x000058D8
		public FindPotentialBookingsExplicitResp FindPotentialBookingsExplicit(FindPotentialBookingsExplicitReq Request)
		{
			return this.WrapServiceMethod<FindPotentialBookingsExplicitResp>(() => this.Proxy.FindPotentialBookingsExplicit(Request));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007710 File Offset: 0x00005910
		public LoadBaseAutoTestBookingSettingsResp LoadBaseAutoTestBookingSettings(LoadBaseAutoTestBookingSettingsReq Request)
		{
			return this.WrapServiceMethod<LoadBaseAutoTestBookingSettingsResp>(() => this.Proxy.LoadBaseAutoTestBookingSettings(Request));
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007748 File Offset: 0x00005948
		public void ClearAutoTestBookingCache(ClearAutoTestBookingCacheReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearAutoTestBookingCache(Request);
			});
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007780 File Offset: 0x00005980
		public LoadAvailableAssetsResp LoadAvailableAssets(LoadAvailableAssetsReq Request)
		{
			return this.WrapServiceMethod<LoadAvailableAssetsResp>(() => this.Proxy.LoadAvailableAssets(Request));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000077B8 File Offset: 0x000059B8
		public LoadAvailableRoomsResp LoadAvailableRooms(LoadAvailableRoomsReq Request)
		{
			return this.WrapServiceMethod<LoadAvailableRoomsResp>(() => this.Proxy.LoadAvailableRooms(Request));
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000077F0 File Offset: 0x000059F0
		public LoadSpecialAccommodationsResp LoadSpecialAccommodations(LoadSpecialAccommodationsReq Request)
		{
			return this.WrapServiceMethod<LoadSpecialAccommodationsResp>(() => this.Proxy.LoadSpecialAccommodations(Request));
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007828 File Offset: 0x00005A28
		public LoadTestRulesResp LoadTestRules(LoadTestRulesReq Request)
		{
			return this.WrapServiceMethod<LoadTestRulesResp>(() => this.Proxy.LoadTestRules(Request));
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007860 File Offset: 0x00005A60
		public ApplySpecialAccommodations2Resp ApplySpecialAccommodations(ApplySpecialAccommodations2Req Request)
		{
			return this.WrapServiceMethod<ApplySpecialAccommodations2Resp>(() => this.Proxy.ApplySpecialAccommodations(Request));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007898 File Offset: 0x00005A98
		public FindPotentialBookings2Resp FindPotentialBookings(FindPotentialBookings2Req Request)
		{
			return this.WrapServiceMethod<FindPotentialBookings2Resp>(() => this.Proxy.FindPotentialBookings(Request));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000078D0 File Offset: 0x00005AD0
		public TryToFindBookingResp TryToFindBooking(TryToFindBookingReq Request)
		{
			return this.WrapServiceMethod<TryToFindBookingResp>(() => this.Proxy.TryToFindBooking(Request));
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007908 File Offset: 0x00005B08
		public AutoBookTestOrExamResp AutoBookTestOrExam(AutoBookTestOrExamReq Request)
		{
			return this.WrapServiceMethod<AutoBookTestOrExamResp>(() => this.Proxy.AutoBookTestOrExam(Request));
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007940 File Offset: 0x00005B40
		public AutoBookTestOrExamPreviewResp AutoBookTestOrExamPreview(AutoBookTestOrExamPreviewReq Request)
		{
			return this.WrapServiceMethod<AutoBookTestOrExamPreviewResp>(() => this.Proxy.AutoBookTestOrExamPreview(Request));
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007978 File Offset: 0x00005B78
		public AutoRescheduleTestOrExamResp AutoRescheduleTestOrExam(AutoRescheduleTestOrExamReq Request)
		{
			return this.WrapServiceMethod<AutoRescheduleTestOrExamResp>(() => this.Proxy.AutoRescheduleTestOrExam(Request));
		}
	}
}
