using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x02000084 RID: 132
	public interface IAutoTestBookingClientManager : IWebService
	{
		// Token: 0x060003DA RID: 986
		FindPotentialBookingsReqDTO LoadBaseAutoTestBookingSettings(eTestExamSettingType TestType, eAutoTestBookingContext TestBookingContext, string OptionalClockWorkSettingsInstanceName, bool ClearCacheFirst);

		// Token: 0x060003DB RID: 987
		FindPotentialBookingsRespDTO FindPotentialBookingsExplicit(FindPotentialBookingsReqDTO req);

		// Token: 0x060003DC RID: 988
		FindPotentialBookingsRespDTO FindPotentialBookings(eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, IList<AccommodationDTO> accs, string ClockWorkInstanceName, bool clearCacheFirst);

		// Token: 0x060003DD RID: 989
		ApplySpecialAccommodationsRespDTO ApplySpecialAccommodations(bool debugMode, eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, IList<AccommodationDTO> accs, string ClockWorkInstanceName, bool clearCacheFirst);

		// Token: 0x060003DE RID: 990
		int CalculateExtraTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<AccommodationDTO> AccommodationsToUse);

		// Token: 0x060003DF RID: 991
		int CalculateBreakTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<AccommodationDTO> AccommodationsToUse);

		// Token: 0x060003E0 RID: 992
		void ClearAutoTestBookingCache(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName);

		// Token: 0x060003E1 RID: 993
		IList<AssetDTO> LoadAvailableAssets(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst);

		// Token: 0x060003E2 RID: 994
		IList<SpecialAccommodationDTO> LoadSpecialAccommodations(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst);

		// Token: 0x060003E3 RID: 995
		IList<RoomDTO> LoadAvailableRooms(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst);

		// Token: 0x060003E4 RID: 996
		IList<TestRuleDTO> LoadTestRules(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst);

		// Token: 0x060003E5 RID: 997
		TryToBookResultDTO TryToFindBooking(eTestExamSettingType TestType, bool StaffIsBooking, int PersonId, int LuCourseId, DateTime ClassStartDateTime, int ClassTestDurationInMinutes, IList<int> AccommodationCidsToUse, bool IgnoreSpecialAccommodations, int BookingAlreadyExistsAppointmentId, IList<TryToBookAccommodationToUseDTO> AdditionalAccommodationsToUse, bool ClearCacheFirst, string ClockWorkInstanceName = null);

		// Token: 0x060003E6 RID: 998
		AutoBookTestExamResultDTO AutoBookTestOrExam(int examId, eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, bool clearCacheFirst);

		// Token: 0x060003E7 RID: 999
		AutoBookTestExamPreviewResultDTO AutoBookTestOrExamPreview(eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, bool clearCacheFirst);

		// Token: 0x060003E8 RID: 1000
		AutoRescheduleTestExamResultDTO AutoRescheduleTestOrExam(int appId);
	}
}
