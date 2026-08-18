using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing;

namespace TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests
{
	// Token: 0x02000014 RID: 20
	public interface ISelfRegClientManager : IWebService
	{
		// Token: 0x0600007E RID: 126
		void ProcessSelfRegRequest(int studentPersonId, eSelfRegCoursesAccommodationsStatus studentIndicatedCoursesAccommodationsStatus, IList<SelfRegCourseInfoDTO> luCourseIdsToApplyTo, List<SelfRegCheckedAccommodationDTO> checkedAccommodations, IList<AccommodationDataDTO> hidingAccommodations, string noteFromStudent, string baseUrl, string studentPersonIdEncodedForUrl, string ipAddressForLoggin);

		// Token: 0x0600007F RID: 127
		AllowedStudentCourseRegistrationsForCustomEmailLogicDTO GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(int studentPersonId);

		// Token: 0x06000080 RID: 128
		AllowedStudentCourseRegistrationsForCustomEmailLogicDTO GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaFor(string hash, string hashPlainText);
	}
}
