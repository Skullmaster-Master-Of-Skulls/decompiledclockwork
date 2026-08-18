using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.CourseRegistrations
{
	// Token: 0x0200006C RID: 108
	public interface ICourseRegistrationClientManager : IWebService
	{
		// Token: 0x0600032B RID: 811
		StudentCourseListDTO LoadCoursesStudentIsAllowedToBookTestsForNow(int StudentPersonId);

		// Token: 0x0600032C RID: 812
		StudentCourseListDTO LoadCoursesStudentIsAllowedToBookFinalExamsForNow(int StudentPersonId);

		// Token: 0x0600032D RID: 813
		void ChangeCourseRegistrationStatus(int CoursesId, eRegistrationStatusDTO NewStatus);

		// Token: 0x0600032E RID: 814
		IList<CourseRegistrationDTO> LoadStudentsCourses(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses);

		// Token: 0x0600032F RID: 815
		CourseRegistrationDTO RegisterStudentInCourse(int StudentPid, int Lucid, bool? IsCourseExemptFromDataSyncForStudent);

		// Token: 0x06000330 RID: 816
		void DeleteCourseRegistration(int CoursesId);

		// Token: 0x06000331 RID: 817
		IList<DateTime> GetUniqueCourseRegistrationStartDatesByStudent(int PersonId);

		// Token: 0x06000332 RID: 818
		void SetDateLetterIssuedByCourses(int CoursesId, DateTime? Date);

		// Token: 0x06000333 RID: 819
		void SetDateLetterIssuedByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x06000334 RID: 820
		void SetDateLetterReturnedByCourses(int CoursesId, DateTime? Date);

		// Token: 0x06000335 RID: 821
		void SetDateLetterReturnedByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x06000336 RID: 822
		void SetProfLastViewedLetterByCourses(int CoursesId, DateTime? Date);

		// Token: 0x06000337 RID: 823
		void SetProfLastViewedLetterByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x06000338 RID: 824
		void SetStudentLastViewedLetterByCourses(int CoursesId, DateTime? Date);

		// Token: 0x06000339 RID: 825
		void SetStudentLastViewedLetterByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x0600033A RID: 826
		CourseRegistrationDTO LoadCourseRegistrationsByStudentAndCourse(int StudentPid, int Lucid);

		// Token: 0x0600033B RID: 827
		bool IsInstructorOrAltContactTeachingStudentsCourse(int StudentPersonId, int LuCourseId, int InstructorId, int AlternateContactId);

		// Token: 0x0600033C RID: 828
		IList<CourseRegistrationWithStudentSpecificInfoDTO> LoadStudentsCoursesWithStudentSpecificInfos(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses);
	}
}
