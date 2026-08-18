using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200006C RID: 108
	internal class CourseRegistrationClientBaseProxy : ClientBase<ICourseRegistration>, ICourseRegistration, IService
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
		public CourseRegistrationClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000D1FB File Offset: 0x0000B3FB
		public CourseRegistrationClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000D208 File Offset: 0x0000B408
		public LoadCoursesStudentIsAllowedToBookFinalExamsForNowResp LoadCoursesStudentIsAllowedToBookFinalExamsForNow(LoadCoursesStudentIsAllowedToBookFinalExamsForNowReq Request)
		{
			return base.Channel.LoadCoursesStudentIsAllowedToBookFinalExamsForNow(Request);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000D228 File Offset: 0x0000B428
		public LoadCoursesStudentIsAllowedToBookTestsForNowResp LoadCoursesStudentIsAllowedToBookTestsForNow(LoadCoursesStudentIsAllowedToBookTestsForNowReq Request)
		{
			return base.Channel.LoadCoursesStudentIsAllowedToBookTestsForNow(Request);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000D246 File Offset: 0x0000B446
		public void ChangeCourseRegistrationStatus(ChangeCourseRegistrationStatusReq Request)
		{
			base.Channel.ChangeCourseRegistrationStatus(Request);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000D258 File Offset: 0x0000B458
		public LoadStudentsCoursesResp LoadStudentsCourses(LoadStudentsCoursesReq Request)
		{
			return base.Channel.LoadStudentsCourses(Request);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000D278 File Offset: 0x0000B478
		public RegisterStudentInCourseResp RegisterStudentInCourse(RegisterStudentInCourseReq Request)
		{
			return base.Channel.RegisterStudentInCourse(Request);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000D296 File Offset: 0x0000B496
		public void DeleteCourseRegistration(DeleteCourseRegistrationReq Request)
		{
			base.Channel.DeleteCourseRegistration(Request);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		public GetUniqueCourseRegistrationStartDatesByStudentResp GetUniqueCourseRegistrationStartDatesByStudent(GetUniqueCourseRegistrationStartDatesByStudentReq Request)
		{
			return base.Channel.GetUniqueCourseRegistrationStartDatesByStudent(Request);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000D2C6 File Offset: 0x0000B4C6
		public void SetDateLetterIssuedByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			base.Channel.SetDateLetterIssuedByCourses(Request);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000D2D6 File Offset: 0x0000B4D6
		public void SetDateLetterIssuedByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			base.Channel.SetDateLetterIssuedByStudentAndCourse(Request);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000D2E6 File Offset: 0x0000B4E6
		public void SetDateLetterReturnedByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			base.Channel.SetDateLetterReturnedByCourses(Request);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000D2F6 File Offset: 0x0000B4F6
		public void SetDateLetterReturnedByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			base.Channel.SetDateLetterReturnedByStudentAndCourse(Request);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000D306 File Offset: 0x0000B506
		public void SetProfLastViewedLetterByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			base.Channel.SetProfLastViewedLetterByCourses(Request);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000D316 File Offset: 0x0000B516
		public void SetProfLastViewedLetterByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			base.Channel.SetProfLastViewedLetterByStudentAndCourse(Request);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000D326 File Offset: 0x0000B526
		public void SetStudentLastViewedLetterByCourses(SetCourseLetterDateByCoursesReq Request)
		{
			base.Channel.SetStudentLastViewedLetterByCourses(Request);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000D336 File Offset: 0x0000B536
		public void SetStudentLastViewedLetterByStudentAndCourse(SetCourseLetterDateByStudentAndCourseReq Request)
		{
			base.Channel.SetStudentLastViewedLetterByStudentAndCourse(Request);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000D348 File Offset: 0x0000B548
		public LoadCourseRegistrationsByStudentAndCourseResp LoadCourseRegistrationsByStudentAndCourse(LoadCourseRegistrationsByStudentAndCourseReq Request)
		{
			return base.Channel.LoadCourseRegistrationsByStudentAndCourse(Request);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000D368 File Offset: 0x0000B568
		public IsInstructorOrAltContactTeachingStudentsCourseResp IsInstructorOrAltContactTeachingStudentsCourse(IsInstructorOrAltContactTeachingStudentsCourseReq Request)
		{
			return base.Channel.IsInstructorOrAltContactTeachingStudentsCourse(Request);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000D388 File Offset: 0x0000B588
		public LoadStudentsCoursesWithStudentSpecificInfosResp LoadStudentsCoursesWithStudentSpecificInfos(LoadStudentsCoursesWithStudentSpecificInfosReq Request)
		{
			return base.Channel.LoadStudentsCoursesWithStudentSpecificInfos(Request);
		}
	}
}
