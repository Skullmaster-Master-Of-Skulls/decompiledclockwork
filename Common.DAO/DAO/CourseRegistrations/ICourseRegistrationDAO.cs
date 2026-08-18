using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.CourseRegistrations
{
	// Token: 0x02000078 RID: 120
	public interface ICourseRegistrationDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002F2 RID: 754
		List<CourseRegistration> LoadStudentsCourses(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses);

		// Token: 0x060002F3 RID: 755
		List<CourseRegistrationWithStudentSpecificInfo> LoadStudentsCoursesWithStudentSpecificInfo(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses);

		// Token: 0x060002F4 RID: 756
		void ChangeCourseRegistrationStatus(int CoursesId, eRegistrationStatus NewStatus);

		// Token: 0x060002F5 RID: 757
		CourseRegistration RegisterStudentInCourse(int StudentPid, int Lucid, bool? ExemptCourseFromDataSyncForStudent);

		// Token: 0x060002F6 RID: 758
		CourseRegistration RegisterStudentInCourse(int StudentPid, int Lucid);

		// Token: 0x060002F7 RID: 759
		CourseRegistration LoadCourseRegistration(int StudentPid, int Lucid);

		// Token: 0x060002F8 RID: 760
		void DeleteCourseRegistration(int CoursesId);

		// Token: 0x060002F9 RID: 761
		IList<DateTime> GetUniqueCourseRegistrationStartDatesByStudent(int PersonId);

		// Token: 0x060002FA RID: 762
		void MergeCourseRegistrations(int PersonIdNew, int PersonIdOld);

		// Token: 0x060002FB RID: 763
		void SetDateLetterIssued(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x060002FC RID: 764
		void SetDateLetterReturned(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x060002FD RID: 765
		void SetProfLastViewedLetter(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x060002FE RID: 766
		void SetStudentLastViewedLetter(int PersonId, int LuCourseId, DateTime? Date);

		// Token: 0x060002FF RID: 767
		void SetDateLetterIssued(int CoursesId, DateTime? Date);

		// Token: 0x06000300 RID: 768
		void SetDateLetterReturned(int CoursesId, DateTime? Date);

		// Token: 0x06000301 RID: 769
		void SetProfLastViewedLetter(int CoursesId, DateTime? Date);

		// Token: 0x06000302 RID: 770
		void SetStudentLastViewedLetter(int CoursesId, DateTime? Date);

		// Token: 0x06000303 RID: 771
		IList<CourseRegistration> LoadAllStudentsWithCoursesByDate(DateTime StartDate, DateTime EndDate, bool IncludeDroppedCourses);

		// Token: 0x06000304 RID: 772
		CourseRegistration LoadCourseRegistrationsByStudentAndCourse(int StudentPid, int Lucid);

		// Token: 0x06000305 RID: 773
		IList<CourseRegistration> LoadCourseRegistrationsByCourse(int LuCourseId);

		// Token: 0x06000306 RID: 774
		IList<PersonBase> LoadStudentsWithActiveRegisteredCoursesAndActiveAccommodations(DateTime StartDate, DateTime EndDate, int AccommodationsExpiryDateControlId);

		// Token: 0x06000307 RID: 775
		IList<CourseRegistration> LoadStudentsCoursesBatch(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, bool IncludeDroppedCourses);

		// Token: 0x06000308 RID: 776
		IList<CourseRegistration> LoadActiveStudentsWithCourses(DateTime StartDate, DateTime EndDate, bool IncludeDroppedCourses = false);

		// Token: 0x06000309 RID: 777
		void UpdateCourseRegistrationSpecificInfoNonEmptyFieldsOnly(int CoursesId, DataSyncExternalCourseStudentSpecific courseStudentSpecificInfo);

		// Token: 0x0600030A RID: 778
		T RegisterStudentInCourse0<T>(int StudentPid, int Lucid, bool? ExemptCourseFromDataSyncForStudent) where T : CourseRegistration;

		// Token: 0x0600030B RID: 779
		int[] LoadStudentCourseRegistrationLuCourseIds(int studentPersonId, bool includeDroppedCourses);

		// Token: 0x0600030C RID: 780
		IList<StudentWithCourseAndAccommodationInfo> LoadStudentsWithCourseAndAccommodationInfosByCourseIds(int accommExpiryCid, int noInstructorViewCid, params int[] lucids);
	}
}
