using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Vets
{
	// Token: 0x02000004 RID: 4
	public interface IVetsChapterClientManager : IWebService
	{
		// Token: 0x0600000E RID: 14
		Task<IList<VetsChapterDTO>> GetChaptersAsync();

		// Token: 0x0600000F RID: 15
		IList<VetsChapterDTO> GetChapters();
	}
}
