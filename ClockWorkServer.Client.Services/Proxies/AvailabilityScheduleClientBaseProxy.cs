using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000053 RID: 83
	internal class AvailabilityScheduleClientBaseProxy : ClientBase<IAvailabilitySchedule>, IAvailabilitySchedule, IService
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000B958 File Offset: 0x00009B58
		public AvailabilityScheduleClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000B963 File Offset: 0x00009B63
		public AvailabilityScheduleClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000B970 File Offset: 0x00009B70
		public LoadAvailabilityItemsByContextAndDateRangeResp LoadAvailabilityItemsByContextAndDateRange(LoadAvailabilityItemsByContextAndDateRangeReq Request)
		{
			return base.Channel.LoadAvailabilityItemsByContextAndDateRange(Request);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000B990 File Offset: 0x00009B90
		public LoadAvailabilityItemsByMultipleContextsAndDateRangeResp LoadAvailabilityItemsByMultipleContextsAndDateRange(LoadAvailabilityItemsByMultipleContextsAndDateRangeReq Request)
		{
			return base.Channel.LoadAvailabilityItemsByMultipleContextsAndDateRange(Request);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		public LoadAvailabilityItemsByContextAndDatesResp LoadAvailabilityItemsByContextAndDates(LoadAvailabilityItemsByContextAndDatesReq Request)
		{
			return base.Channel.LoadAvailabilityItemsByContextAndDates(Request);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public AddAvailabilityTimesByContextAndDateResp AddAvailabilityTimesByContextAndDate(AddAvailabilityTimesByContextAndDateReq Request)
		{
			return base.Channel.AddAvailabilityTimesByContextAndDate(Request);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000B9F0 File Offset: 0x00009BF0
		public AddAvailabilityDatesAndTimesByContextResp AddAvailabilityDatesAndTimesByContext(AddAvailabilityDatesAndTimesByContextReq Request)
		{
			return base.Channel.AddAvailabilityDatesAndTimesByContext(Request);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000BA10 File Offset: 0x00009C10
		public DeleteAvailabilityTimeByContextResp DeleteAvailabilityTimeByContext(DeleteAvailabilityTimeByContextReq Request)
		{
			return base.Channel.DeleteAvailabilityTimeByContext(Request);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000BA30 File Offset: 0x00009C30
		public DeleteAvailabilityDatesAndTimesByContextResp DeleteAvailabilityDatesAndTimesByContext(DeleteAvailabilityDatesAndTimesByContextReq Request)
		{
			return base.Channel.DeleteAvailabilityDatesAndTimesByContext(Request);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000BA50 File Offset: 0x00009C50
		public ClearAvailabilityForTheDayResp ClearAvailabilityForTheDay(ClearAvailabilityForTheDayReq Request)
		{
			return base.Channel.ClearAvailabilityForTheDay(Request);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000BA70 File Offset: 0x00009C70
		public LoadDaysWithAvailabilityResp LoadDaysWithAvailability(LoadDaysWithAvailabilityReq Request)
		{
			return base.Channel.LoadDaysWithAvailability(Request);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000BA90 File Offset: 0x00009C90
		public LoadAllAvailabilityGroupsResp LoadAllAvailabilityGroups(LoadAllAvailabilityGroupsReq Request)
		{
			return base.Channel.LoadAllAvailabilityGroups(Request);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		public LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq Request)
		{
			return base.Channel.LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(Request);
		}
	}
}
