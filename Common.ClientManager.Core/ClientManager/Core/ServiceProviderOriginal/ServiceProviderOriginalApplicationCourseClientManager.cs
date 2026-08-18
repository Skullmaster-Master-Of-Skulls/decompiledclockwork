using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ServiceProviderOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.ServiceProviderOriginal
{
	// Token: 0x02000022 RID: 34
	public class ServiceProviderOriginalApplicationCourseClientManager : IServiceProviderOriginalApplicationCourseClientManager, IWebService
	{
		// Token: 0x06000111 RID: 273 RVA: 0x0000639C File Offset: 0x0000459C
		public IList<LookupCourseBaseDTO> GetProviderCourses(int ServiceProviderId, DateTime StartDate, DateTime EndDate, int ServiceProviderType)
		{
			GetProviderCoursesReq getProviderCoursesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetProviderCoursesReq>();
			getProviderCoursesReq.ServiceProviderId = ServiceProviderId;
			getProviderCoursesReq.StartDate = StartDate;
			getProviderCoursesReq.EndDate = EndDate;
			getProviderCoursesReq.ServiceProviderType = ServiceProviderType;
			return ClientServiceFactory.GetClientInstance<IServiceProviderOriginalApplicationCourse>().GetProviderCourses(getProviderCoursesReq).CourseBases;
		}
	}
}
