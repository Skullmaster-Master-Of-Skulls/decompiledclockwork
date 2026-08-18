using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.ICore.CustomForms
{
	// Token: 0x020000AC RID: 172
	public interface ICustomFieldManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000515 RID: 1301
		Task<Guid> CreateDataInstanceAsync(CustomDataInstance dataInstance);

		// Token: 0x06000516 RID: 1302
		Task DeleteDataInstanceAsync(Guid dataInstanceId);

		// Token: 0x06000517 RID: 1303
		Task UpdateDataInstanceAsync(CustomDataInstance dataInstance);
	}
}
