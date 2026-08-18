using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Campus
{
	// Token: 0x02000074 RID: 116
	public interface ICampusClientManager : IWebService
	{
		// Token: 0x0600035D RID: 861
		IList<SchoolCampusDTO> GetCampusList();

		// Token: 0x0600035E RID: 862
		int CreateCampus(SchoolCampusDTO campus);

		// Token: 0x0600035F RID: 863
		void UpdateCampus(SchoolCampusDTO campus);

		// Token: 0x06000360 RID: 864
		void DeleteCampus(int campusId);
	}
}
