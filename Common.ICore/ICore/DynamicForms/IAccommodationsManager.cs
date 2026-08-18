using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x02000094 RID: 148
	public interface IAccommodationsManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000420 RID: 1056
		List<DynamicDataChange> LoadAccommodationChanges(int PersonId, int LuCourseId, DateTime SinceDate);

		// Token: 0x06000421 RID: 1057
		DateTime? GetStudentAccommodationsExpiryDate(int PersonId);

		// Token: 0x06000422 RID: 1058
		IDictionary<int, DateTime?> LoadAccommodationExpiryDatesForStudents(int[] pids);

		// Token: 0x06000423 RID: 1059
		Task<IDictionary<int, DateTime?>> LoadAccommodationExpiryDatesForStudentsAsync(int[] pids);

		// Token: 0x06000424 RID: 1060
		IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodations(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations, bool IncludeOfflineAccommodations = false);

		// Token: 0x06000425 RID: 1061
		IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations, bool IncludeOfflineAccommodations = false);

		// Token: 0x06000426 RID: 1062
		IList<AccommodationData> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int CourseId);

		// Token: 0x06000427 RID: 1063
		IList<AccommodationData> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int CourseId, out bool IsUsingTemplateAccommodations);

		// Token: 0x06000428 RID: 1064
		void ClearAccommodations(int PersonId, int CourseId, bool RequiresApproval);

		// Token: 0x06000429 RID: 1065
		void MarkAccommodationLetterIssued(int PersonId, params int[] LuCourseIds);

		// Token: 0x0600042A RID: 1066
		void MergeOrReplaceAccommodations(bool ReplaceExistingAccommodations, int SourcePersonId, int SourceLuCourseId, int DestPersonId, int DestLuCourseId);

		// Token: 0x0600042B RID: 1067
		IList<DynamicDataSetWithStudentName> LoadActiveStudentsWithTemplateAccommodations(DateTime StartDate, DateTime EndDate);

		// Token: 0x0600042C RID: 1068
		IList<int> LoadCoursesStudentHasAtLeastOneAccommodationCheckedIn(int PersonId, int[] cids, int[] lucids);
	}
}
