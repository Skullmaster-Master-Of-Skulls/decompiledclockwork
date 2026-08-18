using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000D7 RID: 215
	public class LookupInstructorManagementReusableClientProxy : WCFTokenBasedReusableClientProxy<ILookupInstructorManagement>, ILookupInstructorManagement, IService
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x00016176 File Offset: 0x00014376
		public LookupInstructorManagementReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00016181 File Offset: 0x00014381
		public LookupInstructorManagementReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00016190 File Offset: 0x00014390
		public LoadLookupInstructorsForManagementResp LoadLookupInstructorsForManagement(LoadLookupInstructorsForManagementReq Request)
		{
			return this.WrapServiceMethod<LoadLookupInstructorsForManagementResp>(() => this.Proxy.LoadLookupInstructorsForManagement(Request));
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000161C8 File Offset: 0x000143C8
		public DeleteInstructorResp DeleteInstructor(DeleteInstructorReq Request)
		{
			return this.WrapServiceMethod<DeleteInstructorResp>(() => this.Proxy.DeleteInstructor(Request));
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00016200 File Offset: 0x00014400
		public MergeInstructorsResp MergeInstructors(MergeInstructorsReq Request)
		{
			return this.WrapServiceMethod<MergeInstructorsResp>(() => this.Proxy.MergeInstructors(Request));
		}
	}
}
