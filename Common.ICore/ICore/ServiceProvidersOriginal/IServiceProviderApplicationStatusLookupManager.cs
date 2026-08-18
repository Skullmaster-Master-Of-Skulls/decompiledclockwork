using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.ICore.ServiceProvidersOriginal
{
	// Token: 0x02000042 RID: 66
	public interface IServiceProviderApplicationStatusLookupManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001B8 RID: 440
		IList<ServiceProviderApplicationStatus> LoadAllStatusLookupItems();

		// Token: 0x060001B9 RID: 441
		int CreateStatusLookupItem(ServiceProviderApplicationStatus Item);

		// Token: 0x060001BA RID: 442
		void DeleteStatusLookupItem(int ServiceProviderApplicationStatusLookupId);

		// Token: 0x060001BB RID: 443
		int UpdateStatusLookupItem(ServiceProviderApplicationStatus Item);
	}
}
