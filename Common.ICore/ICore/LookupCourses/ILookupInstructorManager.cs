using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.ICore.LookupCourses
{
	// Token: 0x0200006D RID: 109
	public interface ILookupInstructorManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000310 RID: 784
		LookupInstructor LoadInstructor(int InstructorId);

		// Token: 0x06000311 RID: 785
		int SaveInstructor(LookupInstructor instructor);

		// Token: 0x06000312 RID: 786
		List<LookupInstructor> SaveInstructorsForCourse(int LuCourseId, List<LookupInstructor> Instructors, bool UpdateInstructorInfo);

		// Token: 0x06000313 RID: 787
		LookupInstructor LoadInstructorByUsername(string username);

		// Token: 0x06000314 RID: 788
		LookupInstructor LoadInstructorByEmail(string email);

		// Token: 0x06000315 RID: 789
		LookupInstructor LoadInstructorByEmployeeId(string employeeId);

		// Token: 0x06000316 RID: 790
		List<LookupCourse> LoadInstructorCourses(int InstructorId, int AlternateContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000317 RID: 791
		IList<int> LoadInstructorOrAltContactAssignedLuCourseIds(int InstructorId, int AlternateContactId, bool MustHaveClassTestDefinition, bool MustHaveOneRegisteredStudent);

		// Token: 0x06000318 RID: 792
		IList<LookupCourse> LoadInstructorCoursesWithAtLeastOneStudentRegistered(int InstructorId, int AlternateContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000319 RID: 793
		List<LookupInstructor> LoadAllAssignedInstructors();

		// Token: 0x0600031A RID: 794
		IList<LookupInstructor> LoadInstructorsBySearchString(string SearchString);

		// Token: 0x0600031B RID: 795
		void AssignInstructorToCourse(int InstructorId, int LuCourseId, bool? IsAssignmentExemptFromDataSync);

		// Token: 0x0600031C RID: 796
		void RemoveInstructorFromCourse(int InstructorId, int LuCourseId);

		// Token: 0x0600031D RID: 797
		IList<LookupInstructor> LoadInstructorsByCourse(int LuCourseId);

		// Token: 0x0600031E RID: 798
		void UpdateInstructorDataSyncExemption(int InstructorId, bool NewInstructorExemptStatus);

		// Token: 0x0600031F RID: 799
		IList<DateTime> GetUniqueCourseRegistrationStartDatesByInstructor(int InstructorId);

		// Token: 0x06000320 RID: 800
		IList<StudentWithRequestAndCourseInfo> GetStudentsWithApprovedRequestsByCourseDate(int InstructorId, int AlternateContactId, DateTime StartDate, DateTime EndDate, string ClockWorkSettingsInstanceName);

		// Token: 0x06000321 RID: 801
		int[] FindAllCoursesAnInstructorOrAltContactIsAllowed(int instructorId, int altContactId, int permissionLevel);
	}
}
