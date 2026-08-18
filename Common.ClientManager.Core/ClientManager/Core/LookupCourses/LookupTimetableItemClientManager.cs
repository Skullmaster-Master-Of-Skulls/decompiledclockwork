using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.LookupCourses
{
	// Token: 0x02000043 RID: 67
	public class LookupTimetableItemClientManager : ILookupTimetableItemClientManager, IWebService
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public LookupTimetableItemDTO LoadLookupTimetableItem(int TimetableId)
		{
			LoadLookupTimetableItemReq loadLookupTimetableItemReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLookupTimetableItemReq>();
			loadLookupTimetableItemReq.TimetableId = TimetableId;
			return ClientServiceFactory.GetClientInstance<ILookupTimetableItem>().LoadLookupTimetableItem(loadLookupTimetableItemReq).TimetableItem;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000B900 File Offset: 0x00009B00
		public void SaveLookupTimetableItems(int LuCourseId, List<LookupTimetableItemDTO> items)
		{
			SaveLookupTimetableItemsReq saveLookupTimetableItemsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveLookupTimetableItemsReq>();
			saveLookupTimetableItemsReq.LuCourseId = LuCourseId;
			saveLookupTimetableItemsReq.TimetableItems = items;
			ClientServiceFactory.GetClientInstance<ILookupTimetableItem>().SaveLookupTimetableItems(saveLookupTimetableItemsReq);
		}
	}
}
