using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000DC RID: 220
	internal class LookupTimetableItemClientBaseProxy : ClientBase<ILookupTimetableItem>, ILookupTimetableItem, IService
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x000165CD File Offset: 0x000147CD
		public LookupTimetableItemClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000165D8 File Offset: 0x000147D8
		public LookupTimetableItemClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000165E4 File Offset: 0x000147E4
		public LoadLookupTimetableItemResp LoadLookupTimetableItem(LoadLookupTimetableItemReq Request)
		{
			return base.Channel.LoadLookupTimetableItem(Request);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00016602 File Offset: 0x00014802
		public void SaveLookupTimetableItems(SaveLookupTimetableItemsReq Request)
		{
			base.Channel.SaveLookupTimetableItems(Request);
		}
	}
}
