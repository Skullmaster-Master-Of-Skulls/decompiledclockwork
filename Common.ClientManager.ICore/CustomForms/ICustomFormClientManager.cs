using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.CustomForms
{
	// Token: 0x0200006A RID: 106
	public interface ICustomFormClientManager : IWebService
	{
		// Token: 0x06000319 RID: 793
		Task<CustomFormDTO> LoadFormByIdAsync(Guid formId);

		// Token: 0x0600031A RID: 794
		CustomFormDTO LoadFormById(Guid formId);

		// Token: 0x0600031B RID: 795
		Forest<CustomControlBaseDTO> LoadFormForestById(Guid formId);

		// Token: 0x0600031C RID: 796
		Task<Forest<CustomControlBaseDTO>> LoadFormForestByIdAsync(Guid formId);

		// Token: 0x0600031D RID: 797
		Task<Guid> CreateFormAsync(CustomFormDTO form);

		// Token: 0x0600031E RID: 798
		Task DeleteFormAsync(Guid formId);

		// Token: 0x0600031F RID: 799
		Task UpdateFormAsync(CustomFormDTO form);

		// Token: 0x06000320 RID: 800
		Task<IList<CustomFormDTO>> LoadAllCustomFormsAsync();
	}
}
