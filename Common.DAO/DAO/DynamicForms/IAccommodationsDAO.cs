using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Entity.Accommodations;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.Common.DAO.DynamicForms
{
	// Token: 0x02000085 RID: 133
	public interface IAccommodationsDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600036C RID: 876
		List<DynamicDataChange> LoadAccommodationChanges(int PersonId, int LuCourseId, DateTime SinceDate);

		// Token: 0x0600036D RID: 877
		DateTime? GetStudentAccommodationsExpiryDate(int PersonId, int AccommodationsExpiryDateControlId);

		// Token: 0x0600036E RID: 878
		IDictionary<int, DateTime?> LoadAccommodationExpiryDatesForStudents(int[] pids, int expiryDateCid);

		// Token: 0x0600036F RID: 879
		Task<IDictionary<int, DateTime?>> LoadAccommodationExpiryDatesForStudentsAsync(int[] pids, int expiryDateCid);

		// Token: 0x06000370 RID: 880
		IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodations(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations);

		// Token: 0x06000371 RID: 881
		IList<AccommodationData> LoadAccommodationsByStudentAndCourseOrTemplate(int PersonId, int LuCourseId, out bool IsUsingTemplateAccommodations);

		// Token: 0x06000372 RID: 882
		void ClearAccommodations(int PersonId, int CourseId, bool RequiresApproval);

		// Token: 0x06000373 RID: 883
		IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(int PersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations);

		// Token: 0x06000374 RID: 884
		void MarkAccommodationLetterIssued(int PersonId, params int[] LuCourseIds);

		// Token: 0x06000375 RID: 885
		void MergeAccommodations(int SourcePersonId, int SourceLuCourseId, int DestPersonId, int DestLuCourseId, IList<int> ControlIdsToIgnore);

		// Token: 0x06000376 RID: 886
		void ReplaceAccommodations(int SourcePersonId, int SourceLuCourseId, int DestPersonId, int DestLuCourseId, IList<int> ControlIdsToIgnore);

		// Token: 0x06000377 RID: 887
		IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodationsByCourse(int PersonId, int LuCourseId, bool LoadAccommodations);

		// Token: 0x06000378 RID: 888
		IList<CourseRegistrationWithAccommodations> LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsByCourse(int PersonId, int LuCourseId, bool LoadAccommodations);

		// Token: 0x06000379 RID: 889
		IList<DynamicDataSetWithStudentName> LoadActiveStudentsWithTemplateAccommodations(DateTime StartDate, DateTime EndDate);

		// Token: 0x0600037A RID: 890
		IList<CourseRegistrationWithAccommodations> LoadStudentsAccommodationsAndRequestsForOfflineCourse(int PersonId, bool LoadAccommodations);

		// Token: 0x0600037B RID: 891
		string GetAccommodationsListString(List<AccommodationData> alist, string mailMergeCode, AccommodationListFormattingInfoDAO formattingInfo, TempCache tempCache = null, string listCounterName = null);

		// Token: 0x0600037C RID: 892
		IList<int> LoadCoursesStudentHasAtLeastOneAccommodationCheckedIn(int PersonId, int[] cids, int[] lucids);
	}
}
