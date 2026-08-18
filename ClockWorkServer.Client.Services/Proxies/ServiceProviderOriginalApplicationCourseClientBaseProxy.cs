using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000122 RID: 290
	internal class ServiceProviderOriginalApplicationCourseClientBaseProxy : ClientBase<IServiceProviderOriginalApplicationCourse>, IServiceProviderOriginalApplicationCourse, IService
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x0001D324 File Offset: 0x0001B524
		public ServiceProviderOriginalApplicationCourseClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0001D32F File Offset: 0x0001B52F
		public ServiceProviderOriginalApplicationCourseClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0001D33C File Offset: 0x0001B53C
		public GetProviderCoursesResp GetProviderCourses(GetProviderCoursesReq Request)
		{
			return base.Channel.GetProviderCourses(Request);
		}
	}
}
