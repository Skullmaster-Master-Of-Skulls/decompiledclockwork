using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AvailabilitySchedule;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AvailabilitySchedule
{
	// Token: 0x02000068 RID: 104
	public class AvailabilityScheduleRestClientManager : BearerTokenRestProxy<IAvailabilityScheduleClientManager>, IAvailabilityScheduleClientManager, IWebService
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0000BB6F File Offset: 0x00009D6F
		public AvailabilityScheduleRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000BB79 File Offset: 0x00009D79
		public AvailabilityScheduleRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000BB84 File Offset: 0x00009D84
		public IList<AvailabilityScheduleItemsForContextDTO> LoadAvailabilityItemsByMultipleContextsAndDateRange(IList<AvailabilityScheduleContextDTO> contexts, DateTime startDate, int numDays)
		{
			LoadAvailabilityItemsByMultipleContextsAndDateRangeReq loadAvailabilityItemsByMultipleContextsAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailabilityItemsByMultipleContextsAndDateRangeReq>();
			loadAvailabilityItemsByMultipleContextsAndDateRangeReq.Contexts = contexts;
			loadAvailabilityItemsByMultipleContextsAndDateRangeReq.StartDate = startDate;
			loadAvailabilityItemsByMultipleContextsAndDateRangeReq.NumDays = numDays;
			return base.Post<LoadAvailabilityItemsByMultipleContextsAndDateRangeReq, IList<AvailabilityScheduleItemsForContextDTO>>(loadAvailabilityItemsByMultipleContextsAndDateRangeReq, "availabilityschedule/loadavailabilityitemsbymultiplecontextsanddaterange");
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		public AvailabilityScheduleItemsForContextDTO LoadAvailabilityItemsByContextAndDateRange(AvailabilityScheduleContextDTO context, DateTime startDate, int numDays)
		{
			LoadAvailabilityItemsByContextAndDateRangeReq loadAvailabilityItemsByContextAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailabilityItemsByContextAndDateRangeReq>();
			loadAvailabilityItemsByContextAndDateRangeReq.Context = context;
			loadAvailabilityItemsByContextAndDateRangeReq.StartDate = startDate;
			loadAvailabilityItemsByContextAndDateRangeReq.NumDays = numDays;
			return base.Post<LoadAvailabilityItemsByContextAndDateRangeReq, AvailabilityScheduleItemsForContextDTO>(loadAvailabilityItemsByContextAndDateRangeReq, "availabilityschedule/availabilityitemsbycontextanddaterange");
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000BBFC File Offset: 0x00009DFC
		public AvailabilityScheduleItemsForContextDTO LoadAvailabilityItemsByContextAndDates(AvailabilityScheduleContextDTO context, IList<DateTime> days)
		{
			LoadAvailabilityItemsByContextAndDatesReq loadAvailabilityItemsByContextAndDatesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailabilityItemsByContextAndDatesReq>();
			loadAvailabilityItemsByContextAndDatesReq.Context = context;
			loadAvailabilityItemsByContextAndDatesReq.Days = days;
			return base.Post<LoadAvailabilityItemsByContextAndDatesReq, AvailabilityScheduleItemsForContextDTO>(loadAvailabilityItemsByContextAndDatesReq, "availabilityschedule/loadavailabilityitemsbycontextanddates");
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000BC30 File Offset: 0x00009E30
		public AddAvailabilitiesActionResultDTO AddAvailabilityTimesByContextAndDate(AvailabilityScheduleContextDTO context, DateTime date, IList<AvailabilityScheduleTimeDTO> times, bool abortIfAnyProblems)
		{
			AddAvailabilityTimesByContextAndDateReq addAvailabilityTimesByContextAndDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddAvailabilityTimesByContextAndDateReq>();
			addAvailabilityTimesByContextAndDateReq.Context = context;
			addAvailabilityTimesByContextAndDateReq.Date = date;
			addAvailabilityTimesByContextAndDateReq.Times = times;
			addAvailabilityTimesByContextAndDateReq.AbortIfAnyProblems = abortIfAnyProblems;
			return base.Post<AddAvailabilityTimesByContextAndDateReq, AddAvailabilitiesActionResultDTO>(addAvailabilityTimesByContextAndDateReq, "availabilityschedule/addavailabilitytimes");
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000BC74 File Offset: 0x00009E74
		public AddAvailabilitiesActionResultDTO AddAvailabilityDatesAndTimesByContext(AvailabilityScheduleContextDTO context, IList<DateTime> dates, IList<AvailabilityScheduleTimeDTO> times, bool abortIfAnyProblems)
		{
			AddAvailabilityDatesAndTimesByContextReq addAvailabilityDatesAndTimesByContextReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddAvailabilityDatesAndTimesByContextReq>();
			addAvailabilityDatesAndTimesByContextReq.Context = context;
			addAvailabilityDatesAndTimesByContextReq.Dates = dates;
			addAvailabilityDatesAndTimesByContextReq.Times = times;
			addAvailabilityDatesAndTimesByContextReq.AbortIfAnyProblems = abortIfAnyProblems;
			return base.Post<AddAvailabilityDatesAndTimesByContextReq, AddAvailabilitiesActionResultDTO>(addAvailabilityDatesAndTimesByContextReq, "availabilityschedule/addavailabilitydatesandtimes");
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		public DeleteAvailabilityActionResultDTO DeleteAvailabilityTimeByContext(AvailabilityScheduleContextDTO context, AvailabilityScheduleDateAndTimeDTO dayAndTime)
		{
			DeleteAvailabilityTimeByContextReq deleteAvailabilityTimeByContextReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAvailabilityTimeByContextReq>();
			deleteAvailabilityTimeByContextReq.Context = context;
			deleteAvailabilityTimeByContextReq.DayAndTime = dayAndTime;
			return base.Post<DeleteAvailabilityTimeByContextReq, DeleteAvailabilityActionResultDTO>(deleteAvailabilityTimeByContextReq, "availabilityschedule/deleteavailabilitytime");
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000BCEC File Offset: 0x00009EEC
		public IList<DeleteAvailabilityActionResultDTO> DeleteAvailabilityDatesAndTimesByContext(AvailabilityScheduleContextDTO context, IList<AvailabilityScheduleDateAndTimeDTO> dayAndTimes)
		{
			DeleteAvailabilityDatesAndTimesByContextReq deleteAvailabilityDatesAndTimesByContextReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAvailabilityDatesAndTimesByContextReq>();
			deleteAvailabilityDatesAndTimesByContextReq.Context = context;
			deleteAvailabilityDatesAndTimesByContextReq.DayAndTimes = dayAndTimes;
			return base.Post<DeleteAvailabilityDatesAndTimesByContextReq, IList<DeleteAvailabilityActionResultDTO>>(deleteAvailabilityDatesAndTimesByContextReq, "availabilityschedule/deleteavailabilitydatesandtimes");
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000BD20 File Offset: 0x00009F20
		public void ClearAvailabilityForTheDay(AvailabilityScheduleContextDTO context, IList<DateTime> days)
		{
			ClearAvailabilityForTheDayReq clearAvailabilityForTheDayReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearAvailabilityForTheDayReq>();
			clearAvailabilityForTheDayReq.Context = context;
			clearAvailabilityForTheDayReq.Days = days;
			base.Post<ClearAvailabilityForTheDayReq>(clearAvailabilityForTheDayReq, "availabilityschedule/clearvailabilityfortheday");
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000BD52 File Offset: 0x00009F52
		public IList<DateTime> LoadDaysWithAvailability(int PersonId, IList<int> AvailabilityGroupIds, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<DateTime>(string.Format("availabilityschedule/dayswithavailability/personid/{0}/availabilitygroupids/{1}/range/{2}/{3}", new object[]
			{
				PersonId,
				AvailabilityGroupIds.CommaSeparatedValuesWithoutSpace<int>(),
				StartDate,
				EndDate
			}), true);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000BD90 File Offset: 0x00009F90
		public IList<AvailabilityGroupDTO> LoadAllAvailabilityGroups()
		{
			return base.GetMany<AvailabilityGroupDTO>("availabilityschedule/availabilitygroups", true);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		public IList<AvailabilityScheduleItemsForContextDTO> LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(IList<AvailabilityScheduleContextDTO> contexts, DateTime startDate, int numDays)
		{
			LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq>();
			loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq.Contexts = contexts;
			loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq.StartDate = startDate;
			loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq.NumDays = numDays;
			return base.Post<LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq, IList<AvailabilityScheduleItemsForContextDTO>>(loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq, "availabilityschedule/loadunbookedavailabilityitems");
		}
	}
}
