using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.Context;

namespace TechnoPro.Common.ICore.CustomForms
{
	// Token: 0x020000AB RID: 171
	public interface ICustomDataManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000512 RID: 1298
		Task<CustomDataSet> LoadDataAsync(CustomDataContext context, params Guid[] dataInstanceIds);

		// Token: 0x06000513 RID: 1299
		CustomDataSet LoadData(CustomDataContext context, params Guid[] dataInstanceIds);

		// Token: 0x06000514 RID: 1300
		Task SaveCustomFormsDataAsync(CustomDataSet dataSet, params Guid[] dataInstanceIds);
	}
}
