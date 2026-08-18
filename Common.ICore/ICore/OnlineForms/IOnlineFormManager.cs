using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.Common.ICore.OnlineForms
{
	// Token: 0x02000057 RID: 87
	public interface IOnlineFormManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000247 RID: 583
		List<OnlineForm> GetAllOnlineForms();

		// Token: 0x06000248 RID: 584
		List<OnlineForm> GetActiveOnlineForms();

		// Token: 0x06000249 RID: 585
		Task<List<OnlineForm>> GetActiveOnlineFormsAsync();

		// Token: 0x0600024A RID: 586
		OnlineForm GetOnlineForm(int OnlineFormId);

		// Token: 0x0600024B RID: 587
		void UpdateOnlineForm(OnlineForm OnlineForm);

		// Token: 0x0600024C RID: 588
		int CreateOnlineForm(OnlineForm OnlineForm);

		// Token: 0x0600024D RID: 589
		void DeleteOnlineForm(int SurveyId);

		// Token: 0x0600024E RID: 590
		void DisableOnlineForm(int SurveyId);

		// Token: 0x0600024F RID: 591
		void EnableOnlineForm(int SurveyId);
	}
}
