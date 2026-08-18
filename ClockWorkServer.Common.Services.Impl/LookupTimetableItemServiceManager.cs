using System;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000064 RID: 100
	public class LookupTimetableItemServiceManager : ILookupTimetableItem, IService
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x000113A4 File Offset: 0x0000F5A4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000113B8 File Offset: 0x0000F5B8
		public LoadLookupTimetableItemResp LoadLookupTimetableItem(LoadLookupTimetableItemReq Request)
		{
			ILookupTimetableItemManager lookupTimetableItemManager = new LookupTimetableItemManager(Request.GetOperationContext());
			LookupTimetableItem lookupTimetableItem = lookupTimetableItemManager.LoadLookupTimetableItem(Request.TimetableId);
			return new LoadLookupTimetableItemResp
			{
				TimetableItem = ((lookupTimetableItem == null) ? null : lookupTimetableItem.ToDTO())
			};
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000113FC File Offset: 0x0000F5FC
		public void SaveLookupTimetableItems(SaveLookupTimetableItemsReq Request)
		{
			ILookupTimetableItemManager lookupTimetableItemManager = new LookupTimetableItemManager(Request.GetOperationContext());
			lookupTimetableItemManager.SaveLookupTimetableItems(Request.LuCourseId, Request.TimetableItems.ToList<LookupTimetableItemDTO>().ConvertAll<LookupTimetableItem>((LookupTimetableItemDTO f) => f.ToDomainObject()));
		}
	}
}
