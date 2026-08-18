using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AvailabilitySchedule;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AvailabilitySchedule
{
	// Token: 0x0200007E RID: 126
	public class AvailabilityScheduleClientManager : IAvailabilityScheduleClientManager, IWebService
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x00014BD4 File Offset: 0x00012DD4
		public IList<AvailabilityScheduleItemsForContextDTO> LoadAvailabilityItemsByMultipleContextsAndDateRange(IList<AvailabilityScheduleContextDTO> contexts, DateTime startDate, int numDays)
		{
			LoadAvailabilityItemsByMultipleContextsAndDateRangeReq loadAvailabilityItemsByMultipleContextsAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailabilityItemsByMultipleContextsAndDateRangeReq>();
			loadAvailabilityItemsByMultipleContextsAndDateRangeReq.Contexts = contexts;
			loadAvailabilityItemsByMultipleContextsAndDateRangeReq.StartDate = startDate;
			loadAvailabilityItemsByMultipleContextsAndDateRangeReq.NumDays = numDays;
			LoadAvailabilityItemsByMultipleContextsAndDateRangeResp loadAvailabilityItemsByMultipleContextsAndDateRangeResp = ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().LoadAvailabilityItemsByMultipleContextsAndDateRange(loadAvailabilityItemsByMultipleContextsAndDateRangeReq);
			return (loadAvailabilityItemsByMultipleContextsAndDateRangeResp != null) ? loadAvailabilityItemsByMultipleContextsAndDateRangeResp.Result : null;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00014C20 File Offset: 0x00012E20
		public AvailabilityScheduleItemsForContextDTO LoadAvailabilityItemsByContextAndDateRange(AvailabilityScheduleContextDTO context, DateTime startDate, int numDays)
		{
			LoadAvailabilityItemsByContextAndDateRangeReq loadAvailabilityItemsByContextAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailabilityItemsByContextAndDateRangeReq>();
			loadAvailabilityItemsByContextAndDateRangeReq.Context = context;
			loadAvailabilityItemsByContextAndDateRangeReq.StartDate = startDate;
			loadAvailabilityItemsByContextAndDateRangeReq.NumDays = numDays;
			LoadAvailabilityItemsByContextAndDateRangeResp loadAvailabilityItemsByContextAndDateRangeResp = ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().LoadAvailabilityItemsByContextAndDateRange(loadAvailabilityItemsByContextAndDateRangeReq);
			return (loadAvailabilityItemsByContextAndDateRangeResp != null) ? loadAvailabilityItemsByContextAndDateRangeResp.Result : null;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00014C6C File Offset: 0x00012E6C
		public AvailabilityScheduleItemsForContextDTO LoadAvailabilityItemsByContextAndDates(AvailabilityScheduleContextDTO context, IList<DateTime> days)
		{
			LoadAvailabilityItemsByContextAndDatesReq loadAvailabilityItemsByContextAndDatesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailabilityItemsByContextAndDatesReq>();
			loadAvailabilityItemsByContextAndDatesReq.Context = context;
			loadAvailabilityItemsByContextAndDatesReq.Days = days;
			LoadAvailabilityItemsByContextAndDatesResp loadAvailabilityItemsByContextAndDatesResp = ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().LoadAvailabilityItemsByContextAndDates(loadAvailabilityItemsByContextAndDatesReq);
			return (loadAvailabilityItemsByContextAndDatesResp != null) ? loadAvailabilityItemsByContextAndDatesResp.Result : null;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00014CB0 File Offset: 0x00012EB0
		public AddAvailabilitiesActionResultDTO AddAvailabilityTimesByContextAndDate(AvailabilityScheduleContextDTO context, DateTime date, IList<AvailabilityScheduleTimeDTO> times, bool abortIfAnyProblems)
		{
			AddAvailabilityTimesByContextAndDateReq addAvailabilityTimesByContextAndDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddAvailabilityTimesByContextAndDateReq>();
			addAvailabilityTimesByContextAndDateReq.Context = context;
			addAvailabilityTimesByContextAndDateReq.Date = date;
			addAvailabilityTimesByContextAndDateReq.Times = times;
			addAvailabilityTimesByContextAndDateReq.AbortIfAnyProblems = abortIfAnyProblems;
			AddAvailabilityTimesByContextAndDateResp addAvailabilityTimesByContextAndDateResp = ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().AddAvailabilityTimesByContextAndDate(addAvailabilityTimesByContextAndDateReq);
			return (addAvailabilityTimesByContextAndDateResp != null) ? addAvailabilityTimesByContextAndDateResp.Result : null;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00014D08 File Offset: 0x00012F08
		public AddAvailabilitiesActionResultDTO AddAvailabilityDatesAndTimesByContext(AvailabilityScheduleContextDTO context, IList<DateTime> dates, IList<AvailabilityScheduleTimeDTO> times, bool abortIfAnyProblems)
		{
			AddAvailabilityDatesAndTimesByContextReq addAvailabilityDatesAndTimesByContextReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddAvailabilityDatesAndTimesByContextReq>();
			addAvailabilityDatesAndTimesByContextReq.Context = context;
			addAvailabilityDatesAndTimesByContextReq.Dates = dates;
			addAvailabilityDatesAndTimesByContextReq.Times = times;
			addAvailabilityDatesAndTimesByContextReq.AbortIfAnyProblems = abortIfAnyProblems;
			AddAvailabilityDatesAndTimesByContextResp addAvailabilityDatesAndTimesByContextResp = ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().AddAvailabilityDatesAndTimesByContext(addAvailabilityDatesAndTimesByContextReq);
			return (addAvailabilityDatesAndTimesByContextResp != null) ? addAvailabilityDatesAndTimesByContextResp.Result : null;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00014D60 File Offset: 0x00012F60
		public DeleteAvailabilityActionResultDTO DeleteAvailabilityTimeByContext(AvailabilityScheduleContextDTO context, AvailabilityScheduleDateAndTimeDTO dayAndTime)
		{
			DeleteAvailabilityTimeByContextReq deleteAvailabilityTimeByContextReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAvailabilityTimeByContextReq>();
			deleteAvailabilityTimeByContextReq.Context = context;
			deleteAvailabilityTimeByContextReq.DayAndTime = dayAndTime;
			DeleteAvailabilityTimeByContextResp deleteAvailabilityTimeByContextResp = ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().DeleteAvailabilityTimeByContext(deleteAvailabilityTimeByContextReq);
			return (deleteAvailabilityTimeByContextResp != null) ? deleteAvailabilityTimeByContextResp.Result : null;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00014DA4 File Offset: 0x00012FA4
		public IList<DeleteAvailabilityActionResultDTO> DeleteAvailabilityDatesAndTimesByContext(AvailabilityScheduleContextDTO context, IList<AvailabilityScheduleDateAndTimeDTO> dayAndTimes)
		{
			DeleteAvailabilityDatesAndTimesByContextReq deleteAvailabilityDatesAndTimesByContextReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAvailabilityDatesAndTimesByContextReq>();
			deleteAvailabilityDatesAndTimesByContextReq.Context = context;
			deleteAvailabilityDatesAndTimesByContextReq.DayAndTimes = dayAndTimes;
			DeleteAvailabilityDatesAndTimesByContextResp deleteAvailabilityDatesAndTimesByContextResp = ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().DeleteAvailabilityDatesAndTimesByContext(deleteAvailabilityDatesAndTimesByContextReq);
			return (deleteAvailabilityDatesAndTimesByContextResp != null) ? deleteAvailabilityDatesAndTimesByContextResp.Result : null;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014DE8 File Offset: 0x00012FE8
		public void ClearAvailabilityForTheDay(AvailabilityScheduleContextDTO context, IList<DateTime> days)
		{
			ClearAvailabilityForTheDayReq clearAvailabilityForTheDayReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearAvailabilityForTheDayReq>();
			clearAvailabilityForTheDayReq.Context = context;
			clearAvailabilityForTheDayReq.Days = days;
			ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().ClearAvailabilityForTheDay(clearAvailabilityForTheDayReq);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00014E20 File Offset: 0x00013020
		public IList<DateTime> LoadDaysWithAvailability(int PersonId, IList<int> AvailabilityGroupIds, DateTime StartDate, DateTime EndDate)
		{
			LoadDaysWithAvailabilityReq loadDaysWithAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDaysWithAvailabilityReq>();
			loadDaysWithAvailabilityReq.PersonId = PersonId;
			loadDaysWithAvailabilityReq.StartDate = StartDate;
			loadDaysWithAvailabilityReq.EndDate = EndDate;
			loadDaysWithAvailabilityReq.AvailabilityGroupIds = AvailabilityGroupIds;
			LoadDaysWithAvailabilityResp loadDaysWithAvailabilityResp = ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().LoadDaysWithAvailability(loadDaysWithAvailabilityReq);
			return (loadDaysWithAvailabilityResp != null) ? loadDaysWithAvailabilityResp.DaysWithAvailability : null;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00014E78 File Offset: 0x00013078
		public IList<AvailabilityGroupDTO> LoadAllAvailabilityGroups()
		{
			LoadAllAvailabilityGroupsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllAvailabilityGroupsReq>();
			LoadAllAvailabilityGroupsResp loadAllAvailabilityGroupsResp = ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().LoadAllAvailabilityGroups(request);
			return (loadAllAvailabilityGroupsResp != null) ? loadAllAvailabilityGroupsResp.AvailabilityGroups : null;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00014EAC File Offset: 0x000130AC
		public IList<AvailabilityScheduleItemsForContextDTO> LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(IList<AvailabilityScheduleContextDTO> contexts, DateTime startDate, int numDays)
		{
			LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq>();
			loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq.Contexts = contexts;
			loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq.StartDate = startDate;
			loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq.NumDays = numDays;
			LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp = ClientServiceFactory.GetClientInstance<IAvailabilitySchedule>().LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq);
			return (loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp != null) ? loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp.Result : null;
		}
	}
}
