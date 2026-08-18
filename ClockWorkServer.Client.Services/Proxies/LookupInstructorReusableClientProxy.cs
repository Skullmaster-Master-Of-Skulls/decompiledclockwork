using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000D5 RID: 213
	public class LookupInstructorReusableClientProxy : WCFTokenBasedReusableClientProxy<ILookupInstructor>, ILookupInstructor, IService
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x00015B8E File Offset: 0x00013D8E
		public LookupInstructorReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00015B99 File Offset: 0x00013D99
		public LookupInstructorReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00015BA8 File Offset: 0x00013DA8
		public LoadAllAssignedInstructorsResp LoadAllAssignedInstructors(LoadAllAssignedInstructorsReq Request)
		{
			return this.WrapServiceMethod<LoadAllAssignedInstructorsResp>(() => this.Proxy.LoadAllAssignedInstructors(Request));
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00015BE0 File Offset: 0x00013DE0
		public LoadInstructorResp LoadInstructor(LoadInstructorReq Request)
		{
			return this.WrapServiceMethod<LoadInstructorResp>(() => this.Proxy.LoadInstructor(Request));
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00015C18 File Offset: 0x00013E18
		public LoadInstructorByEmailResp LoadInstructorByEmail(LoadInstructorByEmailReq Request)
		{
			return this.WrapServiceMethod<LoadInstructorByEmailResp>(() => this.Proxy.LoadInstructorByEmail(Request));
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00015C50 File Offset: 0x00013E50
		public LoadInstructorByUsernameResp LoadInstructorByUsername(LoadInstructorByUsernameReq Request)
		{
			return this.WrapServiceMethod<LoadInstructorByUsernameResp>(() => this.Proxy.LoadInstructorByUsername(Request));
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00015C88 File Offset: 0x00013E88
		public LoadInstructorCoursesResp LoadInstructorCourses(LoadInstructorCoursesReq Request)
		{
			return this.WrapServiceMethod<LoadInstructorCoursesResp>(() => this.Proxy.LoadInstructorCourses(Request));
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00015CC0 File Offset: 0x00013EC0
		public SaveInstructorResp SaveInstructor(SaveInstructorReq Request)
		{
			return this.WrapServiceMethod<SaveInstructorResp>(() => this.Proxy.SaveInstructor(Request));
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00015CF8 File Offset: 0x00013EF8
		public SaveInstructorsForCourseResp SaveInstructorsForCourse(SaveInstructorsForCourseReq Request)
		{
			return this.WrapServiceMethod<SaveInstructorsForCourseResp>(() => this.Proxy.SaveInstructorsForCourse(Request));
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00015D30 File Offset: 0x00013F30
		public LoadInstructorByEmployeeIdResp LoadInstructorByEmployeeId(LoadInstructorByEmployeeIdReq Request)
		{
			return this.WrapServiceMethod<LoadInstructorByEmployeeIdResp>(() => this.Proxy.LoadInstructorByEmployeeId(Request));
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00015D68 File Offset: 0x00013F68
		public void AssignInstructorToCourse(AssignInstructorToCourseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.AssignInstructorToCourse(Request);
			});
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00015DA0 File Offset: 0x00013FA0
		public LoadInstructorsBySearchStringResp LoadInstructorsBySearchString(LoadInstructorsBySearchStringReq Request)
		{
			return this.WrapServiceMethod<LoadInstructorsBySearchStringResp>(() => this.Proxy.LoadInstructorsBySearchString(Request));
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00015DD8 File Offset: 0x00013FD8
		public void RemoveInstructorFromCourse(RemoveInstructorFromCourseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.RemoveInstructorFromCourse(Request);
			});
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00015E10 File Offset: 0x00014010
		public LoadInstructorsByCourseResp LoadInstructorsByCourse(LoadInstructorsByCourseReq Request)
		{
			return this.WrapServiceMethod<LoadInstructorsByCourseResp>(() => this.Proxy.LoadInstructorsByCourse(Request));
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00015E48 File Offset: 0x00014048
		public UpdateInstructorDataSyncExemptionResp UpdateInstructorDataSyncExemption(UpdateInstructorDataSyncExemptionReq Request)
		{
			return this.WrapServiceMethod<UpdateInstructorDataSyncExemptionResp>(() => this.Proxy.UpdateInstructorDataSyncExemption(Request));
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00015E80 File Offset: 0x00014080
		public GetUniqueCourseRegistrationStartDatesByInstructorResp GetUniqueCourseRegistrationStartDatesByInstructor(GetUniqueCourseRegistrationStartDatesByInstructorReq Request)
		{
			return this.WrapServiceMethod<GetUniqueCourseRegistrationStartDatesByInstructorResp>(() => this.Proxy.GetUniqueCourseRegistrationStartDatesByInstructor(Request));
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00015EB8 File Offset: 0x000140B8
		public LoadInstructorCoursesWithAtLeastOneStudentRegisteredResp LoadInstructorCoursesWithAtLeastOneStudentRegistered(LoadInstructorCoursesWithAtLeastOneStudentRegisteredReq Request)
		{
			return this.WrapServiceMethod<LoadInstructorCoursesWithAtLeastOneStudentRegisteredResp>(() => this.Proxy.LoadInstructorCoursesWithAtLeastOneStudentRegistered(Request));
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00015EF0 File Offset: 0x000140F0
		public GetStudentsWithApprovedRequestsByCourseDateResp GetStudentsWithApprovedRequestsByCourseDate(GetStudentsWithApprovedRequestsByCourseDateReq Request)
		{
			return this.WrapServiceMethod<GetStudentsWithApprovedRequestsByCourseDateResp>(() => this.Proxy.GetStudentsWithApprovedRequestsByCourseDate(Request));
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00015F28 File Offset: 0x00014128
		public LoadStudentsWithCourseAndAccommodationInfosByCoursesResp LoadStudentsWithCourseAndAccommodationInfosByCourses(LoadStudentsWithCourseAndAccommodationInfosByCoursesReq Request)
		{
			return this.WrapServiceMethod<LoadStudentsWithCourseAndAccommodationInfosByCoursesResp>(() => this.Proxy.LoadStudentsWithCourseAndAccommodationInfosByCourses(Request));
		}
	}
}
