using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.CourseRegistrations
{
	// Token: 0x02000073 RID: 115
	public class CourseRegistrationClientManager : ICourseRegistrationClientManager, IWebService
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x00012B3C File Offset: 0x00010D3C
		public StudentCourseListDTO LoadCoursesStudentIsAllowedToBookTestsForNow(int StudentPersonId)
		{
			LoadCoursesStudentIsAllowedToBookTestsForNowReq loadCoursesStudentIsAllowedToBookTestsForNowReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCoursesStudentIsAllowedToBookTestsForNowReq>();
			loadCoursesStudentIsAllowedToBookTestsForNowReq.StudentPersonId = StudentPersonId;
			LoadCoursesStudentIsAllowedToBookTestsForNowResp loadCoursesStudentIsAllowedToBookTestsForNowResp = ClientServiceFactory.GetClientInstance<ICourseRegistration>().LoadCoursesStudentIsAllowedToBookTestsForNow(loadCoursesStudentIsAllowedToBookTestsForNowReq);
			return (loadCoursesStudentIsAllowedToBookTestsForNowResp != null) ? loadCoursesStudentIsAllowedToBookTestsForNowResp.CourseList : null;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00012B78 File Offset: 0x00010D78
		public StudentCourseListDTO LoadCoursesStudentIsAllowedToBookFinalExamsForNow(int StudentPersonId)
		{
			LoadCoursesStudentIsAllowedToBookFinalExamsForNowReq loadCoursesStudentIsAllowedToBookFinalExamsForNowReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCoursesStudentIsAllowedToBookFinalExamsForNowReq>();
			loadCoursesStudentIsAllowedToBookFinalExamsForNowReq.StudentPersonId = StudentPersonId;
			return ClientServiceFactory.GetClientInstance<ICourseRegistration>().LoadCoursesStudentIsAllowedToBookFinalExamsForNow(loadCoursesStudentIsAllowedToBookFinalExamsForNowReq).CourseList;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00012BB0 File Offset: 0x00010DB0
		public void ChangeCourseRegistrationStatus(int CoursesId, eRegistrationStatusDTO NewStatus)
		{
			ChangeCourseRegistrationStatusReq changeCourseRegistrationStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeCourseRegistrationStatusReq>();
			changeCourseRegistrationStatusReq.CoursesId = CoursesId;
			changeCourseRegistrationStatusReq.NewRegistrationStatus = NewStatus;
			ClientServiceFactory.GetClientInstance<ICourseRegistration>().ChangeCourseRegistrationStatus(changeCourseRegistrationStatusReq);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00012BE8 File Offset: 0x00010DE8
		public IList<CourseRegistrationDTO> LoadStudentsCourses(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses)
		{
			LoadStudentsCoursesReq loadStudentsCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentsCoursesReq>();
			loadStudentsCoursesReq.StartDate = StartDate;
			loadStudentsCoursesReq.EndDate = EndDate;
			loadStudentsCoursesReq.PersonId = PersonId;
			loadStudentsCoursesReq.IncludeDroppedCourses = IncludeDroppedCourses;
			return ClientServiceFactory.GetClientInstance<ICourseRegistration>().LoadStudentsCourses(loadStudentsCoursesReq).CourseRegistrations;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00012C38 File Offset: 0x00010E38
		public IList<CourseRegistrationWithStudentSpecificInfoDTO> LoadStudentsCoursesWithStudentSpecificInfos(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses)
		{
			LoadStudentsCoursesWithStudentSpecificInfosReq loadStudentsCoursesWithStudentSpecificInfosReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentsCoursesWithStudentSpecificInfosReq>();
			loadStudentsCoursesWithStudentSpecificInfosReq.StartDate = StartDate;
			loadStudentsCoursesWithStudentSpecificInfosReq.EndDate = EndDate;
			loadStudentsCoursesWithStudentSpecificInfosReq.PersonId = PersonId;
			loadStudentsCoursesWithStudentSpecificInfosReq.IncludeDroppedCourses = IncludeDroppedCourses;
			return ClientServiceFactory.GetClientInstance<ICourseRegistration>().LoadStudentsCoursesWithStudentSpecificInfos(loadStudentsCoursesWithStudentSpecificInfosReq).CourseRegistrationsWithStudentSpecificInfos;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00012C88 File Offset: 0x00010E88
		public CourseRegistrationDTO RegisterStudentInCourse(int StudentPid, int Lucid, bool? IsCourseExemptFromDataSyncForStudent)
		{
			RegisterStudentInCourseReq registerStudentInCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RegisterStudentInCourseReq>();
			registerStudentInCourseReq.StudentPid = StudentPid;
			registerStudentInCourseReq.Lucid = Lucid;
			registerStudentInCourseReq.IsCourseExemptFromDataSyncForStudent = IsCourseExemptFromDataSyncForStudent;
			return ClientServiceFactory.GetClientInstance<ICourseRegistration>().RegisterStudentInCourse(registerStudentInCourseReq).CourseRegistration;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00012CD0 File Offset: 0x00010ED0
		public void DeleteCourseRegistration(int CoursesId)
		{
			DeleteCourseRegistrationReq deleteCourseRegistrationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteCourseRegistrationReq>();
			deleteCourseRegistrationReq.CoursesId = CoursesId;
			ClientServiceFactory.GetClientInstance<ICourseRegistration>().DeleteCourseRegistration(deleteCourseRegistrationReq);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00012D00 File Offset: 0x00010F00
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByStudent(int PersonId)
		{
			GetUniqueCourseRegistrationStartDatesByStudentReq getUniqueCourseRegistrationStartDatesByStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetUniqueCourseRegistrationStartDatesByStudentReq>();
			getUniqueCourseRegistrationStartDatesByStudentReq.StudentPid = PersonId;
			return ClientServiceFactory.GetClientInstance<ICourseRegistration>().GetUniqueCourseRegistrationStartDatesByStudent(getUniqueCourseRegistrationStartDatesByStudentReq).CourseStartDates;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00012D38 File Offset: 0x00010F38
		public void SetDateLetterIssuedByCourses(int CoursesId, DateTime? Date)
		{
			SetCourseLetterDateByCoursesReq setCourseLetterDateByCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByCoursesReq>();
			setCourseLetterDateByCoursesReq.CoursesId = CoursesId;
			setCourseLetterDateByCoursesReq.Date = Date;
			ClientServiceFactory.GetClientInstance<ICourseRegistration>().SetDateLetterIssuedByCourses(setCourseLetterDateByCoursesReq);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00012D70 File Offset: 0x00010F70
		public void SetDateLetterIssuedByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date)
		{
			SetCourseLetterDateByStudentAndCourseReq setCourseLetterDateByStudentAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByStudentAndCourseReq>();
			setCourseLetterDateByStudentAndCourseReq.PersonId = PersonId;
			setCourseLetterDateByStudentAndCourseReq.LuCourseId = LuCourseId;
			setCourseLetterDateByStudentAndCourseReq.Date = Date;
			ClientServiceFactory.GetClientInstance<ICourseRegistration>().SetDateLetterIssuedByStudentAndCourse(setCourseLetterDateByStudentAndCourseReq);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00012DB0 File Offset: 0x00010FB0
		public void SetDateLetterReturnedByCourses(int CoursesId, DateTime? Date)
		{
			SetCourseLetterDateByCoursesReq setCourseLetterDateByCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByCoursesReq>();
			setCourseLetterDateByCoursesReq.CoursesId = CoursesId;
			setCourseLetterDateByCoursesReq.Date = Date;
			ClientServiceFactory.GetClientInstance<ICourseRegistration>().SetDateLetterReturnedByCourses(setCourseLetterDateByCoursesReq);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00012DE8 File Offset: 0x00010FE8
		public void SetDateLetterReturnedByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date)
		{
			SetCourseLetterDateByStudentAndCourseReq setCourseLetterDateByStudentAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByStudentAndCourseReq>();
			setCourseLetterDateByStudentAndCourseReq.PersonId = PersonId;
			setCourseLetterDateByStudentAndCourseReq.LuCourseId = LuCourseId;
			setCourseLetterDateByStudentAndCourseReq.Date = Date;
			ClientServiceFactory.GetClientInstance<ICourseRegistration>().SetDateLetterReturnedByStudentAndCourse(setCourseLetterDateByStudentAndCourseReq);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00012E28 File Offset: 0x00011028
		public void SetProfLastViewedLetterByCourses(int CoursesId, DateTime? Date)
		{
			SetCourseLetterDateByCoursesReq setCourseLetterDateByCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByCoursesReq>();
			setCourseLetterDateByCoursesReq.CoursesId = CoursesId;
			setCourseLetterDateByCoursesReq.Date = Date;
			ClientServiceFactory.GetClientInstance<ICourseRegistration>().SetProfLastViewedLetterByCourses(setCourseLetterDateByCoursesReq);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00012E60 File Offset: 0x00011060
		public void SetProfLastViewedLetterByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date)
		{
			SetCourseLetterDateByStudentAndCourseReq setCourseLetterDateByStudentAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByStudentAndCourseReq>();
			setCourseLetterDateByStudentAndCourseReq.PersonId = PersonId;
			setCourseLetterDateByStudentAndCourseReq.LuCourseId = LuCourseId;
			setCourseLetterDateByStudentAndCourseReq.Date = Date;
			ClientServiceFactory.GetClientInstance<ICourseRegistration>().SetProfLastViewedLetterByStudentAndCourse(setCourseLetterDateByStudentAndCourseReq);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00012EA0 File Offset: 0x000110A0
		public void SetStudentLastViewedLetterByCourses(int CoursesId, DateTime? Date)
		{
			SetCourseLetterDateByCoursesReq setCourseLetterDateByCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByCoursesReq>();
			setCourseLetterDateByCoursesReq.CoursesId = CoursesId;
			setCourseLetterDateByCoursesReq.Date = Date;
			ClientServiceFactory.GetClientInstance<ICourseRegistration>().SetStudentLastViewedLetterByCourses(setCourseLetterDateByCoursesReq);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00012ED8 File Offset: 0x000110D8
		public void SetStudentLastViewedLetterByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date)
		{
			SetCourseLetterDateByStudentAndCourseReq setCourseLetterDateByStudentAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByStudentAndCourseReq>();
			setCourseLetterDateByStudentAndCourseReq.PersonId = PersonId;
			setCourseLetterDateByStudentAndCourseReq.LuCourseId = LuCourseId;
			setCourseLetterDateByStudentAndCourseReq.Date = Date;
			ClientServiceFactory.GetClientInstance<ICourseRegistration>().SetStudentLastViewedLetterByStudentAndCourse(setCourseLetterDateByStudentAndCourseReq);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00012F18 File Offset: 0x00011118
		public CourseRegistrationDTO LoadCourseRegistrationsByStudentAndCourse(int StudentPid, int Lucid)
		{
			LoadCourseRegistrationsByStudentAndCourseReq loadCourseRegistrationsByStudentAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCourseRegistrationsByStudentAndCourseReq>();
			loadCourseRegistrationsByStudentAndCourseReq.StudentPersonId = StudentPid;
			loadCourseRegistrationsByStudentAndCourseReq.LuCourseId = Lucid;
			return ClientServiceFactory.GetClientInstance<ICourseRegistration>().LoadCourseRegistrationsByStudentAndCourse(loadCourseRegistrationsByStudentAndCourseReq).CourseRegistration;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00012F58 File Offset: 0x00011158
		public bool IsInstructorOrAltContactTeachingStudentsCourse(int StudentPersonId, int LuCourseId, int InstructorId, int AlternateContactId)
		{
			IsInstructorOrAltContactTeachingStudentsCourseReq isInstructorOrAltContactTeachingStudentsCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsInstructorOrAltContactTeachingStudentsCourseReq>();
			isInstructorOrAltContactTeachingStudentsCourseReq.StudentPersonId = StudentPersonId;
			isInstructorOrAltContactTeachingStudentsCourseReq.LuCourseId = LuCourseId;
			isInstructorOrAltContactTeachingStudentsCourseReq.InstructorId = InstructorId;
			isInstructorOrAltContactTeachingStudentsCourseReq.AlternateContactId = AlternateContactId;
			return ClientServiceFactory.GetClientInstance<ICourseRegistration>().IsInstructorOrAltContactTeachingStudentsCourse(isInstructorOrAltContactTeachingStudentsCourseReq).IsTeachingStudentsCourse;
		}
	}
}
