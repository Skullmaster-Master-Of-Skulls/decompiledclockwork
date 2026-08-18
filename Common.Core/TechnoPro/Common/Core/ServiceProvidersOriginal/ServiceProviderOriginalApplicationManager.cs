using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000052 RID: 82
	public class ServiceProviderOriginalApplicationManager : IServiceProviderOriginalApplicationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00011F02 File Offset: 0x00010102
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00011F0A File Offset: 0x0001010A
		public IServiceProviderOriginalApplicationDAO dao { get; set; }

		// Token: 0x06000359 RID: 857 RVA: 0x00011F13 File Offset: 0x00010113
		public ServiceProviderOriginalApplicationManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderOriginalApplicationDAO(this.OpContext.GetProviderTypes());
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00011F3C File Offset: 0x0001013C
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00011F44 File Offset: 0x00010144
		public OperationContext OpContext { get; set; }

		// Token: 0x0600035C RID: 860 RVA: 0x000072EA File Offset: 0x000054EA
		public int CreateApplication(ServiceProviderApplication Application)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000072EA File Offset: 0x000054EA
		public void DeleteApplication(int ServiceProviderApplicationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateApplication(ServiceProviderApplication Application)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000072EA File Offset: 0x000054EA
		public ServiceProviderApplication LoadApplicationByProviderAndTypeAndDate(int ServiceProviderId, int ServiceProviderTypeId, DateTime StartDate, DateTime EndDate)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<ServiceProviderApplication> LoadApplicationsByTypeAndDate(int ServiceProviderTypeId, DateTime StartDate, DateTime EndDate)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<ServiceProviderApplicationBase> LoadApplicationBasesByTypeAndDate(int ServiceProviderTypeId, DateTime StartDate, DateTime EndDate)
		{
			throw new NotImplementedException();
		}
	}
}
