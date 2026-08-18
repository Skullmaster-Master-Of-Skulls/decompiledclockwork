using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.DynamicForms
{
	// Token: 0x0200005B RID: 91
	public interface IAccommodationsClientManager : IWebService
	{
		// Token: 0x060002AA RID: 682
		IList<AccommodationDataDTO> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int LuCourseId);

		// Token: 0x060002AB RID: 683
		IList<AccommodationDataDTO> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int LuCourseId, out bool IsUsingTemplateAccommodations);

		// Token: 0x060002AC RID: 684
		void MergeOrReplaceAccommodations(bool ReplaceExistingAccommodations, int SourcePersonId, int SourceLuCourseId, int DestPersonId, int DestLuCourseId);

		// Token: 0x060002AD RID: 685
		void ClearAccommodations(int PersonId, int CourseId, bool RequiresApproval);

		// Token: 0x060002AE RID: 686
		IList<CourseRegistrationWithAccommodationsDTO> LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations, bool IncludeOfflineAccommodations = false);

		// Token: 0x060002AF RID: 687
		void MarkAccommodationLetterIssued(int PersonId, params int[] LuCourseIds);

		// Token: 0x060002B0 RID: 688
		DateTime? GetStudentAccommodationsExpiryDate(int PersonId);
	}
}
