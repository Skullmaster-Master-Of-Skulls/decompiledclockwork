using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.CourseRegistrations
{
	// Token: 0x020000AF RID: 175
	public interface ICourseRegistrationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000528 RID: 1320
		List<CourseRegistration> LoadStudentsCourses(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses);

		// Token: 0x06000529 RID: 1321
		void ChangeCourseRegistrationStatus(int CoursesId, eRegistrationStatus NewStatus);

		// Token: 0x0600052A RID: 1322
		CourseRegistration RegisterStudentInCourse(int StudentPid, int Lucid, bool? ExemptCourseFromDataSyncForStudent);

		// Token: 0x0600052B RID: 1323
		CourseRegistration RegisterStudentInCourse(int StudentPid, int Lucid);

		// Token: 0x0600052C RID: 1324
		void DeleteCourseRegistration(int CoursesId);

		// Token: 0x0600052D RID: 1325
		IList<DateTime> GetUniqueCourseRegistrationStartDatesByStudent(int PersonId);

		// Token: 0x0600052E RID: 1326
		void MergeCourseRegistrations(int PersonIdNew, int PersonIdOld);

		// Token: 0x0600052F RID: 1327
		IList<CourseRegistration> LoadCourseRegistrationsByCourse(int LuCourseId);

		// Token: 0x06000530 RID: 1328
		void SetDateLetterIssued(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x06000531 RID: 1329
		void SetDateLetterReturned(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x06000532 RID: 1330
		void SetProfLastViewedLetter(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x06000533 RID: 1331
		void SetStudentLastViewedLetter(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x06000534 RID: 1332
		void SetProfLastViewedLetters(int PersonId, IList<int> LuCourseIds, DateTime? Date);

		// Token: 0x06000535 RID: 1333
		void SetStudentLastViewedLetters(int PersonId, IList<int> LuCourseIds, DateTime? Date);

		// Token: 0x06000536 RID: 1334
		void SetDateLetterIssued(int CoursesId, DateTime? Date);

		// Token: 0x06000537 RID: 1335
		void SetDateLetterReturned(int CoursesId, DateTime? Date);

		// Token: 0x06000538 RID: 1336
		void SetProfLastViewedLetter(int CoursesId, DateTime? Date);

		// Token: 0x06000539 RID: 1337
		void SetStudentLastViewedLetter(int CoursesId, DateTime? Date);

		// Token: 0x0600053A RID: 1338
		StudentCourseList LoadCoursesStudentIsAllowedToBookTestsForNow(int StudentPersonId);

		// Token: 0x0600053B RID: 1339
		StudentCourseList LoadCoursesStudentIsAllowedToBookFinalExamsForNow(int StudentPersonId);

		// Token: 0x0600053C RID: 1340
		CourseRegistration LoadCourseRegistrationsByStudentAndCourse(int StudentPid, int Lucid);

		// Token: 0x0600053D RID: 1341
		IList<PersonBase> LoadStudentsWithActiveRegisteredCoursesAndActiveAccommodations(DateTime StartDate, DateTime EndDate);

		// Token: 0x0600053E RID: 1342
		IList<CourseRegistration> LoadStudentsCoursesBatch(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, bool IncludeDroppedCourses);

		// Token: 0x0600053F RID: 1343
		IList<CourseRegistration> LoadActiveStudentsWithCourses(DateTime StartDate, DateTime EndDate, bool IncludeDroppedCourses = false);

		// Token: 0x06000540 RID: 1344
		bool IsInstructorOrAltContactTeachingStudentsCourse(int StudentPersonId, int LuCourseId, int InstructorId, int AlternateContactId);

		// Token: 0x06000541 RID: 1345
		int[] LoadStudentCourseRegistrationLuCourseIds(int studentPersonId, bool includeDroppedCourses);

		// Token: 0x06000542 RID: 1346
		IList<StudentWithCourseAndAccommodationInfo> LoadStudentsWithCourseAndAccommodationInfosByCourseIds(params int[] lucids);

		// Token: 0x06000543 RID: 1347
		IList<CourseRegistrationWithStudentSpecificInfo> LoadStudentsCoursesWithStudentSpecificInfos(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses);
	}
}
