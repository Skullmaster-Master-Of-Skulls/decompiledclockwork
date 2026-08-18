using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000121 RID: 289
	public class ServiceProviderOriginalApplicationCourseReusableClientProxy : WCFTokenBasedReusableClientProxy<IServiceProviderOriginalApplicationCourse>, IServiceProviderOriginalApplicationCourse, IService
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x0001D2D2 File Offset: 0x0001B4D2
		public ServiceProviderOriginalApplicationCourseReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0001D2DD File Offset: 0x0001B4DD
		public ServiceProviderOriginalApplicationCourseReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0001D2EC File Offset: 0x0001B4EC
		public GetProviderCoursesResp GetProviderCourses(GetProviderCoursesReq Request)
		{
			return this.WrapServiceMethod<GetProviderCoursesResp>(() => this.Proxy.GetProviderCourses(Request));
		}
	}
}
