using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200006B RID: 107
	public class CourseRegistrationReusableClientProxy : WCFTokenBasedReusableClientProxy<ICourseRegistration>, ICourseRegistration, IService
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
		public CourseRegistrationReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
		public CourseRegistrationReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000CE00 File Offset: 0x0000B000
		public LoadCoursesStudentIsAllowedToBookFinalExamsForNowResp LoadCoursesStudentIsAllowedToBookFinalExamsForNow(LoadCoursesStudentIsAllowedToBookFinalExamsForNowReq Request)
		{
			return this.WrapServiceMethod<LoadCoursesStudentIsAllowedToBookFinalExamsForNowResp>(() => this.Proxy.LoadCoursesStudentIsAllowedToBookFinalExamsForNow(Request));
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000CE38 File Offset: 0x0000B038
		public LoadCoursesStudentIsAllowedToBookTestsForNowResp LoadCoursesStudentIsAllowedToBookTestsForNow(LoadCoursesStudentIsAllowedToBookTestsForNowReq Request)
		{
			return this.WrapServiceMethod<LoadCoursesStudentIsAllowedToBookTestsForNowResp>(() => this.Proxy.LoadCoursesStudentIsAllowedToBookTestsForNow(Request));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000CE70 File Offset: 0x0000B070
		public LoadStudentsCoursesResp LoadStudentsCourses(LoadStudentsCoursesReq request)
		{
			return this.WrapServiceMethod<LoadStudentsCoursesResp>(() => this.Proxy.LoadStudentsCourses(request));
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		public void ChangeCourseRegistrationStatus(ChangeCourseRegistrationStatusReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ChangeCourseRegistrationStatus(Request);
			});
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		public RegisterStudentInCourseResp RegisterStudentInCourse(RegisterStudentInCourseReq Request)
		{
			return this.WrapServiceMethod<RegisterStudentInCourseResp>(() => this.Proxy.RegisterStudentInCourse(Request));
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000CF18 File Offset: 0x0000B118
		public void DeleteCourseRegistration(DeleteCourseRegistrationReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteCourseRegistration(Request);
			});
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000CF50 File Offset: 0x0000B150
		public GetUniqueCourseRegistrationStartDatesByStudentResp GetUniqueCourseRegistrationStartDatesByStudent(GetUniqueCourseRegistrationStartDatesByStudentReq Request)
		{
			return this.WrapServiceMethod<GetUniqueCourseRegistrationStartDatesByStudentResp>(() => this.Proxy.GetUniqueCourseRegistrationStartDatesByStudent(Request));
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000CF88 File Offset: 0x0000B188
		public void SetDateLetterIssuedByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetDateLetterIssuedByCourses(Request);
			});
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		public void SetDateLetterIssuedByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetDateLetterIssuedByStudentAndCourse(Request);
			});
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		public void SetDateLetterReturnedByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetDateLetterReturnedByCourses(Request);
			});
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000D030 File Offset: 0x0000B230
		public void SetDateLetterReturnedByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetDateLetterReturnedByStudentAndCourse(Request);
			});
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000D068 File Offset: 0x0000B268
		public void SetProfLastViewedLetterByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetProfLastViewedLetterByCourses(Request);
			});
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		public void SetProfLastViewedLetterByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetProfLastViewedLetterByStudentAndCourse(Request);
			});
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		public void SetStudentLastViewedLetterByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetStudentLastViewedLetterByCourses(Request);
			});
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000D110 File Offset: 0x0000B310
		public void SetStudentLastViewedLetterByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SetStudentLastViewedLetterByStudentAndCourse(Request);
			});
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000D148 File Offset: 0x0000B348
		public LoadCourseRegistrationsByStudentAndCourseResp LoadCourseRegistrationsByStudentAndCourse(LoadCourseRegistrationsByStudentAndCourseReq Request)
		{
			return this.WrapServiceMethod<LoadCourseRegistrationsByStudentAndCourseResp>(() => this.Proxy.LoadCourseRegistrationsByStudentAndCourse(Request));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000D180 File Offset: 0x0000B380
		public IsInstructorOrAltContactTeachingStudentsCourseResp IsInstructorOrAltContactTeachingStudentsCourse(IsInstructorOrAltContactTeachingStudentsCourseReq Request)
		{
			return this.WrapServiceMethod<IsInstructorOrAltContactTeachingStudentsCourseResp>(() => this.Proxy.IsInstructorOrAltContactTeachingStudentsCourse(Request));
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		public LoadStudentsCoursesWithStudentSpecificInfosResp LoadStudentsCoursesWithStudentSpecificInfos(LoadStudentsCoursesWithStudentSpecificInfosReq Request)
		{
			return this.WrapServiceMethod<LoadStudentsCoursesWithStudentSpecificInfosResp>(() => this.Proxy.LoadStudentsCoursesWithStudentSpecificInfos(Request));
		}
	}
}
