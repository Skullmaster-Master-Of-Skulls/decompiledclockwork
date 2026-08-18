using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.People
{
	// Token: 0x02000029 RID: 41
	public interface IGroupClientManager : IWebService
	{
		// Token: 0x0600010B RID: 267
		GroupDTO LoadGroupByTitle(string groupTitle, string altGroupTitle);

		// Token: 0x0600010C RID: 268
		int CreateGroupByTitle(string groupTitle);

		// Token: 0x0600010D RID: 269
		GroupDTO LoadGroupById(int GroupId);

		// Token: 0x0600010E RID: 270
		IList<GroupDTO> LoadAllowedGroups(bool OnlyReturnVisibleInCalendarGroups);

		// Token: 0x0600010F RID: 271
		IList<GroupContainerDTO> LoadAllGroupContainers();

		// Token: 0x06000110 RID: 272
		IList<GroupForEditDTO> LoadAllGroupForEdits();
	}
}
