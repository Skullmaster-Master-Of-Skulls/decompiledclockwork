using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Inventory
{
	// Token: 0x02000056 RID: 86
	public class InventoryLocationClientManager : IInventoryLocationClientManager, IWebService
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000D098 File Offset: 0x0000B298
		public int CreateLocation(InventoryLocationDTO location)
		{
			CreateLocationReq createLocationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateLocationReq>();
			createLocationReq.Location = location;
			return ClientServiceFactory.GetClientInstance<IInventoryLocation>().CreateLocation(createLocationReq).LocationId;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
		public InventoryLocationDTO GetLocationById(int locationId)
		{
			GetLocationByIdReq getLocationByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetLocationByIdReq>();
			getLocationByIdReq.LocationId = locationId;
			return ClientServiceFactory.GetClientInstance<IInventoryLocation>().GetLocationById(getLocationByIdReq).Location;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000D108 File Offset: 0x0000B308
		public IList<InventoryLocationDTO> GetAllLocations()
		{
			GetAllLocationsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAllLocationsReq>();
			return ClientServiceFactory.GetClientInstance<IInventoryLocation>().GetAllLocations(request).Locations;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000D138 File Offset: 0x0000B338
		public IList<InventoryLocationDTO> GetLocations(string includingText)
		{
			GetLocationsReq getLocationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetLocationsReq>();
			getLocationsReq.SearchingText = includingText;
			return ClientServiceFactory.GetClientInstance<IInventoryLocation>().GetLocations(getLocationsReq).Locations;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000D170 File Offset: 0x0000B370
		public bool LocationInUse(int locationId)
		{
			LocationInUseReq locationInUseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LocationInUseReq>();
			locationInUseReq.LocationId = locationId;
			return ClientServiceFactory.GetClientInstance<IInventoryLocation>().LocationInUse(locationInUseReq).InUse;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		public bool DeleteLocation(int locationId)
		{
			DeleteLocationReq deleteLocationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteLocationReq>();
			deleteLocationReq.LocationId = locationId;
			return ClientServiceFactory.GetClientInstance<IInventoryLocation>().DeleteLocation(deleteLocationReq).WasDeleted;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		public void UpdateLocation(InventoryLocationDTO location)
		{
			UpdateLocationReq updateLocationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateLocationReq>();
			updateLocationReq.Location = location;
			ClientServiceFactory.GetClientInstance<IInventoryLocation>().UpdateLocation(updateLocationReq);
		}
	}
}
