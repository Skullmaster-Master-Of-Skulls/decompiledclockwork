using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.ICore.AvailabilitySchedule
{
	// Token: 0x020000BA RID: 186
	public interface IAvailabilityScheduleManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000581 RID: 1409
		AvailabilityScheduleItemsForContext LoadAvailabilityItemsByContextAndDateRange(AvailabilityScheduleContext context, DateTime startDate, int numDays);

		// Token: 0x06000582 RID: 1410
		Task<AvailabilityScheduleItemsForContext> LoadAvailabilityItemsByContextAndDateRangeAsync(AvailabilityScheduleContext context, DateTime startDate, int numDays);

		// Token: 0x06000583 RID: 1411
		IList<AvailabilityScheduleItemsForContext> LoadAvailabilityItemsByMultipleContextsAndDateRange(IList<AvailabilityScheduleContext> contexts, DateTime startDate, int numDays);

		// Token: 0x06000584 RID: 1412
		AvailabilityScheduleItemsForContext LoadAvailabilityItemsByContextAndDates(AvailabilityScheduleContext context, IList<DateTime> days);

		// Token: 0x06000585 RID: 1413
		AddAvailabilitiesActionResult AddAvailabilityTimesByContextAndDate(AvailabilityScheduleContext context, DateTime date, IList<AvailabilityScheduleTime> times, bool abortIfAnyProblems);

		// Token: 0x06000586 RID: 1414
		AddAvailabilitiesActionResult AddAvailabilityDatesAndTimesByContext(AvailabilityScheduleContext context, IList<DateTime> dates, IList<AvailabilityScheduleTime> times, bool abortIfAnyProblems);

		// Token: 0x06000587 RID: 1415
		DeleteAvailabilityActionResult DeleteAvailabilityTimeByContext(AvailabilityScheduleContext context, AvailabilityScheduleDateAndTime dayAndTime);

		// Token: 0x06000588 RID: 1416
		IList<DeleteAvailabilityActionResult> DeleteAvailabilityDatesAndTimesByContext(AvailabilityScheduleContext context, IList<AvailabilityScheduleDateAndTime> dayAndTimes);

		// Token: 0x06000589 RID: 1417
		void ClearAvailabilityForTheDay(AvailabilityScheduleContext context, IList<DateTime> days);

		// Token: 0x0600058A RID: 1418
		IList<DateTime> LoadDaysWithAvailability(int PersonId, IList<int> AvailabilityGroupIds, DateTime StartDate, DateTime EndDate);

		// Token: 0x0600058B RID: 1419
		IList<AvailabilityGroup> LoadAllAvailabilityGroups();

		// Token: 0x0600058C RID: 1420
		IList<AvailabilityScheduleItemsForContext> LoadAvailabilityForMultipleContextsAndDates(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, int numDays);

		// Token: 0x0600058D RID: 1421
		IList<AvailabilityScheduleItemsForContext> LoadAvailabilityForMultipleContextsAndDates(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, DateTime endDate);

		// Token: 0x0600058E RID: 1422
		IList<AvailabilityScheduleItemsForContext> LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(IList<AvailabilityScheduleContext> contexts, DateTime startDate, int numDays);

		// Token: 0x0600058F RID: 1423
		Task<IList<AvailabilityScheduleItemsForContext>> LoadAvailabilityForMultipleContextsAndDatesAsync(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, DateTime endDate);

		// Token: 0x06000590 RID: 1424
		Task<IList<AvailabilityScheduleItemsForContext>> LoadAvailabilityForMultipleContextsAndDatesAsync(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, int numDays);
	}
}
