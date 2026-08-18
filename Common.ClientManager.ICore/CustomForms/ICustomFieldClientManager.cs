using System;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.CustomForms
{
	// Token: 0x02000069 RID: 105
	public interface ICustomFieldClientManager : IWebService
	{
		// Token: 0x06000316 RID: 790
		Task<Guid> CreateDataInstanceAsync(CustomDataInstanceDTO dataInstance);

		// Token: 0x06000317 RID: 791
		Task DeleteDataInstanceAsync(Guid dataInstanceId);

		// Token: 0x06000318 RID: 792
		Task UpdateDataInstanceAsync(CustomDataInstanceDTO dataInstance);
	}
}
