using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.DAO.ServiceProvider
{
	// Token: 0x02000031 RID: 49
	public interface IServiceProviderApplicationDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000CF RID: 207
		SPApplication LoadApplicationById(int SPApplicationId);
	}
}
