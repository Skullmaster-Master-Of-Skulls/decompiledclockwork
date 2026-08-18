using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.ServiceProviderOriginal
{
	// Token: 0x0200001F RID: 31
	public interface IServiceProviderOriginalApplicationCourseClientManager : IWebService
	{
		// Token: 0x060000C9 RID: 201
		IList<LookupCourseBaseDTO> GetProviderCourses(int ServiceProviderId, DateTime StartDate, DateTime EndDate, int ServiceProviderType);
	}
}
