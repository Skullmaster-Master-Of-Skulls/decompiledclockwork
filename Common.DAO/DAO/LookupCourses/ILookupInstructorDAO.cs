using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.DAO.LookupCourses
{
	// Token: 0x02000058 RID: 88
	public interface ILookupInstructorDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001FF RID: 511
		LookupInstructor LoadInstructor(int InstructorId);

		// Token: 0x06000200 RID: 512
		void SaveInstructor(LookupInstructor instructor);

		// Token: 0x06000201 RID: 513
		void SaveInstructorsForCourse(int LuCourseId, List<LookupInstructor> Instructors, bool updateInstructorInfo);

		// Token: 0x06000202 RID: 514
		LookupInstructor LoadInstructorByUsername(string username);

		// Token: 0x06000203 RID: 515
		LookupInstructor LoadInstructorByEmail(string email);

		// Token: 0x06000204 RID: 516
		LookupInstructor LoadInstructorByEmployeeId(string employeeId);

		// Token: 0x06000205 RID: 517
		List<LookupCourse> LoadInstructorCourses(int InstructorId, int AltContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate, bool EachCourseMustHaveAtLeastOneRegisteredStudent);

		// Token: 0x06000206 RID: 518
		IList<int> LoadInstructorOrAltContactAssignedLuCourseIds(int InstructorId, int AlternateContactId, bool MustHaveClassTestDefinition, bool MustHaveOneRegisteredStudent);

		// Token: 0x06000207 RID: 519
		List<LookupInstructor> LoadAllAssignedInstructors();

		// Token: 0x06000208 RID: 520
		IList<LookupInstructor> LoadInstructorsBySearchString(string SearchString);

		// Token: 0x06000209 RID: 521
		void AssignInstructorToCourse(int InstructorId, int LuCourseId, bool? IsAssignmentExemptFromDataSync);

		// Token: 0x0600020A RID: 522
		void RemoveInstructorFromCourse(int InstructorId, int LuCourseId);

		// Token: 0x0600020B RID: 523
		IList<LookupInstructor> LoadInstructorsByCourse(int LuCourseId);

		// Token: 0x0600020C RID: 524
		void UpdateInstructorDataSyncExemption(int InstructorId, bool NewInstructorExemptStatus);

		// Token: 0x0600020D RID: 525
		IList<DateTime> GetUniqueCourseRegistrationStartDatesByInstructor(int InstructorId);

		// Token: 0x0600020E RID: 526
		IList<StudentWithRequestAndCourseInfo> GetStudentsWithApprovedRequestsByCourseDate(int InstructorId, int AlternateContactId, DateTime StartDate, DateTime EndDate, int ShowIfActiveAccommodationsExpiry_AccExpiryCid, bool ShowIfLetterGenerated, bool TreatEmptyExpiredDatesAsExpired, bool showifrequestapprovedandaccommsnotexpired);

		// Token: 0x0600020F RID: 527
		int[] FindAllCoursesAnInstructorOrAltContactIsAllowed(int instructorId, int altContactId, int permissionLevel);
	}
}
