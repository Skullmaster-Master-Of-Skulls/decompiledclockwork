using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000A9 RID: 169
	public class InventoryProductSnapshotAsyncActionsReusableClientProxy : WCFReusableClientProxy<IInventoryProductSnapshotAsyncActions>, IInventoryProductSnapshotAsyncActions, IService
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x00012306 File Offset: 0x00010506
		public InventoryProductSnapshotAsyncActionsReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00012311 File Offset: 0x00010511
		public InventoryProductSnapshotAsyncActionsReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00012320 File Offset: 0x00010520
		public void SaveAsPointOfContact(SaveAsPointOfContactReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveAsPointOfContact(request);
			});
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00012358 File Offset: 0x00010558
		public void SaveListAsPointOfContact(SaveListAsPointOfContactReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveListAsPointOfContact(request);
			});
		}
	}
}
