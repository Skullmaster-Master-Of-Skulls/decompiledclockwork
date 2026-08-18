using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.LookupCourses
{
	// Token: 0x0200003E RID: 62
	public interface ILookupTimetableItemClientManager : IWebService
	{
		// Token: 0x060001CA RID: 458
		LookupTimetableItemDTO LoadLookupTimetableItem(int TimetableId);

		// Token: 0x060001CB RID: 459
		void SaveLookupTimetableItems(int LuCourseId, List<LookupTimetableItemDTO> items);
	}
}
