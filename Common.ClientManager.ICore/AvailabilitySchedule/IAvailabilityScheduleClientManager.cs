using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AvailabilitySchedule
{
	// Token: 0x02000077 RID: 119
	public interface IAvailabilityScheduleClientManager : IWebService
	{
		// Token: 0x0600036D RID: 877
		IList<AvailabilityScheduleItemsForContextDTO> LoadAvailabilityItemsByMultipleContextsAndDateRange(IList<AvailabilityScheduleContextDTO> contexts, DateTime startDate, int numDays);

		// Token: 0x0600036E RID: 878
		AvailabilityScheduleItemsForContextDTO LoadAvailabilityItemsByContextAndDateRange(AvailabilityScheduleContextDTO context, DateTime startDate, int numDays);

		// Token: 0x0600036F RID: 879
		AvailabilityScheduleItemsForContextDTO LoadAvailabilityItemsByContextAndDates(AvailabilityScheduleContextDTO context, IList<DateTime> days);

		// Token: 0x06000370 RID: 880
		AddAvailabilitiesActionResultDTO AddAvailabilityTimesByContextAndDate(AvailabilityScheduleContextDTO context, DateTime date, IList<AvailabilityScheduleTimeDTO> times, bool abortIfAnyProblems);

		// Token: 0x06000371 RID: 881
		AddAvailabilitiesActionResultDTO AddAvailabilityDatesAndTimesByContext(AvailabilityScheduleContextDTO context, IList<DateTime> dates, IList<AvailabilityScheduleTimeDTO> times, bool abortIfAnyProblems);

		// Token: 0x06000372 RID: 882
		DeleteAvailabilityActionResultDTO DeleteAvailabilityTimeByContext(AvailabilityScheduleContextDTO context, AvailabilityScheduleDateAndTimeDTO dayAndTime);

		// Token: 0x06000373 RID: 883
		IList<DeleteAvailabilityActionResultDTO> DeleteAvailabilityDatesAndTimesByContext(AvailabilityScheduleContextDTO context, IList<AvailabilityScheduleDateAndTimeDTO> dayAndTimes);

		// Token: 0x06000374 RID: 884
		void ClearAvailabilityForTheDay(AvailabilityScheduleContextDTO context, IList<DateTime> days);

		// Token: 0x06000375 RID: 885
		IList<DateTime> LoadDaysWithAvailability(int PersonId, IList<int> AvailabilityGroupIds, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000376 RID: 886
		IList<AvailabilityGroupDTO> LoadAllAvailabilityGroups();

		// Token: 0x06000377 RID: 887
		IList<AvailabilityScheduleItemsForContextDTO> LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(IList<AvailabilityScheduleContextDTO> contexts, DateTime startDate, int numDays);
	}
}
