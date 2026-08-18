using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000BA RID: 186
	internal class InventoryLocationClientBaseProxy : ClientBase<IInventoryLocation>, IInventoryLocation, IService
	{
		// Token: 0x0600075E RID: 1886 RVA: 0x00013944 File Offset: 0x00011B44
		public InventoryLocationClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001394F File Offset: 0x00011B4F
		public InventoryLocationClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001395C File Offset: 0x00011B5C
		public CreateLocationResp CreateLocation(CreateLocationReq request)
		{
			return base.Channel.CreateLocation(request);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001397C File Offset: 0x00011B7C
		public GetLocationByIdResp GetLocationById(GetLocationByIdReq request)
		{
			return base.Channel.GetLocationById(request);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001399C File Offset: 0x00011B9C
		public GetAllLocationsResp GetAllLocations(GetAllLocationsReq request)
		{
			return base.Channel.GetAllLocations(request);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000139BC File Offset: 0x00011BBC
		public GetLocationsResp GetLocations(GetLocationsReq request)
		{
			return base.Channel.GetLocations(request);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000139DC File Offset: 0x00011BDC
		public LocationInUseResp LocationInUse(LocationInUseReq request)
		{
			return base.Channel.LocationInUse(request);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000139FC File Offset: 0x00011BFC
		public DeleteLocationResp DeleteLocation(DeleteLocationReq request)
		{
			return base.Channel.DeleteLocation(request);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00013A1C File Offset: 0x00011C1C
		public UpdateLocationResp UpdateLocation(UpdateLocationReq request)
		{
			return base.Channel.UpdateLocation(request);
		}
	}
}
