using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200008A RID: 138
	public class AutoTestBookingClientManager : IAutoTestBookingClientManager, IWebService
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0001622C File Offset: 0x0001442C
		public FindPotentialBookingsReqDTO LoadBaseAutoTestBookingSettings(eTestExamSettingType TestType, eAutoTestBookingContext TestBookingContext, string OptionalClockWorkSettingsInstanceName, bool ClearCacheFirst)
		{
			LoadBaseAutoTestBookingSettingsReq loadBaseAutoTestBookingSettingsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadBaseAutoTestBookingSettingsReq>();
			loadBaseAutoTestBookingSettingsReq.TestType = TestType;
			loadBaseAutoTestBookingSettingsReq.TestBookingContext = TestBookingContext;
			loadBaseAutoTestBookingSettingsReq.ClearCacheFirst = ClearCacheFirst;
			loadBaseAutoTestBookingSettingsReq.OptionalClockWorkSettingsInstanceName = OptionalClockWorkSettingsInstanceName;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().LoadBaseAutoTestBookingSettings(loadBaseAutoTestBookingSettingsReq).FindPotentialBookingsRequest;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001627C File Offset: 0x0001447C
		public FindPotentialBookingsRespDTO FindPotentialBookingsExplicit(FindPotentialBookingsReqDTO bookingsReq)
		{
			FindPotentialBookingsExplicitReq findPotentialBookingsExplicitReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FindPotentialBookingsExplicitReq>();
			findPotentialBookingsExplicitReq.Request = bookingsReq;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().FindPotentialBookingsExplicit(findPotentialBookingsExplicitReq).Result;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000162B4 File Offset: 0x000144B4
		public int CalculateExtraTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<AccommodationDTO> AccommodationsToUse)
		{
			CalculateExtraTimeReq calculateExtraTimeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CalculateExtraTimeReq>();
			calculateExtraTimeReq.TestType = TestType;
			calculateExtraTimeReq.ClassTestDurationInMinutes = ClassTestDurationInMinutes;
			calculateExtraTimeReq.AccommodationsToUse = AccommodationsToUse;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().CalculateExtraTime(calculateExtraTimeReq).NumExtraMinutes;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000162FC File Offset: 0x000144FC
		public int CalculateBreakTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<AccommodationDTO> AccommodationsToUse)
		{
			CalculateBreakTimeReq calculateBreakTimeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CalculateBreakTimeReq>();
			calculateBreakTimeReq.TestType = TestType;
			calculateBreakTimeReq.ClassTestDurationInMinutes = ClassTestDurationInMinutes;
			calculateBreakTimeReq.AccommodationsToUse = AccommodationsToUse;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().CalculateBreakTime(calculateBreakTimeReq).NumExtraMinutes;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00016344 File Offset: 0x00014544
		public void ClearAutoTestBookingCache(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName)
		{
			ClearAutoTestBookingCacheReq clearAutoTestBookingCacheReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearAutoTestBookingCacheReq>();
			clearAutoTestBookingCacheReq.TestType = TestType;
			clearAutoTestBookingCacheReq.OptionalClockWorkSettingsInstanceName = ClockWorkSettingsInstanceName;
			ClientServiceFactory.GetClientInstance<IAutoTestBooking>().ClearAutoTestBookingCache(clearAutoTestBookingCacheReq);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001637C File Offset: 0x0001457C
		public IList<AssetDTO> LoadAvailableAssets(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			LoadAvailableAssetsReq loadAvailableAssetsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailableAssetsReq>();
			loadAvailableAssetsReq.TestType = TestType;
			loadAvailableAssetsReq.OptionalClockWorkSettingsInstanceName = ClockWorkSettingsInstanceName;
			loadAvailableAssetsReq.ClearCacheFirst = clearCacheFirst;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().LoadAvailableAssets(loadAvailableAssetsReq).Assets;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000163C4 File Offset: 0x000145C4
		public IList<SpecialAccommodationDTO> LoadSpecialAccommodations(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			LoadSpecialAccommodationsReq loadSpecialAccommodationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadSpecialAccommodationsReq>();
			loadSpecialAccommodationsReq.TestType = TestType;
			loadSpecialAccommodationsReq.ClearCacheFirst = clearCacheFirst;
			loadSpecialAccommodationsReq.OptionalClockWorkSettingsInstanceName = ClockWorkSettingsInstanceName;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().LoadSpecialAccommodations(loadSpecialAccommodationsReq).SpecialAccommodations;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001640C File Offset: 0x0001460C
		public IList<RoomDTO> LoadAvailableRooms(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			LoadAvailableRoomsReq loadAvailableRoomsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAvailableRoomsReq>();
			loadAvailableRoomsReq.TestType = TestType;
			loadAvailableRoomsReq.OptionalClockWorkSettingsInstanceName = ClockWorkSettingsInstanceName;
			loadAvailableRoomsReq.ClearCacheFirst = clearCacheFirst;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().LoadAvailableRooms(loadAvailableRoomsReq).Rooms;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00016454 File Offset: 0x00014654
		public IList<TestRuleDTO> LoadTestRules(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			LoadTestRulesReq loadTestRulesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestRulesReq>();
			loadTestRulesReq.TestType = TestType;
			loadTestRulesReq.OptionalClockWorkSettingsInstanceName = ClockWorkSettingsInstanceName;
			loadTestRulesReq.ClearCacheFirst = clearCacheFirst;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().LoadTestRules(loadTestRulesReq).TestRules;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001649C File Offset: 0x0001469C
		public FindPotentialBookingsRespDTO FindPotentialBookings(eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, IList<AccommodationDTO> accs, string ClockWorkInstanceName, bool clearCacheFirst)
		{
			FindPotentialBookings2Req findPotentialBookings2Req = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FindPotentialBookings2Req>();
			findPotentialBookings2Req.TestType = testType;
			findPotentialBookings2Req.OptionalClockWorkSettingsInstanceName = ClockWorkInstanceName;
			findPotentialBookings2Req.ClearCacheFirst = clearCacheFirst;
			findPotentialBookings2Req.AccommodationsToUse = accs;
			findPotentialBookings2Req.ClassStartDateTime = classStartDateTime;
			findPotentialBookings2Req.ClassEndDateTime = classEndDateTime;
			findPotentialBookings2Req.DebugMode = false;
			findPotentialBookings2Req.LuCourseId = lucid;
			findPotentialBookings2Req.PersonId = pid;
			findPotentialBookings2Req.TestBookingContext = testBookingContext;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().FindPotentialBookings(findPotentialBookings2Req).Result;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00016520 File Offset: 0x00014720
		public ApplySpecialAccommodationsRespDTO ApplySpecialAccommodations(bool debugMode, eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, IList<AccommodationDTO> accs, string ClockWorkInstanceName, bool clearCacheFirst)
		{
			ApplySpecialAccommodations2Req applySpecialAccommodations2Req = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ApplySpecialAccommodations2Req>();
			applySpecialAccommodations2Req.TestType = testType;
			applySpecialAccommodations2Req.OptionalClockWorkSettingsInstanceName = ClockWorkInstanceName;
			applySpecialAccommodations2Req.ClearCacheFirst = clearCacheFirst;
			applySpecialAccommodations2Req.AccommodationsToUse = accs;
			applySpecialAccommodations2Req.ClassStartDateTime = classStartDateTime;
			applySpecialAccommodations2Req.ClassEndDateTime = classEndDateTime;
			applySpecialAccommodations2Req.DebugMode = false;
			applySpecialAccommodations2Req.LuCourseId = lucid;
			applySpecialAccommodations2Req.PersonId = pid;
			applySpecialAccommodations2Req.TestBookingContext = testBookingContext;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().ApplySpecialAccommodations(applySpecialAccommodations2Req).Result;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000165A4 File Offset: 0x000147A4
		public TryToBookResultDTO TryToFindBooking(eTestExamSettingType TestType, bool StaffIsBooking, int PersonId, int LuCourseId, DateTime ClassStartDateTime, int ClassTestDurationInMinutes, IList<int> AccommodationCidsToUse, bool IgnoreSpecialAccommodations, int BookingAlreadyExistsAppointmentId, IList<TryToBookAccommodationToUseDTO> AdditionalAccommodationsToUse, bool ClearCacheFirst, string ClockWorkInstanceName = null)
		{
			TryToFindBookingReq tryToFindBookingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TryToFindBookingReq>();
			tryToFindBookingReq.TestType = TestType;
			tryToFindBookingReq.StaffIsBooking = StaffIsBooking;
			tryToFindBookingReq.PersonId = PersonId;
			tryToFindBookingReq.LuCourseId = LuCourseId;
			tryToFindBookingReq.ClassStartDateTime = ClassStartDateTime;
			tryToFindBookingReq.ClassTestDurationInMinutes = ClassTestDurationInMinutes;
			tryToFindBookingReq.AccommodationsToUse = AccommodationCidsToUse;
			tryToFindBookingReq.IgnoreSpecialAccommodations = IgnoreSpecialAccommodations;
			tryToFindBookingReq.BookingAlreadyExistsAppointmentId = BookingAlreadyExistsAppointmentId;
			tryToFindBookingReq.ClearCacheFirst = ClearCacheFirst;
			tryToFindBookingReq.ClockWorkInstanceNameToUse = ClockWorkInstanceName;
			tryToFindBookingReq.AdditionalAccommodationsToUse = AdditionalAccommodationsToUse;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().TryToFindBooking(tryToFindBookingReq).FindBookingResult;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001663C File Offset: 0x0001483C
		public AutoBookTestExamResultDTO AutoBookTestOrExam(int examId, eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, bool clearCacheFirst)
		{
			AutoBookTestOrExamReq autoBookTestOrExamReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AutoBookTestOrExamReq>();
			autoBookTestOrExamReq.ExamId = examId;
			autoBookTestOrExamReq.ClassStartDateTime = classStartDateTime;
			autoBookTestOrExamReq.ClassEndDateTime = classEndDateTime;
			autoBookTestOrExamReq.ClearCacheFirst = clearCacheFirst;
			autoBookTestOrExamReq.Lucid = lucid;
			autoBookTestOrExamReq.Pid = pid;
			autoBookTestOrExamReq.TestBookingContext = testBookingContext;
			autoBookTestOrExamReq.TestType = testType;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().AutoBookTestOrExam(autoBookTestOrExamReq).AutoBookTestExamResult;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000166B0 File Offset: 0x000148B0
		public AutoBookTestExamPreviewResultDTO AutoBookTestOrExamPreview(eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, bool clearCacheFirst)
		{
			AutoBookTestOrExamPreviewReq autoBookTestOrExamPreviewReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AutoBookTestOrExamPreviewReq>();
			autoBookTestOrExamPreviewReq.ClassStartDateTime = classStartDateTime;
			autoBookTestOrExamPreviewReq.ClassEndDateTime = classEndDateTime;
			autoBookTestOrExamPreviewReq.ClearCacheFirst = clearCacheFirst;
			autoBookTestOrExamPreviewReq.Lucid = lucid;
			autoBookTestOrExamPreviewReq.Pid = pid;
			autoBookTestOrExamPreviewReq.TestBookingContext = testBookingContext;
			autoBookTestOrExamPreviewReq.TestType = testType;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().AutoBookTestOrExamPreview(autoBookTestOrExamPreviewReq).AutoBookTestExamPreviewResult;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001671C File Offset: 0x0001491C
		public AutoRescheduleTestExamResultDTO AutoRescheduleTestOrExam(int appId)
		{
			AutoRescheduleTestOrExamReq autoRescheduleTestOrExamReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AutoRescheduleTestOrExamReq>();
			autoRescheduleTestOrExamReq.AppointmentId = appId;
			return ClientServiceFactory.GetClientInstance<IAutoTestBooking>().AutoRescheduleTestOrExam(autoRescheduleTestOrExamReq).AutoRescheduleTestExamPreviewResult;
		}
	}
}
