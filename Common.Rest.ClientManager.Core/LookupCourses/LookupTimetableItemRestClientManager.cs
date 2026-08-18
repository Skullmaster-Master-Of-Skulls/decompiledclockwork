using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.LookupCourses
{
	// Token: 0x02000038 RID: 56
	public class LookupTimetableItemRestClientManager : BearerTokenRestProxy<ILookupTimetableItemClientManager>, ILookupTimetableItemClientManager, IWebService
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00007078 File Offset: 0x00005278
		public LookupTimetableItemRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007082 File Offset: 0x00005282
		public LookupTimetableItemRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000708D File Offset: 0x0000528D
		public LookupTimetableItemDTO LoadLookupTimetableItem(int TimetableId)
		{
			return base.Get<LookupTimetableItemDTO>(string.Format("lookuptimetableitem/timetableid/{0}", TimetableId), true);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000070A8 File Offset: 0x000052A8
		public void SaveLookupTimetableItems(int LuCourseId, List<LookupTimetableItemDTO> items)
		{
			SaveLookupTimetableItemsReq saveLookupTimetableItemsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveLookupTimetableItemsReq>();
			saveLookupTimetableItemsReq.LuCourseId = LuCourseId;
			saveLookupTimetableItemsReq.TimetableItems = items;
			base.Post<SaveLookupTimetableItemsReq>(saveLookupTimetableItemsReq, "lookuptimetableitem");
		}
	}
}
