using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.DAO.AvailabilitySchedule
{
	// Token: 0x020000A0 RID: 160
	public interface IAvailabilityScheduleDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000425 RID: 1061
		IList<DateTime> LoadDaysWithAvailability(int PersonId, IList<int> AvailabilityGroupIds, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000426 RID: 1062
		IList<AvailabilityGroup> LoadAllAvailabilityGroups();

		// Token: 0x06000427 RID: 1063
		AvailabilityScheduleItemsForContext LoadAvailabilityItemsByContextAndDateRange(AvailabilityScheduleContext context, DateTime startDate, int numDays);

		// Token: 0x06000428 RID: 1064
		Task<AvailabilityScheduleItemsForContext> LoadAvailabilityItemsByContextAndDateRangeAsync(AvailabilityScheduleContext context, DateTime startDate, int numDays);

		// Token: 0x06000429 RID: 1065
		AvailabilityScheduleItemsForContext LoadAvailabilityItemsByContextAndDates(AvailabilityScheduleContext context, IList<DateTime> days);

		// Token: 0x0600042A RID: 1066
		void ResetAvailabilityByContextAndDate(AvailabilityScheduleContext context, DateTime date, IList<Range<TimeSpan>> newTimes);

		// Token: 0x0600042B RID: 1067
		void ClearAvailabilityForTheDay(AvailabilityScheduleContext context, IList<DateTime> days);

		// Token: 0x0600042C RID: 1068
		IList<AvailabilityScheduleItemsForContext> LoadAvailabilityForMultipleContextsAndDates(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, int numDays);

		// Token: 0x0600042D RID: 1069
		Task<IList<AvailabilityScheduleItemsForContext>> LoadAvailabilityForMultipleContextsAndDatesAsync(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, int numDays);
	}
}
