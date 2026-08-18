using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.DAO.CustomForms
{
	// Token: 0x02000093 RID: 147
	public interface ICustomFieldDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003CB RID: 971
		Task<Guid> CreateDataInstanceAsync(CustomDataInstance dataInstance);

		// Token: 0x060003CC RID: 972
		Task DeleteDataInstanceAsync(Guid dataInstanceId);

		// Token: 0x060003CD RID: 973
		Task UpdateDataInstanceAsync(CustomDataInstance dataInstance);
	}
}
