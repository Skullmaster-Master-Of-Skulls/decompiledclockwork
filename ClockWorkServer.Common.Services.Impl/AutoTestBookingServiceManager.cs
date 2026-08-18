using System;
using System.Collections.Generic;
using System.Linq;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.Booker2;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000098 RID: 152
	public class AutoTestBookingServiceManager : IAutoTestBooking, IService
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x000199B8 File Offset: 0x00017BB8
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000199CC File Offset: 0x00017BCC
		public LoadBaseAutoTestBookingSettingsResp LoadBaseAutoTestBookingSettings(LoadBaseAutoTestBookingSettingsReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			FindPotentialBookingsReq findPotentialBookingsReq = autoTestBookingManager.LoadBaseAutoTestBookingSettings(Request.TestType, Request.TestBookingContext, Request.OptionalClockWorkSettingsInstanceName, Request.ClearCacheFirst);
			return new LoadBaseAutoTestBookingSettingsResp
			{
				FindPotentialBookingsRequest = ((findPotentialBookingsReq == null) ? null : findPotentialBookingsReq.ToDTO())
			};
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00019A24 File Offset: 0x00017C24
		public CalculateExtraTimeResp CalculateExtraTime(CalculateExtraTimeReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			int numExtraMinutes = autoTestBookingManager.CalculateExtraTime(Request.TestType, Request.ClassTestDurationInMinutes, Request.AccommodationsToUse.ToList<AccommodationDTO>().ConvertAll<Accommodation>((AccommodationDTO g) => g.ToDomainObject()));
			return new CalculateExtraTimeResp
			{
				NumExtraMinutes = numExtraMinutes
			};
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00019A94 File Offset: 0x00017C94
		public CalculateBreakTimeResp CalculateBreakTime(CalculateBreakTimeReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			int numExtraMinutes = autoTestBookingManager.CalculateBreakTime(Request.TestType, Request.ClassTestDurationInMinutes, Request.AccommodationsToUse.ToList<AccommodationDTO>().ConvertAll<Accommodation>((AccommodationDTO g) => g.ToDomainObject()));
			return new CalculateBreakTimeResp
			{
				NumExtraMinutes = numExtraMinutes
			};
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00019B04 File Offset: 0x00017D04
		public FindPotentialBookingsExplicitResp FindPotentialBookingsExplicit(FindPotentialBookingsExplicitReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			FindPotentialBookingsResp findPotentialBookingsResp = autoTestBookingManager.FindPotentialBookingsExplicit(Request.Request.ToDomainObject());
			return new FindPotentialBookingsExplicitResp
			{
				Result = ((findPotentialBookingsResp == null) ? null : findPotentialBookingsResp.ToDTO())
			};
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00019B4C File Offset: 0x00017D4C
		public void ClearAutoTestBookingCache(ClearAutoTestBookingCacheReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			autoTestBookingManager.ClearAutoTestBookingCache(Request.TestType, null, null, Request.OptionalClockWorkSettingsInstanceName);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00019B7C File Offset: 0x00017D7C
		public LoadAvailableAssetsResp LoadAvailableAssets(LoadAvailableAssetsReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			IList<Asset> list = autoTestBookingManager.LoadAvailableAssets(Request.TestType, null, null, Request.OptionalClockWorkSettingsInstanceName, Request.ClearCacheFirst);
			LoadAvailableAssetsResp loadAvailableAssetsResp = new LoadAvailableAssetsResp();
			IList<AssetDTO> assets;
			if (list != null)
			{
				assets = list.ToList<Asset>().ConvertAll<AssetDTO>((Asset g) => g.ToDTO());
			}
			else
			{
				assets = null;
			}
			loadAvailableAssetsResp.Assets = assets;
			return loadAvailableAssetsResp;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00019BF4 File Offset: 0x00017DF4
		public LoadSpecialAccommodationsResp LoadSpecialAccommodations(LoadSpecialAccommodationsReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			IList<SpecialAccommodation> list = autoTestBookingManager.LoadSpecialAccommodations(Request.TestType, null, null, Request.OptionalClockWorkSettingsInstanceName, Request.ClearCacheFirst);
			LoadSpecialAccommodationsResp loadSpecialAccommodationsResp = new LoadSpecialAccommodationsResp();
			IList<SpecialAccommodationDTO> specialAccommodations;
			if (list != null)
			{
				specialAccommodations = list.ToList<SpecialAccommodation>().ConvertAll<SpecialAccommodationDTO>((SpecialAccommodation g) => g.ToDTO());
			}
			else
			{
				specialAccommodations = null;
			}
			loadSpecialAccommodationsResp.SpecialAccommodations = specialAccommodations;
			return loadSpecialAccommodationsResp;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00019C6C File Offset: 0x00017E6C
		public LoadAvailableRoomsResp LoadAvailableRooms(LoadAvailableRoomsReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			IList<Room> list = autoTestBookingManager.LoadAvailableRooms(Request.TestType, null, null, null, Request.OptionalClockWorkSettingsInstanceName, Request.ClearCacheFirst);
			LoadAvailableRoomsResp loadAvailableRoomsResp = new LoadAvailableRoomsResp();
			IList<RoomDTO> rooms;
			if (list != null)
			{
				rooms = list.ToList<Room>().ConvertAll<RoomDTO>((Room g) => g.ToDTO());
			}
			else
			{
				rooms = null;
			}
			loadAvailableRoomsResp.Rooms = rooms;
			return loadAvailableRoomsResp;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00019CE4 File Offset: 0x00017EE4
		public LoadTestRulesResp LoadTestRules(LoadTestRulesReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			IList<TestRule> list = autoTestBookingManager.LoadTestRules(Request.TestType, null, null, Request.OptionalClockWorkSettingsInstanceName, Request.ClearCacheFirst);
			LoadTestRulesResp loadTestRulesResp = new LoadTestRulesResp();
			IList<TestRuleDTO> testRules;
			if (list != null)
			{
				testRules = list.ToList<TestRule>().ConvertAll<TestRuleDTO>((TestRule g) => g.ToDTO());
			}
			else
			{
				testRules = null;
			}
			loadTestRulesResp.TestRules = testRules;
			return loadTestRulesResp;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00019D5C File Offset: 0x00017F5C
		public FindPotentialBookings2Resp FindPotentialBookings(FindPotentialBookings2Req Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			List<Accommodation> accs = Request.AccommodationsToUse.ToList<AccommodationDTO>().ConvertAll<Accommodation>((AccommodationDTO g) => g.ToDomainObject());
			FindPotentialBookingsResp findPotentialBookingsResp = autoTestBookingManager.FindPotentialBookings(Request.TestType, Request.TestBookingContext, Request.PersonId, Request.LuCourseId, Request.ClassStartDateTime, Request.ClassEndDateTime, accs, Request.OptionalClockWorkSettingsInstanceName, Request.ClearCacheFirst, Request.DebugMode);
			return new FindPotentialBookings2Resp
			{
				Result = ((findPotentialBookingsResp == null) ? null : findPotentialBookingsResp.ToDTO())
			};
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00019E00 File Offset: 0x00018000
		public ApplySpecialAccommodations2Resp ApplySpecialAccommodations(ApplySpecialAccommodations2Req Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			List<Accommodation> accs = Request.AccommodationsToUse.ToList<AccommodationDTO>().ConvertAll<Accommodation>((AccommodationDTO g) => g.ToDomainObject());
			ApplySpecialAccommodationsResp applySpecialAccommodationsResp = autoTestBookingManager.ApplySpecialAccommodations(Request.DebugMode, Request.TestType, Request.TestBookingContext, Request.PersonId, Request.LuCourseId, Request.ClassStartDateTime, Request.ClassEndDateTime, accs, Request.OptionalClockWorkSettingsInstanceName, Request.ClearCacheFirst);
			return new ApplySpecialAccommodations2Resp
			{
				Result = ((applySpecialAccommodationsResp == null) ? null : applySpecialAccommodationsResp.ToDTO())
			};
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00019EA4 File Offset: 0x000180A4
		public TryToFindBookingResp TryToFindBooking(TryToFindBookingReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			IAutoTestBookingManager autoTestBookingManager2 = autoTestBookingManager;
			eTestExamSettingType testType = Request.TestType;
			bool staffIsBooking = Request.StaffIsBooking;
			int personId = Request.PersonId;
			int luCourseId = Request.LuCourseId;
			DateTime classStartDateTime = Request.ClassStartDateTime;
			int classTestDurationInMinutes = Request.ClassTestDurationInMinutes;
			IList<int> accommodationsToUse = Request.AccommodationsToUse;
			bool ignoreSpecialAccommodations = Request.IgnoreSpecialAccommodations;
			int bookingAlreadyExistsAppointmentId = Request.BookingAlreadyExistsAppointmentId;
			IList<TryToBookAccommodationToUseDTO> additionalAccommodationsToUse = Request.AdditionalAccommodationsToUse;
			IList<TryToBookAccommodationToUse> additionalAccommodationsToUse2;
			if (additionalAccommodationsToUse == null)
			{
				additionalAccommodationsToUse2 = null;
			}
			else
			{
				additionalAccommodationsToUse2 = (from g in additionalAccommodationsToUse
				select g.ToDomainObject()).ToList<TryToBookAccommodationToUse>();
			}
			TryToBookResult tryToBookResult = autoTestBookingManager2.TryToFindBooking(testType, staffIsBooking, personId, luCourseId, classStartDateTime, classTestDurationInMinutes, accommodationsToUse, ignoreSpecialAccommodations, bookingAlreadyExistsAppointmentId, additionalAccommodationsToUse2, Request.ClearCacheFirst, Request.ClockWorkInstanceNameToUse);
			return new TryToFindBookingResp
			{
				FindBookingResult = ((tryToBookResult != null) ? tryToBookResult.ToDTO() : null)
			};
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00019F5C File Offset: 0x0001815C
		public AutoBookTestOrExamResp AutoBookTestOrExam(AutoBookTestOrExamReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			AutoBookTestExamResult autoBookTestExamResult = autoTestBookingManager.AutoBookTestOrExam(Request.ExamId, Request.TestType, Request.TestBookingContext, Request.Pid, Request.Lucid, Request.ClassStartDateTime, Request.ClassEndDateTime, Request.ClearCacheFirst);
			return new AutoBookTestOrExamResp
			{
				AutoBookTestExamResult = ((autoBookTestExamResult != null) ? autoBookTestExamResult.ToDTO() : null)
			};
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00019FCC File Offset: 0x000181CC
		public AutoBookTestOrExamPreviewResp AutoBookTestOrExamPreview(AutoBookTestOrExamPreviewReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult = autoTestBookingManager.AutoBookTestOrExamPreview(Request.TestType, Request.TestBookingContext, Request.Pid, Request.Lucid, Request.ClassStartDateTime, Request.ClassEndDateTime, Request.ClearCacheFirst);
			return new AutoBookTestOrExamPreviewResp
			{
				AutoBookTestExamPreviewResult = ((autoBookTestExamPreviewResult != null) ? autoBookTestExamPreviewResult.ToDTO() : null)
			};
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001A034 File Offset: 0x00018234
		public AutoRescheduleTestOrExamResp AutoRescheduleTestOrExam(AutoRescheduleTestOrExamReq Request)
		{
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(Request.GetOperationContext());
			AutoRescheduleTestExamResult autoRescheduleTestExamResult = autoTestBookingManager.AutoRescheduleTestOrExam(Request.AppointmentId);
			return new AutoRescheduleTestOrExamResp
			{
				AutoRescheduleTestExamPreviewResult = ((autoRescheduleTestExamResult != null) ? autoRescheduleTestExamResult.ToDTO() : null)
			};
		}
	}
}
