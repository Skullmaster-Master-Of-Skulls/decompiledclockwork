using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Core.Inventory;
using TechnoPro.Common.Core.Mappers.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000053 RID: 83
	public class InventoryLocationServiceManager : IInventoryLocation, IService
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0000EF54 File Offset: 0x0000D154
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000EF68 File Offset: 0x0000D168
		public CreateLocationResp CreateLocation(CreateLocationReq request)
		{
			IInventoryLocationManager inventoryLocationManager = new InventoryLocationManager(request.GetOperationContext());
			return new CreateLocationResp
			{
				LocationId = inventoryLocationManager.CreateLocation(request.Location.ToDomainObject())
			};
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		public GetLocationByIdResp GetLocationById(GetLocationByIdReq request)
		{
			IInventoryLocationManager inventoryLocationManager = new InventoryLocationManager(request.GetOperationContext());
			return new GetLocationByIdResp
			{
				Location = inventoryLocationManager.GetLocationById(request.LocationId).ToDTO()
			};
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		public GetAllLocationsResp GetAllLocations(GetAllLocationsReq request)
		{
			IInventoryLocationManager inventoryLocationManager = new InventoryLocationManager(request.GetOperationContext());
			return new GetAllLocationsResp
			{
				Locations = inventoryLocationManager.GetAllLocations().ToDTO()
			};
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000F018 File Offset: 0x0000D218
		public GetLocationsResp GetLocations(GetLocationsReq request)
		{
			IInventoryLocationManager inventoryLocationManager = new InventoryLocationManager(request.GetOperationContext());
			return new GetLocationsResp
			{
				Locations = inventoryLocationManager.GetLocations(request.SearchingText).ToDTO()
			};
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000F054 File Offset: 0x0000D254
		public LocationInUseResp LocationInUse(LocationInUseReq request)
		{
			IInventoryLocationManager inventoryLocationManager = new InventoryLocationManager(request.GetOperationContext());
			return new LocationInUseResp
			{
				InUse = inventoryLocationManager.LocationInUse(request.LocationId)
			};
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000F08C File Offset: 0x0000D28C
		public DeleteLocationResp DeleteLocation(DeleteLocationReq request)
		{
			IInventoryLocationManager inventoryLocationManager = new InventoryLocationManager(request.GetOperationContext());
			return new DeleteLocationResp
			{
				WasDeleted = inventoryLocationManager.DeleteLocation(request.LocationId)
			};
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		public UpdateLocationResp UpdateLocation(UpdateLocationReq request)
		{
			IInventoryLocationManager inventoryLocationManager = new InventoryLocationManager(request.GetOperationContext());
			inventoryLocationManager.UpdateLocation(request.Location.ToDomainObject());
			return new UpdateLocationResp();
		}
	}
}
