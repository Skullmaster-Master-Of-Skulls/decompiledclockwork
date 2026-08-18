using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Core.Mappers.CourseRegistrations;
using TechnoPro.Common.Core.Mappers.ServiceProvider;
using TechnoPro.Common.Core.ServiceProvider;
using TechnoPro.Common.ICore.ServiceProviders;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000083 RID: 131
	public class ServiceProviderCourseRegistrationServiceManager : IServiceProviderCourseRegistration, IService
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x00016C64 File Offset: 0x00014E64
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00016C78 File Offset: 0x00014E78
		public LoadCourseRegistrationsByProviderResp LoadCourseRegistrationsByProvider(LoadCourseRegistrationsByProviderReq Request)
		{
			IServiceProviderCourseRegistrationManager serviceProviderCourseRegistrationManager = new ServiceProviderCourseRegistrationManager(Request.GetOperationContext());
			IList<SPProviderCourseRegistration> list = serviceProviderCourseRegistrationManager.LoadCourseRegistrationsByProvider(Request.SPProviderId, Request.StartDate, Request.EndDate);
			LoadCourseRegistrationsByProviderResp loadCourseRegistrationsByProviderResp = new LoadCourseRegistrationsByProviderResp();
			IList<SPProviderCourseRegistrationDTO> courseRegistrations;
			if (list != null)
			{
				courseRegistrations = list.ToList<SPProviderCourseRegistration>().ConvertAll<SPProviderCourseRegistrationDTO>((SPProviderCourseRegistration f) => f.ToDTO());
			}
			else
			{
				courseRegistrations = null;
			}
			loadCourseRegistrationsByProviderResp.CourseRegistrations = courseRegistrations;
			return loadCourseRegistrationsByProviderResp;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00016CEC File Offset: 0x00014EEC
		public LoadCourseRegistrationByIdResp LoadCourseRegistrationById(LoadCourseRegistrationByIdReq Request)
		{
			IServiceProviderCourseRegistrationManager serviceProviderCourseRegistrationManager = new ServiceProviderCourseRegistrationManager(Request.GetOperationContext());
			SPProviderCourseRegistration spproviderCourseRegistration = serviceProviderCourseRegistrationManager.LoadCourseRegistrationById(Request.SPProviderCourseRegistrationId);
			return new LoadCourseRegistrationByIdResp
			{
				CourseRegistration = ((spproviderCourseRegistration == null) ? null : spproviderCourseRegistration.ToDTO())
			};
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00016D30 File Offset: 0x00014F30
		public UpdateCourseRegistrationStatusResp UpdateCourseRegistrationStatus(UpdateCourseRegistrationStatusReq Request)
		{
			IServiceProviderCourseRegistrationManager serviceProviderCourseRegistrationManager = new ServiceProviderCourseRegistrationManager(Request.GetOperationContext());
			serviceProviderCourseRegistrationManager.UpdateCourseRegistrationStatus(Request.SPProviderCourseRegistrationId, Request.NewCourseRegistrationStatus.ToDomainObject());
			return new UpdateCourseRegistrationStatusResp();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00016D6C File Offset: 0x00014F6C
		public UpdateCourseRegistrationResp UpdateCourseRegistration(UpdateCourseRegistrationReq Request)
		{
			IServiceProviderCourseRegistrationManager serviceProviderCourseRegistrationManager = new ServiceProviderCourseRegistrationManager(Request.GetOperationContext());
			serviceProviderCourseRegistrationManager.UpdateCourseRegistration(Request.CourseRegistration.ToDomainObject());
			return new UpdateCourseRegistrationResp();
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00016DA4 File Offset: 0x00014FA4
		public DeleteCourseRegistrationResp DeleteCourseRegistration(DeleteCourseRegistrationReq Request)
		{
			IServiceProviderCourseRegistrationManager serviceProviderCourseRegistrationManager = new ServiceProviderCourseRegistrationManager(Request.GetOperationContext());
			serviceProviderCourseRegistrationManager.DeleteCourseRegistration(Request.SPProviderCourseRegistrationId);
			return new DeleteCourseRegistrationResp();
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00016DD4 File Offset: 0x00014FD4
		public CreateCourseRegistrationResp CreateCourseRegistration(CreateCourseRegistrationReq Request)
		{
			IServiceProviderCourseRegistrationManager serviceProviderCourseRegistrationManager = new ServiceProviderCourseRegistrationManager(Request.GetOperationContext());
			int spproviderCourseRegistrationID = serviceProviderCourseRegistrationManager.CreateCourseRegistration(Request.CourseRegistration.ToDomainObject());
			return new CreateCourseRegistrationResp
			{
				SPProviderCourseRegistrationID = spproviderCourseRegistrationID
			};
		}
	}
}
