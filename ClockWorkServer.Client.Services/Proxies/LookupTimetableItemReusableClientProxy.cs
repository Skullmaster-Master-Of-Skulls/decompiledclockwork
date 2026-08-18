using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000DB RID: 219
	public class LookupTimetableItemReusableClientProxy : WCFTokenBasedReusableClientProxy<ILookupTimetableItem>, ILookupTimetableItem, IService
	{
		// Token: 0x06000893 RID: 2195 RVA: 0x00016546 File Offset: 0x00014746
		public LookupTimetableItemReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00016551 File Offset: 0x00014751
		public LookupTimetableItemReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00016560 File Offset: 0x00014760
		public LoadLookupTimetableItemResp LoadLookupTimetableItem(LoadLookupTimetableItemReq Request)
		{
			return this.WrapServiceMethod<LoadLookupTimetableItemResp>(() => this.Proxy.LoadLookupTimetableItem(Request));
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00016598 File Offset: 0x00014798
		public void SaveLookupTimetableItems(SaveLookupTimetableItemsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveLookupTimetableItems(Request);
			});
		}
	}
}
