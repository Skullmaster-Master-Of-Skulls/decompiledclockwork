using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.LookupCourses
{
	// Token: 0x0200003B RID: 59
	public interface ILookupInstructorClientManager : IWebService
	{
		// Token: 0x060001AF RID: 431
		LookupInstructorDTO LoadInstructor(int InstructorId);

		// Token: 0x060001B0 RID: 432
		int SaveInstructor(LookupInstructorDTO instructor);

		// Token: 0x060001B1 RID: 433
		IList<LookupInstructorDTO> SaveInstructorsForCourse(int LuCourseId, List<LookupInstructorDTO> Instructors, bool UpdateInstructorInfo);

		// Token: 0x060001B2 RID: 434
		LookupInstructorDTO LoadInstructorByUsername(string username);

		// Token: 0x060001B3 RID: 435
		LookupInstructorDTO LoadInstructorByEmail(string email);

		// Token: 0x060001B4 RID: 436
		LookupInstructorDTO LoadInstructorByEmployeeId(string employeeId);

		// Token: 0x060001B5 RID: 437
		IList<LookupCourseDTO> LoadInstructorCourses(int InstructorId, int AlternateContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate);

		// Token: 0x060001B6 RID: 438
		IList<LookupCourseDTO> LoadInstructorCoursesWithAtLeastOneStudentRegistered(int InstructorId, int AlternateContactId, int PermissionLevel, bool MustHaveClassTestDefinition, DateTime StartDate, DateTime EndDate);

		// Token: 0x060001B7 RID: 439
		IList<LookupInstructorDTO> LoadAllAssignedInstructors();

		// Token: 0x060001B8 RID: 440
		IList<LookupInstructorDTO> LoadInstructorsBySearchString(string SearchString);

		// Token: 0x060001B9 RID: 441
		void AssignInstructorToCourse(int InstructorId, int LuCourseId, bool? IsAssignmentExemptFromDataSync);

		// Token: 0x060001BA RID: 442
		void RemoveInstructorFromCourse(int InstructorId, int LuCourseId);

		// Token: 0x060001BB RID: 443
		IList<LookupInstructorDTO> LoadInstructorsByCourse(int LuCourseId);

		// Token: 0x060001BC RID: 444
		void UpdateInstructorDataSyncExemption(int InstructorId, bool NewInstructorExemptStatus);

		// Token: 0x060001BD RID: 445
		IList<DateTime> GetUniqueCourseRegistrationStartDatesByInstructor(int InstructorId);

		// Token: 0x060001BE RID: 446
		IList<LookupCourseDTO> LoadCoursesByInstructor(int InstructorId, int AlternateContactId, DateTime StartDate, DateTime EndDate, int PermissionLevel);

		// Token: 0x060001BF RID: 447
		IList<StudentWithRequestAndCourseInfoDTO> GetStudentsWithApprovedRequestsByCourseDate(int InstructorId, int AlternateContactId, DateTime StartDate, DateTime EndDate);

		// Token: 0x060001C0 RID: 448
		IList<StudentWithCourseAndAccommodationInfoDTO> LoadStudentsWithCourseAndAccommodationInfosByCourses(int instructorId, int altContactId, params int[] lucids);
	}
}
