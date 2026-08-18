using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Form;

namespace TechnoPro.Common.ICore.CustomForms
{
	// Token: 0x020000AE RID: 174
	public interface ICustomFormManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000522 RID: 1314
		Task<IList<CustomForm>> LoadAllCustomForms();

		// Token: 0x06000523 RID: 1315
		Task<CustomForm> LoadFormByIdAsync(Guid formId);

		// Token: 0x06000524 RID: 1316
		CustomForm LoadFormById(Guid formId);

		// Token: 0x06000525 RID: 1317
		Task<Guid> CreateFormAsync(CustomForm form);

		// Token: 0x06000526 RID: 1318
		Task DeleteFormAsync(Guid formId);

		// Token: 0x06000527 RID: 1319
		Task UpdateFormAsync(CustomForm form);
	}
}
