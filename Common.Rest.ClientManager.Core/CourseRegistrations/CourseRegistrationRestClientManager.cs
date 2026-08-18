using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.CourseRegistrations
{
	// Token: 0x02000061 RID: 97
	public class CourseRegistrationRestClientManager : BearerTokenRestProxy<ICourseRegistrationClientManager>, ICourseRegistrationClientManager, IWebService
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0000B391 File Offset: 0x00009591
		public CourseRegistrationRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000B39B File Offset: 0x0000959B
		public CourseRegistrationRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000B3A6 File Offset: 0x000095A6
		public IList<CourseRegistrationDTO> LoadCoursesStudentIsAllowedToBookTestsForNow(int StudentPersonId)
		{
			return base.GetMany<CourseRegistrationDTO>(string.Format("courseregistration/studentisallowedtobooktestfornow/studentpid/{0}", StudentPersonId), true);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000B3BF File Offset: 0x000095BF
		public IList<CourseRegistrationDTO> LoadCoursesStudentIsAllowedToBookFinalExamsForNow(int StudentPersonId)
		{
			return base.GetMany<CourseRegistrationDTO>(string.Format("courseregistration/studentisallowedtobookfinalexamsfornow/studentpid/{0}", StudentPersonId), true);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void ChangeCourseRegistrationStatus(int CoursesId, eRegistrationStatusDTO NewStatus)
		{
			ChangeCourseRegistrationStatusReq changeCourseRegistrationStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeCourseRegistrationStatusReq>();
			changeCourseRegistrationStatusReq.CoursesId = CoursesId;
			changeCourseRegistrationStatusReq.NewRegistrationStatus = NewStatus;
			base.Post<ChangeCourseRegistrationStatusReq>(changeCourseRegistrationStatusReq, "courseregistration/changestatus");
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000B40A File Offset: 0x0000960A
		public IList<CourseRegistrationDTO> LoadStudentsCourses(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses)
		{
			return base.GetMany<CourseRegistrationDTO>(string.Format("courseregistration/studentcourses/pid/{0}/range/{1}/{2}?includedroppedcourses={3}", new object[]
			{
				PersonId,
				StartDate,
				EndDate,
				IncludeDroppedCourses
			}), true);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000B448 File Offset: 0x00009648
		public CourseRegistrationDTO RegisterStudentInCourse(int StudentPid, int Lucid, bool? IsCourseExemptFromDataSyncForStudent)
		{
			RegisterStudentInCourseReq registerStudentInCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RegisterStudentInCourseReq>();
			registerStudentInCourseReq.StudentPid = StudentPid;
			registerStudentInCourseReq.Lucid = Lucid;
			registerStudentInCourseReq.IsCourseExemptFromDataSyncForStudent = IsCourseExemptFromDataSyncForStudent;
			return base.Post<RegisterStudentInCourseReq, CourseRegistrationDTO>(registerStudentInCourseReq, "courseregistration/registerstudentincourse");
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000B481 File Offset: 0x00009681
		public void DeleteCourseRegistration(int CoursesId)
		{
			base.Delete(string.Format("courseregistration/courseid/{0}", CoursesId));
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000B499 File Offset: 0x00009699
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByStudent(int PersonId)
		{
			return base.GetMany<DateTime>(string.Format("courseregistration/uniquecourseregistrationstartdates/studenpid/{0}", PersonId), true);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000B4B4 File Offset: 0x000096B4
		public void SetDateLetterIssuedByCourses(int CoursesId, DateTime? Date)
		{
			SetCourseLetterDateByCoursesReq setCourseLetterDateByCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByCoursesReq>();
			setCourseLetterDateByCoursesReq.CoursesId = CoursesId;
			setCourseLetterDateByCoursesReq.Date = Date;
			base.Post<SetCourseLetterDateByCoursesReq>(setCourseLetterDateByCoursesReq, "courseregistration/dateletterissuedbycourses");
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000B4E8 File Offset: 0x000096E8
		public void SetDateLetterIssuedByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date)
		{
			SetCourseLetterDateByStudentAndCourseReq setCourseLetterDateByStudentAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByStudentAndCourseReq>();
			setCourseLetterDateByStudentAndCourseReq.PersonId = PersonId;
			setCourseLetterDateByStudentAndCourseReq.LuCourseId = LuCourseId;
			setCourseLetterDateByStudentAndCourseReq.Date = Date;
			base.Post<SetCourseLetterDateByStudentAndCourseReq>(setCourseLetterDateByStudentAndCourseReq, "courseregistration/dateletterissuedbystudentandcourse");
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000B524 File Offset: 0x00009724
		public void SetDateLetterReturnedByCourses(int CoursesId, DateTime? Date)
		{
			SetCourseLetterDateByCoursesReq setCourseLetterDateByCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByCoursesReq>();
			setCourseLetterDateByCoursesReq.CoursesId = CoursesId;
			setCourseLetterDateByCoursesReq.Date = Date;
			base.Post<SetCourseLetterDateByCoursesReq>(setCourseLetterDateByCoursesReq, "courseregistration/dateletterreturnedbycourses");
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000B558 File Offset: 0x00009758
		public void SetDateLetterReturnedByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date)
		{
			SetCourseLetterDateByStudentAndCourseReq setCourseLetterDateByStudentAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByStudentAndCourseReq>();
			setCourseLetterDateByStudentAndCourseReq.PersonId = PersonId;
			setCourseLetterDateByStudentAndCourseReq.LuCourseId = LuCourseId;
			setCourseLetterDateByStudentAndCourseReq.Date = Date;
			base.Post<SetCourseLetterDateByStudentAndCourseReq>(setCourseLetterDateByStudentAndCourseReq, "courseregistration/dateletterreturnedbystudentandcourse");
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000B594 File Offset: 0x00009794
		public void SetProfLastViewedLetterByCourses(int CoursesId, DateTime? Date)
		{
			SetCourseLetterDateByCoursesReq setCourseLetterDateByCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByCoursesReq>();
			setCourseLetterDateByCoursesReq.CoursesId = CoursesId;
			setCourseLetterDateByCoursesReq.Date = Date;
			base.Post<SetCourseLetterDateByCoursesReq>(setCourseLetterDateByCoursesReq, "courseregistration/proflastviewedletterbycourses");
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000B5C8 File Offset: 0x000097C8
		public void SetProfLastViewedLetterByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date)
		{
			SetCourseLetterDateByStudentAndCourseReq setCourseLetterDateByStudentAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByStudentAndCourseReq>();
			setCourseLetterDateByStudentAndCourseReq.PersonId = PersonId;
			setCourseLetterDateByStudentAndCourseReq.LuCourseId = LuCourseId;
			setCourseLetterDateByStudentAndCourseReq.Date = Date;
			base.Post<SetCourseLetterDateByStudentAndCourseReq>(setCourseLetterDateByStudentAndCourseReq, "courseregistration/proflastviewedletterbystudentandcourse");
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000B601 File Offset: 0x00009801
		public void SetStudentLastViewedLetterByCourses(int CoursesId, DateTime? Date)
		{
			SetCourseLetterDateByCoursesReq setCourseLetterDateByCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByCoursesReq>();
			setCourseLetterDateByCoursesReq.CoursesId = CoursesId;
			setCourseLetterDateByCoursesReq.Date = Date;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000B61C File Offset: 0x0000981C
		public void SetStudentLastViewedLetterByStudentAndCourse(int PersonId, int LuCourseId, DateTime? Date)
		{
			SetCourseLetterDateByStudentAndCourseReq setCourseLetterDateByStudentAndCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetCourseLetterDateByStudentAndCourseReq>();
			setCourseLetterDateByStudentAndCourseReq.PersonId = PersonId;
			setCourseLetterDateByStudentAndCourseReq.LuCourseId = LuCourseId;
			setCourseLetterDateByStudentAndCourseReq.Date = Date;
			base.Post<SetCourseLetterDateByStudentAndCourseReq>(setCourseLetterDateByStudentAndCourseReq, "courseregistration/studentlastviewedletterbystudentandcourse");
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000B655 File Offset: 0x00009855
		public CourseRegistrationDTO LoadCourseRegistrationsByStudentAndCourse(int StudentPid, int Lucid)
		{
			return base.Get<CourseRegistrationDTO>(string.Format("courseregistration/studentpid/{0}/lucourseid/{1}", StudentPid, Lucid), true);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000B674 File Offset: 0x00009874
		public bool IsInstructorOrAltContactTeachingStudentsCourse(int StudentPersonId, int LuCourseId, int InstructorId, int AlternateContactId)
		{
			return base.Get<bool>(string.Format("courseregistration/isinstructororaltcontactteachingstudentscourse/studentpid/{0}/lucourseid/{1}/instructorid/{2}/altcontactid/{3}", new object[]
			{
				StudentPersonId,
				LuCourseId,
				InstructorId,
				AlternateContactId
			}), true);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000B6B2 File Offset: 0x000098B2
		public IList<CourseRegistrationWithStudentSpecificInfoDTO> LoadStudentsCoursesWithStudentSpecificInfos(DateTime StartDate, DateTime EndDate, int PersonId, bool IncludeDroppedCourses)
		{
			return base.GetMany<CourseRegistrationWithStudentSpecificInfoDTO>(string.Format("courseregistration/studentcourseswithspecificinfos/pid/{0}/range/{1}/{2}?includedroppedcourses={3}", new object[]
			{
				PersonId,
				StartDate,
				EndDate,
				IncludeDroppedCourses
			}), true);
		}
	}
}
