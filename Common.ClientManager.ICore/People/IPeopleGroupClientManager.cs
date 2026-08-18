using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.People
{
	// Token: 0x0200002B RID: 43
	public interface IPeopleGroupClientManager : IWebService
	{
		// Token: 0x06000120 RID: 288
		IList<PersonBaseDTO> LoadUsersByGroupTitle(string GroupTitle, string AlternateGroupTitle = null);
	}
}
