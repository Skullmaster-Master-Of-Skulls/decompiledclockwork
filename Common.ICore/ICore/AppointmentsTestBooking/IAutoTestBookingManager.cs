using System;
using System.Collections.Generic;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000CE RID: 206
	public interface IAutoTestBookingManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600063D RID: 1597
		FindPotentialBookingsReq LoadBaseAutoTestBookingSettings(eTestExamSettingType TestType, eAutoTestBookingContext TestBookingContext, string ClockWorkSettingsInstanceName, bool ClearCacheFirst);

		// Token: 0x0600063E RID: 1598
		FindPotentialBookingsResp FindPotentialBookingsExplicit(FindPotentialBookingsReq req);

		// Token: 0x0600063F RID: 1599
		FindPotentialBookingsResp FindPotentialBookings(eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, IList<Accommodation> accs, string ClockWorkInstanceName, bool clearCacheFirst);

		// Token: 0x06000640 RID: 1600
		FindPotentialBookingsResp FindPotentialBookings(eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, IList<Accommodation> accs, string ClockWorkInstanceName, bool clearCacheFirst, bool debugMode);

		// Token: 0x06000641 RID: 1601
		ApplySpecialAccommodationsResp ApplySpecialAccommodations(bool debugMode, eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, IList<Accommodation> accs, string ClockWorkInstanceName, bool clearCacheFirst);

		// Token: 0x06000642 RID: 1602
		int CalculateExtraTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<Accommodation> AccommodationsToUse);

		// Token: 0x06000643 RID: 1603
		int CalculateBreakTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<Accommodation> AccommodationsToUse);

		// Token: 0x06000644 RID: 1604
		void ClearAutoTestBookingCache(eTestExamSettingType TestType, ISettingManager sm, ICacheStorageManager cache, string ClockWorkSettingsInstanceName);

		// Token: 0x06000645 RID: 1605
		IList<Asset> LoadAvailableAssets(eTestExamSettingType TestType, ISettingManager sm, ICacheStorageManager cache, string ClockWorkSettingsInstanceName, bool clearCacheFirst);

		// Token: 0x06000646 RID: 1606
		IList<SpecialAccommodation> LoadSpecialAccommodations(eTestExamSettingType TestType, ISettingManager sm, ICacheStorageManager cache, string ClockWorkSettingsInstanceName, bool clearCacheFirst);

		// Token: 0x06000647 RID: 1607
		IList<Room> LoadAvailableRooms(eTestExamSettingType TestType, ISettingManager sm, ICacheStorageManager cache, IList<Asset> availableAssets, string ClockWorkSettingsInstanceName, bool clearCacheFirst);

		// Token: 0x06000648 RID: 1608
		IList<TestRule> LoadTestRules(eTestExamSettingType TestType, ISettingManager sm, ICacheStorageManager cache, string ClockWorkSettingsInstanceName, bool clearCacheFirst);

		// Token: 0x06000649 RID: 1609
		TryToBookResult TryToFindBooking(eTestExamSettingType TestType, bool StaffIsBooking, int PersonId, int LuCourseId, DateTime ClassStartDateTime, int ClassTestDurationInMinutes, IList<int> AccommodationsToUse, bool IgnoreSpecialAccommodations, int BookingAlreadyExistsAppointmentId, IList<TryToBookAccommodationToUse> AdditionalAccommodationsToUse, bool clearCacheFirst, string ClockWorkInstanceNameToUse = null);

		// Token: 0x0600064A RID: 1610
		AutoBookTestExamResult AutoBookTestOrExam(int examId, eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, bool clearCacheFirst);

		// Token: 0x0600064B RID: 1611
		AutoBookTestExamPreviewResult AutoBookTestOrExamPreview(eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, bool clearCacheFirst);

		// Token: 0x0600064C RID: 1612
		AutoRescheduleTestExamResult AutoRescheduleTestOrExam(int appId);
	}
}
