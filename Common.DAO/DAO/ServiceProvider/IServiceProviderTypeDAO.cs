using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.DAO.ServiceProvider
{
	// Token: 0x02000036 RID: 54
	public interface IServiceProviderTypeDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000DF RID: 223
		IList<SPProviderType> LoadActiveProviderTypes();
	}
}
