using System;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000050 RID: 80
	public class ServiceProviderOriginalApplicationAvailabilityManager : IServiceProviderOriginalApplicationAvailabilityManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00011E47 File Offset: 0x00010047
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00011E4F File Offset: 0x0001004F
		public IServiceProviderOriginalApplicationAvailabilityDAO dao { get; set; }

		// Token: 0x0600034E RID: 846 RVA: 0x00011E58 File Offset: 0x00010058
		public ServiceProviderOriginalApplicationAvailabilityManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderOriginalApplicationAvailabilityDAO(this.OpContext.GetProviderTypes());
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00011E81 File Offset: 0x00010081
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00011E89 File Offset: 0x00010089
		public OperationContext OpContext { get; set; }
	}
}
