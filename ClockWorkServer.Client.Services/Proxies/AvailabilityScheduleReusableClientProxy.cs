using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000052 RID: 82
	public class AvailabilityScheduleReusableClientProxy : WCFTokenBasedReusableClientProxy<IAvailabilitySchedule>, IAvailabilitySchedule, IService
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0000B6D6 File Offset: 0x000098D6
		public AvailabilityScheduleReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000B6E1 File Offset: 0x000098E1
		public AvailabilityScheduleReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public LoadAvailabilityItemsByContextAndDateRangeResp LoadAvailabilityItemsByContextAndDateRange(LoadAvailabilityItemsByContextAndDateRangeReq Request)
		{
			return this.WrapServiceMethod<LoadAvailabilityItemsByContextAndDateRangeResp>(() => this.Proxy.LoadAvailabilityItemsByContextAndDateRange(Request));
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000B728 File Offset: 0x00009928
		public LoadAvailabilityItemsByMultipleContextsAndDateRangeResp LoadAvailabilityItemsByMultipleContextsAndDateRange(LoadAvailabilityItemsByMultipleContextsAndDateRangeReq Request)
		{
			return this.WrapServiceMethod<LoadAvailabilityItemsByMultipleContextsAndDateRangeResp>(() => this.Proxy.LoadAvailabilityItemsByMultipleContextsAndDateRange(Request));
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000B760 File Offset: 0x00009960
		public LoadAvailabilityItemsByContextAndDatesResp LoadAvailabilityItemsByContextAndDates(LoadAvailabilityItemsByContextAndDatesReq Request)
		{
			return this.WrapServiceMethod<LoadAvailabilityItemsByContextAndDatesResp>(() => this.Proxy.LoadAvailabilityItemsByContextAndDates(Request));
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000B798 File Offset: 0x00009998
		public AddAvailabilityTimesByContextAndDateResp AddAvailabilityTimesByContextAndDate(AddAvailabilityTimesByContextAndDateReq Request)
		{
			return this.WrapServiceMethod<AddAvailabilityTimesByContextAndDateResp>(() => this.Proxy.AddAvailabilityTimesByContextAndDate(Request));
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000B7D0 File Offset: 0x000099D0
		public AddAvailabilityDatesAndTimesByContextResp AddAvailabilityDatesAndTimesByContext(AddAvailabilityDatesAndTimesByContextReq Request)
		{
			return this.WrapServiceMethod<AddAvailabilityDatesAndTimesByContextResp>(() => this.Proxy.AddAvailabilityDatesAndTimesByContext(Request));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000B808 File Offset: 0x00009A08
		public DeleteAvailabilityTimeByContextResp DeleteAvailabilityTimeByContext(DeleteAvailabilityTimeByContextReq Request)
		{
			return this.WrapServiceMethod<DeleteAvailabilityTimeByContextResp>(() => this.Proxy.DeleteAvailabilityTimeByContext(Request));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000B840 File Offset: 0x00009A40
		public DeleteAvailabilityDatesAndTimesByContextResp DeleteAvailabilityDatesAndTimesByContext(DeleteAvailabilityDatesAndTimesByContextReq Request)
		{
			return this.WrapServiceMethod<DeleteAvailabilityDatesAndTimesByContextResp>(() => this.Proxy.DeleteAvailabilityDatesAndTimesByContext(Request));
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000B878 File Offset: 0x00009A78
		public ClearAvailabilityForTheDayResp ClearAvailabilityForTheDay(ClearAvailabilityForTheDayReq Request)
		{
			return this.WrapServiceMethod<ClearAvailabilityForTheDayResp>(() => this.Proxy.ClearAvailabilityForTheDay(Request));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		public LoadDaysWithAvailabilityResp LoadDaysWithAvailability(LoadDaysWithAvailabilityReq Request)
		{
			return this.WrapServiceMethod<LoadDaysWithAvailabilityResp>(() => this.Proxy.LoadDaysWithAvailability(Request));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		public LoadAllAvailabilityGroupsResp LoadAllAvailabilityGroups(LoadAllAvailabilityGroupsReq Request)
		{
			return this.WrapServiceMethod<LoadAllAvailabilityGroupsResp>(() => this.Proxy.LoadAllAvailabilityGroups(Request));
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000B920 File Offset: 0x00009B20
		public LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq Request)
		{
			return this.WrapServiceMethod<LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp>(() => this.Proxy.LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(Request));
		}
	}
}
