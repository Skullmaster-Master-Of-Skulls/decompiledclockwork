using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal.ContractParameters;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Core.ServiceProvidersOriginal;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200007F RID: 127
	public class ServiceProviderOriginalApplicationCourseServiceManager : IServiceProviderOriginalApplicationCourse, IService
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x00016888 File Offset: 0x00014A88
		public GetProviderCoursesResp GetProviderCourses(GetProviderCoursesReq Request)
		{
			IServiceProviderOriginalApplicationCourseManager serviceProviderOriginalApplicationCourseManager = new ServiceProviderOriginalApplicationCourseManager(Request.GetOperationContext());
			IList<LookupCourseBase> providerCourses = serviceProviderOriginalApplicationCourseManager.GetProviderCourses(Request.ServiceProviderId, Request.StartDate, Request.EndDate, Request.ServiceProviderType);
			GetProviderCoursesResp getProviderCoursesResp = new GetProviderCoursesResp();
			IList<LookupCourseBaseDTO> courseBases;
			if (providerCourses != null)
			{
				courseBases = providerCourses.ToList<LookupCourseBase>().ConvertAll<LookupCourseBaseDTO>((LookupCourseBase g) => g.ToDTO());
			}
			else
			{
				courseBases = null;
			}
			getProviderCoursesResp.CourseBases = courseBases;
			return getProviderCoursesResp;
		}
	}
}
