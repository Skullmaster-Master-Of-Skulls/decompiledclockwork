using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000D4 RID: 212
	internal class LookupCourseClientBaseProxy : ClientBase<ILookupCourse>, ILookupCourse, IService
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x000159B8 File Offset: 0x00013BB8
		public LookupCourseClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000159C3 File Offset: 0x00013BC3
		public LookupCourseClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000159D0 File Offset: 0x00013BD0
		public LoadCoursesBySubjectAndSessionResp LoadCoursesBySubjectAndSession(LoadCoursesBySubjectAndSessionReq request)
		{
			return base.Channel.LoadCoursesBySubjectAndSession(request);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000159F0 File Offset: 0x00013BF0
		public LoadCourseByLuCourseIdResp LoadCourseByLuCourseId(LoadCourseByLuCourseIdReq Request)
		{
			return base.Channel.LoadCourseByLuCourseId(Request);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00015A10 File Offset: 0x00013C10
		public CreateLookupCourseResp CreateLookupCourse(CreateLookupCourseReq Request)
		{
			return base.Channel.CreateLookupCourse(Request);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00015A2E File Offset: 0x00013C2E
		public void UpdateCourseInstructorExemption(UpdateCourseInstructorExemptionReq Request)
		{
			base.Channel.UpdateCourseInstructorExemption(Request);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00015A40 File Offset: 0x00013C40
		public LoadCourseBasesBySearchStringResp LoadCourseBasesBySearchString(LoadCourseBasesBySearchStringReq Request)
		{
			return base.Channel.LoadCourseBasesBySearchString(Request);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00015A60 File Offset: 0x00013C60
		public CreateLookupCourseBaseResp CreateLookupCourseBase(CreateLookupCourseBaseReq Request)
		{
			return base.Channel.CreateLookupCourseBase(Request);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00015A80 File Offset: 0x00013C80
		public LoadIsLookupCourseExemptFromDataSyncResp LoadIsLookupCourseExemptFromDataSync(LoadIsLookupCourseExemptFromDataSyncReq Request)
		{
			return base.Channel.LoadIsLookupCourseExemptFromDataSync(Request);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00015A9E File Offset: 0x00013C9E
		public void UpdateLookupCourseExemptionFromDataSync(UpdateLookupCourseExemptionFromDataSyncReq Request)
		{
			base.Channel.UpdateLookupCourseExemptionFromDataSync(Request);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00015AB0 File Offset: 0x00013CB0
		public LoadDurationTermSubjectsBySessionResp LoadDurationTermSubjectsBySession(LoadDurationTermSubjectsBySessionReq Request)
		{
			return base.Channel.LoadDurationTermSubjectsBySession(Request);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00015AD0 File Offset: 0x00013CD0
		public LoadStudentsCoursesBySessionResp LoadStudentsCoursesBySession(LoadStudentsCoursesBySessionReq request)
		{
			return base.Channel.LoadStudentsCoursesBySession(request);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00015AF0 File Offset: 0x00013CF0
		public LoadStudentsCoursesByDatesResp LoadStudentsCoursesByDates(LoadStudentsCoursesByDatesReq request)
		{
			return base.Channel.LoadStudentsCoursesByDates(request);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00015B10 File Offset: 0x00013D10
		public LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionResp LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq Request)
		{
			return base.Channel.LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(Request);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00015B30 File Offset: 0x00013D30
		public LoadUniqueCourseDateRangesBySessionResp LoadUniqueCourseDateRangesBySession(LoadUniqueCourseDateRangesBySessionReq Request)
		{
			return base.Channel.LoadUniqueCourseDateRangesBySession(Request);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00015B50 File Offset: 0x00013D50
		public UpdateCourseDateRangeResp UpdateCourseDateRange(UpdateCourseDateRangeReq Request)
		{
			return base.Channel.UpdateCourseDateRange(Request);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00015B70 File Offset: 0x00013D70
		public LoadCoursesInDateRangeResp LoadCoursesInDateRange(LoadCoursesInDateRangeReq Request)
		{
			return base.Channel.LoadCoursesInDateRange(Request);
		}
	}
}
