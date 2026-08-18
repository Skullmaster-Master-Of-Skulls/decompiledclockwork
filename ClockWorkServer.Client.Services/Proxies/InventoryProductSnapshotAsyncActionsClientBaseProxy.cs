using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000AA RID: 170
	internal class InventoryProductSnapshotAsyncActionsClientBaseProxy : ClientBase<IInventoryProductSnapshotAsyncActions>, IInventoryProductSnapshotAsyncActions, IService
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x0001238D File Offset: 0x0001058D
		public InventoryProductSnapshotAsyncActionsClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00012398 File Offset: 0x00010598
		public InventoryProductSnapshotAsyncActionsClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000123A4 File Offset: 0x000105A4
		public void SaveAsPointOfContact(SaveAsPointOfContactReq request)
		{
			base.Channel.SaveAsPointOfContact(request);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000123B4 File Offset: 0x000105B4
		public void SaveListAsPointOfContact(SaveListAsPointOfContactReq request)
		{
			base.Channel.SaveListAsPointOfContact(request);
		}
	}
}
