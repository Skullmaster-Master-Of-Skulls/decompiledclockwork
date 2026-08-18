using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.Common.DAO.OnlineForms
{
	// Token: 0x02000045 RID: 69
	public interface IOnlineFormDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000162 RID: 354
		List<OnlineForm> GetAllOnlineForms();

		// Token: 0x06000163 RID: 355
		List<OnlineForm> GetActiveOnlineForms();

		// Token: 0x06000164 RID: 356
		OnlineForm GetOnlineForm(int OnlineFormId);

		// Token: 0x06000165 RID: 357
		int CreateNewOnlineForm(OnlineForm OnlineForm);

		// Token: 0x06000166 RID: 358
		void UpdateOnlineForm(OnlineForm OnlineForm);

		// Token: 0x06000167 RID: 359
		void DeleteOnlineForm(int OnlineFormId);

		// Token: 0x06000168 RID: 360
		void DisableOnlineForm(int OnlineFormId);

		// Token: 0x06000169 RID: 361
		void EnableOnlineForm(int OnlineFormId);

		// Token: 0x0600016A RID: 362
		Task<List<OnlineForm>> GetActiveOnlineFormsAsync();
	}
}
