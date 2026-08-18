using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.DAO.AppointmentsTestBooking
{
	// Token: 0x020000BF RID: 191
	public interface IAutoTestBookingDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600051A RID: 1306
		FindPotentialBookingsResp FindPotentialBookingsExplicit(FindPotentialBookingsReq req);

		// Token: 0x0600051B RID: 1307
		int CalculateExtraTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<Accommodation> AccommodationsToUse, IList<SpecialAccommodation> AllSpecialAccommodations);

		// Token: 0x0600051C RID: 1308
		int CalculateBreakTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<Accommodation> AccommodationsToUse, IList<SpecialAccommodation> AllSpecialAccommodations);

		// Token: 0x0600051D RID: 1309
		ApplySpecialAccommodationsResp ApplySpecialAccommodationRules(bool debugMode, int pid, int lucid, IList<SpecialAccommodation> specialAccommodations, DateTime classTestStartDateTime, DateTime classTestEndDateTime, IList<Accommodation> accommodationsToUse, int appIdToIgnoreWhenCheckingStudentsSchedule, int overrideRoomAvailabilityPid, IList<Room> availableRooms, bool IgnoreStudentsSchedule, IList<int> IgnoreStudentAppointmentIds);

		// Token: 0x0600051E RID: 1310
		int FindFinalExamAppTypeToUseForNewExamAutoBooking();
	}
}
