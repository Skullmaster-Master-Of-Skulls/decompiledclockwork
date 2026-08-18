using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x0200004F RID: 79
	public class ServiceProviderApplicationStatusLookupManager : IServiceProviderApplicationStatusLookupManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00011DFC File Offset: 0x0000FFFC
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00011E04 File Offset: 0x00010004
		public IServiceProviderApplicationStatusLookupDAO dao { get; set; }

		// Token: 0x06000345 RID: 837 RVA: 0x00011E0D File Offset: 0x0001000D
		public ServiceProviderApplicationStatusLookupManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderApplicationStatusLookupDAO(this.OpContext.GetProviderTypes());
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00011E36 File Offset: 0x00010036
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00011E3E File Offset: 0x0001003E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000348 RID: 840 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<ServiceProviderApplicationStatus> LoadAllStatusLookupItems()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000072EA File Offset: 0x000054EA
		public int CreateStatusLookupItem(ServiceProviderApplicationStatus Item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000072EA File Offset: 0x000054EA
		public void DeleteStatusLookupItem(int ServiceProviderApplicationStatusLookupId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000072EA File Offset: 0x000054EA
		public int UpdateStatusLookupItem(ServiceProviderApplicationStatus Item)
		{
			throw new NotImplementedException();
		}
	}
}
