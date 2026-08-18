using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.Core.Mappers.CourseRegistrations;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.CourseRegistrations;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000030 RID: 48
	public class CourseRegistrationServiceManager : ICourseRegistration, IService
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00009A08 File Offset: 0x00007C08
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009A1C File Offset: 0x00007C1C
		public LoadCoursesStudentIsAllowedToBookTestsForNowResp LoadCoursesStudentIsAllowedToBookTestsForNow(LoadCoursesStudentIsAllowedToBookTestsForNowReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			StudentCourseList studentCourseList = courseRegistrationManager.LoadCoursesStudentIsAllowedToBookTestsForNow(Request.StudentPersonId);
			return new LoadCoursesStudentIsAllowedToBookTestsForNowResp
			{
				CourseList = ((studentCourseList != null) ? studentCourseList.ToDTO() : null)
			};
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009A60 File Offset: 0x00007C60
		public LoadCoursesStudentIsAllowedToBookFinalExamsForNowResp LoadCoursesStudentIsAllowedToBookFinalExamsForNow(LoadCoursesStudentIsAllowedToBookFinalExamsForNowReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			StudentCourseList studentCourseList = courseRegistrationManager.LoadCoursesStudentIsAllowedToBookFinalExamsForNow(Request.StudentPersonId);
			return new LoadCoursesStudentIsAllowedToBookFinalExamsForNowResp
			{
				CourseList = ((studentCourseList != null) ? studentCourseList.ToDTO() : null)
			};
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009AA4 File Offset: 0x00007CA4
		public LoadStudentsCoursesResp LoadStudentsCourses(LoadStudentsCoursesReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			List<CourseRegistration> list = courseRegistrationManager.LoadStudentsCourses(Request.StartDate, Request.EndDate, Request.PersonId, Request.IncludeDroppedCourses);
			LoadStudentsCoursesResp loadStudentsCoursesResp = new LoadStudentsCoursesResp();
			List<CourseRegistrationDTO> courseRegistrations;
			if (list == null)
			{
				courseRegistrations = null;
			}
			else
			{
				courseRegistrations = list.ConvertAll<CourseRegistrationDTO>((CourseRegistration f) => f.ToDTO());
			}
			loadStudentsCoursesResp.CourseRegistrations = courseRegistrations;
			return loadStudentsCoursesResp;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009B18 File Offset: 0x00007D18
		public void ChangeCourseRegistrationStatus(ChangeCourseRegistrationStatusReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			courseRegistrationManager.ChangeCourseRegistrationStatus(Request.CoursesId, (eRegistrationStatus)Request.NewRegistrationStatus);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009B48 File Offset: 0x00007D48
		public RegisterStudentInCourseResp RegisterStudentInCourse(RegisterStudentInCourseReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			CourseRegistration courseRegistration = courseRegistrationManager.RegisterStudentInCourse(Request.StudentPid, Request.Lucid, Request.IsCourseExemptFromDataSyncForStudent);
			return new RegisterStudentInCourseResp
			{
				CourseRegistration = ((courseRegistration != null) ? courseRegistration.ToDTO() : null)
			};
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009B98 File Offset: 0x00007D98
		public void DeleteCourseRegistration(DeleteCourseRegistrationReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			courseRegistrationManager.DeleteCourseRegistration(Request.CoursesId);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009BC0 File Offset: 0x00007DC0
		public GetUniqueCourseRegistrationStartDatesByStudentResp GetUniqueCourseRegistrationStartDatesByStudent(GetUniqueCourseRegistrationStartDatesByStudentReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			IList<DateTime> uniqueCourseRegistrationStartDatesByStudent = courseRegistrationManager.GetUniqueCourseRegistrationStartDatesByStudent(Request.StudentPid);
			return new GetUniqueCourseRegistrationStartDatesByStudentResp
			{
				CourseStartDates = uniqueCourseRegistrationStartDatesByStudent
			};
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009BF8 File Offset: 0x00007DF8
		public void SetDateLetterIssuedByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			courseRegistrationManager.SetDateLetterIssued(Request.PersonId, Request.LuCourseId, Request.Date);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009C2C File Offset: 0x00007E2C
		public void SetDateLetterReturnedByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			courseRegistrationManager.SetDateLetterReturned(Request.PersonId, Request.LuCourseId, Request.Date);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009C60 File Offset: 0x00007E60
		public void SetProfLastViewedLetterByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			courseRegistrationManager.SetProfLastViewedLetter(Request.PersonId, Request.LuCourseId, Request.Date);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009C94 File Offset: 0x00007E94
		public void SetStudentLastViewedLetterByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			courseRegistrationManager.SetStudentLastViewedLetter(Request.PersonId, Request.LuCourseId, Request.Date);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009CC8 File Offset: 0x00007EC8
		public void SetDateLetterIssuedByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			courseRegistrationManager.SetDateLetterIssued(Request.CoursesId, Request.Date);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009CF8 File Offset: 0x00007EF8
		public void SetDateLetterReturnedByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			courseRegistrationManager.SetDateLetterReturned(Request.CoursesId, Request.Date);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009D28 File Offset: 0x00007F28
		public void SetProfLastViewedLetterByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			courseRegistrationManager.SetProfLastViewedLetter(Request.CoursesId, Request.Date);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009D58 File Offset: 0x00007F58
		public void SetStudentLastViewedLetterByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			courseRegistrationManager.SetStudentLastViewedLetter(Request.CoursesId, Request.Date);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009D88 File Offset: 0x00007F88
		public LoadCourseRegistrationsByStudentAndCourseResp LoadCourseRegistrationsByStudentAndCourse(LoadCourseRegistrationsByStudentAndCourseReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			CourseRegistration courseRegistration = courseRegistrationManager.LoadCourseRegistrationsByStudentAndCourse(Request.StudentPersonId, Request.LuCourseId);
			return new LoadCourseRegistrationsByStudentAndCourseResp
			{
				CourseRegistration = ((courseRegistration == null) ? null : courseRegistration.ToDTO())
			};
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009DD4 File Offset: 0x00007FD4
		public IsInstructorOrAltContactTeachingStudentsCourseResp IsInstructorOrAltContactTeachingStudentsCourse(IsInstructorOrAltContactTeachingStudentsCourseReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			return new IsInstructorOrAltContactTeachingStudentsCourseResp
			{
				IsTeachingStudentsCourse = courseRegistrationManager.IsInstructorOrAltContactTeachingStudentsCourse(Request.StudentPersonId, Request.LuCourseId, Request.InstructorId, Request.AlternateContactId)
			};
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009E1C File Offset: 0x0000801C
		public LoadStudentsCoursesWithStudentSpecificInfosResp LoadStudentsCoursesWithStudentSpecificInfos(LoadStudentsCoursesWithStudentSpecificInfosReq Request)
		{
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(Request.GetOperationContext());
			IList<CourseRegistrationWithStudentSpecificInfo> list = courseRegistrationManager.LoadStudentsCoursesWithStudentSpecificInfos(Request.StartDate, Request.EndDate, Request.PersonId, Request.IncludeDroppedCourses);
			LoadStudentsCoursesWithStudentSpecificInfosResp loadStudentsCoursesWithStudentSpecificInfosResp = new LoadStudentsCoursesWithStudentSpecificInfosResp();
			List<CourseRegistrationWithStudentSpecificInfoDTO> courseRegistrationsWithStudentSpecificInfos;
			if (list == null)
			{
				courseRegistrationsWithStudentSpecificInfos = null;
			}
			else
			{
				courseRegistrationsWithStudentSpecificInfos = (from g in list
				select g.ToDTO()).ToList<CourseRegistrationWithStudentSpecificInfoDTO>();
			}
			loadStudentsCoursesWithStudentSpecificInfosResp.CourseRegistrationsWithStudentSpecificInfos = courseRegistrationsWithStudentSpecificInfos;
			return loadStudentsCoursesWithStudentSpecificInfosResp;
		}
	}
}
