using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000D8 RID: 216
	internal class LookupInstructorManagementClientBaseProxy : ClientBase<ILookupInstructorManagement>, ILookupInstructorManagement, IService
	{
		// Token: 0x0600087C RID: 2172 RVA: 0x00016238 File Offset: 0x00014438
		public LookupInstructorManagementClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00016243 File Offset: 0x00014443
		public LookupInstructorManagementClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00016250 File Offset: 0x00014450
		public LoadLookupInstructorsForManagementResp LoadLookupInstructorsForManagement(LoadLookupInstructorsForManagementReq Request)
		{
			return base.Channel.LoadLookupInstructorsForManagement(Request);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00016270 File Offset: 0x00014470
		public DeleteInstructorResp DeleteInstructor(DeleteInstructorReq Request)
		{
			return base.Channel.DeleteInstructor(Request);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00016290 File Offset: 0x00014490
		public MergeInstructorsResp MergeInstructors(MergeInstructorsReq Request)
		{
			return base.Channel.MergeInstructors(Request);
		}
	}
}
