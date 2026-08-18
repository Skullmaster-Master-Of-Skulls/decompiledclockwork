using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Core.AvailabilitySchedule;
using TechnoPro.Common.Core.Mappers.AvailabilitySchedule;
using TechnoPro.Common.ICore.AvailabilitySchedule;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000026 RID: 38
	public class AvailabilityScheduleServiceManager : IAvailabilitySchedule, IService
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00008750 File Offset: 0x00006950
		public LoadAvailabilityItemsByContextAndDateRangeResp LoadAvailabilityItemsByContextAndDateRange(LoadAvailabilityItemsByContextAndDateRangeReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IAvailabilityScheduleManager availabilityScheduleManager2 = availabilityScheduleManager;
			AvailabilityScheduleContextDTO context = Request.Context;
			AvailabilityScheduleItemsForContext availabilityScheduleItemsForContext = availabilityScheduleManager2.LoadAvailabilityItemsByContextAndDateRange((context != null) ? context.ToDomainObject() : null, Request.StartDate, Request.NumDays);
			return new LoadAvailabilityItemsByContextAndDateRangeResp
			{
				Result = ((availabilityScheduleItemsForContext != null) ? availabilityScheduleItemsForContext.ToDTO() : null)
			};
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000087AC File Offset: 0x000069AC
		public LoadAvailabilityItemsByMultipleContextsAndDateRangeResp LoadAvailabilityItemsByMultipleContextsAndDateRange(LoadAvailabilityItemsByMultipleContextsAndDateRangeReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IAvailabilityScheduleManager availabilityScheduleManager2 = availabilityScheduleManager;
			IList<AvailabilityScheduleContextDTO> contexts = Request.Contexts;
			IList<AvailabilityScheduleContext> contexts2;
			if (contexts == null)
			{
				contexts2 = null;
			}
			else
			{
				contexts2 = (from g in contexts
				select g.ToDomainObject()).ToList<AvailabilityScheduleContext>();
			}
			IList<AvailabilityScheduleItemsForContext> list = availabilityScheduleManager2.LoadAvailabilityItemsByMultipleContextsAndDateRange(contexts2, Request.StartDate, Request.NumDays);
			LoadAvailabilityItemsByMultipleContextsAndDateRangeResp loadAvailabilityItemsByMultipleContextsAndDateRangeResp = new LoadAvailabilityItemsByMultipleContextsAndDateRangeResp();
			IList<AvailabilityScheduleItemsForContextDTO> result;
			if (list == null)
			{
				result = null;
			}
			else
			{
				result = (from g in list
				select g.ToDTO()).ToList<AvailabilityScheduleItemsForContextDTO>();
			}
			loadAvailabilityItemsByMultipleContextsAndDateRangeResp.Result = result;
			return loadAvailabilityItemsByMultipleContextsAndDateRangeResp;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008850 File Offset: 0x00006A50
		public LoadAvailabilityItemsByContextAndDatesResp LoadAvailabilityItemsByContextAndDates(LoadAvailabilityItemsByContextAndDatesReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IAvailabilityScheduleManager availabilityScheduleManager2 = availabilityScheduleManager;
			AvailabilityScheduleContextDTO context = Request.Context;
			AvailabilityScheduleItemsForContext availabilityScheduleItemsForContext = availabilityScheduleManager2.LoadAvailabilityItemsByContextAndDates((context != null) ? context.ToDomainObject() : null, Request.Days);
			return new LoadAvailabilityItemsByContextAndDatesResp
			{
				Result = ((availabilityScheduleItemsForContext != null) ? availabilityScheduleItemsForContext.ToDTO() : null)
			};
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000088A8 File Offset: 0x00006AA8
		public AddAvailabilityTimesByContextAndDateResp AddAvailabilityTimesByContextAndDate(AddAvailabilityTimesByContextAndDateReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IAvailabilityScheduleManager availabilityScheduleManager2 = availabilityScheduleManager;
			AvailabilityScheduleContextDTO context = Request.Context;
			AvailabilityScheduleContext context2 = (context != null) ? context.ToDomainObject() : null;
			DateTime date = Request.Date;
			IList<AvailabilityScheduleTimeDTO> times = Request.Times;
			IList<AvailabilityScheduleTime> times2;
			if (times == null)
			{
				times2 = null;
			}
			else
			{
				times2 = (from g in times
				select g.ToDomainObject()).ToList<AvailabilityScheduleTime>();
			}
			AddAvailabilitiesActionResult addAvailabilitiesActionResult = availabilityScheduleManager2.AddAvailabilityTimesByContextAndDate(context2, date, times2, Request.AbortIfAnyProblems);
			return new AddAvailabilityTimesByContextAndDateResp
			{
				Result = ((addAvailabilitiesActionResult != null) ? addAvailabilitiesActionResult.ToDTO() : null)
			};
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000893C File Offset: 0x00006B3C
		public AddAvailabilityDatesAndTimesByContextResp AddAvailabilityDatesAndTimesByContext(AddAvailabilityDatesAndTimesByContextReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IAvailabilityScheduleManager availabilityScheduleManager2 = availabilityScheduleManager;
			AvailabilityScheduleContextDTO context = Request.Context;
			AvailabilityScheduleContext context2 = (context != null) ? context.ToDomainObject() : null;
			IList<DateTime> dates = Request.Dates;
			IList<AvailabilityScheduleTimeDTO> times = Request.Times;
			IList<AvailabilityScheduleTime> times2;
			if (times == null)
			{
				times2 = null;
			}
			else
			{
				times2 = (from g in times
				select g.ToDomainObject()).ToList<AvailabilityScheduleTime>();
			}
			AddAvailabilitiesActionResult addAvailabilitiesActionResult = availabilityScheduleManager2.AddAvailabilityDatesAndTimesByContext(context2, dates, times2, Request.AbortIfAnyProblems);
			return new AddAvailabilityDatesAndTimesByContextResp
			{
				Result = ((addAvailabilitiesActionResult != null) ? addAvailabilitiesActionResult.ToDTO() : null)
			};
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000089D0 File Offset: 0x00006BD0
		public DeleteAvailabilityTimeByContextResp DeleteAvailabilityTimeByContext(DeleteAvailabilityTimeByContextReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IAvailabilityScheduleManager availabilityScheduleManager2 = availabilityScheduleManager;
			AvailabilityScheduleContextDTO context = Request.Context;
			AvailabilityScheduleContext context2 = (context != null) ? context.ToDomainObject() : null;
			AvailabilityScheduleDateAndTimeDTO dayAndTime = Request.DayAndTime;
			DeleteAvailabilityActionResult deleteAvailabilityActionResult = availabilityScheduleManager2.DeleteAvailabilityTimeByContext(context2, (dayAndTime != null) ? dayAndTime.ToDomainObject() : null);
			return new DeleteAvailabilityTimeByContextResp
			{
				Result = ((deleteAvailabilityActionResult != null) ? deleteAvailabilityActionResult.ToDTO() : null)
			};
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008A34 File Offset: 0x00006C34
		public DeleteAvailabilityDatesAndTimesByContextResp DeleteAvailabilityDatesAndTimesByContext(DeleteAvailabilityDatesAndTimesByContextReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IAvailabilityScheduleManager availabilityScheduleManager2 = availabilityScheduleManager;
			AvailabilityScheduleContextDTO context = Request.Context;
			AvailabilityScheduleContext context2 = (context != null) ? context.ToDomainObject() : null;
			IList<AvailabilityScheduleDateAndTimeDTO> dayAndTimes = Request.DayAndTimes;
			IList<AvailabilityScheduleDateAndTime> dayAndTimes2;
			if (dayAndTimes == null)
			{
				dayAndTimes2 = null;
			}
			else
			{
				dayAndTimes2 = (from g in dayAndTimes
				select g.ToDomainObject()).ToList<AvailabilityScheduleDateAndTime>();
			}
			IList<DeleteAvailabilityActionResult> list = availabilityScheduleManager2.DeleteAvailabilityDatesAndTimesByContext(context2, dayAndTimes2);
			DeleteAvailabilityDatesAndTimesByContextResp deleteAvailabilityDatesAndTimesByContextResp = new DeleteAvailabilityDatesAndTimesByContextResp();
			IList<DeleteAvailabilityActionResultDTO> result;
			if (list == null)
			{
				result = null;
			}
			else
			{
				result = (from g in list
				select g.ToDTO()).ToList<DeleteAvailabilityActionResultDTO>();
			}
			deleteAvailabilityDatesAndTimesByContextResp.Result = result;
			return deleteAvailabilityDatesAndTimesByContextResp;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public ClearAvailabilityForTheDayResp ClearAvailabilityForTheDay(ClearAvailabilityForTheDayReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IAvailabilityScheduleManager availabilityScheduleManager2 = availabilityScheduleManager;
			AvailabilityScheduleContextDTO context = Request.Context;
			availabilityScheduleManager2.ClearAvailabilityForTheDay((context != null) ? context.ToDomainObject() : null, Request.Days);
			return new ClearAvailabilityForTheDayResp();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008B24 File Offset: 0x00006D24
		public LoadDaysWithAvailabilityResp LoadDaysWithAvailability(LoadDaysWithAvailabilityReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IList<DateTime> daysWithAvailability = availabilityScheduleManager.LoadDaysWithAvailability(Request.PersonId, Request.AvailabilityGroupIds, Request.StartDate, Request.EndDate);
			return new LoadDaysWithAvailabilityResp
			{
				DaysWithAvailability = daysWithAvailability
			};
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008B70 File Offset: 0x00006D70
		public LoadAllAvailabilityGroupsResp LoadAllAvailabilityGroups(LoadAllAvailabilityGroupsReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IList<AvailabilityGroup> list = availabilityScheduleManager.LoadAllAvailabilityGroups();
			LoadAllAvailabilityGroupsResp loadAllAvailabilityGroupsResp = new LoadAllAvailabilityGroupsResp();
			IList<AvailabilityGroupDTO> availabilityGroups;
			if (list == null)
			{
				availabilityGroups = null;
			}
			else
			{
				availabilityGroups = (from g in list
				select g.ToDTO()).ToList<AvailabilityGroupDTO>();
			}
			loadAllAvailabilityGroupsResp.AvailabilityGroups = availabilityGroups;
			return loadAllAvailabilityGroupsResp;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008BD4 File Offset: 0x00006DD4
		public LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeReq Request)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(Request.GetOperationContext());
			IAvailabilityScheduleManager availabilityScheduleManager2 = availabilityScheduleManager;
			IList<AvailabilityScheduleContextDTO> contexts = Request.Contexts;
			IList<AvailabilityScheduleContext> contexts2;
			if (contexts == null)
			{
				contexts2 = null;
			}
			else
			{
				contexts2 = (from g in contexts
				select g.ToDomainObject()).ToList<AvailabilityScheduleContext>();
			}
			IList<AvailabilityScheduleItemsForContext> list = availabilityScheduleManager2.LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(contexts2, Request.StartDate, Request.NumDays);
			LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp = new LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp();
			IList<AvailabilityScheduleItemsForContextDTO> result;
			if (list == null)
			{
				result = null;
			}
			else
			{
				result = (from g in list
				select g.ToDTO()).ToList<AvailabilityScheduleItemsForContextDTO>();
			}
			loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp.Result = result;
			return loadUnbookedAvailabilityItemsByMultipleContextsAndDateRangeResp;
		}
	}
}
