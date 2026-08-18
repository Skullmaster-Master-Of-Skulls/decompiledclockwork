using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000D6 RID: 214
	internal class LookupInstructorClientBaseProxy : ClientBase<ILookupInstructor>, ILookupInstructor, IService
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x00015F60 File Offset: 0x00014160
		public LookupInstructorClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00015F6B File Offset: 0x0001416B
		public LookupInstructorClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00015F78 File Offset: 0x00014178
		public LoadAllAssignedInstructorsResp LoadAllAssignedInstructors(LoadAllAssignedInstructorsReq Request)
		{
			return base.Channel.LoadAllAssignedInstructors(Request);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00015F98 File Offset: 0x00014198
		public LoadInstructorResp LoadInstructor(LoadInstructorReq Request)
		{
			return base.Channel.LoadInstructor(Request);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00015FB8 File Offset: 0x000141B8
		public LoadInstructorByEmailResp LoadInstructorByEmail(LoadInstructorByEmailReq Request)
		{
			return base.Channel.LoadInstructorByEmail(Request);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00015FD8 File Offset: 0x000141D8
		public LoadInstructorByUsernameResp LoadInstructorByUsername(LoadInstructorByUsernameReq Request)
		{
			return base.Channel.LoadInstructorByUsername(Request);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00015FF8 File Offset: 0x000141F8
		public LoadInstructorCoursesResp LoadInstructorCourses(LoadInstructorCoursesReq Request)
		{
			return base.Channel.LoadInstructorCourses(Request);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00016018 File Offset: 0x00014218
		public SaveInstructorResp SaveInstructor(SaveInstructorReq Request)
		{
			return base.Channel.SaveInstructor(Request);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00016038 File Offset: 0x00014238
		public SaveInstructorsForCourseResp SaveInstructorsForCourse(SaveInstructorsForCourseReq Request)
		{
			return base.Channel.SaveInstructorsForCourse(Request);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00016058 File Offset: 0x00014258
		public LoadInstructorByEmployeeIdResp LoadInstructorByEmployeeId(LoadInstructorByEmployeeIdReq Request)
		{
			return base.Channel.LoadInstructorByEmployeeId(Request);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00016076 File Offset: 0x00014276
		public void AssignInstructorToCourse(AssignInstructorToCourseReq Request)
		{
			base.Channel.AssignInstructorToCourse(Request);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00016088 File Offset: 0x00014288
		public LoadInstructorsBySearchStringResp LoadInstructorsBySearchString(LoadInstructorsBySearchStringReq Request)
		{
			return base.Channel.LoadInstructorsBySearchString(Request);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000160A6 File Offset: 0x000142A6
		public void RemoveInstructorFromCourse(RemoveInstructorFromCourseReq Request)
		{
			base.Channel.RemoveInstructorFromCourse(Request);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000160B8 File Offset: 0x000142B8
		public LoadInstructorsByCourseResp LoadInstructorsByCourse(LoadInstructorsByCourseReq Request)
		{
			return base.Channel.LoadInstructorsByCourse(Request);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000160D8 File Offset: 0x000142D8
		public UpdateInstructorDataSyncExemptionResp UpdateInstructorDataSyncExemption(UpdateInstructorDataSyncExemptionReq Request)
		{
			return base.Channel.UpdateInstructorDataSyncExemption(Request);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000160F8 File Offset: 0x000142F8
		public GetUniqueCourseRegistrationStartDatesByInstructorResp GetUniqueCourseRegistrationStartDatesByInstructor(GetUniqueCourseRegistrationStartDatesByInstructorReq Request)
		{
			return base.Channel.GetUniqueCourseRegistrationStartDatesByInstructor(Request);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00016118 File Offset: 0x00014318
		public LoadInstructorCoursesWithAtLeastOneStudentRegisteredResp LoadInstructorCoursesWithAtLeastOneStudentRegistered(LoadInstructorCoursesWithAtLeastOneStudentRegisteredReq Request)
		{
			return base.Channel.LoadInstructorCoursesWithAtLeastOneStudentRegistered(Request);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00016138 File Offset: 0x00014338
		public GetStudentsWithApprovedRequestsByCourseDateResp GetStudentsWithApprovedRequestsByCourseDate(GetStudentsWithApprovedRequestsByCourseDateReq Request)
		{
			return base.Channel.GetStudentsWithApprovedRequestsByCourseDate(Request);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00016158 File Offset: 0x00014358
		public LoadStudentsWithCourseAndAccommodationInfosByCoursesResp LoadStudentsWithCourseAndAccommodationInfosByCourses(LoadStudentsWithCourseAndAccommodationInfosByCoursesReq Request)
		{
			return base.Channel.LoadStudentsWithCourseAndAccommodationInfosByCourses(Request);
		}
	}
}
