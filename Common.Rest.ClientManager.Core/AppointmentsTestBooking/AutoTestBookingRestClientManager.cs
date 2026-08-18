using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000074 RID: 116
	public class AutoTestBookingRestClientManager : BearerTokenRestProxy<IAutoTestBookingClientManager>, IAutoTestBookingClientManager, IWebService
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x0000CBD6 File Offset: 0x0000ADD6
		public AutoTestBookingRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		public AutoTestBookingRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000CBEB File Offset: 0x0000ADEB
		public FindPotentialBookingsReqDTO LoadBaseAutoTestBookingSettings(eTestExamSettingType TestType, eAutoTestBookingContext TestBookingContext, string OptionalClockWorkSettingsInstanceName, bool ClearCacheFirst)
		{
			return base.Get<FindPotentialBookingsReqDTO>(string.Format("autotestbooking/baseautotestbookingsettings/testtype/{0}/context/{1}?settingsinstancename={2}&clearcache={3}", new object[]
			{
				TestType,
				TestBookingContext,
				OptionalClockWorkSettingsInstanceName,
				ClearCacheFirst
			}), true);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000CC24 File Offset: 0x0000AE24
		public FindPotentialBookingsRespDTO FindPotentialBookingsExplicit(FindPotentialBookingsReqDTO req)
		{
			return base.Post<FindPotentialBookingsReqDTO, FindPotentialBookingsRespDTO>(req, "autotestbooking/findpotentialbookingsexplicit");
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000CC34 File Offset: 0x0000AE34
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
			return base.Post<FindPotentialBookings2Req, FindPotentialBookingsRespDTO>(findPotentialBookings2Req, "autotestbooking/findpotentialbookings");
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
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
			return base.Post<ApplySpecialAccommodations2Req, ApplySpecialAccommodationsRespDTO>(applySpecialAccommodations2Req, "autotestbooking/applyspecialaccommodations");
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000CD18 File Offset: 0x0000AF18
		public int CalculateExtraTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<AccommodationDTO> AccommodationsToUse)
		{
			CalculateExtraTimeReq calculateExtraTimeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CalculateExtraTimeReq>();
			calculateExtraTimeReq.TestType = TestType;
			calculateExtraTimeReq.ClassTestDurationInMinutes = ClassTestDurationInMinutes;
			calculateExtraTimeReq.AccommodationsToUse = AccommodationsToUse;
			return base.Post<CalculateExtraTimeReq, int>(calculateExtraTimeReq, "autotestbooking/calculateextratime");
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000CD54 File Offset: 0x0000AF54
		public int CalculateBreakTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<AccommodationDTO> AccommodationsToUse)
		{
			CalculateBreakTimeReq calculateBreakTimeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CalculateBreakTimeReq>();
			calculateBreakTimeReq.TestType = TestType;
			calculateBreakTimeReq.ClassTestDurationInMinutes = ClassTestDurationInMinutes;
			calculateBreakTimeReq.AccommodationsToUse = AccommodationsToUse;
			return base.Post<CalculateBreakTimeReq, int>(calculateBreakTimeReq, "autotestbooking/calculatebreaktime");
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000CD90 File Offset: 0x0000AF90
		public void ClearAutoTestBookingCache(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName)
		{
			ClearAutoTestBookingCacheReq clearAutoTestBookingCacheReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearAutoTestBookingCacheReq>();
			clearAutoTestBookingCacheReq.TestType = TestType;
			clearAutoTestBookingCacheReq.OptionalClockWorkSettingsInstanceName = ClockWorkSettingsInstanceName;
			base.Post<ClearAutoTestBookingCacheReq>(clearAutoTestBookingCacheReq, "autotestbooking/clearautotestbookingcache");
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000CDC2 File Offset: 0x0000AFC2
		public IList<AssetDTO> LoadAvailableAssets(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			return base.GetMany<AssetDTO>(string.Format("autotestbooking/availableassets/testtype/{0}?clockworksettingsinstancename={1}&clearcache={2}", TestType, ClockWorkSettingsInstanceName, clearCacheFirst), true);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000CDE2 File Offset: 0x0000AFE2
		public IList<SpecialAccommodationDTO> LoadSpecialAccommodations(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			return base.GetMany<SpecialAccommodationDTO>(string.Format("autotestbooking/specialaccommodations/testtype/{0}?clockworksettingsinstancename={1}&clearcache={2}", TestType, ClockWorkSettingsInstanceName, clearCacheFirst), true);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000CE02 File Offset: 0x0000B002
		public IList<RoomDTO> LoadAvailableRooms(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			return base.GetMany<RoomDTO>(string.Format("autotestbooking/availablerooms/testtype/{0}?clockworksettingsinstancename={1}&clearcache={2}", TestType, ClockWorkSettingsInstanceName, clearCacheFirst), true);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000CE22 File Offset: 0x0000B022
		public IList<TestRuleDTO> LoadTestRules(eTestExamSettingType TestType, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			return base.GetMany<TestRuleDTO>(string.Format("autotestbooking/testrules/testtype/{0}?clockworksettingsinstancename={1}&clearcache={2}", TestType, ClockWorkSettingsInstanceName, clearCacheFirst), true);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000CE44 File Offset: 0x0000B044
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
			return base.Post<TryToFindBookingReq, TryToBookResultDTO>(tryToFindBookingReq, "autotestbooking/trytofindbooking");
		}
	}
}
