using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Form;

namespace TechnoPro.Common.DAO.CustomForms
{
	// Token: 0x02000094 RID: 148
	public interface ICustomFormDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003CE RID: 974
		Task<CustomForm> LoadFormByIdAsync(Guid formId);

		// Token: 0x060003CF RID: 975
		CustomForm LoadFormById(Guid formId);

		// Token: 0x060003D0 RID: 976
		Task<Guid> CreateFormAsync(CustomForm form);

		// Token: 0x060003D1 RID: 977
		Task DeleteFormAsync(Guid formId);

		// Token: 0x060003D2 RID: 978
		Task UpdateFormAsync(CustomForm form);

		// Token: 0x060003D3 RID: 979
		Task<IList<CustomForm>> LoadAllCustomFormsAsync();
	}
}
