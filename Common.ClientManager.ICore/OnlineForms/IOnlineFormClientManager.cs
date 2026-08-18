using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.OnlineForms
{
	// Token: 0x02000030 RID: 48
	public interface IOnlineFormClientManager : IWebService
	{
		// Token: 0x06000140 RID: 320
		IList<OnlineFormDTO> GetAllOnlineForms();

		// Token: 0x06000141 RID: 321
		IList<OnlineFormDTO> GetActiveOnlineForms();

		// Token: 0x06000142 RID: 322
		Task<IList<OnlineFormDTO>> GetActiveOnlineFormsAsync();

		// Token: 0x06000143 RID: 323
		OnlineFormDTO GetOnlineForm(int OnlineFormId);

		// Token: 0x06000144 RID: 324
		void UpdateOnlineForm(OnlineFormDTO OnlineForm);

		// Token: 0x06000145 RID: 325
		int CreateNewOnlineForm(OnlineFormDTO OnlineForm);

		// Token: 0x06000146 RID: 326
		void DeleteOnlineForm(int OnlineFormId);

		// Token: 0x06000147 RID: 327
		void DisableOnlineForm(int OnlineFormId);

		// Token: 0x06000148 RID: 328
		void EnableOnlineForm(int OnlineFormId);
	}
}
