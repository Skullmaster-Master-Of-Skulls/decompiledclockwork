using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000B9 RID: 185
	public class InventoryLocationReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryLocation>, IInventoryLocation, IService
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x000137A2 File Offset: 0x000119A2
		public InventoryLocationReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000137AD File Offset: 0x000119AD
		public InventoryLocationReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000137BC File Offset: 0x000119BC
		public CreateLocationResp CreateLocation(CreateLocationReq request)
		{
			return this.WrapServiceMethod<CreateLocationResp>(() => this.Proxy.CreateLocation(request));
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000137F4 File Offset: 0x000119F4
		public GetLocationByIdResp GetLocationById(GetLocationByIdReq request)
		{
			return this.WrapServiceMethod<GetLocationByIdResp>(() => this.Proxy.GetLocationById(request));
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001382C File Offset: 0x00011A2C
		public GetAllLocationsResp GetAllLocations(GetAllLocationsReq request)
		{
			return this.WrapServiceMethod<GetAllLocationsResp>(() => this.Proxy.GetAllLocations(request));
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00013864 File Offset: 0x00011A64
		public GetLocationsResp GetLocations(GetLocationsReq request)
		{
			return this.WrapServiceMethod<GetLocationsResp>(() => this.Proxy.GetLocations(request));
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001389C File Offset: 0x00011A9C
		public LocationInUseResp LocationInUse(LocationInUseReq reequest)
		{
			return this.WrapServiceMethod<LocationInUseResp>(() => this.Proxy.LocationInUse(reequest));
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000138D4 File Offset: 0x00011AD4
		public DeleteLocationResp DeleteLocation(DeleteLocationReq request)
		{
			return this.WrapServiceMethod<DeleteLocationResp>(() => this.Proxy.DeleteLocation(request));
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001390C File Offset: 0x00011B0C
		public UpdateLocationResp UpdateLocation(UpdateLocationReq request)
		{
			return this.WrapServiceMethod<UpdateLocationResp>(() => this.Proxy.UpdateLocation(request));
		}
	}
}
