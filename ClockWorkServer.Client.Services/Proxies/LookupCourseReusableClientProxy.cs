using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000D3 RID: 211
	public class LookupCourseReusableClientProxy : WCFTokenBasedReusableClientProxy<ILookupCourse>, ILookupCourse, IService
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00015656 File Offset: 0x00013856
		public LookupCourseReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00015661 File Offset: 0x00013861
		public LookupCourseReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00015670 File Offset: 0x00013870
		public LoadCoursesBySubjectAndSessionResp LoadCoursesBySubjectAndSession(LoadCoursesBySubjectAndSessionReq request)
		{
			return this.WrapServiceMethod<LoadCoursesBySubjectAndSessionResp>(() => this.Proxy.LoadCoursesBySubjectAndSession(request));
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000156A8 File Offset: 0x000138A8
		public LoadCourseByLuCourseIdResp LoadCourseByLuCourseId(LoadCourseByLuCourseIdReq Request)
		{
			return this.WrapServiceMethod<LoadCourseByLuCourseIdResp>(() => this.Proxy.LoadCourseByLuCourseId(Request));
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000156E0 File Offset: 0x000138E0
		public CreateLookupCourseResp CreateLookupCourse(CreateLookupCourseReq Request)
		{
			return this.WrapServiceMethod<CreateLookupCourseResp>(() => this.Proxy.CreateLookupCourse(Request));
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00015718 File Offset: 0x00013918
		public void UpdateCourseInstructorExemption(UpdateCourseInstructorExemptionReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateCourseInstructorExemption(Request);
			});
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00015750 File Offset: 0x00013950
		public LoadCourseBasesBySearchStringResp LoadCourseBasesBySearchString(LoadCourseBasesBySearchStringReq Request)
		{
			return this.WrapServiceMethod<LoadCourseBasesBySearchStringResp>(() => this.Proxy.LoadCourseBasesBySearchString(Request));
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00015788 File Offset: 0x00013988
		public CreateLookupCourseBaseResp CreateLookupCourseBase(CreateLookupCourseBaseReq Request)
		{
			return this.WrapServiceMethod<CreateLookupCourseBaseResp>(() => this.Proxy.CreateLookupCourseBase(Request));
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000157C0 File Offset: 0x000139C0
		public LoadIsLookupCourseExemptFromDataSyncResp LoadIsLookupCourseExemptFromDataSync(LoadIsLookupCourseExemptFromDataSyncReq Request)
		{
			return this.WrapServiceMethod<LoadIsLookupCourseExemptFromDataSyncResp>(() => this.Proxy.LoadIsLookupCourseExemptFromDataSync(Request));
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000157F8 File Offset: 0x000139F8
		public void UpdateLookupCourseExemptionFromDataSync(UpdateLookupCourseExemptionFromDataSyncReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateLookupCourseExemptionFromDataSync(Request);
			});
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00015830 File Offset: 0x00013A30
		public LoadDurationTermSubjectsBySessionResp LoadDurationTermSubjectsBySession(LoadDurationTermSubjectsBySessionReq Request)
		{
			return this.WrapServiceMethod<LoadDurationTermSubjectsBySessionResp>(() => this.Proxy.LoadDurationTermSubjectsBySession(Request));
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00015868 File Offset: 0x00013A68
		public LoadStudentsCoursesBySessionResp LoadStudentsCoursesBySession(LoadStudentsCoursesBySessionReq request)
		{
			return this.WrapServiceMethod<LoadStudentsCoursesBySessionResp>(() => this.Proxy.LoadStudentsCoursesBySession(request));
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000158A0 File Offset: 0x00013AA0
		public LoadStudentsCoursesByDatesResp LoadStudentsCoursesByDates(LoadStudentsCoursesByDatesReq request)
		{
			return this.WrapServiceMethod<LoadStudentsCoursesByDatesResp>(() => this.Proxy.LoadStudentsCoursesByDates(request));
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000158D8 File Offset: 0x00013AD8
		public LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionResp LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq Request)
		{
			return this.WrapServiceMethod<LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionResp>(() => this.Proxy.LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(Request));
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00015910 File Offset: 0x00013B10
		public LoadUniqueCourseDateRangesBySessionResp LoadUniqueCourseDateRangesBySession(LoadUniqueCourseDateRangesBySessionReq Request)
		{
			return this.WrapServiceMethod<LoadUniqueCourseDateRangesBySessionResp>(() => this.Proxy.LoadUniqueCourseDateRangesBySession(Request));
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00015948 File Offset: 0x00013B48
		public UpdateCourseDateRangeResp UpdateCourseDateRange(UpdateCourseDateRangeReq Request)
		{
			return this.WrapServiceMethod<UpdateCourseDateRangeResp>(() => this.Proxy.UpdateCourseDateRange(Request));
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00015980 File Offset: 0x00013B80
		public LoadCoursesInDateRangeResp LoadCoursesInDateRange(LoadCoursesInDateRangeReq Request)
		{
			return this.WrapServiceMethod<LoadCoursesInDateRangeResp>(() => this.Proxy.LoadCoursesInDateRange(Request));
		}
	}
}
