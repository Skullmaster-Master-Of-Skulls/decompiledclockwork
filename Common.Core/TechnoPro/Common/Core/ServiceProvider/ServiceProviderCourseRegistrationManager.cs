using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.ServiceProvider;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.ICore.ServiceProviders;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.ServiceProvider
{
	// Token: 0x0200004A RID: 74
	public class ServiceProviderCourseRegistrationManager : IServiceProviderCourseRegistrationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00011B33 File Offset: 0x0000FD33
		public ServiceProviderCourseRegistrationManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderCourseRegistrationDAO(opContext);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00011B51 File Offset: 0x0000FD51
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00011B59 File Offset: 0x0000FD59
		public OperationContext OpContext { get; set; }

		// Token: 0x06000311 RID: 785 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<SPProviderCourseRegistration> LoadCourseRegistrationsByProvider(int SPProviderId, DateTime StartDate, DateTime EndDate)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000072EA File Offset: 0x000054EA
		public SPProviderCourseRegistration LoadCourseRegistrationById(int SPProviderCourseRegistrationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateCourseRegistrationStatus(int SPProviderCourseRegistrationId, CourseRegistrationStatus NewStatus)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateCourseRegistration(SPProviderCourseRegistration ProviderCourseRegistration)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000072EA File Offset: 0x000054EA
		public void DeleteCourseRegistration(int SPProviderCourseRegistrationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000072EA File Offset: 0x000054EA
		public int CreateCourseRegistration(SPProviderCourseRegistration ProviderCourseRegistration)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000093 RID: 147
		public IServiceProviderCourseRegistrationDAO dao;
	}
}
