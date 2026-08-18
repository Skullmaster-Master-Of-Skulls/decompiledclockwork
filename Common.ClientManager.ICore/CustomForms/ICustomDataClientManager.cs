using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.CustomForms
{
	// Token: 0x02000068 RID: 104
	public interface ICustomDataClientManager : IWebService
	{
		// Token: 0x06000313 RID: 787
		Task<CustomDataSetDTO> LoadDataAsync(CustomDataContextDTO context, IList<Guid> dataInstanceIds);

		// Token: 0x06000314 RID: 788
		CustomDataSetDTO LoadData(CustomDataContextDTO context, IList<Guid> dataInstanceIds);

		// Token: 0x06000315 RID: 789
		Task SaveCustomFormsDataAsync(CustomDataSetDTO dataSet, IList<Guid> dataInstanceIds);
	}
}
