using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.ICore.ServiceProviders
{
	// Token: 0x0200003E RID: 62
	public interface IServiceProviderTypeManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000198 RID: 408
		SPProviderType LoadProviderTypeById(int SPProviderTypeId);

		// Token: 0x06000199 RID: 409
		IList<SPProviderType> LoadProviderTypeByBehaviourCode(eProviderTypeBehaviourCode Code);

		// Token: 0x0600019A RID: 410
		IList<SPProviderType> LoadAllProviderTypes();

		// Token: 0x0600019B RID: 411
		int CreateProviderType(SPProviderType ProviderType);

		// Token: 0x0600019C RID: 412
		void UpdateProviderType(SPProviderType ProviderType);

		// Token: 0x0600019D RID: 413
		void DeleteProviderType(int SPProviderTypeId);
	}
}
